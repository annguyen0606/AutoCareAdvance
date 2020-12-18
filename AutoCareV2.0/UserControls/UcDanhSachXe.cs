using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls
{
    public partial class UcDanhSachXe : UserControl
    {
        private DataTable dtMaker = new DataTable();
        private SqlCommand cmd;
        private DataTable dtMotor = new DataTable();
        private decimal tien;
        private string IdXe = "";

        public UcDanhSachXe()
        {
            InitializeComponent();
        }

        private void LoadListMotor()
        {
            cmd = new SqlCommand("Select IDXe, TenXe, HangSanXuat, DVT, DoiXe, DonGia From XeMay Where IDCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtMotor = Class.datatabase.getData(cmd);
        }

        private void LoadMaker()
        {
            cmd = new SqlCommand("Select * From NhaSanXuat");
            dtMaker = Class.datatabase.getData(cmd);
        }

        private void ResetData()
        {
            txt_DoiXe.Clear();
            txtDonGia.Clear();
            txtMaXe.Clear();
            txtTenXe.Clear();
            cboDVT.SelectedIndex = 0;
            cboNhaSanXuat.SelectedIndex = 0;
        }

        private void UcDanhSachXe_Load(object sender, EventArgs e)
        {
            LoadListMotor();
            LoadMaker();
            HangSanXuat.DataSource = dtMaker;
            HangSanXuat.ValueMember = "IdNhaSanXuat";
            HangSanXuat.DisplayMember = "TenNhaSanXuat";
            dgvDanhSachXe.DataSource = dtMotor;
            cboNhaSanXuat.DataSource = dtMaker;
            cboNhaSanXuat.ValueMember = "IdNhaSanXuat";
            cboNhaSanXuat.DisplayMember = "TenNhaSanXuat";

            txtMaXe.Focus();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtMaXe.Text))
                {
                    MessageBox.Show("Mã xe không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaXe.Focus();

                    return;
                }

                if (String.IsNullOrEmpty(txtTenXe.Text))
                {
                    MessageBox.Show("Tên xe không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenXe.Focus();

                    return;
                }

                if (String.IsNullOrEmpty(txtDonGia.Text))
                {
                    MessageBox.Show("Đơn giá phải là số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenXe.Focus();

                    return;
                }

                cmd = new SqlCommand("if not exists( select 1 from xemay where IDXe = @IdXe and IdCongTy = @IdCongTy) begin insert Xemay(IDXe,TenXe,HangSanXuat,DVT,DoiXe, DonGia, IDCongTy) Values(@IdXe,@TenXe,@HangSanXuat,@DVT, @DoiXe, @DonGia,@IdCongTy) end");
                cmd.Parameters.AddWithValue("@IdXe", txtMaXe.Text);
                cmd.Parameters.AddWithValue("@TenXe", txtTenXe.Text);
                cmd.Parameters.AddWithValue("@HangSanXuat", cboNhaSanXuat.SelectedValue);
                cmd.Parameters.AddWithValue("@DVT", cboDVT.Text);
                cmd.Parameters.AddWithValue("@DoiXe", txt_DoiXe.Text);
                if (txtDonGia.Text != "")
                {
                    cmd.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(txtDonGia.Text));
                }
                else
                {
                    cmd.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(0));
                }
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Thêm thông tin xe thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dtMotor.Rows.Add(txtMaXe.Text, txtTenXe.Text, cboNhaSanXuat.SelectedValue, cboDVT.Text);
                    ResetData();
                    LoadListMotor();
                    dgvDanhSachXe.DataSource = dtMotor;
                }
                else
                {
                    MessageBox.Show("Mã xe đã tồn tại. \nNhập mã xe khác.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaXe.SelectAll();
                    txtMaXe.Focus();
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (IdXe != "")
            {
                try
                {
                    if (txtMaXe.Text != IdXe)
                    {
                        MessageBox.Show("Mã xe đã bị thay đổi. \nCập nhật không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaXe.SelectAll();
                        txtMaXe.Focus();

                        return;
                    }

                    if (String.IsNullOrEmpty(txtTenXe.Text))
                    {
                        MessageBox.Show("Tên xe không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenXe.Focus();

                        return;
                    }

                    if (String.IsNullOrEmpty(txtDonGia.Text))
                    {
                        MessageBox.Show("Đơn giá xe không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTenXe.Focus();

                        return;
                    }

                    cmd = new SqlCommand("update XeMay Set TenXe = @TenXe, HangSanXuat = @HangSanXuat, DVT = @DVT, DoiXe=@DoiXe, DonGia=@DonGia Where IdXe = @IdXe and idCongTy = @IdCongTy");
                    cmd.Parameters.AddWithValue("@IdXe", txtMaXe.Text);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@DVT", cboDVT.Text);
                    cmd.Parameters.AddWithValue("@HangSanXuat", cboNhaSanXuat.SelectedValue);
                    cmd.Parameters.AddWithValue("@TenXe", txtTenXe.Text);
                    cmd.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(txtDonGia.Text));
                    cmd.Parameters.AddWithValue("@DoiXe", txt_DoiXe.Text);

                    if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin xe thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ResetData();
                        LoadListMotor();
                        dgvDanhSachXe.DataSource = dtMotor;

                        try
                        {
                            DataRow[] r = dtMotor.Select("IdXe = '" + txtMaXe.Text + "'");
                            r[0]["TenXe"] = txtTenXe.Text;
                            r[0]["DVT"] = cboDVT.Text;
                            r[0]["HangSanXuat"] = cboNhaSanXuat.SelectedValue;
                        }
                        catch { LoadListMotor(); }
                    }
                    else
                    {
                        MessageBox.Show("Cập nhật không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (txtMaXe.Text != IdXe)
            {
                MessageBox.Show("Mã xe đã bị thay đổi. \nCập nhật không thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaXe.SelectAll();
                txtMaXe.Focus();

                return;
            }

            if (!String.IsNullOrEmpty(txtMaXe.Text))
            {
                DialogResult chon = MessageBox.Show("Bạn có chắn chắn xóa thông tin loại xe này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (chon == DialogResult.Yes)
                {
                    try
                    {
                        cmd = new SqlCommand("delete XeMay Where IdXe = @IdXe and IdCongTy = @IdCongTy");
                        cmd.Parameters.AddWithValue("@IdXe", txtMaXe.Text);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                        {
                            MessageBox.Show("Xóa thông tin xe thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ResetData();
                            LoadListMotor();
                            dgvDanhSachXe.DataSource = dtMotor;

                            try
                            {
                                DataRow[] r = dtMotor.Select("IdXe = '" + txtMaXe.Text + "'");
                                DataRow row = r[0];
                                dtMotor.Rows.Remove(row);
                            }
                            catch { LoadListMotor(); }
                            return;
                        }
                        else
                        {
                            MessageBox.Show("Thông tin xe không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void dgvDanhSachXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaXe.Text = Convert.ToString(dgvDanhSachXe.Rows[e.RowIndex].Cells["MaXe"].Value);
                IdXe = Convert.ToString(dgvDanhSachXe.Rows[e.RowIndex].Cells["MaXe"].Value);
                txtTenXe.Text = Convert.ToString(dgvDanhSachXe.Rows[e.RowIndex].Cells["TenXe"].Value);
                txt_DoiXe.Text = dgvDanhSachXe.Rows[e.RowIndex].Cells["DoiXe"].Value.ToString();
                txtDonGia.Text = dgvDanhSachXe.Rows[e.RowIndex].Cells["DonGia"].Value.ToString();
                cboDVT.Text = dgvDanhSachXe.Rows[e.RowIndex].Cells["DVT"].Value.ToString();
                cboNhaSanXuat.SelectedValue = dgvDanhSachXe.Rows[e.RowIndex].Cells["HangSanXuat"].Value;

                //txt_DoiXe.Text=dgvDanhSachXe.Rows[e.RowIndex].Cells[
            }
            catch { }
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtDonGia.Text))
                {
                    tien = 0;
                }
                else
                {
                    tien = Convert.ToDecimal(txtDonGia.Text);
                }
            }
            catch { MessageBox.Show("Đơn giá phải là kiểu số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            txtDonGia.Text = tien.ToString("0,0");
            txtDonGia.SelectionStart = txtDonGia.Text.Length;
        }
    }
}