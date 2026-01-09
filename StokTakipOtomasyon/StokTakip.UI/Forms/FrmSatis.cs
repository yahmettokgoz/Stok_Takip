using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using StokTakip.Business.Services;
using StokTakip.DataAccess.Models;
using StokTakip.UI.Helpers;

namespace StokTakip.UI.Forms
{
    public partial class FrmSatis : Form
    {
        private readonly UrunService _urunService;
        private readonly SatisService _satisService;
        private DataTable dtSepet;
        private TextBox txtArama;
        private DataGridView dgvUrunler;
        private DataGridView dgvSepet;
        private Label lblToplamTutar;
        private Button btnOdemeYap;

        public FrmSatis()
        {
            InitializeComponent();
            _urunService = new UrunService();
            _satisService = new SatisService();
            
            this.Text = "Satış İşlemi";
            this.Size = new Size(1400, 800);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = ThemeColors.Background;

            CreateModernUI();
            LoadUrunler();
            InitializeSepet();
        }

        private void CreateModernUI()
        {
            // Sol Panel - Ürün Listesi
            Panel leftPanel = new Panel
            {
                Location = new Point(20, 20),
                Size = new Size(650, 720),
                BackColor = ThemeColors.CardBackground
            };
            this.Controls.Add(leftPanel);

            // Başlık
            Label lblBaslik = new Label
            {
                Text = "ÜRÜNLER",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = ThemeColors.Primary,
                Location = new Point(20, 15),
                AutoSize = true
            };
            leftPanel.Controls.Add(lblBaslik);

            // Arama kutusu
            txtArama = new TextBox
            {
                Location = new Point(20, 50),
                Size = new Size(610, 35),
                Font = new Font("Segoe UI", 11F),
                BackColor = ThemeColors.Background,
                ForeColor = ThemeColors.TextPrimary,
                BorderStyle = BorderStyle.FixedSingle
            };
            txtArama.TextChanged += TxtArama_TextChanged;
            leftPanel.Controls.Add(txtArama);

            // Ürün DataGridView
            dgvUrunler = new DataGridView
            {
                Location = new Point(20, 95),
                Size = new Size(610, 600),
                BackgroundColor = ThemeColors.Background,
                ForeColor = ThemeColors.TextPrimary,
                GridColor = ThemeColors.Slate700,
                BorderStyle = BorderStyle.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = ThemeColors.CardBackground,
                    ForeColor = ThemeColors.TextPrimary,
                    SelectionBackColor = ThemeColors.Primary,
                    SelectionForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F),
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
            dgvUrunler.CellDoubleClick += DgvUrunler_CellDoubleClick;
            leftPanel.Controls.Add(dgvUrunler);

            // Sağ Panel - Sepet
            Panel rightPanel = new Panel
            {
                Location = new Point(690, 20),
                Size = new Size(670, 720),
                BackColor = ThemeColors.CardBackground
            };
            this.Controls.Add(rightPanel);

            // Sepet Başlık
            Label lblSepetBaslik = new Label
            {
                Text = "SEPET",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = ThemeColors.Success,
                Location = new Point(20, 15),
                AutoSize = true
            };
            rightPanel.Controls.Add(lblSepetBaslik);

            // Sepet DataGridView
            dgvSepet = new DataGridView
            {
                Location = new Point(20, 50),
                Size = new Size(630, 540),
                BackgroundColor = ThemeColors.Background,
                ForeColor = ThemeColors.TextPrimary,
                GridColor = ThemeColors.Slate700,
                BorderStyle = BorderStyle.None,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = ThemeColors.CardBackground,
                    ForeColor = ThemeColors.TextPrimary,
                    SelectionBackColor = ThemeColors.Primary,
                    SelectionForeColor = Color.White,
                    Font = new Font("Segoe UI", 10F),
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
                ReadOnly = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = true,
                RowHeadersVisible = false,
                ColumnHeadersHeight = 40,
                RowTemplate = { Height = 35 }
            };
            dgvSepet.CellValueChanged += DgvSepet_CellValueChanged;
            dgvSepet.KeyDown += DgvSepet_KeyDown;
            rightPanel.Controls.Add(dgvSepet);

            // Toplam Tutar Paneli
            Panel toplamPanel = new Panel
            {
                Location = new Point(20, 600),
                Size = new Size(630, 50),
                BackColor = ThemeColors.Slate700
            };
            rightPanel.Controls.Add(toplamPanel);

            Label lblToplamText = new Label
            {
                Text = "TOPLAM:",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                ForeColor = ThemeColors.TextPrimary,
                Location = new Point(20, 12),
                AutoSize = true
            };
            toplamPanel.Controls.Add(lblToplamText);

            lblToplamTutar = new Label
            {
                Text = "₺0,00",
                Font = new Font("Segoe UI", 18F, FontStyle.Bold),
                ForeColor = ThemeColors.Success,
                Location = new Point(450, 8),
                Size = new Size(160, 35),
                TextAlign = ContentAlignment.MiddleRight
            };
            toplamPanel.Controls.Add(lblToplamTutar);

            // Ödeme Yap Butonu
            btnOdemeYap = new Button
            {
                Text = "✓ ÖDEME YAP",
                Location = new Point(20, 660),
                Size = new Size(630, 50),
                BackColor = ThemeColors.Success,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand
            };
            btnOdemeYap.FlatAppearance.BorderSize = 0;
            btnOdemeYap.Click += BtnOdemeYap_Click;
            rightPanel.Controls.Add(btnOdemeYap);
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
                        u.Marka.ToLower().Contains(aramaMetni.ToLower())
                    ).ToList();
                }

