namespace AutoCareV2._0
{
    partial class FrmTNMungsinhnhatKH
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTNMungsinhnhatKH));
            this.grvsnkh = new System.Windows.Forms.DataGridView();
            this.MaKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HoKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenKH = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column5 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DienThoai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column7 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column8 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.IdCongty = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.khachHangBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.grvsnkh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.khachHangBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // grvsnkh
            // 
            this.grvsnkh.AllowUserToAddRows = false;
            this.grvsnkh.AllowUserToDeleteRows = false;
            this.grvsnkh.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.grvsnkh.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvsnkh.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaKH,
            this.HoKH,
            this.TenKH,
            this.Column4,
            this.Column5,
            this.DienThoai,
            this.Column7,
            this.Column8,
            this.Column1,
            this.IdCongty});
            this.grvsnkh.Location = new System.Drawing.Point(2, 3);
            this.grvsnkh.Name = "grvsnkh";
            this.grvsnkh.Size = new System.Drawing.Size(847, 303);
            this.grvsnkh.TabIndex = 1;
            // 
            // MaKH
            // 
            this.MaKH.DataPropertyName = "IdKhachHang";
            this.MaKH.HeaderText = "Mã KH";
            this.MaKH.Name = "MaKH";
            this.MaKH.ReadOnly = true;
            // 
            // HoKH
            // 
            this.HoKH.DataPropertyName = "HoKH";
            this.HoKH.HeaderText = "Họ KH";
            this.HoKH.Name = "HoKH";
            // 
            // TenKH
            // 
            this.TenKH.DataPropertyName = "TenKH";
            this.TenKH.HeaderText = "Tên KH";
            this.TenKH.Name = "TenKH";
            // 
            // Column4
            // 
            this.Column4.DataPropertyName = "GioiTinh";
            this.Column4.HeaderText = "Giới tính";
            this.Column4.Name = "Column4";
            // 
            // Column5
            // 
            this.Column5.DataPropertyName = "Diachi";
            this.Column5.HeaderText = "Địa chỉ";
            this.Column5.Name = "Column5";
            // 
            // DienThoai
            // 
            this.DienThoai.DataPropertyName = "DienThoai";
            this.DienThoai.HeaderText = "Điện thoại";
            this.DienThoai.Name = "DienThoai";
            // 
            // Column7
            // 
            this.Column7.DataPropertyName = "NgaySinh";
            this.Column7.HeaderText = "Ngày sinh";
            this.Column7.Name = "Column7";
            // 
            // Column8
            // 
            this.Column8.DataPropertyName = "NgayMua";
            this.Column8.HeaderText = "Ngày mua xe";
            this.Column8.Name = "Column8";
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "MaNhomKH";
            this.Column1.HeaderText = "Mã nhóm KH";
            this.Column1.Name = "Column1";
            // 
            // IdCongty
            // 
            this.IdCongty.DataPropertyName = "IdCongty";
            this.IdCongty.HeaderText = "Mã công ty";
            this.IdCongty.Name = "IdCongty";
            // 
            // khachHangBindingSource
            // 
            this.khachHangBindingSource.DataMember = "KhachHang";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.buttonX1.Image = ((System.Drawing.Image)(resources.GetObject("buttonX1.Image")));
            this.buttonX1.Location = new System.Drawing.Point(366, 312);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(95, 30);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 2;
            this.buttonX1.Text = "Gửi tin";
            this.buttonX1.Click += new System.EventHandler(this.buttonX1_Click);
            // 
            // FrmTNMungsinhnhatKH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(850, 349);
            this.Controls.Add(this.buttonX1);
            this.Controls.Add(this.grvsnkh);
            this.Name = "FrmTNMungsinhnhatKH";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Danh sách khách hàng sinh nhật trong ngày";
            this.Load += new System.EventHandler(this.FrmTNMungsinhnhatKH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grvsnkh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.khachHangBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grvsnkh;
      //  private AutoCareDataSet4 autoCareDataSet4;
        private System.Windows.Forms.BindingSource khachHangBindingSource;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn HoKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenKH;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column4;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column5;
        private System.Windows.Forms.DataGridViewTextBoxColumn DienThoai;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column7;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column8;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn IdCongty;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        // private AutoCareDataSet4TableAdapters.KhachHangTableAdapter khachHangTableAdapter;
    }
}