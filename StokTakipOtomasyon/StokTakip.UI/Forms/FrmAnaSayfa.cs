using System;
using System.Drawing;
using System.Windows.Forms;
using StokTakip.Business.Services;
using StokTakip.UI.Components;
using StokTakip.UI.Helpers;

namespace StokTakip.UI.Forms
{
    public partial class FrmAnaSayfa : Form
    {
        private readonly UrunService _urunService;
        private readonly SatisService _satisService;
        
        // Dashboard Widgets
        private DashboardWidget widgetToplamUrun;
        private DashboardWidget widgetStokDegeri;
        private DashboardWidget widgetGunlukSatis;
        private DashboardWidget widgetKritikStok;
        
        // Sidebar Buttons
        private Panel pnlSidebar;
        private Button btnDashboard;
        private Button btnUrunler;
        private Button btnSatis;
        private Button btnStokHareket;
        private Button btnRaporlar;
        private Button btnAyarlar;
        private Button btnCikis;
        
        // Header
        private Panel pnlHeader;
        private Label lblPageTitle;
        private Label lblUserInfo;
        
        // Content Panel
        private Panel pnlContent;

        public FrmAnaSayfa()
        {
            InitializeComponent();
            _urunService = new UrunService();
            _satisService = new SatisService();
            
            CreateModernUI();
            LoadDashboardData();
        }

        private void CreateModernUI()
        {
            // Form ayarları
            this.Size = new Size(1600, 850);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.FormBorderStyle = FormBorderStyle.Sizable;

            CreateSidebar();
            CreateHeader();
            CreateContentPanel();
        }

        private void CreateSidebar()
        {
            pnlSidebar = new Panel
            {
                Width = 250,
                Dock = DockStyle.Left,
                BackColor = Color.FromArgb(15, 23, 42)
            };

            // Logo/Title
            Label lblLogo = new Label
            {
                Text = "🏪 STOK TAKİP",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold),
                ForeColor = Color.FromArgb(59, 130, 246),
                Location = new Point(20, 30),
                AutoSize = true
            };
            pnlSidebar.Controls.Add(lblLogo);

            // Menü butonları
            int yPos = 120;
            btnDashboard = CreateSidebarButton("🏠 Dashboard", yPos, true);
            btnDashboard.Click += (s, e) => ShowDashboard();
            
            btnUrunler = CreateSidebarButton("📦 Ürünler", yPos += 60, false);
            btnUrunler.Click += (s, e) => {
                FrmUrunler frmUrunler = new FrmUrunler();
                frmUrunler.ShowDialog();
            };
            
            btnSatis = CreateSidebarButton("🛒 Satış Yap", yPos += 60, false);
            btnSatis.Click += (s, e) => {
                FrmSatis frmSatis = new FrmSatis();
                frmSatis.ShowDialog();
                LoadDashboardData(); // Dashboard'u güncelle
            };
            
            btnStokHareket = CreateSidebarButton("📊 Stok Hareketleri", yPos += 60, false);
            btnStokHareket.Click += (s, e) => {
                FrmStokHareketleri frmStokHareket = new FrmStokHareketleri();
                frmStokHareket.ShowDialog();
            };
            
            btnRaporlar = CreateSidebarButton("📈 Raporlar", yPos += 60, false);
            btnRaporlar.Click += (s, e) => {
                FrmRaporlar frmRaporlar = new FrmRaporlar();
                frmRaporlar.ShowDialog();
            };
            
            btnAyarlar = CreateSidebarButton("⚙️ Ayarlar", yPos += 60, false);
            btnAyarlar.Click += (s, e) => {
                FrmAyarlar frmAyarlar = new FrmAyarlar();
                frmAyarlar.ShowDialog();
            };

            // Çıkış butonu (alt kısımda)
            btnCikis = new Button
            {
                Text = "🚪 Çıkış Yap",
                Size = new Size(210, 45),
                Location = new Point(20, 890),
                BackColor = Color.FromArgb(239, 68, 68),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnCikis.FlatAppearance.BorderSize = 0;
            btnCikis.Click += BtnCikis_Click;
            pnlSidebar.Controls.Add(btnCikis);

            this.Controls.Add(pnlSidebar);
        }

