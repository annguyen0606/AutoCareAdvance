using AutoCareV2._0.Class;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class UcBanXe : UserControl
    {
        public UcBanXe()
        {
            InitializeComponent();
        }

        private decimal giaban, vat;
        private decimal thanhtien;
        private decimal tienphuthu;
        private decimal tongtien = 0m;
        private string idkhachhang = "";
        private string _TenXe = "";
        private KhDB classdb = new KhDB();
        private DataTable dtThongTinXe = new DataTable();
        private DataTable dtXeMay = new DataTable();
        private DataTable dtKhachHang = new DataTable();
        private DataTable dtKieuXe = new DataTable();
        private DataTable dtNhaCungCap = new DataTable();
        private DataTable dtXe = new DataTable();
        private DataTable dtNhanVienBH = new DataTable();


        private void LoadKieuXe()
        {
            SqlCommand cmd = new SqlCommand("Select IdLoaiXe, TenLoaiXe from LoaiXe Where IdCongTy =@IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtKieuXe = Class.datatabase.getData(cmd);
        }

        private void LoadNhanVien()
        {
            var cmd = new SqlCommand("select TenNhanVien,IdNhanVien from NhanVien where Idcongty = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtNhanVienBH = Class.datatabase.getData(cmd);
            cboNhanVienBan.DataSource = dtNhanVienBH;
            cboNhanVienBan.DisplayMember = "TenNhanVien";
            cboNhanVienBan.ValueMember = "IdNhanVien";
        }

        private void LoadXe()
        {
            SqlCommand cmd = new SqlCommand("Select * from Xemay Where IdCongTy =@IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtXe = Class.datatabase.getData(cmd);
        }

        private void GiaBan_textchanged(object sender, EventArgs e)
        {
            if (dgvXeBan.CurrentCell.ColumnIndex == dgvXeBan.Columns["DonGia"].Index)
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
                dgvXeBan.CurrentRow.Cells["VAT1"].Value = (giaban / 10).ToString("0,0");
                dgvXeBan.CurrentRow.Cells["ThanhTien1"].Value = (giaban + (giaban / 10)).ToString("0,0");
                thanhtien = 0;
                for (int i = 0; i < dgvXeBan.Rows.Count - 1; i++)
                {
                    try
                    {
                        thanhtien += Convert.ToDecimal(dgvXeBan.Rows[i].Cells["ThanhTien1"].Value);
                    }
                    catch { }
                }
                tienphuthu = 0;
                try
                {
                    tienphuthu = Convert.ToDecimal(txtPhuThu.Text);
                }
                catch { }
                txtTongTien.Text = thanhtien.ToString("0,0");
                txt_TongSoTien.Text = (tienphuthu + thanhtien).ToString("0,0");
            }
        }

        private void txtVAT_changed(object sender, EventArgs e)
        {
            if (dgvXeBan.Columns["VAT1"].Index == dgvXeBan.CurrentCell.ColumnIndex)
            {
                TextBox txt = (sender as TextBox);
                try
                {
                    if (String.IsNullOrEmpty(txt.Text))
                    {
                        vat = 0;
                    }
                    else
                    {
                        vat = Convert.ToDecimal(txt.Text);
                    }
                }
                catch { }
                txt.Text = vat.ToString("0,0");
                txt.SelectionStart = txt.Text.Length;
                try
                {
                    giaban = Convert.ToDecimal(dgvXeBan.CurrentRow.Cells["DonGia"].Value);
                }
                catch { giaban = 0; }
                dgvXeBan.CurrentRow.Cells["ThanhTien1"].Value = (giaban + vat).ToString();
                thanhtien = 0;
                decimal phuthu = 0;
                try
                {
                    phuthu = Convert.ToDecimal(txtPhuThu.Text);
                }
                catch { }
                for (int i = 0; i < dgvXeBan.Rows.Count - 1; i++)
                {
                    try
                    {
                        thanhtien += Convert.ToDecimal(dgvXeBan.Rows[i].Cells["ThanhTien1"].Value);
                    }
                    catch { }
                }
                txtTongTien.Text = (thanhtien + phuthu).ToString("0,0");
            }
        }

        private void Tailai()
        {
            txtTongTien.Text = "0";
            txtPhuThu.Text = "0";
            txt_TongSoTien.Text = "0";

            LoadKieuXe();
            TenLoaiXe.DataSource = dtKieuXe;
            TenLoaiXe.ValueMember = "IdLoaiXe";
            TenLoaiXe.DisplayMember = "TenLoaiXe";

            dtKhachHang.Clear();
            dtKhachHang = classdb.LayDanhSachKhachHang();
            cboKhachHang.DataSource = dtKhachHang;
            cboKhachHang.DisplayMember = "HTKhachHang";
            cboKhachHang.ValueMember = "IdKhachHang";
            cboKhachHang.SelectedIndex = -1;

            dtThongTinXe = new DataTable();
            dtThongTinXe = classdb.LayThongTinXeTrongKho();
            this.SoKhung.DataSource = dtThongTinXe;
            this.SoKhung.DisplayMember = "SoKhung";
            this.SoMay.DataSource = dtThongTinXe;
            this.SoMay.DisplayMember = "SoMay";

            dtXeMay = classdb.LayTenXe();
            this.TenXe1.DataSource = dtXeMay;
            this.TenXe1.DisplayMember = "TenXe";
            this.TenXe1.ValueMember = "IdXe";

            this.Kho.DataSource = classdb.LoadTenKho();
            this.Kho.DisplayMember = "TenKho";
            this.Kho.ValueMember = "IdKho";

            dtNhaCungCap = classdb.NhaCungCap();
            this.NhaCungCap.DataSource = dtNhaCungCap;
            this.NhaCungCap.DisplayMember = "TenNhaCungCap";
            this.NhaCungCap.ValueMember = "IdNhaCungCap";

            dateTimeInputNgayHoaDon.Value = DateTime.Now;
            dgvXeBan.Rows[0].Cells["DangKiem"].Value = true;
            dgvXeBan.Rows[0].Cells["SoBaoHanh"].Value = true;
        }

        private void UcBanXe_Load(object sender, EventArgs e)
        {
           
            txtTongTien.Text = "0";
            txtPhuThu.Text = "0";
            txt_TongSoTien.Text = "0";

            LoadXe();
            LoadKieuXe();

            TenLoaiXe.DataSource = dtKieuXe;
            TenLoaiXe.ValueMember = "IdLoaiXe";
            TenLoaiXe.DisplayMember = "TenLoaiXe";

            dtKhachHang = classdb.LayDanhSachKhachHang();
            cboKhachHang.DataSource = dtKhachHang;
            cboKhachHang.DisplayMember = "HTKhachHang";
            cboKhachHang.ValueMember = "IdKhachHang";
            cboKhachHang.SelectedIndex = -1;

            dtThongTinXe = new DataTable();
            dtThongTinXe = classdb.LayThongTinXeTrongKho();

            this.Kho.DataSource = classdb.LoadTenKho();
            this.Kho.DisplayMember = "TenKho";
            this.Kho.ValueMember = "IdKho";

            dtNhaCungCap = classdb.NhaCungCap();
            this.NhaCungCap.DataSource = dtNhaCungCap;
            this.NhaCungCap.DisplayMember = "TenNhaCungCap";
            this.NhaCungCap.ValueMember = "IdNhaCungCap";

            dateTimeInputNgayHoaDon.Value = DateTime.Now;
            dgvXeBan.Rows[0].Cells["DangKiem"].Value = true;
            dgvXeBan.Rows[0].Cells["SoBaoHanh"].Value = true;

            LoadNhanVien();
        }

        private void cboKhachHang_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(cboKhachHang.SelectedValue.ToString()))
            {
                DataRow[] r = dtKhachHang.Select("IdKhachHang = '" + cboKhachHang.SelectedValue.ToString() + "'");
                if (r.Length > 0)
                {
                    txtDiaChi.Text = Convert.ToString(r[0]["DiaChi"]);
                }
            }
        }

        private void txtPhuThu_TextChanged(object sender, EventArgs e)
        {
            try
            {
                decimal tienPhuThu = decimal.Parse(txtPhuThu.Text);
                txtPhuThu.Text = tienPhuThu.ToString("0,0");
                txtPhuThu.SelectionStart = txtPhuThu.Text.Length;

                if (txtTongTien.Text.Trim() == "")
                {
                    return;
                }

                txt_TongSoTien.Text = (tienPhuThu + decimal.Parse(txtTongTien.Text)).ToString("0,0");
            }
            catch
            {
                if (txtPhuThu.Text.Trim() != "")
                {
                    txtPhuThu.Text = txtPhuThu.Text.Substring(0, txtPhuThu.Text.Length - 1);
                    MessageBox.Show("Tiền phụ thu phải là số", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void dgvXeBan_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (dgvXeBan.Rows.Count > 1)
                {
                    var dataGridViewColumn1 = dgvXeBan.Columns["Kho"];
                    if (dataGridViewColumn1 != null && e.ColumnIndex == dataGridViewColumn1.Index)
                    {
                        string chuoi = dgvXeBan.Rows[e.RowIndex].Cells["Kho"].Value.ToString();

                        DataTable dsxe = LayTenXeTheoKho(chuoi);
                        if (dsxe.Rows.Count > 0)
                        {
                            this.TenXe1.DataSource = dsxe;
                            this.TenXe1.DisplayMember = "TenXe";
                            this.TenXe1.ValueMember = "IdXe";
                        }
                        else
                        {
                            MessageBox.Show("Không có xe nào trong kho đã chọn!", "Thông báo");
                        }
                    }
                    var column = dgvXeBan.Columns["TenXe1"];
                    if (column != null && e.ColumnIndex == column.Index)
                    {
                        string idkho = dgvXeBan.Rows[e.RowIndex].Cells["Kho"].Value.ToString();
                        string idkey = dgvXeBan.Rows[e.RowIndex].Cells["TenXe1"].Value.ToString();

                        DataTable dsxe = LaySoKhungTheoKho_TenXe(idkho, idkey);
                        if (dsxe.Rows.Count > 0)
                        {
                            this.SoKhung.DataSource = dsxe;
                            this.SoKhung.DisplayMember = "SoKhung";
                        }
                        else
                        {
                            dgvXeBan.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                    var viewColumn = dgvXeBan.Columns["SoKhung"];
                    if (viewColumn != null && e.ColumnIndex == viewColumn.Index)
                    {
                        string sokhung = dgvXeBan.Rows[e.RowIndex].Cells["SoKhung"].Value.ToString();
                        string idkho = dgvXeBan.Rows[e.RowIndex].Cells["Kho"].Value.ToString();
                        string idkey = dgvXeBan.Rows[e.RowIndex].Cells["TenXe1"].Value.ToString();

                        DataTable dsxe = LaySoMayTheoKho_TenXe_SoKhung(idkho, idkey, sokhung);
                        if (dsxe.Rows.Count > 0)
                        {
                            this.SoMay.DataSource = dsxe;
                            this.SoMay.DisplayMember = "SoMay";
                        }
                        else
                        {
                            dgvXeBan.Rows.RemoveAt(e.RowIndex);
                        }
                    }
                    var dataGridViewColumn = dgvXeBan.Columns["SoMay"];
                    if (dataGridViewColumn != null && e.ColumnIndex == dataGridViewColumn.Index)
                    {
                        string chuoi = dgvXeBan.Rows[e.RowIndex].Cells["SoMay"].Value.ToString();
                        string sokhung = dgvXeBan.Rows[e.RowIndex].Cells["SoKhung"].Value.ToString();
                        string idkho = dgvXeBan.Rows[e.RowIndex].Cells["Kho"].Value.ToString();
                        string idkey = dgvXeBan.Rows[e.RowIndex].Cells["TenXe1"].Value.ToString();

                        DataTable dsxe = LayThongTinNCC_LoaiXe(idkho, idkey, sokhung, chuoi);
                        if (dsxe.Rows.Count > 0)
                        {
                            dgvXeBan.Rows[e.RowIndex].Cells["DonGia"].Value = dsxe.Rows[0]["DonGia"].ToString();
                        }
                        else
                        {
                            dgvXeBan.Rows.RemoveAt(e.RowIndex);
                        }
                    }

                    //Lấy thông tin đơn giá + tính toán
                    var gridViewColumn = dgvXeBan.Columns["DonGia"];
                    if (gridViewColumn != null && e.ColumnIndex == gridViewColumn.Index)
                    {
                        try
                        {
                            giaban = Convert.ToDecimal(dgvXeBan.Rows[e.RowIndex].Cells["DonGia"].Value.ToString());
                        }
                        catch
                        {
                            // ignored
                        }

                        dgvXeBan.Rows[e.RowIndex].Cells["DonGia"].Value = giaban.ToString("0,0");
                        if (dgvXeBan.CurrentRow != null)
                        {
                            dgvXeBan.CurrentRow.Cells["VAT1"].Value = (giaban / 10).ToString("0,0");
                            dgvXeBan.CurrentRow.Cells["ThanhTien1"].Value = (giaban + (giaban / 10)).ToString("0,0");
                        }
                        thanhtien = 0;
                        for (int i = 0; i < dgvXeBan.Rows.Count - 1; i++)
                        {
                            try
                            {
                                thanhtien += Convert.ToDecimal(dgvXeBan.Rows[i].Cells["ThanhTien1"].Value);
                            }
                            catch
                            {
                                // ignored
                            }
                        }
                        tienphuthu = 0;
                        try
                        {
                            tienphuthu = Convert.ToDecimal(txtPhuThu.Text);
                        }
                        catch
                        {
                            // ignored
                        }
                        txtTongTien.Text = thanhtien.ToString("0,0");
                        txt_TongSoTien.Text = (tienphuthu + thanhtien).ToString("0,0");
                    }
                }
            }
            catch
            {
                // ignored
            }
        }

        private DataTable LayTenXeTheoKho(string idkho)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = Class.datatabase.getConnection())
            {
                cnn.Open();

                SqlDataAdapter adap = new SqlDataAdapter("SELECT xm.IdXe, xm.IDXe + ' - ' + xm.TenXe AS TenXe FROM ChiTietXe ctx, XeMay xm WHERE xm.IdCongTy=@IdCongTy AND ctx.IdKho=@IdKho AND ctx.IdKey=xm.IDXe GROUP BY xm.IDXe + ' - ' + xm.TenXe, xm.IdXe", cnn);

                adap.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                adap.SelectCommand.Parameters.AddWithValue("@IdKho", Convert.ToInt64(idkho));

                adap.Fill(dt);

                cnn.Close();
            }

            return dt;
        }

        private DataTable LaySoKhungTheoKho_TenXe(string idkho, string idkey)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = Class.datatabase.getConnection())
            {
                cnn.Open();

                SqlDataAdapter adap = new SqlDataAdapter(@"SELECT SoKhung, SoMay FROM ChiTietXe WHERE IdCongTy=@IdCongTy AND IdKho=@IdKho AND IdKey=@IdKey", cnn);

                adap.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                adap.SelectCommand.Parameters.AddWithValue("@IdKho", Convert.ToInt64(idkho));
                adap.SelectCommand.Parameters.AddWithValue("@Idkey", idkey);

                adap.Fill(dt);
                cnn.Close();
            }

            return dt;
        }

        private DataTable LaySoMayTheoKho_TenXe_SoKhung(string idkho, string idkey, string sokhung)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = Class.datatabase.getConnection())
            {
                cnn.Open();

                SqlDataAdapter adap = new SqlDataAdapter(@"SELECT SoKhung, SoMay FROM ChiTietXe WHERE IdCongTy=@IdCongTy AND IdKho=@IdKho AND IdKey=@IdKey AND SoKhung=@SoKhung", cnn);

                adap.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                adap.SelectCommand.Parameters.AddWithValue("@IdKho", Convert.ToInt64(idkho));
                adap.SelectCommand.Parameters.AddWithValue("@Idkey", idkey);
                adap.SelectCommand.Parameters.AddWithValue("@SoKhung", sokhung);

                adap.Fill(dt);
                cnn.Close();
            }

            return dt;
        }

        private DataTable LayThongTinNCC_LoaiXe(string idkho, string idkey, string sokhung, string somay)
        {
            DataTable dt = new DataTable();

            using (SqlConnection cnn = Class.datatabase.getConnection())
            {
                cnn.Open();

                SqlDataAdapter adap = new SqlDataAdapter(@"SELECT SoKhung, SoMay, IdNhaCungCap, IdLoaiXe, DonGia FROM ChiTietXe WHERE IdCongTy=@IdCongTy AND IdKho=@IdKho AND IdKey=@IdKey AND SoKhung=@SoKhung AND SoMay=@SoMay", cnn);

                adap.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                adap.SelectCommand.Parameters.AddWithValue("@IdKho", Convert.ToInt64(idkho));
                adap.SelectCommand.Parameters.AddWithValue("@Idkey", idkey);
                adap.SelectCommand.Parameters.AddWithValue("@SoKhung", sokhung);
                adap.SelectCommand.Parameters.AddWithValue("@SoMay", somay);

                adap.Fill(dt);
                cnn.Close();
            }

            return dt;
        }

        private void dgvXeBan_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvXeBan.Columns["DonGia"].Index == dgvXeBan.CurrentCell.ColumnIndex)
            {
                TextBox txt_GiaBan = e.Control as TextBox;
                txt_GiaBan.TextChanged += new EventHandler(GiaBan_textchanged);
            }
            if (dgvXeBan.Columns["VAT1"].Index == dgvXeBan.CurrentCell.ColumnIndex)
            {
                TextBox txt_VAT = e.Control as TextBox;

                txt_VAT.TextChanged += new EventHandler(txtVAT_changed);
            }
        }

        private void dgvXeBan_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            try
            {
                dgvXeBan.Rows[e.RowCount].Cells["DangKiem"].Value = true;
                dgvXeBan.Rows[e.RowCount].Cells["SoBaoHanh"].Value = true;
            }
            catch { }
        }

        private void dgvXeBan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvXeBan.Columns["xoa"].Index && e.RowIndex != dgvXeBan.Rows.Count - 1 && e.RowIndex >= 0 && dgvXeBan.Rows.Count > 1)
            {
                dgvXeBan.Rows.RemoveAt(e.RowIndex);
                tongtien = 0;
                for (int k = 0; k < dgvXeBan.Rows.Count - 1; k++)
                {
                    tongtien += Convert.ToDecimal(dgvXeBan.Rows[k].Cells["ThanhTien1"].Value);
                }
                txtTongTien.Text = tongtien.ToString("0,0");
                txt_TongSoTien.Text = (Convert.ToDecimal(txtPhuThu.Text) + tongtien).ToString("0,0");
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetControls();
        }

        private void ResetControls()
        {
            try
            {
                cboKhachHang.SelectedText = "";
                txtSoHoaDon.Text = "";
                txtGhiChu.Text = "";
                txtDiaChi.Text = "";
                txt_TongSoTien.Text = "0";
                txtTongTien.Text = "0";
                txtPhuThu.Text = "0";
            }
            catch (Exception)
            { }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            cboKhachHang.Focus();

            if (cboKhachHang.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            #region lấy thôn gtin khách hàng đẩy vào api lên caresoft
            //ThongTinKhachHang ttkh = new ThongTinKhachHang();
            //DataRow[] drttkh = dtKhachHang.Select("IdKhachHang = '" + cboKhachHang.SelectedValue + "'");
            //if (drttkh.Length > 0)
            //{
            //    txtDiaChi.Text = Convert.ToString(drttkh[0]["DiaChi"]);
            //    ttkh.address = Convert.ToString(drttkh[0]["DiaChi"]);
            //    ttkh.username = Convert.ToString(drttkh[0]["HTKhachHang"]);
            //    ttkh.note = txtGhiChu.Text;
            //    ttkh.phone_no= Convert.ToString(drttkh[0]["DienThoai"]);
            //    if (drttkh[0]["GioiTinh"].ToString()=="Nam")
            //    {
            //        ttkh.gender = 0;
            //    }
            //    else
            //    {
            //        ttkh.gender = 1;
            //    }
            //}
            //List<Parameter> danhsach = new List<Parameter>();
            #endregion
            if (String.IsNullOrEmpty(txtSoHoaDon.Text))
            {
                MessageBox.Show("Bạn chưa nhập số hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoHoaDon.Focus();
                return;
            }

            if (dateTimeInputNgayHoaDon.ValueObject == null)
            {
                MessageBox.Show("Bạn chưa nhập ngày lập hóa đơn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                dateTimeInputNgayHoaDon.Focus();
                return;
            }

            if (cboKhachHang.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboKhachHang.Focus();
                return;
            }

            if (dgvXeBan.Rows.Count - 1 <= 0)
            {
                MessageBox.Show("Bạn chưa nhập xe vào danh sách xe bán!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboNhanVienBan.SelectedItem == null)
            {
                MessageBox.Show("Bạn chưa chọn nhân viên bán hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            idkhachhang = Convert.ToString(cboKhachHang.SelectedValue);
            SqlCommand cmd2 = new SqlCommand("Select SoHoaDonBanHang from HoaDonbanHang WHERE SoHoaDonBanHang = @SoHoaDonBanHang");
            cmd2.Parameters.AddWithValue("@SoHoaDonBanHang", txtSoHoaDon.Text);
            if (Class.datatabase.getData(cmd2).Rows.Count > 0)
            {
                MessageBox.Show("Số hóa đơn đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoHoaDon.SelectAll();
                txtSoHoaDon.Focus();

                return;
            }
            if (dgvXeBan.Rows.Count > 1)
            {
                tongtien = 0;
                DataTable dtsoKhung = new DataTable();
                dtsoKhung.Columns.Add("SoKhung");
                DataRow r;
                for (int i = 0; i < dgvXeBan.Rows.Count - 1; i++)
                {
                    if (String.IsNullOrEmpty(Convert.ToString(dgvXeBan.Rows[i].Cells["DonGia"].Value)))
                    {
                        MessageBox.Show("Thông tin giá bán của xe tại dòng " + (i + 1).ToString() + " không được để trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    if (dtsoKhung.Select("SoKhung = '" + Convert.ToString(dgvXeBan.Rows[i].Cells["SoKhung"].Value) + "'").Length > 0)
                    {
                        MessageBox.Show("Thông tin xe đã có trong danh sách.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //
                    r = dtsoKhung.NewRow();
                    r["SoKhung"] = Convert.ToString(dgvXeBan.Rows[i].Cells["SoKhung"].Value);
                    dtsoKhung.Rows.Add(r);
                    tongtien += Convert.ToDecimal(dgvXeBan.Rows[i].Cells["DonGia"].Value);
                }

                txtTongTien.Text = tongtien.ToString();

                ////Thread Lưu hóa đơn bán hàng.
                //Thread threadBackgroup = new Thread(new ThreadStart(() =>
                //{
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Class.datatabase.getConnection();
                cmd.CommandTimeout = 0;
                cmd.Connection.Open();
                //SqlTransaction tran = cmd.Connection.BeginTransaction();
                //cmd.Transaction = tran;
                try
                {
                    for (int i = 0; i < dgvXeBan.Rows.Count - 1; i++)
                    {
                        string SoKhung = "";
                        string SoMay = "";
                        try
                        {
                            SoKhung = dgvXeBan.Rows[i].Cells["SoKhung"].Value.ToString();
                        }
                        catch (Exception)
                        { }

                        try
                        {
                            SoMay = Convert.ToString(dgvXeBan.Rows[i].Cells["SoMay"].Value);
                        }
                        catch (Exception)
                        { }

                        string MauXe = "";
                        cmd.CommandText = @"select B.TenMauXe 
                                                from ChiTietXe A
                                                inner join MauXeMay B on A.IdMauXe = B.IdMauXe and A.IdCongTy = B.IdCongTy
                                                where IdKey = @IdKey and A.SoKhung = @SoKhung and A.SoMay = @SoMay and A.IdCongTy = @IdCongTy";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdKey", Convert.ToString(dgvXeBan.Rows[i].Cells["TenXe1"].Value));
                        cmd.Parameters.AddWithValue("@SoKhung", SoKhung);
                        cmd.Parameters.AddWithValue("@SoMay", SoMay);
                        cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                       
                        var rel = cmd.ExecuteScalar();
                        if (rel != null)
                            MauXe = rel.ToString();
                        #region import data to contact
                        //danhsach.Add(new Parameter
                        //{
                        //    id = Define.MauXe,
                        //    value = MauXe,
                        //});
                        //danhsach.Add(new Parameter
                        //{
                        //    id = Define.SoKhung,
                        //    value = SoKhung,
                        //});
                        //danhsach.Add(new Parameter
                        //{
                        //    id = Define.SoMay,
                        //    value = SoMay,
                        //});
                        #endregion
                        //Check xe co con trong kho khong
                        cmd.CommandText = "Delete ChiTietXe WHERE IdCongTy = @IdCongTy And SoKhung = @SoKhung And SoMay = @SoMay And IdKey = @IdXe";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@SoKhung", SoKhung);
                        cmd.Parameters.AddWithValue("@SoMay", SoMay);
                        cmd.Parameters.AddWithValue("@IdXe", Convert.ToString(dgvXeBan.Rows[i].Cells["TenXe1"].Value));
                        if (cmd.ExecuteNonQuery() <= 0)
                        {
                            cmd.Connection.Close();
                            MessageBox.Show("Xe tại dòng " + (i + 1) + " đã bán hoặc không tồn tại trong kho.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        else
                        {
                            DataRow[] r1 = dtXe.Select("IdXe = '" + dgvXeBan.Rows[i].Cells["TenXe1"].Value.ToString() + "'");
                            if (r1.Length > 0)
                            {
                                _TenXe = r1[0]["TenXe"].ToString();
                            }

                            cmd.CommandText = @"insert into Xedaban(TenXe,NgayBan,IdKhachHang,IdCongTy,IdCuaHang,SoKhung,SoMay,SoLuong,DonGia,MauXe,IdLoaiXe)
                                                    values(@TenXe,@NgayBan,@IdKhachHang,@IdCongTy,@IdCuaHang,@SoKhung,@SoMay,@SoLuong,@DonGia,@MauXe,@IdLoaiXe)";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TenXe", _TenXe);
                            cmd.Parameters.AddWithValue("@NgayBan", dateTimeInputNgayHoaDon.Value);
                            cmd.Parameters.AddWithValue("@IdKhachHang", idkhachhang);
                            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdCuaHang", EmployeeInfo.IdCuaHang);
                            cmd.Parameters.AddWithValue("@SoKhung", dgvXeBan.Rows[i].Cells["SoKhung"].Value.ToString());
                            cmd.Parameters.AddWithValue("@SoMay", dgvXeBan.Rows[i].Cells["SoMay"].Value.ToString());
                            cmd.Parameters.AddWithValue("@SoLuong", "1");
                            cmd.Parameters.AddWithValue("@DonGia", Convert.ToDouble(dgvXeBan.Rows[i].Cells["DonGia"].Value.ToString().Replace(",", "").Replace(".", "")));
                            cmd.Parameters.AddWithValue("@MauXe", MauXe);
                            cmd.Parameters.AddWithValue("@IdLoaiXe", dgvXeBan.Rows[i].Cells["TenLoaiXe"].Value.ToString());
                            cmd.ExecuteNonQuery();

                            cmd.CommandText = @"insert into ChiTietHoaDonBan (SoHoaDonBanHang, IDXe, SoKhung, SoMay, DonGia, GhiChu, IdCongTy, IdKho, SoChungTu, VAT)
                                                    values(@SoHoaDonBanHang,@IdXe,@SoKhung,@SoMay,@DonGia,@GhiChu,@IdCongTy,@IdKho,@SoChungTu, @VAT)";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@SoHoaDonBanHang", txtSoHoaDon.Text);
                            cmd.Parameters.AddWithValue("@IdXe", Convert.ToString(dgvXeBan.Rows[i].Cells["TenXe1"].Value.ToString()));
                            cmd.Parameters.AddWithValue("@SoKhung", Convert.ToString(dgvXeBan.Rows[i].Cells["SoKhung"].Value.ToString()));
                            cmd.Parameters.AddWithValue("@SoMay", Convert.ToString(dgvXeBan.Rows[i].Cells["SoMay"].Value.ToString()));
                            cmd.Parameters.AddWithValue("@DonGia", Convert.ToDouble(dgvXeBan.Rows[i].Cells["DonGia"].Value.ToString().Replace(",", "").Replace(".", "")));
                            cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdKho", Convert.ToString(dgvXeBan.Rows[i].Cells["Kho"].Value.ToString()));
                            cmd.Parameters.AddWithValue("@SoChungTu", SqlString.Null);
                            cmd.Parameters.AddWithValue("@VAT", Convert.ToDouble(dgvXeBan.Rows[i].Cells["VAT1"].Value.ToString().Replace(",", "").Replace(".", "")));
                            cmd.ExecuteNonQuery();
                        }
                    }

                    cmd.CommandText = @"insert Into HoaDonBanHang (SoHoaDonBanHang, IdCongTy, SoChungTu, IdKhachHang, IdNhanVien, NgayChungTu, NgayTaoHoaDon, NgayHachToan,IDCuaHang, CoHoaDon, DaNhanHoaDon, TienPhuThu)
                                            values(@SoHoaDonBanHang,@IdCongTy,@SoChungTu,@IdKhachHang,@IdNhanVien,@NgayChungTu,@NgayTaoHoaDon,@NgayHachToan,@IdCuaHang,@CoHoaDon,@DaNhanHoaDon, @TienPhuThu)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SoHoaDonBanHang", txtSoHoaDon.Text);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@SoChungTu", SqlString.Null);
                    cmd.Parameters.AddWithValue("@IdKhachHang", idkhachhang);
                    cmd.Parameters.AddWithValue("@IdNhanVien", cboNhanVienBan.SelectedValue);
                    cmd.Parameters.AddWithValue("@NgayChungTu", SqlDateTime.Null);
                    cmd.Parameters.AddWithValue("@NgayTaoHoaDon", dateTimeInputNgayHoaDon.Value);
                    cmd.Parameters.AddWithValue("@NgayHachToan", SqlDateTime.Null);
                    cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                    cmd.Parameters.AddWithValue("@CoHoaDon", chkCoHoaDon.Checked);
                    cmd.Parameters.AddWithValue("@DaNhanHoaDon", chkDaNhanHoaDon.Checked);
                    cmd.Parameters.AddWithValue("@TienPhuThu", Convert.ToDouble(txtPhuThu.Text.Replace(",", "").Replace(".", "")));
                    cmd.ExecuteNonQuery();

                    //thêm phiếu thu
                    cmd.CommandText = @"INSERT INTO [PhieuThu] ([SoTienThu],[SoHoaDon],[NgayHachToan],[IdCongTy],[IdNhanVien],[IdCuaHang], [IdLoaiPhieuThu])
                                        VALUES (@SoTienThu,@SoHoaDon,@NgayHachToan,@IdCongTy,@IdNhanVien,@IdCuaHang, @IdLoaiPhieuThu)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SoTienThu", Convert.ToDouble(txt_TongSoTien.Text.Replace(",", "").Replace(".", "")));
                    cmd.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                    cmd.Parameters.AddWithValue("@NgayHachToan", dateTimeInputNgayHoaDon.Value);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdNhanVien", cboNhanVienBan.SelectedValue);
                    cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                    cmd.Parameters.AddWithValue("@IdLoaiPhieuThu", 6);
                    cmd.ExecuteNonQuery();
                    #region import to caresoft
                    //danhsach.Add(new Parameter
                    //{
                    //    id = Define.SoHD,
                    //    value = txtSoHoaDon.Text,
                    // });
                    //danhsach.Add(new Parameter
                    //{
                    //    id = Define.CPThanhToan,
                    //    value = txt_TongSoTien.Text.Replace(",", "").Replace(".", "")
                    //});
                    //danhsach.Add(new Parameter
                    //{
                    //    id = Define.NgayBanXe,
                    //    value = dateTimeInputNgayHoaDon.Value.ToString("yyyy/MM/dd")
                    //}) ;
                    //DataRow[] drNhanVien = dtNhanVienBH.Select("IdNhanVien = '" + cboNhanVienBan.SelectedValue.ToString() + "'");
                    //string TenNhanVien=string.Empty;
                    //if (drNhanVien.Length > 0)
                    //{
                    //    TenNhanVien = Convert.ToString(drNhanVien[0]["TenNhanVien"]);
                    //}
                    //danhsach.Add(new Parameter
                    //{
                    //    id = Define.NhanVienBH,
                    //    value=TenNhanVien
                        
                    //});
                    //ttkh.custom_fields = danhsach;
                    //string contact = JsonConvert.SerializeObject(new{ contact = ttkh});
                    //int iddulieu = 0;
                    //using (HttpClient httpClient = new HttpClient())
                    //{
                    //    httpClient.BaseAddress = new Uri(Define.api);
                    //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer",Define.token);
                    //    HttpRequestMessage resquestMessage = new HttpRequestMessage(HttpMethod.Post, "api/v1/contacts");
                    //    resquestMessage.Content = new StringContent(contact, Encoding.UTF8, "application/json");
                    //    HttpResponseMessage responseMessage = httpClient.PostAsync("api/v1/contacts", resquestMessage.Content).Result;
                    //    if (responseMessage.IsSuccessStatusCode)
                    //    {
                    //        MessageBox.Show("Import thành công dữ liệu");
                    //    }
                    //    else
                    //    {
                    //        MessageBox.Show("Thất bại " + responseMessage.RequestMessage);
                           
                    //        if (responseMessage.ReasonPhrase.Equals("Bad Request"))
                    //        {
                    //            ErrorRequest response = JsonConvert.DeserializeObject<ErrorRequest>(responseMessage.Content.ReadAsStringAsync().Result);
                    //            iddulieu =int.Parse( response.extra_data.duplicate_id.ToString());
                    //        }
                            
                    //    }
                    //    if (iddulieu>0)
                    //    {
                    //        HttpRequestMessage resquestMessage1 = new HttpRequestMessage(HttpMethod.Put, "/api/v1/contacts/"+ iddulieu);
                    //        resquestMessage1.Content = new StringContent(contact, Encoding.UTF8, "application/json");
                    //        HttpResponseMessage responseMessage1 = httpClient.PutAsync("api/v1/contacts/"+ iddulieu, resquestMessage1.Content).Result;
                    //        if (responseMessage1.IsSuccessStatusCode)
                    //        {
                    //            MessageBox.Show("Import thành công dữ liệu");
                    //        }
                    //        else
                    //        {
                    //            MessageBox.Show("Thất bai");
                    //        }
                    //    }
                    //}
                    #endregion
                    //tran.Commit();
                    MessageBox.Show("Thêm hóa đơn bán xe thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    //tran.Rollback();
                    MessageBox.Show("Thêm hóa đơn bán xe thất bại." + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    cmd.Connection.Close();
                }
                //}));
                //threadBackgroup.Start();
            }
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
        }

        private void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            frmThemKhachHang fm = new frmThemKhachHang();
            if (fm.ShowDialog() == DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(KhachHang.idkhachhang))
                {
                    dtKhachHang = classdb.LayDanhSachKhachHang();
                    cboKhachHang.DataSource = dtKhachHang;

                    cboKhachHang.SelectedValue = KhachHang.idkhachhang;
                    DataRow[] r = dtKhachHang.Select("IdKhachHang = '" + KhachHang.idkhachhang + "'");
                    if (r.Length > 0)
                    {
                        txtDiaChi.Text = Convert.ToString(r[0]["DiaChi"]);
                    }
                }
            }
        }

        private void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty((txtTongTien.Text)) && !String.IsNullOrEmpty(txtPhuThu.Text))
            {
                if (decimal.TryParse(txtTongTien.Text, out giaban))
                {
                    txt_TongSoTien.Text =
                        (Convert.ToDecimal(txtTongTien.Text) + Convert.ToDecimal(txtPhuThu.Text)).ToString("N0");
                }
            }
        }
    }
}