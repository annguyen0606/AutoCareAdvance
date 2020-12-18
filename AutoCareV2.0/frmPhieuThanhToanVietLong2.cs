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
    public partial class frmPhieuThanhToanVietLong2 : Form
    {
        private SqlDataProvider sqlPrv = new SqlDataProvider();
        private SqlConnection con;
        private string cn = Class.datatabase.connect;
        public string LoaiHinhBaoDuong = "";
        public frmPhieuThanhToanVietLong2()
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
        private async void FrmPhieuThanhToanVietLong2_Load(object sender, EventArgs e)
        {
            DataTable dtPhuTung = new DataTable();
            DataTable dtTienCong = new DataTable();

            connect();

            string tongtien = "0";
            decimal tien = 0m;

            SqlCommand cmd = new SqlCommand("sp_Test_Report3", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dtPhuTung);

            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select TenKH, DienThoai, DiaChi from dbo.ThongTinNguoiDiBaoDuong where IdBaoDuong=@IdBaoDuong and IdCongTy=@IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            DataTable nguoiDiBaoDuong = new DataTable();
            da.Fill(nguoiDiBaoDuong);

            if (nguoiDiBaoDuong.Rows.Count > 0)
            {
                dtPhuTung.Rows[0]["DienThoai"] = nguoiDiBaoDuong.Rows[0]["DienThoai"];
                dtPhuTung.Rows[0]["TenKH"] = nguoiDiBaoDuong.Rows[0]["TenKH"];
                dtPhuTung.Rows[0]["DiaChi"] = nguoiDiBaoDuong.Rows[0]["DiaChi"];
            }

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "sp_Report_ThoDichVu_TienCong3";
            da.Fill(dtTienCong);

            DataColumn cl1 = new DataColumn("NoiDungBD");
            DataColumn cl2 = new DataColumn("TienCong");
            cl2.DataType = typeof(decimal);

            dtPhuTung.Columns.Add(cl1);
            dtPhuTung.Columns.Add(cl2);

            int index = 0;

            SqlCommand cpp = new SqlCommand();
            cpp.CommandText = "select TienLayCuaKH,CongViec from ThueNgoai where IdCongTy=@congty and IdBaoDuong=@baoduong";

            cpp.Parameters.AddWithValue("@baoduong", Class.SelectedCustomer._idbaoduong);
            cpp.Parameters.AddWithValue("@congty", Class.CompanyInfo.idcongty);

            DataTable dtngoai = new DataTable();
            dtngoai = Class.datatabase.getData(cpp);

            SqlCommand cdd = new SqlCommand();
            cdd.CommandText = "select PhanTramGiam as phangiam from LichSuBaoDuongPhieu where IdCongTy=@congty and IdBaoDuong=@baoduong";

            cdd.Parameters.AddWithValue("@baoduong", Class.SelectedCustomer._idbaoduong);
            cdd.Parameters.AddWithValue("@congty", Class.CompanyInfo.idcongty);
            DataTable dttiengiam = new DataTable();
            dttiengiam = Class.datatabase.getData(cdd);
            float tienTrietKhau = 0;

            cdd.CommandText = "select TienTrietKhau from LichSuBaoDuongPhieu where IdCongTy=@congty and IdBaoDuong=@baoduong";
            cdd.Parameters.Clear();
            cdd.Parameters.AddWithValue("@baoduong", Class.SelectedCustomer._idbaoduong);
            cdd.Parameters.AddWithValue("@congty", Class.CompanyInfo.idcongty);

            string _tienTrietKhau = Class.datatabase.ExecuteScalar(cdd);
            if (!String.IsNullOrEmpty(_tienTrietKhau))
                tienTrietKhau = tienTrietKhau + float.Parse(_tienTrietKhau);
            //PhanTramGiamTruBindingSource.DataSource = dttiengiam;
            if (dtTienCong.Rows.Count > 0)
            {
                foreach (DataRow rows in dtTienCong.Rows)
                {
                    DataRow drnew = dtPhuTung.NewRow();

                    drnew["TienCong"] = rows["TienKhachTra"].ToString();
                    drnew["TenPhuTung"] = rows["NoiDungBD"].ToString();
                    dtPhuTung.Rows.Add(drnew);
                    int soDong = dtPhuTung.Rows.Count;
                    dtPhuTung.Rows[soDong - 1]["TT"] = soDong;
                    index++;
                }
            }

            if (dtngoai.Rows.Count > 0)
            {
                foreach (DataRow rows in dtngoai.Rows)
                {
                    DataRow drnew = dtPhuTung.NewRow();
                    drnew["TienCong"] = rows["TienLayCuaKH"].ToString();
                    drnew["TenPhuTung"] = rows["CongViec"].ToString();
                    dtPhuTung.Rows.Add(drnew);
                    int soDong = dtPhuTung.Rows.Count;
                    dtPhuTung.Rows[soDong - 1]["TT"] = soDong;
                    index++;
                }
            }

            //BaoCaoPhuTungThayTheBindingSource.DataSource = dtPhuTung;

            foreach (DataRow rows in dtPhuTung.Rows)
            {
                if (!String.IsNullOrEmpty(rows["TienCong"].ToString()))
                {
                    tien += Convert.ToDecimal(rows["TienCong"]);
                }
                if (!String.IsNullOrEmpty(rows["GiaTien"].ToString()))
                {
                    tien += Convert.ToDecimal(rows["GiaTien"]);
                }
            }
            decimal tienPhuTung = 0m;
            decimal tienCongTho = 0m;
            foreach (DataRow rows in dtPhuTung.Rows)
            {
                if (!String.IsNullOrEmpty(rows["TienCong"].ToString()))
                {
                    tienCongTho += Convert.ToDecimal(rows["TienCong"]);
                }
            }

            foreach (DataRow rows in dtPhuTung.Rows)
            {
                if (!String.IsNullOrEmpty(rows["GiaTien"].ToString()))
                {
                    tienPhuTung += Convert.ToDecimal(rows["GiaTien"]);
                }
            }

            if (dttiengiam != null && dttiengiam.Rows.Count > 0)
            {
                if (!String.IsNullOrEmpty(dttiengiam.Rows[0]["phangiam"].ToString()))
                    tien = tien - (tien * decimal.Parse(dttiengiam.Rows[0]["phangiam"].ToString()) / 100);
            }

            tien = tien - (decimal)tienTrietKhau;

            //
            tongtien = ChuyenSo(tien.ToString("0"));
            DataTable dtTien = new DataTable();
            DataRow dr;
            DataColumn cl = new DataColumn("TienBangChu");
            dtTien.Columns.Add(cl);
            dr = dtTien.NewRow();
            string kitudau = tongtien.Substring(0, 1).ToUpper();
            tongtien = kitudau + tongtien.Substring(1);
            dr["TienBangChu"] = tongtien + " VND";
            dtTien.Rows.Add(dr);

            //DocTienBindingSource.DataSource = dtTien;

            //them thong tin
            DataTable dtthongtin = new DataTable();
            SqlCommand cmo = new SqlCommand();
            cmo.CommandText = @"select DiaChi as diachi,DienThoai as didong,DienThoaiBan as dtban,TenLapPhieu as
            lapphieu,TenQuanLy as quanly,TenCongTy as tencongty from congty where IdCongTy=@idct";

            cmo.Parameters.Clear();

            cmo.Parameters.AddWithValue("@idct", Class.CompanyInfo.idcongty);
            dtthongtin = Class.datatabase.getData(cmo);

            //Lay nguoi lap phieu
            //DataTable dtnguoilapphieu = new DataTable();
            //SqlCommand cbonguoilapphieu = new SqlCommand();
            //cbonguoilapphieu.CommandText = "SELECT TenNguoiLapPhieu FROM NguoiLapPhieu WHERE IdBaoDuong = @IdBaoDuong";
            //cbonguoilapphieu.Parameters.Clear();
            //cbonguoilapphieu.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
            //dtnguoilapphieu = Class.datatabase.getData(cbonguoilapphieu);

            //if (dtnguoilapphieu.Rows.Count > 0 && !String.IsNullOrEmpty(dtnguoilapphieu.Rows[0]["TenNguoiLapPhieu"].ToString()))
            //    dtthongtin.Rows[0]["lapphieu"] = dtnguoilapphieu.Rows[0]["TenNguoiLapPhieu"];
            //else
            //{
            //    dtthongtin.Rows[0]["lapphieu"] = Class.EmployeeInfo.TenNhanVien;
            //}
            dtthongtin.Rows[0]["lapphieu"] = Class.EmployeeInfo.TenNhanVien;
            //dtthongtin.Rows[0]["didong"] = SoDienThoai;
            //Lay ngay giao xe
            DataTable dtngaygiaoxe = new DataTable();
            SqlCommand cmdngaygiaoxe = new SqlCommand();
            cmdngaygiaoxe.CommandText = "SELECT NgayBaoDuong, NgayGiaoXe FROM LichSuBaoDuongXe WHERE IdBaoDuong = @IdBaoDuong";
            cmdngaygiaoxe.Parameters.Clear();
            cmdngaygiaoxe.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
            dtngaygiaoxe = Class.datatabase.getData(cmdngaygiaoxe);

            //Lay tho dich vu
            /*
            DataTable dttho = new DataTable();
            SqlCommand cmdtho = new SqlCommand();*/
            //cmdtho.CommandText = @"SELECT DISTINCT thodv.IdTho, thodv.tenTho
            //                        FROM ThoDichVu thodv
            //                        inner join ThoDichVu_TienCongTho2 thodv_tc on thodv.IdTho = thodv_tc.IdTho
            //                        inner join ThueNgoai thue on thue.IdTho = thodv.IdTho
            //                        WHERE thue.IdBaoDuong = @IdBaoDuong or thodv_tc.IdBaoDuong = @IdBaoDuong";


            //cmdtho.CommandText = @"SELECT thodv.IdTho, thodv.tenTho
            //                        FROM ThoDichVu thodv with(nolock)
            //                        inner join ThoDichVu_TienCongTho2 thodv_tc with(nolock) on thodv.IdTho = thodv_tc.IdTho
            //                        inner join ThueNgoai thue with(nolock) on thue.IdTho = thodv.IdTho
            //                        WHERE thue.IdBaoDuong = @IdBaoDuong
            //                        union all
            //                        SELECT thodv.IdTho, thodv.tenTho
            //                        FROM ThoDichVu thodv with(nolock)
            //                        inner join ThoDichVu_TienCongTho2 thodv_tc with(nolock) on thodv.IdTho = thodv_tc.IdTho
            //                        inner join ThueNgoai thue with(nolock) on thue.IdTho = thodv.IdTho
            //                        WHERE thodv_tc.IdBaoDuong = @IdBaoDuong";
            //cmdtho.Parameters.Clear();

            //cmdtho.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
            //dttho = Class.datatabase.getData(cmdtho);



            //string kythuatvien = "";

            //if (dttho != null)
            //{
            //    for (int i = 0; i < dttho.Rows.Count; i++)
            //    {
            //        if (i == dttho.Rows.Count - 1)
            //            kythuatvien = kythuatvien + dttho.Rows[i]["tenTho"];
            //        else
            //            kythuatvien = kythuatvien + dttho.Rows[i]["tenTho"] + "\r\n";
            //    }
            //}

            DateTime dateGiaoXe = Convert.ToDateTime(dtngaygiaoxe.Rows[0]["NgayGiaoXe"]);
            DateTime dateNhanXe = Convert.ToDateTime(dtngaygiaoxe.Rows[0]["NgayBaoDuong"]);
            string NgayGiaoXe = "Ngày " + dateGiaoXe.Day + " tháng " + dateGiaoXe.Month + " năm " + dateGiaoXe.Year + "";
            string GioGiaoXe = "" + dateNhanXe.Hour + "h" + dateNhanXe.Minute + "p" + " / " + "" + dateGiaoXe.Hour + "h" + dateGiaoXe.Minute + "p";
            DataColumn ColumnNgayGiaoXe = dtthongtin.Columns.Add("ngay", typeof(String));
            DataColumn ColumnGioGiaoXe = dtthongtin.Columns.Add("GioGiaoXe", typeof(String));
            DataColumn ColumnKyThuatVien = dtthongtin.Columns.Add("KyThuatVien", typeof(String));
            dtthongtin.Rows[0]["ngay"] = NgayGiaoXe;
            dtthongtin.Rows[0]["GioGiaoXe"] = GioGiaoXe;
            //dtthongtin.Rows[0]["KyThuatVien"] = kythuatvien;
            dtthongtin.Rows[0]["KyThuatVien"] = "";

            string ngayBaoDuongTam = dtPhuTung.Rows[0]["NgayBaoDuong"].ToString();
            string[] thoigianbaoduong = ngayBaoDuongTam.Split(' ');
            string[] ngayBD = thoigianbaoduong[0].Split('/');

            dtPhuTung.Columns.Add("day_NgayBaoDuong", typeof(string));
            dtPhuTung.Columns.Add("month_NgayBaoDuong", typeof(string));
            dtPhuTung.Columns.Add("year_NgayBaoDuong", typeof(string));

            dtPhuTung.Rows[0]["day_NgayBaoDuong"] = ngayBD[1];
            dtPhuTung.Rows[0]["month_NgayBaoDuong"] = ngayBD[0];
            dtPhuTung.Rows[0]["year_NgayBaoDuong"] = ngayBD[2];

            DataTable dtPhuTungReal = ReReverseRowsInDataTable(dtPhuTung);
            dtPhuTungReal.Rows[0]["TenKH"] = dtPhuTung.Rows[0]["TenKH"];
            dtPhuTungReal.Rows[0]["DienThoai"] = dtPhuTung.Rows[0]["DienThoai"];
            dtPhuTungReal.Rows[0]["DiaChi"] = dtPhuTung.Rows[0]["DiaChi"];
            dtPhuTungReal.Rows[0]["TenXe"] = dtPhuTung.Rows[0]["TenXe"];
            dtPhuTungReal.Rows[0]["BienSo"] = dtPhuTung.Rows[0]["BienSo"];
            dtPhuTungReal.Rows[0]["Sokhung"] = dtPhuTung.Rows[0]["Sokhung"];
            dtPhuTungReal.Rows[0]["SoMay"] = dtPhuTung.Rows[0]["SoMay"];
            dtPhuTungReal.Rows[0]["SoKm"] = dtPhuTung.Rows[0]["SoKm"];
            dtPhuTungReal.Rows[0]["NgayMua"] = dtPhuTung.Rows[0]["NgayMua"];

            for (int f = 0; f < dtPhuTungReal.Rows.Count; f++)
            {
                dtPhuTungReal.Rows[f]["TT"] = f + 1;
            }

            int soDongCanThem = dtPhuTungReal.Rows.Count;
            int k = 0;
            switch (soDongCanThem)
            {
                case 0:
                    for (k = 0; k < 15; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 1:
                    for (k = 0; k < 14; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 2:
                    for (k = 0; k < 13; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 3:
                    for (k = 0; k < 12; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 4:
                    for (k = 0; k < 11; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 5:
                    for (k = 0; k < 10; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 6:
                    for (k = 0; k < 9; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 7:
                    for (k = 0; k < 8; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 8:
                    for (k = 0; k < 7; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 9:
                    for (k = 0; k < 6; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 10:
                    for (k = 0; k < 5; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 11:
                    for (k = 0; k < 4; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 12:
                    for (k = 0; k < 3; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 13:
                    for (k = 0; k < 2; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
                case 14:
                    for (k = 0; k < 1; k++)
                    {
                        dtPhuTungReal.Rows.Add();
                    }
                    break;
            }
            string[] gioPhutGiayBD = thoigianbaoduong[1].Split(':');
            DateTime dt = new DateTime(int.Parse(ngayBD[2]), int.Parse(ngayBD[0]), int.Parse(ngayBD[1]), int.Parse(gioPhutGiayBD[0]), int.Parse(gioPhutGiayBD[1]), int.Parse(gioPhutGiayBD[2]), 66);
            string ngayBaoDuong = String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt) + thoigianbaoduong[2].ToString();
            string timeDuKien = "0";
            if (String.IsNullOrEmpty(dtPhuTung.Rows[0]["TGDUKIEN"].ToString().Trim()))
            {
                timeDuKien = XuLyThoiGianDuKien(int.Parse(ngayBD[1]), int.Parse(ngayBD[0]), int.Parse(ngayBD[2]), int.Parse(gioPhutGiayBD[0]), int.Parse(gioPhutGiayBD[1]), 10, thoigianbaoduong[2].ToString());
            }
            else
            {
                timeDuKien = XuLyThoiGianDuKien(int.Parse(ngayBD[1]), int.Parse(ngayBD[0]), int.Parse(ngayBD[2]), int.Parse(gioPhutGiayBD[0]), int.Parse(gioPhutGiayBD[1]), int.Parse(dtPhuTung.Rows[0]["TGDUKIEN"].ToString().Trim()), thoigianbaoduong[2].ToString());
            }
            string[] arrayNgayGioGiaoXe = dtPhuTung.Rows[0]["NgayGiaoXe"].ToString().Split(' ');
            string[] arrayNgayGX = arrayNgayGioGiaoXe[0].Split('/');
            string[] arrayGioGX = arrayNgayGioGiaoXe[1].Split(':');
            DateTime dt2 = new DateTime(int.Parse(arrayNgayGX[2]), int.Parse(arrayNgayGX[0]), int.Parse(arrayNgayGX[1]), int.Parse(arrayGioGX[0]), int.Parse(arrayGioGX[1]), int.Parse(arrayGioGX[2]), 66);
            string ngayGiaoXe1 = String.Format("{0:dd/MM/yyyy hh:mm:ss} ", dt2) + arrayNgayGioGiaoXe[2].ToString();
            DataTable dtThoiGianBaoDuong = sqlPrv.GetData("select CONVERT(VARCHAR(10),NgayGiaoXe,103) as ngay,CONVERT(VARCHAR(10),NgayGiaoXe,108) as gio from LichSuBaoDuongXe where IdBaoDuong=" + Class.SelectedCustomer._idbaoduong);
            ReportDataSource reportThoiGian = new ReportDataSource();
            reportThoiGian.Name = "ThoiGianBaoDuong";
            reportThoiGian.Value = dtThoiGianBaoDuong;

            ReportDataSource reportGiamTru = new ReportDataSource();
            reportGiamTru.Name = "DataSetGiamTru";
            reportGiamTru.Value = dttiengiam;


            ReportDataSource reportDataPhuTung = new ReportDataSource();
            reportDataPhuTung.Name = "DataSetPhuTung";
            reportDataPhuTung.Value = dtPhuTungReal;


            ReportDataSource reportDataDocTien = new ReportDataSource();
            reportDataDocTien.Name = "DataSetTongTien";
            reportDataDocTien.Value = dtTien;

            ReportDataSource reportDataCongty = new ReportDataSource();
            reportDataCongty.Name = "DataSetThongTin";
            reportDataCongty.Value = dtthongtin;

            string[] tuVanKH = dtPhuTung.Rows[0]["TuVanSuaChua"].ToString().Split('\n');
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

            string[] yeuCauKH = dtPhuTung.Rows[0]["YeuCauKH"].ToString().Split('\n');
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

            ReportParameter reportTongTienCongThoTT;
            ReportParameter reportTongTienPhuTungTT;
            ReportParameter reportTongTien;
            if (tienCongTho > 0)
            {
                string tongTienCongTho = string.Format("{0:#,#.}", tienCongTho);
                reportTongTienCongThoTT = new ReportParameter("TongTienCongThoThanhToan", tongTienCongTho);
            }
            else
            {
                string tongTienCongTho = "0";
                reportTongTienCongThoTT = new ReportParameter("TongTienCongThoThanhToan", tongTienCongTho);
            }
            if (tienPhuTung > 0)
            {
                string tongTienPhuTung = string.Format("{0:#,#.}", tienPhuTung);
                reportTongTienPhuTungTT = new ReportParameter("TongTienPhuTungThanhToan", tongTienPhuTung);
            }
            else
            {
                string tongTienPhuTung = "0";
                reportTongTienPhuTungTT = new ReportParameter("TongTienPhuTungThanhToan", tongTienPhuTung);
            }
            if (tien > 0)
            {
                string soTien = string.Format("{0:#,#.}", tien);
                reportTongTien = new ReportParameter("TongTien", soTien);
            }
            else
            {
                string soTien = "0";
                reportTongTien = new ReportParameter("TongTien", soTien);
            }

            ReportParameter reportSoPhieu = new ReportParameter("SoPhieu", Class.SelectedCustomer._idbaoduong);
            //ReportParameter reportTho = new ReportParameter("ThoSuaChua", kythuatvien);
            ReportParameter reportTho = new ReportParameter("ThoSuaChua", " ");
            ReportParameter reportTrietKhau = new ReportParameter("TienTrietKhau", tienTrietKhau.ToString());
            ReportParameter reportTuVanKH1 = new ReportParameter("TuVanKH1", tuVan1);
            ReportParameter reportTuVanKH2 = new ReportParameter("TuVanKH2", tuVan2);
            ReportParameter reportTuVanKH3 = new ReportParameter("TuVanKH3", tuVan3);
            ReportParameter reportThoiGianDuKien = new ReportParameter("ThoiGianDuKien", timeDuKien);
            ReportParameter reportYeuCauKH1 = new ReportParameter("YeuCauKH1", yeuCau1);
            ReportParameter reportYeuCauKH2 = new ReportParameter("YeuCauKH2", yeuCau2);
            ReportParameter reportYeuCauKH3 = new ReportParameter("YeuCauKH3", yeuCau3);
            ReportParameter reportTongTienThanhToan = new ReportParameter("TongTienThanhToan", tongtien + " VND");
            ReportParameter reportNgayBaoDuong = new ReportParameter("NgayBaoDuong", ngayBaoDuong);
            ReportParameter reportNgayGiaoXe = new ReportParameter("NgayGioTraXe", ngayGiaoXe1);


            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.SetParameters(reportSoPhieu);
            reportViewer.LocalReport.SetParameters(reportTho);
            reportViewer.LocalReport.SetParameters(reportTrietKhau);
            reportViewer.LocalReport.SetParameters(reportTuVanKH1);
            reportViewer.LocalReport.SetParameters(reportTuVanKH2);
            reportViewer.LocalReport.SetParameters(reportTuVanKH3);
            reportViewer.LocalReport.SetParameters(reportThoiGianDuKien);
            reportViewer.LocalReport.SetParameters(reportYeuCauKH1);
            reportViewer.LocalReport.SetParameters(reportYeuCauKH2);
            reportViewer.LocalReport.SetParameters(reportYeuCauKH3);
            reportViewer.LocalReport.SetParameters(reportTongTienPhuTungTT);
            reportViewer.LocalReport.SetParameters(reportTongTienCongThoTT);
            reportViewer.LocalReport.SetParameters(reportTongTienThanhToan);
            reportViewer.LocalReport.SetParameters(reportNgayBaoDuong);
            reportViewer.LocalReport.SetParameters(reportTongTien);
            reportViewer.LocalReport.SetParameters(reportNgayGiaoXe);
            reportViewer.LocalReport.DataSources.Add(reportGiamTru);
            reportViewer.LocalReport.DataSources.Add(reportDataPhuTung);
            reportViewer.LocalReport.DataSources.Add(reportDataDocTien);
            reportViewer.LocalReport.DataSources.Add(reportDataCongty);
            reportViewer.LocalReport.DataSources.Add(reportThoiGian);
            reportViewer.LocalReport.Refresh();

            this.reportViewer.RefreshReport();
        }
        private string ChuyenSo(string number)
        {
            string[] dv = { "", "mươi", "trăm", "nghìn", "triệu", "tỉ" };
            string[] cs = { "không", "một", "hai", "ba", "bốn", "năm", "sáu", "bảy", "tám", "chín" };
            string doc;
            int i, j, k, n, len, found, ddv, rd;

            len = number.Length;
            number += "ss";
            doc = "";
            found = 0;
            ddv = 0;
            rd = 0;

            i = 0;
            while (i < len)
            {
                //So chu so o hang dang duyet
                n = (len - i + 2) % 3 + 1;

                //Kiem tra so 0
                found = 0;
                for (j = 0; j < n; j++)
                {
                    if (number[i + j] != '0')
                    {
                        found = 1;
                        break;
                    }
                }

                //Duyet n chu so
                if (found == 1)
                {
                    rd = 1;
                    for (j = 0; j < n; j++)
                    {
                        ddv = 1;
                        switch (number[i + j])
                        {
                            case '0':
                                if (n - j == 3) doc += cs[0] + " ";
                                if (n - j == 2)
                                {
                                    if (number[i + j + 1] != '0') doc += "lẻ ";
                                    ddv = 0;
                                }
                                break;

                            case '1':
                                if (n - j == 3) doc += cs[1] + " ";
                                if (n - j == 2)
                                {
                                    doc += "mười ";
                                    ddv = 0;
                                }
                                if (n - j == 1)
                                {
                                    if (i + j == 0) k = 0;
                                    else k = i + j - 1;

                                    if (number[k] != '1' && number[k] != '0')
                                        doc += "mốt ";
                                    else
                                        doc += cs[1] + " ";
                                }
                                break;

                            case '5':
                                if (i + j == len - 1)
                                    doc += "lăm ";
                                else
                                    doc += cs[5] + " ";
                                break;

                            default:
                                doc += cs[(int)number[i + j] - 48] + " ";
                                break;
                        }

                        //Doc don vi nho
                        if (ddv == 1)
                        {
                            doc += dv[n - j - 1] + " ";
                        }
                    }
                }

                //Doc don vi lon
                if (len - i - n > 0)
                {
                    if ((len - i - n) % 9 == 0)
                    {
                        if (rd == 1)
                            for (k = 0; k < (len - i - n) / 9; k++)
                                doc += "tỉ ";
                        rd = 0;
                    }
                    else
                        if (found != 0) doc += dv[((len - i - n + 1) % 9) / 3 + 2] + " ";
                }

                i += n;
            }

            if (len == 1)
                if (number[0] == '0' || number[0] == '5') return cs[(int)number[0] - 48];

            return doc;
        }
        string XuLyThoiGianDuKien(int dayBaoDuong, int monthBaoDuong, int yearBaoDuong, int hourBaoDuong, int minuteBaoDuong, int minutePlan, string sangChieu)
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
            if (minutePlan <= soPhutConLaiTrongNgay)
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
                if (minuteWillComple / 60 < 12)
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
                if (monthBaoDuong < 12)
                {
                    if (soNgayTongCong > days)
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

        private DataTable ReReverseRowsInDataTable(DataTable inputTable)
        {
            DataTable outputTable = inputTable.Clone();
            for (int i = inputTable.Rows.Count - 1; i >= 0; i--)
            {
                outputTable.ImportRow(inputTable.Rows[i]);
            }

            return outputTable;
        }

        private void FrmPhieuThanhToanVietLong2_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}
