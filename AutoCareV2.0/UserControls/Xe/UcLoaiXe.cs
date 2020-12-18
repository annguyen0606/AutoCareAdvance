using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AutoCareV2._0.UserControls
{
    public partial class UcLoaiXe : UserControl
    {
        public UcLoaiXe()
        {
            InitializeComponent();
        }   

        private static string ID = "";

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtKieuXe.Text))
                {
                    MessageBox.Show("Bạn chưa nhập kiểu xe.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SqlCommand cmd = new SqlCommand("insert into LoaiXe (TenLoaiXe, GhiChu, IdCongTy) values (@TenLoaiXe, @GhiChu, @IdCongTy)");
                cmd.Parameters.AddWithValue("@TenLoaiXe", txtKieuXe.Text);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@GhiChu", txtMota.Text);
                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Thêm kiểu xe thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dt.Rows.Add(txtKieuXe.Text, txtMota.Text);
                    return;
                }
                else
                {
                    MessageBox.Show("Kiểu xe này đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmQuanLyKieuXe_Load(object sender, EventArgs e)
        {
            LoadListTypeMotor();

        }
        DataTable dt = new DataTable();
        private void LoadListTypeMotor()
        {
            SqlCommand cmd = new SqlCommand("select IdLoaiXe, TenLoaiXe, GhiChu from LoaiXe where IdCongTy=@IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dt = Class.datatabase.getData(cmd);
            dgvKieuXe.DataSource = dt;
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult chon = MessageBox.Show("Bạn có chắc chắn muốn cập nhật thông tin kiểu xe này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (chon == DialogResult.Yes)
                {
                    SqlCommand cmd = new SqlCommand("update LoaiXe set TenLoaiXe=@TenLoaiXe, GhiChu=@GhiChu Where IdLoaiXe=@IdLoaiXe");
                    cmd.Parameters.AddWithValue("@IdLoaiXe", ID);
                    cmd.Parameters.AddWithValue("@GhiChu", txtMota.Text);
                    cmd.Parameters.AddWithValue("@TenLoaiXe", txtKieuXe.Text);
                    if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    {
                        try
                        {
                            DataRow[] r = dt.Select("TenLoaiXe = '" + txtKieuXe.Text + "'");
                            r[0]["GhiChu"] = txtMota.Text;
                        }
                        catch { LoadListTypeMotor(); }

                        MessageBox.Show("Cập nhật kiểu xe thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }
                    else
                    {
                        MessageBox.Show("Loại xe này không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }



        private void dgvKieuXe_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKieuXe.SelectedRows.Count > 0)
            {
                ID = dgvKieuXe.SelectedRows[0].Cells[0].Value.ToString();
                txtKieuXe.Text = Convert.ToString(dgvKieuXe.SelectedRows[0].Cells[1].Value.ToString());
                txtMota.Text = Convert.ToString(dgvKieuXe.SelectedRows[0].Cells[2].Value.ToString());
            }
        }
    }
}
