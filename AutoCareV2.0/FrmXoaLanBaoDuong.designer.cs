namespace AutoCareV2._0
{
    partial class FrmXoaLanBaoDuong
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmXoaLanBaoDuong));
            this.grvDanhSachXeBaoDuong = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BienSo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Sokhung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoMay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayBaoDuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayGiaoXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdBaoDuong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.dt_TuNgay = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dt_DenNgay = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btn_TimKiem = new DevComponents.DotNetBar.ButtonX();
            this.btn_Xoa = new DevComponents.DotNetBar.ButtonX();
            this.gbDanhSachXeBaoDuong = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.inPhieuBaoDuongToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grvDanhSachXeBaoDuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_TuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_DenNgay)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.gbDanhSachXeBaoDuong.SuspendLayout();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // grvDanhSachXeBaoDuong
            // 
            this.grvDanhSachXeBaoDuong.AllowUserToAddRows = false;
            this.grvDanhSachXeBaoDuong.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvDanhSachXeBaoDuong.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grvDanhSachXeBaoDuong.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvDanhSachXeBaoDuong.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.TenXe,
            this.BienSo,
            this.Sokhung,
            this.SoMay,
            this.NgayBaoDuong,
            this.NgayGiaoXe,
            this.IdBaoDuong});
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grvDanhSachXeBaoDuong.DefaultCellStyle = dataGridViewCellStyle4;
            this.grvDanhSachXeBaoDuong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvDanhSachXeBaoDuong.EnableHeadersVisualStyles = false;
            this.grvDanhSachXeBaoDuong.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(170)))), ((int)(((byte)(170)))), ((int)(((byte)(170)))));
            this.grvDanhSachXeBaoDuong.Location = new System.Drawing.Point(3, 16);
            this.grvDanhSachXeBaoDuong.Name = "grvDanhSachXeBaoDuong";
            this.grvDanhSachXeBaoDuong.ReadOnly = true;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grvDanhSachXeBaoDuong.RowHeadersDefaultCellStyle = dataGridViewCellStyle5;
            this.grvDanhSachXeBaoDuong.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvDanhSachXeBaoDuong.Size = new System.Drawing.Size(982, 403);
            this.grvDanhSachXeBaoDuong.TabIndex = 0;
            this.grvDanhSachXeBaoDuong.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewX1_CellClick);
            this.grvDanhSachXeBaoDuong.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grvDanhSachXeBaoDuong_CellMouseDown);
            // 
            // STT
            // 
            this.STT.DataPropertyName = "STT";
            this.STT.FillWeight = 20F;
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            // 
            // TenXe
            // 
            this.TenXe.DataPropertyName = "TenXe";
            this.TenXe.FillWeight = 70F;
            this.TenXe.HeaderText = "Tên Xe";
            this.TenXe.Name = "TenXe";
            this.TenXe.ReadOnly = true;
            // 
            // BienSo
            // 
            this.BienSo.DataPropertyName = "BienSo";
            this.BienSo.FillWeight = 60F;
            this.BienSo.HeaderText = "Biển số";
            this.BienSo.Name = "BienSo";
            this.BienSo.ReadOnly = true;
            // 
            // Sokhung
            // 
            this.Sokhung.DataPropertyName = "Sokhung";
            this.Sokhung.HeaderText = "Số Khung";
            this.Sokhung.Name = "Sokhung";
            this.Sokhung.ReadOnly = true;
            // 
            // SoMay
            // 
            this.SoMay.DataPropertyName = "SoMay";
            this.SoMay.HeaderText = "Số Máy";
            this.SoMay.Name = "SoMay";
            this.SoMay.ReadOnly = true;
            // 
            // NgayBaoDuong
            // 
            this.NgayBaoDuong.DataPropertyName = "NgayBaoDuong";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle2.NullValue = null;
            this.NgayBaoDuong.DefaultCellStyle = dataGridViewCellStyle2;
            this.NgayBaoDuong.FillWeight = 80F;
            this.NgayBaoDuong.HeaderText = "Ngày bảo dưỡng";
            this.NgayBaoDuong.Name = "NgayBaoDuong";
            this.NgayBaoDuong.ReadOnly = true;
            // 
            // NgayGiaoXe
            // 
            this.NgayGiaoXe.DataPropertyName = "NgayGiaoXe";
            dataGridViewCellStyle3.Format = "dd/MM/yyyy HH:mm:ss";
            dataGridViewCellStyle3.NullValue = null;
            this.NgayGiaoXe.DefaultCellStyle = dataGridViewCellStyle3;
            this.NgayGiaoXe.FillWeight = 80F;
            this.NgayGiaoXe.HeaderText = "Ngày giao xe";
            this.NgayGiaoXe.Name = "NgayGiaoXe";
            this.NgayGiaoXe.ReadOnly = true;
            // 
            // IdBaoDuong
            // 
            this.IdBaoDuong.DataPropertyName = "IdBaoDuong";
            this.IdBaoDuong.HeaderText = "IdBaoDuong";
            this.IdBaoDuong.Name = "IdBaoDuong";
            this.IdBaoDuong.ReadOnly = true;
            this.IdBaoDuong.Visible = false;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(25, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(47, 23);
            this.labelX1.TabIndex = 3;
            this.labelX1.Text = "Từ ngày:";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(220, 6);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(56, 23);
            this.labelX2.TabIndex = 3;
            this.labelX2.Text = "Đến ngày:";
            // 
            // dt_TuNgay
            // 
            // 
            // 
            // 
            this.dt_TuNgay.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dt_TuNgay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dt_TuNgay.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dt_TuNgay.ButtonDropDown.Visible = true;
            this.dt_TuNgay.IsPopupCalendarOpen = false;
            this.dt_TuNgay.Location = new System.Drawing.Point(78, 7);
            // 
            // 
            // 
            this.dt_TuNgay.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dt_TuNgay.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dt_TuNgay.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dt_TuNgay.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dt_TuNgay.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dt_TuNgay.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dt_TuNgay.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dt_TuNgay.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dt_TuNgay.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dt_TuNgay.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dt_TuNgay.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dt_TuNgay.MonthCalendar.DisplayMonth = new System.DateTime(2014, 3, 1, 0, 0, 0, 0);
            this.dt_TuNgay.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dt_TuNgay.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dt_TuNgay.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dt_TuNgay.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dt_TuNgay.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dt_TuNgay.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dt_TuNgay.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dt_TuNgay.MonthCalendar.TodayButtonVisible = true;
            this.dt_TuNgay.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dt_TuNgay.Name = "dt_TuNgay";
            this.dt_TuNgay.Size = new System.Drawing.Size(132, 20);
            this.dt_TuNgay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dt_TuNgay.TabIndex = 4;
            // 
            // dt_DenNgay
            // 
            // 
            // 
            // 
            this.dt_DenNgay.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dt_DenNgay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dt_DenNgay.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dt_DenNgay.ButtonDropDown.Visible = true;
            this.dt_DenNgay.IsPopupCalendarOpen = false;
            this.dt_DenNgay.Location = new System.Drawing.Point(278, 7);
            // 
            // 
            // 
            this.dt_DenNgay.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dt_DenNgay.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dt_DenNgay.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dt_DenNgay.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dt_DenNgay.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dt_DenNgay.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dt_DenNgay.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dt_DenNgay.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dt_DenNgay.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dt_DenNgay.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dt_DenNgay.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dt_DenNgay.MonthCalendar.DisplayMonth = new System.DateTime(2014, 3, 1, 0, 0, 0, 0);
            this.dt_DenNgay.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dt_DenNgay.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dt_DenNgay.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dt_DenNgay.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dt_DenNgay.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dt_DenNgay.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dt_DenNgay.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dt_DenNgay.MonthCalendar.TodayButtonVisible = true;
            this.dt_DenNgay.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dt_DenNgay.Name = "dt_DenNgay";
            this.dt_DenNgay.Size = new System.Drawing.Size(132, 20);
            this.dt_DenNgay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dt_DenNgay.TabIndex = 4;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.Location = new System.Drawing.Point(0, 6);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(342, 42);
            this.labelX3.TabIndex = 5;
            this.labelX3.Text = "XÓA LẦN BẢO DƯỠNG";
            this.labelX3.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.labelX4);
            this.groupBox1.Controls.Add(this.dt_TuNgay);
            this.groupBox1.Controls.Add(this.btn_TimKiem);
            this.groupBox1.Controls.Add(this.dt_DenNgay);
            this.groupBox1.Controls.Add(this.btn_Xoa);
            this.groupBox1.Controls.Add(this.labelX1);
            this.groupBox1.Controls.Add(this.labelX2);
            this.groupBox1.Location = new System.Drawing.Point(348, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(628, 73);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.labelX4.Location = new System.Drawing.Point(25, 33);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(439, 23);
            this.labelX4.TabIndex = 5;
            this.labelX4.Text = "(Chú ý: Nhấn Ctrl + F để tìm kiếm một xe trong danh sách, nhấn Esc để hủy bỏ tìm " +
    "kiếm)";
            // 
            // btn_TimKiem
            // 
            this.btn_TimKiem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_TimKiem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_TimKiem.Image = ((System.Drawing.Image)(resources.GetObject("btn_TimKiem.Image")));
            this.btn_TimKiem.Location = new System.Drawing.Point(429, 2);
            this.btn_TimKiem.Name = "btn_TimKiem";
            this.btn_TimKiem.Size = new System.Drawing.Size(95, 30);
            this.btn_TimKiem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_TimKiem.TabIndex = 1;
            this.btn_TimKiem.Text = "&Tìm kiếm";
            this.btn_TimKiem.Click += new System.EventHandler(this.btn_TimKiem_Click);
            // 
            // btn_Xoa
            // 
            this.btn_Xoa.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Xoa.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Xoa.Image = ((System.Drawing.Image)(resources.GetObject("btn_Xoa.Image")));
            this.btn_Xoa.Location = new System.Drawing.Point(530, 2);
            this.btn_Xoa.Name = "btn_Xoa";
            this.btn_Xoa.Size = new System.Drawing.Size(95, 30);
            this.btn_Xoa.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Xoa.TabIndex = 1;
            this.btn_Xoa.Text = "&Xóa";
            this.btn_Xoa.Click += new System.EventHandler(this.btn_Xoa_Click);
            // 
            // gbDanhSachXeBaoDuong
            // 
            this.gbDanhSachXeBaoDuong.Controls.Add(this.grvDanhSachXeBaoDuong);
            this.gbDanhSachXeBaoDuong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbDanhSachXeBaoDuong.Location = new System.Drawing.Point(0, 90);
            this.gbDanhSachXeBaoDuong.Name = "gbDanhSachXeBaoDuong";
            this.gbDanhSachXeBaoDuong.Size = new System.Drawing.Size(988, 422);
            this.gbDanhSachXeBaoDuong.TabIndex = 7;
            this.gbDanhSachXeBaoDuong.TabStop = false;
            this.gbDanhSachXeBaoDuong.Text = "Danh sách xe bảo dưỡng";
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.inPhieuBaoDuongToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(179, 26);
            // 
            // inPhieuBaoDuongToolStripMenuItem
            // 
            this.inPhieuBaoDuongToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("inPhieuBaoDuongToolStripMenuItem.Image")));
            this.inPhieuBaoDuongToolStripMenuItem.Name = "inPhieuBaoDuongToolStripMenuItem";
            this.inPhieuBaoDuongToolStripMenuItem.Size = new System.Drawing.Size(178, 22);
            this.inPhieuBaoDuongToolStripMenuItem.Text = "In phiếu bảo dưỡng";
            this.inPhieuBaoDuongToolStripMenuItem.Click += new System.EventHandler(this.inPhieuBaoDuongToolStripMenuItem_Click);
            // 
            // FrmXoaLanBaoDuong
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(207)))), ((int)(((byte)(221)))), ((int)(((byte)(238)))));
            this.ClientSize = new System.Drawing.Size(988, 512);
            this.Controls.Add(this.gbDanhSachXeBaoDuong);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.labelX3);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmXoaLanBaoDuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Xóa lần bảo dưỡng";
            this.Load += new System.EventHandler(this.FrmXoaLanBaoDuong_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmXoaLanBaoDuong_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.grvDanhSachXeBaoDuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_TuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dt_DenNgay)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.gbDanhSachXeBaoDuong.ResumeLayout(false);
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.DataGridViewX grvDanhSachXeBaoDuong;
        private DevComponents.DotNetBar.ButtonX btn_TimKiem;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dt_TuNgay;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dt_DenNgay;
        private DevComponents.DotNetBar.ButtonX btn_Xoa;
        private DevComponents.DotNetBar.LabelX labelX3;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.LabelX labelX4;
        private System.Windows.Forms.GroupBox gbDanhSachXeBaoDuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn BienSo;
        private System.Windows.Forms.DataGridViewTextBoxColumn Sokhung;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoMay;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayBaoDuong;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayGiaoXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdBaoDuong;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem inPhieuBaoDuongToolStripMenuItem;
    }
}