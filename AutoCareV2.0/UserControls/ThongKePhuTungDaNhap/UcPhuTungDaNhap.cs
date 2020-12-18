using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoCareV2._0.Class;
using System.Data.SqlClient;

namespace AutoCareV2._0.UserControls
{
    public partial class UcPhuTungDaNhap : UserControl
    {
        public UcPhuTungDaNhap()
        {
            InitializeComponent();
        }
        DataTable dt = new DataTable();
        DataTable dtHoaDon = new DataTable();
        KhDB classdb = new KhDB();
        DataTable dtNhaCungCap = new DataTable();

        DataTable dtCongNo = new DataTable();
        private static string IdKey;
        //private void showthongtincongty()
        //{
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.CommandText = "select * from CongTy where IdCongTy=" + Class.CompanyInfo.idcongty;
        //    DataTable dtThongTin = new DataTable();
        //    dtThongTin = Class.datatabase.getData(cmd);
        //    Microsoft.Reporting.WinForms.ReportDataSource data3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", dtThongTin);
        //    reportViewer1.LocalReport.DataSources.Add(data3);
        //    //frmThongTinBindingSource.DataSource = dtThongTin;

        //}
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

        private void UcPhuTungDaNhap_Load(object sender, EventArgs e)
        {
            dateTimeInputDenNgay.Value = DateTime.Now;
            dateTimeInputTuNgay.Value = DateTime.Now;
            dtNhaCungCap = classdb.NhaCungCap();
            this.NhaCungCap.DataSource = dtNhaCungCap;
            this.NhaCungCap.ValueMember = "IdNhaCungCap";
            this.NhaCungCap.DisplayMember = "TenNhaCungCap";
            //Load cboNhacungcap
            cboNhacungcap.DataSource = dtNhaCungCap;
            cboNhacungcap.ValueMember = "IdNhaCungCap";
            cboNhacungcap.DisplayMember = "TenNhaCungCap";

            this.KieuNhap.DataSource = classdb.KieuNhapXe();
            this.KieuNhap.ValueMember = "IdKieuNhapXe";
            this.KieuNhap.DisplayMember = "TenKieuNhapXe";
            SqlCommand cmd = new SqlCommand("Select (MaPT + '-' + TenPT) as MaPT, IdPT FRom PhuTung Where IDcongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            DataTable dt = Class.datatabase.getData(cmd);
            if (dt.Rows.Count > 0)
            {
                PhuTung.DataSource = dt;
                PhuTung.DisplayMember = "MaPT";
                PhuTung.ValueMember = "IdPT";
            }
        }
        int rownumber = -1;
        private void buttonItem2_Click(object sender, EventArgs e)
        {
            try
            {
                frmPhieuChi2 frm = new frmPhieuChi2();
                decimal a = 0;

                a = Convert.ToDecimal(dataGridViewX1.Rows[rownumber].Cells["TongTien"].Value);
                frm.tongtien = a;
                frm.tienDaTra = Convert.ToDecimal(dataGridViewX1.Rows[rownumber].Cells["TienDaTra"].Value);
                frm.idKey = Convert.ToString(dataGridViewX1.Rows[rownumber].Cells["SoHoaDonNhap"].Value);
                frm.idNhaCungCap = Convert.ToString(dataGridViewX1.Rows[rownumber].Cells["NhaCungCap"].Value);
                try
                {
                    frm.nhaCungCap = Convert.ToString(dtNhaCungCap.Select("IdNhaCungCap = '" + Convert.ToString(dataGridViewX1.Rows[rownumber].Cells["NhaCungCap"].Value) + "'")[0]["TenNhaCungCap"]);
                }
                catch (Exception)
                {}
                
                frm.soHoaDon = Convert.ToString(dtHoaDon.Select("IDKey = '" + frm.idKey + "'")[0]["SoHoaDon"]);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    dataGridViewX1.Rows[rownumber].Cells["TienDaTra"].Value = frm.tienDaTra;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            if (!checkNgayThang())
            {
                MessageBox.Show(@"Thời gian thống kê không hợp lệ.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            SqlCommand cmd1 = new SqlCommand();
            if (chkThongkenhacungcap.Checked)
            {
                if (rbtnTatCa.Checked)
                {
                    cmd.CommandText = "Select IDKey, LoHang,GhiChu,TongTien,TienDaTra,IDNhaCungCap,NgayLap,IDKieuNhap,DaNhanHoaDon From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And IDNhaCungCap=@IDNhacungcap And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc)";
                    cmd1.CommandText = "Select SoHoaDon,IDKey From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And IDNhaCungCap=@IDNhacungcap And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc)";

                }
                if (rbtnDaTraDuTien.Checked)
                {
                    cmd.CommandText = "Select IdKey, LoHang,GhiChu,TongTien,TienDaTra,IDNhaCungCap,NgayLap,IDKieuNhap,DaNhanHoaDon From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And IDNhaCungCap=@IDNhacungcap And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra >= TongTien";
                    cmd1.CommandText = "Select SoHoaDon,IdKey From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And IDNhaCungCap=@IDNhacungcap And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra >= TongTien";
                }
                if (rbtnChuaTraDuTien.Checked)
                {
                    cmd.CommandText = "Select IDKey, LoHang,GhiChu,TongTien,TienDaTra,IDNhaCungCap,NgayLap,IDKieuNhap,DaNhanHoaDon From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And IDNhaCungCap=@IDNhacungcap And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra < TongTien";
                    cmd1.CommandText = "Select SoHoaDon, IdKey From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And IDNhaCungCap=@IDNhacungcap And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra < TongTien";
                }
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IDNhacungcap", cboNhacungcap.SelectedValue);
                cmd.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value);
                cmd.Parameters.AddWithValue("@KetThuc", dateTimeInputDenNgay.Value);
                //
                cmd1.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd1.Parameters.AddWithValue("@IDNhacungcap", cboNhacungcap.SelectedValue);
                cmd1.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value);
                cmd1.Parameters.AddWithValue("@KetThuc", dateTimeInputDenNgay.Value);
            }
            else
            {
                if (rbtnTatCa.Checked)
                {
                    cmd.CommandText = "Select IDKey, LoHang,GhiChu,TongTien,TienDaTra,IDNhaCungCap,NgayLap,IDKieuNhap,DaNhanHoaDon From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc)";
                    cmd1.CommandText = "Select SoHoaDon,IDKey From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc)";

                }
                if (rbtnDaTraDuTien.Checked)
                {
                    cmd.CommandText = "Select IdKey, LoHang,GhiChu,TongTien,TienDaTra,IDNhaCungCap,NgayLap,IDKieuNhap,DaNhanHoaDon From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra >= TongTien";
                    cmd1.CommandText = "Select SoHoaDon,IdKey From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra >= TongTien";
                }
                if (rbtnChuaTraDuTien.Checked)
                {
                    cmd.CommandText = "Select IDKey, LoHang,GhiChu,TongTien,TienDaTra,IDNhaCungCap,NgayLap,IDKieuNhap,DaNhanHoaDon From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra < TongTien";
                    cmd1.CommandText = "Select SoHoaDon, IdKey From HoaDonNhapPhuTung Where IdCongTy = @IDCongTy And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra < TongTien";
                }
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value);
                cmd.Parameters.AddWithValue("@KetThuc", dateTimeInputDenNgay.Value);
                //
                cmd1.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd1.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value);
                cmd1.Parameters.AddWithValue("@KetThuc", dateTimeInputDenNgay.Value);
            }

            dtHoaDon = Class.datatabase.getData(cmd1);
            if (dtHoaDon.Rows.Count > 0)
            {
                SoHoaDonNhap.DataSource = dtHoaDon;
                SoHoaDonNhap.DisplayMember = "SoHoaDon";
                SoHoaDonNhap.ValueMember = "IDKey";
            }
            dataGridViewX1.DataSource = Class.datatabase.getData(cmd);
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            if (!checkNgayThang())
            {
                MessageBox.Show(@"Thời gian thống kê không hợp lệ.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            if (chkThongkenhacungcap.Checked)
            {
                if (rbtnTatCa.Checked)
                {
                    cmd.CommandText = "Select TenNhaCungCap, TenPT, ChiTietNhapPhuTung.SoLuong, ChiTietNhapPhuTung.DonGiaNhap As DonGia,ChiTietNhapPhuTung.DonGiaNhap * ChiTietNhapPhuTung.SoLuong as ThanhTien  From HoaDonNhapPhuTung inner join ChiTietNhapPhuTung On HoaDonNhapPhuTung.IDKey = ChiTietNhapPhuTung.IDKey"
                 + " Inner join PhuTung  ON PhuTung.IDPT = ChiTietNhapPhuTung.IdPhuTung inner join NhaCungCap ON HoaDonNhapPhuTung.IDNhaCungCap = NhaCungCap.IdNhaCungCap Where HoaDonNhapPhuTung.IdCongTy = @IdCongTy And HoaDonNhapPhuTung.IDNhaCungCap=@IDNhacungcap And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc)";

                }
                if (rbtnDaTraDuTien.Checked)
                {
                    cmd.CommandText = "Select TenNhaCungCap, TenPT, ChiTietNhapPhuTung.SoLuong, ChiTietNhapPhuTung.DonGiaNhap As DonGia,ChiTietNhapPhuTung.DonGiaNhap * ChiTietNhapPhuTung.SoLuong as ThanhTien  From HoaDonNhapPhuTung inner join ChiTietNhapPhuTung On HoaDonNhapPhuTung.IDKey = ChiTietNhapPhuTung.IDKey"
                     + " Inner join PhuTung  ON PhuTung.IDPT = ChiTietNhapPhuTung.IdPhuTung inner join NhaCungCap ON HoaDonNhapPhuTung.IDNhaCungCap = NhaCungCap.IdNhaCungCap Where HoaDonNhapPhuTung.IdCongTy = @IdCongTy And HoaDonNhapPhuTung.IDNhaCungCap=@IDNhacungcap And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra >= TongTien";
                }
                if (rbtnChuaTraDuTien.Checked)
                {
                    cmd.CommandText = "Select TenNhaCungCap, TenPT, ChiTietNhapPhuTung.SoLuong, ChiTietNhapPhuTung.DonGiaNhap As DonGia,ChiTietNhapPhuTung.DonGiaNhap * ChiTietNhapPhuTung.SoLuong as ThanhTien  From HoaDonNhapPhuTung inner join ChiTietNhapPhuTung On HoaDonNhapPhuTung.IDKey = ChiTietNhapPhuTung.IDKey"
                     + " Inner join PhuTung  ON PhuTung.IdPT = ChiTietNhapPhuTung.IdPhuTung inner join NhaCungCap ON HoaDonNhapPhuTung.IDNhaCungCap = NhaCungCap.IdNhaCungCap Where HoaDonNhapPhuTung.IdCongTy = @IdCongTy And HoaDonNhapPhuTung.IDNhaCungCap=@IDNhacungcap And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra < TongTien";
                }
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IDNhacungcap", cboNhacungcap.SelectedValue);
                cmd.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value);
                cmd.Parameters.AddWithValue("@KetThuc", dateTimeInputDenNgay.Value);
            }
            else
            {
                if (rbtnTatCa.Checked)
                {
                    cmd.CommandText = "Select TenNhaCungCap, TenPT, ChiTietNhapPhuTung.SoLuong, ChiTietNhapPhuTung.DonGiaNhap As DonGia,ChiTietNhapPhuTung.DonGiaNhap * ChiTietNhapPhuTung.SoLuong as ThanhTien  From HoaDonNhapPhuTung inner join ChiTietNhapPhuTung On HoaDonNhapPhuTung.IDKey = ChiTietNhapPhuTung.IDKey"
                 + " Inner join PhuTung  ON PhuTung.IDPT = ChiTietNhapPhuTung.IdPhuTung inner join NhaCungCap ON HoaDonNhapPhuTung.IDNhaCungCap = NhaCungCap.IdNhaCungCap Where HoaDonNhapPhuTung.IdCongTy = @IdCongTy And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc)";

                }
                if (rbtnDaTraDuTien.Checked)
                {
                    cmd.CommandText = "Select TenNhaCungCap, TenPT, ChiTietNhapPhuTung.SoLuong, ChiTietNhapPhuTung.DonGiaNhap As DonGia,ChiTietNhapPhuTung.DonGiaNhap * ChiTietNhapPhuTung.SoLuong as ThanhTien  From HoaDonNhapPhuTung inner join ChiTietNhapPhuTung On HoaDonNhapPhuTung.IDKey = ChiTietNhapPhuTung.IDKey"
                     + " Inner join PhuTung  ON PhuTung.IDPT = ChiTietNhapPhuTung.IdPhuTung inner join NhaCungCap ON HoaDonNhapPhuTung.IDNhaCungCap = NhaCungCap.IdNhaCungCap Where HoaDonNhapPhuTung.IdCongTy = @IdCongTy And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra >= TongTien";
                }
                if (rbtnChuaTraDuTien.Checked)
                {
                    cmd.CommandText = "Select TenNhaCungCap, TenPT, ChiTietNhapPhuTung.SoLuong, ChiTietNhapPhuTung.DonGiaNhap As DonGia,ChiTietNhapPhuTung.DonGiaNhap * ChiTietNhapPhuTung.SoLuong as ThanhTien  From HoaDonNhapPhuTung inner join ChiTietNhapPhuTung On HoaDonNhapPhuTung.IDKey = ChiTietNhapPhuTung.IDKey"
                     + " Inner join PhuTung  ON PhuTung.IdPT = ChiTietNhapPhuTung.IdPhuTung inner join NhaCungCap ON HoaDonNhapPhuTung.IDNhaCungCap = NhaCungCap.IdNhaCungCap Where HoaDonNhapPhuTung.IdCongTy = @IdCongTy And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra < TongTien";
                }
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value);
                cmd.Parameters.AddWithValue("@KetThuc", dateTimeInputDenNgay.Value);

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
            frmThongKe frm = new frmThongKe();
            frm.reportViewer1.LocalReport.DataSources.Clear();
            frm.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.ReportHangNhap.rdlc";
            cmd.CommandText = "select * from CongTy where IdCongTy=" + Class.CompanyInfo.idcongty;
            DataTable dtThongTin = new DataTable();
            dtThongTin = Class.datatabase.getData(cmd);
            Microsoft.Reporting.WinForms.ReportDataSource data3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", dtThongTin);
            Microsoft.Reporting.WinForms.ReportDataSource data1 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dt2);
            Microsoft.Reporting.WinForms.ReportDataSource data2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtThoiGian);
            frm.reportViewer1.LocalReport.DataSources.Add(data1);
            frm.reportViewer1.LocalReport.DataSources.Add(data2);
            frm.reportViewer1.LocalReport.DataSources.Add(data3);
            frm.ShowDialog();
        }

