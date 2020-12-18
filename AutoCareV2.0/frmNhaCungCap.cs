using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using AutoCareV2._0.Class;
namespace AutoCareV2._0
{
    public partial class frmNhaCungCap : Form
    {
        public frmNhaCungCap()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtTenNhaCungCap.Text) && String.IsNullOrEmpty(CompanyInfo.idcongty))
                {
                    MessageBox.Show("Tên nhà cung cấp không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                KhachHang.idnhacungcap = "";
                string sql = "Insert Into NhaCungCap(TenNhaCungCap,IdCongTy,SoDienThoai,Fax,Email,MaSoThue,NguoiDaiDien,DiaChi) Values(@TenNhaCungCap,@IdCongTy,@SoDienThoai,@Fax,@Email,@MaSoThue,@NguoiDaiDien,@DiaChi) select @@Identity";
                SqlCommand cmd = new SqlCommand(sql,datatabase.getConnection());
                cmd.Connection.Open();
                cmd.Parameters.AddWithValue("@TenNhaCungCap", txtTenNhaCungCap.Text);
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@SoDienThoai", txtSoDienThoai.Text);
                cmd.Parameters.AddWithValue("@Fax", txtFax.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@MaSoThue", txtMaSoThue.Text);
                cmd.Parameters.AddWithValue("@NguoiDaiDien", txtNguoiDaiDien.Text);
                cmd.Parameters.AddWithValue("@DiaChi", txtDiaChi.Text);
               KhachHang.idnhacungcap = cmd.ExecuteScalar().ToString();
                    MessageBox.Show("Thêm nhà cung cấp thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void frmNhaCungCap_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}
