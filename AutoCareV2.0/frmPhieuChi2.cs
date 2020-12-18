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
    public partial class frmPhieuChi2 : Form
    {
        public frmPhieuChi2()
        {
            InitializeComponent();
        }
        public string soHoaDon = "";
        public string idKey = "";
        public string nhaCungCap = "";
        public string idNhaCungCap = "";
        public decimal tongtien = 0;
        public decimal tienDaTra = 0;
        decimal tienNo = 0;
        private void frmPhieuChi2_Load(object sender, EventArgs e)
        {
            dateTimeInputNgayTra.Value = DateTime.Now;
            txtNhaCungCap.Text = nhaCungCap;
            txtTienDaTra.Text = string.Format("{0:0,0}", tienDaTra);
            txtTongTien.Text = string.Format("{0:0,0}", tongtien);
            tienNo = tongtien - tienDaTra;
            txtSoTienNo.Text = tienNo.ToString("0,0");
            txtSoHoaDon.Text = soHoaDon;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (dateTimeInputNgayTra.ValueObject == null)
            {
                MessageBox.Show("Thời gian không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            decimal tientra;
            if (!decimal.TryParse(txtSoTienTra.Text, out  tientra))
            {
                MessageBox.Show("Số tiền trả không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tientra > tienNo)
            {
                MessageBox.Show("Số tiền trả lớn hơn số tiền nợ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (tientra == 0)
            {
                MessageBox.Show("Số tiền trả phải lớn hơn: 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.datatabase.getConnection();
            cmd.CommandTimeout = 0;
            cmd.CommandText = "Update HoaDonNhapPhuTung set TienDaTra = @TienDaTra Where IdCongTy = @IdCongTy and IdKey = @idKey";
            cmd.Connection.Open();
            SqlTransaction tran = cmd.Connection.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                cmd.Parameters.AddWithValue("@TienDaTra", tienDaTra + tientra);
                cmd.Parameters.AddWithValue("@IdKey", idKey);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.ExecuteNonQuery();
                cmd.CommandText = "insert into PhieuChi(IDLoaiPhieuChi,SoTienChi,NgayHachToan,IDCongTy,NguoiNhan,IdNhaCungCap,IDNhanVien,NoiDung,DaNhanHang,IdCuaHang,SoHoaDon) " +
                "Values(@IDLoaiPhieuChi,@SoTienChi,@NgayChi,@IDCongTy,@NguoiNhan,@IdNhaCungCap,@IdNhanVien,@NoiDung,@DaNhanHang,@IdCuaHang,@SoHoaDon)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdLoaiPhieuChi", 2);
                cmd.Parameters.AddWithValue("@SoTienChi", tientra);
                cmd.Parameters.AddWithValue("@NgayChi", dateTimeInputNgayTra.Value);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@NguoiNhan", txtNguoiNhan.Text);
                cmd.Parameters.AddWithValue("@IdNhaCungCap", idNhaCungCap);
                cmd.Parameters.AddWithValue("@IdNhanVien", Class.EmployeeInfo.idnhanvien);
                cmd.Parameters.AddWithValue("@NoiDung", txtLyDoChi.Text);
                cmd.Parameters.AddWithValue("@DaNhanHang", "True");
                cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                cmd.Parameters.AddWithValue("@SoHoaDon", soHoaDon);
                cmd.ExecuteNonQuery();
                tran.Commit();
                cmd.Connection.Close();
                tienDaTra += tientra;
                txtTienDaTra.Text = tienDaTra.ToString("0,0");
                tienNo = tongtien - tienDaTra;
                txtSoTienNo.Text = tienNo.ToString("0,0");
                MessageBox.Show("Thêm phiếu chi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                cmd.Connection.Close();
                MessageBox.Show("Lỗi : " + ex.Message + " Vui lòng kiểm tra lại dữ liệu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void frmPhieuChi2_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void checkBoxX1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX1.Checked)
                txtSoTienTra.Text = txtSoTienNo.Text;
            else
                txtSoTienTra.Text = "0";
        }

        private void txtSoTienTra_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal tientra;
                tientra = Convert.ToDecimal(txtSoTienTra.Text);
                txtSoTienTra.Text = tientra.ToString("0,0");
                txtSoTienTra.SelectionStart = txtSoTienTra.Text.Length;
            }
            catch { }
        }
    }
}
