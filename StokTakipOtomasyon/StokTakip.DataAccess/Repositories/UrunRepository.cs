using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StokTakip.DataAccess.Context;
using StokTakip.DataAccess.Models;

namespace StokTakip.DataAccess.Repositories
{
    public class UrunRepository
    {
        /// <summary>
        /// Tüm ürünleri kategori bilgileriyle birlikte getirir
        /// </summary>
        public List<Urun> GetAll()
        {
            string query = @"
                SELECT u.*, k.KategoriAdi, k.Renk as KategoriRenk
                FROM Urunler u
                INNER JOIN Kategoriler k ON u.KategoriID = k.KategoriID
                WHERE u.Aktif = 1
                ORDER BY u.UrunAdi";

            DataTable dt = SqlHelper.ExecuteQuery(query);

            List<Urun> urunler = new List<Urun>();
            foreach (DataRow row in dt.Rows)
            {
                urunler.Add(MapToUrun(row));
            }

            return urunler;
        }

        /// <summary>
        /// Kategoriye göre ürünleri getirir
        /// </summary>
        public List<Urun> GetByCategory(int kategoriId)
        {
            string query = @"
                SELECT u.*, k.KategoriAdi, k.Renk as KategoriRenk
                FROM Urunler u
                INNER JOIN Kategoriler k ON u.KategoriID = k.KategoriID
                WHERE u.KategoriID = @KategoriID AND u.Aktif = 1
                ORDER BY u.UrunAdi";

            SqlParameter[] parameters = { new SqlParameter("@KategoriID", kategoriId) };
            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            List<Urun> urunler = new List<Urun>();
            foreach (DataRow row in dt.Rows)
            {
                urunler.Add(MapToUrun(row));
            }

            return urunler;
        }

        /// <summary>
        /// Kritik stok seviyesindeki ürünleri getirir
        /// </summary>
        public List<Urun> GetCriticalStock()
        {
            string query = @"
                SELECT u.*, k.KategoriAdi, k.Renk as KategoriRenk
                FROM Urunler u
                INNER JOIN Kategoriler k ON u.KategoriID = k.KategoriID
                WHERE u.StokMiktari <= u.MinimumStok AND u.Aktif = 1
                ORDER BY u.StokMiktari";

            DataTable dt = SqlHelper.ExecuteQuery(query);

            List<Urun> urunler = new List<Urun>();
            foreach (DataRow row in dt.Rows)
            {
                urunler.Add(MapToUrun(row));
            }

            return urunler;
        }

        /// <summary>
        /// Ürün adına göre arama yapar
        /// </summary>
        public List<Urun> Search(string searchText)
        {
            string query = @"
                SELECT u.*, k.KategoriAdi, k.Renk as KategoriRenk
                FROM Urunler u
                INNER JOIN Kategoriler k ON u.KategoriID = k.KategoriID
                WHERE (u.UrunAdi LIKE @SearchText OR u.BarkodNo LIKE @SearchText OR u.Marka LIKE @SearchText)
                AND u.Aktif = 1
                ORDER BY u.UrunAdi";

            SqlParameter[] parameters = { 
                new SqlParameter("@SearchText", "%" + searchText + "%") 
            };
            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            List<Urun> urunler = new List<Urun>();
            foreach (DataRow row in dt.Rows)
            {
                urunler.Add(MapToUrun(row));
            }

            return urunler;
        }

        /// <summary>
        /// ID'ye göre ürün getirir
        /// </summary>
        public Urun GetById(int id)
        {
            string query = @"
                SELECT u.*, k.KategoriAdi, k.Renk as KategoriRenk
                FROM Urunler u
                INNER JOIN Kategoriler k ON u.KategoriID = k.KategoriID
                WHERE u.UrunID = @UrunID";

            SqlParameter[] parameters = { new SqlParameter("@UrunID", id) };
            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
                return MapToUrun(dt.Rows[0]);

            return null;
        }

