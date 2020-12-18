namespace AutoCareV2._0
{
    partial class frmPhieuXuatKhoNgoai
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
            this.LayChiTietPhuTungTheoDonDat_ResultBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnHuyPhieu = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.txbDuongDanFileExcel = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cboSheet = new System.Windows.Forms.ComboBox();
            this.btnTaiLaiPhieu = new System.Windows.Forms.Button();
            this.btnXacNhanPhieu = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.LayChiTietPhuTungTheoDonDat_ResultBindingSource)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // LayChiTietPhuTungTheoDonDat_ResultBindingSource
            // 
            this.LayChiTietPhuTungTheoDonDat_ResultBindingSource.DataSource = typeof(AutoCareV2._0.LayChiTietPhuTungTheoDonDat_Result);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.reportViewer1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(895, 1000);
            this.panel1.TabIndex = 0;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.LayChiTietPhuTungTheoDonDat_ResultBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.PhieuXuatKhoNgoaiVietLong.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(895, 1000);
            this.reportViewer1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(917, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(84, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Trạng thái phiếu";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(1109, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 13);
            this.label2.TabIndex = 2;
            // 
            // btnHuyPhieu
            // 
            this.btnHuyPhieu.Location = new System.Drawing.Point(1253, 555);
            this.btnHuyPhieu.Name = "btnHuyPhieu";
            this.btnHuyPhieu.Size = new System.Drawing.Size(75, 23);
            this.btnHuyPhieu.TabIndex = 3;
            this.btnHuyPhieu.Text = "Hủy phiếu";
            this.btnHuyPhieu.UseVisualStyleBackColor = true;
            this.btnHuyPhieu.Click += new System.EventHandler(this.BtnHuyPhieu_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(913, 82);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(620, 467);
            this.dataGridView1.TabIndex = 4;
            // 
            // txbDuongDanFileExcel
            // 
            this.txbDuongDanFileExcel.Location = new System.Drawing.Point(998, 32);
            this.txbDuongDanFileExcel.Name = "txbDuongDanFileExcel";
            this.txbDuongDanFileExcel.ReadOnly = true;
            this.txbDuongDanFileExcel.Size = new System.Drawing.Size(146, 20);
            this.txbDuongDanFileExcel.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(920, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(60, 13);
            this.label3.TabIndex = 7;
            this.label3.Text = "Đường dẫn";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1150, 30);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 8;
            this.button1.Text = "Chọn File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(920, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Chọn sheet";
            // 
            // cboSheet
            // 
            this.cboSheet.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSheet.FormattingEnabled = true;
            this.cboSheet.Location = new System.Drawing.Point(998, 56);
            this.cboSheet.Name = "cboSheet";
            this.cboSheet.Size = new System.Drawing.Size(146, 21);
            this.cboSheet.TabIndex = 10;
            this.cboSheet.SelectedIndexChanged += new System.EventHandler(this.CboSheet_SelectedIndexChanged);
            // 
            // btnTaiLaiPhieu
            // 
            this.btnTaiLaiPhieu.Location = new System.Drawing.Point(1150, 56);
            this.btnTaiLaiPhieu.Name = "btnTaiLaiPhieu";
            this.btnTaiLaiPhieu.Size = new System.Drawing.Size(75, 23);
            this.btnTaiLaiPhieu.TabIndex = 11;
            this.btnTaiLaiPhieu.Text = "Tải phiếu";
            this.btnTaiLaiPhieu.UseVisualStyleBackColor = true;
            this.btnTaiLaiPhieu.Click += new System.EventHandler(this.BtnTaiLaiPhieu_Click);
            // 
            // btnXacNhanPhieu
            // 
            this.btnXacNhanPhieu.Location = new System.Drawing.Point(1172, 555);
            this.btnXacNhanPhieu.Name = "btnXacNhanPhieu";
            this.btnXacNhanPhieu.Size = new System.Drawing.Size(75, 23);
            this.btnXacNhanPhieu.TabIndex = 1;
            this.btnXacNhanPhieu.Text = "Xác nhận phiếu";
            this.btnXacNhanPhieu.UseVisualStyleBackColor = true;
            this.btnXacNhanPhieu.Click += new System.EventHandler(this.BtnXacNhanPhieu_Click);
            // 
            // frmPhieuXuatKhoNgoai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1545, 1061);
            this.Controls.Add(this.btnXacNhanPhieu);
            this.Controls.Add(this.btnTaiLaiPhieu);
            this.Controls.Add(this.cboSheet);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txbDuongDanFileExcel);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnHuyPhieu);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "frmPhieuXuatKhoNgoai";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu xuất kho ngoại";
            this.Load += new System.EventHandler(this.FrmPhieuXuatKhoNgoai_Load);
            ((System.ComponentModel.ISupportInitialize)(this.LayChiTietPhuTungTheoDonDat_ResultBindingSource)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource LayChiTietPhuTungTheoDonDat_ResultBindingSource;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnHuyPhieu;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.TextBox txbDuongDanFileExcel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboSheet;
        private System.Windows.Forms.Button btnTaiLaiPhieu;
        private System.Windows.Forms.Button btnXacNhanPhieu;
    }
}