using System;
using System.Windows.Forms;
using StokTakip.UI.Forms;

namespace StokTakip.UI
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana giriş noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            
            // Login formu ile başlat
            Application.Run(new FrmLogin());
        }
    }
}
