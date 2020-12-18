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
    public partial class FrmDoiPass : Form
    {
        string cn = Class.datatabase.connect;
        private SqlConnection con;
        public FrmDoiPass()
        {
            InitializeComponent();
        }
        private void connect()
        {
            try
            {
                con = new SqlConnection(cn);
                con.Open();
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi kết nối: kiểm tra lại kết nối đường truyền mạng");
            }
        }
        private void FrmDoiPass_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string passNew = Class.Checksum.GetMd5Hash(txt_PassCu.Text.Trim(), Class.CompanyInfo.secretkey);
            if (passNew == Class.EmployeeInfo.Pass)
            {
                if (txt_PassMoi.Text.Trim() == txt_PassMoi2.Text.Trim())
                {
                    try
                    {
                        connect();
                        SqlCommand cmd = new SqlCommand("update TaiKhoanDangNhap set pass=@pass where UserName=@UserName", con);
                        cmd.Parameters.AddWithValue("@pass", Class.Checksum.GetMd5Hash(txt_PassMoi.Text, Class.CompanyInfo.secretkey));
                        cmd.Parameters.AddWithValue("@UserName", Class.EmployeeInfo.UserName);
                        cmd.ExecuteNonQuery();

                        MessageBox.Show("Cập nhật mật khẩu thành công !");
                        this.Close();
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi " + ex.Message); }
                }
                else { MessageBox.Show("Mật khẩu mới phải giống nhau !"); }
            }
            else { MessageBox.Show("Mật khẩu cũ không đúng !"); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
