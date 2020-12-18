namespace AutoCareV2._0
{
    partial class frmPhieuSuaChuaDinhKy
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
            this.BaoCaoPhuTungThayTheDinhKyBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PhanTramGiamTruBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.BaoCaoPhuTungThayTheDinhKyBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhanTramGiamTruBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // BaoCaoPhuTungThayTheBindingSource
            // 
            this.BaoCaoPhuTungThayTheDinhKyBindingSource.DataSource = typeof(AutoCareV2._0.Class.BaoCaoPhuTungThayTheDinhKy);
            // 
            // PhanTramGiamTruBindingSource
            // 
            this.PhanTramGiamTruBindingSource.DataSource = typeof(AutoCareV2._0.Class.PhanTramGiamTru);
            // 
            // reportViewer2
            // 
            this.reportViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSetPhuTungDinhKy";
            reportDataSource1.Value = this.BaoCaoPhuTungThayTheDinhKyBindingSource;
            reportDataSource2.Name = "DataSetGiamTru";
            reportDataSource2.Value = this.PhanTramGiamTruBindingSource;
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer2.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.PhieuSuaChuaDinhKy.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(0, 0);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(755, 684);
            this.reportViewer2.TabIndex = 0;
            // 
            // frmPhieuSuaChua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(755, 684);
            this.Controls.Add(this.reportViewer2);
            this.Name = "frmPhieuSuaChuaDinhKy";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu Sửa Chữa Định Kỳ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPhieuSuaChuaDinhKy_FormClosing);
            this.Load += new System.EventHandler(this.frmPhieuSuaChuaDinhKy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BaoCaoPhuTungThayTheDinhKyBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhanTramGiamTruBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.BindingSource BaoCaoPhuTungThayTheDinhKyBindingSource;
        private System.Windows.Forms.BindingSource PhanTramGiamTruBindingSource;
    }
}