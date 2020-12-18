using AutoCareV2._0.Class;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls.QuanLyPhuTung
{
    public partial class UcNhapMoiPhuTung : UserControl
    {
        #region Variable

        private KhDB classdb = new KhDB();
        private DataTable dtNhaCungCap = new DataTable();
        private DataTable dtPhuTung = new DataTable();
        private int soLuongNhap = 0;
        private decimal donGiaNhap = 0;
        private decimal tongtien;
        private decimal giaban;
        private decimal flag_decimal;
        private int flag_int;

        #endregion Variable

        /// <summary>
        /// Initializes a new instance of the <see cref="UcNhapMoiPhuTung"/> class.
        /// </summary>
        public UcNhapMoiPhuTung()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the UcNhapMoiPhuTung control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void UcNhapMoiPhuTung_Load(object sender, EventArgs e)
        {
            LayKieuNhapHang();
            LayNhaCungCap();
            LayDanhSachKho(CompanyInfo.idcongty);
            btnClear.Show();

            txtSoLuong.Text = "0";
            txtDonGiaNhap.Text = "0";

            dateTimeInputNgayHoaDon.Value = DateTime.Now;
        }

        /// <summary>
        /// Lays the danh sach kho.
        /// </summary>
        /// <param name="idCongTy">The identifier cong ty.</param>
        private void LayDanhSachKho(string idCongTy)
        {
            using (SqlCommand cmd = new SqlCommand("select * from KhoHang where IdCongTy = @IdCongTy"))
            {
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                DataTable tblKhoHang = datatabase.getData(cmd);

                cbbChonKho.ValueMember = "IdKho";
                cbbChonKho.DisplayMember = "TenKho";
                cbbChonKho.DataSource = tblKhoHang;
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbbChonKho control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void cbbChonKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbChonKho.SelectedValue != null && !String.IsNullOrEmpty(cbbChonKho.Text))
            {
                cbbChonPhuTung.DataSource = null;
                LayDanhSachPhuTungCuaKho(cbbChonKho.SelectedValue.ToString());
            }
        }

        /// <summary>
        /// Lays the danh sach phu tung cua kho.
        /// </summary>
        /// <param name="IdKho">The identifier kho.</param>
        private void LayDanhSachPhuTungCuaKho(string IdKho)
        {
            using (SqlCommand cmd = new SqlCommand("select DISTINCT IdPT, (MaPT + '-' + TenPT) as TenPhuTung from PhuTung where IdKho = @IdKho order by TenPhuTung"))
            {
                cmd.Parameters.AddWithValue("@IdKho", IdKho);
                DataTable tblPhuTung = datatabase.getData(cmd);

                cbbChonPhuTung.ValueMember = "IdPT";
                cbbChonPhuTung.DisplayMember = "TenPhuTung";
                cbbChonPhuTung.DataSource = tblPhuTung;
            }
        }

        /// <summary>
        /// Lays the kieu nhap hang.
        /// </summary>
        private void LayKieuNhapHang()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KieuNhapXe");
            cboKieuNhap.DataSource = datatabase.getData(cmd);
            cboKieuNhap.ValueMember = "IdKieuNhapXe";
            cboKieuNhap.DisplayMember = "TenKieuNhapXe";
        }

        /// <summary>
        /// Lays the nha cung cap.
        /// </summary>
        private void LayNhaCungCap()
        {
            SqlCommand cmd = new SqlCommand("SElect * FROM NhaCungCap WHERE IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            dtNhaCungCap = datatabase.getData(cmd);
            cboNhaCungCap.DataSource = dtNhaCungCap;
            cboNhaCungCap.ValueMember = "IdNhaCungCap";
            cboNhaCungCap.DisplayMember = "TenNhaCungCap";
            cboNhaCungCap.SelectedIndex = -1;
        }

        /// <summary>
        /// Handles the Click event of the btnLuu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnLuu_Click(object sender, EventArgs e)
        {
            decimal donGia = 0m; int soLuongNhap = 0;

            if (dateTimeInputNgayHoaDon.ValueObject == null)
            {
                MessageBox.Show("Bạn chưa nhập thời gian nhập phụ tùng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimeInputNgayHoaDon.Focus();

                return;
            }
            if (String.IsNullOrEmpty(txtSoHoaDon.Text))
            {
                MessageBox.Show("Bạn chưa nhập hóa đơn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoHoaDon.Focus();

                return;
            }
            else
            {
                if (!txtSoHoaDon.Text.Equals("0"))
                {
                    SqlCommand cmd1 = new SqlCommand("Select * From HoaDonNhapPhuTung Where IDCongTy = @IDCongTy and SoHoaDon = @SoHoaDon");
                    cmd1.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd1.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                    if (datatabase.getData(cmd1).Rows.Count > 0)
                    {
                        MessageBox.Show("Số hóa đơn này đã tồn tại. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSoHoaDon.SelectAll();
                        txtSoHoaDon.Focus();

                        return;
                    }
                    cmd1.Connection.Close();
                }
            } 

            SqlCommand cmd = new SqlCommand(@"Insert Into HoaDonNhapPhuTung(SoHoaDon,LoHang,IDNhanVien,IDNhaCungCap,NgayLap,IDCuaHang,IdKieuNhap,IDCongTy,DaNhanHoaDon,TienDaTra,TongTien,GhiChu) 
                                            values (@SoHoaDon,@LoHang,@IDNhanVien,@IDNhaCungCap,@NgayLap,@IDCuaHang,@IdKieuNhap,@IDCongTy,@DaNhanHoaDon,@TienDaTra,@TongTien,@GhiChu) Select @@Identity");
            cmd.Connection = datatabase.getConnection();
            cmd.Connection.Open();

            cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
            cmd.Parameters.AddWithValue("@LoHang", txtLoHang.Text);
            cmd.Parameters.AddWithValue("@IDNhanVien", EmployeeInfo.idnhanvien);
            if (cboNhaCungCap.SelectedIndex == -1)
            {
                cmd.Parameters.AddWithValue("@IDNhaCungCap", SqlString.Null);
            }
            else
            {
                cmd.Parameters.AddWithValue("@IDNhaCungCap", cboNhaCungCap.SelectedValue);
            }
            cmd.Parameters.AddWithValue("@NgayLap", dateTimeInputNgayHoaDon.Value);
            cmd.Parameters.AddWithValue("@IDCuaHang", EmployeeInfo.IdCuaHang);
            cmd.Parameters.AddWithValue("@IdKieuNhap", cboKieuNhap.SelectedValue);
            cmd.Parameters.AddWithValue("@IDCongTy", CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@DaNhanHoaDon", chkDaNhanHoaDon.Checked);

            decimal tienDaTra;
            try
            {
                tienDaTra = Convert.ToDecimal(txtTienDaTra.Text);
            }
            catch
            { tienDaTra = 0; }

            try
            {
                tongtien = Convert.ToDecimal(txtTongTien.Text);
            }
            catch
            {
                tongtien = 0;
            }

            cmd.Parameters.AddWithValue("@TienDaTra", tienDaTra);
            cmd.Parameters.AddWithValue("@TongTien", tongtien);
            cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);

            string key = cmd.ExecuteScalar().ToString();

            if (tienDaTra > 0)
            {
                cmd.CommandText = "insert into PhieuChi(IDLoaiPhieuChi,SoTienChi,NgayHachToan,IDCongTy,NguoiNhan,IdNhaCungCap,IDNhanVien,NoiDung,DaNhanHang,IdCuaHang,SoHoaDon) " +
                                 " Values (@IDLoaiPhieuChi,@SoTienChi,@NgayChi,@IDCongTy,@NguoiNhan,@IdNhaCungCap,@IdNhanVien,@NoiDung,@DaNhanHang,@IdCuaHang,@SoHoaDon)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdLoaiPhieuChi", 2);
                cmd.Parameters.AddWithValue("@SoTienChi", tienDaTra);
                cmd.Parameters.AddWithValue("@NgayChi", dateTimeInputNgayHoaDon.Value);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@NguoiNhan", "");

                if (cboNhaCungCap.SelectedIndex == -1)
                {
                    cmd.Parameters.AddWithValue("@IDNhaCungCap", SqlString.Null);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@IDNhaCungCap", cboNhaCungCap.SelectedValue);
                }
                cmd.Parameters.AddWithValue("@IdNhanVien", Class.EmployeeInfo.idnhanvien);
                cmd.Parameters.AddWithValue("@NoiDung", txtGhiChu.Text);
                cmd.Parameters.AddWithValue("@DaNhanHang", chkDaNhanHoaDon.Checked);
                cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                if (cmd.ExecuteNonQuery() <= 0)
                {
                    MessageBox.Show(@"Thêm phiếu chi thất bại.", @"Thông báo");
                    return;
                }
            }

            SqlTransaction tran = cmd.Connection.BeginTransaction();
            cmd.Transaction = tran;
            try
            {
                for (int i = 0; i < dgvPhuTungNhap.Rows.Count; i++)
                {
                    soLuongNhap = Convert.ToInt32(dgvPhuTungNhap.Rows[i].Cells["SoLuong"].Value);
                    donGia = Convert.ToDecimal(dgvPhuTungNhap.Rows[i].Cells["DonGia"].Value);
                    string idpt2 = dgvPhuTungNhap.Rows[i].Cells["IdPhuTung"].Value.ToString();

                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandText = "select * from PhuTung Where IdPT=@idpt And IdCongTy=@idct";
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.AddWithValue("@idpt", idpt2);
                    cmd2.Parameters.AddWithValue("@idct", Class.CompanyInfo.idcongty);

                    DataTable datainfo = Class.datatabase.getData(cmd2);

                    cmd.CommandText = "Insert into ChiTietNhapPhuTung Values(@SoHoaDon,@IDPhuTung,@SoLuong,@DonGiaNhap,@IdKey)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                    cmd.Parameters.AddWithValue("@IDPhuTung", idpt2);
                    cmd.Parameters.AddWithValue("@SoLuong", soLuongNhap);
                    cmd.Parameters.AddWithValue("@DonGiaNhap", donGia);
                    cmd.Parameters.AddWithValue("@IdKey", key);
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "UPDATE PhuTung set SoLuong = isnull(SoLuong,0) + @SoLuong WHERE  IdCongTy = @IdCongTy AND IdPT = @IdPhuTung";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SoLuong", soLuongNhap);
                    cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdPhuTung", idpt2);

                    if (cmd.ExecuteNonQuery() <= 0)
                    {
                        MessageBox.Show("Phụ tùng " + idpt2 + ". Không thể cập nhật số lượng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        tran.Rollback();
                        return;
                    }

                    if (datainfo.Rows.Count > 0)
                    {
                        cmd.CommandText = "insert into KhoNhap(IdPT,MaPT,TenPT,SoLuong,NgayNhap,LoaiNhap,IdKho,GhiChu,IdCongTy) values(@idpt,@mapt,@tenPT,@soluong,@ngaynhap,@loai,@idkho,@ghichu,@idcongty)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idpt", datainfo.Rows[0]["IdPT"]);
                        cmd.Parameters.AddWithValue("@mapt", datainfo.Rows[0]["MaPT"]);
                        cmd.Parameters.AddWithValue("@tenPT", datainfo.Rows[0]["TenPT"]);
                        cmd.Parameters.AddWithValue("@soluong", soLuongNhap);
                        cmd.Parameters.AddWithValue("@ngaynhap", dateTimeInputNgayHoaDon.Value);
                        cmd.Parameters.AddWithValue("@loai", cboKieuNhap.Text);
                        cmd.Parameters.AddWithValue("@idkho", datainfo.Rows[0]["IdKho"]);
                        cmd.Parameters.AddWithValue("@ghichu", txtGhiChu.Text);
                        cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                        if (cmd.ExecuteNonQuery() <= 0)
                        {
                            MessageBox.Show("Phụ tùng " + idpt2 + ". Không nhập được vào Kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            tran.Rollback();
                            return;
                        }
                    }
                }
                tran.Commit();
                MessageBox.Show("Nhập phụ tùng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { cmd.Connection.Close(); }
        }

        private void cboNhaCungCap_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = dtNhaCungCap.Select("IdNhaCungCap = " + "'" + cboNhaCungCap.SelectedValue.ToString() + "'");
                txtDiaChi.Text = rows[0]["DiaChi"].ToString();
            }
            catch { }
        }

        private void cboNhaCungCap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow[] rows = dtNhaCungCap.Select("IdNhaCungCap = " + "'" + cboNhaCungCap.SelectedValue.ToString() + "'");
                txtDiaChi.Text = rows[0]["DiaChi"].ToString();
            }
        }

        private void dgvPhuTungNhap_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex > 0)
                {
                    if (e.ColumnIndex == dgvPhuTungNhap.Columns["SoLuong"].Index)
                    {
                        if (int.TryParse(Convert.ToString(dgvPhuTungNhap.Rows[e.RowIndex].Cells["SoLuong"].Value), out soLuongNhap))
                        {
                            if (soLuongNhap <= 0)
                            {
                                MessageBox.Show("Số lượng nhập không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                dgvPhuTungNhap.Rows[e.RowIndex].Cells["SoLuong"].Value = 1;
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Số lượng nhập không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgvPhuTungNhap.Rows[e.RowIndex].Cells["SoLuong"].Value = 1;
                            return;
                        }
                    }
                    if (e.ColumnIndex == dgvPhuTungNhap.Columns["DonGia"].Index)
                    {
                        if (decimal.TryParse(Convert.ToString(dgvPhuTungNhap.Rows[e.RowIndex].Cells["DonGia"].Value), out donGiaNhap))
                        {
                            if (donGiaNhap < 0)
                            {
                                MessageBox.Show("Đơn giá nhập không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                dgvPhuTungNhap.Rows[e.RowIndex].Cells["DonGia"].Value = 0;
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Đơn giá nhập không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            dgvPhuTungNhap.Rows[e.RowIndex].Cells["DonGia"].Value = 0;
                            return;
                        }
                    }

                    dgvPhuTungNhap.Rows[e.RowIndex].Cells["ThanhTien"].Value = Convert.ToDecimal(dgvPhuTungNhap.Rows[e.RowIndex].Cells["DonGia"].Value) * Convert.ToDecimal(dgvPhuTungNhap.Rows[e.RowIndex].Cells["SoLuong"].Value);

                    decimal tongtien = 0;
                    foreach (DataGridViewRow row in dgvPhuTungNhap.Rows)
                    {
                        tongtien += decimal.Parse(row.Cells["ThanhTien"].Value.ToString());
                    }

                    txtTongTien.Text = tongtien.ToString();
                }
            }
            catch { }
        }

        private void dgvPhuTungNhap_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvPhuTungNhap.Columns["DonGia"].Index == dgvPhuTungNhap.CurrentCell.ColumnIndex)
            {
                TextBox txt_GiaBan = e.Control as TextBox;
                txt_GiaBan.TextChanged += new EventHandler(GiaBan_textchanged);
            }
        }

        private void dgvPhuTungNhap_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex > 0 && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dgvPhuTungNhap.Columns["Xoa"].Index)
                    {
                        dgvPhuTungNhap.Rows.RemoveAt(e.RowIndex);
                    }
                }
            }
            catch { }
        }

        private void dgvPhuTungNhap_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dgvPhuTungNhap.Rows[e.RowIndex].HeaderCell.Value = Convert.ToString(e.RowIndex + 1);
        }

        private void GiaBan_textchanged(object sender, EventArgs e)
        {
            if (dgvPhuTungNhap.CurrentCell.ColumnIndex == dgvPhuTungNhap.Columns["DonGia"].Index)
            {
                TextBox t = (sender as TextBox);
                try
                {
                    if (String.IsNullOrEmpty(t.Text))
                    {
                        giaban = 0;
                    }
                    else
                    {
                        giaban = Convert.ToDecimal(t.Text);
                    }
                }
                catch { }
                t.Text = giaban.ToString("0,0");
                t.SelectionStart = t.Text.Length;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            cboNhaCungCap.SelectedIndex = -1;
            txtDiaChi.Clear();
            txtGhiChu.Clear();
            txtLoHang.Clear();
            txtSoHoaDon.Clear();
            txtTongTien.Clear();
            dateTimeInputNgayHoaDon.Value = DateTime.Now;
            chkDaNhanHoaDon.Checked = true;
        }

        private void txtTienDaTra_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTienDaTra.Text.Length > 0)
                {
                    string Text = txtTienDaTra.Text;
                    txtTienDaTra.Text = String.Format("{0:N0}", decimal.Parse(Text));
                    txtTienDaTra.SelectionStart = txtTienDaTra.Text.Length;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void Chk_TraDu_CheckedChanged(object sender, EventArgs e)
        {
            if (Chk_TraDu.Checked)
                txtTienDaTra.Text = txtTongTien.Text;
            else
                txtTienDaTra.Text = "0";
        }

        private void btnNhap_Click(object sender, EventArgs e)
        {
            bool flag = false;

            if (cbbChonPhuTung.SelectedValue == null)
            {
                MessageBox.Show("Bạn cần chọn phụ tùng cần nhập!\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            foreach(DataGridViewRow row in dgvPhuTungNhap.Rows)
            {
                if (row.Cells["IdKho"].Value.ToString() == cbbChonKho.SelectedValue.ToString() && row.Cells["IdPhuTung"].Value.ToString() == cbbChonPhuTung.SelectedValue.ToString())
                {
                    flag = true;   
                    break;
                }
            }

            if (flag)
            {
                MessageBox.Show("Phụ tùng đã tồn tại trong danh sách cần nhập!\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            dgvPhuTungNhap.Rows.Add
                (
                    cbbChonKho.SelectedValue.ToString(),
                    cbbChonPhuTung.SelectedValue.ToString(),
                    cbbChonKho.Text,
                    cbbChonPhuTung.Text,
                    txtSoLuong.Text,
                    decimal.Parse(txtDonGiaNhap.Text),
                    decimal.Parse(txtSoLuong.Text) * decimal.Parse(txtDonGiaNhap.Text)
                );
        }

        private void txtDonGiaNhap_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtDonGiaNhap.Text, out flag_decimal))
            {
                txtDonGiaNhap.Text = decimal.Parse(txtDonGiaNhap.Text).ToString("0,0");
                txtDonGiaNhap.SelectionStart = txtDonGiaNhap.Text.Length;
            }
            else
                txtDonGiaNhap.Text = "0";
        }

        private void txtSoLuong_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtSoLuong.Text, out flag_int))
                txtSoLuong.Text = "0";
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtTongTien.Text, out flag_decimal))
            {
                txtTongTien.Text = decimal.Parse(txtTongTien.Text).ToString("0,0");
                txtTongTien.SelectionStart = txtTongTien.Text.Length;
            }
            else
                txtTongTien.Text = "0";
        }

        private void dgvPhuTungNhap_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            decimal tongtien = 0;

            foreach (DataGridViewRow row in dgvPhuTungNhap.Rows)
            {
                tongtien += decimal.Parse(row.Cells["ThanhTien"].Value.ToString());                
            }

            txtTongTien.Text = tongtien.ToString();
        }

        private void dgvPhuTungNhap_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
            decimal tongtien = 0;

            foreach (DataGridViewRow row in dgvPhuTungNhap.Rows)
            {
                tongtien += decimal.Parse(row.Cells["ThanhTien"].Value.ToString());
            }

            txtTongTien.Text = tongtien.ToString();
        }
    }
}