using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoCareV2._0.Class;
using System.Windows.Threading;

namespace AutoCareV2._0
{
    public partial class FrmLichSuTichDiem : Form
    {
        public  string idTichDiem;
        public long? idKhachHang;
        public string tenKH;
        public string sdt;

        private string cn = Class.datatabase.connect;
        private SqlDataAdapter da = new SqlDataAdapter();
        private SqlCommand _cmd = new SqlCommand();
        private SqlConnection con;

        public FrmLichSuTichDiem()
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

        private void FrmCauHinhTichDiem_Load(object sender, EventArgs e)
        {
            if (idKhachHang != null)
            {
                panelLichSu.Visible = false;
                panelThemDiem.Visible = true;
                txtTenKH.Text = tenKH;
                txtSDT.Text = sdt;
            }
            else
            {
                panelLichSu.Visible = true;
                panelThemDiem.Visible = false;
                cb_Type.Items.Add("Tất cả");
                cb_Type.Items.Add("Quy đổi");
                cb_Type.Items.Add("Nạp tiền");
                cb_Type.Text = "Tất cả";
            }
                
            dataGridView1.DataSource = GetCauHinh();
        }

        private DataTable GetCauHinh()
        {
            if (idKhachHang != null)
            {
                connect();
                DataTable dt = new DataTable();
                string sql = string.Format("select kh.tenkh,kh.dienthoai,diem,loai,lstd.ngaytao from LichSuTichDiem lstd with(nolock) join tichdiem td with(nolock) on lstd.idtichdiem=td.idtichdiem join khachhang kh with(nolock) on kh.idkhachhang=td.idkhachhang where kh.dienthoai='{0}'", sdt);
                if (!string.IsNullOrEmpty(cb_Type.Text) && cb_Type.SelectedIndex != 0)
                {
                    sql += string.Format(" and loai = N'{0}'", cb_Type.Text);
                }
                sql += " order by lstd.id desc";
                da = new SqlDataAdapter(sql, con);
                dt.Clear();
                da.Fill(dt);
                return dt;
            }
            else if (!string.IsNullOrEmpty(idTichDiem))
            {
                connect();
                DataTable dt = new DataTable();
                string sql = string.Format("select diem,loai,ngaytao from LichSuTichDiem with(nolock) where idTichDiem = {0}", idTichDiem);
                if (!string.IsNullOrEmpty(cb_Type.Text) && cb_Type.SelectedIndex != 0)
                {
                    sql += string.Format(" and loai = N'{0}'", cb_Type.Text);
                }
                sql += " order by id desc";
                da = new SqlDataAdapter(sql, con);
                dt.Clear();
                da.Fill(dt);
                return dt;
            }
            else
            {
                return null;
            }
        }

        private void cb_Type_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView1.DataSource = GetCauHinh();
        }

        private void btn_AddPoint_Click(object sender, EventArgs e)
        {
            connect();
            DataTable dt = new DataTable();
            string sql = string.Format(@"update tichdiem set tongdiem=tongdiem + {0},diemconlai=diemconlai + {1} where idkhachhang={2} and idtichdiem={3}", Convert.ToInt64(txtDiem.Text), Convert.ToInt64(txtDiem.Text), idKhachHang, idTichDiem);
            SqlCommand cmd = new SqlCommand(sql, con);
            int result = cmd.ExecuteNonQuery();
            if (result > 0)
            {
                sql = string.Format(@"insert into lichsutichdiem (idtichdiem,diem,loai,ngaytao) values ({0},{1},N'Nạp tiền','{2}')", idTichDiem, Convert.ToInt64(txtDiem.Text), DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                SqlCommand cmd1 = new SqlCommand(sql, con);
                cmd1.ExecuteNonQuery();

                MessageBox.Show("Thêm thành công");
                dataGridView1.DataSource = GetCauHinh();
                return;
            }
            else
            {
                MessageBox.Show("Thêm thất bại.\nVui lòng liên hệ với quản trị viên.");
                return;
            }
        }

        private void btnQuyDoi_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtQuyDoi.Text))
            {
                MessageBox.Show("Vui lòng nhập số tiền quy đổi ra điểm.");
                return;
            }
            if (string.IsNullOrEmpty(txtSoTien.Text))
            {
                MessageBox.Show("Vui lòng nhập số tiền.");
                return;
            }
            txtDiem.Text = Convert.ToInt64(Math.Round(Convert.ToDouble(txtSoTien.Text) / Convert.ToDouble(txtQuyDoi.Text))).ToString();
        }
    }
}