                DataTable dt = new DataTable();
                dt.Columns.Add("UrunID", typeof(int));
                dt.Columns.Add("Ürün Adı", typeof(string));
                dt.Columns.Add("Marka", typeof(string));
                dt.Columns.Add("Stok", typeof(int));
                dt.Columns.Add("Fiyat", typeof(string));

                foreach (var urun in urunler)
                {
                    dt.Rows.Add(
                        urun.UrunID,
                        urun.UrunAdi,
                        urun.Marka,
                        urun.StokMiktari,
                        urun.SatisFiyati.ToString("C2")
                    );
                }

                dgvUrunler.DataSource = dt;
                dgvUrunler.Columns["UrunID"].Visible = false;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Ürünler yüklenirken hata: " + ex.Message);
            }
        }

        private void InitializeSepet()
        {
            dtSepet = new DataTable();
            dtSepet.Columns.Add("UrunID", typeof(int));
            dtSepet.Columns.Add("Ürün Adı", typeof(string));
            dtSepet.Columns.Add("Fiyat", typeof(decimal));
            dtSepet.Columns.Add("Miktar", typeof(int));
            dtSepet.Columns.Add("Toplam", typeof(decimal));

            dgvSepet.DataSource = dtSepet;
            dgvSepet.Columns["UrunID"].Visible = false;
            dgvSepet.Columns["Fiyat"].ReadOnly = true;
            dgvSepet.Columns["Toplam"].ReadOnly = true;
            dgvSepet.Columns["Ürün Adı"].ReadOnly = true;
        }

        private void DgvUrunler_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            try
            {
                int urunId = Convert.ToInt32(dgvUrunler.Rows[e.RowIndex].Cells["UrunID"].Value);
                int mevcutStok = Convert.ToInt32(dgvUrunler.Rows[e.RowIndex].Cells["Stok"].Value);

                if (mevcutStok <= 0)
                {
                    MessageHelper.ShowWarning("Bu ürün stokta yok!");
                    return;
                }

                var urun = _urunService.GetById(urunId);
                
                // Sepette var mı kontrol et
                DataRow existingRow = null;
                foreach (DataRow row in dtSepet.Rows)
                {
                    if (Convert.ToInt32(row["UrunID"]) == urunId)
                    {
                        existingRow = row;
                        break;
                    }
                }

                if (existingRow != null)
                {
                    int mevcutMiktar = Convert.ToInt32(existingRow["Miktar"]);
                    if (mevcutMiktar >= mevcutStok)
                    {
                        MessageHelper.ShowWarning("Stok miktarı aşılamaz!");
                        return;
                    }
                    existingRow["Miktar"] = mevcutMiktar + 1;
                    existingRow["Toplam"] = Convert.ToDecimal(existingRow["Fiyat"]) * (mevcutMiktar + 1);
                }
                else
                {
                    dtSepet.Rows.Add(
                        urun.UrunID,
                        urun.UrunAdi,
                        urun.SatisFiyati,
                        1,
                        urun.SatisFiyati
                    );
                }

                HesaplaToplamTutar();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Ürün eklenirken hata: " + ex.Message);
            }
        }

        private void DgvSepet_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            if (dgvSepet.Columns[e.ColumnIndex].Name == "Miktar")
            {
                try
                {
                    DataGridViewRow row = dgvSepet.Rows[e.RowIndex];
                    int miktar = Convert.ToInt32(row.Cells["Miktar"].Value);
                    decimal fiyat = Convert.ToDecimal(row.Cells["Fiyat"].Value);
                    
                    if (miktar <= 0)
                    {
                        MessageHelper.ShowWarning("Miktar 0'dan büyük olmalı!");
                        row.Cells["Miktar"].Value = 1;
                        return;
                    }

                    row.Cells["Toplam"].Value = fiyat * miktar;
                    HesaplaToplamTutar();
                }
                catch { }
            }
        }

        private void DgvSepet_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && dgvSepet.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvSepet.SelectedRows)
                {
                    if (!row.IsNewRow)
                    {
                        dgvSepet.Rows.Remove(row);
                    }
                }
                HesaplaToplamTutar();
            }
        }

        private void HesaplaToplamTutar()
        {
            decimal toplam = 0;
            foreach (DataRow row in dtSepet.Rows)
            {
                toplam += Convert.ToDecimal(row["Toplam"]);
            }
            lblToplamTutar.Text = toplam.ToString("C2");
        }

        private void TxtArama_TextChanged(object sender, EventArgs e)
        {
            LoadUrunler(txtArama.Text);
        }

        private void BtnOdemeYap_Click(object sender, EventArgs e)
        {
            if (dtSepet.Rows.Count == 0)
            {
                MessageHelper.ShowWarning("Sepet boş! Lütfen ürün ekleyin.");
                return;
            }

            try
            {
                decimal toplamTutar = 0;
                foreach (DataRow row in dtSepet.Rows)
                {
                    toplamTutar += Convert.ToDecimal(row["Toplam"]);
                }

                // Satış kaydı oluştur
                Satis satis = new Satis
                {
                    ToplamTutar = toplamTutar,
                    OdenenTutar = toplamTutar,
                    KalanTutar = 0,
                    OdemeYontemi = "Nakit",
                    IndirimOrani = 0,
                    IndirimTutari = 0,
                    KDVOrani = 0,
                    SatisTarihi = DateTime.Now,
                    Durum = "Tamamlandi"
                };

                // Satış detayları
                List<SatisDetay> detaylar = new List<SatisDetay>();
                foreach (DataRow row in dtSepet.Rows)
                {
                    int urunId = Convert.ToInt32(row["UrunID"]);
                    var urun = _urunService.GetById(urunId);
                    
                    detaylar.Add(new SatisDetay
                    {
                        UrunID = urunId,
                        Miktar = Convert.ToInt32(row["Miktar"]),
                        BirimFiyat = Convert.ToDecimal(row["Fiyat"]),
                        ToplamFiyat = Convert.ToDecimal(row["Toplam"]),
                        AlisFiyati = urun.AlisFiyati
                    });
                }

                // Satışı kaydet
                int personelId = SessionManager.AktifKullanici.KullaniciID;
                _satisService.CreateSatis(satis, detaylar, personelId);

                MessageHelper.ShowSuccess("Satış başarıyla tamamlandı!");
                
                // Sepeti temizle
                dtSepet.Clear();
                HesaplaToplamTutar();
                LoadUrunler(); // Stok güncellemeleri için
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Satış kaydedilirken hata: " + ex.Message);
            }
        }
    }
}
