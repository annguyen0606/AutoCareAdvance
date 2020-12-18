using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls.BaoDuong
{
    public partial class UcTiepNhanDongA : UserControl
    {
        private string idKhachHang = "";
        private string idBaoDuong = "";
        private Class.ChangeOilByKM ChangeOilKM = new Class.ChangeOilByKM();

        #region UcTiepNhanDongA

        public UcTiepNhanDongA()
        {
            InitializeComponent();
            grvLichSuBaoDuong.AutoGenerateColumns = false;
        }

        #endregion UcTiepNhanDongA

        #region UcTiepNhanDongA_Load

        private void UcTiepNhanDongA_Load(object sender, EventArgs e)
        {
            Auto_BienSo();
            LoadXeDangBaoDuong();
        }

        #endregion UcTiepNhanDongA_Load

        #region Autocomplete Biển số

        private void Auto_BienSo()
        {
            AutoCompleteStringCollection BienSoXe = new AutoCompleteStringCollection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.datatabase.getConnection();
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;

            cmd.CommandText = "select BienSo, SoMay from LichSuBaoDuongXe where IdCongty=" + Class.CompanyInfo.idcongty;
            cmd.Connection.Open();
            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    BienSoXe.Add(reader["BienSo"].ToString());
                }
            }
            cmd.Connection.Close();

            txtTimKiemBienSoXe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtTimKiemBienSoXe.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtTimKiemBienSoXe.AutoCompleteCustomSource = BienSoXe;
        }

        #endregion Autocomplete Biển số

        #region Load xe đang bảo dưỡng

        private void LoadXeDangBaoDuong()
        {
            string sql = "Select * from LichSuBaoDuongXeTam Where IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
            Class.tblBaoDuong.lsBaoduongxetam = Class.datatabase.getData(cmd);
        }

        #endregion Load xe đang bảo dưỡng

        #region Tìm kiếm xe

        private void TimKiemXe(string _BienSoXe, string _SoDienThoai)
        {
            idKhachHang = "";
            grvLichSuBaoDuong.DataSource = null;

            txtDienThoaiKhachHang.Clear();
            txtTenXe.Clear();
            txtBienSoXe.Clear();
            txtGhiChuBaoDuong.Clear();
            txtLanBaoDuong.Clear();

            if (String.IsNullOrEmpty(txtTimKiemBienSoXe.Text) && String.IsNullOrEmpty(txtTimKiemSoDienThoai.Text))
            {
                MessageBox.Show("Cận nhập vào thông tin Biển số hoặc Số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtTimKiemBienSoXe.Focus();

                return;
            }

            #region Tìm kiếm theo biển số xe

            if (String.IsNullOrEmpty(_SoDienThoai) && !String.IsNullOrEmpty(_BienSoXe))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and lsbdx.BienSo like @TKBienSo");
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@TKBienSo", _BienSoXe.Trim());

                    DataTable dt = Class.datatabase.getData(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            txtDienThoaiKhachHang.Text = dt.Rows[0]["DienThoai"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txtDienThoaiKhachHang.Text = ""; }

                        try { txtTenXe.Text = dt.Rows[0]["TenXe"].ToString(); }
                        catch { txtTenXe.Text = ""; }

                        try { txtBienSoXe.Text = dt.Rows[0]["BienSo"].ToString(); }
                        catch { txtBienSoXe.Text = ""; }

                        string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                                        + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                                        + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                                        + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                                        + " Where lsbdx.IdCongTy=@IdCongTy and BienSo like @TKBienSo";
                        cmd = new SqlCommand(sql);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKBienSo", _BienSoXe.Trim());

                        DataTable dt_LichSu = Class.datatabase.getData(cmd);

                        grvLichSuBaoDuong.DataSource = dt_LichSu;
                        int solan = 0;
                        solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
                        txtLanBaoDuong.Text = Convert.ToString(solan);
                    }
                    else
                    {
                        cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and BienSo like @TKBienSo");
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKBienSo", _BienSoXe.Trim());

                        dt = Class.datatabase.getData(cmd);

                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("Thông tin biển số không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        try
                        {
                            txtDienThoaiKhachHang.Text = dt.Rows[0]["DienThoai"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txtDienThoaiKhachHang.Text = ""; }

                        try { txtTenXe.Text = dt.Rows[0]["TenXe"].ToString(); }
                        catch { txtTenXe.Text = ""; }

                        try { txtBienSoXe.Text = dt.Rows[0]["BienSo"].ToString(); }
                        catch { txtBienSoXe.Text = ""; }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
                }
            }

            #endregion Tìm kiếm theo biển số xe

            #region Tìm kiếm theo số điện thoại

            if (!String.IsNullOrEmpty(_SoDienThoai) && String.IsNullOrEmpty(_BienSoXe))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai");
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@TKSoDienThoai", _SoDienThoai.Trim());

                    DataTable dt = Class.datatabase.getData(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            txtDienThoaiKhachHang.Text = dt.Rows[0]["DienThoai"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txtDienThoaiKhachHang.Text = ""; }

                        try { txtTenXe.Text = dt.Rows[0]["TenXe"].ToString(); }
                        catch { txtTenXe.Text = ""; }

                        try { txtBienSoXe.Text = dt.Rows[0]["BienSo"].ToString(); }
                        catch { txtBienSoXe.Text = ""; }

                        string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                                        + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                                        + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                                        + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                                        + " left outer join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang"
                                        + " Where lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai";
                        cmd = new SqlCommand(sql);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKSoDienThoai", _SoDienThoai.Trim());

                        DataTable dt_LichSu = Class.datatabase.getData(cmd);

                        grvLichSuBaoDuong.DataSource = dt_LichSu;
                        int solan = 0;
                        solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
                        txtLanBaoDuong.Text = Convert.ToString(solan);
                    }

                    #region Tìm trong bảng tạm

                    else
                    {
                        cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and BienSo like @TKSoDienThoai");
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKSoDienThoai", _SoDienThoai.Trim());

                        dt = Class.datatabase.getData(cmd);

                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("Thông tin số điện thoại không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        try
                        {
                            txtDienThoaiKhachHang.Text = dt.Rows[0]["DienThoai"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txtDienThoaiKhachHang.Text = ""; }

                        try { txtTenXe.Text = dt.Rows[0]["TenXe"].ToString(); }
                        catch { txtTenXe.Text = ""; }

                        try { txtBienSoXe.Text = dt.Rows[0]["BienSo"].ToString(); }
                        catch { txtBienSoXe.Text = ""; }
                    }

                    #endregion Tìm trong bảng tạm
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
                }
            }

            #endregion Tìm kiếm theo số điện thoại

            #region Tìm kiếm theo số điện thoại và biển số

            if (!String.IsNullOrEmpty(_BienSoXe) && !String.IsNullOrEmpty(_SoDienThoai))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai and BienSo like @TKBienSo");
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@TKSoDienThoai", _SoDienThoai.Trim());
                    cmd.Parameters.AddWithValue("@TKBienSo", _BienSoXe.Trim());

                    DataTable dt = Class.datatabase.getData(cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            txtDienThoaiKhachHang.Text = dt.Rows[0]["DienThoai"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txtDienThoaiKhachHang.Text = ""; }

                        try { txtTenXe.Text = dt.Rows[0]["TenXe"].ToString(); }
                        catch { txtTenXe.Text = ""; }

                        try { txtBienSoXe.Text = dt.Rows[0]["BienSo"].ToString(); }
                        catch { txtBienSoXe.Text = ""; }

                        string sql = "select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu from LichSuBaoDuongXe lsbdx"
                                        + " left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong"
                                        + " left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong"
                                        + " left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho"
                                        + " left outer join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang"
                                        + " Where lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai and lsbdx.BienSo like @TKBienSo";
                        cmd = new SqlCommand(sql);
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKSoDienThoai", _SoDienThoai.Trim());
                        cmd.Parameters.AddWithValue("@TKBienSo", _BienSoXe.Trim());

                        DataTable dt_LichSu = Class.datatabase.getData(cmd);

                        grvLichSuBaoDuong.DataSource = dt_LichSu;
                        int solan = 0;
                        solan = Convert.ToInt32(dt_LichSu.Rows[0]["SoLan"]) + 1;
                        txtLanBaoDuong.Text = Convert.ToString(solan);

                        cmd.Connection.Close();
                    }

                    #region "Tìm trong bảng tạm

                    else
                    {
                        cmd = new SqlCommand("select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang WHERE lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai and BienSo like @TKBienSo");
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@TKSoDienThoai", _SoDienThoai.Trim());
                        cmd.Parameters.AddWithValue("@TKBienSo", _BienSoXe.Trim());

                        dt = Class.datatabase.getData(cmd);

                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("Thông tin biển số và số điện thoại không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        try
                        {
                            txtDienThoaiKhachHang.Text = dt.Rows[0]["DienThoai"].ToString();
                            idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { txtDienThoaiKhachHang.Text = ""; }

                        try { txtTenXe.Text = dt.Rows[0]["TenXe"].ToString(); }
                        catch { txtTenXe.Text = ""; }

                        try { txtBienSoXe.Text = dt.Rows[0]["BienSo"].ToString(); }
                        catch { txtBienSoXe.Text = ""; }
                    }

                    #endregion "Tìm trong bảng tạm
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
                }
            }

            #endregion Tìm kiếm theo số điện thoại và biển số
        }

        #endregion Tìm kiếm xe

        #region btnTimKiem_Click

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            TimKiemXe(txtTimKiemBienSoXe.Text.Trim(), txtTimKiemSoDienThoai.Text.Trim());
        }

        #endregion btnTimKiem_Click

        #region Reset Controls Form

        private void ResetForm()
        {
            idKhachHang = "";
            grvLichSuBaoDuong.DataSource = null;

            txtDienThoaiKhachHang.Clear();
            txtTenXe.Clear();
            txtBienSoXe.Clear();
            txtGhiChuBaoDuong.Clear();
            txtLanBaoDuong.Clear();
            dpkNgayBaoDuong.Text = Convert.ToString(DateTime.Now);
            dpkNgayGiaoXe.Text = Convert.ToString(DateTime.Now);
        }

        #endregion Reset Controls Form

        #region btnThemXe_Click

        private void btnThemXe_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtBienSoXe.Text.Trim()) && String.IsNullOrEmpty(txtDienThoaiKhachHang.Text.Trim()))
            {
                MessageBox.Show("Bạn chưa nhập Biển số hoặc Số điện thoại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtDienThoaiKhachHang.Focus();
                return;
            }
            if (String.IsNullOrEmpty(txtTenXe.Text))
            {
                MessageBox.Show("Bạn chưa nhập tên xe!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                txtTenXe.Focus();
                return;
            }

            try
            {
                SqlCommand cmd = new SqlCommand();

                if (String.IsNullOrEmpty(idKhachHang))
                {
                    //Thêm mới khách hàng
                    cmd.CommandText = @"insert into KhachHang(idcongty,tenkh,dienthoai,LoaiKH) values(@Idcongty,@tenkh,@dienthoai,@LoaiKH) select @@Identity";
                    cmd.Parameters.Clear();

                    cmd.Connection = Class.datatabase.getConnection();
                    cmd.Parameters.AddWithValue("@Idcongty", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@tenkh", txtTenKhachHang.Text);
                    cmd.Parameters.AddWithValue("@dienthoai", txtDienThoaiKhachHang.Text);
                    cmd.Parameters.AddWithValue("@LoaiKH", "2");
                    cmd.Connection.Open();
                    idKhachHang = cmd.ExecuteScalar().ToString();
                }

                //Thêm lịch sử bảo dưỡng
                cmd.CommandText = @"insert into LichSuBaoDuongXeTam(idcuahang,idkhachhang,idcongty,tenxe,bienso,ngaybaoduong,NgayGiaoXe,solan,GhiChu) values(@idcuahang, @idkhachhang, @idcongty, @tenxe, @bienso,@ngaybaoduong,@NgayGiaoXe, @solan,@GhiChu)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idcuahang", Class.EmployeeInfo.IdCuaHang);
                cmd.Parameters.AddWithValue("@idkhachhang", idKhachHang);
                cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@tenxe", txtTenXe.Text);
                cmd.Parameters.AddWithValue("@bienso", txtBienSoXe.Text);
                cmd.Parameters.AddWithValue("@ngaybaoduong", dpkNgayBaoDuong.Value);
                cmd.Parameters.AddWithValue("@NgayGiaoXe", dpkNgayGiaoXe.Value);
                cmd.Parameters.AddWithValue("@GhiChu", txtGhiChuBaoDuong.Text);
                if (String.IsNullOrEmpty(txtLanBaoDuong.Text))
                {
                    cmd.Parameters.AddWithValue("@solan", "1");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@solan", txtLanBaoDuong.Text);
                }
                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Nhập xe bảo dưỡng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadXeDangBaoDuong();
                    ResetForm();
                }
                else
                {
                    MessageBox.Show("Nhập xe bảo dưỡng thất bại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                //Lấy lịch sử bảo dưỡng xe tạm
                cmd.CommandText = "Select * from LichSuBaoDuongXeTam Where IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang";

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                Class.tblBaoDuong.lsBaoduongxetam = Class.datatabase.getData(cmd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
            finally
            {
                ResetForm();
            }
        }

        #endregion btnThemXe_Click

        #region grvLichSuBaoDuong_MouseDown

        private void grvLichSuBaoDuong_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit;
            if (e.Button == MouseButtons.Right)
            {
                hit = grvLichSuBaoDuong.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    ToolStripMenuItemInPhieuBaoDuong.Enabled = true;
                    if (!((DataGridViewRow)(grvLichSuBaoDuong.Rows[hit.RowIndex])).Selected)
                    {
                        grvLichSuBaoDuong.ClearSelection();
                        ((DataGridViewRow)(grvLichSuBaoDuong.Rows[hit.RowIndex])).Selected = true;
                        idBaoDuong = Convert.ToString(grvLichSuBaoDuong.Rows[hit.RowIndex].Cells["MaBaoDuong"].Value);
                    }
                    if (((DataGridViewRow)(grvLichSuBaoDuong.Rows[hit.RowIndex])).Selected)
                    {
                        idBaoDuong = Convert.ToString(grvLichSuBaoDuong.Rows[hit.RowIndex].Cells["MaBaoDuong"].Value);
                    }
                }
                else
                {
                    ToolStripMenuItemInPhieuBaoDuong.Enabled = false;
                }
            }
        }

        #endregion grvLichSuBaoDuong_MouseDown

        #region ToolStripMenuItemInPhieuBaoDuong_Click

        private void ToolStripMenuItemInPhieuBaoDuong_Click(object sender, EventArgs e)
        {
            Class.SelectedCustomer._idbaoduong = idBaoDuong;

            if (String.IsNullOrEmpty(Class.SelectedCustomer._idbaoduong))
            {
                MessageBox.Show("Lần bảo dưỡng không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        #endregion ToolStripMenuItemInPhieuBaoDuong_Click
    }
}