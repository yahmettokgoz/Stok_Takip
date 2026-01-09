using System.Windows.Forms;

namespace StokTakip.UI.Helpers
{
    /// <summary>
    /// Kullanıcı mesajları için yardımcı sınıf
    /// </summary>
    public static class MessageHelper
    {
        /// <summary>
        /// Başarı mesajı gösterir
        /// </summary>
        public static void ShowSuccess(string message, string title = "Başarılı")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Hata mesajı gösterir
        /// </summary>
        public static void ShowError(string message, string title = "Hata")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Uyarı mesajı gösterir
        /// </summary>
        public static void ShowWarning(string message, string title = "Uyarı")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Bilgi mesajı gösterir
        /// </summary>
        public static void ShowInfo(string message, string title = "Bilgi")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Onay sorusu sorar
        /// </summary>
        public static bool ShowConfirm(string message, string title = "Onay")
        {
            DialogResult result = MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            return result == DialogResult.Yes;
        }

        /// <summary>
        /// Exception mesajını gösterir
        /// </summary>
        public static void ShowException(System.Exception ex)
        {
            MessageBox.Show($"Bir hata oluştu:\n\n{ex.Message}", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
