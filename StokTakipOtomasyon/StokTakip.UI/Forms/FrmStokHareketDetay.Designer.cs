namespace StokTakip.UI.Forms
{
    partial class FrmStokHareketDetay
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.panelMain = new System.Windows.Forms.Panel();
            this.txtAciklama = new System.Windows.Forms.TextBox();
            this.lblAciklama = new System.Windows.Forms.Label();
            this.txtBirimFiyat = new System.Windows.Forms.TextBox();
            this.lblBirimFiyat = new System.Windows.Forms.Label();
            this.txtMiktar = new System.Windows.Forms.TextBox();
            this.lblMiktar = new System.Windows.Forms.Label();
            this.cmbHareketTipi = new System.Windows.Forms.ComboBox();
            this.lblHareketTipi = new System.Windows.Forms.Label();
            this.cmbUrun = new System.Windows.Forms.ComboBox();
            this.lblUrun = new System.Windows.Forms.Label();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnIptal = new StokTakip.UI.Components.ModernButton();
            this.btnKaydet = new StokTakip.UI.Components.ModernButton();
            this.panelTop.SuspendLayout();
            this.panelMain.SuspendLayout();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelTop.Controls.Add(this.lblBaslik);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(600, 60);
            this.panelTop.TabIndex = 0;
            // 
            // lblBaslik
            // 
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Location = new System.Drawing.Point(20, 15);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(224, 30);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.Text = "Yeni Stok Hareketi";
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.panelMain.Controls.Add(this.txtAciklama);
            this.panelMain.Controls.Add(this.lblAciklama);
            this.panelMain.Controls.Add(this.txtBirimFiyat);
            this.panelMain.Controls.Add(this.lblBirimFiyat);
            this.panelMain.Controls.Add(this.txtMiktar);
            this.panelMain.Controls.Add(this.lblMiktar);
            this.panelMain.Controls.Add(this.cmbHareketTipi);
            this.panelMain.Controls.Add(this.lblHareketTipi);
            this.panelMain.Controls.Add(this.cmbUrun);
            this.panelMain.Controls.Add(this.lblUrun);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 60);
            this.panelMain.Name = "panelMain";
            this.panelMain.Padding = new System.Windows.Forms.Padding(20);
            this.panelMain.Size = new System.Drawing.Size(600, 390);
            this.panelMain.TabIndex = 1;
            // 
            // txtAciklama
            // 
            this.txtAciklama.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtAciklama.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAciklama.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtAciklama.ForeColor = System.Drawing.Color.White;
            this.txtAciklama.Location = new System.Drawing.Point(30, 280);
            this.txtAciklama.Multiline = true;
            this.txtAciklama.Name = "txtAciklama";
            this.txtAciklama.Size = new System.Drawing.Size(540, 80);
            this.txtAciklama.TabIndex = 9;
            // 
            // lblAciklama
            // 
            this.lblAciklama.AutoSize = true;
            this.lblAciklama.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblAciklama.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblAciklama.Location = new System.Drawing.Point(30, 255);
            this.lblAciklama.Name = "lblAciklama";
            this.lblAciklama.Size = new System.Drawing.Size(69, 19);
            this.lblAciklama.TabIndex = 8;
            this.lblAciklama.Text = "Açıklama";
            // 
            // txtBirimFiyat
            // 
            this.txtBirimFiyat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtBirimFiyat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBirimFiyat.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBirimFiyat.ForeColor = System.Drawing.Color.White;
            this.txtBirimFiyat.Location = new System.Drawing.Point(310, 210);
            this.txtBirimFiyat.Name = "txtBirimFiyat";
            this.txtBirimFiyat.Size = new System.Drawing.Size(260, 27);
            this.txtBirimFiyat.TabIndex = 7;
            this.txtBirimFiyat.Text = "0";
            // 
            // lblBirimFiyat
            // 
            this.lblBirimFiyat.AutoSize = true;
            this.lblBirimFiyat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblBirimFiyat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblBirimFiyat.Location = new System.Drawing.Point(310, 185);
            this.lblBirimFiyat.Name = "lblBirimFiyat";
            this.lblBirimFiyat.Size = new System.Drawing.Size(78, 19);
            this.lblBirimFiyat.TabIndex = 6;
            this.lblBirimFiyat.Text = "Birim Fiyat";
            // 
            // txtMiktar
            // 
            this.txtMiktar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtMiktar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtMiktar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMiktar.ForeColor = System.Drawing.Color.White;
            this.txtMiktar.Location = new System.Drawing.Point(30, 210);
            this.txtMiktar.Name = "txtMiktar";
            this.txtMiktar.Size = new System.Drawing.Size(260, 27);
            this.txtMiktar.TabIndex = 5;
            this.txtMiktar.Text = "1";
            // 
            // lblMiktar
            // 
            this.lblMiktar.AutoSize = true;
            this.lblMiktar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMiktar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblMiktar.Location = new System.Drawing.Point(30, 185);
            this.lblMiktar.Name = "lblMiktar";
            this.lblMiktar.Size = new System.Drawing.Size(51, 19);
            this.lblMiktar.TabIndex = 4;
            this.lblMiktar.Text = "Miktar";
            // 
            // cmbHareketTipi
            // 
            this.cmbHareketTipi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cmbHareketTipi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHareketTipi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbHareketTipi.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbHareketTipi.ForeColor = System.Drawing.Color.White;
            this.cmbHareketTipi.FormattingEnabled = true;
            this.cmbHareketTipi.Location = new System.Drawing.Point(30, 140);
            this.cmbHareketTipi.Name = "cmbHareketTipi";
            this.cmbHareketTipi.Size = new System.Drawing.Size(540, 28);
            this.cmbHareketTipi.TabIndex = 3;
            // 
            // lblHareketTipi
            // 
            this.lblHareketTipi.AutoSize = true;
            this.lblHareketTipi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHareketTipi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblHareketTipi.Location = new System.Drawing.Point(30, 115);
            this.lblHareketTipi.Name = "lblHareketTipi";
            this.lblHareketTipi.Size = new System.Drawing.Size(88, 19);
            this.lblHareketTipi.TabIndex = 2;
            this.lblHareketTipi.Text = "Hareket Tipi";
            // 
            // cmbUrun
            // 
            this.cmbUrun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cmbUrun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUrun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbUrun.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbUrun.ForeColor = System.Drawing.Color.White;
            this.cmbUrun.FormattingEnabled = true;
            this.cmbUrun.Location = new System.Drawing.Point(30, 55);
            this.cmbUrun.Name = "cmbUrun";
            this.cmbUrun.Size = new System.Drawing.Size(540, 28);
            this.cmbUrun.TabIndex = 1;
            // 
            // lblUrun
            // 
            this.lblUrun.AutoSize = true;
            this.lblUrun.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblUrun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblUrun.Location = new System.Drawing.Point(30, 30);
            this.lblUrun.Name = "lblUrun";
            this.lblUrun.Size = new System.Drawing.Size(39, 19);
            this.lblUrun.TabIndex = 0;
            this.lblUrun.Text = "Ürün";
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelBottom.Controls.Add(this.btnIptal);
            this.panelBottom.Controls.Add(this.btnKaydet);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 450);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(600, 70);
            this.panelBottom.TabIndex = 2;
            // 
            // btnIptal
            // 
            this.btnIptal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnIptal.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIptal.FlatAppearance.BorderSize = 0;
            this.btnIptal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIptal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnIptal.ForeColor = System.Drawing.Color.White;
            this.btnIptal.Location = new System.Drawing.Point(320, 15);
            this.btnIptal.Name = "btnIptal";
            this.btnIptal.Size = new System.Drawing.Size(250, 40);
            this.btnIptal.TabIndex = 1;
            this.btnIptal.Text = "İptal";
            this.btnIptal.UseVisualStyleBackColor = false;
            this.btnIptal.Click += new System.EventHandler(this.BtnIptal_Click);
            // 
            // btnKaydet
            // 
            this.btnKaydet.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnKaydet.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKaydet.FlatAppearance.BorderSize = 0;
            this.btnKaydet.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKaydet.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnKaydet.ForeColor = System.Drawing.Color.White;
            this.btnKaydet.Location = new System.Drawing.Point(30, 15);
            this.btnKaydet.Name = "btnKaydet";
            this.btnKaydet.Size = new System.Drawing.Size(250, 40);
            this.btnKaydet.TabIndex = 0;
            this.btnKaydet.Text = "Kaydet";
            this.btnKaydet.UseVisualStyleBackColor = false;
            this.btnKaydet.Click += new System.EventHandler(this.BtnKaydet_Click);
            // 
            // FrmStokHareketDetay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.ClientSize = new System.Drawing.Size(600, 520);
            this.Controls.Add(this.panelMain);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStokHareketDetay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Yeni Stok Hareketi";
            this.Load += new System.EventHandler(this.FrmStokHareketDetay_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Panel panelMain;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.ComboBox cmbUrun;
        private System.Windows.Forms.Label lblUrun;
        private System.Windows.Forms.ComboBox cmbHareketTipi;
        private System.Windows.Forms.Label lblHareketTipi;
        private System.Windows.Forms.TextBox txtMiktar;
        private System.Windows.Forms.Label lblMiktar;
        private System.Windows.Forms.TextBox txtBirimFiyat;
        private System.Windows.Forms.Label lblBirimFiyat;
        private System.Windows.Forms.TextBox txtAciklama;
        private System.Windows.Forms.Label lblAciklama;
        private Components.ModernButton btnKaydet;
        private Components.ModernButton btnIptal;
    }
}
