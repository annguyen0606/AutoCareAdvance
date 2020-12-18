namespace AutoCareV2._0
{
    partial class FrmPhieuBanXe
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
            this.PhieuBanXeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.BanXeBindingSource = new System.Windows.Forms.BindingSource(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.PhieuBanXeBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.BanXeBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // PhieuBanXeBindingSource
            // 
            this.PhieuBanXeBindingSource.DataSource = typeof(AutoCareV2._0.Class.PhieuBanXe);
            // 
            // reportViewer1
            // 
            this.reportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.PhieuBanXeBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.DucTri.BanXe.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(712, 670);
            this.reportViewer1.TabIndex = 0;
            // 
            // FrmPhieuBanXe
            // 
            this.ClientSize = new System.Drawing.Size(712, 670);
            this.Controls.Add(this.reportViewer1);
            this.DoubleBuffered = true;
            this.Name = "FrmPhieuBanXe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Phiếu bán xe";
            this.Load += new System.EventHandler(this.FrmPhieuBanXe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.PhieuBanXeBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.BanXeBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource BanXeBindingSource;
        private System.Windows.Forms.BindingSource PhieuBanXeBindingSource;
    }
}