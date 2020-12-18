using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls.KhachHang
{
    public partial class UcQuanLyKhachHangNew : UserControl
    {
        public UcQuanLyKhachHangNew()
        {
            InitializeComponent();
        }

        private void UcQuanLyKhachHangNew_Load(object sender, EventArgs e)
        {

        }

        private void RefreshData()
        {
            btnSearch_Click(null, new EventArgs());
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }

        private void btnAddCus_Click(object sender, EventArgs e)
        {
            FrmKhachHang frm = new FrmKhachHang {Refresh = new FrmKhachHang.ReloadData(RefreshData)};
            frm.ShowDialog();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable) grvData.DataSource;

                if (dt != null && dt.Rows.Count > 0)
                    Export(dt, "Danh sach", "DANH SÁCH KHÁCH HÀNG");
                else
                    MessageBox.Show("Không tồn tại thông tin khách hàng!\nVui lòng kiểm tra lại.", "Thông báo",
                        MessageBoxButtons.OK);
            }
            catch
            {
                //
            }
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
                Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "O1");
                head.MergeCells = true;
                head.Value2 = title;
                head.Font.Bold = true;
                head.Font.Name = "Tahoma";
                head.Font.Size = "18";
                head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Tạo tiêu đề cột
                Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
                cl1.Value2 = "Mã KH";
                cl1.ColumnWidth = 10.5;

                Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
                cl2.Value2 = "Họ KH";
                cl2.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
                cl3.Value2 = "Tên KH";
                cl3.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
                cl4.Value2 = "Giới tính";
                cl4.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
                cl5.Value2 = "Ngày sinh";
                cl5.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
                cl6.Value2 = "Điện thoại";
                cl6.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
                cl7.Value2 = "Địa chỉ";
                cl7.ColumnWidth = 25.0;

                Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
                cl8.Value2 = "CMND";
                cl8.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
                cl9.Value2 = "Ngày mua";
                cl9.ColumnWidth = 15;

                Microsoft.Office.Interop.Excel.Range cl10 = oSheet.get_Range("J3", "J3");
                cl10.Value2 = "Sổ BH";
                cl10.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl11 = oSheet.get_Range("K3", "K3");
                cl11.Value2 = "Tên xe";
                cl11.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl12 = oSheet.get_Range("L3", "L3");
                cl12.Value2 = "Biển số";
                cl12.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl13 = oSheet.get_Range("M3", "M3");
                cl13.Value2 = "Số khung";
                cl13.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl14 = oSheet.get_Range("N3", "N3");
                cl14.Value2 = "Số máy";
                cl14.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl15 = oSheet.get_Range("O3", "O3");
                cl15.Value2 = "Loại KH";
                cl15.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "O3");
                rowHead.Font.Bold = true;

                // Kẻ viền
                rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                // Thiết lập màu nền
                rowHead.Interior.ColorIndex = 15;
                rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Tạo mẳng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,
                // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.
                object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];

                //Chuyển dữ liệu từ DataTable vào mảng đối tượng
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    DataRow dr = dt.Rows[r];
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
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

        private void btnUpdateCuss_Click(object sender, EventArgs e)
        {
            if (grvData.SelectedRows.Count <= 0)
            {
                MessageBox.Show("Bạn chưa chọn khách hàng cần cập nhật thông tin!\nVui lòng kiểm tra lại.", @"Thông báo",
                    MessageBoxButtons.OK);
                return;
            }


        }
    }
}
