using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AutoCareV2._0
{
    public partial class FrmTNMungsinhnhatKH : Form
    {
        private DataTable dtkh = new DataTable("KhachHang");
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
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối " + ex.Message);
            }
        }
        public FrmTNMungsinhnhatKH()
        {
            InitializeComponent();
        }

        private void FrmTNMungsinhnhatKH_Load(object sender, EventArgs e)
        {
            connect();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandTimeout = 0;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"Select *, (select top 1  convert(bit,idkhachhang) from tinnhanluutru" +
                " tnlt where tnlt.idkhachhang=kh.idkhachhang and smstype='Sinh nhat') as 'Đã nhắn tin' " +
                "from KhachHang kh where DAY(ngaysinh)=DAY(getdate())AND MONTH(ngaysinh)=MONTH(getdate()) and idcongty=" + Class.CompanyInfo.idcongty;
            da.SelectCommand = cmd;
            da.Fill(dtkh);
            disconnect();
            grvsnkh.DataSource = dtkh;

        }
        private void disconnect()
        {
            con.Close();// dong ket noi
            con.Dispose();//giai phong tai nguyen
            con = null;// huy doi tuong
        }
        private void btnguitin_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand com = new SqlCommand();
           
                com.CommandText = "select top 1 sms from smsconfig where type='Sinh nhat' and idcongty="+ Class.CompanyInfo.idcongty;
                DataTable dt = new DataTable();
                dt = Class.datatabase.getData(com);
                string smsconfig = null;
                if(dt.Rows.Count>0)
                smsconfig = dt.Rows[0][0].ToString();
                
                if (smsconfig != null)
                {
                    int count = 0;
                    foreach (DataGridViewRow r in grvsnkh.SelectedRows)
                    {
                        string idkhachhang = r.Cells["MaKH"].Value.ToString();
                        string tenkhachang = r.Cells["HoKH"].Value.ToString() + " " + r.Cells["TenKH"].Value.ToString();
                        smsconfig = smsconfig.Replace("[TenKH]", tenkhachang);
                        string phone = r.Cells["DienThoai"].Value.ToString();
                        com = new SqlCommand("Insert into tinnhan (sms,phone,smstype,idkhachhang,idcongty,sendername) values (N'" +
                            smsconfig + "','" + phone + "','Sinh nhat'," + idkhachhang + "," + Class.CompanyInfo.idcongty + ",'" + Class.CompanyInfo.sendername + "')", con);
                        com.ExecuteNonQuery();
                        count++;
                    }
                    MessageBox.Show("Gửi thành công " + count.ToString() + " Tin nhắn");
                }
                else
                {
                    MessageBox.Show("Gửi không thành công do chưa cấu hình mẫu tin nhắn sinh nhật.");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand com = new SqlCommand();

                com.CommandText = "select top 1 sms from smsconfig where type='Sinh nhat' and idcongty=" + Class.CompanyInfo.idcongty;
                DataTable dt = new DataTable();
                dt = Class.datatabase.getData(com);
                string smsconfig = null;
                if (dt.Rows.Count > 0)
                    smsconfig = dt.Rows[0][0].ToString();

                if (smsconfig != null)
                {
                    int count = 0;
                    foreach (DataGridViewRow r in grvsnkh.SelectedRows)
                    {
                        string idkhachhang = r.Cells["MaKH"].Value.ToString();
                        string tenkhachang = r.Cells["HoKH"].Value.ToString() + " " + r.Cells["TenKH"].Value.ToString();
                        smsconfig = smsconfig.Replace("[TenKH]", tenkhachang);
                        string phone = r.Cells["DienThoai"].Value.ToString();
                        com = new SqlCommand("Insert into tinnhan (sms,phone,smstype,idkhachhang,idcongty,sendername) values (N'" +
                            smsconfig + "','" + phone + "','Sinh nhat'," + idkhachhang + "," + Class.CompanyInfo.idcongty + ",'" + Class.CompanyInfo.sendername + "')", con);
                        com.ExecuteNonQuery();
                        count++;
                    }
                    MessageBox.Show("Gửi thành công " + count.ToString() + " Tin nhắn");
                }
                else
                {
                    MessageBox.Show("Gửi không thành công do chưa cấu hình mẫu tin nhắn sinh nhật.");
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
    }
}
