using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace AutoCareV2._0.UserControls.ThongKeBaoDuong
{
    public partial class UcKhachDenBaoDuong : UserControl
    {
        private System.Data.DataTable tbl;

        /// <summary>
        ///
        /// </summary>
        public UcKhachDenBaoDuong()
        {
            InitializeComponent();

            dataGridView1.AutoGenerateColumns = false;
        }

        /// <summary>
        /// Xuất dữ liệu ra file Excel
        /// </summary>
        /// <param name="dt">Dữ liệu DataTable</param>
        /// <param name="sheetName">Tên Sheet</param>
        /// <param name="title">Tiêu đề</param>
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
                Excel.Range head = oSheet.get_Range("A1", "O1");
                head.MergeCells = true;
                head.Value2 = title;
                head.Font.Bold = true;
                head.Font.Name = "Tahoma";
                head.Font.Size = "18";
                head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Tạo tiêu đề cột
                Excel.Range cl100 = oSheet.get_Range("A3", "A3");
                cl100.Value2 = "STT";
                cl100.ColumnWidth = 8.0;

                Excel.Range cl1 = oSheet.get_Range("B3", "B3");
                cl1.Value2 = "Tên KH";
                cl1.ColumnWidth = 15.0;

                Excel.Range cl2 = oSheet.get_Range("C3", "C3");
                cl2.Value2 = "Điện Thoại";
                cl2.ColumnWidth = 10.0;

                Excel.Range cl3 = oSheet.get_Range("D3", "D3");
                cl3.Value2 = "Giới Tính";
                cl3.ColumnWidth = 10.0;

                Excel.Range cl4 = oSheet.get_Range("E3", "E3");
                cl4.Value2 = "Địa chỉ";
                cl4.ColumnWidth = 15.0;

                Excel.Range cl5 = oSheet.get_Range("F3", "F3");
                cl5.Value2 = "Tên xe";
                cl5.ColumnWidth = 20.0;

                Excel.Range cl7 = oSheet.get_Range("G3", "G3");
                cl7.Value2 = "Biển Số";
                cl7.ColumnWidth = 25.0;

                Excel.Range cl8 = oSheet.get_Range("H3", "H3");
                cl8.Value2 = "Số Khung";
                cl8.ColumnWidth = 15.0;

                Excel.Range cl10 = oSheet.get_Range("I3", "I3");
                cl10.Value2 = "Ngày bảo dưỡng";
                cl10.ColumnWidth = 15.0;

                Excel.Range cl11 = oSheet.get_Range("J3", "J3");
                cl11.Value2 = "Khách Đến Từ";
                cl11.ColumnWidth = 10.0;

                Excel.Range cl12 = oSheet.get_Range("K3", "K3");
                cl12.Value2 = "Lần Bảo Dưỡng";
                cl12.ColumnWidth = 10.0;

                Excel.Range cl13 = oSheet.get_Range("L3", "L3");
                cl13.Value2 = "Tiền phụ tùng";
                cl13.ColumnWidth = 20.0;

                Excel.Range cl14 = oSheet.get_Range("M3", "M3");
                cl14.Value2 = "Tiền công thợ";
                cl14.ColumnWidth = 20.0;

                Excel.Range cl15 = oSheet.get_Range("N3", "N3");
                cl15.Value2 = "Ghi Chú";
                cl15.ColumnWidth = 20.0;

                Excel.Range cl16 = oSheet.get_Range("O3", "O3");
                cl16.Value2 = "Loại BD";
                cl16.ColumnWidth = 20.0;

                Excel.Range cl17 = oSheet.get_Range("P3", "P3");
                cl17.Value2 = "Thợ sửa chữa";
                cl17.ColumnWidth = 20.0;

                Excel.Range cl18 = oSheet.get_Range("Q3", "Q3");
                cl18.Value2 = "Kỹ thuật cuối";
                cl18.ColumnWidth = 20.0;

                Excel.Range rowHead = oSheet.get_Range("A3", "Q3");
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
                Excel.Range c1 = (Excel.Range)oSheet.Cells[rowStart, columnStart];
                // Ô kết thúc điền dữ liệu
                Excel.Range c2 = (Excel.Range)oSheet.Cells[rowEnd, columnEnd];
                // Lấy về vùng điền dữ liệu
                Excel.Range range = oSheet.get_Range(c1, c2);

                //Điền dữ liệu vào vùng đã thiết lập
                range.Value2 = arr;

                //Định dạng dữ liệu
                Excel.Range cc1 = (Excel.Range)oSheet.Cells[rowStart, 9];
                Excel.Range cc2 = (Excel.Range)oSheet.Cells[rowEnd, 9];
                Excel.Range rangeTonTruoc = oSheet.get_Range(cc1, cc2);
                rangeTonTruoc.NumberFormat = "DD/MM/YYYY";

                // Kẻ viền
                range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                // Căn giữa cột STT
                Excel.Range c3 = (Excel.Range)oSheet.Cells[rowEnd, columnStart];
                Excel.Range c4 = oSheet.get_Range(c1, c3);
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcKhachDenBaoDuong_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;

            Initialize_TieuChiLoc();
            LoadMaintenanceTypes();
        }

        private void Initialize_TieuChiLoc()
        {
            string[] arrTieuChiLoc = 
            {
                "Tất cả",
                "Khách đến lần 1" ,
                "Khách đến lần 2" ,
                "Khách đến lần 3" ,
                "Khách đến lần 4" ,
                "Khách đến lần 5" ,
                "Khách đến lần 6" ,
                "Khách đến lần 7" ,
                "Khách đến lần 8" 
            };

            cbb_TieuChiLoc.DataSource = arrTieuChiLoc;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnfind_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(Class.datatabase.connect))
            {
                con.Open();
                string select = @"select distinct kh.IdKhachHang as 'Mã KH',HoKH,TenKH,DienThoai,GioiTinh,NgaySinh,DiaChi,CMND,
                                (select count(*) from LichSuBaoDuongXe where idkhachhang=tn.idkhachhang and ngaybaoduong between @d1 and @d2) as SoLanBaoDuong
                                from khachhang kh inner join TinNhanLuuTru tn
                                on kh.IdKhachHang=tn.IdKhachHang where kh.Idcongty=@idCongty and
                                tn.smstype like '%Bao duong%' and tn.timesend between @d1 and @d2";
                if (rdbt_Go.Checked)
                {
                    select = "select * from (" + select + ") tbl where solanbaoduong >0";
                }
                else if (rdbt_notGo.Checked)
                {
                    select = "select * from (" + select + ") tbl where solanbaoduong <=0";
                }

                SqlCommand cmd = new SqlCommand(select, con);

                cmd.Parameters.AddWithValue("@idCongty", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@d1", dateTimePicker1.Value.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@d2", dateTimePicker2.Value.ToString("yyyyMMdd") + " 23:59:59");

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                tbl = new DataTable();
                da.Fill(tbl);
                con.Close();
                if (tbl.Rows.Count > 0)
                {
                    dataGridView1.DataSource = tbl;
                    groupBox1.Text = "Kết quả tìm kiếm (" + tbl.Rows.Count.ToString() + " KH)";
                }
                else
                {
                    MessageBox.Show("Không tìm thấy khách hàng nào");
                    dataGridView1.DataSource = tbl;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (tbl == null) { MessageBox.Show("Chưa có dữ liệu khách hàng được tìm thấy."); return; }
            printDialog1.ShowDialog();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExportExcel_Click(object sender, EventArgs e)
        {
            if (tbl == null) { MessageBox.Show("Chưa có dữ liệu khách hàng được tìm thấy."); return; }
            int rowc = tbl.Rows.Count;
            int columc = tbl.Columns.Count;

            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            try
            {
                //Start Excel and get Application object.
                oXL = new Excel.Application();
                oXL.Visible = true;

                //Get a new workbook.
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;

                //Add table headers going cell by cell.
                for (int i = 1; i <= columc; i++)
                {
                    oSheet.Cells[1, i] = tbl.Columns[i - 1].ColumnName;
                }

                //oSheet.Cells[1, 1] = "First Name";
                //oSheet.Cells[1, 2] = "Last Name";
                //oSheet.Cells[1, 3] = "Full Name";
                //oSheet.Cells[1, 4] = "Salary";

                //Format A1:D1 as bold, vertical alignment = center.
                oSheet.get_Range("A1", "I1").Font.Bold = true;
                oSheet.get_Range("A1", "I1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

                // Create an array to multiple values at once.
                string[,] saNames = new string[rowc, columc];
                for (int i = 0; i < rowc; i++)
                {
                    for (int j = 0; j < columc; j++)
                    {
                        saNames[i, j] = tbl.Rows[i][j].ToString();
                    }
                }

                //saNames[0, 0] = "John";
                //saNames[0, 1] = "Smith";
                //saNames[1, 0] = "Tom";
                //saNames[1, 1] = "Brown";
                //saNames[2, 0] = "Sue";
                //saNames[2, 1] = "Thomas";
                //saNames[3, 0] = "Jane";
                //saNames[3, 1] = "Jones";
                //saNames[4, 0] = "Adam";
                //saNames[4, 1] = "Johnson";

                //Fill A2:B6 with an array of values (First and Last Names).
                oSheet.get_Range("A2", "I" + rowc.ToString()).Value2 = saNames;

                ////Fill C2:C6 with a relative formula (=A2 & " " & B2).
                //oRng = oSheet.get_Range("C2", "C6");
                //oRng.Formula = "=A2 & \" \" & B2";

                ////Fill D2:D6 with a formula(=RAND()*100000) and apply format.
                //oRng = oSheet.get_Range("D2", "D6");
                //oRng.Formula = "=RAND()*100000";
                //oRng.NumberFormat = "$0.00";

                //AutoFit columns A:D.
                oRng = oSheet.get_Range("A1", "H1");
                oRng.EntireColumn.AutoFit();

                //Manipulate a variable number of columns for Quarterly Sales Data.
                //DisplayQuarterlySales(oSheet);

                //Make sure Excel is visible and give the user control
                //of Microsoft Excel's lifetime.
                oXL.Visible = true;
                oXL.UserControl = true;
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

        /// <summary>
        ///
        /// </summary>
        /// <param name="oWS"></param>
        private void DisplayQuarterlySales(Excel._Worksheet oWS)
        {
            Excel._Workbook oWB;
            Excel.Series oSeries;
            Excel.Range oResizeRange;
            Excel._Chart oChart;
            String sMsg;
            int iNumQtrs;

            //Determine how many quarters to display data for.
            for (iNumQtrs = 4; iNumQtrs >= 2; iNumQtrs--)
            {
                sMsg = "Enter sales data for ";
                sMsg = String.Concat(sMsg, iNumQtrs);
                sMsg = String.Concat(sMsg, " quarter(s)?");

                DialogResult iRet = MessageBox.Show(sMsg, "Quarterly Sales?",
                    MessageBoxButtons.YesNo);
                if (iRet == DialogResult.Yes)
                    break;
            }

            sMsg = "Displaying data for ";
            sMsg = String.Concat(sMsg, iNumQtrs);
            sMsg = String.Concat(sMsg, " quarter(s).");

            MessageBox.Show(sMsg, "Quarterly Sales");

            //Starting at E1, fill headers for the number of columns selected.
            oResizeRange = oWS.get_Range("E1", "E1").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Formula = "=\"Q\" & COLUMN()-4 & CHAR(10) & \"Sales\"";

            //Change the Orientation and WrapText properties for the headers.
            oResizeRange.Orientation = 38;
            oResizeRange.WrapText = true;

            //Fill the interior color of the headers.
            oResizeRange.Interior.ColorIndex = 36;

            //Fill the columns with a formula and apply a number format.
            oResizeRange = oWS.get_Range("E2", "E6").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Formula = "=RAND()*100";
            oResizeRange.NumberFormat = "$0.00";

            //Apply borders to the Sales data and headers.
            oResizeRange = oWS.get_Range("E1", "E6").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Borders.Weight = Excel.XlBorderWeight.xlThin;

            //Add a Totals formula for the sales data and apply a border.
            oResizeRange = oWS.get_Range("E8", "E8").get_Resize(Missing.Value, iNumQtrs);
            oResizeRange.Formula = "=SUM(E2:E6)";
            oResizeRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle
                = Excel.XlLineStyle.xlDouble;
            oResizeRange.Borders.get_Item(Excel.XlBordersIndex.xlEdgeBottom).Weight
                = Excel.XlBorderWeight.xlThick;

            //Add a Chart for the selected data.
            oWB = (Excel._Workbook)oWS.Parent;
            oChart = (Excel._Chart)oWB.Charts.Add(Missing.Value, Missing.Value,
                Missing.Value, Missing.Value);

            //Use the ChartWizard to create a new chart from the selected data.
            oResizeRange = oWS.get_Range("E2:E6", Missing.Value).get_Resize(
                Missing.Value, iNumQtrs);
            oChart.ChartWizard(oResizeRange, Excel.XlChartType.xl3DColumn, Missing.Value,
                Excel.XlRowCol.xlColumns, Missing.Value, Missing.Value, Missing.Value,
                Missing.Value, Missing.Value, Missing.Value, Missing.Value);
            oSeries = (Excel.Series)oChart.SeriesCollection(1);
            oSeries.XValues = oWS.get_Range("A2", "A6");
            for (int iRet = 1; iRet <= iNumQtrs; iRet++)
            {
                oSeries = (Excel.Series)oChart.SeriesCollection(iRet);
                String seriesName;
                seriesName = "=\"Q";
                seriesName = String.Concat(seriesName, iRet);
                seriesName = String.Concat(seriesName, "\"");
                oSeries.Name = seriesName;
            }

            oChart.Location(Excel.XlChartLocation.xlLocationAsObject, oWS.Name);

            //Move the chart so as not to cover your data.
            oResizeRange = (Excel.Range)oWS.Rows.get_Item(10, Missing.Value);
            oWS.Shapes.Item("Chart 1").Top = (float)(double)oResizeRange.Top;
            oResizeRange = (Excel.Range)oWS.Columns.get_Item(2, Missing.Value);
            oWS.Shapes.Item("Chart 1").Left = (float)(double)oResizeRange.Left;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            string i = Convert.ToString(cbb_TieuChiLoc.SelectedIndex);

            dataGridView1.DataSource = null;

            using (SqlConnection con = new SqlConnection(Class.datatabase.connect))
            {
                if (Convert.ToInt32(i) >= 0)
                {
                    try
                    {
                        con.Open();
                        
                        SqlCommand cmd = new SqlCommand
                        {
                            Connection = con,
                            CommandText = "sp_search_khachbaoduong_new",
                            //CommandText = "sp_search_khachbaoduong_byduocnv",
                            CommandType = CommandType.StoredProcedure
                        };
                        cmd.CommandTimeout = 0;
                        cmd.Parameters.AddWithValue("@idCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@ngayTu", dateTimePicker1.Value.ToString("yyyyMMdd"));
                        cmd.Parameters.AddWithValue("@ngayDen", dateTimePicker2.Value.ToString("yyyyMMdd")+ " 23:59:59");
                        cmd.Parameters.AddWithValue("@isGiaoXe", cbNgayMuaXe.Checked);
                        cmd.Parameters.AddWithValue("@isDinhKy", cb_DBDinhKy.Checked);
                        cmd.Parameters.AddWithValue("@isDichVu", cb_BDDichVu.Checked);
                        cmd.Parameters.AddWithValue("@loaiBaoDuong", comboBoxLoaiBaoDuong.SelectedValue.ToString());
                        cmd.Parameters.AddWithValue("@lanBaoDuong", cbb_TieuChiLoc.SelectedIndex);
                        cmd.Parameters.AddWithValue("@bienSo", txtBienSo.Text.Trim());
                        cmd.Parameters.AddWithValue("@soKhung", txtSoKhung.Text.Trim());

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        tbl = new DataTable();
                        da.Fill(tbl);
                        if (tbl.Rows.Count > 0)
                        {
                            dataGridView1.DataSource = tbl;
                            groupBox1.Text = @"Kết quả tìm kiếm (" + tbl.Rows.Count.ToString() + @" KH)";

                            con.Close();
                        }
                        else
                        {
                            MessageBox.Show(@"Không tìm thấy khách hàng nào!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            dataGridView1.DataSource = tbl;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    finally
                    {
                        con.Close();
                    }
                }
                else
                {
                    MessageBox.Show(@"Bạn chưa chọn tiêu chí lọc!", @"Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (tbl == null) { MessageBox.Show(@"Chưa có dữ liệu khách hàng được tìm thấy!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            printDialog1.ShowDialog();
        }

        #region Lấy loại bảo dưỡng

        private void LoadMaintenanceTypes()
        {
            Dictionary<string, string> MaintenanceTypes = new Dictionary<string, string>();

            MaintenanceTypes.Add("", "Tất cả");
            MaintenanceTypes.Add(Keywords.MaintenanceTypes.VehicleWarranty, "Xe bảo hành");
            MaintenanceTypes.Add(Keywords.MaintenanceTypes.LightMaintenance, "Bảo dưỡng nhẹ");
            MaintenanceTypes.Add(Keywords.MaintenanceTypes.HevyMaintenance, "Bảo dưỡng nặng");
            comboBoxLoaiBaoDuong.DisplayMember = "Value";
            comboBoxLoaiBaoDuong.ValueMember = "Key";
            comboBoxLoaiBaoDuong.DataSource = new BindingSource(MaintenanceTypes, null);
        }

        #endregion Lấy loại bảo dưỡng

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX3_Click(object sender, EventArgs e)
        {
            #region "Code cũ"
            //if (tbl == null) { MessageBox.Show("Chưa có dữ liệu khách hàng được tìm thấy."); return; }
            //int rowc = tbl.Rows.Count;
            //int columc = tbl.Columns.Count;

            //Excel.Application oXL;
            //Excel._Workbook oWB;
            //Excel._Worksheet oSheet;
            //Excel.Range oRng;

            //try
            //{
            //    //Start Excel and get Application object.
            //    oXL = new Excel.Application();
            //    oXL.Visible = true;

            //    //Get a new workbook.
            //    oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
            //    oSheet = (Excel._Worksheet)oWB.ActiveSheet;

            //    //Add table headers going cell by cell.
            //    for (int i = 1; i <= columc; i++)
            //    {
            //        oSheet.Cells[1, i] = tbl.Columns[i - 1].ColumnName;
            //    }

            //    //oSheet.Cells[1, 1] = "First Name";
            //    //oSheet.Cells[1, 2] = "Last Name";
            //    //oSheet.Cells[1, 3] = "Full Name";
            //    //oSheet.Cells[1, 4] = "Salary";

            //    //Format A1:D1 as bold, vertical alignment = center.
            //    oSheet.get_Range("A1", "I1").Font.Bold = true;
            //    oSheet.get_Range("A1", "I1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;

            //    // Create an array to multiple values at once.
            //    string[,] saNames = new string[rowc, columc];
            //    for (int i = 0; i < rowc; i++)
            //    {
            //        for (int j = 0; j < columc; j++)
            //        {
            //            saNames[i, j] = tbl.Rows[i][j].ToString();
            //        }
            //    }

            //    //saNames[0, 0] = "John";
            //    //saNames[0, 1] = "Smith";
            //    //saNames[1, 0] = "Tom";
            //    //saNames[1, 1] = "Brown";
            //    //saNames[2, 0] = "Sue";
            //    //saNames[2, 1] = "Thomas";
            //    //saNames[3, 0] = "Jane";
            //    //saNames[3, 1] = "Jones";
            //    //saNames[4, 0] = "Adam";
            //    //saNames[4, 1] = "Johnson";

            //    //Fill A2:B6 with an array of values (First and Last Names).
            //    oSheet.get_Range("A2", "I" + rowc.ToString()).Value2 = saNames;

            //    ////Fill C2:C6 with a relative formula (=A2 & " " & B2).
            //    //oRng = oSheet.get_Range("C2", "C6");
            //    //oRng.Formula = "=A2 & \" \" & B2";

            //    ////Fill D2:D6 with a formula(=RAND()*100000) and apply format.
            //    //oRng = oSheet.get_Range("D2", "D6");
            //    //oRng.Formula = "=RAND()*100000";
            //    //oRng.NumberFormat = "$0.00";

            //    //AutoFit columns A:D.
            //    oRng = oSheet.get_Range("A1", "H1");
            //    oRng.EntireColumn.AutoFit();

            //    //Manipulate a variable number of columns for Quarterly Sales Data.
            //    //DisplayQuarterlySales(oSheet);

            //    //Make sure Excel is visible and give the user control
            //    //of Microsoft Excel's lifetime.
            //    oXL.Visible = true;
            //    oXL.UserControl = true;
            //}
            //catch (Exception theException)
            //{
            //    String errorMessage;
            //    errorMessage = "Error: ";
            //    errorMessage = String.Concat(errorMessage, theException.Message);
            //    errorMessage = String.Concat(errorMessage, " Line: ");
            //    errorMessage = String.Concat(errorMessage, theException.Source);

            //    MessageBox.Show(errorMessage, "Error");
            //}
            #endregion

            #region "Code Mới"

            if (tbl == null)
            {
                MessageBox.Show(@"Chưa có dữ liệu khách hàng được tìm thấy!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Export(tbl, "Danh sach khach hang", "DANH SÁCH KHÁCH HÀNG ĐẾN BẢO DƯỠNG");
            }
            catch
            {
                //
            }

            #endregion
        }
    }
}