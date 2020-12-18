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
    public partial class frmQuanLyNhaCungCap : Form
    {
        private string id;

        public frmQuanLyNhaCungCap()
        {
            InitializeComponent();
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Bạn chưa chọn nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtTenNhaCungCap.Text.Trim()=="")
                {
                    MessageBox.Show("Tên nhà cung cấp không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (String.IsNullOrEmpty(txtTenNhaCungCap.Text))
                {
                    MessageBox.Show("Bạn chưa nhập Tên nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenNhaCungCap.Focus();
                    ResetControls();
                    return;
                }

                SqlCommand cmd = new SqlCommand("UPDate NhaCungCap Set TenNhaCungCap = @TenNhaCungCap, DiaChi = @DiaChi, NguoiDaiDien =@NguoiDaiDien, Email =@Email, MaSoThue = @MaSoThue, SoDienThoai =@SoDienThoai Where IdCongTy =@IdCongTy And IdNhaCungCap =@IdNhaCungCap");
                cmd.Parameters.AddWithValue("@TenNhaCungCap", txtTenNhaCungCap.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@NguoiDaiDien", txtNguoiDaiDien.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@MaSoThue", txtMaSoThue.Text);
                cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IdNhaCungCap", id);

                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Cập nhật thông tin nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhaCungCap();
                    return;
                }
                else
                {
                    MessageBox.Show("Cập nhật không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi : " + ex.Message); }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                txtTenNhaCungCap.Text = Convert.ToString(dataGridViewX1.Rows[e.RowIndex].Cells["TenNhaCungCap"].Value);
                txtDiaChi.Text = Convert.ToString(dataGridViewX1.Rows[e.RowIndex].Cells["DiaChi"].Value);
                id = Convert.ToString(dataGridViewX1.Rows[e.RowIndex].Cells["IDNCC"].Value);
                txtEmail.Text = Convert.ToString(dataGridViewX1.Rows[e.RowIndex].Cells["Email"].Value);
                txtMaSoThue.Text = Convert.ToString(dataGridViewX1.Rows[e.RowIndex].Cells["MaSoThue"].Value);
                txtNguoiDaiDien.Text = Convert.ToString(dataGridViewX1.Rows[e.RowIndex].Cells["NguoiDaiDien"].Value);
                txtSoDienThoai.Text = Convert.ToString(dataGridViewX1.Rows[e.RowIndex].Cells["SoDienThoai"].Value);
            }
        }

        private void frmQuanLyNhaCungCap_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            LoadNhaCungCap();
        }

        private DataTable dt = new DataTable();

        private void LoadNhaCungCap()
        {
            SqlCommand cmd = new SqlCommand("Select IdNhaCungCap, TenNhaCungCap,DiaChi,SoDienThoai,Email,MaSoThue,NguoiDaiDien From NhaCungCap Where IDCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dt = Class.datatabase.getData(cmd);
            dataGridViewX1.DataSource = dt;
        }

        void ResetControls()
        {
            txtDiaChi.Clear();
            txtEmail.Clear();
            txtMaSoThue.Clear();
            txtNguoiDaiDien.Clear();
            txtSoDienThoai.Clear();
            txtTenNhaCungCap.Clear();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTenNhaCungCap.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenNhaCungCap.Focus();

                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand("Insert Into NhaCungCap(TenNhaCungCap,IdCongTy,SoDienThoai,Email,MaSoThue,NguoiDaiDien,DiaChi) Values(@TenNhaCungCap,@IdCongTy,@SoDienThoai,@Email,@MaSoThue,@NguoiDaiDien,@DiaChi)");
                cmd.Parameters.AddWithValue("@TenNhaCungCap", txtTenNhaCungCap.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
                cmd.Parameters.AddWithValue("@NguoiDaiDien", txtNguoiDaiDien.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@MaSoThue", txtMaSoThue.Text);
                cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Thêm thông tin nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadNhaCungCap();
                    ResetControls();
                    return;
                }
                else
                {
                    MessageBox.Show("Thêm thông tin nhà cung cấp thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi : " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Bạn chưa chọn nhà cung cấp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                DialogResult chon = MessageBox.Show("Bạn có chắc chắn muốn xóa thông tin nhà cung cấp này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (chon == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("DELETE NhaCungCap WHERE IdNhaCungCap=@IdNhaCungCap AND IdCongTy=@IdCongTy");
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdNhaCungCap", id);

                    if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    {
                        MessageBox.Show("Xóa thông tin nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadNhaCungCap();
                        ResetControls();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Xóa nhà cung cấp thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi : " + ex.Message); }
        }
    }
}