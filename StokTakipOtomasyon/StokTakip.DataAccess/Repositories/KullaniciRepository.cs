using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using StokTakip.DataAccess.Context;
using StokTakip.DataAccess.Models;

namespace StokTakip.DataAccess.Repositories
{
    public class KullaniciRepository
    {
        /// <summary>
        /// Kullanıcı girişi yapar
        /// </summary>
        public Kullanici Login(string kullaniciAdi, string sifre)
        {
            string query = @"
                SELECT * FROM Kullanicilar 
                WHERE KullaniciAdi = @KullaniciAdi 
                AND Sifre = @Sifre 
                AND Aktif = 1";

            SqlParameter[] parameters = {
                new SqlParameter("@KullaniciAdi", kullaniciAdi),
                new SqlParameter("@Sifre", sifre)
            };

            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                
                // Son giriş tarihini güncelle
                UpdateLastLoginDate(Convert.ToInt32(row["KullaniciID"]));

                return MapToKullanici(row);
            }

            return null;
        }

        /// <summary>
        /// Tüm kullanıcıları getirir
        /// </summary>
        public List<Kullanici> GetAll()
        {
            string query = "SELECT * FROM Kullanicilar ORDER BY AdSoyad";
            DataTable dt = SqlHelper.ExecuteQuery(query);

            List<Kullanici> kullanicilar = new List<Kullanici>();
            foreach (DataRow row in dt.Rows)
            {
                kullanicilar.Add(MapToKullanici(row));
            }

            return kullanicilar;
        }

        /// <summary>
        /// ID'ye göre kullanıcı getirir
        /// </summary>
        public Kullanici GetById(int id)
        {
            string query = "SELECT * FROM Kullanicilar WHERE KullaniciID = @KullaniciID";
            SqlParameter[] parameters = { new SqlParameter("@KullaniciID", id) };

            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
                return MapToKullanici(dt.Rows[0]);

            return null;
        }

        /// <summary>
        /// Kullanıcı adına göre kullanıcı getirir
        /// </summary>
        public Kullanici GetByUsername(string kullaniciAdi)
        {
            string query = "SELECT * FROM Kullanicilar WHERE KullaniciAdi = @KullaniciAdi";
            SqlParameter[] parameters = { new SqlParameter("@KullaniciAdi", kullaniciAdi) };

            DataTable dt = SqlHelper.ExecuteQuery(query, parameters);

            if (dt.Rows.Count > 0)
                return MapToKullanici(dt.Rows[0]);

            return null;
        }

        /// <summary>
        /// Yeni kullanıcı ekler
        /// </summary>
        public int Insert(Kullanici kullanici)
        {
            string query = @"
                INSERT INTO Kullanicilar 
                (KullaniciAdi, Email, Sifre, AdSoyad, Rol, Telefon, Aktif)
                VALUES 
                (@KullaniciAdi, @Email, @Sifre, @AdSoyad, @Rol, @Telefon, @Aktif)";

            SqlParameter[] parameters = {
                new SqlParameter("@KullaniciAdi", kullanici.KullaniciAdi),
                new SqlParameter("@Email", kullanici.Email),
                new SqlParameter("@Sifre", kullanici.Sifre),
                new SqlParameter("@AdSoyad", kullanici.AdSoyad),
                new SqlParameter("@Rol", kullanici.Rol),
                new SqlParameter("@Telefon", (object)kullanici.Telefon ?? DBNull.Value),
                new SqlParameter("@Aktif", kullanici.Aktif)
            };

            return SqlHelper.ExecuteInsertWithIdentity(query, parameters);
        }

        /// <summary>
        /// Kullanıcı bilgilerini günceller
        /// </summary>
        public bool Update(Kullanici kullanici)
        {
            string query = @"
                UPDATE Kullanicilar SET
                    Email = @Email,
                    AdSoyad = @AdSoyad,
                    Rol = @Rol,
                    Telefon = @Telefon,
                    Aktif = @Aktif
                WHERE KullaniciID = @KullaniciID";

            SqlParameter[] parameters = {
                new SqlParameter("@KullaniciID", kullanici.KullaniciID),
                new SqlParameter("@Email", kullanici.Email),
                new SqlParameter("@AdSoyad", kullanici.AdSoyad),
                new SqlParameter("@Rol", kullanici.Rol),
                new SqlParameter("@Telefon", (object)kullanici.Telefon ?? DBNull.Value),
                new SqlParameter("@Aktif", kullanici.Aktif)
            };

            return SqlHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Kullanıcı şifresini değiştirir
        /// </summary>
        public bool ChangePassword(int kullaniciId, string yeniSifre)
        {
            string query = "UPDATE Kullanicilar SET Sifre = @Sifre WHERE KullaniciID = @KullaniciID";

            SqlParameter[] parameters = {
                new SqlParameter("@KullaniciID", kullaniciId),
                new SqlParameter("@Sifre", yeniSifre)
            };

            return SqlHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Kullanıcı siler (soft delete)
        /// </summary>
        public bool Delete(int id)
        {
            string query = "UPDATE Kullanicilar SET Aktif = 0 WHERE KullaniciID = @KullaniciID";
            SqlParameter[] parameters = { new SqlParameter("@KullaniciID", id) };

            return SqlHelper.ExecuteNonQuery(query, parameters) > 0;
        }

        /// <summary>
        /// Son giriş tarihini günceller
        /// </summary>
        private void UpdateLastLoginDate(int kullaniciId)
        {
            string query = "UPDATE Kullanicilar SET SonGirisTarihi = GETDATE() WHERE KullaniciID = @KullaniciID";
            SqlParameter[] parameters = { new SqlParameter("@KullaniciID", kullaniciId) };
            SqlHelper.ExecuteNonQuery(query, parameters);
        }

        /// <summary>
        /// DataRow'u Kullanici nesnesine dönüştürür
        /// </summary>
        private Kullanici MapToKullanici(DataRow row)
        {
            return new Kullanici
            {
                KullaniciID = Convert.ToInt32(row["KullaniciID"]),
                KullaniciAdi = row["KullaniciAdi"].ToString(),
                Email = row["Email"].ToString(),
                Sifre = row["Sifre"].ToString(),
                AdSoyad = row["AdSoyad"].ToString(),
                Rol = row["Rol"].ToString(),
                Telefon = row["Telefon"] != DBNull.Value ? row["Telefon"].ToString() : null,
                Aktif = Convert.ToBoolean(row["Aktif"]),
                KayitTarihi = Convert.ToDateTime(row["KayitTarihi"]),
                SonGirisTarihi = row["SonGirisTarihi"] != DBNull.Value ? 
                    (DateTime?)Convert.ToDateTime(row["SonGirisTarihi"]) : null
            };
        }
    }
}
