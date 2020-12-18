using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Windows.Forms.VisualStyles;
using Microsoft.Vbe.Interop;

namespace AutoCareV2._0
{
    public partial class FrmThongKeKhachBdTheoTinNhan : Form
    {
        #region variables

        private DataTable _tableDataExcel;
        private DataTable _tableKhachDaDen;
        private DataTable _tableKhachChuaDen;
        #endregion

        public FrmThongKeKhachBdTheoTinNhan()
        {
            InitializeComponent();
        }

        private void FrmThongKeKhachBDTheoTinNhan_Load(object sender, EventArgs e)
        {
            _tableDataExcel = new DataTable();
        }

        private void btnChooseFile_Click(object sender, EventArgs e)
        {
            openFileDialog.ShowDialog();
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
            string path = openFileDialog.FileName;
            txtPathExcel.Text = path;
            const string hdr = "Yes";
            string strConn;
            if (path.Substring(path.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=" + hdr + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=" + hdr + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();

            OleDbDataAdapter cmd = new OleDbDataAdapter("select * from [Sheet1$]", strConn);
            cmd.TableMappings.Add("Table", "TestTable");
            DataSet ds = new DataSet();
            cmd.Fill(ds);

            _tableDataExcel = ds.Tables[0];
            grvDataExcel.DataSource = _tableDataExcel;
            gbDataExcel.Text = @"Dữ liệu file Excel (" + _tableDataExcel.Rows.Count + @" dòng)";
            conn.Close();
        }

        /// <summary>
        /// Handles the LinkClicked event of the linkXemFileMau control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LinkLabelLinkClickedEventArgs"/> instance containing the event data.</param>
        private void linkXemFileMau_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string pathFile = Environment.CurrentDirectory + "\\excel_template_statistic.xls";

            var attributes = File.GetAttributes(pathFile);
            File.SetAttributes(pathFile, attributes | FileAttributes.ReadOnly);
            System.Diagnostics.Process.Start(pathFile);
            File.SetAttributes(pathFile, attributes);
        }
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (_tableDataExcel == null)
            {
                MessageBox.Show("Bạn chưa mở tệp Excel!\nVui lòng kiểm tra lại.", @"Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (_tableDataExcel.Rows.Count <= 0)
            {
                MessageBox.Show("Nội dung file Excel không chứa dữ liệu!\nVui lòng kiểm tra lại.", @"Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            } 
            if (cbbLoaiBaoDuong.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn Loại bảo dưỡng!\nVui lòng kiểm tra lại.", @"Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (String.IsNullOrEmpty(dpkTuNgay.Text) || String.IsNullOrEmpty(dpkDenNgay.Text))
            {
                MessageBox.Show("Bạn chưa chọn khoảng thời gian!\nVui lòng kiểm tra lại.", @"Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandText = "sp_LayDanhSachKHDaNhanTinBD",
                    CommandType = CommandType.StoredProcedure
                };

                cmd.Parameters.AddWithValue("@loaiBD", cbbLoaiBaoDuong.SelectedIndex);
                cmd.Parameters.AddWithValue("@idCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@tuNgay", dpkTuNgay.Value.ToString("yyyyMMdd") + " 00:00:00");
                cmd.Parameters.AddWithValue("@denNgay", dpkDenNgay.Value.ToString("yyyyMMdd") + " 23:59:59");

                DataTable tableKhachHang = Class.datatabase.getData(cmd);

                if (tableKhachHang != null && tableKhachHang.Rows.Count > 0)
                {
                    _tableKhachDaDen = tableKhachHang.Clone();
                    _tableKhachChuaDen = tableKhachHang.Clone();

                    foreach (DataRow row in tableKhachHang.Rows)
                    {
                        //DataRow[] dataRows = _tableDataExcel.Select("SDT = '" + row["DienThoai"].ToString().Trim()
                        //                                            + "' AND (BienSo = '" +
                        //                                            row["BienSo"].ToString().Trim()
                        //                                            + "' OR SoKhung = '" +
                        //                                            row["SoKhung"].ToString().Trim()
                        //                                            + "' OR SoMay = '" + row["SoMay"].ToString().Trim() + "')");

                        //tienlm - update - chỉ check theo số điện thoại
                        DataRow[] dataRows = _tableDataExcel.Select("SDT = '" + row["DienThoai"].ToString().Trim() + "'");

                        if (dataRows.Any())
                            _tableKhachDaDen.ImportRow(row);
                        else
                            _tableKhachChuaDen.ImportRow(row);
                    }

                    grvKhachDaDen.DataSource = _tableKhachDaDen;
                    grvKhachChuaDen.DataSource = _tableKhachChuaDen;

                    gbKhachDaDen.Text = @"Khách đã đến bảo dưỡng (" + _tableKhachDaDen.Rows.Count + @" khách hàng)";
                    gbKhachChuaDen.Text = @"Khách đã đến bảo dưỡng (" + _tableKhachChuaDen.Rows.Count + @" khách hàng)";

                    DataTable tableStatistic = new DataTable();
                    tableStatistic.Columns.Add(new DataColumn("Ten", typeof (string)));
                    tableStatistic.Columns.Add(new DataColumn("GiaTri", typeof (int)));

                    DataRow drDaDen = tableStatistic.NewRow();
                    drDaDen["Ten"] = "Khách đã đến";
                    drDaDen["GiaTri"] = _tableKhachDaDen.Rows.Count;
                    tableStatistic.Rows.Add(drDaDen);

                    DataRow drChuaDen = tableStatistic.NewRow();
                    drChuaDen["Ten"] = "Khách chưa đến";
                    drChuaDen["GiaTri"] = _tableKhachChuaDen.Rows.Count;
                    tableStatistic.Rows.Add(drChuaDen);

                    chartStatistic.DataSource = tableStatistic;

                    chartStatistic.ChartAreas["ChartArea1"].AxisX.Title = "Loại";
                    chartStatistic.ChartAreas["ChartArea1"].AxisY.Title = "Số kh";
                    chartStatistic.Series["Series1"].XValueMember = "Ten";
                    chartStatistic.Series["Series1"].YValueMembers = "GiaTri";
                }
                else
                    MessageBox.Show(@"Không tìm thấy thông tin khách hàng!", @"Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Lỗi: " + exception.Message, @"Thông báo lỗi", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void btnExportKhachDaDen_Click(object sender, EventArgs e)
        {
            if(_tableKhachDaDen != null && _tableKhachDaDen.Rows.Count > 0)
                Export(_tableKhachDaDen, "KhachDaDen", "Danh sách khách đã đến");
        }

        private void pnExportKhachChuaDen_Click(object sender, EventArgs e)
        {
            if (_tableKhachChuaDen != null && _tableKhachChuaDen.Rows.Count > 0)
                Export(_tableKhachChuaDen, "KhachChuaDen", "Danh sách khách chưa đến");
        }

        private void Export(DataTable dt, string sheetName, string title)
        {
            //Tạo các đối tượng Excel
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetName;
            try
            {
                // Tạo phần đầu nếu muốn
                Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "H1");
                head.MergeCells = true;
                head.Value2 = title;
                head.Font.Bold = true;
                head.Font.Name = "Tahoma";
                head.Font.Size = "18";
                head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Tạo tiêu đề cột
                Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
                cl1.Value2 = "Tên khách hàng";
                cl1.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
                cl2.Value2 = "Giới tính";
                cl2.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
                cl3.Value2 = "Điện Thoại";
                cl3.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
                cl4.Value2 = "Tên xe";
                cl4.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
                cl5.Value2 = "Biển số";
                cl5.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
                cl6.Value2 = "Số khung";
                cl6.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
                cl7.Value2 = "Số máy";
                cl7.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
                cl8.Value2 = "Ngày mua";
                cl8.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "H3");
                rowHead.Font.Bold = true;

                // Kẻ viền
                rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                // Thiết lập màu nền
                rowHead.Interior.ColorIndex = 15;
                rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Tạo mẳng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,
                // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.
                string[,] arr = new string[dt.Rows.Count, dt.Columns.Count];

                //Chuyển dữ liệu từ DataTable vào mảng đối tượng
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    DataRow dr = dt.Rows[r];
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c].ToString();
                    }
                }


                //Thiết lập vùng điền dữ liệu
                int rowStart = 4;
                int columnStart = 1;

                int rowEnd = rowStart + dt.Rows.Count - 1;
                int columnEnd = dt.Columns.Count;

                // Ô bắt đầu điền dữ liệu
                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
                // Ô kết thúc điền dữ liệu
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
                // Lấy về vùng điền dữ liệu
                Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

                //Định dạng dữ liệu
                Microsoft.Office.Interop.Excel.Range cc1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, 8];
                Microsoft.Office.Interop.Excel.Range cc2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, 8];
                Microsoft.Office.Interop.Excel.Range rangeNgayMua = oSheet.get_Range(cc1, cc2);
                rangeNgayMua.NumberFormat = "DD/MM/YYYY";

                //Điền dữ liệu vào vùng đã thiết lập
                range.Value2 = arr;

                // Kẻ viền
                range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                // Căn giữa cột STT
                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];
                Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);
                oSheet.get_Range(c3, c4).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }
        }
    }
}
