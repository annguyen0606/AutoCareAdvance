using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls
{
    public partial class UcXeDaNhap : UserControl
    {
        private SqlCommand cmd;
        private DataTable dtListProvider = new DataTable();
        private DataTable dtKieuNhapXe = new DataTable();
        private Class.KhDB classdb = new Class.KhDB();
        private DataTable dtListNhanVien = new DataTable();
        private DataTable dtSoHoaDon = new DataTable();
        private DataTable dtHDNhapXe = new DataTable();
        private DataTable dtchiTietNhapXe = new DataTable();
        private DataTable dtChiTietPK = new DataTable();
        private string id;
        private int rownumber = -1;
        private DataTable dtCongNo = new DataTable();
        private DataTable dtThongTin = new DataTable();

        public UcXeDaNhap()
        {
            InitializeComponent();
        }

        private void LoadListProvider()
        {
            cmd = new SqlCommand("Select IdNhaCungCap, TenNhaCungCap From NhaCungCap Where IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtListProvider = Class.datatabase.getData(cmd);
        }

        private void LoadKieuNhapXe()
        {
            dtKieuNhapXe = classdb.KieuNhapXe();
            IdKieuNhapXe.DataSource = dtKieuNhapXe;
            IdKieuNhapXe.ValueMember = "IdKieuNhapXe";
            IdKieuNhapXe.DisplayMember = "TenKieuNhapXe";
        }

        private void LoadNhanVien()
        {
            cmd = new SqlCommand("Select IDNhanVien, TenNhanVien From NhanVien Where IdCongTy =@IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtListNhanVien = Class.datatabase.getData(cmd);
        }

        private void GetSoHoaDon()
        {
            cmd = new SqlCommand("select SoHoaDonNhap, IDKey From HoaDonNhapXe Where IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtSoHoaDon = Class.datatabase.getData(cmd);
        }

        private bool checkNgayThang()
        {
            if (dateTimeInputTuNgay.ValueObject == null || dateTimeInputDenNgay.ValueObject == null)
            {
                return false;
            }
            if (dateTimeInputTuNgay.Value > dateTimeInputDenNgay.Value.AddSeconds(1))
            {
                return false;
            }
            return true;
        }

        private void UcXeDaNhap_Load(object sender, EventArgs e)
        {
            LoadKieuNhapXe();
            LoadListProvider();
            LoadNhanVien();
            GetSoHoaDon();

            IdKieuNhapXe.DataSource = dtKieuNhapXe;
            IdKieuNhapXe.ValueMember = "IdKieuNhapXe";
            IdKieuNhapXe.DisplayMember = "TenKieuNhapXe";

            NhanVien.DataSource = dtListNhanVien;
            NhanVien.ValueMember = "IdNhanVien";
            NhanVien.DisplayMember = "TenNhanVien";

            cboNhaCungCap.DataSource = dtListProvider;
            cboNhaCungCap.ValueMember = "IdNhaCungCap";
            cboNhaCungCap.DisplayMember = "TenNhaCungCap";

            IdNhaCungCap.DataSource = dtListProvider;
            IdNhaCungCap.ValueMember = "IdNhaCungCap";
            IdNhaCungCap.DisplayMember = "TenNhaCungCap";

            dateTimeInputDenNgay.Value = DateTime.Now;
            dateTimeInputTuNgay.Value = DateTime.Now;
        }

        /// <summary>
        /// Tính tổng tiền của hóa đơn nhập xe.
        /// </summary>
        /// <returns>Tổng số tiền nhập xe</returns>
        private Decimal TinhTongTien()
        {
            Decimal _TongTien = 0;

            for (int i = 0; i <= dataGridViewX2.Rows.Count - 1; i++)
            {
                try
                {
                    _TongTien = _TongTien + Convert.ToDecimal(dataGridViewX2.Rows[i].Cells["GiaVAT"].Value.ToString());
                }
                catch { _TongTien = 0; }
            }
            for (int i = 0; i <= grvPhuKien.Rows.Count - 1; i++)
            {
                try
                {
                    _TongTien = _TongTien + Convert.ToDecimal(grvPhuKien.Rows[i].Cells["Tien"].Value.ToString());
                }
                catch { _TongTien = 0; }
            }

            return _TongTien;
        }

        private void buttonItem9_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(id))
                {
                    MessageBox.Show("Bạn phải chọn Hóa đơn nhập xe trước!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return;
                }
                FrmChiNhapXe frm = new FrmChiNhapXe();

                frm.tongtien = TinhTongTien();
                frm.tienDaTra = Convert.ToDecimal(dataGridViewX1.Rows[rownumber].Cells["TienDaTra"].Value);
                frm.idKey = Convert.ToString(dataGridViewX1.Rows[rownumber].Cells["IDKey"].Value);
                frm.idNhaCungCap = Convert.ToString(dataGridViewX1.Rows[rownumber].Cells["IdNhaCungCap"].Value);
                frm.nhaCungCap = Convert.ToString(dtListProvider.Select("IdNhaCungCap = '" + Convert.ToString(dataGridViewX1.Rows[rownumber].Cells["IdNhaCungCap"].Value) + "'")[0]["TenNhaCungCap"]);
                frm.soHoaDon = Convert.ToString(dtSoHoaDon.Select("IDKey = '" + frm.idKey + "'")[0]["SoHoaDonNhap"]);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewX1.Rows[rownumber].Cells["TienDaTra"].Value = frm.tienDaTra;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            cmd = new SqlCommand();
            string sql = @"Select h.IDKey, h.SoHoaDonNhap,h.IDNhaCungCap,h.IDNhanVienTaoPhieu,h.NgayHoaDon,h.DaNhanHoaDOn,h.IdKieuNhapXe,
                           h.LoHang,h.TienDaTra From HoaDonNhapXe h Where h.IdCongTy = @IdCongTy and h.NgayHoaDon between @TuNgay and @DenNgay";

            if (chkTimTheoNhaCungCap.Checked)
            {
                sql += " and h.IdNhaCungCap = @idNhaCungcap";
                cmd.Parameters.AddWithValue("@IdNhaCungCap", cboNhaCungCap.SelectedValue);
            }
            if (rbtnDNHD.Checked)
            {
                sql += " and h.DaNhanHoaDon = 'True'";
            }
            if (rbtnCNHD.Checked)
            {
                sql += " and h.DaNhanHoaDon = 'False'";
            }
            cmd.CommandText = sql;
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@TuNgay", dateTimeInputTuNgay.Value.ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@DenNgay", new DateTime(dateTimeInputDenNgay.Value.Year, dateTimeInputDenNgay.Value.Month, dateTimeInputDenNgay.Value.Day, 23, 59, 59).ToString("yyyy-MM-dd hh:mm:ss"));

            dtHDNhapXe.Clear();
            dtHDNhapXe = Class.datatabase.getData(cmd);
            dataGridViewX1.ClearSelection();
            dataGridViewX1.DataSource = dtHDNhapXe;
            dataGridViewX1.Columns["IDKey"].Visible = false;
        }

        private void btnBaocaocongno_Click(object sender, EventArgs e)
        {
            if (!checkNgayThang())
            {
                MessageBox.Show("Thời gian thống kê không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dtThoiGian = new DataTable();
            dtThoiGian.Columns.Add("TuNgay", typeof(DateTime));
            dtThoiGian.Columns.Add("DenNgay", typeof(DateTime));
            DataRow rows = dtThoiGian.NewRow();
            rows["TuNgay"] = dateTimeInputTuNgay.Value;
            rows["DenNgay"] = dateTimeInputDenNgay.Value;
            dtThoiGian.Rows.Add(rows);
            SqlCommand cmd = new SqlCommand();
            if (chkTimTheoNhaCungCap.Checked)
            {
                cmd.CommandText = @"Select ChiTietNhapXe.SoHoaDonNhap, (select SUM(GiaCoVAT) from ChiTietNhapXe where IdKey=HoaDonNhapXe.IDkey) AS TongTien,HoaDonNhapXe.TienDaTra,
                                    NhaCungCap.TenNhaCungCap,(select SUM(GiaCoVAT) from ChiTietNhapXe where IdKey=HoaDonNhapXe.IDkey) - HoaDonNhapXe.TienDaTra as CongNo 
                                    from HoaDonNhapXe inner join ChiTietNhapXe on HoaDonNhapXe.IDkey=ChiTietNhapXe.IdKey
                                    inner join NhaCungCap on NhaCungCap.IdNhaCungCap=HoaDonNhapXe.IDNhaCungCap
                                    where HoaDonNhapXe.IdCongTy = @IdCongTy And HoaDonNhapXe.IDNhacungcap = @IDNhacungcap and HoaDonNhapXe.NgayHoaDon between Convert(date,@BatDau) And Convert(date,@KetThuc) and ChiTietNhapXe.IdKey in 
                                    (select IdKey from HoaDonNhapXe where TienDaTra<(select SUM(GiaCoVAT) from ChiTietNhapXe where IdKey=HoaDonNhapXe.IDkey))";

                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IDNhacungcap", cboNhaCungCap.SelectedValue);
                cmd.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@KetThuc", new DateTime(dateTimeInputDenNgay.Value.Year, dateTimeInputDenNgay.Value.Month, dateTimeInputDenNgay.Value.Day, 23, 59, 59).ToString("yyyy-MM-dd hh:mm:ss"));
            }
            else
            {
                cmd.CommandText = @"Select ChiTietNhapXe.SoHoaDonNhap, (select SUM(GiaCoVAT) from ChiTietNhapXe where IdKey=HoaDonNhapXe.IDkey) AS TongTien,HoaDonNhapXe.TienDaTra,
                                    NhaCungCap.TenNhaCungCap,(select SUM(GiaCoVAT) from ChiTietNhapXe where IdKey=HoaDonNhapXe.IDkey) - HoaDonNhapXe.TienDaTra as CongNo 
                                    from HoaDonNhapXe inner join ChiTietNhapXe on HoaDonNhapXe.IDkey=ChiTietNhapXe.IdKey
                                    inner join NhaCungCap on NhaCungCap.IdNhaCungCap=HoaDonNhapXe.IDNhaCungCap
                                    where HoaDonNhapXe.IdCongTy = @IdCongTy and HoaDonNhapXe.NgayHoaDon between Convert(date,@BatDau) And Convert(date,@KetThuc) and ChiTietNhapXe.IdKey in 
                                    (select IdKey from HoaDonNhapXe where TienDaTra<(select SUM(GiaCoVAT) from ChiTietNhapXe where IdKey=HoaDonNhapXe.IDkey))";

                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@KetThuc", new DateTime(dateTimeInputDenNgay.Value.Year, dateTimeInputDenNgay.Value.Month, dateTimeInputDenNgay.Value.Day, 23, 59, 59).ToString("yyyyMMdd"));
            }
            //
            dtCongNo = Class.datatabase.getData(cmd);
            //
            cmd.CommandText = "select * from CongTy where IdCongTy=" + Class.CompanyInfo.idcongty;
            dtThongTin = Class.datatabase.getData(cmd);

            frmThongKe frm = new frmThongKe();
            frm.reportViewer1.LocalReport.DataSources.Clear();
            frm.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.BCCongNoXeNhap.rdlc";
            Microsoft.Reporting.WinForms.ReportDataSource data1 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtCongNo);
            Microsoft.Reporting.WinForms.ReportDataSource data3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", dtThongTin);
            Microsoft.Reporting.WinForms.ReportDataSource data2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtThoiGian);
            frm.reportViewer1.LocalReport.DataSources.Add(data1);
            frm.reportViewer1.LocalReport.DataSources.Add(data2);
            frm.reportViewer1.LocalReport.DataSources.Add(data3);
            frm.ShowDialog();
        }

        private void btnThongkexe_Click(object sender, EventArgs e)
        {
            if (!checkNgayThang())
            {
                MessageBox.Show("Thời gian thống kê không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            if (chkTimTheoNhaCungCap.Checked)
            {
                if (rbtnTatCaHD.Checked)
                {
                    cmd.CommandText = "Select NhaCungCap.TenNhaCungCap,XeMay.TenXe,MauXe.TenMau,ChiTietNhapXe.SoKhung,ChiTietNhapXe.SoMay,ChiTietNhapXe.GiaNhap as DonGia,ChiTietNhapXe.GiaCoVAT as GiaVAT "
                        + "From HoaDonNhapXe inner join ChiTietNhapXe on HoaDonNhapXe.IDkey=ChiTietNhapXe.IdKey inner join XeMay on XeMay.IDXe=ChiTietNhapXe.IdXe inner join NhaCungCap on NhaCungCap.IdNhaCungCap=HoaDonNhapXe.IDNhaCungCap inner join MauXe on MauXe.IdMaMau=ChiTietNhapXe.IdMauXe"
                        + " Where HoaDonNhapXe.IdCongTy = @IdCongTy And HoaDonNhapXe.IDNhacungcap = @IDNhacungcap And HoaDonNhapXe.NgayHoaDon between Convert(date,@BatDau) And Convert(date,@KetThuc)";
                }
                if (rbtnCNHD.Checked)
                {
                    cmd.CommandText = "Select NhaCungCap.TenNhaCungCap,XeMay.TenXe,MauXe.TenMau,ChiTietNhapXe.SoKhung,ChiTietNhapXe.SoMay,ChiTietNhapXe.GiaNhap as DonGia,ChiTietNhapXe.GiaCoVAT as GiaVAT "
                          + "From HoaDonNhapXe inner join ChiTietNhapXe on HoaDonNhapXe.IDkey=ChiTietNhapXe.IdKey inner join XeMay on XeMay.IDXe=ChiTietNhapXe.IdXe inner join NhaCungCap on NhaCungCap.IdNhaCungCap=HoaDonNhapXe.IDNhaCungCap inner join MauXe on MauXe.IdMaMau=ChiTietNhapXe.IdMauXe"
                          + " Where HoaDonNhapXe.IdCongTy = @IdCongTy And HoaDonNhapXe.IDNhacungcap = @IDNhacungcap And HoaDonNhapXe.DaNhanHoaDon= 'False' And HoaDonNhapXe.NgayHoaDon between Convert(date,@BatDau) And Convert(date,@KetThuc)";
                }
                if (rbtnDNHD.Checked)
                {
                    cmd.CommandText = "Select NhaCungCap.TenNhaCungCap,XeMay.TenXe,MauXe.TenMau,ChiTietNhapXe.SoKhung,ChiTietNhapXe.SoMay,ChiTietNhapXe.GiaNhap as DonGia,ChiTietNhapXe.GiaCoVAT as GiaVAT "
                  + "From HoaDonNhapXe inner join ChiTietNhapXe on HoaDonNhapXe.IDkey=ChiTietNhapXe.IdKey inner join XeMay on XeMay.IDXe=ChiTietNhapXe.IdXe inner join NhaCungCap on NhaCungCap.IdNhaCungCap=HoaDonNhapXe.IDNhaCungCap inner join MauXe on MauXe.IdMaMau=ChiTietNhapXe.IdMauXe"
                  + " Where HoaDonNhapXe.IdCongTy = @IdCongTy And HoaDonNhapXe.IDNhacungcap = @IDNhacungcap And HoaDonNhapXe.DaNhanHoaDon= 'True' And HoaDonNhapXe.NgayHoaDon between Convert(date,@BatDau) And Convert(date,@KetThuc)";
                }
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IDNhacungcap", cboNhaCungCap.SelectedValue);
                cmd.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@KetThuc", new DateTime(dateTimeInputDenNgay.Value.Year, dateTimeInputDenNgay.Value.Month, dateTimeInputDenNgay.Value.Day, 23, 59, 59).ToString("yyyy-MM-dd hh:mm:ss"));
            }
            else
            {
                if (rbtnTatCaHD.Checked)
                {
                    cmd.CommandText = "Select NhaCungCap.TenNhaCungCap,XeMay.TenXe,MauXe.TenMau,ChiTietNhapXe.SoKhung,ChiTietNhapXe.SoMay,ChiTietNhapXe.GiaNhap as DonGia,ChiTietNhapXe.GiaCoVAT as GiaVAT "
                        + "From HoaDonNhapXe inner join ChiTietNhapXe on HoaDonNhapXe.IDkey=ChiTietNhapXe.IdKey inner join XeMay on XeMay.IDXe=ChiTietNhapXe.IdXe inner join NhaCungCap on NhaCungCap.IdNhaCungCap=HoaDonNhapXe.IDNhaCungCap inner join MauXe on MauXe.IdMaMau=ChiTietNhapXe.IdMauXe"
                        + " Where HoaDonNhapXe.IdCongTy = @IdCongTy And HoaDonNhapXe.NgayHoaDon between Convert(date,@BatDau) And Convert(date,@KetThuc)";
                }
                if (rbtnCNHD.Checked)
                {
                    cmd.CommandText = "Select NhaCungCap.TenNhaCungCap,XeMay.TenXe,MauXe.TenMau,ChiTietNhapXe.SoKhung,ChiTietNhapXe.SoMay,ChiTietNhapXe.GiaNhap as DonGia,ChiTietNhapXe.GiaCoVAT as GiaVAT "
                          + "From HoaDonNhapXe inner join ChiTietNhapXe on HoaDonNhapXe.IDkey=ChiTietNhapXe.IdKey inner join XeMay on XeMay.IDXe=ChiTietNhapXe.IdXe inner join NhaCungCap on NhaCungCap.IdNhaCungCap=HoaDonNhapXe.IDNhaCungCap inner join MauXe on MauXe.IdMaMau=ChiTietNhapXe.IdMauXe"
                          + " Where HoaDonNhapXe.IdCongTy = @IdCongTy And HoaDonNhapXe.DaNhanHoaDon= 'False' And HoaDonNhapXe.NgayHoaDon between Convert(date,@BatDau) And Convert(date,@KetThuc)";
                }
                if (rbtnDNHD.Checked)
                {
                    cmd.CommandText = "Select NhaCungCap.TenNhaCungCap,XeMay.TenXe,MauXe.TenMau,ChiTietNhapXe.SoKhung,ChiTietNhapXe.SoMay,ChiTietNhapXe.GiaNhap as DonGia,ChiTietNhapXe.GiaCoVAT as GiaVAT "
                  + "From HoaDonNhapXe inner join ChiTietNhapXe on HoaDonNhapXe.IDkey=ChiTietNhapXe.IdKey inner join XeMay on XeMay.IDXe=ChiTietNhapXe.IdXe inner join NhaCungCap on NhaCungCap.IdNhaCungCap=HoaDonNhapXe.IDNhaCungCap inner join MauXe on MauXe.IdMaMau=ChiTietNhapXe.IdMauXe"
                  + " Where HoaDonNhapXe.IdCongTy = @IdCongTy And HoaDonNhapXe.DaNhanHoaDon= 'True' And HoaDonNhapXe.NgayHoaDon between Convert(date,@BatDau) And Convert(date,@KetThuc)";
                }
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@KetThuc", new DateTime(dateTimeInputDenNgay.Value.Year, dateTimeInputDenNgay.Value.Month, dateTimeInputDenNgay.Value.Day, 23, 59, 59).ToString("yyyy-MM-dd hh:mm:ss"));
            }

            DataTable dt2 = new DataTable();
            dt2 = Class.datatabase.getData(cmd);
            DataTable dtThoiGian = new DataTable();
            dtThoiGian.Columns.Add("TuNgay", typeof(DateTime));
            dtThoiGian.Columns.Add("DenNgay", typeof(DateTime));
            DataRow rows = dtThoiGian.NewRow();
            rows["TuNgay"] = dateTimeInputTuNgay.Value;
            rows["DenNgay"] = dateTimeInputDenNgay.Value;
            dtThoiGian.Rows.Add(rows);
            cmd.CommandText = "select * from CongTy where IdCongTy=" + Class.CompanyInfo.idcongty;
            dtThongTin = Class.datatabase.getData(cmd);

            frmThongKe frm = new frmThongKe();
            frm.reportViewer1.LocalReport.DataSources.Clear();
            frm.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.ReportXeNhap.rdlc";
            Microsoft.Reporting.WinForms.ReportDataSource data1 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dt2);
            Microsoft.Reporting.WinForms.ReportDataSource data2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtThoiGian);
            Microsoft.Reporting.WinForms.ReportDataSource data3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", dtThongTin);
            frm.reportViewer1.LocalReport.DataSources.Add(data1);
            frm.reportViewer1.LocalReport.DataSources.Add(data2);
            frm.reportViewer1.LocalReport.DataSources.Add(data3);
            frm.ShowDialog();
        }

        private void dataGridViewX1_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit;
            if (e.Button == MouseButtons.Right)
            {
                hit = dataGridViewX1.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    buttonItem9.Enabled = true;
                    if (!((DataGridViewRow)(dataGridViewX1.Rows[hit.RowIndex])).Selected)
                    {
                        dataGridViewX1.ClearSelection();
                        ((DataGridViewRow)(dataGridViewX1.Rows[hit.RowIndex])).Selected = true;
                        rownumber = hit.RowIndex;
                    }
                    if (((DataGridViewRow)(dataGridViewX1.Rows[hit.RowIndex])).Selected)
                    {
                        rownumber = hit.RowIndex;
                    }
                }
                else
                    buttonItem9.Enabled = false;
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                LoadChiTietXe();
            }
        }

        private void LoadChiTietXe()
        {
            try
            {
                id = Convert.ToString(dataGridViewX1.SelectedRows[0].Cells["IDkey"].Value.ToString());

                cmd = new SqlCommand(@"Select ChiTietNhapXe.IdXe, TenXe, TenLoaiXe, SoKhung,SoMay,TenMauXe,DangKiem,SoBaoHanh, GiaCoVAT From ChiTietNhapXe
                                    inner join XeMay on XeMay.IdXe = ChiTietNhapXe.IdXe
                                    inner join LoaiXe on LoaiXe.IdLoaiXe=ChiTietNhapXe.IdLoaiXe
                                    inner join MauXeMay on MauXeMay.IdMauXe=ChiTietNhapXe.IdMauXe
                                    Where ChiTietNhapXe.IdKey = @idKey");

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idKey", id);
                dtchiTietNhapXe = Class.datatabase.getData(cmd);
                dataGridViewX2.DataSource = dtchiTietNhapXe;

                cmd = new SqlCommand(@"SELECT ctpk.IdPhuKien, pk.TenPhuKien, pk.DVT, ctpk.ThanhTien
                                    FROM ChiTietNhapPhuKien ctpk, PhuKien pk
                                    WHERE pk.IdPhuKien = ctpk.IdPhuKien AND ctpk.IdCongTy = @IdCongTy AND ctpk.IdKey = @IdKey");
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IdKey", id);
                dtChiTietPK = Class.datatabase.getData(cmd);
                grvPhuKien.DataSource = dtChiTietPK;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi load danh sách xe! Lỗi: " + ex.Message);
            }
        }

        private void dataGridViewX2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Bạn có muốn xóa xe đã chọn không?", "Xóa xe trong kho!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string MaXe = dataGridViewX2.Rows[e.RowIndex].Cells["IdXe"].Value.ToString();
                    string SoKhung = dataGridViewX2.Rows[e.RowIndex].Cells["SoKhung"].Value.ToString();
                    string SoMay = dataGridViewX2.Rows[e.RowIndex].Cells["SoMay"].Value.ToString();

                    try
                    {
                        SqlCommand cmDeleteXe = new SqlCommand("delete ChiTietNhapXe where IdCongTy = @IdCongTy and IdXe = @IdXe and SoKhung = @SoKhung and SoMay = @SoMay");
                        cmDeleteXe.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmDeleteXe.Parameters.AddWithValue("@IdXe", MaXe);
                        cmDeleteXe.Parameters.AddWithValue("@SoKhung", SoKhung);
                        cmDeleteXe.Parameters.AddWithValue("@SoMay", SoMay);

                        Class.datatabase.ExcuteNonQuery(cmDeleteXe);

                        SqlCommand cmdDeleteXeTrongKho = new SqlCommand("delete ChiTietXe where IdCongTy = @IdCongTy and IdKey = @IdKey and SoKhung = @SoKhung and SoMay = @SoMay");
                        cmdDeleteXeTrongKho.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmdDeleteXeTrongKho.Parameters.AddWithValue("@IdKey", MaXe);
                        cmdDeleteXeTrongKho.Parameters.AddWithValue("@SoKhung", SoKhung);
                        cmdDeleteXeTrongKho.Parameters.AddWithValue("@SoMay", SoMay);

                        Class.datatabase.ExcuteNonQuery(cmdDeleteXeTrongKho);

                        MessageBox.Show("Xóa xe thành công!");

                        LoadChiTietXe();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xóa xe không thành công! Lỗi: " + ex.Message);
                    }
                }
            }
        }

        private void dataGridViewX2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string MaXe = dataGridViewX2.Rows[e.RowIndex].Cells["IdXe"].Value.ToString();
                string TenXe = dataGridViewX2.Rows[e.RowIndex].Cells["TenXe"].Value.ToString();
                string MauXe = dataGridViewX2.Rows[e.RowIndex].Cells["MauXe"].Value.ToString();
                string LoaiXe = dataGridViewX2.Rows[e.RowIndex].Cells["KieuXe"].Value.ToString();
                string SoKhung = dataGridViewX2.Rows[e.RowIndex].Cells["SoKhung"].Value.ToString();
                string SoMay = dataGridViewX2.Rows[e.RowIndex].Cells["SoMay"].Value.ToString();

                frmCapNhatThongTinXe frm = new frmCapNhatThongTinXe();
                frm.IdKey = MaXe;
                frm.TenXe = TenXe;
                frm.MauXe = MauXe;
                frm.LoaiXe = LoaiXe;
                frm.SoKhung = SoKhung;
                frm.SoMay = SoMay;
                frm.ReloadXe = LoadChiTietXe;
                frm.fromHoaDonNhapXe = true;
                frm.ShowDialog();
            }
        }
    }
}