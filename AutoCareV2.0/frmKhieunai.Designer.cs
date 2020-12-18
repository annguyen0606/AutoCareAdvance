namespace AutoCareV2._0
{
    partial class frmKhieunai
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmKhieunai));
            this.labelThoDichVu = new System.Windows.Forms.Label();
            this.labelSoLuongXuat = new System.Windows.Forms.Label();
            this.labelChonKho = new System.Windows.Forms.Label();
            this.labelTieuDe = new System.Windows.Forms.Label();
            this.panelContent = new System.Windows.Forms.Panel();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.txtketqua = new System.Windows.Forms.TextBox();
            this.txtbienphap = new System.Windows.Forms.RichTextBox();
            this.txtnoidung = new System.Windows.Forms.RichTextBox();
            this.labelPhuTung = new System.Windows.Forms.Label();
            this.panelChucNang = new System.Windows.Forms.Panel();
            this.buttonThoat = new System.Windows.Forms.Button();
            this.buttonThem = new System.Windows.Forms.Button();
            this.panelTieuDe = new System.Windows.Forms.Panel();
            this.panelContent.SuspendLayout();
            this.panelChucNang.SuspendLayout();
            this.panelTieuDe.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelThoDichVu
            // 
            this.labelThoDichVu.AutoSize = true;
            this.labelThoDichVu.Location = new System.Drawing.Point(12, 178);
            this.labelThoDichVu.Name = "labelThoDichVu";
            this.labelThoDichVu.Size = new System.Drawing.Size(47, 13);
            this.labelThoDichVu.TabIndex = 0;
            this.labelThoDichVu.Text = "Kết quả:";
            // 
            // labelSoLuongXuat
            // 
            this.labelSoLuongXuat.AutoSize = true;
            this.labelSoLuongXuat.Location = new System.Drawing.Point(12, 113);
            this.labelSoLuongXuat.Name = "labelSoLuongXuat";
            this.labelSoLuongXuat.Size = new System.Drawing.Size(72, 13);
            this.labelSoLuongXuat.TabIndex = 0;
            this.labelSoLuongXuat.Text = "BP giải quyết:";
            // 
            // labelChonKho
            // 
            this.labelChonKho.AutoSize = true;
            this.labelChonKho.Location = new System.Drawing.Point(12, 15);
            this.labelChonKho.Name = "labelChonKho";
            this.labelChonKho.Size = new System.Drawing.Size(35, 13);
            this.labelChonKho.TabIndex = 0;
            this.labelChonKho.Text = "Ngày:";
            // 
            // labelTieuDe
            // 
            this.labelTieuDe.AutoSize = true;
            this.labelTieuDe.Font = new System.Drawing.Font("Times New Roman", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTieuDe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.labelTieuDe.Location = new System.Drawing.Point(25, 6);
            this.labelTieuDe.Name = "labelTieuDe";
            this.labelTieuDe.Size = new System.Drawing.Size(330, 20);
            this.labelTieuDe.TabIndex = 0;
            this.labelTieuDe.Text = "THÔNG TIN KHIẾU NẠI BẢO DƯỠNG";
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.dateTimePicker1);
            this.panelContent.Controls.Add(this.txtketqua);
            this.panelContent.Controls.Add(this.txtbienphap);
            this.panelContent.Controls.Add(this.txtnoidung);
            this.panelContent.Controls.Add(this.labelThoDichVu);
            this.panelContent.Controls.Add(this.labelSoLuongXuat);
            this.panelContent.Controls.Add(this.labelPhuTung);
            this.panelContent.Controls.Add(this.labelChonKho);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(0, 33);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(388, 237);
            this.panelContent.TabIndex = 4;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Checked = false;
            this.dateTimePicker1.CustomFormat = "dd/MM/yyyy";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(101, 15);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 13;
            // 
            // txtketqua
            // 
            this.txtketqua.Location = new System.Drawing.Point(101, 178);
            this.txtketqua.Name = "txtketqua";
            this.txtketqua.Size = new System.Drawing.Size(255, 20);
            this.txtketqua.TabIndex = 11;
            // 
            // txtbienphap
            // 
            this.txtbienphap.Location = new System.Drawing.Point(101, 110);
            this.txtbienphap.Name = "txtbienphap";
            this.txtbienphap.Size = new System.Drawing.Size(255, 44);
            this.txtbienphap.TabIndex = 10;
            this.txtbienphap.Text = "";
            // 
            // txtnoidung
            // 
            this.txtnoidung.Location = new System.Drawing.Point(101, 40);
            this.txtnoidung.Name = "txtnoidung";
            this.txtnoidung.Size = new System.Drawing.Size(255, 50);
            this.txtnoidung.TabIndex = 9;
            this.txtnoidung.Text = "";
            // 
            // labelPhuTung
            // 
            this.labelPhuTung.AutoSize = true;
            this.labelPhuTung.Location = new System.Drawing.Point(12, 38);
            this.labelPhuTung.Name = "labelPhuTung";
            this.labelPhuTung.Size = new System.Drawing.Size(53, 13);
            this.labelPhuTung.TabIndex = 0;
            this.labelPhuTung.Text = "Nội dung:";
            // 
            // panelChucNang
            // 
            this.panelChucNang.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelChucNang.Controls.Add(this.buttonThoat);
            this.panelChucNang.Controls.Add(this.buttonThem);
            this.panelChucNang.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelChucNang.Location = new System.Drawing.Point(0, 270);
            this.panelChucNang.Name = "panelChucNang";
            this.panelChucNang.Size = new System.Drawing.Size(388, 33);
            this.panelChucNang.TabIndex = 5;
            // 
            // buttonThoat
            // 
            this.buttonThoat.BackColor = System.Drawing.Color.Transparent;
            this.buttonThoat.Image = ((System.Drawing.Image)(resources.GetObject("buttonThoat.Image")));
            this.buttonThoat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonThoat.Location = new System.Drawing.Point(281, 5);
            this.buttonThoat.Name = "buttonThoat";
            this.buttonThoat.Size = new System.Drawing.Size(75, 23);
            this.buttonThoat.TabIndex = 1;
            this.buttonThoat.Text = "Thoát";
            this.buttonThoat.UseVisualStyleBackColor = false;
            this.buttonThoat.Click += new System.EventHandler(this.buttonThoat_Click);
            // 
            // buttonThem
            // 
            this.buttonThem.BackColor = System.Drawing.Color.Transparent;
            this.buttonThem.Image = ((System.Drawing.Image)(resources.GetObject("buttonThem.Image")));
            this.buttonThem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonThem.Location = new System.Drawing.Point(198, 5);
            this.buttonThem.Name = "buttonThem";
            this.buttonThem.Size = new System.Drawing.Size(75, 23);
            this.buttonThem.TabIndex = 0;
            this.buttonThem.Text = "Lưu";
            this.buttonThem.UseVisualStyleBackColor = false;
            this.buttonThem.Click += new System.EventHandler(this.buttonThem_Click);
            // 
            // panelTieuDe
            // 
            this.panelTieuDe.Controls.Add(this.labelTieuDe);
            this.panelTieuDe.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTieuDe.Location = new System.Drawing.Point(0, 0);
            this.panelTieuDe.Name = "panelTieuDe";
            this.panelTieuDe.Size = new System.Drawing.Size(388, 33);
            this.panelTieuDe.TabIndex = 3;
            // 
            // frmKhieunai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(388, 303);
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelChucNang);
            this.Controls.Add(this.panelTieuDe);
            this.Name = "frmKhieunai";
            this.Text = "Thông tin khiếu nại";
            this.Load += new System.EventHandler(this.frmKhieunai_Load);
            this.panelContent.ResumeLayout(false);
            this.panelContent.PerformLayout();
            this.panelChucNang.ResumeLayout(false);
            this.panelTieuDe.ResumeLayout(false);
            this.panelTieuDe.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        
        private System.Windows.Forms.Label labelThoDichVu;
        private System.Windows.Forms.Label labelSoLuongXuat;
        private System.Windows.Forms.Label labelChonKho;
        private System.Windows.Forms.Button buttonThoat;
        private System.Windows.Forms.Button buttonThem;
        private System.Windows.Forms.Label labelTieuDe;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelChucNang;
        private System.Windows.Forms.Panel panelTieuDe;
        private System.Windows.Forms.TextBox txtketqua;
        private System.Windows.Forms.RichTextBox txtbienphap;
        private System.Windows.Forms.RichTextBox txtnoidung;
        private System.Windows.Forms.Label labelPhuTung;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
    }
}