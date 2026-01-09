using System;
using System.Drawing;
using System.Windows.Forms;
using StokTakip.Business.Services;
using StokTakip.DataAccess.Models;
using StokTakip.UI.Helpers;

namespace StokTakip.UI.Forms
{
    public partial class FrmStokHareketDetay : Form
    {
        private readonly StokService _stokService;
        private readonly UrunService _urunService;

        public FrmStokHareketDetay()
        {
            InitializeComponent();
            _stokService = new StokService();
            _urunService = new UrunService();
        }

        private void FrmStokHareketDetay_Load(object sender, EventArgs e)
        {
            LoadHareketTipleri();
            LoadUrunler();
        }

        private void LoadHareketTipleri()
        {
            cmbHareketTipi.Items.Add("Stok Girişi");
            cmbHareketTipi.Items.Add("İade");
            cmbHareketTipi.Items.Add("Fire/Kayıp");
            cmbHareketTipi.SelectedIndex = 0;
        }

        private void LoadUrunler()
        {
            var urunler = _urunService.GetAll();
            foreach (var urun in urunler)
            {
                cmbUrun.Items.Add(new UrunItem 
                { 
                    UrunID = urun.UrunID, 
                    UrunAdi = $"{urun.UrunAdi} (Stok: {urun.StokMiktari})" 
                });
            }
            cmbUrun.DisplayMember = "UrunAdi";
            cmbUrun.ValueMember = "UrunID";
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                // Validasyon
                if (cmbUrun.SelectedItem == null)
                {
                    MessageHelper.ShowWarning("Lütfen ürün seçiniz.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtMiktar.Text) || !int.TryParse(txtMiktar.Text, out int miktar) || miktar <= 0)
                {
                    MessageHelper.ShowWarning("Lütfen geçerli bir miktar giriniz.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtBirimFiyat.Text) || !decimal.TryParse(txtBirimFiyat.Text, out decimal birimFiyat) || birimFiyat < 0)
                {
                    MessageHelper.ShowWarning("Lütfen geçerli bir birim fiyat giriniz.");
                    return;
                }

                var selectedItem = (UrunItem)cmbUrun.SelectedItem;
                int urunID = selectedItem.UrunID;

                var hareket = new StokHareket
                {
                    UrunID = urunID,
                    HareketTipi = cmbHareketTipi.SelectedItem.ToString(),
                    Miktar = miktar,
                    BirimFiyat = birimFiyat,
                    Aciklama = txtAciklama.Text,
                    PersonelID = SessionManager.AktifKullanici.KullaniciID,
                    HareketTarihi = DateTime.Now
                };

                _stokService.AddHareket(hareket);

                MessageHelper.ShowSuccess("Stok hareketi başarıyla kaydedildi.");
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Kayıt sırasında hata: " + ex.Message);
            }
        }

        private void BtnIptal_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private class UrunItem
        {
            public int UrunID { get; set; }
            public string UrunAdi { get; set; }
        }
    }
}
