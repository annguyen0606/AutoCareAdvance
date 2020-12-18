namespace AutoCareV2._0
{
    partial class frmBangCongTho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmBangCongTho));
            this.panelHeader = new System.Windows.Forms.Panel();
            this.btnStatistic = new DevComponents.DotNetBar.ButtonX();
            this.dpkDateTo = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.lblDateTo = new DevComponents.DotNetBar.LabelX();
            this.dpkDateFrom = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.lblDateFrom = new DevComponents.DotNetBar.LabelX();
            this.lblBangChamCongTho = new DevComponents.DotNetBar.LabelX();
            this.groupBoxContent = new System.Windows.Forms.GroupBox();
            this.reportViewerCongTho = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panelHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dpkDateTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpkDateFrom)).BeginInit();
            this.groupBoxContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Controls.Add(this.btnStatistic);
            this.panelHeader.Controls.Add(this.dpkDateTo);
            this.panelHeader.Controls.Add(this.lblDateTo);
            this.panelHeader.Controls.Add(this.dpkDateFrom);
            this.panelHeader.Controls.Add(this.lblDateFrom);
            this.panelHeader.Controls.Add(this.lblBangChamCongTho);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(854, 74);
            this.panelHeader.TabIndex = 0;
            // 
            // btnStatistic
            // 
            this.btnStatistic.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnStatistic.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnStatistic.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnStatistic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStatistic.Location = new System.Drawing.Point(750, 26);
            this.btnStatistic.Name = "btnStatistic";
            this.btnStatistic.Size = new System.Drawing.Size(92, 23);
            this.btnStatistic.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnStatistic.TabIndex = 3;
            this.btnStatistic.Text = "Thống kê";
            this.btnStatistic.Click += new System.EventHandler(this.btnStatistic_Click);
            // 
            // dpkDateTo
            // 
            this.dpkDateTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.dpkDateTo.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dpkDateTo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkDateTo.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dpkDateTo.ButtonDropDown.Visible = true;
            this.dpkDateTo.CustomFormat = "dd/MM/yyyy";
            this.dpkDateTo.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dpkDateTo.IsPopupCalendarOpen = false;
            this.dpkDateTo.Location = new System.Drawing.Point(579, 40);
            // 
            // 
            // 
            this.dpkDateTo.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dpkDateTo.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkDateTo.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dpkDateTo.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dpkDateTo.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dpkDateTo.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dpkDateTo.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dpkDateTo.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dpkDateTo.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dpkDateTo.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dpkDateTo.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkDateTo.MonthCalendar.DisplayMonth = new System.DateTime(2015, 8, 1, 0, 0, 0, 0);
            this.dpkDateTo.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dpkDateTo.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dpkDateTo.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dpkDateTo.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dpkDateTo.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dpkDateTo.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dpkDateTo.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkDateTo.MonthCalendar.TodayButtonVisible = true;
            this.dpkDateTo.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dpkDateTo.Name = "dpkDateTo";
            this.dpkDateTo.Size = new System.Drawing.Size(152, 22);
            this.dpkDateTo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dpkDateTo.TabIndex = 2;
            // 
            // lblDateTo
            // 
            this.lblDateTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.lblDateTo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDateTo.Location = new System.Drawing.Point(467, 40);
            this.lblDateTo.Name = "lblDateTo";
            this.lblDateTo.Size = new System.Drawing.Size(106, 23);
            this.lblDateTo.TabIndex = 1;
            this.lblDateTo.Text = "Công thợ đến ngày:";
            this.lblDateTo.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // dpkDateFrom
            // 
            this.dpkDateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.dpkDateFrom.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dpkDateFrom.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkDateFrom.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dpkDateFrom.ButtonDropDown.Visible = true;
            this.dpkDateFrom.CustomFormat = "dd/MM/yyyy";
            this.dpkDateFrom.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dpkDateFrom.IsPopupCalendarOpen = false;
            this.dpkDateFrom.Location = new System.Drawing.Point(579, 12);
            // 
            // 
            // 
            this.dpkDateFrom.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dpkDateFrom.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkDateFrom.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dpkDateFrom.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dpkDateFrom.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dpkDateFrom.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dpkDateFrom.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dpkDateFrom.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dpkDateFrom.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dpkDateFrom.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dpkDateFrom.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkDateFrom.MonthCalendar.DisplayMonth = new System.DateTime(2015, 8, 1, 0, 0, 0, 0);
            this.dpkDateFrom.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dpkDateFrom.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dpkDateFrom.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dpkDateFrom.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dpkDateFrom.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dpkDateFrom.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dpkDateFrom.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkDateFrom.MonthCalendar.TodayButtonVisible = true;
            this.dpkDateFrom.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dpkDateFrom.Name = "dpkDateFrom";
            this.dpkDateFrom.Size = new System.Drawing.Size(152, 22);
            this.dpkDateFrom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dpkDateFrom.TabIndex = 2;
            // 
            // lblDateFrom
            // 
            this.lblDateFrom.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.lblDateFrom.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDateFrom.Location = new System.Drawing.Point(467, 12);
            this.lblDateFrom.Name = "lblDateFrom";
            this.lblDateFrom.Size = new System.Drawing.Size(106, 23);
            this.lblDateFrom.TabIndex = 1;
            this.lblDateFrom.Text = "Công thợ từ ngày:";
            this.lblDateFrom.TextAlignment = System.Drawing.StringAlignment.Far;
            // 
            // lblBangChamCongTho
            // 
            // 
            // 
            // 
            this.lblBangChamCongTho.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblBangChamCongTho.Font = new System.Drawing.Font("Times New Roman", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBangChamCongTho.Location = new System.Drawing.Point(6, 9);
            this.lblBangChamCongTho.Name = "lblBangChamCongTho";
            this.lblBangChamCongTho.Size = new System.Drawing.Size(244, 59);
            this.lblBangChamCongTho.TabIndex = 0;
            this.lblBangChamCongTho.Text = "BẢNG CHẤM CÔNG THỢ";
            // 
            // groupBoxContent
            // 
            this.groupBoxContent.Controls.Add(this.reportViewerCongTho);
            this.groupBoxContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxContent.Location = new System.Drawing.Point(0, 74);
            this.groupBoxContent.Name = "groupBoxContent";
            this.groupBoxContent.Size = new System.Drawing.Size(854, 583);
            this.groupBoxContent.TabIndex = 1;
            this.groupBoxContent.TabStop = false;
            this.groupBoxContent.Text = "Thông tin công thợ";
            // 
            // reportViewerCongTho
            // 
            this.reportViewerCongTho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerCongTho.Location = new System.Drawing.Point(3, 18);
            this.reportViewerCongTho.Name = "reportViewerCongTho";
            this.reportViewerCongTho.Size = new System.Drawing.Size(848, 562);
            this.reportViewerCongTho.TabIndex = 0;
            // 
            // frmBangCongTho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 657);
            this.Controls.Add(this.groupBoxContent);
            this.Controls.Add(this.panelHeader);
            this.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmBangCongTho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bảng theo dõi công thợ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmBangCongTho_FormClosing);
            this.Load += new System.EventHandler(this.frmBangCongTho_Load);
            this.panelHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dpkDateTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpkDateFrom)).EndInit();
            this.groupBoxContent.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelHeader;
        private System.Windows.Forms.GroupBox groupBoxContent;
        private DevComponents.DotNetBar.LabelX lblBangChamCongTho;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dpkDateTo;
        private DevComponents.DotNetBar.LabelX lblDateTo;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dpkDateFrom;
        private DevComponents.DotNetBar.LabelX lblDateFrom;
        private DevComponents.DotNetBar.ButtonX btnStatistic;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewerCongTho;
    }
}