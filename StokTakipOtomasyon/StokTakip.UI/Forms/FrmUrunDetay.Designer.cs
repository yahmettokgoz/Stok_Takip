using System;
using System.Drawing;
using System.Windows.Forms;

namespace StokTakip.UI.Forms
{
    partial class FrmUrunDetay
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // FrmUrunDetay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new Size(700, 750);
            this.Name = "FrmUrunDetay";
            this.Text = "Ürün Detay";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
