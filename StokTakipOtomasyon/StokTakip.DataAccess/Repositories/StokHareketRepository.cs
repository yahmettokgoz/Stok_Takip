using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StokTakip.DataAccess.Context;
using StokTakip.DataAccess.Models;

namespace StokTakip.DataAccess.Repositories
{
    public class StokHareketRepository
    {
        /// <summary>
        /// Tüm stok hareketlerini getirir
        /// </summary>
        public List<StokHareket> GetAll()
        {
            string query = @"
                SELECT sh.*, u.UrunAdi, k.AdSoyad as PersonelAdi
                FROM StokHareketleri sh
                INNER JOIN Urunler u ON sh.UrunID = u.UrunID
                INNER JOIN Kullanicilar k ON sh.PersonelID = k.KullaniciID
                ORDER BY sh.HareketTarihi DESC";

            DataTable dt = SqlHelper.ExecuteQuery(query);

            List<StokHareket> hareketler = new List<StokHareket>();
            foreach (DataRow row in dt.Rows)
            {
                hareketler.Add(MapToStokHareket(row));
            }

            return hareketler;
        }

        /// <summary>
        /// Ürüne göre stok hareketlerini getirir
        /// </summary>
        public List<StokHareket> GetByUrun(int urunId)
        {
            string query = @"
                SELECT sh.*, u.UrunAdi, k.AdSoyad as PersonelAdi
                FROM StokHareketleri sh
                INNER JOIN Urunler u ON sh.UrunID = u.UrunID
                INNER JOIN Kullanicilar k ON sh.PersonelID = k.KullaniciID
                WHERE sh.UrunID = @UrunID
                ORDER BY sh.HareketTarihi DESC";

            SqlParameter[] parameters = { new SqlParameter("@UrunID", urunId) };
            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            List<StokHareket> hareketler = new List<StokHareket>();
            foreach (DataRow row in dt.Rows)
            {
                hareketler.Add(MapToStokHareket(row));
            }

            return hareketler;
        }

        /// <summary>
        /// Tarih aralığına göre stok hareketlerini getirir
        /// </summary>
        public List<StokHareket> GetByDateRange(DateTime baslangic, DateTime bitis)
        {
            string query = @"
                SELECT sh.*, u.UrunAdi, k.AdSoyad as PersonelAdi
                FROM StokHareketleri sh
                INNER JOIN Urunler u ON sh.UrunID = u.UrunID
                INNER JOIN Kullanicilar k ON sh.PersonelID = k.KullaniciID
                WHERE sh.HareketTarihi BETWEEN @Baslangic AND @Bitis
                ORDER BY sh.HareketTarihi DESC";

            SqlParameter[] parameters = {
                new SqlParameter("@Baslangic", baslangic),
                new SqlParameter("@Bitis", bitis)
            };

            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            List<StokHareket> hareketler = new List<StokHareket>();
            foreach (DataRow row in dt.Rows)
            {
                hareketler.Add(MapToStokHareket(row));
            }

            return hareketler;
        }

