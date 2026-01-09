using System;
using StokTakip.DataAccess.Models;
using StokTakip.DataAccess.Repositories;
using StokTakip.Business.Helpers;

namespace StokTakip.Business.Services
{
    public class AuthService
    {
        private readonly KullaniciRepository _kullaniciRepo;

        public AuthService()
        {
            _kullaniciRepo = new KullaniciRepository();
        }

        /// <summary>
        /// Kullanıcı girişi yapar
        /// </summary>
        public Kullanici Login(string kullaniciAdi, string sifre)
        {
            if (string.IsNullOrWhiteSpace(kullaniciAdi))
                throw new Exception("Kullanıcı adı boş olamaz!");

            if (string.IsNullOrWhiteSpace(sifre))
                throw new Exception("Şifre boş olamaz!");

            // Şifreyi hashle
            string hashedPassword = SecurityHelper.HashPassword(sifre);

            // Login işlemi
            Kullanici kullanici = _kullaniciRepo.Login(kullaniciAdi, hashedPassword);

            if (kullanici == null)
                throw new Exception("Kullanıcı adı veya şifre hatalı!");

            if (!kullanici.Aktif)
                throw new Exception("Bu kullanıcı hesabı aktif değil!");

            return kullanici;
        }

        /// <summary>
        /// Yeni kullanıcı kaydı yapar
        /// </summary>
        public int Register(Kullanici kullanici, string sifre)
        {
            // Validasyonlar
            ValidateKullanici(kullanici, sifre);

            // Şifreyi hashle
            kullanici.Sifre = SecurityHelper.HashPassword(sifre);
            kullanici.Aktif = true;

            // Kaydet
            return _kullaniciRepo.Insert(kullanici);
        }

        /// <summary>
        /// Şifre değiştirir
        /// </summary>
        public bool ChangePassword(int kullaniciId, string eskiSifre, string yeniSifre)
        {
            // Eski şifreyi kontrol et
            Kullanici kullanici = _kullaniciRepo.GetById(kullaniciId);
            
            if (kullanici == null)
                throw new Exception("Kullanıcı bulunamadı!");

            string hashedOldPassword = SecurityHelper.HashPassword(eskiSifre);
            
            if (kullanici.Sifre != hashedOldPassword)
                throw new Exception("Eski şifre hatalı!");

            // Yeni şifre kontrolü
            if (string.IsNullOrWhiteSpace(yeniSifre) || yeniSifre.Length < 6)
                throw new Exception("Yeni şifre en az 6 karakter olmalıdır!");

            // Şifreyi değiştir
            string hashedNewPassword = SecurityHelper.HashPassword(yeniSifre);
            return _kullaniciRepo.ChangePassword(kullaniciId, hashedNewPassword);
        }

        /// <summary>
        /// Şifre değiştirir (kullanıcı adı ile)
        /// </summary>
        public bool ChangePassword(string kullaniciAdi, string eskiSifre, string yeniSifre)
        {
            // Kullanıcıyı bul
            Kullanici kullanici = _kullaniciRepo.GetByUsername(kullaniciAdi);
            
            if (kullanici == null)
                throw new Exception("Kullanıcı bulunamadı!");

            // Eski şifreyi kontrol et
            string hashedOldPassword = SecurityHelper.HashPassword(eskiSifre);
            
            if (kullanici.Sifre != hashedOldPassword)
                throw new Exception("Eski şifre hatalı!");

            // Yeni şifre kontrolü
            if (string.IsNullOrWhiteSpace(yeniSifre) || yeniSifre.Length < 6)
                throw new Exception("Yeni şifre en az 6 karakter olmalıdır!");

            // Şifreyi değiştir
            string hashedNewPassword = SecurityHelper.HashPassword(yeniSifre);
            return _kullaniciRepo.ChangePassword(kullanici.KullaniciID, hashedNewPassword);
        }

        /// <summary>
        /// Kullanıcı validasyonu
        /// </summary>
        private void ValidateKullanici(Kullanici kullanici, string sifre)
        {
            if (string.IsNullOrWhiteSpace(kullanici.KullaniciAdi))
                throw new Exception("Kullanıcı adı boş olamaz!");

            if (kullanici.KullaniciAdi.Length < 3)
                throw new Exception("Kullanıcı adı en az 3 karakter olmalıdır!");

            if (string.IsNullOrWhiteSpace(kullanici.Email))
                throw new Exception("E-posta adresi boş olamaz!");

            if (!kullanici.Email.Contains("@"))
                throw new Exception("Geçerli bir e-posta adresi giriniz!");

            if (string.IsNullOrWhiteSpace(sifre) || sifre.Length < 6)
                throw new Exception("Şifre en az 6 karakter olmalıdır!");

            if (string.IsNullOrWhiteSpace(kullanici.AdSoyad))
                throw new Exception("Ad Soyad boş olamaz!");

            if (string.IsNullOrWhiteSpace(kullanici.Rol))
                throw new Exception("Rol seçilmelidir!");
        }
    }
}
