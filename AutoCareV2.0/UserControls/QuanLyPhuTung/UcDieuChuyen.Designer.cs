namespace AutoCareV2._0.UserControls.QuanLyPhuTung
{
    partial class UcDieuChuyen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcDieuChuyen));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbbChoCoSoXuat = new System.Windows.Forms.ComboBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtGiatien = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtSoLuongCon = new System.Windows.Forms.TextBox();
            this.cboKhoXuat = new System.Windows.Forms.ComboBox();
            this.cboPhuTung = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtGhiChu = new System.Windows.Forms.TextBox();
            this.txtSoLuongChuyen = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.bttChuyen = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbbChonCoSoNhan = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.lstvDanhSachPhuTung = new System.Windows.Forms.ListView();
            this.MaPT = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.SoLuong1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.NgayNhap = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Kho = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GhiChu = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.GiaTien = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label1 = new System.Windows.Forms.Label();
            this.cboKhoNhan = new System.Windows.Forms.ComboBox();
            this.btnDieuChuyen = new DevComponents.DotNetBar.ButtonX();
            this.button2 = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.button1 = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbbChoCoSoXuat);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtGiatien);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtSoLuongCon);
            this.groupBox1.Controls.Add(this.cboKhoXuat);
            this.groupBox1.Controls.Add(this.cboPhuTung);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtGhiChu);
            this.groupBox1.Controls.Add(this.txtSoLuongChuyen);
            this.groupBox1.Controls.Add(this.dateTimePicker1);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(9, 78);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(454, 344);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kho Xuất";
            // 
            // cbbChoCoSoXuat
            // 
            this.cbbChoCoSoXuat.FormattingEnabled = true;
            this.cbbChoCoSoXuat.Items.AddRange(new object[] {
            "Honda Đức Trí 1",
            "Honda Đức Trí 2"});
            this.cbbChoCoSoXuat.Location = new System.Drawing.Point(124, 315);
            this.cbbChoCoSoXuat.Name = "cbbChoCoSoXuat";
            this.cbbChoCoSoXuat.Size = new System.Drawing.Size(200, 21);
            this.cbbChoCoSoXuat.TabIndex = 14;
            this.cbbChoCoSoXuat.Visible = false;
            this.cbbChoCoSoXuat.SelectedIndexChanged += new System.EventHandler(this.cbbChoCoSoXuat_SelectedIndexChanged);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(13, 318);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 13;
            this.label10.Text = "Chọn cửa hàng";
            this.label10.Visible = false;
            // 
            // txtGiatien
            // 
            this.txtGiatien.Location = new System.Drawing.Point(124, 151);
            this.txtGiatien.Name = "txtGiatien";
            this.txtGiatien.ReadOnly = true;
            this.txtGiatien.Size = new System.Drawing.Size(200, 20);
            this.txtGiatien.TabIndex = 12;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(13, 154);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(102, 13);
            this.label8.TabIndex = 11;
            this.label8.Text = "Giá Tiền / 1 Đơn Vị:";
            // 
            // txtSoLuongCon
            // 
            this.txtSoLuongCon.BackColor = System.Drawing.Color.White;
            this.txtSoLuongCon.Location = new System.Drawing.Point(124, 98);
            this.txtSoLuongCon.Name = "txtSoLuongCon";
            this.txtSoLuongCon.ReadOnly = true;
            this.txtSoLuongCon.Size = new System.Drawing.Size(200, 20);
            this.txtSoLuongCon.TabIndex = 6;
            // 
            // cboKhoXuat
            // 
            this.cboKhoXuat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoXuat.FormattingEnabled = true;
            this.cboKhoXuat.Location = new System.Drawing.Point(124, 40);
            this.cboKhoXuat.Name = "cboKhoXuat";
            this.cboKhoXuat.Size = new System.Drawing.Size(200, 21);
            this.cboKhoXuat.TabIndex = 10;
            this.cboKhoXuat.SelectionChangeCommitted += new System.EventHandler(this.cboKhoXuat_SelectionChangeCommitted);
            // 
            // cboPhuTung
            // 
            this.cboPhuTung.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboPhuTung.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboPhuTung.FormattingEnabled = true;
            this.cboPhuTung.Location = new System.Drawing.Point(124, 69);
            this.cboPhuTung.Name = "cboPhuTung";
            this.cboPhuTung.Size = new System.Drawing.Size(200, 21);
            this.cboPhuTung.TabIndex = 9;
            this.cboPhuTung.SelectionChangeCommitted += new System.EventHandler(this.cboPhuTung_SelectionChangeCommitted);
            this.cboPhuTung.KeyUp += new System.Windows.Forms.KeyEventHandler(this.cboPhuTung_KeyUp);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 267);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(74, 13);
            this.label6.TabIndex = 8;
            this.label6.Text = "Ngày Chuyển:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 181);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Ghi Chú:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Số Lượng Chuyển:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(13, 98);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 13);
            this.label7.TabIndex = 7;
            this.label7.Text = "Số Lượng Còn:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(57, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Phụ Tùng:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(124, 181);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(200, 70);
            this.txtGhiChu.TabIndex = 6;
            // 
            // txtSoLuongChuyen
            // 
            this.txtSoLuongChuyen.Location = new System.Drawing.Point(124, 126);
            this.txtSoLuongChuyen.Name = "txtSoLuongChuyen";
            this.txtSoLuongChuyen.Size = new System.Drawing.Size(200, 20);
            this.txtSoLuongChuyen.TabIndex = 6;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dateTimePicker1.Location = new System.Drawing.Point(124, 267);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(13, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Kho Xuất:";
            // 
            // bttChuyen
            // 
            this.bttChuyen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.bttChuyen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.bttChuyen.Location = new System.Drawing.Point(479, 166);
            this.bttChuyen.Name = "bttChuyen";
            this.bttChuyen.Size = new System.Drawing.Size(50, 55);
            this.bttChuyen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bttChuyen.TabIndex = 2;
            this.bttChuyen.Text = ">>";
            this.bttChuyen.Click += new System.EventHandler(this.bttChuyen_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbbChonCoSoNhan);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.lstvDanhSachPhuTung);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.cboKhoNhan);
            this.groupBox2.Location = new System.Drawing.Point(544, 78);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(469, 331);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Kho Nhận";
            // 
            // cbbChonCoSoNhan
            // 
            this.cbbChonCoSoNhan.FormattingEnabled = true;
            this.cbbChonCoSoNhan.Items.AddRange(new object[] {
            "Honda Đức Trí 1",
            "Honda Đức Trí 2"});
            this.cbbChonCoSoNhan.Location = new System.Drawing.Point(104, 33);
            this.cbbChonCoSoNhan.Name = "cbbChonCoSoNhan";
            this.cbbChonCoSoNhan.Size = new System.Drawing.Size(200, 21);
            this.cbbChonCoSoNhan.TabIndex = 14;
            this.cbbChonCoSoNhan.SelectedIndexChanged += new System.EventHandler(this.cbbChonCoSoNhan_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 39);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(80, 13);
            this.label9.TabIndex = 13;
            this.label9.Text = "Chọn cửa hàng";
            // 
            // lstvDanhSachPhuTung
            // 
            this.lstvDanhSachPhuTung.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.MaPT,
            this.SoLuong1,
            this.NgayNhap,
            this.Kho,
            this.GhiChu,
            this.GiaTien});
            this.lstvDanhSachPhuTung.FullRowSelect = true;
            this.lstvDanhSachPhuTung.GridLines = true;
            this.lstvDanhSachPhuTung.Location = new System.Drawing.Point(12, 104);
            this.lstvDanhSachPhuTung.Name = "lstvDanhSachPhuTung";
            this.lstvDanhSachPhuTung.Size = new System.Drawing.Size(442, 182);
            this.lstvDanhSachPhuTung.TabIndex = 3;
            this.lstvDanhSachPhuTung.UseCompatibleStateImageBehavior = false;
            this.lstvDanhSachPhuTung.View = System.Windows.Forms.View.Details;
            // 
            // MaPT
            // 
            this.MaPT.Text = "Mã Phụ Tùng";
            this.MaPT.Width = 89;
            // 
            // SoLuong1
            // 
            this.SoLuong1.Text = "Số Lượng";
            this.SoLuong1.Width = 73;
            // 
            // NgayNhap
            // 
            this.NgayNhap.Text = "Ngày Nhập";
            this.NgayNhap.Width = 89;
            // 
            // Kho
            // 
            this.Kho.Text = "Kho Xuất";
            // 
            // GhiChu
            // 
            this.GhiChu.Text = "Ghi Chú";
            this.GhiChu.Width = 61;
            // 
            // GiaTien
            // 
            this.GiaTien.Text = "VND/ĐơnVị";
            this.GiaTien.Width = 78;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Kho Nhận:";
            // 
            // cboKhoNhan
            // 
            this.cboKhoNhan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhoNhan.FormattingEnabled = true;
            this.cboKhoNhan.Location = new System.Drawing.Point(104, 62);
            this.cboKhoNhan.Name = "cboKhoNhan";
            this.cboKhoNhan.Size = new System.Drawing.Size(200, 21);
            this.cboKhoNhan.TabIndex = 1;
            // 
            // btnDieuChuyen
            // 
            this.btnDieuChuyen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnDieuChuyen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDieuChuyen.Image = ((System.Drawing.Image)(resources.GetObject("btnDieuChuyen.Image")));
            this.btnDieuChuyen.Location = new System.Drawing.Point(404, 443);
            this.btnDieuChuyen.Name = "btnDieuChuyen";
            this.btnDieuChuyen.Size = new System.Drawing.Size(95, 30);
            this.btnDieuChuyen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDieuChuyen.TabIndex = 5;
            this.btnDieuChuyen.Text = "Điều Chuyển";
            this.btnDieuChuyen.Click += new System.EventHandler(this.btnDieuChuyen_Click);
            // 
            // button2
            // 
            this.button2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.button2.Image = ((System.Drawing.Image)(resources.GetObject("button2.Image")));
            this.button2.Location = new System.Drawing.Point(505, 443);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(95, 30);
            this.button2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.button2.TabIndex = 6;
            this.button2.Text = "Hủy Bỏ";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.btnDieuChuyen);
            this.panelEx1.Controls.Add(this.button2);
            this.panelEx1.Controls.Add(this.groupBox1);
            this.panelEx1.Controls.Add(this.bttChuyen);
            this.panelEx1.Controls.Add(this.groupBox2);
            this.panelEx1.Controls.Add(this.button1);
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1042, 523);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 7;
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(9, 4);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(1013, 68);
            this.labelX1.TabIndex = 7;
            this.labelX1.Text = "ĐIỀU CHUYỂN";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // button1
            // 
            this.button1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.button1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.button1.Location = new System.Drawing.Point(479, 227);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(50, 55);
            this.button1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.button1.TabIndex = 3;
            this.button1.Text = "<<";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UcDieuChuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.Controls.Add(this.panelEx1);
            this.Name = "UcDieuChuyen";
            this.Size = new System.Drawing.Size(1045, 526);
            this.Load += new System.EventHandler(this.UcDieuChuyen_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtGiatien;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtSoLuongCon;
        private System.Windows.Forms.ComboBox cboKhoXuat;
        private System.Windows.Forms.ComboBox cboPhuTung;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtGhiChu;
        private System.Windows.Forms.TextBox txtSoLuongChuyen;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.ButtonX bttChuyen;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView lstvDanhSachPhuTung;
        private System.Windows.Forms.ColumnHeader MaPT;
        private System.Windows.Forms.ColumnHeader SoLuong1;
        private System.Windows.Forms.ColumnHeader NgayNhap;
        private System.Windows.Forms.ColumnHeader Kho;
        private System.Windows.Forms.ColumnHeader GhiChu;
        private System.Windows.Forms.ColumnHeader GiaTien;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboKhoNhan;
        private DevComponents.DotNetBar.ButtonX btnDieuChuyen;
        private DevComponents.DotNetBar.ButtonX button2;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX button1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.ComboBox cbbChoCoSoXuat;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.ComboBox cbbChonCoSoNhan;
    }
}
