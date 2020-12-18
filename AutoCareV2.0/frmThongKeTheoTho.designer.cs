namespace AutoCareV2._0
{
    partial class frmThongKeTheoTho
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmThongKeTheoTho));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.chkTheotho = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cboThongtintho = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.btnThongKe = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dateTimeInputDenNgay = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dateTimeInputTuNgay = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.frmThongKeBaoDuongTheoKhachHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ThoiGianBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInputDenNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInputTuNgay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.frmThongKeBaoDuongTheoKhachHangBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThoiGianBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.labelX4);
            this.panelEx1.Controls.Add(this.chkTheotho);
            this.panelEx1.Controls.Add(this.cboThongtintho);
            this.panelEx1.Controls.Add(this.labelX3);
            this.panelEx1.Controls.Add(this.reportViewer1);
            this.panelEx1.Controls.Add(this.btnThongKe);
            this.panelEx1.Controls.Add(this.labelX2);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.dateTimeInputDenNgay);
            this.panelEx1.Controls.Add(this.dateTimeInputTuNgay);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(854, 668);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.Location = new System.Drawing.Point(12, 11);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(327, 89);
            this.labelX4.TabIndex = 13;
            this.labelX4.Text = "THỐNG KÊ THEO THỢ";
            this.labelX4.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // chkTheotho
            // 
            // 
            // 
            // 
            this.chkTheotho.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkTheotho.Location = new System.Drawing.Point(602, 74);
            this.chkTheotho.Name = "chkTheotho";
            this.chkTheotho.Size = new System.Drawing.Size(100, 23);
            this.chkTheotho.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkTheotho.TabIndex = 12;
            this.chkTheotho.Text = "Theo Thợ";
            // 
            // cboThongtintho
            // 
            this.cboThongtintho.DisplayMember = "Text";
            this.cboThongtintho.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboThongtintho.FormattingEnabled = true;
            this.cboThongtintho.ItemHeight = 14;
            this.cboThongtintho.Location = new System.Drawing.Point(439, 77);
            this.cboThongtintho.Name = "cboThongtintho";
            this.cboThongtintho.Size = new System.Drawing.Size(157, 20);
            this.cboThongtintho.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboThongtintho.TabIndex = 11;
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(369, 74);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(57, 23);
            this.labelX3.TabIndex = 10;
            this.labelX3.Text = "Tên Thợ:";
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Bottom;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.frmThongKeBaoDuongTheoKhachHangBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.ThoiGianBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.ReportThongKeBaoDuongTheoTho.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 107);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(854, 561);
            this.reportViewer1.TabIndex = 9;
            // 
            // btnThongKe
            // 
            this.btnThongKe.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnThongKe.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnThongKe.Image = ((System.Drawing.Image)(resources.GetObject("btnThongKe.Image")));
            this.btnThongKe.ImageFixedSize = new System.Drawing.Size(30, 30);
            this.btnThongKe.Location = new System.Drawing.Point(602, 20);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(95, 30);
            this.btnThongKe.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnThongKe.TabIndex = 8;
            this.btnThongKe.Text = "&Thống kê";
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(369, 45);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(57, 23);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "Đến ngày:";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(369, 20);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(57, 23);
            this.labelX1.TabIndex = 6;
            this.labelX1.Text = "Từ ngày:";
            // 
            // dateTimeInputDenNgay
            // 
            // 
            // 
            // 
            this.dateTimeInputDenNgay.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateTimeInputDenNgay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputDenNgay.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dateTimeInputDenNgay.ButtonDropDown.Visible = true;
            this.dateTimeInputDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dateTimeInputDenNgay.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dateTimeInputDenNgay.IsPopupCalendarOpen = false;
            this.dateTimeInputDenNgay.Location = new System.Drawing.Point(439, 48);
            // 
            // 
            // 
            this.dateTimeInputDenNgay.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInputDenNgay.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputDenNgay.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dateTimeInputDenNgay.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateTimeInputDenNgay.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateTimeInputDenNgay.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInputDenNgay.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateTimeInputDenNgay.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateTimeInputDenNgay.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateTimeInputDenNgay.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateTimeInputDenNgay.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputDenNgay.MonthCalendar.DisplayMonth = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimeInputDenNgay.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateTimeInputDenNgay.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInputDenNgay.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateTimeInputDenNgay.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInputDenNgay.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateTimeInputDenNgay.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputDenNgay.MonthCalendar.TodayButtonVisible = true;
            this.dateTimeInputDenNgay.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dateTimeInputDenNgay.Name = "dateTimeInputDenNgay";
            this.dateTimeInputDenNgay.Size = new System.Drawing.Size(157, 20);
            this.dateTimeInputDenNgay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dateTimeInputDenNgay.TabIndex = 4;
            // 
            // dateTimeInputTuNgay
            // 
            // 
            // 
            // 
            this.dateTimeInputTuNgay.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dateTimeInputTuNgay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputTuNgay.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dateTimeInputTuNgay.ButtonDropDown.Visible = true;
            this.dateTimeInputTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dateTimeInputTuNgay.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dateTimeInputTuNgay.IsPopupCalendarOpen = false;
            this.dateTimeInputTuNgay.Location = new System.Drawing.Point(439, 20);
            // 
            // 
            // 
            this.dateTimeInputTuNgay.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInputTuNgay.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputTuNgay.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dateTimeInputTuNgay.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dateTimeInputTuNgay.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dateTimeInputTuNgay.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInputTuNgay.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dateTimeInputTuNgay.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dateTimeInputTuNgay.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dateTimeInputTuNgay.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dateTimeInputTuNgay.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputTuNgay.MonthCalendar.DisplayMonth = new System.DateTime(2014, 1, 1, 0, 0, 0, 0);
            this.dateTimeInputTuNgay.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dateTimeInputTuNgay.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dateTimeInputTuNgay.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dateTimeInputTuNgay.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dateTimeInputTuNgay.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dateTimeInputTuNgay.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dateTimeInputTuNgay.MonthCalendar.TodayButtonVisible = true;
            this.dateTimeInputTuNgay.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dateTimeInputTuNgay.Name = "dateTimeInputTuNgay";
            this.dateTimeInputTuNgay.Size = new System.Drawing.Size(157, 20);
            this.dateTimeInputTuNgay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dateTimeInputTuNgay.TabIndex = 5;
            // 
            // frmThongKeBaoDuongTheoKhachHangBindingSource
            // 
            this.frmThongKeBaoDuongTheoKhachHangBindingSource.DataSource = typeof(AutoCareV2._0.Class.frmThongKeBaoDuongTheoKhachHang);
            // 
            // ThoiGianBindingSource
            // 
            this.ThoiGianBindingSource.DataSource = typeof(AutoCareV2._0.Class.ThoiGian);
            // 
            // frmThongKeTheoTho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 668);
            this.Controls.Add(this.panelEx1);
            this.Name = "frmThongKeTheoTho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê theo thợ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmThongKeTheoTho_FormClosing);
            this.Load += new System.EventHandler(this.frmThongKeTheoTho_Load_1);
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInputDenNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dateTimeInputTuNgay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.frmThongKeBaoDuongTheoKhachHangBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThoiGianBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnThongKe;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateTimeInputDenNgay;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dateTimeInputTuNgay;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource frmThongKeBaoDuongTheoKhachHangBindingSource;
        private System.Windows.Forms.BindingSource ThoiGianBindingSource;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkTheotho;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboThongtintho;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
    }
}