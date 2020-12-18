namespace AutoCareV2._0.UserControls.ThongKeXe
{
    partial class UcXeDaBan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcXeDaBan));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dtTo = new System.Windows.Forms.DateTimePicker();
            this.dtFrom = new System.Windows.Forms.DateTimePicker();
            this.dtgrvxedaban = new System.Windows.Forms.DataGridView();
            this.lbl_SoXeDaBan = new System.Windows.Forms.Label();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dataGridViewHoaDonBanXe = new System.Windows.Forms.DataGridView();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTongSoXe = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.radioButtonTraThieu = new System.Windows.Forms.RadioButton();
            this.radioButtonTatCa = new System.Windows.Forms.RadioButton();
            this.radioButtonTraDu = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.SoHoaDonBanHang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoChungTu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayTaoHoaDon = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TienDaTra = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CongNo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenNhanVien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoKhung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoMay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DonGia = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenKho = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvxedaban)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHoaDonBanXe)).BeginInit();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(8, 26);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(49, 13);
            this.label2.TabIndex = 12;
            this.label2.Text = "Từ ngày:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(183, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Đến ngày:";
            // 
            // dtTo
            // 
            this.dtTo.CustomFormat = "dd/MM/yyyy";
            this.dtTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtTo.Location = new System.Drawing.Point(183, 42);
            this.dtTo.Name = "dtTo";
            this.dtTo.Size = new System.Drawing.Size(135, 20);
            this.dtTo.TabIndex = 9;
            // 
            // dtFrom
            // 
            this.dtFrom.CustomFormat = "dd/MM/yyyy";
            this.dtFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtFrom.Location = new System.Drawing.Point(8, 42);
            this.dtFrom.Name = "dtFrom";
            this.dtFrom.Size = new System.Drawing.Size(135, 20);
            this.dtFrom.TabIndex = 8;
            // 
            // dtgrvxedaban
            // 
            this.dtgrvxedaban.AllowUserToAddRows = false;
            this.dtgrvxedaban.AllowUserToDeleteRows = false;
            this.dtgrvxedaban.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dtgrvxedaban.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dtgrvxedaban.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.TenXe,
            this.SoKhung,
            this.SoMay,
            this.DonGia,
            this.GhiChu,
            this.TenKho});
            this.dtgrvxedaban.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dtgrvxedaban.Location = new System.Drawing.Point(3, 16);
            this.dtgrvxedaban.Name = "dtgrvxedaban";
            this.dtgrvxedaban.RowHeadersVisible = false;
            this.dtgrvxedaban.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dtgrvxedaban.Size = new System.Drawing.Size(978, 192);
            this.dtgrvxedaban.TabIndex = 7;
            // 
            // lbl_SoXeDaBan
            // 
            this.lbl_SoXeDaBan.AutoSize = true;
            this.lbl_SoXeDaBan.Location = new System.Drawing.Point(653, 23);
            this.lbl_SoXeDaBan.Name = "lbl_SoXeDaBan";
            this.lbl_SoXeDaBan.Size = new System.Drawing.Size(0, 13);
            this.lbl_SoXeDaBan.TabIndex = 13;
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.AutoScroll = true;
            this.panelEx1.AutoSize = true;
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.groupBox5);
            this.panelEx1.Controls.Add(this.groupBox4);
            this.panelEx1.Controls.Add(this.txtTongSoXe);
            this.panelEx1.Controls.Add(this.groupBox3);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.groupBox2);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.groupBox1);
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1000, 585);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 14;
            // 
            // groupBox5
            // 
            this.groupBox5.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox5.Controls.Add(this.dataGridViewHoaDonBanXe);
            this.groupBox5.Location = new System.Drawing.Point(7, 112);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(981, 227);
            this.groupBox5.TabIndex = 21;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Danh sách hóa đơn bán xe";
            // 
            // dataGridViewHoaDonBanXe
            // 
            this.dataGridViewHoaDonBanXe.AllowUserToAddRows = false;
            this.dataGridViewHoaDonBanXe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridViewHoaDonBanXe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewHoaDonBanXe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.SoHoaDonBanHang,
            this.SoChungTu,
            this.NgayTaoHoaDon,
            this.TenKH,
            this.TongTien,
            this.TienDaTra,
            this.CongNo,
            this.TenNhanVien});
            this.dataGridViewHoaDonBanXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridViewHoaDonBanXe.Location = new System.Drawing.Point(3, 16);
            this.dataGridViewHoaDonBanXe.Name = "dataGridViewHoaDonBanXe";
            this.dataGridViewHoaDonBanXe.RowHeadersVisible = false;
            this.dataGridViewHoaDonBanXe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridViewHoaDonBanXe.Size = new System.Drawing.Size(975, 208);
            this.dataGridViewHoaDonBanXe.TabIndex = 0;
            this.dataGridViewHoaDonBanXe.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewHoaDonBanXe_CellClick);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label3);
            this.groupBox4.Controls.Add(this.dtFrom);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.dtTo);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Location = new System.Drawing.Point(264, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(329, 90);
            this.groupBox4.TabIndex = 20;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Thời gian thống kê";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(147, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "==>";
            // 
            // txtTongSoXe
            // 
            this.txtTongSoXe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTongSoXe.Location = new System.Drawing.Point(932, 562);
            this.txtTongSoXe.Name = "txtTongSoXe";
            this.txtTongSoXe.Size = new System.Drawing.Size(53, 20);
            this.txtTongSoXe.TabIndex = 17;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.radioButtonTraThieu);
            this.groupBox3.Controls.Add(this.radioButtonTatCa);
            this.groupBox3.Controls.Add(this.radioButtonTraDu);
            this.groupBox3.Location = new System.Drawing.Point(611, 16);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(136, 90);
            this.groupBox3.TabIndex = 19;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Thống kê theo";
            // 
            // radioButtonTraThieu
            // 
            this.radioButtonTraThieu.Location = new System.Drawing.Point(6, 65);
            this.radioButtonTraThieu.Name = "radioButtonTraThieu";
            this.radioButtonTraThieu.Size = new System.Drawing.Size(114, 17);
            this.radioButtonTraThieu.TabIndex = 0;
            this.radioButtonTraThieu.TabStop = true;
            this.radioButtonTraThieu.Text = "Khách chưa trả đủ";
            this.radioButtonTraThieu.UseVisualStyleBackColor = true;
            // 
            // radioButtonTatCa
            // 
            this.radioButtonTatCa.Location = new System.Drawing.Point(6, 21);
            this.radioButtonTatCa.Name = "radioButtonTatCa";
            this.radioButtonTatCa.Size = new System.Drawing.Size(56, 17);
            this.radioButtonTatCa.TabIndex = 0;
            this.radioButtonTatCa.TabStop = true;
            this.radioButtonTatCa.Text = "Tất cả";
            this.radioButtonTatCa.UseVisualStyleBackColor = true;
            // 
            // radioButtonTraDu
            // 
            this.radioButtonTraDu.AutoSize = true;
            this.radioButtonTraDu.Location = new System.Drawing.Point(6, 42);
            this.radioButtonTraDu.Name = "radioButtonTraDu";
            this.radioButtonTraDu.Size = new System.Drawing.Size(87, 17);
            this.radioButtonTraDu.TabIndex = 18;
            this.radioButtonTraDu.TabStop = true;
            this.radioButtonTraDu.Text = "Khách trả đủ";
            this.radioButtonTraDu.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(863, 565);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(63, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Tổng số xe:";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.dtgrvxedaban);
            this.groupBox2.Location = new System.Drawing.Point(4, 345);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(984, 211);
            this.groupBox2.TabIndex = 15;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Danh sách xe đã bán";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Times New Roman", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.Blue;
            this.labelX1.Location = new System.Drawing.Point(17, 30);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(225, 65);
            this.labelX1.TabIndex = 14;
            this.labelX1.Text = "THỐNG KÊ XE ĐÃ BÁN";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.buttonX1);
            this.groupBox1.Location = new System.Drawing.Point(765, 16);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(223, 90);
            this.groupBox1.TabIndex = 13;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Chức năng";
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.Location = new System.Drawing.Point(117, 35);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(95, 30);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 16;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Image = ((System.Drawing.Image)(resources.GetObject("buttonX1.Image")));
            this.buttonX1.Location = new System.Drawing.Point(13, 35);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(95, 30);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 13;
            this.buttonX1.Text = "&Thống kê";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // SoHoaDonBanHang
            // 
            this.SoHoaDonBanHang.DataPropertyName = "SoHoaDonBanHang";
            this.SoHoaDonBanHang.HeaderText = "Số hóa đơn";
            this.SoHoaDonBanHang.Name = "SoHoaDonBanHang";
            this.SoHoaDonBanHang.ReadOnly = true;
            // 
            // SoChungTu
            // 
            this.SoChungTu.DataPropertyName = "SoChungTu";
            this.SoChungTu.HeaderText = "Số chứng từ";
            this.SoChungTu.Name = "SoChungTu";
            this.SoChungTu.ReadOnly = true;
            // 
            // NgayTaoHoaDon
            // 
            this.NgayTaoHoaDon.DataPropertyName = "NgayTaoHoaDon";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            this.NgayTaoHoaDon.DefaultCellStyle = dataGridViewCellStyle2;
            this.NgayTaoHoaDon.HeaderText = "Ngày tạo";
            this.NgayTaoHoaDon.Name = "NgayTaoHoaDon";
            this.NgayTaoHoaDon.ReadOnly = true;
            // 
            // TenKH
            // 
            this.TenKH.DataPropertyName = "TenKH";
            this.TenKH.HeaderText = "Tên khách hàng";
            this.TenKH.Name = "TenKH";
            this.TenKH.ReadOnly = true;
            // 
            // TongTien
            // 
            this.TongTien.DataPropertyName = "TongTien";
            dataGridViewCellStyle3.Format = "N0";
            dataGridViewCellStyle3.NullValue = null;
            this.TongTien.DefaultCellStyle = dataGridViewCellStyle3;
            this.TongTien.HeaderText = "Tổng tiền";
            this.TongTien.Name = "TongTien";
            this.TongTien.ReadOnly = true;
            // 
            // TienDaTra
            // 
            this.TienDaTra.DataPropertyName = "TienDaTra";
            dataGridViewCellStyle4.Format = "N0";
            dataGridViewCellStyle4.NullValue = null;
            this.TienDaTra.DefaultCellStyle = dataGridViewCellStyle4;
            this.TienDaTra.HeaderText = "Tiền đã trả";
            this.TienDaTra.Name = "TienDaTra";
            this.TienDaTra.ReadOnly = true;
            // 
            // CongNo
            // 
            this.CongNo.DataPropertyName = "CongNo";
            dataGridViewCellStyle5.Format = "N0";
            dataGridViewCellStyle5.NullValue = null;
            this.CongNo.DefaultCellStyle = dataGridViewCellStyle5;
            this.CongNo.HeaderText = "Công nợ";
            this.CongNo.Name = "CongNo";
            this.CongNo.ReadOnly = true;
            // 
            // TenNhanVien
            // 
            this.TenNhanVien.DataPropertyName = "TenNhanVien";
            this.TenNhanVien.HeaderText = "Tên nhân viên";
            this.TenNhanVien.Name = "TenNhanVien";
            this.TenNhanVien.ReadOnly = true;
            // 
            // TenXe
            // 
            this.TenXe.DataPropertyName = "TenXe";
            this.TenXe.HeaderText = "Tên xe";
            this.TenXe.Name = "TenXe";
            this.TenXe.ReadOnly = true;
            // 
            // SoKhung
            // 
            this.SoKhung.DataPropertyName = "SoKhung";
            this.SoKhung.HeaderText = "Số khung";
            this.SoKhung.Name = "SoKhung";
            this.SoKhung.ReadOnly = true;
            // 
            // SoMay
            // 
            this.SoMay.DataPropertyName = "SoMay";
            this.SoMay.HeaderText = "Số máy";
            this.SoMay.Name = "SoMay";
            this.SoMay.ReadOnly = true;
            // 
            // DonGia
            // 
            this.DonGia.DataPropertyName = "DonGia";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.DonGia.DefaultCellStyle = dataGridViewCellStyle1;
            this.DonGia.HeaderText = "Đơn giá";
            this.DonGia.Name = "DonGia";
            this.DonGia.ReadOnly = true;
            // 
            // GhiChu
            // 
            this.GhiChu.DataPropertyName = "GhiChu";
            this.GhiChu.HeaderText = "Ghi chú";
            this.GhiChu.Name = "GhiChu";
            this.GhiChu.ReadOnly = true;
            // 
            // TenKho
            // 
            this.TenKho.DataPropertyName = "TenKho";
            this.TenKho.HeaderText = "Tên kho";
            this.TenKho.Name = "TenKho";
            this.TenKho.ReadOnly = true;
            // 
            // UcXeDaBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.Controls.Add(this.panelEx1);
            this.Controls.Add(this.lbl_SoXeDaBan);
            this.Name = "UcXeDaBan";
            this.Size = new System.Drawing.Size(1000, 591);
            this.Load += new System.EventHandler(this.XeDaBan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dtgrvxedaban)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewHoaDonBanXe)).EndInit();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtTo;
        private System.Windows.Forms.DateTimePicker dtFrom;
        private System.Windows.Forms.DataGridView dtgrvxedaban;
        private System.Windows.Forms.Label lbl_SoXeDaBan;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private System.Windows.Forms.GroupBox groupBox2;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private System.Windows.Forms.TextBox txtTongSoXe;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton radioButtonTraDu;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioButtonTraThieu;
        private System.Windows.Forms.RadioButton radioButtonTatCa;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.DataGridView dataGridViewHoaDonBanXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoKhung;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoMay;
        private System.Windows.Forms.DataGridViewTextBoxColumn DonGia;
        private System.Windows.Forms.DataGridViewTextBoxColumn GhiChu;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKho;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoHoaDonBanHang;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoChungTu;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayTaoHoaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn TienDaTra;
        private System.Windows.Forms.DataGridViewTextBoxColumn CongNo;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenNhanVien;
    }
}
