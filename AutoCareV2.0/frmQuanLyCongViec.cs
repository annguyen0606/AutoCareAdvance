using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmQuanLyCongViec : Form
    {
        #region Delegate LoadData
        public delegate void ReloadCombobox();
        public ReloadCombobox Refresh;
        #endregion

        #region Variable
        private static DataTable dtCongViec = new DataTable();
        private int idtiencong = 0;
        private bool kq = false;
        private string _MaCV;
        #endregion

        public frmQuanLyCongViec()
        {
            InitializeComponent();
        }

        private void ResetData()
        {
            txtMaCV.Text = "";
            txtNoiDungCV.Text = "";
            txtTienCong.Text = "";
            idtiencong = 0;
            _MaCV = "";
            kq = false;
            rtbGhiChu.Text = "";

            LoadCongViec();
        }

        private void LoadCongViec()
        {
            try
            {
                SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
                myCon.Open();
                string sql = "select * from TienCongTho a where a.IdCongTy=@IdCongTy";
                SqlDataAdapter da = new SqlDataAdapter(sql, myCon);
                da.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                dtCongViec.Clear();
                da.Fill(dtCongViec);
                dsCongViec.DataSource = dtCongViec;
                dsCongViec.Columns["IdTienCong"].Visible = false;
                dsCongViec.Columns["TienCong"].DefaultCellStyle.Format = "0,0";
                dsCongViec.Columns["IdCongty"].Visible = false;
                myCon.Close();
            }
            catch { }
        }

        private bool CheckMaBD(string ma)
        {
            bool kq = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.datatabase.getConnection();
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;

            //cmd.CommandText = "select IdTienCong from TienCongTho where MaBD='"+ ma +"' and IdCongty=" + Class.CompanyInfo.idcongty;
            cmd.CommandText = "select IdTienCong from TienCongTho where MaBD=@MaBD and IdCongty=@IdCongTy";
            cmd.Parameters.AddWithValue("@MaBD", ma);
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            cmd.Connection.Open();
            reader = cmd.ExecuteReader();

            if (reader != null)
            {
                while (reader.Read())
                {
                    kq = true;
                }
            }
            cmd.Connection.Close();

            return kq;
        }

        private void frmQuanLyCongViec_Load(object sender, EventArgs e)
        {
            txtMaCV.Focus();
            txtTienCong.Text = Convert.ToDecimal(0).ToString("0,0");

            LoadCongViec();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetData();
        }

        private void dsCongViec_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                DataGridViewRow r = dsCongViec.Rows[e.RowIndex];

                _MaCV = Convert.ToString(r.Cells[1].Value);
                idtiencong = Convert.ToInt32(r.Cells[0].Value);
                txtMaCV.Text = Convert.ToString(r.Cells[1].Value);
                txtNoiDungCV.Text = Convert.ToString(r.Cells[2].Value);
                txtTienCong.Text = Convert.ToString(r.Cells[3].Value);
                rtbGhiChu.Text = Convert.ToString(r.Cells[4].Value);
            }
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtMaCV.Text))
                {
                    MessageBox.Show("Mã công việc không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaCV.Focus();

                    return;
                }

                if (String.IsNullOrEmpty(txtTienCong.Text))
                {
                    MessageBox.Show("Tiền công không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTienCong.Focus();

                    return;
                }

                if (Convert.ToDecimal(txtTienCong.Text) < 0)
                {
                    MessageBox.Show("Tiền công không được nhỏ hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTienCong.Focus();

                    return;
                }

                SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myCon;
                myCon.Open();
                cmd.CommandText = "if not exists(select 1 From TienCongTho Where MaBD = @MaBD And IdCongTy = @IdCongTy) begin INSERT INTO TienCongTho (MaBD, NoiDungBD, TienCong, GhiChu, IdCongTy) VALUES (@MaBD, @NoiDungBD, @TienCong, @GhiChu, @IdCongTy) select @@Identity end";
                cmd.Parameters.AddWithValue("@MaBD", txtMaCV.Text.Trim());
                cmd.Parameters.AddWithValue("@noiDungBD", txtNoiDungCV.Text.Trim());
                cmd.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(txtTienCong.Text));
                cmd.Parameters.AddWithValue("@GhiChu", rtbGhiChu.Text);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                string i = Convert.ToString(cmd.ExecuteScalar());

                if (String.IsNullOrEmpty(i))
                {
                    MessageBox.Show("Mã công việc này đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaCV.SelectAll();
                    txtMaCV.Focus();

                    return;
                }

                int id = int.Parse(i);
                ResetData();
                myCon.Close();
                MessageBox.Show("Thêm mới công việc thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (idtiencong != 0)
            {
                try
                {
                    if (String.IsNullOrEmpty(txtMaCV.Text))
                    {
                        MessageBox.Show("Mã công việc không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaCV.Focus();

                        return;
                    }

                    if (String.IsNullOrEmpty(txtTienCong.Text))
                    {
                        MessageBox.Show("Tiền công không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTienCong.Focus();

                        return;
                    }

                    if (Convert.ToDecimal(txtTienCong.Text) < 0)
                    {
                        MessageBox.Show("Tiền công không được nhỏ hơn 0", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtTienCong.Focus();

                        return;
                    }

                    if (_MaCV != txtMaCV.Text)
                    {
                        kq = CheckMaBD(txtMaCV.Text);

                        if (kq == false)
                        {
                            SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = myCon;
                            myCon.Open();
                            cmd.CommandText = "UPDATE TienCongTho SET MaBD=@MaBD, NoiDungBD=@NoiDungBD, TienCong=@TienCong, GhiChu=@GhiChu WHERE IdTienCong=@IdTienCong AND IdCongTy=@IdCongTy";
                            cmd.Parameters.AddWithValue("@IdTienCong", idtiencong);
                            cmd.Parameters.AddWithValue("@MaBD", txtMaCV.Text.Trim());
                            cmd.Parameters.AddWithValue("@NoiDungBD", txtNoiDungCV.Text.Trim());
                            cmd.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(txtTienCong.Text));
                            cmd.Parameters.AddWithValue("@GhiChu", rtbGhiChu.Text);
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                            {
                                MessageBox.Show("Cập nhật công việc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                ResetData();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã công việc đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaCV.SelectAll();
                            txtMaCV.Focus();

                            return;
                        }
                    }
                    else
                    {
                        SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = myCon;
                        myCon.Open();
                        cmd.CommandText = "UPDATE TienCongTho SET MaBD=@MaBD, NoiDungBD=@NoiDungBD, TienCong=@TienCong, GhiChu=@GhiChu WHERE IdTienCong=@IdTienCong AND IdCongTy=@IdCongTy";
                        cmd.Parameters.AddWithValue("@IdTienCong", idtiencong);
                        cmd.Parameters.AddWithValue("@MaBD", txtMaCV.Text.Trim());
                        cmd.Parameters.AddWithValue("@NoiDungBD", txtNoiDungCV.Text.Trim());
                        cmd.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(txtTienCong.Text));
                        cmd.Parameters.AddWithValue("@GhiChu", rtbGhiChu.Text);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                        {
                            LoadCongViec();
                            MessageBox.Show("Cập nhật công việc thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật không thành công, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn công việc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (idtiencong != 0)
                {
                    DialogResult chon = MessageBox.Show("Bạn có chắc chắn muốn xóa công việc này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (chon == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "DELETE FROM TienCongTho WHERE IdTienCong=@IdTienCong";

                        cmd.Parameters.AddWithValue("@IdTienCong", idtiencong);
                        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                        {
                            MessageBox.Show("Xóa công việc thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            ResetData();
                        }
                    }
                }
                else { MessageBox.Show("Bạn chưa chọn công việc!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void txtMaCV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtNoiDungCV.Focus();
        }

        private void txtNoiDungCV_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtTienCong.Focus();
        }

        private void txtTienCong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                rtbGhiChu.Focus();
        }

        private void txtTienCong_TextChanged(object sender, EventArgs e)
        {
            decimal tien = 0;

            try
            {
                if (String.IsNullOrEmpty(txtTienCong.Text))
                {
                    tien = 0;
                }
                else
                {
                    tien = Convert.ToDecimal(txtTienCong.Text);
                }
            }
            catch { MessageBox.Show("Tiền công phải là kiểu số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            txtTienCong.Text = tien.ToString("0,0");
            txtTienCong.SelectionStart = txtTienCong.Text.Length;
        }

        private void frmQuanLyCongViec_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (Refresh != null)
                Refresh();
        }
    }
}