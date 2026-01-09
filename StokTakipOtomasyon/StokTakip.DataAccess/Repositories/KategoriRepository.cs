using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StokTakip.DataAccess.Context;
using StokTakip.DataAccess.Models;

namespace StokTakip.DataAccess.Repositories
{
    public class KategoriRepository
    {
        /// <summary>
        /// Tüm kategorileri getirir
        /// </summary>
        public List<Kategori> GetAll()
        {
            string query = "SELECT * FROM Kategoriler WHERE Aktif = 1 ORDER BY KategoriAdi";
            DataTable dt = SqlHelper.ExecuteQuery(query);

            List<Kategori> kategoriler = new List<Kategori>();
            foreach (DataRow row in dt.Rows)
            {
                kategoriler.Add(MapToKategori(row));
            }

            return kategoriler;
        }

        /// <summary>
        /// Ana kategorileri getirir (Alt kategori olmayanlar)
        /// </summary>
        public List<Kategori> GetMainCategories()
        {
            string query = "SELECT * FROM Kategoriler WHERE UstKategoriID IS NULL AND Aktif = 1 ORDER BY KategoriAdi";
            DataTable dt = SqlHelper.ExecuteQuery(query);

            List<Kategori> kategoriler = new List<Kategori>();
            foreach (DataRow row in dt.Rows)
            {
                kategoriler.Add(MapToKategori(row));
            }

            return kategoriler;
        }

        /// <summary>
        /// Belirli bir üst kategoriye ait alt kategorileri getirir
        /// </summary>
        public List<Kategori> GetSubCategories(int ustKategoriId)
        {
            string query = "SELECT * FROM Kategoriler WHERE UstKategoriID = @UstKategoriID AND Aktif = 1 ORDER BY KategoriAdi";
            SqlParameter[] parameters = { new SqlParameter("@UstKategoriID", ustKategoriId) };
            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            List<Kategori> kategoriler = new List<Kategori>();
            foreach (DataRow row in dt.Rows)
            {
                kategoriler.Add(MapToKategori(row));
            }

            return kategoriler;
        }

        /// <summary>
        /// ID'ye göre kategori getirir
        /// </summary>
        public Kategori GetById(int id)
        {
            string query = "SELECT * FROM Kategoriler WHERE KategoriID = @KategoriID";
            SqlParameter[] parameters = { new SqlParameter("@KategoriID", id) };

            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
                return MapToKategori(dt.Rows[0]);

            return null;
        }

        /// <summary>
        /// Yeni kategori ekler
        /// </summary>
        public int Insert(Kategori kategori)
        {
            string query = @"
                INSERT INTO Kategoriler 
                (KategoriAdi, Aciklama, UstKategoriID, Ikon, Renk, Aktif)
                VALUES 
                (@KategoriAdi, @Aciklama, @UstKategoriID, @Ikon, @Renk, @Aktif)";

            SqlParameter[] parameters = {
                new SqlParameter("@KategoriAdi", kategori.KategoriAdi),
                new SqlParameter("@Aciklama", (object)kategori.Aciklama ?? DBNull.Value),
                new SqlParameter("@UstKategoriID", (object)kategori.UstKategoriID ?? DBNull.Value),
                new SqlParameter("@Ikon", (object)kategori.Ikon ?? DBNull.Value),
                new SqlParameter("@Renk", kategori.Renk ?? "#3B82F6"),
                new SqlParameter("@Aktif", kategori.Aktif)
            };

            return SqlHelper.ExecuteInsertWithIdentity(query, parameters);
        }

        /// <summary>
        /// Kategori bilgilerini günceller
        /// </summary>
        public bool Update(Kategori kategori)
        {
            string query = @"
                UPDATE Kategoriler SET
                    KategoriAdi = @KategoriAdi,
                    Aciklama = @Aciklama,
                    UstKategoriID = @UstKategoriID,
                    Ikon = @Ikon,
                    Renk = @Renk,
                    Aktif = @Aktif
                WHERE KategoriID = @KategoriID";

            SqlParameter[] parameters = {
                new SqlParameter("@KategoriID", kategori.KategoriID),
                new SqlParameter("@KategoriAdi", kategori.KategoriAdi),
                new SqlParameter("@Aciklama", (object)kategori.Aciklama ?? DBNull.Value),
                new SqlParameter("@UstKategoriID", (object)kategori.UstKategoriID ?? DBNull.Value),
                new SqlParameter("@Ikon", (object)kategori.Ikon ?? DBNull.Value),
                new SqlParameter("@Renk", kategori.Renk ?? "#3B82F6"),
                new SqlParameter("@Aktif", kategori.Aktif)
            };

            return SqlHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Kategori siler (soft delete)
        /// </summary>
        public bool Delete(int id)
        {
            string query = "UPDATE Kategoriler SET Aktif = 0 WHERE KategoriID = @KategoriID";
            SqlParameter[] parameters = { new SqlParameter("@KategoriID", id) };

            return SqlHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// DataRow'u Kategori nesnesine dönüştürür
        /// </summary>
        private Kategori MapToKategori(DataRow row)
        {
            return new Kategori
            {
                KategoriID = Convert.ToInt32(row["KategoriID"]),
                KategoriAdi = row["KategoriAdi"].ToString(),
                Aciklama = row["Aciklama"] != DBNull.Value ? row["Aciklama"].ToString() : null,
                UstKategoriID = row["UstKategoriID"] != DBNull.Value ? 
                    (int?)Convert.ToInt32(row["UstKategoriID"]) : null,
                Ikon = row["Ikon"] != DBNull.Value ? row["Ikon"].ToString() : null,
                Renk = row["Renk"] != DBNull.Value ? row["Renk"].ToString() : "#3B82F6",
                Aktif = Convert.ToBoolean(row["Aktif"])
            };
        }
    }
}
