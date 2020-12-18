namespace AutoCareV2._0.UserControls
{
    partial class UcXeConTrongKho
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcXeConTrongKho));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            this.btnXuatBaoCao = new DevComponents.DotNetBar.ButtonX();
            this.btnTatCa = new DevComponents.DotNetBar.ButtonX();
            this.txtSoMay = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSoKhung = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.cboMauXe = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboNhaCungCap = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cboTenXe = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.labelX7 = new DevComponents.DotNetBar.LabelX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cboKieuXe = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dgvDanhSachXe = new DevComponents.DotNetBar.Controls.DataGridViewX();
            this.XoaXe = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.MaXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenXe = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenLoaiXe = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.TenMauXe = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.NhaCungCap = new DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn();
            this.SoKhung = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.SoMay = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelEx1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachXe)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXuatBaoCao
            // 
            this.btnXuatBaoCao.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnXuatBaoCao.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnXuatBaoCao.Image = ((System.Drawing.Image)(resources.GetObject("btnXuatBaoCao.Image")));
            this.btnXuatBaoCao.ImageFixedSize = new System.Drawing.Size(30, 30);
            this.btnXuatBaoCao.Location = new System.Drawing.Point(627, 63);
            this.btnXuatBaoCao.Name = "btnXuatBaoCao";
            this.btnXuatBaoCao.Size = new System.Drawing.Size(110, 30);
            this.btnXuatBaoCao.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnXuatBaoCao.TabIndex = 4;
            this.btnXuatBaoCao.Text = "Xuất báo cáo";
            this.btnXuatBaoCao.Visible = false;
            this.btnXuatBaoCao.Click += new System.EventHandler(this.btnXuatBaoCao_Click);
            // 
            // btnTatCa
            // 
            this.btnTatCa.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTatCa.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTatCa.Image = ((System.Drawing.Image)(resources.GetObject("btnTatCa.Image")));
            this.btnTatCa.ImageFixedSize = new System.Drawing.Size(30, 30);
            this.btnTatCa.Location = new System.Drawing.Point(627, 27);
            this.btnTatCa.Name = "btnTatCa";
            this.btnTatCa.Size = new System.Drawing.Size(110, 30);
            this.btnTatCa.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTatCa.TabIndex = 4;
            this.btnTatCa.Text = "&Tất cả xe";
            this.btnTatCa.Click += new System.EventHandler(this.btnTatCa_Click);
            // 
            // txtSoMay
            // 
            // 
            // 
            // 
            this.txtSoMay.Border.Class = "TextBoxBorder";
            this.txtSoMay.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSoMay.Location = new System.Drawing.Point(407, 77);
            this.txtSoMay.Name = "txtSoMay";
            this.txtSoMay.Size = new System.Drawing.Size(191, 20);
            this.txtSoMay.TabIndex = 3;
            this.txtSoMay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSoMay_KeyDown);
            // 
            // txtSoKhung
            // 
            // 
            // 
            // 
            this.txtSoKhung.Border.Class = "TextBoxBorder";
            this.txtSoKhung.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSoKhung.Location = new System.Drawing.Point(407, 50);
            this.txtSoKhung.Name = "txtSoKhung";
            this.txtSoKhung.Size = new System.Drawing.Size(191, 20);
            this.txtSoKhung.TabIndex = 3;
            this.txtSoKhung.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSoKhung_KeyDown);
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(25, 71);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(46, 23);
            this.labelX3.TabIndex = 2;
            this.labelX3.Text = "Màu xe:";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(25, 45);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(46, 23);
            this.labelX2.TabIndex = 2;
            this.labelX2.Text = "Kiểu xe:";
            // 
            // labelX6
            // 
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Location = new System.Drawing.Point(319, 74);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(66, 23);
            this.labelX6.TabIndex = 2;
            this.labelX6.Text = "Số máy:";
            // 
            // labelX5
            // 
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Location = new System.Drawing.Point(319, 47);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(66, 23);
            this.labelX5.TabIndex = 2;
            this.labelX5.Text = "Số khung:";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(319, 19);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(82, 23);
            this.labelX4.TabIndex = 2;
            this.labelX4.Text = "Nhà cung cấp:";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(25, 19);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(46, 23);
            this.labelX1.TabIndex = 2;
            this.labelX1.Text = "Tên xe:";
            // 
            // cboMauXe
            // 
            this.cboMauXe.DisplayMember = "Text";
            this.cboMauXe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboMauXe.FormattingEnabled = true;
            this.cboMauXe.ItemHeight = 14;
            this.cboMauXe.Location = new System.Drawing.Point(89, 74);
            this.cboMauXe.Name = "cboMauXe";
            this.cboMauXe.Size = new System.Drawing.Size(200, 20);
            this.cboMauXe.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboMauXe.TabIndex = 1;
            this.cboMauXe.SelectionChangeCommitted += new System.EventHandler(this.cboMauXe_SelectionChangeCommitted);
            // 
            // cboNhaCungCap
            // 
            this.cboNhaCungCap.DisplayMember = "Text";
            this.cboNhaCungCap.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNhaCungCap.FormattingEnabled = true;
            this.cboNhaCungCap.ItemHeight = 14;
            this.cboNhaCungCap.Location = new System.Drawing.Point(407, 22);
            this.cboNhaCungCap.Name = "cboNhaCungCap";
            this.cboNhaCungCap.Size = new System.Drawing.Size(191, 20);
            this.cboNhaCungCap.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboNhaCungCap.TabIndex = 1;
            // 
            // cboTenXe
            // 
            this.cboTenXe.DisplayMember = "Text";
            this.cboTenXe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTenXe.FormattingEnabled = true;
            this.cboTenXe.ItemHeight = 14;
            this.cboTenXe.Location = new System.Drawing.Point(89, 22);
            this.cboTenXe.Name = "cboTenXe";
            this.cboTenXe.Size = new System.Drawing.Size(200, 20);
            this.cboTenXe.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboTenXe.TabIndex = 1;
            this.cboTenXe.SelectionChangeCommitted += new System.EventHandler(this.cboTenXe_SelectionChangeCommitted);
            // 
            // panelEx1
            // 
            this.panelEx1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.labelX7);
            this.panelEx1.Controls.Add(this.groupBox2);
            this.panelEx1.Controls.Add(this.groupBox1);
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1142, 541);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 1;
            // 
            // labelX7
            // 
            this.labelX7.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.labelX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX7.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX7.Location = new System.Drawing.Point(6, 17);
            this.labelX7.Name = "labelX7";
            this.labelX7.Size = new System.Drawing.Size(367, 107);
            this.labelX7.TabIndex = 7;
            this.labelX7.Text = "XE CÒN TRONG KHO";
            this.labelX7.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.labelX1);
            this.groupBox2.Controls.Add(this.cboTenXe);
            this.groupBox2.Controls.Add(this.btnXuatBaoCao);
            this.groupBox2.Controls.Add(this.cboNhaCungCap);
            this.groupBox2.Controls.Add(this.btnTatCa);
            this.groupBox2.Controls.Add(this.cboKieuXe);
            this.groupBox2.Controls.Add(this.txtSoMay);
            this.groupBox2.Controls.Add(this.cboMauXe);
            this.groupBox2.Controls.Add(this.txtSoKhung);
            this.groupBox2.Controls.Add(this.labelX4);
            this.groupBox2.Controls.Add(this.labelX3);
            this.groupBox2.Controls.Add(this.labelX5);
            this.groupBox2.Controls.Add(this.labelX2);
            this.groupBox2.Controls.Add(this.labelX6);
            this.groupBox2.Location = new System.Drawing.Point(379, 17);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(750, 107);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin";
            // 
            // cboKieuXe
            // 
            this.cboKieuXe.DisplayMember = "Text";
            this.cboKieuXe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKieuXe.FormattingEnabled = true;
            this.cboKieuXe.ItemHeight = 14;
            this.cboKieuXe.Location = new System.Drawing.Point(89, 48);
            this.cboKieuXe.Name = "cboKieuXe";
            this.cboKieuXe.Size = new System.Drawing.Size(200, 20);
            this.cboKieuXe.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboKieuXe.TabIndex = 1;
            this.cboKieuXe.SelectionChangeCommitted += new System.EventHandler(this.cboKieuXe_SelectionChangeCommitted);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.dgvDanhSachXe);
            this.groupBox1.Location = new System.Drawing.Point(3, 130);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1126, 408);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            // 
            // dgvDanhSachXe
            // 
            this.dgvDanhSachXe.AllowUserToAddRows = false;
            this.dgvDanhSachXe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDanhSachXe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDanhSachXe.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.XoaXe,
            this.MaXe,
            this.TenXe,
            this.TenLoaiXe,
            this.TenMauXe,
            this.NhaCungCap,
            this.SoKhung,
            this.SoMay});
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDanhSachXe.DefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDanhSachXe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDanhSachXe.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(215)))), ((int)(((byte)(229)))));
            this.dgvDanhSachXe.Location = new System.Drawing.Point(3, 16);
            this.dgvDanhSachXe.Name = "dgvDanhSachXe";
            this.dgvDanhSachXe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDanhSachXe.Size = new System.Drawing.Size(1120, 389);
            this.dgvDanhSachXe.TabIndex = 0;
            this.dgvDanhSachXe.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachXe_CellClick);
            this.dgvDanhSachXe.CellDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvDanhSachXe_CellDoubleClick);
            // 
            // XoaXe
            // 
            this.XoaXe.FillWeight = 15F;
            this.XoaXe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.XoaXe.HeaderText = "";
            this.XoaXe.Image = ((System.Drawing.Image)(resources.GetObject("XoaXe.Image")));
            this.XoaXe.Name = "XoaXe";
            this.XoaXe.Text = null;
            // 
            // MaXe
            // 
            this.MaXe.DataPropertyName = "IdKey";
            this.MaXe.HeaderText = "Mã Xe";
            this.MaXe.Name = "MaXe";
            this.MaXe.ReadOnly = true;
            // 
            // TenXe
            // 
            this.TenXe.DataPropertyName = "TenXe";
            this.TenXe.HeaderText = "Tên xe";
            this.TenXe.Name = "TenXe";
            this.TenXe.ReadOnly = true;
            // 
            // TenLoaiXe
            // 
            this.TenLoaiXe.DataPropertyName = "IdLoaiXe";
            this.TenLoaiXe.DisplayMember = "Text";
            this.TenLoaiXe.DropDownHeight = 106;
            this.TenLoaiXe.DropDownWidth = 121;
            this.TenLoaiXe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TenLoaiXe.HeaderText = "Loại Xe";
            this.TenLoaiXe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TenLoaiXe.IntegralHeight = false;
            this.TenLoaiXe.ItemHeight = 15;
            this.TenLoaiXe.Name = "TenLoaiXe";
            this.TenLoaiXe.ReadOnly = true;
            this.TenLoaiXe.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TenLoaiXe.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // TenMauXe
            // 
            this.TenMauXe.DataPropertyName = "IdMauXe";
            this.TenMauXe.DisplayMember = "Text";
            this.TenMauXe.DropDownHeight = 106;
            this.TenMauXe.DropDownWidth = 121;
            this.TenMauXe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TenMauXe.HeaderText = "Màu Xe";
            this.TenMauXe.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.TenMauXe.IntegralHeight = false;
            this.TenMauXe.ItemHeight = 15;
            this.TenMauXe.Name = "TenMauXe";
            this.TenMauXe.ReadOnly = true;
            this.TenMauXe.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.TenMauXe.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // NhaCungCap
            // 
            this.NhaCungCap.DataPropertyName = "IdNhaCungCap";
            this.NhaCungCap.DisplayMember = "Text";
            this.NhaCungCap.DropDownHeight = 106;
            this.NhaCungCap.DropDownWidth = 121;
            this.NhaCungCap.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NhaCungCap.HeaderText = "Nhà Cung Cấp";
            this.NhaCungCap.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.NhaCungCap.IntegralHeight = false;
            this.NhaCungCap.ItemHeight = 15;
            this.NhaCungCap.Name = "NhaCungCap";
            this.NhaCungCap.ReadOnly = true;
            this.NhaCungCap.Resizable = System.Windows.Forms.DataGridViewTriState.True;
            this.NhaCungCap.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // SoKhung
            // 
            this.SoKhung.DataPropertyName = "SoKhung";
            this.SoKhung.HeaderText = "Số Khung";
            this.SoKhung.Name = "SoKhung";
            this.SoKhung.ReadOnly = true;
            // 
            // SoMay
            // 
            this.SoMay.DataPropertyName = "SoMay";
            this.SoMay.HeaderText = "Số Máy";
            this.SoMay.Name = "SoMay";
            this.SoMay.ReadOnly = true;
            // 
            // UcXeConTrongKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelEx1);
            this.Name = "UcXeConTrongKho";
            this.Size = new System.Drawing.Size(1142, 541);
            this.Load += new System.EventHandler(this.UcXeConTrongKho_Load);
            this.panelEx1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDanhSachXe)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnXuatBaoCao;
        private DevComponents.DotNetBar.ButtonX btnTatCa;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSoMay;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSoKhung;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboMauXe;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboNhaCungCap;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboTenXe;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboKieuXe;
        private DevComponents.DotNetBar.Controls.DataGridViewX dgvDanhSachXe;
        private DevComponents.DotNetBar.LabelX labelX7;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn XoaXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaXe;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenXe;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn TenLoaiXe;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn TenMauXe;
        private DevComponents.DotNetBar.Controls.DataGridViewComboBoxExColumn NhaCungCap;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoKhung;
        private System.Windows.Forms.DataGridViewTextBoxColumn SoMay;
    }
}
