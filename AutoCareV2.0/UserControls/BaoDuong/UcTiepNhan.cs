using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls.BaoDuong
{
    public partial class UcTiepNhan : UserControl
    {
        private string idKhachHang = "";
        private string idBaoDuong3 = "";
        private Class.ChangeOilByKM ChangeOilKM = new Class.ChangeOilByKM();

        /// <summary>
        ///
        /// </summary>
        public UcTiepNhan()
        {
            InitializeComponent();
            this.VerticalScroll.Visible = true;
            this.HorizontalScroll.Visible = true;
            this.HorizontalScroll.Enabled = true;
            LoadMaintenanceTypes();
        }

        private void LoadMaintenanceTypes()
        {
            Dictionary<string, string> MaintenanceTypes = new Dictionary<string, string>();
            MaintenanceTypes.Add(Keywords.MaintenanceTypes.LightMaintenance, "Bảo dưỡng nhẹ");
            MaintenanceTypes.Add(Keywords.MaintenanceTypes.HevyMaintenance, "Bảo dưỡng nặng");
            cbbMaintenanceTypes.DisplayMember = "Value";
            cbbMaintenanceTypes.ValueMember = "Key";
            cbbMaintenanceTypes.DataSource = new BindingSource(MaintenanceTypes, null);
        }

        // public delegate void SEND();
        // delegate tham chiếu tới 1 hàm kiểu void tênham(string s);
        // public SEND sender; //1 biến kiểu SEND
        private void LoadXeDangBaoDuong()
        {
            string sql = "Select * from LichSuBaoDuongXeTam Where IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
            Class.tblBaoDuong.lsBaoduongxetam = Class.datatabase.getData(cmd);

            //dgvDsXeDangBaoDuong1.DataSource = dtxeBaoDuong;
            //dgvDsXeDangBaoDuong2.DataSource = dtxeBaoDuong;
            //dgvDsXeDangBaoDuong3.DataSource = dtxeBaoDuong;
        }

        #region "AutoComplete"

        private void Auto_BienSo()
        {
            AutoCompleteStringCollection BienSo = new AutoCompleteStringCollection();
            AutoCompleteStringCollection SoMay = new AutoCompleteStringCollection();
            AutoCompleteStringCollection SoKhung = new AutoCompleteStringCollection();
            //AutoCompleteStringCollection SoDienThoai = new AutoCompleteStringCollection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.datatabase.getConnection();
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;

            //Bao dưỡng dịch vụ
            if (rdb1.Checked)
            {
                cmd.CommandText = "select BienSo,SoMay from LichSuBaoDuongXe where IdCongty=" + Class.CompanyInfo.idcongty;
                cmd.Connection.Open();
                reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        BienSo.Add(reader["BienSo"].ToString());
                        //SoMay.Add(reader["SoMay"].ToString());
                    }
                }
                cmd.Connection.Close();
                //cmd.CommandText = "select BienSo,SoMay from LichSuBaoDuongXeTam where IdCongty=" + Class.CompanyInfo.idcongty;
                //cmd.Connection.Open();
                //reader = cmd.ExecuteReader();
                //if (reader != null)
                //{
                //    while (reader.Read())
                //    {
                //        auto1.Add(reader["BienSo"].ToString());
                //        SoMay.Add(reader["SoMay"].ToString());
                //    }
                //}
                //cmd.Connection.Close();
            }
            //Bao dưỡng định kỳ
            if (rdb2.Checked)
            {
                //BienSo.Clear();
                //SoMay.Clear();
                //txt_TimKiemBienSo.AutoCompleteCustomSource.Clear();
                //txt_TimKiemSoMay.AutoCompleteCustomSource.Clear();
                //cmd.CommandText = "select BienSo,SoMay from XeDaBan where IdCongty=" + Class.CompanyInfo.idcongty;
                cmd.CommandText = "select SoKhung, SoMay from XeDaBan where IdCongty=" + Class.CompanyInfo.idcongty;
                cmd.Connection.Open();
                reader = cmd.ExecuteReader();
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        //SoKhung.Add(reader["SoKhung"].ToString());
                        //SoMay.Add(reader["SoMay"].ToString());
                    }
                }
                cmd.Connection.Close();
            }

            txt_TimKiemBienSo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txt_TimKiemBienSo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txt_TimKiemBienSo.AutoCompleteCustomSource = BienSo;
            ////
            //txt_TimKiemSoMay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //txt_TimKiemSoMay.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txt_TimKiemSoMay.AutoCompleteCustomSource = SoMay;
            ////
            //txt_TimKiemSoKhung.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //txt_TimKiemSoKhung.AutoCompleteSource = AutoCompleteSource.CustomSource;
            //txt_TimKiemSoKhung.AutoCompleteCustomSource = SoKhung;
        }

        #endregion "AutoComplete"

        /// <summary>
        /// Tìm kiếm xe theo bảo dưỡng dịch vụ
        /// </summary>
        private void TimBaoduongdichvu()
        {
            idKhachHang = "";
            grv_LichSuBaoDuong.DataSource = null;
            txt_LanBD.Clear();
            txt_TenXe.Clear();
            txt_BienSo.Clear();
            txt_KhachHang.Clear();
            txt_Phone.Clear();
            txt_DiaChi.Clear();
            txt_CMND.Clear();
            txt_SoKhung.Clear();
            txt_SoMay.Clear();
            txt_GhiChu.Clear();
            txt_SoSBH.Clear();

            #region Yêu cầu Thắng Lợi

            //Cho cho nhập vào: Biển số, Số điện thoại. Tìm kiếm theo Biển số, Số điện thoại hoặc cả hai.

            //Điều kiện 1: Chỉ nhập Biển số

            #region "Tìm kiếm theo biển số"

            if (!String.IsNullOrEmpty(txt_TimKiemBienSo.Text) && String.IsNullOrEmpty(txt_TimKiemSDT.Text))
            {
                try
                {
                    //SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and lsbdx.BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                    SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and lsbdx.BienSo like @TKBienSo");
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@TKBienSo", txt_TimKiemBienSo.Text.Trim());

                    DataTable dt = Class.datatabase.getData(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txt_KhachHang.Text = ""; }
                        try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
                        catch { dt_NgaySinh.Value = DateTime.Now; }
                        try
                        {
                            txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { txt_DiaChi.Text = ""; }
                        try
                        {
                            txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { txt_Phone.Text = ""; }
                        try
                        {
                            txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
                        }
                        catch { txt_CMND.Text = ""; }

                        try
                        {
                            txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { txt_TenXe.Text = ""; }

                        try
                        {
                            txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { txt_SoKhung.Text = ""; }
                        try
                        {
                            txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { txt_SoMay.Text = ""; }
                        try
                        {
                            txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { txt_BienSo.Text = ""; }
                        try
                        {
                            cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { cbb_GioiTinh.Text = ""; }

                        //Thêm trường ghi chú bảo dưỡng
                        //string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                        //                + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                        //                + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                        //                + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                        //                + " Where lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'";

                        string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                                        + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                                        + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                                        + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                                        + " Where lsbdx.IdCongTy=@IdCongTy and BienSo like @TKBienSo";
                        cmd = new SqlCommand(sql);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKBienSo", txt_TimKiemBienSo.Text.Trim());

                        DataTable dt_LichSu = Class.datatabase.getData(cmd);

                        grv_LichSuBaoDuong.DataSource = dt_LichSu;
                        int solan = 0;
                        solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
                        txt_LanBD.Text = Convert.ToString(solan);

                        //Đóng kết nối
                        cmd.Connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thông tin biển số không tồn tại lịch sử bảo dưỡng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_TimKiemBienSo.SelectAll();
                        txt_TimKiemBienSo.Focus();

                        return;
                    }

                    #region Bỏ tìm trong bảng tạm

                    //else
                    //{
                    //    cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                    //    dt = Class.datatabase.getData(cmd);
                    //    if (dt.Rows.Count <= 0)
                    //    {
                    //        MessageBox.Show("Thông tin biển số không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //    try
                    //    {
                    //        txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                    //        idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                    //    }
                    //    catch { txt_KhachHang.Text = ""; }
                    //    try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
                    //    catch { dt_NgaySinh.Value = DateTime.Now; }
                    //    try
                    //    {
                    //        txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                    //    }
                    //    catch { txt_DiaChi.Text = ""; }
                    //    try
                    //    {
                    //        txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
                    //    }
                    //    catch { txt_Phone.Text = ""; }
                    //    try
                    //    {
                    //        txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
                    //    }
                    //    catch { txt_CMND.Text = ""; }

                    //    try
                    //    {
                    //        txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
                    //    }
                    //    catch { txt_TenXe.Text = ""; }

                    //    try
                    //    {
                    //        txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                    //    }
                    //    catch { txt_SoKhung.Text = ""; }
                    //    try
                    //    {
                    //        txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
                    //    }
                    //    catch { txt_SoMay.Text = ""; }
                    //    try
                    //    {
                    //        txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
                    //    }
                    //    catch { txt_BienSo.Text = ""; }
                    //    try
                    //    {
                    //        cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                    //    }
                    //    catch { cbb_GioiTinh.Text = ""; }
                    //}

                    #endregion Bỏ tìm trong bảng tạm
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
                }
            }

            #endregion "Tìm kiếm theo biển số"

            //Điều kiện 2: Chỉ nhập vào Số điện thoại

            #region "Tìm kiếm theo số điện thoại"

            if (String.IsNullOrEmpty(txt_TimKiemBienSo.Text) && !String.IsNullOrEmpty(txt_TimKiemSDT.Text))
            {
                try
                {
                    //SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and kh.DienThoai like '" + txt_TimKiemSDT.Text.Trim() + "'");
                    SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai");
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@TKSoDienThoai", txt_TimKiemSDT.Text.Trim());

                    DataTable dt = Class.datatabase.getData(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txt_KhachHang.Text = ""; }
                        try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
                        catch { dt_NgaySinh.Value = DateTime.Now; }
                        try
                        {
                            txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { txt_DiaChi.Text = ""; }
                        try
                        {
                            txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { txt_Phone.Text = ""; }
                        try
                        {
                            txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
                        }
                        catch { txt_CMND.Text = ""; }

                        try
                        {
                            txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { txt_TenXe.Text = ""; }

                        try
                        {
                            txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { txt_SoKhung.Text = ""; }
                        try
                        {
                            txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { txt_SoMay.Text = ""; }
                        try
                        {
                            txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { txt_BienSo.Text = ""; }
                        try
                        {
                            cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { cbb_GioiTinh.Text = ""; }

                        //Thêm trường ghi chú bảo dưỡng
                        //string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                        //                + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                        //                + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                        //                + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                        //                + " left outer join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang"
                        //                + " Where lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and kh.DienThoai like '" + txt_TimKiemSDT.Text.Trim() + "'";

                        string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                                        + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                                        + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                                        + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                                        + " left outer join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang"
                                        + " Where lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai";
                        cmd = new SqlCommand(sql);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKSoDienThoai", txt_TimKiemSDT.Text.Trim());

                        DataTable dt_LichSu = Class.datatabase.getData(cmd);

                        grv_LichSuBaoDuong.DataSource = dt_LichSu;
                        int solan = 0;
                        solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
                        txt_LanBD.Text = Convert.ToString(solan);

                        cmd.Connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thông tin số điện thoại không tồn trong tại lịch sử bảo dưỡng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_TimKiemSDT.SelectAll();
                        txt_TimKiemSDT.Focus();

                        return;
                    }

                    #region " Bỏ Tìm trong bảng tạm

                    //else
                    //{
                    //    cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                    //    dt = Class.datatabase.getData(cmd);
                    //    if (dt.Rows.Count <= 0)
                    //    {
                    //        MessageBox.Show("Thông tin biển số không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //    try
                    //    {
                    //        txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                    //        idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                    //    }
                    //    catch { txt_KhachHang.Text = ""; }
                    //    try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
                    //    catch { dt_NgaySinh.Value = DateTime.Now; }
                    //    try
                    //    {
                    //        txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                    //    }
                    //    catch { txt_DiaChi.Text = ""; }
                    //    try
                    //    {
                    //        txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
                    //    }
                    //    catch { txt_Phone.Text = ""; }
                    //    try
                    //    {
                    //        txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
                    //    }
                    //    catch { txt_CMND.Text = ""; }

                    //    try
                    //    {
                    //        txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
                    //    }
                    //    catch { txt_TenXe.Text = ""; }

                    //    try
                    //    {
                    //        txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                    //    }
                    //    catch { txt_SoKhung.Text = ""; }
                    //    try
                    //    {
                    //        txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
                    //    }
                    //    catch { txt_SoMay.Text = ""; }
                    //    try
                    //    {
                    //        txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
                    //    }
                    //    catch { txt_BienSo.Text = ""; }
                    //    try
                    //    {
                    //        cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                    //    }
                    //    catch { cbb_GioiTinh.Text = ""; }
                    //}

                    #endregion " Bỏ Tìm trong bảng tạm
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
                }
            }

            #endregion "Tìm kiếm theo số điện thoại"

            //Điều kiện 3: Nhập vào cả Biển số và Số điện thoại

            #region "Tìm kiếm theo số điện thoại và Biển số"

            if (!String.IsNullOrEmpty(txt_TimKiemBienSo.Text) && !String.IsNullOrEmpty(txt_TimKiemSDT.Text))
            {
                try
                {
                    //SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and kh.DienThoai like '" + txt_TimKiemSDT.Text.Trim() + "' and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");

                    SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai and BienSo like @TKBienSo");
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@TKSoDienThoai", txt_TimKiemSDT.Text.Trim());
                    cmd.Parameters.AddWithValue("@TKBienSo", txt_TimKiemBienSo.Text.Trim());

                    DataTable dt = Class.datatabase.getData(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txt_KhachHang.Text = ""; }
                        try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
                        catch { dt_NgaySinh.Value = DateTime.Now; }
                        try
                        {
                            txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { txt_DiaChi.Text = ""; }
                        try
                        {
                            txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { txt_Phone.Text = ""; }
                        try
                        {
                            txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
                        }
                        catch { txt_CMND.Text = ""; }

                        try
                        {
                            txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { txt_TenXe.Text = ""; }

                        try
                        {
                            txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { txt_SoKhung.Text = ""; }
                        try
                        {
                            txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { txt_SoMay.Text = ""; }
                        try
                        {
                            txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { txt_BienSo.Text = ""; }
                        try
                        {
                            cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { cbb_GioiTinh.Text = ""; }

                        //Thêm trường ghi chú bảo dưỡng
                        //string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                        //                + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                        //                + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                        //                + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                        //                + " left outer join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang"
                        //                + " Where lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and kh.DienThoai like '" + txt_TimKiemSDT.Text.Trim() + "' and lsbdx.BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'";

                        string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                                        + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                                        + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                                        + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                                        + " left outer join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang"
                                        + " Where lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai and lsbdx.BienSo like @TKBienSo";
                        cmd = new SqlCommand(sql);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKSoDienThoai", txt_TimKiemSDT.Text.Trim());
                        cmd.Parameters.AddWithValue("@TKBienSo", txt_TimKiemBienSo.Text.Trim());

                        DataTable dt_LichSu = Class.datatabase.getData(cmd);

                        grv_LichSuBaoDuong.DataSource = dt_LichSu;
                        int solan = 0;
                        solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
                        txt_LanBD.Text = Convert.ToString(solan);

                        cmd.Connection.Close();
                    }
                    else
                    {
                        MessageBox.Show("Thông tin Số điện thoại và Biển số không tồn tại trong lịch sử bảo dưỡng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txt_TimKiemBienSo.SelectAll();
                        txt_TimKiemBienSo.Focus();

                        return;
                    }

                    #region "Tìm trong bảng tạm

                    //else
                    //{
                    //    cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                    //    dt = Class.datatabase.getData(cmd);
                    //    if (dt.Rows.Count <= 0)
                    //    {
                    //        MessageBox.Show("Thông tin biển số không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //        return;
                    //    }
                    //    try
                    //    {
                    //        txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                    //        idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                    //    }
                    //    catch { txt_KhachHang.Text = ""; }
                    //    try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
                    //    catch { dt_NgaySinh.Value = DateTime.Now; }
                    //    try
                    //    {
                    //        txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                    //    }
                    //    catch { txt_DiaChi.Text = ""; }
                    //    try
                    //    {
                    //        txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
                    //    }
                    //    catch { txt_Phone.Text = ""; }
                    //    try
                    //    {
                    //        txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
                    //    }
                    //    catch { txt_CMND.Text = ""; }

                    //    try
                    //    {
                    //        txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
                    //    }
                    //    catch { txt_TenXe.Text = ""; }

                    //    try
                    //    {
                    //        txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                    //    }
                    //    catch { txt_SoKhung.Text = ""; }
                    //    try
                    //    {
                    //        txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
                    //    }
                    //    catch { txt_SoMay.Text = ""; }
                    //    try
                    //    {
                    //        txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
                    //    }
                    //    catch { txt_BienSo.Text = ""; }
                    //    try
                    //    {
                    //        cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                    //    }
                    //    catch { cbb_GioiTinh.Text = ""; }
                    //}

                    #endregion "Tìm trong bảng tạm
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
                }
            }

            #endregion "Tìm kiếm theo số điện thoại và Biển số"

            if (String.IsNullOrEmpty(txt_TimKiemBienSo.Text) && String.IsNullOrEmpty(txt_TimKiemSDT.Text))
            {
                MessageBox.Show("Bạn chưa nhập vào thông tin tìm kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_TimKiemBienSo.Focus();

                return;
            }

            #endregion Yêu cầu Thắng Lợi

            #region Yêu cầu cũ

            #region "Tìm kiếm theo số máy"

            //if (!String.IsNullOrEmpty(txt_TimKiemSoMay.Text))
            //{
            //    try
            //    {
            //        SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");
            //        DataTable dt = Class.datatabase.getData(cmd);
            //        if (dt.Rows.Count > 0)
            //        {
            //            try
            //            {
            //                txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
            //                idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
            //            }
            //            catch { txt_KhachHang.Text = ""; }
            //            try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
            //            catch { dt_NgaySinh.Value = DateTime.Now; }
            //            try
            //            {
            //                txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
            //            }
            //            catch { txt_DiaChi.Text = ""; }
            //            try
            //            {
            //                txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
            //            }
            //            catch { txt_Phone.Text = ""; }
            //            try
            //            {
            //                txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
            //            }
            //            catch { txt_CMND.Text = ""; }

            //            try
            //            {
            //                txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
            //            }
            //            catch { txt_TenXe.Text = ""; }

            //            try
            //            {
            //                txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
            //            }
            //            catch { txt_SoKhung.Text = ""; }
            //            try
            //            {
            //                txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
            //            }
            //            catch { txt_SoMay.Text = ""; }
            //            try
            //            {
            //                txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
            //            }
            //            catch { txt_BienSo.Text = ""; }
            //            try
            //            {
            //                cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
            //            }
            //            catch { cbb_GioiTinh.Text = ""; }
            //            try
            //            {
            //                txt_SoSBH.Text = dt.Rows[0]["SoSBH"].ToString();
            //            }
            //            catch { txt_SoSBH.Text = ""; }
            //            string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan,CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong from LichSuBaoDuongXe lsbdx"
            //                            + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
            //                            + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
            //                            + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
            //                            + " Where lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'";
            //            cmd = new SqlCommand(sql);
            //            DataTable dt_LichSu = Class.datatabase.getData(cmd);

            //            grv_LichSuBaoDuong.DataSource = dt_LichSu;
            //            int solan = 0;
            //            solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
            //            txt_LanBD.Text = Convert.ToString(solan);
            //        }
            //        else
            //        {
            //            cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");
            //            dt = Class.datatabase.getData(cmd);
            //            if (dt.Rows.Count <= 0)
            //            {
            //                MessageBox.Show("Thông tin số máy không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                return;
            //            }
            //            try
            //            {
            //                txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
            //                idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
            //            }
            //            catch { txt_KhachHang.Text = ""; }
            //            try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
            //            catch { dt_NgaySinh.Value = DateTime.Now; }
            //            try
            //            {
            //                txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
            //            }
            //            catch { txt_DiaChi.Text = ""; }
            //            try
            //            {
            //                txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
            //            }
            //            catch { txt_Phone.Text = ""; }
            //            try
            //            {
            //                txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
            //            }
            //            catch { txt_CMND.Text = ""; }

            //            try
            //            {
            //                txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
            //            }
            //            catch { txt_TenXe.Text = ""; }

            //            try
            //            {
            //                txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
            //            }
            //            catch { txt_SoKhung.Text = ""; }
            //            try
            //            {
            //                txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
            //            }
            //            catch { txt_SoMay.Text = ""; }
            //            try
            //            {
            //                txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
            //            }
            //            catch { txt_BienSo.Text = ""; }
            //            try
            //            {
            //                cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
            //            }
            //            catch { cbb_GioiTinh.Text = ""; }
            //        }
            //    }
            //    catch (Exception ex) { MessageBox.Show("Số máy không tồn tại !" + ex.Message); }
            //}

            #endregion "Tìm kiếm theo số máy"

            #region "Tìm kiếm theo biển số"

            //else
            //{
            //    if (!String.IsNullOrEmpty(txt_TimKiemBienSo.Text))
            //    {
            //        try
            //        {
            //            SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
            //            DataTable dt = Class.datatabase.getData(cmd);
            //            if (dt.Rows.Count > 0)
            //            {
            //                try
            //                {
            //                    txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
            //                    idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
            //                }
            //                catch { txt_KhachHang.Text = ""; }
            //                try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
            //                catch { dt_NgaySinh.Value = DateTime.Now; }
            //                try
            //                {
            //                    txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
            //                }
            //                catch { txt_DiaChi.Text = ""; }
            //                try
            //                {
            //                    txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
            //                }
            //                catch { txt_Phone.Text = ""; }
            //                try
            //                {
            //                    txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
            //                }
            //                catch { txt_CMND.Text = ""; }

            //                try
            //                {
            //                    txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
            //                }
            //                catch { txt_TenXe.Text = ""; }

            //                try
            //                {
            //                    txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
            //                }
            //                catch { txt_SoKhung.Text = ""; }
            //                try
            //                {
            //                    txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
            //                }
            //                catch { txt_SoMay.Text = ""; }
            //                try
            //                {
            //                    txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
            //                }
            //                catch { txt_BienSo.Text = ""; }
            //                try
            //                {
            //                    cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
            //                }
            //                catch { cbb_GioiTinh.Text = ""; }
            //                string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong from LichSuBaoDuongXe lsbdx"
            //                                + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
            //                                + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
            //                                + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
            //                                + " Where lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'";
            //                cmd = new SqlCommand(sql);
            //                DataTable dt_LichSu = Class.datatabase.getData(cmd);

            //                grv_LichSuBaoDuong.DataSource = dt_LichSu;
            //                int solan = 0;
            //                solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
            //                txt_LanBD.Text = Convert.ToString(solan);
            //            }
            //            else
            //            {
            //                cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
            //                dt = Class.datatabase.getData(cmd);
            //                if (dt.Rows.Count <= 0)
            //                {
            //                    MessageBox.Show("Thông tin biển số không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    return;
            //                }
            //                try
            //                {
            //                    txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
            //                    idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
            //                }
            //                catch { txt_KhachHang.Text = ""; }
            //                try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
            //                catch { dt_NgaySinh.Value = DateTime.Now; }
            //                try
            //                {
            //                    txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
            //                }
            //                catch { txt_DiaChi.Text = ""; }
            //                try
            //                {
            //                    txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
            //                }
            //                catch { txt_Phone.Text = ""; }
            //                try
            //                {
            //                    txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
            //                }
            //                catch { txt_CMND.Text = ""; }

            //                try
            //                {
            //                    txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
            //                }
            //                catch { txt_TenXe.Text = ""; }

            //                try
            //                {
            //                    txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
            //                }
            //                catch { txt_SoKhung.Text = ""; }
            //                try
            //                {
            //                    txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
            //                }
            //                catch { txt_SoMay.Text = ""; }
            //                try
            //                {
            //                    txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
            //                }
            //                catch { txt_BienSo.Text = ""; }
            //                try
            //                {
            //                    cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
            //                }
            //                catch { cbb_GioiTinh.Text = ""; }
            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Biển số không tồn tại !" + ex.Message);

            //        }
            //    }
            //}

            #endregion "Tìm kiếm theo biển số"

            #endregion Yêu cầu cũ

            Class.tblBaoDuong.idkhachhang = idKhachHang;
            try
            {
                grv_LichSuBaoDuong.Columns["IdBaoDuong"].Visible = false;
                grv_LichSuBaoDuong.Columns["YeuCauKH"].Visible = false;
                grv_LichSuBaoDuong.Columns["MaThoDuyet"].Visible = false;
                grv_LichSuBaoDuong.Columns["SoPhieu"].Visible = false;
                grv_LichSuBaoDuong.Columns["ThayDau"].Visible = false;
                grv_LichSuBaoDuong.Columns["ThayDauMay"].Visible = false;
                grv_LichSuBaoDuong.Columns["ChuanDoan"].Visible = false;
                grv_LichSuBaoDuong.Columns["YeuCauKH"].Visible = false;
                grv_LichSuBaoDuong.Columns["TongTien"].Visible = false;

                //fixed collumn width
                grv_LichSuBaoDuong.Columns["STT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["STT"].Width = 50;
                grv_LichSuBaoDuong.Columns["TenXe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["TenXe"].Width = 200;
                grv_LichSuBaoDuong.Columns["BienSo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["BienSo"].Width = 110;
                grv_LichSuBaoDuong.Columns["NgayBaoDuong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["NgayBaoDuong"].Width = 130;
                grv_LichSuBaoDuong.Columns["NgayGiaoXe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["NgayGiaoXe"].Width = 130;
                grv_LichSuBaoDuong.Columns["SoLan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["SoLan"].Width = 50;
                grv_LichSuBaoDuong.Columns["TenThoDuyet"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["TenThoDuyet"].Width = 150;
                //grv_LichSuBaoDuong.Columns["TenThoDuyet"].Visible = false;
                //grv_LichSuBaoDuong.Columns["STT"].Visible = false;
                grv_LichSuBaoDuong.Columns["TongTien"].DefaultCellStyle.Format = "0,0";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Cảnh báo");
            }
        }

        /// <summary>
        /// Tìm kiếm xe theo bảo dưỡng định kỳ
        /// </summary>
        private void TimBaodinhky()
        {
            idKhachHang = "";
            grv_LichSuBaoDuong.DataSource = null;
            txt_LanBD.Clear();
            txt_TenXe.Clear();
            txt_BienSo.Clear();
            txt_KhachHang.Clear();
            txt_Phone.Clear();
            txt_DiaChi.Clear();
            txt_CMND.Clear();
            txt_SoKhung.Clear();
            txt_SoMay.Clear();
            txt_GhiChu.Clear();
            txt_SoSBH.Clear();

            //Cho cho nhập vào: Số khung, số may. Tìm kiếm theo Số khung, Số máy hoặc cả hai.

            //Điều kiện 1: Chỉ nhập số máy

            #region "Tìm theo số máy"

            if (!String.IsNullOrEmpty(txt_TimKiemSoMay.Text) && String.IsNullOrEmpty(txt_TimKiemSoKhung.Text))
            {
                try
                {
                    //SqlCommand cmd = new SqlCommand("select top 1 * from XeDaBan lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");

                    SqlCommand cmd = new SqlCommand("select top 1 * from XeDaBan lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and SoMay like @TKSoMay");
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@TKSoMay", txt_TimKiemSoMay.Text.Trim());

                    DataTable dt = Class.datatabase.getData(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txt_KhachHang.Text = ""; }
                        try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
                        catch { dt_NgaySinh.Value = DateTime.Now; }
                        try
                        {
                            txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { txt_DiaChi.Text = ""; }
                        try
                        {
                            txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { txt_Phone.Text = ""; }
                        try
                        {
                            txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
                        }
                        catch { txt_CMND.Text = ""; }

                        try
                        {
                            txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { txt_TenXe.Text = ""; }

                        try
                        {
                            txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { txt_SoKhung.Text = ""; }
                        try
                        {
                            txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { txt_SoMay.Text = ""; }
                        try
                        {
                            txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { txt_BienSo.Text = ""; }
                        try
                        {
                            cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { cbb_GioiTinh.Text = ""; }
                        try
                        {
                            txt_SoSBH.Text = dt.Rows[0]["SoSBH"].ToString();
                        }
                        catch { txt_SoSBH.Text = ""; }

                        //Ngày mua xe
                        try
                        {
                            dt_NgayMuaXe.Text = dt.Rows[0]["NgayBan"].ToString();
                        }
                        catch { dt_NgayMuaXe.Text = Convert.ToString(DateTime.Now); }

                        //Loại khách hàng
                        try
                        {
                            cboKhachDen.Text = dt.Rows[0]["KhachDenTu"].ToString();
                        }
                        catch { cboKhachDen.Text = ""; }

                        //Thêm trường ghi chú bảo dưỡng.
                        //string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan,CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                        //                + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                        //                + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                        //                + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                        //                + " Where lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'";

                        string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan,CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                                       + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                                       + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                                       + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                                       + " Where lsbdx.IdCongTy=@IdCongTy and SoMay like @TKSoMay";
                        cmd = new SqlCommand(sql);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKSoMay", txt_TimKiemSoMay.Text.Trim());

                        DataTable dt_LichSu = Class.datatabase.getData(cmd);

                        grv_LichSuBaoDuong.DataSource = dt_LichSu;
                        int solan = 0;

                        try
                        {
                            solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
                            txt_LanBD.Text = Convert.ToString(solan);
                        }
                        catch
                        {
                            txt_LanBD.Text = "1";
                        }

                        cmd.Connection.Close();
                    }
                    else
                    {
                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("Thông tin số máy không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txt_TimKiemSoMay.SelectAll();
                            txt_TimKiemSoMay.Focus();

                            return;
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo"); }
            }

            #endregion "Tìm theo số máy"

            //Điều kiện 2: Chỉ nhập vào số khung.

            #region "Tìm theo số khung"

            if (String.IsNullOrEmpty(txt_TimKiemSoMay.Text) && !String.IsNullOrEmpty(txt_TimKiemSoKhung.Text))
            {
                try
                {
                    //SqlCommand cmd = new SqlCommand("select top 1 * from XeDaBan lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoKhung like '" + txt_TimKiemSoKhung.Text.Trim() + "'");

                    SqlCommand cmd = new SqlCommand("select top 1 * from XeDaBan lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and SoKhung like @TKSoKhung");
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@TKSoKhung", txt_TimKiemSoKhung.Text.Trim());

                    DataTable dt = Class.datatabase.getData(cmd);

                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txt_KhachHang.Text = ""; }
                        try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
                        catch { dt_NgaySinh.Value = DateTime.Now; }
                        try
                        {
                            txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { txt_DiaChi.Text = ""; }
                        try
                        {
                            txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { txt_Phone.Text = ""; }
                        try
                        {
                            txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
                        }
                        catch { txt_CMND.Text = ""; }

                        try
                        {
                            txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { txt_TenXe.Text = ""; }

                        try
                        {
                            txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { txt_SoKhung.Text = ""; }
                        try
                        {
                            txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { txt_SoMay.Text = ""; }
                        try
                        {
                            txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { txt_BienSo.Text = ""; }
                        try
                        {
                            cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { cbb_GioiTinh.Text = ""; }
                        try
                        {
                            txt_SoSBH.Text = dt.Rows[0]["SoSBH"].ToString();
                        }
                        catch { txt_SoSBH.Text = ""; }

                        //Ngày mua xe
                        try
                        {
                            dt_NgayMuaXe.Text = dt.Rows[0]["NgayBan"].ToString();
                        }
                        catch { dt_NgayMuaXe.Text = Convert.ToString(DateTime.Now); }

                        //Loại khách hàng
                        try
                        {
                            cboKhachDen.Text = dt.Rows[0]["KhachDenTu"].ToString();
                        }
                        catch { cboKhachDen.Text = ""; }

                        //Thêm trường ghi chú bảo dưỡng.
                        //string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan,CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                        //                + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                        //                + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                        //                + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                        //                + " Where lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoKhung like '" + txt_TimKiemSoKhung.Text.Trim() + "'";

                        string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan,CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                                        + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                                        + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                                        + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                                        + " Where lsbdx.IdCongTy=@IdCongTy and SoKhung like @TKSoKhung";
                        cmd = new SqlCommand(sql);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKSoKhung", txt_TimKiemSoKhung.Text.Trim());

                        DataTable dt_LichSu = Class.datatabase.getData(cmd);

                        grv_LichSuBaoDuong.DataSource = dt_LichSu;
                        int solan = 0;

                        try
                        {
                            solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
                            txt_LanBD.Text = Convert.ToString(solan);
                        }
                        catch
                        {
                            txt_LanBD.Text = "1";
                        }

                        cmd.Connection.Close();
                    }
                    else
                    {
                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("Thông tin số khung không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txt_TimKiemSoKhung.SelectAll();
                            txt_TimKiemSoKhung.Focus();

                            return;
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo"); }
            }

            #endregion "Tìm theo số khung"

            //Điều kiện 3: Nhập vào cả số khung và số máy

            #region "Tìm kiếm theo số khung và số máy"

            if (!String.IsNullOrEmpty(txt_TimKiemSoMay.Text) && !String.IsNullOrEmpty(txt_TimKiemSoKhung.Text))
            {
                try
                {
                    //SqlCommand cmd = new SqlCommand("select top 1 * from XeDaBan lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoKhung like '" + txt_TimKiemSoKhung.Text.Trim() + "' and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");

                    SqlCommand cmd = new SqlCommand("select top 1 * from XeDaBan lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and SoKhung like @TKSoKhung and SoMay like @TKSoMay");
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@TKSoKhung", txt_TimKiemSoKhung.Text.Trim());
                    cmd.Parameters.AddWithValue("@TKSoMay", txt_TimKiemSoMay.Text.Trim());

                    DataTable dt = Class.datatabase.getData(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txt_KhachHang.Text = ""; }
                        try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
                        catch { dt_NgaySinh.Value = DateTime.Now; }
                        try
                        {
                            txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { txt_DiaChi.Text = ""; }
                        try
                        {
                            txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { txt_Phone.Text = ""; }
                        try
                        {
                            txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
                        }
                        catch { txt_CMND.Text = ""; }

                        try
                        {
                            txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { txt_TenXe.Text = ""; }

                        try
                        {
                            txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { txt_SoKhung.Text = ""; }
                        try
                        {
                            txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { txt_SoMay.Text = ""; }
                        try
                        {
                            txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { txt_BienSo.Text = ""; }
                        try
                        {
                            cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { cbb_GioiTinh.Text = ""; }
                        try
                        {
                            txt_SoSBH.Text = dt.Rows[0]["SoSBH"].ToString();
                        }
                        catch { txt_SoSBH.Text = ""; }

                        //Ngày mua xe
                        try
                        {
                            dt_NgayMuaXe.Text = dt.Rows[0]["NgayBan"].ToString();
                        }
                        catch { dt_NgayMuaXe.Text = Convert.ToString(DateTime.Now); }

                        //Loại khách hàng
                        try
                        {
                            cboKhachDen.Text = dt.Rows[0]["KhachDenTu"].ToString();
                        }
                        catch { cboKhachDen.Text = ""; }

                        //Thêm trường ghi chú bảo dưỡng.
                        //string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan,CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                        //                + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                        //                + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                        //                + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                        //                + " Where lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoKhung like '" + txt_TimKiemSoKhung.Text.Trim() + "' and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'";

                        string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan,CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                                        + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                                        + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                                        + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                                        + " Where lsbdx.IdCongTy=@IdCongTy and SoKhung like @TKSoKhung and SoMay like @TKSoMay";
                        cmd = new SqlCommand(sql);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKSoKhung", txt_TimKiemSoKhung.Text.Trim());
                        cmd.Parameters.AddWithValue("@TKSoMay", txt_TimKiemSoMay.Text.Trim());

                        DataTable dt_LichSu = Class.datatabase.getData(cmd);

                        grv_LichSuBaoDuong.DataSource = dt_LichSu;
                        int solan = 0;

                        try
                        {
                            solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
                            txt_LanBD.Text = Convert.ToString(solan);
                        }
                        catch
                        {
                            txt_LanBD.Text = "1";
                        }

                        cmd.Connection.Close();
                    }
                    else
                    {
                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("Thông tin số khung hoặc số máy không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txt_TimKiemSoMay.SelectAll();
                            txt_TimKiemSoMay.Focus();

                            return;
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo"); }
            }

            #endregion "Tìm kiếm theo số khung và số máy"

            if (String.IsNullOrEmpty(txt_TimKiemSoMay.Text) && String.IsNullOrEmpty(txt_TimKiemSoKhung.Text))
            {
                MessageBox.Show("Bạn chưa nhập vào thông tin tìm kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txt_TimKiemSoMay.Focus();

                return;
            }

            #region "Tìm kếm theo biển số"

            //else
            //{
            //    if (!String.IsNullOrEmpty(txt_TimKiemBienSo.Text))
            //    {
            //        try
            //        {
            //            SqlCommand cmd = new SqlCommand("select top 1 * from XeDaBan lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
            //            DataTable dt = Class.datatabase.getData(cmd);
            //            if (dt.Rows.Count > 0)
            //            {
            //                try
            //                {
            //                    txt_KhachHang.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
            //                    idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
            //                }
            //                catch { txt_KhachHang.Text = ""; }

            //                try { dt_NgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
            //                catch { dt_NgaySinh.Value = DateTime.Now; }

            //                try
            //                {
            //                    txt_DiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
            //                }
            //                catch { txt_DiaChi.Text = ""; }

            //                try
            //                {
            //                    txt_Phone.Text = dt.Rows[0]["DienThoai"].ToString();
            //                }
            //                catch { txt_Phone.Text = ""; }

            //                try
            //                {
            //                    txt_CMND.Text = dt.Rows[0]["CMND"].ToString();
            //                }
            //                catch { txt_CMND.Text = ""; }

            //                try
            //                {
            //                    txt_TenXe.Text = dt.Rows[0]["TenXe"].ToString();
            //                }
            //                catch { txt_TenXe.Text = ""; }

            //                try
            //                {
            //                    txt_SoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
            //                }
            //                catch { txt_SoKhung.Text = ""; }
            //                try
            //                {
            //                    txt_SoMay.Text = dt.Rows[0]["SoMay"].ToString();
            //                }
            //                catch { txt_SoMay.Text = ""; }
            //                try
            //                {
            //                    txt_BienSo.Text = dt.Rows[0]["BienSo"].ToString();
            //                }
            //                catch { txt_BienSo.Text = ""; }
            //                try
            //                {
            //                    cbb_GioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
            //                }
            //                catch { cbb_GioiTinh.Text = ""; }

            //                string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong from LichSuBaoDuongXe lsbdx"
            //                                + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
            //                                + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
            //                                + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
            //                                + " Where lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'";
            //                cmd = new SqlCommand(sql);
            //                DataTable dt_LichSu = Class.datatabase.getData(cmd);

            //                grv_LichSuBaoDuong.DataSource = dt_LichSu;
            //                int solan = 0;
            //                try
            //                {
            //                    solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
            //                    txt_LanBD.Text = Convert.ToString(solan);
            //                }
            //                catch
            //                {
            //                    txt_LanBD.Text = "1";
            //                }
            //            }
            //            else
            //            {
            //                if (dt.Rows.Count <= 0)
            //                {
            //                    MessageBox.Show("Thông tin biển số không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //                    return;
            //                }

            //            }
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Biển số không tồn tại !" + ex.Message);

            //        }
            //    }
            //}

            #endregion "Tìm kếm theo biển số"

            Class.tblBaoDuong.idkhachhang = idKhachHang;

            try
            {
                grv_LichSuBaoDuong.Columns["IdBaoDuong"].Visible = false;
                grv_LichSuBaoDuong.Columns["YeuCauKH"].Visible = false;
                grv_LichSuBaoDuong.Columns["MaThoDuyet"].Visible = false;
                grv_LichSuBaoDuong.Columns["SoPhieu"].Visible = false;
                grv_LichSuBaoDuong.Columns["ThayDau"].Visible = false;
                grv_LichSuBaoDuong.Columns["ThayDauMay"].Visible = false;
                grv_LichSuBaoDuong.Columns["ChuanDoan"].Visible = false;
                grv_LichSuBaoDuong.Columns["YeuCauKH"].Visible = false;
                grv_LichSuBaoDuong.Columns["TongTien"].Visible = false;

                //fix collumn width
                grv_LichSuBaoDuong.Columns["STT"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["STT"].Width = 50;
                grv_LichSuBaoDuong.Columns["TenXe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["TenXe"].Width = 200;
                grv_LichSuBaoDuong.Columns["BienSo"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["BienSo"].Width = 110;
                grv_LichSuBaoDuong.Columns["NgayBaoDuong"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["NgayBaoDuong"].Width = 130;
                grv_LichSuBaoDuong.Columns["NgayGiaoXe"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["NgayGiaoXe"].Width = 130;
                grv_LichSuBaoDuong.Columns["SoLan"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["SoLan"].Width = 50;
                grv_LichSuBaoDuong.Columns["TenThoDuyet"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                grv_LichSuBaoDuong.Columns["TenThoDuyet"].Width = 150;

                //grv_LichSuBaoDuong.Columns["TenThoDuyet"].Visible = false;
                //grv_LichSuBaoDuong.Columns["STT"].Visible = false;
                grv_LichSuBaoDuong.Columns["TongTien"].DefaultCellStyle.Format = "0,0";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Cảnh báo");
            }
        }

        //xong

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UcTiepNhan_Load(object sender, EventArgs e)
        {
            if (Class.CompanyInfo.GoiPhanMem.ToLower() == "bao duong")
            {
                rdb1.Checked = true;
                rdb2.Checked = false;
            }
            else
            {
                rdb1.Checked = false;
                rdb2.Checked = true;
            }

            cbb_GioiTinh.Text = "Nam";
            LoadXeDangBaoDuong();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                if (txt_BienSo.Text == "")
                {
                    MessageBox.Show("Mời nhập biển số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (String.IsNullOrEmpty(idKhachHang))
                {
                    SqlCommand cmd1 = new SqlCommand(@"insert into KhachHang(idcongty,TenKH,Ngaysinh,dienthoai,diachi,CMND,gioitinh,MaNhomKH,LoaiKH) values(@Idcongty,@TenKH,@Ngaysinh,@dienthoai,@diachi,@CMND,@gioitinh,@MaNhomKH,@LoaiKH) select @@Identity");
                    cmd1.Connection = Class.datatabase.getConnection();
                    //chen thong tin khach hang , com tra ve ma khach hang.
                    cmd1.Parameters.AddWithValue("@Idcongty", Class.CompanyInfo.idcongty);
                    cmd1.Parameters.AddWithValue("@TenKH", txt_KhachHang.Text);
                    cmd1.Parameters.AddWithValue("@Ngaysinh", dt_NgaySinh.Value);
                    cmd1.Parameters.AddWithValue("@dienthoai", txt_Phone.Text);
                    cmd1.Parameters.AddWithValue("@diachi", txt_DiaChi.Text);
                    cmd1.Parameters.AddWithValue("@CMND", txt_CMND.Text);
                    cmd1.Parameters.AddWithValue("@gioitinh", Convert.ToString(cbb_GioiTinh.Text));
                    cmd1.Parameters.AddWithValue("@MaNhomKH", Convert.ToString(cbb_NhomKH.Text));
                    cmd1.Parameters.AddWithValue("@LoaiKH", "2");
                    cmd1.Connection.Open();
                    idKhachHang = cmd1.ExecuteScalar().ToString();
                    cmd1.Connection.Close();
                }
                string LoaiBaoDuong = ((KeyValuePair<string, string>)cbbMaintenanceTypes.SelectedItem).Key;
                string sql = @"insert into LichSuBaoDuongXeTam(idcuahang,idkhachhang,idcongty,tenxe,bienso,somay,sokhung,ngaybaoduong,solan,LoaiBaoDuong) values(@idcuahang, @idkhachhang, @idcongty, @tenxe, @bienso,@somay,@sokhung,@ngaybaoduong, @solan,@LoaiBaoDuong)";
                cmd = new SqlCommand(sql);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idcuahang", Class.EmployeeInfo.IdCuaHang);
                cmd.Parameters.AddWithValue("@idkhachhang", idKhachHang);
                cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@tenxe", txt_TenXe.Text);
                cmd.Parameters.AddWithValue("@bienso", txt_BienSo.Text);
                cmd.Parameters.AddWithValue("@somay", txt_SoMay.Text);
                cmd.Parameters.AddWithValue("@sokhung", txt_SoKhung.Text);
                cmd.Parameters.AddWithValue("@ngaybaoduong", dt_NgayBaoDuong.Value);
                cmd.Parameters.AddWithValue("@LoaiBaoDuong", LoaiBaoDuong);

                if (String.IsNullOrEmpty(txt_LanBD.Text))
                {
                    cmd.Parameters.AddWithValue("@solan", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@solan", txt_LanBD.Text);
                }
                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Nhập xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadXeDangBaoDuong();
                }
                else
                {
                    MessageBox.Show("Nhập xe bảo dưỡng thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                cmd = new SqlCommand("Select * from LichSuBaoDuongXeTam Where IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang");
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                Class.tblBaoDuong.lsBaoduongxetam = Class.datatabase.getData(cmd);
                //dgvDsXeDangBaoDuong1.DataSource = dtxeBaoDuong2;
                //dgvDsXeDangBaoDuong2.DataSource = dtxeBaoDuong2;
                //dgvDsXeDangBaoDuong3.DataSource = dtxeBaoDuong2;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            #region capnhatthongtinkhachhang

            string tenkh = txt_KhachHang.Text;
            string ngaysinh = dt_NgaySinh.Text;
            string gioitinh = cbb_GioiTinh.Text.ToString();
            string dienthoai = txt_Phone.Text;
            string diachi = txt_DiaChi.Text;
            string socmnd = txt_CMND.Text;
            string sosbh = txt_SoSBH.Text;
            string ghichu = txt_GhiChu.Text;
            // string nhomkh = cbb_NhomKH.SelectedItem.ToString();
            SqlCommand com = new SqlCommand();
            com.CommandText =// "Update KhachHang set TenKH='"+ tenkh + "',GioiTinh='"+gioitinh+"',NgaySinh='"+ngaysinh+"',DienThoai='"+dienthoai+"',Diachi='"+diachi+"',CMND='"+socmnd+"',SoSBH='"+sosbh+"' where IdKhachHang='"+idKhachHang+"' And IdCongty='"+Class.CompanyInfo.idcongty+"'";
            "update KhachHang set TenKH=@hoten,GioiTinh=@gioitinh,NgaySinh=@ngaysinh,DienThoai=@dienthoai,Diachi=@diachi,CMND=@cmnd,SoSBH=@sosbh where IdKhachHang=@idkhachhang And IdCongty=@idconty";
            com.Parameters.Clear();
            com.Parameters.AddWithValue("@hoten", tenkh);
            com.Parameters.AddWithValue("@gioitinh", gioitinh);
            com.Parameters.AddWithValue("@ngaysinh", ngaysinh);
            com.Parameters.AddWithValue("@dienthoai", dienthoai);
            com.Parameters.AddWithValue("@diachi", diachi);
            com.Parameters.AddWithValue("@cmnd", socmnd);
            com.Parameters.AddWithValue("@sosbh", sosbh);
            com.Parameters.AddWithValue("@idkhachhang", idKhachHang);
            com.Parameters.AddWithValue("@idconty", Class.CompanyInfo.idcongty);
            if (Class.datatabase.ExcuteNonQuery(com) > 0)
            {
                MessageBox.Show("Cập nhật thông tin khách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Cập nhật thông tin khách hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            #endregion capnhatthongtinkhachhang

            #region Cap nhat thong tin xe

            if (!String.IsNullOrEmpty(txt_TimKiemSoMay.Text))
            {
                SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");
                DataTable dt = Class.datatabase.getData(cmd);
                if (dt.Rows.Count > 0)
                {
                    string sql = "update LichSuBaoDuongXe Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And SoMay = @SoMay1";
                    cmd = new SqlCommand(sql);
                    cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                    cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                    cmd.Parameters.AddWithValue("@Somay1", txt_TimKiemSoMay.Text);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");
                    dt = new DataTable();
                    dt = Class.datatabase.getData(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        string sql = "update LichSuBaoDuongXeTam Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And SoMay = @SoMay1";
                        cmd = new SqlCommand(sql);
                        cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                        cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                        cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                        cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                        cmd.Parameters.AddWithValue("@Somay1", txt_TimKiemSoMay.Text);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thông tin số máy không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }
            //
            if (!String.IsNullOrEmpty(txt_TimKiemBienSo.Text))
            {
                SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                DataTable dt = Class.datatabase.getData(cmd);
                if (dt.Rows.Count > 0)
                {
                    string sql = "update LichSuBaoDuongXe Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And BienSo = @BienSo1";
                    cmd = new SqlCommand(sql);
                    cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                    cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                    cmd.Parameters.AddWithValue("@BienSo1", txt_TimKiemBienSo.Text);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    {
                        MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
                else
                {
                    cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                    dt = new DataTable();
                    dt = Class.datatabase.getData(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        string sql = "update LichSuBaoDuongXeTam Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And BienSo = @BienSo1";
                        cmd = new SqlCommand(sql);
                        cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                        cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                        cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                        cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                        cmd.Parameters.AddWithValue("@BienSo1", txt_TimKiemBienSo.Text);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                        {
                            MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Thông tin biển số không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }
            }

            #endregion Cap nhat thong tin xe
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (rdb1.Checked)
            {
                TimBaoduongdichvu();
            }
            else
            {
                TimBaodinhky();
            }
        }

        /// <summary>
        /// Thêm xe bảo dưỡng vào bảng tạm  ===> Thay thành Thêm trực tiếp vào Lịch sử bảo dưỡng
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX2_Click(object sender, EventArgs e)
        {
            #region "Code lưu bảng tạm"

            if (ChangeOilKM.IsUseChangeOilByKM(Class.CompanyInfo.idcongty) == true && txtSoKm.Text.Trim() == "")
            {
                MessageBox.Show("Bạn cần nhập số km của xe");
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand();

                if (String.IsNullOrEmpty(idKhachHang))
                {
                    if (rdb2.Checked)
                    {
                        if (String.IsNullOrEmpty(txt_SoKhung.Text.Trim()) && String.IsNullOrEmpty(txt_SoMay.Text.Trim()))
                        {
                            MessageBox.Show("Bạn chưa nhập Số khung hoặc Số máy.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txt_SoKhung.Focus();
                            return;
                        }
                        //Thêm khách hàng
                        SqlCommand cmd1 = new SqlCommand(@"insert into KhachHang(idcongty,TenKH,Ngaysinh,dienthoai,diachi,CMND,gioitinh,MaNhomKH,LoaiKH, KhachDenTu) values(@Idcongty,@TenKH,@Ngaysinh,@dienthoai,@diachi,@CMND,@gioitinh,@MaNhomKH,@LoaiKH, @KhachDenTu) select @@Identity");
                        cmd1.Connection = Class.datatabase.getConnection();
                        //chen thong tin khach hang , com tra ve ma khach hang.
                        cmd1.Parameters.AddWithValue("@Idcongty", Class.CompanyInfo.idcongty);
                        cmd1.Parameters.AddWithValue("@TenKH", txt_KhachHang.Text);
                        cmd1.Parameters.AddWithValue("@Ngaysinh", dt_NgaySinh.Value);
                        cmd1.Parameters.AddWithValue("@dienthoai", txt_Phone.Text);
                        cmd1.Parameters.AddWithValue("@diachi", txt_DiaChi.Text);
                        cmd1.Parameters.AddWithValue("@CMND", txt_CMND.Text);
                        cmd1.Parameters.AddWithValue("@gioitinh", Convert.ToString(cbb_GioiTinh.Text));
                        cmd1.Parameters.AddWithValue("@MaNhomKH", Convert.ToString(cbb_NhomKH.Text));
                        cmd1.Parameters.AddWithValue("@LoaiKH", "1");
                        cmd1.Parameters.AddWithValue("@NgayMua", dt_NgayMuaXe.Value);
                        cmd1.Parameters.AddWithValue("@KhachDenTu", Convert.ToString(cboKhachDen.Text));
                        cmd1.Connection.Open();
                        idKhachHang = cmd1.ExecuteScalar().ToString();
                        cmd1.Connection.Close();

                        //Thêm vào xe đã bán
                        SqlCommand cmd2 = new SqlCommand(@"insert into XeDaBan(TenXe, BienSo, NgayBan, IdKhachHang, IdCongTy, SoKhung, SoMay) values(@TenXe, @BienSo, @NgayBan, @IdKhachHang, @IdCongTy, @SoKhung, @SoMay)");
                        cmd2.Connection = Class.datatabase.getConnection();
                        //chen thong tin khach hang , com tra ve ma khach hang.
                        cmd2.Parameters.AddWithValue("@Idcongty", Class.CompanyInfo.idcongty);
                        cmd2.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                        cmd2.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                        cmd2.Parameters.AddWithValue("@NgayBan", dt_NgayMuaXe.Value);
                        cmd2.Parameters.AddWithValue("@IdkhachHang", idKhachHang);
                        cmd2.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                        cmd2.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                        cmd2.Connection.Open();
                        cmd2.ExecuteNonQuery();
                        cmd2.Connection.Close();
                    }
                    if (rdb1.Checked)
                    {
                        if (String.IsNullOrEmpty(txt_BienSo.Text.Trim()) && String.IsNullOrEmpty(txt_Phone.Text.Trim()))
                        {
                            MessageBox.Show("Bạn chưa nhập Biển số hoặc Số điện thoại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txt_BienSo.Focus();

                            return;
                        }
                        if (txt_TenXe.Text.Trim() == "")
                        {
                            MessageBox.Show("Bạn chưa nhập tên xe", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        SqlCommand cmd1 = new SqlCommand(@"insert into KhachHang(idcongty,TenKH,Ngaysinh,dienthoai,diachi,CMND,gioitinh,MaNhomKH,LoaiKH) values(@Idcongty,@TenKH,@Ngaysinh,@dienthoai,@diachi,@CMND,@gioitinh,@MaNhomKH,@LoaiKH) select @@Identity");
                        cmd1.Connection = Class.datatabase.getConnection();
                        //chen thong tin khach hang , com tra ve ma khach hang.
                        cmd1.Parameters.AddWithValue("@Idcongty", Class.CompanyInfo.idcongty);
                        cmd1.Parameters.AddWithValue("@TenKH", txt_KhachHang.Text);
                        cmd1.Parameters.AddWithValue("@Ngaysinh", dt_NgaySinh.Value);
                        cmd1.Parameters.AddWithValue("@dienthoai", txt_Phone.Text);
                        cmd1.Parameters.AddWithValue("@diachi", txt_DiaChi.Text);
                        cmd1.Parameters.AddWithValue("@CMND", txt_CMND.Text);
                        cmd1.Parameters.AddWithValue("@gioitinh", Convert.ToString(cbb_GioiTinh.Text));
                        cmd1.Parameters.AddWithValue("@MaNhomKH", Convert.ToString(cbb_NhomKH.Text));
                        cmd1.Parameters.AddWithValue("@LoaiKH", "2");
                        cmd1.Connection.Open();
                        idKhachHang = cmd1.ExecuteScalar().ToString();
                        cmd1.Connection.Close();
                    }
                }

                string LoaiBaoDuong = ((KeyValuePair<string, string>)cbbMaintenanceTypes.SelectedItem).Key;
                string sql = @"insert into LichSuBaoDuongXeTam(idcuahang,idkhachhang,idcongty,tenxe,bienso,somay,sokhung,ngaybaoduong,NgayGiaoXe,solan,GhiChu,SoKm,LoaiBaoDuong) values(@idcuahang, @idkhachhang, @idcongty, @tenxe, @bienso,@somay,@sokhung,@ngaybaoduong,@NgayGiaoXe, @solan,@GhiChu,@SoKm,@LoaiBaoDuong)";
                cmd = new SqlCommand(sql);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idcuahang", Class.EmployeeInfo.IdCuaHang);
                cmd.Parameters.AddWithValue("@idkhachhang", idKhachHang);
                cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@tenxe", txt_TenXe.Text);
                cmd.Parameters.AddWithValue("@bienso", txt_BienSo.Text);
                cmd.Parameters.AddWithValue("@somay", txt_SoMay.Text);
                cmd.Parameters.AddWithValue("@sokhung", txt_SoKhung.Text);
                cmd.Parameters.AddWithValue("@ngaybaoduong", dt_NgayBaoDuong.Value);
                cmd.Parameters.AddWithValue("@NgayGiaoXe", dt_NgayGiaoXe.Value);
                cmd.Parameters.AddWithValue("@GhiChu", txt_GhiChu.Text);
                cmd.Parameters.AddWithValue("@SoKm", txtSoKm.Text);
                cmd.Parameters.AddWithValue("@LoaiBaoDuong", LoaiBaoDuong);
                if (String.IsNullOrEmpty(txt_LanBD.Text))
                {
                    cmd.Parameters.AddWithValue("@solan", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@solan", txt_LanBD.Text);
                }
                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Nhập xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadXeDangBaoDuong();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Nhập xe bảo dưỡng thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                cmd = new SqlCommand("Select * from LichSuBaoDuongXeTam Where IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang");
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                Class.tblBaoDuong.lsBaoduongxetam = Class.datatabase.getData(cmd);
                //dgvDsXeDangBaoDuong1.DataSource = dtxeBaoDuong2;
                //dgvDsXeDangBaoDuong2.DataSource = dtxeBaoDuong2;
                //dgvDsXeDangBaoDuong3.DataSource = dtxeBaoDuong2;
                //if(!KiemTraTenTabConTrol("&Thay phụ tùng"))
                //{
                //   UcThayPhuTung ucThayPT = new UcThayPhuTung();
                // ucThayPT.LoadXeDangBaoDuong();
                //ucThayPT.Show();
                //}
                // this.sender("gui");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            #endregion "Code lưu bảng tạm"

            #region "Code lưu trực tiếp bảng chính"

            //string i = Convert.ToString(cboKhachDen.SelectedIndex);
            //try
            //{
            //    SqlCommand cmd = new SqlCommand();
            //    if (txt_BienSo.Text == "")
            //    {
            //        MessageBox.Show("Mời nhập biển số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //        return;
            //    }
            //    if (String.IsNullOrEmpty(idKhachHang))
            //    {
            //        SqlCommand cmd1 = new SqlCommand(@"insert into KhachHang(idcongty,TenKH,Ngaysinh,dienthoai,diachi,CMND,gioitinh,MaNhomKH,LoaiKH, KhachDenTu) values(@Idcongty,@TenKH,@Ngaysinh,@dienthoai,@diachi,@CMND,@gioitinh,@MaNhomKH,@LoaiKH, @KhachDenTu) select @@Identity");
            //        cmd1.Connection = Class.datatabase.getConnection();
            //        //chen thong tin khach hang , com tra ve ma khach hang.
            //        cmd1.Parameters.AddWithValue("@Idcongty", Class.CompanyInfo.idcongty);
            //        cmd1.Parameters.AddWithValue("@TenKH", txt_KhachHang.Text);
            //        cmd1.Parameters.AddWithValue("@Ngaysinh", dt_NgaySinh.Value);
            //        cmd1.Parameters.AddWithValue("@dienthoai", txt_Phone.Text);
            //        cmd1.Parameters.AddWithValue("@diachi", txt_DiaChi.Text);
            //        cmd1.Parameters.AddWithValue("@CMND", txt_CMND.Text);
            //        cmd1.Parameters.AddWithValue("@gioitinh", Convert.ToString(cbb_GioiTinh.Text));
            //        cmd1.Parameters.AddWithValue("@MaNhomKH", Convert.ToString(cbb_NhomKH.Text));
            //        cmd1.Parameters.AddWithValue("@LoaiKH", "2");
            //        cmd1.Parameters.AddWithValue("@KhachDenTu", Convert.ToString(cboKhachDen.SelectedIndex));
            //        cmd1.Connection.Open();
            //        idKhachHang = cmd1.ExecuteScalar().ToString();
            //        cmd1.Connection.Close();

            //    }

            //    string sql = @"insert into LichSuBaoDuongXe(idcuahang,idkhachhang,idcongty,tenxe,bienso,somay,sokhung,ngaybaoduong, NgayGiaoXe,solan, GhiChu) values(@idcuahang, @idkhachhang, @idcongty, @tenxe, @bienso,@somay,@sokhung,@ngaybaoduong, @NgayGiaoXe, @solan, @GhiChu)";
            //    cmd = new SqlCommand(sql);
            //    cmd.Parameters.Clear();
            //    cmd.Parameters.AddWithValue("@idcuahang", Class.EmployeeInfo.IdCuaHang);
            //    cmd.Parameters.AddWithValue("@idkhachhang", idKhachHang);
            //    cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            //    cmd.Parameters.AddWithValue("@tenxe", txt_TenXe.Text);
            //    cmd.Parameters.AddWithValue("@bienso", txt_BienSo.Text);
            //    cmd.Parameters.AddWithValue("@somay", txt_SoMay.Text);
            //    cmd.Parameters.AddWithValue("@sokhung", txt_SoKhung.Text);
            //    cmd.Parameters.AddWithValue("@ngaybaoduong", dt_NgayBaoDuong.Value);
            //    cmd.Parameters.AddWithValue("@NgayGiaoXe", dt_NgayGiaoXe.Value);
            //    cmd.Parameters.AddWithValue("@GhiChu", txt_GhiChu.Text);
            //    if (String.IsNullOrEmpty(txt_LanBD.Text))
            //    {
            //        cmd.Parameters.AddWithValue("@solan", "1");
            //    }
            //    else
            //    {
            //        cmd.Parameters.AddWithValue("@solan", txt_LanBD.Text);
            //    }
            //    if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
            //    {
            //        MessageBox.Show("Nhập xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //        Kiemtra();
            //        //Đóng kết nối
            //        cmd.Connection.Close();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Nhập xe bảo dưỡng thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //}

            #endregion "Code lưu trực tiếp bảng chính"
        }

        //SuperTabControl spt = new SuperTabControl();
        //private bool KiemTraTenTabConTrol(string name)
        //{
        //    for (int i = 0; i < spt.Tabs.Count; i++)
        //    {
        //        if (spt.Tabs[i].Text == name)
        //        {
        //            spt.SelectedTabIndex = i;
        //            return true;
        //        }
        //    }
        //    return false;
        //}

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonX3_Click(object sender, EventArgs e)
        {
            try
            {
                #region capnhatthongtinkhachhang

                string tenkh = txt_KhachHang.Text;
                DateTime ngaysinh = dt_NgaySinh.Value;
                string gioitinh = cbb_GioiTinh.Text.ToString();
                string dienthoai = txt_Phone.Text;
                string diachi = txt_DiaChi.Text;
                string socmnd = txt_CMND.Text;
                string sosbh = txt_SoSBH.Text;
                string ghichu = txt_GhiChu.Text;
                // string nhomkh = cbb_NhomKH.SelectedItem.ToString();
                SqlCommand com = new SqlCommand();
                com.CommandText =// "Update KhachHang set TenKH='"+ tenkh + "',GioiTinh='"+gioitinh+"',NgaySinh='"+ngaysinh+"',DienThoai='"+dienthoai+"',Diachi='"+diachi+"',CMND='"+socmnd+"',SoSBH='"+sosbh+"' where IdKhachHang='"+idKhachHang+"' And IdCongty='"+Class.CompanyInfo.idcongty+"'";
                "update KhachHang set TenKH=@hoten,GioiTinh=@gioitinh,NgaySinh=@ngaysinh,DienThoai=@dienthoai,Diachi=@diachi,CMND=@cmnd,SoSBH=@sosbh, KhachDenTu=@KhachDenTu, NgayMua=@NgayMuaXe where IdKhachHang=@idkhachhang And IdCongty=@idconty";
                com.Parameters.Clear();
                com.Parameters.AddWithValue("@hoten", tenkh);
                com.Parameters.AddWithValue("@gioitinh", gioitinh);
                com.Parameters.AddWithValue("@ngaysinh", ngaysinh);
                com.Parameters.AddWithValue("@dienthoai", dienthoai);
                com.Parameters.AddWithValue("@diachi", diachi);
                com.Parameters.AddWithValue("@cmnd", socmnd);
                com.Parameters.AddWithValue("@sosbh", sosbh);
                com.Parameters.AddWithValue("@idkhachhang", idKhachHang);
                com.Parameters.AddWithValue("@idconty", Class.CompanyInfo.idcongty);
                if (rdb2.Checked)
                {
                    com.Parameters.AddWithValue("@KhachDenTu", cboKhachDen.Text);
                    com.Parameters.AddWithValue("@NgayMuaXe", dt_NgayMuaXe.Value);
                }
                if (rdb1.Checked)
                {
                    com.Parameters.AddWithValue("@KhachDenTu", "");
                    com.Parameters.AddWithValue("@NgayMuaXe", "");
                }

                if (Class.datatabase.ExcuteNonQuery(com) > 0)
                {
                    MessageBox.Show("Cập nhật thông tin khách thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Cập nhật thông tin khách hàng thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                #endregion capnhatthongtinkhachhang

                if (rdb1.Checked)
                {
                    #region Cap nhat thong tin xe cũ

                    //if (!String.IsNullOrEmpty(txt_TimKiemSoMay.Text))
                    //{
                    //    SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");
                    //    DataTable dt = Class.datatabase.getData(cmd);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        string sql = "update LichSuBaoDuongXe Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And SoMay = @SoMay1";
                    //        cmd = new SqlCommand(sql);
                    //        cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    //        cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    //        cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                    //        cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                    //        cmd.Parameters.AddWithValue("@Somay1", txt_TimKiemSoMay.Text);
                    //        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    //        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    //        {
                    //            MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            return;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");
                    //        dt = new DataTable();
                    //        dt = Class.datatabase.getData(cmd);
                    //        if (dt.Rows.Count > 0)
                    //        {
                    //            string sql = "update LichSuBaoDuongXeTam Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And SoMay = @SoMay1";
                    //            cmd = new SqlCommand(sql);
                    //            cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    //            cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    //            cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                    //            cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                    //            cmd.Parameters.AddWithValue("@Somay1", txt_TimKiemSoMay.Text);
                    //            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    //            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    //            {
                    //                MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                return;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Thông tin số máy không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            return;
                    //        }
                    //    }
                    //}
                    ////
                    //if (!String.IsNullOrEmpty(txt_TimKiemBienSo.Text))
                    //{
                    //    SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                    //    DataTable dt = Class.datatabase.getData(cmd);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        string sql = "update LichSuBaoDuongXe Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And BienSo = @BienSo1";
                    //        cmd = new SqlCommand(sql);
                    //        cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    //        cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    //        cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                    //        cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                    //        cmd.Parameters.AddWithValue("@BienSo1", txt_TimKiemBienSo.Text);
                    //        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    //        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    //        {
                    //            MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            return;
                    //        }
                    //    }
                    //    else
                    //    {
                    //        cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                    //        dt = new DataTable();
                    //        dt = Class.datatabase.getData(cmd);
                    //        if (dt.Rows.Count > 0)
                    //        {
                    //            string sql = "update LichSuBaoDuongXeTam Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And BienSo = @BienSo1";
                    //            cmd = new SqlCommand(sql);
                    //            cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    //            cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    //            cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                    //            cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                    //            cmd.Parameters.AddWithValue("@BienSo1", txt_TimKiemBienSo.Text);
                    //            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    //            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    //            {
                    //                MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //                return;
                    //            }
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Thông tin biển số không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //            return;
                    //        }
                    //    }
                    //}

                    #endregion Cap nhat thong tin xe cũ

                    #region "Cập nhật thông tin xe mới"

                    if (!String.IsNullOrEmpty(txt_TimKiemBienSo.Text))
                    {
                        SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                        DataTable dt = Class.datatabase.getData(cmd);
                        if (dt.Rows.Count > 0)
                        {
                            string sql = "update LichSuBaoDuongXe Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And BienSo = @BienSo1";
                            cmd = new SqlCommand(sql);
                            cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                            cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                            cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                            cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                            cmd.Parameters.AddWithValue("@BienSo1", txt_TimKiemBienSo.Text);
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                            {
                                MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                        else
                        {
                            cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                            dt = new DataTable();
                            dt = Class.datatabase.getData(cmd);
                            if (dt.Rows.Count > 0)
                            {
                                string sql = "update LichSuBaoDuongXeTam Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And BienSo = @BienSo1";
                                cmd = new SqlCommand(sql);
                                cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                                cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                                cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                                cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                                cmd.Parameters.AddWithValue("@BienSo1", txt_TimKiemBienSo.Text);
                                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                                {
                                    MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                MessageBox.Show("Thông tin biển số không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }

                    #endregion "Cập nhật thông tin xe mới"
                }
                if (rdb2.Checked)
                {
                    #region Cap nhat thong tin xe cũ

                    //if (!String.IsNullOrEmpty(txt_TimKiemSoMay.Text))
                    //{
                    //    SqlCommand cmd2 = new SqlCommand("update XeDaBan Set TenXe = @TenXe, BienSo = @BienSo Where IDCongTy = @IdCongTy And SoMay = @SoMay1");
                    //    cmd2.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    //    cmd2.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    //    cmd2.Parameters.AddWithValue("@Somay1", txt_TimKiemSoMay.Text);
                    //    cmd2.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                    //    if (Class.datatabase.ExcuteNonQuery(cmd2) > 0)
                    //    {
                    //        SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");
                    //        DataTable dt = Class.datatabase.getData(cmd);
                    //        if (dt.Rows.Count > 0)
                    //        {
                    //            string sql = "update LichSuBaoDuongXe Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And SoMay = @SoMay1";
                    //            cmd = new SqlCommand(sql);
                    //            cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    //            cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    //            cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                    //            cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                    //            cmd.Parameters.AddWithValue("@Somay1", txt_TimKiemSoMay.Text);
                    //            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    //            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    //            {
                    //            }
                    //        }
                    //        else
                    //        {
                    //            cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");
                    //            dt = new DataTable();
                    //            dt = Class.datatabase.getData(cmd);
                    //            if (dt.Rows.Count > 0)
                    //            {
                    //                string sql = "update LichSuBaoDuongXeTam Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And SoMay = @SoMay1";
                    //                cmd = new SqlCommand(sql);
                    //                cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    //                cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    //                cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                    //                cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                    //                cmd.Parameters.AddWithValue("@Somay1", txt_TimKiemSoMay.Text);
                    //                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    //                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    //                {
                    //                }
                    //            }

                    //        }
                    //        MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return;
                    //    }
                    //}
                    ////
                    //if (!String.IsNullOrEmpty(txt_TimKiemBienSo.Text))
                    //{
                    //    SqlCommand cmd2 = new SqlCommand("update XeDaBan Set TenXe = @TenXe, BienSo = @BienSo Where IDCongTy = @IdCongTy And BienSo = @BienSo1");
                    //    cmd2.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    //    cmd2.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    //    cmd2.Parameters.AddWithValue("@BienSo1", txt_TimKiemBienSo.Text);
                    //    cmd2.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                    //    if (Class.datatabase.ExcuteNonQuery(cmd2) > 0)
                    //    {
                    //        SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                    //        DataTable dt = Class.datatabase.getData(cmd);
                    //        if (dt.Rows.Count > 0)
                    //        {
                    //            string sql = "update LichSuBaoDuongXe Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And BienSo = @BienSo1";
                    //            cmd = new SqlCommand(sql);
                    //            cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    //            cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    //            cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                    //            cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                    //            cmd.Parameters.AddWithValue("@BienSo1", txt_TimKiemBienSo.Text);
                    //            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    //            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    //            {
                    //            }
                    //        }
                    //        else
                    //        {
                    //            cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and BienSo like '" + txt_TimKiemBienSo.Text.Trim() + "'");
                    //            dt = new DataTable();
                    //            dt = Class.datatabase.getData(cmd);
                    //            if (dt.Rows.Count > 0)
                    //            {
                    //                string sql = "update LichSuBaoDuongXeTam Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And BienSo = @BienSo1";
                    //                cmd = new SqlCommand(sql);
                    //                cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                    //                cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                    //                cmd.Parameters.AddWithValue("@SoKhung", txt_SoKhung.Text);
                    //                cmd.Parameters.AddWithValue("@SoMay", txt_SoMay.Text);
                    //                cmd.Parameters.AddWithValue("@BienSo1", txt_TimKiemBienSo.Text);
                    //                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    //                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    //                {
                    //                }
                    //            }

                    //        }

                    //        MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return;
                    //    }
                    //}

                    #endregion Cap nhat thong tin xe cũ

                    #region Cập nhật thông tin xe mới

                    if (!String.IsNullOrEmpty(txt_TimKiemSoMay.Text))
                    {
                        SqlCommand cmd2 = new SqlCommand("update XeDaBan Set TenXe = @TenXe, BienSo = @BienSo Where IDCongTy = @IdCongTy And SoMay = @SoMay1");
                        cmd2.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                        cmd2.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                        cmd2.Parameters.AddWithValue("@Somay1", txt_TimKiemSoMay.Text);
                        cmd2.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                        if (Class.datatabase.ExcuteNonQuery(cmd2) > 0)
                        {
                            SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");
                            DataTable dt = Class.datatabase.getData(cmd);
                            if (dt.Rows.Count > 0)
                            {
                                string sql = "update LichSuBaoDuongXe Set TenXe = @TenXe, BienSo = @BienSo Where IDCongTy = @IdCongTy And SoMay = @SoMay1";
                                cmd = new SqlCommand(sql);
                                cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                                cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                                cmd.Parameters.AddWithValue("@Somay1", txt_TimKiemSoMay.Text);
                                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                                {
                                    MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    return;
                                }
                            }
                            else
                            {
                                cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and SoMay like '" + txt_TimKiemSoMay.Text.Trim() + "'");
                                dt = new DataTable();
                                dt = Class.datatabase.getData(cmd);
                                if (dt.Rows.Count > 0)
                                {
                                    string sql = "update LichSuBaoDuongXeTam Set TenXe = @TenXe, BienSo = @BienSo Where IDCongTy = @IdCongTy And SoMay = @SoMay1";
                                    cmd = new SqlCommand(sql);
                                    cmd.Parameters.AddWithValue("@TenXe", txt_TenXe.Text);
                                    cmd.Parameters.AddWithValue("@BienSo", txt_BienSo.Text);
                                    cmd.Parameters.AddWithValue("@Somay1", txt_TimKiemSoMay.Text);
                                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                                    {
                                        MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        return;
                                    }
                                }
                            }
                            MessageBox.Show("Cập nhật thông tin xe bảo dưỡng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    #endregion Cập nhật thông tin xe mới
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdb2_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb2.Checked)
            {
                //txt_SoKhung.ReadOnly = true;
                //txt_SoMay.ReadOnly = true;
                lblNgayMuaXe.Visible = true;
                dt_NgayMuaXe.Visible = true;
                lblKhachDen.Visible = true;
                cboKhachDen.Visible = true;

                txt_TimKiemSoKhung.Enabled = true;
                txt_TimKiemSoMay.Enabled = true;
                txt_TimKiemBienSo.Enabled = false;
                txt_TimKiemSDT.Enabled = false;

                txt_TimKiemSoKhung.Clear();
                txt_TimKiemSoMay.Clear();
                txt_TimKiemSDT.Clear();
                txt_TimKiemBienSo.Clear();

                txt_TimKiemSoMay.Select();

                rdb1.Checked = false;

                //Auto_BienSo();

                ResetForm();
            }
            else
            {
                txt_SoKhung.ReadOnly = false;
                txt_SoMay.ReadOnly = false;
                lblNgayMuaXe.Visible = false;
                dt_NgayMuaXe.Visible = false;
                lblKhachDen.Visible = false;
                cboKhachDen.Visible = false;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rdb1_CheckedChanged(object sender, EventArgs e)
        {
            if (rdb1.Checked)
            {
                txt_TimKiemSoKhung.Enabled = false;
                txt_TimKiemSoMay.Enabled = false;
                txt_TimKiemBienSo.Enabled = true;
                txt_TimKiemSDT.Enabled = true;

                txt_TimKiemSoKhung.Clear();
                txt_TimKiemSoMay.Clear();
                txt_TimKiemSDT.Clear();
                txt_TimKiemBienSo.Clear();

                txt_TimKiemBienSo.Select();

                rdb2.Checked = false;

                Auto_BienSo();

                ResetForm();
            }
        }

        /// <summary>
        /// Reset Form
        /// </summary>
        private void ResetForm()
        {
            txt_KhachHang.Clear();
            dt_NgaySinh.Text = Convert.ToString(DateTime.Now);
            cbb_GioiTinh.Text = "";
            txt_Phone.Clear();
            txt_DiaChi.Clear();
            cboKhachDen.Text = "";
            dt_NgayMuaXe.Text = Convert.ToString(DateTime.Now);
            cbb_NhomKH.Text = "";
            txt_CMND.Clear();
            txt_SoSBH.Clear();
            txt_GhiChu.Clear();
            txt_TenXe.Clear();
            txt_BienSo.Clear();
            txt_SoKhung.Clear();
            txt_SoMay.Clear();
            txtphieu.Clear();
            dt_NgayBaoDuong.Text = Convert.ToString(DateTime.Now);
            dt_NgayGiaoXe.Text = Convert.ToString(DateTime.Now);
            txt_LanBD.Clear();

            grv_LichSuBaoDuong.DataSource = null;
        }

        /// <summary>
        /// Lấy thông tin xe theo số máy theo sự kiện textchanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_TimKiemSoMay_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_TimKiemSoMay.Text != "")
                {
                    SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
                    DataTable dt = new DataTable();
                    myCon.Open();
                    string sql = "select TenXe, SoKhung, SoMay from XeDaBan a where a.IdCongTy= '" + Convert.ToInt32(Class.CompanyInfo.idcongty) + "' and a.SoMay like '%" + txt_TimKiemSoMay.Text + "%'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, myCon);

                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dt;
                        dataGridView1.Visible = true;
                    }
                    else
                    {
                        dataGridView1.DataSource = null;
                        dataGridView1.Visible = false;
                    }
                    myCon.Close();
                }
                else
                {
                    dataGridView1.DataSource = null;
                    dataGridView1.Visible = false;
                }
            }
            catch { dataGridView1.DataSource = null; dataGridView1.Visible = false; }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_TimKiemSoMay.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                dataGridView1.Visible = false;
            }
            catch { }
        }

        private void txt_TimKiemSoMay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txt_TimKiemSoMay.Text = "";

                dataGridView1.DataSource = null;
                dataGridView1.Visible = false;
            }
        }

        /// <summary>
        /// Lấy thông tin xe theo số khung theo sự kiện textchanged
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_TimKiemSoKhung_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txt_TimKiemSoKhung.Text != "")
                {
                    SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
                    DataTable dt = new DataTable();
                    myCon.Open();
                    string sql = "select TenXe, SoKhung, SoMay from XeDaBan a where a.IdCongTy= '" + Convert.ToInt32(Class.CompanyInfo.idcongty) + "' and a.SoKhung like '%" + txt_TimKiemSoKhung.Text + "%'";
                    SqlDataAdapter da = new SqlDataAdapter(sql, myCon);

                    dt.Clear();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        dataGridView2.DataSource = dt;
                        dataGridView2.Visible = true;
                    }
                    else
                    {
                        dataGridView2.DataSource = null;
                        dataGridView2.Visible = false;
                    }
                    myCon.Close();
                }
                else
                {
                    dataGridView2.DataSource = null;
                    dataGridView2.Visible = false;
                }
            }
            catch { dataGridView2.DataSource = null; dataGridView2.Visible = false; }
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txt_TimKiemSoKhung.Text = dataGridView2.Rows[e.RowIndex].Cells[1].Value.ToString();
                dataGridView2.Visible = false;
            }
            catch { }
        }

        private void txt_TimKiemSoKhung_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                txt_TimKiemSoKhung.Text = "";

                dataGridView2.DataSource = null;
                dataGridView2.Visible = false;
            }
        }

        private void txt_TimKiemSoMay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    int rowIndex = dataGridView1.CurrentCell.RowIndex;

                    txt_TimKiemSoMay.Text = dataGridView1.Rows[rowIndex].Cells[2].Value.ToString();
                    dataGridView1.Visible = false;

                    txt_TimKiemSoMay.SelectAll();
                    txt_TimKiemSoMay.Focus();

                    TimBaodinhky();
                }
                catch { }
            }
        }

        private void txt_TimKiemSoKhung_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)13)
            {
                try
                {
                    int rowIndex = dataGridView2.CurrentCell.RowIndex;

                    txt_TimKiemSoKhung.Text = dataGridView2.Rows[rowIndex].Cells[1].Value.ToString();
                    dataGridView2.Visible = false;

                    txt_TimKiemSoKhung.SelectAll();
                    txt_TimKiemSoKhung.Focus();

                    TimBaodinhky();
                }
                catch { }
            }
        }

        private void txt_TimKiemSoMay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Up))
            {
                MoveUp(dataGridView1);
            }
            if (e.KeyCode.Equals(Keys.Down))
            {
                MoveDown(dataGridView1);
            }
            e.Handled = true;
        }

        private void MoveDown(DataGridView dgv)
        {
            try
            {
                if (dgv.CurrentRow == null) return;
                if (dgv.CurrentRow.Index + 1 <= dgv.Rows.Count - 1)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentRow.Index + 1].Cells[0];
                    dgv.Rows[dgv.CurrentCell.RowIndex].Selected = true;
                }
            }
            catch { }
        }

        private static void MoveUp(DataGridView dgv)
        {
            try
            {
                if (dgv.CurrentRow == null) return;
                if (dgv.CurrentRow.Index - 1 >= 0)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentRow.Index - 1].Cells[0];
                    dgv.Rows[dgv.CurrentCell.RowIndex].Selected = true;
                }
            }
            catch { }
        }

        private void txt_TimKiemSoKhung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Up))
            {
                MoveUp(dataGridView2);
            }
            if (e.KeyCode.Equals(Keys.Down))
            {
                MoveDown(dataGridView2);
            }
            e.Handled = true;
        }

        private void txt_TimKiemBienSo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonX1_Click(sender, new EventArgs());
        }

        private void txt_TimKiemSDT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                buttonX1_Click(sender, new EventArgs());
        }

        private void grv_LichSuBaoDuong_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                grv_LichSuBaoDuong.ContextMenuStrip = contextMenuStrip;

                try
                {
                    idBaoDuong3 = Convert.ToString(grv_LichSuBaoDuong.Rows[e.RowIndex].Cells["IDBaoDuong"].Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    idBaoDuong3 = "";
                }
            }
            else
            {
                grv_LichSuBaoDuong.ContextMenuStrip = null;
                idBaoDuong3 = "";
            }

            Class.SelectedCustomer._idbaoduong = idBaoDuong3;
        }

        private void ToolStripMenuItemInPhieuBaoDuong_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Class.SelectedCustomer._idbaoduong))
            { 
                MessageBox.Show("Lần bảo dưỡng không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (Convert.ToInt64(Class.CompanyInfo.idcongty) != 31)
            {
                FrmPhieuSuaChuaThangLoi frm = new FrmPhieuSuaChuaThangLoi();
                frm.ShowDialog();
            }
            else
            {
                frmPhieuSuaChuaTM98 frm = new frmPhieuSuaChuaTM98();
                frm.ShowDialog();
            }
        }

        private void ToolStripMenuItemInPhieuBaoGia_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Class.SelectedCustomer._idbaoduong))
            {
                MessageBox.Show("Lần bảo dưỡng không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            #region Lấy thông tin báo giá
            SqlCommand cmd = new SqlCommand();
            DataTable tableBaoGia = new DataTable();

            cmd.CommandText = @"SELECT IdBaoGia, IdKhachHang, IdBaoDuong
                                FROM BaoGiaSuaChua WHERE IdBaoDuong = @IdBaoDuong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);

            tableBaoGia = Class.datatabase.getData(cmd);
            #endregion

            if (tableBaoGia.Rows.Count <= 0)
            {
                MessageBox.Show("Thông tin báo giá không tồn tại!\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FrmInBangBaoGia frmInBaoGia = new FrmInBangBaoGia();
            frmInBaoGia.IdBaoDuong = Convert.ToInt64(Class.SelectedCustomer._idbaoduong);

            frmInBaoGia.ShowDialog();
        }

        private void ToolStripMenuItemSuaLichSuBaoDuong_Click(object sender, EventArgs e)
        {
            FrmCapNhatLichSuBaoDuong frmCapNhatBaoDuong = new FrmCapNhatLichSuBaoDuong();
            frmCapNhatBaoDuong.ShowDialog();
        }
    }
}