using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StokTakip.UI.Components
{
    /// <summary>
    /// Dashboard için modern widget/kart bileşeni
    /// </summary>
    public class DashboardWidget : Panel
    {
        private Label lblTitle;
        private Label lblValue;
        private Label lblSubtext;
        private Panel iconPanel;
        
        private string widgetTitle = "Başlık";
        private string widgetValue = "0";
        private string widgetSubtext = "";
        private Color accentColor = Color.FromArgb(59, 130, 246);
        private string iconText = "📊";

        public DashboardWidget()
        {
            InitializeComponents();
            this.BackColor = Color.FromArgb(30, 41, 59);
            this.Size = new Size(250, 120);
            this.Padding = new Padding(20);
            
            // Double buffering
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint | 
                         ControlStyles.AllPaintingInWmPaint | 
                         ControlStyles.OptimizedDoubleBuffer, true);

            this.Paint += DashboardWidget_Paint;
        }

        private void InitializeComponents()
        {
            // Icon Panel
            iconPanel = new Panel
            {
                Size = new Size(50, 50),
                Location = new Point(20, 15),
                BackColor = Color.FromArgb(59, 130, 246)
            };
            iconPanel.Paint += IconPanel_Paint;

            // Title Label
            lblTitle = new Label
            {
                Text = widgetTitle,
                ForeColor = Color.FromArgb(148, 163, 184),
                Font = new Font("Segoe UI", 9F, FontStyle.Regular),
                Location = new Point(80, 15),
                AutoSize = true
            };

            // Value Label
            lblValue = new Label
            {
                Text = widgetValue,
                ForeColor = Color.FromArgb(248, 250, 252),
                Font = new Font("Segoe UI", 24F, FontStyle.Bold),
                Location = new Point(80, 35),
                AutoSize = true
            };

            // Subtext Label
            lblSubtext = new Label
            {
                Text = widgetSubtext,
                ForeColor = Color.FromArgb(100, 116, 139),
                Font = new Font("Segoe UI", 8F, FontStyle.Regular),
                Location = new Point(80, 85),
                AutoSize = true
            };

            this.Controls.Add(iconPanel);
            this.Controls.Add(lblTitle);
            this.Controls.Add(lblValue);
            this.Controls.Add(lblSubtext);
        }

        // Properties
        public string WidgetTitle
        {
            get => widgetTitle;
            set { widgetTitle = value; lblTitle.Text = value; }
        }

        public string WidgetValue
        {
            get => widgetValue;
            set { widgetValue = value; lblValue.Text = value; }
        }

        public string WidgetSubtext
        {
            get => widgetSubtext;
            set { widgetSubtext = value; lblSubtext.Text = value; }
        }

        public Color AccentColor
        {
            get => accentColor;
            set { accentColor = value; iconPanel.BackColor = value; Invalidate(); }
        }

        public string IconText
        {
            get => iconText;
            set { iconText = value; iconPanel.Invalidate(); }
        }

        private void IconPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Rounded rectangle
            using (GraphicsPath path = GetRoundedRectangle(iconPanel.ClientRectangle, 10))
            {
                iconPanel.Region = new Region(path);
            }

            // Icon text çiz
            using (Font iconFont = new Font("Segoe UI Emoji", 20F))
            {
                SizeF textSize = g.MeasureString(iconText, iconFont);
                PointF textLocation = new PointF(
                    (iconPanel.Width - textSize.Width) / 2,
                    (iconPanel.Height - textSize.Height) / 2
                );
                g.DrawString(iconText, iconFont, Brushes.White, textLocation);
            }
        }

        private void DashboardWidget_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Border ve rounded corners
            using (GraphicsPath path = GetRoundedRectangle(ClientRectangle, 12))
            {
                using (Pen pen = new Pen(Color.FromArgb(71, 85, 105), 2))
                {
                    g.DrawPath(pen, path);
                }
            }

            // Accent line (sol tarafta)
            using (Pen accentPen = new Pen(accentColor, 4))
            {
                g.DrawLine(accentPen, 0, 15, 0, Height - 15);
            }
        }

        private GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }
    }
}
