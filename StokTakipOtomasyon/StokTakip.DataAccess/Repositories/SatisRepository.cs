using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StokTakip.DataAccess.Context;
using StokTakip.DataAccess.Models;

namespace StokTakip.DataAccess.Repositories
{
    public class SatisRepository
    {
        /// <summary>
        /// Yeni satış numarası oluşturur (FS2025-00001 formatında)
        /// </summary>
        public string GenerateSatisNo()
        {
            string query = "SELECT COUNT(*) FROM Satislar WHERE YEAR(SatisTarihi) = YEAR(GETDATE())";
            int count = Convert.ToInt32(SqlHelper.ExecuteScalar(query));
            return $"FS{DateTime.Now.Year}-{(count + 1).ToString("D5")}";
        }

        /// <summary>
        /// Tüm satışları getirir
        /// </summary>
        public List<Satis> GetAll()
        {
            string query = @"
                SELECT s.*, k.AdSoyad as PersonelAdi
                FROM Satislar s
                INNER JOIN Kullanicilar k ON s.SaticPersonelID = k.KullaniciID
                ORDER BY s.SatisTarihi DESC";

            DataTable dt = SqlHelper.ExecuteQuery(query);

            List<Satis> satislar = new List<Satis>();
            foreach (DataRow row in dt.Rows)
            {
                satislar.Add(MapToSatis(row));
            }

            return satislar;
        }

        /// <summary>
        /// Tarih aralığına göre satışları getirir
        /// </summary>
        public List<Satis> GetByDateRange(DateTime baslangic, DateTime bitis)
        {
            string query = @"
                SELECT s.*, k.AdSoyad as PersonelAdi
                FROM Satislar s
                INNER JOIN Kullanicilar k ON s.SaticPersonelID = k.KullaniciID
                WHERE s.SatisTarihi BETWEEN @Baslangic AND @Bitis
                ORDER BY s.SatisTarihi DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@Baslangic", baslangic),
                new SqlParameter("@Bitis", bitis)
            };

            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            List<Satis> satislar = new List<Satis>();
            foreach (DataRow row in dt.Rows)
            {
                satislar.Add(MapToSatis(row));
            }

            return satislar;
        }

        /// <summary>
        /// Bugünkü satışları getirir
        /// </summary>
        public List<Satis> GetToday()
        {
            string query = @"
                SELECT s.*, k.AdSoyad as PersonelAdi
                FROM Satislar s
                INNER JOIN Kullanicilar k ON s.SaticPersonelID = k.KullaniciID
                WHERE CAST(s.SatisTarihi AS DATE) = CAST(GETDATE() AS DATE)
                ORDER BY s.SatisTarihi DESC";

            DataTable dt = SqlHelper.ExecuteQuery(query);

            List<Satis> satislar = new List<Satis>();
            foreach (DataRow row in dt.Rows)
            {
                satislar.Add(MapToSatis(row));
            }

            return satislar;
        }

        /// <summary>
        /// ID'ye göre satış getirir
        /// </summary>
        public Satis GetById(int id)
        {
            string query = @"
                SELECT s.*, k.AdSoyad as PersonelAdi
                FROM Satislar s
                INNER JOIN Kullanicilar k ON s.SaticPersonelID = k.KullaniciID
                WHERE s.SatisID = @SatisID";

            SqlParameter[] parameters = { new SqlParameter("@SatisID", id) };
            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
                return MapToSatis(dt.Rows[0]);

            return null;
        }

        /// <summary>
        /// Satış detaylarını getirir
        /// </summary>
        public List<SatisDetay> GetDetaylar(int satisId)
        {
            string query = @"
                SELECT sd.*, u.UrunAdi, u.BarkodNo as UrunBarkod
                FROM SatisDetaylari sd
                INNER JOIN Urunler u ON sd.UrunID = u.UrunID
                WHERE sd.SatisID = @SatisID";

            SqlParameter[] parameters = { new SqlParameter("@SatisID", satisId) };
            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            List<SatisDetay> detaylar = new List<SatisDetay>();
            foreach (DataRow row in dt.Rows)
            {
                detaylar.Add(new SatisDetay
                {
                    DetayID = Convert.ToInt32(row["DetayID"]),
                    SatisID = Convert.ToInt32(row["SatisID"]),
                    UrunID = Convert.ToInt32(row["UrunID"]),
                    Miktar = Convert.ToInt32(row["Miktar"]),
                    BirimFiyat = Convert.ToDecimal(row["BirimFiyat"]),
                    ToplamFiyat = Convert.ToDecimal(row["ToplamFiyat"]),
                    AlisFiyati = row["AlisFiyati"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["AlisFiyati"]) : null,
                    UrunAdi = row["UrunAdi"].ToString(),
                    UrunBarkod = row["UrunBarkod"] != DBNull.Value ? row["UrunBarkod"].ToString() : null
                });
            }

            return detaylar;
        }

