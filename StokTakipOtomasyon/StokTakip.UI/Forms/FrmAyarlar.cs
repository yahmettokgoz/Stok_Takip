using System;
using System.Drawing;
using System.Windows.Forms;
using StokTakip.Business.Helpers;
using StokTakip.Business.Services;
using StokTakip.UI.Helpers;

namespace StokTakip.UI.Forms
{
    public partial class FrmAyarlar : Form
    {
        private AuthService _authService;
        private Panel pnlHeader;
        private Button btnKapat;
        private Label lblBaslik;
        private GroupBox grpKullaniciBilgileri;
        private GroupBox grpSifreDegistir;
        private Label lblKullaniciAdi;
        private Label lblAdSoyad;
        private Label lblEmail;
        private Label lblRol;
        private TextBox txtEskiSifre;
        private TextBox txtYeniSifre;
        private TextBox txtYeniSifreTekrar;
        private Button btnSifreDegistir;

        public FrmAyarlar()
        {
            _authService = new AuthService();
            InitializeComponent();
            InitializeCustomComponents();
            LoadUserInfo();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmAyarlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.ClientSize = new System.Drawing.Size(1100, 750);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmAyarlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ayarlar";
            this.Load += FrmAyarlar_Load;
            this.ResumeLayout(false);
        }

        private void InitializeCustomComponents()
        {
            // Header Panel
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.FromArgb(30, 41, 59)
            };
            this.Controls.Add(pnlHeader);

            // Baslik
            lblBaslik = new Label
            {
                Text = "AYARLAR",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(20, 15),
                AutoSize = true
            };
            pnlHeader.Controls.Add(lblBaslik);

            // Kapat butonu
            btnKapat = new Button
            {
                Text = "✕",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Size = new Size(50, 50),
                BackColor = Color.Transparent,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };
            btnKapat.FlatAppearance.BorderSize = 0;
            btnKapat.Click += (s, e) => this.Close();
            btnKapat.MouseEnter += (s, e) => btnKapat.BackColor = Color.FromArgb(239, 68, 68);
            btnKapat.MouseLeave += (s, e) => btnKapat.BackColor = Color.Transparent;
            pnlHeader.Controls.Add(btnKapat);

            // Kullanici Bilgileri GroupBox
            grpKullaniciBilgileri = new GroupBox
            {
                Text = "Kullanıcı Bilgileri",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(50, 100),
                Size = new Size(1000, 250),
                BackColor = Color.FromArgb(30, 41, 59)
            };
            this.Controls.Add(grpKullaniciBilgileri);

            // Kullanici Adi Label
            Label lblKullaniciAdiTitle = new Label
            {
                Text = "Kullanıcı Adı:",
                Location = new Point(40, 60),
                Size = new Size(180, 30),
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 12)
            };
            grpKullaniciBilgileri.Controls.Add(lblKullaniciAdiTitle);

            lblKullaniciAdi = new Label
            {
                Location = new Point(230, 60),
                Size = new Size(350, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            grpKullaniciBilgileri.Controls.Add(lblKullaniciAdi);

            // Ad Soyad Label
            Label lblAdSoyadTitle = new Label
            {
                Text = "Ad Soyad:",
                Location = new Point(40, 110),
                Size = new Size(180, 30),
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 12)
            };
            grpKullaniciBilgileri.Controls.Add(lblAdSoyadTitle);

            lblAdSoyad = new Label
            {
                Location = new Point(230, 110),
                Size = new Size(350, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            grpKullaniciBilgileri.Controls.Add(lblAdSoyad);

            // Email Label
            Label lblEmailTitle = new Label
            {
                Text = "E-posta:",
                Location = new Point(40, 160),
                Size = new Size(180, 30),
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 12)
            };
            grpKullaniciBilgileri.Controls.Add(lblEmailTitle);

            lblEmail = new Label
            {
                Location = new Point(230, 160),
                Size = new Size(350, 30),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            grpKullaniciBilgileri.Controls.Add(lblEmail);

            // Rol Label
            Label lblRolTitle = new Label
            {
                Text = "Rol:",
                Location = new Point(620, 60),
                Size = new Size(100, 30),
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 12)
            };
            grpKullaniciBilgileri.Controls.Add(lblRolTitle);

            lblRol = new Label
            {
                Location = new Point(730, 60),
                Size = new Size(200, 30),
                ForeColor = Color.FromArgb(59, 130, 246),
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            grpKullaniciBilgileri.Controls.Add(lblRol);

            // Sifre Degistir GroupBox
            grpSifreDegistir = new GroupBox
            {
                Text = "Şifre Değiştir",
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                Location = new Point(50, 380),
                Size = new Size(1000, 320),
                BackColor = Color.FromArgb(30, 41, 59)
            };
            this.Controls.Add(grpSifreDegistir);

            // Eski Sifre
            Label lblEskiSifre = new Label
            {
                Text = "Eski Şifre:",
                Location = new Point(40, 60),
                Size = new Size(180, 30),
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 12)
            };
            grpSifreDegistir.Controls.Add(lblEskiSifre);

            txtEskiSifre = new TextBox
            {
                Location = new Point(230, 60),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 12),
                UseSystemPasswordChar = true,
                BackColor = Color.FromArgb(51, 65, 85),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            grpSifreDegistir.Controls.Add(txtEskiSifre);

            // Yeni Sifre
            Label lblYeniSifre = new Label
            {
                Text = "Yeni Şifre:",
                Location = new Point(40, 120),
                Size = new Size(180, 30),
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 12)
            };
            grpSifreDegistir.Controls.Add(lblYeniSifre);

            txtYeniSifre = new TextBox
            {
                Location = new Point(230, 120),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 12),
                UseSystemPasswordChar = true,
                BackColor = Color.FromArgb(51, 65, 85),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            grpSifreDegistir.Controls.Add(txtYeniSifre);

            // Yeni Sifre Tekrar
            Label lblYeniSifreTekrar = new Label
            {
                Text = "Yeni Şifre (Tekrar):",
                Location = new Point(40, 180),
                Size = new Size(180, 30),
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 12)
            };
            grpSifreDegistir.Controls.Add(lblYeniSifreTekrar);

