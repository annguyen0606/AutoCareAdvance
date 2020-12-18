namespace AutoCareV2._0.UserControls
{
    partial class UcPhieuChi
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcPhieuChi));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.cboKhachHang = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtLyDoChi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtNguoiNhan = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDiaChi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.btnAddCustomer = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX9 = new DevComponents.DotNetBar.LabelX();
            this.cboLoaiPhieuChi = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.txtTienChi = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.txtSoHoaDon = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.expandablePanel1 = new DevComponents.DotNetBar.ExpandablePanel();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.dateTimeInputNgayHachToan = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.expandablePanel2 = new DevComponents.DotNetBar.ExpandablePanel();
            this.buttonItem5 = new DevComponents.DotNetBar.ButtonItem();
            this.btnLuu = new DevComponents.DotNetBar.ButtonItem();
            this.btnReset = new DevComponents.DotNetBar.ButtonItem();
            this.bar1 = new DevComponents.DotNetBar.Bar();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.expandablePanel3 = new DevComponents.DotNetBar.ExpandablePanel();
            this.panelEx5 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx6 = new DevComponents.DotNetBar.PanelEx();
            this.grvDanhSachPhieuChi = new System.Windows.Forms.DataGridView();
            this.STT = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoPhieuChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LoaiPhieuChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoTienChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LyDo = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NguoiNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayChi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.contextMenuPhieuChi = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.InPhieuChiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.XoaPhieuChiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panelEx3.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.expandablePanel1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInputNgayHachToan)).BeginInit();
            this.expandablePanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.expandablePanel3.SuspendLayout();
            this.panelEx5.SuspendLayout();
            this.panelEx6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvDanhSachPhieuChi)).BeginInit();
            this.contextMenuPhieuChi.SuspendLayout();
            this.SuspendLayout();
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(11, 71);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(87, 23);
            this.labelX5.TabIndex = 0;
            this.labelX5.Text = "Người nhận:";
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboKhachHang.DisplayMember = "Text";
            this.cboKhachHang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKhachHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.ItemHeight = 17;
            this.cboKhachHang.Location = new System.Drawing.Point(104, 14);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(621, 23);
            this.cboKhachHang.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboKhachHang.TabIndex = 0;
            // 
            // txtLyDoChi
            // 
            this.txtLyDoChi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtLyDoChi.Border.Class = "TextBoxBorder";
            this.txtLyDoChi.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtLyDoChi.Location = new System.Drawing.Point(104, 100);
            this.txtLyDoChi.Multiline = true;
            this.txtLyDoChi.Name = "txtLyDoChi";
            this.txtLyDoChi.Size = new System.Drawing.Size(679, 68);
            this.txtLyDoChi.TabIndex = 3;
            // 
            // txtNguoiNhan
            // 
            this.txtNguoiNhan.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtNguoiNhan.Border.Class = "TextBoxBorder";
            this.txtNguoiNhan.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtNguoiNhan.Location = new System.Drawing.Point(104, 71);
            this.txtNguoiNhan.Name = "txtNguoiNhan";
            this.txtNguoiNhan.Size = new System.Drawing.Size(679, 23);
            this.txtNguoiNhan.TabIndex = 2;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtDiaChi.Border.Class = "TextBoxBorder";
            this.txtDiaChi.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDiaChi.Location = new System.Drawing.Point(104, 44);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.Size = new System.Drawing.Size(679, 23);
            this.txtDiaChi.TabIndex = 1;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(11, 94);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(87, 23);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Lý do chi:";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(11, 42);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(87, 23);
            this.labelX3.TabIndex = 0;
            this.labelX3.Text = "Địa chỉ:";
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.btnAddCustomer);
            this.panelEx3.Controls.Add(this.labelX5);
            this.panelEx3.Controls.Add(this.cboKhachHang);
            this.panelEx3.Controls.Add(this.txtLyDoChi);
            this.panelEx3.Controls.Add(this.txtNguoiNhan);
            this.panelEx3.Controls.Add(this.txtDiaChi);
            this.panelEx3.Controls.Add(this.labelX1);
            this.panelEx3.Controls.Add(this.labelX2);
            this.panelEx3.Controls.Add(this.labelX3);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(0, 0);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(818, 177);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 0;
            // 
            // btnAddCustomer
            // 
            this.btnAddCustomer.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAddCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddCustomer.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnAddCustomer.Location = new System.Drawing.Point(731, 15);
            this.btnAddCustomer.Name = "btnAddCustomer";
            this.btnAddCustomer.Size = new System.Drawing.Size(52, 21);
            this.btnAddCustomer.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAddCustomer.TabIndex = 0;
            this.btnAddCustomer.Text = "Thêm";
            this.btnAddCustomer.Click += new System.EventHandler(this.btnAddCustomer_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(11, 14);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(87, 23);
            this.labelX2.TabIndex = 0;
            this.labelX2.Text = "Khách hàng:";
            // 
            // labelX9
            // 
            // 
            // 
            // 
            this.labelX9.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX9.Location = new System.Drawing.Point(13, 102);
            this.labelX9.Name = "labelX9";
            this.labelX9.Size = new System.Drawing.Size(91, 23);
            this.labelX9.TabIndex = 0;
            this.labelX9.Text = "Loại chi:";
            // 
            // cboLoaiPhieuChi
            // 
            this.cboLoaiPhieuChi.DisplayMember = "Text";
            this.cboLoaiPhieuChi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiPhieuChi.FormattingEnabled = true;
            this.cboLoaiPhieuChi.ItemHeight = 17;
            this.cboLoaiPhieuChi.Location = new System.Drawing.Point(111, 102);
            this.cboLoaiPhieuChi.Name = "cboLoaiPhieuChi";
            this.cboLoaiPhieuChi.Size = new System.Drawing.Size(193, 23);
            this.cboLoaiPhieuChi.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboLoaiPhieuChi.TabIndex = 3;
            // 
            // txtTienChi
            // 
            this.txtTienChi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtTienChi.Border.Class = "TextBoxBorder";
            this.txtTienChi.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTienChi.Location = new System.Drawing.Point(111, 70);
            this.txtTienChi.Name = "txtTienChi";
            this.txtTienChi.Size = new System.Drawing.Size(193, 23);
            this.txtTienChi.TabIndex = 2;
            this.txtTienChi.TextChanged += new System.EventHandler(this.txtTienChi_TextChanged);
            // 
            // labelX8
            // 
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Location = new System.Drawing.Point(13, 70);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(91, 23);
            this.labelX8.TabIndex = 0;
            this.labelX8.Text = "Tiền Chi:";
            // 
            // txtSoHoaDon
            // 
            this.txtSoHoaDon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.txtSoHoaDon.Border.Class = "TextBoxBorder";
            this.txtSoHoaDon.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSoHoaDon.Location = new System.Drawing.Point(111, 39);
            this.txtSoHoaDon.Name = "txtSoHoaDon";
            this.txtSoHoaDon.Size = new System.Drawing.Size(193, 23);
            this.txtSoHoaDon.TabIndex = 1;
            this.txtSoHoaDon.WatermarkText = "Nhập 0 nếu không có hóa đơn";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.panelEx3);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 26);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(818, 177);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // expandablePanel1
            // 
            this.expandablePanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expandablePanel1.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel1.Controls.Add(this.panelEx2);
            this.expandablePanel1.HideControlsWhenCollapsed = true;
            this.expandablePanel1.Location = new System.Drawing.Point(3, 57);
            this.expandablePanel1.Name = "expandablePanel1";
            this.expandablePanel1.Size = new System.Drawing.Size(818, 203);
            this.expandablePanel1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel1.Style.GradientAngle = 90;
            this.expandablePanel1.TabIndex = 0;
            this.expandablePanel1.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel1.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel1.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel1.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel1.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel1.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel1.TitleStyle.GradientAngle = 90;
            this.expandablePanel1.TitleText = "Thông tin chung";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(13, 39);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(91, 23);
            this.labelX6.TabIndex = 0;
            this.labelX6.Text = "Số hóa đơn:";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(13, 8);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(91, 23);
            this.labelX4.TabIndex = 0;
            this.labelX4.Text = "Ngày chi:";
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.labelX9);
            this.panelEx4.Controls.Add(this.cboLoaiPhieuChi);
            this.panelEx4.Controls.Add(this.txtTienChi);
            this.panelEx4.Controls.Add(this.labelX8);
            this.panelEx4.Controls.Add(this.txtSoHoaDon);
            this.panelEx4.Controls.Add(this.labelX6);
            this.panelEx4.Controls.Add(this.labelX4);
            this.panelEx4.Controls.Add(this.dateTimeInputNgayHachToan);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(0, 26);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(316, 177);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx4.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 0;
            // 
            // dateTimeInputNgayHachToan
            // 
            // 
            // 
            // 
            this.dateTimeInputNgayHachToan.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateTimeInputNgayHachToan.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputNgayHachToan.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dateTimeInputNgayHachToan.ButtonDropDown.Visible = true;
            this.dateTimeInputNgayHachToan.IsPopupCalendarOpen = false;
            this.dateTimeInputNgayHachToan.Location = new System.Drawing.Point(111, 8);
            // 
            // 
            // 
            this.dateTimeInputNgayHachToan.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInputNgayHachToan.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputNgayHachToan.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dateTimeInputNgayHachToan.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateTimeInputNgayHachToan.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateTimeInputNgayHachToan.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInputNgayHachToan.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateTimeInputNgayHachToan.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateTimeInputNgayHachToan.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateTimeInputNgayHachToan.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateTimeInputNgayHachToan.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputNgayHachToan.MonthCalendar.DisplayMonth = new System.DateTime(2013, 12, 1, 0, 0, 0, 0);
            this.dateTimeInputNgayHachToan.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateTimeInputNgayHachToan.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInputNgayHachToan.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateTimeInputNgayHachToan.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInputNgayHachToan.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateTimeInputNgayHachToan.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputNgayHachToan.MonthCalendar.TodayButtonVisible = true;
            this.dateTimeInputNgayHachToan.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dateTimeInputNgayHachToan.Name = "dateTimeInputNgayHachToan";
            this.dateTimeInputNgayHachToan.Size = new System.Drawing.Size(193, 23);
            this.dateTimeInputNgayHachToan.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dateTimeInputNgayHachToan.TabIndex = 0;
            // 
            // expandablePanel2
            // 
            this.expandablePanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.expandablePanel2.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel2.Controls.Add(this.panelEx4);
            this.expandablePanel2.HideControlsWhenCollapsed = true;
            this.expandablePanel2.Location = new System.Drawing.Point(827, 57);
            this.expandablePanel2.Name = "expandablePanel2";
            this.expandablePanel2.Size = new System.Drawing.Size(316, 203);
            this.expandablePanel2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel2.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel2.Style.GradientAngle = 90;
            this.expandablePanel2.TabIndex = 1;
            this.expandablePanel2.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel2.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel2.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel2.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel2.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel2.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel2.TitleStyle.GradientAngle = 90;
            this.expandablePanel2.TitleText = "Thông tin chứng từ";
            // 
            // buttonItem5
            // 
            this.buttonItem5.Image = ((System.Drawing.Image)(resources.GetObject("buttonItem5.Image")));
            this.buttonItem5.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.buttonItem5.Name = "buttonItem5";
            this.buttonItem5.Stretch = true;
            this.buttonItem5.Text = "In";
            this.buttonItem5.Visible = false;
            // 
            // btnLuu
            // 
            this.btnLuu.Image = ((System.Drawing.Image)(resources.GetObject("btnLuu.Image")));
            this.btnLuu.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Stretch = true;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnReset
            // 
            this.btnReset.Image = ((System.Drawing.Image)(resources.GetObject("btnReset.Image")));
            this.btnReset.ImagePosition = DevComponents.DotNetBar.eImagePosition.Top;
            this.btnReset.Name = "btnReset";
            this.btnReset.Stretch = true;
            this.btnReset.Text = "Reset";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // bar1
            // 
            this.bar1.AntiAlias = true;
            this.bar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.bar1.DockSide = DevComponents.DotNetBar.eDockSide.Document;
            this.bar1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.bar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnReset,
            this.btnLuu,
            this.buttonItem5});
            this.bar1.Location = new System.Drawing.Point(979, 0);
            this.bar1.Name = "bar1";
            this.bar1.Size = new System.Drawing.Size(167, 54);
            this.bar1.Stretch = true;
            this.bar1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.bar1.TabIndex = 25;
            this.bar1.TabStop = false;
            this.bar1.Text = "bar1";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.expandablePanel3);
            this.panelEx1.Controls.Add(this.labelX7);
            this.panelEx1.Controls.Add(this.bar1);
            this.panelEx1.Controls.Add(this.expandablePanel2);
            this.panelEx1.Controls.Add(this.expandablePanel1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1146, 546);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // expandablePanel3
            // 
            this.expandablePanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.expandablePanel3.CanvasColor = System.Drawing.SystemColors.Control;
            this.expandablePanel3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.expandablePanel3.Controls.Add(this.panelEx5);
            this.expandablePanel3.HideControlsWhenCollapsed = true;
            this.expandablePanel3.Location = new System.Drawing.Point(3, 275);
            this.expandablePanel3.Name = "expandablePanel3";
            this.expandablePanel3.Size = new System.Drawing.Size(1140, 268);
            this.expandablePanel3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel3.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.expandablePanel3.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.expandablePanel3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.ItemText;
            this.expandablePanel3.Style.GradientAngle = 90;
            this.expandablePanel3.TabIndex = 27;
            this.expandablePanel3.TitleStyle.Alignment = System.Drawing.StringAlignment.Center;
            this.expandablePanel3.TitleStyle.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.expandablePanel3.TitleStyle.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.expandablePanel3.TitleStyle.Border = DevComponents.DotNetBar.eBorderType.RaisedInner;
            this.expandablePanel3.TitleStyle.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.expandablePanel3.TitleStyle.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.expandablePanel3.TitleStyle.GradientAngle = 90;
            this.expandablePanel3.TitleText = "Danh sách các phiếu chi tạo trong ngày";
            // 
            // panelEx5
            // 
            this.panelEx5.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx5.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx5.Controls.Add(this.panelEx6);
            this.panelEx5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx5.Location = new System.Drawing.Point(0, 26);
            this.panelEx5.Name = "panelEx5";
            this.panelEx5.Size = new System.Drawing.Size(1140, 242);
            this.panelEx5.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx5.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx5.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx5.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx5.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx5.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx5.Style.GradientAngle = 90;
            this.panelEx5.TabIndex = 0;
            // 
            // panelEx6
            // 
            this.panelEx6.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx6.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx6.Controls.Add(this.grvDanhSachPhieuChi);
            this.panelEx6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx6.Location = new System.Drawing.Point(0, 0);
            this.panelEx6.Name = "panelEx6";
            this.panelEx6.Size = new System.Drawing.Size(1140, 242);
            this.panelEx6.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx6.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx6.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx6.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx6.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx6.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx6.Style.GradientAngle = 90;
            this.panelEx6.TabIndex = 0;
            // 
            // grvDanhSachPhieuChi
            // 
            this.grvDanhSachPhieuChi.AllowUserToAddRows = false;
            this.grvDanhSachPhieuChi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvDanhSachPhieuChi.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvDanhSachPhieuChi.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.STT,
            this.SoPhieuChi,
            this.SoHD,
            this.LoaiPhieuChi,
            this.SoTienChi,
            this.LyDo,
            this.NguoiNhan,
            this.NgayChi});
            this.grvDanhSachPhieuChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvDanhSachPhieuChi.Location = new System.Drawing.Point(0, 0);
            this.grvDanhSachPhieuChi.Name = "grvDanhSachPhieuChi";
            this.grvDanhSachPhieuChi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvDanhSachPhieuChi.Size = new System.Drawing.Size(1140, 242);
            this.grvDanhSachPhieuChi.TabIndex = 0;
            this.grvDanhSachPhieuChi.CellMouseDown += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grvDanhSachPhieuChi_CellMouseDown);
            // 
            // STT
            // 
            this.STT.DataPropertyName = "RowNum";
            this.STT.FillWeight = 30F;
            this.STT.HeaderText = "STT";
            this.STT.Name = "STT";
            this.STT.ReadOnly = true;
            // 
            // SoPhieuChi
            // 
            this.SoPhieuChi.DataPropertyName = "SoPhieuChi";
            this.SoPhieuChi.FillWeight = 70F;
            this.SoPhieuChi.HeaderText = "Số phiếu chi";
            this.SoPhieuChi.Name = "SoPhieuChi";
            this.SoPhieuChi.ReadOnly = true;
            // 
            // SoHD
            // 
            this.SoHD.DataPropertyName = "SoHoaDon";
            this.SoHD.FillWeight = 70F;
            this.SoHD.HeaderText = "Số hóa đơn";
            this.SoHD.Name = "SoHD";
            this.SoHD.ReadOnly = true;
            // 
            // LoaiPhieuChi
            // 
            this.LoaiPhieuChi.DataPropertyName = "TenLoaiPhieuChi";
            this.LoaiPhieuChi.FillWeight = 90F;
            this.LoaiPhieuChi.HeaderText = "Loại phiếu chi";
            this.LoaiPhieuChi.Name = "LoaiPhieuChi";
            this.LoaiPhieuChi.ReadOnly = true;
            // 
            // SoTienChi
            // 
            this.SoTienChi.DataPropertyName = "SoTienChi";
            dataGridViewCellStyle1.Format = "N0";
            dataGridViewCellStyle1.NullValue = null;
            this.SoTienChi.DefaultCellStyle = dataGridViewCellStyle1;
            this.SoTienChi.FillWeight = 60F;
            this.SoTienChi.HeaderText = "Số tiền";
            this.SoTienChi.Name = "SoTienChi";
            this.SoTienChi.ReadOnly = true;
            // 
            // LyDo
            // 
            this.LyDo.DataPropertyName = "NoiDung";
            this.LyDo.HeaderText = "Lý do chi";
            this.LyDo.Name = "LyDo";
            this.LyDo.ReadOnly = true;
            // 
            // NguoiNhan
            // 
            this.NguoiNhan.DataPropertyName = "NguoiNhan";
            this.NguoiNhan.FillWeight = 80F;
            this.NguoiNhan.HeaderText = "Người nhận";
            this.NguoiNhan.Name = "NguoiNhan";
            this.NguoiNhan.ReadOnly = true;
            // 
            // NgayChi
            // 
            this.NgayChi.DataPropertyName = "NgayHachToan";
            dataGridViewCellStyle2.Format = "dd/MM/yyyy";
            this.NgayChi.DefaultCellStyle = dataGridViewCellStyle2;
            this.NgayChi.FillWeight = 80F;
            this.NgayChi.HeaderText = "Ngày chi";
            this.NgayChi.Name = "NgayChi";
            this.NgayChi.ReadOnly = true;
            // 
            // labelX7
            // 
            this.labelX7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("Times New Roman", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.Location = new System.Drawing.Point(3, 0);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(905, 57);
            this.labelX7.TabIndex = 26;
            this.labelX7.Text = "PHIẾU CHI";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // contextMenuPhieuChi
            // 
            this.contextMenuPhieuChi.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.InPhieuChiToolStripMenuItem,
            this.toolStripMenuItem2,
            this.XoaPhieuChiToolStripMenuItem});
            this.contextMenuPhieuChi.Name = "contextMenuPhieuChi";
            this.contextMenuPhieuChi.Size = new System.Drawing.Size(147, 54);
            // 
            // InPhieuChiToolStripMenuItem
            // 
            this.InPhieuChiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("InPhieuChiToolStripMenuItem.Image")));
            this.InPhieuChiToolStripMenuItem.Name = "InPhieuChiToolStripMenuItem";
            this.InPhieuChiToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.InPhieuChiToolStripMenuItem.Text = "In phiếu chi";
            this.InPhieuChiToolStripMenuItem.Click += new System.EventHandler(this.InPhieuChiToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(143, 6);
            // 
            // XoaPhieuChiToolStripMenuItem
            // 
            this.XoaPhieuChiToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("XoaPhieuChiToolStripMenuItem.Image")));
            this.XoaPhieuChiToolStripMenuItem.Name = "XoaPhieuChiToolStripMenuItem";
            this.XoaPhieuChiToolStripMenuItem.Size = new System.Drawing.Size(146, 22);
            this.XoaPhieuChiToolStripMenuItem.Text = "Xóa phiếu chi";
            this.XoaPhieuChiToolStripMenuItem.Click += new System.EventHandler(this.XoaPhieuChiToolStripMenuItem_Click);
            // 
            // UcPhieuChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx1);
            this.Name = "UcPhieuChi";
            this.Size = new System.Drawing.Size(1146, 546);
            this.Load += new System.EventHandler(this.UcPhieuChi_Load);
            this.panelEx3.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.expandablePanel1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInputNgayHachToan)).EndInit();
            this.expandablePanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bar1)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.expandablePanel3.ResumeLayout(false);
            this.panelEx5.ResumeLayout(false);
            this.panelEx6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvDanhSachPhieuChi)).EndInit();
            this.contextMenuPhieuChi.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboKhachHang;
        private DevComponents.DotNetBar.Controls.TextBoxX txtLyDoChi;
        private DevComponents.DotNetBar.Controls.TextBoxX txtNguoiNhan;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDiaChi;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX9;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboLoaiPhieuChi;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTienChi;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSoHoaDon;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel1;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateTimeInputNgayHachToan;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel2;
        private DevComponents.DotNetBar.ButtonItem buttonItem5;
        private DevComponents.DotNetBar.ButtonItem btnLuu;
        private DevComponents.DotNetBar.ButtonItem btnReset;
        private DevComponents.DotNetBar.Bar bar1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.LabelX labelX7;
        private DevComponents.DotNetBar.ButtonX btnAddCustomer;
        private DevComponents.DotNetBar.ExpandablePanel expandablePanel3;
        private DevComponents.DotNetBar.PanelEx panelEx5;
        private DevComponents.DotNetBar.PanelEx panelEx6;
        private System.Windows.Forms.DataGridView grvDanhSachPhieuChi;
        private System.Windows.Forms.ContextMenuStrip contextMenuPhieuChi;
        private System.Windows.Forms.ToolStripMenuItem InPhieuChiToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem XoaPhieuChiToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn STT;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoPhieuChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn LoaiPhieuChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoTienChi;
        private System.Windows.Forms.DataGridViewTextBoxColumn LyDo;
        private System.Windows.Forms.DataGridViewTextBoxColumn NguoiNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayChi;
    }
}
