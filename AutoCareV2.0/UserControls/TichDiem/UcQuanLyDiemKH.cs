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

namespace AutoCareV2._0.UserControls.TichDiem
{
    public partial class UcQuanLyDiemKH : UserControl
    {
        private string cn = Class.datatabase.connect;
        private DataTable dt = new DataTable("KhachHang");
        private DataTable dtcty = new DataTable("CongTy");
        private DataTable dtnkh = new DataTable("NhomKH");
        private SqlDataAdapter da = new SqlDataAdapter();
        private static int pagesize = 20;
        private SqlConnection con;
        private string idTichDiem = "";
        private int _rowIndexCellMouseDown = -1;

        public UcQuanLyDiemKH()
        {
            InitializeComponent();
            panelEx1.HorizontalScroll.Enabled = true;
            panelEx1.HorizontalScroll.Visible = true;
            panelEx1.VerticalScroll.Visible = true;
            panelEx1.VerticalScroll.Enabled = true;

            dataGridView1.CellMouseDown += dataGridView1_CellMouseDown;
            NapTienToolStripMenuItem.Click += NapTienToolStripMenuItem_Click;
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

        //private void getdata()
        //{
        //    string sql = "Select kh.IdKhachHang as 'Mã khách hàng',kh.TenKH as 'Tên khách hàng',kh.DienThoai as 'Số điện thoại',td.idCongty as 'Mã công ty',td.TongDiem as 'Tổng điểm', diemConLai as 'Điểm hiện tại',ngayTao as 'Ngày tạo' from TichDiem td join KhachHang kh on td.IdKhachHang=kh.IdKhachHang where td.idcongty=" + Class.CompanyInfo.idcongty;
        //    da = new SqlDataAdapter(sql, con);
        //    dt.Clear();
        //    da.Fill(dt);
        //}

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
                Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "G1");
                head.MergeCells = true;
                head.Value2 = title;
                head.Font.Bold = true;
                head.Font.Name = "Tahoma";
                head.Font.Size = "18";
                head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Tạo tiêu đề cột
                Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
                cl1.Value2 = "Mã khách hàng";
                cl1.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
                cl2.Value2 = "Tên khách hàng";
                cl2.ColumnWidth = 10.5;

                Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
                cl3.Value2 = "Số điện thoại";
                cl3.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
                cl4.Value2 = "Mã công ty";
                cl4.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
                cl5.Value2 = "Tổng điểm";
                cl5.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
                cl6.Value2 = "Điểm hiện tại";
                cl6.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
                cl7.Value2 = "Ngày tạo";
                cl7.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "G3");
                rowHead.Font.Bold = true;

                // Kẻ viền
                rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                // Thiết lập màu nền
                rowHead.Interior.ColorIndex = 15;
                rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Tạo mẳng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,
                // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.
                if (dt != null && dt.Rows.Count > 0)
                {
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

        #region Tìm khách hàng

        private void FindKhachHang()
        {
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
            if (txtSDT.Text != "") where += " and kh.DienThoai like '%" + txtSDT.Text.Replace("'", "''") + "%'";

            if (txtTenKH.Text != "") where += " and kh.TenKH like N'%" + txtTenKH.Text.Replace("'", "''") + "%'";

            select = @"select * from (
                    Select distinct td.IdTichDiem as id_TD,kh.TenKH,kh.DienThoai,td.idCongty,td.TongDiem, diemConLai,Row_Number() over (order by kh.idkhachHang) as rownum from TichDiem td join KhachHang kh on td.dienthoai=kh.dienthoai where td.idcongty=" + Class.CompanyInfo.idcongty;

            if (where != "") select += where;
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

                    totalpage = (totalrecord % pagesize != 0) ? totalrecord / pagesize + 1 : totalpage / pagesize;
                    top = curentpage * pagesize - pagesize + 1;
                    end = curentpage * pagesize;
                    lblTotalPage.Text = totalpage.ToString();
                }
                catch
                {
                    //
                }
            }

            select = @"Select id_TD,TenKH,DienThoai,idCongty,TongDiem, diemConLai " + select.Remove(0, 9);
            select += " ) tbl where rownum between " + top.ToString() + " and " + end.ToString();
            da = new SqlDataAdapter(select, con);
            dt.Clear();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                dataGridView1.DataSource = dt;
                object sms = new SqlCommand("select COUNT(*) from KhachHang where IdCongty=" + Class.CompanyInfo.idcongty, con).ExecuteScalar();
                lbl_SoKH.Text = @"Số KH : " + totalSum;
            }
            else
            {
                lbl_SoKH.Text = "";
                MessageBox.Show(@"Không tìm thấy khách hàng nào");
            }
        }

        #endregion Tìm khách hàng

        private void UcQuanLyKhachHang_Load(object sender, EventArgs e)
        {
            panelEx1.AutoScroll = true;
            panelEx1.VerticalScroll.Visible = false;
            panelEx1.VerticalScroll.Enabled = false;
            panelEx1.HorizontalScroll.Visible = true;
            panelEx1.HorizontalScroll.Enabled = true;
            panelEx1.HorizontalScroll.Minimum = 0;
            panelEx1.HorizontalScroll.Maximum = 1333;
            Load_cbbSoKH();
            connect();
            //getdata();
        }

        private void cbb_SoKHpage_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_SoKHpage.SelectedIndex != 4)
            {
                pagesize = Convert.ToInt32(cbb_SoKHpage.Text);

                FindKhachHang();
            }
            else
            {
                FindKhachHang();
            }
        }

        private void btn_Export_Click(object sender, EventArgs e)
        {
            DataTable dt = (DataTable)dataGridView1.DataSource;
            Export(dt, "Danh sach", "DANH SÁCH ĐIỂM KHÁCH HÀNG");
        }

        private void btn_Search_Click(object sender, EventArgs e)
        {
            FindKhachHang();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            idTichDiem = dataGridView1.Rows[e.RowIndex].Cells["id_TD"].Value.ToString();
            FrmLichSuTichDiem frm = new FrmLichSuTichDiem();
            frm.idTichDiem = idTichDiem;
            frm.ShowDialog();
        }

        private void dataGridView1_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                dataGridView1.ContextMenuStrip = contextMenuStripThongTinKhachHang;

                try
                {
                    _rowIndexCellMouseDown = dataGridView1.CurrentRow.Index;
                }
                catch (Exception ex)
                {
                    _rowIndexCellMouseDown = -1;
                }
            }
            else
            {
                dataGridView1.ContextMenuStrip = null;
                _rowIndexCellMouseDown = -1;
            }
        }

        private void NapTienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            long? idKhachHang = null;
            if (dataGridView1.Rows[_rowIndexCellMouseDown].Cells["IdKhachHang"].Value != null)
                idKhachHang = Convert.ToInt64(dataGridView1.Rows[_rowIndexCellMouseDown].Cells["IdKhachHang"].Value.ToString());
            FrmLichSuTichDiem frm = new FrmLichSuTichDiem();
            frm.idKhachHang = idKhachHang;
            frm.idTichDiem = dataGridView1.Rows[_rowIndexCellMouseDown].Cells["id_TD"].Value.ToString();
            frm.tenKH = dataGridView1.Rows[_rowIndexCellMouseDown].Cells["TenKH"].Value.ToString();
            frm.sdt = dataGridView1.Rows[_rowIndexCellMouseDown].Cells["DienThoai"].Value.ToString();
            frm.ShowDialog();
        }

        private void txt_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                FindKhachHang();
        }
    }
}