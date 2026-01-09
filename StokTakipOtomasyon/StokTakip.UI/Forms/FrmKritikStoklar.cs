using System;
using System.Drawing;
using System.Windows.Forms;
using StokTakip.Business.Services;
using StokTakip.UI.Helpers;

namespace StokTakip.UI.Forms
{
    public partial class FrmKritikStoklar : Form
    {
        private readonly UrunService _urunService;

        public FrmKritikStoklar()
        {
            InitializeComponent();
            _urunService = new UrunService();
        }

        private void FrmKritikStoklar_Load(object sender, EventArgs e)
        {
            LoadKritikStoklar();
        }

        private void LoadKritikStoklar()
        {
            var kritikUrunler = _urunService.GetCriticalStock();
            dgvKritikStoklar.DataSource = kritikUrunler;

            // Tüm kolonları gizle
            foreach (DataGridViewColumn col in dgvKritikStoklar.Columns)
            {
                col.Visible = false;
            }

            // Sadece istediğimiz kolonları göster ve ayarla
            if (dgvKritikStoklar.Columns["UrunAdi"] != null)
            {
                dgvKritikStoklar.Columns["UrunAdi"].Visible = true;
                dgvKritikStoklar.Columns["UrunAdi"].HeaderText = "Ürün Adı";
                dgvKritikStoklar.Columns["UrunAdi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvKritikStoklar.Columns["UrunAdi"].FillWeight = 40;
                dgvKritikStoklar.Columns["UrunAdi"].DisplayIndex = 0;
            }
            if (dgvKritikStoklar.Columns["KategoriAdi"] != null)
            {
                dgvKritikStoklar.Columns["KategoriAdi"].Visible = true;
                dgvKritikStoklar.Columns["KategoriAdi"].HeaderText = "Kategori";
                dgvKritikStoklar.Columns["KategoriAdi"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvKritikStoklar.Columns["KategoriAdi"].FillWeight = 20;
                dgvKritikStoklar.Columns["KategoriAdi"].DisplayIndex = 1;
            }
            if (dgvKritikStoklar.Columns["Marka"] != null)
            {
                dgvKritikStoklar.Columns["Marka"].Visible = true;
                dgvKritikStoklar.Columns["Marka"].HeaderText = "Marka";
                dgvKritikStoklar.Columns["Marka"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvKritikStoklar.Columns["Marka"].FillWeight = 20;
                dgvKritikStoklar.Columns["Marka"].DisplayIndex = 2;
            }
            if (dgvKritikStoklar.Columns["StokMiktari"] != null)
            {
                dgvKritikStoklar.Columns["StokMiktari"].Visible = true;
                dgvKritikStoklar.Columns["StokMiktari"].HeaderText = "Mevcut";
                dgvKritikStoklar.Columns["StokMiktari"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvKritikStoklar.Columns["StokMiktari"].FillWeight = 10;
                dgvKritikStoklar.Columns["StokMiktari"].DisplayIndex = 3;
            }
            if (dgvKritikStoklar.Columns["MinimumStok"] != null)
            {
                dgvKritikStoklar.Columns["MinimumStok"].Visible = true;
                dgvKritikStoklar.Columns["MinimumStok"].HeaderText = "Minimum";
                dgvKritikStoklar.Columns["MinimumStok"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                dgvKritikStoklar.Columns["MinimumStok"].FillWeight = 10;
                dgvKritikStoklar.Columns["MinimumStok"].DisplayIndex = 4;
            }

            lblToplamKritik.Text = $"Toplam {kritikUrunler.Count} ürün kritik seviyede";
        }

        private void DgvKritikStoklar_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            // Tüm satırı kırmızı yap
            if (e.RowIndex >= 0)
            {
                dgvKritikStoklar.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(254, 226, 226);
                dgvKritikStoklar.Rows[e.RowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(153, 27, 27);
            }
        }

        private void BtnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
