using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using StokTakip.Business.Services;
using StokTakip.UI.Helpers;

namespace StokTakip.UI.Forms
{
    public partial class FrmRaporlar : Form
    {
        private RaporService _raporService;
        private Chart chartGunlukSatis;
        private Chart chartEnCokSatan;
        private Chart chartKategoriBazli;
        private Chart chartAylikTrend;
        private DateTimePicker dtpBaslangic;
        private DateTimePicker dtpBitis;
        private Button btnGoster;
        private Panel pnlHeader;
        private Button btnKapat;
        private Label lblBaslik;

        public FrmRaporlar()
        {
            _raporService = new RaporService();
            InitializeComponent();
            InitializeCustomComponents();
            this.Load += FrmRaporlar_Load;
            LoadDefaultReports();
        }

        private void FrmRaporlar_Load(object sender, EventArgs e)
        {
            // X butonunu sağ üste konumlandır
            btnKapat.Location = new Point(this.ClientSize.Width - btnKapat.Width - 10, 5);
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmRaporlar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = Color.FromArgb(15, 23, 42);
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmRaporlar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Raporlar";
            this.WindowState = FormWindowState.Maximized;
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
                Text = "RAPORLAR VE ANALİZLER",
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

            // Tarih filtreleri paneli
            Panel pnlFiltre = new Panel
            {
                Location = new Point(20, 80),
                Size = new Size(1560, 60),
                BackColor = Color.FromArgb(30, 41, 59)
            };
            this.Controls.Add(pnlFiltre);

            Label lblBaslangic = new Label
            {
                Text = "Başlangıç:",
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };
            pnlFiltre.Controls.Add(lblBaslangic);

            dtpBaslangic = new DateTimePicker
            {
                Location = new Point(120, 17),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now.AddDays(-30)
            };
            pnlFiltre.Controls.Add(dtpBaslangic);

            Label lblBitis = new Label
            {
                Text = "Bitiş:",
                ForeColor = Color.White,
                Location = new Point(350, 20),
                AutoSize = true
            };
            pnlFiltre.Controls.Add(lblBitis);

            dtpBitis = new DateTimePicker
            {
                Location = new Point(410, 17),
                Width = 200,
                Format = DateTimePickerFormat.Short,
                Value = DateTime.Now
            };
            pnlFiltre.Controls.Add(dtpBitis);

            btnGoster = new Button
            {
                Text = "Raporları Göster",
                Location = new Point(640, 12),
                Size = new Size(150, 35),
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnGoster.FlatAppearance.BorderSize = 0;
            btnGoster.Click += BtnGoster_Click;
            pnlFiltre.Controls.Add(btnGoster);

            // Gunluk Satis Grafigi
            chartGunlukSatis = CreateChart("GÜNLÜK SATIŞ TRENDI", new Point(20, 160), new Size(760, 340));
            this.Controls.Add(chartGunlukSatis);

            // En Cok Satan Urunler Grafigi
            chartEnCokSatan = CreateChart("EN ÇOK SATAN ÜRÜNLER (TOP 10)", new Point(800, 160), new Size(780, 340));
            this.Controls.Add(chartEnCokSatan);

            // Kategori Bazli Satis Grafigi
            chartKategoriBazli = CreateChart("KATEGORİ BAZLI SATIŞLAR", new Point(20, 520), new Size(760, 340));
            this.Controls.Add(chartKategoriBazli);

            // Aylik Trend Grafigi
            chartAylikTrend = CreateChart("AYLIK SATIŞ TRENDİ", new Point(800, 520), new Size(780, 340));
            this.Controls.Add(chartAylikTrend);
        }

        private Chart CreateChart(string title, Point location, Size size)
        {
            Chart chart = new Chart
            {
                Location = location,
                Size = size,
                BackColor = Color.FromArgb(30, 41, 59),
                ForeColor = Color.White
            };

            // Chart Area
            ChartArea chartArea = new ChartArea
            {
                BackColor = Color.FromArgb(30, 41, 59),
                BorderWidth = 0
            };
            chartArea.AxisX.LabelStyle.ForeColor = Color.White;
            chartArea.AxisX.LineColor = Color.FromArgb(71, 85, 105);
            chartArea.AxisX.MajorGrid.LineColor = Color.FromArgb(51, 65, 85);
            chartArea.AxisY.LabelStyle.ForeColor = Color.White;
            chartArea.AxisY.LineColor = Color.FromArgb(71, 85, 105);
            chartArea.AxisY.MajorGrid.LineColor = Color.FromArgb(51, 65, 85);
            chart.ChartAreas.Add(chartArea);

            // Title
            Title chartTitle = new Title
            {
                Text = title,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.White,
                Docking = Docking.Top
            };
            chart.Titles.Add(chartTitle);

            // Legend
            Legend legend = new Legend
            {
                ForeColor = Color.White,
                BackColor = Color.Transparent,
                Docking = Docking.Bottom
            };
            chart.Legends.Add(legend);

            return chart;
        }

        private void LoadDefaultReports()
        {
            try
            {
                LoadGunlukSatisGrafigi();
                LoadEnCokSatanGrafigi();
                LoadKategoriBazliGrafik();
                LoadAylikTrendGrafigi();
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Raporlar yüklenirken hata oluştu: " + ex.Message);
            }
        }

        private void BtnGoster_Click(object sender, EventArgs e)
        {
            if (dtpBitis.Value < dtpBaslangic.Value)
            {
                MessageHelper.ShowWarning("Bitiş tarihi başlangıç tarihinden küçük olamaz!");
                return;
            }

            LoadGunlukSatisGrafigi();
        }

        private void LoadGunlukSatisGrafigi()
        {
            try
            {
                chartGunlukSatis.Series.Clear();

                DataTable dt = _raporService.GetGunlukSatisRaporu(dtpBaslangic.Value, dtpBitis.Value);

                if (dt.Rows.Count == 0)
                {
                    return;
                }

                Series seriesSatis = new Series("Satış Sayısı")
                {
                    ChartType = SeriesChartType.Column,
                    Color = Color.FromArgb(59, 130, 246),
                    BorderWidth = 2
                };

                Series seriesCiro = new Series("Toplam Ciro (TL)")
                {
                    ChartType = SeriesChartType.Line,
                    Color = Color.FromArgb(34, 197, 94),
                    BorderWidth = 3,
                    MarkerStyle = MarkerStyle.Circle,
                    MarkerSize = 8,
                    YAxisType = AxisType.Secondary
                };

                foreach (DataRow row in dt.Rows)
                {
                    string tarih = Convert.ToDateTime(row["Tarih"]).ToString("dd/MM");
                    int satisSayisi = Convert.ToInt32(row["SatisSayisi"]);
                    decimal toplamCiro = Convert.ToDecimal(row["ToplamCiro"]);

                    seriesSatis.Points.AddXY(tarih, satisSayisi);
                    seriesCiro.Points.AddXY(tarih, toplamCiro);
                }

                chartGunlukSatis.Series.Add(seriesSatis);
                chartGunlukSatis.Series.Add(seriesCiro);

                // Y ekseni ayarlari
                chartGunlukSatis.ChartAreas[0].AxisY2.Enabled = AxisEnabled.True;
                chartGunlukSatis.ChartAreas[0].AxisY2.LabelStyle.ForeColor = Color.White;
                chartGunlukSatis.ChartAreas[0].AxisY2.LineColor = Color.FromArgb(71, 85, 105);
                chartGunlukSatis.ChartAreas[0].AxisY2.MajorGrid.LineColor = Color.FromArgb(51, 65, 85);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Günlük satış grafiği yüklenirken hata: " + ex.Message);
            }
        }

        private void LoadEnCokSatanGrafigi()
        {
            try
            {
                chartEnCokSatan.Series.Clear();

                DataTable dt = _raporService.GetEnCokSatanUrunler(10);

                if (dt.Rows.Count == 0)
                {
                    return;
                }

                Series series = new Series("Satış Miktarı")
                {
                    ChartType = SeriesChartType.Bar,
                    Palette = ChartColorPalette.BrightPastel
                };

                foreach (DataRow row in dt.Rows)
                {
                    string urunAdi = row["UrunAdi"].ToString();
                    if (urunAdi.Length > 20)
                        urunAdi = urunAdi.Substring(0, 20) + "...";
                    
                    int miktar = Convert.ToInt32(row["ToplamSatisMiktari"]);
                    series.Points.AddXY(urunAdi, miktar);
                }

                chartEnCokSatan.Series.Add(series);
                chartEnCokSatan.ChartAreas[0].AxisX.Interval = 1;
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("En çok satan ürünler grafiği yüklenirken hata: " + ex.Message);
            }
        }

        private void LoadKategoriBazliGrafik()
        {
            try
            {
                chartKategoriBazli.Series.Clear();

                DataTable dt = _raporService.GetKategoriyeGoreSatislar();

                if (dt.Rows.Count == 0)
                {
                    return;
                }

                Series series = new Series("Kategori Satışları")
                {
                    ChartType = SeriesChartType.Pie,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold)
                };

                series["PieLabelStyle"] = "Outside";
                series["PieLineColor"] = "White";

                Color[] colors = new Color[]
                {
                    Color.FromArgb(59, 130, 246),   // Blue
                    Color.FromArgb(34, 197, 94),    // Green
                    Color.FromArgb(249, 115, 22),   // Orange
                    Color.FromArgb(168, 85, 247),   // Purple
                    Color.FromArgb(236, 72, 153),   // Pink
                    Color.FromArgb(234, 179, 8)     // Yellow
                };

                int colorIndex = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string kategori = row["KategoriAdi"].ToString();
                    decimal ciro = Convert.ToDecimal(row["ToplamCiro"]);
                    
                    DataPoint point = new DataPoint();
                    point.SetValueXY(kategori, ciro);
                    point.Label = kategori + "\n₺" + ciro.ToString("N0");
                    point.LabelForeColor = Color.White;
                    point.Color = colors[colorIndex % colors.Length];
                    
                    series.Points.Add(point);
                    colorIndex++;
                }

                chartKategoriBazli.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Kategori bazlı grafik yüklenirken hata: " + ex.Message);
            }
        }

        private void LoadAylikTrendGrafigi()
        {
            try
            {
                chartAylikTrend.Series.Clear();

                DataTable dt = _raporService.GetAylikSatisTrendi(DateTime.Now.Year);

                if (dt.Rows.Count == 0)
                {
                    return;
                }

                Series series = new Series("Aylık Ciro")
                {
                    ChartType = SeriesChartType.SplineArea,
                    Color = Color.FromArgb(150, 59, 130, 246),
                    BorderColor = Color.FromArgb(59, 130, 246),
                    BorderWidth = 3
                };

                string[] aylar = { "", "Ocak", "Şubat", "Mart", "Nisan", "Mayıs", "Haziran",
                                  "Temmuz", "Ağustos", "Eylül", "Ekim", "Kasım", "Aralık" };

                foreach (DataRow row in dt.Rows)
                {
                    int ay = Convert.ToInt32(row["Ay"]);
                    decimal ciro = Convert.ToDecimal(row["ToplamCiro"]);
                    series.Points.AddXY(aylar[ay], ciro);
                }

                chartAylikTrend.Series.Add(series);
            }
            catch (Exception ex)
            {
                MessageHelper.ShowError("Aylık trend grafiği yüklenirken hata: " + ex.Message);
            }
        }
    }
}
