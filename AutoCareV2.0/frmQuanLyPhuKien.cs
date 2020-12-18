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
    public partial class frmQuanLyPhuKien : Form
    {
        private SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
        private static DataTable dtPhuKien = new DataTable();
        private bool kq = false;
        private int _IdPhuKien;

        public frmQuanLyPhuKien()
        {
            InitializeComponent();
        }

        private void LoadPhuKien()
        {
            try
            {
                myCon.Open();
                string sql = "select * from PhuKien a where a.IdCongTy=" + Convert.ToInt32(Class.CompanyInfo.idcongty);
                SqlDataAdapter da = new SqlDataAdapter(sql, myCon);

                dtPhuKien.Clear();
                da.Fill(dtPhuKien);
                dsPhuKien.DataSource = dtPhuKien;

                dsPhuKien.Columns["IdPhukien"].Visible = false;

                myCon.Close();
            }
            catch { }
        }

        private void Reset()
        {
            txtDonViTinh.Text = "";
            txtTenPhuKien.Text = "";
            _IdPhuKien = 0;

            LoadPhuKien();
        }

        private void frmQuanLyPhuKien_Load(object sender, EventArgs e)
        {
            txtTenPhuKien.Focus();

            LoadPhuKien();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void dsPhuKien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _IdPhuKien = Convert.ToInt32(dsPhuKien.Rows[e.RowIndex].Cells[0].Value.ToString());
                txtTenPhuKien.Text = dsPhuKien.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtDonViTinh.Text = dsPhuKien.Rows[e.RowIndex].Cells[2].Value.ToString();
            }
            catch { }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_IdPhuKien == 0)
            {
                MessageBox.Show("Bạn chưa chọn Phụ kiện!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            try
            {
                DialogResult chon = MessageBox.Show("Bạn có chắc chắn muốn xóa phụ kiện này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (chon == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "DELETE FROM PhuKien WHERE IdPhuKien=@IdPhuKien AND IdCongTy=@IdCongTy";

                    cmd.Parameters.AddWithValue("@IdPhuKien", _IdPhuKien);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    {
                        Reset();
                        MessageBox.Show("Xóa phụ kiện thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtTenPhuKien.Text))
            {
                MessageBox.Show("Bạn chưa nhập Tên phụ kiện!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPhuKien.Focus();

                return;
            }

            if (String.IsNullOrEmpty(txtDonViTinh.Text))
            {
                MessageBox.Show("Bạn chưa nhập Đơn vị tính!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonViTinh.Focus();

                return;
            }

            try
            {
                myCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "INSERT INTO PhuKien(TenPhuKien, DVT, IdCongTy) VALUES(@TenPhuKien, @DVT, @IdCongTy)";

                cmd.Parameters.AddWithValue("@TenPhuKien", txtTenPhuKien.Text);
                cmd.Parameters.AddWithValue("@DVT", txtDonViTinh.Text);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Thêm phụ kiện thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    myCon.Close();
                    Reset();
                    return;
                }
                else
                {
                    MessageBox.Show("Thêm phụ kiện thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Thông Báo");
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (_IdPhuKien == 0)
            {
                MessageBox.Show("Bạn chưa chọn Phụ kiện!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (String.IsNullOrEmpty(txtTenPhuKien.Text))
            {
                MessageBox.Show("Bạn chưa nhập Tên phụ kiện!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenPhuKien.Focus();

                return;
            }

            if (String.IsNullOrEmpty(txtDonViTinh.Text))
            {
                MessageBox.Show("Bạn chưa nhập Đơn vị tính!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDonViTinh.Focus();

                return;
            }

            try
            {
                myCon.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "UPDATE PhuKien SET TenPhuKien=@TenPhuKien, DVT=@DVT WHERE IdPhuKien=@IdPhuKien AND IdCongTy=@IdCongTy";

                cmd.Parameters.AddWithValue("@IdPhuKien", _IdPhuKien);
                cmd.Parameters.AddWithValue("@TenPhuKien", txtTenPhuKien.Text);
                cmd.Parameters.AddWithValue("@DVT", txtDonViTinh.Text);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Sửa phụ kiện thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    myCon.Close();
                    Reset();
                    return;
                }
                else
                {
                    MessageBox.Show("Sửa phụ kiện thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi" + ex.Message, "Thông Báo");
            }
        }

        private void txtTenPhuKien_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtDonViTinh.Focus();
        }

        private void txtDonViTinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnThemMoi.Focus();
        }
    }
}