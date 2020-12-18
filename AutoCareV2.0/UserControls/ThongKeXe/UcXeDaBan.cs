using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls.ThongKeXe
{
    public partial class UcXeDaBan : UserControl
    {
        private DataTable dt = new DataTable("XeDaBan");

        public UcXeDaBan()
        {
            InitializeComponent();

            dataGridViewHoaDonBanXe.AutoGenerateColumns = false;
            dtgrvxedaban.AutoGenerateColumns = false;
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
                cl1.Value2 = "Số hóa đơn bán";
                cl1.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
                cl2.Value2 = "Ngày tạo hóa đơn";
                cl2.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
                cl3.Value2 = "Tên khách hàng";
                cl3.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
                cl4.Value2 = "Số điện thoại";
                cl4.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
                cl5.Value2 = "Địa chỉ";
                cl5.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
                cl6.Value2 = "Ngày sinh";
                cl6.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
                cl7.Value2 = "Tên xe";
                cl7.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
                cl8.Value2 = "Số khung";
                cl8.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
                cl9.Value2 = "Số Máy";
                cl9.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl10 = oSheet.get_Range("J3", "J3");
                cl10.Value2 = "Đơn giá";
                cl10.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl11 = oSheet.get_Range("K3", "K3");
                cl11.Value2 = "Ghi chú";
                cl11.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl12 = oSheet.get_Range("L3", "L3");
                cl12.Value2 = "Tên kho";
                cl12.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl13 = oSheet.get_Range("M3", "M3");
                cl13.Value2 = "Số chứng từ";
                cl13.ColumnWidth = 15.0;

                //Microsoft.Office.Interop.Excel.Range cl14 = oSheet.get_Range("N3", "N3");
                //cl14.Value2 = "LanBaoDuong";
                //cl14.ColumnWidth = 10.0;

                //Microsoft.Office.Interop.Excel.Range cl15 = oSheet.get_Range("O3", "O3");
                //cl15.Value2 = "GhiChu";
                //cl15.ColumnWidth = 20.0;

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

        private void ExportDanhSachXeDaBan(DataTable dt, string sheetName, string title)
        {
            try
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

                // Tạo phần đầu nếu muốn
                Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "R1");
                head.MergeCells = true;
                head.Value2 = title;
                head.Font.Bold = true;
                head.Font.Name = "Tahoma";
                head.Font.Size = "18";
                head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Tạo tiêu đề cột
                Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A6");
                cl1.MergeCells = true;
                cl1.Value2 = "STT";
                cl1.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cNgayBan = oSheet.get_Range("B3", "B6");
                cNgayBan.MergeCells = true;
                cNgayBan.Value2 = "NGÀY BÁN";
                cNgayBan.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("C3", "J3");
                cl2.MergeCells = true;
                cl2.Value2 = "THÔNG TIN XE";
                cl2.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cGia = oSheet.get_Range("C4", "C6");
                cGia.MergeCells = true;
                cGia.Value2 = "GIÁ";
                cGia.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cCachThanhToan = oSheet.get_Range("D4", "E4");
                cCachThanhToan.MergeCells = true;
                cCachThanhToan.Value2 = "CÁCH THANH TOÁN";
                cCachThanhToan.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cTienMat = oSheet.get_Range("D5", "D6");
                cTienMat.MergeCells = true;
                cTienMat.Value2 = "TIỀN MẶT";
                cTienMat.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cTraGop = oSheet.get_Range("E5", "E6");
                cTraGop.MergeCells = true;
                cTraGop.Value2 = "TRẢ GÓP";
                cTraGop.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cTenXeVaDangKy = oSheet.get_Range("F5", "F6");
                cTenXeVaDangKy.MergeCells = true;
                cTenXeVaDangKy.Value2 = "KIỂU XE";
                cTenXeVaDangKy.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cKieuXe = oSheet.get_Range("F4", "J4");
                cKieuXe.MergeCells = true;
                cKieuXe.Value2 = "TÊN XE VÀ ĐĂNG KÝ XE";
                cKieuXe.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cChiTiet = oSheet.get_Range("G5", "H5");
                cChiTiet.MergeCells = true;
                cChiTiet.Value2 = "CHI TIẾT";
                cChiTiet.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cLoai = oSheet.get_Range("G6", "G6");
                cLoai.Value2 = "LOẠI";
                cLoai.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cMau = oSheet.get_Range("H6", "H6");
                cMau.Value2 = "MẦU";
                cMau.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cSoKhung = oSheet.get_Range("I5", "I6");
                cSoKhung.MergeCells = true;
                cSoKhung.Value2 = "SỐ KHUNG";
                cSoKhung.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cSoMay = oSheet.get_Range("J5", "J6");
                cSoMay.MergeCells = true;
                cSoMay.Value2 = "SỐ MÁY";
                cSoMay.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cThongTinKhachHang = oSheet.get_Range("K3", "P3");
                cThongTinKhachHang.MergeCells = true;
                cThongTinKhachHang.Value2 = "THÔNG TIN KHÁCH HÀNG";
                cThongTinKhachHang.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cHoTen = oSheet.get_Range("K4", "K6");
                cHoTen.MergeCells = true;
                cHoTen.Value2 = "HỌ TÊN";
                cHoTen.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cGioiTinh = oSheet.get_Range("L4", "L6");
                cGioiTinh.MergeCells = true;
                cGioiTinh.Value2 = "GIỚI TÍNH";
                cHoTen.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cNgheNghiep = oSheet.get_Range("M4", "M6");
                cNgheNghiep.MergeCells = true;
                cNgheNghiep.Value2 = "NGHỀ NGHIỆP";
                cNgheNghiep.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cNgaySinh = oSheet.get_Range("N4", "N6");
                cNgaySinh.MergeCells = true;
                cNgaySinh.Value2 = "NGÀY SINH";
                cNgaySinh.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cDiaChiLienLac = oSheet.get_Range("O4", "P4");
                cDiaChiLienLac.MergeCells = true;
                cDiaChiLienLac.Value2 = "ĐỊA CHỈ LIÊN LAC";
                cDiaChiLienLac.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cDiaChi = oSheet.get_Range("O5", "O6");
                cDiaChi.MergeCells = true;
                cDiaChi.Value2 = "ĐỊA CHỈ";
                cDiaChi.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cDienThoai = oSheet.get_Range("P5", "P6");
                cDienThoai.MergeCells = true;
                cDienThoai.Value2 = "ĐIỆN THOẠI";
                cDienThoai.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cNhanVienBan = oSheet.get_Range("Q3", "Q6");
                cNhanVienBan.MergeCells = true;
                cNhanVienBan.Value2 = "NHÂN VIÊN BÁN";
                cNhanVienBan.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cSoHoaDon = oSheet.get_Range("R3", "R6");
                cSoHoaDon.MergeCells = true;
                cSoHoaDon.Value2 = "SỐ HÓA ĐƠN";
                cSoHoaDon.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "R6");
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
                        if (c == dt.Columns.Count - 1 || dt.Columns.Contains("DienThoai") || dt.Columns.Contains("SoKhung") || dt.Columns.Contains("SoMay"))
                            arr[r, c] = "'" + dr[c];
                        else
                            arr[r, c] = dr[c];
                    }
                }

                //Thiết lập vùng điền dữ liệu
                int rowStart = 7;
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
                Microsoft.Office.Interop.Excel.Range ngayBanStart = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, 2];
                Microsoft.Office.Interop.Excel.Range ngayBanEnd = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, 2];
                Microsoft.Office.Interop.Excel.Range rangeNgayMua = oSheet.get_Range(ngayBanStart, ngayBanEnd);
                rangeNgayMua.NumberFormat = "DD/MM/YYYY";

                //Định dạng dữ liệu
                Microsoft.Office.Interop.Excel.Range ngaySinhStart = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, 14];
                Microsoft.Office.Interop.Excel.Range ngaySinhnEnd = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, 14];
                Microsoft.Office.Interop.Excel.Range rangeNgaySinh = oSheet.get_Range(ngaySinhStart, ngaySinhnEnd);
                rangeNgaySinh.NumberFormat = "DD/MM/YYYY";

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

        public void FindLichSuBanXe()
        {
            dtgrvxedaban.DataSource = null;

            if (dtFrom.Value > dtTo.Value)
            {
                MessageBox.Show("Ngày không hợp lệ! \nNgày trước không được lớn hơn ngày sau.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dtFrom.Focus();

                return;
            }

            try
            {
                SqlDataAdapter da = new SqlDataAdapter();

                using (SqlConnection con = new SqlConnection(Class.datatabase.connect))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;

                    if (radioButtonTatCa.Checked == true)
                    {
                        cmd.CommandText = @"Select DISTINCT HoaDonBanHang.SoHoaDonBanHang, HoaDonBanHang.SoChungTu, HoaDonBanHang.NgayTaoHoaDon, KhachHang.TenKH,
                                            (select SUM(DonGia) + SUM(isnull(VAT,0)) from ChiTietHoaDonBan where SoHoaDonBanHang=HoaDonBanHang.SoHoaDonBanHang)
                                            AS TongTien, PhieuThu.SoTienThu AS TienDaTra, (select SUM(DonGia) + SUM(isnull(VAT,0)) from ChiTietHoaDonBan where SoHoaDonBanHang=HoaDonBanHang.SoHoaDonBanHang) - PhieuThu.SoTienThu
                                            AS CongNo, NhanVien.TenNhanVien
                                            from HoaDonBanHang inner join ChiTietHoaDonBan on HoaDonBanHang.SoHoaDonBanHang=ChiTietHoaDonBan.SoHoaDonBanHang
                                            inner join PhieuThu on PhieuThu.SoHoaDon=HoaDonBanHang.SoHoaDonBanHang
                                            inner join KhachHang on HoaDonBanHang.IdKhachHang=KhachHang.IdKhachHang
                                            inner join NhanVien on HoaDonBanHang.IdNhanVien=NhanVien.IdNhanVien
                                            where HoaDonBanHang.IdCongTy = @IdCongTy and HoaDonBanHang.NgayTaoHoaDon between Convert(date,@BatDau) And Convert(date,@KetThuc)";
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@BatDau", dtFrom.Value.Date);
                        cmd.Parameters.AddWithValue("@KetThuc", dtTo.Value.Date.AddDays(1));

                        da.SelectCommand = cmd;
                        dt.Clear();
                        da.Fill(dt);
                    }
                    else if (radioButtonTraDu.Checked == true)
                    {
                        cmd.CommandText = @"Select DISTINCT HoaDonBanHang.SoHoaDonBanHang, HoaDonBanHang.SoChungTu, HoaDonBanHang.NgayTaoHoaDon, KhachHang.TenKH,
                                            (select SUM(DonGia) + SUM(isnull(VAT,0)) from ChiTietHoaDonBan where SoHoaDonBanHang=HoaDonBanHang.SoHoaDonBanHang)
                                            AS TongTien, PhieuThu.SoTienThu AS TienDaTra, (select SUM(DonGia) + SUM(isnull(VAT,0)) from ChiTietHoaDonBan where SoHoaDonBanHang=HoaDonBanHang.SoHoaDonBanHang) - PhieuThu.SoTienThu
                                            AS CongNo, NhanVien.TenNhanVien
                                            from HoaDonBanHang inner join ChiTietHoaDonBan on HoaDonBanHang.SoHoaDonBanHang=ChiTietHoaDonBan.SoHoaDonBanHang
                                            inner join PhieuThu on PhieuThu.SoHoaDon=HoaDonBanHang.SoHoaDonBanHang
                                            inner join KhachHang on HoaDonBanHang.IdKhachHang=KhachHang.IdKhachHang
                                            inner join NhanVien on HoaDonBanHang.IdNhanVien=NhanVien.IdNhanVien
                                            where HoaDonBanHang.IdCongTy = @IdCongTy and HoaDonBanHang.NgayTaoHoaDon between Convert(date,@BatDau) And Convert(date,@KetThuc)
                                            and ChiTietHoaDonBan.SoHoaDonBanHang IN
                                            (select SoHoaDon from PhieuThu where SoTienThu >= (select SUM(DonGia) + SUM(isnull(VAT,0)) from ChiTietHoaDonBan where SoHoaDonBanHang=HoaDonBanHang.SoHoaDonBanHang))";
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@BatDau", dtFrom.Value);
                        cmd.Parameters.AddWithValue("@KetThuc", dtTo.Value);

                        da.SelectCommand = cmd;
                        dt.Clear();
                        da.Fill(dt);
                    }
                    else if (radioButtonTraThieu.Checked == true)
                    {
                        cmd.CommandText = @"Select DISTINCT HoaDonBanHang.SoHoaDonBanHang, HoaDonBanHang.SoChungTu, HoaDonBanHang.NgayTaoHoaDon, KhachHang.TenKH,
                                            (select SUM(DonGia) + SUM(isnull(VAT,0)) from ChiTietHoaDonBan where SoHoaDonBanHang=HoaDonBanHang.SoHoaDonBanHang)
                                            AS TongTien, PhieuThu.SoTienThu AS TienDaTra, (select SUM(DonGia) + SUM(isnull(VAT,0)) from ChiTietHoaDonBan where SoHoaDonBanHang=HoaDonBanHang.SoHoaDonBanHang) - PhieuThu.SoTienThu
                                            AS CongNo, NhanVien.TenNhanVien
                                            from HoaDonBanHang inner join ChiTietHoaDonBan on HoaDonBanHang.SoHoaDonBanHang=ChiTietHoaDonBan.SoHoaDonBanHang
                                            inner join PhieuThu on PhieuThu.SoHoaDon=HoaDonBanHang.SoHoaDonBanHang
                                            inner join KhachHang on HoaDonBanHang.IdKhachHang=KhachHang.IdKhachHang
                                            inner join NhanVien on HoaDonBanHang.IdNhanVien=NhanVien.IdNhanVien
                                            where HoaDonBanHang.IdCongTy = @IdCongTy and HoaDonBanHang.NgayTaoHoaDon between Convert(date,@BatDau) And Convert(date,@KetThuc)
                                            and ChiTietHoaDonBan.SoHoaDonBanHang IN
                                            (select SoHoaDon from PhieuThu where SoTienThu < (select SUM(DonGia) from ChiTietHoaDonBan where SoHoaDonBanHang=HoaDonBanHang.SoHoaDonBanHang))";
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@BatDau", dtFrom.Value);
                        cmd.Parameters.AddWithValue("@KetThuc", dtTo.Value);

                        da.SelectCommand = cmd;
                        dt.Clear();
                        da.Fill(dt);
                    }

                    if (dt.Rows.Count > 0)
                    {
                        dataGridViewHoaDonBanXe.DataSource = null;
                        dataGridViewHoaDonBanXe.DataSource = dt;

                        txtTongSoXe.Text = dt.Rows.Count.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Không tồn tại danh sách xe đã bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    con.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FindLichSuBanXe();
        }

        private void XeDaBan_Load(object sender, EventArgs e)
        {
            radioButtonTatCa.Checked = true;
            dtFrom.Value = DateTime.Now;
            dtTo.Value = DateTime.Now;
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            FindLichSuBanXe();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                #region Cũ - 20170119

                //DataTable dsXe = new DataTable();

                //if (dataGridViewHoaDonBanXe.DataSource == null)
                //{
                //    MessageBox.Show("Chưa có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //    return;
                //}
                //else
                //{
                //    using (SqlConnection cnn = Class.datatabase.getConnection())
                //    {
                //        cnn.Open();

                //        for (int i = 0; i < dataGridViewHoaDonBanXe.Rows.Count; i++)
                //        {
                //            string SoHoaDon = dataGridViewHoaDonBanXe.Rows[i].Cells[0].Value.ToString();

                //            SqlDataAdapter adap = new SqlDataAdapter(@"SELECT DISTINCT hd.SoHoaDonBanHang, CONVERT(nchar(10), hd.NgayTaoHoaDon, 103) AS NgayTaoHoaDon, kh.TenKH, kh.DienThoai, kh.Diachi,
                //                                                       CONVERT(nchar(10), kh.NgaySinh, 103) AS NgaySinh, xm.TenXe, ctx.SoKhung, ctx.SoMay, ctx.DonGia, ctx.GhiChu, k.TenKho, ctx.SoChungTu
                //                                                       FROM ChiTietHoaDonBan ctx, HoaDonBanHang hd, KhoHang k, XeMay xm, KhachHang kh WHERE hd.IdCongTy=@IdCongTy
                //                                                       AND hd.SoHoaDonBanHang=ctx.SoHoaDonBanHang AND k.IdKho=ctx.IdKho AND kh.IdKhachHang=hd.IdKhachHang
                //                                                       AND xm.IDXe=ctx.IDXe AND hd.SoHoaDonBanHang=@SoHoaDonBanHang", cnn);
                //            adap.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                //            adap.SelectCommand.Parameters.AddWithValue("@SoHoaDonBanHang", SoHoaDon);

                //            adap.Fill(dsXe);
                //        }

                //        cnn.Close();
                //    }
                //}
                //if (dt.Rows.Count > 0)
                //{
                //    Export(dsXe, "DanhSachXe", "Danh sách xe đã bán");
                //}

                #endregion Cũ - 20170119

                #region Mới - 20170119

                DataTable dsXe = new DataTable();

                if (dataGridViewHoaDonBanXe.DataSource == null)
                {
                    MessageBox.Show(@"Chưa có dữ liệu!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    SqlCommand cmdGetDataExport = new SqlCommand("sp_Export_KhachHang_MuaXe")
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    cmdGetDataExport.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmdGetDataExport.Parameters.AddWithValue("@TuNgay", dtFrom.Value.Date.Add(new TimeSpan(0,0,0)));
                    cmdGetDataExport.Parameters.AddWithValue("@DenNgay", dtTo.Value.Date.Add(new TimeSpan(23, 59, 59)));

                    DataSet dsDataXe = Class.datatabase.GetDataSet(cmdGetDataExport);

                    if (dsDataXe != null && dsDataXe.Tables.Count > 0)
                    {
                        if (dsDataXe.Tables[0] != null && dsDataXe.Tables[0].Rows.Count > 0)
                            ExportDanhSachXeDaBan(dsDataXe.Tables[0], "Danh Sach Khach Mua Xe", "DANH SÁCH KHÁCH MUA XE TỪ NGÀY: " + dtFrom.Value.ToString("dd/MM/yyyy") + " ĐẾN NGÀY: " + dtTo.Value.ToString("dd/MM/yyyy"));
                    }
                }
                #endregion Mới - 20170119
            }
            catch (Exception ex) { MessageBox.Show(@"Lỗi: " + ex.Message); }
        }

        private void dataGridViewHoaDonBanXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string soHD = dataGridViewHoaDonBanXe.Rows[e.RowIndex].Cells[0].Value.ToString();

                using (SqlConnection cnn = Class.datatabase.getConnection())
                {
                    cnn.Open();

                    SqlDataAdapter adap =
                        new SqlDataAdapter(
                            @"SELECT hd.SoHoaDonBanHang, xm.TenXe, ctx.SoKhung, ctx.SoMay, ctx.DonGia, ctx.GhiChu, k.TenKho, ctx.SoChungTu
                            FROM ChiTietHoaDonBan ctx, HoaDonBanHang hd, KhoHang k, XeMay xm WHERE hd.IdCongTy=@IdCongTy
                            AND hd.SoHoaDonBanHang=ctx.SoHoaDonBanHang AND k.IdKho=ctx.IdKho AND xm.IDXe=ctx.IDXe AND hd.SoHoaDonBanHang=@SoHoaDonBanHang",
                            cnn);

                    adap.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    adap.SelectCommand.Parameters.AddWithValue("@SoHoaDonBanHang", soHD);

                    DataTable dt = new DataTable();
                    adap.Fill(dt);

                    dtgrvxedaban.DataSource = null;
                    dtgrvxedaban.DataSource = dt;

                    cnn.Close();
                }
                txtTongSoXe.Text = dtgrvxedaban.Rows.Count.ToString();
            }
            catch
            {
                //
            }
        }
    }
}