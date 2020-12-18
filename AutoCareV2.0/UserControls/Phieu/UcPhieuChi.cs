using AutoCareV2._0.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls
{
    public partial class UcPhieuChi : UserControl
    {
        private KhDB classdb = new KhDB();
        private DocTien doctien = new DocTien();
        private decimal tien;
        private int RowIndex_Phieu = -1;

        public UcPhieuChi()
        {
            InitializeComponent();
        }

        private void txtTienChi_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal tientra;
                tientra = Convert.ToDecimal(txtTienChi.Text);
                txtTienChi.Text = tientra.ToString("0,0");
                txtTienChi.SelectionStart = txtTienChi.Text.Length;
            }
            catch { }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTienChi.Clear();
            txtNguoiNhan.Clear();
            txtDiaChi.Clear();
            dateTimeInputNgayHachToan.ValueObject = null;
            txtSoHoaDon.Clear();
            txtLyDoChi.Clear();
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtSoHoaDon.Text == null)
                {
                    MessageBox.Show("Số hóa đơn không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dateTimeInputNgayHachToan.ValueObject == null)
                {
                    MessageBox.Show("Bạn chưa nhập thời gian tạo phiếu chi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!decimal.TryParse(txtTienChi.Text, out tien))
                {
                    MessageBox.Show("Số tiền phải là số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SqlCommand cmd = new SqlCommand("Insert InTo PhieuChi(IdLoaiPhieuChi,SoTienChi,NgayHachToan,IdCongTy,NguoiNhan,IdNhaCungCap,IdNhanVien,NoiDung,IdCuaHang,SoHoaDon)Values(@IdLoaiPhieuChi,@SoTienChi,@NgayHachToan,@IDCongTy,@NguoiNhan,@IdNhaCungCap,@IdNhanVien,@NoiDung,@IdCuaHang,@SoHoaDon)");
                cmd.Parameters.AddWithValue("@IdLoaiPhieuChi", cboLoaiPhieuChi.SelectedValue);
                cmd.Parameters.AddWithValue("@SoTienChi", tien);
                cmd.Parameters.AddWithValue("@NgayHachToan", dateTimeInputNgayHachToan.Value);
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@NguoiNhan", txtNguoiNhan.Text);

                if (cboKhachHang.SelectedValue.ToString() != "0")
                    cmd.Parameters.AddWithValue("@IdNhaCungCap", DBNull.Value);
                else
                    cmd.Parameters.AddWithValue("@IdNhaCungCap", cboKhachHang.SelectedValue);
                cmd.Parameters.AddWithValue("@IdNhanVien", EmployeeInfo.idnhanvien);
                cmd.Parameters.AddWithValue("@NoiDung", txtLyDoChi.Text);
                cmd.Parameters.AddWithValue("@IdCuaHang", EmployeeInfo.IdCuaHang);
                cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                if (datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Thêm phiếu chi thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Thêm phiếu chi thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                //Xuất hóa đơn
                DialogResult chon = MessageBox.Show("Bạn có muốn xuất hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (chon == DialogResult.Yes)
                {
                    #region Xuất phiếu chi

                    DataTable DLPhieuChi = new DataTable();

                    DLPhieuChi.Clear();

                    DLPhieuChi.Columns.Add("SoPhieuChi", typeof(string));
                    DLPhieuChi.Columns.Add("NguoiNhan", typeof(string));
                    DLPhieuChi.Columns.Add("DiaChi", typeof(string));
                    DLPhieuChi.Columns.Add("LyDoChi", typeof(string));
                    DLPhieuChi.Columns.Add("TongTien", typeof(decimal));
                    DLPhieuChi.Columns.Add("DocTien", typeof(string));

                    DataRow dr = DLPhieuChi.NewRow();
                    dr = DLPhieuChi.NewRow();

                    dr["SoPhieuChi"] = txtSoHoaDon.Text;
                    dr["NguoiNhan"] = cboKhachHang.Text;
                    dr["DiaChi"] = txtDiaChi.Text;
                    dr["LyDoChi"] = txtLyDoChi.Text;
                    dr["TongTien"] = Convert.ToDecimal(txtTienChi.Text);
                    dr["DocTien"] = doctien.ChuyenSo(tien.ToString("0"));

                    DLPhieuChi.Rows.Add(dr);

                    frmPhieuChi frm = new frmPhieuChi();
                    frm.dtPhieuChi = DLPhieuChi;
                    frm.Show();

                    #endregion Xuất phiếu chi
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Handles the Load event of the UcPhieuChi control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void UcPhieuChi_Load(object sender, EventArgs e)
        {
            dateTimeInputNgayHachToan.Value = DateTime.Now;
            cboLoaiPhieuChi.DataSource = classdb.LoaiPhieuChi();
            cboLoaiPhieuChi.ValueMember = "IdLoaiPhieuChi";
            cboLoaiPhieuChi.DisplayMember = "TenLoaiPhieuChi";

            LoadNhaCungCap();
            LayDanhSachPhieuChi();
        }

        private void LayDanhSachPhieuChi()
        {
            string IdCongty = Class.CompanyInfo.idcongty;
            string DateFrom = DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd") + " 00:00:00";
            string DateTo = DateTime.Now.ToString("yyyy-MM-dd") + " 23:59:59";

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "sp_LayDanhSachPhieuChi";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdCongTy", IdCongty);
                cmd.Parameters.AddWithValue("@NgayTaoTu", DateFrom);
                cmd.Parameters.AddWithValue("@NgayTaoDen", DateTo);

                DataTable tblResult = datatabase.getData(cmd);
                grvDanhSachPhieuChi.DataSource = tblResult;
            }
        }

        private void LoadNhaCungCap()
        {
            DataTable tblNhaCungCap = classdb.NhaCungCap();
            DataRow drNhaCungCap = tblNhaCungCap.NewRow();
            drNhaCungCap["TenNhaCungCap"] = "---";
            drNhaCungCap["IdNhaCungCap"] = 0;
            tblNhaCungCap.Rows.InsertAt(drNhaCungCap, 0);

            cboKhachHang.ValueMember = "IdNhaCungCap";
            cboKhachHang.DisplayMember = "TenNhaCungCap";
            cboKhachHang.DataSource = tblNhaCungCap;
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            frmQuanLyNhaCungCap frmQlNcc = new frmQuanLyNhaCungCap();
            frmQlNcc.FormClosed += frmQlNcc_FormClosed;
            frmQlNcc.ShowDialog();
        }

        private void frmQlNcc_FormClosed(object sender, FormClosedEventArgs e)
        {
            LoadNhaCungCap();
        }

        private void grvDanhSachPhieuChi_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                grvDanhSachPhieuChi.ContextMenuStrip = contextMenuPhieuChi;

                try
                {
                    RowIndex_Phieu = grvDanhSachPhieuChi.CurrentRow.Index;
                }
                catch (Exception ex)
                {
                    RowIndex_Phieu = -1;
                }
            }
            else
            {
                grvDanhSachPhieuChi.ContextMenuStrip = null;
                RowIndex_Phieu = -1;
            }
        }

        private void InPhieuChiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RowIndex_Phieu != -1)
            {
                try
                {
                    #region Xuất phiếu chi

                    DataTable DLPhieuChi = new DataTable();
                    string SoPhieuChi = grvDanhSachPhieuChi.Rows[RowIndex_Phieu].Cells["SoPhieuChi"].Value.ToString();

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = "sp_PhieuChi_GetById";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@SoPhieuChi", SoPhieuChi);

                        DLPhieuChi = datatabase.getData(cmd);
                    }

                    if (DLPhieuChi.Rows.Count > 0)
                    {
                        DLPhieuChi.Rows[0]["DocTien"] = doctien.ChuyenSo(decimal.Parse(DLPhieuChi.Rows[0]["TongTien"].ToString()).ToString("0"));
                    }

                    frmPhieuChi frm = new frmPhieuChi();
                    frm.dtPhieuChi = DLPhieuChi;
                    frm.Show();

                    #endregion Xuất phiếu chi
                }
                catch { }
                finally { }
            }
        }

        private void XoaPhieuChiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (RowIndex_Phieu != -1)
            {
                if (Class.EmployeeInfo.Quyen.ToLower() != "qtv")
                {
                    MessageBox.Show("Bạn không có quyền xóa phiếu chi!\nVui lòng kiểm tra lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }

                if (MessageBox.Show("Bạn có muốn xóa phiếu chi đã chọn?", "Xóa phiếu chi?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    try
                    {
                        DataTable DLPhieuChi = new DataTable();
                        string SoPhieuChi = grvDanhSachPhieuChi.Rows[RowIndex_Phieu].Cells["SoPhieuChi"].Value.ToString();

                        using (SqlCommand cmd = new SqlCommand())
                        {
                            cmd.CommandText = "sp_PhieuChi_Delete";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@SoPhieuChi", SoPhieuChi);

                            int j = datatabase.ExcuteNonQuery(cmd);

                            LayDanhSachPhieuChi();
                        }
                    }
                    catch { }
                }
            }
        }
    }
}