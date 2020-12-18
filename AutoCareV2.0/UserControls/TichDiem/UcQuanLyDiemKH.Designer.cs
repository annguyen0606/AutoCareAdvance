namespace AutoCareV2._0.UserControls.TichDiem
{
    partial class UcQuanLyDiemKH
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UcQuanLyDiemKH));
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.cbb_SoKHpage = new System.Windows.Forms.ComboBox();
            this.label18 = new System.Windows.Forms.Label();
            this.lblTotalPage = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtCurrentPage = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbl_SoKH = new System.Windows.Forms.Label();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.id_TD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DienThoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCongty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tongDiem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.diemConLai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.detail = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Export = new System.Windows.Forms.Button();
            this.btn_Search = new System.Windows.Forms.Button();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.dataGridViewButtonXColumn1 = new DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_Next = new System.Windows.Forms.Button();
            this.btn_Pre = new System.Windows.Forms.Button();
            this.NapTienToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStripThongTinKhachHang = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.contextMenuStripThongTinKhachHang.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelEx1
            // 
            this.panelEx1.AutoScroll = true;
            this.panelEx1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(941, 537);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.BackColor2.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 26;
            // 
            // cbb_SoKHpage
            // 
            this.cbb_SoKHpage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cbb_SoKHpage.FormattingEnabled = true;
            this.cbb_SoKHpage.Location = new System.Drawing.Point(869, 482);
            this.cbb_SoKHpage.Name = "cbb_SoKHpage";
            this.cbb_SoKHpage.Size = new System.Drawing.Size(58, 21);
            this.cbb_SoKHpage.TabIndex = 30;
            this.cbb_SoKHpage.SelectedIndexChanged += new System.EventHandler(this.cbb_SoKHpage_SelectedIndexChanged);
            // 
            // label18
            // 
            this.label18.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(768, 485);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(96, 13);
            this.label18.TabIndex = 29;
            this.label18.Text = "Chọn Số KH/Page";
            // 
            // lblTotalPage
            // 
            this.lblTotalPage.AutoSize = true;
            this.lblTotalPage.Location = new System.Drawing.Point(117, 487);
            this.lblTotalPage.Name = "lblTotalPage";
            this.lblTotalPage.Size = new System.Drawing.Size(13, 13);
            this.lblTotalPage.TabIndex = 33;
            this.lblTotalPage.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(100, 487);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 13);
            this.label6.TabIndex = 32;
            this.label6.Text = "/";
            // 
            // txtCurrentPage
            // 
            this.txtCurrentPage.Location = new System.Drawing.Point(55, 483);
            this.txtCurrentPage.Name = "txtCurrentPage";
            this.txtCurrentPage.Size = new System.Drawing.Size(39, 20);
            this.txtCurrentPage.TabIndex = 31;
            this.txtCurrentPage.Text = "1";
            this.txtCurrentPage.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.AutoSize = true;
            this.groupBox1.Controls.Add(this.lbl_SoKH);
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Location = new System.Drawing.Point(14, 90);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(912, 387);
            this.groupBox1.TabIndex = 38;
            this.groupBox1.TabStop = false;
            // 
            // lbl_SoKH
            // 
            this.lbl_SoKH.AutoSize = true;
            this.lbl_SoKH.Location = new System.Drawing.Point(5, 0);
            this.lbl_SoKH.Name = "lbl_SoKH";
            this.lbl_SoKH.Size = new System.Drawing.Size(0, 13);
            this.lbl_SoKH.TabIndex = 13;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.id_TD,
            this.TenKH,
            this.DienThoai,
            this.IdCongty,
            this.tongDiem,
            this.diemConLai,
            this.ngayTao,
            this.detail});
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.Location = new System.Drawing.Point(6, 7);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(900, 360);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // id_TD
            // 
            this.id_TD.DataPropertyName = "id_TD";
            this.id_TD.HeaderText = "id";
            this.id_TD.Name = "id_TD";
            this.id_TD.ReadOnly = true;
            this.id_TD.Visible = false;
            // 
            // TenKH
            // 
            this.TenKH.DataPropertyName = "TenKH";
            this.TenKH.FillWeight = 127.2494F;
            this.TenKH.HeaderText = "Tên khách hàng";
            this.TenKH.Name = "TenKH";
            this.TenKH.ReadOnly = true;
            // 
            // DienThoai
            // 
            this.DienThoai.DataPropertyName = "DienThoai";
            this.DienThoai.FillWeight = 127.2494F;
            this.DienThoai.HeaderText = "Số điện thoại";
            this.DienThoai.Name = "DienThoai";
            this.DienThoai.ReadOnly = true;
            // 
            // IdCongty
            // 
            this.IdCongty.DataPropertyName = "IdCongty";
            this.IdCongty.FillWeight = 127.2494F;
            this.IdCongty.HeaderText = "Mã công ty";
            this.IdCongty.Name = "IdCongty";
            this.IdCongty.ReadOnly = true;
            // 
            // tongDiem
            // 
            this.tongDiem.DataPropertyName = "tongDiem";
            this.tongDiem.FillWeight = 127.2494F;
            this.tongDiem.HeaderText = "Tổng điểm";
            this.tongDiem.Name = "tongDiem";
            this.tongDiem.ReadOnly = true;
            // 
            // diemConLai
            // 
            this.diemConLai.DataPropertyName = "diemConLai";
            this.diemConLai.FillWeight = 127.2494F;
            this.diemConLai.HeaderText = "Điểm hiện tại";
            this.diemConLai.Name = "diemConLai";
            this.diemConLai.ReadOnly = true;
            // 
            // ngayTao
            // 
            this.ngayTao.DataPropertyName = "ngayTao";
            this.ngayTao.FillWeight = 127.2494F;
            this.ngayTao.HeaderText = "Ngày tạo";
            this.ngayTao.Name = "ngayTao";
            this.ngayTao.ReadOnly = true;
            // 
            // detail
            // 
            this.detail.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.detail.DataPropertyName = "detail";
            this.detail.FillWeight = 36.80998F;
            this.detail.HeaderText = "Chi tiết";
            this.detail.Image = ((System.Drawing.Image)(resources.GetObject("detail.Image")));
            this.detail.Name = "detail";
            this.detail.ReadOnly = true;
            this.detail.Text = null;
            this.detail.ToolTipText = "Xem chi tiết";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.AutoSize = true;
            this.groupBox2.BackColor = System.Drawing.Color.Transparent;
            this.groupBox2.Controls.Add(this.btn_Export);
            this.groupBox2.Controls.Add(this.btn_Search);
            this.groupBox2.Controls.Add(this.txtSDT);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtTenKH);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Location = new System.Drawing.Point(14, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(913, 79);
            this.groupBox2.TabIndex = 39;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Thông tin thẻ khách hàng";
            // 
            // btn_Export
            // 
            this.btn_Export.Location = new System.Drawing.Point(423, 37);
            this.btn_Export.Name = "btn_Export";
            this.btn_Export.Size = new System.Drawing.Size(75, 23);
            this.btn_Export.TabIndex = 13;
            this.btn_Export.Text = "Export";
            this.btn_Export.UseVisualStyleBackColor = true;
            this.btn_Export.Click += new System.EventHandler(this.btn_Export_Click);
            // 
            // btn_Search
            // 
            this.btn_Search.BackColor = System.Drawing.Color.Transparent;
            this.btn_Search.Location = new System.Drawing.Point(342, 37);
            this.btn_Search.Name = "btn_Search";
            this.btn_Search.Size = new System.Drawing.Size(75, 23);
            this.btn_Search.TabIndex = 12;
            this.btn_Search.Text = "Tìm kiếm";
            this.btn_Search.UseVisualStyleBackColor = false;
            this.btn_Search.Click += new System.EventHandler(this.btn_Search_Click);
            // 
            // txtSDT
            // 
            this.txtSDT.Location = new System.Drawing.Point(188, 39);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.Size = new System.Drawing.Size(135, 20);
            this.txtSDT.TabIndex = 11;
            this.txtSDT.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(185, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Số điện thoại";
            // 
            // txtTenKH
            // 
            this.txtTenKH.Location = new System.Drawing.Point(8, 39);
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.Size = new System.Drawing.Size(135, 20);
            this.txtTenKH.TabIndex = 9;
            this.txtTenKH.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txt_KeyPress);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 13);
            this.label2.TabIndex = 8;
            this.label2.Text = "Tên khách hàng";
            // 
            // dataGridViewButtonXColumn1
            // 
            this.dataGridViewButtonXColumn1.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.dataGridViewButtonXColumn1.FillWeight = 53.29949F;
            this.dataGridViewButtonXColumn1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.dataGridViewButtonXColumn1.HeaderText = "Chi tiết";
            this.dataGridViewButtonXColumn1.Image = ((System.Drawing.Image)(resources.GetObject("dataGridViewButtonXColumn1.Image")));
            this.dataGridViewButtonXColumn1.Name = "dataGridViewButtonXColumn1";
            this.dataGridViewButtonXColumn1.ReadOnly = true;
            this.dataGridViewButtonXColumn1.Text = null;
            this.dataGridViewButtonXColumn1.Width = 65;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.button1.Image = ((System.Drawing.Image)(resources.GetObject("button1.Image")));
            this.button1.Location = new System.Drawing.Point(190, 481);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 23);
            this.button1.TabIndex = 37;
            this.button1.UseVisualStyleBackColor = false;
            // 
            // btn_Next
            // 
            this.btn_Next.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_Next.Image = ((System.Drawing.Image)(resources.GetObject("btn_Next.Image")));
            this.btn_Next.Location = new System.Drawing.Point(148, 481);
            this.btn_Next.Name = "btn_Next";
            this.btn_Next.Size = new System.Drawing.Size(24, 23);
            this.btn_Next.TabIndex = 36;
            this.btn_Next.UseVisualStyleBackColor = false;
            // 
            // btn_Pre
            // 
            this.btn_Pre.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.btn_Pre.Image = ((System.Drawing.Image)(resources.GetObject("btn_Pre.Image")));
            this.btn_Pre.Location = new System.Drawing.Point(14, 481);
            this.btn_Pre.Name = "btn_Pre";
            this.btn_Pre.Size = new System.Drawing.Size(24, 23);
            this.btn_Pre.TabIndex = 35;
            this.btn_Pre.UseVisualStyleBackColor = false;
            // 
            // NapTienToolStripMenuItem
            // 
            this.NapTienToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("NapTienToolStripMenuItem.Image")));
            this.NapTienToolStripMenuItem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.NapTienToolStripMenuItem.Name = "NapTienToolStripMenuItem";
            this.NapTienToolStripMenuItem.Size = new System.Drawing.Size(126, 22);
            this.NapTienToolStripMenuItem.Text = "Nạp điểm";
            this.NapTienToolStripMenuItem.TextDirection = System.Windows.Forms.ToolStripTextDirection.Horizontal;
            // 
            // contextMenuStripThongTinKhachHang
            // 
            this.contextMenuStripThongTinKhachHang.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.NapTienToolStripMenuItem});
            this.contextMenuStripThongTinKhachHang.Name = "contextMenuStripThongTinKhachHang";
            this.contextMenuStripThongTinKhachHang.Size = new System.Drawing.Size(127, 26);
            // 
            // UcQuanLyDiemKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_Next);
            this.Controls.Add(this.btn_Pre);
            this.Controls.Add(this.lblTotalPage);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txtCurrentPage);
            this.Controls.Add(this.cbb_SoKHpage);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.panelEx1);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "UcQuanLyDiemKH";
            this.Size = new System.Drawing.Size(941, 537);
            this.Load += new System.EventHandler(this.UcQuanLyKhachHang_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.contextMenuStripThongTinKhachHang.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.ComboBox cbb_SoKHpage;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label lblTotalPage;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtCurrentPage;
        private System.Windows.Forms.Button btn_Pre;
        private System.Windows.Forms.Button btn_Next;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.Label label2;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.Button btn_Search;
        private System.Windows.Forms.Label lbl_SoKH;
        private System.Windows.Forms.Button btn_Export;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn dataGridViewButtonXColumn1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStripThongTinKhachHang;
        private System.Windows.Forms.ToolStripMenuItem NapTienToolStripMenuItem;
        private System.Windows.Forms.DataGridViewTextBoxColumn id_TD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn DienThoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCongty;
        private System.Windows.Forms.DataGridViewTextBoxColumn tongDiem;
        private System.Windows.Forms.DataGridViewTextBoxColumn diemConLai;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayTao;
        private DevComponents.DotNetBar.Controls.DataGridViewButtonXColumn detail;
    }
}
