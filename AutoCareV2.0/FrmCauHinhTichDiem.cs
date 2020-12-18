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
    public partial class FrmCauHinhTichDiem : Form
    {
        private string cn = Class.datatabase.connect;
        private SqlDataAdapter da = new SqlDataAdapter();
        private SqlCommand _cmd = new SqlCommand();
        private SqlConnection con;

        public FrmCauHinhTichDiem()
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
            dataGridView1.DataSource = GetCauHinh();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSoTien.Text) && Convert.ToInt64(txtSoTien.Text) > 0)
            {

                try
                {
                    connect();
                    SqlCommand cmdupdate = new SqlCommand();
                    cmdupdate.Connection = con;
                    bool isUpdate = false;
                    string sql = string.Empty;
                    DataTable dt = new DataTable();
                    dt = GetCauHinh();

                    if (dt.Rows.Count > 0)
                    {
                        sql = "update CauHinhTichDiem set soTien=" + txtSoTien.Text + ",guiTin=" + (chk_GuiTin.Checked ? 1 : 0) + ",ngaySua='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE idCongTy=" + Class.CompanyInfo.idcongty;
                        isUpdate = true;
                    }
                    else
                    {
                        sql = "insert CauHinhTichDiem(idCongTy,soTien,soDiem,guiTin,ngayTao) values(" + Class.CompanyInfo.idcongty + "," + txtSoTien.Text + ",1," + (chk_GuiTin.Checked ? 1 : 0) + ",'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                    }

                    da = new SqlDataAdapter(sql, con);
                    dt.Clear();
                    da.Fill(dt);

                    if (isUpdate)
                        MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    else
                        MessageBox.Show("Thêm thành công!", "Thông báo");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally
                {
                    dataGridView1.DataSource = GetCauHinh();
                }
            }
            else
            {
                MessageBox.Show("Số tiền không được để trống!", "Thông báo");
                return;
            }
        }

        private void SoTien_TextChanged(object sender, EventArgs e)
        {
            float flag;

            if (!String.IsNullOrEmpty(txtSoTien.Text))
            {
                if (float.TryParse(txtSoTien.Text, out flag) == false)
                {
                    MessageBox.Show("Chiết khấu phải là kiểu số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    txtSoTien.Text = "0";
                    return;
                }
            }
        }

        private DataTable GetCauHinh()
        {
            connect();
            DataTable dt = new DataTable();
            string sql = "SELECT IdCongTy,soTien as 'Số Tiền', soDiem as 'Số điểm', guiTin as 'Gửi tin?', ngayTao as 'Ngày tạo' FROM CauHinhTichDiem WHERE idCongTy=" + Class.CompanyInfo.idcongty;
            da = new SqlDataAdapter(sql, con);
            dt.Clear();
            da.Fill(dt);

            return dt;
        }
    }
}