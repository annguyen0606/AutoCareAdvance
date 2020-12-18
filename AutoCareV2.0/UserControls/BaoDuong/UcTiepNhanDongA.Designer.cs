namespace AutoCareV2._0.UserControls.BaoDuong
{
    partial class UcTiepNhanDongA
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcTiepNhanDongA));
            this.panelMain = new System.Windows.Forms.Panel();
            this.gbLichSuBaoDuong = new System.Windows.Forms.GroupBox();
            this.grvLichSuBaoDuong = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BienSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayBaoDuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayGiaoXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ThayDau = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ThayDauMay = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.YeuCauKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaThoDuyet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenThoDuyet = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GhiChu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaBaoDuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.contextMenuStripInPhieuBaoDuong = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ToolStripMenuItemInPhieuBaoDuong = new System.Windows.Forms.ToolStripMenuItem();
            this.gbKhachVaXeBaoDuong = new System.Windows.Forms.GroupBox();
            this.txtLanBaoDuong = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtGhiChuBaoDuong = new System.Windows.Forms.TextBox();
            this.dpkNgayGiaoXe = new System.Windows.Forms.DateTimePicker();
            this.dpkNgayBaoDuong = new System.Windows.Forms.DateTimePicker();
            this.txtBienSoXe = new System.Windows.Forms.TextBox();
            this.txtTenXe = new System.Windows.Forms.TextBox();
            this.txtDienThoaiKhachHang = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbChucNang = new System.Windows.Forms.GroupBox();
            this.btnThemXe = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.gbThongTinTimKiem = new System.Windows.Forms.GroupBox();
            this.txtTimKiemSoDienThoai = new System.Windows.Forms.TextBox();
            this.txtTimKiemBienSoXe = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTieuDe = new DevComponents.DotNetBar.LabelX();
            this.label10 = new System.Windows.Forms.Label();
            this.txtTenKhachHang = new System.Windows.Forms.TextBox();
            this.panelMain.SuspendLayout();
            this.gbLichSuBaoDuong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvLichSuBaoDuong)).BeginInit();
            this.contextMenuStripInPhieuBaoDuong.SuspendLayout();
            this.gbKhachVaXeBaoDuong.SuspendLayout();
            this.gbChucNang.SuspendLayout();
            this.gbThongTinTimKiem.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.Controls.Add(this.gbLichSuBaoDuong);
            this.panelMain.Controls.Add(this.gbKhachVaXeBaoDuong);
            this.panelMain.Controls.Add(this.gbChucNang);
            this.panelMain.Controls.Add(this.gbThongTinTimKiem);
            this.panelMain.Controls.Add(this.lblTieuDe);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(976, 486);
            this.panelMain.TabIndex = 0;
            // 
            // gbLichSuBaoDuong
            // 
            this.gbLichSuBaoDuong.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbLichSuBaoDuong.Controls.Add(this.grvLichSuBaoDuong);
            this.gbLichSuBaoDuong.Location = new System.Drawing.Point(3, 192);
            this.gbLichSuBaoDuong.Name = "gbLichSuBaoDuong";
            this.gbLichSuBaoDuong.Size = new System.Drawing.Size(970, 280);
            this.gbLichSuBaoDuong.TabIndex = 4;
            this.gbLichSuBaoDuong.TabStop = false;
            this.gbLichSuBaoDuong.Text = "Lịch sử bảo dưỡng xe";
            // 
            // grvLichSuBaoDuong
            // 
            this.grvLichSuBaoDuong.AllowUserToAddRows = false;
            this.grvLichSuBaoDuong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvLichSuBaoDuong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvLichSuBaoDuong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.TenXe,
            this.BienSo,
            this.NgayBaoDuong,
            this.NgayGiaoXe,
            this.SoLan,
            this.ThayDau,
            this.ThayDauMay,
            this.YeuCauKH,
            this.TongTien,
            this.MaThoDuyet,
            this.TenThoDuyet,
            this.GhiChu,
            this.MaBaoDuong});
            this.grvLichSuBaoDuong.ContextMenuStrip = this.contextMenuStripInPhieuBaoDuong;
            this.grvLichSuBaoDuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvLichSuBaoDuong.Location = new System.Drawing.Point(3, 16);
            this.grvLichSuBaoDuong.Name = "grvLichSuBaoDuong";
            this.grvLichSuBaoDuong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvLichSuBaoDuong.Size = new System.Drawing.Size(964, 261);
            this.grvLichSuBaoDuong.TabIndex = 1;
            this.grvLichSuBaoDuong.TabStop = false;
            this.grvLichSuBaoDuong.VirtualMode = true;
            this.grvLichSuBaoDuong.MouseDown += new System.Windows.Forms.MouseEventHandler(this.grvLichSuBaoDuong_MouseDown);
            // 
            // STT
            // 
            this.STT.DataPropertyName = "STT";
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            // 
            // TenXe
            // 
            this.TenXe.DataPropertyName = "TenXe";
            this.TenXe.HeaderText = "Tên xe";
            this.TenXe.Name = "TenXe";
            this.TenXe.ReadOnly = true;
            // 
            // BienSo
            // 
            this.BienSo.DataPropertyName = "BienSo";
            this.BienSo.HeaderText = "Biển số";
            this.BienSo.Name = "BienSo";
            this.BienSo.ReadOnly = true;
            // 
            // NgayBaoDuong
            // 
            this.NgayBaoDuong.DataPropertyName = "NgayBaoDuong";
            this.NgayBaoDuong.HeaderText = "Ngày bảo dưỡng";
            this.NgayBaoDuong.Name = "NgayBaoDuong";
            this.NgayBaoDuong.ReadOnly = true;
            // 
            // NgayGiaoXe
            // 
            this.NgayGiaoXe.DataPropertyName = "NgayGiaoXe";
            this.NgayGiaoXe.HeaderText = "Ngày giao xe";
            this.NgayGiaoXe.Name = "NgayGiaoXe";
            this.NgayGiaoXe.ReadOnly = true;
            // 
            // SoLan
            // 
            this.SoLan.DataPropertyName = "SoLan";
            this.SoLan.HeaderText = "Lần BD";
            this.SoLan.Name = "SoLan";
            this.SoLan.ReadOnly = true;
            // 
            // ThayDau
            // 
            this.ThayDau.DataPropertyName = "ThayDau";
            this.ThayDau.HeaderText = "Thay dầu";
            this.ThayDau.Name = "ThayDau";
            this.ThayDau.ReadOnly = true;
            // 
            // ThayDauMay
            // 
            this.ThayDauMay.DataPropertyName = "ThayDauMay";
            this.ThayDauMay.HeaderText = "Thay dầu máy";
            this.ThayDauMay.Name = "ThayDauMay";
            this.ThayDauMay.ReadOnly = true;
            // 
            // YeuCauKH
            // 
            this.YeuCauKH.DataPropertyName = "YeuCauKH";
            this.YeuCauKH.HeaderText = "Yêu cầu KH";
            this.YeuCauKH.Name = "YeuCauKH";
            this.YeuCauKH.ReadOnly = true;
            // 
            // TongTien
            // 
            this.TongTien.DataPropertyName = "TongTien";
            this.TongTien.HeaderText = "Tổng tiền";
            this.TongTien.Name = "TongTien";
            this.TongTien.ReadOnly = true;
            // 
            // MaThoDuyet
            // 
            this.MaThoDuyet.DataPropertyName = "MaThoDuyet";
            this.MaThoDuyet.HeaderText = "Mã thợ";
            this.MaThoDuyet.Name = "MaThoDuyet";
            this.MaThoDuyet.ReadOnly = true;
            // 
            // TenThoDuyet
            // 
            this.TenThoDuyet.DataPropertyName = "TenThoDuyet";
            this.TenThoDuyet.HeaderText = "Tên thợ";
            this.TenThoDuyet.Name = "TenThoDuyet";
            this.TenThoDuyet.ReadOnly = true;
            // 
            // GhiChu
            // 
            this.GhiChu.DataPropertyName = "GhiChu";
            this.GhiChu.HeaderText = "Ghi chú";
            this.GhiChu.Name = "GhiChu";
            this.GhiChu.ReadOnly = true;
            // 
            // MaBaoDuong
            // 
            this.MaBaoDuong.DataPropertyName = "IDBaoDuong";
            this.MaBaoDuong.HeaderText = "IdBaoDuong";
            this.MaBaoDuong.Name = "MaBaoDuong";
            this.MaBaoDuong.Visible = false;
            // 
            // contextMenuStripInPhieuBaoDuong
            // 
            this.contextMenuStripInPhieuBaoDuong.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemInPhieuBaoDuong});
            this.contextMenuStripInPhieuBaoDuong.Name = "contextMenuStripInPhieuBaoDuong";
            this.contextMenuStripInPhieuBaoDuong.Size = new System.Drawing.Size(179, 26);
            // 
            // ToolStripMenuItemInPhieuBaoDuong
            // 
            this.ToolStripMenuItemInPhieuBaoDuong.Image = ((System.Drawing.Image)(resources.GetObject("ToolStripMenuItemInPhieuBaoDuong.Image")));
            this.ToolStripMenuItemInPhieuBaoDuong.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.ToolStripMenuItemInPhieuBaoDuong.Name = "ToolStripMenuItemInPhieuBaoDuong";
            this.ToolStripMenuItemInPhieuBaoDuong.Size = new System.Drawing.Size(178, 22);
            this.ToolStripMenuItemInPhieuBaoDuong.Text = "In phiếu bảo dưỡng";
            this.ToolStripMenuItemInPhieuBaoDuong.Click += new System.EventHandler(this.ToolStripMenuItemInPhieuBaoDuong_Click);
            // 
            // gbKhachVaXeBaoDuong
            // 
            this.gbKhachVaXeBaoDuong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbKhachVaXeBaoDuong.Controls.Add(this.txtLanBaoDuong);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.label9);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.txtGhiChuBaoDuong);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.dpkNgayGiaoXe);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.dpkNgayBaoDuong);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.txtBienSoXe);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.txtTenXe);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.txtTenKhachHang);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.txtDienThoaiKhachHang);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.label8);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.label7);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.label6);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.label5);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.label4);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.label10);
            this.gbKhachVaXeBaoDuong.Controls.Add(this.label3);
            this.gbKhachVaXeBaoDuong.Location = new System.Drawing.Point(3, 85);
            this.gbKhachVaXeBaoDuong.Name = "gbKhachVaXeBaoDuong";
            this.gbKhachVaXeBaoDuong.Size = new System.Drawing.Size(970, 101);
            this.gbKhachVaXeBaoDuong.TabIndex = 3;
            this.gbKhachVaXeBaoDuong.TabStop = false;
            this.gbKhachVaXeBaoDuong.Text = "Thông tin khách hàng và xe bảo dưỡng";
            // 
            // txtLanBaoDuong
            // 
            this.txtLanBaoDuong.Location = new System.Drawing.Point(705, 72);
            this.txtLanBaoDuong.Name = "txtLanBaoDuong";
            this.txtLanBaoDuong.ReadOnly = true;
            this.txtLanBaoDuong.Size = new System.Drawing.Size(190, 20);
            this.txtLanBaoDuong.TabIndex = 7;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(601, 75);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(79, 13);
            this.label9.TabIndex = 0;
            this.label9.Text = "Lần bảo dưỡng";
            // 
            // txtGhiChuBaoDuong
            // 
            this.txtGhiChuBaoDuong.Location = new System.Drawing.Point(705, 19);
            this.txtGhiChuBaoDuong.Multiline = true;
            this.txtGhiChuBaoDuong.Name = "txtGhiChuBaoDuong";
            this.txtGhiChuBaoDuong.Size = new System.Drawing.Size(190, 45);
            this.txtGhiChuBaoDuong.TabIndex = 6;
            // 
            // dpkNgayGiaoXe
            // 
            this.dpkNgayGiaoXe.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpkNgayGiaoXe.Location = new System.Drawing.Point(400, 68);
            this.dpkNgayGiaoXe.Name = "dpkNgayGiaoXe";
            this.dpkNgayGiaoXe.Size = new System.Drawing.Size(190, 20);
            this.dpkNgayGiaoXe.TabIndex = 5;
            // 
            // dpkNgayBaoDuong
            // 
            this.dpkNgayBaoDuong.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dpkNgayBaoDuong.Location = new System.Drawing.Point(400, 42);
            this.dpkNgayBaoDuong.Name = "dpkNgayBaoDuong";
            this.dpkNgayBaoDuong.Size = new System.Drawing.Size(190, 20);
            this.dpkNgayBaoDuong.TabIndex = 4;
            // 
            // txtBienSoXe
            // 
            this.txtBienSoXe.Location = new System.Drawing.Point(400, 19);
            this.txtBienSoXe.Name = "txtBienSoXe";
            this.txtBienSoXe.Size = new System.Drawing.Size(190, 20);
            this.txtBienSoXe.TabIndex = 3;
            // 
            // txtTenXe
            // 
            this.txtTenXe.Location = new System.Drawing.Point(113, 68);
            this.txtTenXe.Name = "txtTenXe";
            this.txtTenXe.Size = new System.Drawing.Size(190, 20);
            this.txtTenXe.TabIndex = 2;
            // 
            // txtDienThoaiKhachHang
            // 
            this.txtDienThoaiKhachHang.Location = new System.Drawing.Point(113, 42);
            this.txtDienThoaiKhachHang.Name = "txtDienThoaiKhachHang";
            this.txtDienThoaiKhachHang.Size = new System.Drawing.Size(190, 20);
            this.txtDienThoaiKhachHang.TabIndex = 1;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(601, 22);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(98, 13);
            this.label8.TabIndex = 0;
            this.label8.Text = "Ghi chú bảo dưỡng";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(312, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(69, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Ngày giao xe";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(312, 47);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(86, 13);
            this.label6.TabIndex = 0;
            this.label6.Text = "Ngày bảo dưỡng";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(312, 22);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(56, 13);
            this.label5.TabIndex = 0;
            this.label5.Text = "Biển số xe";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 13);
            this.label4.TabIndex = 0;
            this.label4.Text = "Tên xe bảo dưỡng";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Số điện thoại";
            // 
            // gbChucNang
            // 
            this.gbChucNang.Controls.Add(this.btnThemXe);
            this.gbChucNang.Controls.Add(this.btnTimKiem);
            this.gbChucNang.Location = new System.Drawing.Point(572, 3);
            this.gbChucNang.Name = "gbChucNang";
            this.gbChucNang.Size = new System.Drawing.Size(203, 76);
            this.gbChucNang.TabIndex = 2;
            this.gbChucNang.TabStop = false;
            this.gbChucNang.Text = "Chức năng";
            // 
            // btnThemXe
            // 
            this.btnThemXe.Image = ((System.Drawing.Image)(resources.GetObject("btnThemXe.Image")));
            this.btnThemXe.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThemXe.Location = new System.Drawing.Point(104, 30);
            this.btnThemXe.Name = "btnThemXe";
            this.btnThemXe.Size = new System.Drawing.Size(90, 35);
            this.btnThemXe.TabIndex = 2;
            this.btnThemXe.Text = "Thêm xe";
            this.btnThemXe.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnThemXe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnThemXe.UseVisualStyleBackColor = true;
            this.btnThemXe.Click += new System.EventHandler(this.btnThemXe_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Image")));
            this.btnTimKiem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTimKiem.Location = new System.Drawing.Point(8, 30);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(90, 35);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnTimKiem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // gbThongTinTimKiem
            // 
            this.gbThongTinTimKiem.Controls.Add(this.txtTimKiemSoDienThoai);
            this.gbThongTinTimKiem.Controls.Add(this.txtTimKiemBienSoXe);
            this.gbThongTinTimKiem.Controls.Add(this.label2);
            this.gbThongTinTimKiem.Controls.Add(this.label1);
            this.gbThongTinTimKiem.Location = new System.Drawing.Point(330, 3);
            this.gbThongTinTimKiem.Name = "gbThongTinTimKiem";
            this.gbThongTinTimKiem.Size = new System.Drawing.Size(224, 76);
            this.gbThongTinTimKiem.TabIndex = 1;
            this.gbThongTinTimKiem.TabStop = false;
            this.gbThongTinTimKiem.Text = "Tìm kiếm thông tin bảo dưỡng";
            // 
            // txtTimKiemSoDienThoai
            // 
            this.txtTimKiemSoDienThoai.Location = new System.Drawing.Point(84, 50);
            this.txtTimKiemSoDienThoai.Name = "txtTimKiemSoDienThoai";
            this.txtTimKiemSoDienThoai.Size = new System.Drawing.Size(133, 20);
            this.txtTimKiemSoDienThoai.TabIndex = 2;
            // 
            // txtTimKiemBienSoXe
            // 
            this.txtTimKiemBienSoXe.Location = new System.Drawing.Point(84, 26);
            this.txtTimKiemBienSoXe.Name = "txtTimKiemBienSoXe";
            this.txtTimKiemBienSoXe.Size = new System.Drawing.Size(133, 20);
            this.txtTimKiemBienSoXe.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(22, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Biển số xe";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số điện thoại";
            // 
            // lblTieuDe
            // 
            // 
            // 
            // 
            this.lblTieuDe.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTieuDe.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTieuDe.Location = new System.Drawing.Point(3, 3);
            this.lblTieuDe.Name = "lblTieuDe";
            this.lblTieuDe.Size = new System.Drawing.Size(321, 76);
            this.lblTieuDe.TabIndex = 0;
            this.lblTieuDe.Text = "TIẾP NHẬN XE BẢO DƯỠNG";
            this.lblTieuDe.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(14, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(86, 13);
            this.label10.TabIndex = 0;
            this.label10.Text = "Tên khách hàng";
            // 
            // txtTenKhachHang
            // 
            this.txtTenKhachHang.Location = new System.Drawing.Point(113, 19);
            this.txtTenKhachHang.Name = "txtTenKhachHang";
            this.txtTenKhachHang.Size = new System.Drawing.Size(190, 20);
            this.txtTenKhachHang.TabIndex = 0;
            // 
            // UcTiepNhanDongA
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.Controls.Add(this.panelMain);
            this.Name = "UcTiepNhanDongA";
            this.Size = new System.Drawing.Size(976, 486);
            this.Load += new System.EventHandler(this.UcTiepNhanDongA_Load);
            this.panelMain.ResumeLayout(false);
            this.gbLichSuBaoDuong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvLichSuBaoDuong)).EndInit();
            this.contextMenuStripInPhieuBaoDuong.ResumeLayout(false);
            this.gbKhachVaXeBaoDuong.ResumeLayout(false);
            this.gbKhachVaXeBaoDuong.PerformLayout();
            this.gbChucNang.ResumeLayout(false);
            this.gbThongTinTimKiem.ResumeLayout(false);
            this.gbThongTinTimKiem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelMain;
        private DevComponents.DotNetBar.LabelX lblTieuDe;
        private System.Windows.Forms.GroupBox gbChucNang;
        private System.Windows.Forms.GroupBox gbThongTinTimKiem;
        private System.Windows.Forms.GroupBox gbLichSuBaoDuong;
        private System.Windows.Forms.GroupBox gbKhachVaXeBaoDuong;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnThemXe;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.TextBox txtTimKiemSoDienThoai;
        private System.Windows.Forms.TextBox txtTimKiemBienSoXe;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtBienSoXe;
        private System.Windows.Forms.TextBox txtTenXe;
        private System.Windows.Forms.TextBox txtDienThoaiKhachHang;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker dpkNgayBaoDuong;
        private System.Windows.Forms.TextBox txtGhiChuBaoDuong;
        private System.Windows.Forms.DateTimePicker dpkNgayGiaoXe;
        private System.Windows.Forms.DataGridView grvLichSuBaoDuong;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripInPhieuBaoDuong;
        private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemInPhieuBaoDuong;
        private System.Windows.Forms.TextBox txtLanBaoDuong;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn BienSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayBaoDuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayGiaoXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLan;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ThayDau;
        private System.Windows.Forms.DataGridViewCheckBoxColumn ThayDauMay;
        private System.Windows.Forms.DataGridViewTextBoxColumn YeuCauKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaThoDuyet;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenThoDuyet;
        private System.Windows.Forms.DataGridViewTextBoxColumn GhiChu;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaBaoDuong;
        private System.Windows.Forms.TextBox txtTenKhachHang;
        private System.Windows.Forms.Label label10;

    }
}
