using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;
using System.Data.SqlClient;
using AutoCareV2._0.Class;

namespace AutoCareV2._0
{
    public partial class FrmNhapMaThe : Form
    {
        private string cn = Class.datatabase.connect;
        private SqlConnection con;
        public string idKhachHang;
        public string sdt;
        public string bienSo;
        public string soKhung;
        public string soMay;
        public string tenXe;

        public FrmNhapMaThe()
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
            catch
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_TaoThe_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtMaThe.Text))
                {
                    connect();
                    string sql = string.Format(@"insert into CauHinhThe_Xe(idtag,idkhachhang,dienthoai,bienso,sokhung,somay,ngaytao,idcongty,tenxe) values('{0}',{1},'{2}','{3}','{4}','{5}','{6}',{7},N'{8}')", txtMaThe.Text, idKhachHang, sdt, bienSo, soKhung, soMay, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"), CompanyInfo.idcongty, tenXe);
                    SqlCommand cmd = new SqlCommand(sql, con);
                    int i = cmd.ExecuteNonQuery();
                    con.Close();

                    if (i > 0)
                    {
                        if (MessageBox.Show(@"Tạo thẻ thành công!", @"Kết quả tạo thẻ", MessageBoxButtons.OK) == DialogResult.OK)
                        {con.Close();
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                if (MessageBox.Show(@"Lỗi: " + ex, @"Kết quả tạo thẻ", MessageBoxButtons.OK) == DialogResult.OK)
                {
                    con.Close();
                    this.Close();
                }
            }
        }
    }
}
