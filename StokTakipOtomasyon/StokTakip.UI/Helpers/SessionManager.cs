using StokTakip.DataAccess.Models;

namespace StokTakip.UI.Helpers
{
    /// <summary>
    /// Aktif kullanıcı oturumu için singleton sınıf
    /// </summary>
    public static class SessionManager
    {
        private static Kullanici _aktifKullanici;

        /// <summary>
        /// Giriş yapan kullanıcı
        /// </summary>
        public static Kullanici AktifKullanici
        {
            get => _aktifKullanici;
            set => _aktifKullanici = value;
        }

        /// <summary>
        /// Kullanıcı giriş yapmış mı?
        /// </summary>
        public static bool IsLoggedIn => _aktifKullanici != null;

        /// <summary>
        /// Kullanıcı admin mi?
        /// </summary>
        public static bool IsAdmin => _aktifKullanici?.Rol == "Admin";

        /// <summary>
        /// Oturumu kapat
        /// </summary>
        public static void Logout()
        {
            _aktifKullanici = null;
        }
    }
}
