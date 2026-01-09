using System;
using System.Data;
using System.Data.SqlClient;
using StokTakip.DataAccess.Context;

namespace StokTakip.Business.Services
{
    public class RaporService
    {
        // Gunluk satis raporu
        public DataTable GetGunlukSatisRaporu(DateTime baslangic, DateTime bitis)
        {
            string query = @"
                SELECT 
                    CONVERT(DATE, s.SatisTarihi) as Tarih,
                    COUNT(s.SatisID) as SatisSayisi,
                    SUM(s.ToplamTutar) as ToplamCiro
                FROM Satislar s
                WHERE s.SatisTarihi BETWEEN @Baslangic AND @Bitis
                GROUP BY CONVERT(DATE, s.SatisTarihi)
                ORDER BY Tarih";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Baslangic", baslangic),
                new SqlParameter("@Bitis", bitis.AddDays(1).AddSeconds(-1))
            };

            return SqlHelper.ExecuteQuery(query, parameters);
        }

        // En cok satan urunler
        public DataTable GetEnCokSatanUrunler(int top = 10)
        {
            string query = @"
                SELECT TOP (@Top)
                    u.UrunAdi,
                    SUM(sd.Miktar) as ToplamSatisMiktari,
                    SUM(sd.BirimFiyat * sd.Miktar) as ToplamCiro
                FROM SatisDetaylari sd
                INNER JOIN Urunler u ON sd.UrunID = u.UrunID
                GROUP BY u.UrunAdi
                ORDER BY ToplamSatisMiktari DESC";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Top", top)
            };

            return SqlHelper.ExecuteQuery(query, parameters);
        }

        // Kategori bazli satis raporu
        public DataTable GetKategoriyeGoreSatislar()
        {
            string query = @"
                SELECT 
                    k.KategoriAdi,
                    COUNT(DISTINCT sd.SatisID) as SatisSayisi,
                    SUM(sd.Miktar) as ToplamMiktar,
                    SUM(sd.BirimFiyat * sd.Miktar) as ToplamCiro
                FROM SatisDetaylari sd
                INNER JOIN Urunler u ON sd.UrunID = u.UrunID
                INNER JOIN Kategoriler k ON u.KategoriID = k.KategoriID
                GROUP BY k.KategoriAdi
                ORDER BY ToplamCiro DESC";

            return SqlHelper.ExecuteQuery(query, null);
        }

        // Stok durum raporu
        public DataTable GetStokDurumRaporu()
        {
            string query = @"
                SELECT 
                    u.UrunAdi,
                    u.StokMiktari,
                    u.MinimumStok,
                    CASE 
                        WHEN u.StokMiktari <= u.MinimumStok THEN 'Kritik'
                        WHEN u.StokMiktari <= (u.MinimumStok * 2) THEN 'Dusuk'
                        ELSE 'Normal'
                    END as Durum,
                    k.KategoriAdi
                FROM Urunler u
                INNER JOIN Kategoriler k ON u.KategoriID = k.KategoriID
                WHERE u.Aktif = 1
                ORDER BY u.StokMiktari ASC";

            return SqlHelper.ExecuteQuery(query, null);
        }

        // Aylik satis trendi
        public DataTable GetAylikSatisTrendi(int yil)
        {
            string query = @"
                SELECT 
                    MONTH(s.SatisTarihi) as Ay,
                    COUNT(s.SatisID) as SatisSayisi,
                    SUM(s.ToplamTutar) as ToplamCiro
                FROM Satislar s
                WHERE YEAR(s.SatisTarihi) = @Yil
                GROUP BY MONTH(s.SatisTarihi)
                ORDER BY Ay";

            var parameters = new SqlParameter[]
            {
                new SqlParameter("@Yil", yil)
            };

            return SqlHelper.ExecuteQuery(query, parameters);
        }

        // Kritik stok sayisi
        public int GetKritikStokSayisi()
        {
            string query = @"
                SELECT COUNT(*) 
                FROM Urunler 
                WHERE Aktif = 1 AND StokMiktari <= MinimumStok";

            var result = SqlHelper.ExecuteScalar(query, null);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        // Bugun satis sayisi
        public int GetBugunSatisSayisi()
        {
            string query = @"
                SELECT COUNT(*) 
                FROM Satislar 
                WHERE CONVERT(DATE, SatisTarihi) = CONVERT(DATE, GETDATE())";

            var result = SqlHelper.ExecuteScalar(query, null);
            return result != null ? Convert.ToInt32(result) : 0;
        }

        // Bugun toplam ciro
        public decimal GetBugunToplamCiro()
        {
            string query = @"
                SELECT ISNULL(SUM(ToplamTutar), 0)
                FROM Satislar 
                WHERE CONVERT(DATE, SatisTarihi) = CONVERT(DATE, GETDATE())";

            var result = SqlHelper.ExecuteScalar(query, null);
            return result != null ? Convert.ToDecimal(result) : 0;
        }

        // Toplam urun sayisi
        public int GetToplamUrunSayisi()
        {
            string query = @"
                SELECT COUNT(*) 
                FROM Urunler 
                WHERE Aktif = 1";

            var result = SqlHelper.ExecuteScalar(query, null);
            return result != null ? Convert.ToInt32(result) : 0;
        }
    }
}
