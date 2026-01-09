using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using StokTakip.Business.Services;
using StokTakip.UI.Components;
using StokTakip.UI.Helpers;

namespace StokTakip.UI.Forms
{
    public partial class FrmStokHareketleri : Form
    {
        private readonly StokService _stokService;
        private readonly UrunService _urunService;

        public FrmStokHareketleri()
        {
            InitializeComponent();
            _stokService = new StokService();
            _urunService = new UrunService();
        }

        private void FrmStokHareketleri_Load(object sender, EventArgs e)
        {
            LoadHareketTipleri();
            LoadUrunler();
            dtpBaslangic.Value = DateTime.Now.AddMonths(-1);
            dtpBitis.Value = DateTime.Now;
            LoadStokHareketleri();
        }

        private void LoadHareketTipleri()
        {
            cmbHareketTipi.Items.Add("Tümü");
            cmbHareketTipi.Items.Add("Stok Girişi");
            cmbHareketTipi.Items.Add("Satış");
            cmbHareketTipi.Items.Add("İade");
            cmbHareketTipi.Items.Add("Fire/Kayıp");
            cmbHareketTipi.SelectedIndex = 0;
        }

        private void LoadUrunler()
        {
            cmbUrun.Items.Add(new UrunItem { UrunID = 0, UrunAdi = "Tüm Ürünler" });
            var urunler = _urunService.GetAll();
            foreach (var urun in urunler)
            {
                cmbUrun.Items.Add(new UrunItem { UrunID = urun.UrunID, UrunAdi = urun.UrunAdi });
            }
            cmbUrun.DisplayMember = "UrunAdi";
            cmbUrun.ValueMember = "UrunID";
            cmbUrun.SelectedIndex = 0;
        }

        private void LoadStokHareketleri()
        {
            try
            {
                int urunID = 0;
                if (cmbUrun.SelectedItem != null)
                {
                    var selectedItem = (UrunItem)cmbUrun.SelectedItem;
                    urunID = selectedItem.UrunID;
                }

                string hareketTipi = cmbHareketTipi.SelectedItem?.ToString();
                if (hareketTipi == "Tümü") hareketTipi = null;

                var hareketler = _stokService.GetHareketler(
                    dtpBaslangic.Value.Date,
                    dtpBitis.Value.Date.AddDays(1).AddSeconds(-1),
                    urunID == 0 ? (int?)null : urunID,
                    hareketTipi
                );

                dgvHareketler.DataSource = hareketler;

                // Kolonları ayarla
                if (dgvHareketler.Columns.Count > 0)
                {
                    // Gizle
                    if (dgvHareketler.Columns["HareketID"] != null)
                        dgvHareketler.Columns["HareketID"].Visible = false;
                    if (dgvHareketler.Columns["UrunID"] != null)
                        dgvHareketler.Columns["UrunID"].Visible = false;
                    if (dgvHareketler.Columns["KullaniciID"] != null)
                        dgvHareketler.Columns["KullaniciID"].Visible = false;

                    // Göster ve düzenle
                    if (dgvHareketler.Columns["HareketTarihi"] != null)
                    {
                        dgvHareketler.Columns["HareketTarihi"].HeaderText = "Tarih";
                        dgvHareketler.Columns["HareketTarihi"].Width = 140;
                        dgvHareketler.Columns["HareketTarihi"].DefaultCellStyle.Format = "dd.MM.yyyy HH:mm";
                    }
                    if (dgvHareketler.Columns["UrunAdi"] != null)
                    {
                        dgvHareketler.Columns["UrunAdi"].HeaderText = "Ürün";
                        dgvHareketler.Columns["UrunAdi"].Width = 250;
                    }
                    if (dgvHareketler.Columns["HareketTipi"] != null)
                    {
                        dgvHareketler.Columns["HareketTipi"].HeaderText = "İşlem Tipi";
                        dgvHareketler.Columns["HareketTipi"].Width = 120;
                    }
                    if (dgvHareketler.Columns["Miktar"] != null)
                    {
                        dgvHareketler.Columns["Miktar"].HeaderText = "Miktar";
                        dgvHareketler.Columns["Miktar"].Width = 80;
                    }
                    if (dgvHareketler.Columns["OncekiStok"] != null)
                    {
                        dgvHareketler.Columns["OncekiStok"].HeaderText = "Önceki";
                        dgvHareketler.Columns["OncekiStok"].Width = 80;
                    }
                    if (dgvHareketler.Columns["SonrakiStok"] != null)
                    {
                        dgvHareketler.Columns["SonrakiStok"].HeaderText = "Sonraki";
                        dgvHareketler.Columns["SonrakiStok"].Width = 80;
                    }
                    if (dgvHareketler.Columns["BirimFiyat"] != null)
                    {
                        dgvHareketler.Columns["BirimFiyat"].HeaderText = "Birim Fiyat";
                        dgvHareketler.Columns["BirimFiyat"].Width = 100;
                        dgvHareketler.Columns["BirimFiyat"].DefaultCellStyle.Format = "C2";
                    }
                    if (dgvHareketler.Columns["Aciklama"] != null)
                    {
                        dgvHareketler.Columns["Aciklama"].HeaderText = "Açıklama";
                        dgvHareketler.Columns["Aciklama"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                    }
                    if (dgvHareketler.Columns["KullaniciAdi"] != null)
                    {
                        dgvHareketler.Columns["KullaniciAdi"].HeaderText = "Kullanıcı";
                        dgvHareketler.Columns["KullaniciAdi"].Width = 100;
                    }
                }

                lblToplamHareket.Text = $"Toplam {hareketler.Count} hareket kaydı";
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Hareketler yüklenirken hata: " + ex.Message);
            }
        }

        private void DgvHareketler_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Hareket tipine göre renklendirme
            if (dgvHareketler.Columns["HareketTipi"] != null)
            {
                int hareketTipiIndex = dgvHareketler.Columns["HareketTipi"].Index;
                var hareketTipi = dgvHareketler.Rows[e.RowIndex].Cells[hareketTipiIndex].Value?.ToString();

                if (hareketTipi == "Stok Girişi" || hareketTipi == "İade")
                {
                    dgvHareketler.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(220, 252, 231);
                    dgvHareketler.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(21, 128, 61);
                }
                else if (hareketTipi == "Satış" || hareketTipi == "Fire/Kayıp")
                {
                    dgvHareketler.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 226, 226);
                    dgvHareketler.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(153, 27, 27);
                }
            }
        }

        private void BtnFiltrele_Click(object sender, EventArgs e)
        {
            LoadStokHareketleri();
        }

        private void BtnYeniHareket_Click(object sender, EventArgs e)
        {
            FrmStokHareketDetay frmDetay = new FrmStokHareketDetay();
            if (frmDetay.ShowDialog() == DialogResult.OK)
            {
                LoadStokHareketleri();
            }
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private class UrunItem
        {
            public int UrunID { get; set; }
            public string UrunAdi { get; set; }
        }
    }
}
