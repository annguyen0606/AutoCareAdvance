namespace AutoCareV2._0
{
    partial class FrmThongKeKhachBdTheoTinNhan
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmThongKeKhachBdTheoTinNhan));
            this.pnFunction = new System.Windows.Forms.Panel();
            this.cbbLoaiBaoDuong = new System.Windows.Forms.ComboBox();
            this.lblLoaiBaoDuong = new System.Windows.Forms.Label();
            this.dpkDenNgay = new System.Windows.Forms.DateTimePicker();
            this.lblDenNgay = new System.Windows.Forms.Label();
            this.dpkTuNgay = new System.Windows.Forms.DateTimePicker();
            this.lblTuNgay = new System.Windows.Forms.Label();
            this.btnThongKe = new System.Windows.Forms.Button();
            this.linkXemFileMau = new System.Windows.Forms.LinkLabel();
            this.btnChooseFile = new System.Windows.Forms.Button();
            this.txtPathExcel = new System.Windows.Forms.TextBox();
            this.lblChonTepExcel = new System.Windows.Forms.Label();
            this.pnDataExcelAndChart = new System.Windows.Forms.Panel();
            this.gbDataExcel = new System.Windows.Forms.GroupBox();
            this.grvDataExcel = new System.Windows.Forms.DataGridView();
            this.gbChart = new System.Windows.Forms.GroupBox();
            this.chartStatistic = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.pnDataResult = new System.Windows.Forms.Panel();
            this.gbKhachDaDen = new System.Windows.Forms.GroupBox();
            this.pnData = new System.Windows.Forms.Panel();
            this.grvKhachDaDen = new System.Windows.Forms.DataGridView();
            this.pnExport = new System.Windows.Forms.Panel();
            this.btnExportKhachDaDen = new System.Windows.Forms.Button();
            this.gbKhachChuaDen = new System.Windows.Forms.GroupBox();
            this.pnData1 = new System.Windows.Forms.Panel();
            this.grvKhachChuaDen = new System.Windows.Forms.DataGridView();
            this.pnExport1 = new System.Windows.Forms.Panel();
            this.pnExportKhachChuaDen = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.pnFunction.SuspendLayout();
            this.pnDataExcelAndChart.SuspendLayout();
            this.gbDataExcel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvDataExcel)).BeginInit();
            this.gbChart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartStatistic)).BeginInit();
            this.pnDataResult.SuspendLayout();
            this.gbKhachDaDen.SuspendLayout();
            this.pnData.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhachDaDen)).BeginInit();
            this.pnExport.SuspendLayout();
            this.gbKhachChuaDen.SuspendLayout();
            this.pnData1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grvKhachChuaDen)).BeginInit();
            this.pnExport1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnFunction
            // 
            this.pnFunction.Controls.Add(this.cbbLoaiBaoDuong);
            this.pnFunction.Controls.Add(this.lblLoaiBaoDuong);
            this.pnFunction.Controls.Add(this.dpkDenNgay);
            this.pnFunction.Controls.Add(this.lblDenNgay);
            this.pnFunction.Controls.Add(this.dpkTuNgay);
            this.pnFunction.Controls.Add(this.lblTuNgay);
            this.pnFunction.Controls.Add(this.btnThongKe);
            this.pnFunction.Controls.Add(this.linkXemFileMau);
            this.pnFunction.Controls.Add(this.btnChooseFile);
            this.pnFunction.Controls.Add(this.txtPathExcel);
            this.pnFunction.Controls.Add(this.lblChonTepExcel);
            this.pnFunction.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnFunction.Location = new System.Drawing.Point(0, 0);
            this.pnFunction.Name = "pnFunction";
            this.pnFunction.Size = new System.Drawing.Size(1032, 40);
            this.pnFunction.TabIndex = 0;
            // 
            // cbbLoaiBaoDuong
            // 
            this.cbbLoaiBaoDuong.FormattingEnabled = true;
            this.cbbLoaiBaoDuong.Items.AddRange(new object[] {
            "Bảo dưỡng định kỳ",
            "Bảo dưỡng dịch vụ"});
            this.cbbLoaiBaoDuong.Location = new System.Drawing.Point(385, 8);
            this.cbbLoaiBaoDuong.Name = "cbbLoaiBaoDuong";
            this.cbbLoaiBaoDuong.Size = new System.Drawing.Size(121, 21);
            this.cbbLoaiBaoDuong.TabIndex = 11;
            // 
            // lblLoaiBaoDuong
            // 
            this.lblLoaiBaoDuong.AutoSize = true;
            this.lblLoaiBaoDuong.Location = new System.Drawing.Point(331, 12);
            this.lblLoaiBaoDuong.Name = "lblLoaiBaoDuong";
            this.lblLoaiBaoDuong.Size = new System.Drawing.Size(48, 13);
            this.lblLoaiBaoDuong.TabIndex = 10;
            this.lblLoaiBaoDuong.Text = "Loại BD:";
            // 
            // dpkDenNgay
            // 
            this.dpkDenNgay.CustomFormat = "dd/MM/yyyy";
            this.dpkDenNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkDenNgay.Location = new System.Drawing.Point(740, 8);
            this.dpkDenNgay.Name = "dpkDenNgay";
            this.dpkDenNgay.Size = new System.Drawing.Size(99, 20);
            this.dpkDenNgay.TabIndex = 9;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.AutoSize = true;
            this.lblDenNgay.Location = new System.Drawing.Point(676, 12);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(56, 13);
            this.lblDenNgay.TabIndex = 8;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dpkTuNgay
            // 
            this.dpkTuNgay.CustomFormat = "dd/MM/yyyy";
            this.dpkTuNgay.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dpkTuNgay.Location = new System.Drawing.Point(569, 8);
            this.dpkTuNgay.Name = "dpkTuNgay";
            this.dpkTuNgay.Size = new System.Drawing.Size(99, 20);
            this.dpkTuNgay.TabIndex = 7;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.AutoSize = true;
            this.lblTuNgay.Location = new System.Drawing.Point(512, 12);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(49, 13);
            this.lblTuNgay.TabIndex = 6;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // btnThongKe
            // 
            this.btnThongKe.Location = new System.Drawing.Point(847, 7);
            this.btnThongKe.Name = "btnThongKe";
            this.btnThongKe.Size = new System.Drawing.Size(75, 23);
            this.btnThongKe.TabIndex = 5;
            this.btnThongKe.Text = "Thống kê";
            this.btnThongKe.UseVisualStyleBackColor = true;
            this.btnThongKe.Click += new System.EventHandler(this.btnThongKe_Click);
            // 
            // linkXemFileMau
            // 
            this.linkXemFileMau.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.linkXemFileMau.AutoSize = true;
            this.linkXemFileMau.Location = new System.Drawing.Point(933, 13);
            this.linkXemFileMau.Name = "linkXemFileMau";
            this.linkXemFileMau.Size = new System.Drawing.Size(96, 13);
            this.linkXemFileMau.TabIndex = 4;
            this.linkXemFileMau.TabStop = true;
            this.linkXemFileMau.Text = "Xem file Excel mẫu";
            this.linkXemFileMau.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkXemFileMau_LinkClicked);
            // 
            // btnChooseFile
            // 
            this.btnChooseFile.Location = new System.Drawing.Point(269, 6);
            this.btnChooseFile.Name = "btnChooseFile";
            this.btnChooseFile.Size = new System.Drawing.Size(30, 23);
            this.btnChooseFile.TabIndex = 2;
            this.btnChooseFile.Text = "...";
            this.btnChooseFile.UseVisualStyleBackColor = true;
            this.btnChooseFile.Click += new System.EventHandler(this.btnChooseFile_Click);
            // 
            // txtPathExcel
            // 
            this.txtPathExcel.Location = new System.Drawing.Point(97, 8);
            this.txtPathExcel.Name = "txtPathExcel";
            this.txtPathExcel.Size = new System.Drawing.Size(166, 20);
            this.txtPathExcel.TabIndex = 1;
            // 
            // lblChonTepExcel
            // 
            this.lblChonTepExcel.AutoSize = true;
            this.lblChonTepExcel.Location = new System.Drawing.Point(12, 11);
            this.lblChonTepExcel.Name = "lblChonTepExcel";
            this.lblChonTepExcel.Size = new System.Drawing.Size(79, 13);
            this.lblChonTepExcel.TabIndex = 0;
            this.lblChonTepExcel.Text = "Chọn tệp Excel";
            // 
            // pnDataExcelAndChart
            // 
            this.pnDataExcelAndChart.Controls.Add(this.gbDataExcel);
            this.pnDataExcelAndChart.Controls.Add(this.gbChart);
            this.pnDataExcelAndChart.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnDataExcelAndChart.Location = new System.Drawing.Point(0, 40);
            this.pnDataExcelAndChart.Name = "pnDataExcelAndChart";
            this.pnDataExcelAndChart.Size = new System.Drawing.Size(361, 498);
            this.pnDataExcelAndChart.TabIndex = 1;
            // 
            // gbDataExcel
            // 
            this.gbDataExcel.Controls.Add(this.grvDataExcel);
            this.gbDataExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDataExcel.Location = new System.Drawing.Point(0, 0);
            this.gbDataExcel.Name = "gbDataExcel";
            this.gbDataExcel.Size = new System.Drawing.Size(361, 262);
            this.gbDataExcel.TabIndex = 0;
            this.gbDataExcel.TabStop = false;
            this.gbDataExcel.Text = "Dữ liệu file Excel (0 dòng)";
            // 
            // grvDataExcel
            // 
            this.grvDataExcel.AllowUserToAddRows = false;
            this.grvDataExcel.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvDataExcel.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvDataExcel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvDataExcel.Location = new System.Drawing.Point(3, 16);
            this.grvDataExcel.Name = "grvDataExcel";
            this.grvDataExcel.RowHeadersVisible = false;
            this.grvDataExcel.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvDataExcel.Size = new System.Drawing.Size(355, 243);
            this.grvDataExcel.TabIndex = 0;
            // 
            // gbChart
            // 
            this.gbChart.Controls.Add(this.chartStatistic);
            this.gbChart.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbChart.Location = new System.Drawing.Point(0, 262);
            this.gbChart.Name = "gbChart";
            this.gbChart.Size = new System.Drawing.Size(361, 236);
            this.gbChart.TabIndex = 0;
            this.gbChart.TabStop = false;
            this.gbChart.Text = "Biểu đồ";
            // 
            // chartStatistic
            // 
            chartArea1.AxisX.Title = "Loại";
            chartArea1.AxisY.Title = "Số khách";
            chartArea1.Name = "ChartArea1";
            this.chartStatistic.ChartAreas.Add(chartArea1);
            this.chartStatistic.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Name = "Legend1";
            this.chartStatistic.Legends.Add(legend1);
            this.chartStatistic.Location = new System.Drawing.Point(3, 16);
            this.chartStatistic.Name = "chartStatistic";
            this.chartStatistic.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Excel;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Pie;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartStatistic.Series.Add(series1);
            this.chartStatistic.Size = new System.Drawing.Size(355, 217);
            this.chartStatistic.TabIndex = 0;
            // 
            // pnDataResult
            // 
            this.pnDataResult.Controls.Add(this.gbKhachDaDen);
            this.pnDataResult.Controls.Add(this.gbKhachChuaDen);
            this.pnDataResult.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnDataResult.Location = new System.Drawing.Point(361, 40);
            this.pnDataResult.Name = "pnDataResult";
            this.pnDataResult.Size = new System.Drawing.Size(671, 498);
            this.pnDataResult.TabIndex = 2;
            // 
            // gbKhachDaDen
            // 
            this.gbKhachDaDen.Controls.Add(this.pnData);
            this.gbKhachDaDen.Controls.Add(this.pnExport);
            this.gbKhachDaDen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbKhachDaDen.Location = new System.Drawing.Point(0, 0);
            this.gbKhachDaDen.Name = "gbKhachDaDen";
            this.gbKhachDaDen.Size = new System.Drawing.Size(671, 262);
            this.gbKhachDaDen.TabIndex = 1;
            this.gbKhachDaDen.TabStop = false;
            this.gbKhachDaDen.Text = "Khách đã đến bảo dưỡng (0 khách hàng)";
            // 
            // pnData
            // 
            this.pnData.Controls.Add(this.grvKhachDaDen);
            this.pnData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnData.Location = new System.Drawing.Point(3, 16);
            this.pnData.Name = "pnData";
            this.pnData.Size = new System.Drawing.Size(665, 217);
            this.pnData.TabIndex = 1;
            // 
            // grvKhachDaDen
            // 
            this.grvKhachDaDen.AllowUserToAddRows = false;
            this.grvKhachDaDen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvKhachDaDen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvKhachDaDen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvKhachDaDen.EnableHeadersVisualStyles = false;
            this.grvKhachDaDen.Location = new System.Drawing.Point(0, 0);
            this.grvKhachDaDen.Name = "grvKhachDaDen";
            this.grvKhachDaDen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvKhachDaDen.Size = new System.Drawing.Size(665, 217);
            this.grvKhachDaDen.TabIndex = 0;
            // 
            // pnExport
            // 
            this.pnExport.Controls.Add(this.btnExportKhachDaDen);
            this.pnExport.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnExport.Location = new System.Drawing.Point(3, 233);
            this.pnExport.Name = "pnExport";
            this.pnExport.Size = new System.Drawing.Size(665, 26);
            this.pnExport.TabIndex = 0;
            // 
            // btnExportKhachDaDen
            // 
            this.btnExportKhachDaDen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExportKhachDaDen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExportKhachDaDen.Location = new System.Drawing.Point(597, 2);
            this.btnExportKhachDaDen.Name = "btnExportKhachDaDen";
            this.btnExportKhachDaDen.Size = new System.Drawing.Size(67, 23);
            this.btnExportKhachDaDen.TabIndex = 0;
            this.btnExportKhachDaDen.Text = "Export";
            this.btnExportKhachDaDen.UseVisualStyleBackColor = true;
            this.btnExportKhachDaDen.Click += new System.EventHandler(this.btnExportKhachDaDen_Click);
            // 
            // gbKhachChuaDen
            // 
            this.gbKhachChuaDen.Controls.Add(this.pnData1);
            this.gbKhachChuaDen.Controls.Add(this.pnExport1);
            this.gbKhachChuaDen.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbKhachChuaDen.Location = new System.Drawing.Point(0, 262);
            this.gbKhachChuaDen.Name = "gbKhachChuaDen";
            this.gbKhachChuaDen.Size = new System.Drawing.Size(671, 236);
            this.gbKhachChuaDen.TabIndex = 0;
            this.gbKhachChuaDen.TabStop = false;
            this.gbKhachChuaDen.Text = "Khách chưa đến bảo dưỡng (0 khách hàng)";
            // 
            // pnData1
            // 
            this.pnData1.Controls.Add(this.grvKhachChuaDen);
            this.pnData1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnData1.Location = new System.Drawing.Point(3, 16);
            this.pnData1.Name = "pnData1";
            this.pnData1.Size = new System.Drawing.Size(665, 189);
            this.pnData1.TabIndex = 1;
            // 
            // grvKhachChuaDen
            // 
            this.grvKhachChuaDen.AllowUserToAddRows = false;
            this.grvKhachChuaDen.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.grvKhachChuaDen.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.grvKhachChuaDen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grvKhachChuaDen.EnableHeadersVisualStyles = false;
            this.grvKhachChuaDen.Location = new System.Drawing.Point(0, 0);
            this.grvKhachChuaDen.Name = "grvKhachChuaDen";
            this.grvKhachChuaDen.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grvKhachChuaDen.Size = new System.Drawing.Size(665, 189);
            this.grvKhachChuaDen.TabIndex = 0;
            // 
            // pnExport1
            // 
            this.pnExport1.Controls.Add(this.pnExportKhachChuaDen);
            this.pnExport1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnExport1.Location = new System.Drawing.Point(3, 205);
            this.pnExport1.Name = "pnExport1";
            this.pnExport1.Size = new System.Drawing.Size(665, 28);
            this.pnExport1.TabIndex = 0;
            // 
            // pnExportKhachChuaDen
            // 
            this.pnExportKhachChuaDen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnExportKhachChuaDen.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pnExportKhachChuaDen.Location = new System.Drawing.Point(597, 3);
            this.pnExportKhachChuaDen.Name = "pnExportKhachChuaDen";
            this.pnExportKhachChuaDen.Size = new System.Drawing.Size(67, 23);
            this.pnExportKhachChuaDen.TabIndex = 0;
            this.pnExportKhachChuaDen.Text = "Export";
            this.pnExportKhachChuaDen.UseVisualStyleBackColor = true;
            this.pnExportKhachChuaDen.Click += new System.EventHandler(this.pnExportKhachChuaDen_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "openFileDialog";
            this.openFileDialog.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog_FileOk);
            // 
            // FrmThongKeKhachBdTheoTinNhan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1032, 538);
            this.Controls.Add(this.pnDataResult);
            this.Controls.Add(this.pnDataExcelAndChart);
            this.Controls.Add(this.pnFunction);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FrmThongKeKhachBdTheoTinNhan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thống kê khách bảo dưỡng theo tin đã nhắn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmThongKeKhachBDTheoTinNhan_Load);
            this.pnFunction.ResumeLayout(false);
            this.pnFunction.PerformLayout();
            this.pnDataExcelAndChart.ResumeLayout(false);
            this.gbDataExcel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvDataExcel)).EndInit();
            this.gbChart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chartStatistic)).EndInit();
            this.pnDataResult.ResumeLayout(false);
            this.gbKhachDaDen.ResumeLayout(false);
            this.pnData.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvKhachDaDen)).EndInit();
            this.pnExport.ResumeLayout(false);
            this.gbKhachChuaDen.ResumeLayout(false);
            this.pnData1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grvKhachChuaDen)).EndInit();
            this.pnExport1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnFunction;
        private System.Windows.Forms.Panel pnDataExcelAndChart;
        private System.Windows.Forms.Panel pnDataResult;
        private System.Windows.Forms.GroupBox gbChart;
        private System.Windows.Forms.GroupBox gbDataExcel;
        private System.Windows.Forms.GroupBox gbKhachDaDen;
        private System.Windows.Forms.GroupBox gbKhachChuaDen;
        private System.Windows.Forms.DataGridView grvDataExcel;
        private System.Windows.Forms.Panel pnData;
        private System.Windows.Forms.Panel pnExport;
        private System.Windows.Forms.Panel pnExport1;
        private System.Windows.Forms.Panel pnData1;
        private System.Windows.Forms.Button btnExportKhachDaDen;
        private System.Windows.Forms.Button pnExportKhachChuaDen;
        private System.Windows.Forms.DataGridView grvKhachDaDen;
        private System.Windows.Forms.DataGridView grvKhachChuaDen;
        private System.Windows.Forms.Label lblChonTepExcel;
        private System.Windows.Forms.LinkLabel linkXemFileMau;
        private System.Windows.Forms.Button btnChooseFile;
        private System.Windows.Forms.TextBox txtPathExcel;
        private System.Windows.Forms.Button btnThongKe;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartStatistic;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.DateTimePicker dpkTuNgay;
        private System.Windows.Forms.Label lblTuNgay;
        private System.Windows.Forms.DateTimePicker dpkDenNgay;
        private System.Windows.Forms.Label lblDenNgay;
        private System.Windows.Forms.ComboBox cbbLoaiBaoDuong;
        private System.Windows.Forms.Label lblLoaiBaoDuong;

    }
}