        /// <summary>
        /// Barkod numarasına göre ürün getirir
        /// </summary>
        public Urun GetByBarkod(string barkodNo)
        {
            string query = @"
                SELECT u.*, k.KategoriAdi, k.Renk as KategoriRenk
                FROM Urunler u
                INNER JOIN Kategoriler k ON u.KategoriID = k.KategoriID
                WHERE u.BarkodNo = @BarkodNo AND u.Aktif = 1";

            SqlParameter[] parameters = { new SqlParameter("@BarkodNo", barkodNo) };
            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
                return MapToUrun(dt.Rows[0]);

            return null;
        }

        /// <summary>
        /// Yeni ürün ekler
        /// </summary>
        public int Insert(Urun urun)
        {
            string query = @"
                INSERT INTO Urunler 
                (BarkodNo, UrunAdi, KategoriID, Marka, Model, Renk, Beden, 
                 AlisFiyati, SatisFiyati, StokMiktari, MinimumStok, Aciklama, ResimUrl, Aktif)
                VALUES 
                (@BarkodNo, @UrunAdi, @KategoriID, @Marka, @Model, @Renk, @Beden,
                 @AlisFiyati, @SatisFiyati, @StokMiktari, @MinimumStok, @Aciklama, @ResimUrl, @Aktif)";

            SqlParameter[] parameters = {
                new SqlParameter("@BarkodNo", (object)urun.BarkodNo ?? DBNull.Value),
                new SqlParameter("@UrunAdi", urun.UrunAdi),
                new SqlParameter("@KategoriID", urun.KategoriID),
                new SqlParameter("@Marka", (object)urun.Marka ?? DBNull.Value),
                new SqlParameter("@Model", (object)urun.Model ?? DBNull.Value),
                new SqlParameter("@Renk", (object)urun.Renk ?? DBNull.Value),
                new SqlParameter("@Beden", (object)urun.Beden ?? DBNull.Value),
                new SqlParameter("@AlisFiyati", urun.AlisFiyati),
                new SqlParameter("@SatisFiyati", urun.SatisFiyati),
                new SqlParameter("@StokMiktari", urun.StokMiktari),
                new SqlParameter("@MinimumStok", urun.MinimumStok),
                new SqlParameter("@Aciklama", (object)urun.Aciklama ?? DBNull.Value),
                new SqlParameter("@ResimUrl", (object)urun.ResimUrl ?? DBNull.Value),
                new SqlParameter("@Aktif", urun.Aktif)
            };

            return SqlHelper.ExecuteInsertWithIdentity(query, parameters);
        }

