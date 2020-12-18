using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using Excel = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using AutoCareV2._0.Class;
namespace AutoCareV2._0
{
    public partial class ExcelNhapXe : Form
    {
        public ExcelNhapXe()
        {
            InitializeComponent();
        }
        string path;
       public DataTable dt = new DataTable();
        Excel.Application appExcel;
        Excel.Workbook workbookExcel;
        private void btnChonFile_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog chon = new OpenFileDialog();
                chon.Filter = "Excel File| *.xls;*.xlsx";
                if (chon.ShowDialog() == DialogResult.OK)
                {
                    path = chon.FileName;
                    txtPath.Text = path;
                    appExcel = new Excel.Application();
                    workbookExcel = appExcel.Workbooks.Open(path);
                    cboChonSheet.Items.Clear();
                    foreach (Excel.Worksheet sheets in workbookExcel.Sheets)
                    {
                        cboChonSheet.Items.Add(sheets.Name);
                    }
                    workbookExcel.Close();
                }
            }
            catch { }
        }
        KhDB classdb = new KhDB();
        DataTable dtXeNhap = new DataTable();
        private void cboChonSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            dtXeNhap = classdb.SelectDataFromExcel(cboChonSheet.SelectedItem.ToString(),path);
            cboDonGia.Items.Clear();
        
            cboMauXe.Items.Clear();
            cboSoKhung.Items.Clear();
            cboSoMay.Items.Clear();
            cboTenXe.Items.Clear();
            foreach (DataColumn cl in dtXeNhap.Columns)
            {
                
                cboDonGia.Items.Add(cl.ColumnName);
           
                cboMauXe.Items.Add(cl.ColumnName);
                cboSoKhung.Items.Add(cl.ColumnName);
                cboSoMay.Items.Add(cl.ColumnName);
                cboTenXe.Items.Add(cl.ColumnName);
            }
            if (cboChonSheet.SelectedIndex == -1)
            {
                btnImport.Enabled = false;
            }
            else
                btnImport.Enabled = true;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            dt = new DataTable();
            dt.Columns.Add("TenXe");
            dt.Columns.Add("MauXe");
            dt.Columns.Add("SoKhung");
            dt.Columns.Add("SoMay");
            dt.Columns.Add("KhoNhap");
            dt.Columns.Add("DonGia", typeof(decimal));
            DataRow dr = dt.NewRow();
            foreach (DataRow dr1 in dtXeNhap.Rows)
            {
                try
                {
                    dr["TenXe"] = dr1[Convert.ToString(cboTenXe.SelectedItem)];
                }
                catch { }
                try
                {
                    dr["MauXe"] = dr1[Convert.ToString(cboMauXe.SelectedItem)];
                }
                catch { }
                try
                {
                    dr["SoKhung"] = dr1[Convert.ToString(cboSoKhung.SelectedItem)];
                }
                catch { }
                try
                {
                    dr["SoMay"] = dr1[Convert.ToString(cboSoMay.SelectedItem)];
                }
                catch { }          
                try
                {
                    dr["DonGia"] = dr1[Convert.ToString(cboDonGia.SelectedItem)];
                }
                catch { }
                
                dt.Rows.Add(dr);
                dr = dt.NewRow();
                
            }
            DialogResult = DialogResult.OK;
        }
    }
}
