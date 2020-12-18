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
    public partial class frmPhieuSuaChuaHondaVietLong : Form
    {
        public string idBaoDuongTamThoi = "";
        public string ThoiGianDuKien = "";
        private SqlDataProvider sqlPrv = new SqlDataProvider();
        private SqlConnection con;
        private string cn = Class.datatabase.connect;
        public frmPhieuSuaChuaHondaVietLong()
        {
            InitializeComponent();
        }
        private void connect()
        {
            try
            {
                con = new SqlConnection(cn);
                con.Open();
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối !");
            }
        }
        private void FrmPhieuSuaChuaHondaVietLong_Load(object sender, EventArgs e)
        {
            DataTable dtPhuTung = new DataTable();
            connect();
            SqlCommand cmd = new SqlCommand("sp_Test_Report3", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdBaoDuong", int.Parse(idBaoDuongTamThoi.ToString()));
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtPhuTung);
            if (dtPhuTung.Rows.Count < 1)
            {
                dtPhuTung.Rows.Add();
            }

            DataColumn cl1 = new DataColumn("NoiDungBD");
            DataColumn cl2 = new DataColumn("TienCong");
            cl2.DataType = typeof(decimal);

            dtPhuTung.Columns.Add(cl1);
            dtPhuTung.Columns.Add(cl2);

            int index = 0;

            SqlCommand cdd = new SqlCommand();

            cdd.CommandText = "select * from dbo.LichSuBaoDuongXeTam where IdCongTy=@congty and IdBaoDuong=@baoduong";
            cdd.Parameters.AddWithValue("@baoduong", int.Parse(idBaoDuongTamThoi.ToString()));
            cdd.Parameters.AddWithValue("@congty", Class.CompanyInfo.idcongty);
            DataTable lsBDTam = new DataTable();
            lsBDTam = Class.datatabase.getData(cdd);
            if (lsBDTam.Rows.Count <= 0)
            {
                MessageBox.Show("Phiếu không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            textBoxX1.Text = lsBDTam.Rows[0]["YeuCauKH"].ToString();
            textBoxX2.Text = lsBDTam.Rows[0]["TuVanSuaChua"].ToString();
            dtPhuTung.Rows[0]["BienSo"] = lsBDTam.Rows[0]["BienSo"];
            
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select TenKH, DienThoai, DiaChi from dbo.ThongTinNguoiDiBaoDuong where IdBaoDuongTam=@IdBaoDuong and IdCongTy=@IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", int.Parse(idBaoDuongTamThoi.ToString()));
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            DataTable nguoiDiBaoDuong = new DataTable();
            da.Fill(nguoiDiBaoDuong);

            if (nguoiDiBaoDuong.Rows.Count > 0)
            {
                dtPhuTung.Rows[0]["DienThoai"] = nguoiDiBaoDuong.Rows[0]["DienThoai"];
                dtPhuTung.Rows[0]["TenKH"] = nguoiDiBaoDuong.Rows[0]["TenKH"];
                dtPhuTung.Rows[0]["DiaChi"] = nguoiDiBaoDuong.Rows[0]["DiaChi"];
            }
            else
            {
                cmd.CommandText = @"select TenKH, DienThoai, DiaChi from dbo.KhachHang where IdKhachHang=@idkhachhang and IdCongTy=@IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idkhachhang", Int64.Parse(lsBDTam.Rows[0]["IdKhachHang"].ToString()));
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                nguoiDiBaoDuong = new DataTable();
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(nguoiDiBaoDuong);
                if (nguoiDiBaoDuong.Rows.Count > 0)
                {
                    dtPhuTung.Rows[0]["DienThoai"] = nguoiDiBaoDuong.Rows[0]["DienThoai"];
                    dtPhuTung.Rows[0]["TenKH"] = nguoiDiBaoDuong.Rows[0]["TenKH"];
                    dtPhuTung.Rows[0]["DiaChi"] = nguoiDiBaoDuong.Rows[0]["DiaChi"];
                }
            }
            DataTable dttiengiam = new DataTable();

            DataTable dtTien = new DataTable();
            DataRow dr;

            //them thong tin
            DataTable dtthongtin = new DataTable();
            SqlCommand cmo = new SqlCommand();
            cmo.CommandText = @"select DiaChi as diachi,DienThoai as didong,DienThoaiBan as dtban,TenLapPhieu as
            lapphieu,TenQuanLy as quanly,TenCongTy as tencongty from congty where IdCongTy=@idct";

            cmo.Parameters.Clear();

            cmo.Parameters.AddWithValue("@idct", Class.CompanyInfo.idcongty);
            dtthongtin = Class.datatabase.getData(cmo);

            DataTable dtngaygiaoxe = new DataTable();
            SqlCommand cmdngaygiaoxe = new SqlCommand();

            cmdngaygiaoxe.CommandText = "SELECT * FROM LichSuBaoDuongXeTam WHERE IdBaoDuong = @IdBaoDuong";
            cmdngaygiaoxe.Parameters.Clear();
            cmdngaygiaoxe.Parameters.AddWithValue("@IdBaoDuong", int.Parse(idBaoDuongTamThoi.ToString()));
            dtngaygiaoxe = Class.datatabase.getData(cmdngaygiaoxe);


            if (dtngaygiaoxe.Rows.Count <= 0)
            {
                dtngaygiaoxe.Rows.Add();
            }
            string[] ngaymua = null; 
            DataTable dtXeDaBan = new DataTable();
            SqlCommand command = new SqlCommand();
            command.CommandText = "select NgayBan FROM XeDaBan where IdCongty = @idcongty and IdKhachHang = @idkhachhang";
            command.Parameters.Clear();
            command.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            command.Parameters.AddWithValue("@idkhachhang", int.Parse(dtngaygiaoxe.Rows[0]["IdKhachHang"].ToString().Trim()));
            dtXeDaBan = Class.datatabase.getData(command);
            if (dtXeDaBan.Rows.Count > 0)
            {
                ngaymua = dtXeDaBan.Rows[0]["NgayBan"].ToString().Split(' ');
            }
            else
            {
                DataTable dtngayban = new DataTable();
                SqlCommand cnb = new SqlCommand();
                cnb.CommandText = @"select NgayMua from dbo.KhachHang where IdCongty = @idcongty and IdKhachHang = @idkhachhang";
                cnb.Parameters.Clear();
                cnb.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                cnb.Parameters.AddWithValue("@idkhachhang", int.Parse(dtngaygiaoxe.Rows[0]["IdKhachHang"].ToString().Trim()));
                dtngayban = Class.datatabase.getData(cnb);

                if (dtngayban.Rows.Count <= 0)
                {
                    dtngayban.Rows.Add();
                }
                ngaymua = dtngayban.Rows[0]["NgayMua"].ToString().Split(' ');
            }
            //DateTime dtNgayMua = DateTime.Parse(dtngayban.Rows[0]["NgayBan"].ToString());
            string strNgayMua = string.Empty;
            if (ngaymua.Length>1)
            {
                DateTime dtNgayMua = DateTime.Parse(ngaymua[0].ToString());
                strNgayMua = dtNgayMua.ToString("dd/MM/yyyy");
            }
            //dtPhuTung.Rows[0]["NgayMua"] =ngaymua[0];
            dtPhuTung.Rows[0]["NgayMua"] = strNgayMua;
            dtPhuTung.Rows[0]["SoKhung"] = dtngaygiaoxe.Rows[0]["SoKhung"].ToString();
            dtPhuTung.Rows[0]["SoMay"] = dtngaygiaoxe.Rows[0]["SoMay"].ToString();
            dtPhuTung.Rows[0]["SoKm"] = dtngaygiaoxe.Rows[0]["SoKm"].ToString();
            dtPhuTung.Rows[0]["TenXe"] = dtngaygiaoxe.Rows[0]["TenXe"].ToString();

            DataColumn ColumnNgayGiaoXe = dtthongtin.Columns.Add("ngay", typeof(String));
            DataColumn ColumnGioGiaoXe = dtthongtin.Columns.Add("GioGiaoXe", typeof(String));
            DataColumn ColumnKyThuatVien = dtthongtin.Columns.Add("KyThuatVien", typeof(String));

            dtthongtin.Rows[0]["ngay"] = "06";
            dtthongtin.Rows[0]["GioGiaoXe"] = "06";
            dtthongtin.Rows[0]["KyThuatVien"] = "ANC";

            string ngayBaoDuongTam = dtngaygiaoxe.Rows[0]["NgayBaoDuong"].ToString();
            string[] thoigianbaoduong = ngayBaoDuongTam.Split(' ');
            string[] ngayBD = thoigianbaoduong[0].Split('/');

            dtPhuTung.Columns.Add("day_NgayBaoDuong", typeof(string));
            dtPhuTung.Columns.Add("month_NgayBaoDuong", typeof(string));
            dtPhuTung.Columns.Add("year_NgayBaoDuong", typeof(string));


            dtPhuTung.Rows[0]["day_NgayBaoDuong"] = ngayBD[1];
            dtPhuTung.Rows[0]["month_NgayBaoDuong"] = ngayBD[0];
            dtPhuTung.Rows[0]["year_NgayBaoDuong"] = ngayBD[2];



            for (int k = 0; k < 15; k++)
            {
                dtPhuTung.Rows.Add();
            }

            ReportDataSource reportGiamTru = new ReportDataSource();
            reportGiamTru.Name = "DataSetGiamTru";
            reportGiamTru.Value = dttiengiam;

            ReportDataSource reportDataPhuTung = new ReportDataSource();
            reportDataPhuTung.Name = "DataSetPhuTung";
            reportDataPhuTung.Value = dtPhuTung;

            ReportDataSource reportDataDocTien = new ReportDataSource();
            reportDataDocTien.Name = "DataSetTongTien";
            reportDataDocTien.Value = dtTien;

            ReportDataSource reportDataCongty = new ReportDataSource();
            reportDataCongty.Name = "DataSetThongTin";
            reportDataCongty.Value = dtthongtin;

            string[] tuVanKH = lsBDTam.Rows[0]["TuVanSuaChua"].ToString().Split('\n');
            int tuVanMember = tuVanKH.Length;
            string tuVan1 = "";
            string tuVan2 = "";
            string tuVan3 = "";
            switch (tuVanMember)
            {
                case 1:
                    tuVan1 = tuVanKH[0];
                    tuVan2 = "";
                    tuVan3 = "";
                    break;
                case 2:
                    tuVan1 = tuVanKH[0];
                    tuVan2 = tuVanKH[1];
                    tuVan3 = "";
                    break;
                case 3:
                    tuVan1 = tuVanKH[0];
                    tuVan2 = tuVanKH[1];
                    tuVan3 = tuVanKH[2];
                    break;
                default:
                    tuVan1 = "";
                    tuVan2 = "";
                    tuVan3 = "";
                    break;
            }

            string[] yeuCauKH = lsBDTam.Rows[0]["YeuCauKH"].ToString().Split('\n');
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

            string[] gioPhutGiayBD = thoigianbaoduong[1].Split(':');
            DateTime dt = new DateTime(int.Parse(ngayBD[2]), int.Parse(ngayBD[0]), int.Parse(ngayBD[1]), int.Parse(gioPhutGiayBD[0]), int.Parse(gioPhutGiayBD[1]), int.Parse(gioPhutGiayBD[2]), 66);
            string ngayBaoDuong = String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + thoigianbaoduong[2].ToString();
            string datetesst = dt.ToString("dd/MM/yyyy");
            string timeDuKien = "0";
            if (String.IsNullOrEmpty(lsBDTam.Rows[0]["TGDUKIEN"].ToString().Trim()))
            {
                timeDuKien = XuLyThoiGianDuKien(int.Parse(ngayBD[1]), int.Parse(ngayBD[0]), int.Parse(ngayBD[2]), int.Parse(gioPhutGiayBD[0]), int.Parse(gioPhutGiayBD[1]), 10, thoigianbaoduong[2].ToString());
            }
            else
            {
                timeDuKien = XuLyThoiGianDuKien(int.Parse(ngayBD[1]), int.Parse(ngayBD[0]), int.Parse(ngayBD[2]), int.Parse(gioPhutGiayBD[0]), int.Parse(gioPhutGiayBD[1]), int.Parse(lsBDTam.Rows[0]["TGDUKIEN"].ToString().Trim()), thoigianbaoduong[2].ToString());
            }
            

            string tongTienCongTho = "0";
            ReportParameter reportTongTienCongThoTT = new ReportParameter("TongTienCongThoThanhToan", tongTienCongTho);
            string tongTienPhuTung = "0";
            ReportParameter reportTongTienPhuTungTT = new ReportParameter("TongTienPhuTungThanhToan", tongTienPhuTung);
            string soTien = "0";
            ReportParameter reportTongTien = new ReportParameter("TongTien", soTien);
            ReportParameter reportSoPhieu = new ReportParameter("SoPhieu", idBaoDuongTamThoi.ToString());
            ReportParameter reportTho = new ReportParameter("ThoSuaChua", " ");
            ReportParameter reportTrietKhau = new ReportParameter("TienTrietKhau", "0");
            ReportParameter reportTuVanKH1 = new ReportParameter("TuVanKH1", tuVan1);
            ReportParameter reportTuVanKH2 = new ReportParameter("TuVanKH2", tuVan2);
            ReportParameter reportTuVanKH3 = new ReportParameter("TuVanKH3", tuVan3);
            ReportParameter reportTGDK = new ReportParameter("ThoiGianDuKien", timeDuKien);
            ReportParameter reportYeuCauKH1 = new ReportParameter("YeuCauKH1", yeuCau1);
            ReportParameter reportYeuCauKH2 = new ReportParameter("YeuCauKH2", yeuCau2);
            ReportParameter reportYeuCauKH3 = new ReportParameter("YeuCauKH3", yeuCau3);
            ReportParameter reportNgayBaoDuong = new ReportParameter("NgayBaoDuong", ngayBaoDuong);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.SetParameters(reportSoPhieu);
            reportViewer1.LocalReport.SetParameters(reportTho);
            reportViewer1.LocalReport.SetParameters(reportTrietKhau);
            reportViewer1.LocalReport.SetParameters(reportTuVanKH1);
            reportViewer1.LocalReport.SetParameters(reportTuVanKH2);
            reportViewer1.LocalReport.SetParameters(reportTuVanKH3);
            reportViewer1.LocalReport.SetParameters(reportTGDK);
            reportViewer1.LocalReport.SetParameters(reportYeuCauKH1);
            reportViewer1.LocalReport.SetParameters(reportYeuCauKH2);
            reportViewer1.LocalReport.SetParameters(reportYeuCauKH3);
            reportViewer1.LocalReport.SetParameters(reportTongTienPhuTungTT);
            reportViewer1.LocalReport.SetParameters(reportTongTienCongThoTT);
            reportViewer1.LocalReport.SetParameters(reportTongTien);
            reportViewer1.LocalReport.SetParameters(reportNgayBaoDuong);
            reportViewer1.LocalReport.DataSources.Add(reportGiamTru);
            reportViewer1.LocalReport.DataSources.Add(reportDataPhuTung);
            reportViewer1.LocalReport.DataSources.Add(reportDataDocTien);
            reportViewer1.LocalReport.DataSources.Add(reportDataCongty);
            reportViewer1.LocalReport.Refresh();

            this.reportViewer1.RefreshReport();
        }

        private void ButtonX1_Click(object sender, EventArgs e)
        {
            if (Class.EmployeeInfo.UserName == "vietlong2kho"
                 || Class.EmployeeInfo.UserName == "vietlong3kho"
                 || Class.EmployeeInfo.UserName == "vietlong1kho")
            {
                btnLuuLai.Enabled = false;
                buttonX1.Enabled = false;
            }
            DataTable dtPhuTung = new DataTable();
            connect();

            SqlCommand cmd = new SqlCommand("sp_Test_Report3", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdBaoDuong", int.Parse(idBaoDuongTamThoi.ToString()));
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtPhuTung);
            if (dtPhuTung.Rows.Count < 1)
            {
                dtPhuTung.Rows.Add();
            }

            DataColumn cl1 = new DataColumn("NoiDungBD");
            DataColumn cl2 = new DataColumn("TienCong");
            cl2.DataType = typeof(decimal);

            dtPhuTung.Columns.Add(cl1);
            dtPhuTung.Columns.Add(cl2);

            int index = 0;

            SqlCommand cdd = new SqlCommand();
            cdd.CommandText = "select * from dbo.LichSuBaoDuongXeTam where IdCongTy=@congty and IdBaoDuong=@baoduong";
            cdd.Parameters.AddWithValue("@baoduong", int.Parse(idBaoDuongTamThoi.ToString()));
            cdd.Parameters.AddWithValue("@congty", Class.CompanyInfo.idcongty);
            DataTable lsBDTam = new DataTable();
            lsBDTam = Class.datatabase.getData(cdd);
            if (lsBDTam.Rows.Count <= 0)
            {
                MessageBox.Show("Phiếu không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            textBoxX1.Text = lsBDTam.Rows[0]["YeuCauKH"].ToString();
            textBoxX2.Text = lsBDTam.Rows[0]["TuVanSuaChua"].ToString();
            dtPhuTung.Rows[0]["BienSo"] = lsBDTam.Rows[0]["BienSo"];

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select * from dbo.ThongTinNguoiDiBaoDuong where IdBaoDuongTam=@IdBaoDuong and IdCongTy=@IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", int.Parse(idBaoDuongTamThoi.ToString()));
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            DataTable nguoiDiBaoDuong = new DataTable();
            da.Fill(nguoiDiBaoDuong);

            if (nguoiDiBaoDuong.Rows.Count > 0)
            {
                dtPhuTung.Rows[0]["DienThoai"] = nguoiDiBaoDuong.Rows[0]["DienThoai"];
                dtPhuTung.Rows[0]["TenKH"] = nguoiDiBaoDuong.Rows[0]["TenKH"];
                dtPhuTung.Rows[0]["DiaChi"] = nguoiDiBaoDuong.Rows[0]["DiaChi"];
            }
            else
            {
                cmd.CommandText = @"select * from dbo.KhachHang where IdKhachHang=@idkhachhang and IdCongTy=@IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idkhachhang", Int64.Parse(lsBDTam.Rows[0]["IdKhachHang"].ToString()));
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                nguoiDiBaoDuong = new DataTable();
                SqlDataAdapter adt = new SqlDataAdapter(cmd);
                adt.Fill(nguoiDiBaoDuong);
                if (nguoiDiBaoDuong.Rows.Count > 0)
                {
                    dtPhuTung.Rows[0]["DienThoai"] = nguoiDiBaoDuong.Rows[0]["DienThoai"];
                    dtPhuTung.Rows[0]["TenKH"] = nguoiDiBaoDuong.Rows[0]["TenKH"];
                    dtPhuTung.Rows[0]["DiaChi"] = nguoiDiBaoDuong.Rows[0]["DiaChi"];
                }
            }
            DataTable dttiengiam = new DataTable();

            DataTable dtTien = new DataTable();


            //them thong tin
            DataTable dtthongtin = new DataTable();
            SqlCommand cmo = new SqlCommand();
            cmo.CommandText = @"select DiaChi as diachi,DienThoai as didong,DienThoaiBan as dtban,TenLapPhieu as
            lapphieu,TenQuanLy as quanly,TenCongTy as tencongty from congty where IdCongTy=@idct";

            cmo.Parameters.Clear();

            cmo.Parameters.AddWithValue("@idct", Class.CompanyInfo.idcongty);
            dtthongtin = Class.datatabase.getData(cmo);

            DataTable dtngaygiaoxe = new DataTable();
            SqlCommand cmdngaygiaoxe = new SqlCommand();

            cmdngaygiaoxe.CommandText = "SELECT * FROM LichSuBaoDuongXeTam WHERE IdBaoDuong = @IdBaoDuong";
            cmdngaygiaoxe.Parameters.Clear();
            cmdngaygiaoxe.Parameters.AddWithValue("@IdBaoDuong", int.Parse(idBaoDuongTamThoi.ToString()));
            dtngaygiaoxe = Class.datatabase.getData(cmdngaygiaoxe);


            if (dtngaygiaoxe.Rows.Count <= 0)
            {
                dtngaygiaoxe.Rows.Add();
            }
            DataTable dtngayban = new DataTable();
            SqlCommand cnb = new SqlCommand();
            cnb.CommandText = @"select *from dbo.KhachHang where IdCongty = @idcongty and IdKhachHang = @idkhachhang";
            cnb.Parameters.Clear();
            cnb.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cnb.Parameters.AddWithValue("@idkhachhang", int.Parse(dtngaygiaoxe.Rows[0]["IdKhachHang"].ToString().Trim()));
            dtngayban = Class.datatabase.getData(cnb);

            if (dtngayban.Rows.Count <= 0)
            {
                dtngayban.Rows.Add();
            }
            string[] ngaymua = dtngayban.Rows[0]["NgayMua"].ToString().Split(' ');
            dtPhuTung.Rows[0]["NgayMua"] = ngaymua[0];
            dtPhuTung.Rows[0]["SoKhung"] = dtngaygiaoxe.Rows[0]["SoKhung"].ToString();
            dtPhuTung.Rows[0]["SoMay"] = dtngaygiaoxe.Rows[0]["SoMay"].ToString();
            dtPhuTung.Rows[0]["SoKm"] = dtngaygiaoxe.Rows[0]["SoKm"].ToString();
            dtPhuTung.Rows[0]["TenXe"] = dtngaygiaoxe.Rows[0]["TenXe"].ToString();

            DataColumn ColumnNgayGiaoXe = dtthongtin.Columns.Add("ngay", typeof(String));
            DataColumn ColumnGioGiaoXe = dtthongtin.Columns.Add("GioGiaoXe", typeof(String));
            DataColumn ColumnKyThuatVien = dtthongtin.Columns.Add("KyThuatVien", typeof(String));

            dtthongtin.Rows[0]["ngay"] = "06";
            dtthongtin.Rows[0]["GioGiaoXe"] = "06";
            dtthongtin.Rows[0]["KyThuatVien"] = "ANC";

            string ngayBaoDuongTam = dtngaygiaoxe.Rows[0]["NgayBaoDuong"].ToString();
            string[] thoigianbaoduong = ngayBaoDuongTam.Split(' ');
            string[] ngayBD = thoigianbaoduong[0].Split('/');

            dtPhuTung.Columns.Add("day_NgayBaoDuong", typeof(string));
            dtPhuTung.Columns.Add("month_NgayBaoDuong", typeof(string));
            dtPhuTung.Columns.Add("year_NgayBaoDuong", typeof(string));


            dtPhuTung.Rows[0]["day_NgayBaoDuong"] = ngayBD[1];
            dtPhuTung.Rows[0]["month_NgayBaoDuong"] = ngayBD[0];
            dtPhuTung.Rows[0]["year_NgayBaoDuong"] = ngayBD[2];



            for (int k = 0; k < 15; k++)
            {
                dtPhuTung.Rows.Add();
            }

            ReportDataSource reportGiamTru = new ReportDataSource();
            reportGiamTru.Name = "DataSetGiamTru";
            reportGiamTru.Value = dttiengiam;

            ReportDataSource reportDataPhuTung = new ReportDataSource();
            reportDataPhuTung.Name = "DataSetPhuTung";
            reportDataPhuTung.Value = dtPhuTung;

            ReportDataSource reportDataDocTien = new ReportDataSource();
            reportDataDocTien.Name = "DataSetTongTien";
            reportDataDocTien.Value = dtTien;

            ReportDataSource reportDataCongty = new ReportDataSource();
            reportDataCongty.Name = "DataSetThongTin";
            reportDataCongty.Value = dtthongtin;

            string[] tuVanKH = lsBDTam.Rows[0]["TuVanSuaChua"].ToString().Split('\n');
            int tuVanMember = tuVanKH.Length;
            string tuVan1 = "";
            string tuVan2 = "";
            string tuVan3 = "";
            switch (tuVanMember)
            {
                case 1:
                    tuVan1 = tuVanKH[0];
                    tuVan2 = "";
                    tuVan3 = "";
                    break;
                case 2:
                    tuVan1 = tuVanKH[0];
                    tuVan2 = tuVanKH[1];
                    tuVan3 = "";
                    break;
                case 3:
                    tuVan1 = tuVanKH[0];
                    tuVan2 = tuVanKH[1];
                    tuVan3 = tuVanKH[2];
                    break;
                default:
                    tuVan1 = "";
                    tuVan2 = "";
                    tuVan3 = "";
                    break;
            }

            string[] yeuCauKH = lsBDTam.Rows[0]["YeuCauKH"].ToString().Split('\n');
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

            string[] gioPhutGiayBD = thoigianbaoduong[1].Split(':');
            DateTime dt = new DateTime(int.Parse(ngayBD[2]), int.Parse(ngayBD[0]), int.Parse(ngayBD[1]), int.Parse(gioPhutGiayBD[0]), int.Parse(gioPhutGiayBD[1]), int.Parse(gioPhutGiayBD[2]), 66);
            string ngayBaoDuong = String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + thoigianbaoduong[2].ToString();

            string timeDuKien = "0";
            if (String.IsNullOrEmpty(lsBDTam.Rows[0]["TGDUKIEN"].ToString().Trim()))
            {
                timeDuKien = XuLyThoiGianDuKien(int.Parse(ngayBD[1]), int.Parse(ngayBD[0]), int.Parse(ngayBD[2]), int.Parse(gioPhutGiayBD[0]), int.Parse(gioPhutGiayBD[1]), 10, thoigianbaoduong[2].ToString());
            }
            else
            {
                timeDuKien = XuLyThoiGianDuKien(int.Parse(ngayBD[1]), int.Parse(ngayBD[0]), int.Parse(ngayBD[2]), int.Parse(gioPhutGiayBD[0]), int.Parse(gioPhutGiayBD[1]), int.Parse(lsBDTam.Rows[0]["TGDUKIEN"].ToString().Trim()), thoigianbaoduong[2].ToString());
            }

            string tongTienCongTho = "0";
            ReportParameter reportTongTienCongThoTT = new ReportParameter("TongTienCongThoThanhToan", tongTienCongTho);
            string tongTienPhuTung = "0";
            ReportParameter reportTongTienPhuTungTT = new ReportParameter("TongTienPhuTungThanhToan", tongTienPhuTung);
            string soTien = "0";
            ReportParameter reportTongTien = new ReportParameter("TongTien", soTien);
            ReportParameter reportSoPhieu = new ReportParameter("SoPhieu", idBaoDuongTamThoi.ToString());
            ReportParameter reportTho = new ReportParameter("ThoSuaChua", " ");
            ReportParameter reportTrietKhau = new ReportParameter("TienTrietKhau", "0");
            ReportParameter reportTuVanKH1 = new ReportParameter("TuVanKH1", tuVan1);
            ReportParameter reportTuVanKH2 = new ReportParameter("TuVanKH2", tuVan2);
            ReportParameter reportTuVanKH3 = new ReportParameter("TuVanKH3", tuVan3);
            ReportParameter reportTGDK = new ReportParameter("ThoiGianDuKien", timeDuKien);
            ReportParameter reportYeuCauKH1 = new ReportParameter("YeuCauKH1", yeuCau1);
            ReportParameter reportYeuCauKH2 = new ReportParameter("YeuCauKH2", yeuCau2);
            ReportParameter reportYeuCauKH3 = new ReportParameter("YeuCauKH3", yeuCau3);
            ReportParameter reportNgayBaoDuong = new ReportParameter("NgayBaoDuong", ngayBaoDuong);

            reportViewer1.LocalReport.DataSources.Clear();
            reportViewer1.LocalReport.SetParameters(reportSoPhieu);
            reportViewer1.LocalReport.SetParameters(reportTho);
            reportViewer1.LocalReport.SetParameters(reportTrietKhau);
            reportViewer1.LocalReport.SetParameters(reportTuVanKH1);
            reportViewer1.LocalReport.SetParameters(reportTuVanKH2);
            reportViewer1.LocalReport.SetParameters(reportTuVanKH3);
            reportViewer1.LocalReport.SetParameters(reportTGDK);
            reportViewer1.LocalReport.SetParameters(reportYeuCauKH1);
            reportViewer1.LocalReport.SetParameters(reportYeuCauKH2);
            reportViewer1.LocalReport.SetParameters(reportYeuCauKH3);
            reportViewer1.LocalReport.SetParameters(reportTongTienPhuTungTT);
            reportViewer1.LocalReport.SetParameters(reportTongTienCongThoTT);
            reportViewer1.LocalReport.SetParameters(reportTongTien);
            reportViewer1.LocalReport.SetParameters(reportNgayBaoDuong);
            reportViewer1.LocalReport.DataSources.Add(reportGiamTru);
            reportViewer1.LocalReport.DataSources.Add(reportDataPhuTung);
            reportViewer1.LocalReport.DataSources.Add(reportDataDocTien);
            reportViewer1.LocalReport.DataSources.Add(reportDataCongty);
            reportViewer1.LocalReport.Refresh();

            this.reportViewer1.RefreshReport();
        }

        private void BtnLuuLai_Click(object sender, EventArgs e)
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
                    cmd.CommandText = @"Update dbo.LichSuBaoDuongXeTam Set YeuCauKH = @yeucaukh, TuVanSuaChua = @TuVanSuaChua where IdBaoDuong = @idbaoduong and IdCongTy = @idcongty";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@yeucaukh", textBoxX1.Text);
                    cmd.Parameters.AddWithValue("@TuVanSuaChua", textBoxX2.Text);
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

        string XuLyThoiGianDuKien(int dayBaoDuong, int monthBaoDuong, int yearBaoDuong, int hourBaoDuong, int minuteBaoDuong,int minutePlan ,string sangChieu)
        {
            int soPhutConLaiTrongNgay = 0;
            if (sangChieu.Equals("AM") || hourBaoDuong == 12)
            {
                soPhutConLaiTrongNgay = 24 * 60 - hourBaoDuong * 60 - minuteBaoDuong;
            }
            else if (sangChieu.Equals("PM"))
            {
                soPhutConLaiTrongNgay = 24 * 60 - (hourBaoDuong + 12) * 60 - minuteBaoDuong;
            }
            if(minutePlan <= soPhutConLaiTrongNgay)
            {
                int minuteWillComple = 0;
                if (sangChieu.Equals("AM") || hourBaoDuong == 12)
                {
                    minuteWillComple = hourBaoDuong * 60 + minuteBaoDuong + minutePlan;
                }
                else
                {
                    minuteWillComple = (hourBaoDuong + 12) * 60 + minuteBaoDuong + minutePlan;
                }
                if (minuteWillComple/60 < 12)
                {
                    DateTime dt = new DateTime(yearBaoDuong, monthBaoDuong, dayBaoDuong, minuteWillComple / 60, minuteWillComple % 60, 0, 66);
                    return String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + "AM";
                }
                else if (minuteWillComple / 60 > 12)
                {
                    DateTime dt = new DateTime(yearBaoDuong, monthBaoDuong, dayBaoDuong, minuteWillComple / 60 - 12, minuteWillComple % 60, 0, 66);
                    return String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + "PM";
                }
                else
                {
                    DateTime dt = new DateTime(yearBaoDuong, monthBaoDuong, dayBaoDuong, minuteWillComple / 60, minuteWillComple % 60, 0, 66);
                    return String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + "PM";
                }
            }
            else
            {
                int thoiGianNgayTiepTheo = minutePlan - soPhutConLaiTrongNgay;
                int days = DateTime.DaysInMonth(yearBaoDuong, monthBaoDuong);
                int soNgayTongCong = 0;
                soNgayTongCong += thoiGianNgayTiepTheo / 1440 + dayBaoDuong;
                int thoiGianConThua = thoiGianNgayTiepTheo % 1440;
                if(monthBaoDuong < 12)
                {
                    if(soNgayTongCong > days)
                    {
                        dayBaoDuong = soNgayTongCong % days;
                        monthBaoDuong += 1;
                    }
                    else
                    {
                        dayBaoDuong = soNgayTongCong;
                    }
                    hourBaoDuong = thoiGianConThua / 60;
                    minuteBaoDuong = thoiGianConThua % 60;
                    if (hourBaoDuong < 12)
                    {
                        DateTime dt = new DateTime(yearBaoDuong, monthBaoDuong, dayBaoDuong, hourBaoDuong, minuteBaoDuong, 0, 66);
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + "AM";
                    }
                    else if (hourBaoDuong > 12)
                    {
                        DateTime dt = new DateTime(yearBaoDuong, monthBaoDuong, dayBaoDuong, hourBaoDuong - 12, minuteBaoDuong, 0, 66);
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + "PM";
                    }
                    else
                    {
                        DateTime dt = new DateTime(yearBaoDuong, monthBaoDuong, dayBaoDuong, hourBaoDuong, minuteBaoDuong, 0, 66);
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + "PM";
                    }
                }
                else
                {
                    if (soNgayTongCong > days)
                    {
                        dayBaoDuong = soNgayTongCong % days;
                        monthBaoDuong = 1;
                        yearBaoDuong += 1;
                    }
                    else
                    {
                        dayBaoDuong = soNgayTongCong;
                    }
                    hourBaoDuong = thoiGianConThua / 60;
                    minuteBaoDuong = thoiGianConThua % 60;
                    if (hourBaoDuong < 12)
                    {
                        DateTime dt = new DateTime(yearBaoDuong, monthBaoDuong, dayBaoDuong, hourBaoDuong, minuteBaoDuong, 0, 66);
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + "AM";
                    }
                    else if (hourBaoDuong > 12)
                    {
                        DateTime dt = new DateTime(yearBaoDuong, monthBaoDuong, dayBaoDuong, hourBaoDuong - 12, minuteBaoDuong, 0, 66);
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + "PM";
                    }
                    else
                    {
                        DateTime dt = new DateTime(yearBaoDuong, monthBaoDuong, dayBaoDuong, hourBaoDuong / 60, minuteBaoDuong, 0, 66);
                        return String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + "PM";
                    }
                }
            }
            
        }
    }
}
