using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmQuanLyThoDichVu : Form
    {
        private int IdTho;
        private string _MaTho;
        private bool kq = false;
        private bool vldti = false;

        public frmQuanLyThoDichVu()
        {
            InitializeComponent();
        }

        private void frmQuanLyThoDichVu_Load(object sender, EventArgs e)
        {
            txtTenCongTy.Text = Class.CompanyInfo.tencongty;

            txtMaTho.Focus();

            LoadData();

            LoadChucVu();

            LoadBoPhan();

            LoadCuaHang();
        }

        /// <summary>
        ///
        /// </summary>
        private void LoadData()
        {
            using (SqlConnection con = Class.datatabase.getConnection())
            {
                try
                {
                    con.Open();

                    string sql = @"SELECT IdTho, tdv.MaTho, ch.IdCuaHang, bp.IdBoPhan, cv.IdChucVu, TenTho, tdv.Phone, CONVERT(nchar(10), NgaySinh, 103) AS NgaySinh, ct.TenCongTy, ch.TenCuaHang, bp.TenBoPhan, cv.TenChucVu
                                From ThoDichVu tdv
                                LEFT OUTER JOIN CongTy ct ON tdv.Idcongty=ct.IdCongTy
                                LEFT OUTER JOIN CuaHang ch ON tdv.IdCuaHang=ch.IdCuaHang
                                LEFT OUTER JOIN BoPhan bp ON tdv.IdBoPhan=bp.IdBoPhan
                                LEFT OUTER JOIN ChucVu cv ON tdv.IdChucVu=cv.IdChucVu
                                WHERE ct.IdCongTy=@IdCongTy and tdv.TinhTrangLamViec is null ";
                    SqlDataAdapter da = new SqlDataAdapter(sql, con);
                    da.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                    DataTable dtThoDV = new DataTable();

                    dtThoDV.Clear();
                    da.Fill(dtThoDV);
                    dataGridViewThoDichVu.DataSource = dtThoDV;

                    dataGridViewThoDichVu.Columns["IdTho"].Visible = false;
                    dataGridViewThoDichVu.Columns["IdCuaHang"].Visible = false;
                    dataGridViewThoDichVu.Columns["IdBoPhan"].Visible = false;
                    dataGridViewThoDichVu.Columns["IdChucVu"].Visible = false;

                    con.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        private void LoadChucVu()
        {
            try
            {
                using (SqlConnection cnn = Class.datatabase.getConnection())
                {
                    cnn.Open();

                    string tenchucvu = "SELECT DISTINCT IdChucVu, TenChucVu FROM ChucVu WHERE IdCongTy=@IdCongTy";
                    SqlCommand cmd = new SqlCommand(tenchucvu, cnn);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    adap.Fill(dt);

                    comboBoxChucVu.DataSource = null;
                    comboBoxChucVu.DataSource = dt;
                    comboBoxChucVu.DisplayMember = "TenChucVu";
                    comboBoxChucVu.ValueMember = "IdChucVu";

                    comboBoxChucVu.SelectedIndex = -1;

                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
            }
        }

        private void LoadBoPhan()
        {
            try
            {
                using (SqlConnection cnn = Class.datatabase.getConnection())
                {
                    cnn.Open();

                    string tenbophan = "SELECT DISTINCT IdBoPhan, TenBoPhan FROM BoPhan WHERE IdCongTy=@IdCongTy";
                    SqlCommand cmd = new SqlCommand(tenbophan, cnn);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    adap.Fill(dt);

                    comboBoxBoPhan.DataSource = null;
                    comboBoxBoPhan.DataSource = dt;
                    comboBoxBoPhan.DisplayMember = "TenBoPhan";
                    comboBoxBoPhan.ValueMember = "IdBoPhan";

                    comboBoxBoPhan.SelectedIndex = -1;

                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
            }
        }

        private void LoadCuaHang()
        {
            try
            {
                using (SqlConnection cnn = Class.datatabase.getConnection())
                {
                    cnn.Open();

                    string tencuahang = "SELECT DISTINCT IdCuaHang, TenCuaHang FROM CuaHang WHERE IdCongTy=@IdCongTy";
                    SqlCommand cmd = new SqlCommand(tencuahang, cnn);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                    SqlDataAdapter adap = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();

                    adap.Fill(dt);

                    comboBoxCuaHang.DataSource = null;
                    comboBoxCuaHang.DataSource = dt;
                    comboBoxCuaHang.DisplayMember = "TenCuaHang";
                    comboBoxCuaHang.ValueMember = "IdCuaHang";

                    comboBoxCuaHang.SelectedIndex = -1;

                    cnn.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
            }
        }

        private void ResetForm()
        {
            txtDienThoai.Text = "";
            txtMaTho.Text = "";
            txtTenTho.Text = "";
            comboBoxBoPhan.Text = "";
            comboBoxChucVu.Text = "";
            comboBoxCuaHang.Text = "";
            IdTho = 0;
            _MaTho = "";
            dateTimePickerNgaySinh.Value = DateTime.Now;

            LoadData();
        }

        /// <summary>
        ///
        /// </summary>
        private void ValidationInput()
        {
            String PhoneNumber = "0\\d{9,10}";

            if (String.IsNullOrEmpty(txtMaTho.Text))
            {
                MessageBox.Show("Mã thợ dịch vụ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaTho.Focus();

                return;
            }

            if (txtMaTho.Text.Length > 10)
            {
                MessageBox.Show("Mã thợ chỉ được nhập lớn hơn 0 và nhỏ hơn 10 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaTho.SelectAll();
                txtMaTho.Focus();

                return;
            }

            if (String.IsNullOrEmpty(txtTenTho.Text))
            {
                MessageBox.Show("Tên thợ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTho.Focus();

                return;
            }

            if (txtTenTho.Text.Length > 100)
            {
                MessageBox.Show("Tên thợ không được vượt quá 100 ký tự", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenTho.SelectAll();
                txtTenTho.Focus();

                return;
            }

            if (comboBoxBoPhan.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Bộ phận cho thợ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxBoPhan.Focus();

                return;
            }
            if (comboBoxChucVu.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Chức vụ cho thợ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxChucVu.Focus();

                return;
            }
            if (comboBoxCuaHang.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Cửa hàng cho thợ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                comboBoxCuaHang.Focus();

                return;
            }
            if (String.IsNullOrEmpty(txtDienThoai.Text))
            {
                MessageBox.Show("Số điện thoại của thợ không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDienThoai.Focus();

                return;
            }
            if (txtDienThoai.Text.Length > 11)
            {
                MessageBox.Show("Số điện thoại không được lớn hơn 11 ký tự!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                vldti = false;
                txtDienThoai.SelectAll();
                txtDienThoai.Focus();

                return;
            }
            else
            {
                vldti = true;
            }

            if (!Regex.IsMatch(txtDienThoai.Text, PhoneNumber))
            {
                MessageBox.Show("Nhập không đúng định dạng số điện thoại! Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                vldti = false;
                txtDienThoai.SelectAll();
                txtDienThoai.Focus();

                return;
            }
            else
            {
                vldti = true;
            }
        }

        private bool CheckMaTho(string ma)
        {
            bool kq = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.datatabase.getConnection();
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;

            //cmd.CommandText = "select IdTho from ThoDichVu where MaTho='" + ma + "' and IdCongty=" + Class.CompanyInfo.idcongty;
            cmd.CommandText = "select IdTho from ThoDichVu where MaTho=@MaTho and IdCongty=@IdCongTy";
            cmd.Parameters.AddWithValue("@MaTho", ma);
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

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            try
            {
                ValidationInput();
                if (vldti == true)
                {
                    SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = myCon;
                    myCon.Open();

                    cmd.CommandText = @"if not exists(select 1 From ThoDichVu Where MaTho = @MaTho And IdCongTy = @IdCongTy and TinhTrangLamViec is null)
                                    begin INSERT INTO ThoDichVu (MaTho, tenTho, IdChucVu, IdBoPhan, IdCongTy, IdCuaHang, Phone, ngaySinh)
                                    VALUES (@MaTho, @tenTho, @IdChuVu, @IdBoPhan, @IdCongTy, @IdCuaHang, @Phone, @ngaySinh) select @@Identity end";
                    cmd.Parameters.AddWithValue("@MaTho", txtMaTho.Text.Trim());
                    cmd.Parameters.AddWithValue("@tenTho", txtTenTho.Text.Trim());
                    cmd.Parameters.AddWithValue("@IdChuVu", comboBoxChucVu.SelectedValue);
                    cmd.Parameters.AddWithValue("@IdBoPhan", comboBoxBoPhan.SelectedValue);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdCuaHang", comboBoxCuaHang.SelectedValue);
                    cmd.Parameters.AddWithValue("@Phone", txtDienThoai.Text.Trim());
                    cmd.Parameters.AddWithValue("@ngaySinh", dateTimePickerNgaySinh.Value);

                    string i = Convert.ToString(cmd.ExecuteScalar());
                    if (String.IsNullOrEmpty(i))
                    {
                        MessageBox.Show("Mã thợ đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaTho.SelectAll();
                        txtMaTho.Focus();

                        return;
                    }
                    int id = int.Parse(i);
                    myCon.Close();
                    ResetForm();
                    MessageBox.Show("Thêm mới thợ dịch vụ thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridViewThoDichVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                try
                {
                    DataGridViewRow r = dataGridViewThoDichVu.Rows[e.RowIndex];

                    _MaTho = r.Cells[1].Value.ToString();
                    IdTho = Convert.ToInt32(r.Cells[0].Value);
                    txtMaTho.Text = r.Cells[1].Value.ToString();

                    if (r.Cells[2].Value.ToString() != "" && r.Cells[2].Value != null)
                    {
                        comboBoxCuaHang.SelectedValue = r.Cells[2].Value;
                    }
                    else
                    {
                        comboBoxCuaHang.SelectedValue = -1;
                    }

                    if (r.Cells[3].Value.ToString() != "" && r.Cells[3].Value != null)
                    {
                        comboBoxBoPhan.SelectedValue = r.Cells[3].Value;
                    }
                    else
                    {
                        comboBoxBoPhan.SelectedValue = -1;
                    }

                    if (r.Cells[4].Value.ToString() != "" && r.Cells[4].Value != null)
                    {
                        comboBoxChucVu.SelectedValue = r.Cells[4].Value;
                    }
                    else
                    {
                        comboBoxChucVu.SelectedValue = -1;
                    }

                    txtTenTho.Text = r.Cells[5].Value.ToString();
                    txtDienThoai.Text = r.Cells[6].Value.ToString().Trim();
                    dateTimePickerNgaySinh.Value = Convert.ToDateTime(r.Cells[7].Value);
                }
                catch { }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (IdTho != 0)
            //    {
            //        DialogResult chon = MessageBox.Show("Bạn có chắc chắn muốn xóa thợ dịch vụ này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //        if (chon == DialogResult.Yes)
            //        {
            //            SqlCommand cmd = new SqlCommand();
            //            cmd.CommandText = "DELETE FROM ThoDichVu WHERE IdTho=@IdTho";

            //            cmd.Parameters.AddWithValue("@IdTho", IdTho);
            //            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
            //            {
            //                MessageBox.Show("Xóa thợ dịch vụ thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                ResetForm();
            //            }
            //        }
            //    }
            //    else { MessageBox.Show("Bạn chưa chọn Thợ dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            //}
            //catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            try
            {
                if (IdTho != 0)
                {
                    DialogResult chon = MessageBox.Show("Bạn có chắc chắn muốn xóa thợ dịch vụ này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (chon == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "Update ThoDichVu SET TinhTrangLamViec = 1 WHERE IdTho=@IdTho";

                        cmd.Parameters.AddWithValue("@IdTho", IdTho);
                        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                        {
                            MessageBox.Show("Xóa thợ dịch vụ thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetForm();
                        }
                    }
                }
                else { MessageBox.Show("Bạn chưa chọn Thợ dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_MaTho))
            {
                MessageBox.Show("Bạn chưa chọn Thợ dịch vụ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                ValidationInput();
                if (vldti == true)
                {
                    if (_MaTho != txtMaTho.Text)
                    {
                        kq = CheckMaTho(txtMaTho.Text);

                        if (kq == false)
                        {
                            SqlCommand cmd = new SqlCommand();
                            cmd.CommandText = @"UPDATE ThoDichVu SET MaTho=@MaTho, tenTho=@tenTho, IdChucVu=@IdChucVu, IdBoPhan=@IdBoPhan, IdCuaHang=@IdCuaHang, Phone=@Phone, ngaySinh=@ngaySinh,     
                                            WHERE IdTho=@IdTho AND IdCongTy=@IdCongTy";
                            cmd.Parameters.AddWithValue("@IdTho", IdTho);
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@MaTho", txtMaTho.Text.Trim());
                            cmd.Parameters.AddWithValue("@tenTho", txtTenTho.Text.Trim());
                            cmd.Parameters.AddWithValue("@IdChucVu", comboBoxChucVu.SelectedValue);
                            cmd.Parameters.AddWithValue("@IdBoPhan", comboBoxBoPhan.SelectedValue);
                            cmd.Parameters.AddWithValue("@IdCuaHang", comboBoxCuaHang.SelectedValue);
                            cmd.Parameters.AddWithValue("@Phone", txtDienThoai.Text.Trim());
                            cmd.Parameters.AddWithValue("@ngaySinh", dateTimePickerNgaySinh.Value);

                            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                            {
                                MessageBox.Show("Cập nhật thợ dịch vụ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ResetForm();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Mã thợ đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaTho.SelectAll();
                            txtMaTho.Focus();
                        }
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = @"UPDATE ThoDichVu SET MaTho=@MaTho, tenTho=@tenTho, IdChucVu=@IdChucVu, IdBoPhan=@IdBoPhan, IdCuaHang=@IdCuaHang, Phone=@Phone, ngaySinh=@ngaySinh
                                            WHERE IdTho=@IdTho AND IdCongTy=@IdCongTy";
                        cmd.Parameters.AddWithValue("@IdTho", IdTho);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@MaTho", txtMaTho.Text.Trim());
                        cmd.Parameters.AddWithValue("@tenTho", txtTenTho.Text.Trim());
                        cmd.Parameters.AddWithValue("@IdChucVu", comboBoxChucVu.SelectedValue);
                        cmd.Parameters.AddWithValue("@IdBoPhan", comboBoxBoPhan.SelectedValue);
                        cmd.Parameters.AddWithValue("@IdCuaHang", comboBoxCuaHang.SelectedValue);
                        cmd.Parameters.AddWithValue("@Phone", txtDienThoai.Text.Trim());
                        cmd.Parameters.AddWithValue("@ngaySinh", dateTimePickerNgaySinh.Value);

                        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                        {
                            MessageBox.Show("Cập nhật thợ dịch vụ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ResetForm();
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật không thành công, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMaTho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtTenTho.Focus();
        }

        private void txtTenTho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                comboBoxChucVu.Focus();
        }

        private void comboBoxChucVu_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                comboBoxBoPhan.Focus();
        }

        private void comboBoxCuaHang_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtDienThoai.Focus();
        }

        private void comboBoxBoPhan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                comboBoxCuaHang.Focus();
        }

        private void txtDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                dateTimePickerNgaySinh.Focus();
        }

        private void dateTimePickerNgaySinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                btnThemMoi.Focus();
        }
    }
}