        /// <summary>
        /// Yeni satış ekler (Transaction ile stok güncelleme ve hareket kaydı)
        /// </summary>
        public int Insert(Satis satis, List<SatisDetay> detaylar, int personelId)
        {
            int satisId = 0;

            SqlHelper.ExecuteTransaction((connection, transaction) =>
            {
                // 1. Satış kaydını oluştur
                string insertSatisQuery = @"
                    INSERT INTO Satislar 
                    (SatisNo, SaticPersonelID, MusteriAdSoyad, MusteriTelefon, ToplamTutar, OdenenTutar, 
                     KalanTutar, OdemeYontemi, IndirimOrani, IndirimTutari, KDVOrani, NetKar, Durum)
                    VALUES 
                    (@SatisNo, @SaticPersonelID, @MusteriAdSoyad, @MusteriTelefon, @ToplamTutar, @OdenenTutar,
                     @KalanTutar, @OdemeYontemi, @IndirimOrani, @IndirimTutari, @KDVOrani, @NetKar, @Durum);
                    SELECT SCOPE_IDENTITY();";

                using (SqlCommand cmd = new SqlCommand(insertSatisQuery, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@SatisNo", satis.SatisNo);
                    cmd.Parameters.AddWithValue("@SaticPersonelID", satis.SaticPersonelID);
                    cmd.Parameters.AddWithValue("@MusteriAdSoyad", (object)satis.MusteriAdSoyad ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@MusteriTelefon", (object)satis.MusteriTelefon ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ToplamTutar", satis.ToplamTutar);
                    cmd.Parameters.AddWithValue("@OdenenTutar", satis.OdenenTutar);
                    cmd.Parameters.AddWithValue("@KalanTutar", satis.KalanTutar);
                    cmd.Parameters.AddWithValue("@OdemeYontemi", (object)satis.OdemeYontemi ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@IndirimOrani", satis.IndirimOrani);
                    cmd.Parameters.AddWithValue("@IndirimTutari", satis.IndirimTutari);
                    cmd.Parameters.AddWithValue("@KDVOrani", satis.KDVOrani);
                    cmd.Parameters.AddWithValue("@NetKar", (object)satis.NetKar ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@Durum", satis.Durum);
                    
                    satisId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // 2. Satış detaylarını ekle ve stokları güncelle
                foreach (var detay in detaylar)
                {
                    // Satış detayını ekle
                    string insertDetayQuery = @"
                        INSERT INTO SatisDetaylari 
                        (SatisID, UrunID, Miktar, BirimFiyat, ToplamFiyat, AlisFiyati)
                        VALUES 
                        (@SatisID, @UrunID, @Miktar, @BirimFiyat, @ToplamFiyat, @AlisFiyati)";

                    using (SqlCommand cmd = new SqlCommand(insertDetayQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@SatisID", satisId);
                        cmd.Parameters.AddWithValue("@UrunID", detay.UrunID);
                        cmd.Parameters.AddWithValue("@Miktar", detay.Miktar);
                        cmd.Parameters.AddWithValue("@BirimFiyat", detay.BirimFiyat);
                        cmd.Parameters.AddWithValue("@ToplamFiyat", detay.ToplamFiyat);
                        cmd.Parameters.AddWithValue("@AlisFiyati", (object)detay.AlisFiyati ?? DBNull.Value);
                        cmd.ExecuteNonQuery();
                    }

                    // Mevcut stok miktarını al
                    string getStokQuery = "SELECT StokMiktari FROM Urunler WHERE UrunID = @UrunID";
                    int mevcutStok = 0;
                    using (SqlCommand cmd = new SqlCommand(getStokQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UrunID", detay.UrunID);
                        mevcutStok = Convert.ToInt32(cmd.ExecuteScalar());
                    }

                    // Stok hareketini kaydet
                    string insertHareketQuery = @"
                        INSERT INTO StokHareketleri 
                        (UrunID, HareketTipi, Miktar, BirimFiyat, ToplamTutar, OncekiStok, YeniStok, PersonelID, Aciklama)
                        VALUES 
                        (@UrunID, 'Cikis', @Miktar, @BirimFiyat, @ToplamTutar, @OncekiStok, @YeniStok, @PersonelID, @Aciklama)";

                    using (SqlCommand cmd = new SqlCommand(insertHareketQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@UrunID", detay.UrunID);
                        cmd.Parameters.AddWithValue("@Miktar", detay.Miktar);
                        cmd.Parameters.AddWithValue("@BirimFiyat", detay.BirimFiyat);
                        cmd.Parameters.AddWithValue("@ToplamTutar", detay.ToplamFiyat);
                        cmd.Parameters.AddWithValue("@OncekiStok", mevcutStok);
                        cmd.Parameters.AddWithValue("@YeniStok", mevcutStok - detay.Miktar);
                        cmd.Parameters.AddWithValue("@PersonelID", personelId);
                        cmd.Parameters.AddWithValue("@Aciklama", $"Satış No: {satis.SatisNo}");
                        cmd.ExecuteNonQuery();
                    }

                    // Ürün stoğunu güncelle
                    string updateStokQuery = @"
                        UPDATE Urunler SET 
                            StokMiktari = StokMiktari - @Miktar,
                            GuncellemeTarihi = GETDATE()
                        WHERE UrunID = @UrunID";

                    using (SqlCommand cmd = new SqlCommand(updateStokQuery, connection, transaction))
                    {
                        cmd.Parameters.AddWithValue("@Miktar", detay.Miktar);
                        cmd.Parameters.AddWithValue("@UrunID", detay.UrunID);
                        cmd.ExecuteNonQuery();
                    }
                }
            });

            return satisId;
        }

        /// <summary>
        /// Bugünkü toplam satış tutarını döner
        /// </summary>
        public decimal GetTodayTotal()
        {
            string query = @"
                SELECT ISNULL(SUM(ToplamTutar), 0) 
                FROM Satislar 
                WHERE CAST(SatisTarihi AS DATE) = CAST(GETDATE() AS DATE) 
                AND Durum = 'Tamamlandi'";

            object result = SqlHelper.ExecuteScalar(query);
            return Convert.ToDecimal(result);
        }

        /// <summary>
        /// Bugünkü toplam kar tutarını döner
        /// </summary>
        public decimal GetTodayProfit()
        {
            string query = @"
                SELECT ISNULL(SUM(NetKar), 0) 
                FROM Satislar 
                WHERE CAST(SatisTarihi AS DATE) = CAST(GETDATE() AS DATE) 
                AND Durum = 'Tamamlandi'";

            object result = SqlHelper.ExecuteScalar(query);
            return Convert.ToDecimal(result);
        }

        /// <summary>
        /// Aylık satış istatistiklerini getirir
        /// </summary>
        public DataTable GetMonthlySalesStats(int year)
        {
            string query = @"
                SELECT 
                    MONTH(SatisTarihi) as Ay,
                    COUNT(*) as SatisSayisi,
                    SUM(ToplamTutar) as ToplamCiro,
                    SUM(NetKar) as ToplamKar
                FROM Satislar
                WHERE YEAR(SatisTarihi) = @Year AND Durum = 'Tamamlandi'
                GROUP BY MONTH(SatisTarihi)
                ORDER BY MONTH(SatisTarihi)";

            SqlParameter[] parameters = { new SqlParameter("@Year", year) };
            return SqlHelper.ExecuteQuery(query, parameters);
        }

        /// <summary>
        /// DataRow'u Satis nesnesine dönüştürür
        /// </summary>
        private Satis MapToSatis(DataRow row)
        {
            return new Satis
            {
                SatisID = Convert.ToInt32(row["SatisID"]),
                SatisNo = row["SatisNo"].ToString(),
                SaticPersonelID = Convert.ToInt32(row["SaticPersonelID"]),
                MusteriAdSoyad = row["MusteriAdSoyad"] != DBNull.Value ? row["MusteriAdSoyad"].ToString() : null,
                MusteriTelefon = row["MusteriTelefon"] != DBNull.Value ? row["MusteriTelefon"].ToString() : null,
                ToplamTutar = Convert.ToDecimal(row["ToplamTutar"]),
                OdenenTutar = Convert.ToDecimal(row["OdenenTutar"]),
                KalanTutar = Convert.ToDecimal(row["KalanTutar"]),
                OdemeYontemi = row["OdemeYontemi"] != DBNull.Value ? row["OdemeYontemi"].ToString() : null,
                IndirimOrani = Convert.ToDecimal(row["IndirimOrani"]),
                IndirimTutari = Convert.ToDecimal(row["IndirimTutari"]),
                KDVOrani = Convert.ToDecimal(row["KDVOrani"]),
                NetKar = row["NetKar"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["NetKar"]) : null,
                Durum = row["Durum"].ToString(),
                SatisTarihi = Convert.ToDateTime(row["SatisTarihi"]),
                // Navigation Properties
                PersonelAdi = row["PersonelAdi"].ToString()
            };
        }
    }
}
