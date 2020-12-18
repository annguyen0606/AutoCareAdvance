using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutoCareV2._0.UserControls.KhachHang
{
    public partial class UcQuanLyKhachHang : UserControl
    {
        private string cn = Class.datatabase.connect;
        private DataTable dt = new DataTable("KhachHang");
        private DataTable dtcty = new DataTable("CongTy");
        private DataTable dtnkh = new DataTable("NhomKH");
        private SqlDataAdapter da = new SqlDataAdapter();
        private static int pagesize = 20;
        private SqlConnection con;

        public UcQuanLyKhachHang()
        {
            InitializeComponent();
            panelEx1.HorizontalScroll.Enabled = true;
            panelEx1.HorizontalScroll.Visible = true;
            panelEx1.VerticalScroll.Visible = true;
            panelEx1.VerticalScroll.Enabled = true;
        }

        private void connect()
        {
            try
            {
                con = new SqlConnection(cn);
                con.Open();
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void getdata()
        {
            connect();
            SqlCommand cmd = new SqlCommand("Select TenNhomKH,MaNhomKH from NhomKhachHang  where idcongty=" + Class.CompanyInfo.idcongty, con);
            DataTable dc = new DataTable();
            dc.Columns.Add("TenNhomKH");
            dc.Columns.Add("MaNhomKH");

            DataRow r = dc.NewRow(); r[0] = "Tất cả nhóm"; r[1] = "";
            dc.Rows.Add(r);

            using (SqlDataReader rd = cmd.ExecuteReader())
            {
                while (rd.Read())
                {
                    r = dc.NewRow();
                    r[0] = rd[0].ToString();
                    r[1] = rd[1].ToString();
                    dc.Rows.Add(r);
                }
            }
            con.Close();
            con.Dispose();
            cbonhomkh.DataSource = dc;
            cbonhomkh.ValueMember = "MaNhomKH";
            cbonhomkh.DisplayMember = "TenNhomKH";
        }

        private void Load_cbbSoKH()
        {
            cbb_SoKHpage.Items.Add("20");
            cbb_SoKHpage.Items.Add("50");
            cbb_SoKHpage.Items.Add("100");
            cbb_SoKHpage.Items.Add("200");
            cbb_SoKHpage.Items.Add("Tất cả");
        }

        #region Xuất Excel

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
                cl2.Value2 = "Mã KH";
                cl2.ColumnWidth = 10.5;

                Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
                cl3.Value2 = "Họ KH";
                cl3.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
                cl4.Value2 = "Tên KH";
                cl4.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
                cl5.Value2 = "Giới tính";
                cl5.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
                cl6.Value2 = "Ngày sinh";
                cl6.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
                cl7.Value2 = "Điện thoại";
                cl7.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
                cl8.Value2 = "Địa chỉ";
                cl8.ColumnWidth = 25.0;

                Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
                cl9.Value2 = "CMND";
                cl9.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl10 = oSheet.get_Range("J3", "J3");
                cl10.Value2 = "Ngày mua";
                cl10.ColumnWidth = 15;

                Microsoft.Office.Interop.Excel.Range cl11 = oSheet.get_Range("K3", "K3");
                cl11.Value2 = "Sổ BH";
                cl11.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl12 = oSheet.get_Range("L3", "L3");
                cl12.Value2 = "Tên xe";
                cl12.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl13 = oSheet.get_Range("M3", "M3");
                cl13.Value2 = "Biển số";
                cl13.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl14 = oSheet.get_Range("N3", "N3");
                cl14.Value2 = "Số khung";
                cl14.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl15 = oSheet.get_Range("O3", "O3");
                cl15.Value2 = "Số máy";
                cl15.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl16 = oSheet.get_Range("P3", "P3");
                cl16.Value2 = "Loại KH";
                cl16.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "P3");
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

        #endregion Xuất Excel

        #region Tìm tất cả khách hàng

        private void FindKhachHangAll()
        {
            connect();
            int totalrecord = 0;
            int curentpage = 1;
            int end;
            int top = 1;
            int totalpage = 0;

            try
            {
                curentpage = int.Parse(txtCurrentPage.Text);
            }
            catch { }

            if (cbb_SoKHpage.SelectedIndex > 0 && cbb_SoKHpage.SelectedIndex != 4)
            {
                pagesize = Convert.ToInt32(cbb_SoKHpage.Text);
            }

            end = pagesize + 1;

            string select = "";
            string where = "";
            if (txtTimKiemPhone.Text != "") where += " and kh.DienThoai like '%" + txtTimKiemPhone.Text.Replace("'", "''") + "%'";

            if (txtSomay.Text != "") where += " and XeDaBan.SoMay like '%" + txtSomay.Text.Replace("'", "''") + "%'";

            if (txtSokhung.Text != "") where += " and XeDaBan.SoKhung like '%" + txtSokhung.Text.Replace("'", "''") + "%'";

            if (txtBienso.Text != "") where += " and LichSuBaoDuongXe.BienSo like '%" + txtBienso.Text.Replace("'", "''") + "%'";

            if (chkNgayMua.Checked == true)
            {
                string dfrom = dtngaytaofrom.Value.ToString("yyyyMMdd");
                string dto = dtngaytaoto.Value.ToString("yyyyMMdd");
                where += " and kh.NgayMua between '" + dfrom + "' and '" + dto + " 23:59:59'";

            }

            if (txtBienso.Text == "" & txtSokhung.Text == "" & txtSomay.Text == "")
            {
                if (chkNgayMua.Checked == false && txtTimKiemPhone.Text == "")
                {
                    MessageBox.Show(@"Bạn phải điền thông tin tìm kiếm");
                    return;
                }
                select = @" select * from
                    (select Row_Number() over (order by kh.idkhachHang) as rownum, kh.IdKhachHang as [MãKH],kh.HoKh as [Họ KH],kh.TenKH as [Tên KH],kh.GioiTinh as [Giới Tính],kh.NgaySinh as [Ngày Sinh],DienThoai  as [Điện Thoại],DiaChi as [Địa Chỉ],CMND,SoSBH as [Số SBH],kh.LoaiKH as [Loại KH],'' as BienSo,kh.NgayMua as NgayMuaXe from KhachHang kh
                     where kh.idcongty=" + Class.CompanyInfo.idcongty;
            }
            else
            {
                select = @" select * from
                    (select Row_Number() over (order by kh.idkhachHang) as rownum, kh.IdKhachHang as [MãKH] ,HoKh as [Họ KH],TenKH as [Tên KH],GioiTinh as [Giới Tính],NgaySinh as [Ngày Sinh],DienThoai as [Điện Thoại],DiaChi as [Địa Chỉ],CMND,SoSBH as [Số SBH],kh.LoaiKH as [Loại KH],'' as BienSo,XeDaBan.NgayBan as NgayMuaXe, XeDaBan.TenXe as TenXe, XeDaBan.SoKhung, XeDaBan.SoMay from KhachHang kh
                    inner join XeDaBan on XeDaBan.IdKhachHang=kh.IdKhachHang
                     where kh.idcongty=" + Class.CompanyInfo.idcongty;
                if (txtBienso.Text != "")
                {
                    select = @" select * from
                        (select kh.IdKhachHang as [MãKH],HoKh as [Họ KH],TenKH as [Tên KH],GioiTinh as [Giới Tính],NgaySinh as [Ngày Sinh],DienThoai as [Điện Thoại],DiaChi as [Địa Chỉ],CMND,kh.LoaiKH as [Loại KH],LichSuBaoDuongXe.NgayBaoDuong as NgayXeDenBaoDuong,LichSuBaoDuongXe.NgayGiaoXe as NgayGiaoXe,SoSBH as [Số SBH],LichSuBaoDuongXe.TenXe,LichSuBaoDuongXe.BienSo,LichSuBaoDuongXe.SoKhung,LichSuBaoDuongXe.SoMay,kh.LoaiKH ,Row_Number() over (order by kh.idkhachHang) as rownum from KhachHang kh
                        inner join LichSuBaoDuongXe on LichSuBaoDuongXe.IdKhachHang = kh.IdKhachHang  where kh.idcongty=" + Class.CompanyInfo.idcongty;
                }
            }

            string dkbienso = "";

            if (where != "") select += where;
            if (dkbienso != "") select += dkbienso;
            string slcount = "select count(*)" + select.Remove(0, 9) + ") tbl";

            totalrecord = int.Parse(new SqlCommand(slcount, con).ExecuteScalar().ToString());

            int totalSum = int.Parse(new SqlCommand(slcount, con).ExecuteScalar().ToString());

            if (cbb_SoKHpage.SelectedIndex != 4)
            {
                totalrecord = int.Parse(new SqlCommand(slcount, con).ExecuteScalar().ToString());
                totalpage = (totalrecord % pagesize != 0) ? totalrecord / pagesize + 1 : totalpage / pagesize;
                top = curentpage * pagesize - pagesize + 1;
                end = curentpage * pagesize;
                lblTotalPage.Text = totalpage.ToString();
            }
            else
            {
                try
                {
                    totalrecord = int.Parse(new SqlCommand(slcount, con).ExecuteScalar().ToString());

                    pagesize = totalrecord;

                    totalpage = (totalrecord%pagesize != 0) ? totalrecord/pagesize + 1 : totalpage/pagesize;
                    top = curentpage*pagesize - pagesize + 1;
                    end = curentpage*pagesize;
                    lblTotalPage.Text = totalpage.ToString();
                }
                catch
                {
                    //
                }
            }

            //select = "select IdKhachHang as 'MãKH',HoKh as 'Họ KH',TenKH as 'Tên KH',GioiTinh as 'Giới Tính',CONVERT(nchar(10), NgaySinh, 103) as 'Ngày Sinh',DienThoai as 'Điện Thoại',DiaChi as 'Địa Chỉ',"
            //          + " CMND,CONVERT(nchar(10), NgayMuaXe, 103) as 'Ngày Mua', SoSBH as 'Số SBH' ,TenXe as 'Tên Xe',BienSo as 'Biển Số',SoKhung as 'Số Khung',SoMay as 'Số Máy',LoaiKH as 'Loại KH'" + select.Remove(0, 9);

           

            //select = @" select * from
            //        (select kh.IdKhachHang,HoKh,TenKH,GioiTinh,NgaySinh,DienThoai,DiaChi,CMND,kh.NgayMua as NgayMuaXe,SoSBH, '' as BienSo ,Row_Number() over (order by kh.idkhachHang) as rownum from KhachHang kh
            //         where kh.idcongty=" + Class.CompanyInfo.idcongty;

            select += " ) tbl where rownum between " + top.ToString() + " and " + end.ToString();

            da = new SqlDataAdapter(select, con);
            dt.Clear();
            da.Fill(dt);
           
            if (dt.Rows.Count > 0)
            {
                grvkhachhang.DataSource = dt;
                object sms = new SqlCommand("select COUNT(*) from KhachHang where IdCongty=" + Class.CompanyInfo.idcongty + " and NgayMua between '" + dtngaytaofrom.Value.ToString("yyyyMMdd") + "' and ' " + dtngaytaoto.Value.ToString("yyyyMMdd") + " 23:59:59'", con).ExecuteScalar();
                lbl_SoKH.Text = @"Số KH : " + totalSum;
            }
            else
            {
                lbl_SoKH.Text = "";
                MessageBox.Show(@"Không tìm thấy khách hàng nào");
            }
             con.Close();
        }

        #endregion Tìm tất cả khách hàng

        #region Tìm khách hàng

        private void FindKhachHang()
        {
            connect();
            int totalrecord = 0; int curentpage = 1;
            try
            {
                curentpage = int.Parse(txtCurrentPage.Text);
            }
            catch { }
            int end;

            if (cbb_SoKHpage.SelectedIndex > 0 && cbb_SoKHpage.SelectedIndex != 4)
            {
                pagesize = Convert.ToInt32(cbb_SoKHpage.Text);
            }

            end = pagesize + 1;

            int top = 1;
            int totalpage = 0;

            string select = "";
            string where = "";
            if (txtTimKiemPhone.Text != "") where += " and kh.DienThoai like '%" + txtTimKiemPhone.Text.Replace("'", "''") + "%'";

            if (dtngaytaofrom.Value.ToShortDateString() != "" && dtngaytaoto.Value.ToShortDateString() != "")
            {
                string dfrom = dtngaytaofrom.Value.ToString("yyyyMMdd");
                string dto = dtngaytaoto.Value.ToString("yyyyMMdd");
                where += " and XeDaBan.NgayTao between '" + dfrom + "' and '" + dto + " 23:59:59'";
            }

            string dkbienso = "";
            if (txtBienso.Text != "") dkbienso += " and BienSo like '" + txtBienso.Text.Replace("'", "''") + "%'";
            if (txtSokhung.Text != "") dkbienso += " and Sokhung like '" + txtSokhung.Text.Replace("'", "''") + "%'";
            if (txtSomay.Text != "") dkbienso += " and Somay like '" + txtSomay.Text.Replace("'", "''") + "%'";

            if (dkbienso != "") dkbienso = " and kh.IdKhachHang in (select distinct IdKhachHang from XeDaBan where " + dkbienso.Remove(0, 4) + ") " +
                "or kh.IdKhachHang in (select distinct IdKhachHang from LichSuBaoDuongXe where " + dkbienso.Remove(0, 4) + ")";

            select = @" select * from
                    (select kh.IdKhachHang ,HoKh,TenKH,GioiTinh,NgaySinh,DienThoai,DiaChi,CMND,kh.LoaiKH,XeDaBan.NgayBan as NgayMuaXe,XeDaBan.IdXeDaBan,SoSBH,XeDaBan.TenXe,LichSuBaoDuongXe.BienSo,XeDaBan.SoKhung,XeDaBan.SoMay, Row_Number() over (order by kh.idkhachHang) as rownum from KhachHang kh
                    left join XeDaBan on XeDaBan.IdKhachHang=kh.IdKhachHang
                    left join LichSuBaoDuongXe on LichSuBaoDuongXe.IdKhachHang = kh.IdKhachHang  where kh.idcongty=" + Class.CompanyInfo.idcongty;

            if (where != "") select += where;
            if (dkbienso != "") select += dkbienso;
            string slcount = "select count(*)" + select.Remove(0, 9) + ") tbl";

            int totalSum = int.Parse(new SqlCommand(slcount, con).ExecuteScalar().ToString());

            if (cbb_SoKHpage.SelectedIndex != 4)
            {
                totalrecord = int.Parse(new SqlCommand(slcount, con).ExecuteScalar().ToString());
                totalpage = (totalrecord % pagesize != 0) ? totalrecord / pagesize + 1 : totalpage / pagesize;
                top = curentpage * pagesize - pagesize + 1;
                end = curentpage * pagesize;
                lblTotalPage.Text = totalpage.ToString();
            }
            else
            {
                try
                {
                    totalrecord = int.Parse(new SqlCommand(slcount, con).ExecuteScalar().ToString());

                    pagesize = totalrecord;

                    totalpage = (totalrecord % pagesize != 0) ? totalrecord / pagesize + 1 : totalpage / pagesize;
                    top = curentpage * pagesize - pagesize + 1;
                    end = curentpage * pagesize;
                    lblTotalPage.Text = totalpage.ToString();
                }
                catch { }
            }

            select = "select IdKhachHang as 'MãKH',HoKh as 'Họ KH',TenKH as 'Tên KH',GioiTinh as 'Giới Tính',CONVERT(nchar(10), NgaySinh, 103) as 'Ngày Sinh',DienThoai as 'Điện Thoại',DiaChi as 'Địa Chỉ',"
            + " CMND,CONVERT(nchar(103), NgayMuaXe, 103) as 'Ngày Mua Xe',SoSBH as 'Số SBH' ,TenXe as 'Tên Xe',BienSo as 'Biển Số',SoKhung as 'Số Khung',SoMay as 'Số Máy',LoaiKH as 'Loại KH',IdXeDaBan as 'Mã Xe'" + select.Remove(0, 9);
            select += " ) tbl where rownum between " + top.ToString() + " and " + end.ToString();

            da = new SqlDataAdapter(select, con);
            dt.Clear();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                grvkhachhang.DataSource = dt;
                object sms = new SqlCommand("select COUNT(*) from KhachHang where IdCongty=" + Class.CompanyInfo.idcongty + " and NgayMua between '" + dtngaytaofrom.Value.ToString("yyyyMMdd") + "' and ' " + dtngaytaoto.Value.ToString("yyyyMMdd") + " 23:59:59'", con).ExecuteScalar();
                lbl_SoKH.Text = "Số KH : " + totalSum;
            }
            else
            {
                lbl_SoKH.Text = "";
                MessageBox.Show("Không tìm thấy khách hàng nào");
            }
            con.Close();
        }

        #endregion Tìm khách hàng

        #region Tìm khách mua xe

        private void FindKhachHangMuaXe()
        {
            connect();
            int totalrecord = 0; int curentpage = 1;
            try
            {
                curentpage = int.Parse(txtCurrentPage.Text);
            }
            catch { }
            int end;

            if (cbb_SoKHpage.SelectedIndex > 0 && cbb_SoKHpage.SelectedIndex != 4)
            {
                pagesize = Convert.ToInt32(cbb_SoKHpage.Text);
            }

            end = pagesize + 1;

            int top = 1;
            int totalpage = 0;

            string select = "";
            string where = "";
            if (txtTimKiemPhone.Text != "") where += " and kh.DienThoai like '%" + txtTimKiemPhone.Text.Replace("'", "''") + "%'";

            if (dtngaytaofrom.Value.ToShortDateString() != "" && dtngaytaoto.Value.ToShortDateString() != "")
            {
                string dfrom = dtngaytaofrom.Value.ToString("yyyyMMdd");
                string dto = dtngaytaoto.Value.ToString("yyyyMMdd");

                if (!chkNgayMua.Checked)
                    where += " and xdb.NgayBan between '" + dfrom + "' and '" + dto + " 23:59:59'";
                else
                    where += " and xdb.NgayBan between '" + dfrom + "' and '" + dto + " 23:59:59'";
            }

            string dkbienso = "";
            if (txtBienso.Text != "") dkbienso += " and BienSo like '%" + txtBienso.Text.Replace("'", "''") + "%'";
            if (txtSokhung.Text != "") dkbienso += " and Sokhung like '%" + txtSokhung.Text.Replace("'", "''") + "%'";
            if (txtSomay.Text != "") dkbienso += " and Somay like '%" + txtSomay.Text.Replace("'", "''") + "%'";

            if (dkbienso != "") dkbienso = " and kh.IdKhachHang in (select distinct IdKhachHang from XeDaBan where " + dkbienso.Remove(0, 4) + ") ";

            select = @" select * from
                        (select kh.IdKhachHang,HoKh,TenKH,GioiTinh,NgaySinh,DienThoai,DiaChi,CMND,xdb.NgayBan as NgayMuaXe,SoSBH,xdb.TenXe,xdb.IdXeDaBan,xdb.BienSo,xdb.SoKhung,xdb.SoMay,xdb.LoaiKH ,Row_Number() over (order by kh.idkhachHang) as rownum from KhachHang kh
                        inner join XeDaBan xdb on xdb.IdKhachHang=kh.IdKhachHang
                        where kh.idcongty=" + Class.CompanyInfo.idcongty;

            if (where != "") select += where;
            if (dkbienso != "") select += dkbienso;
            string slcount = "select count(*)" + select.Remove(0, 9) + ") tbl";

            int totalSum = int.Parse(new SqlCommand(slcount, con).ExecuteScalar().ToString());

            if (cbb_SoKHpage.SelectedIndex != 4)
            {
                totalrecord = int.Parse(new SqlCommand(slcount, con).ExecuteScalar().ToString());
                totalpage = (totalrecord % pagesize != 0) ? totalrecord / pagesize + 1 : totalpage / pagesize;
                top = curentpage * pagesize - pagesize + 1;
                end = curentpage * pagesize;
                lblTotalPage.Text = totalpage.ToString();
            }
            else
            {
                try
                {
                    totalrecord = int.Parse(new SqlCommand(slcount, con).ExecuteScalar().ToString());

                    pagesize = totalrecord;

                    totalpage = (totalrecord % pagesize != 0) ? totalrecord / pagesize + 1 : totalpage / pagesize;
                    top = curentpage * pagesize - pagesize + 1;
                    end = curentpage * pagesize;
                    lblTotalPage.Text = totalpage.ToString();
                }
                catch { }
            }

            select = "select IdKhachHang as 'MãKH',HoKh as 'Họ KH',TenKH as 'Tên KH',GioiTinh as 'Giới Tính',CONVERT(nchar(10), NgaySinh, 103) as 'Ngày Sinh',DienThoai as 'Điện Thoại',DiaChi as 'Địa Chỉ',"
                     + " CMND,CONVERT(nchar(10), NgayMuaXe, 103) as 'Ngày Mua Xe',SoSBH as 'Số SBH' ,TenXe as 'Tên Xe',BienSo as 'Biển Số',SoKhung as 'Số Khung',SoMay as 'Số Máy',LoaiKH as 'Loại KH',IdXeDaBan as 'Mã Xe'" + select.Remove(0, 9);
            select += " ) tbl where rownum between " + top.ToString() + " and " + end.ToString();

            da = new SqlDataAdapter(select, con);
            dt.Clear();
            da.Fill(dt);
           
            if (dt.Rows.Count > 0)
            {
                grvkhachhang.DataSource = dt;
                object sms = new SqlCommand("select COUNT(*) from XeDaBan where IdCongty=" + Class.CompanyInfo.idcongty + " and NgayTao between '" + dtngaytaofrom.Value.ToString("yyyyMMdd") + "' and ' " + dtngaytaoto.Value.ToString("yyyyMMdd") + " 23:59:59'", con).ExecuteScalar();
                lbl_SoKH.Text = "Số KH : " + totalSum;
            }
            else
            {
                lbl_SoKH.Text = "";
                MessageBox.Show("Không tìm thấy khách hàng nào");
            }
            con.Close();
        }

        #endregion Tìm khách mua xe

        #region Tìm khách bảo dưỡng

        private void FindKhachHangBaoDuong()
        {
            connect();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select IdKhachHang as 'MãKH',HoKh as 'Họ KH',TenKH as 'Tên KH',GioiTinh as 'Giới Tính',CONVERT(nchar(10), NgaySinh, 103) as 'Ngày Sinh',DienThoai as 'Điện Thoại',DiaChi as 'Địa Chỉ', CMND,CONVERT(nchar(10), NgayXeDenBaoDuong, 103) as 'Ngày Bảo Dưỡng', CMND,CONVERT(nchar(10), NgayGiaoXe, 103) as 'Ngày Giao Xe' ,SoSBH as 'Số SBH' ,TenXe as 'Tên Xe',BienSo as 'Biển Số',SoKhung as 'Số Khung',SoMay as 'Số Máy',LoaiKH as 'Loại KH' from    
                                (select kh.IdKhachHang,HoKh,TenKH,GioiTinh,NgaySinh,DienThoai,DiaChi,CMND,LichSuBaoDuongXe.NgayBaoDuong as NgayXeDenBaoDuong, LichSuBaoDuongXe.NgayGiaoXe as NgayGiaoXe ,SoSBH,LichSuBaoDuongXe.TenXe,LichSuBaoDuongXe.BienSo,LichSuBaoDuongXe.SoKhung,LichSuBaoDuongXe.SoMay,kh.LoaiKH ,Row_Number() over (order by kh.idkhachHang) as rownum from KhachHang kh    
                                inner join LichSuBaoDuongXe on LichSuBaoDuongXe.IdKhachHang = kh.IdKhachHang  where kh.idcongty = @IdCongTy and LichSuBaoDuongXe.BienSo like @BienSo and LichSuBaoDuongXe.Sokhung LIKE @SoKhung and LichSuBaoDuongXe.SoMay LIKE @SoMay and kh.DienThoai like @DienThoai and LichSuBaoDuongXe.NgayBaoDuong between @TuNgay and @DenNgay and kh.IdKhachHang in (select distinct IdKhachHang from LichSuBaoDuongXe where  BienSo like @BienSo) ) tbl where rownum between 1 and 20";
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@BienSo", "%" + txtBienso.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@SoKhung", "%" + txtSokhung.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@SoMay", "%" + txtSomay.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@DienThoai", "%" + txtTimKiemPhone.Text.Trim() + "%");
            cmd.Parameters.AddWithValue("@TuNgay", dtngaytaofrom.Value.ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@DenNgay", dtngaytaoto.Value.ToString("yyyyMMdd") + " 23:59:59");
            
            dt.Clear();
            dt = Class.datatabase.getData(cmd);
            int totalSum = dt.Rows.Count;
            if (dt.Rows.Count > 0)
            {
                grvkhachhang.DataSource = dt;
                object sms = new SqlCommand("select COUNT(*) from KhachHang where IdCongty=" + Class.CompanyInfo.idcongty + " and NgayMua between '" + dtngaytaofrom.Value.ToString("yyyyMMdd") + "' and ' " + dtngaytaoto.Value.ToString("yyyyMMdd") + " 23:59:59'", con).ExecuteScalar();
                lbl_SoKH.Text = "Số KH : " + totalSum;
            }
            else
            {
                lbl_SoKH.Text = "";
                MessageBox.Show("Không tìm thấy khách hàng nào");
            }
        }

        #endregion Tìm khách bảo dưỡng

        private void UcQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            if (Class.EmployeeInfo.Quyen.ToLower() != "qtv")
                btnXoaTimKiem.Enabled = false;

            dtngaytaofrom.Value = DateTime.Now;
            dtngaytaoto.Value = DateTime.Now;
            dtpngaysinh.Value = DateTime.Now;
            panelEx1.AutoScroll = true;
            panelEx1.VerticalScroll.Visible = false;
            panelEx1.VerticalScroll.Enabled = false;
            panelEx1.HorizontalScroll.Visible = true;
            panelEx1.HorizontalScroll.Enabled = true;
            panelEx1.HorizontalScroll.Minimum = 0;
            panelEx1.HorizontalScroll.Maximum = 1333;
            Load_cbbSoKH();
            cbb_LoaiKH.Items.Add("Bao duong");
            cbb_LoaiKH.Items.Add("Dich vu");
            getdata();
        }

        private void cbb_SoKHpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_SoKHpage.SelectedIndex != 4)
            {
                pagesize = Convert.ToInt32(cbb_SoKHpage.Text);

                if (cbokhachmuaxe.Checked == false && CboKhachbaoduong.Checked == true)
                {
                    FindKhachHangBaoDuong();
                }
                else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == false)
                {
                    FindKhachHangMuaXe();
                }
                else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == true)
                {
                    FindKhachHang();
                }
                else if (CboKhachbaoduong.Checked == true)
                {
                    FindKhachHangBaoDuong();
                }
                else
                {
                    FindKhachHangAll();
                }
            }
            else
            {
                if (cbokhachmuaxe.Checked == false && CboKhachbaoduong.Checked == true)
                {
                    FindKhachHangBaoDuong();
                }
                else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == false)
                {
                    FindKhachHangMuaXe();
                }
                else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == true)
                {
                    FindKhachHang();
                }
                else
                {
                    FindKhachHangAll();
                }
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                connect();
                SqlCommand cmdupdate = new SqlCommand();
                cmdupdate.Connection = con;

                if (String.IsNullOrEmpty(txttenkh.Text))
                {
                    MessageBox.Show("Tên khách hàng không được để trống !");
                    txttenkh.Focus();

                    return;
                }

                if (String.IsNullOrEmpty(txtdienthoai.Text))
                {
                    MessageBox.Show("Điện thoại khách hàng không được để trống !");
                    txtdienthoai.Focus();

                    return;
                }

                if (String.IsNullOrEmpty(cbb_LoaiKH.Text))
                {
                    MessageBox.Show("Loại khách hàng không được để trống !");
                    cbb_LoaiKH.Focus();

                    return;
                }

                string sql = "SELECT DienThoai FROM KhachHang WHERE IdCongTy=" + Class.CompanyInfo.idcongty;
                da = new SqlDataAdapter(sql, con);
                dt.Clear();
                da.Fill(dt);

                bool checkSDT = false;
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (txtdienthoai.Text == dt.Rows[i]["DienThoai"].ToString())
                        {
                            checkSDT = true;
                            break;
                        }
                    }
                }
                if (!checkSDT)
                {
                    string dete = DateTime.Now.ToShortDateString();
                    if (dtpngaysinh.Value.ToShortDateString() == dete)
                        cmdupdate.CommandText = @"Insert KhachHang(IdCongTy,HoKH,TenKH,DienThoai,DiaChi,MaNhomKH,NgayMua,CMND,GioiTinh,SoSBH,LoaiKH)
                                        values(@IdCongTy ,@HoKH,@TenKH,@DienThoai,@DiaChi,@MaNhomKH,@NgayMua,@CMND,@GioiTinh,@SoSBH,@LoaiKH)";
                    else
                    {
                        cmdupdate.CommandText = @"Insert KhachHang(IdCongTy,HoKH,TenKH,NgaySinh,DienThoai,DiaChi,MaNhomKH,NgayMua,CMND,GioiTinh,SoSBH,LoaiKH)
                                        values(@IdCongTy ,@HoKH,@TenKH,@NgaySinh,@DienThoai,@DiaChi,@MaNhomKH,@NgayMua,@CMND,@GioiTinh,@SoSBH,@LoaiKH)";
                        cmdupdate.Parameters.AddWithValue("@NgaySinh", dtpngaysinh.Value);
                    }
                    cmdupdate.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmdupdate.Parameters.AddWithValue(@"HoKH", txthokh.Text);
                    cmdupdate.Parameters.AddWithValue("@TenKH", txttenkh.Text);
                    cmdupdate.Parameters.AddWithValue("@DiaChi", txtdiachi.Text);
                    cmdupdate.Parameters.AddWithValue("@DienThoai", txtdienthoai.Text);
                    cmdupdate.Parameters.AddWithValue("@MaNhomKH", cbonhomkh.SelectedValue);
                    cmdupdate.Parameters.AddWithValue("@NgayMua", DateTime.Now);
                    cmdupdate.Parameters.AddWithValue("@CMND", txtcmnd.Text);
                    cmdupdate.Parameters.AddWithValue("@GioiTinh", txtGioitinh.Text);
                    cmdupdate.Parameters.AddWithValue("@SoSBH", txtSoSBH.Text);
                    cmdupdate.Parameters.AddWithValue("@LoaiKH", Convert.ToString(cbb_LoaiKH.SelectedIndex + 1));
                    cmdupdate.ExecuteNonQuery();
                    MessageBox.Show("Thêm khách hàng thành công!", "Thông báo");
                    //FindKhachHang();
                }
                else
                {
                    MessageBox.Show("Số điện thoại đã tồn tại", "Thông báo");
                    txtdienthoai.SelectAll();
                    txtdienthoai.Focus();
                }
                con.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi thêm khách hàng: " + ex.Message);
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (txtmakh.Text != "")
            {
                connect();
                if (String.IsNullOrEmpty(txttenkh.Text))
                {
                    MessageBox.Show("Tên khách hàng không được để trống !");
                    txttenkh.Focus();

                    return;
                }

                if (String.IsNullOrEmpty(txtdienthoai.Text))
                {
                    MessageBox.Show("Điện thoại khách hàng không được để trống !");
                    txtdienthoai.Focus();

                    return;
                }

                if (String.IsNullOrEmpty(cbb_LoaiKH.Text))
                {
                    MessageBox.Show("Loại khách hàng không được để trống !");
                    cbb_LoaiKH.Focus();

                    return;
                }

                SqlCommand cmdupdate = new SqlCommand();
                cmdupdate.Connection = con;
                cmdupdate.CommandText = @"Update KhachHang set HoKH=@HoKH,TenKH=@TenKH,NgaySinh=@NgaySinh,GioiTinh=@Gioitinh,DienThoai=@DienThoai,DiaChi=@DiaChi,MaNhomKH=@MaNhomKH,CMND=@CMND,SoSBH = @SoSBH, LoaiKH=@LoaiKH where IdKhachHang=@idKhachHang";
                cmdupdate.Parameters.AddWithValue("@IdKhachHang", txtmakh.Text);
                cmdupdate.Parameters.AddWithValue("@HoKH", txthokh.Text);
                cmdupdate.Parameters.AddWithValue("@TenKH", txttenkh.Text);
                cmdupdate.Parameters.AddWithValue("@NgaySinh", dtpngaysinh.Value);
                cmdupdate.Parameters.AddWithValue("@DiaChi", txtdiachi.Text);
                cmdupdate.Parameters.AddWithValue("@DienThoai", txtdienthoai.Text);
                cmdupdate.Parameters.AddWithValue("@MaNhomKH", cbonhomkh.SelectedValue);
                cmdupdate.Parameters.AddWithValue("@Gioitinh", txtGioitinh.Text);
                cmdupdate.Parameters.AddWithValue("@CMND", txtcmnd.Text);
                cmdupdate.Parameters.AddWithValue("@SoSBH", txtSoSBH.Text);
                cmdupdate.Parameters.AddWithValue("@LoaiKH", Convert.ToString(cbb_LoaiKH.SelectedIndex + 1));

                cmdupdate.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Sửa thông tin khách hàng thành công!", "Thông báo");

                #region phan loai KH

                txtCurrentPage.Text = "1";
                dt = new DataTable();
                grvkhachhang.DataSource = null;
                if (cbokhachmuaxe.Checked == false && CboKhachbaoduong.Checked == true)
                {
                    FindKhachHangBaoDuong();
                    buttonX5.Enabled = false;
                }
                else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == false)
                {
                    buttonX5.Enabled = true;
                    FindKhachHangMuaXe();
                }
                else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == true)
                {
                    buttonX5.Enabled = true;
                    FindKhachHang();
                }
                else
                {
                    FindKhachHangAll();
                    buttonX5.Enabled = false;
                }
                try { grvkhachhang.Columns["Loại KH"].Visible = false; }
                catch { }

                #endregion phan loai KH
            }
            else MessageBox.Show("Bạn chưa chọn khách hàng", "Thông báo");
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtmakh.Text.Length > 0)
                {
                    if (MessageBox.Show("Bạn chắc chắn muốn xóa khách hàng đã chọn?", "Xóa khách hàng", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        connect();
                        DataRow row = dt.Select("MãKH = " + Convert.ToInt32(txtmakh.Text))[0];
                        row.BeginEdit();
                        row.Delete();
                        row.EndEdit();
                        SqlCommand commandDelete = new SqlCommand();
                        commandDelete.Connection = con;
                        commandDelete.CommandType = System.Data.CommandType.Text;
                        commandDelete.CommandText = @"Delete From ChiTietHoaDonBan where SoHoaDonBanHang in (
                                                    select hdbh.SoHoaDonBanHang 
                                                    from khachhang kh
                                                    inner join HoaDonBanHang hdbh on hdbh.IdKhachHang=kh.IdKhachHang and hdbh.IdKhachHang = @IdKhachHang and hdbh.IdCongty= " + Class.CompanyInfo.idcongty + @");
                                                    Delete From HoaDonBanHang where SoHoaDonBanHang in  (
                                                    select hdbh.SoHoaDonBanHang 
                                                    from khachhang kh
                                                    inner join HoaDonBanHang hdbh on hdbh.IdKhachHang=kh.IdKhachHang and hdbh.IdKhachHang = @IdKhachHang and hdbh.IdCongty= " + Class.CompanyInfo.idcongty + @");
                                                    Delete From KhachHang where IdKhachHang = @IdKhachHang;";

                        commandDelete.Parameters.Add("@IdKhachHang", SqlDbType.Int, 50, "MãKH");
                        da.DeleteCommand = commandDelete;
                        da.Update(dt);
                        con.Close();
                        MessageBox.Show(@"Bạn đã xóa thành công !", "THÔNG BÁO", MessageBoxButtons.OK);
                    }
                }
                else MessageBox.Show(@"Bạn chưa chọn khách hàng", "Thông báo");
            }
            catch (Exception ex) { MessageBox.Show(@"Lỗi: " + ex.Message, "Thông báo"); }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            txtmakh.Text = "";
            txthokh.Text = "";
            txttenkh.Text = "";
            txtdiachi.Text = "";
            txtdienthoai.Text = "";
            txtcmnd.Text = "";
            cbonhomkh.Text = "";
            txtGioitinh.Text = "";
            txtSoSBH.Text = "";
            cbb_LoaiKH.Text = "";
            dtpngaysinh.Value = DateTime.Now;

            #region phan loai KH

            txtCurrentPage.Text = "1";
            dt = new DataTable();
            grvkhachhang.DataSource = null;
            if (cbokhachmuaxe.Checked == false && CboKhachbaoduong.Checked == true)
            {
                FindKhachHangBaoDuong();
                buttonX5.Enabled = false;
            }
            else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == false)
            {
                buttonX5.Enabled = true;
                FindKhachHangMuaXe();
            }
            else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == true)
            {
                buttonX5.Enabled = true;
                FindKhachHang();
            }
            else
            {
                FindKhachHangAll();
                buttonX5.Enabled = false;
            }
            try { grvkhachhang.Columns["Loại KH"].Visible = false; }
            catch { }

            #endregion phan loai KH
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            int ThangBDCuoi;

            if (MessageBox.Show(@"Bạn có muốn Phân loại KH ?", @"Phân loại KH", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                connect();
                string thangnhan = "select thangnhan from SMSMaintenanceConfig where idcongty=" + Class.CompanyInfo.idcongty;
                SqlDataAdapter da1 = new SqlDataAdapter(thangnhan, con);
                DataTable dt1 = new DataTable();
                da1.Fill(dt1);

                if (dt1.Rows.Count > 0)
                {
                    string[] tmp = { "" };
                    tmp = dt1.Rows[0]["thangnhan"].ToString().Split(',');
                    ThangBDCuoi = int.Parse(tmp[tmp.Length - 1]);
                }
                else
                {
                    MessageBox.Show(@"Bạn chưa Cấu hình lịch bảo dưỡng! \nCấu hình lịch bảo dưỡng trước.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }

                string sqlXeDaBan = "SELECT * FROM xedaban WHERE loaikh=1 and idcongty=" + Class.CompanyInfo.idcongty;
                da = new SqlDataAdapter(sqlXeDaBan, con);
                dt.Clear();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    DateTime ngayBan;
                    int Dem = 0;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (DateTime.TryParse(dt.Rows[i]["Ngày Mua Xe"].ToString(), out ngayBan))
                        {
                            ngayBan = Convert.ToDateTime(dt.Rows[i]["Ngày Mua Xe"]);
                            DateTime _ngayBD = ngayBan.AddMonths(ThangBDCuoi);

                            if (_ngayBD == DateTime.Now || _ngayBD < DateTime.Now)
                            {
                                Dem++;

                                SqlCommand cmdupdate = new SqlCommand();
                                cmdupdate.Connection = con;
                                cmdupdate.CommandText = @"UPDATE XeDaBan SET LoaiKH='2' WHERE IdCongTy=@IdCongTy and IdXeDaBan=@IdXeDaBan";
                                cmdupdate.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmdupdate.Parameters.AddWithValue("@IdXeDaBan", Convert.ToInt32(dt.Rows[i]["Mã Xe"].ToString()));
                                cmdupdate.ExecuteNonQuery();

                                cmdupdate.CommandText = @"UPDATE KhachHang SET LoaiKH='2' WHERE IdKhachHang=@IdKhachHang";
                                cmdupdate.Parameters.AddWithValue("@IdKhachHang", Convert.ToInt32(dt.Rows[i]["MãKH"].ToString()));
                                cmdupdate.ExecuteNonQuery();
                            }
                        }
                    }

                    MessageBox.Show(@"Số KH hết Bảo dưỡng định kỳ được chuyển sang KH Dịch vụ là : " + Dem);
                }
                else
                {
                    MessageBox.Show(@"Không có xe trong danh sách xe đã bán!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                con.Close();
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            txtCurrentPage.Text = "1";

            dt = new DataTable();
            grvkhachhang.DataSource = dt;
            if (cbokhachmuaxe.Checked == false && CboKhachbaoduong.Checked == true)
            {
                FindKhachHangBaoDuong();
                buttonX5.Enabled = false;
            }
            else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == false)
            {
                buttonX5.Enabled = true;
                FindKhachHangMuaXe();
            }
            else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == true)
            {
                buttonX5.Enabled = true;
                FindKhachHang();
            }
            else
            {
                FindKhachHangAll();
                buttonX5.Enabled = false;
            }
            try
            {
                grvkhachhang.Columns["MãKH"].Visible = false;
                grvkhachhang.Columns["Họ KH"].Visible = false;
                grvkhachhang.Columns["CMND"].Visible = false;
                grvkhachhang.Columns["Số SBH"].Visible = false;
                grvkhachhang.Columns["Loại KH"].Visible = false;
            }
            catch { }
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)grvkhachhang.DataSource;
                Export(dt, "Danh sach", "DANH SÁCH KHÁCH HÀNG");
            }
            catch { }
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            txtCurrentPage.Text = (int.Parse(txtCurrentPage.Text) - 1).ToString();
            if (cbokhachmuaxe.Checked == false && CboKhachbaoduong.Checked == true)
            {
                FindKhachHangBaoDuong();
            }
            else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == false)
            {
                FindKhachHangMuaXe();
            }
            else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == true)
            {
                FindKhachHang();
            }
            else
            {
                FindKhachHangAll();
            }
        }

        private void buttonX9_Click(object sender, EventArgs e)
        {
            txtCurrentPage.Text = (int.Parse(txtCurrentPage.Text) + 1).ToString();
            if (cbokhachmuaxe.Checked == false && CboKhachbaoduong.Checked == true)
            {
                FindKhachHangBaoDuong();
            }
            else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == false)
            {
                FindKhachHangMuaXe();
            }
            else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == true)
            {
                FindKhachHang();
            }
            else
            {
                FindKhachHangAll();
            }
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            txtCurrentPage.Text = (int.Parse(lblTotalPage.Text).ToString());
            if (cbokhachmuaxe.Checked == false && CboKhachbaoduong.Checked == true)
            {
                FindKhachHangBaoDuong();
            }
            else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == false)
            {
                FindKhachHangMuaXe();
            }
            else if (cbokhachmuaxe.Checked == true && CboKhachbaoduong.Checked == true)
            {
                FindKhachHang();
            }

            else
            {
                FindKhachHangAll();
            }
        }

        private void grvkhachhang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                object value = DateTime.Parse(grvkhachhang.Rows[e.RowIndex].Cells["Ngày Sinh"].Value.ToString());
                if (value != null)
                {
                    DateTime dt;
                    if (DateTime.TryParse(value.ToString(), out dt))
                    {
                        dtpngaysinh.Value = dt;
                    }
                }
            }
            catch { dtpngaysinh.Text = null; }
            try
            {
                txtmakh.Text = grvkhachhang.Rows[e.RowIndex].Cells["MãKH"].Value.ToString();
                txthokh.Text = grvkhachhang.Rows[e.RowIndex].Cells["Họ KH"].Value.ToString();
                txttenkh.Text = grvkhachhang.Rows[e.RowIndex].Cells["Tên KH"].Value.ToString();
                txtGioitinh.Text = grvkhachhang.Rows[e.RowIndex].Cells["Giới Tính"].Value.ToString();
                txtdienthoai.Text = grvkhachhang.Rows[e.RowIndex].Cells["Điện Thoại"].Value.ToString();
                txtdiachi.Text = grvkhachhang.Rows[e.RowIndex].Cells["Địa Chỉ"].Value.ToString();
                txtcmnd.Text = grvkhachhang.Rows[e.RowIndex].Cells["CMND"].Value.ToString();
                txtSoSBH.Text = grvkhachhang.Rows[e.RowIndex].Cells["Số SBH"].Value.ToString();
                using (SqlConnection cnn = Class.datatabase.getConnection())
                {
                    cnn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT LoaiKH FROM KhachHang WHERE IdCongty=@IdCongTy AND IdKhachHang=@IdKhachHang", cnn);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdKhachHang", grvkhachhang.Rows[e.RowIndex].Cells[0].Value);

                    DataTable dt = new DataTable();
                    SqlDataAdapter adap = new SqlDataAdapter(cmd);

                    adap.Fill(dt);
                    cnn.Close();
                    if (dt.Rows.Count > 0)
                    {
                        if (Convert.ToInt32(dt.Rows[0][0].ToString()) == 1)
                            cbb_LoaiKH.SelectedIndex = 0;
                        else
                            cbb_LoaiKH.SelectedIndex = 1;
                    }
                    else
                    {
                        cbb_LoaiKH.SelectedIndex = -1;
                    }
                }
            }
            catch { }
        }

        private void btnXoaTimKiem_Click(object sender, EventArgs e)
        {
            DataTable khachmuaxe = new DataTable();
            DataTable khachbaoduong = new DataTable();

            using(SqlConnection cnn = Class.datatabase.getConnection())
            {
                cnn.Open();

                if (CboKhachbaoduong.Checked && cbokhachmuaxe.Checked == false)
                {
                    SqlCommand cm = new SqlCommand(@"SELECT kh.IdKhachHang from KhachHang kh, LichSuBaoDuongXe lsbd where kh.IdCongty=@IdCongTy
                                                    and kh.IdKhachHang=lsbd.IdKhachHang
                                                    and lsbd.NgayBaoDuong between @TuNgay and @DenNgay", cnn);
                    cm.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cm.Parameters.AddWithValue("@TuNgay", dtngaytaofrom.Value.ToString("yyyyMMdd"));
                    cm.Parameters.AddWithValue("@DenNgay", dtngaytaoto.Value.ToString("yyyyMMdd") + " 23:59:59");

                    SqlDataAdapter adap = new SqlDataAdapter(cm);
                    adap.Fill(khachbaoduong);
                }
                else if (CboKhachbaoduong.Checked == false && cbokhachmuaxe.Checked)
                {
                    SqlCommand cm = new SqlCommand(@"SELECT kh.IdKhachHang from KhachHang kh, XeDaBan xdb where kh.IdCongty=@IdCongTy
                                                    and kh.IdKhachHang=xdb.IdKhachHang
                                                    and xdb.NgayBan between @TuNgay and @DenNgay", cnn);
                    cm.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cm.Parameters.AddWithValue("@TuNgay", dtngaytaofrom.Value.ToString("yyyyMMdd"));
                    cm.Parameters.AddWithValue("@DenNgay", dtngaytaoto.Value.ToString("yyyyMMdd") + " 23:59:59");

                    var batdau = dtngaytaofrom.Value.ToString("yyyyMMdd");
                    var ketthuc = dtngaytaoto.Value.ToString("yyyyMMdd") + " 23:59:59";

                    SqlDataAdapter adap = new SqlDataAdapter(cm);
                    adap.Fill(khachmuaxe);
                }

                cnn.Close();
            }

            if (MessageBox.Show("Bạn chắc chắn muốn xóa danh sách khách hàng?", "Xóa khách hàng", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Class.datatabase.getConnection();
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                SqlTransaction tran = cmd.Connection.BeginTransaction();
                cmd.Transaction = tran;

                try
                {
                    #region
                    if (CboKhachbaoduong.Checked && cbokhachmuaxe.Checked == false)
                    {
                        for(int i=0;i<khachbaoduong.Rows.Count;i++)
                        {
                            cmd.CommandText = "delete from LichSuBaoDuongXe where IdKhachHang=@IdKhachHang AND IdCongty=@IdCongTy";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdKhachHang", khachbaoduong.Rows[i][0].ToString());
                            cmd.ExecuteNonQuery();


                            cmd.CommandText = "delete from KhachHang where IdKhachHang=@IdKhachHang AND IdCongty=@IdCongTy";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdKhachHang", khachbaoduong.Rows[i][0].ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else if (CboKhachbaoduong.Checked == false && cbokhachmuaxe.Checked )
                    {
                        for (int i = 0; i < khachmuaxe.Rows.Count; i++)
                        {
                            cmd.CommandText = "delete from XeDaBan where IdKhachHang=@IdKhachHang AND IdCongty=@IdCongTy";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdKhachHang", khachmuaxe.Rows[i][0].ToString());
                            cmd.ExecuteNonQuery();


                            cmd.CommandText = "delete from KhachHang where IdKhachHang=@IdKhachHang AND IdCongty=@IdCongTy";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdKhachHang", khachmuaxe.Rows[i][0].ToString());
                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        MessageBox.Show(@"Hãy chọn tiêu chí lọc khách hàng.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                        return;
                    }
                    #endregion

                    #region
                    //if (CboKhachbaoduong.Checked == true && cbokhachmuaxe.Checked == false)
                    //    cmd.CommandText = "delete from LichSuBaoDuongXe where IdKhachHang in (select IdKhachhang from KhachHang where IdCongty=@IdCongty and NgayMua between @TuNgay and @DenNgay)";
                    //else if (CboKhachbaoduong.Checked == false && cbokhachmuaxe.Checked == true)
                    //    cmd.CommandText = "delete from XeDaBan where IdKhachHang in (select IdKhachHang from KhachHang where IdCongty=@IdCongty and NgayMua between @TuNgay and @DenNgay)";
                    //else
                    //    MessageBox.Show("Hãy chọn tiêu chí lọc khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    //cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    //cmd.Parameters.AddWithValue("@TuNgay", dtngaytaofrom.Value.ToString("yyyyMMdd"));
                    //cmd.Parameters.AddWithValue("@DenNgay", dtngaytaoto.Value.ToString("yyyyMMdd") + " 23:59:59");
                    //cmd.ExecuteNonQuery();
                    //cmd.CommandText = "delete from KhachHang where IdCongty=@IdCongty and NgayMua between @TuNgay and @DenNgay";
                    //cmd.ExecuteNonQuery();
                    #endregion

                    tran.Commit();
                    MessageBox.Show(@"Xóa thông tin khách hàng và xe thành công.", @"Thông báo");
                    buttonX6_Click(new object(), new EventArgs());
                }
                catch
                {
                    tran.Rollback();
                    MessageBox.Show(@"Xóa thông tin khách hàng và xe thất bại.", "Thông báo");
                }
                finally { cmd.Connection.Close(); }
            }
        }

        private void chkNgayMua_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNgayMua.Checked)
                lblNgayTaoTu.Text = @"Ngày mua từ:";
            else
                lblNgayTaoTu.Text = @"Ngày tạo từ:";
        }

        private void CboKhachbaoduong_CheckedChanged(object sender, EventArgs e)
        {
            if (CboKhachbaoduong.Checked)
                lblNgayTaoTu.Text = @"Ngày bảo dưỡng từ";
            else
                lblNgayTaoTu.Text = @"Ngày tạo từ:";
        }
    }
}