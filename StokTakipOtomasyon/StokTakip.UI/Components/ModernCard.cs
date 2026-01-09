using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StokTakip.UI.Components
{
    /// <summary>
    /// Modern kart bileşeni - Ürünler ve dashboard widget'ları için
    /// </summary>
    public class ModernCard : Panel
    {
        private bool isHovered = false;
        private Color borderColor = Color.FromArgb(71, 85, 105);
        private Color hoverBorderColor = Color.FromArgb(59, 130, 246);
        private int borderRadius = 12;
        private int borderWidth = 2;

        public ModernCard()
        {
            // Temel ayarlar
            this.BackColor = Color.FromArgb(30, 41, 59);
            this.ForeColor = Color.FromArgb(248, 250, 252);
            this.Padding = new Padding(15);
            this.Cursor = Cursors.Hand;
            
            // Double buffering - flicker önleme
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.UserPaint | 
                         ControlStyles.AllPaintingInWmPaint | 
                         ControlStyles.OptimizedDoubleBuffer, true);

            // Event'ler
            this.MouseEnter += ModernCard_MouseEnter;
            this.MouseLeave += ModernCard_MouseLeave;
            this.Paint += ModernCard_Paint;
        }

        // Properties
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; Invalidate(); }
        }

        public Color BorderColor
        {
            get => borderColor;
            set { borderColor = value; Invalidate(); }
        }

        public Color HoverBorderColor
        {
            get => hoverBorderColor;
            set { hoverBorderColor = value; Invalidate(); }
        }

        private void ModernCard_MouseEnter(object sender, EventArgs e)
        {
            isHovered = true;
            this.BackColor = Color.FromArgb(51, 65, 85);
            Invalidate();
        }

        private void ModernCard_MouseLeave(object sender, EventArgs e)
        {
            isHovered = false;
            this.BackColor = Color.FromArgb(30, 41, 59);
            Invalidate();
        }

        private void ModernCard_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Border çiz
            using (GraphicsPath path = GetRoundedRectangle(ClientRectangle, borderRadius))
            {
                using (Pen pen = new Pen(isHovered ? hoverBorderColor : borderColor, borderWidth))
                {
                    g.DrawPath(pen, path);
                }
            }
        }

        private GraphicsPath GetRoundedRectangle(Rectangle bounds, int radius)
        {
            int diameter = radius * 2;
            GraphicsPath path = new GraphicsPath();

            // Köşeleri yuvarla
            path.AddArc(bounds.X, bounds.Y, diameter, diameter, 180, 90);
            path.AddArc(bounds.Right - diameter, bounds.Y, diameter, diameter, 270, 90);
            path.AddArc(bounds.Right - diameter, bounds.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(bounds.X, bounds.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();

            return path;
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }
    }
}
