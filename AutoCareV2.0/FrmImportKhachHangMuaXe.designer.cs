namespace AutoCareV2._0
{
    partial class FrmImportKhachHangMuaXe
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmImportKhachHangMuaXe));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.gbCustInfo = new System.Windows.Forms.GroupBox();
            this.cboSoSBH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.cboDiaChi = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.cboCMND = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.cboDienThoai = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.cboGioiTinh = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.cboNgaySinh = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.cboTenKhachHang = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.cboHoKhachHang = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.gbXeInfo = new System.Windows.Forms.GroupBox();
            this.cboLoaiKH = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX17 = new DevComponents.DotNetBar.LabelX();
            this.cboSoLuong = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.cboGia = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX10 = new DevComponents.DotNetBar.LabelX();
            this.cboSoMay = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX11 = new DevComponents.DotNetBar.LabelX();
            this.cboSoKhung = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX12 = new DevComponents.DotNetBar.LabelX();
            this.cboBienSo = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX13 = new DevComponents.DotNetBar.LabelX();
            this.cboMauXe = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX14 = new DevComponents.DotNetBar.LabelX();
            this.cboTenXe = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX15 = new DevComponents.DotNetBar.LabelX();
            this.cboNgayMua = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX16 = new DevComponents.DotNetBar.LabelX();
            this.cbSheets = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.label4 = new System.Windows.Forms.Label();
            this.cbb_DinhDang = new System.Windows.Forms.ComboBox();
            this.chk_ThayDau = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.gbCustInfo.SuspendLayout();
            this.gbXeInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "excel 2003 file|*.xls| Excel 2007 | *.xlsx";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Location = new System.Drawing.Point(75, 9);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(291, 20);
            this.txtFilePath.TabIndex = 0;
            this.txtFilePath.MouseDown += new System.Windows.Forms.MouseEventHandler(this.txtFilePath_MouseDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Chọn file";
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(3, 63);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(756, 190);
            this.dataGridView1.TabIndex = 3;
            // 
            // gbCustInfo
            // 
            this.gbCustInfo.Controls.Add(this.cboSoSBH);
            this.gbCustInfo.Controls.Add(this.labelX8);
            this.gbCustInfo.Controls.Add(this.cboDiaChi);
            this.gbCustInfo.Controls.Add(this.labelX7);
            this.gbCustInfo.Controls.Add(this.cboCMND);
            this.gbCustInfo.Controls.Add(this.labelX6);
            this.gbCustInfo.Controls.Add(this.cboDienThoai);
            this.gbCustInfo.Controls.Add(this.labelX5);
            this.gbCustInfo.Controls.Add(this.cboGioiTinh);
            this.gbCustInfo.Controls.Add(this.labelX4);
            this.gbCustInfo.Controls.Add(this.cboNgaySinh);
            this.gbCustInfo.Controls.Add(this.labelX3);
            this.gbCustInfo.Controls.Add(this.cboTenKhachHang);
            this.gbCustInfo.Controls.Add(this.labelX2);
            this.gbCustInfo.Controls.Add(this.cboHoKhachHang);
            this.gbCustInfo.Controls.Add(this.labelX1);
            this.gbCustInfo.Location = new System.Drawing.Point(2, 259);
            this.gbCustInfo.Name = "gbCustInfo";
            this.gbCustInfo.Size = new System.Drawing.Size(364, 267);
            this.gbCustInfo.TabIndex = 4;
            this.gbCustInfo.TabStop = false;
            this.gbCustInfo.Text = "Trường dữ liệu Khách hàng";
            // 
            // cboSoSBH
            // 
            this.cboSoSBH.DisplayMember = "Text";
            this.cboSoSBH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSoSBH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSoSBH.FormattingEnabled = true;
            this.cboSoSBH.ItemHeight = 14;
            this.cboSoSBH.Location = new System.Drawing.Point(110, 207);
            this.cboSoSBH.Name = "cboSoSBH";
            this.cboSoSBH.Size = new System.Drawing.Size(235, 20);
            this.cboSoSBH.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSoSBH.TabIndex = 1;
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(11, 204);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(88, 23);
            this.labelX8.TabIndex = 0;
            this.labelX8.Text = "Số SBH:";
            // 
            // cboDiaChi
            // 
            this.cboDiaChi.DisplayMember = "Text";
            this.cboDiaChi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDiaChi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDiaChi.FormattingEnabled = true;
            this.cboDiaChi.ItemHeight = 14;
            this.cboDiaChi.Location = new System.Drawing.Point(110, 181);
            this.cboDiaChi.Name = "cboDiaChi";
            this.cboDiaChi.Size = new System.Drawing.Size(235, 20);
            this.cboDiaChi.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDiaChi.TabIndex = 1;
            // 
            // labelX7
            // 
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Location = new System.Drawing.Point(11, 178);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(88, 23);
            this.labelX7.TabIndex = 0;
            this.labelX7.Text = "Địa chỉ:";
            // 
            // cboCMND
            // 
            this.cboCMND.DisplayMember = "Text";
            this.cboCMND.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCMND.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboCMND.FormattingEnabled = true;
            this.cboCMND.ItemHeight = 14;
            this.cboCMND.Location = new System.Drawing.Point(110, 155);
            this.cboCMND.Name = "cboCMND";
            this.cboCMND.Size = new System.Drawing.Size(235, 20);
            this.cboCMND.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboCMND.TabIndex = 1;
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(11, 152);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(88, 23);
            this.labelX6.TabIndex = 0;
            this.labelX6.Text = "Số CMND:";
            // 
            // cboDienThoai
            // 
            this.cboDienThoai.DisplayMember = "Text";
            this.cboDienThoai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDienThoai.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboDienThoai.FormattingEnabled = true;
            this.cboDienThoai.ItemHeight = 14;
            this.cboDienThoai.Location = new System.Drawing.Point(110, 129);
            this.cboDienThoai.Name = "cboDienThoai";
            this.cboDienThoai.Size = new System.Drawing.Size(235, 20);
            this.cboDienThoai.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDienThoai.TabIndex = 1;
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(11, 126);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(88, 23);
            this.labelX5.TabIndex = 0;
            this.labelX5.Text = "Số điện thoại:";
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.DisplayMember = "Text";
            this.cboGioiTinh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGioiTinh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboGioiTinh.FormattingEnabled = true;
            this.cboGioiTinh.ItemHeight = 14;
            this.cboGioiTinh.Location = new System.Drawing.Point(110, 103);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(235, 20);
            this.cboGioiTinh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboGioiTinh.TabIndex = 1;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(11, 100);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(88, 23);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "Giới tính:";
            // 
            // cboNgaySinh
            // 
            this.cboNgaySinh.DisplayMember = "Text";
            this.cboNgaySinh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNgaySinh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNgaySinh.FormattingEnabled = true;
            this.cboNgaySinh.ItemHeight = 14;
            this.cboNgaySinh.Location = new System.Drawing.Point(110, 77);
            this.cboNgaySinh.Name = "cboNgaySinh";
            this.cboNgaySinh.Size = new System.Drawing.Size(235, 20);
            this.cboNgaySinh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboNgaySinh.TabIndex = 1;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(11, 74);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(88, 23);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "Ngày sinh:";
            // 
            // cboTenKhachHang
            // 
            this.cboTenKhachHang.DisplayMember = "Text";
            this.cboTenKhachHang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTenKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTenKhachHang.FormattingEnabled = true;
            this.cboTenKhachHang.ItemHeight = 14;
            this.cboTenKhachHang.Location = new System.Drawing.Point(110, 51);
            this.cboTenKhachHang.Name = "cboTenKhachHang";
            this.cboTenKhachHang.Size = new System.Drawing.Size(235, 20);
            this.cboTenKhachHang.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTenKhachHang.TabIndex = 1;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(11, 48);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(88, 23);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "Tên khách hàng:";
            // 
            // cboHoKhachHang
            // 
            this.cboHoKhachHang.DisplayMember = "Text";
            this.cboHoKhachHang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboHoKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboHoKhachHang.FormattingEnabled = true;
            this.cboHoKhachHang.ItemHeight = 14;
            this.cboHoKhachHang.Location = new System.Drawing.Point(110, 25);
            this.cboHoKhachHang.Name = "cboHoKhachHang";
            this.cboHoKhachHang.Size = new System.Drawing.Size(235, 20);
            this.cboHoKhachHang.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboHoKhachHang.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(11, 22);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(88, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Họ khách hàng:";
            // 
            // gbXeInfo
            // 
            this.gbXeInfo.Controls.Add(this.cboLoaiKH);
            this.gbXeInfo.Controls.Add(this.labelX17);
            this.gbXeInfo.Controls.Add(this.cboSoLuong);
            this.gbXeInfo.Controls.Add(this.labelX9);
            this.gbXeInfo.Controls.Add(this.cboGia);
            this.gbXeInfo.Controls.Add(this.labelX10);
            this.gbXeInfo.Controls.Add(this.cboSoMay);
            this.gbXeInfo.Controls.Add(this.labelX11);
            this.gbXeInfo.Controls.Add(this.cboSoKhung);
            this.gbXeInfo.Controls.Add(this.labelX12);
            this.gbXeInfo.Controls.Add(this.cboBienSo);
            this.gbXeInfo.Controls.Add(this.labelX13);
            this.gbXeInfo.Controls.Add(this.cboMauXe);
            this.gbXeInfo.Controls.Add(this.labelX14);
            this.gbXeInfo.Controls.Add(this.cboTenXe);
            this.gbXeInfo.Controls.Add(this.labelX15);
            this.gbXeInfo.Controls.Add(this.cboNgayMua);
            this.gbXeInfo.Controls.Add(this.labelX16);
            this.gbXeInfo.Location = new System.Drawing.Point(384, 259);
            this.gbXeInfo.Name = "gbXeInfo";
            this.gbXeInfo.Size = new System.Drawing.Size(375, 267);
            this.gbXeInfo.TabIndex = 5;
            this.gbXeInfo.TabStop = false;
            this.gbXeInfo.Text = "Trường dữ liệu của Xe";
            // 
            // cboLoaiKH
            // 
            this.cboLoaiKH.DisplayMember = "Text";
            this.cboLoaiKH.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiKH.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboLoaiKH.FormattingEnabled = true;
            this.cboLoaiKH.ItemHeight = 14;
            this.cboLoaiKH.Location = new System.Drawing.Point(115, 233);
            this.cboLoaiKH.Name = "cboLoaiKH";
            this.cboLoaiKH.Size = new System.Drawing.Size(235, 20);
            this.cboLoaiKH.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiKH.TabIndex = 10;
            // 
            // labelX17
            // 
            // 
            // 
            // 
            this.labelX17.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX17.Location = new System.Drawing.Point(16, 230);
            this.labelX17.Name = "labelX17";
            this.labelX17.Size = new System.Drawing.Size(88, 23);
            this.labelX17.TabIndex = 2;
            this.labelX17.Text = "Loại KH:";
            // 
            // cboSoLuong
            // 
            this.cboSoLuong.DisplayMember = "Text";
            this.cboSoLuong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSoLuong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSoLuong.FormattingEnabled = true;
            this.cboSoLuong.ItemHeight = 14;
            this.cboSoLuong.Location = new System.Drawing.Point(115, 207);
            this.cboSoLuong.Name = "cboSoLuong";
            this.cboSoLuong.Size = new System.Drawing.Size(235, 20);
            this.cboSoLuong.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSoLuong.TabIndex = 10;
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(16, 204);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(88, 23);
            this.labelX9.TabIndex = 2;
            this.labelX9.Text = "Số lượng:";
            // 
            // cboGia
            // 
            this.cboGia.DisplayMember = "Text";
            this.cboGia.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGia.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboGia.FormattingEnabled = true;
            this.cboGia.ItemHeight = 14;
            this.cboGia.Location = new System.Drawing.Point(115, 181);
            this.cboGia.Name = "cboGia";
            this.cboGia.Size = new System.Drawing.Size(235, 20);
            this.cboGia.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboGia.TabIndex = 11;
            // 
            // labelX10
            // 
            // 
            // 
            // 
            this.labelX10.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX10.Location = new System.Drawing.Point(16, 178);
            this.labelX10.Name = "labelX10";
            this.labelX10.Size = new System.Drawing.Size(88, 23);
            this.labelX10.TabIndex = 3;
            this.labelX10.Text = "Giá:";
            // 
            // cboSoMay
            // 
            this.cboSoMay.DisplayMember = "Text";
            this.cboSoMay.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSoMay.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSoMay.FormattingEnabled = true;
            this.cboSoMay.ItemHeight = 14;
            this.cboSoMay.Location = new System.Drawing.Point(115, 155);
            this.cboSoMay.Name = "cboSoMay";
            this.cboSoMay.Size = new System.Drawing.Size(235, 20);
            this.cboSoMay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSoMay.TabIndex = 12;
            // 
            // labelX11
            // 
            // 
            // 
            // 
            this.labelX11.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX11.Location = new System.Drawing.Point(16, 152);
            this.labelX11.Name = "labelX11";
            this.labelX11.Size = new System.Drawing.Size(88, 23);
            this.labelX11.TabIndex = 4;
            this.labelX11.Text = "Số máy:";
            // 
            // cboSoKhung
            // 
            this.cboSoKhung.DisplayMember = "Text";
            this.cboSoKhung.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSoKhung.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSoKhung.FormattingEnabled = true;
            this.cboSoKhung.ItemHeight = 14;
            this.cboSoKhung.Location = new System.Drawing.Point(115, 129);
            this.cboSoKhung.Name = "cboSoKhung";
            this.cboSoKhung.Size = new System.Drawing.Size(235, 20);
            this.cboSoKhung.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboSoKhung.TabIndex = 13;
            // 
            // labelX12
            // 
            // 
            // 
            // 
            this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX12.Location = new System.Drawing.Point(16, 126);
            this.labelX12.Name = "labelX12";
            this.labelX12.Size = new System.Drawing.Size(88, 23);
            this.labelX12.TabIndex = 5;
            this.labelX12.Text = "Số khung:";
            // 
            // cboBienSo
            // 
            this.cboBienSo.DisplayMember = "Text";
            this.cboBienSo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboBienSo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboBienSo.FormattingEnabled = true;
            this.cboBienSo.ItemHeight = 14;
            this.cboBienSo.Location = new System.Drawing.Point(115, 103);
            this.cboBienSo.Name = "cboBienSo";
            this.cboBienSo.Size = new System.Drawing.Size(235, 20);
            this.cboBienSo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboBienSo.TabIndex = 14;
            // 
            // labelX13
            // 
            // 
            // 
            // 
            this.labelX13.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX13.Location = new System.Drawing.Point(16, 100);
            this.labelX13.Name = "labelX13";
            this.labelX13.Size = new System.Drawing.Size(88, 23);
            this.labelX13.TabIndex = 6;
            this.labelX13.Text = "Biển số:";
            // 
            // cboMauXe
            // 
            this.cboMauXe.DisplayMember = "Text";
            this.cboMauXe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboMauXe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboMauXe.FormattingEnabled = true;
            this.cboMauXe.ItemHeight = 14;
            this.cboMauXe.Location = new System.Drawing.Point(115, 77);
            this.cboMauXe.Name = "cboMauXe";
            this.cboMauXe.Size = new System.Drawing.Size(235, 20);
            this.cboMauXe.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboMauXe.TabIndex = 15;
            // 
            // labelX14
            // 
            // 
            // 
            // 
            this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX14.Location = new System.Drawing.Point(16, 74);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new System.Drawing.Size(88, 23);
            this.labelX14.TabIndex = 7;
            this.labelX14.Text = "Màu xe:";
            // 
            // cboTenXe
            // 
            this.cboTenXe.DisplayMember = "Text";
            this.cboTenXe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTenXe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTenXe.FormattingEnabled = true;
            this.cboTenXe.ItemHeight = 14;
            this.cboTenXe.Location = new System.Drawing.Point(115, 51);
            this.cboTenXe.Name = "cboTenXe";
            this.cboTenXe.Size = new System.Drawing.Size(235, 20);
            this.cboTenXe.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTenXe.TabIndex = 16;
            // 
            // labelX15
            // 
            // 
            // 
            // 
            this.labelX15.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX15.Location = new System.Drawing.Point(16, 48);
            this.labelX15.Name = "labelX15";
            this.labelX15.Size = new System.Drawing.Size(88, 23);
            this.labelX15.TabIndex = 8;
            this.labelX15.Text = "Tên xe:";
            // 
            // cboNgayMua
            // 
            this.cboNgayMua.DisplayMember = "Text";
            this.cboNgayMua.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNgayMua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboNgayMua.FormattingEnabled = true;
            this.cboNgayMua.ItemHeight = 14;
            this.cboNgayMua.Location = new System.Drawing.Point(115, 25);
            this.cboNgayMua.Name = "cboNgayMua";
            this.cboNgayMua.Size = new System.Drawing.Size(235, 20);
            this.cboNgayMua.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboNgayMua.TabIndex = 17;
            // 
            // labelX16
            // 
            // 
            // 
            // 
            this.labelX16.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX16.Location = new System.Drawing.Point(16, 22);
            this.labelX16.Name = "labelX16";
            this.labelX16.Size = new System.Drawing.Size(88, 23);
            this.labelX16.TabIndex = 9;
            this.labelX16.Text = "Ngày mua xe:";
            // 
            // cbSheets
            // 
            this.cbSheets.FormattingEnabled = true;
            this.cbSheets.Location = new System.Drawing.Point(444, 9);
            this.cbSheets.Name = "cbSheets";
            this.cbSheets.Size = new System.Drawing.Size(112, 21);
            this.cbSheets.TabIndex = 7;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(375, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Chọn Sheet";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(0, 538);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(394, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Chú ý: Các trường phải có: SĐT, Ngày mua, Biển số hoặc Số khung hoặc Số máy";
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(431, 535);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(328, 20);
            this.progressBar1.TabIndex = 10;
            this.progressBar1.Visible = false;
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Image = ((System.Drawing.Image)(resources.GetObject("buttonX1.Image")));
            this.buttonX1.Location = new System.Drawing.Point(562, 4);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(95, 30);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 11;
            this.buttonX1.Text = "&Open File";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX2.Image = ((System.Drawing.Image)(resources.GetObject("buttonX2.Image")));
            this.buttonX2.Location = new System.Drawing.Point(664, 4);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(95, 30);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 12;
            this.buttonX2.Text = "&Import";
            this.buttonX2.Click += new System.EventHandler(this.buttonX2_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(13, 40);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(56, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "Định dạng";
            // 
            // cbb_DinhDang
            // 
            this.cbb_DinhDang.FormattingEnabled = true;
            this.cbb_DinhDang.Items.AddRange(new object[] {
            "Ngay/Thang/Nam",
            "Thang/Ngay/Nam"});
            this.cbb_DinhDang.Location = new System.Drawing.Point(75, 36);
            this.cbb_DinhDang.Name = "cbb_DinhDang";
            this.cbb_DinhDang.Size = new System.Drawing.Size(140, 21);
            this.cbb_DinhDang.TabIndex = 14;
            // 
            // chk_ThayDau
            // 
            this.chk_ThayDau.AutoSize = true;
            this.chk_ThayDau.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chk_ThayDau.Location = new System.Drawing.Point(223, 38);
            this.chk_ThayDau.Name = "chk_ThayDau";
            this.chk_ThayDau.Size = new System.Drawing.Size(280, 17);
            this.chk_ThayDau.TabIndex = 15;
            this.chk_ThayDau.Text = "Nhắn tin mời thay dầu (dựa vào mốc là \'Ngày mua xe\')";
            this.chk_ThayDau.UseVisualStyleBackColor = true;
            // 
            // FrmImportKhachHangMuaXe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(769, 561);
            this.Controls.Add(this.chk_ThayDau);
            this.Controls.Add(this.cbb_DinhDang);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.buttonX2);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbSheets);
            this.Controls.Add(this.gbXeInfo);
            this.Controls.Add(this.gbCustInfo);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtFilePath);
            this.Name = "FrmImportKhachHangMuaXe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thêm danh sách khách hàng mua xe";
            this.Load += new System.EventHandler(this.FrmImportKhachHangMuaXe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.gbCustInfo.ResumeLayout(false);
            this.gbXeInfo.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.GroupBox gbCustInfo;
        private System.Windows.Forms.GroupBox gbXeInfo;
        private System.Windows.Forms.ComboBox cbSheets;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ProgressBar progressBar1;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbb_DinhDang;
        private System.Windows.Forms.CheckBox chk_ThayDau;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboHoKhachHang;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSoSBH;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDiaChi;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboCMND;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDienThoai;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGioiTinh;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNgaySinh;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTenKhachHang;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiKH;
        private DevComponents.DotNetBar.LabelX labelX17;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSoLuong;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboGia;
        private DevComponents.DotNetBar.LabelX labelX10;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSoMay;
        private DevComponents.DotNetBar.LabelX labelX11;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboSoKhung;
        private DevComponents.DotNetBar.LabelX labelX12;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboBienSo;
        private DevComponents.DotNetBar.LabelX labelX13;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboMauXe;
        private DevComponents.DotNetBar.LabelX labelX14;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTenXe;
        private DevComponents.DotNetBar.LabelX labelX15;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNgayMua;
        private DevComponents.DotNetBar.LabelX labelX16;
    }
}