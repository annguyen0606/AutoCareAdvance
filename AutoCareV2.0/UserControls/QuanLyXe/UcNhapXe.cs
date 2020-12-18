using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoCareV2._0.Class;

namespace AutoCareV2._0.UserControls
{
    public partial class UcNhapXe : UserControl
    {
        private KhDB classdb = new KhDB();

        private DocTien classDocTien = new DocTien();
        private DataTable dtLoadDetailType = new DataTable();
        private DataTable dtTenXe = new DataTable();
        private DataTable dtMauXeNull = new DataTable();
        private DataTable dtKieuXeNull = new DataTable();
        private DataTable dtNhaCungCap = new DataTable();
        private DataTable dtColorDetail = new DataTable();
        private DataTable dtPhuKien = new DataTable();
        private DataTable dtThongTinXe = new DataTable();

        private decimal tongtien = 0m;
        private DataTable dt = new DataTable();
        private decimal dongia1, vat1;
        private decimal tien = 0;
        private decimal tongthanhtien;
        private decimal tienvat;
        private decimal tienDaTra;
        private decimal tienpk = 0;
        private int SLPK = 0;
        private bool check = false;

        public UcNhapXe()
        {
            InitializeComponent();
        }

        private void LoadChiTietKieuXe()
        {
            SqlCommand cmd = new SqlCommand("select IdLoaiXe, TenLoaiXe from LoaiXe Where IdCongTy=@IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtLoadDetailType = Class.datatabase.getData(cmd);
        }

        private void LayThongTinXe()
        {
            SqlCommand cmd = new SqlCommand("select * from XeMay Where IdCongTy=@IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtThongTinXe = Class.datatabase.getData(cmd);
        }

        private void LayKieuNhapHang()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM KieuNhapXe");
            cboKieuNhap.DataSource = datatabase.getData(cmd);
            cboKieuNhap.ValueMember = "IdKieuNhapXe";
            cboKieuNhap.DisplayMember = "TenKieuNhapXe";
        }

        private void LayNhaCungCap()
        {
            SqlCommand cmd = new SqlCommand("Select * FROm NhaCungCap WHERE IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            dtNhaCungCap = datatabase.getData(cmd);
            cboNhaCungCap.DataSource = dtNhaCungCap;
            cboNhaCungCap.ValueMember = "IdNhaCungCap";
            cboNhaCungCap.DisplayMember = "TenNhaCungCap";
            cboNhaCungCap.Text = null;
        }

        private void LoadColorDetail()
        {
            SqlCommand cmd = new SqlCommand("select IdMauXe, TenMauXe from MauXeMay where IdCongTy=@IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtColorDetail = Class.datatabase.getData(cmd);
        }

        private void UcNhapXe_Load(object sender, EventArgs e)
        {
            try
            {
                LoadChiTietKieuXe();
                LayThongTinXe();

                dtTenXe = classdb.LayTenXe();
                this.TenXe.DataSource = dtTenXe;
                this.TenXe.DisplayMember = "TenXe";
                this.TenXe.ValueMember = "IdXe";

                this.TenPhuKien.DataSource = classdb.LayTenPhuKien();
                this.TenPhuKien.DisplayMember = "TenPhuKien";
                this.TenPhuKien.ValueMember = "IdPhuKien";

                LayKieuNhapHang();
                LayNhaCungCap();

                this.KhoNhap.DataSource = classdb.LoadTenKho();
                this.KhoNhap.DisplayMember = "TenKho";
                this.KhoNhap.ValueMember = "IdKho";

                this.KhoNhapPK.DataSource = classdb.LoadTenKho();
                this.KhoNhapPK.DisplayMember = "TenKho";
                this.KhoNhapPK.ValueMember = "IdKho";

                dgvxXeMua.Rows[0].Cells["DangKiem"].Value = true;
                dgvxXeMua.Rows[0].Cells["SoBaoHanh"].Value = true;
                dateTimeInputNgayHoaDon.Value = DateTime.Now;
                LoadColorDetail();
                dtMauXeNull.Columns.Add("IdMauXe");
                dtMauXeNull.Columns.Add("TenMauXe");
                //
                dtKieuXeNull.Columns.Add("IdLoaiXe");
                dtKieuXeNull.Columns.Add("TenLoaiXe");
                MauXe.DataSource = dtColorDetail;
                MauXe.ValueMember = "IdMauXe";
                MauXe.DisplayMember = "TenMauXe";
                KieuXe.DataSource = dtLoadDetailType;
                KieuXe.ValueMember = "IdLoaiXe";
                KieuXe.DisplayMember = "TenLoaiXe";
            }
            catch { }
        }

        private void cboNhaCungCap_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = dtNhaCungCap.Select("IdNhaCungCap = " + "'" + cboNhaCungCap.SelectedValue.ToString() + "'");
                txtDiaChi.Text = Convert.ToString(rows[0]["DiaChi"]);
            }
            catch { }
        }

        private void cboNhaCungCap_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow[] rows = dtNhaCungCap.Select("IdNhaCungCap = " + "'" + cboNhaCungCap.SelectedValue.ToString() + "'");
                txtDiaChi.Text = Convert.ToString(rows[0]["DiaChi"]);
            }
        }