        /// <summary>
        /// Ürün bilgilerini günceller
        /// </summary>
        public bool Update(Urun urun)
        {
            string query = @"
                UPDATE Urunler SET
                    BarkodNo = @BarkodNo,
                    UrunAdi = @UrunAdi,
                    KategoriID = @KategoriID,
                    Marka = @Marka,
                    Model = @Model,
                    Renk = @Renk,
                    Beden = @Beden,
                    AlisFiyati = @AlisFiyati,
                    SatisFiyati = @SatisFiyati,
                    StokMiktari = @StokMiktari,
                    MinimumStok = @MinimumStok,
                    Aciklama = @Aciklama,
                    ResimUrl = @ResimUrl,
                    Aktif = @Aktif,
                    GuncellemeTarihi = GETDATE()
                WHERE UrunID = @UrunID";

            SqlParameter[] parameters = {
                new SqlParameter("@UrunID", urun.UrunID),
                new SqlParameter("@BarkodNo", (object)urun.BarkodNo ?? DBNull.Value),
                new SqlParameter("@UrunAdi", urun.UrunAdi),
                new SqlParameter("@KategoriID", urun.KategoriID),
                new SqlParameter("@Marka", (object)urun.Marka ?? DBNull.Value),
                new SqlParameter("@Model", (object)urun.Model ?? DBNull.Value),
                new SqlParameter("@Renk", (object)urun.Renk ?? DBNull.Value),
                new SqlParameter("@Beden", (object)urun.Beden ?? DBNull.Value),
                new SqlParameter("@AlisFiyati", urun.AlisFiyati),
                new SqlParameter("@SatisFiyati", urun.SatisFiyati),
                new SqlParameter("@StokMiktari", urun.StokMiktari),
                new SqlParameter("@MinimumStok", urun.MinimumStok),
                new SqlParameter("@Aciklama", (object)urun.Aciklama ?? DBNull.Value),
                new SqlParameter("@ResimUrl", (object)urun.ResimUrl ?? DBNull.Value),
                new SqlParameter("@Aktif", urun.Aktif)
            };

            return SqlHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Ürün stok miktarını günceller
        /// </summary>
        public bool UpdateStock(int urunId, int yeniStokMiktari)
        {
            string query = @"
                UPDATE Urunler SET 
                    StokMiktari = @StokMiktari,
                    GuncellemeTarihi = GETDATE()
                WHERE UrunID = @UrunID";

            SqlParameter[] parameters = {
                new SqlParameter("@UrunID", urunId),
                new SqlParameter("@StokMiktari", yeniStokMiktari)
            };

            return SqlHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Ürün siler (hard delete)
        /// </summary>
        public bool Delete(int id)
        {
            string query = "DELETE FROM Urunler WHERE UrunID = @UrunID";
            SqlParameter[] parameters = { new SqlParameter("@UrunID", id) };

            return SqlHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Toplam ürün sayısını döner
        /// </summary>
        public int GetTotalCount()
        {
            string query = "SELECT COUNT(*) FROM Urunler WHERE Aktif = 1";
            object result = SqlHelper.ExecuteScalar(query);
            return Convert.ToInt32(result);
        }

        /// <summary>
        /// Toplam stok değerini hesaplar
        /// </summary>
        public decimal GetTotalStockValue()
        {
            string query = "SELECT ISNULL(SUM(StokMiktari * AlisFiyati), 0) FROM Urunler WHERE Aktif = 1";
            object result = SqlHelper.ExecuteScalar(query);
            return Convert.ToDecimal(result);
        }

        /// <summary>
        /// DataRow'u Urun nesnesine dönüştürür
        /// </summary>
        private Urun MapToUrun(DataRow row)
        {
            return new Urun
            {
                UrunID = Convert.ToInt32(row["UrunID"]),
                BarkodNo = row["BarkodNo"] != DBNull.Value ? row["BarkodNo"].ToString() : null,
                UrunAdi = row["UrunAdi"].ToString(),
                KategoriID = Convert.ToInt32(row["KategoriID"]),
                Marka = row["Marka"] != DBNull.Value ? row["Marka"].ToString() : null,
                Model = row["Model"] != DBNull.Value ? row["Model"].ToString() : null,
                Renk = row["Renk"] != DBNull.Value ? row["Renk"].ToString() : null,
                Beden = row["Beden"] != DBNull.Value ? row["Beden"].ToString() : null,
                AlisFiyati = Convert.ToDecimal(row["AlisFiyati"]),
                SatisFiyati = Convert.ToDecimal(row["SatisFiyati"]),
                StokMiktari = Convert.ToInt32(row["StokMiktari"]),
                MinimumStok = Convert.ToInt32(row["MinimumStok"]),
                Aciklama = row["Aciklama"] != DBNull.Value ? row["Aciklama"].ToString() : null,
                ResimUrl = row["ResimUrl"] != DBNull.Value ? row["ResimUrl"].ToString() : null,
                KayitTarihi = Convert.ToDateTime(row["KayitTarihi"]),
                GuncellemeTarihi = row["GuncellemeTarihi"] != DBNull.Value ? 
                    (DateTime?)Convert.ToDateTime(row["GuncellemeTarihi"]) : null,
                Aktif = Convert.ToBoolean(row["Aktif"]),
                // Navigation Properties
                KategoriAdi = row["KategoriAdi"].ToString(),
                KategoriRenk = row["KategoriRenk"] != DBNull.Value ? row["KategoriRenk"].ToString() : "#3B82F6"
            };
        }
    }
}
