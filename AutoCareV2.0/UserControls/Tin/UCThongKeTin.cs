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

namespace AutoCareV2._0.UserControls.Tin
{
    public partial class UCThongKeTin : UserControl
    {
        int TotalCheckBoxes = 0;
        int TotalCheckedCheckBoxes = 0;
        CheckBox HeaderCheckBox = null;
        bool IsHeaderCheckBoxClicked = false;
        private int rownumber = -1;

        private string cn = Class.datatabase.connect;
        private DataTable dttn = new DataTable("TinNhan");
        private DataTable dtsms = new DataTable("SMSConfig");
        private DataTable dtthieu = new DataTable("ThuongHieu");
        private SqlDataAdapter da = new SqlDataAdapter();
        private SqlConnection con;

        public UCThongKeTin()
        {
            InitializeComponent();
        }

        private void connect()
        {
            string cn = Class.datatabase.connect;
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
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = @"Select type from smsconfig where idcongty=" + Class.CompanyInfo.idcongty;
            da.SelectCommand = cmd;
            da.Fill(dtsms);
            dtsms.Columns.Add("Tuy bien");
            cboloaitn.DataSource = dtsms;
            cboloaitn.DisplayMember = "type";
            cboloaitn.ValueMember = "type";
        }

        private void disconnect()
        {
            con.Close();//dong ket noi
            con.Dispose();//giai phong tai nguyen
            con = null;//huy doi tuong
        }

        private void UCThongKeTin_Load(object sender, EventArgs e)
        {
            dttungay.Value = DateTime.Now;
            dtdenngay.Value = DateTime.Now;

            connect();
            getdata();
        }

        private void grvtn_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            try
            { 
                if (e.RowIndex == -1 && e.ColumnIndex == 0)
                    ResetHeaderCheckBoxLocation(e.ColumnIndex, e.RowIndex);
            }
            catch { }
        }

        private void ResetHeaderCheckBoxLocation(int ColumnIndex, int RowIndex)
        {
            try
            {
                //Get the column header cell bounds
                Rectangle oRectangle = this.grvtn.GetCellDisplayRectangle(ColumnIndex, RowIndex, true);

                Point oPoint = new Point();

                oPoint.X = oRectangle.Location.X + (oRectangle.Width - HeaderCheckBox.Width) / 2 + 1;
                oPoint.Y = oRectangle.Location.Y + (oRectangle.Height - HeaderCheckBox.Height) / 2 + 1;

                //Change the location of the CheckBox to make it stay on the header
                HeaderCheckBox.Location = oPoint;
            }
            catch { }
        }

        private void grvtn_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            try
            {
                if (grvtn.CurrentCell is DataGridViewCheckBoxCell)
                    grvtn.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
            catch { }
        }

