namespace AutoCareV2._0
{
    partial class FrmPhieuXuatKho
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.PhieuXuatKhoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DocTienBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PhieuDieuChuyenBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PhieuXuatKhoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocTienBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuDieuChuyenBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource2.Name = "DataSet1";
            reportDataSource2.Value = null;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.ReportBaoCaoDieuChuyen.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(651, 601);
            this.reportViewer1.TabIndex = 0;
            // 
            // PhieuXuatKhoBindingSource
            // 
            this.PhieuXuatKhoBindingSource.DataSource = typeof(AutoCareV2._0.Class.PhieuXuatKho);
            // 
            // DocTienBindingSource
            // 
            this.DocTienBindingSource.DataSource = typeof(AutoCareV2._0.Class.DocTien);
            // 
            // PhieuDieuChuyenBindingSource
            // 
            this.PhieuDieuChuyenBindingSource.DataMember = "PhieuDieuChuyen";
            // 
            // FrmPhieuXuatKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(651, 601);
            this.Controls.Add(this.reportViewer1);
            this.Name = "FrmPhieuXuatKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu Xuất Kho";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmPhieuXuatKho_FormClosing);
            this.Load += new System.EventHandler(this.FrmPhieuXuatKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PhieuXuatKhoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocTienBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuDieuChuyenBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.BindingSource PhieuXuatKhoBindingSource;
        private System.Windows.Forms.BindingSource DocTienBindingSource;
        private System.Windows.Forms.BindingSource PhieuDieuChuyenBindingSource;
        public Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        


    }
}