namespace AutoCareV2._0
{
    partial class PhuTungUpdate
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PhuTungUpdate));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chk_CapNhatSoLuong = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chk_CapNhatGia = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtPart = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.btn_CapNhat = new DevComponents.DotNetBar.ButtonX();
            this.btn_HuyBo = new DevComponents.DotNetBar.ButtonX();
            this.btn_Import = new DevComponents.DotNetBar.ButtonX();
            this.btn_ChonFile = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chk_CapNhatSoLuong);
            this.groupBox3.Controls.Add(this.chk_CapNhatGia);
            this.groupBox3.Controls.Add(this.btn_Import);
            this.groupBox3.Controls.Add(this.txtPart);
            this.groupBox3.Controls.Add(this.btn_ChonFile);
            this.groupBox3.Location = new System.Drawing.Point(343, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(479, 83);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Import Phụ tùng cập nhật số lượng";
            // 
            // chk_CapNhatSoLuong
            // 
            // 
            // 
            // 
            this.chk_CapNhatSoLuong.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chk_CapNhatSoLuong.Location = new System.Drawing.Point(108, 54);
            this.chk_CapNhatSoLuong.Name = "chk_CapNhatSoLuong";
            this.chk_CapNhatSoLuong.Size = new System.Drawing.Size(125, 23);
            this.chk_CapNhatSoLuong.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chk_CapNhatSoLuong.TabIndex = 16;
            this.chk_CapNhatSoLuong.Text = "Cập nhật số lượng";
            // 
            // chk_CapNhatGia
            // 
            // 
            // 
            // 
            this.chk_CapNhatGia.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chk_CapNhatGia.Location = new System.Drawing.Point(20, 54);
            this.chk_CapNhatGia.Name = "chk_CapNhatGia";
            this.chk_CapNhatGia.Size = new System.Drawing.Size(100, 23);
            this.chk_CapNhatGia.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chk_CapNhatGia.TabIndex = 15;
            this.chk_CapNhatGia.Text = "Cập nhật giá";
            // 
            // txtPart
            // 
            this.txtPart.BackColor = System.Drawing.Color.White;
            this.txtPart.Location = new System.Drawing.Point(20, 26);
            this.txtPart.Name = "txtPart";
            this.txtPart.ReadOnly = true;
            this.txtPart.Size = new System.Drawing.Size(248, 20);
            this.txtPart.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.dataGridView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox1.Location = new System.Drawing.Point(0, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(939, 326);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Danh sách Phụ tùng cập nhật số lượng";
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(3, 16);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(933, 307);
            this.dataGridView1.TabIndex = 0;
            // 
            // btn_CapNhat
            // 
            this.btn_CapNhat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_CapNhat.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_CapNhat.Image = ((System.Drawing.Image)(resources.GetObject("btn_CapNhat.Image")));
            this.btn_CapNhat.Location = new System.Drawing.Point(836, 34);
            this.btn_CapNhat.Name = "btn_CapNhat";
            this.btn_CapNhat.Size = new System.Drawing.Size(95, 30);
            this.btn_CapNhat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_CapNhat.TabIndex = 15;
            this.btn_CapNhat.Text = "&Cập nhật ";
            this.btn_CapNhat.Click += new System.EventHandler(this.btn_CapNhat_Click);
            // 
            // btn_HuyBo
            // 
            this.btn_HuyBo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_HuyBo.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_HuyBo.Image = ((System.Drawing.Image)(resources.GetObject("btn_HuyBo.Image")));
            this.btn_HuyBo.Location = new System.Drawing.Point(836, 69);
            this.btn_HuyBo.Name = "btn_HuyBo";
            this.btn_HuyBo.Size = new System.Drawing.Size(95, 30);
            this.btn_HuyBo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_HuyBo.TabIndex = 14;
            this.btn_HuyBo.Text = "&Hủy bỏ";
            this.btn_HuyBo.Click += new System.EventHandler(this.btn_HuyBo_Click);
            // 
            // btn_Import
            // 
            this.btn_Import.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Import.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_Import.Image = ((System.Drawing.Image)(resources.GetObject("btn_Import.Image")));
            this.btn_Import.Location = new System.Drawing.Point(375, 22);
            this.btn_Import.Name = "btn_Import";
            this.btn_Import.Size = new System.Drawing.Size(95, 30);
            this.btn_Import.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Import.TabIndex = 14;
            this.btn_Import.Text = "&Import";
            this.btn_Import.Click += new System.EventHandler(this.btn_Import_Click);
            // 
            // btn_ChonFile
            // 
            this.btn_ChonFile.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_ChonFile.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btn_ChonFile.Image = ((System.Drawing.Image)(resources.GetObject("btn_ChonFile.Image")));
            this.btn_ChonFile.ImageFixedSize = new System.Drawing.Size(20, 20);
            this.btn_ChonFile.Location = new System.Drawing.Point(274, 22);
            this.btn_ChonFile.Name = "btn_ChonFile";
            this.btn_ChonFile.Size = new System.Drawing.Size(95, 30);
            this.btn_ChonFile.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_ChonFile.TabIndex = 14;
            this.btn_ChonFile.Text = "&Chọn tệp tin";
            this.btn_ChonFile.Click += new System.EventHandler(this.btn_ChonFile_Click);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(0, 21);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(337, 83);
            this.labelX1.TabIndex = 17;
            this.labelX1.Text = "CẬP NHẬT PHỤ TÙNG";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // PhuTungUpdate
            // 
            this.ClientSize = new System.Drawing.Size(939, 437);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_CapNhat);
            this.Controls.Add(this.btn_HuyBo);
            this.Controls.Add(this.groupBox3);
            this.DoubleBuffered = true;
            this.Name = "PhuTungUpdate";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cập nhật phụ tùng";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PhuTungUpdate_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.PhuTungUpdate_FormClosed);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtPart;
        private DevComponents.DotNetBar.ButtonX btn_Import;
        private DevComponents.DotNetBar.ButtonX btn_ChonFile;
        private DevComponents.DotNetBar.ButtonX btn_HuyBo;
        private DevComponents.DotNetBar.ButtonX btn_CapNhat;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DataGridView dataGridView1;
        private DevComponents.DotNetBar.Controls.CheckBoxX chk_CapNhatGia;
        private DevComponents.DotNetBar.Controls.CheckBoxX chk_CapNhatSoLuong;
        private DevComponents.DotNetBar.LabelX labelX1;
    }
}