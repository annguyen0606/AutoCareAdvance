namespace AutoCareV2._0
{
    partial class FrmCapNhatXeDaBan
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmCapNhatXeDaBan));
            this.pnHead = new System.Windows.Forms.Panel();
            this.pnFooter = new System.Windows.Forms.Panel();
            this.gbThongTinXe = new System.Windows.Forms.GroupBox();
            this.lblHeadContent = new DevComponents.DotNetBar.LabelX();
            this.btnExit = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.lblTenXe = new DevComponents.DotNetBar.LabelX();
            this.lblBienSo = new DevComponents.DotNetBar.LabelX();
            this.lblNgayBan = new DevComponents.DotNetBar.LabelX();
            this.lblMauXe = new DevComponents.DotNetBar.LabelX();
            this.lblSoKhung = new DevComponents.DotNetBar.LabelX();
            this.lblSoMay = new DevComponents.DotNetBar.LabelX();
            this.txtTenXe = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtBienSo = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtMauXe = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSoKhung = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSoMay = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.dpkNgayBan = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.pnHead.SuspendLayout();
            this.pnFooter.SuspendLayout();
            this.gbThongTinXe.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dpkNgayBan)).BeginInit();
            this.SuspendLayout();
            // 
            // pnHead
            // 
            this.pnHead.Controls.Add(this.lblHeadContent);
            this.pnHead.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnHead.Location = new System.Drawing.Point(0, 0);
            this.pnHead.Name = "pnHead";
            this.pnHead.Size = new System.Drawing.Size(443, 43);
            this.pnHead.TabIndex = 0;
            // 
            // pnFooter
            // 
            this.pnFooter.Controls.Add(this.btnSave);
            this.pnFooter.Controls.Add(this.btnExit);
            this.pnFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnFooter.Location = new System.Drawing.Point(0, 148);
            this.pnFooter.Name = "pnFooter";
            this.pnFooter.Size = new System.Drawing.Size(443, 35);
            this.pnFooter.TabIndex = 1;
            // 
            // gbThongTinXe
            // 
            this.gbThongTinXe.Controls.Add(this.dpkNgayBan);
            this.gbThongTinXe.Controls.Add(this.txtSoMay);
            this.gbThongTinXe.Controls.Add(this.txtSoKhung);
            this.gbThongTinXe.Controls.Add(this.txtMauXe);
            this.gbThongTinXe.Controls.Add(this.txtBienSo);
            this.gbThongTinXe.Controls.Add(this.txtTenXe);
            this.gbThongTinXe.Controls.Add(this.lblSoMay);
            this.gbThongTinXe.Controls.Add(this.lblSoKhung);
            this.gbThongTinXe.Controls.Add(this.lblMauXe);
            this.gbThongTinXe.Controls.Add(this.lblNgayBan);
            this.gbThongTinXe.Controls.Add(this.lblBienSo);
            this.gbThongTinXe.Controls.Add(this.lblTenXe);
            this.gbThongTinXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTinXe.Location = new System.Drawing.Point(0, 43);
            this.gbThongTinXe.Name = "gbThongTinXe";
            this.gbThongTinXe.Size = new System.Drawing.Size(443, 105);
            this.gbThongTinXe.TabIndex = 0;
            this.gbThongTinXe.TabStop = false;
            this.gbThongTinXe.Text = "Thông tin xe";
            // 
            // lblHeadContent
            // 
            // 
            // 
            // 
            this.lblHeadContent.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblHeadContent.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeadContent.Location = new System.Drawing.Point(78, 10);
            this.lblHeadContent.Name = "lblHeadContent";
            this.lblHeadContent.Size = new System.Drawing.Size(276, 23);
            this.lblHeadContent.TabIndex = 0;
            this.lblHeadContent.Text = "CẬP NHẬT THÔNG TIN XE ĐÃ BÁN";
            // 
            // btnExit
            // 
            this.btnExit.Location = new System.Drawing.Point(358, 6);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(75, 23);
            this.btnExit.TabIndex = 0;
            this.btnExit.Text = "Thoát";
            this.btnExit.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(280, 6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu lại";
            this.btnSave.UseVisualStyleBackColor = true;
            // 
            // lblTenXe
            // 
            // 
            // 
            // 
            this.lblTenXe.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblTenXe.Location = new System.Drawing.Point(10, 23);
            this.lblTenXe.Name = "lblTenXe";
            this.lblTenXe.Size = new System.Drawing.Size(55, 23);
            this.lblTenXe.TabIndex = 0;
            this.lblTenXe.Text = "Tên xe:";
            // 
            // lblBienSo
            // 
            // 
            // 
            // 
            this.lblBienSo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblBienSo.Location = new System.Drawing.Point(10, 48);
            this.lblBienSo.Name = "lblBienSo";
            this.lblBienSo.Size = new System.Drawing.Size(55, 23);
            this.lblBienSo.TabIndex = 0;
            this.lblBienSo.Text = "Biển số:";
            // 
            // lblNgayBan
            // 
            // 
            // 
            // 
            this.lblNgayBan.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblNgayBan.Location = new System.Drawing.Point(10, 73);
            this.lblNgayBan.Name = "lblNgayBan";
            this.lblNgayBan.Size = new System.Drawing.Size(55, 23);
            this.lblNgayBan.TabIndex = 0;
            this.lblNgayBan.Text = "Ngày bán:";
            // 
            // lblMauXe
            // 
            // 
            // 
            // 
            this.lblMauXe.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblMauXe.Location = new System.Drawing.Point(229, 23);
            this.lblMauXe.Name = "lblMauXe";
            this.lblMauXe.Size = new System.Drawing.Size(52, 23);
            this.lblMauXe.TabIndex = 0;
            this.lblMauXe.Text = "Màu xe:";
            // 
            // lblSoKhung
            // 
            // 
            // 
            // 
            this.lblSoKhung.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSoKhung.Location = new System.Drawing.Point(229, 48);
            this.lblSoKhung.Name = "lblSoKhung";
            this.lblSoKhung.Size = new System.Drawing.Size(52, 23);
            this.lblSoKhung.TabIndex = 0;
            this.lblSoKhung.Text = "Số khung:";
            // 
            // lblSoMay
            // 
            // 
            // 
            // 
            this.lblSoMay.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblSoMay.Location = new System.Drawing.Point(229, 73);
            this.lblSoMay.Name = "lblSoMay";
            this.lblSoMay.Size = new System.Drawing.Size(52, 23);
            this.lblSoMay.TabIndex = 0;
            this.lblSoMay.Text = "Số máy:";
            // 
            // txtTenXe
            // 
            // 
            // 
            // 
            this.txtTenXe.Border.Class = "TextBoxBorder";
            this.txtTenXe.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtTenXe.Location = new System.Drawing.Point(71, 24);
            this.txtTenXe.Name = "txtTenXe";
            this.txtTenXe.Size = new System.Drawing.Size(146, 20);
            this.txtTenXe.TabIndex = 0;
            // 
            // txtBienSo
            // 
            // 
            // 
            // 
            this.txtBienSo.Border.Class = "TextBoxBorder";
            this.txtBienSo.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBienSo.Location = new System.Drawing.Point(71, 49);
            this.txtBienSo.Name = "txtBienSo";
            this.txtBienSo.Size = new System.Drawing.Size(146, 20);
            this.txtBienSo.TabIndex = 1;
            // 
            // txtMauXe
            // 
            // 
            // 
            // 
            this.txtMauXe.Border.Class = "TextBoxBorder";
            this.txtMauXe.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMauXe.Location = new System.Drawing.Point(287, 24);
            this.txtMauXe.Name = "txtMauXe";
            this.txtMauXe.Size = new System.Drawing.Size(146, 20);
            this.txtMauXe.TabIndex = 3;
            // 
            // txtSoKhung
            // 
            // 
            // 
            // 
            this.txtSoKhung.Border.Class = "TextBoxBorder";
            this.txtSoKhung.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSoKhung.Location = new System.Drawing.Point(287, 49);
            this.txtSoKhung.Name = "txtSoKhung";
            this.txtSoKhung.Size = new System.Drawing.Size(146, 20);
            this.txtSoKhung.TabIndex = 4;
            // 
            // txtSoMay
            // 
            // 
            // 
            // 
            this.txtSoMay.Border.Class = "TextBoxBorder";
            this.txtSoMay.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSoMay.Location = new System.Drawing.Point(287, 74);
            this.txtSoMay.Name = "txtSoMay";
            this.txtSoMay.Size = new System.Drawing.Size(146, 20);
            this.txtSoMay.TabIndex = 5;
            // 
            // dpkNgayBan
            // 
            // 
            // 
            // 
            this.dpkNgayBan.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dpkNgayBan.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkNgayBan.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dpkNgayBan.ButtonDropDown.Visible = true;
            this.dpkNgayBan.IsPopupCalendarOpen = false;
            this.dpkNgayBan.Location = new System.Drawing.Point(71, 74);
            // 
            // 
            // 
            this.dpkNgayBan.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dpkNgayBan.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkNgayBan.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dpkNgayBan.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dpkNgayBan.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dpkNgayBan.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dpkNgayBan.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dpkNgayBan.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dpkNgayBan.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dpkNgayBan.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dpkNgayBan.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkNgayBan.MonthCalendar.DisplayMonth = new System.DateTime(2016, 3, 1, 0, 0, 0, 0);
            this.dpkNgayBan.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dpkNgayBan.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dpkNgayBan.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dpkNgayBan.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dpkNgayBan.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dpkNgayBan.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dpkNgayBan.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dpkNgayBan.MonthCalendar.TodayButtonVisible = true;
            this.dpkNgayBan.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dpkNgayBan.Name = "dpkNgayBan";
            this.dpkNgayBan.Size = new System.Drawing.Size(146, 20);
            this.dpkNgayBan.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dpkNgayBan.TabIndex = 2;
            // 
            // FrmCapNhatXeDaBan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(443, 183);
            this.Controls.Add(this.gbThongTinXe);
            this.Controls.Add(this.pnFooter);
            this.Controls.Add(this.pnHead);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmCapNhatXeDaBan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật thông tin xe đã bán";
            this.pnHead.ResumeLayout(false);
            this.pnFooter.ResumeLayout(false);
            this.gbThongTinXe.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dpkNgayBan)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnHead;
        private System.Windows.Forms.Panel pnFooter;
        private System.Windows.Forms.GroupBox gbThongTinXe;
        private DevComponents.DotNetBar.LabelX lblHeadContent;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnExit;
        private DevComponents.DotNetBar.LabelX lblTenXe;
        private DevComponents.DotNetBar.LabelX lblSoMay;
        private DevComponents.DotNetBar.LabelX lblSoKhung;
        private DevComponents.DotNetBar.LabelX lblMauXe;
        private DevComponents.DotNetBar.LabelX lblNgayBan;
        private DevComponents.DotNetBar.LabelX lblBienSo;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dpkNgayBan;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSoMay;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSoKhung;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMauXe;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBienSo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtTenXe;
    }
}