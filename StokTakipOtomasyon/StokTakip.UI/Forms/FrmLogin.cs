using System;
using System.Drawing;
using System.Windows.Forms;
using StokTakip.Business.Services;
using StokTakip.DataAccess.Models;
using StokTakip.UI.Helpers;

namespace StokTakip.UI.Forms
{
    public partial class FrmLogin : Form
    {
        private readonly AuthService _authService;

        public FrmLogin()
        {
            InitializeComponent();
            _authService = new AuthService();
            
            // Modern X kapama butonu ekle
            CreateCloseButton();
            
            // Enter tuşu ile giriş yapabilme
            this.txtPassword.KeyPress += TxtPassword_KeyPress;
            this.txtUsername.KeyPress += TxtUsername_KeyPress;
        }

        private void CreateCloseButton()
        {
            Button btnClose = new Button
            {
                Text = "✕",
                Size = new System.Drawing.Size(40, 40),
                Location = new System.Drawing.Point(950, 10),
                BackColor = System.Drawing.Color.FromArgb(15, 23, 42),
                ForeColor = System.Drawing.Color.FromArgb(148, 163, 184),
                FlatStyle = FlatStyle.Flat,
                Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Regular),
                Cursor = Cursors.Hand
            };
            btnClose.FlatAppearance.BorderSize = 0;
            btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(30, 41, 59);
            btnClose.Click += (s, e) => this.Close();
            btnClose.MouseEnter += (s, e) => {
                btnClose.BackColor = System.Drawing.Color.FromArgb(239, 68, 68);
                btnClose.ForeColor = System.Drawing.Color.White;
            };
            btnClose.MouseLeave += (s, e) => {
                btnClose.BackColor = System.Drawing.Color.FromArgb(15, 23, 42);
                btnClose.ForeColor = System.Drawing.Color.FromArgb(148, 163, 184);
            };
            
            this.Controls.Add(btnClose);
            btnClose.BringToFront();
        }

        private void TxtPassword_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void TxtUsername_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                txtPassword.Focus();
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                string kullaniciAdi = txtUsername.Text.Trim();
                string sifre = txtPassword.Text;

                // Validasyon
                if (string.IsNullOrWhiteSpace(kullaniciAdi))
                {
                    MessageHelper.ShowWarning("Lütfen kullanıcı adı giriniz!");
                    txtUsername.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(sifre))
                {
                    MessageHelper.ShowWarning("Lütfen şifre giriniz!");
                    txtPassword.Focus();
                    return;
                }

                // Giriş işlemi
                btnLogin.Enabled = false;
                btnLogin.Text = "Giriş yapılıyor...";
                Application.DoEvents();

                Kullanici kullanici = _authService.Login(kullaniciAdi, sifre);

                // Session'a kaydet
                SessionManager.AktifKullanici = kullanici;

                // Ana sayfayı aç
                this.Hide();
                FrmAnaSayfa frmAnaSayfa = new FrmAnaSayfa();
                frmAnaSayfa.FormClosed += (s, args) => this.Close();
                frmAnaSayfa.Show();
            }
            catch (Exception ex)
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "GİRİŞ YAP";
                MessageHelper.ShowError(ex.Message);
                txtPassword.Clear();
                txtPassword.Focus();
            }
        }
    }
}
