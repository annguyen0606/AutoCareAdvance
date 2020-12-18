using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmQuanLyKhoPhuTung : Form
    {
        private string maKho = "";

        public frmQuanLyKhoPhuTung()
        {
            InitializeComponent();
        }

        private void frmKhoPhuTung_Load(object sender, EventArgs e)
        {
            Load_ddlTenCuaHang();
            KHOHANG();
        }

        private void KHOHANG()
        {
            string sql = "select * from KhoHang WHERE IdCongTy=" + Convert.ToInt32(Class.CompanyInfo.idcongty);
            SqlCommand cmd = new SqlCommand(sql);
            DataTable dt = Class.datatabase.getData(cmd);
            dgvKhoPhuTung.DataSource = dt;
        }

        private void Load_ddlTenCuaHang()
        {
            string tencuahang = "SELECT IdCuaHang, TenCuaHang FROM CuaHang WHERE IdCongTy=" + Convert.ToInt32(Class.CompanyInfo.idcongty);
            SqlCommand cmd = new SqlCommand(tencuahang);
            DataTable dt = Class.datatabase.getData(cmd);
            cboCuaHang.DataSource = dt;
            cboCuaHang.DisplayMember = "TenCuaHang";
            cboCuaHang.ValueMember = "IdCuaHang";
        }

        private void dgvKhoPhuTung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    cboCuaHang.SelectedValue = dgvKhoPhuTung.Rows[e.RowIndex].Cells["MaCuaHang"].Value.ToString();
                    txtTenKho.Text = dgvKhoPhuTung.Rows[e.RowIndex].Cells["TenKho"].Value.ToString();
                    txtDienGiai.Text = dgvKhoPhuTung.Rows[e.RowIndex].Cells["DienGiai"].Value.ToString();
                    maKho = dgvKhoPhuTung.Rows[e.RowIndex].Cells["MaKho1"].Value.ToString();
                }
            }
            catch { }
        }

        private void frmQuanLyKhoPhuTung_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class.Closing.tontai_KhoPhuTung = false;
        }

        void ResetControls()
        {
            txtTenKho.Text = "";
            txtDienGiai.Text = "";
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (cboCuaHang.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Cửa hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCuaHang.Focus();
                
                return;
            }

            if (String.IsNullOrEmpty(txtTenKho.Text))
            {
                MessageBox.Show("Tên kho không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKho.Focus();
                
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO KhoHang (IdCongTy, IdCuaHang, TenKho, DienGiai) VALUES (@IdCongTy, @IdCuaHang, @TenKho, @DienGiai)";
                cmd.Parameters.AddWithValue("@IdCongTy", Convert.ToInt32(Class.CompanyInfo.idcongty));
                cmd.Parameters.AddWithValue("@IdCuaHang", cboCuaHang.SelectedValue);
                cmd.Parameters.AddWithValue("@TenKho", txtTenKho.Text);
                cmd.Parameters.AddWithValue("@DienGiai", txtDienGiai.Text);
                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Thêm kho mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetControls();
                    KHOHANG();
                }
                else
                {
                    MessageBox.Show("Thêm kho mới thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(maKho))
            {
                MessageBox.Show("Bạn chưa chọn Kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (cboCuaHang.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Cửa hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboCuaHang.Focus();

                return;
            }

            if (txtTenKho.Text == "")
            {
                MessageBox.Show("Tên kho không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenKho.Focus();

                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE KhoHang SET IdCongTy=@IdCongTy, IdCuaHang=@IdCuaHang, TenKho=@TenKho, DienGiai=@DienGiai WHERE IdKho=@IdKho";
                cmd.Parameters.AddWithValue("@IdKho", maKho);
                cmd.Parameters.AddWithValue("@IdCongTy", Convert.ToInt32(Class.CompanyInfo.idcongty));
                cmd.Parameters.AddWithValue("@IdCuaHang", cboCuaHang.SelectedValue);
                cmd.Parameters.AddWithValue("@TenKho", txtTenKho.Text);
                cmd.Parameters.AddWithValue("@DienGiai", txtDienGiai.Text);

                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Cập nhật kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    ResetControls();
                    KHOHANG();
                }
                else
                {
                    MessageBox.Show("Cập nhật Kho thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(maKho))
            {
                MessageBox.Show("Bạn chưa chọn Kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            try
            {
                DialogResult chon = MessageBox.Show("Bạn có chắc chắn muốn xóa Kho hàng này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (chon == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "DELETE KhoHang WHERE IdKho=@IdKho AND IdCongTy=@IdCongTy";
                    cmd.Parameters.AddWithValue("@IdKho", maKho);
                    cmd.Parameters.AddWithValue("@IdCongTy", Convert.ToInt32(Class.CompanyInfo.idcongty));

                    if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    {
                        MessageBox.Show("Xóa kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ResetControls();
                        KHOHANG();
                    }
                    else
                    {
                        MessageBox.Show("Xóa Kho thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }
    }
}