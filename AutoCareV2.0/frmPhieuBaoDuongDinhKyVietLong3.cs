using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmPhieuBaoDuongDinhKyVietLong3 : Form
    {
        public string idBaoDuongTamThoi = "";
        public string lichbd = "";

        DataTable datagiamtru = new DataTable();
        public frmPhieuBaoDuongDinhKyVietLong3()
        {
            InitializeComponent();
            if (lichbd.Equals("lsbd"))
            {
                button1.Enabled = false;
                button2.Enabled = false;
                textBoxX1.Enabled = false;
            }
        }

        private void FrmPhieuBaoDuongDinhKyVietLong3_Load(object sender, EventArgs e)
        {
            if (Class.EmployeeInfo.UserName == "vietlong2kho"
                || Class.EmployeeInfo.UserName == "vietlong3kho"
                || Class.EmployeeInfo.UserName == "vietlong1kho")
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
            DataTable dataphutungdinhky = new DataTable();
            SqlCommand cmd = new SqlCommand();

            if (lichbd.Equals("lsbd"))
            {
                cmd.CommandText = @"select TenKH, DienThoai, DiaChi from dbo.ThongTinNguoiDiBaoDuong where IdBaoDuong = @IdBaoDuong";
            }
            else
            {
                cmd.CommandText = @"select TenKH, DienThoai, DiaChi from dbo.ThongTinNguoiDiBaoDuong where IdBaoDuongTam = @IdBaoDuong";
            }

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", int.Parse(idBaoDuongTamThoi));
            DataTable dtNguoiBD = Class.datatabase.getData(cmd);

            if (lichbd.Equals("lsbd"))
            {
                cmd.CommandText = @"select NgayBaoDuong, IdKhachHang, TenXe, BienSo, SoKhung, SoMay, SoLan, SoKm, GIOVAOXE, TGDUKIEN, YeuCauKH, TuVanSuaChua from dbo.LichSuBaoDuongXe where IdBaoDuong = @IdBaoDuong";
            }
            else
            {
                cmd.CommandText = @"select NgayBaoDuong, IdKhachHang, TenXe, BienSo, SoKhung, SoMay, SoLan, SoKm, GIOVAOXE, TGDUKIEN, YeuCauKH, TuVanSuaChua from dbo.LichSuBaoDuongXeTam where IdBaoDuong = @IdBaoDuong";
            }

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", int.Parse(idBaoDuongTamThoi));
            dataphutungdinhky = Class.datatabase.getData(cmd);

            if (dataphutungdinhky.Rows.Count <= 0)
            {
                MessageBox.Show("Phiếu không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string ngayGioBD = dataphutungdinhky.Rows[0]["NgayBaoDuong"].ToString();

            string[] gioBaoDuongTam = ngayGioBD.Split(' ');
            string[] ngayBaoDuongTam = gioBaoDuongTam[0].Split('/');
            dataphutungdinhky.Columns.Remove("NgayBaoDuong");
            dataphutungdinhky.Columns.Add("NgayBaoDuong");
            dataphutungdinhky.Columns.Add("TenKH");
            dataphutungdinhky.Columns.Add("DienThoai");
            dataphutungdinhky.Columns.Add("DiaChi");
            dataphutungdinhky.Columns.Add("day_NgayBaoDuong");
            dataphutungdinhky.Columns.Add("month_NgayBaoDuong");
            dataphutungdinhky.Columns.Add("year_NgayBaoDuong");
            dataphutungdinhky.Columns.Add("day_NgayMua");
            dataphutungdinhky.Columns.Add("month_NgayMua");
            dataphutungdinhky.Columns.Add("year_NgayMua");

            cmd.CommandText = @"select TenKH, DienThoai, DiaChi, NgayMua from dbo.KhachHang where IdKhachHang=@idkhachhang and IdCongTy=@IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idkhachhang", Int64.Parse(dataphutungdinhky.Rows[0]["IdKhachHang"].ToString()));
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            DataTable dtKH = new DataTable();
            dtKH = Class.datatabase.getData(cmd);

            if (dtNguoiBD.Rows.Count > 0)
            {
                dataphutungdinhky.Rows[0]["TenKH"] = dtNguoiBD.Rows[0]["TenKH"];
                dataphutungdinhky.Rows[0]["DienThoai"] = dtNguoiBD.Rows[0]["DienThoai"];
                dataphutungdinhky.Rows[0]["DiaChi"] = dtNguoiBD.Rows[0]["DiaChi"];
            }
            else
            {
                if (dtKH.Rows.Count > 0)
                {
                    dataphutungdinhky.Rows[0]["DienThoai"] = dtKH.Rows[0]["DienThoai"];
                    dataphutungdinhky.Rows[0]["TenKH"] = dtKH.Rows[0]["TenKH"];
                    dataphutungdinhky.Rows[0]["DiaChi"] = dtKH.Rows[0]["DiaChi"];
                }
                else
                {
                    MessageBox.Show("Không thể lấy thông tin khách hàng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (String.IsNullOrEmpty(dtKH.Rows[0]["NgayMua"].ToString()))
            {
                dataphutungdinhky.Rows[0]["day_NgayMua"] = " ";
                dataphutungdinhky.Rows[0]["month_NgayMua"] = " ";
                dataphutungdinhky.Rows[0]["year_NgayMua"] = " ";
            }
            else
            {
                string[] ngayGioMuaXe = dtKH.Rows[0]["NgayMua"].ToString().Split(' ');
                string[] ngayMuaXe = ngayGioMuaXe[0].Split('/');
                dataphutungdinhky.Rows[0]["day_NgayMua"] = ngayMuaXe[1];
                dataphutungdinhky.Rows[0]["month_NgayMua"] = ngayMuaXe[0];
                dataphutungdinhky.Rows[0]["year_NgayMua"] = ngayMuaXe[2];
            }

            dataphutungdinhky.Rows[0]["NgayBaoDuong"] = gioBaoDuongTam[1] + " " + gioBaoDuongTam[2];
            dataphutungdinhky.Rows[0]["day_NgayBaoDuong"] = ngayBaoDuongTam[1];
            dataphutungdinhky.Rows[0]["month_NgayBaoDuong"] = ngayBaoDuongTam[0];
            dataphutungdinhky.Rows[0]["year_NgayBaoDuong"] = ngayBaoDuongTam[2];

            textBoxX1.Text = dataphutungdinhky.Rows[0]["YeuCauKH"].ToString();
            string[] yeuCauKH = dataphutungdinhky.Rows[0]["YeuCauKH"].ToString().Split('\n');
            int yeuCauMember = yeuCauKH.Length;
            string yeuCau1 = "";
            string yeuCau2 = "";
            string yeuCau3 = "";
            switch (yeuCauMember)
            {
                case 1:
                    if (String.IsNullOrEmpty(yeuCauKH[0]))
                    {
                        yeuCau1 = " ";
                    }
                    else
                    {
                        yeuCau1 = yeuCauKH[0];
                    }
                    yeuCau2 = " ";
                    yeuCau3 = " ";
                    break;
                case 2:
                    yeuCau1 = yeuCauKH[0];
                    yeuCau2 = yeuCauKH[1];
                    yeuCau3 = " ";
                    break;
                case 3:
                    yeuCau1 = yeuCauKH[0];
                    yeuCau2 = yeuCauKH[1];
                    yeuCau3 = yeuCauKH[2];
                    break;
                default:
                    yeuCau1 = " ";
                    yeuCau2 = " ";
                    yeuCau3 = " ";
                    break;
            }
            ReportParameter km1 = new ReportParameter();
            ReportParameter km2 = new ReportParameter();
            ReportParameter km3 = new ReportParameter();
            ReportParameter km4 = new ReportParameter();
            ReportParameter km5 = new ReportParameter();
            string soKm = dataphutungdinhky.Rows[0]["SoKm"].ToString();
            switch (soKm.Length)
            {
                case 1:
                    km1 = new ReportParameter("Km1", 0.ToString());
                    km2 = new ReportParameter("Km2", 0.ToString());
                    km3 = new ReportParameter("Km3", 0.ToString());
                    km4 = new ReportParameter("Km4", 0.ToString());
                    km5 = new ReportParameter("Km5", soKm[0].ToString());
                    break;
                case 2:
                    km1 = new ReportParameter("Km1", 0.ToString());
                    km2 = new ReportParameter("Km2", 0.ToString());
                    km3 = new ReportParameter("Km3", 0.ToString());
                    km4 = new ReportParameter("Km4", soKm[0].ToString());
                    km5 = new ReportParameter("Km5", soKm[1].ToString());
                    break;
                case 3:
                    km1 = new ReportParameter("Km1", 0.ToString());
                    km2 = new ReportParameter("Km2", 0.ToString());
                    km3 = new ReportParameter("Km3", soKm[0].ToString());
                    km4 = new ReportParameter("Km4", soKm[1].ToString());
                    km5 = new ReportParameter("Km5", soKm[2].ToString());
                    break;
                case 4:
                    km1 = new ReportParameter("Km1", 0.ToString());
                    km2 = new ReportParameter("Km2", soKm[0].ToString());
                    km3 = new ReportParameter("Km3", soKm[1].ToString());
                    km4 = new ReportParameter("Km4", soKm[2].ToString());
                    km5 = new ReportParameter("Km5", soKm[3].ToString());
                    break;
                case 5:
                    km1 = new ReportParameter("Km1", soKm[0].ToString());
                    km2 = new ReportParameter("Km2", soKm[1].ToString());
                    km3 = new ReportParameter("Km3", soKm[2].ToString());
                    km4 = new ReportParameter("Km4", soKm[3].ToString());
                    km5 = new ReportParameter("Km5", soKm[4].ToString());
                    break;
            }

            Microsoft.Reporting.WinForms.ReportParameter[] rPrams = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("SoPhieu",dataphutungdinhky.Rows[0]["SoLan"].ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("ThoSuaChua", "ANC"),
                new Microsoft.Reporting.WinForms.ReportParameter("TienTrietKhau", "10000"),
                new Microsoft.Reporting.WinForms.ReportParameter("YeuCauKH1",yeuCau1),
                new Microsoft.Reporting.WinForms.ReportParameter("YeuCauKH2", yeuCau2),
                new Microsoft.Reporting.WinForms.ReportParameter("YeuCauKH3", yeuCau3),
            };
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSetGiamTru";
            reportDataSource.Value = datagiamtru;

            ReportDataSource reportDataSource2 = new ReportDataSource();
            reportDataSource2.Name = "DataSetPhuTungDinhKy";
            reportDataSource2.Value = dataphutungdinhky;

            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.SetParameters(rPrams);
            reportViewer1.LocalReport.SetParameters(km1);
            reportViewer1.LocalReport.SetParameters(km2);
            reportViewer1.LocalReport.SetParameters(km3);
            reportViewer1.LocalReport.SetParameters(km4);
            reportViewer1.LocalReport.SetParameters(km5);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            if (Class.EmployeeInfo.UserName == "vietlong2kho"
                || Class.EmployeeInfo.UserName == "vietlong3kho"
                || Class.EmployeeInfo.UserName == "vietlong1kho")
            {
                button1.Enabled = false;
                button2.Enabled = false;
            }
            DataTable dataphutungdinhky = new DataTable();
            SqlCommand cmd = new SqlCommand();

            if (lichbd.Equals("lsbd"))
            {
                cmd.CommandText = @"select TenKH, DienThoai, DiaChi from dbo.ThongTinNguoiDiBaoDuong where IdBaoDuong = @IdBaoDuong";
            }
            else
            {
                cmd.CommandText = @"select TenKH, DienThoai, DiaChi from dbo.ThongTinNguoiDiBaoDuong where IdBaoDuongTam = @IdBaoDuong";
            }

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", int.Parse(idBaoDuongTamThoi));
            DataTable dtNguoiBD = Class.datatabase.getData(cmd);

            if (lichbd.Equals("lsbd"))
            {
                cmd.CommandText = @"select NgayBaoDuong, IdKhachHang, TenXe, BienSo, SoKhung, SoMay, SoLan, SoKm, GIOVAOXE, TGDUKIEN, YeuCauKH, TuVanSuaChua from dbo.LichSuBaoDuongXe where IdBaoDuong = @IdBaoDuong";
            }
            else
            {
                cmd.CommandText = @"select NgayBaoDuong, IdKhachHang, TenXe, BienSo, SoKhung, SoMay, SoLan, SoKm, GIOVAOXE, TGDUKIEN, YeuCauKH, TuVanSuaChua from dbo.LichSuBaoDuongXeTam where IdBaoDuong = @IdBaoDuong";
            }

            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", int.Parse(idBaoDuongTamThoi));
            dataphutungdinhky = Class.datatabase.getData(cmd);

            if (dataphutungdinhky.Rows.Count <= 0)
            {
                MessageBox.Show("Phiếu không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            string ngayGioBD = dataphutungdinhky.Rows[0]["NgayBaoDuong"].ToString();

            string[] gioBaoDuongTam = ngayGioBD.Split(' ');
            string[] ngayBaoDuongTam = gioBaoDuongTam[0].Split('/');
            dataphutungdinhky.Columns.Remove("NgayBaoDuong");
            dataphutungdinhky.Columns.Add("NgayBaoDuong");
            dataphutungdinhky.Columns.Add("TenKH");
            dataphutungdinhky.Columns.Add("DienThoai");
            dataphutungdinhky.Columns.Add("DiaChi");
            dataphutungdinhky.Columns.Add("day_NgayBaoDuong");
            dataphutungdinhky.Columns.Add("month_NgayBaoDuong");
            dataphutungdinhky.Columns.Add("year_NgayBaoDuong");
            dataphutungdinhky.Columns.Add("day_NgayMua");
            dataphutungdinhky.Columns.Add("month_NgayMua");
            dataphutungdinhky.Columns.Add("year_NgayMua");

            cmd.CommandText = @"select TenKH, DienThoai, DiaChi, NgayMua from dbo.KhachHang where IdKhachHang=@idkhachhang and IdCongTy=@IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idkhachhang", Int64.Parse(dataphutungdinhky.Rows[0]["IdKhachHang"].ToString()));
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            DataTable dtKH = new DataTable();
            dtKH = Class.datatabase.getData(cmd);

            if (dtNguoiBD.Rows.Count > 0)
            {
                dataphutungdinhky.Rows[0]["TenKH"] = dtNguoiBD.Rows[0]["TenKH"];
                dataphutungdinhky.Rows[0]["DienThoai"] = dtNguoiBD.Rows[0]["DienThoai"];
                dataphutungdinhky.Rows[0]["DiaChi"] = dtNguoiBD.Rows[0]["DiaChi"];
            }
            else
            {
                if (dtKH.Rows.Count > 0)
                {
                    dataphutungdinhky.Rows[0]["DienThoai"] = dtKH.Rows[0]["DienThoai"];
                    dataphutungdinhky.Rows[0]["TenKH"] = dtKH.Rows[0]["TenKH"];
                    dataphutungdinhky.Rows[0]["DiaChi"] = dtKH.Rows[0]["DiaChi"];
                }
                else
                {
                    MessageBox.Show("Không thể lấy thông tin khách hàng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            if (String.IsNullOrEmpty(dtKH.Rows[0]["NgayMua"].ToString()))
            {
                dataphutungdinhky.Rows[0]["day_NgayMua"] = " ";
                dataphutungdinhky.Rows[0]["month_NgayMua"] = " ";
                dataphutungdinhky.Rows[0]["year_NgayMua"] = " ";
            }
            else
            {
                string[] ngayGioMuaXe = dtKH.Rows[0]["NgayMua"].ToString().Split(' ');
                string[] ngayMuaXe = ngayGioMuaXe[0].Split('/');
                dataphutungdinhky.Rows[0]["day_NgayMua"] = ngayMuaXe[1];
                dataphutungdinhky.Rows[0]["month_NgayMua"] = ngayMuaXe[0];
                dataphutungdinhky.Rows[0]["year_NgayMua"] = ngayMuaXe[2];
            }

            dataphutungdinhky.Rows[0]["NgayBaoDuong"] = gioBaoDuongTam[1] + " " + gioBaoDuongTam[2];
            dataphutungdinhky.Rows[0]["day_NgayBaoDuong"] = ngayBaoDuongTam[1];
            dataphutungdinhky.Rows[0]["month_NgayBaoDuong"] = ngayBaoDuongTam[0];
            dataphutungdinhky.Rows[0]["year_NgayBaoDuong"] = ngayBaoDuongTam[2];

            textBoxX1.Text = dataphutungdinhky.Rows[0]["YeuCauKH"].ToString();
            string[] yeuCauKH = dataphutungdinhky.Rows[0]["YeuCauKH"].ToString().Split('\n');
            int yeuCauMember = yeuCauKH.Length;
            string yeuCau1 = "";
            string yeuCau2 = "";
            string yeuCau3 = "";
            switch (yeuCauMember)
            {
                case 1:
                    if (String.IsNullOrEmpty(yeuCauKH[0]))
                    {
                        yeuCau1 = " ";
                    }
                    else
                    {
                        yeuCau1 = yeuCauKH[0];
                    }
                    yeuCau2 = " ";
                    yeuCau3 = " ";
                    break;
                case 2:
                    yeuCau1 = yeuCauKH[0];
                    yeuCau2 = yeuCauKH[1];
                    yeuCau3 = " ";
                    break;
                case 3:
                    yeuCau1 = yeuCauKH[0];
                    yeuCau2 = yeuCauKH[1];
                    yeuCau3 = yeuCauKH[2];
                    break;
                default:
                    yeuCau1 = " ";
                    yeuCau2 = " ";
                    yeuCau3 = " ";
                    break;
            }
            ReportParameter km1 = new ReportParameter();
            ReportParameter km2 = new ReportParameter();
            ReportParameter km3 = new ReportParameter();
            ReportParameter km4 = new ReportParameter();
            ReportParameter km5 = new ReportParameter();
            string soKm = dataphutungdinhky.Rows[0]["SoKm"].ToString();
            switch (soKm.Length)
            {
                case 1:
                    km1 = new ReportParameter("Km1", 0.ToString());
                    km2 = new ReportParameter("Km2", 0.ToString());
                    km3 = new ReportParameter("Km3", 0.ToString());
                    km4 = new ReportParameter("Km4", 0.ToString());
                    km5 = new ReportParameter("Km5", soKm[0].ToString());
                    break;
                case 2:
                    km1 = new ReportParameter("Km1", 0.ToString());
                    km2 = new ReportParameter("Km2", 0.ToString());
                    km3 = new ReportParameter("Km3", 0.ToString());
                    km4 = new ReportParameter("Km4", soKm[0].ToString());
                    km5 = new ReportParameter("Km5", soKm[1].ToString());
                    break;
                case 3:
                    km1 = new ReportParameter("Km1", 0.ToString());
                    km2 = new ReportParameter("Km2", 0.ToString());
                    km3 = new ReportParameter("Km3", soKm[0].ToString());
                    km4 = new ReportParameter("Km4", soKm[1].ToString());
                    km5 = new ReportParameter("Km5", soKm[2].ToString());
                    break;
                case 4:
                    km1 = new ReportParameter("Km1", 0.ToString());
                    km2 = new ReportParameter("Km2", soKm[0].ToString());
                    km3 = new ReportParameter("Km3", soKm[1].ToString());
                    km4 = new ReportParameter("Km4", soKm[2].ToString());
                    km5 = new ReportParameter("Km5", soKm[3].ToString());
                    break;
                case 5:
                    km1 = new ReportParameter("Km1", soKm[0].ToString());
                    km2 = new ReportParameter("Km2", soKm[1].ToString());
                    km3 = new ReportParameter("Km3", soKm[2].ToString());
                    km4 = new ReportParameter("Km4", soKm[3].ToString());
                    km5 = new ReportParameter("Km5", soKm[4].ToString());
                    break;
            }

            Microsoft.Reporting.WinForms.ReportParameter[] rPrams = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                new Microsoft.Reporting.WinForms.ReportParameter("SoPhieu",dataphutungdinhky.Rows[0]["SoLan"].ToString()),
                new Microsoft.Reporting.WinForms.ReportParameter("ThoSuaChua", "ANC"),
                new Microsoft.Reporting.WinForms.ReportParameter("TienTrietKhau", "10000"),
                new Microsoft.Reporting.WinForms.ReportParameter("YeuCauKH1",yeuCau1),
                new Microsoft.Reporting.WinForms.ReportParameter("YeuCauKH2", yeuCau2),
                new Microsoft.Reporting.WinForms.ReportParameter("YeuCauKH3", yeuCau3),
            };
            ReportDataSource reportDataSource = new ReportDataSource();
            reportDataSource.Name = "DataSetGiamTru";
            reportDataSource.Value = datagiamtru;

            ReportDataSource reportDataSource2 = new ReportDataSource();
            reportDataSource2.Name = "DataSetPhuTungDinhKy";
            reportDataSource2.Value = dataphutungdinhky;

            reportViewer1.LocalReport.DataSources.Clear();

            reportViewer1.LocalReport.SetParameters(rPrams);
            reportViewer1.LocalReport.SetParameters(km1);
            reportViewer1.LocalReport.SetParameters(km2);
            reportViewer1.LocalReport.SetParameters(km3);
            reportViewer1.LocalReport.SetParameters(km4);
            reportViewer1.LocalReport.SetParameters(km5);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            reportViewer1.RefreshReport();
            this.reportViewer1.RefreshReport();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (SqlConnection myCon = new SqlConnection(Class.datatabase.connect))
            {
                if (myCon.State == ConnectionState.Closed)
                {
                    myCon.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myCon;
                SqlTransaction transaction;
                transaction = myCon.BeginTransaction();
                cmd.Transaction = transaction;
                try
                {
                    cmd.CommandText = @"Update dbo.LichSuBaoDuongXeTam Set YeuCauKH = @yeucaukh where IdBaoDuong = @idbaoduong and IdCongTy = @idcongty";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@yeucaukh", textBoxX1.Text);
                    cmd.Parameters.AddWithValue("@idbaoduong", int.Parse(idBaoDuongTamThoi.ToString()));
                    cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                    if (cmd.ExecuteNonQuery() > 0)
                    {
                        MessageBox.Show("Lưu thành công, bấm tải lại để xem", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
                finally { myCon.Close(); }

            }
        }
    }
}