        private void txtTienDaTra_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtTienDaTra.Text))
                {
                    tienDaTra = 0;
                }
                else
                {
                    tienDaTra = Convert.ToDecimal(txtTienDaTra.Text);
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi." + ex.Message); }

            txtTienDaTra.Text = tienDaTra.ToString("0,0");
            txtTienDaTra.SelectionStart = txtTienDaTra.Text.Length;
        }

        private void chk_TraDu_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_TraDu.Checked == true)
                txtTienDaTra.Text = txtTongTien.Text;
            else txtTienDaTra.Text = "0";
        }

        //test
        private void TextDonGia1_Changed(object sender, EventArgs e)
        {
            if (dgvxXeMua.CurrentCell.ColumnIndex == dgvxXeMua.Columns["DonGia1"].Index)
            {
                try
                {
                    if (String.IsNullOrEmpty((sender as TextBox).Text))
                    {
                        tien = 0;
                    }
                    else
                    {
                        tien = Convert.ToDecimal((sender as TextBox).Text);
                    }
                }
                catch { }
                tienvat = tien / 10;
                (sender as TextBox).Text = string.Format("{0:0,0}", tien);
                (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
                dgvxXeMua.CurrentRow.Cells["VAT"].Value = tienvat.ToString("0,0");
                tienvat = Convert.ToDecimal(dgvxXeMua.CurrentRow.Cells["VAT"].Value);
                tien = Convert.ToDecimal((sender as TextBox).Text);
                dgvxXeMua.CurrentRow.Cells["ThanhTien"].Value = (tienvat + tien).ToString("0,0");
                tongthanhtien = 0;
                for (int i = 0; i < dgvxXeMua.Rows.Count - 1; i++)
                {
                    try
                    {
                        tongthanhtien += Convert.ToDecimal(dgvxXeMua.Rows[i].Cells["ThanhTien"].Value);
                    }
                    catch { }
                }
                for (int i = 0; i < dtgrvPhuKien.Rows.Count - 1; i++)
                {
                    try
                    {
                        tongthanhtien += Convert.ToDecimal(dtgrvPhuKien.Rows[i].Cells["DonGiaPK"].Value);
                    }
                    catch { }
                }
                txtTongTien.Text = tongthanhtien.ToString("0,0");
            }
        }

        private void TxtVAT_Changed(object sender, EventArgs e)
        {
            if (dgvxXeMua.CurrentCell.ColumnIndex == dgvxXeMua.Columns["VAT"].Index)
            {
                try
                {
                    if (String.IsNullOrEmpty((sender as TextBox).Text))
                    {
                        tienvat = 0;
                    }
                    else
                    {
                        tienvat = Convert.ToDecimal((sender as TextBox).Text);
                    }
                }
                catch { }
                (sender as TextBox).Text = tienvat.ToString("0,0");
                (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
                tienvat = Convert.ToDecimal((sender as TextBox).Text);
                tien = Convert.ToDecimal(dgvxXeMua.CurrentRow.Cells["DonGia1"].Value);
                dgvxXeMua.CurrentRow.Cells["ThanhTien"].Value = (tienvat + tien).ToString("0,0");
                tongthanhtien = 0;
                for (int i = 0; i < dgvxXeMua.Rows.Count - 1; i++)
                {
                    try
                    {
                        tongthanhtien += Convert.ToDecimal(dgvxXeMua.Rows[i].Cells["ThanhTien"].Value);
                    }
                    catch { }
                }
                for (int i = 0; i < dtgrvPhuKien.Rows.Count - 1; i++)
                {
                    try
                    {
                        tongthanhtien += Convert.ToDecimal(dtgrvPhuKien.Rows[i].Cells["DonGiaPK"].Value);
                    }
                    catch { }
                }
                txtTongTien.Text = tongthanhtien.ToString("0,0");
            }
        }

        private void cboTenXe1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgvxXeMua.CurrentCell.ColumnIndex == dgvxXeMua.Columns["TenXe"].Index)
                {
                    DataGridViewComboBoxCell cbb2 = (dgvxXeMua.CurrentRow.Cells["MauXe"] as DataGridViewComboBoxCell);
                    DataGridViewComboBoxCell cbb = (dgvxXeMua.CurrentRow.Cells["KieuXe"] as DataGridViewComboBoxCell);

                    DataRow[] r = dtTenXe.Select("TenXe = '" + Convert.ToString((sender as ComboBox).Text) + "'");
                    if (r.Length > 0)
                    {
                        string idXe = Convert.ToString(r[0]["IDXe"]);
                        DataRow[] row = dtLoadDetailType.Select("IdXe = '" + idXe + "'");

                        if (row.Length > 0)
                        {
                            cbb.DataSource = row.CopyToDataTable();
                            cbb.DisplayMember = "TenLoaiXe";
                            cbb.ValueMember = "IdLoaiXe";
                        }
                        else
                        {
                            cbb.DataSource = dtKieuXeNull;
                            cbb.DisplayMember = "TenLoaiXe";
                            cbb.ValueMember = "IdLoaiXe";
                        }
                        DataRow[] row2 = dtColorDetail.Select("IdXe = '" + idXe + "'");
                        dtMauXeNull.Clear();
                        if (row2.Length > 0)
                        {
                            cbb2.DataSource = row2.CopyToDataTable();
                            cbb2.DisplayMember = "TenMauXe";
                            cbb2.ValueMember = "IdMauXe";
                        }
                        else
                        {
                            cbb2.DataSource = dtMauXeNull;
                            cbb2.DisplayMember = "TenMauXe";
                            cbb2.ValueMember = "IdMauXe";
                        }
                    }
                }
            }
            catch { }
        }

        private void cboKho_SelectIndexCommit(object sender, EventArgs e)
        {
            if (dgvxXeMua.Columns["KhoNhap"].Index == dgvxXeMua.CurrentCell.ColumnIndex)
            {
                for (int i = dgvxXeMua.CurrentRow.Index; i < dgvxXeMua.Rows.Count - 1; i++)
                {
                    dgvxXeMua.Rows[i].Cells["KhoNhap"].Value = (sender as ComboBox).SelectedValue;
                }
            }
        }

        private void dgvxXeMua_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dgvxXeMua.CurrentCell.ColumnIndex == dgvxXeMua.Columns["DonGia1"].Index)
            {
                TextBox txt = e.Control as TextBox;
                txt.Name = "DonGia01";
                txt.TextChanged += new EventHandler(TextDonGia1_Changed);
            }
            if (dgvxXeMua.CurrentCell.ColumnIndex == dgvxXeMua.Columns["VAT"].Index)
            {
                TextBox txt1 = new TextBox();
                txt1 = e.Control as TextBox;
                txt1.Name = "VAT01";
                txt1.TextChanged += new EventHandler(TxtVAT_Changed);
            }
            if (dgvxXeMua.CurrentCell.ColumnIndex == dgvxXeMua.Columns["TenXe"].Index)
            {
                ComboBox cboTenXe1 = e.Control as ComboBox;
                cboTenXe1.SelectionChangeCommitted += new EventHandler(cboTenXe1_SelectedIndexChanged);
            }
            if (dgvxXeMua.CurrentCell.ColumnIndex == dgvxXeMua.Columns["KhoNhap"].Index)
            {
                ComboBox cboKho = e.Control as ComboBox;
                cboKho.SelectionChangeCommitted += new EventHandler(cboKho_SelectIndexCommit);
            }
        }

        private void dgvxXeMua_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            dgvxXeMua.Rows[e.RowIndex].Cells["DangKiem"].Value = true;
            dgvxXeMua.Rows[e.RowIndex].Cells["SoBaoHanh"].Value = true;
        }

        private void dgvxXeMua_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvxXeMua.Columns["delete"].Index && e.RowIndex >= 0 && dgvxXeMua.Rows.Count > 1 && e.RowIndex != dgvxXeMua.Rows.Count - 1)
            {
                dgvxXeMua.Rows.RemoveAt(e.RowIndex);
                tongtien = 0;
                decimal tien3;
                decimal tien4;

                for (int k = 0; k < dgvxXeMua.Rows.Count - 1; k++)
                {
                    if (decimal.TryParse(Convert.ToString(dgvxXeMua.Rows[k].Cells["ThanhTien"].Value), out tien3))
                    {
                        tongtien += tien3;
                    }
                }
                for (int k = 0; k < dtgrvPhuKien.Rows.Count - 1; k++)
                {
                    if (decimal.TryParse(Convert.ToString(dtgrvPhuKien.Rows[k].Cells["DonGiaPK"].Value), out tien4))
                    {
                        tongtien += tien4;
                    }
                }

                txtTongTien.Text = tongtien.ToString("0,0");
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txtDiaChi.Clear();
            txtGhiChu.Clear();
            txtLoHang.Clear();
            txtSoHoaDon.Clear();
            txtTongTien.Clear();
            txtTienDaTra.Clear();
            cboNhaCungCap.Text = "";
            dgvxXeMua.Rows.Clear();
            dtgrvPhuKien.Rows.Clear();
        }
        //bugger tu day

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                txtDiaChi.Focus();

                if (cboNhaCungCap.SelectedValue==null)
                {
                    MessageBox.Show("Bạn chưa chọn nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (dateTimeInputNgayHoaDon.ValueObject == null)
                {
                    MessageBox.Show("Ngày tạo hóa đơn chưa nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (String.IsNullOrEmpty(txtSoHoaDon.Text))
                {
                    MessageBox.Show("Số hóa đơn chưa nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (String.IsNullOrEmpty(Convert.ToString(cboNhaCungCap.SelectedValue)))
                {
                    MessageBox.Show("Bạn chưa chọn nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dgvxXeMua.Rows.Count - 1 <= 0)
                {
                    MessageBox.Show("Bạn chưa nhập xe vào danh sách xe nhập!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                dt = new DataTable();
                dt.Columns.Add("MaXe");
                dt.Columns.Add("TenXe");
                dt.Columns.Add("HangSanXuat");
                dt.Columns.Add("DVT");
                DataTable dtKtSKhung = new DataTable();
                dtKtSKhung.Columns.Add("SoKhung");
                dtKtSKhung.Columns.Add("SoMay");

                DataTable dtChiTietXe = classdb.LayChiTietXe();
                try
                {
                    //Kiểm tra có nhập phụ kiện không
                    if (chkPhuKien.Checked == true)
                    {
                        //Kiểm tra dữ liệu phụ kiện
                        if (dtgrvPhuKien.Rows.Count - 1 > 0)
                        {
                            for (int i = 0; i < dtgrvPhuKien.Rows.Count - 1; i++)
                            {
                                if (String.IsNullOrEmpty(Convert.ToString(dtgrvPhuKien.Rows[i].Cells["TenPhuKien"].Value)))
                                {
                                    MessageBox.Show("Chưa chọn phụ kiện tại dòng " + (i + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                if (String.IsNullOrEmpty(Convert.ToString(dtgrvPhuKien.Rows[i].Cells["KhoNhapPK"].Value)))
                                {
                                    MessageBox.Show("Chưa chọn kho nhập phụ kiện tại dòng " + (i + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                if (String.IsNullOrEmpty(Convert.ToString(dtgrvPhuKien.Rows[i].Cells["DonGiaPK"].Value)))
                                {
                                    MessageBox.Show("Chưa nhập đơn giá phụ kiện tại dòng " + (i + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bạn chưa nhập phụ kiện vào danh sách phụ kiện!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    //Xử lý dữ liệu nhập xe
                    int j = 0, m = 0;
                    for (int i = 0; i < dgvxXeMua.Rows.Count - 1; i++)
                    {
                        if (String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[i].Cells["TenXe"].Value)) && String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[i].Cells["SoMay"].Value)) && String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[i].Cells["SoMay"].Value)))
                        {
                            j = dgvxXeMua.Rows.Count;
                        }
                        if (String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[i].Cells["TenXe"].Value)))
                        {
                            MessageBox.Show("Chưa nhập tên xe tại dòng " + (i + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[i].Cells["KieuXe"].Value)))
                        {
                            MessageBox.Show("Chưa chọn kiểu xe tại dòng " + (i + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[i].Cells["MauXe"].Value)))
                        {
                            MessageBox.Show("Chưa chọn màu xe tại dòng " + (i + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[i].Cells["SoKhung"].Value)))
                        {
                            MessageBox.Show("Chưa nhập số khung tại dòng " + (i + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[i].Cells["SoMay"].Value)))
                        {
                            MessageBox.Show("Chưa nhập số máy tại dòng " + (i + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[i].Cells["KhoNhap"].Value)))
                        {
                            MessageBox.Show("Chưa chọn kho nhập xe tại dòng " + (i + 1), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        string soKhung = Convert.ToString(dgvxXeMua.Rows[i].Cells["SoKhung"].Value).Trim();
                        DataRow[] r0 = dtChiTietXe.Select("SoKhung = '" + soKhung + "'");
                        if (r0.Length > 0)
                        {
                            MessageBox.Show("Thông tin số khung " + soKhung + " tại dòng " + (i + 1) + " đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }

                        //
                        string soMay = Convert.ToString(dgvxXeMua.Rows[i].Cells["SoMay"].Value).Trim();
                        DataRow[] r01 = dtChiTietXe.Select("SoMay = '" + soMay + "'");
                        if (r01.Length > 0)
                        {
                            MessageBox.Show("Thông tin số máy " + soMay + " tại dòng " + (i + 1) + " đã tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        if (dtKtSKhung.Rows.Count > 0)
                        {
                            DataRow[] dr1 = dtKtSKhung.Select("SoKhung = '" + soKhung + "'");
                            if (dr1.Length > 0)
                            {
                                MessageBox.Show("Danh sách nhập xe chứa nhiều hơn 1 số khung " + soKhung, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                            dr1 = dtKtSKhung.Select("SoMay = '" + soMay + "'");
                            if (dr1.Length > 0)
                            {
                                MessageBox.Show("Danh sách nhập xe chứa nhiều hơn 1 số máy " + soMay, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        DataRow dr = dtKtSKhung.NewRow();
                        dr["SoKhung"] = soKhung;
                        dr["SoMay"] = soMay;
                        dtKtSKhung.Rows.Add(dr);

                        string tenXe = Convert.ToString(dgvxXeMua.Rows[i].Cells["TenXe"].Value).Trim();
                        DataRow[] r = dtTenXe.Select("IdXe = '" + tenXe + "'");
                        if (r.Length <= 0)
                        {
                            if (dt.Rows.Count > 0)
                            {
                                DataRow[] row1 = dt.Select("TenXe = '" + Convert.ToString(dgvxXeMua.Rows[i].Cells["TenXe"].Value).Trim() + "'");
                                if (row1.Length <= 0)
                                {
                                    DataRow r1 = dt.NewRow();
                                    r1["MaXe"] = null;
                                    r1["TenXe"] = Convert.ToString(dgvxXeMua.Rows[i].Cells["TenXe"].Value).Trim();
                                    r1["DVT"] = "Chiếc";
                                    r1["HangSanXuat"] = null;
                                    dt.Rows.Add(r1);
                                }
                            }
                            else
                            {
                                DataRow r1 = dt.NewRow();
                                r1["MaXe"] = null;
                                r1["TenXe"] = Convert.ToString(dgvxXeMua.Rows[i].Cells["TenXe"].Value).Trim();
                                r1["DVT"] = "Chiếc";
                                r1["HangSanXuat"] = null;
                                dt.Rows.Add(r1);
                            }
                        }
                        m = i;
                        if (j > 0)
                        {
                            i = j;
                        }
                    }
                    if (dt.Rows.Count > 0)
                    {
                        DialogResult chon = MessageBox.Show("Tìm thấy " + dt.Rows.Count + " loại xe mới. Cần thêm thông tin xe trước khi nhập xe?", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        foreach (DataRow r in dt.Rows)
                        {
                            string tenxemoi = "Danh sách xe mới: " + Environment.NewLine;
                            tenxemoi += Convert.ToString(r["TenXe"]) + Environment.NewLine;
                            MessageBox.Show(tenxemoi);
                        }
                        return;
                    }
                    else
                    {
                        try
                        {
                            string idNhaCungCap = "", idKieuNhap = "";

                            idNhaCungCap = cboNhaCungCap.SelectedValue.ToString();
                            idKieuNhap = cboKieuNhap.SelectedValue.ToString();

                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = Class.datatabase.getConnection();
                            cmd.Connection.Open();
                            SqlTransaction tran = cmd.Connection.BeginTransaction();
                            cmd.Transaction = tran;
                            try
                            {
                                int k = 0;
                                string idKey = string.Empty;
                                SqlCommand cmHDN = new SqlCommand();
                                cmHDN.Connection = Class.datatabase.getConnection();
                                cmHDN.Connection.Open();
                                SqlTransaction tranHoaDonNhapXe = cmHDN.Connection.BeginTransaction();
                                cmHDN.Transaction = tranHoaDonNhapXe;
                                try
                                {
                                    if (txtSoHoaDon.Text == "0")
                                    {
                                        cmHDN.CommandText = "Insert into HoaDonNhapXe(SoHoaDonNhap,IdNhaCungCap,IdNhanVienTaoPhieu,NgayHoaDon,DaNhanHoaDon,IDCongTy,IdKieuNhapXe,GhiChu,LoHang,IDCuaHang,TienDaTra) Values (@SoHoaDonNhap,@IdNhaCungCap,@IdNhanVienTaoPhieu,@NgayHoaDon,@DaNhanHoaDon,@IDCongTy,@IdKieuNhapXe,@GhiChu,@LoHang,@IdCuaHang,@TienDaTra) select @@identity";
                                    }
                                    else
                                    {
                                        cmHDN.CommandText = "if not exists(Select top 1 * From HoaDonNhapXe Where SoHoaDonNhap = @SoHoaDonNhap and IdCongTy = @IdCongTy)"
                                                            + " Insert into HoaDonNhapXe(SoHoaDonNhap,IdNhaCungCap,IdNhanVienTaoPhieu,NgayHoaDon,DaNhanHoaDon,IDCongTy,IdKieuNhapXe,GhiChu,LoHang,IDCuaHang,TienDaTra)"
                                                            + " Values(@SoHoaDonNhap,@IdNhaCungCap,@IdNhanVienTaoPhieu,@NgayHoaDon,@DaNhanHoaDon,@IDCongTy,@IdKieuNhapXe,@GhiChu,@LoHang,@IdCuaHang,@TienDaTra) "
                                                            + " select @@identity";
                                    }

                                    cmHDN.Parameters.AddWithValue("@SoHoaDonNhap", txtSoHoaDon.Text);
                                    cmHDN.Parameters.AddWithValue("@IDNhaCungCap", idNhaCungCap);
                                    cmHDN.Parameters.AddWithValue("@IDNhanVienTaoPhieu", Class.EmployeeInfo.idnhanvien);
                                    cmHDN.Parameters.AddWithValue("@NgayHoaDon", dateTimeInputNgayHoaDon.Value);
                                    cmHDN.Parameters.AddWithValue("@DaNhanHoaDon", chkDaNhanHoaDon.Checked);
                                    cmHDN.Parameters.AddWithValue("@IDCongTy", Class.CompanyInfo.idcongty);
                                    cmHDN.Parameters.AddWithValue("@IdKieuNhapXe", idKieuNhap);
                                    cmHDN.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
                                    cmHDN.Parameters.AddWithValue("@LoHang", txtLoHang.Text);
                                    cmHDN.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                                    cmHDN.Parameters.AddWithValue("@TienDaTra", tienDaTra);
                                    idKey = cmHDN.ExecuteScalar().ToString();
                                    tranHoaDonNhapXe.Commit();
                                    cmHDN.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi"+ex.Message);
                                    tranHoaDonNhapXe.Rollback();
                                    cmHDN.Connection.Close();
                                    tran.Rollback();
                                    cmd.Connection.Close();
                                    return;
                                }

                                if (String.IsNullOrEmpty(idKey))
                                {
                                    MessageBox.Show("Số hóa đơn đã tồn tại. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    tran.Rollback();
                                    cmd.Connection.Close();
                                    return;
                                }
                                for (int i = 0; i < dgvxXeMua.Rows.Count - 1; i++)
                                {
                                    SqlCommand cmChiTietNhapXe = new SqlCommand();
                                    cmChiTietNhapXe.Connection = Class.datatabase.getConnection();
                                    cmChiTietNhapXe.Connection.Open();
                                    SqlTransaction tranChiTietNhapXe = cmChiTietNhapXe.Connection.BeginTransaction();
                                    cmChiTietNhapXe.Transaction = tranChiTietNhapXe;
                                    string tenXe = Convert.ToString(dgvxXeMua.Rows[i].Cells["TenXe"].Value);
                                    DataRow[] r3 = dtTenXe.Select("IdXe = '" + tenXe + "'");
                                    cmChiTietNhapXe.CommandText = "Insert into ChiTietNhapXe (SoHoaDonNhap,IdKey,IdXe,SoKhung,SoMay,GiaNhap,VAT,GiaCoVAT,IdMauXe,IdCongTy,SoChungTu,DangKiem,SoBaoHanh,IdLoaiXe) values (@SoHoaDonNhap,@IdKey,@IdXe,@SoKhung,@SoMay,@GiaNhap,@VAT,@GiaCoVAT,@IdMauXe,@IdCongTy,@SoChungTu,@DangKiem,@SoBaoHanh,@IdLoaiXe)";
                                    cmChiTietNhapXe.Parameters.Clear();
                                    cmChiTietNhapXe.Parameters.AddWithValue("@SoHoaDonNhap", txtSoHoaDon.Text);
                                    cmChiTietNhapXe.Parameters.AddWithValue("@IdKey", idKey);
                                    cmChiTietNhapXe.Parameters.AddWithValue("@IdXe", Convert.ToString(r3[0]["IdXe"]));
                                    cmChiTietNhapXe.Parameters.AddWithValue("@SoKhung", Convert.ToString(dgvxXeMua.Rows[i].Cells["SoKhung"].Value));
                                    cmChiTietNhapXe.Parameters.AddWithValue("@SoMay", Convert.ToString(dgvxXeMua.Rows[i].Cells["SoMay"].Value));
                                    dongia1 = 0; vat1 = 0;
                                    if (!String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[i].Cells["DonGia1"].Value)))
                                    {
                                        dongia1 = Convert.ToDecimal(Convert.ToString(dgvxXeMua.Rows[i].Cells["DonGia1"].Value));
                                    }
                                    if (!String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[i].Cells["VAT"].Value)))
                                    {
                                        vat1 = Convert.ToDecimal(Convert.ToString(dgvxXeMua.Rows[i].Cells["VAT"].Value));
                                    }
                                    try
                                    {
                                        cmChiTietNhapXe.Parameters.AddWithValue("@GiaNhap", dongia1);
                                        cmChiTietNhapXe.Parameters.AddWithValue("@VAT", vat1);
                                        cmChiTietNhapXe.Parameters.AddWithValue("@GiaCoVAT", dongia1 + vat1);
                                        cmChiTietNhapXe.Parameters.AddWithValue("@IdMauXe", Convert.ToString(dgvxXeMua.Rows[i].Cells["MauXe"].Value));
                                        cmChiTietNhapXe.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                        cmChiTietNhapXe.Parameters.AddWithValue("@SoChungTu", SqlString.Null);
                                        cmChiTietNhapXe.Parameters.AddWithValue("@DangKiem", Convert.ToBoolean(dgvxXeMua.Rows[i].Cells["DangKiem"].Value));
                                        cmChiTietNhapXe.Parameters.AddWithValue("@SoBaoHanh", Convert.ToBoolean(dgvxXeMua.Rows[i].Cells["SoBaoHanh"].Value));
                                        cmChiTietNhapXe.Parameters.AddWithValue("@IdLoaiXe", Convert.ToString(dgvxXeMua.Rows[i].Cells["KieuXe"].Value));
                                        cmChiTietNhapXe.ExecuteNonQuery();
                                        tranChiTietNhapXe.Commit();
                                        cmChiTietNhapXe.Connection.Close();

                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Lỗi "+ex.Message);
                                        tranChiTietNhapXe.Rollback();
                                        cmChiTietNhapXe.Connection.Close();
                                        tran.Rollback();
                                        cmd.Connection.Close();
                                        return;
                                    }
                                    SqlCommand cmChiTietXe = new SqlCommand();
                                    cmChiTietXe.Connection = Class.datatabase.getConnection();
                                    cmChiTietXe.Connection.Open();
                                    SqlTransaction tranChiTietXe = cmChiTietXe.Connection.BeginTransaction();
                                    cmChiTietXe.Transaction = tranChiTietXe;
                                    try
                                    {
                                        //@IdXe,@IdMauXe,@SoKhung,@SoMay,@IdNhaCungCap,@IdKho,@DonGia,@IdCongTy,@IdLoaiXe,@DangKiem,@SoBaoHanh
                                        cmChiTietXe.CommandText = "Insert Into ChiTietXe (IdKey,IdMauXe,SoKhung,SoMay,IdNhaCungCap,IdKho,DonGia,IdCongTy,IdLoaiXe,DangKiem,SoBaoHanh) Values(@IdXe,@IdMauXe,@SoKhung,@SoMay,@IdNhaCungCap,@IdKho,@DonGia,@IdCongTy,@IdLoaiXe,@DangKiem,@SoBaoHanh)";
                                        cmChiTietXe.Parameters.AddWithValue("@IdXe", Convert.ToString(r3[0]["IdXe"]));
                                        cmChiTietXe.Parameters.AddWithValue("@IdMauXe", Convert.ToString(dgvxXeMua.Rows[i].Cells["MauXe"].Value));
                                        cmChiTietXe.Parameters.AddWithValue("@SoKhung", Convert.ToString(dgvxXeMua.Rows[i].Cells["SoKhung"].Value));
                                        cmChiTietXe.Parameters.AddWithValue("@SoMay", Convert.ToString(dgvxXeMua.Rows[i].Cells["SoMay"].Value));

                                        cmChiTietXe.Parameters.AddWithValue("@IdNhaCungCap", idNhaCungCap);
                                        cmChiTietXe.Parameters.AddWithValue("@IdKho", Convert.ToString(dgvxXeMua.Rows[i].Cells["KhoNhap"].Value));
                                        cmChiTietXe.Parameters.AddWithValue("@DonGia", dongia1);
                                        cmChiTietXe.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                        cmChiTietXe.Parameters.AddWithValue("@IdLoaiXe", Convert.ToString(dgvxXeMua.Rows[i].Cells["KieuXe"].Value));
                                        cmChiTietXe.Parameters.AddWithValue("@DangKiem", Convert.ToBoolean(dgvxXeMua.Rows[i].Cells["DangKiem"].Value));
                                        cmChiTietXe.Parameters.AddWithValue("@SoBaoHanh", Convert.ToBoolean(dgvxXeMua.Rows[i].Cells["SoBaoHanh"].Value));
                                        cmChiTietXe.ExecuteNonQuery();
                                        tranChiTietXe.Commit();
                                        cmChiTietXe.Connection.Close();
                                        k++;
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Lỗi " + ex.Message);
                                        tranChiTietXe.Rollback();
                                        cmChiTietXe.Connection.Close();
                                        tran.Rollback();
                                        cmd.Connection.Close();
                                        return;
                                    }
                                }

                                //Insert phụ kiện
                                if (chkPhuKien.Checked == true)
                                {
                                    for (int i = 0; i < dtgrvPhuKien.Rows.Count - 1; i++)
                                    {
                                        SqlCommand cmChiTietNhapPhuKien = new SqlCommand();
                                        cmChiTietNhapPhuKien.Connection = Class.datatabase.getConnection();
                                        cmChiTietNhapPhuKien.Connection.Open();
                                        SqlTransaction tranChiTietNhapPhuKien = cmChiTietNhapPhuKien.Connection.BeginTransaction();
                                        cmChiTietNhapPhuKien.Transaction = tranChiTietNhapPhuKien;
                                        try
                                        {
                                            cmChiTietNhapPhuKien.CommandText = "INSERT INTO ChiTietNhapPhuKien(SoHoaDonNhap, IdPhuKien, ThanhTien, IdCongTy, Idkey) VALUES(@SoHoaDonNhap, @IdPhuKien, @ThanhTien, @IdCongTy, @IdKey)";
                                            cmChiTietNhapPhuKien.Parameters.Clear();
                                            cmChiTietNhapPhuKien.Parameters.AddWithValue("@SoHoaDonNhap", txtSoHoaDon.Text);
                                            cmChiTietNhapPhuKien.Parameters.AddWithValue("@IdPhuKien", Convert.ToInt32(dtgrvPhuKien.Rows[i].Cells["TenPhuKien"].Value));
                                            cmChiTietNhapPhuKien.Parameters.AddWithValue("@ThanhTien", Convert.ToDecimal(dtgrvPhuKien.Rows[i].Cells["DonGiaPK"].Value));
                                            cmChiTietNhapPhuKien.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                            cmChiTietNhapPhuKien.Parameters.AddWithValue("@Idkey", idKey);
                                            cmChiTietNhapPhuKien.ExecuteNonQuery();
                                            tranChiTietNhapPhuKien.Commit();
                                            cmChiTietNhapPhuKien.Connection.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Lỗi " + ex.Message);
                                            tranChiTietNhapPhuKien.Rollback();
                                            cmChiTietNhapPhuKien.Connection.Close();
                                            tran.Rollback();
                                            cmd.Connection.Close();
                                            return;
                                        }

                                        //check = LayDataPhuKien(Convert.ToInt32(dtgrvPhuKien.Rows[i].Cells["TenPhuKien"].Value), Convert.ToInt32(dtgrvPhuKien.Rows[i].Cells["KhoNhapPK"].Value));

                                        SqlDataReader reader;
                                        check = false;
                                        SqlCommand cmChiTietPhuKien = new SqlCommand();
                                        cmChiTietPhuKien.Connection = Class.datatabase.getConnection();
                                        cmChiTietPhuKien.Connection.Open();
                                        SqlTransaction tranChiTietPhuKien = cmChiTietPhuKien.Connection.BeginTransaction();
                                        cmChiTietPhuKien.Transaction = tranChiTietPhuKien;
                                        try
                                        {
                                            cmChiTietPhuKien.CommandText = "select SoLuong from ChiTietPhuKien where IdPhuKien='" + Convert.ToInt32(dtgrvPhuKien.Rows[i].Cells["TenPhuKien"].Value) + "' and Idkho='" + Convert.ToInt32(dtgrvPhuKien.Rows[i].Cells["KhoNhapPK"].Value) + "' and IdCongty=" + Class.CompanyInfo.idcongty;
                                            reader = cmChiTietPhuKien.ExecuteReader();
                                           
                                            if (reader != null)
                                            {
                                                while (reader.Read())
                                                {
                                                    SLPK = 0;
                                                    check = true;
                                                    SLPK = Convert.ToInt32(reader["SoLuong"].ToString());
                                                }
                                            }
                                            tranChiTietPhuKien.Commit();
                                            reader.Close();
                                            cmChiTietPhuKien.Connection.Close();
                                        }
                                        catch(Exception ex)
                                        {
                                            MessageBox.Show("Lỗi " + ex.Message);
                                            tranChiTietPhuKien.Rollback();
                                            cmChiTietPhuKien.Connection.Close();
                                            tran.Rollback();
                                            cmd.Connection.Close();
                                            return;

                                        }

                                        if (check == false)
                                        {
                                            SqlCommand cmISChiTietPhuKien = new SqlCommand();
                                            cmISChiTietPhuKien.Connection = Class.datatabase.getConnection();
                                            cmISChiTietPhuKien.Connection.Open();
                                            SqlTransaction tranISChiTietPhuKien = cmISChiTietPhuKien.Connection.BeginTransaction();
                                            cmISChiTietPhuKien.Transaction = tranISChiTietPhuKien;
                                            try
                                            {
                                                cmISChiTietPhuKien.CommandText = "INSERT INTO ChiTietPhuKien(IdPhuKien, DonGia, SoLuong, IdKho, IdCongTy) VALUES(@IdPhuKien, @DonGia, @SoLuong, @IdKho, @IdCongTy)";
                                                cmISChiTietPhuKien.Parameters.Clear();
                                                cmISChiTietPhuKien.Parameters.AddWithValue("@IdPhuKien", Convert.ToInt32(dtgrvPhuKien.Rows[i].Cells["TenPhuKien"].Value));
                                                cmISChiTietPhuKien.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(dtgrvPhuKien.Rows[i].Cells["DonGiaPK"].Value));
                                                cmISChiTietPhuKien.Parameters.AddWithValue("@SoLuong", 1);
                                                cmISChiTietPhuKien.Parameters.AddWithValue("@IdKho", Convert.ToInt32(dtgrvPhuKien.Rows[i].Cells["KhoNhapPK"].Value));
                                                cmISChiTietPhuKien.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                                cmISChiTietPhuKien.ExecuteNonQuery();
                                                tranISChiTietPhuKien.Commit();
                                                cmISChiTietPhuKien.Connection.Close();
                                            }
                                            catch(Exception ex)
                                            {
                                                MessageBox.Show("Lỗi " + ex.Message);
                                                tranISChiTietPhuKien.Rollback();
                                                cmISChiTietPhuKien.Connection.Close();
                                                tran.Rollback();
                                                cmd.Connection.Close();
                                                return;
                                            }
                                        }
                                        else
                                        {
                                            SqlCommand cmISChiTietPhuKien = new SqlCommand();
                                            cmISChiTietPhuKien.Connection = Class.datatabase.getConnection();
                                            cmISChiTietPhuKien.Connection.Open();
                                            SqlTransaction tranISChiTietPhuKien = cmISChiTietPhuKien.Connection.BeginTransaction();
                                            cmISChiTietPhuKien.Transaction = tranISChiTietPhuKien;
                                            try
                                            {
                                                cmISChiTietPhuKien.CommandText = "UPDATE ChiTietPhuKien SET DonGia=@DonGia, SoLuong=@SoLuong WHERE IdPhuKien=@IdPhuKien AND IdKho=@IdKho AND IdCongTy=@IdCongTy";
                                                cmISChiTietPhuKien.Parameters.Clear();
                                                cmISChiTietPhuKien.Parameters.AddWithValue("@IdPhuKien", Convert.ToInt32(dtgrvPhuKien.Rows[i].Cells["TenPhuKien"].Value));
                                                cmISChiTietPhuKien.Parameters.AddWithValue("@DonGia", Convert.ToDecimal(dtgrvPhuKien.Rows[i].Cells["DonGiaPK"].Value));
                                                cmISChiTietPhuKien.Parameters.AddWithValue("@SoLuong", SLPK + 1);
                                                cmISChiTietPhuKien.Parameters.AddWithValue("@IdKho", Convert.ToInt32(dtgrvPhuKien.Rows[i].Cells["KhoNhapPK"].Value));
                                                cmISChiTietPhuKien.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                                cmISChiTietPhuKien.ExecuteNonQuery();
                                                tranISChiTietPhuKien.Commit();
                                                cmISChiTietPhuKien.Connection.Close();
                                            }
                                            catch (Exception ex)
                                            {
                                                MessageBox.Show("Lỗi " + ex.Message);
                                                tranISChiTietPhuKien.Rollback();
                                                cmISChiTietPhuKien.Connection.Close();
                                                tran.Rollback();
                                                cmd.Connection.Close();
                                                return;
                                            }
                                        }
                                    }
                                }

                                //Tạo Phiếu Chi Nhập Xe
                                if (decimal.TryParse(txtTienDaTra.Text, out tienDaTra))
                                {
                                    if (tienDaTra != 0)
                                    {
                                        SqlCommand cmPhieuChi = new SqlCommand();
                                        cmPhieuChi.Connection = Class.datatabase.getConnection();
                                        cmPhieuChi.Connection.Open();
                                        SqlTransaction trancmPhieuChi = cmPhieuChi.Connection.BeginTransaction();
                                        cmPhieuChi.Transaction = trancmPhieuChi;
                                        try
                                        {
                                            cmPhieuChi.CommandText = "Insert InTo PhieuChi(IdLoaiPhieuChi,SoTienChi,NgayHachToan,IdCongTy,NguoiNhan,IdNhaCungCap,IdNhanVien,NoiDung,IdCuaHang,SoHoaDon)Values(@IdLoaiPhieuChi,@SoTienChi,@NgayHachToan,@IDCongTy,@NguoiNhan,@IdNhaCungCap,@IdNhanVien,@NoiDung,@IdCuaHang,@SoHoaDon)";
                                            cmPhieuChi.Parameters.Clear();
                                            cmPhieuChi.Parameters.AddWithValue("@IdLoaiPhieuChi", 1);//Chi nhập xe
                                            cmPhieuChi.Parameters.AddWithValue("@SoTienChi", tienDaTra);
                                            cmPhieuChi.Parameters.AddWithValue("@NgayHachToan", dateTimeInputNgayHoaDon.Value);
                                            cmPhieuChi.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                            cmPhieuChi.Parameters.AddWithValue("@NguoiNhan", cboNhaCungCap.Text);
                                            cmPhieuChi.Parameters.AddWithValue("@IdNhaCungCap", cboNhaCungCap.SelectedValue);
                                            cmPhieuChi.Parameters.AddWithValue("@IdNhanVien", EmployeeInfo.idnhanvien);
                                            cmPhieuChi.Parameters.AddWithValue("@NoiDung", txtGhiChu.Text);
                                            cmPhieuChi.Parameters.AddWithValue("@IdCuaHang", EmployeeInfo.IdCuaHang);
                                            cmPhieuChi.Parameters.AddWithValue("@SoHoaDon", txtSoHoaDon.Text);
                                            cmPhieuChi.ExecuteNonQuery();
                                            trancmPhieuChi.Commit();
                                        }
                                        catch(Exception ex)
                                        {
                                            MessageBox.Show("Lỗi " + ex.Message);
                                            trancmPhieuChi.Rollback();
                                            cmPhieuChi.Connection.Close();
                                            tran.Rollback();
                                            cmd.Connection.Close();
                                            return;
                                        }
                                    }
                                }
                                tran.Commit();
                                MessageBox.Show("Thêm thông tin hóa đơn nhập xe thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Reset();
                            }
                            catch (Exception ex)
                            {
                                tran.Rollback();

                                MessageBox.Show("Thêm thông tin hóa đơn nhập xe thất bại: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            finally { cmd.Connection.Close(); }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Lỗi : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi : " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            try
            {
                ExcelNhapXe fr = new ExcelNhapXe();
                if (fr.ShowDialog() == DialogResult.OK)
                {
                    decimal tongtien2 = 0, tien2;
                    dgvxXeMua.DataSource = fr.dt;
                    for (int index = dgvxXeMua.Rows.Count - 2; index >= 0; index--)
                    {
                        if (!String.IsNullOrEmpty(Convert.ToString(dgvxXeMua.Rows[index].Cells["TenXe"].Value)))
                        {
                            if (decimal.TryParse(Convert.ToString(dgvxXeMua.Rows[index].Cells["DonGia1"].Value), out tien2))
                            {
                                tongtien2 += tien2;
                            }
                        }
                        else
                        {
                            dgvxXeMua.Rows.RemoveAt(index);
                        }
                    }
                    txtTongTien.Text = String.Format("{0:0,0}", tongtien2);
                }
            }
            catch (Exception ex) { }
        }

        private void btnThemNhaCungCap_Click(object sender, EventArgs e)
        {
            frmNhaCungCap frm = new frmNhaCungCap();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (!String.IsNullOrEmpty(Class.KhachHang.idnhacungcap))
                {
                    LayNhaCungCap();
                    cboNhaCungCap.SelectedValue = Class.KhachHang.idnhacungcap;
                    DataRow[] rows = dtNhaCungCap.Select("IdNhaCungCap = " + "'" + Class.KhachHang.idnhacungcap + "'");
                    txtDiaChi.Text = Convert.ToString(rows[0]["DiaChi"]);
                }
            }
        }

        private void cboTenPK_SelectIndexCommit(object sender, EventArgs e)
        {
            if (dtgrvPhuKien.Columns["TenPhuKien"].Index == dtgrvPhuKien.CurrentCell.ColumnIndex)
            {
                for (int i = dtgrvPhuKien.CurrentRow.Index; i < dtgrvPhuKien.Rows.Count - 1; i++)
                {
                    dtgrvPhuKien.Rows[i].Cells["TenPhuKien"].Value = (sender as ComboBox).SelectedValue;
                }
            }
        }

        private void cboKhoPK_SelectIndexCommit(object sender, EventArgs e)
        {
            if (dtgrvPhuKien.Columns["KhoNhapPK"].Index == dtgrvPhuKien.CurrentCell.ColumnIndex)
            {
                for (int i = dtgrvPhuKien.CurrentRow.Index; i < dtgrvPhuKien.Rows.Count - 1; i++)
                {
                    dtgrvPhuKien.Rows[i].Cells["KhoNhapPK"].Value = (sender as ComboBox).SelectedValue;
                }
            }
        }

        private void DonGiaPK_Changed(object sender, EventArgs e)
        {
            if (dtgrvPhuKien.CurrentCell.ColumnIndex == dtgrvPhuKien.Columns["DonGiaPK"].Index)
            {
                try
                {
                    if (String.IsNullOrEmpty((sender as TextBox).Text))
                    {
                        tienpk = 0;
                    }
                    else
                    {
                        tienpk = Convert.ToDecimal((sender as TextBox).Text);
                    }
                }
                catch { }
                (sender as TextBox).Text = tienpk.ToString("0,0");
                (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
                tienpk = Convert.ToDecimal((sender as TextBox).Text);
                dtgrvPhuKien.CurrentRow.Cells["DonGiaPK"].Value = (tienpk + 0).ToString("0,0");
                tongthanhtien = 0;
                for (int i = 0; i < dtgrvPhuKien.Rows.Count; i++)
                {
                    try
                    {
                        tongthanhtien += Convert.ToDecimal(dtgrvPhuKien.Rows[i].Cells["DonGiaPK"].Value);
                    }
                    catch { }
                }
                for (int i = 0; i < dgvxXeMua.Rows.Count - 1; i++)
                {
                    try
                    {
                        tongthanhtien += Convert.ToDecimal(dgvxXeMua.Rows[i].Cells["ThanhTien"].Value);
                    }
                    catch { }
                }
                txtTongTien.Text = tongthanhtien.ToString("0,0");
            }
        }

        private void chkPhuKien_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPhuKien.Checked == true)
            {
                exPhuKien.Visible = true;
            }
            else
            {
                exPhuKien.Visible = false;
            }
        }

        private void dtgrvPhuKien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dtgrvPhuKien.Columns["deletePK"].Index && e.RowIndex >= 0 && dtgrvPhuKien.Rows.Count > 1 && e.RowIndex != dtgrvPhuKien.Rows.Count - 1)
            {
                dtgrvPhuKien.Rows.RemoveAt(e.RowIndex);
                tongtien = 0;
                decimal tien3;
                decimal tien4;

                for (int k = 0; k < dgvxXeMua.Rows.Count - 1; k++)
                {
                    if (decimal.TryParse(Convert.ToString(dgvxXeMua.Rows[k].Cells["ThanhTien"].Value), out tien3))
                    {
                        tongtien += tien3;
                    }
                }
                for (int k = 0; k < dtgrvPhuKien.Rows.Count - 1; k++)
                {
                    if (decimal.TryParse(Convert.ToString(dtgrvPhuKien.Rows[k].Cells["DonGiaPK"].Value), out tien4))
                    {
                        tongtien += tien4;
                    }
                }
                txtTongTien.Text = tongtien.ToString("0,0");
            }
        }

        private void dtgrvPhuKien_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (dtgrvPhuKien.CurrentCell.ColumnIndex == dtgrvPhuKien.Columns["DonGiaPK"].Index)
            {
                TextBox txtpk = e.Control as TextBox;
                txtpk.Name = "DonGiaPK01";
                txtpk.TextChanged += new EventHandler(DonGiaPK_Changed);
            }
            if (dtgrvPhuKien.CurrentCell.ColumnIndex == dtgrvPhuKien.Columns["TenPhuKien"].Index)
            {
                ComboBox cboTenPK = e.Control as ComboBox;
                cboTenPK.SelectionChangeCommitted += new EventHandler(cboTenPK_SelectIndexCommit);
            }
            if (dtgrvPhuKien.CurrentCell.ColumnIndex == dtgrvPhuKien.Columns["KhoNhapPK"].Index)
            {
                ComboBox cboKhoPK = e.Control as ComboBox;
                cboKhoPK.SelectionChangeCommitted += new EventHandler(cboKhoPK_SelectIndexCommit);
            }
        }

        /// <summary>
        /// Kiểm tra phụ kiện trong kho
        /// </summary>
        /// <param name="IdPK">Mã phụ kiện</param>
        /// <returns>True: nếu có trong kho; False: nếu không có trong kho.</returns>
        private bool LayDataPhuKien(int IdPK, int Idkho)
        {
            bool kq = false;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;
            SLPK = 0;

            cmd.CommandText = "select SoLuong from ChiTietPhuKien where IdPhuKien='" + IdPK + "' and Idkho='" + Idkho + "' and IdCongty=" + Class.CompanyInfo.idcongty;
            reader = cmd.ExecuteReader();

            if (reader != null)
            {
                while (reader.Read())
                {
                    kq = true;
                    SLPK = Convert.ToInt32(reader["SoLuong"].ToString());
                }
            }

            return kq;
        }

        private void dgvxXeMua_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex > 0) // check
                {
                    if (e.ColumnIndex == dgvxXeMua.Columns["TenXe"].Index)
                    {
                        string chuoi = dgvxXeMua.Rows[e.RowIndex].Cells["TenXe"].Value.ToString();
                        DataRow[] rows = dtThongTinXe.Select("IdXe = '" + chuoi + "'");
                        if (rows.Length > 0)
                        {
                            //dgvxXeMua.Rows[e.RowIndex].Cells["SoMay"].Value = rows[0]["SoMay"].ToString();
                            //dgvXeBan.Rows[e.RowIndex].Cells["TenXe1"].Value = rows[0]["IdXe"].ToString();
                            dgvxXeMua.Rows[e.RowIndex].Cells["DonGia1"].Value = rows[0]["DonGia"].ToString();
                            //dgvXeBan.Rows[e.RowIndex].Cells["Kho"].Value = rows[0]["IdKho"].ToString();
                        }
                        else
                        {
                            dgvxXeMua.Rows.RemoveAt(e.RowIndex);
                        }
                    }

                    //Lấy thông tin đơn giá + tính toán
                    if (e.ColumnIndex == dgvxXeMua.Columns["DonGia1"].Index)
                    {
                        try
                        {
                            tien = Convert.ToDecimal(dgvxXeMua.Rows[e.RowIndex].Cells["DonGia1"].Value.ToString());
                        }
                        catch { }

                        tienvat = tien / 10;
                        //(sender as TextBox).Text = string.Format("{0:0,0}", tien);
                        //(sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
                        dgvxXeMua.CurrentRow.Cells["VAT"].Value = tienvat.ToString("0,0");
                        tienvat = Convert.ToDecimal(dgvxXeMua.CurrentRow.Cells["VAT"].Value);
                        //tien = Convert.ToDecimal((sender as TextBox).Text);
                        dgvxXeMua.CurrentRow.Cells["ThanhTien"].Value = (tienvat + tien).ToString("0,0");
                        tongthanhtien = 0;
                        for (int i = 0; i < dgvxXeMua.Rows.Count - 1; i++)
                        {
                            try
                            {
                                tongthanhtien += Convert.ToDecimal(dgvxXeMua.Rows[i].Cells["ThanhTien"].Value);
                            }
                            catch { }
                        }
                        for (int i = 0; i < dtgrvPhuKien.Rows.Count - 1; i++)
                        {
                            try
                            {
                                tongthanhtien += Convert.ToDecimal(dtgrvPhuKien.Rows[i].Cells["DonGiaPK"].Value);
                            }
                            catch { }
                        }
                    }
                    txtTongTien.Text = tongthanhtien.ToString("0,0");
                }
            }
            catch { }
        }
    }
}