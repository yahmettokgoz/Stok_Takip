using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StokTakip.Business.Services;
using StokTakip.DataAccess.Models;
using StokTakip.DataAccess.Repositories;
using StokTakip.UI.Components;
using StokTakip.UI.Helpers;

namespace StokTakip.UI.Forms
{
    public partial class FrmUrunDetay : Form
    {
        private readonly UrunService _urunService;
        private readonly KategoriRepository _kategoriRepository;
        private int? _urunId;
        private bool _isEditMode;

        // Controls
        private TextBox txtBarkod, txtUrunAdi, txtMarka, txtModel, txtRenk;
        private TextBox txtAlisFiyati, txtSatisFiyati, txtStokMiktari, txtMinStok;
        private ComboBox cmbKategori;
        private RichTextBox txtAciklama;
        private ModernButton btnKaydet, btnIptal;
        private Label lblTitle;

        public FrmUrunDetay(int? urunId = null)
        {
            InitializeComponent();
            _urunService = new UrunService();
            _kategoriRepository = new KategoriRepository();
            _urunId = urunId;
            _isEditMode = urunId.HasValue;

            CreateModernUI();
            LoadKategoriler();

            if (_isEditMode)
                LoadUrunData();
        }

        private void CreateModernUI()
        {
            // Form ayarları
            this.Size = new Size(700, 750);
            this.StartPosition = FormStartPosition.CenterParent;
            this.BackColor = ThemeColors.Background;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;

            // Başlık
            lblTitle = new Label
            {
                Text = _isEditMode ? "Ürün Düzenle" : "Yeni Ürün Ekle",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                Location = new Point(30, 20),
                AutoSize = true
            };
            this.Controls.Add(lblTitle);

            int yPos = 80;
            int labelX = 30;
            int inputX = 180;
            int inputWidth = 480;

            // Barkod No
            CreateLabel("Barkod No:", labelX, yPos);
            txtBarkod = CreateTextBox(inputX, yPos, inputWidth);
            yPos += 50;

            // Ürün Adı
            CreateLabel("Ürün Adı:", labelX, yPos);
            txtUrunAdi = CreateTextBox(inputX, yPos, inputWidth);
            yPos += 50;

            // Kategori
            CreateLabel("Kategori:", labelX, yPos);
            cmbKategori = new ComboBox
            {
                Location = new Point(inputX, yPos),
                Width = inputWidth,
                Font = new Font("Segoe UI", 10F),
                BackColor = ThemeColors.CardBackground,
                ForeColor = ThemeColors.TextPrimary,
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat
            };
            this.Controls.Add(cmbKategori);
            yPos += 50;

            // Marka ve Model (yan yana)
            CreateLabel("Marka:", labelX, yPos);
            txtMarka = CreateTextBox(inputX, yPos, 230);
            CreateLabel("Model:", inputX + 250, yPos);
            txtModel = CreateTextBox(inputX + 320, yPos, 160);
            yPos += 50;

            // Renk
            CreateLabel("Renk:", labelX, yPos);
            txtRenk = CreateTextBox(inputX, yPos, 230);
            yPos += 50;

            // Alış ve Satış Fiyatı (yan yana)
            CreateLabel("Alış Fiyatı:", labelX, yPos);
            txtAlisFiyati = CreateTextBox(inputX, yPos, 230);
            txtAlisFiyati.KeyPress += NumberOnly_KeyPress;
            CreateLabel("Satış Fiyatı:", inputX + 250, yPos);
            txtSatisFiyati = CreateTextBox(inputX + 360, yPos, 120);
            txtSatisFiyati.KeyPress += NumberOnly_KeyPress;
            yPos += 50;

            // Stok ve Min Stok (yan yana)
            CreateLabel("Stok Miktarı:", labelX, yPos);
            txtStokMiktari = CreateTextBox(inputX, yPos, 230);
            txtStokMiktari.KeyPress += IntegerOnly_KeyPress;
            CreateLabel("Min. Stok:", inputX + 250, yPos);
            txtMinStok = CreateTextBox(inputX + 360, yPos, 120);
            txtMinStok.KeyPress += IntegerOnly_KeyPress;
            yPos += 50;

            // Açıklama
            CreateLabel("Açıklama:", labelX, yPos);
            txtAciklama = new RichTextBox
            {
                Location = new Point(inputX, yPos),
                Width = inputWidth,
                Height = 80,
                Font = new Font("Segoe UI", 9.5F),
                BackColor = ThemeColors.CardBackground,
                ForeColor = ThemeColors.TextPrimary,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txtAciklama);
            yPos += 100;

            // Butonlar
            btnKaydet = new ModernButton
            {
                Text = "💾 Kaydet",
                Location = new Point(inputX, yPos),
                Size = new Size(230, 45),
                GradientStart = ThemeColors.Success,
                GradientEnd = Color.FromArgb(16, 185, 129)
            };
            btnKaydet.Click += BtnKaydet_Click;
            this.Controls.Add(btnKaydet);

            btnIptal = new ModernButton
            {
                Text = "❌ İptal",
                Location = new Point(inputX + 250, yPos),
                Size = new Size(230, 45),
                GradientStart = ThemeColors.Slate700,
                GradientEnd = ThemeColors.CardBackground
            };
            btnIptal.Click += (s, e) => this.Close();
            this.Controls.Add(btnIptal);
        }

