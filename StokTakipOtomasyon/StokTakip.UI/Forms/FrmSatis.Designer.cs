using System;
using System.Drawing;
using System.Windows.Forms;

namespace StokTakip.UI.Forms
{
    partial class FrmSatis
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
            // FrmSatis
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new Size(1400, 800);
            this.Name = "FrmSatis";
            this.Text = "Satış İşlemi";
            this.ResumeLayout(false);
        }

        #endregion
    }
}
