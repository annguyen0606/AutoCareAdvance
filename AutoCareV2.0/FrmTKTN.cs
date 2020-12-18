using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
//using Microsoft.Office.Core;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;
namespace AutoCareV2._0
{
    public partial class FrmTKTN : Form
    {
        string cn = Class.datatabase.connect;
        private DataTable dttn = new DataTable("TinNhan");
        private DataTable dtsms = new DataTable("SMSConfig");
        private DataTable dtthieu = new DataTable("ThuongHieu");
        private SqlDataAdapter da = new SqlDataAdapter();
        private SqlConnection con;
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
        //xong
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
        public FrmTKTN()
        {
            InitializeComponent();
        }
        //xong
        private void FrmTKTN_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            dttungay.Value = DateTime.Now;
            dtdenngay.Value = DateTime.Now;

            connect();
            getdata();
        }
        //xong
        private void disconnect()
        {
            con.Close();//dong ket noi
            con.Dispose();//giai phong tai nguyen
            con = null;//huy doi tuong
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

            lblTotalSms.Text = "Tổng số tin nhắn: " + total.ToString();

            if (dttn.Rows.Count > 0)
            {
                grvtn.DataSource = dttn;
            }
            else
            {
                MessageBox.Show("Không tìm thấy " + status + " từ " + dttungay.Value.ToString("dd/MM/yyyy") + " - " + dtdenngay.Value.ToString("dd/MM/yyyy"));
            }
        }

        private void cboloaitn_SelectedIndexChanged(object sender, EventArgs e)
        {

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
                oSheet.get_Range("A2", "J" + rowc.ToString()).Value2 = saNames;
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
    }
}