        private Label CreateLabel(string text, int x, int y)
        {
            Label lbl = new Label
            {
                Text = text,
                Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                ForeColor = ThemeColors.TextSecondary,
                Location = new Point(x, y + 5),
                AutoSize = true
            };
            this.Controls.Add(lbl);
            return lbl;
        }

        private TextBox CreateTextBox(int x, int y, int width)
        {
            TextBox txt = new TextBox
            {
                Location = new Point(x, y),
                Width = width,
                Height = 30,
                Font = new Font("Segoe UI", 10F),
                BackColor = ThemeColors.CardBackground,
                ForeColor = ThemeColors.TextPrimary,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txt);
            return txt;
        }

        private void LoadKategoriler()
        {
            try
            {
                var kategoriler = _kategoriRepository.GetAll();
                cmbKategori.DataSource = kategoriler;
                cmbKategori.DisplayMember = "KategoriAdi";
                cmbKategori.ValueMember = "KategoriID";
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Kategoriler yüklenirken hata: " + ex.Message);
            }
        }

        private void LoadUrunData()
        {
            try
            {
                var urun = _urunService.GetById(_urunId.Value);
                if (urun == null)
                {
                    MessageHelper.ShowError("Ürün bulunamadı!");
                    this.Close();
                    return;
                }

                txtBarkod.Text = urun.BarkodNo;
                txtUrunAdi.Text = urun.UrunAdi;
                cmbKategori.SelectedValue = urun.KategoriID;
                txtMarka.Text = urun.Marka;
                txtModel.Text = urun.Model;
                txtRenk.Text = urun.Renk;
                txtAlisFiyati.Text = urun.AlisFiyati.ToString("F2");
                txtSatisFiyati.Text = urun.SatisFiyati.ToString("F2");
                txtStokMiktari.Text = urun.StokMiktari.ToString();
                txtMinStok.Text = urun.MinimumStok.ToString();
                txtAciklama.Text = urun.Aciklama;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Ürün yüklenirken hata: " + ex.Message);
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasyon
                if (string.IsNullOrWhiteSpace(txtBarkod.Text))
                {
                    MessageHelper.ShowWarning("Barkod No boş olamaz!");
                    txtBarkod.Focus();
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtUrunAdi.Text))
                {
                    MessageHelper.ShowWarning("Ürün Adı boş olamaz!");
                    txtUrunAdi.Focus();
                    return;
                }

                if (cmbKategori.SelectedValue == null)
                {
                    MessageHelper.ShowWarning("Kategori seçiniz!");
                    return;
                }

                if (!decimal.TryParse(txtAlisFiyati.Text, out decimal alisFiyati))
                {
                    MessageHelper.ShowWarning("Geçerli bir alış fiyatı giriniz!");
                    txtAlisFiyati.Focus();
                    return;
                }

                if (!decimal.TryParse(txtSatisFiyati.Text, out decimal satisFiyati))
                {
                    MessageHelper.ShowWarning("Geçerli bir satış fiyatı giriniz!");
                    txtSatisFiyati.Focus();
                    return;
                }

                if (!int.TryParse(txtStokMiktari.Text, out int stokMiktari))
                {
                    MessageHelper.ShowWarning("Geçerli bir stok miktarı giriniz!");
                    txtStokMiktari.Focus();
                    return;
                }

                // Ürün nesnesi oluştur
                Urun urun = new Urun
                {
                    BarkodNo = txtBarkod.Text.Trim(),
                    UrunAdi = txtUrunAdi.Text.Trim(),
                    KategoriID = Convert.ToInt32(cmbKategori.SelectedValue),
                    Marka = txtMarka.Text.Trim(),
                    Model = txtModel.Text.Trim(),
                    Renk = txtRenk.Text.Trim(),
                    AlisFiyati = alisFiyati,
                    SatisFiyati = satisFiyati,
                    StokMiktari = stokMiktari,
                    MinimumStok = int.TryParse(txtMinStok.Text, out int minStok) ? minStok : 0,
                    Aciklama = txtAciklama.Text.Trim(),
                    Aktif = true  // Ürün aktif olarak işaretle
                };

                if (_isEditMode)
                {
                    urun.UrunID = _urunId.Value;
                    _urunService.Update(urun);
                    MessageHelper.ShowSuccess("Ürün başarıyla güncellendi!");
                }
                else
                {
                    _urunService.Insert(urun);
                    MessageHelper.ShowSuccess("Ürün başarıyla eklendi!");
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Kayıt sırasında hata: " + ex.Message);
            }
        }

        private void NumberOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != ',' && e.KeyChar != '.')
            {
                e.Handled = true;
            }
        }

        private void IntegerOnly_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
