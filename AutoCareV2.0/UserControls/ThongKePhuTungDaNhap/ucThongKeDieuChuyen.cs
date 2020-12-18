using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using AutoCareV2._0.Class;

namespace AutoCareV2._0.UserControls.ThongKePhuTungDaNhap
{
    public partial class ucThongKeDieuChuyen : UserControl
    {
        int RowIndex = -1;

        public ucThongKeDieuChuyen()
        {
            InitializeComponent();
        }

        private void ucThongKeDieuChuyen_Load(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string date_from = "";
            string date_to = "";

            if (!String.IsNullOrEmpty(dpkFrom.Text))
                date_from = dpkFrom.Value.ToString("MM/dd/yyyy");
            if (!String.IsNullOrEmpty(dpkTo.Text))
                date_to = dpkTo.Value.ToString("MM/dd/yyyy");

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_transfer_statistic_sparepart";
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@company_id", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@date_from", date_from);
                cmd.Parameters.AddWithValue("@date_to", date_to);

                DataTable tableResult = Class.datatabase.getData(cmd);
                grvDanhSachDieuChuyen.DataSource = tableResult;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if(grvDanhSachDieuChuyen.Rows.Count <= 0)
            {
                MessageBox.Show("Không tồn tại danh sách điều chuyển phụ tùng!\nVui lòng kiểm tra lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                DataTable tableExport = new DataTable();
                tableExport.Columns.Add(new DataColumn("STT", typeof(string)));
                tableExport.Columns.Add(new DataColumn("TenKhoXuat", typeof(string)));
                tableExport.Columns.Add(new DataColumn("TenKhoNhap", typeof(string)));
                tableExport.Columns.Add(new DataColumn("MaPT", typeof(string)));
                tableExport.Columns.Add(new DataColumn("TenPT", typeof(string)));
                tableExport.Columns.Add(new DataColumn("SoLuong", typeof(string)));
                tableExport.Columns.Add(new DataColumn("NgayDieuChuyen", typeof(string)));

                foreach(DataGridViewRow row in grvDanhSachDieuChuyen.Rows)
                {
                    DataRow drExport = tableExport.NewRow();
                    drExport["STT"] = row.Cells["RowNumber"].Value.ToString();
                    drExport["TenKhoXuat"] = row.Cells["TenKhoXuat"].Value.ToString();
                    drExport["TenKhoNhap"] = row.Cells["TenKhoNhap"].Value.ToString();
                    drExport["MaPT"] = row.Cells["Ma_PT_Xuat"].Value.ToString();
                    drExport["TenPT"] = row.Cells["Ten_PT_Xuat"].Value.ToString();
                    drExport["SoLuong"] = row.Cells["SoLuongXuat"].Value.ToString();
                    drExport["NgayDieuChuyen"] = row.Cells["NgayXuat"].Value.ToString();

                    tableExport.Rows.Add(drExport);
                }

                DateTime date = DateTime.Now;
                string fileName = "DanhSachDieuChuyen" + date.Year + date.Month + date.Day + date.Hour + date.Minute + date.Second;
                
                //Xuất danh sách
                ExportToExcel(tableExport, fileName);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        #region Export DataTable to Excel
        private void ExportToExcel(DataTable tableData, string FileName)
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
        #endregion

        private void XoaDieuChuyenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RowIndex != -1)
            {
                if (Class.EmployeeInfo.Quyen.ToLower() != "qtv")
                {
                    MessageBox.Show("Bạn không có quyền xóa điều chuyển!\nVui lòng kiểm tra lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Bạn có muốn xóa lệnh điều chuyển đã chọn?", "Xóa điều chuyển?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        string IdKhoNhap = grvDanhSachDieuChuyen.Rows[RowIndex].Cells["IdKhoNhap"].Value.ToString();
                        string IdKhoXuat = grvDanhSachDieuChuyen.Rows[RowIndex].Cells["IdKhoXuat"].Value.ToString();
                        string MaPT = grvDanhSachDieuChuyen.Rows[RowIndex].Cells["Ma_PT_Xuat"].Value.ToString();
                        string SoLuong = grvDanhSachDieuChuyen.Rows[RowIndex].Cells["SoLuongXuat"].Value.ToString();
                        string NgayXuat = DateTime.Parse(grvDanhSachDieuChuyen.Rows[RowIndex].Cells["NgayXuat"].Value.ToString()).ToString("yyyyMMdd");

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "sp_delete_transfer_sparepart";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdKhoNhap", IdKhoNhap);
                            cmd.Parameters.AddWithValue("@IdKhoXuat", IdKhoXuat);
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@NgayXuat", NgayXuat);
                            cmd.Parameters.AddWithValue("@MaPT", MaPT);
                            cmd.Parameters.AddWithValue("@SoLuong", SoLuong);

                            int j = datatabase.ExcuteNonQuery(cmd);

                            btnTimKiem_Click(sender, new EventArgs());
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
                }
            }
        }

        private void grvDanhSachDieuChuyen_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                grvDanhSachDieuChuyen.ContextMenuStrip = contextMenuStrip;

                try
                {
                    RowIndex = grvDanhSachDieuChuyen.CurrentRow.Index;
                }
                catch (Exception ex)
                {
                    RowIndex = -1;
                }
            }
            else
            {
                grvDanhSachDieuChuyen.ContextMenuStrip = null;
                RowIndex = -1;
            }
        }
    }
}
