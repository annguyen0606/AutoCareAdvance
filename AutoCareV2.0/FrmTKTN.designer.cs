namespace AutoCareV2._0
{
    partial class FrmTKTN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTKTN));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExport = new System.Windows.Forms.Button();
            this.txtPhone = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.rTuyBien = new System.Windows.Forms.RadioButton();
            this.rDangNhan = new System.Windows.Forms.RadioButton();
            this.rDaNhan = new System.Windows.Forms.RadioButton();
            this.btnxem = new System.Windows.Forms.Button();
            this.cboloaitn = new System.Windows.Forms.ComboBox();
            this.dtdenngay = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.dttungay = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTotalSms = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grvtn = new System.Windows.Forms.DataGridView();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvtn)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.txtPhone);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.groupBox3);
            this.groupBox1.Controls.Add(this.btnxem);
            this.groupBox1.Controls.Add(this.cboloaitn);
            this.groupBox1.Controls.Add(this.dtdenngay);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dttungay);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1005, 53);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            // 
            // btnExport
            // 
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnExport.Location = new System.Drawing.Point(933, 13);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(71, 30);
            this.btnExport.TabIndex = 10;
            this.btnExport.Text = "Export";
            this.btnExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnExport.UseVisualStyleBackColor = true;
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // txtPhone
            // 
            this.txtPhone.Location = new System.Drawing.Point(490, 18);
            this.txtPhone.Name = "txtPhone";
            this.txtPhone.Size = new System.Drawing.Size(96, 20);
            this.txtPhone.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(451, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Số ĐT";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.rTuyBien);
            this.groupBox3.Controls.Add(this.rDangNhan);
            this.groupBox3.Controls.Add(this.rDaNhan);
            this.groupBox3.Location = new System.Drawing.Point(591, 9);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(269, 32);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            // 
            // rTuyBien
            // 
            this.rTuyBien.AutoSize = true;
            this.rTuyBien.Location = new System.Drawing.Point(184, 11);
            this.rTuyBien.Name = "rTuyBien";
            this.rTuyBien.Size = new System.Drawing.Size(80, 17);
            this.rTuyBien.TabIndex = 5;
            this.rTuyBien.Text = "Tin tùy biến";
            this.rTuyBien.UseVisualStyleBackColor = true;
            // 
            // rDangNhan
            // 
            this.rDangNhan.AutoSize = true;
            this.rDangNhan.Location = new System.Drawing.Point(86, 10);
            this.rDangNhan.Name = "rDangNhan";
            this.rDangNhan.Size = new System.Drawing.Size(95, 17);
            this.rDangNhan.TabIndex = 5;
            this.rDangNhan.Text = "Tin đang nhắn";
            this.rDangNhan.UseVisualStyleBackColor = true;
            // 
            // rDaNhan
            // 
            this.rDaNhan.AutoSize = true;
            this.rDaNhan.Checked = true;
            this.rDaNhan.Location = new System.Drawing.Point(5, 10);
            this.rDaNhan.Name = "rDaNhan";
            this.rDaNhan.Size = new System.Drawing.Size(83, 17);
            this.rDaNhan.TabIndex = 4;
            this.rDaNhan.TabStop = true;
            this.rDaNhan.Text = "Tin đã nhắn";
            this.rDaNhan.UseVisualStyleBackColor = true;
            // 
            // btnxem
            // 
            this.btnxem.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnxem.ForeColor = System.Drawing.Color.Black;
            this.btnxem.Image = ((System.Drawing.Image)(resources.GetObject("btnxem.Image")));
            this.btnxem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnxem.Location = new System.Drawing.Point(866, 13);
            this.btnxem.Name = "btnxem";
            this.btnxem.Size = new System.Drawing.Size(66, 30);
            this.btnxem.TabIndex = 0;
            this.btnxem.Text = "Xem";
            this.btnxem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnxem.UseVisualStyleBackColor = false;
            this.btnxem.Click += new System.EventHandler(this.btnxem_Click);
            // 
            // cboloaitn
            // 
            this.cboloaitn.FormattingEnabled = true;
            this.cboloaitn.Items.AddRange(new object[] {
            "Sinh nhat",
            "Bao duong",
            "Cam on bao duong",
            "Cam on mua xe"});
            this.cboloaitn.Location = new System.Drawing.Point(353, 18);
            this.cboloaitn.Name = "cboloaitn";
            this.cboloaitn.Size = new System.Drawing.Size(91, 21);
            this.cboloaitn.TabIndex = 2;
            this.cboloaitn.SelectedIndexChanged += new System.EventHandler(this.cboloaitn_SelectedIndexChanged);
            // 
            // dtdenngay
            // 
            this.dtdenngay.CustomFormat = "dd/MM/yyyy";
            this.dtdenngay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtdenngay.Location = new System.Drawing.Point(218, 18);
            this.dtdenngay.Name = "dtdenngay";
            this.dtdenngay.Size = new System.Drawing.Size(88, 20);
            this.dtdenngay.TabIndex = 1;
            this.dtdenngay.Value = new System.DateTime(2013, 9, 6, 9, 30, 1, 0);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(312, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Loại tin";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(153, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Đến ngày";
            // 
            // dttungay
            // 
            this.dttungay.CustomFormat = "dd/MM/yyyy";
            this.dttungay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dttungay.Location = new System.Drawing.Point(64, 18);
            this.dttungay.Name = "dttungay";
            this.dttungay.Size = new System.Drawing.Size(85, 20);
            this.dttungay.TabIndex = 1;
            this.dttungay.Value = new System.DateTime(2013, 9, 6, 9, 30, 1, 0);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Từ ngày";
            // 
            // lblTotalSms
            // 
            this.lblTotalSms.AutoSize = true;
            this.lblTotalSms.Location = new System.Drawing.Point(829, 430);
            this.lblTotalSms.Name = "lblTotalSms";
            this.lblTotalSms.Size = new System.Drawing.Size(0, 13);
            this.lblTotalSms.TabIndex = 9;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grvtn);
            this.groupBox2.Location = new System.Drawing.Point(3, 62);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1004, 366);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            // 
            // grvtn
            // 
            this.grvtn.AllowUserToAddRows = false;
            this.grvtn.AllowUserToDeleteRows = false;
            this.grvtn.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvtn.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grvtn.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvtn.Location = new System.Drawing.Point(3, 16);
            this.grvtn.Name = "grvtn";
            this.grvtn.Size = new System.Drawing.Size(998, 347);
            this.grvtn.TabIndex = 0;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.groupBox1);
            this.panelEx1.Controls.Add(this.groupBox2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1012, 452);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 10;
            // 
            // FrmTKTN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 452);
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.lblTotalSms);
            this.Name = "FrmTKTN";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xem thống kê tin nhắn";
            this.Load += new System.EventHandler(this.FrmTKTN_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvtn)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnxem;
        private System.Windows.Forms.ComboBox cboloaitn;
        private System.Windows.Forms.DateTimePicker dtdenngay;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker dttungay;
        private System.Windows.Forms.DataGridView grvtn;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rDangNhan;
        private System.Windows.Forms.RadioButton rDaNhan;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtPhone;
        private System.Windows.Forms.Label lblTotalSms;
        private System.Windows.Forms.Button btnExport;
        private System.Windows.Forms.RadioButton rTuyBien;
        private DevComponents.DotNetBar.PanelEx panelEx1;
    }
}