        private void btnBaoCaoCongNo_Click(object sender, EventArgs e)
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
            if (chkThongkenhacungcap.Checked)
            {
                cmd.CommandText = "Select TongTien,TienDaTra,TenNhaCungCap, TongTien - TienDaTra as CongNo From HoaDonNhapPhuTung inner join NhaCungCap " +
"On NhaCungCap.IdNhaCungCap = HoaDonNhapPhuTung.IDNhaCungCap Where HoaDonNhapPhuTung.IdCongTy = @IDCongTy And HoaDonNhapPhuTung.IDNhaCungCap=@IDNhacungcap And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra < TongTien";
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IDNhacungcap", cboNhacungcap.SelectedValue);
                cmd.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value);
                cmd.Parameters.AddWithValue("@KetThuc", dateTimeInputDenNgay.Value);
            }
            else
            {
                cmd.CommandText = "Select TongTien,TienDaTra,TenNhaCungCap, TongTien - TienDaTra as CongNo From HoaDonNhapPhuTung inner join NhaCungCap " +
"On NhaCungCap.IdNhaCungCap = HoaDonNhapPhuTung.IDNhaCungCap Where HoaDonNhapPhuTung.IdCongTy = @IDCongTy And NgayLap between Convert(date,@BatDau) And Convert(date,@KetThuc) And TienDaTra < TongTien";
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value);
                cmd.Parameters.AddWithValue("@KetThuc", dateTimeInputDenNgay.Value);
            }

            //
            dtCongNo = Class.datatabase.getData(cmd);
            //
            frmThongKe frm = new frmThongKe();
            frm.reportViewer1.LocalReport.DataSources.Clear();
            frm.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.BCCongNo.rdlc";
            cmd.CommandText = "select * from CongTy where IdCongTy=" + Class.CompanyInfo.idcongty;
            DataTable dtThongTin = new DataTable();
            dtThongTin = Class.datatabase.getData(cmd);
            Microsoft.Reporting.WinForms.ReportDataSource data3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", dtThongTin);
            Microsoft.Reporting.WinForms.ReportDataSource data1 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtCongNo);
            Microsoft.Reporting.WinForms.ReportDataSource data2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtThoiGian);
            frm.reportViewer1.LocalReport.DataSources.Add(data1);
            frm.reportViewer1.LocalReport.DataSources.Add(data2);
            frm.reportViewer1.LocalReport.DataSources.Add(data3);
            frm.ShowDialog();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (IdKey != "" && IdKey != null)
            {
                if (MessageBox.Show(@"Sau khi xóa hóa đơn sẽ không thể phục hồi dữ liệu, bạn có muốn xóa?", @"Xóa hóa đơn nhập phụ tùng", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    SqlConnection con = new SqlConnection();
                    con = Class.datatabase.getConnection();
                    con.Open();
                    string tienDaTra = new SqlCommand("select TienDaTra from HoaDonNhapPhuTung where IDKey=" + IdKey, con).ExecuteScalar().ToString();
                    con.Close();

                    if (Convert.ToDecimal(tienDaTra) > 0)
                    {
                        if (MessageBox.Show(@"Phiếu chi đã được cho hóa đơn này.\nBạn cần xóa phiếu chi để thông tin được thống nhất!\nBạn có muốn tiếp tục?", @"Xóa hóa đơn nhập phụ tùng?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Delete_HoaDonNhapPhuTung();
                        }
                    }
                    else
                    {
                        Delete_HoaDonNhapPhuTung();
                    }
                }
            }
            else { MessageBox.Show(@"Hãy chọn hóa đơn muốn xóa.", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        /// <summary>
        /// Delete_s the hoa don nhap phu tung.
        /// </summary>
        private void Delete_HoaDonNhapPhuTung()
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select * from ChiTietNhapPhuTung where IDKey=@IdKey";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdKey", IdKey);
            DataTable dtChiTietNhapPT = new DataTable();
            dtChiTietNhapPT = Class.datatabase.getData(cmd);

            SqlConnection con = new SqlConnection();
            con = Class.datatabase.getConnection();

            cmd.Connection = con;
            cmd.Connection.Open();
            SqlTransaction tran = cmd.Connection.BeginTransaction();
            cmd.Transaction = tran;

            try
            {
                for (int i = 0; i < dtChiTietNhapPT.Rows.Count; i++)
                {
                    cmd.CommandText = "update PhuTung set SoLuong = SoLuong - @SoLuongNhap where IdPT=@IdPT";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@SoLuongNhap", dtChiTietNhapPT.Rows[i]["SoLuong"].ToString().Trim());
                    cmd.Parameters.AddWithValue("@IdPT", dtChiTietNhapPT.Rows[i]["IdPhuTung"]);
                    cmd.ExecuteNonQuery();
                }
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdKey", IdKey);
                cmd.CommandText = "Delete from ChiTietNhapPhuTung where IDKey=@IdKey";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "Delete from HoaDonNhapPhuTung where IDKey=@IdKey";
                cmd.ExecuteNonQuery();

                tran.Commit();
                MessageBox.Show(@"Xóa hóa đơn thành công.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnTimKiem_Click(null, null);
                dataGridViewXChiTiet.DataSource = null;
            }
            catch (Exception ex)
            {
                tran.Rollback();
                MessageBox.Show(@"Xóa hóa đơn thất bại." + ex.Message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { cmd.Connection.Close(); }
        }

        private void dataGridViewX1_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit;
            if (e.Button == MouseButtons.Right)
            {
                hit = dataGridViewX1.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    buttonItem1.Enabled = true;
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
                    buttonItem1.Enabled = false;
            }
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            //{
            //    IdKey = dataGridViewX1.Rows[e.RowIndex].Cells["SoHoaDonNhap"].Value.ToString();
            //    rownumber = e.RowIndex;
            //    SqlCommand cmd = new SqlCommand("Select IdPhuTung,SoLuong,DonGiaNhap, SoLuong * DonGiaNhap As ThanhTien From ChiTietNhapPhuTung Where IDKey = @IdKey");
            //    cmd.Parameters.AddWithValue("@IDKey", IdKey);
            //    dt = Class.datatabase.getData(cmd);
            //    dataGridViewXChiTiet.DataSource = dt;
            //}
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                IdKey = dataGridViewX1.Rows[e.RowIndex].Cells["SoHoaDonNhap"].Value.ToString();
                rownumber = e.RowIndex;
                SqlCommand cmd = new SqlCommand(@"Select c.TenKho,a.IdPhuTung,a.SoLuong,DonGiaNhap, a.SoLuong * DonGiaNhap As ThanhTien From ChiTietNhapPhuTung a
                                                inner join PhuTung b on b.IdPT = a.IdPhuTung
                                                inner join KhoHang c on b.IdKho = c.IdKho Where IDKey = @IdKey");
                cmd.Parameters.AddWithValue("@IDKey", IdKey);
                dt = Class.datatabase.getData(cmd);
                dataGridViewXChiTiet.DataSource = dt;
            }
        }

        private void btnItemPrint_Click(object sender, EventArgs e)
        {
            string idHoaDon = dataGridViewX1.Rows[rownumber].Cells["SoHoaDonNhap"].Value.ToString();
            string idNhaCungCap = Convert.ToString(dataGridViewX1.Rows[rownumber].Cells["NhaCungCap"].Value);

            FrmPhieuNhapKho frmPhieu = new FrmPhieuNhapKho(idNhaCungCap, idHoaDon);
            frmPhieu.ShowDialog();
        }
    }
}
