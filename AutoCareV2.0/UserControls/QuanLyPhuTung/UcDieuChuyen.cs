using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls.QuanLyPhuTung
{
    public partial class UcDieuChuyen : UserControl
    {
        public UcDieuChuyen()
        {
            InitializeComponent();
        }

        private Class.KhDB khodb = new Class.KhDB();
        private DataTable dtkho = new DataTable();
        private DataTable dtPhuTung = new DataTable();

        private int soluong = 0, soluongdanhap = 0;
        private decimal gia;
        
        private string idKhoNhan = "";
        private string idKhoXuat = "";

        private void layPhuTung()
        {
            if (idKhoXuat == "")
            {
                string sql = "select MaPT + ' - ' + TenPT AS TenPhuTung, MaPT, SoLuong,DonGia from PhuTung where IdCongTy = @IdCongTy and IdKho = @IdKho";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IdKho", cboKhoXuat.SelectedValue);
                dtPhuTung = Class.datatabase.getData(cmd);
            }
            else
            {
                string sql = "select MaPT + ' - ' + TenPT AS TenPhuTung, MaPT, SoLuong,DonGia from PhuTung where IdCongTy = @IdCongTy and IdKho = @IdKho";
                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@IdCongTy", idKhoXuat);
                cmd.Parameters.AddWithValue("@IdKho", cboKhoXuat.SelectedValue);
                dtPhuTung = Class.datatabase.getData(cmd);
            }
        }

        public DataTable LayTenKho(string idCongty)
        {
            string sql = "SELECT IdKho, TenKho FROM KhoHang WHERE IdCongty=" + idCongty;
            SqlCommand cmd = new SqlCommand(sql);
            return Class.datatabase.getData(cmd);
        }

        private void UcDieuChuyen_Load(object sender, EventArgs e)
        {
            #region "Chỉ dùng cho Đức Trí"

            if (Class.CompanyInfo.idcongty == "1" || Class.CompanyInfo.idcongty == "30")
            {
                cbbChoCoSoXuat.Enabled = true;
                cbbChonCoSoNhan.Enabled = true;
            }
            else
            {
                cbbChoCoSoXuat.Enabled = false;
                cbbChonCoSoNhan.Enabled = false;
            }

            #endregion "Chỉ dùng cho Đức Trí"

            dtkho = khodb.LoadTenKho();
            //
            cboKhoNhan.DataSource = khodb.LoadTenKho();
            cboKhoNhan.DisplayMember = "TenKho";
            cboKhoNhan.ValueMember = "IdKho";
            cboKhoNhan.Text = null;
            //
            cboKhoXuat.DataSource = dtkho;
            cboKhoXuat.DisplayMember = "TenKho";
            cboKhoXuat.ValueMember = "IdKho";
            cboKhoXuat.Text = null;
        }

        private void cboKhoXuat_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                layPhuTung();
                cboPhuTung.DataSource = dtPhuTung;
                cboPhuTung.DisplayMember = "TenPhuTung";
                cboPhuTung.ValueMember = "MaPT";
                cboPhuTung.Text = "";
            }
            catch { }
        }

        //xong
        private void cboPhuTung_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = dtPhuTung.Select("MaPT = " + "'" + cboPhuTung.SelectedValue + "'");
                if (rows.Length > 0)
                {
                    soluong = Convert.ToInt32(rows[0]["SoLuong"]);
                    gia = Convert.ToDecimal(rows[0]["DonGia"]);
                    txtGiatien.Text = gia.ToString("0,0");
                    for (int i = 0; i < lstvDanhSachPhuTung.Items.Count; i++)
                    {
                        if (lstvDanhSachPhuTung.Items[i].SubItems[0].Text == cboPhuTung.SelectedValue.ToString())
                        {
                            soluongdanhap = 0;
                            soluongdanhap += Convert.ToInt32(lstvDanhSachPhuTung.Items[i].SubItems[1].Text);
                            txtSoLuongCon.Text = (soluong - soluongdanhap).ToString();
                            return;
                        }
                    }
                    txtSoLuongCon.Text = soluong.ToString();
                }
            }
            catch { }
        }

        private void cboPhuTung_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                try
                {
                    DataRow[] rows = dtPhuTung.Select("MaPT = " + "'" + cboPhuTung.SelectedValue + "'");
                    if (rows.Length > 0)
                    {
                        soluong = Convert.ToInt32(rows[0]["SoLuong"]);

                        for (int i = 0; i < lstvDanhSachPhuTung.Items.Count; i++)
                        {
                            if (lstvDanhSachPhuTung.Items[i].SubItems[0].Text == cboPhuTung.SelectedValue.ToString())
                            {
                                soluongdanhap = 0;
                                soluongdanhap += Convert.ToInt32(lstvDanhSachPhuTung.Items[i].SubItems[1].Text);
                                txtSoLuongCon.Text = (soluong - soluongdanhap).ToString();
                                return;
                            }
                        }
                        txtSoLuongCon.Text = soluong.ToString();
                    }
                }
                catch { }
            }
        }

        private void bttChuyen_Click(object sender, EventArgs e)
        {
            try
            {
                soluongdanhap = 0;
                int kt;

                if (String.IsNullOrEmpty(cboPhuTung.SelectedValue.ToString()))
                {
                    MessageBox.Show("Bạn chưa chọn phụ tùng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtSoLuongChuyen.Text, out kt))
                {
                    MessageBox.Show("Số lượng phải là kiểu số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (Convert.ToInt32(txtSoLuongChuyen.Text) > soluong)
                {
                    MessageBox.Show("Số lượng nhập lớn hơn số lượng hiện có.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                for (int i = 0; i < lstvDanhSachPhuTung.Items.Count; i++)
                {
                    if (lstvDanhSachPhuTung.Items[i].SubItems[0].Text == cboPhuTung.SelectedValue.ToString() && lstvDanhSachPhuTung.Items[i].SubItems[3].Text == cboKhoXuat.SelectedValue.ToString())
                    {
                        soluongdanhap += Convert.ToInt32(lstvDanhSachPhuTung.Items[i].SubItems[1].Text);
                        soluongdanhap += Convert.ToInt32(txtSoLuongChuyen.Text);
                        if (soluongdanhap > soluong)
                        {
                            MessageBox.Show("Số lượng nhập lớn hơn số lượng hiện có.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        txtSoLuongCon.Text = (soluong - soluongdanhap).ToString();
                        lstvDanhSachPhuTung.Items[i].SubItems[1].Text = soluongdanhap.ToString();

                        return;
                    }
                }
                ListViewItem viewItem = new ListViewItem(cboPhuTung.SelectedValue.ToString());
                viewItem.SubItems.Add(txtSoLuongChuyen.Text);
                viewItem.SubItems.Add(dateTimePicker1.Value.ToShortDateString());
                viewItem.SubItems.Add(cboKhoXuat.SelectedValue.ToString());
                viewItem.SubItems.Add(txtGhiChu.Text);
                viewItem.SubItems.Add(txtGiatien.Text);
                lstvDanhSachPhuTung.Items.Add(viewItem);
                txtSoLuongCon.Text = (soluong - Convert.ToInt32(txtSoLuongChuyen.Text)).ToString();
            }
            catch { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < lstvDanhSachPhuTung.Items.Count; i++)
                {
                    if (lstvDanhSachPhuTung.Items[i].Selected)
                    {
                        if (lstvDanhSachPhuTung.Items[i].SubItems[3].Text == cboKhoXuat.SelectedValue.ToString())
                        {
                            txtSoLuongCon.Text = (Convert.ToInt32(txtSoLuongCon.Text) + Convert.ToInt32(lstvDanhSachPhuTung.Items[i].SubItems[1].Text)).ToString();
                        }
                        lstvDanhSachPhuTung.Items[i].Remove();
                    }
                }

                //for (int i = 0; i < lstvDanhSachPhuTung.Items.Count; i++)
                //{
                //    if (lstvDanhSachPhuTung.Items[i].SubItems[0].Text == mapt)
                //    {
                //        if (cboPhuTung.SelectedValue.ToString() == mapt && lstvDanhSachPhuTung.Items[i].SubItems[3].Text == cboKhoXuat.SelectedValue.ToString())
                //        {
                //            txtSoLuongCon.Text = (Convert.ToInt32(txtSoLuongCon.Text) + Convert.ToInt32(lstvDanhSachPhuTung.Items[i].SubItems[1].Text)).ToString();
                //        }
                //        lstvDanhSachPhuTung.Items.RemoveAt(i);
                //        break;
                //    }
                //}
            }
            catch { }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lstvDanhSachPhuTung.Items.Clear();
            txtSoLuongCon.Text = "";
            txtSoLuongChuyen.Text = "";
            txtGiatien.Text = "";
            txtGhiChu.Text = "";
        }

        private void btnDieuChuyen_Click(object sender, EventArgs e)
        {
            try
            {
                if (lstvDanhSachPhuTung.Items.Count > 0)
                {
                    DataTable dtDsDieuChuyen = new DataTable();
                    dtDsDieuChuyen.Columns.Add("MaPT");
                    dtDsDieuChuyen.Columns.Add("TenPT");
                    dtDsDieuChuyen.Columns.Add("SoLuong");
                    dtDsDieuChuyen.Columns.Add("TenKhoXuat");
                    dtDsDieuChuyen.Columns.Add("TenKhoNhap");
                    dtDsDieuChuyen.Columns.Add("GhiChu");
                    dtDsDieuChuyen.Columns.Add("NgayDieuChuyen");
                    DataRow row;
                    int dem = 0;
                    foreach (ListViewItem items in lstvDanhSachPhuTung.Items)
                    {
                        string mapt1 = items.SubItems[0].Text;
                        string _idkhoxuat = items.SubItems[3].Text;
                        DateTime _ngayXuat = new DateTime();
                        try
                        {
                            _ngayXuat = Convert.ToDateTime(items.SubItems[2].Text);
                        }
                        catch { }
                        string _ghiChu = items.SubItems[4].Text;

                        string sql = "select MaPT, TenPT, DVT, DonGia, SoLuong from PhuTung where IdCongTy = @IdCongTy and IdKho= @IdKho and MaPT = @MaPT";
                        SqlCommand cmd = new SqlCommand(sql);
                        if (idKhoXuat == "")
                        {
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@IdCongTy", idKhoXuat);
                        }
                        cmd.Parameters.AddWithValue("@IdKho", _idkhoxuat);
                        cmd.Parameters.AddWithValue("@MaPT", mapt1);
                        DataTable dt = new DataTable();
                        dt = Class.datatabase.getData(cmd);
                        int slTruoc = int.Parse(dt.Rows[0]["SoLuong"].ToString());
                        string tenpt = dt.Rows[0]["TenPT"].ToString();
                        string dvt = dt.Rows[0]["DVT"].ToString();
                        string dongia = dt.Rows[0]["DonGia"].ToString();

                        string sl = items.SubItems[1].Text;
                        int soluong = int.Parse(sl);
                        int slSau = slTruoc - soluong;

                        cmd = new SqlCommand();
                        cmd.CommandText = "sp_KhoDieuChuyen";
                        cmd.Parameters.AddWithValue("@IdCongTy", Convert.ToInt32(Class.CompanyInfo.idcongty));
                        if (idKhoNhan == "")
                        {
                            cmd.Parameters.AddWithValue("@IdCongTyNhan", Convert.ToInt32(Class.CompanyInfo.idcongty));
                        }
                        else
                        {
                            cmd.Parameters.AddWithValue("@IdCongTyNhan", Convert.ToInt32(idKhoNhan));
                        }
                        cmd.Parameters.AddWithValue("@slSau", slSau);
                        cmd.Parameters.AddWithValue("@IdKhoXuat", _idkhoxuat);
                        cmd.Parameters.AddWithValue("@MaPT", mapt1);
                        cmd.Parameters.AddWithValue("@TenPT", tenpt);
                        cmd.Parameters.AddWithValue("@slChuyen", soluong);
                        cmd.Parameters.AddWithValue("@NgayXuat", _ngayXuat);
                        cmd.Parameters.AddWithValue("@LoaiXuat", "Xuat dieu chuyen");
                        cmd.Parameters.AddWithValue("@GhiChu", _ghiChu);

                        if (cboKhoNhan.SelectedValue != null)
                        {
                            cmd.Parameters.AddWithValue("@IdKhoNhan", cboKhoNhan.SelectedValue);
                        }
                        else
                        {
                            MessageBox.Show(@"Bạn chưa chọn Kho nhận!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                            return;
                        }
                        cmd.Parameters.AddWithValue("@DVT", dvt);
                        cmd.Parameters.AddWithValue("@DonGia", dongia);
                        cmd.Parameters.AddWithValue("@LoaiNhap", "Nhap dieu chuyen");
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                        {
                            dem++;
                        }
                        row = dtDsDieuChuyen.NewRow();
                        row["MaPT"] = mapt1;
                        row["TenPT"] = mapt1;
                        row["SoLuong"] = items.SubItems[1].Text;
                        DataRow[] r = dtkho.Select("IdKho = '" + _idkhoxuat + "'");
                        row["TenKhoXuat"] = r[0]["TenKho"].ToString() + " - " + _idkhoxuat;
                        row["TenKhoNhap"] = cboKhoNhan.Text + " - " + cboKhoNhan.SelectedValue.ToString();
                        row["GhiChu"] = items.SubItems[4].Text;
                        row["NgayDieuChuyen"] = items.SubItems[2].Text;
                        dtDsDieuChuyen.Rows.Add(row);
                    }
                    if (dem == lstvDanhSachPhuTung.Items.Count)
                    {
                        MessageBox.Show(@"Điều chuyển thành công.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        lstvDanhSachPhuTung.Items.Clear();
                    }
                    else
                    {
                        MessageBox.Show(@"Điều chuyển thành công " + dem + @"//" + lstvDanhSachPhuTung.Items.Count + @" phụ tùng.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                    layPhuTung();
                    FrmPhieuXuatKho frm = new FrmPhieuXuatKho();
                    frm.Text = @"Phiếu Điều Chuyển";

                    frm.reportViewer1.LocalReport.DataSources.Clear();
                    frm.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.ReportBaoCaoDieuChuyen.rdlc";
                    Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtDsDieuChuyen);
                    frm.reportViewer1.LocalReport.DataSources.Add(dataset);
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, @"Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cbbChoCoSoXuat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbChoCoSoXuat.SelectedIndex == 0)
            {
                idKhoXuat = "1";

                cboKhoXuat.DataSource = null;
                cboKhoXuat.DataSource = LayTenKho(idKhoXuat);
                cboKhoXuat.DisplayMember = "TenKho";
                cboKhoXuat.ValueMember = "IdKho";
                cboKhoXuat.Text = null;
            }
            if (cbbChoCoSoXuat.SelectedIndex == 1)
            {
                idKhoXuat = "30";

                cboKhoXuat.DataSource = null;
                cboKhoXuat.DataSource = LayTenKho(idKhoXuat);
                cboKhoXuat.DisplayMember = "TenKho";
                cboKhoXuat.ValueMember = "IdKho";
                cboKhoXuat.Text = null;
            }
        }

        private void cbbChonCoSoNhan_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbChonCoSoNhan.SelectedIndex == 0)
            {
                idKhoNhan = "1";

                cboKhoNhan.DataSource = null;
                cboKhoNhan.DataSource = LayTenKho(idKhoNhan);
                cboKhoNhan.DisplayMember = "TenKho";
                cboKhoNhan.ValueMember = "IdKho";
                cboKhoNhan.Text = null;
            }
            if (cbbChonCoSoNhan.SelectedIndex == 1)
            {
                idKhoNhan = "30";

                cboKhoNhan.DataSource = null;
                cboKhoNhan.DataSource = LayTenKho(idKhoNhan);
                cboKhoNhan.DisplayMember = "TenKho";
                cboKhoNhan.ValueMember = "IdKho";
                cboKhoNhan.Text = null;
            }
        }
    }
}