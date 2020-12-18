using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoCareV2._0.Class;

namespace AutoCareV2._0.UserControls
{
    public partial class UcPhieuThu : UserControl
    {
        public UcPhieuThu()
        {
            InitializeComponent();
        }

        private DataTable dtHDBanHang = new DataTable();
        private DataTable dtChiTietHoaDon = new DataTable();
        private KhDB classdb = new KhDB();
        private DataTable dtChiNhanhNganHang = new DataTable();
        private DataTable dtHopDong = new DataTable();
        private DataTable dtPhieuThu = new DataTable();
        private DataTable dtTTKhachHang = new DataTable();
        private decimal tienDaTra, tongtien = 0m;
        private string loaiPhieu = "";
        private DataTable dtXeMay = new DataTable();
        private DataTable dtXeMay2 = new DataTable();

        // DataRow[] rTenXe; string tenxe;
        private DataTable dtSoPhieu = new DataTable();

        private void load1()
        {
            SqlCommand cmd = new SqlCommand("Select * From HoaDonBanHang Where IdCongTy = @IdCongTy And SoHoaDonBanHang = @SoHoaDonBanHang");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@SoHoaDonBanHang", txtSoHoaDon.Text);
            dtHDBanHang = new DataTable();
            dtHDBanHang = datatabase.getData(cmd);
            if (dtHDBanHang.Rows.Count <= 0)
            {
                MessageBox.Show("Thông tin số hóa đơn không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                //
                cmd = new SqlCommand("Select SoKhung, SoMay, IDKho, DonGia,IDXe From ChiTietHoaDonBan Where IdCongTy = @IdCongTy And SoHoaDonBanHang = @SoHoaDonBanHang");
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@SoHoaDonBanHang", txtSoHoaDon.Text);
                dtChiTietHoaDon = datatabase.getData(cmd);
                dgvXeBan.DataSource = dtChiTietHoaDon;
                cmd = new SqlCommand("Select HoKH + ' ' + TenKH as TenKhachHang, DiaChi From KhachHang WHERE IdCongTy = @IdCongTy and IdKhachHang = @IdKhachHang");
                cmd.Parameters.AddWithValue("@IdKhachHang", Convert.ToString(dtHDBanHang.Rows[0]["IdKhachHang"]));
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                dtTTKhachHang = datatabase.getData(cmd);
                try
                {
                    cboKhachHang.Text = Convert.ToString(dtTTKhachHang.Rows[0]["TenKhachHang"]);
                    txtDiaChi.Text = Convert.ToString(dtTTKhachHang.Rows[0]["DiaChi"]);
                }
                catch { }
                //
                tongtien = 0m;
                foreach (DataRow r in dtChiTietHoaDon.Rows)
                {
                    tongtien += Convert.ToDecimal(r["DonGia"]);
                }
                txtTongTien.Text = tongtien.ToString("0,0");
                decimal tienPhuThu;
                try
                {
                    tienPhuThu = Convert.ToDecimal(dtHDBanHang.Rows[0]["TienPhuThu"].ToString());
                }
                catch { tienPhuThu = 0; }

                txtTienPhuThu.Text = tienPhuThu.ToString("0,0");
                txtTongSoTien.Text = (tongtien + tienPhuThu).ToString("0,0");
                //
                cmd = new SqlCommand("Select * from PhieuThu WHERE IdCongTy = @IdCongTy and SoHoaDon = @SoHoaDon");
                cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                dtPhieuThu = datatabase.getData(cmd);

                //kiem tra phieu thu
                if (dtPhieuThu.Rows.Count > 0)
                {
                    DataRow[] r1 = dtPhieuThu.Select("SoHoaDon = '" + txtSoHoaDon.Text + "'");
                    if (r1.Length > 0)
                    {
                        tienDaTra = 0;
                        foreach (DataRow r in r1)
                        {
                            tienDaTra += Convert.ToDecimal(r["SoTienThu"]);
                        }
                    }
                    txtTienDaTra.Text = tienDaTra.ToString("0,0");
                    loaiPhieu = Convert.ToString(dtPhieuThu.Rows[0]["IdLoaiPhieuThu"]);

                    if (loaiPhieu == "3")
                    {
                        #region Load Thong Tin Neu La Tra Gop

                        cboLoaiPhieuThu.SelectedValue = "3";
                        expanThongTinHD.Visible = true;
                        cmd = new SqlCommand("Select * from HopDongTraGop Where IdCongTy = @IdCongTy And SoHoaDon = @SoHoaDon");
                        cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                        dtHopDong = new DataTable();
                        dtHopDong = datatabase.getData(cmd);
                        //
                        txtSoHopDong.Text = Convert.ToString(dtHopDong.Rows[0]["SoHopDongTraGop"]);
                        dateTimeInputNgaLapHopDong.Value = Convert.ToDateTime(dtHopDong.Rows[0]["NgayLapHopDong"]);
                        txtSoTienTraGop.Text = string.Format("{0:0,0}", dtHopDong.Rows[0]["SoTienTraGop"]);

                        dateTimeInputNgayHenTra.Text = Convert.ToString(dtHopDong.Rows[0]["NgayHenTra"]);
                        txtLaiSuat.Text = Convert.ToString(dtHopDong.Rows[0]["LaiSuat"]);
                        cboThoiHanTraGop.SelectedIndex = Convert.ToInt32(dtHopDong.Rows[0]["ThoiGianTraGop"]) - 1;
                        cboNganHang.SelectedValue = Convert.ToString(dtHopDong.Rows[0]["IDNganHang"]);
                        DataRow[] row = dtChiNhanhNganHang.Select("IdNganHang = '" + Convert.ToString(cboNganHang.SelectedValue) + "'");
                        if (row.Length > 0)
                        {
                            DataTable dt = new DataTable();
                            dt = row.CopyToDataTable<DataRow>();
                            cboChiNhanh.DataSource = dt;
                            cboChiNhanh.DisplayMember = "TenChiNhanh";
                            cboChiNhanh.ValueMember = "IdChiNhanhNganHang";
                        }
                        cboChiNhanh.SelectedValue = Convert.ToString(dtHopDong.Rows[0]["IDChiNhanh"]);
                        txtNguoiDaiDien.Text = Convert.ToString(dtHopDong.Rows[0]["NguoiDaiDien"]);
                        tienDaTra = Convert.ToDecimal(dtHopDong.Rows[0]["SoTienThu"]);
                        //kiem tra tien da tra
                        if (String.IsNullOrEmpty(txtSoTienTraGop.Text))
                            txtSoTienTraGop.Text = "0";
                        if (String.IsNullOrEmpty(txtLaiSuat.Text))
                            txtLaiSuat.Text = "0";
                        double laisuat = Convert.ToDouble(txtLaiSuat.Text) / 100;
                        double kihan = Convert.ToDouble(cboThoiHanTraGop.SelectedIndex + 1);
                        double tong = (laisuat * Convert.ToDouble(txtSoTienTraGop.Text) * kihan) + Convert.ToDouble(txtSoTienTraGop.Text);
                        txtTienTraHangThang.Text = (tong / kihan).ToString("0,0");
                        txtTongNo.Text = tong.ToString("0,0");

                        #endregion Load Thong Tin Neu La Tra Gop
                    }
                    else
                    {
                        //cboLoaiPhieuThu.SelectedValue = Convert.ToString(dtPhieuThu.Rows[0]["IdLoaiPhieuThu"]);
                    }
                }
                else
                {
                    txtTienDaTra.Text = "0";
                }
            }
        }

        private void ResetForm()
        {
            txtDiaChi.Clear();
            txtGhiChu.Clear();
            txtLaiSuat.Clear();
            txtNguoiDaiDien.Clear();

            txtSoHopDong.Clear();
            txtSoTienTraGop.Clear();
            txtTienDaTra.Clear();
            txtTienThu.Clear();
            txtTienTraHangThang.Clear();
            txtTongNo.Clear();
            txtTongTien.Clear();
            txtTongSoTien.Clear();
            dateTimeInputNgayHenTra.Value = DateTime.Now;
            cboThoiHanTraGop.SelectedIndex = -1;
            dateTimeInputNgaLapHopDong.ValueObject = null;
            cboNganHang.SelectedIndex = -1;
            cboChiNhanh.SelectedIndex = -1;
        }

        private void TinhLai()
        {
            decimal n;
            if (decimal.TryParse(txtTienThu.Text, out n) && cboLoaiPhieuThu.SelectedValue.ToString() == "3")
            {
                if (dtPhieuThu.Rows.Count <= 0)
                {
                    if (decimal.TryParse(txtLaiSuat.Text, out n) && cboThoiHanTraGop.SelectedIndex >= 0)
                    {
                        txtSoTienTraGop.Text = (Convert.ToDecimal(txtTongSoTien.Text) - Convert.ToDecimal(txtTienThu.Text)).ToString("0,0");
                        double laisuat = Convert.ToDouble(txtLaiSuat.Text) / 100;
                        double kihan = Convert.ToDouble(cboThoiHanTraGop.SelectedIndex + 1);
                        double tong = (laisuat * Convert.ToDouble(txtSoTienTraGop.Text) * kihan) + Convert.ToDouble(txtSoTienTraGop.Text);
                        txtTienTraHangThang.Text = (tong / kihan).ToString("0,0");
                        txtTongNo.Text = tong.ToString("0,0");
                    }
                }
            }
        }

        private void cboLoaiPhieuThu_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboLoaiPhieuThu.SelectedValue.ToString() == "3")
            {
                expanThongTinHD.Visible = true;
            }
            else
            {
                expanThongTinHD.Visible = false;
            }
        }

