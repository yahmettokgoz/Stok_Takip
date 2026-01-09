using System;
using System.Security.Cryptography;
using System.Text;

namespace StokTakip.Business.Helpers
{
    /// <summary>
    /// Şifreleme ve hash işlemleri için yardımcı sınıf
    /// </summary>
    public static class SecurityHelper
    {
        /// <summary>
        /// Metni SHA-256 ile hashler
        /// </summary>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return string.Empty;

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("X2"));
                }
                
                return builder.ToString();
            }
        }

        /// <summary>
        /// Şifre gücünü kontrol eder
        /// </summary>
        public static bool IsStrongPassword(string password)
        {
            if (string.IsNullOrEmpty(password) || password.Length < 6)
                return false;

            bool hasUpper = false;
            bool hasLower = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                if (char.IsLower(c)) hasLower = true;
                if (char.IsDigit(c)) hasDigit = true;
            }

            return hasUpper && hasLower && hasDigit;
        }
    }
}