            txtYeniSifreTekrar = new TextBox
            {
                Location = new Point(230, 180),
                Size = new Size(400, 35),
                Font = new Font("Segoe UI", 12),
                UseSystemPasswordChar = true,
                BackColor = Color.FromArgb(51, 65, 85),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            grpSifreDegistir.Controls.Add(txtYeniSifreTekrar);

            // Sifre Degistir Butonu
            btnSifreDegistir = new Button
            {
                Text = "Şifreyi Değiştir",
                Location = new Point(230, 240),
                Size = new Size(220, 50),
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 12, FontStyle.Bold)
            };
            btnSifreDegistir.FlatAppearance.BorderSize = 0;
            btnSifreDegistir.Click += BtnSifreDegistir_Click;
            grpSifreDegistir.Controls.Add(btnSifreDegistir);
        }

        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            // X butonunu sag uste konumlandir
            btnKapat.Location = new Point(this.ClientSize.Width - btnKapat.Width - 10, 5);
        }

        private void LoadUserInfo()
        {
            var kullanici = SessionManager.AktifKullanici;
            if (kullanici != null)
            {
                lblKullaniciAdi.Text = kullanici.KullaniciAdi;
                lblAdSoyad.Text = kullanici.AdSoyad;
                lblEmail.Text = kullanici.Email;
                lblRol.Text = kullanici.Rol;
            }
        }

        private void BtnSifreDegistir_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasyon
                if (string.IsNullOrWhiteSpace(txtEskiSifre.Text))
                {
                    MessageHelper.ShowWarning("Eski şifrenizi giriniz!");
                    txtEskiSifre.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtYeniSifre.Text))
                {
                    MessageHelper.ShowWarning("Yeni şifrenizi giriniz!");
                    txtYeniSifre.Focus();
                    return;
                }

                if (txtYeniSifre.Text.Length < 6)
                {
                    MessageHelper.ShowWarning("Yeni şifre en az 6 karakter olmalıdır!");
                    txtYeniSifre.Focus();
                    return;
                }

                if (txtYeniSifre.Text != txtYeniSifreTekrar.Text)
                {
                    MessageHelper.ShowWarning("Yeni şifreler eşleşmiyor!");
                    txtYeniSifreTekrar.Focus();
                    return;
                }

                // Eski sifre kontrolu
                string eskiSifreHash = SecurityHelper.HashPassword(txtEskiSifre.Text);
                var kullanici = SessionManager.AktifKullanici;

                if (kullanici.Sifre != eskiSifreHash)
                {
                    MessageHelper.ShowWarning("Eski şifre yanlış!");
                    txtEskiSifre.Focus();
                    return;
                }

                // Sifre degistir
                bool success = _authService.ChangePassword(
                    kullanici.KullaniciAdi,
                    txtEskiSifre.Text,
                    txtYeniSifre.Text
                );

                if (success)
                {
                    MessageHelper.ShowSuccess("Şifreniz başarıyla değiştirildi!");
                    
                    // Formu temizle
                    txtEskiSifre.Clear();
                    txtYeniSifre.Clear();
                    txtYeniSifreTekrar.Clear();
                }
                else
                {
                    MessageHelper.ShowError("Şifre değiştirme işlemi başarısız!");
                }
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Hata oluştu: " + ex.Message);
            }
        }
    }
}
