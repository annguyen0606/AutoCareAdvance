namespace AutoCareV2._0
{
    partial class frmPhieuXuatKhoNoiBoVietLong
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
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnXacNhanXuatKho = new System.Windows.Forms.Button();
            this.btnHuyDon = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.PhieuXuatKhoNoiBoVietLong.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.ServerReport.BearerToken = null;
            this.reportViewer1.Size = new System.Drawing.Size(872, 1123);
            this.reportViewer1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.reportViewer1);
            this.panel1.Location = new System.Drawing.Point(12, 12);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(872, 1123);
            this.panel1.TabIndex = 1;
            // 
            // btnXacNhanXuatKho
            // 
            this.btnXacNhanXuatKho.Location = new System.Drawing.Point(921, 12);
            this.btnXacNhanXuatKho.Name = "btnXacNhanXuatKho";
            this.btnXacNhanXuatKho.Size = new System.Drawing.Size(75, 23);
            this.btnXacNhanXuatKho.TabIndex = 2;
            this.btnXacNhanXuatKho.Text = "Xác nhận";
            this.btnXacNhanXuatKho.UseVisualStyleBackColor = true;
            this.btnXacNhanXuatKho.Click += new System.EventHandler(this.BtnXacNhanXuatKho_Click);
            // 
            // btnHuyDon
            // 
            this.btnHuyDon.Location = new System.Drawing.Point(921, 41);
            this.btnHuyDon.Name = "btnHuyDon";
            this.btnHuyDon.Size = new System.Drawing.Size(75, 23);
            this.btnHuyDon.TabIndex = 3;
            this.btnHuyDon.Text = "Hủy đơn";
            this.btnHuyDon.UseVisualStyleBackColor = true;
            this.btnHuyDon.Click += new System.EventHandler(this.BtnHuyDon_Click);
            // 
            // frmPhieuXuatKhoNoiBoVietLong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 1061);
            this.Controls.Add(this.btnHuyDon);
            this.Controls.Add(this.btnXacNhanXuatKho);
            this.Controls.Add(this.panel1);
            this.Name = "frmPhieuXuatKhoNoiBoVietLong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu xuất kho Việt Long";
            this.Load += new System.EventHandler(this.FrmPhieuXuatKhoNoiBoVietLong_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnXacNhanXuatKho;
        private System.Windows.Forms.Button btnHuyDon;
    }
}