        private void cboThoiHanTraGop_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                TinhLai();
            }
            catch { }
        }

        private void txtSoHoaDon_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    ResetForm();
                    load1();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtTienThu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TinhLai();
                decimal tienThu = Convert.ToDecimal(txtTienThu.Text);
                txtTienThu.Text = tienThu.ToString("0,0");
                txtTienThu.SelectionStart = txtTienThu.Text.Length;
            }
            catch { }
        }

        private void chkTraDu_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                //if(chkTraDu.Checked == true)
                //{
                //    txtTienDaTra.Text = txtTongSoTien.Text;
                //}
                //else
                //{
                //    txtTienDaTra.Text = "";
                //}
                decimal tienThu = Convert.ToDecimal(txtTongSoTien.Text) - Convert.ToDecimal(txtTienDaTra.Text);
                txtTienThu.Text = tienThu.ToString("0,0");
            }
            catch
            {
                MessageBox.Show("Thông tin nhập chưa đúng hoặc thiếu! ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
            txtSoHoaDon.Clear();
            dgvXeBan.DataSource = null;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtSoHoaDon.Text == null)
            {
                MessageBox.Show("Số hóa đơn không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dateTimeInputNgayHachToan.ValueObject == null)
            {
                MessageBox.Show("Ngày thu không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dateTimeInputNgayHenTra.Value < DateTime.Now.AddDays(-1))
            {
                MessageBox.Show("Ngày hẹn trả không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (txtSoHoaDon.Text == "0")
            {
                SqlCommand cmd = new SqlCommand();
                string sql = "Insert Into PhieuThu(IdLoaiPhieuThu,SoTienThu,SoHoaDon,NgayHachToan,IdCongTy,IdNhanVien,IdCuaHang,GhiChu)"
                                        + " Values(@IdLoaiPhieuThu, @SoTienThu,@SoHoaDon,@NgayHachToan,@IdCongTy,@IdNhanVien,@IdCuaHang,@GhiChu)";
                cmd.CommandText = sql;
                try
                {
                    cmd.Parameters.AddWithValue("@SoTienThu", Convert.ToDecimal(txtTienThu.Text));
                }
                catch
                {
                    MessageBox.Show("Số tiền thu phải là kiểu số!", "Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTienThu.SelectAll();
                    txtTienThu.Focus();

                    return;
                }
                cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                cmd.Parameters.AddWithValue("@NgayHachToan", dateTimeInputNgayHachToan.Value);
                cmd.Parameters.AddWithValue("@IdLoaiPhieuThu", cboLoaiPhieuThu.SelectedValue);
                cmd.Parameters.AddWithValue("@IdNhanVien", EmployeeInfo.idnhanvien);
                cmd.Parameters.AddWithValue("@IdCuaHang", EmployeeInfo.IdCuaHang);
                cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd.Connection = Class.datatabase.getConnection();

                cmd.Connection.Open();
                cmd.ExecuteNonQuery();
                cmd.Connection.Close();

                MessageBox.Show("Thêm phiếu thu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                #region Xuất hóa đơn thu

                DataTable DLHoaDon = new DataTable();

                DLHoaDon.Clear();

                DLHoaDon.Columns.Add("SoHoaDonBanHang", typeof(string));
                DLHoaDon.Columns.Add("TenCongTy", typeof(string));
                DLHoaDon.Columns.Add("TenKH", typeof(string));
                //DLHoaDon.Columns.Add("NgaySinh", typeof(string));
                //DLHoaDon.Columns.Add("DienThoai", typeof(string));
                DLHoaDon.Columns.Add("DiaChiKhachHang", typeof(string));
                //DLHoaDon.Columns.Add("LoaiXe", typeof(string));
                //DLHoaDon.Columns.Add("MauXe", typeof(string));
                //DLHoaDon.Columns.Add("SoKhung", typeof(string));
                //DLHoaDon.Columns.Add("SoMay", typeof(string));
                DLHoaDon.Columns.Add("GhiChu", typeof(string));
                DLHoaDon.Columns.Add("DonGia", typeof(decimal));
                //DLHoaDon.Columns.Add("TienPhuThu", typeof(decimal));
                DLHoaDon.Columns.Add("TienDaThanhToan", typeof(decimal));

                DataRow dr = DLHoaDon.NewRow();

                dr = DLHoaDon.NewRow();
                dr["SoHoaDonBanHang"] = txtSoHoaDon.Text;
                dr["TenCongTy"] = CompanyInfo.tencongty;
                dr["TenKH"] = cboKhachHang.Text;
                //dr["NgaySinh"] = "tienlm";
                //dr["DienThoai"] = "tienlm";
                dr["DiaChiKhachHang"] = txtDiaChi.Text;
                //dr["LoaiXe"] = "tienlm";
                //dr["MauXe"] = "tienlm";
                //dr["SoKhung"] = "tienlm";
                //dr["SoMay"] = "tienlm";
                dr["GhiChu"] = txtGhiChu.Text;
                dr["DonGia"] = Convert.ToDecimal(txtTienThu.Text);
                //dr["TienPhuThu"] = Convert.ToDecimal(txtTienThu.Text);
                dr["TienDaThanhToan"] = Convert.ToDecimal(txtTienThu.Text);

                DLHoaDon.Rows.Add(dr);

                FrmPhieuBanXe frm = new FrmPhieuBanXe();
                frm.dtHoaDon = DLHoaDon;
                frm.Show();

                #endregion Xuất hóa đơn thu
            }
            else
            {
                SqlCommand cmd5 = new SqlCommand("Select * From HoaDonBanHang Where IdCongTy = @IdCongTy And SoHoaDonBanHang = @SoHoaDonBanHang");
                cmd5.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd5.Parameters.AddWithValue("@SoHoaDonBanHang", txtSoHoaDon.Text);
                dtHDBanHang = new DataTable();
                dtHDBanHang = datatabase.getData(cmd5);
                if (dtHDBanHang.Rows.Count <= 0)
                {
                    MessageBox.Show("Thông tin số hóa đơn không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    cmd5 = new SqlCommand("Select SoKhung, SoMay, IDKho, DonGia,IDXe, GhiChu From ChiTietHoaDonBan Where IdCongTy = @IdCongTy And SoHoaDonBanHang = @SoHoaDonBanHang");
                    cmd5.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    cmd5.Parameters.AddWithValue("@SoHoaDonBanHang", txtSoHoaDon.Text);
                    dtChiTietHoaDon = datatabase.getData(cmd5);
                    dgvXeBan.DataSource = dtChiTietHoaDon;
                    cmd5 = new SqlCommand("Select HoKH + ' ' + TenKH as TenKhachHang, CONVERT(varchar(10), NgaySinh, 103) as NgaySinh, DienThoai, DiaChi From KhachHang WHERE IdCongTy = @IdCongTy and IdKhachHang = @IdKhachHang ");
                    cmd5.Parameters.AddWithValue("@IdKhachHang", Convert.ToString(dtHDBanHang.Rows[0]["IdKhachHang"]));
                    cmd5.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    dtTTKhachHang = datatabase.getData(cmd5);
                    try
                    {
                        cboKhachHang.Text = Convert.ToString(dtTTKhachHang.Rows[0]["TenKhachHang"]);
                        txtDiaChi.Text = Convert.ToString(dtTTKhachHang.Rows[0]["DiaChi"]);
                    }
                    catch { }

                    tongtien = 0m;
                    foreach (DataRow r in dtChiTietHoaDon.Rows)
                    {
                        tongtien += Convert.ToDecimal(r["DonGia"]);
                    }
                    txtTongTien.Text = tongtien.ToString("0,0");

                    cmd5 = new SqlCommand("Select * from PhieuThu WHERE IdCongTy = @IdCongTy and SoHoaDon = @SoHoaDon");
                    cmd5.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                    cmd5.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    dtPhieuThu = datatabase.getData(cmd5);
                    //kiem tra phieu thu
                    if (dtPhieuThu.Rows.Count > 0)
                    {
                        DataRow[] r1 = dtPhieuThu.Select("SoHoaDon = '" + txtSoHoaDon.Text + "'");
                        if (r1.Length > 0)
                        {
                            tienDaTra = 0;
                            foreach (DataRow r in r1)
                            {
                                tienDaTra += Convert.ToDecimal(r["SoTienThu"]);
                            }
                        }
                        txtTienDaTra.Text = tienDaTra.ToString("0,0");
                        loaiPhieu = Convert.ToString(dtPhieuThu.Rows[0]["IdLoaiPhieuThu"]);
                        if (loaiPhieu == "3")//ghi chu
                        {
                            cboLoaiPhieuThu.SelectedValue = "3";
                            expanThongTinHD.Visible = true;
                            cmd5 = new SqlCommand("Select * from HopDongTraGop Where IdCongTy = @IdCongTy And SoHoaDon = @SoHoaDon");
                            cmd5.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                            cmd5.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                            dtHopDong = new DataTable();
                            dtHopDong = datatabase.getData(cmd5);
                            //
                            txtSoHopDong.Text = Convert.ToString(dtHopDong.Rows[0]["SoHopDongTraGop"]);
                            dateTimeInputNgaLapHopDong.Value = Convert.ToDateTime(dtHopDong.Rows[0]["NgayLapHopDong"]);
                            txtSoTienTraGop.Text = string.Format("{0:0,0}", dtHopDong.Rows[0]["SoTienTraGop"]);

                            dateTimeInputNgayHenTra.Value = Convert.ToDateTime(dtHopDong.Rows[0]["NgayHenTra"]);
                            txtLaiSuat.Text = Convert.ToString(dtHopDong.Rows[0]["LaiSuat"]);
                            cboThoiHanTraGop.SelectedIndex = Convert.ToInt32(dtHopDong.Rows[0]["ThoiGianTraGop"]) - 1;
                            cboNganHang.SelectedValue = Convert.ToString(dtHopDong.Rows[0]["IDNganHang"]);
                            DataRow[] row = dtChiNhanhNganHang.Select("IdNganHang = '" + Convert.ToString(cboNganHang.SelectedValue) + "'");
                            if (row.Length > 0)
                            {
                                DataTable dt = new DataTable();
                                dt = row.CopyToDataTable<DataRow>();
                                cboChiNhanh.DataSource = dt;
                                cboChiNhanh.DisplayMember = "TenChiNhanh";
                                cboChiNhanh.ValueMember = "IdChiNhanhNganHang";
                            }

                            cboChiNhanh.SelectedValue = Convert.ToString(dtHopDong.Rows[0]["IDChiNhanh"]);
                            txtNguoiDaiDien.Text = Convert.ToString(dtHopDong.Rows[0]["NguoiDaiDien"]);
                            tienDaTra = Convert.ToDecimal(dtHopDong.Rows[0]["SoTienThu"]);

                            //kiem tra tien da tra
                            if (String.IsNullOrEmpty(txtSoTienTraGop.Text))
                                txtSoTienTraGop.Text = "0";
                            if (String.IsNullOrEmpty(txtLaiSuat.Text))
                                txtLaiSuat.Text = "0";
                            double laisuat = Convert.ToDouble(txtLaiSuat.Text) / 100;
                            double kihan = Convert.ToDouble(cboThoiHanTraGop.SelectedIndex + 1);
                            double tong = (laisuat * Convert.ToDouble(txtSoTienTraGop.Text) * kihan) + Convert.ToDouble(txtSoTienTraGop.Text);
                            txtTienTraHangThang.Text = (tong / kihan).ToString("0,0");
                            txtTongNo.Text = tong.ToString("0,0");
                        }
                        else
                        {
                            //cboLoaiPhieuThu.SelectedValue = Convert.ToString(dtPhieuThu.Rows[0]["IdLoaiPhieuThu"]);
                            //nếu loại phiếu thu khác trả góp
                        }
                    }// neu chua co phieu thu
                    else
                    {
                        txtTienDaTra.Text = "0";
                    }
                    decimal kt;
                    if (!decimal.TryParse(txtTienThu.Text, out kt))
                    {
                        MessageBox.Show("Số tiền thu phải là số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    SqlCommand cmd1 = new SqlCommand("Select * from PhieuThu WHERE IdCongTy = @IdCongTy and SoHoaDon = @SoHoaDon");
                    cmd1.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                    cmd1.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    dtPhieuThu = datatabase.getData(cmd1);

                    //kiem tra phieu thu
                    if (dtPhieuThu.Rows.Count <= 0)
                    {
                        #region Khởi tạo phiếu thu

                        decimal tien = (Convert.ToDecimal(txtTongTien.Text) - Convert.ToDecimal(txtTienThu.Text));
                        if (tien < 0)
                        {
                            MessageBox.Show("Số tiền trả lớn hơn tổng tiền.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        if (cboLoaiPhieuThu.SelectedValue.ToString() == "3")
                        {
                            #region Trả Góp

                            if (String.IsNullOrEmpty(txtSoHopDong.Text) || dateTimeInputNgaLapHopDong.ValueObject == null || String.IsNullOrEmpty(txtLaiSuat.Text) || String.IsNullOrEmpty(cboThoiHanTraGop.Text) || String.IsNullOrEmpty(cboNganHang.SelectedValue.ToString()))
                            {
                                MessageBox.Show("Thông tin hợp đồng chưa cung cấp đủ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            SqlCommand cmd = new SqlCommand("Select * from HopDongTraGop Where IdCongTy = @IdCongTy And SoHoaDon = @SoHoaDon And SoHopDongTraGop = @SoHopDong");
                            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                            cmd.Parameters.AddWithValue("@SoHopDong", txtSoHopDong.Text);
                            if (datatabase.getData(cmd).Rows.Count > 0)
                            {
                                MessageBox.Show("Số hợp đồng đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            // bat dau thuc hien cau lenh
                            cmd = new SqlCommand("Insert Into HopDongTraGop (SoHopDongTraGop, IdCongTy, ThoiGianTraGop, LaiSuat, NgayHenTra, IDNganHang, IDChiNhanh, NguoiDaiDien, SoHoaDon, NgayLapHopDong, SoTienTraGop, SoTienThu, IdCuaHang, IdNhanVien)"
                                                                    + " Values(@SoHopDongTraGop, @IdCongTy, @ThoiGianTraGop, @LaiSuat, @NgayHenTra,@IdNganHang,@IdChiNhanh,@NguoiDaiDien,@SoHoaDon,@NgayLapHopDong,@SoTienTraGop,@SoTienThu,@IdCuaHang,@IdNhanVien)");
                            cmd.Parameters.AddWithValue("@SoHopDongTraGop", txtSoHopDong.Text);
                            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@ThoiGianTraGop", cboThoiHanTraGop.SelectedIndex + 1);
                            cmd.Parameters.AddWithValue("@LaiSuat", txtLaiSuat.Text);
                            cmd.Parameters.AddWithValue("@NgayHenTra", dateTimeInputNgayHenTra.Value);
                            cmd.Parameters.AddWithValue("@IdNganHang", cboNganHang.SelectedValue);
                            cmd.Parameters.AddWithValue("@IdChiNhanh", cboChiNhanh.SelectedValue);
                            cmd.Parameters.AddWithValue("@NguoiDaiDien", txtNguoiDaiDien.Text);
                            cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                            cmd.Parameters.AddWithValue("@NgayLapHopDong", dateTimeInputNgaLapHopDong.Value);
                            cmd.Parameters.AddWithValue("@SoTienTraGop", Convert.ToDecimal(txtSoTienTraGop.Text));
                            cmd.Parameters.AddWithValue("@SoTienThu", Convert.ToDecimal(txtTienThu.Text));
                            cmd.Parameters.AddWithValue("@IdCuaHang", EmployeeInfo.IdCuaHang);
                            cmd.Parameters.AddWithValue("@IdNhanVien", EmployeeInfo.idnhanvien);
                            cmd.Connection = Class.datatabase.getConnection();
                            cmd.Connection.Open();
                            SqlTransaction tran = cmd.Connection.BeginTransaction();
                            cmd.Transaction = tran;
                            try
                            {
                                cmd.ExecuteNonQuery();
                                string sql = "Insert Into PhieuThu(IdLoaiPhieuThu,SoTienThu,SoHoaDon,SoHopDong,NgayHachToan,IdCongTy,IdNhanVien,IdCuaHang,GhiChu)"
                                                        + " Values(@IdLoaiPhieuThu, @SoTienThu,@SoHoaDon,@SoHopDong,@NgayHachToan,@IdCongTy,@IdNhanVien,@IdCuaHang,@GhiChu)";
                                cmd.CommandText = sql;
                                cmd.Parameters.AddWithValue("@IdLoaiPhieuThu", "3");
                                cmd.Parameters.AddWithValue("@SoHopDong", txtSoHopDong.Text);
                                cmd.Parameters.AddWithValue("@NgayHachToan", dateTimeInputNgayHachToan.Value);
                                cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                                cmd.ExecuteNonQuery();

                                tran.Commit();
                                MessageBox.Show("Thêm hợp đồng và phiếu thu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtTienDaTra.Text = txtTienThu.Text;
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();
                                MessageBox.Show("Thêm hợp đồng và phiếu thu thất bại." + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally { cmd.Connection.Close(); }
                            //ket thuc thuc hien cau lenh

                            #endregion Trả Góp
                        }
                        else
                        {
                            #region Not Trả Góp

                            SqlCommand cmd = new SqlCommand();
                            string sql = "Insert Into PhieuThu(IdLoaiPhieuThu,SoTienThu,SoHoaDon,NgayHachToan,IdCongTy,IdNhanVien,IdCuaHang,GhiChu)"
                                                    + " Values(@IdLoaiPhieuThu, @SoTienThu,@SoHoaDon,@NgayHachToan,@IdCongTy,@IdNhanVien,@IdCuaHang,@GhiChu)";
                            cmd.CommandText = sql;
                            cmd.Parameters.AddWithValue("@SoTienThu", Convert.ToDecimal(txtTienThu.Text));
                            cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                            cmd.Parameters.AddWithValue("@NgayHachToan", dateTimeInputNgayHachToan.Value);
                            cmd.Parameters.AddWithValue("@IdLoaiPhieuThu", cboLoaiPhieuThu.SelectedValue);
                            cmd.Parameters.AddWithValue("@IdNhanVien", EmployeeInfo.idnhanvien);
                            cmd.Parameters.AddWithValue("@IdCuaHang", EmployeeInfo.IdCuaHang);
                            cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                            cmd.Connection = Class.datatabase.getConnection();

                            cmd.Connection.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Connection.Close();

                            MessageBox.Show("Thêm phiếu thu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            txtTienDaTra.Text = txtTienThu.Text;

                            #endregion Not Trả Góp
                        }

                        #endregion Khởi tạo phiếu thu
                    }
                    else
                    {
                        #region Các lần thu tiếp theo

                        try
                        {
                            //loaiPhieu = Convert.ToString(dtPhieuThu.Rows[0]["IdLoaiPhieuThu"]);
                            DataRow[] r1 = dtPhieuThu.Select("SoHoaDon = '" + txtSoHoaDon.Text + "'");
                            if (r1.Length > 0)
                            {
                                tienDaTra = 0;
                                foreach (DataRow r in r1)
                                {
                                    tienDaTra += Convert.ToDecimal(r["SoTienThu"]);
                                }
                            }
                            txtTienDaTra.Text = tienDaTra.ToString("0,0");

                            //cboLoaiPhieuThu.SelectedValue = loaiPhieu;
                            decimal tien = tienDaTra + Convert.ToDecimal(txtTienThu.Text);
                            if (tien > tongtien)
                            {
                                MessageBox.Show("Tổng tiền trả lớn hơn tổng tiền cần thu.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            if (loaiPhieu == "3")
                            {
                                #region Trả Góp

                                SqlCommand cmd = new SqlCommand("Insert Into PhieuThu(IdLoaiPhieuThu,SoTienThu,SoHoaDon,SoHopDong,NgayHachToan,IdCongTy,IdNhanVien,IdCuaHang,GhiChu)"
                                                                            + " Values(@IdLoaiPhieuThu, @SoTienThu,@SoHoaDon,@SoHopDong,@NgayHachToan,@IdCongTy,@IdNhanVien,@IdCuaHang,@GhiChu)");
                                cmd.Parameters.AddWithValue("@IdLoaiPhieuThu", loaiPhieu);
                                cmd.Parameters.AddWithValue("@SoHopDong", txtSoHopDong.Text);
                                cmd.Parameters.AddWithValue("@IdcongTy", CompanyInfo.idcongty);
                                cmd.Parameters.AddWithValue("@SoTienThu", Convert.ToDecimal(txtTienThu.Text));
                                cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                                cmd.Parameters.AddWithValue("@NgayHachToan", dateTimeInputNgayHachToan.Value);
                                cmd.Parameters.AddWithValue("@IdNhanVien", EmployeeInfo.idnhanvien);
                                cmd.Parameters.AddWithValue("@IdCuaHang", EmployeeInfo.IdCuaHang);
                                cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                                cmd.Connection = datatabase.getConnection();
                                cmd.Connection.Open();
                                try
                                {
                                    if (cmd.ExecuteNonQuery() > 0)
                                    {
                                        txtTienDaTra.Text = (Convert.ToDecimal(txtTienDaTra.Text) + Convert.ToDecimal(txtTienThu.Text)).ToString("0,0");
                                        MessageBox.Show("Thêm phiếu thu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Thêm phiếu thu thất bại." + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                                #endregion Trả Góp
                            }
                            else
                            {
                                #region Not Trả Góp

                                SqlCommand cmd = new SqlCommand("Insert Into PhieuThu(IdLoaiPhieuThu,SoTienThu,SoHoaDon,NgayHachToan,IdCongTy,IdNhanVien,IdCuaHang,GhiChu)"
                                                                            + " Values(@IdLoaiPhieuThu, @SoTienThu,@SoHoaDon,@NgayHachToan,@IdCongTy,@IdNhanVien,@IdCuaHang,@GhiChu)");
                                cmd.Parameters.AddWithValue("@IdLoaiPhieuThu", cboLoaiPhieuThu.SelectedValue);
                                cmd.Parameters.AddWithValue("@IdcongTy", CompanyInfo.idcongty);
                                cmd.Parameters.AddWithValue("@SoTienThu", txtTienThu.Text);
                                cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                                cmd.Parameters.AddWithValue("@NgayHachToan", dateTimeInputNgayHachToan.Value);
                                cmd.Parameters.AddWithValue("@IdNhanVien", EmployeeInfo.idnhanvien);
                                cmd.Parameters.AddWithValue("@IdCuaHang", EmployeeInfo.IdCuaHang);
                                cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                                if (datatabase.ExcuteNonQuery(cmd) > 0)
                                {
                                    txtTienDaTra.Text = (Convert.ToDecimal(txtTienDaTra.Text) + Convert.ToDecimal(txtTienThu.Text)).ToString("0,0");
                                    MessageBox.Show("Thêm phiếu thu thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                    MessageBox.Show("Thêm phiếu thu thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                #endregion Not Trả Góp
                            }
                        }
                        catch (Exception ex) { MessageBox.Show("Lỗi." + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error); }

                        #endregion Các lần thu tiếp theo
                    }
                }
            }

            //Xuất hóa đơn bán lẻ
            if (cboLoaiPhieuThu.SelectedValue.ToString() == "1")
            {
                if (dgvXeBan.Rows.Count > 0)
                {
                    for (int i = 0; i < dgvXeBan.Rows.Count - 1; i++)
                    {
                        #region Xuất hóa đơn thu

                        try
                        {
                            DataTable DLHoaDon = new DataTable();

                            DLHoaDon.Clear();

                            DLHoaDon.Columns.Add("SoHoaDonBanHang", typeof(string));
                            DLHoaDon.Columns.Add("TenCongTy", typeof(string));
                            DLHoaDon.Columns.Add("TenKH", typeof(string));
                            DLHoaDon.Columns.Add("NgaySinh", typeof(string));
                            DLHoaDon.Columns.Add("DienThoai", typeof(string));
                            DLHoaDon.Columns.Add("DiaChiKhachHang", typeof(string));
                            DLHoaDon.Columns.Add("LoaiXe", typeof(string));
                            //DLHoaDon.Columns.Add("MauXe", typeof(string));
                            DLHoaDon.Columns.Add("SoKhung", typeof(string));
                            DLHoaDon.Columns.Add("SoMay", typeof(string));
                            DLHoaDon.Columns.Add("GhiChu", typeof(string));
                            DLHoaDon.Columns.Add("DonGia", typeof(decimal));
                            //DLHoaDon.Columns.Add("TienPhuThu", typeof(decimal));
                            DLHoaDon.Columns.Add("TienDaThanhToan", typeof(decimal));

                            DataRow dr = DLHoaDon.NewRow();

                            SqlCommand cmd = new SqlCommand("Select IdXe, IdXe + ' - ' + TenXe as TenXe,HangSanXuat,DVT From XeMay WHere IdCongTy = @IdCongTy AND IdXe=@IdXe");
                            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdXe", dgvXeBan.Rows[i].Cells["TenXe1"].Value.ToString());
                            dtXeMay2 = new DataTable();
                            dtXeMay2 = datatabase.getData(cmd);

                            dr = DLHoaDon.NewRow();
                            dr["SoHoaDonBanHang"] = txtSoHoaDon.Text;
                            dr["TenCongTy"] = CompanyInfo.tencongty;
                            dr["TenKH"] = cboKhachHang.Text;
                            try
                            {
                                dr["NgaySinh"] = Convert.ToString(dtTTKhachHang.Rows[0]["NgaySinh"]);
                                dr["DienThoai"] = Convert.ToString(dtTTKhachHang.Rows[0]["DienThoai"]);
                            }
                            catch
                            {
                                dr["NgaySinh"] = "";
                                dr["DienThoai"] = "";
                            }
                            dr["DiaChiKhachHang"] = txtDiaChi.Text;
                            dr["LoaiXe"] = Convert.ToString(dtXeMay2.Rows[0]["TenXe"]);
                            //dr["MauXe"] = "tienlm";
                            dr["SoKhung"] = dgvXeBan.Rows[i].Cells["SoKhung"].Value.ToString();
                            dr["SoMay"] = dgvXeBan.Rows[i].Cells["SoMay"].Value.ToString();
                            dr["GhiChu"] = Convert.ToString(dtChiTietHoaDon.Rows[0]["GhiChu"]);
                            dr["DonGia"] = Convert.ToDecimal(dgvXeBan.Rows[i].Cells["GiaBan"].Value.ToString());
                            //dr["TienPhuThu"] = Convert.ToDecimal(txtTienThu.Text);
                            dr["TienDaThanhToan"] = Convert.ToDecimal(dgvXeBan.Rows[i].Cells["GiaBan"].Value.ToString());

                            DLHoaDon.Rows.Add(dr);

                            FrmPhieuBanXe frm = new FrmPhieuBanXe();
                            frm.dtHoaDon = DLHoaDon;
                            frm.Show();
                        }
                        catch { }

                        #endregion Xuất hóa đơn thu
                    }
                }
            }

            //Xuat hoa don ban buon
            if (cboLoaiPhieuThu.SelectedValue.ToString() == "2")
            {
                DialogResult chon = MessageBox.Show("Bạn có muốn xuất hóa đơn không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (chon == DialogResult.Yes)
                {
                    if (dgvXeBan.Rows.Count > 0)
                    {
                        for (int i = 0; i < dgvXeBan.Rows.Count - 1; i++)
                        {
                            #region Xuất hóa đơn thu

                            try
                            {
                                DataTable DLHoaDon = new DataTable();

                                DLHoaDon.Clear();

                                DLHoaDon.Columns.Add("SoHoaDonBanHang", typeof(string));
                                DLHoaDon.Columns.Add("TenCongTy", typeof(string));
                                DLHoaDon.Columns.Add("TenKH", typeof(string));
                                DLHoaDon.Columns.Add("NgaySinh", typeof(string));
                                DLHoaDon.Columns.Add("DienThoai", typeof(string));
                                DLHoaDon.Columns.Add("DiaChiKhachHang", typeof(string));
                                DLHoaDon.Columns.Add("LoaiXe", typeof(string));
                                //DLHoaDon.Columns.Add("MauXe", typeof(string));
                                DLHoaDon.Columns.Add("SoKhung", typeof(string));
                                DLHoaDon.Columns.Add("SoMay", typeof(string));
                                DLHoaDon.Columns.Add("GhiChu", typeof(string));
                                DLHoaDon.Columns.Add("DonGia", typeof(decimal));
                                //DLHoaDon.Columns.Add("TienPhuThu", typeof(decimal));
                                DLHoaDon.Columns.Add("TienDaThanhToan", typeof(decimal));

                                DataRow dr = DLHoaDon.NewRow();

                                //Lay ten xe may
                                SqlCommand cmd = new SqlCommand("Select IdXe, IdXe + ' - ' + TenXe as TenXe,HangSanXuat,DVT From XeMay WHere IdCongTy = @IdCongTy AND IdXe=@IdXe");
                                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                cmd.Parameters.AddWithValue("@IdXe", dgvXeBan.Rows[i].Cells["TenXe1"].Value.ToString());
                                dtXeMay2 = new DataTable();
                                dtXeMay2 = datatabase.getData(cmd);

                                dr = DLHoaDon.NewRow();
                                dr["SoHoaDonBanHang"] = txtSoHoaDon.Text;
                                dr["TenCongTy"] = CompanyInfo.tencongty;
                                dr["TenKH"] = cboKhachHang.Text;
                                try
                                {
                                    dr["NgaySinh"] = Convert.ToString(dtTTKhachHang.Rows[0]["NgaySinh"]);
                                    dr["DienThoai"] = Convert.ToString(dtTTKhachHang.Rows[0]["DienThoai"]);
                                }
                                catch
                                {
                                    dr["NgaySinh"] = "";
                                    dr["DienThoai"] = "";
                                }
                                dr["DiaChiKhachHang"] = txtDiaChi.Text;
                                dr["LoaiXe"] = Convert.ToString(dtXeMay2.Rows[0]["TenXe"]);
                                //dr["MauXe"] = "tienlm";
                                dr["SoKhung"] = dgvXeBan.Rows[i].Cells["SoKhung"].Value.ToString();
                                dr["SoMay"] = dgvXeBan.Rows[i].Cells["SoMay"].Value.ToString();
                                dr["GhiChu"] = Convert.ToString(dtChiTietHoaDon.Rows[0]["GhiChu"]);
                                dr["DonGia"] = Convert.ToDecimal(dgvXeBan.Rows[i].Cells["GiaBan"].Value.ToString());
                                //dr["TienPhuThu"] = Convert.ToDecimal(txtTienThu.Text);
                                dr["TienDaThanhToan"] = Convert.ToDecimal(dgvXeBan.Rows[i].Cells["GiaBan"].Value.ToString());

                                DLHoaDon.Rows.Add(dr);

                                FrmPhieuBanXe frm = new FrmPhieuBanXe();
                                frm.dtHoaDon = DLHoaDon;
                                frm.Show();
                            }
                            catch { }

                            #endregion Xuất hóa đơn thu
                        }
                    }
                }
            }
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTienDaTra.Text != "")
                {
                    Class.Phieu.soPhieuBanXe = txtSoHoaDon.Text.Trim();
                    Class.Phieu.tienDaThanhToan = Convert.ToDecimal(txtTienDaTra.Text);
                    FrmPhieuBanXe frm = new FrmPhieuBanXe();
                    frm.Show();
                }
                else
                {
                    MessageBox.Show("Chưa nhập số tiền thanh toán", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Thông tin nhập chưa đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
        }

        private void txtLaiSuat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TinhLai();
            }
            catch (Exception ex) { MessageBox.Show("Bạn chưa điền số tiền thu." + ex.Message); }
        }

        private void txtSoHopDong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                frmChiTietHopDong frm = new frmChiTietHopDong();
                frm.soHopDong = txtSoHopDong.Text;
                frm.ShowDialog();
            }
        }

        private void UcPhieuThu_Load(object sender, EventArgs e)
        {
            txtTongTien.Text = "0";
            txtTongSoTien.Text = "0";
            txtTienThu.Text = "0";
            txtTienDaTra.Text = "0";
            txtTienPhuThu.Text = "0";

            SqlCommand cmd = new SqlCommand("Select SoHoaDonBanHang From HoaDonBanHang Where IdCongTy = @IdCongTy And IdCuaHang = @IdCuaHang");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@IdCuaHang", EmployeeInfo.IdCuaHang);
            cmd.Connection = datatabase.getConnection();
            AutoCompleteStringCollection source = new AutoCompleteStringCollection();
            txtSoHoaDon.AutoCompleteCustomSource = source;
            cmd.Connection.Open();
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                source.Add(reader["SoHoaDonBanHang"].ToString());
            }
            cmd.Connection.Close();
            txtSoHoaDon.AutoCompleteCustomSource = source;
            this.Kho.DataSource = classdb.LoadTenKho();
            this.Kho.DisplayMember = "TenKho";
            this.Kho.ValueMember = "IdKho";
            dtXeMay = classdb.LayTenXe();
            this.TenXe1.DataSource = dtXeMay;
            this.TenXe1.DisplayMember = "TenXe";
            this.TenXe1.ValueMember = "IdXe";
            this.cboNganHang.DataSource = classdb.LayDanhSachNganHang();
            cboNganHang.DisplayMember = "TenNganHang";
            cboNganHang.ValueMember = "IdNganHang";
            dtChiNhanhNganHang = classdb.LayDanhSachChiNhanhNganHang();
            this.cboLoaiPhieuThu.DataSource = classdb.LoaiPhieuThu();
            this.cboLoaiPhieuThu.DisplayMember = "TenLoaiPhieuThu";
            this.cboLoaiPhieuThu.ValueMember = "IdLoaiPhieuThu";
            //cboKhachHang.DataSource = classdb.LayDanhSachKhachHang();
            //cboKhachHang.ValueMember = "
            dateTimeInputNgaLapHopDong.Value = DateTime.Now;
            dateTimeInputNgayHachToan.Value = DateTime.Now;
            dateTimeInputNgayHenTra.Value = DateTime.Now;
        }

        private void cboNganHang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            DataRow[] row = dtChiNhanhNganHang.Select("IdNganHang = '" + Convert.ToString(cboNganHang.SelectedValue) + "'");
            if (row.Length > 0)
            {
                DataTable dt = new DataTable();
                dt = row.CopyToDataTable<DataRow>();
                cboChiNhanh.DataSource = dt;
                cboChiNhanh.DisplayMember = "TenChiNhanh";
                cboChiNhanh.ValueMember = "IdChiNhanhNganHang";
            }
            else
                cboChiNhanh.DataSource = null;
        }

        private void txtTienDaTra_TextChanged(object sender, EventArgs e)
        {
        }
    }
}