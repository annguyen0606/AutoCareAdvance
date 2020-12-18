using AutoCareV2._0.Class;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmPhuTungAlertDetail : Form
    {
        public delegate void StartTimer();

        private BackgroundWorker bgw = new BackgroundWorker();

        public StartTimer _StartTimer;
        public List<PhuTung> ListPhuTung;
        private static int TimeDelay = 0;

        #region frmPhuTungAlertDetail
        public frmPhuTungAlertDetail()
        {
            InitializeComponent();
            grvPhuTung.AutoGenerateColumns = false;
        }
        #endregion

        #region frmPhuTungAlertDetail_Load
        private void frmPhuTungAlertDetail_Load(object sender, EventArgs e)
        {
            if (ListPhuTung != null && ListPhuTung.Count > 0)
            {
                btnExport.Enabled = true;
                grvPhuTung.DataSource = ListPhuTung;

                //Đóng sau 2 phút
                TimeDelay = 120;
                timer.Start();
            }
        }
        #endregion

        #region frmPhuTungAlertDetail_FormClosed

        #endregion

        #region btnExport_Click
        private void btnExport_Click(object sender, EventArgs e)
        {
            btnExport.Enabled = false;
            timer.Stop();

            bgw.DoWork += bgw_DoWork;
            bgw.RunWorkerCompleted += bgw_RunWorkerCompleted;
            bgw.RunWorkerAsync();
        }
        #endregion

        #region bgw_RunWorkerCompleted
        private void bgw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }
        #endregion

        #region bgw_DoWork
        private void bgw_DoWork(object sender, DoWorkEventArgs e)
        {
            DataTable tablePhuTung = new DataTable();
            tablePhuTung = ConvertToDataTable(ListPhuTung);

            ExportToExcel(tablePhuTung, "DanhSachPhuTungCanNhap");
        }
        #endregion

        #region Export DataTable to Excel

        public static void ExportToExcel(DataTable tableData, string FileName)
        {
            Microsoft.Office.Interop.Excel.Application excel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbook workbook = excel.Application.Workbooks.Add(true);

            // Add column headings...
            int iCol = 0;
            foreach (DataColumn c in tableData.Columns)
            {
                iCol++;
                excel.Cells[1, iCol] = c.ColumnName;
            }
            // for each row of data...
            int iRow = 0;
            foreach (DataRow r in tableData.Rows)
            {
                iRow++;
                // add each row's cell data...
                iCol = 0;
                foreach (DataColumn c in tableData.Columns)
                {
                    iCol++;
                    excel.Cells[iRow + 1, iCol] = r[c.ColumnName];
                }
            }

            // Global missing reference for objects we are not defining...
            object missing = System.Reflection.Missing.Value;

            // If wanting to Save the workbook...
            workbook.SaveAs(FileName + ".xls",
                Microsoft.Office.Interop.Excel.XlFileFormat.xlXMLSpreadsheet, missing, missing,
                false, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                missing, missing, missing, missing, missing);

            // If wanting to make Excel visible and activate the worksheet...
            excel.Visible = true;
            Microsoft.Office.Interop.Excel.Worksheet worksheet = (Microsoft.Office.Interop.Excel.Worksheet)excel.ActiveSheet;
            ((Microsoft.Office.Interop.Excel._Worksheet)worksheet).Activate();

            //// If wanting excel to shutdown...
            //((Microsoft.Office.Interop.Excel._Application)excel).Quit();
        }

        #endregion Export DataTable to Excel

        #region Convert List To DataTable

        public DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));

            DataTable table = new DataTable();

            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);

            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;

                table.Rows.Add(row);
            }

            return table;
        }

        #endregion Convert List To DataTable

        #region btnClose_Click
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region timer_Tick
        private void timer_Tick(object sender, EventArgs e)
        {
            TimeDelay--;

            if (TimeDelay == 0)
            {
                timer.Stop();
                this.Close();
            }
        }
        #endregion

        private void frmPhuTungAlertDetail_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (_StartTimer != null)
                _StartTimer();
        }
    }
}