        private Button CreateSidebarButton(string text, int yPos, bool isActive)
        {
            Button btn = new Button
            {
                Text = text,
                Size = new Size(210, 45),
                Location = new Point(20, yPos),
                BackColor = isActive ? Color.FromArgb(30, 41, 59) : Color.Transparent,
                ForeColor = isActive ? Color.FromArgb(59, 130, 246) : Color.FromArgb(148, 163, 184),
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10F, isActive ? FontStyle.Bold : FontStyle.Regular),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.MouseEnter += (s, e) => {
                if (!isActive) btn.BackColor = Color.FromArgb(30, 41, 59);
            };
            btn.MouseLeave += (s, e) => {
                if (!isActive) btn.BackColor = Color.Transparent;
            };
            pnlSidebar.Controls.Add(btn);
            return btn;
        }

        private void CreateHeader()
        {
            pnlHeader = new Panel
            {
                Height = 70,
                Dock = DockStyle.Top,
                BackColor = Color.FromArgb(30, 41, 59)
            };

            lblPageTitle = new Label
            {
                Text = "Dashboard",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.FromArgb(248, 250, 252),
                Location = new Point(30, 20),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblPageTitle);

            lblUserInfo = new Label
            {
                Text = string.Format("Hosgeldiniz {0}!", SessionManager.AktifKullanici.AdSoyad),
                Font = new Font("Segoe UI", 15F),
                ForeColor = Color.FromArgb(148, 163, 184),
                Location = new Point(877, 25),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblUserInfo);

            this.Controls.Add(pnlHeader);
        }

        private void CreateContentPanel()
        {
            pnlContent = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(15, 23, 42),
                AutoScroll = true
            };

            // Dashboard Widgets
            widgetToplamUrun = new DashboardWidget
            {
                Location = new Point(280, 100),
                WidgetTitle = "TOPLAM \u00dcR\u00dcN",
                WidgetValue = "0",
                WidgetSubtext = "",
                AccentColor = Color.FromArgb(59, 130, 246),
                IconText = "📦"
            };
            pnlContent.Controls.Add(widgetToplamUrun);

            widgetStokDegeri = new DashboardWidget
            {
                Location = new Point(560, 100),
                WidgetTitle = "STOK DE\u011eER\u0130",
                WidgetValue = "₺0",
                WidgetSubtext = "",
                AccentColor = Color.FromArgb(34, 197, 94),
                IconText = "💰"
            };
            pnlContent.Controls.Add(widgetStokDegeri);

            widgetGunlukSatis = new DashboardWidget
            {
                Location = new Point(840, 100),
                WidgetTitle = "G\u00dcNL\u00dcK SATI\u015e",
                WidgetValue = "₺0",
                WidgetSubtext = "",
                AccentColor = Color.FromArgb(168, 85, 247),
                IconText = "🛒"
            };
            pnlContent.Controls.Add(widgetGunlukSatis);

            widgetKritikStok = new DashboardWidget
            {
                Location = new Point(1120, 100),
                WidgetTitle = "KRİTİK STOK",
                WidgetValue = "0",
                WidgetSubtext = "",
                AccentColor = Color.FromArgb(249, 115, 22),
                IconText = "⚠️",
                Cursor = Cursors.Hand
            };
            widgetKritikStok.Click += WidgetKritikStok_Click;
            pnlContent.Controls.Add(widgetKritikStok);

            this.Controls.Add(pnlContent);
        }

        private void LoadDashboardData()
        {
            try
            {
                // Toplam ürün sayısı
                int toplamUrun = _urunService.GetTotalCount();
                widgetToplamUrun.WidgetValue = toplamUrun.ToString();

                // Toplam stok değeri
                decimal stokDegeri = _urunService.GetTotalStockValue();
                widgetStokDegeri.WidgetValue = stokDegeri.ToString("C0");

                // Günlük satış
                decimal gunlukSatis = _satisService.GetTodayTotal();
                widgetGunlukSatis.WidgetValue = gunlukSatis.ToString("C0");

                // Kritik stok
                int kritikStok = _urunService.GetCriticalStock().Count;
                widgetKritikStok.WidgetValue = kritikStok.ToString();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError($"Dashboard verileri yüklenirken hata oluştu:\n{ex.Message}");
            }
        }

        private void ShowDashboard()
        {
            lblPageTitle.Text = "Dashboard";
            LoadDashboardData();
        }

        private void WidgetKritikStok_Click(object sender, EventArgs e)
        {
            FrmKritikStoklar frmKritikStoklar = new FrmKritikStoklar();
            frmKritikStoklar.ShowDialog();
        }

        private void BtnCikis_Click(object sender, EventArgs e)
        {
            if (MessageHelper.ShowConfirm("Çıkış yapmak istediğinize emin misiniz?", "Çıkış"))
            {
                SessionManager.Logout();
                this.Close();
                Application.Restart();
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            Application.Exit();
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {

        }
    }
}
