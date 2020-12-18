namespace AutoCareV2._0
{
    partial class FrmPhieuNhapKho
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
            this.reportViewerPhieuNhapKho = new Microsoft.Reporting.WinForms.ReportViewer();
            this.ThongTinNhaCungCapBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.PhieuNhapKhoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinNhaCungCapBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuNhapKhoBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // reportViewerPhieuNhapKho
            // 
            this.reportViewerPhieuNhapKho.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "ThongTinNhaCungCap";
            reportDataSource1.Value = this.ThongTinNhaCungCapBindingSource;
            reportDataSource2.Name = "PhieuNhapKho";
            reportDataSource2.Value = this.PhieuNhapKhoBindingSource;
            this.reportViewerPhieuNhapKho.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewerPhieuNhapKho.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewerPhieuNhapKho.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.PhieuNhapKho.rdlc";
            this.reportViewerPhieuNhapKho.Location = new System.Drawing.Point(0, 0);
            this.reportViewerPhieuNhapKho.Name = "reportViewerPhieuNhapKho";
            this.reportViewerPhieuNhapKho.Size = new System.Drawing.Size(760, 493);
            this.reportViewerPhieuNhapKho.TabIndex = 0;
            // 
            // ThongTinNhaCungCapBindingSource
            // 
            this.ThongTinNhaCungCapBindingSource.DataSource = typeof(AutoCareV2._0.Class.ThongTinNhaCungCap);
            // 
            // PhieuNhapKhoBindingSource
            // 
            this.PhieuNhapKhoBindingSource.DataSource = typeof(AutoCareV2._0.Class.PhieuNhapKho);
            // 
            // frmPhieuNhapKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(760, 493);
            this.Controls.Add(this.reportViewerPhieuNhapKho);
            this.Name = "FrmPhieuNhapKho";
            this.Text = "In phiếu nhập kho phụ tùng";
            this.Load += new System.EventHandler(this.frmPhieuNhapKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.ThongTinNhaCungCapBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PhieuNhapKhoBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerPhieuNhapKho;
        private System.Windows.Forms.BindingSource ThongTinNhaCungCapBindingSource;
        private System.Windows.Forms.BindingSource PhieuNhapKhoBindingSource;
    }
}