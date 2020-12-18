using AutoCareUtil;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class FrmCapNhatLichSuBaoDuong : Form
    {
        #region Variable

        private DataTable tableKhachHang = new DataTable();
        private DataTable tableBaoDuongPhieu = new DataTable();
        private DataTable tableCongThoTheoTien = new DataTable();
        private DataTable tableCongThoTheoGio = new DataTable();
        private DataTable tableCongThoThueNgoai = new DataTable();
        private DataTable tablePhuTung = new DataTable();

        private SqlCommand cmd = new SqlCommand();
        private SqlDataProvider sqlPrv = new SqlDataProvider();

        private bool IsActive = false;

        #endregion Variable

        public FrmCapNhatLichSuBaoDuong()
        {
            InitializeComponent();

            dataGridViewPhuTung.AutoGenerateColumns = false;
            dataGridViewCongThoSuaNgoai.AutoGenerateColumns = false;
            dataGridViewCongThoTheoPhut.AutoGenerateColumns = false;
            dataGridViewCongThoTheoTien.AutoGenerateColumns = false;
        }

        private void FrmCapNhatLichSuBaoDuong_Load(object sender, EventArgs e)
        {
            //Lấy thông tin lần bảo dưỡng
            LoadThongTinLanBaoDuong();
        }

        private void LoadThongTinLanBaoDuong()
        {
            LayDanhSachKho();
            LayDanhSachPhuTung();

            LayNoiDungSuaChuaTheoTienCong();
            LayNoiDungSuaChuaTheoGio();

            LayThongTinTho();

            LoadThongTinKhachHang();

            LoadThongTinBaoDuong();

            LoadThongTinPhuTung();

            LoadThongTinCongTho();
        }

        private void LoadThongTinKhachHang()
        {
            LayThongTinKhachHang();
            HienThiThongTinKhachHang();
        }

        private void LoadThongTinBaoDuong()
        {
            LayThongTinBaoDuong();
            HienThiThongTinBaoDuong();
        }

        public void LoadThongTinPhuTung()
        {
            LayThongTinPhuTung();
            HienThiThongTinPhuTung();

            if (IsActive == true)
            {
                object sender = new object();
                buttonLuu_Click(sender, new EventArgs());
            }
        }

        private void LoadThongTinCongTho()
        {
            LayThongTinCongThoTheoTien();
            HienThiCongThoTheoTien();

            LayThongTinCongThoTheoGio();
            HienThiCongViecTheoGio();

            LayThongTinCongThoThueNgoai();
            HienThiCongViecThueNgoai();

            if (IsActive == true)
            {
                object sender = new object();
                buttonLuu_Click(sender, new EventArgs());
            }
        }

        private void LayDanhSachKho()
        {
            cmd.CommandText = @"SELECT IdKho, TenKho
                                FROM KhoHang WHERE IdCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            DataTable tableKho = Class.datatabase.getData(cmd);

            cboKho.DisplayMember = "TenKho";
            cboKho.ValueMember = "IdKho";
            cboKho.DataSource = tableKho;
        }

        private void LayDanhSachPhuTung()
        {
            cmd.CommandText = @"SELECT IdPT, (ISNULL(CONVERT(nvarchar(20),MaPT), '') + ' -- ' + TenPT) AS TenPT
                                FROM PhuTung WHERE IdCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            DataTable tablePhuTung = Class.datatabase.getData(cmd);

            cboPhuTung.DisplayMember = "TenPT";
            cboPhuTung.ValueMember = "IdPT";
            cboPhuTung.DataSource = tablePhuTung;
        }

        private void LayNoiDungSuaChuaTheoTienCong()
        {
            cmd.CommandText = @"SELECT IdTienCong, (MaBD + ' -- ' + NoiDungBD) AS NoiDungBD
                                FROM TienCongTho WHERE IdCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            DataTable tableTienCongTho = Class.datatabase.getData(cmd);

            cboCongViec.DisplayMember = "NoiDungBD";
            cboCongViec.ValueMember = "IdTienCong";
            cboCongViec.DataSource = tableTienCongTho;
        }

        private void LayNoiDungSuaChuaTheoGio()
        {
            cmd.CommandText = @"SELECT IdGioViec, (MaGioViec + ' -- ' + PhuLuc) AS NoiDungBD
                                FROM GioViec WHERE IdCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            DataTable tableCongViecTheoGio = Class.datatabase.getData(cmd);

            cboCongViecTheoGio.DisplayMember = "NoiDungBD";
            cboCongViecTheoGio.ValueMember = "IdGioViec";
            cboCongViecTheoGio.DataSource = tableCongViecTheoGio;
        }

        private void LayThongTinTho()
        {
            cmd.CommandText = @"SELECT IdTho, (ISNULL(CONVERT(nvarchar(10),MaTho), '') + ' -- ' + tenTho) AS MaThoTenTho, MaTho, tenTho
                                FROM ThoDichVu WHERE IdCongTy = @IdCongTy and TinhTrangLamViec is null";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            DataTable tableTho = Class.datatabase.getData(cmd);

            Tho.DisplayMember = "MaThoTenTho";
            Tho.ValueMember = "IdTho";
            cboTho.DisplayMember = "MaThoTenTho";
            cboTho.ValueMember = "IdTho";
            cboThoTheoGio.DisplayMember = "MaThoTenTho";
            cboThoTheoGio.ValueMember = "IdTho";
            cboThoThueNgoai.DisplayMember = "MaThoTenTho";
            cboThoThueNgoai.ValueMember = "IdTho";
            comboBoxChonTho.DisplayMember = "MaThoTenTho";
            comboBoxChonTho.ValueMember = "IdTho";

            Tho.DataSource = tableTho;
            cboTho.DataSource = tableTho;
            cboThoTheoGio.DataSource = tableTho;
            cboThoThueNgoai.DataSource = tableTho;
            comboBoxChonTho.DataSource = tableTho;
        }

        private void LayThongTinKhachHang()
        {
            cmd.CommandText = @"select *from dbo.ThongTinNguoiDiBaoDuong where IdBaoDuong = @idbaoduong and IdCongTy = @idcongty";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", int.Parse(Class.SelectedCustomer._idbaoduong.ToString()));
            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            DataTable checkExist = new DataTable();
            checkExist = Class.datatabase.getData(cmd);
            if(checkExist.Rows.Count > 0)
            {
                cmd.CommandText = @"SELECT KhachHang.IdKhachHang, KhachHang.HoKH, ThongTinNguoiDiBaoDuong.TenKH, KhachHang.NgaySinh , ThongTinNguoiDiBaoDuong.DienThoai, ThongTinNguoiDiBaoDuong.DiaChi, 
                                KhachHang.NhanTinThayDau, KhachHang.NhanTinThayBugi, KhachHang.NhanTinThayDauHopSo, KhachHang.NhanTinThayDayDai,
                                KhachHang.NhanTinThayLocGio, LichSuBaoDuongXe.IdBaoDuong, LichSuBaoDuongXe.TenXe,
                                LichSuBaoDuongXe.BienSo, LichSuBaoDuongXe.Sokhung, LichSuBaoDuongXe.SoMay, LichSuBaoDuongXe.SoKm,
                                LichSuBaoDuongXe.NgayGiaoXe, LichSuBaoDuongXe.NgayBaoDuong, LichSuBaoDuongXe.SoLan, LichSuBaoDuongXe.ThayDau
                                FROM KhachHang INNER JOIN LichSuBaoDuongXe ON KhachHang.IdKhachHang = LichSuBaoDuongXe.IdKhachHang 
                                LEFT JOIN ThongTinNguoiDiBaoDuong on ThongTinNguoiDiBaoDuong.IdBaoDuong = LichSuBaoDuongXe.IdBaoDuong
                                WHERE LichSuBaoDuongXe.IdBaoDuong = @IdBaoDuong";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
                tableKhachHang = Class.datatabase.getData(cmd);
                textBoxKhachhang.Text = tableKhachHang.Rows[0]["TenKH"].ToString();
            }
            else
            {
                cmd.CommandText = @"SELECT KhachHang.IdKhachHang, KhachHang.HoKH, KhachHang.TenKH, KhachHang.NgaySinh, KhachHang.DienThoai, KhachHang.Diachi, 
                                KhachHang.NhanTinThayDau, KhachHang.NhanTinThayBugi, KhachHang.NhanTinThayDauHopSo, KhachHang.NhanTinThayDayDai,
                                KhachHang.NhanTinThayLocGio, LichSuBaoDuongXe.IdBaoDuong, LichSuBaoDuongXe.TenXe,
                                LichSuBaoDuongXe.BienSo, LichSuBaoDuongXe.Sokhung, LichSuBaoDuongXe.SoMay, LichSuBaoDuongXe.SoKm,
                                LichSuBaoDuongXe.NgayGiaoXe, LichSuBaoDuongXe.NgayBaoDuong, LichSuBaoDuongXe.SoLan, LichSuBaoDuongXe.ThayDau
                                FROM KhachHang INNER JOIN LichSuBaoDuongXe ON KhachHang.IdKhachHang = LichSuBaoDuongXe.IdKhachHang
                                WHERE LichSuBaoDuongXe.IdBaoDuong = @IdBaoDuong";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
                tableKhachHang = Class.datatabase.getData(cmd);
                textBoxKhachhang.Text = tableKhachHang.Rows[0]["HoKH"].ToString() + " " + tableKhachHang.Rows[0]["TenKH"].ToString();
            }
            
        }

        private void HienThiThongTinKhachHang()
        {
            textBoxDiaChi.Text = tableKhachHang.Rows[0]["Diachi"].ToString();
            textBoxDienThoai.Text = tableKhachHang.Rows[0]["DienThoai"].ToString();
            textBoxBienSo.Text = tableKhachHang.Rows[0]["BienSo"].ToString();
            textBoxLoaiXe.Text = tableKhachHang.Rows[0]["TenXe"].ToString();
            textBoxsoKhung.Text = tableKhachHang.Rows[0]["Sokhung"].ToString();
            textBoxSoMay.Text = tableKhachHang.Rows[0]["SoMay"].ToString();
            textBoxSoKm.Text = tableKhachHang.Rows[0]["SoKm"].ToString();
            dateTimePickerNgayBaoDuong.Text = tableKhachHang.Rows[0]["NgayBaoDuong"].ToString();
            dateTimePickerNgayGiaoXe.Text = tableKhachHang.Rows[0]["NgayGiaoXe"].ToString();
            textBoxLanBaoDuong.Text = tableKhachHang.Rows[0]["SoLan"].ToString();
            chk_ThayDau.Checked = tableKhachHang.Rows[0]["ThayDau"].ToString().ToLower() == "true" ? true : false;
            chk_bugi.Checked = tableKhachHang.Rows[0]["NhanTinThayBugi"].ToString().ToLower() == "true" ? true : false;
            chk_DauHopSo.Checked = tableKhachHang.Rows[0]["NhanTinThayDauHopSo"].ToString().ToLower() == "true" ? true : false;
            chk_DayDai.Checked = tableKhachHang.Rows[0]["NhanTinThayDayDai"].ToString().ToLower() == "true" ? true : false;
            chk_LocGio.Checked = tableKhachHang.Rows[0]["NhanTinThayLocGio"].ToString().ToLower() == "true" ? true : false;
        }

        private void LayThongTinBaoDuong()
        {
            cmd.CommandText = @"SELECT LichSuBaoDuongPhieu.idBaoDuong, LichSuBaoDuongPhieu.SoPhieu, LichSuBaoDuongPhieu.TongTien, LichSuBaoDuongPhieu.TienCongTho,
                                LichSuBaoDuongPhieu.TienPT, LichSuBaoDuongPhieu.PhanTramGiam,LichSuBaoDuongPhieu.TienTrietKhau, LichSuBaoDuongXe.GhiChu, NguoiLapPhieu.TenNguoiLapPhieu
                                FROM LichSuBaoDuongPhieu INNER JOIN LichSuBaoDuongXe ON LichSuBaoDuongPhieu.idBaoDuong = LichSuBaoDuongXe.IdBaoDuong LEFT JOIN
                                NguoiLapPhieu ON LichSuBaoDuongXe.IdBaoDuong = NguoiLapPhieu.IdBaoDuong WHERE LichSuBaoDuongPhieu.idBaoDuong = @idBaoDuong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idBaoDuong", Class.SelectedCustomer._idbaoduong);

            tableBaoDuongPhieu = Class.datatabase.getData(cmd);
        }

        private void HienThiThongTinBaoDuong()
        {
            textBoxSoPhieu.Text = tableBaoDuongPhieu.Rows[0]["SoPhieu"].ToString();
            textBoxChietKhau.Text = tableBaoDuongPhieu.Rows[0]["PhanTramGiam"].ToString();
            textBoxTienTrietKhau.Text = tableBaoDuongPhieu.Rows[0]["TienTrietKhau"].ToString();
            textBoxGhiChuBaoDuong.Text = tableBaoDuongPhieu.Rows[0]["GhiChu"].ToString();
            textBoxNguoiLapPhieu.Text = tableBaoDuongPhieu.Rows[0]["TenNguoiLapPhieu"].ToString();
        }

        private void LayThongTinCongThoTheoTien()
        {
            cmd.CommandText = @"SELECT * FROM ThoDichVu_TienCongTho2 WHERE IdBaoDuong = @IdBaoDuong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);

            tableCongThoTheoTien = Class.datatabase.getData(cmd);
        }

        private void HienThiCongThoTheoTien()
        {
            dataGridViewCongThoTheoTien.DataSource = null;
            dataGridViewCongThoTheoTien.DataSource = tableCongThoTheoTien;
            dataGridViewCongThoTheoTien.ClearSelection();
        }

        private void LayThongTinCongThoTheoGio()
        {
            cmd.CommandText = @"SELECT * FROM ThoDichVu_GioViec WHERE IdBaoDuong = @IdBaoDuong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);

            tableCongThoTheoGio = Class.datatabase.getData(cmd);
        }

        private void HienThiCongViecTheoGio()
        {
            dataGridViewCongThoTheoPhut.DataSource = null;
            dataGridViewCongThoTheoPhut.DataSource = tableCongThoTheoGio;
            dataGridViewCongThoTheoPhut.ClearSelection();
        }

        private void LayThongTinCongThoThueNgoai()
        {
            cmd.CommandText = @"SELECT * FROM ThueNgoai WHERE IdBaoDuong = @IdBaoDuong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);

            tableCongThoThueNgoai = Class.datatabase.getData(cmd);
        }

        private void HienThiCongViecThueNgoai()
        {
            dataGridViewCongThoSuaNgoai.DataSource = null;
            dataGridViewCongThoSuaNgoai.DataSource = tableCongThoThueNgoai;
            dataGridViewCongThoSuaNgoai.ClearSelection();
        }

        private void LayThongTinPhuTung()
        {
            cmd.CommandText = @"SELECT * FROM LichSuBaoDuongChiTiet2 WHERE IdBaoDuong = @IdBaoDuong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);

            tablePhuTung = Class.datatabase.getData(cmd);
        }

        private void HienThiThongTinPhuTung()
        {
            dataGridViewPhuTung.DataSource = null;
            dataGridViewPhuTung.DataSource = tablePhuTung;
            dataGridViewPhuTung.ClearSelection();
        }

        private void dataGridViewPhuTung_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal tongtienPT = 0;

            foreach (DataGridViewRow dataRow in dataGridViewPhuTung.Rows)
            {
                tongtienPT += Convert.ToDecimal(dataRow.Cells["ThanhTien"].Value);
            }

            textBoxTienPhuTung.Text = tongtienPT.ToString();
        }

        private void dataGridViewCongThoTheoTien_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal tongtienCongTho = 0;

            foreach (DataGridViewRow dataRow in dataGridViewCongThoTheoTien.Rows)
            {
                tongtienCongTho += Convert.ToDecimal(dataRow.Cells["TienKhachTra"].Value);
            }

            textBoxTienCongTho.Text = tongtienCongTho.ToString();
        }

        private void dataGridViewCongThoSuaNgoai_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal tongtienThueNgoai = 0;

            foreach (DataGridViewRow dataRow in dataGridViewCongThoSuaNgoai.Rows)
            {
                tongtienThueNgoai += Convert.ToDecimal(dataRow.Cells["TienLayCuaKH"].Value);
            }

            textBoxTienThueNgoai.Text = tongtienThueNgoai.ToString();
        }

        private void textBoxTienPhuTung_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTienPhuTung.Text))
            {
                textBoxTienPhuTung.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienPhuTung.Text));
                textBoxTienPhuTung.SelectionStart = textBoxTienPhuTung.Text.Length;

                TinhTongTien();
            }
        }

        private void textBoxTienCongTho_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTienCongTho.Text))
            {
                textBoxTienCongTho.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienCongTho.Text));
                textBoxTienCongTho.SelectionStart = textBoxTienCongTho.Text.Length;

                TinhTongTien();
            }
        }

        private void textBoxTienThueNgoai_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTienThueNgoai.Text))
            {
                textBoxTienThueNgoai.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienThueNgoai.Text));
                textBoxTienThueNgoai.SelectionStart = textBoxTienThueNgoai.Text.Length;

                TinhTongTien();
            }
        }

        private void textBoxTongTien_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTongTien.Text))
            {
                textBoxTongTien.Text = string.Format("{0:N0}", decimal.Parse(textBoxTongTien.Text));
                textBoxTongTien.SelectionStart = textBoxTongTien.Text.Length;
            }
        }

        private void TinhTongTien()
        {
            decimal tongtienPT = 0;
            decimal tongtienCongTho = 0;
            decimal tongtienThueNgoai = 0;
            decimal chietkhau = 0;
            decimal tienChietKhau = 0;
            decimal tongtien = 0;

            if (!String.IsNullOrEmpty(textBoxTienPhuTung.Text))
                tongtienPT = Convert.ToDecimal(textBoxTienPhuTung.Text);
            if (!String.IsNullOrEmpty(textBoxTienCongTho.Text))
                tongtienCongTho = Convert.ToDecimal(textBoxTienCongTho.Text);
            if (!String.IsNullOrEmpty(textBoxTienThueNgoai.Text))
                tongtienThueNgoai = Convert.ToDecimal(textBoxTienThueNgoai.Text);
            if (!String.IsNullOrEmpty(textBoxChietKhau.Text))
                chietkhau = Convert.ToDecimal(textBoxChietKhau.Text);
            if (!String.IsNullOrEmpty(textBoxTienTrietKhau.Text))
                tienChietKhau = Convert.ToDecimal(textBoxTienTrietKhau.Text);

            tongtien = (tongtienPT + tongtienCongTho + tongtienThueNgoai) - (((tongtienPT + tongtienCongTho + tongtienThueNgoai)) * (chietkhau / 100)) - tienChietKhau;

            textBoxTongTien.Text = tongtien.ToString();
        }

        private void textBoxChietKhau_TextChanged(object sender, EventArgs e)
        {
            float flag;

            if (String.IsNullOrEmpty(textBoxChietKhau.Text) || float.TryParse(textBoxChietKhau.Text, out flag) == false)
            {
                textBoxChietKhau.Text = "0";
            }
            else
            {
                if (float.Parse(textBoxChietKhau.Text) < 0)
                    textBoxChietKhau.Text = "0";
            }

            TinhTongTien();
        }

        private void buttonThemCV_Click(object sender, EventArgs e)
        {
            if (comboBoxChonTho.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Thợ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (comboBoxChonTho.SelectedValue != null)
            {
                DataTable table = (DataTable)comboBoxChonTho.DataSource;

                var result = from myRow in ((DataTable)comboBoxChonTho.DataSource).AsEnumerable()
                             where myRow.Field<int>("IdTho") == Convert.ToInt32(comboBoxChonTho.SelectedValue)
                             select myRow;

                if (result.Count() > 0)
                {
                    Class.ThongTinTho.idtho = result.FirstOrDefault().Field<int>("IdTho").ToString();
                    Class.ThongTinTho.tentho = result.FirstOrDefault().Field<string>("tenTho").ToString();
                    Class.ThongTinTho.matho = result.FirstOrDefault().Field<string>("MaTho").ToString();

                    FrmChiTietCongTho frmCongTho = new FrmChiTietCongTho();
                    frmCongTho.IdBaoDuong = Class.SelectedCustomer._idbaoduong;
                    frmCongTho.Check = true;
                    frmCongTho.SuaLichSuBaoDuong = true;
                    frmCongTho.LoadDanhSachCongTho = new FrmChiTietCongTho.LoadCongTho(LoadThongTinCongTho);
                    IsActive = true;
                    frmCongTho.FormClosed += frmCongTho_FormClosed;

                    frmCongTho.ShowDialog();
                }
            }
        }

        private void buttonThemPT_Click(object sender, EventArgs e)
        {
            FrmThemPhuTungBaoDuong frmThemPhuTung = new FrmThemPhuTungBaoDuong();
            IsActive = true;
            frmThemPhuTung.LayPhuTungBaoDuong = new FrmThemPhuTungBaoDuong.LoadDanhSachPhuTung(LoadThongTinPhuTung);
            frmThemPhuTung.FormClosed += frmThemPhuTung_FormClosed;

            frmThemPhuTung.ShowDialog();
        }

        private void frmThemPhuTung_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsActive = false;
        }

        private void frmCongTho_FormClosed(object sender, FormClosedEventArgs e)
        {
            IsActive = false;
        }

        private void buttonLuu_Click(object sender, EventArgs e)
        {
            buttonLuu.Enabled = false;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT IdBaoDuong, TenNguoiLapPhieu
                                FROM dbo.NguoiLapPhieu WHERE IdBaoDuong = @IdBaoDuong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);

            DataTable tableNguoiLapPhieu = Class.datatabase.getData(cmd);

            using (SqlConnection cnn = Class.datatabase.getConnection())
            {
                cnn.Open();
                cmd.Connection = cnn;

                using (SqlTransaction tran = cnn.BeginTransaction())
                {
                    cmd.Transaction = tran;

                    try
                    {
                        using (SqlConnection conn = new SqlConnection(Class.datatabase.connect))
                        {
                            if (conn.State == ConnectionState.Closed)
                            {
                                conn.Open();
                            }
                            //Cập nhật mã thợ sửa chữa
                            for (int i = 0; i < dataGridViewPhuTung.Rows.Count; i++)
                            {
                                cmd.CommandText = @"UPDATE dbo.LichSuBaoDuongChiTiet2 SET idTho = @IdTho WHERE IdBaoDuong = @IdBaoDuong and MaPT =@MaPT";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdTho", int.Parse(dataGridViewPhuTung.Rows[i].Cells[7].Value.ToString()));
                                cmd.Parameters.AddWithValue("@IdBaoDuong", tableKhachHang.Rows[0]["IdBaoDuong"].ToString());
                                cmd.Parameters.AddWithValue("@MaPT", dataGridViewPhuTung.Rows[i].Cells[1].Value.ToString().Trim());
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = @"UPDATE dbo.LichSuBaoDuongXe SET KYTHUATVIEN = @IdTho WHERE IdBaoDuong = @IdBaoDuong and IdCongTy = @idcongty";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdTho", int.Parse(dataGridViewPhuTung.Rows[i].Cells[7].Value.ToString()));
                                cmd.Parameters.AddWithValue("@IdBaoDuong", tableKhachHang.Rows[0]["IdBaoDuong"].ToString());
                                cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                cmd.ExecuteNonQuery();
                            }
                            conn.Close();
                        }

                        //Cập nhật lịch sử bảo dưỡng xe
                        cmd.CommandText = @"UPDATE dbo.LichSuBaoDuongXe
                                            SET NgayBaoDuong = @NgayBaoDuong, NgayGiaoXe = @NgayGiaoXe, GhiChu =@GhiChu
                                            WHERE IdBaoDuong = @IdBaoDuong";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@NgayBaoDuong", dateTimePickerNgayBaoDuong.Value);
                        cmd.Parameters.AddWithValue("@NgayGiaoXe", dateTimePickerNgayGiaoXe.Value);
                        cmd.Parameters.AddWithValue("@GhiChu", textBoxGhiChuBaoDuong.Text);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);

                        cmd.ExecuteNonQuery();

                        //Cập nhật người lập phiếu
                        if (!String.IsNullOrEmpty(textBoxNguoiLapPhieu.Text))
                        {
                            if (tableNguoiLapPhieu.Rows.Count > 0)
                            {
                                cmd.CommandText = @"UPDATE dbo.NguoiLapPhieu
                                                    SET TenNguoiLapPhieu = @TenNguoiLapPhieu
                                                    WHERE IdBaoDuong = @IdBaoDuong";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@TenNguoiLapPhieu", textBoxNguoiLapPhieu.Text);
                                cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);

                                cmd.ExecuteNonQuery();
                            }
                            else
                            {
                                cmd.CommandText = @"INSERT INTO dbo.NguoiLapPhieu
                                                    (IdBaoDuong, TenNguoiLapPhieu)
                                                    VALUES (@IdBaoDuong,@TenNguoiLapPhieu)";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
                                cmd.Parameters.AddWithValue("@TenNguoiLapPhieu", textBoxNguoiLapPhieu.Text);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        //Cập nhật Lịch sử bảo dưỡng phiếu
                        cmd.CommandText = @"UPDATE dbo.LichSuBaoDuongPhieu
                                            SET SoPhieu = @SoPhieu, TongTien = @TongTien, TienCongTho = @TienCongTho,
                                            TienPT = @TienPT, PhanTramGiam = @PhanTramGiam, NgayGiaoXe = @NgayGiaoXe,
                                            TienTrietKhau = @TienTrietKhau
                                            WHERE idBaoDuong = @idBaoDuong";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@SoPhieu", textBoxSoPhieu.Text);
                        cmd.Parameters.AddWithValue("@TongTien", textBoxTongTien.Text);
                        cmd.Parameters.AddWithValue("@TienCongTho", textBoxTienCongTho.Text);
                        cmd.Parameters.AddWithValue("@TienPT", textBoxTienPhuTung.Text);
                        cmd.Parameters.AddWithValue("@PhanTramGiam", textBoxChietKhau.Text);
                        cmd.Parameters.AddWithValue("@TienTrietKhau", textBoxTienTrietKhau.Text);
                        cmd.Parameters.AddWithValue("@NgayGiaoXe", dateTimePickerNgayGiaoXe.Value);
                        cmd.Parameters.AddWithValue("@idBaoDuong", Class.SelectedCustomer._idbaoduong);

                        cmd.ExecuteNonQuery();

                        //Cập nhật Phiếu thu
                        cmd.CommandText = @"UPDATE dbo.PhieuThu
                                            SET SoTienThu = @SoTienThu
                                            WHERE SoHoaDon = @SoHoaDon AND IdCongTy = @IdCongTy";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@SoTienThu", textBoxTongTien.Text);
                        cmd.Parameters.AddWithValue("@SoHoaDon", Class.SelectedCustomer._idbaoduong);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                        cmd.ExecuteNonQuery();

                        #region Nhan tin thay dau

                        if (chk_ThayDau.Checked && !string.IsNullOrEmpty(tableKhachHang.Rows[0]["DienThoai"].ToString()))
                        {
                            using (SqlConnection con = new SqlConnection(Class.datatabase.connect))
                            {
                                con.Open();
                                string ngaymua = string.Empty;
                                //Cập nhật khách hàng thay dầu
                                cmd.CommandText = @"UPDATE dbo.LichSuBaoDuongXe
                                            SET ThayDau = 1
                                            WHERE IdBaoDuong = @IdBaoDuong";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdBaoDuong", tableKhachHang.Rows[0]["IdBaoDuong"].ToString());
                                cmd.ExecuteNonQuery();

                                // Lấy cấu hình tin nhắn thay dầu
                                //SqlCommand cm = new SqlCommand(@"Select top 1 * from SMSMoiThayDau_Config where idcongty=@IdCongTy and active=1");
                                //cm.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                //DataTable tblThayDauConfig = new DataTable();
                                //tblThayDauConfig = Class.datatabase.getData(cm);

                                // Lấy nội dung tin nhắn thay dầu
                                SqlCommand cm1 = new SqlCommand(@"select sms from SMSConfig where idcongty=@IdCongTy and Type='Thay dau'");
                                cm1.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                DataTable smsContentThayDau = new DataTable();
                                smsContentThayDau = Class.datatabase.getData(cm1);

                                //if (tblThayDauConfig.Rows.Count > 0 && smsContentThayDau.Rows.Count > 0)
                                if (smsContentThayDau.Rows.Count > 0)
                                {
                                    string nhansausongay = "45";
                                    string gionhan = "10";

                                    string IdKhachHang = tableKhachHang.Rows[0]["IdKhachHang"].ToString();
                                    string BienSo = tableKhachHang.Rows[0]["BienSo"].ToString();

                                    SqlCommand cm2 = new SqlCommand(@"select NgayBan from dbo.khachhang kh with(nolock) join xedaban xdb with(nolock) on kh.IdKhachHang=xdb.IdKhachHang where kh.IdCongTy=@IdCongTy and kh.IdKhachHang=@IdKhachHang and xdb.BienSo=@BienSo");
                                    cm2.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    cm2.Parameters.AddWithValue("@IdKhachHang", IdKhachHang);
                                    cm2.Parameters.AddWithValue("@BienSo", BienSo);
                                    DataTable tblXeDaBan = new DataTable();
                                    tblXeDaBan = Class.datatabase.getData(cm2);

                                    if (tblXeDaBan.Rows.Count > 0)
                                    {
                                        ngaymua = tblXeDaBan.Rows[0]["NgayBan"].ToString();
                                    }
                                    else
                                    {
                                        ngaymua = tableKhachHang.Rows[0]["NgayGiaoXe"].ToString();
                                    }

                                    DateTime dt2;
                                    if (ngaymua == "")
                                    {
                                        dt2 = DateTime.Now;
                                    }
                                    else
                                    {
                                        dt2 = Convert.ToDateTime(ngaymua);
                                    }

                                    //DateTime tmp = dt2;
                                    DateTime d = dt2.AddDays(int.Parse(nhansausongay));
                                    DateTime timechedule = new DateTime(d.Year, d.Month, d.Day, int.Parse(gionhan), 0, 0, 0);

                                    //dont - 20190806 - chỉ gửi tin nhắn nếu lịch gửi >= ngày hiện tại
                                    if (timechedule != null && timechedule.Date >= DateTime.Now.Date)
                                    {
                                        string hoKH = tableKhachHang.Rows[0]["HoKH"].ToString();
                                        string tenKH = tableKhachHang.Rows[0]["TenKH"].ToString();
                                        string ngaySinh = tableKhachHang.Rows[0]["NgaySinh"].ToString();
                                        string[] tmpNgaySinh = ngaySinh.Split('/');
                                        string tenXe = tableKhachHang.Rows[0]["TenXe"].ToString();
                                        string bienSo = tableKhachHang.Rows[0]["BienSo"].ToString();
                                        string soKhung = tableKhachHang.Rows[0]["SoKhung"].ToString();
                                        string soMay = tableKhachHang.Rows[0]["SoMay"].ToString();
                                        string dienThoai = tableKhachHang.Rows[0]["DienThoai"].ToString();

                                        string resms = Utilities.smsreplace(
                                            smsContentThayDau.Rows[0]["sms"].ToString(),
                                            hoKH + " " + tenKH,
                                            !string.IsNullOrEmpty(ngaySinh) ? Convert.ToInt32(tmpNgaySinh[0]) + "/" + Convert.ToInt32(tmpNgaySinh[1]) + "/" + Convert.ToInt32(tmpNgaySinh[2].Split(' ')[0]) : "",
                                            Class.CompanyInfo.sendername,
                                            tenXe,
                                            bienSo,
                                            soKhung,
                                            soMay,
                                            dienThoai,
                                            "",
                                            timechedule.ToString("dd/MM/yyyy"), "", "");

                                        bool isunicode = Tools.GetDataCoding(resms) == 8;

                                        // if sms exists and not send then continue
                                        SqlCommand cm3 = new SqlCommand(@"select smsid from TinNhan with (nolock)
	                                                                where phone = @phone and sms = @sms and SenderName = @sendername and smstype = @smstype
	                                                                and IdCongTy = @idcongty and IdKhachHang = @idkhachhang");
                                        cm3.Parameters.AddWithValue("@phone", tableKhachHang.Rows[0]["DienThoai"].ToString());
                                        cm3.Parameters.AddWithValue("@sms", resms);
                                        cm3.Parameters.AddWithValue("@sendername", Class.CompanyInfo.sendername);
                                        cm3.Parameters.AddWithValue("@smstype", "Thay dau");
                                        cm3.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                        cm3.Parameters.AddWithValue("@idkhachhang", tableKhachHang.Rows[0]["IdKhachHang"].ToString());
                                        DataTable tblCheckExistsSMS = new DataTable();
                                        tblCheckExistsSMS = Class.datatabase.getData(cm3);
                                        if (tblCheckExistsSMS.Rows.Count > 0)
                                        {
                                            SqlCommand cm4 = new SqlCommand(@"delete from TinNhan
	                                                                where phone = @phone and sms = @sms and SenderName = @sendername and smstype = @smstype
	                                                                and IdCongTy = @idcongty and IdKhachHang = @idkhachhang and timeSchedule >= @timeSchedule");
                                            cm4.Parameters.AddWithValue("@phone", tableKhachHang.Rows[0]["DienThoai"].ToString());
                                            cm4.Parameters.AddWithValue("@sms", resms);
                                            cm4.Parameters.AddWithValue("@sendername", Class.CompanyInfo.sendername);
                                            cm4.Parameters.AddWithValue("@smstype", "Thay dau");
                                            cm4.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                            cm4.Parameters.AddWithValue("@idkhachhang", tableKhachHang.Rows[0]["IdKhachHang"].ToString());
                                            cm4.Parameters.AddWithValue("@timeSchedule", timechedule);
                                            Class.datatabase.getData(cm4);
                                        }
                                        // hẹn lịch 6 lần gửi thay dầu
                                        for (int i = 0; i < 6; i++)
                                        {
                                            // send sms
                                            timechedule = timechedule.AddDays(45);
                                            string sql = @"sp_TinNhan_InsertWithCheckLoop";
                                            cmd = new SqlCommand(sql, con);
                                            cmd.Parameters.Clear();
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.AddWithValue("@sendername", Class.CompanyInfo.sendername);
                                            cmd.Parameters.AddWithValue("@phone", dienThoai);
                                            cmd.Parameters.AddWithValue("@sms", resms);
                                            cmd.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                                            cmd.Parameters.AddWithValue("@smstype", "Thay dau");
                                            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                            cmd.Parameters.AddWithValue("@idkhachhang", tableKhachHang.Rows[0]["IdKhachHang"].ToString());
                                            cmd.Parameters.AddWithValue("@timeSchedule", timechedule);
                                            cmd.Parameters.AddWithValue("@isUnicode", isunicode ? "1" : "0");

                                            cmd.ExecuteNonQuery();
                                        }
                                        
                                    }
                                }
                                con.Close();
                            }
                        }
                        else
                        {
                            using (SqlConnection con = new SqlConnection(Class.datatabase.connect))
                            {
                                con.Open();

                                // update trạng thái thay dầu = 0
                                cmd.CommandText = @"UPDATE LichSuBaoDuongXe
                                            SET ThayDau = 0
                                            WHERE IdBaoDuong = @IdBaoDuong";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdBaoDuong", tableKhachHang.Rows[0]["IdBaoDuong"].ToString());
                                cmd.ExecuteNonQuery();

                                // delete tin nhắn hẹn thay dầu
                                SqlCommand cm = new SqlCommand(@"delete from TinNhan
	                                                                where phone = @phone and SenderName = @sendername and smstype = @smstype
	                                                                and IdCongTy = @idcongty and IdKhachHang = @idkhachhang");
                                cm.Parameters.AddWithValue("@phone", tableKhachHang.Rows[0]["DienThoai"].ToString());
                                cm.Parameters.AddWithValue("@sendername", Class.CompanyInfo.sendername);
                                cm.Parameters.AddWithValue("@smstype", "Thay dau");
                                cm.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                cm.Parameters.AddWithValue("@idkhachhang", tableKhachHang.Rows[0]["IdKhachHang"].ToString());
                                Class.datatabase.getData(cm);

                                con.Close();
                            }
                        }

                        #endregion Nhan tin thay dau

                        
                        MessageBox.Show("Lưu lịch sử bảo dưỡng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        buttonLuu.Enabled = true;
                        tran.Commit();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        buttonLuu.Enabled = true;
                        tran.Rollback();
                    }
                }
            }
        }

        private void dataGridViewCongThoTheoTien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridViewCongThoTheoTien.Columns["XoaCVTien"].Index)
                {
                    if (MessageBox.Show("Bạn có muốn xóa việc này không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        IsActive = true;

                        int RowIndex = e.RowIndex;
                        string IdBaoDuongXe = Class.SelectedCustomer._idbaoduong;
                        string Id = dataGridViewCongThoTheoTien.Rows[RowIndex].Cells["IdTienCong"].Value.ToString();
                        string IdTho = dataGridViewCongThoTheoTien.Rows[RowIndex].Cells["cboTho"].Value.ToString();
                        string IdTienCong = dataGridViewCongThoTheoTien.Rows[e.RowIndex].Cells["cboCongViec"].Value.ToString();

                        SqlCommand cm = new SqlCommand(@"SELECT * FROM ThoDichVu_TienCongTho2 WHERE IdCongTy=@IdCongTy AND IdBaoDuong=@IdBaoDuong
                                                         AND IdTienCong=@IdTienCong ORDER BY TienKhachTra DESC");
                        cm.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cm.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                        cm.Parameters.AddWithValue("@IdTienCong", IdTienCong);

                        DataTable dt = new DataTable();
                        dt = Class.datatabase.getData(cm);

                        if (dt.Rows.Count > 1)
                        {
                            int i = dt.Rows.Count - 1;

                            decimal tiencong = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["TienCong"].ToString()) / i);
                            decimal tienkhachtra = Convert.ToDecimal(dt.Rows[0]["TienKhachTra"].ToString());

                            if (Id != dt.Rows[0]["Id"].ToString())
                            {
                                SqlCommand cmd1 = new SqlCommand(@"UPDATE ThoDichVu_TienCongTho2 SET TienCong=@TienCong WHERE IdCongTy=@IdCongTy
                                                                   AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong");
                                cmd1.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd1.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                                cmd1.Parameters.AddWithValue("@IdTienCong", IdTienCong);
                                cmd1.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["TienCong"].ToString()) + tiencong));
                                sqlPrv.ExecuteNonQuery(cmd1);

                                SqlCommand cmd = new SqlCommand("delete ThoDichVu_TienCongTho2 where Id = @Id");
                                cmd.Parameters.AddWithValue("@Id", Id);
                                sqlPrv.ExecuteNonQuery(cmd);
                            }
                            else
                            {
                                SqlCommand cmd1 = new SqlCommand(@"UPDATE ThoDichVu_TienCongTho2 SET TienCong=@TienCong WHERE IdCongTy=@IdCongTy
                                                                   AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong");
                                cmd1.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd1.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                                cmd1.Parameters.AddWithValue("@IdTienCong", IdTienCong);
                                cmd1.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["TienCong"].ToString()) + tiencong));
                                sqlPrv.ExecuteNonQuery(cmd1);

                                SqlCommand cmd2 = new SqlCommand(@"UPDATE ThoDichVu_TienCongTho2 SET TienKhachTra=@TienKhachTra WHERE IdCongTy=@IdCongTy
                                                                   AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong AND Id=@Id");
                                cmd2.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd2.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                                cmd2.Parameters.AddWithValue("@IdTienCong", IdTienCong);
                                cmd2.Parameters.AddWithValue("@TienKhachTra", Convert.ToDecimal(dt.Rows[0]["TienKhachTra"].ToString()));
                                cmd2.Parameters.AddWithValue("@Id", Convert.ToDecimal(dt.Rows[1]["Id"].ToString()));
                                sqlPrv.ExecuteNonQuery(cmd2);

                                SqlCommand cmd = new SqlCommand("delete ThoDichVu_TienCongTho2 where Id=@Id");
                                cmd.Parameters.AddWithValue("@Id", Id);
                                sqlPrv.ExecuteNonQuery(cmd);
                            }
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("delete ThoDichVu_TienCongTho2 where Id=@Id");
                            cmd.Parameters.AddWithValue("@Id", Id);
                            sqlPrv.ExecuteNonQuery(cmd);
                        }

                        LoadThongTinCongTho();
                        IsActive = false;
                    }
                }
            }
        }

        private void dataGridViewCongThoTheoPhut_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridViewCongThoTheoPhut.Columns["XoaCVGio"].Index)
                {
                    if (MessageBox.Show("Bạn có muốn xóa việc này không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        IsActive = true;

                        int RowIndex = e.RowIndex;
                        string IdBaoDuongXe = Class.SelectedCustomer._idbaoduong;
                        string IdTho = dataGridViewCongThoTheoPhut.Rows[RowIndex].Cells["cboThoTheoGio"].Value.ToString();

                        SqlCommand cmd = new SqlCommand("delete ThoDichVu_GioViec where IdCongTy=@IdCongTy and IdTho=@IdTho and IdBaoDuong=@IdBaoDuong");
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdTho", IdTho);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
                        sqlPrv.ExecuteNonQuery(cmd);

                        LoadThongTinCongTho();
                        IsActive = false;
                    }
                }
            }
        }

        private void dataGridViewCongThoSuaNgoai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == dataGridViewCongThoSuaNgoai.Columns["XoaThuaNgoai"].Index)
                {
                    if (MessageBox.Show("Bạn có muốn xóa việc này không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        IsActive = true;

                        string IdBaoDuongXe = Class.SelectedCustomer._idbaoduong;
                        string IdThueNgoai = dataGridViewCongThoSuaNgoai.Rows[e.RowIndex].Cells["IdThueNgoai"].Value.ToString();

                        SqlCommand cmd = new SqlCommand("delete ThueNgoai where IdTheuNgoai = @IdThueNgoai");
                        cmd.Parameters.AddWithValue("@IdThueNgoai", IdThueNgoai);
                        sqlPrv.ExecuteNonQuery(cmd);

                        LoadThongTinCongTho();
                        IsActive = false;
                    }
                }
            }
        }

        private void dataGridViewPhuTung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridViewPhuTung.Columns["XoaPT"].Index)
                    {
                        if (MessageBox.Show("Bạn có muốn xóa phụ tùng khỏi danh sách phụ tùng bảo dưỡng?", "Thông báo xóa phụ tùng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                        {
                            IsActive = true;

                            string IdKho = dataGridViewPhuTung.Rows[e.RowIndex].Cells["cboKho"].Value.ToString();
                            string IdBaoDuong = dataGridViewPhuTung.Rows[e.RowIndex].Cells["IdBaoDuong"].Value.ToString();
                            string IdPT = dataGridViewPhuTung.Rows[e.RowIndex].Cells["IdPhuTung"].Value.ToString();

                            //Cập nhật lại số lượng trong kho
                            cmd.CommandText = @"SELECT IdPT, MaPT, TenPT, SoLuong
                                                FROM PhuTung WHERE IdKho = @IdKho AND IdPT = @IdPT";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdKho", IdKho);
                            cmd.Parameters.AddWithValue("@IdPT", IdPT);

                            DataTable tablePhuTung = Class.datatabase.getData(cmd);

                            if (tablePhuTung.Rows.Count > 0)
                            {
                                int soluong = 0;

                                if (String.IsNullOrEmpty(tablePhuTung.Rows[0]["SoLuong"].ToString()))
                                    soluong = 0;
                                else
                                    soluong = Convert.ToInt32(tablePhuTung.Rows[0]["SoLuong"].ToString());

                                int soluongsau = Convert.ToInt32(dataGridViewPhuTung.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString()) + soluong;

                                cmd.CommandText = @"UPDATE PhuTung
                                                    SET SoLuong = @SoLuong WHERE IdCongTy = @IdCongTy AND IdPT = @IdPT";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@SoLuong", soluongsau);
                                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd.Parameters.AddWithValue("@IdPT", IdPT);

                                Class.datatabase.ExcuteNonQuery(cmd);
                            }

                            //Xóa phụ tùng bảo dưỡng
                            cmd.CommandText = @"DELETE FROM LichSuBaoDuongChiTiet2 WHERE IdBaoDuong = @IdBaoDuong AND IdPhuTung = @IdPhuTung AND IdKho = @IdKho";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
                            cmd.Parameters.AddWithValue("@IdPhuTung", IdPT);
                            cmd.Parameters.AddWithValue("@IdKho", IdKho);

                            Class.datatabase.ExcuteNonQuery(cmd);

                            //Load lại danh sách phụ tùng
                            LoadThongTinPhuTung();
                            IsActive = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                IsActive = false;
            }
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonIn_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Class.SelectedCustomer._idbaoduong))
            {
                MessageBox.Show("Lần bảo dưỡng không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

           
            if (int.Parse(Class.CompanyInfo.idcongty) == 94)
            {
                frmPhieuThanhToanVietLong2 frm = new frmPhieuThanhToanVietLong2();
                frm.ShowDialog();
            }
            else if (int.Parse(Class.CompanyInfo.idcongty) == 95)
            {
                frmPhieuThanhToanVietLong3 frm = new frmPhieuThanhToanVietLong3();
                frm.ShowDialog();
            }
            else
            {
                FrmPhieuSuaChuaThangLoi frm = new FrmPhieuSuaChuaThangLoi();
                frm.ShowDialog();
            }

        }

        private void textBoxTienTrietKhau_TextChanged(object sender, EventArgs e)
        {
            float flag;

            if (String.IsNullOrEmpty(textBoxTienTrietKhau.Text) || float.TryParse(textBoxTienTrietKhau.Text, out flag) == false)
            {
                textBoxTienTrietKhau.Text = "0";
            }
            else
            {
                if (float.Parse(textBoxTienTrietKhau.Text) < 0)
                    textBoxTienTrietKhau.Text = "0";
            }

            textBoxTienTrietKhau.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienTrietKhau.Text));
            textBoxTienTrietKhau.SelectionStart = textBoxTienTrietKhau.Text.Length;

            TinhTongTien();
        }

        private void BtnInPhieuBaoDuong_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Class.SelectedCustomer._idbaoduong))
            {
                MessageBox.Show("Lần bảo dưỡng không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (int.Parse(Class.CompanyInfo.idcongty) == 94)
            {
                frmPhieuBaoDuongDinhKyVietLong2 frm = new frmPhieuBaoDuongDinhKyVietLong2();
                frm.idBaoDuongTamThoi = Class.SelectedCustomer._idbaoduong.ToString().Trim();
                frm.lichbd = "lsbd";
                frm.ShowDialog();
            }
            else if (int.Parse(Class.CompanyInfo.idcongty) == 95)
            {
                frmPhieuBaoDuongDinhKyVietLong3 frm = new frmPhieuBaoDuongDinhKyVietLong3();
                frm.idBaoDuongTamThoi = Class.SelectedCustomer._idbaoduong.ToString().Trim();
                frm.lichbd = "lsbd";
                frm.ShowDialog();
            }
            else
            {
                frmPhieuBaoDuongDinhKy frm = new frmPhieuBaoDuongDinhKy();
                frm.idBaoDuongTamThoi = Class.SelectedCustomer._idbaoduong.ToString().Trim();
                frm.lichbd = "lsbd";
                frm.ShowDialog();
            }
            
        }
    }
}