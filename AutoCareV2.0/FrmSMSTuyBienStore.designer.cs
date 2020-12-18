namespace AutoCareV2._0
{
    partial class FrmSMSTuyBienStore
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Huy = new DevComponents.DotNetBar.ButtonX();
            this.btn_Moi = new DevComponents.DotNetBar.ButtonX();
            this.grv_LichDangNhan = new System.Windows.Forms.DataGridView();
            this.IdSMSTuyBien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SMS = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Countmes = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.smsType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.GioNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayNhan = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.NgayDatLich = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCongTy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.grv_LichDaNhan = new System.Windows.Forms.DataGridView();
            this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn6 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.dataGridViewTextBoxColumn8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btn_Sua = new DevComponents.DotNetBar.ButtonX();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grv_LichDangNhan)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grv_LichDaNhan)).BeginInit();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Sua);
            this.groupBox1.Controls.Add(this.btn_Huy);
            this.groupBox1.Controls.Add(this.btn_Moi);
            this.groupBox1.Controls.Add(this.grv_LichDangNhan);
            this.groupBox1.Location = new System.Drawing.Point(9, 10);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(795, 175);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Lịch đang nhắn";
            // 
            // btn_Huy
            // 
            this.btn_Huy.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Huy.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Huy.Location = new System.Drawing.Point(234, 18);
            this.btn_Huy.Name = "btn_Huy";
            this.btn_Huy.Size = new System.Drawing.Size(95, 30);
            this.btn_Huy.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Huy.TabIndex = 4;
            this.btn_Huy.Text = "&Hủy lịch";
            this.btn_Huy.Click += new System.EventHandler(this.btn_Huy_Click);
            // 
            // btn_Moi
            // 
            this.btn_Moi.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Moi.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Moi.Location = new System.Drawing.Point(453, 18);
            this.btn_Moi.Name = "btn_Moi";
            this.btn_Moi.Size = new System.Drawing.Size(95, 30);
            this.btn_Moi.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Moi.TabIndex = 3;
            this.btn_Moi.Text = "&Làm mới";
            this.btn_Moi.Click += new System.EventHandler(this.btn_Moi_Click);
            // 
            // grv_LichDangNhan
            // 
            this.grv_LichDangNhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grv_LichDangNhan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.IdSMSTuyBien,
            this.SMS,
            this.Countmes,
            this.smsType,
            this.GioNhan,
            this.NgayNhan,
            this.NgayDatLich,
            this.IdCongTy});
            this.grv_LichDangNhan.Location = new System.Drawing.Point(6, 58);
            this.grv_LichDangNhan.Name = "grv_LichDangNhan";
            this.grv_LichDangNhan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grv_LichDangNhan.Size = new System.Drawing.Size(782, 107);
            this.grv_LichDangNhan.TabIndex = 0;
            this.grv_LichDangNhan.SelectionChanged += new System.EventHandler(this.grv_LichDangNhan_SelectionChanged);
            // 
            // IdSMSTuyBien
            // 
            this.IdSMSTuyBien.DataPropertyName = "IdSMSTuyBien";
            this.IdSMSTuyBien.HeaderText = "Mã số";
            this.IdSMSTuyBien.Name = "IdSMSTuyBien";
            this.IdSMSTuyBien.ReadOnly = true;
            // 
            // SMS
            // 
            this.SMS.DataPropertyName = "SMS";
            this.SMS.HeaderText = "Nội dung";
            this.SMS.Name = "SMS";
            this.SMS.ReadOnly = true;
            // 
            // Countmes
            // 
            this.Countmes.DataPropertyName = "Countmes";
            this.Countmes.HeaderText = "Bản tin";
            this.Countmes.Name = "Countmes";
            this.Countmes.ReadOnly = true;
            // 
            // smsType
            // 
            this.smsType.DataPropertyName = "smsType";
            this.smsType.HeaderText = "Loại tin";
            this.smsType.Name = "smsType";
            this.smsType.ReadOnly = true;
            // 
            // GioNhan
            // 
            this.GioNhan.DataPropertyName = "GioNhan";
            this.GioNhan.HeaderText = "Giờ nhắn";
            this.GioNhan.Name = "GioNhan";
            this.GioNhan.ReadOnly = true;
            // 
            // NgayNhan
            // 
            this.NgayNhan.DataPropertyName = "NgayNhan";
            this.NgayNhan.HeaderText = "Ngày nhắn";
            this.NgayNhan.Name = "NgayNhan";
            this.NgayNhan.ReadOnly = true;
            // 
            // NgayDatLich
            // 
            this.NgayDatLich.DataPropertyName = "NgayDatLich";
            this.NgayDatLich.HeaderText = "Ngày đặt lịch";
            this.NgayDatLich.Name = "NgayDatLich";
            this.NgayDatLich.ReadOnly = true;
            // 
            // IdCongTy
            // 
            this.IdCongTy.DataPropertyName = "IdCongTy";
            this.IdCongTy.HeaderText = "Mã công ty";
            this.IdCongTy.Name = "IdCongTy";
            this.IdCongTy.ReadOnly = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.grv_LichDaNhan);
            this.groupBox2.Location = new System.Drawing.Point(9, 197);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(795, 235);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Lịch đã nhắn";
            // 
            // grv_LichDaNhan
            // 
            this.grv_LichDaNhan.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grv_LichDaNhan.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.dataGridViewTextBoxColumn1,
            this.dataGridViewTextBoxColumn2,
            this.dataGridViewTextBoxColumn3,
            this.dataGridViewTextBoxColumn4,
            this.dataGridViewTextBoxColumn5,
            this.dataGridViewTextBoxColumn6,
            this.dataGridViewTextBoxColumn7,
            this.dataGridViewTextBoxColumn8});
            this.grv_LichDaNhan.Location = new System.Drawing.Point(7, 19);
            this.grv_LichDaNhan.Name = "grv_LichDaNhan";
            this.grv_LichDaNhan.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grv_LichDaNhan.Size = new System.Drawing.Size(782, 210);
            this.grv_LichDaNhan.TabIndex = 0;
            this.grv_LichDaNhan.SelectionChanged += new System.EventHandler(this.grv_LichDangNhan_SelectionChanged);
            // 
            // dataGridViewTextBoxColumn1
            // 
            this.dataGridViewTextBoxColumn1.DataPropertyName = "IdSMSTuyBien";
            this.dataGridViewTextBoxColumn1.HeaderText = "Mã số";
            this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
            this.dataGridViewTextBoxColumn1.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn2
            // 
            this.dataGridViewTextBoxColumn2.DataPropertyName = "SMS";
            this.dataGridViewTextBoxColumn2.HeaderText = "Nội dung";
            this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
            this.dataGridViewTextBoxColumn2.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn3
            // 
            this.dataGridViewTextBoxColumn3.DataPropertyName = "Countmes";
            this.dataGridViewTextBoxColumn3.HeaderText = "Bản tin";
            this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
            this.dataGridViewTextBoxColumn3.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn4
            // 
            this.dataGridViewTextBoxColumn4.DataPropertyName = "smsType";
            this.dataGridViewTextBoxColumn4.HeaderText = "Loại tin";
            this.dataGridViewTextBoxColumn4.Name = "dataGridViewTextBoxColumn4";
            this.dataGridViewTextBoxColumn4.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn5
            // 
            this.dataGridViewTextBoxColumn5.DataPropertyName = "GioNhan";
            this.dataGridViewTextBoxColumn5.HeaderText = "Giờ nhắn";
            this.dataGridViewTextBoxColumn5.Name = "dataGridViewTextBoxColumn5";
            this.dataGridViewTextBoxColumn5.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn6
            // 
            this.dataGridViewTextBoxColumn6.DataPropertyName = "NgayNhan";
            this.dataGridViewTextBoxColumn6.HeaderText = "Ngày nhắn";
            this.dataGridViewTextBoxColumn6.Name = "dataGridViewTextBoxColumn6";
            this.dataGridViewTextBoxColumn6.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn7
            // 
            this.dataGridViewTextBoxColumn7.DataPropertyName = "NgayDatLich";
            this.dataGridViewTextBoxColumn7.HeaderText = "Ngày đặt lịch";
            this.dataGridViewTextBoxColumn7.Name = "dataGridViewTextBoxColumn7";
            this.dataGridViewTextBoxColumn7.ReadOnly = true;
            // 
            // dataGridViewTextBoxColumn8
            // 
            this.dataGridViewTextBoxColumn8.DataPropertyName = "IdCongTy";
            this.dataGridViewTextBoxColumn8.HeaderText = "Mã công ty";
            this.dataGridViewTextBoxColumn8.Name = "dataGridViewTextBoxColumn8";
            this.dataGridViewTextBoxColumn8.ReadOnly = true;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.groupBox1);
            this.panelEx1.Controls.Add(this.groupBox2);
            this.panelEx1.Location = new System.Drawing.Point(3, 2);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(814, 447);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            this.panelEx1.Text = "panelEx1";
            // 
            // btn_Sua
            // 
            this.btn_Sua.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Sua.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Sua.Location = new System.Drawing.Point(343, 18);
            this.btn_Sua.Name = "btn_Sua";
            this.btn_Sua.Size = new System.Drawing.Size(95, 30);
            this.btn_Sua.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Sua.TabIndex = 5;
            this.btn_Sua.Text = "&Sửa lịch";
            this.btn_Sua.Click += new System.EventHandler(this.btn_Sua_Click);
            // 
            // FrmSMSTuyBienStore
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(819, 453);
            this.Controls.Add(this.panelEx1);
            this.Name = "FrmSMSTuyBienStore";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Lịch sử nhắn tin tùy biến";
            this.Load += new System.EventHandler(this.FrmSMSTuyBienStore_Load);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grv_LichDangNhan)).EndInit();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grv_LichDaNhan)).EndInit();
            this.panelEx1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView grv_LichDangNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdSMSTuyBien;
        private System.Windows.Forms.DataGridViewTextBoxColumn SMS;
        private System.Windows.Forms.DataGridViewTextBoxColumn Countmes;
        private System.Windows.Forms.DataGridViewTextBoxColumn smsType;
        private System.Windows.Forms.DataGridViewTextBoxColumn GioNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn NgayDatLich;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCongTy;
        private System.Windows.Forms.DataGridView grv_LichDaNhan;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn4;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn5;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn6;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn7;
        private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn8;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btn_Huy;
        private DevComponents.DotNetBar.ButtonX btn_Moi;
        private DevComponents.DotNetBar.ButtonX btn_Sua;
    }
}