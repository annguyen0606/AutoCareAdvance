using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls.ThongKeBaoDuong
{
    public partial class UcKhachKhongDenBaoDuong : UserControl
    {
        private DataTable _tbl;
        private DateTime _tungay, _denngay;
        private string _thangBd;

        public UcKhachKhongDenBaoDuong()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Export data to Excel
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="sheetName">Name Sheet</param>
        /// <param name="title">Title</param>
        // ReSharper disable once MemberCanBeMadeStatic.Local
        private void Export(DataTable dt, string sheetName, string title)
        {
            //Tạo các đối tượng Excel
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;

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
                cl1.Value2 = "Mã KH";
                cl1.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
                cl2.Value2 = "Tên KH";
                cl2.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
                cl3.Value2 = "Điện Thoại";
                cl3.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
                cl4.Value2 = "Giới Tính";
                cl4.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
                cl5.Value2 = "Ngày Sinh";
                cl5.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
                cl6.Value2 = "Địa Chỉ";
                cl6.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
                cl7.Value2 = "Số CMND";
                cl7.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
                cl8.Value2 = "Số Khung";
                cl8.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
                cl9.Value2 = "Số Máy";
                cl9.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl10 = oSheet.get_Range("J3", "J3");
                cl10.Value2 = "Ngày Mua Xe";
                cl10.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "O3");
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
                Microsoft.Office.Interop.Excel.Range cc1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, 10];
                Microsoft.Office.Interop.Excel.Range cc2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, 10];
                Microsoft.Office.Interop.Excel.Range rangeNgayMua = oSheet.get_Range(cc1, cc2);
                rangeNgayMua.NumberFormat = "@";

                Microsoft.Office.Interop.Excel.Range cNgaySinhDau = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, 5];
                Microsoft.Office.Interop.Excel.Range cNgaySinhCuoi = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, 5];
                Microsoft.Office.Interop.Excel.Range rangeNgaySinh = oSheet.get_Range(cNgaySinhDau, cNgaySinhCuoi);
                rangeNgaySinh.NumberFormat = "@";

                Microsoft.Office.Interop.Excel.Range cSoDienThoaiDau = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, 3];
                Microsoft.Office.Interop.Excel.Range cSoDienThoaiCuoi = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, 3];
                Microsoft.Office.Interop.Excel.Range rangeSoDienThoai = oSheet.get_Range(cSoDienThoaiDau, cSoDienThoaiCuoi);
                rangeSoDienThoai.NumberFormat = "@";

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

                MessageBox.Show(errorMessage, @"Error");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        // ReSharper disable once FunctionComplexityOverflow
        private void btnTim_Click(object sender, EventArgs e)
        {
            string i = Convert.ToString(cbb_TieuChiLoc.SelectedIndex);
            dgvKhachHang.DataSource = null;

            if (radioButtonBaoDuongDK.Checked == false && radioButtonBaoDuongDV.Checked == false)
            {
                MessageBox.Show(@"Bạn chưa chọn loại hình bảo dưỡng!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (radioButtonBaoDuongDK.Checked)
            {
                if (Convert.ToInt32(i) < 0)
                {
                    MessageBox.Show(@"Bạn chưa chọn tiêu chí lọc.", @"Thông Báo");
                    return;
                }

                int thangNhan = 0;
                int soLan = 0;

                DataTable customThangNhan = new DataTable();
                customThangNhan.Columns.Add(new DataColumn("TenXe", typeof (string)));
                customThangNhan.Columns.Add(new DataColumn("ThangNhan", typeof (int)));
                customThangNhan.Columns.Add(new DataColumn("IdCongTy", typeof(long)));

                #region "Tính ngày tháng theo lần bảo dưỡng"

                LayThangBd();
                if (!String.IsNullOrEmpty(_thangBd))
                {
                    string[] listConfig = _thangBd.Split(';');

                    foreach (var item in listConfig)
                    {
                        string[] xeConfig = item.Split(':');

                        if (xeConfig.Length > 1)
                        {
                            try
                            {
                                string[] listThangNhan = xeConfig[1].Split(',');

                                DataRow dr = customThangNhan.NewRow();
                                dr["TenXe"] = xeConfig[0];
                                dr["IdCongTy"] = Class.CompanyInfo.idcongty;

                                #region "bảo dưỡng lần 1"

                                if (i == "0")
                                {
                                    if (listThangNhan.Length > 0)
                                    {
                                        soLan = 1;
                                        dr["ThangNhan"] = Convert.ToInt32(listThangNhan[0]);
                                        customThangNhan.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 1"

                                #region "bảo dưỡng lần 2"

                                if (i == "1")
                                {
                                    if (listThangNhan.Length >= 2)
                                    {
                                        soLan = 2;
                                        dr["ThangNhan"] = Convert.ToInt32(listThangNhan[1]);
                                        customThangNhan.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 2"

                                #region "Bảo dưỡng lần 3"

                                if (i == "2")
                                {
                                    if (listThangNhan.Length >= 3)
                                    {
                                        soLan = 3;
                                        dr["ThangNhan"] = Convert.ToInt32(listThangNhan[2]);
                                        customThangNhan.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "Bảo dưỡng lần 3"

                                #region "bảo dưỡng lần 4

                                if (i == "3")
                                {
                                    if (listThangNhan.Length >= 4)
                                    {
                                        soLan = 4;
                                        dr["ThangNhan"] = Convert.ToInt32(listThangNhan[3]);
                                        customThangNhan.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 4

                                #region "bảo dưỡng lần 5"

                                if (i == "4")
                                {
                                    if (listThangNhan.Length >= 5)
                                    {
                                        soLan = 5;
                                        dr["ThangNhan"] = Convert.ToInt32(listThangNhan[4]);
                                        customThangNhan.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 5"

                                #region "bảo dưỡng lần 6"

                                if (i == "5")
                                {
                                    if (listThangNhan.Length >= 6)
                                    {
                                        soLan = 6;
                                        dr["ThangNhan"] = Convert.ToInt32(listThangNhan[5]);
                                        customThangNhan.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 6"

                                #region "bảo dưỡng lần 7"

                                if (i == "6")
                                {
                                    if (listThangNhan.Length >= 7)
                                    {
                                        soLan = 7;
                                        dr["ThangNhan"] = Convert.ToInt32(listThangNhan[6]);
                                        customThangNhan.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 7"

                                #region "bảo dưỡng lần 8"

                                if (i == "7")
                                {
                                    if (listThangNhan.Length >= 8)
                                    {
                                        soLan = 8;
                                        dr["ThangNhan"] = Convert.ToInt32(listThangNhan[7]);
                                        customThangNhan.Rows.Add(dr);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 8"
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo");
                                return;
                            }
                        }
                        else
                        {
                            string[] listThangNhan = item.Split(',');

                            try
                            {
                                #region "bảo dưỡng lần 1"

                                if (i == "0")
                                {
                                    if (listThangNhan.Length > 0)
                                    {
                                        soLan = 1;
                                        thangNhan = Convert.ToInt32(listThangNhan[0]);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 1"

                                #region "bảo dưỡng lần 2"

                                if (i == "1")
                                {
                                    if (listThangNhan.Length >= 2)
                                    {
                                        soLan = 2;
                                        thangNhan = Convert.ToInt32(listThangNhan[1]);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 2"

                                #region "Bảo dưỡng lần 3"

                                if (i == "2")
                                {
                                    if (listThangNhan.Length >= 3)
                                    {
                                        soLan = 3;
                                        thangNhan = Convert.ToInt32(listThangNhan[2]);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "Bảo dưỡng lần 3"

                                #region "bảo dưỡng lần 4

                                if (i == "3")
                                {
                                    if (listThangNhan.Length >= 4)
                                    {
                                        soLan = 4;
                                        thangNhan = Convert.ToInt32(listThangNhan[3]);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 4

                                #region "bảo dưỡng lần 5"

                                if (i == "4")
                                {
                                    if (listThangNhan.Length >= 5)
                                    {
                                        soLan = 5;
                                        thangNhan = Convert.ToInt32(listThangNhan[4]);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 5"

                                #region "bảo dưỡng lần 6"

                                if (i == "5")
                                {
                                    if (listThangNhan.Length >= 6)
                                    {
                                        soLan = 6;
                                        thangNhan = Convert.ToInt32(listThangNhan[5]);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 6"

                                #region "bảo dưỡng lần 7"

                                if (i == "6")
                                {
                                    if (listThangNhan.Length >= 7)
                                    {
                                        soLan = 7;
                                        thangNhan = Convert.ToInt32(listThangNhan[6]);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 7"

                                #region "bảo dưỡng lần 8"

                                if (i == "7")
                                {
                                    if (listThangNhan.Length >= 8)
                                    {
                                        soLan = 8;
                                        thangNhan = Convert.ToInt32(listThangNhan[7]);
                                    }
                                    else
                                    {
                                        MessageBox.Show(@"Không tồn tại tháng bảo dưỡng!", @"Thông báo");
                                        return;
                                    }
                                }

                                #endregion "bảo dưỡng lần 8"
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo");
                                return;
                            }
                        }
                    }

                    #region Tìm kiếm

                    if (thangNhan > 0)
                    {
                        using (SqlConnection con = new SqlConnection(Class.datatabase.connect))
                        {
                            try
                            {
                                con.Open();

                                SqlCommand cmd = new SqlCommand("pro_LayDSKhachChuaDenBaoDuong", con)
                                {
                                    CommandType = CommandType.StoredProcedure
                                };

                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@ngayBDTu",
                                    dateTimePickerTuNgay.Value.ToString("yyyy-MM-dd 00:00:00"));
                                cmd.Parameters.AddWithValue("@ngayBDDen",
                                    dateTimePickerDenNgay.Value.ToString("yyyy-MM-dd 23:59:59"));
                                cmd.Parameters.AddWithValue("@thangNhan", thangNhan);
                                cmd.Parameters.AddWithValue("@soLan", soLan);
                                cmd.Parameters.AddWithValue("@idCongty", Class.CompanyInfo.idcongty);
                                cmd.Parameters.AddWithValue("@tableInput", customThangNhan);

                                SqlDataAdapter da = new SqlDataAdapter(cmd);
                                _tbl = new DataTable();
                                da.Fill(_tbl);
                                if (_tbl.Rows.Count > 0)
                                {
                                    dgvKhachHang.DataSource = _tbl;
                                    groupBox1.Text = @"Kết quả tìm kiếm (" + _tbl.Rows.Count + @" KH)";

                                    con.Close();
                                }
                                else
                                {
                                    MessageBox.Show(@"Không tìm thấy khách hàng nào");
                                    groupBox1.Text = @"Danh sách khách hàng";
                                    return;
                                }
                            }
                            catch (Exception ex) { MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo"); }
                            finally { con.Close(); }
                        }
                    }

                    #endregion Tìm kiếm
                }

                #endregion "Tính ngày tháng theo lần bảo dưỡng"
            }

            if (radioButtonBaoDuongDV.Checked)
            {
                TinhToan(9);

                #region Tìm kiếm

                using (SqlConnection con = new SqlConnection(Class.datatabase.connect))
                {
                    string select = @"select kh1.IdKhachHang, kh1.TenKH, kh1.DienThoai, kh1.GioiTinh, CONVERT(VARCHAR(10), kh1.NgaySinh, 103) as NgaySinh, kh1.Diachi, kh1.CMND, ls1.SoKhung, ls1.SoMay, ls1.BienSo, CONVERT(VARCHAR(10), kh1.NgayMua, 103) as NgayMua
                                    from KhachHang kh1, LichSuBaoDuongXe ls1
                                    where Not Exists
                                    (
	                                    select kh2.IdKhachHang, ls2.SoLan
	                                    from KhachHang kh2, LichSuBaoDuongXe ls2
	                                    where kh1.IdKhachHang = kh2.IdKhachHang and kh2.IdKhachHang=ls2.IdKhachHang and kh2.IdCongty=@IdCongty
	                                    and kh2.LoaiKH=2 and ls2.SoLan > ls1.SoLan
                                    ) and kh1.IdKhachHang=ls1.IdKhachHang and kh1.IdCongty=@IdCongty and kh1.LoaiKH=2 and ls1.NgayBaoDuong between @d1 and @d2";

                    try
                    {
                        con.Open();

                        SqlCommand cmd = new SqlCommand(select, con);

                        cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@d1", _tungay);
                        cmd.Parameters.AddWithValue("@d2", _denngay);

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        _tbl = new DataTable();
                        da.Fill(_tbl);
                        con.Close();
                        if (_tbl.Rows.Count > 0)
                        {
                            dgvKhachHang.DataSource = _tbl;
                            groupBox1.Text = @"Kết quả tìm kiếm (" + _tbl.Rows.Count + @" KH)";
                        }
                        else
                        {
                            MessageBox.Show(@"Không tìm thấy khách hàng nào");
                            groupBox1.Text = @"Danh sách khách hàng";
                        }
                    }
                    catch (Exception ex) { MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo"); }
                    finally { con.Close(); }
                }

                #endregion Tìm kiếm
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcKhachKhongDenBaoDuong_Load(object sender, EventArgs e)
        {
            dateTimePickerTuNgay.Value = DateTime.Now;
            dateTimePickerDenNgay.Value = DateTime.Now;
            Initialize_TieuChiLoc();

            if (radioButtonBaoDuongDV.Checked)
                cbb_TieuChiLoc.Enabled = false;
            else
                cbb_TieuChiLoc.Enabled = true;
        }

        private void Initialize_TieuChiLoc()
        {
            string[] arrTieuChiLoc =
            {
                "Bảo dưỡng lần 1",
                "Bảo dưỡng lần 2",
                "Bảo dưỡng lần 3",
                "Bảo dưỡng lần 4",
                "Bảo dưỡng lần 5",
                "Bảo dưỡng lần 6",
                "Bảo dưỡng lần 7",
                "Bảo dưỡng lần 8"
            };

            cbb_TieuChiLoc.DataSource = arrTieuChiLoc;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            if (_tbl == null)
            {
                MessageBox.Show(@"Chưa có dữ liệu khách hàng được tìm thấy.");
            }
            else
            {
                try
                {
                    Export(_tbl, "Danh sach khach hang", "DANH SÁCH KHÁCH HÀNG CHƯA ĐẾN BẢO DƯỠNG");
                }
                catch
                {
                    //
                }
            }
        }

        /// <summary>
        /// Lấy tháng BD từ SMS Config
        /// </summary>
        private void LayThangBd()
        {
            using (SqlConnection con = new SqlConnection(Class.datatabase.connect))
            {
                try
                {
                    con.Open();

                    string select = "SELECT ThangNhan FROM SMSMaintenanceConfig WHERE IdCongTy=@IdCongTy";

                    SqlCommand cmd = new SqlCommand(select, con);

                    cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    con.Close();
                    if (dt.Rows.Count > 0)
                    {
                        _thangBd = dt.Rows[0][0].ToString();
                    }
                    else
                    {
                        MessageBox.Show(@"Công ty chưa cài đặt lịch bảo dưỡng định kỳ! Vui lòng đặt lịch bảo dưỡng định kỳ.", @"Thông Báo");
                    }
                }
                catch (Exception ex) {

                    MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo");
                    con.Close(); 
                }
            }
        }

        /// <summary>
        /// Tìm ngày mua xe theo lần bảo dưỡng
        /// </summary>
        /// <param name="thangBd">Tháng xe cần bảo dưỡng</param>
        private void TinhToan(int thangBd)
        {
            thangBd = 0 - thangBd;

            _tungay = dateTimePickerTuNgay.Value.Date.AddMonths(thangBd);

            _denngay = dateTimePickerDenNgay.Value.Date.AddMonths(thangBd);
        }

        private void radioButtonBaoDuongDV_CheckedChanged(object sender, EventArgs e)
        {
            cbb_TieuChiLoc.SelectedIndex = -1;
            cbb_TieuChiLoc.Enabled = false;
        }

        private void radioButtonBaoDuongDK_CheckedChanged(object sender, EventArgs e)
        {
            cbb_TieuChiLoc.Enabled = true;
        }
    }
}