        private void grvtn_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (!IsHeaderCheckBoxClicked)
                    RowCheckBoxClick((DataGridViewCheckBoxCell)grvtn[e.ColumnIndex, e.RowIndex]);
            }
            catch { }
        }

        private void RowCheckBoxClick(DataGridViewCheckBoxCell RCheckBox)
        {
            try
            {
                if (RCheckBox != null)
                {
                    //Modifiy Counter;            
                    if ((bool)RCheckBox.Value && TotalCheckedCheckBoxes < TotalCheckBoxes)
                        TotalCheckedCheckBoxes++;
                    else if (TotalCheckedCheckBoxes > 0)
                        TotalCheckedCheckBoxes--;

                    //Change state of the header CheckBox.
                    if (TotalCheckedCheckBoxes < TotalCheckBoxes)
                        HeaderCheckBox.Checked = false;
                    else if (TotalCheckedCheckBoxes == TotalCheckBoxes)
                        HeaderCheckBox.Checked = true;
                }
            }
            catch { }
        }

        private void HeaderCheckBox_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                HeaderCheckBoxClick((CheckBox)sender);
            }
            catch { }
        }

        private void HeaderCheckBoxClick(CheckBox HCheckBox)
        {
            try
            {
                IsHeaderCheckBoxClicked = true;

                foreach (DataGridViewRow Row in grvtn.Rows)
                    ((DataGridViewCheckBoxCell)Row.Cells["chkBxSelect"]).Value = HCheckBox.Checked;

                grvtn.RefreshEdit();

                TotalCheckedCheckBoxes = HCheckBox.Checked ? TotalCheckBoxes : 0;

                IsHeaderCheckBoxClicked = false;
            }
            catch { }
        }

        private void HeaderCheckBox_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Space)
                    HeaderCheckBoxClick((CheckBox)sender);
            }
            catch { }
        }

        private void AddHeaderCheckBox()
        {
            try
            {
                HeaderCheckBox = new CheckBox();

                HeaderCheckBox.Size = new Size(15, 15);

                //Add the CheckBox into the DataGridView
                this.grvtn.Controls.Add(HeaderCheckBox);
            }
            catch { }
        }

        private void btnxem_Click(object sender, EventArgs e)
        {
            dttn.Clear();
            string loaitin = "";
            if (cboloaitn.SelectedValue != null && cboloaitn.SelectedValue.ToString().Trim() != "") { loaitin = cboloaitn.SelectedValue.ToString(); }

            SqlCommand cmd = new SqlCommand(@"select smsid as 'Mã tin nhắn', IdKhachHang as'Mã KH', phone as 'Điện thoại',sms as 'Nội dung',countmes as 'Bản tin',SenderName as 'Thương hiệu',
                                smstype as'Loại tin',timesend as'Thời gian gửi',trangthai as 'Trạng thái gửi' from TinNhanLuuTru where smstype like '%'+@smstype + '%'
                                and idcongty=@idcongty and timesend between @df and @dt and phone like '%'+@phone+'%'", con);

            cmd.Parameters.AddWithValue("@smstype", loaitin);
            cmd.Parameters.AddWithValue("@df", dttungay.Value.ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@dt", dtdenngay.Value.ToString("yyyyMMdd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);

            SqlCommand cmd2 = new SqlCommand(@"select smsid as 'Mã tin nhắn', idkhachhang as' Mã khách hàng', phone as 'Điện thoại',countmes as 'Bản tin',
                                sendername as 'Thương hiệu', smstype as 'Loại tin nhắn', timeschedule as 'Thời gian nhắn' from TinNhan where smstype like @smstype and idcongty=@idcongty and timeSchedule between @df and @dt and phone like '%'+@phone+'%'", con);
            cmd2.Parameters.AddWithValue("@smstype", loaitin);
            cmd2.Parameters.AddWithValue("@df", dttungay.Value.ToString("yyyyMMdd"));
            cmd2.Parameters.AddWithValue("@dt", dtdenngay.Value.ToString("yyyyMMdd") + " 23:59:59");
            cmd2.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cmd2.Parameters.AddWithValue("@phone", txtPhone.Text);

            SqlCommand cmd3 = new SqlCommand(@"select smsid as 'Mã tin nhắn', IdKhachHang as'Mã KH', phone as 'Điện thoại',sms as 'Nội dung',countmes as 'Bản tin',SenderName as 'Thương hiệu',
                                smstype as'Loại tin',timesend as'Thời gian gửi',trangthai as 'Trạng thái gửi' from TinNhanLuuTru where smstype = 'Tuy bien'
                                and idcongty=@idcongty and timesend between @df and @dt and phone like '%'+@phone+'%'", con);

            cmd3.Parameters.AddWithValue("@df", dttungay.Value.ToString("yyyyMMdd"));
            cmd3.Parameters.AddWithValue("@dt", dtdenngay.Value.ToString("yyyyMMdd") + " 23:59:59");
            cmd3.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cmd3.Parameters.AddWithValue("@phone", txtPhone.Text);

            string status = "";

            if (rDaNhan.Checked)
            {
                da.SelectCommand = cmd; status = "Tin nhắn đã nhắn";
            }
            else if (rDangNhan.Checked)
            {
                da.SelectCommand = cmd2; status = "Tin nhắn đang nhắn";
            }
            else if (rTuyBien.Checked)
            {
                da.SelectCommand = cmd3; status = "Tin nhắn tùy biến";
            }
            dttn = new DataTable();
            da.Fill(dttn);
            int total = 0;

            foreach (DataRow r in dttn.Rows)
            {
                total += int.Parse(r["Bản tin"].ToString());
            }
            //string smsSuccessfull = new SqlCommand("select COUNT(*) from TinNhanLuuTru where timesend between '" + dttungay.Value.ToString("yyyyMMdd") + "' and '" + dtdenngay.Value.ToString("yyyyMMdd") + " 23:59:59' and trangthai like 'Successfull%'", con).ExecuteScalar().ToString();

            lblTotalSms.Text = "Tổng số tin nhắn: " + total;

            if (dttn.Rows.Count > 0)
            {
                grvtn.DataSource = dttn;
            }
            else
            {
                MessageBox.Show("Không tìm thấy " + status + " từ " + dttungay.Value.ToString("dd/MM/yyyy") + " - " + dtdenngay.Value.ToString("dd/MM/yyyy"));
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (dttn == null) { MessageBox.Show("Chưa có dữ liệu khách hàng được tìm thấy."); return; }
            int rowc = dttn.Rows.Count;
            int columc = dttn.Columns.Count;
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            try
            {
                oXL = new Excel.Application();
                oXL.Visible = true;
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                for (int i = 1; i <= columc; i++)
                {
                    oSheet.Cells[1, i] = dttn.Columns[i - 1].ColumnName;
                }
                oSheet.get_Range("A1", "J1").Font.Bold = true;
                oSheet.get_Range("A1", "J1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                string[,] saNames = new string[rowc, columc];
                for (int i = 0; i < rowc; i++)
                {
                    for (int j = 0; j < columc; j++)
                    {
                        saNames[i, j] = dttn.Rows[i][j].ToString();
                    }
                }
                oSheet.get_Range("A2", "J" + rowc).Value2 = saNames;
                oRng = oSheet.get_Range("A1", "J1");
                oRng.EntireColumn.AutoFit();
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

        private void buttonX1_Click(object sender, EventArgs e)
        {
            dttn.Clear();
            string loaitin = "";

            if (cboloaitn.SelectedValue != null && cboloaitn.SelectedValue.ToString().Trim() != "") { loaitin = cboloaitn.SelectedValue.ToString(); }

            SqlCommand cmd = new SqlCommand(@"select smsid as 'Mã tin nhắn', IdKhachHang as'Mã KH', phone as 'Điện thoại',sms as 'Nội dung',countmes as 'Bản tin',SenderName as 'Thương hiệu',
                                            smstype as'Loại tin',timesend as'Thời gian gửi',trangthai as 'Trạng thái gửi' from TinNhanLuuTru where smstype like '%'+@smstype + '%'
                                            and idcongty=@idcongty and timesend between @df and @dt and phone like '%'+@phone+'%'", con);

            cmd.Parameters.AddWithValue("@smstype", loaitin);
            cmd.Parameters.AddWithValue("@df", dttungay.Value.ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@dt", dtdenngay.Value.ToString("yyyyMMdd") + " 23:59:59");
            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@phone", txtPhone.Text);

            SqlCommand cmd2 = new SqlCommand(@"select smsid as 'Mã tin nhắn', idkhachhang as' Mã khách hàng', phone as 'Điện thoại',countmes as 'Bản tin',
                                             sendername as 'Thương hiệu', smstype as 'Loại tin nhắn', timeschedule as 'Thời gian nhắn' from TinNhan where smstype like @smstype and idcongty=@idcongty and timeSchedule between @df and @dt and phone like '%'+@phone+'%'", con);
            cmd2.Parameters.AddWithValue("@smstype", loaitin);
            cmd2.Parameters.AddWithValue("@df", dttungay.Value.ToString("yyyyMMdd"));
            cmd2.Parameters.AddWithValue("@dt", dtdenngay.Value.ToString("yyyyMMdd") + " 23:59:59");
            cmd2.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cmd2.Parameters.AddWithValue("@phone", txtPhone.Text);

            SqlCommand cmd3 = new SqlCommand(@"select smsid as 'Mã tin nhắn', IdKhachHang as'Mã KH', phone as 'Điện thoại',sms as 'Nội dung',countmes as 'Bản tin',SenderName as 'Thương hiệu',
                                             smstype as'Loại tin',timesend as'Thời gian gửi',trangthai as 'Trạng thái gửi' from TinNhanLuuTru where smstype = 'Tuy bien'
                                             and idcongty=@idcongty and timesend between @df and @dt and phone like '%'+@phone+'%'", con);

            cmd3.Parameters.AddWithValue("@df", dttungay.Value.ToString("yyyyMMdd"));
            cmd3.Parameters.AddWithValue("@dt", dtdenngay.Value.ToString("yyyyMMdd") + " 23:59:59");
            cmd3.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cmd3.Parameters.AddWithValue("@phone", txtPhone.Text);

            string status = "";

            if (rDaNhan.Checked)
            {
                da.SelectCommand = cmd; status = "Tin nhắn đã nhắn";
            }
            else if (rDangNhan.Checked)
            {
                da.SelectCommand = cmd2; status = "Tin nhắn đang nhắn";
            }
            else if (rTuyBien.Checked)
            {
                da.SelectCommand = cmd3; status = "Tin nhắn tùy biến";
            }
            dttn = new DataTable();
            da.Fill(dttn);
            //int total = 0;
            //int Success = 0;

            //foreach (DataRow r in dttn.Rows)
            //{
            //    total += int.Parse(r["Bản tin"].ToString());
            //}
            //string smsSuccessfull = new SqlCommand("select COUNT(*) from TinNhanLuuTru where timesend between '" + dttungay.Value.ToString("yyyyMMdd") + "' and '" + dtdenngay.Value.ToString("yyyyMMdd") + " 23:59:59' and trangthai like 'Successfull%'", con).ExecuteScalar().ToString();

            //lblTotalSms.Text = "Tổng số tin nhắn: " + total.ToString();
            //groupBox2.Text = "Danh sách tin nhắn (" + total + " tin)";
            groupBox2.Text = "Danh sách tin nhắn (" + dttn.Rows.Count + " tin)";

            if (dttn.Rows.Count > 0)
            {
                AddHeaderCheckBox();

                HeaderCheckBox.KeyUp += new KeyEventHandler(HeaderCheckBox_KeyUp);
                HeaderCheckBox.MouseClick += new MouseEventHandler(HeaderCheckBox_MouseClick);
                grvtn.CellValueChanged += new DataGridViewCellEventHandler(grvtn_CellValueChanged);
                grvtn.CurrentCellDirtyStateChanged += new EventHandler(grvtn_CurrentCellDirtyStateChanged);
                grvtn.CellPainting += new DataGridViewCellPaintingEventHandler(grvtn_CellPainting);

                grvtn.DataSource = dttn;

                grvtn.Columns["chkBxSelect"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grvtn.Columns["chkBxSelect"].Width = 50;
            }
            else
            {
                MessageBox.Show("Không tìm thấy " + status + " từ " + dttungay.Value.ToString("dd/MM/yyyy") + " - " + dtdenngay.Value.ToString("dd/MM/yyyy"));

                dataGridViewKhachHang.DataSource = null;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            #region
            DataTable dtkh = new DataTable();
            dtkh = (DataTable)dataGridViewKhachHang.DataSource;

            if (dtkh == null)
            {
                MessageBox.Show(@"Chưa có dữ liệu khách hàng được tìm thấy.");
                return;
            }
            else
            {
                //ExportDTToExcel(dtkh);
                Export(dtkh, "Danh sach", "Danh sách khách hàng");
            }
            #endregion
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            using (SqlConnection cnn = Class.datatabase.getConnection())
            {
                cnn.Open();
                DataTable tb = new DataTable();

                DataTable tableIdKhachHangIdSms = new DataTable();
                tableIdKhachHangIdSms.Columns.Add(new DataColumn("IdKhachHang", typeof (long)));
                tableIdKhachHangIdSms.Columns.Add(new DataColumn("IdSms", typeof(long)));
                tableIdKhachHangIdSms.Columns.Add(new DataColumn("IdCongTy", typeof(int)));

                foreach (DataGridViewRow row in grvtn.Rows)
                {
                    try
                    {
                        if ((bool) row.Cells[0].Value && row.Cells[0].Value != null)
                        {
                            var dr = tableIdKhachHangIdSms.NewRow();
                            dr["IdKhachHang"] = row.Cells[2].Value;
                            dr["IdSms"] = row.Cells[1].Value;
                            dr["IdCongTy"] = Class.CompanyInfo.idcongty;

                            tableIdKhachHangIdSms.Rows.Add(dr);
                        }
                    }
                    catch
                    {
                        //
                    }
                }     

                try
                {
                    string smsType = "";
                    string chuoi = cboloaitn.Text.ToUpper();

                    //if (rDaNhan.Checked)
                    //{
                    //    if (!String.IsNullOrEmpty(cboloaitn.Text))
                    //    {
                    //        string chuoi1 = "mua xe".ToUpper();
                    //        string chuoi2 = cboloaitn.Text.ToUpper();

                    //        if ((int)(chuoi2.IndexOf(chuoi1)) >= 0)
                    //        {
                    //            string commandText = @"SELECT DISTINCT kh.TenKH AS 'Tên khách hàng', kh.GioiTinh AS 'Giới Tính',
                    //                                    CONVERT(nchar(10), kh.NgaySinh, 103) AS 'Ngày Sinh', datepart(yy,GETDATE()) - YEAR(kh.NgaySinh) AS 'Tuổi', kh.DienThoai AS 'Điện thoại', kh.Diachi AS 'Địa chỉ',
                    //                                    SoSBH AS 'Số SBH', xdb.SoKhung AS 'Số Khung', xdb.SoMay AS 'Số Máy', xdb.Mauxe AS 'Màu xe', CONVERT(nchar(10), xdb.NgayBan, 103) AS 'Ngày Mua', tnlt.smstype AS 'Loại tin',
                    //                                    CONVERT(nchar(10), tnlt.timesend, 103) AS 'Ngày gửi', CONVERT(nchar(8), tnlt.timesend, 108) AS 'Giờ gửi', tnlt.trangthai AS 'Trạng thái gửi'
                    //                                    FROM KhachHang kh
                    //                                    LEFT JOIN  TinNhanLuuTru tnlt  ON kh.IdKhachHang=tnlt.IdKhachHang
                    //                                    LEFT JOIN XeDaBan xdb ON xdb.IdKhachHang=tnlt.IdKhachHang
                    //                                    WHERE tnlt.IdCongTy=" + Class.CompanyInfo.idcongty + " AND tnlt.IdKhachHang in ("+ arrCusId + ") AND tnlt.smsid in (" + arrSmsId + ")";

                    //            SqlCommand cmd = new SqlCommand(commandText, cnn);
                    //            SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    //            adap.Fill(tb);
                    //        }
                    //        else
                    //        {
                    //            string commandText = @"SELECT DISTINCT kh.TenKH AS 'Tên khách hàng', kh.GioiTinh AS 'Giới tính', CONVERT(nchar(10), kh.NgaySinh, 103) AS 'Ngày sinh',
                    //                                    DATEPART(YY, GETDATE()) - YEAR(kh.NgaySinh) AS 'Tuổi', kh.DienThoai AS 'Điện thoại', kh.Diachi AS 'Địa chỉ', kh.SoSBH AS 'Số SBH',
                    //                                    'Số khung' = CASE WHEN ls.Sokhung IS NOT NULL AND xdb.SoKhung IS NULL THEN ls.Sokhung WHEN xdb.SoKhung IS NOT NULL AND ls.Sokhung IS NULL THEN xdb.SoKhung
                    //                                    WHEN xdb.SoKhung IS NOT NULL AND ls.Sokhung IS NOT NULL THEN xdb.SoKhung WHEN xdb.SoKhung IS NULL AND ls.Sokhung IS NULL THEN '' END,
                    //                                    'Số máy' = CASE WHEN ls.SoMay IS NOT NULL AND xdb.SoMay IS NULL THEN ls.SoMay WHEN xdb.SoMay IS NOT NULL AND ls.SoMay IS NULL THEN xdb.SoMay
                    //                                    WHEN xdb.SoMay IS NOT NULL AND ls.SoMay IS NOT NULL THEN xdb.SoMay WHEN xdb.SoMay IS NULL AND ls.SoMay IS NULL THEN '' END,
                    //                                    'Màu xe' = CASE WHEN xdb.Mauxe IS NOT NULL THEN xdb.Mauxe ELSE '' END, 'Ngày mua' = CASE WHEN xdb.NgayBan IS NOT NULL AND kh.NgayMua IS NULL THEN CONVERT(nchar(10),xdb.NgayBan,103)
                    //                                    WHEN xdb.NgayBan IS NULL AND kh.NgayMua IS NOT NULL THEN CONVERT(nchar(10),kh.NgayMua,103) WHEN xdb.NgayBan IS NOT NULL AND kh.NgayMua IS NOT NULL THEN CONVERT(nchar(10),xdb.NgayBan,103)
                    //                                    WHEN xdb.NgayBan IS NULL AND kh.NgayMua IS NULL THEN '' END, tnlt.smstype AS 'Loại tin', CONVERT(nchar(10), tnlt.timesend, 103) AS 'Ngày gửi', 
                    //                                    CONVERT(nchar(8), tnlt.timesend, 108) AS 'Giờ gửi', tnlt.trangthai AS 'Trạng thái gửi' FROM KhachHang kh
                    //                                    LEFT JOIN LichSuBaoDuongXe ls ON ls.IdKhachHang=kh.IdKhachHang
                    //                                    LEFT JOIN XeDaBan xdb ON xdb.IdKhachHang=kh.IdKhachHang
                    //                                    LEFT JOIN TinNhanLuuTru tnlt ON tnlt.IdKhachHang=kh.IdKhachHang
                    //                                    WHERE tnlt.IdCongTy=" + Class.CompanyInfo.idcongty + " AND tnlt.IdKhachHang in (" + arrCusId + ") AND tnlt.smsid in (" + arrSmsId + ")";

                    //            SqlCommand cmd = new SqlCommand(commandText, cnn);

                    //            SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    //            adap.Fill(tb);
                    //        }
                    //    }
                    //}
                    //if (rTuyBien.Checked)
                    //{
                    //    string commandText = @"SELECT DISTINCT kh.TenKH AS 'Tên khách hàng', kh.GioiTinh AS 'Giới tính', CONVERT(nchar(10), kh.NgaySinh, 103) AS 'Ngày sinh',
                    //                            DATEPART(YY, GETDATE()) - YEAR(kh.NgaySinh) AS 'Tuổi', kh.DienThoai AS 'Điện thoại', kh.Diachi AS 'Địa chỉ', kh.SoSBH AS 'Số SBH',
                    //                            'Số khung' = CASE WHEN ls.Sokhung IS NOT NULL AND xdb.SoKhung IS NULL THEN ls.Sokhung WHEN xdb.SoKhung IS NOT NULL AND ls.Sokhung IS NULL THEN xdb.SoKhung
                    //                            WHEN xdb.SoKhung IS NOT NULL AND ls.Sokhung IS NOT NULL THEN xdb.SoKhung WHEN xdb.SoKhung IS NULL AND ls.Sokhung IS NULL THEN '' END,
                    //                            'Số máy' = CASE WHEN ls.SoMay IS NOT NULL AND xdb.SoMay IS NULL THEN ls.SoMay WHEN xdb.SoMay IS NOT NULL AND ls.SoMay IS NULL THEN xdb.SoMay
                    //                            WHEN xdb.SoMay IS NOT NULL AND ls.SoMay IS NOT NULL THEN xdb.SoMay WHEN xdb.SoMay IS NULL AND ls.SoMay IS NULL THEN '' END,
                    //                            'Màu xe' = CASE WHEN xdb.Mauxe IS NOT NULL THEN xdb.Mauxe ELSE '' END, 'Ngày mua' = CASE WHEN xdb.NgayBan IS NOT NULL AND kh.NgayMua IS NULL THEN CONVERT(nchar(10),xdb.NgayBan,103)
                    //                            WHEN xdb.NgayBan IS NULL AND kh.NgayMua IS NOT NULL THEN CONVERT(nchar(10),kh.NgayMua,103) WHEN xdb.NgayBan IS NOT NULL AND kh.NgayMua IS NOT NULL THEN CONVERT(nchar(10),xdb.NgayBan,103)
                    //                            WHEN xdb.NgayBan IS NULL AND kh.NgayMua IS NULL THEN '' END, tnlt.smstype AS 'Loại tin', CONVERT(nchar(10), tnlt.timesend, 103) AS 'Ngày gửi', 
                    //                            CONVERT(nchar(8), tnlt.timesend, 108) AS 'Giờ gửi', tnlt.trangthai AS 'Trạng thái gửi' FROM KhachHang kh
                    //                            LEFT JOIN LichSuBaoDuongXe ls ON ls.IdKhachHang=kh.IdKhachHang
                    //                            LEFT JOIN XeDaBan xdb ON xdb.IdKhachHang=kh.IdKhachHang
                    //                            LEFT JOIN TinNhanLuuTru tnlt ON tnlt.IdKhachHang=kh.IdKhachHang
                    //                            WHERE tnlt.IdCongTy=" + Class.CompanyInfo.idcongty + " AND tnlt.IdKhachHang in (" + arrCusId + ") AND tnlt.smsid in (" + arrSmsId + ")";

                    //    SqlCommand cmd = new SqlCommand(commandText, cnn);

                    //    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    //    adap.Fill(tb);
                    //}
                    //if (rDangNhan.Checked)
                    //{
                    //    if (!String.IsNullOrEmpty(cboloaitn.Text))
                    //    {
                    //        string chuoi1 = "mua xe".ToUpper();
                    //        string chuoi2 = cboloaitn.Text.ToUpper();

                    //        if ((int)(chuoi2.IndexOf(chuoi1)) >= 0)
                    //        {
                    //            string commandText = @"SELECT kh.TenKH AS 'Tên khách hàng', kh.GioiTinh AS 'Giới Tính',
                    //                                    CONVERT(nchar(10), kh.NgaySinh, 103) AS 'Ngày Sinh', datepart(yy,GETDATE()) - YEAR(kh.NgaySinh) AS 'Tuổi', kh.DienThoai AS 'Điện thoại', kh.Diachi AS 'Địa chỉ',
                    //                                    SoSBH AS 'Số SBH', xdb.SoKhung AS 'Số Khung', xdb.SoMay AS 'Số Máy', xdb.Mauxe AS 'Màu xe', CONVERT(nchar(10), xdb.NgayBan, 103) AS 'Ngày Mua', tn.smstype AS 'Loại tin',
                    //                                    CONVERT(nchar(10), tn.timeschedule, 103) AS 'Ngày gửi', CONVERT(nchar(8), tn.timeschedule, 108) AS 'Giờ gửi' FROM KhachHang kh
                    //                                    LEFT JOIN  TinNhan tn  ON kh.IdKhachHang=tn.IdKhachHang
                    //                                    LEFT JOIN XeDaBan xdb ON xdb.IdKhachHang=tn.IdKhachHang
                    //                                    WHERE tnlt.IdCongTy=" + Class.CompanyInfo.idcongty + " AND tnlt.IdKhachHang in (" + arrCusId + ") AND tnlt.smsid in (" + arrSmsId + ")";

                    //            SqlCommand cmd = new SqlCommand(commandText, cnn);

                    //            SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    //            adap.Fill(tb);
                    //        }
                    //        else
                    //        {
                    //            string commandText = @"SELECT DISTINCT kh.TenKH AS 'Tên khách hàng', kh.GioiTinh AS 'Giới tính', CONVERT(nchar(10), kh.NgaySinh, 103) AS 'Ngày sinh',
                    //                                    DATEPART(YY, GETDATE()) - YEAR(kh.NgaySinh) AS 'Tuổi', kh.DienThoai AS 'Điện thoại', kh.Diachi AS 'Địa chỉ', kh.SoSBH AS 'Số SBH',
                    //                                    'Số khung' = CASE WHEN ls.Sokhung IS NOT NULL AND xdb.SoKhung IS NULL THEN ls.Sokhung WHEN xdb.SoKhung IS NOT NULL AND ls.Sokhung IS NULL THEN xdb.SoKhung
                    //                                    WHEN xdb.SoKhung IS NOT NULL AND ls.Sokhung IS NOT NULL THEN xdb.SoKhung WHEN xdb.SoKhung IS NULL AND ls.Sokhung IS NULL THEN '' END,
                    //                                    'Số máy' = CASE WHEN ls.SoMay IS NOT NULL AND xdb.SoMay IS NULL THEN ls.SoMay WHEN xdb.SoMay IS NOT NULL AND ls.SoMay IS NULL THEN xdb.SoMay
                    //                                    WHEN xdb.SoMay IS NOT NULL AND ls.SoMay IS NOT NULL THEN xdb.SoMay WHEN xdb.SoMay IS NULL AND ls.SoMay IS NULL THEN '' END,
                    //                                    'Màu xe' = CASE WHEN xdb.Mauxe IS NOT NULL THEN xdb.Mauxe ELSE '' END, 'Ngày mua' = CASE WHEN xdb.NgayBan IS NOT NULL AND kh.NgayMua IS NULL THEN CONVERT(nchar(10),xdb.NgayBan,103)
                    //                                    WHEN xdb.NgayBan IS NULL AND kh.NgayMua IS NOT NULL THEN CONVERT(nchar(10),kh.NgayMua,103) WHEN xdb.NgayBan IS NOT NULL AND kh.NgayMua IS NOT NULL THEN CONVERT(nchar(10),xdb.NgayBan,103)
                    //                                    WHEN xdb.NgayBan IS NULL AND kh.NgayMua IS NULL THEN '' END, tn.smstype AS 'Loại tin',
                    //                                    CONVERT(nchar(10), tn.timeschedule, 103) AS 'Ngày gửi', CONVERT(nchar(8), tn.timeschedule, 108) AS 'Giờ gửi' FROM KhachHang kh
                    //                                    LEFT JOIN LichSuBaoDuongXe ls ON ls.IdKhachHang=kh.IdKhachHang
                    //                                    LEFT JOIN XeDaBan xdb ON xdb.IdKhachHang=kh.IdKhachHang
                    //                                    LEFT JOIN TinNhan tn ON tn.IdKhachHang=kh.IdKhachHang
                    //                                    WHERE tnlt.IdCongTy=" + Class.CompanyInfo.idcongty + " AND tnlt.IdKhachHang in (" + arrCusId + ") AND tnlt.smsid in (" + arrSmsId + ")";

                    //            SqlCommand cmd = new SqlCommand(commandText, cnn);

                    //            SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    //            adap.Fill(tb);
                    //        }
                    //    }
                    //}

                    if (rDaNhan.Checked)
                        smsType = "DaNhan";
                    if (rTuyBien.Checked)
                        smsType = "TuyBien";
                    if (rDangNhan.Checked)
                        smsType = "DangNhan";

                    SqlCommand cmd = new SqlCommand("pro_export_cus_info_from_sms", cnn);
                    cmd.Parameters.Clear();
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@chuoi", chuoi);
                    cmd.Parameters.AddWithValue("@smsType", smsType);
                    cmd.Parameters.AddWithValue("@idCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@tableInput", tableIdKhachHangIdSms);

                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    adap.Fill(tb);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Error: " + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                dataGridViewKhachHang.DataSource = null;
                dataGridViewKhachHang.DataSource = tb;

                try
                {
                    //fix collumn width
                    dataGridViewKhachHang.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[0].Width = 150;
                    dataGridViewKhachHang.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[1].Width = 50;
                    dataGridViewKhachHang.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[2].Width = 70;
                    dataGridViewKhachHang.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[3].Width = 30;
                    dataGridViewKhachHang.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[4].Width = 100;
                    dataGridViewKhachHang.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[5].Width = 120;
                    dataGridViewKhachHang.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[6].Width = 70;
                    dataGridViewKhachHang.Columns[7].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[7].Width = 130;
                    dataGridViewKhachHang.Columns[8].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[8].Width = 100;
                    dataGridViewKhachHang.Columns[9].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[9].Width = 50;
                    dataGridViewKhachHang.Columns[10].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[10].Width = 70;
                    dataGridViewKhachHang.Columns[11].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[11].Width = 70;
                    dataGridViewKhachHang.Columns[12].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                    dataGridViewKhachHang.Columns[12].Width = 50;
                }
                catch { }

                cnn.Close();
            }
        }

        private void grvtn_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit;
            if (e.Button == MouseButtons.Right)
            {
                hit = grvtn.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    toolStripMenuItemUpdateKH.Enabled = true;
                    if (!((DataGridViewRow)(grvtn.Rows[hit.RowIndex])).Selected)
                    {
                        grvtn.ClearSelection();
                        ((DataGridViewRow)(grvtn.Rows[hit.RowIndex])).Selected = true;
                        rownumber = hit.RowIndex;
                    }
                    if (((DataGridViewRow)(grvtn.Rows[hit.RowIndex])).Selected)
                    {
                        rownumber = hit.RowIndex;
                    }
                }
                else
                    toolStripMenuItemUpdateKH.Enabled = false;
            }
        }

        private void toolStripMenuItemUpdateKH_Click(object sender, EventArgs e)
        {
            try
            {
                using(SqlConnection cnn = Class.datatabase.getConnection())
                {
                    cnn.Open();

                    SqlCommand cmd = new SqlCommand("SELECT HoKH, TenKH, CONVERT(nchar(10), NgaySinh, 103) AS NgaySinh, DienThoai FROM KhachHang WHERE IdKhachHang=@IdKhachHang", cnn);
                    cmd.Parameters.AddWithValue("@IdKhachHang", grvtn.Rows[rownumber].Cells[2].Value);

                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataTable da = new DataTable();

                    adap.Fill(da);

                    if(da.Rows.Count > 0)
                    {
                        frmUpdateKhachHang frm = new frmUpdateKhachHang();

                    lap:
                        frm.txtHoKhachHang.Text = da.Rows[0][0].ToString();
                        frm.txtTenKhachHang.Text = da.Rows[0][1].ToString();
                        frm.txtNgaySinh.Text = da.Rows[0][2].ToString();
                        frm.txtSoDienThoai.Text = da.Rows[0][3].ToString();

                        if (frm.ShowDialog() == DialogResult.OK)
                        {
                            if(String.IsNullOrEmpty(frm.txtSoDienThoai.Text))
                            {
                                MessageBox.Show("Số điện thoại khách hàng không được để trống!", "Thông báo");

                                goto lap;
                            }
                            else
                            {
                                SqlCommand cmd1 = new SqlCommand("UPDATE KhachHang SET DienThoai=@DienThoai WHERE IdKhachHang=@IdKhachHang", cnn);
                                cmd1.Parameters.AddWithValue("@DienThoai", frm.txtSoDienThoai.Text);
                                cmd1.Parameters.AddWithValue("@IdKhachHang", grvtn.Rows[rownumber].Cells[2].Value);

                                cmd1.ExecuteNonQuery();

                                MessageBox.Show("Cập nhật số điện thoại khách hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không tồn tại thông tin khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    cnn.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message, "Thông báo"); }
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
                cl1.Value2 = "Tên Khách Hàng";
                cl1.ColumnWidth = 25.0;

                Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
                cl2.Value2 = "Giới tính";
                cl2.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
                cl3.Value2 = "Ngày sinh";
                cl3.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
                cl4.Value2 = "Tuổi";
                cl4.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
                cl5.Value2 = "Điện thoại";
                cl5.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
                cl6.Value2 = "Địa chỉ";
                cl6.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
                cl7.Value2 = "Số SBH";
                cl7.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
                cl8.Value2 = "Số Khung";
                cl8.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
                cl9.Value2 = "Số Máy";
                cl9.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl10 = oSheet.get_Range("J3", "J3");
                cl10.Value2 = "Màu xe";
                cl10.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl11 = oSheet.get_Range("K3", "K3");
                cl11.Value2 = "Ngày Mua";
                cl11.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl12 = oSheet.get_Range("L3", "L3");
                cl12.Value2 = "Loại tin";
                cl12.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl13 = oSheet.get_Range("M3", "M3");
                cl13.Value2 = "Ngày gửi";
                cl13.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl14 = oSheet.get_Range("N3", "N3");
                cl14.Value2 = "Giờ gửi";
                cl14.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl15 = oSheet.get_Range("O3", "O3");
                cl15.Value2 = "Trạng thái gửi";
                cl15.ColumnWidth = 25.0;

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

        public void ExportDTToExcel(System.Data.DataTable dt)  
        {  
            Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();  
            app.Visible = false;

            Microsoft.Office.Interop.Excel.Workbook wb = app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);
            Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)wb.ActiveSheet;
 
            // Headers.  
            for (int i = 0; i < dt.Columns.Count; i++)  
            {  
                ws.Cells[1, i + 1] = dt.Columns[i].ColumnName;  
            }  
 
            // Content.  
            for (int i = 0; i < dt.Rows.Count; i++)  
            {  
                for (int j = 0; j < dt.Columns.Count; j++)  
                {  
                    ws.Cells[i + 2, j + 1] = dt.Rows[i][j].ToString();  
                }  
            }  
 
            // Lots of options here. See the documentation.  
            wb.SaveAs("d:\\tsst.xlsx");

            wb.Close();
            app.Quit();  
        } 

        private void buttonX4_Click(object sender, EventArgs e)
        {
            #region
            if (dttn == null) { MessageBox.Show("Chưa có dữ liệu tin nhắn được tìm thấy."); return; }
            int rowc = dttn.Rows.Count;
            int columc = dttn.Columns.Count;
            Excel.Application oXL;
            Excel._Workbook oWB;
            Excel._Worksheet oSheet;
            Excel.Range oRng;

            try
            {
                oXL = new Excel.Application();
                oXL.Visible = true;
                oWB = (Excel._Workbook)(oXL.Workbooks.Add(Missing.Value));
                oSheet = (Excel._Worksheet)oWB.ActiveSheet;
                for (int i = 1; i <= columc; i++)
                {
                    oSheet.Cells[1, i] = dttn.Columns[i - 1].ColumnName;
                }
                oSheet.get_Range("A1", "J1").Font.Bold = true;
                oSheet.get_Range("A1", "J1").VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
                string[,] saNames = new string[rowc, columc];
                for (int i = 0; i < rowc; i++)
                {
                    for (int j = 0; j < columc; j++)
                    {
                        saNames[i, j] = dttn.Rows[i][j].ToString();
                    }
                }

                // DoNT UPDATE 2018/05/07 Start
                //oSheet.get_Range("A2", "J" + rowc).Value2 = saNames;
                int rowc1 = rowc + 1;
                oSheet.get_Range("A2", "J" + rowc1).Value2 = saNames;
                // DoNT UPDATE 2018/05/07 End

                oRng = oSheet.get_Range("A1", "J1");
                oRng.EntireColumn.AutoFit();
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
            #endregion
        }
    }
}