using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StokTakip.Business.Services;
using StokTakip.DataAccess.Models;
using StokTakip.UI.Components;
using StokTakip.UI.Helpers;

namespace StokTakip.UI.Forms
{
    public partial class FrmUrunler : Form
    {
        private readonly UrunService _urunService;
        private DataTable dtUrunler;

        // Controls
        private Panel pnlTop;
        private TextBox txtArama;
        private ModernButton btnYeniUrun;
        private ModernButton btnDuzenle;
        private ModernButton btnSil;
        private ModernButton btnYenile;
        private DataGridView dgvUrunler;
        private Label lblToplamKayit;

        public FrmUrunler()
        {
            InitializeComponent();
            _urunService = new UrunService();
            CreateModernUI();
            LoadUrunler();
        }

        private void CreateModernUI()
        {
            // Form ayarları
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ThemeColors.Background;
            this.Text = "Ürün Yönetimi";

            // Üst panel (arama ve butonlar)
            pnlTop = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = ThemeColors.CardBackground,
                Padding = new Padding(20)
            };

            // Arama kutusu
            Label lblArama = new Label
            {
                Text = "🔍",
                Font = new Font("Segoe UI", 14F),
                ForeColor = ThemeColors.TextSecondary,
                Location = new Point(20, 30),
                AutoSize = true
            };
            pnlTop.Controls.Add(lblArama);

            txtArama = new TextBox
            {
                Location = new Point(50, 25),
                Width = 300,
                Height = 30,
                Font = new Font("Segoe UI", 11F),
                BackColor = ThemeColors.Background,
                ForeColor = ThemeColors.TextPrimary,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtArama.TextChanged += TxtArama_TextChanged;
            pnlTop.Controls.Add(txtArama);

            // Butonlar
            btnYeniUrun = new ModernButton
            {
                Text = "➕ Yeni Ürün",
                Location = new Point(380, 20),
                Size = new Size(130, 40),
                GradientStart = ThemeColors.Success,
                GradientEnd = Color.FromArgb(16, 185, 129)
            };
            btnYeniUrun.Click += BtnYeniUrun_Click;
            pnlTop.Controls.Add(btnYeniUrun);

            btnDuzenle = new ModernButton
            {
                Text = "✏️ Düzenle",
                Location = new Point(520, 20),
                Size = new Size(120, 40),
                GradientStart = ThemeColors.Primary,
                GradientEnd = Color.FromArgb(37, 99, 235)
            };
            btnDuzenle.Click += BtnDuzenle_Click;
            pnlTop.Controls.Add(btnDuzenle);

            btnSil = new ModernButton
            {
                Text = "🗑️ Sil",
                Location = new Point(650, 20),
                Size = new Size(100, 40),
                GradientStart = ThemeColors.Danger,
                GradientEnd = Color.FromArgb(220, 38, 38)
            };
            btnSil.Click += BtnSil_Click;
            pnlTop.Controls.Add(btnSil);

            btnYenile = new ModernButton
            {
                Text = "🔄 Yenile",
                Location = new Point(760, 20),
                Size = new Size(110, 40),
                GradientStart = ThemeColors.CardBackground,
                GradientEnd = ThemeColors.Slate700
            };
            btnYenile.Click += (s, e) => LoadUrunler();
            pnlTop.Controls.Add(btnYenile);

            // Toplam kayıt sayısı
            lblToplamKayit = new Label
            {
                Text = "Toplam: 0 ürün",
                Location = new Point(900, 30),
                AutoSize = true,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = ThemeColors.TextSecondary
            };
            pnlTop.Controls.Add(lblToplamKayit);

            this.Controls.Add(pnlTop);

            // DataGridView
            CreateDataGridView();
        }

