using AutoCareV2._0.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class FrmLapBaoGiaSuaChua : Form
    {
        #region Delegate
        public delegate void LayDanhSachPhuTung();
        public delegate void LayDanhSachCongViec();

        public LayDanhSachPhuTung LayPhuTung;
        public LayDanhSachCongViec LayCongViec;
        #endregion

        #region Variable
        public long IdBaoDuong;

        private SqlCommand cmd = new SqlCommand();

        private DataTable tableKhoHang = new DataTable();
        private DataTable tableCongViec = new DataTable();
        private DataTable tablePhuTung = new DataTable();
        private DataTable tableKhachHang = new DataTable();

        private DataTable tableBaoGiaCongViec = new DataTable();
        private DataTable tableBaoGiaPhuTung = new DataTable();

        private DataTable tableBaoGia = new DataTable();

        private DocTien doctien = new DocTien();
        private string IdBaoGiaSuaChua = "";
        #endregion

        #region FrmLapBaoGiaSuaChua
        public FrmLapBaoGiaSuaChua()
        {
            InitializeComponent();

            dataGridViewCongViec.AutoGenerateColumns = false;
            dataGridViewPhuTung.AutoGenerateColumns = false;

            comboBoxCongViec.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxCongViec.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            comboBoxPhuTung.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxPhuTung.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }
        #endregion

        #region FrmLapBaoGiaSuaChua_Load
        private void FrmLapBaoGiaSuaChua_Load(object sender, EventArgs e)
        {
            HienThiDuLieuLenControls();
        }
        #endregion

        #region Hiển thị dữ liệu lên các controls
        private void HienThiDuLieuLenControls()
        {
            Load_DanhSachCongViec();
            Load_cboCongViec();

            Load_DanhSachPhuTung();

            Load_DanhSachKho();
            HienThiDanhSachKho();

            Load_ThongTinKhachHang();
            LayThongTinBaoGia();
            HienThiThongTinKhachHang();

            if (tableBaoGia.Rows.Count <= 0)
            {
                TaoBangBaoGiaCongViec();
                TaoBangBaoGiaPhuTung();

                LoadDanhSachCongViec();
                LoadDanhSachPhuTung();

                textBoxTienCong.Text = "0";
                textBoxTienPhuTung.Text = "0";
                textBoxTienSauVAT.Text = "0";
                textBoxTienVAT.Text = "0";
            }
            else
            {
                IdBaoGiaSuaChua = tableBaoGia.Rows[0]["IdBaoGia"].ToString();
                textBoxCoVanDV.Text = tableBaoGia.Rows[0]["CoVanDV"].ToString();
                textBoxTruongPhong.Text = tableBaoGia.Rows[0]["TruongPhongDV"].ToString();

                cmd.CommandText = @"SELECT IdBaoGiaCongTho, IdBaoGia, IdTienCong, NoiDungCV, TienCong, GhiChu, DaThucHien
                                    FROM BaoGiaCongThoTam WHERE IdBaoGia = @IdBaoGia";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdBaoGia", IdBaoGiaSuaChua);

                tableBaoGiaCongViec = Class.datatabase.getData(cmd);

                cmd.CommandText = @"SELECT IdBaoGiaPhuTung, IdBaoGia, IdPhuTung, IdKho, MaPT, TenPT, DVT, SoLuong, DonGia, ThanhTien, DaThucHien
                                    FROM BaoGiaPhuTungTam WHERE IdBaoGia = @IdBaoGia";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdBaoGia", IdBaoGiaSuaChua);

                tableBaoGiaPhuTung = Class.datatabase.getData(cmd);

                LoadDanhSachCongViec();
                LoadDanhSachPhuTung();
            }
        }
        #endregion

        #region Lấy thông tin báo giá
        private void LayThongTinBaoGia()
        {
            cmd.CommandText = @"SELECT IdBaoGia, IdKhachHang, IdBaoDuong, NgayBaoGia, TongTienVatTu, TongTienCong, VAT, TongSauVAT, CoVanDV, TruongPhongDV
                                FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);

            tableBaoGia = Class.datatabase.getData(cmd);
        }
        #endregion

        #region Khởi tạo bảng báo giá công việc
        private void TaoBangBaoGiaCongViec()
        {
            tableBaoGiaCongViec.Columns.Add(new DataColumn("IdBaoGiaCongTho", typeof(Int64)));
            tableBaoGiaCongViec.Columns.Add(new DataColumn("IdBaoGia", typeof(Int64)));
            tableBaoGiaCongViec.Columns.Add(new DataColumn("IdTienCong", typeof(Int32)));
            tableBaoGiaCongViec.Columns.Add(new DataColumn("NoiDungCV", typeof(String)));
            tableBaoGiaCongViec.Columns.Add(new DataColumn("TienCong", typeof(Decimal)));
            tableBaoGiaCongViec.Columns.Add(new DataColumn("GhiChu", typeof(Decimal)));
            tableBaoGiaCongViec.Columns.Add(new DataColumn("DaThucHien", typeof(Boolean)));
        }
        #endregion

        #region Tạo bảng báo giá phụ tùng
        private void TaoBangBaoGiaPhuTung()
        {
            tableBaoGiaPhuTung.Columns.Add(new DataColumn("IdBaoGiaPhuTung", typeof(Int64)));
            tableBaoGiaPhuTung.Columns.Add(new DataColumn("IdBaoGia", typeof(Int64)));
            tableBaoGiaPhuTung.Columns.Add(new DataColumn("IdPhuTung", typeof(Int32)));
            tableBaoGiaPhuTung.Columns.Add(new DataColumn("IdKho", typeof(Int64)));
            tableBaoGiaPhuTung.Columns.Add(new DataColumn("MaPT", typeof(String)));
            tableBaoGiaPhuTung.Columns.Add(new DataColumn("TenPT", typeof(String)));
            tableBaoGiaPhuTung.Columns.Add(new DataColumn("DVT", typeof(String)));
            tableBaoGiaPhuTung.Columns.Add(new DataColumn("SoLuong", typeof(Int32)));
            tableBaoGiaPhuTung.Columns.Add(new DataColumn("DonGia", typeof(Decimal)));
            tableBaoGiaPhuTung.Columns.Add(new DataColumn("ThanhTien", typeof(Decimal)));
            tableBaoGiaPhuTung.Columns.Add(new DataColumn("DaThucHien", typeof(Boolean)));
        }
        #endregion

        #region Lấy danh sách công việc
        private void Load_DanhSachCongViec()
        {
            cmd.CommandText = @"SELECT IdTienCong, MaBD, NoiDungBD, TienCong
                                FROM TienCongTho
                                WHERE IdCongTy=@IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            tableCongViec = Class.datatabase.getData(cmd);
        }
        #endregion

        #region Hiển thị công việc lên gridview
        private void LoadDanhSachCongViec()
        {
            dataGridViewCongViec.DataSource = null;
            dataGridViewCongViec.DataSource = tableBaoGiaCongViec;
        }
        #endregion

        #region Hiển thị danh sách công việc lên combobox
        private void Load_cboCongViec()
        {
            if (tableCongViec.Rows.Count > 0)
            {
                comboBoxCongViec.DisplayMember = "NoiDungBD";
                comboBoxCongViec.ValueMember = "IdTienCong";
                comboBoxCongViec.DataSource = tableCongViec;
            }
        }
        #endregion

        #region Load danh sách công việc
        public void ReloadCombobox()
        {
            Load_DanhSachCongViec();
            Load_cboCongViec();
        }
        #endregion

        #region Lấy danh sách kho phụ tùng
        private void Load_DanhSachKho()
        {
            cmd.CommandText = @"SELECT IdKho, IdCongTy, IdCuaHang, TenKho
                                FROM KhoHang WHERE IdCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            tableKhoHang = Class.datatabase.getData(cmd);
        }
        #endregion

        #region Hiển thị danh sách kho lên combobox
        private void HienThiDanhSachKho()
        {
            if (tableKhoHang.Rows.Count > 0)
            {
                comboBoxKhoPhuTung.DisplayMember = "TenKho";
                comboBoxKhoPhuTung.ValueMember = "IdKho";
                comboBoxKhoPhuTung.DataSource = tableKhoHang;
            }
        }
        #endregion

        #region Lấy danh sách phụ tùng
        private void Load_DanhSachPhuTung()
        {
            cmd.CommandText = @"SELECT IdPT, ISNULL(MaPT, '') + '-- ' + ISNULL(TenPT, '') AS TenPT, MaPT, DVT, DonGia, SoLuong, IdKho
                                FROM PhuTung
                                WHERE IdCongTy=@IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            tablePhuTung = Class.datatabase.getData(cmd);
        }
        #endregion

        #region Hiển thị danh sách phụ tùng lên gridview
        private void LoadDanhSachPhuTung()
        {
            dataGridViewPhuTung.DataSource = null;
            dataGridViewPhuTung.DataSource = tableBaoGiaPhuTung;
        }
        #endregion

        #region Hiển thị danh sách phụ tùng lên combobox
        private void Load_cboPhuTung(int IdKho)
        {
            var tableResult = (from myRow in tablePhuTung.AsEnumerable()
                               where myRow.Field<int>("IdKho") == IdKho
                               select myRow);

            if (tableResult.Count() > 0)
            {
                comboBoxPhuTung.DisplayMember = "TenPT";
                comboBoxPhuTung.ValueMember = "IdPT";
                comboBoxPhuTung.DataSource = tableResult.CopyToDataTable();
            }
            else
            {
                comboBoxPhuTung.DataSource = null;
                textBoxSoLuongXuat.Clear();
                textBoxSoLuongCon.Clear();
            }
        }
        #endregion

        #region Lấy thông tin khách hàng bảo dưỡng
        private void Load_ThongTinKhachHang()
        {
            if (IdBaoDuong != null)
            {
                cmd.CommandText = @"SELECT kh.IdKhachHang, kh.HoKH, kh.TenKH, kh.GioiTinh, kh.NgaySinh, kh.DienThoai, kh.Diachi,
                                    ls.IdBaoDuong, ls.IdCuaHang, ls.TenXe, ls.BienSo, ls.Sokhung, ls.SoMay, ls.SoKm
                                    FROM KhachHang kh, LichSuBaoDuongXeTam ls
                                    WHERE kh.IdKhachHang = ls.IdKhachHang AND ls.IdBaoDuong=@IdBaoDuong";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);

                tableKhachHang = Class.datatabase.getData(cmd);
            }
        }
        #endregion

        #region Hiển thị thông tin khách hàng lên các TextBox
        private void HienThiThongTinKhachHang()
        {
            if (tableKhachHang.Rows.Count > 0)
            {
                textBoxKhachHang.Text = tableKhachHang.Rows[0]["HoKH"].ToString() + " " + tableKhachHang.Rows[0]["TenKH"].ToString();
                textBoxDiaChi.Text = tableKhachHang.Rows[0]["Diachi"].ToString();
                textBoxDienThoai.Text = tableKhachHang.Rows[0]["DienThoai"].ToString();

                textBoxLoaiXe.Text = tableKhachHang.Rows[0]["TenXe"].ToString();
                textBoxSoDangKy.Text = tableKhachHang.Rows[0]["BienSo"].ToString();
                textBoxSoKhung.Text = tableKhachHang.Rows[0]["Sokhung"].ToString();
                textBoxSoMay.Text = tableKhachHang.Rows[0]["SoMay"].ToString();
                textBoxSoKm.Text = tableKhachHang.Rows[0]["SoKm"].ToString();
            }
        }
        #endregion

        #region Thêm mới công việc
        private void buttonThemCV_Click(object sender, EventArgs e)
        {
            if (comboBoxCongViec.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn Công việc!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            foreach (DataGridViewRow row in dataGridViewCongViec.Rows)
            {
                if (row.Cells["IdTienCong"].Value.ToString() == comboBoxCongViec.SelectedValue.ToString())
                {
                    MessageBox.Show("Công việc đã có trong danh sách!\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            try
            {
                DataRow dr = tableBaoGiaCongViec.NewRow();
                DataTable tableResult = (from myRow in tableCongViec.AsEnumerable()
                                         where myRow.Field<Int32>("IdTienCong") == Convert.ToInt32(comboBoxCongViec.SelectedValue)
                                         select myRow).CopyToDataTable();

                dr["IdTienCong"] = Convert.ToInt32(tableResult.Rows[0]["IdTienCong"].ToString());
                dr["NoiDungCV"] = tableResult.Rows[0]["NoiDungBD"].ToString();
                dr["TienCong"] = Convert.ToDecimal(tableResult.Rows[0]["TienCong"].ToString());
                dr["DaThucHien"] = false;

                tableBaoGiaCongViec.Rows.Add(dr);

                LoadDanhSachCongViec();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region Thêm phụ tùng
        private void buttonThemPT_Click(object sender, EventArgs e)
        {
            if (comboBoxPhuTung.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn phụ tùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (String.IsNullOrEmpty(textBoxSoLuongXuat.Text))
            {
                MessageBox.Show("Bạn chưa nhập vào số lượng xuất ra!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            foreach (DataGridViewRow row in dataGridViewPhuTung.Rows)
            {
                if (row.Cells["IdPhuTung"].Value.ToString() == comboBoxPhuTung.SelectedValue.ToString())
                {
                    MessageBox.Show("Phụ tùng đã có trong danh sách!\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
            }

            try
            {
                DataRow dr = tableBaoGiaPhuTung.NewRow();
                DataTable tableResult = (from myRow in tablePhuTung.AsEnumerable()
                                         where myRow.Field<long>("IdPT") == Convert.ToInt64(comboBoxPhuTung.SelectedValue)
                                         select myRow).CopyToDataTable();

                dr["IdPhuTung"] = Convert.ToInt64(tableResult.Rows[0]["IdPT"].ToString());
                dr["IdKho"] = Convert.ToInt64(tableResult.Rows[0]["IdKho"].ToString());
                dr["MaPT"] = tableResult.Rows[0]["MaPT"].ToString();
                dr["TenPT"] = tableResult.Rows[0]["TenPT"].ToString();
                dr["DVT"] = tableResult.Rows[0]["DVT"].ToString();
                dr["SoLuong"] = Convert.ToInt32(textBoxSoLuongXuat.Text);
                dr["DonGia"] = Convert.ToDecimal(tableResult.Rows[0]["DonGia"].ToString());
                dr["ThanhTien"] = Convert.ToDecimal(textBoxSoLuongXuat.Text) * Convert.ToDecimal(tableResult.Rows[0]["DonGia"].ToString());
                dr["DaThucHien"] = false;

                tableBaoGiaPhuTung.Rows.Add(dr);

                LoadDanhSachPhuTung();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region textBoxSoLuongXuat_TextChanged
        private void textBoxSoLuongXuat_TextChanged(object sender, EventArgs e)
        {
            int flag;

            if (!String.IsNullOrEmpty(textBoxSoLuongXuat.Text))
            {
                if (!String.IsNullOrEmpty(textBoxSoLuongCon.Text) && int.TryParse(textBoxSoLuongCon.Text, out flag) == false)
                {
                    MessageBox.Show("Số lượng trong kho không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    textBoxSoLuongXuat.Clear();
                    return;
                }

                if (!String.IsNullOrEmpty(textBoxSoLuongXuat.Text) && int.TryParse(textBoxSoLuongXuat.Text, out flag) == false)
                {
                    MessageBox.Show("Số lượng xuất ra không đúng định dạng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    textBoxSoLuongXuat.Clear();
                    textBoxSoLuongXuat.Focus();
                    return;
                }

                if (!String.IsNullOrEmpty(textBoxSoLuongCon.Text) && int.Parse(textBoxSoLuongXuat.Text) > int.Parse(textBoxSoLuongCon.Text))
                {
                    MessageBox.Show("Số lượng xuất ra không được lớn hơn số lượng còn!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    textBoxSoLuongXuat.Clear();
                    textBoxSoLuongXuat.Focus();
                    return;
                }
            }
        }
        #endregion

        #region comboBoxPhuTung_SelectedIndexChanged
        private void comboBoxPhuTung_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (comboBoxPhuTung.SelectedValue != null)
                {
                    textBoxSoLuongXuat.Enabled = true;

                    var result = (from myRow in tablePhuTung.AsEnumerable()
                                  where myRow.Field<long>("IdPT") == Convert.ToInt64(comboBoxPhuTung.SelectedValue)
                                  select myRow);

                    textBoxSoLuongCon.Text = result.FirstOrDefault().Field<int>("SoLuong").ToString();
                }
                else
                    textBoxSoLuongXuat.Enabled = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thống báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        #endregion

        #region dataGridViewCongViec_CellContentClick
        private void dataGridViewCongViec_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridViewCongViec.Columns["XoaCV"].Index)
                    {
                        int IdCV = Convert.ToInt32(dataGridViewCongViec.Rows[e.RowIndex].Cells["IdTienCong"].Value);

                        if (!String.IsNullOrEmpty(IdBaoGiaSuaChua))
                        {
                            if (MessageBox.Show("Báo giá sửa chữa đã được lưu!\nCông việc thực hiện sẽ được xóa trong báo giá.\nBạn có thực sự muốn xóa?", "Thông báo xóa công viêc?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                cmd.CommandText = "DELETE FROM BaoGiaCongThoTam WHERE IdTienCong = @IdTienCong";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdTienCong", dataGridViewCongViec.Rows[e.RowIndex].Cells["IdTienCong"].Value.ToString());

                                Class.datatabase.ExcuteNonQuery(cmd);
                            }
                            else
                                return;
                        }

                        var result = from row in tableBaoGiaCongViec.AsEnumerable()
                                     where row.Field<int>("IdTienCong") != IdCV
                                     select row;

                        if (result.Count() <= 0)
                            tableBaoGiaCongViec.Clear();
                        else
                            tableBaoGiaCongViec = result.CopyToDataTable();

                        LoadDanhSachCongViec();
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
        }
        #endregion

        #region dataGridViewPhuTung_CellContentClick
        private void dataGridViewPhuTung_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridViewPhuTung.Columns["XoaPT"].Index)
                    {
                        int IdPT = Convert.ToInt32(dataGridViewPhuTung.Rows[e.RowIndex].Cells["IdPhuTung"].Value);

                        if (!String.IsNullOrEmpty(IdBaoGiaSuaChua))
                        {
                            if (MessageBox.Show("Báo giá sửa chữa đã được lưu!\nPhụ tùng sẽ được xóa trong báo giá.\nBạn có thực sự muốn xóa?", "Thông báo xóa phụ tùng?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                cmd.CommandText = "DELETE FROM BaoGiaPhuTungTam WHERE IdPhuTung = @IdPhuTung";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdPhuTung", dataGridViewPhuTung.Rows[e.RowIndex].Cells["IdPhuTung"].Value.ToString());

                                Class.datatabase.ExcuteNonQuery(cmd);
                            }
                            else
                                return;
                        }

                        var result = from row in tableBaoGiaPhuTung.AsEnumerable()
                                     where row.Field<int>("IdPhuTung") != IdPT
                                     select row;

                        if (result.Count() <= 0)
                            tableBaoGiaPhuTung.Clear();
                        else
                            tableBaoGiaPhuTung = result.CopyToDataTable();

                        LoadDanhSachPhuTung();
                    }
                }
            }
            catch { }
        }
        #endregion

        #region dataGridViewCongViec_DataBindingComplete
        private void dataGridViewCongViec_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal tongtienCV = 0;

            try
            {
                foreach (DataGridViewRow row in dataGridViewCongViec.Rows)
                {
                    tongtienCV += Convert.ToDecimal(row.Cells["ThanhTien"].Value);
                }

                textBoxTienCong.Text = tongtienCV.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion

        #region dataGridViewPhuTung_DataBindingComplete
        private void dataGridViewPhuTung_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal tongtienPT = 0;

            try
            {
                foreach (DataGridViewRow row in dataGridViewPhuTung.Rows)
                {
                    tongtienPT += Convert.ToDecimal(row.Cells["ThanhTienPT"].Value);
                }

                textBoxTienPhuTung.Text = tongtienPT.ToString();
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        #endregion

        #region textBoxTienCong_TextChanged
        private void textBoxTienCong_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTienCong.Text))
            {
                textBoxTienCong.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienCong.Text));
                textBoxTienCong.SelectionStart = textBoxTienCong.Text.Length;
            }

            if (!String.IsNullOrEmpty(textBoxTienCong.Text) && !String.IsNullOrEmpty(textBoxTienPhuTung.Text) && !String.IsNullOrEmpty(textBoxTienVAT.Text))
            {
                textBoxTienSauVAT.Text = (Convert.ToDecimal(textBoxTienCong.Text) + Convert.ToDecimal(textBoxTienPhuTung.Text) + Convert.ToDecimal(textBoxTienVAT.Text)).ToString();
            }
        }
        #endregion

        #region textBoxTienPhuTung_TextChanged
        private void textBoxTienPhuTung_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTienPhuTung.Text))
            {
                textBoxTienPhuTung.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienPhuTung.Text));
                textBoxTienPhuTung.SelectionStart = textBoxTienPhuTung.Text.Length;

                textBoxTienVAT.Text = (Convert.ToDecimal(textBoxTienPhuTung.Text) * (decimal)0.1).ToString();
            }

            if (!String.IsNullOrEmpty(textBoxTienCong.Text) && !String.IsNullOrEmpty(textBoxTienPhuTung.Text) && !String.IsNullOrEmpty(textBoxTienVAT.Text))
            {
                textBoxTienSauVAT.Text = (Convert.ToDecimal(textBoxTienCong.Text) + Convert.ToDecimal(textBoxTienPhuTung.Text) + Convert.ToDecimal(textBoxTienVAT.Text)).ToString();
            }
        }
        #endregion

        #region textBoxTienVAT_TextChanged
        private void textBoxTienVAT_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTienVAT.Text))
            {
                textBoxTienVAT.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienVAT.Text));
                textBoxTienVAT.SelectionStart = textBoxTienVAT.Text.Length;
            }

            if (!String.IsNullOrEmpty(textBoxTienCong.Text) && !String.IsNullOrEmpty(textBoxTienPhuTung.Text) && !String.IsNullOrEmpty(textBoxTienVAT.Text))
            {
                textBoxTienSauVAT.Text = (Convert.ToDecimal(textBoxTienCong.Text) + Convert.ToDecimal(textBoxTienPhuTung.Text) + Convert.ToDecimal(textBoxTienVAT.Text)).ToString();
            }
        }
        #endregion

        #region textBoxTienSauVAT_TextChanged
        private void textBoxTienSauVAT_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTienCong.Text))
            {
                textBoxTienSauVAT.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienSauVAT.Text));
                textBoxTienSauVAT.SelectionStart = textBoxTienSauVAT.Text.Length;

                textBoxTienBangChu.Text = doctien.ChuyenSo(textBoxTienSauVAT.Text.Replace(",", ""));
            }
        }
        #endregion

        #region buttonThoat_Click
        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region buttonInBaoGia_Click
        private void buttonInBaoGia_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(IdBaoGiaSuaChua))
            {
                MessageBox.Show("Thông tin báo giá không tồn tại!\nCần lưu thông tin báo giá trước khi in.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            FrmInBangBaoGia frmInBaoGia = new FrmInBangBaoGia();
            frmInBaoGia.IdBaoDuongTam = IdBaoDuong;
            frmInBaoGia.ShowDialog();
        }
        #endregion

        #region buttonDongYSuaChua_Click
        private void buttonDongYSuaChua_Click(object sender, EventArgs e)
        {
            //Khách hàng đồng ý sửa chữa với báo giá.
            buttonDongYSuaChua.Enabled = false;

            //Lấy thông tin công việc & phụ tùng đang bảo dưỡng
            DataTable tablePhuTungDangBaoDuong = new DataTable();
            DataTable tableCongViecDangBaoDuong = new DataTable();

            cmd.CommandText = @"SELECT IdTienCong, IdCongTy, IdBaoDuong
                                FROM ThoDichVu_TienCongThoTam WHERE IdBaoDuong = @IdBaoDuong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);

            tableCongViecDangBaoDuong = Class.datatabase.getData(cmd);

            cmd.CommandText = @"SELECT IdBaoDuong, IdPhuTung, TenPhuTung, MaPT, IdKho
                                FROM LichSuBaoDuongChiTietTam2 WHERE IdBaoDuong = @IdBaoDuong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);

            tablePhuTungDangBaoDuong = Class.datatabase.getData(cmd);
            
            //Lưu báo giá nếu
            buttonLuu_Click(sender, new EventArgs());

            //Lưu chi tiết báo giá
            using (SqlConnection cnn = Class.datatabase.getConnection())
            {
                cnn.Open();
                cmd.Connection = cnn;

                using (SqlTransaction tran = cnn.BeginTransaction())
                {
                    cmd.Transaction = tran;

                    try
                    {
                        foreach (DataGridViewRow row in dataGridViewCongViec.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells["DaThucHienCV"].Value) == true)
                            {
                                //So sánh với công việc đang bảo dưỡng
                                var result = from myRow in tableCongViecDangBaoDuong.AsEnumerable()
                                             where myRow.Field<int>("IdTienCong") == Convert.ToInt32(row.Cells["IdTienCong"].Value)
                                             select myRow;

                                if (result.Count() <= 0)
                                {
                                    //Thêm vào bảng công thợ tạm
                                    cmd.CommandText = @"INSERT INTO ThoDichVu_TienCongThoTam
                                                        (IdTienCong, NgaySuaChua, IdCongTy, IdBaoDuong, NoiDungBD, TienCong, TienKhachTra)
                                                        VALUES(@IdTienCong,@NgaySuaChua,@IdCongTy,@IdBaoDuong,@NoiDungBD,@TienCong,@TienKhachTra)";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@IdTienCong", row.Cells["IdTienCong"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@NgaySuaChua", tableBaoGia.Rows[0]["NgayBaoGia"].ToString());
                                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                                    cmd.Parameters.AddWithValue("@NoiDungBD", row.Cells["NoiDungCV"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@TienCong", row.Cells["ThanhTien"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@TienKhachTra", row.Cells["ThanhTien"].Value.ToString());

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        //Lưu báo giá phụ tùng
                        foreach (DataGridViewRow row in dataGridViewPhuTung.Rows)
                        {
                            if (Convert.ToBoolean(row.Cells["DaThucHienPT"].Value) == true)
                            {
                                //So sánh với phụ tùng đang bảo dưỡng
                                var result = from myRow in tablePhuTungDangBaoDuong.AsEnumerable()
                                             where myRow.Field<String>("IdPhuTung") == row.Cells["IdPhuTung"].Value.ToString()
                                             select myRow;

                                if (result.Count() <= 0)
                                {
                                    //Thêm vào lịch sử bảo dưỡng chi tiết tạm
                                    cmd.CommandText = @"INSERT INTO LichSuBaoDuongChiTietTam2
                                                        (IdBaoDuong, MaPT, Soluong, TenPhuTung, Gia, GiaTien, IdKho, IdPhuTung)
                                                        VALUES (@IdBaoDuong,@MaPT,@Soluong,@TenPhuTung,@Gia,@GiaTien,@IdKho,@IdPhuTung)";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                                    cmd.Parameters.AddWithValue("@MaPT", row.Cells["MaPT"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@Soluong", row.Cells["SoLuong"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@TenPhuTung", row.Cells["TenPT"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@Gia", row.Cells["DonGia"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@GiaTien", row.Cells["ThanhTienPT"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@IdKho", row.Cells["IdKho"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@IdPhuTung", row.Cells["IdPhuTung"].Value.ToString());

                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        tran.Commit();
                        MessageBox.Show("Nhận công việc và Phụ tùng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        buttonDongYSuaChua.Enabled = true;

                        if(LayPhuTung != null && LayCongViec != null)
                        {
                            LayPhuTung();
                            LayCongViec();

                            this.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        buttonDongYSuaChua.Enabled = true;
                        tran.Rollback();
                    }
                }
            }
        }
        #endregion

        #region buttonLuu_Click
        private void buttonLuu_Click(object sender, EventArgs e)
        {
            buttonLuu.Enabled = false;

            if (dataGridViewCongViec.Rows.Count <= 0 && dataGridViewPhuTung.Rows.Count <= 0)
            {
                MessageBox.Show("Không tồn tại danh sách Công việc và Phụ tùng!\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                buttonLuu.Enabled = true;
                return;
            }

            //if (String.IsNullOrEmpty(textBoxCoVanDV.Text))
            //{
            //    MessageBox.Show("Bạn chưa nhập tên Cố vấn dịch vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    buttonLuu.Enabled = true;
            //    return;
            //}

            //if (String.IsNullOrEmpty(textBoxTruongPhong.Text))
            //{
            //    MessageBox.Show("Bạn chưa nhập tên Trưởng phòng dịch vụ!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    buttonLuu.Enabled = true;
            //    return;
            //}

            using (SqlConnection cnn = Class.datatabase.getConnection())
            {
                cnn.Open();
                cmd.Connection = cnn;

                using (SqlTransaction tran = cnn.BeginTransaction())
                {
                    cmd.Transaction = tran;

                    if (String.IsNullOrEmpty(IdBaoGiaSuaChua))
                    {
                        //Lưu lần đầu

                        #region Lưu báo giá lần đầu

                        try
                        {
                            //Lưu báo giá chung
                            cmd.CommandText = @"INSERT INTO BaoGiaSuaChuaTam
                                                (IdKhachHang, IdBaoDuong, NgayBaoGia, TongTienVatTu, TongTienCong, VAT, TongSauVAT, CoVanDV, TruongPhongDV)
                                                VALUES (@IdKhachHang,@IdBaoDuong,@NgayBaoGia,@TongTienVatTu,@TongTienCong,@VAT,@TongSauVAT,@CoVanDV,@TruongPhongDV)
                                                SELECT @@IDENTITY";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdKhachHang", tableKhachHang.Rows[0]["IdKhachHang"].ToString());
                            cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                            cmd.Parameters.AddWithValue("@NgayBaoGia", DateTime.Now);
                            cmd.Parameters.AddWithValue("@TongTienVatTu", textBoxTienPhuTung.Text);
                            cmd.Parameters.AddWithValue("@TongTienCong", textBoxTienCong.Text);
                            cmd.Parameters.AddWithValue("@VAT", textBoxTienVAT.Text);
                            cmd.Parameters.AddWithValue("@TongSauVAT", textBoxTienSauVAT.Text);
                            cmd.Parameters.AddWithValue("@CoVanDV", textBoxCoVanDV.Text);
                            cmd.Parameters.AddWithValue("@TruongPhongDV", textBoxTruongPhong.Text);

                            IdBaoGiaSuaChua = cmd.ExecuteScalar().ToString();

                            //Lưu báo giá công việc
                            foreach (DataGridViewRow row in dataGridViewCongViec.Rows)
                            {
                                cmd.CommandText = @"INSERT INTO BaoGiaCongThoTam
                                                    (IdBaoGia, IdTienCong, NoiDungCV, TienCong, DaThucHien)
                                                    VALUES (@IdBaoGia,@IdTienCong,@NoiDungCV,@TienCong,@DaThucHien)";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdBaoGia", IdBaoGiaSuaChua);
                                cmd.Parameters.AddWithValue("@IdTienCong", row.Cells["IdTienCong"].Value.ToString());
                                cmd.Parameters.AddWithValue("@NoiDungCV", row.Cells["NoiDungCV"].Value.ToString());
                                cmd.Parameters.AddWithValue("@TienCong", row.Cells["TienCong"].Value.ToString());
                                cmd.Parameters.AddWithValue("@DaThucHien", row.Cells["DaThucHienCV"].Value);

                                cmd.ExecuteNonQuery();
                            }

                            //Lưu báo giá phụ tùng
                            foreach (DataGridViewRow row in dataGridViewPhuTung.Rows)
                            {
                                cmd.CommandText = @"INSERT INTO BaoGiaPhuTungTam
                                                    (IdBaoGia, IdPhuTung, IdKho, MaPT, TenPT, DVT, SoLuong, DonGia, ThanhTien, DaThucHien)
                                                    VALUES (@IdBaoGia,@IdPhuTung,@IdKho,@MaPT,@TenPT,@DVT,@SoLuong,@DonGia,@ThanhTien,@DaThucHien)";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdBaoGia", IdBaoGiaSuaChua);
                                cmd.Parameters.AddWithValue("@IdPhuTung", row.Cells["IdPhuTung"].Value.ToString());
                                cmd.Parameters.AddWithValue("@IdKho", row.Cells["IdKho"].Value.ToString());
                                cmd.Parameters.AddWithValue("@MaPT", row.Cells["MaPT"].Value.ToString());
                                cmd.Parameters.AddWithValue("@TenPT", row.Cells["TenPT"].Value.ToString());
                                cmd.Parameters.AddWithValue("@DVT", row.Cells["DVT"].Value.ToString());
                                cmd.Parameters.AddWithValue("@SoLuong", row.Cells["SoLuong"].Value.ToString());
                                cmd.Parameters.AddWithValue("@DonGia", row.Cells["DonGia"].Value.ToString());
                                cmd.Parameters.AddWithValue("@ThanhTien", row.Cells["ThanhTienPT"].Value.ToString());
                                cmd.Parameters.AddWithValue("@DaThucHien", row.Cells["DaThucHienPT"].Value);

                                cmd.ExecuteNonQuery();
                            }

                            tran.Commit();

                            MessageBox.Show("Lưu thông tin báo giá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            buttonLuu.Enabled = true;
                            tran.Rollback();
                        }
                        finally
                        {
                            HienThiDuLieuLenControls();
                            buttonLuu.Enabled = true;
                        }

                        #endregion Lưu báo giá lần đầu
                    }
                    else
                    {
                        try
                        {
                            //Lưu báo giá chung
                            cmd.CommandText = @"UPDATE BaoGiaSuaChuaTam
                                                SET IdKhachHang = @IdKhachHang, IdBaoDuong = @IdBaoDuong, TongTienVatTu = @TongTienVatTu,
                                                TongTienCong = @TongTienCong, VAT = @VAT, TongSauVAT = @TongSauVAT, CoVanDV = @CoVanDV, TruongPhongDV = @TruongPhongDV
                                                WHERE IdBaoGia = @IdBaoGia";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoGia", IdBaoGiaSuaChua);
                            cmd.Parameters.AddWithValue("@IdKhachHang", tableKhachHang.Rows[0]["IdKhachHang"].ToString());
                            cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                            cmd.Parameters.AddWithValue("@TongTienVatTu", textBoxTienPhuTung.Text);
                            cmd.Parameters.AddWithValue("@TongTienCong", textBoxTienCong.Text);
                            cmd.Parameters.AddWithValue("@VAT", textBoxTienVAT.Text);
                            cmd.Parameters.AddWithValue("@TongSauVAT", textBoxTienSauVAT.Text);
                            cmd.Parameters.AddWithValue("@CoVanDV", textBoxCoVanDV.Text);
                            cmd.Parameters.AddWithValue("@TruongPhongDV", textBoxTruongPhong.Text);

                            cmd.ExecuteNonQuery();

                            //Lưu báo giá công việc
                            foreach (DataGridViewRow row in dataGridViewCongViec.Rows)
                            {
                                if (String.IsNullOrEmpty(row.Cells["IdBaoGiaCongTho"].Value.ToString()))
                                {
                                    cmd.CommandText = @"INSERT INTO BaoGiaCongThoTam
                                                    (IdBaoGia, IdTienCong, NoiDungCV, TienCong, DaThucHien)
                                                    VALUES (@IdBaoGia,@IdTienCong,@NoiDungCV,@TienCong,@DaThucHien)";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@IdBaoGia", IdBaoGiaSuaChua);
                                    cmd.Parameters.AddWithValue("@IdTienCong", row.Cells["IdTienCong"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@NoiDungCV", row.Cells["NoiDungCV"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@TienCong", row.Cells["TienCong"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@DaThucHien", row.Cells["DaThucHienCV"].Value);

                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmd.CommandText = @"UPDATE BaoGiaCongThoTam
                                                        SET IdBaoGia = @IdBaoGia, IdTienCong = @IdTienCong, NoiDungCV = @NoiDungCV,
                                                        TienCong = @TienCong, DaThucHien = @DaThucHien
                                                        WHERE IdBaoGiaCongTho = @IdBaoGiaCongTho";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@IdBaoGiaCongTho", row.Cells["IdBaoGiaCongTho"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@IdBaoGia", IdBaoGiaSuaChua);
                                    cmd.Parameters.AddWithValue("@IdTienCong", row.Cells["IdTienCong"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@NoiDungCV", row.Cells["NoiDungCV"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@TienCong", row.Cells["TienCong"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@DaThucHien", row.Cells["DaThucHienCV"].Value);

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            //Lưu báo giá phụ tùng
                            foreach (DataGridViewRow row in dataGridViewPhuTung.Rows)
                            {
                                if (String.IsNullOrEmpty(row.Cells["ColumnIdBaoGiaPhuTung"].Value.ToString()))
                                {
                                    cmd.CommandText = @"INSERT INTO BaoGiaPhuTungTam
                                                    (IdBaoGia, IdPhuTung, IdKho, MaPT, TenPT, DVT, SoLuong, DonGia, ThanhTien, DaThucHien)
                                                    VALUES (@IdBaoGia,@IdPhuTung,@IdKho,@MaPT,@TenPT,@DVT,@SoLuong,@DonGia,@ThanhTien,@DaThucHien)";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@IdBaoGia", IdBaoGiaSuaChua);
                                    cmd.Parameters.AddWithValue("@IdPhuTung", row.Cells["IdPhuTung"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@IdKho", row.Cells["IdKho"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@MaPT", row.Cells["MaPT"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@TenPT", row.Cells["TenPT"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@DVT", row.Cells["DVT"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@SoLuong", row.Cells["SoLuong"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@DonGia", row.Cells["DonGia"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@ThanhTien", row.Cells["ThanhTienPT"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@DaThucHien", row.Cells["DaThucHienPT"].Value);

                                    cmd.ExecuteNonQuery();
                                }
                                else
                                {
                                    cmd.CommandText = @"UPDATE BaoGiaPhuTungTam
                                                        SET IdBaoGia = @IdBaoGia, IdPhuTung = @IdPhuTung, IdKho = @IdKho, TenPT = @TenPT, MaPT = @MaPT,
                                                        DVT = @DVT, DonGia = @DonGia, SoLuong = @SoLuong, ThanhTien = @ThanhTien, DaThucHien = @DaThucHien
                                                        WHERE IdBaoGiaPhuTung = @IdBaoGiaPhuTung";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@IdBaoGiaPhuTung", row.Cells["ColumnIdBaoGiaPhuTung"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@IdBaoGia", IdBaoGiaSuaChua);
                                    cmd.Parameters.AddWithValue("@IdPhuTung", row.Cells["IdPhuTung"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@IdKho", row.Cells["IdKho"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@MaPT", row.Cells["MaPT"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@TenPT", row.Cells["TenPT"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@DVT", row.Cells["DVT"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@SoLuong", row.Cells["SoLuong"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@DonGia", row.Cells["DonGia"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@ThanhTien", row.Cells["ThanhTienPT"].Value.ToString());
                                    cmd.Parameters.AddWithValue("@DaThucHien", row.Cells["DaThucHienPT"].Value);

                                    cmd.ExecuteNonQuery();
                                }
                            }

                            tran.Commit();

                            MessageBox.Show("Lưu thông tin báo giá thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            buttonLuu.Enabled = true;
                            tran.Rollback();
                        }
                        finally
                        {
                            HienThiDuLieuLenControls();
                            buttonLuu.Enabled = true;
                        }
                    }
                }

                cnn.Close();
            }
        }
        #endregion

        #region comboBoxCongViec_KeyDown
        private void comboBoxCongViec_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                buttonThemCV_Click(sender, new EventArgs());
            }
        }
        #endregion

        #region comboBoxKhoPhuTung_SelectedIndexChanged
        private void comboBoxKhoPhuTung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxKhoPhuTung.SelectedValue != null)
            {
                Load_cboPhuTung(Convert.ToInt32(comboBoxKhoPhuTung.SelectedValue));
            }
        }
        #endregion

        #region Thêm mới công việc
        private void buttonThemMoiCV_Click(object sender, EventArgs e)
        {
            frmQuanLyCongViec frmCV = new frmQuanLyCongViec();
            frmCV.Refresh = new frmQuanLyCongViec.ReloadCombobox(ReloadCombobox);

            frmCV.ShowDialog();
        }
        #endregion
    }
}