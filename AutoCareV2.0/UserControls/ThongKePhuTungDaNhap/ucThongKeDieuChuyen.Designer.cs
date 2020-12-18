namespace AutoCareV2._0.UserControls.ThongKePhuTungDaNhap
{
    partial class ucThongKeDieuChuyen
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucThongKeDieuChuyen));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panelMain = new DevComponents.DotNetBar.PanelEx();
            this.gbContent = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.grvDanhSachDieuChuyen = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.panelSpace = new System.Windows.Forms.Panel();
            this.gbHeader = new DevComponents.DotNetBar.Controls.GroupPanel();
            this.panelFunction = new System.Windows.Forms.Panel();
            this.btnExport = new DevComponents.DotNetBar.ButtonX();
            this.btnTimKiem = new DevComponents.DotNetBar.ButtonX();
            this.dpkTo = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.lblTo = new DevComponents.DotNetBar.LabelX();
            this.dpkFrom = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.lblFrom = new DevComponents.DotNetBar.LabelX();
            this.lblTittle = new DevComponents.DotNetBar.LabelX();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.XoaDieuChuyenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.RowNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenKhoXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenKhoNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ma_PT_Xuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ma_PT_Nhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ten_PT_Xuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Ten_PT_Nhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuongXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoLuongNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdKhoXuat = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdKhoNhap = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelMain.SuspendLayout();
            this.gbContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvDanhSachDieuChuyen)).BeginInit();
            this.gbHeader.SuspendLayout();
            this.panelFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dpkTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpkFrom)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelMain
            // 
            this.panelMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelMain.Controls.Add(this.gbContent);
            this.panelMain.Controls.Add(this.panelSpace);
            this.panelMain.Controls.Add(this.gbHeader);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1000, 530);
            this.panelMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelMain.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelMain.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelMain.Style.GradientAngle = 90;
            this.panelMain.TabIndex = 0;
            // 
            // gbContent
            // 
            this.gbContent.CanvasColor = System.Drawing.SystemColors.Control;
            this.gbContent.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gbContent.Controls.Add(this.grvDanhSachDieuChuyen);
            this.gbContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbContent.Location = new System.Drawing.Point(0, 68);
            this.gbContent.Name = "gbContent";
            this.gbContent.Size = new System.Drawing.Size(1000, 462);
            // 
            // 
            // 
            this.gbContent.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gbContent.Style.BackColorGradientAngle = 90;
            this.gbContent.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gbContent.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbContent.Style.BorderBottomWidth = 1;
            this.gbContent.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gbContent.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbContent.Style.BorderLeftWidth = 1;
            this.gbContent.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbContent.Style.BorderRightWidth = 1;
            this.gbContent.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbContent.Style.BorderTopWidth = 1;
            this.gbContent.Style.CornerDiameter = 4;
            this.gbContent.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gbContent.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gbContent.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gbContent.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gbContent.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gbContent.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gbContent.TabIndex = 2;
            this.gbContent.Text = "Danh sách điều chuyển phụ tùng";
            // 
            // grvDanhSachDieuChuyen
            // 
            this.grvDanhSachDieuChuyen.AllowUserToAddRows = false;
            this.grvDanhSachDieuChuyen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvDanhSachDieuChuyen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvDanhSachDieuChuyen.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.RowNumber,
            this.TenKhoXuat,
            this.TenKhoNhap,
            this.Ma_PT_Xuat,
            this.Ma_PT_Nhap,
            this.Ten_PT_Xuat,
            this.Ten_PT_Nhap,
            this.SoLuongXuat,
            this.SoLuongNhap,
            this.NgayXuat,
            this.NgayNhap,
            this.IdKhoXuat,
            this.IdKhoNhap});
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grvDanhSachDieuChuyen.DefaultCellStyle = dataGridViewCellStyle2;
            this.grvDanhSachDieuChuyen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvDanhSachDieuChuyen.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.grvDanhSachDieuChuyen.Location = new System.Drawing.Point(0, 0);
            this.grvDanhSachDieuChuyen.Name = "grvDanhSachDieuChuyen";
            this.grvDanhSachDieuChuyen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvDanhSachDieuChuyen.Size = new System.Drawing.Size(994, 436);
            this.grvDanhSachDieuChuyen.TabIndex = 0;
            this.grvDanhSachDieuChuyen.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grvDanhSachDieuChuyen_CellMouseDown);
            // 
            // panelSpace
            // 
            this.panelSpace.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelSpace.Location = new System.Drawing.Point(0, 51);
            this.panelSpace.Name = "panelSpace";
            this.panelSpace.Size = new System.Drawing.Size(1000, 17);
            this.panelSpace.TabIndex = 1;
            // 
            // gbHeader
            // 
            this.gbHeader.CanvasColor = System.Drawing.SystemColors.Control;
            this.gbHeader.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.Office2007;
            this.gbHeader.Controls.Add(this.panelFunction);
            this.gbHeader.Controls.Add(this.lblTittle);
            this.gbHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbHeader.Location = new System.Drawing.Point(0, 0);
            this.gbHeader.Name = "gbHeader";
            this.gbHeader.Size = new System.Drawing.Size(1000, 51);
            // 
            // 
            // 
            this.gbHeader.Style.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.gbHeader.Style.BackColorGradientAngle = 90;
            this.gbHeader.Style.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.gbHeader.Style.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbHeader.Style.BorderBottomWidth = 1;
            this.gbHeader.Style.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.gbHeader.Style.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbHeader.Style.BorderLeftWidth = 1;
            this.gbHeader.Style.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbHeader.Style.BorderRightWidth = 1;
            this.gbHeader.Style.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.gbHeader.Style.BorderTopWidth = 1;
            this.gbHeader.Style.CornerDiameter = 4;
            this.gbHeader.Style.CornerType = DevComponents.DotNetBar.eCornerType.Rounded;
            this.gbHeader.Style.TextAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Center;
            this.gbHeader.Style.TextColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.gbHeader.Style.TextLineAlignment = DevComponents.DotNetBar.eStyleTextAlignment.Near;
            // 
            // 
            // 
            this.gbHeader.StyleMouseDown.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            // 
            // 
            // 
            this.gbHeader.StyleMouseOver.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.gbHeader.TabIndex = 0;
            // 
            // panelFunction
            // 
            this.panelFunction.Controls.Add(this.btnExport);
            this.panelFunction.Controls.Add(this.btnTimKiem);
            this.panelFunction.Controls.Add(this.dpkTo);
            this.panelFunction.Controls.Add(this.lblTo);
            this.panelFunction.Controls.Add(this.dpkFrom);
            this.panelFunction.Controls.Add(this.lblFrom);
            this.panelFunction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelFunction.Location = new System.Drawing.Point(292, 0);
            this.panelFunction.Name = "panelFunction";
            this.panelFunction.Size = new System.Drawing.Size(702, 45);
            this.panelFunction.TabIndex = 11;
            // 
            // btnExport
            // 
            this.btnExport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnExport.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnExport.Image = ((System.Drawing.Image)(resources.GetObject("btnExport.Image")));
            this.btnExport.Location = new System.Drawing.Point(589, 7);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(105, 30);
            this.btnExport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnExport.TabIndex = 16;
            this.btnExport.Text = "&Xuất Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTimKiem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTimKiem.Image = ((System.Drawing.Image)(resources.GetObject("btnTimKiem.Image")));
            this.btnTimKiem.Location = new System.Drawing.Point(478, 7);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(105, 30);
            this.btnTimKiem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTimKiem.TabIndex = 16;
            this.btnTimKiem.Text = "&Tìm kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // dpkTo
            // 
            // 
            // 
            // 
            this.dpkTo.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dpkTo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkTo.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dpkTo.ButtonDropDown.Visible = true;
            this.dpkTo.CustomFormat = "dd/MM/yyyy";
            this.dpkTo.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dpkTo.IsPopupCalendarOpen = false;
            this.dpkTo.Location = new System.Drawing.Point(302, 10);
            // 
            // 
            // 
            this.dpkTo.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dpkTo.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkTo.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dpkTo.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dpkTo.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dpkTo.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dpkTo.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dpkTo.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dpkTo.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dpkTo.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dpkTo.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkTo.MonthCalendar.DisplayMonth = new System.DateTime(2013, 12, 1, 0, 0, 0, 0);
            this.dpkTo.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dpkTo.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dpkTo.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dpkTo.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dpkTo.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dpkTo.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkTo.MonthCalendar.TodayButtonVisible = true;
            this.dpkTo.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dpkTo.Name = "dpkTo";
            this.dpkTo.Size = new System.Drawing.Size(136, 25);
            this.dpkTo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dpkTo.TabIndex = 13;
            // 
            // lblTo
            // 
            // 
            // 
            // 
            this.lblTo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTo.Location = new System.Drawing.Point(238, 11);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(58, 23);
            this.lblTo.TabIndex = 14;
            this.lblTo.Text = "Đến ngày:";
            // 
            // dpkFrom
            // 
            // 
            // 
            // 
            this.dpkFrom.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dpkFrom.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkFrom.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dpkFrom.ButtonDropDown.Visible = true;
            this.dpkFrom.CustomFormat = "dd/MM/yyyy";
            this.dpkFrom.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dpkFrom.IsPopupCalendarOpen = false;
            this.dpkFrom.Location = new System.Drawing.Point(76, 10);
            // 
            // 
            // 
            this.dpkFrom.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dpkFrom.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkFrom.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dpkFrom.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dpkFrom.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dpkFrom.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dpkFrom.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dpkFrom.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dpkFrom.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dpkFrom.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dpkFrom.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkFrom.MonthCalendar.DisplayMonth = new System.DateTime(2013, 12, 1, 0, 0, 0, 0);
            this.dpkFrom.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dpkFrom.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dpkFrom.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dpkFrom.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dpkFrom.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dpkFrom.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkFrom.MonthCalendar.TodayButtonVisible = true;
            this.dpkFrom.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dpkFrom.Name = "dpkFrom";
            this.dpkFrom.Size = new System.Drawing.Size(136, 25);
            this.dpkFrom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dpkFrom.TabIndex = 12;
            // 
            // lblFrom
            // 
            // 
            // 
            // 
            this.lblFrom.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblFrom.Location = new System.Drawing.Point(15, 11);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(55, 23);
            this.lblFrom.TabIndex = 15;
            this.lblFrom.Text = "Từ ngày:";
            // 
            // lblTittle
            // 
            // 
            // 
            // 
            this.lblTittle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTittle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTittle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTittle.Location = new System.Drawing.Point(0, 0);
            this.lblTittle.Name = "lblTittle";
            this.lblTittle.Size = new System.Drawing.Size(292, 45);
            this.lblTittle.TabIndex = 10;
            this.lblTittle.Text = "THỐNG KÊ ĐIỀU CHUYỂN PHỤ TÙNG";
            this.lblTittle.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.XoaDieuChuyenToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(163, 26);
            // 
            // XoaDieuChuyenToolStripMenuItem
            // 
            this.XoaDieuChuyenToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("XoaDieuChuyenToolStripMenuItem.Image")));
            this.XoaDieuChuyenToolStripMenuItem.Name = "XoaDieuChuyenToolStripMenuItem";
            this.XoaDieuChuyenToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.XoaDieuChuyenToolStripMenuItem.Text = "Xóa điều chuyển";
            this.XoaDieuChuyenToolStripMenuItem.Click += new System.EventHandler(this.XoaDieuChuyenToolStripMenuItem_Click);
            // 
            // RowNumber
            // 
            this.RowNumber.DataPropertyName = "RowNumber";
            this.RowNumber.FillWeight = 20F;
            this.RowNumber.HeaderText = "STT";
            this.RowNumber.Name = "RowNumber";
            this.RowNumber.ReadOnly = true;
            // 
            // TenKhoXuat
            // 
            this.TenKhoXuat.DataPropertyName = "TenKhoXuat";
            this.TenKhoXuat.HeaderText = "Tên kho xuất";
            this.TenKhoXuat.Name = "TenKhoXuat";
            this.TenKhoXuat.ReadOnly = true;
            // 
            // TenKhoNhap
            // 
            this.TenKhoNhap.DataPropertyName = "TenKhoNhap";
            this.TenKhoNhap.HeaderText = "Tên kho nhập";
            this.TenKhoNhap.Name = "TenKhoNhap";
            this.TenKhoNhap.ReadOnly = true;
            // 
            // Ma_PT_Xuat
            // 
            this.Ma_PT_Xuat.DataPropertyName = "Ma_PT_Xuat";
            this.Ma_PT_Xuat.FillWeight = 60F;
            this.Ma_PT_Xuat.HeaderText = "Mã phụ tùng";
            this.Ma_PT_Xuat.Name = "Ma_PT_Xuat";
            this.Ma_PT_Xuat.ReadOnly = true;
            // 
            // Ma_PT_Nhap
            // 
            this.Ma_PT_Nhap.DataPropertyName = "Ma_PT_Nhap";
            this.Ma_PT_Nhap.HeaderText = "Ma_PT_Nhap";
            this.Ma_PT_Nhap.Name = "Ma_PT_Nhap";
            this.Ma_PT_Nhap.ReadOnly = true;
            this.Ma_PT_Nhap.Visible = false;
            // 
            // Ten_PT_Xuat
            // 
            this.Ten_PT_Xuat.DataPropertyName = "Ten_PT_Xuat";
            this.Ten_PT_Xuat.HeaderText = "Tên phụ tùng";
            this.Ten_PT_Xuat.Name = "Ten_PT_Xuat";
            this.Ten_PT_Xuat.ReadOnly = true;
            // 
            // Ten_PT_Nhap
            // 
            this.Ten_PT_Nhap.DataPropertyName = "Ten_PT_Nhap";
            this.Ten_PT_Nhap.HeaderText = "Ten_PT_Nhap";
            this.Ten_PT_Nhap.Name = "Ten_PT_Nhap";
            this.Ten_PT_Nhap.ReadOnly = true;
            this.Ten_PT_Nhap.Visible = false;
            // 
            // SoLuongXuat
            // 
            this.SoLuongXuat.DataPropertyName = "SoLuongXuat";
            this.SoLuongXuat.FillWeight = 45F;
            this.SoLuongXuat.HeaderText = "Số lượng";
            this.SoLuongXuat.Name = "SoLuongXuat";
            this.SoLuongXuat.ReadOnly = true;
            // 
            // SoLuongNhap
            // 
            this.SoLuongNhap.DataPropertyName = "SoLuongNhap";
            this.SoLuongNhap.HeaderText = "SoLuongNhap";
            this.SoLuongNhap.Name = "SoLuongNhap";
            this.SoLuongNhap.ReadOnly = true;
            this.SoLuongNhap.Visible = false;
            // 
            // NgayXuat
            // 
            this.NgayXuat.DataPropertyName = "NgayXuat";
            dataGridViewCellStyle1.Format = "dd/MM/yyyy";
            this.NgayXuat.DefaultCellStyle = dataGridViewCellStyle1;
            this.NgayXuat.FillWeight = 60F;
            this.NgayXuat.HeaderText = "Ngày";
            this.NgayXuat.Name = "NgayXuat";
            this.NgayXuat.ReadOnly = true;
            // 
            // NgayNhap
            // 
            this.NgayNhap.DataPropertyName = "NgayNhap";
            this.NgayNhap.HeaderText = "NgayNhap";
            this.NgayNhap.Name = "NgayNhap";
            this.NgayNhap.ReadOnly = true;
            this.NgayNhap.Visible = false;
            // 
            // IdKhoXuat
            // 
            this.IdKhoXuat.DataPropertyName = "IdKhoXuat";
            this.IdKhoXuat.HeaderText = "IdKhoXuat";
            this.IdKhoXuat.Name = "IdKhoXuat";
            this.IdKhoXuat.ReadOnly = true;
            this.IdKhoXuat.Visible = false;
            // 
            // IdKhoNhap
            // 
            this.IdKhoNhap.DataPropertyName = "IdKhoNhap";
            this.IdKhoNhap.HeaderText = "IdKhoNhap";
            this.IdKhoNhap.Name = "IdKhoNhap";
            this.IdKhoNhap.ReadOnly = true;
            this.IdKhoNhap.Visible = false;
            // 
            // ucThongKeDieuChuyen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ucThongKeDieuChuyen";
            this.Size = new System.Drawing.Size(1000, 530);
            this.Load += new System.EventHandler(this.ucThongKeDieuChuyen_Load);
            this.panelMain.ResumeLayout(false);
            this.gbContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvDanhSachDieuChuyen)).EndInit();
            this.gbHeader.ResumeLayout(false);
            this.panelFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dpkTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpkFrom)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelMain;
        private DevComponents.DotNetBar.Controls.GroupPanel gbContent;
        private System.Windows.Forms.Panel panelSpace;
        private DevComponents.DotNetBar.Controls.GroupPanel gbHeader;
        private DevComponents.DotNetBar.LabelX lblTittle;
        private System.Windows.Forms.Panel panelFunction;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dpkTo;
        private DevComponents.DotNetBar.LabelX lblTo;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dpkFrom;
        private DevComponents.DotNetBar.LabelX lblFrom;
        private DevComponents.DotNetBar.ButtonX btnTimKiem;
        private DevComponents.DotNetBar.ButtonX btnExport;
        private DevComponents.DotNetBar.Controls.DataGridViewX grvDanhSachDieuChuyen;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem XoaDieuChuyenToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn RowNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKhoXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKhoNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ma_PT_Xuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ma_PT_Nhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ten_PT_Xuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn Ten_PT_Nhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuongXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoLuongNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayNhap;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdKhoXuat;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdKhoNhap;
    }
}