        private void CreateDataGridView()
        {
            dgvUrunler = new DataGridView
            {
                Location = new Point(20, 100),
                Size = new Size(1360, 660),
                BackgroundColor = ThemeColors.Background,
                BorderStyle = BorderStyle.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = ThemeColors.CardBackground,
                    ForeColor = ThemeColors.TextPrimary,
                    SelectionBackColor = ThemeColors.Primary,
                    SelectionForeColor = Color.White,
                    Font = new Font("Segoe UI", 9.5F),
                    Padding = new Padding(5)
                },
                ColumnHeadersDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = ThemeColors.Slate700,
                    ForeColor = ThemeColors.TextPrimary,
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    Padding = new Padding(5),
                    Alignment = DataGridViewContentAlignment.MiddleLeft
                },
                EnableHeadersVisualStyles = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 }
            };

            dgvUrunler.CellDoubleClick += (s, e) => BtnDuzenle_Click(s, e);

            this.Controls.Add(dgvUrunler);
        }

        private void LoadUrunler(string aramaMetni = "")
        {
            try
            {
                var urunler = _urunService.GetAll();

                if (!string.IsNullOrWhiteSpace(aramaMetni))
                {
                    urunler = urunler.Where(u =>
                        u.UrunAdi.ToLower().Contains(aramaMetni.ToLower()) ||
                        u.BarkodNo.Contains(aramaMetni) ||
                        u.Marka.ToLower().Contains(aramaMetni.ToLower())
                    ).ToList();
                }

                dtUrunler = new DataTable();
                dtUrunler.Columns.Add("UrunID", typeof(int));
                dtUrunler.Columns.Add("Barkod No", typeof(string));
                dtUrunler.Columns.Add("Ürün Adı", typeof(string));
                dtUrunler.Columns.Add("Kategori", typeof(string));
                dtUrunler.Columns.Add("Marka", typeof(string));
                dtUrunler.Columns.Add("Renk", typeof(string));
                dtUrunler.Columns.Add("Alış Fiyatı", typeof(string));
                dtUrunler.Columns.Add("Satış Fiyatı", typeof(string));
                dtUrunler.Columns.Add("Stok", typeof(int));
                dtUrunler.Columns.Add("Kar Marjı %", typeof(string));
                dtUrunler.Columns.Add("Durum", typeof(string));

                foreach (var urun in urunler)
                {
                    string durum = urun.StokMiktari <= urun.MinimumStok ? "⚠️ Kritik" : "✅ Normal";
                    dtUrunler.Rows.Add(
                        urun.UrunID,
                        urun.BarkodNo,
                        urun.UrunAdi,
                        urun.KategoriAdi,
                        urun.Marka,
                        urun.Renk,
                        urun.AlisFiyati.ToString("C2"),
                        urun.SatisFiyati.ToString("C2"),
                        urun.StokMiktari,
                        urun.KarMarji.ToString("F2") + "%",
                        durum
                    );
                }

                dgvUrunler.DataSource = dtUrunler;
                dgvUrunler.Columns["UrunID"].Visible = false;

                // Stok sütununa renk
                foreach (DataGridViewRow row in dgvUrunler.Rows)
                {
                    if (row.Cells["Durum"].Value.ToString().Contains("Kritik"))
                    {
                        row.DefaultCellStyle.BackColor = Color.FromArgb(50, 30, 20);
                    }
                }

                lblToplamKayit.Text = $"Toplam: {urunler.Count} ürün";
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Ürünler yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void TxtArama_TextChanged(object sender, EventArgs e)
        {
            LoadUrunler(txtArama.Text);
        }

        private void BtnYeniUrun_Click(object sender, EventArgs e)
        {
            FrmUrunDetay frm = new FrmUrunDetay();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtArama.Text = "";  // Arama kutusunu temizle
                LoadUrunler();
            }
        }

        private void BtnDuzenle_Click(object sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count == 0)
            {
                MessageHelper.ShowWarning("Lütfen düzenlemek istediğiniz ürünü seçin!");
                return;
            }

            int urunId = Convert.ToInt32(dgvUrunler.SelectedRows[0].Cells["UrunID"].Value);
            FrmUrunDetay frm = new FrmUrunDetay(urunId);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                txtArama.Text = "";  // Arama kutusunu temizle
                LoadUrunler();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            if (dgvUrunler.SelectedRows.Count == 0)
            {
                MessageHelper.ShowWarning("Lütfen silmek istediğiniz ürünü seçin!");
                return;
            }

            int urunId = Convert.ToInt32(dgvUrunler.SelectedRows[0].Cells["UrunID"].Value);
            string urunAdi = dgvUrunler.SelectedRows[0].Cells["Ürün Adı"].Value.ToString();

            if (!MessageHelper.ShowConfirm($"'{urunAdi}' ürününü silmek istediğinizden emin misiniz?"))
                return;

            try
            {
                _urunService.Delete(urunId);
                MessageHelper.ShowSuccess("Ürün başarıyla silindi!");
                LoadUrunler();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Ürün silinirken hata oluştu: " + ex.Message);
            }
        }
    }
}