        /// <summary>
        /// Stok hareketi ekler (Transaction ile ürün stoğunu günceller)
        /// </summary>
        public bool Insert(StokHareket hareket)
        {
            return SqlHelper.ExecuteTransaction((connection, transaction) =>
            {
                // 1. Stok hareketini kaydet
                string insertQuery = @"
                    INSERT INTO StokHareketleri 
                    (UrunID, HareketTipi, Miktar, BirimFiyat, ToplamTutar, OncekiStok, YeniStok, PersonelID, Aciklama)
                    VALUES 
                    (@UrunID, @HareketTipi, @Miktar, @BirimFiyat, @ToplamTutar, @OncekiStok, @YeniStok, @PersonelID, @Aciklama)";

                using (SqlCommand cmd = new SqlCommand(insertQuery, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@UrunID", hareket.UrunID);
                    cmd.Parameters.AddWithValue("@HareketTipi", hareket.HareketTipi);
                    cmd.Parameters.AddWithValue("@Miktar", hareket.Miktar);
                    cmd.Parameters.AddWithValue("@BirimFiyat", (object)hareket.BirimFiyat ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@ToplamTutar", (object)hareket.ToplamTutar ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@OncekiStok", hareket.OncekiStok);
                    cmd.Parameters.AddWithValue("@YeniStok", hareket.YeniStok);
                    cmd.Parameters.AddWithValue("@PersonelID", hareket.PersonelID);
                    cmd.Parameters.AddWithValue("@Aciklama", (object)hareket.Aciklama ?? DBNull.Value);
                    cmd.ExecuteNonQuery();
                }

                // 2. Ürün stoğunu güncelle
                string updateQuery = "UPDATE Urunler SET StokMiktari = @YeniStok WHERE UrunID = @UrunID";
                using (SqlCommand cmd = new SqlCommand(updateQuery, connection, transaction))
                {
                    cmd.Parameters.AddWithValue("@YeniStok", hareket.YeniStok);
                    cmd.Parameters.AddWithValue("@UrunID", hareket.UrunID);
                    cmd.ExecuteNonQuery();
                }
            });
        }

        /// <summary>
        /// DataRow'u StokHareket nesnesine dönüştürür
        /// </summary>
        private StokHareket MapToStokHareket(DataRow row)
        {
            return new StokHareket
            {
                HareketID = Convert.ToInt32(row["HareketID"]),
                UrunID = Convert.ToInt32(row["UrunID"]),
                HareketTipi = row["HareketTipi"].ToString(),
                Miktar = Convert.ToInt32(row["Miktar"]),
                BirimFiyat = row["BirimFiyat"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["BirimFiyat"]) : null,
                ToplamTutar = row["ToplamTutar"] != DBNull.Value ? (decimal?)Convert.ToDecimal(row["ToplamTutar"]) : null,
                OncekiStok = Convert.ToInt32(row["OncekiStok"]),
                YeniStok = Convert.ToInt32(row["YeniStok"]),
                PersonelID = Convert.ToInt32(row["PersonelID"]),
                Aciklama = row["Aciklama"] != DBNull.Value ? row["Aciklama"].ToString() : null,
                HareketTarihi = Convert.ToDateTime(row["HareketTarihi"]),
                // Navigation Properties
                UrunAdi = row["UrunAdi"].ToString(),
                PersonelAdi = row["PersonelAdi"].ToString()
            };
        }

        /// <summary>
        /// Filtrelere göre stok hareketlerini getirir
        /// </summary>
        public List<StokHareket> GetHareketlerFiltered(DateTime baslangic, DateTime bitis, int? urunID, string hareketTipi)
        {
            string query = @"
                SELECT 
                    sh.*, 
                    u.UrunAdi,
                    k.KullaniciAdi as PersonelAdi
                FROM StokHareketleri sh
                INNER JOIN Urunler u ON sh.UrunID = u.UrunID
                INNER JOIN Kullanicilar k ON sh.PersonelID = k.KullaniciID
                WHERE sh.HareketTarihi BETWEEN @Baslangic AND @Bitis";

            if (urunID.HasValue)
                query += " AND sh.UrunID = @UrunID";

            if (!string.IsNullOrEmpty(hareketTipi))
                query += " AND sh.HareketTipi = @HareketTipi";

            query += " ORDER BY sh.HareketTarihi DESC";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@Baslangic", baslangic),
                new SqlParameter("@Bitis", bitis),
                new SqlParameter("@UrunID", (object)urunID ?? DBNull.Value),
                new SqlParameter("@HareketTipi", (object)hareketTipi ?? DBNull.Value)
            };

            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);
            List<StokHareket> hareketler = new List<StokHareket>();

            foreach (DataRow row in dt.Rows)
            {
                hareketler.Add(MapToStokHareket(row));
            }

            return hareketler;
        }
    }
}
