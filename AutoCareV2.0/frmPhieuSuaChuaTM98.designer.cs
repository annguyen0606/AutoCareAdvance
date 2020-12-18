namespace AutoCareV2._0
{
    partial class frmPhieuSuaChuaTM98
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.DocTienBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ThongTinCongTyHienThiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PhanTramGiamTruBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.BaoCaoPhuTungThayTheBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.DocTienBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinCongTyHienThiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhanTramGiamTruBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaoCaoPhuTungThayTheBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetTongTien";
            reportDataSource1.Value = this.DocTienBindingSource;
            reportDataSource2.Name = "DataSetThongTin";
            reportDataSource2.Value = this.ThongTinCongTyHienThiBindingSource;
            reportDataSource3.Name = "DataSetGiamTru";
            reportDataSource3.Value = this.PhanTramGiamTruBindingSource;
            reportDataSource4.Name = "DataSetPhuTung";
            reportDataSource4.Value = this.BaoCaoPhuTungThayTheBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.PhieuSuaChua_ThuongMai98.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(660, 680);
            this.reportViewer1.TabIndex = 0;
            // 
            // DocTienBindingSource
            // 
            this.DocTienBindingSource.DataSource = typeof(AutoCareV2._0.Class.DocTien);
            // 
            // ThongTinCongTyHienThiBindingSource
            // 
            this.ThongTinCongTyHienThiBindingSource.DataSource = typeof(AutoCareV2._0.Class.ThongTinCongTyHienThi);
            // 
            // PhanTramGiamTruBindingSource
            // 
            this.PhanTramGiamTruBindingSource.DataSource = typeof(AutoCareV2._0.Class.PhanTramGiamTru);
            // 
            // BaoCaoPhuTungThayTheBindingSource
            // 
            this.BaoCaoPhuTungThayTheBindingSource.DataSource = typeof(AutoCareV2._0.Class.BaoCaoPhuTungThayThe);
            // 
            // frmPhieuSuaChuaTM98
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 680);
            this.Controls.Add(this.reportViewer1);
            this.Name = "frmPhieuSuaChuaTM98";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu sửa chữa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPhieuSuaChuaTM98_FormClosing);
            this.Load += new System.EventHandler(this.frmPhieuSuaChuaTM98_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DocTienBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinCongTyHienThiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhanTramGiamTruBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BaoCaoPhuTungThayTheBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource BaoCaoPhuTungThayTheBindingSource;
        private System.Windows.Forms.BindingSource DocTienBindingSource;
        private System.Windows.Forms.BindingSource ThongTinCongTyHienThiBindingSource;
        private System.Windows.Forms.BindingSource PhanTramGiamTruBindingSource;
    }
}