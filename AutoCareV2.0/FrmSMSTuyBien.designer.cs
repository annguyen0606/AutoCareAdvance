namespace AutoCareV2._0
{
    partial class FrmSMSTuyBien
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
            this.cbb_Search = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grv_DsKhachHang = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_SoKH = new System.Windows.Forms.Label();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.cbosheet = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btn_DatLich = new DevComponents.DotNetBar.ButtonX();
            this.btn_Loc = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cbb_Key = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dt_NgayNhan = new System.Windows.Forms.DateTimePicker();
            this.txt_SMS = new System.Windows.Forms.TextBox();
            this.txt_GioNhan = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_BanTin = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_KyTuCon = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lbl_KyTuDaNhan = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnHuyBo = new DevComponents.DotNetBar.ButtonX();
            this.label10 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.radioButtonNam = new System.Windows.Forms.RadioButton();
            this.radioButtonNu = new System.Windows.Forms.RadioButton();
            this.btn_locGioiTinh = new DevComponents.DotNetBar.ButtonX();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grv_DsKhachHang)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbb_Search
            // 
            this.cbb_Search.FormattingEnabled = true;
            this.cbb_Search.Items.AddRange(new object[] {
            "Anh",
            "Chị",
            "Cô",
            "Chú"});
            this.cbb_Search.Location = new System.Drawing.Point(186, 20);
            this.cbb_Search.Name = "cbb_Search";
            this.cbb_Search.Size = new System.Drawing.Size(237, 21);
            this.cbb_Search.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(170, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Lọc theo đại từ đứng trước tên KH";
            // 
            // grv_DsKhachHang
            // 
            this.grv_DsKhachHang.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grv_DsKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grv_DsKhachHang.Location = new System.Drawing.Point(3, 16);
            this.grv_DsKhachHang.Name = "grv_DsKhachHang";
            this.grv_DsKhachHang.Size = new System.Drawing.Size(543, 162);
            this.grv_DsKhachHang.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.grv_DsKhachHang);
            this.groupBox1.Location = new System.Drawing.Point(29, 178);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(549, 181);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách khách hàng";
            // 
            // lbl_SoKH
            // 
            this.lbl_SoKH.AutoSize = true;
            this.lbl_SoKH.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_SoKH.Location = new System.Drawing.Point(29, 362);
            this.lbl_SoKH.Name = "lbl_SoKH";
            this.lbl_SoKH.Size = new System.Drawing.Size(0, 13);
            this.lbl_SoKH.TabIndex = 0;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Location = new System.Drawing.Point(429, 114);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(95, 30);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 9;
            this.buttonX1.Text = "&Mở";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // cbosheet
            // 
            this.cbosheet.FormattingEnabled = true;
            this.cbosheet.Location = new System.Drawing.Point(314, 123);
            this.cbosheet.Name = "cbosheet";
            this.cbosheet.Size = new System.Drawing.Size(109, 21);
            this.cbosheet.TabIndex = 8;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(250, 126);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(66, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Chọn Sheet:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(71, 123);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(173, 20);
            this.textBox1.TabIndex = 6;
            this.textBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.textBox1_MouseDown);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(54, 13);
            this.label5.TabIndex = 5;
            this.label5.Text = "Chọn File:";
            // 
            // btn_DatLich
            // 
            this.btn_DatLich.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_DatLich.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_DatLich.Location = new System.Drawing.Point(391, 375);
            this.btn_DatLich.Name = "btn_DatLich";
            this.btn_DatLich.Size = new System.Drawing.Size(95, 30);
            this.btn_DatLich.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_DatLich.TabIndex = 4;
            this.btn_DatLich.Text = "&Đặt lịch gửi";
            this.btn_DatLich.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // btn_Loc
            // 
            this.btn_Loc.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Loc.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Loc.Location = new System.Drawing.Point(429, 16);
            this.btn_Loc.Name = "btn_Loc";
            this.btn_Loc.Size = new System.Drawing.Size(95, 30);
            this.btn_Loc.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Loc.TabIndex = 4;
            this.btn_Loc.Text = "&Lọc khách hàng";
            this.btn_Loc.Click += new System.EventHandler(this.btn_Loc_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.cbb_Key);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.dt_NgayNhan);
            this.groupBox2.Controls.Add(this.txt_SMS);
            this.groupBox2.Controls.Add(this.txt_GioNhan);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lbl_BanTin);
            this.groupBox2.Controls.Add(this.label11);
            this.groupBox2.Controls.Add(this.lbl_KyTuCon);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.lbl_KyTuDaNhan);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(586, 16);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 343);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Cấu hình tin nhắn";
            // 
            // cbb_Key
            // 
            this.cbb_Key.FormattingEnabled = true;
            this.cbb_Key.Items.AddRange(new object[] {
            "",
            "[TenKH]",
            "[NgaySinh]",
            "[ThuongHieu]",
            "[SoDienThoai]"});
            this.cbb_Key.Location = new System.Drawing.Point(222, 60);
            this.cbb_Key.Name = "cbb_Key";
            this.cbb_Key.Size = new System.Drawing.Size(121, 21);
            this.cbb_Key.TabIndex = 4;
            this.cbb_Key.SelectedIndexChanged += new System.EventHandler(this.cbb_Key_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(194, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Key";
            // 
            // dt_NgayNhan
            // 
            this.dt_NgayNhan.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dt_NgayNhan.Location = new System.Drawing.Point(222, 26);
            this.dt_NgayNhan.Name = "dt_NgayNhan";
            this.dt_NgayNhan.Size = new System.Drawing.Size(121, 20);
            this.dt_NgayNhan.TabIndex = 2;
            this.dt_NgayNhan.Value = new System.DateTime(2014, 1, 20, 0, 0, 0, 0);
            // 
            // txt_SMS
            // 
            this.txt_SMS.Location = new System.Drawing.Point(6, 87);
            this.txt_SMS.Multiline = true;
            this.txt_SMS.Name = "txt_SMS";
            this.txt_SMS.Size = new System.Drawing.Size(337, 231);
            this.txt_SMS.TabIndex = 1;
            this.txt_SMS.TextChanged += new System.EventHandler(this.txt_SMS_TextChanged);
            // 
            // txt_GioNhan
            // 
            this.txt_GioNhan.Location = new System.Drawing.Point(66, 26);
            this.txt_GioNhan.Name = "txt_GioNhan";
            this.txt_GioNhan.Size = new System.Drawing.Size(55, 20);
            this.txt_GioNhan.TabIndex = 1;
            this.txt_GioNhan.Text = "16";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(161, 30);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ngày nhắn";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 66);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(91, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Nội dung tin nhắn";
            // 
            // lbl_BanTin
            // 
            this.lbl_BanTin.AutoSize = true;
            this.lbl_BanTin.Location = new System.Drawing.Point(305, 321);
            this.lbl_BanTin.Name = "lbl_BanTin";
            this.lbl_BanTin.Size = new System.Drawing.Size(0, 13);
            this.lbl_BanTin.TabIndex = 0;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(249, 321);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(58, 13);
            this.label11.TabIndex = 0;
            this.label11.Text = "Số bản tin:";
            // 
            // lbl_KyTuCon
            // 
            this.lbl_KyTuCon.AutoSize = true;
            this.lbl_KyTuCon.Location = new System.Drawing.Point(193, 321);
            this.lbl_KyTuCon.Name = "lbl_KyTuCon";
            this.lbl_KyTuCon.Size = new System.Drawing.Size(0, 13);
            this.lbl_KyTuCon.TabIndex = 0;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(137, 321);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(55, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Ký tự còn:";
            // 
            // lbl_KyTuDaNhan
            // 
            this.lbl_KyTuDaNhan.AutoSize = true;
            this.lbl_KyTuDaNhan.Location = new System.Drawing.Point(81, 321);
            this.lbl_KyTuDaNhan.Name = "lbl_KyTuDaNhan";
            this.lbl_KyTuDaNhan.Size = new System.Drawing.Size(0, 13);
            this.lbl_KyTuDaNhan.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 321);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(77, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Ký tự đã nhắn:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Giờ nhắn";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnHuyBo);
            this.panelEx1.Controls.Add(this.lbl_SoKH);
            this.panelEx1.Controls.Add(this.groupBox3);
            this.panelEx1.Controls.Add(this.groupBox1);
            this.panelEx1.Controls.Add(this.btn_DatLich);
            this.panelEx1.Controls.Add(this.groupBox2);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(976, 413);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 5;
            this.panelEx1.Text = "panelEx1";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.btn_locGioiTinh);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Controls.Add(this.buttonX1);
            this.groupBox3.Controls.Add(this.btn_Loc);
            this.groupBox3.Controls.Add(this.cbosheet);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.cbb_Search);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.textBox1);
            this.groupBox3.Location = new System.Drawing.Point(29, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(549, 156);
            this.groupBox3.TabIndex = 10;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Lấy thông tin khách hàng";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "excel 2003 file|*.xls| Excel 2007 | *.xlsx";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // btnHuyBo
            // 
            this.btnHuyBo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnHuyBo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnHuyBo.Location = new System.Drawing.Point(496, 375);
            this.btnHuyBo.Name = "btnHuyBo";
            this.btnHuyBo.Size = new System.Drawing.Size(95, 30);
            this.btnHuyBo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnHuyBo.TabIndex = 11;
            this.btnHuyBo.Text = "&Hủy bỏ";
            this.btnHuyBo.Click += new System.EventHandler(this.btnHuyBo_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(11, 74);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(150, 13);
            this.label10.TabIndex = 10;
            this.label10.Text = "Lọc theo giới tính khách hàng";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.radioButtonNu);
            this.panel1.Controls.Add(this.radioButtonNam);
            this.panel1.Location = new System.Drawing.Point(186, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(237, 28);
            this.panel1.TabIndex = 11;
            // 
            // radioButtonNam
            // 
            this.radioButtonNam.AutoSize = true;
            this.radioButtonNam.Location = new System.Drawing.Point(3, 6);
            this.radioButtonNam.Name = "radioButtonNam";
            this.radioButtonNam.Size = new System.Drawing.Size(106, 17);
            this.radioButtonNam.TabIndex = 0;
            this.radioButtonNam.TabStop = true;
            this.radioButtonNam.Text = "Khách hàng nam";
            this.radioButtonNam.UseVisualStyleBackColor = true;
            // 
            // radioButtonNu
            // 
            this.radioButtonNu.AutoSize = true;
            this.radioButtonNu.Location = new System.Drawing.Point(131, 6);
            this.radioButtonNu.Name = "radioButtonNu";
            this.radioButtonNu.Size = new System.Drawing.Size(98, 17);
            this.radioButtonNu.TabIndex = 1;
            this.radioButtonNu.TabStop = true;
            this.radioButtonNu.Text = "Khách hàng nữ";
            this.radioButtonNu.UseVisualStyleBackColor = true;
            // 
            // btn_locGioiTinh
            // 
            this.btn_locGioiTinh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_locGioiTinh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_locGioiTinh.Location = new System.Drawing.Point(429, 64);
            this.btn_locGioiTinh.Name = "btn_locGioiTinh";
            this.btn_locGioiTinh.Size = new System.Drawing.Size(95, 30);
            this.btn_locGioiTinh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_locGioiTinh.TabIndex = 12;
            this.btn_locGioiTinh.Text = "&Lọc khách hàng";
            this.btn_locGioiTinh.Click += new System.EventHandler(this.btn_locGioiTinh_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(39, 48);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(475, 13);
            this.label12.TabIndex = 13;
            this.label12.Text = "---------------------------------------------------------------------------------" +
    "---------------------------------------------------------------------------";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(39, 98);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(475, 13);
            this.label13.TabIndex = 13;
            this.label13.Text = "---------------------------------------------------------------------------------" +
    "---------------------------------------------------------------------------";
            // 
            // FrmSMSTuyBien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 413);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmSMSTuyBien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đặt lịch tin nhắn tùy biến";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmSMSTuyBien_FormClosed);
            this.Load += new System.EventHandler(this.FrmSMSTuyBien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grv_DsKhachHang)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cbb_Search;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox cbb_Key;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_BanTin;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lbl_KyTuCon;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label lbl_KyTuDaNhan;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_SoKH;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btn_DatLich;
        private DevComponents.DotNetBar.ButtonX btn_Loc;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label8;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.ComboBox cbosheet;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevComponents.DotNetBar.ButtonX btnHuyBo;
        private DevComponents.DotNetBar.ButtonX btn_locGioiTinh;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.RadioButton radioButtonNu;
        private System.Windows.Forms.RadioButton radioButtonNam;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        public System.Windows.Forms.DataGridView grv_DsKhachHang;
        public System.Windows.Forms.DateTimePicker dt_NgayNhan;
        public System.Windows.Forms.TextBox txt_GioNhan;
        public System.Windows.Forms.TextBox txt_SMS;
    }
}