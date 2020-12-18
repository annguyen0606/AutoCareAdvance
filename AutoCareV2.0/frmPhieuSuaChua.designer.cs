namespace AutoCareV2._0
{
    partial class frmPhieuSuaChua
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
            this.BaoCaoPhuTungThayTheBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.DocTienBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.ThongTinCongTyHienThiBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PhanTramGiamTruBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ThongTinBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.BaoCaoPhuTungThayTheBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocTienBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinCongTyHienThiBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhanTramGiamTruBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BaoCaoPhuTungThayTheBindingSource
            // 
            this.BaoCaoPhuTungThayTheBindingSource.DataSource = typeof(AutoCareV2._0.Class.BaoCaoPhuTungThayThe);
            this.BaoCaoPhuTungThayTheBindingSource.CurrentChanged += new System.EventHandler(this.BaoCaoPhuTungThayTheBindingSource_CurrentChanged);
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
            // reportViewer2
            // 
            this.reportViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetPhuTung";
            reportDataSource1.Value = this.BaoCaoPhuTungThayTheBindingSource;
            reportDataSource2.Name = "DataSetTongTien";
            reportDataSource2.Value = this.DocTienBindingSource;
            reportDataSource3.Name = "DataSetThongTin";
            reportDataSource3.Value = this.ThongTinCongTyHienThiBindingSource;
            reportDataSource4.Name = "DataSetGiamTru";
            reportDataSource4.Value = this.PhanTramGiamTruBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource3);
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource4);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.PhieuBaoDuongDongAMotor.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(0, 0);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(755, 684);
            this.reportViewer2.TabIndex = 0;
            // 
            // ThongTinBindingSource
            // 
            this.ThongTinBindingSource.DataSource = typeof(AutoCareV2._0.Class.ThongTinCongTyHienThi);
            // 
            // frmPhieuSuaChua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 684);
            this.Controls.Add(this.reportViewer2);
            this.Name = "frmPhieuSuaChua";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu Sửa Chữa";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPhieuSuaChua_FormClosing);
            this.Load += new System.EventHandler(this.frmPhieuSuaChua_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BaoCaoPhuTungThayTheBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DocTienBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinCongTyHienThiBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhanTramGiamTruBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.BindingSource BaoCaoPhuTungThayTheBindingSource;
        private System.Windows.Forms.BindingSource DocTienBindingSource;
        private System.Windows.Forms.BindingSource ThongTinBindingSource;
        private System.Windows.Forms.BindingSource ThongTinCongTyHienThiBindingSource;
        private System.Windows.Forms.BindingSource PhanTramGiamTruBindingSource;
    }
}