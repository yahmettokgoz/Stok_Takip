namespace StokTakip.UI.Forms
{
    partial class FrmStokHareketleri
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnKapat = new System.Windows.Forms.Button();
            this.lblBaslik = new System.Windows.Forms.Label();
            this.panelFilter = new System.Windows.Forms.Panel();
            this.btnFiltrele = new StokTakip.UI.Components.ModernButton();
            this.cmbUrun = new System.Windows.Forms.ComboBox();
            this.lblUrun = new System.Windows.Forms.Label();
            this.cmbHareketTipi = new System.Windows.Forms.ComboBox();
            this.lblHareketTipi = new System.Windows.Forms.Label();
            this.dtpBitis = new System.Windows.Forms.DateTimePicker();
            this.lblBitis = new System.Windows.Forms.Label();
            this.dtpBaslangic = new System.Windows.Forms.DateTimePicker();
            this.lblBaslangic = new System.Windows.Forms.Label();
            this.dgvHareketler = new System.Windows.Forms.DataGridView();
            this.panelBottom = new System.Windows.Forms.Panel();
            this.btnYeniHareket = new StokTakip.UI.Components.ModernButton();
            this.lblToplamHareket = new System.Windows.Forms.Label();
            this.panelTop.SuspendLayout();
            this.panelFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHareketler)).BeginInit();
            this.panelBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelTop.Controls.Add(this.btnKapat);
            this.panelTop.Controls.Add(this.lblBaslik);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1400, 70);
            this.panelTop.TabIndex = 0;
            // 
            // lblBaslik
            // 
            this.lblBaslik.AutoSize = true;
            this.lblBaslik.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblBaslik.ForeColor = System.Drawing.Color.White;
            this.lblBaslik.Location = new System.Drawing.Point(30, 20);
            this.lblBaslik.Name = "lblBaslik";
            this.lblBaslik.Size = new System.Drawing.Size(216, 32);
            this.lblBaslik.TabIndex = 0;
            this.lblBaslik.Text = "Stok Hareketleri";
            // 
            // btnKapat
            // 
            this.btnKapat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKapat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnKapat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKapat.FlatAppearance.BorderSize = 0;
            this.btnKapat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKapat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnKapat.ForeColor = System.Drawing.Color.White;
            this.btnKapat.Location = new System.Drawing.Point(1340, 20);
            this.btnKapat.Name = "btnKapat";
            this.btnKapat.Size = new System.Drawing.Size(40, 30);
            this.btnKapat.TabIndex = 1;
            this.btnKapat.Text = "X";
            this.btnKapat.UseVisualStyleBackColor = false;
            this.btnKapat.Click += new System.EventHandler(this.BtnKapat_Click);
            // 
            // panelFilter
            // 
            this.panelFilter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.panelFilter.Controls.Add(this.btnFiltrele);
            this.panelFilter.Controls.Add(this.cmbUrun);
            this.panelFilter.Controls.Add(this.lblUrun);
            this.panelFilter.Controls.Add(this.cmbHareketTipi);
            this.panelFilter.Controls.Add(this.lblHareketTipi);
            this.panelFilter.Controls.Add(this.dtpBitis);
            this.panelFilter.Controls.Add(this.lblBitis);
            this.panelFilter.Controls.Add(this.dtpBaslangic);
            this.panelFilter.Controls.Add(this.lblBaslangic);
            this.panelFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFilter.Location = new System.Drawing.Point(0, 70);
            this.panelFilter.Name = "panelFilter";
            this.panelFilter.Size = new System.Drawing.Size(1400, 80);
            this.panelFilter.TabIndex = 1;
            // 
            // btnFiltrele
            // 
            this.btnFiltrele.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnFiltrele.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnFiltrele.FlatAppearance.BorderSize = 0;
            this.btnFiltrele.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFiltrele.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFiltrele.ForeColor = System.Drawing.Color.White;
            this.btnFiltrele.Location = new System.Drawing.Point(1250, 25);
            this.btnFiltrele.Name = "btnFiltrele";
            this.btnFiltrele.Size = new System.Drawing.Size(120, 35);
            this.btnFiltrele.TabIndex = 8;
            this.btnFiltrele.Text = "Filtrele";
            this.btnFiltrele.UseVisualStyleBackColor = false;
            this.btnFiltrele.Click += new System.EventHandler(this.BtnFiltrele_Click);
            // 
            // cmbUrun
            // 
            this.cmbUrun.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cmbUrun.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUrun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbUrun.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbUrun.ForeColor = System.Drawing.Color.White;
            this.cmbUrun.FormattingEnabled = true;
            this.cmbUrun.Location = new System.Drawing.Point(950, 30);
            this.cmbUrun.Name = "cmbUrun";
            this.cmbUrun.Size = new System.Drawing.Size(250, 25);
            this.cmbUrun.TabIndex = 7;
            // 
            // lblUrun
            // 
            this.lblUrun.AutoSize = true;
            this.lblUrun.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUrun.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblUrun.Location = new System.Drawing.Point(950, 10);
            this.lblUrun.Name = "lblUrun";
            this.lblUrun.Size = new System.Drawing.Size(35, 15);
            this.lblUrun.TabIndex = 6;
            this.lblUrun.Text = "Ürün";
            // 
            // cmbHareketTipi
            // 
            this.cmbHareketTipi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cmbHareketTipi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbHareketTipi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmbHareketTipi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbHareketTipi.ForeColor = System.Drawing.Color.White;
            this.cmbHareketTipi.FormattingEnabled = true;
            this.cmbHareketTipi.Location = new System.Drawing.Point(650, 30);
            this.cmbHareketTipi.Name = "cmbHareketTipi";
            this.cmbHareketTipi.Size = new System.Drawing.Size(250, 25);
            this.cmbHareketTipi.TabIndex = 5;
            // 
            // lblHareketTipi
            // 
            this.lblHareketTipi.AutoSize = true;
            this.lblHareketTipi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHareketTipi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblHareketTipi.Location = new System.Drawing.Point(650, 10);
            this.lblHareketTipi.Name = "lblHareketTipi";
            this.lblHareketTipi.Size = new System.Drawing.Size(71, 15);
            this.lblHareketTipi.TabIndex = 4;
            this.lblHareketTipi.Text = "Hareket Tipi";
            // 
            // dtpBitis
            // 
            this.dtpBitis.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.dtpBitis.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpBitis.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBitis.Location = new System.Drawing.Point(350, 30);
            this.dtpBitis.Name = "dtpBitis";
            this.dtpBitis.Size = new System.Drawing.Size(250, 25);
            this.dtpBitis.TabIndex = 3;
            // 
            // lblBitis
            // 
            this.lblBitis.AutoSize = true;
            this.lblBitis.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBitis.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblBitis.Location = new System.Drawing.Point(350, 10);
            this.lblBitis.Name = "lblBitis";
            this.lblBitis.Size = new System.Drawing.Size(62, 15);
            this.lblBitis.TabIndex = 2;
            this.lblBitis.Text = "Bitiş Tarihi";
            // 
            // dtpBaslangic
            // 
            this.dtpBaslangic.CalendarMonthBackground = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.dtpBaslangic.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpBaslangic.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpBaslangic.Location = new System.Drawing.Point(30, 30);
            this.dtpBaslangic.Name = "dtpBaslangic";
            this.dtpBaslangic.Size = new System.Drawing.Size(250, 25);
            this.dtpBaslangic.TabIndex = 1;
            // 
            // lblBaslangic
            // 
            this.lblBaslangic.AutoSize = true;
            this.lblBaslangic.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBaslangic.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblBaslangic.Location = new System.Drawing.Point(30, 10);
            this.lblBaslangic.Name = "lblBaslangic";
            this.lblBaslangic.Size = new System.Drawing.Size(90, 15);
            this.lblBaslangic.TabIndex = 0;
            this.lblBaslangic.Text = "Başlangıç Tarihi";
            // 
            // dgvHareketler
            // 
            this.dgvHareketler.AllowUserToAddRows = false;
            this.dgvHareketler.AllowUserToDeleteRows = false;
            this.dgvHareketler.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.None;
            this.dgvHareketler.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.dgvHareketler.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvHareketler.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHareketler.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvHareketler.ColumnHeadersHeight = 40;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvHareketler.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvHareketler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvHareketler.EnableHeadersVisualStyles = false;
            this.dgvHareketler.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.dgvHareketler.Location = new System.Drawing.Point(0, 150);
            this.dgvHareketler.MultiSelect = false;
            this.dgvHareketler.Name = "dgvHareketler";
            this.dgvHareketler.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F);
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvHareketler.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvHareketler.RowHeadersVisible = false;
            this.dgvHareketler.RowTemplate.Height = 35;
            this.dgvHareketler.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHareketler.Size = new System.Drawing.Size(1400, 600);
            this.dgvHareketler.TabIndex = 2;
            this.dgvHareketler.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.DgvHareketler_CellFormatting);
            // 
            // panelBottom
            // 
            this.panelBottom.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.panelBottom.Controls.Add(this.btnYeniHareket);
            this.panelBottom.Controls.Add(this.lblToplamHareket);
            this.panelBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBottom.Location = new System.Drawing.Point(0, 750);
            this.panelBottom.Name = "panelBottom";
            this.panelBottom.Size = new System.Drawing.Size(1400, 60);
            this.panelBottom.TabIndex = 3;
            // 
            // btnYeniHareket
            // 
            this.btnYeniHareket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnYeniHareket.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnYeniHareket.FlatAppearance.BorderSize = 0;
            this.btnYeniHareket.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnYeniHareket.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnYeniHareket.ForeColor = System.Drawing.Color.White;
            this.btnYeniHareket.Location = new System.Drawing.Point(1200, 12);
            this.btnYeniHareket.Name = "btnYeniHareket";
            this.btnYeniHareket.Size = new System.Drawing.Size(170, 38);
            this.btnYeniHareket.TabIndex = 1;
            this.btnYeniHareket.Text = "+ Yeni Hareket";
            this.btnYeniHareket.UseVisualStyleBackColor = false;
            this.btnYeniHareket.Click += new System.EventHandler(this.BtnYeniHareket_Click);
            // 
            // lblToplamHareket
            // 
            this.lblToplamHareket.AutoSize = true;
            this.lblToplamHareket.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblToplamHareket.ForeColor = System.Drawing.Color.White;
            this.lblToplamHareket.Location = new System.Drawing.Point(30, 20);
            this.lblToplamHareket.Name = "lblToplamHareket";
            this.lblToplamHareket.Size = new System.Drawing.Size(169, 20);
            this.lblToplamHareket.TabIndex = 0;
            this.lblToplamHareket.Text = "Toplam 0 hareket kaydı";
            // 
            // FrmStokHareketleri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.ClientSize = new System.Drawing.Size(1400, 810);
            this.Controls.Add(this.dgvHareketler);
            this.Controls.Add(this.panelBottom);
            this.Controls.Add(this.panelFilter);
            this.Controls.Add(this.panelTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmStokHareketleri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Stok Hareketleri";
            this.Load += new System.EventHandler(this.FrmStokHareketleri_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            this.panelFilter.ResumeLayout(false);
            this.panelFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHareketler)).EndInit();
            this.panelBottom.ResumeLayout(false);
            this.panelBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnKapat;
        private System.Windows.Forms.Label lblBaslik;
        private System.Windows.Forms.Panel panelFilter;
        private System.Windows.Forms.DataGridView dgvHareketler;
        private System.Windows.Forms.Panel panelBottom;
        private System.Windows.Forms.Label lblToplamHareket;
        private System.Windows.Forms.DateTimePicker dtpBaslangic;
        private System.Windows.Forms.Label lblBaslangic;
        private System.Windows.Forms.DateTimePicker dtpBitis;
        private System.Windows.Forms.Label lblBitis;
        private System.Windows.Forms.ComboBox cmbHareketTipi;
        private System.Windows.Forms.Label lblHareketTipi;
        private System.Windows.Forms.ComboBox cmbUrun;
        private System.Windows.Forms.Label lblUrun;
        private Components.ModernButton btnFiltrele;
        private Components.ModernButton btnYeniHareket;
    }
}
