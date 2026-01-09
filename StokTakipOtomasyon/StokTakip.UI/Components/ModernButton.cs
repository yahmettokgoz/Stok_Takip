using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace StokTakip.UI.Components
{
    /// <summary>
    /// Modern buton bileşeni - Gradient ve hover efektli
    /// </summary>
    public class ModernButton : Button
    {
        private bool isHovered = false;
        private Color gradientStart = Color.FromArgb(59, 130, 246);
        private Color gradientEnd = Color.FromArgb(37, 99, 235);
        private int borderRadius = 8;

        public ModernButton()
        {
            // Temel ayarlar
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.BackColor = Color.Transparent;
            this.ForeColor = Color.White;
            this.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            this.Cursor = Cursors.Hand;
            this.Size = new Size(120, 40);
            
            // Double buffering
            this.SetStyle(ControlStyles.UserPaint | 
                         ControlStyles.AllPaintingInWmPaint | 
                         ControlStyles.OptimizedDoubleBuffer, true);

            // Event'ler
            this.MouseEnter += ModernButton_MouseEnter;
            this.MouseLeave += ModernButton_MouseLeave;
            this.Paint += ModernButton_Paint;
        }

        // Properties
        public int BorderRadius
        {
            get => borderRadius;
            set { borderRadius = value; Invalidate(); }
        }

        public Color GradientStart
        {
            get => gradientStart;
            set { gradientStart = value; Invalidate(); }
        }

        public Color GradientEnd
        {
            get => gradientEnd;
            set { gradientEnd = value; Invalidate(); }
        }

        private void ModernButton_MouseEnter(object sender, EventArgs e)
        {
            isHovered = true;
            Invalidate();
        }

        private void ModernButton_MouseLeave(object sender, EventArgs e)
        {
            isHovered = false;
            Invalidate();
        }

        private void ModernButton_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            // Gradient brush
            using (LinearGradientBrush brush = new LinearGradientBrush(
                ClientRectangle,
                isHovered ? Color.FromArgb(37, 99, 235) : gradientStart,
                isHovered ? Color.FromArgb(29, 78, 216) : gradientEnd,
                LinearGradientMode.Vertical))
            {
                using (GraphicsPath path = GetRoundedRectangle(ClientRectangle, borderRadius))
                {
                    g.FillPath(brush, path);
                }
            }

            // Text çiz
            TextRenderer.DrawText(g, this.Text, this.Font, ClientRectangle, this.ForeColor,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
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
