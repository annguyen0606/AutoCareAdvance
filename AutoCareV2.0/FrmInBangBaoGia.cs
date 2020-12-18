using Microsoft.Reporting.WinForms;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class FrmInBangBaoGia : Form
    {
        #region Variable

        private DataTable tableThongTinCongTy = new DataTable();
        private DataTable tableThongTinKhachHang = new DataTable();
        private DataTable tableBaoGiaSuaChua = new DataTable();
        private DataTable tableBaoGiaCongTho = new DataTable();
        private DataTable tableBaoGiaPhuTung = new DataTable();

        private DataTable dataCongTy = new DataTable();
        private DataTable dataKhachHang = new DataTable();
        private DataTable dataBaoGia = new DataTable();
        private DataTable dataCongTho = new DataTable();
        private DataTable dataPhuTung = new DataTable();

        public long IdBaoDuong = 0;
        public long IdBaoDuongTam = 0;

        private SqlCommand cmd = new SqlCommand();
        private Class.DocTien doctien = new Class.DocTien();

        #endregion Variable

        public FrmInBangBaoGia()
        {
            InitializeComponent();
        }

        private void FrmInBangBaoGia_Load(object sender, EventArgs e)
        {
            if (IdBaoDuong == 0 && IdBaoDuongTam == 0)
            {
                MessageBox.Show("Thông tin lần bảo dưỡng không tồn tại!\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if(IdBaoDuong != 0)
            {
                //Lấy thông tin công ty
                cmd.CommandText = @"SELECT TenCongTy, DiaChi, DienThoai
                                    FROM CongTy WHERE IdCongTy = @IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                tableThongTinCongTy = Class.datatabase.getData(cmd);

                //Lấy thông tin khách hàng
                cmd.CommandText = @"SELECT kh.IdKhachHang, kh.HoKH, kh.TenKH, kh.GioiTinh, kh.NgaySinh, kh.DienThoai, kh.Diachi,
                                    ls.IdBaoDuong, ls.IdCuaHang, ls.TenXe, ls.BienSo, ls.Sokhung, ls.SoMay, ls.SoKm
                                    FROM KhachHang kh, LichSuBaoDuongXe ls
                                    WHERE kh.IdKhachHang = ls.IdKhachHang AND ls.IdBaoDuong = @IdBaoDuong";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);

                tableThongTinKhachHang = Class.datatabase.getData(cmd);

                //Lấy thông tin báo giá
                cmd.CommandText = @"SELECT IdBaoGia, NgayBaoGia, TongTienVatTu, TongTienCong, VAT, TongSauVAT, CoVanDV, TruongPhongDV
                                    FROM BaoGiaSuaChua WHERE IdBaoDuong = @IdBaoDuong";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);

                tableBaoGiaSuaChua = Class.datatabase.getData(cmd);

                if (tableBaoGiaSuaChua.Rows.Count > 0)
                {
                    //Lấy thông tin chi tiết báo giá
                    cmd.CommandText = @"SELECT ROW_NUMBER() over (order by (select 1)) as STT, NoiDungCV, TienCong, DaThucHien
                                        FROM BaoGiaCongTho WHERE IdBaoGia = @IdBaoGia";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaSuaChua.Rows[0]["IdBaoGia"].ToString());

                    tableBaoGiaCongTho = Class.datatabase.getData(cmd);

                    cmd.CommandText = @"SELECT ROW_NUMBER() over (order by (select 1)) as STT, TenPT, MaPT, DVT, SoLuong, DonGia, ThanhTien, DaThucHien
                                        FROM BaoGiaPhuTung WHERE IdBaoGia = @IdBaoGia";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaSuaChua.Rows[0]["IdBaoGia"].ToString());

                    tableBaoGiaPhuTung = Class.datatabase.getData(cmd);
                }
            }

            if(IdBaoDuongTam != 0)
            {
                //Lấy thông tin công ty
                cmd.CommandText = @"SELECT TenCongTy, DiaChi, DienThoai
                                    FROM CongTy WHERE IdCongTy = @IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

                tableThongTinCongTy = Class.datatabase.getData(cmd);

                //Lấy thông tin khách hàng
                cmd.CommandText = @"SELECT kh.IdKhachHang, kh.HoKH, kh.TenKH, kh.GioiTinh, kh.NgaySinh, kh.DienThoai, kh.Diachi,
                                    ls.IdBaoDuong, ls.IdCuaHang, ls.TenXe, ls.BienSo, ls.Sokhung, ls.SoMay, ls.SoKm
                                    FROM KhachHang kh, LichSuBaoDuongXeTam ls
                                    WHERE kh.IdKhachHang = ls.IdKhachHang AND ls.IdBaoDuong = @IdBaoDuong";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongTam);

                tableThongTinKhachHang = Class.datatabase.getData(cmd);

                //Lấy thông tin báo giá
                cmd.CommandText = @"SELECT IdBaoGia, NgayBaoGia, TongTienVatTu, TongTienCong, VAT, TongSauVAT, CoVanDV, TruongPhongDV
                                    FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongTam);

                tableBaoGiaSuaChua = Class.datatabase.getData(cmd);

                if (tableBaoGiaSuaChua.Rows.Count > 0)
                {
                    //Lấy thông tin chi tiết báo giá
                    cmd.CommandText = @"SELECT ROW_NUMBER() over (order by (select 1)) as STT, NoiDungCV, TienCong, DaThucHien
                                        FROM BaoGiaCongThoTam WHERE IdBaoGia = @IdBaoGia";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaSuaChua.Rows[0]["IdBaoGia"].ToString());

                    tableBaoGiaCongTho = Class.datatabase.getData(cmd);

                    cmd.CommandText = @"SELECT ROW_NUMBER() over (order by (select 1)) as STT, TenPT, MaPT, DVT, SoLuong, DonGia, ThanhTien, DaThucHien
                                        FROM BaoGiaPhuTungTam WHERE IdBaoGia = @IdBaoGia";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaSuaChua.Rows[0]["IdBaoGia"].ToString());

                    tableBaoGiaPhuTung = Class.datatabase.getData(cmd);
                }
            }
            
            dataCongTy.Columns.Add(new DataColumn("TenCongTy", typeof(String)));
            dataCongTy.Columns.Add(new DataColumn("DiaChi", typeof(String)));
            dataCongTy.Columns.Add(new DataColumn("SoDienThoai", typeof(String)));
            dataCongTy.Columns.Add(new DataColumn("SoFax", typeof(String)));
            dataCongTy.Columns.Add(new DataColumn("MaSoThue", typeof(String)));

            dataKhachHang.Columns.Add(new DataColumn("TenKhachHang", typeof(String)));
            dataKhachHang.Columns.Add(new DataColumn("DiaChi", typeof(String)));
            dataKhachHang.Columns.Add(new DataColumn("MaSoThue", typeof(String)));
            dataKhachHang.Columns.Add(new DataColumn("DienThoai", typeof(String)));
            dataKhachHang.Columns.Add(new DataColumn("LaiXe", typeof(String)));
            dataKhachHang.Columns.Add(new DataColumn("SoDangKy", typeof(String)));
            dataKhachHang.Columns.Add(new DataColumn("LoaiXe", typeof(String)));
            dataKhachHang.Columns.Add(new DataColumn("SoKhung", typeof(String)));
            dataKhachHang.Columns.Add(new DataColumn("SoMay", typeof(String)));
            dataKhachHang.Columns.Add(new DataColumn("SoKm", typeof(String)));

            dataBaoGia.Columns.Add(new DataColumn("SoBaoGia", typeof(String)));
            dataBaoGia.Columns.Add(new DataColumn("NgayBaoGia", typeof(String)));
            dataBaoGia.Columns.Add(new DataColumn("TongTienVatTu", typeof(Decimal)));
            dataBaoGia.Columns.Add(new DataColumn("TongTienCong", typeof(Decimal)));
            dataBaoGia.Columns.Add(new DataColumn("VAT", typeof(Decimal)));
            dataBaoGia.Columns.Add(new DataColumn("TongSauVAT", typeof(Decimal)));
            dataBaoGia.Columns.Add(new DataColumn("TongTienBangChu", typeof(String)));
            dataBaoGia.Columns.Add(new DataColumn("CoVanDichVu", typeof(String)));
            dataBaoGia.Columns.Add(new DataColumn("TruongPhongDichVu", typeof(String)));

            dataCongTho.Columns.Add(new DataColumn("STT", typeof(String)));
            dataCongTho.Columns.Add(new DataColumn("MoTaCongViec", typeof(String)));
            dataCongTho.Columns.Add(new DataColumn("DonGia", typeof(Decimal)));
            dataCongTho.Columns.Add(new DataColumn("ThanhTien", typeof(Decimal)));
            dataCongTho.Columns.Add(new DataColumn("DaThucHien", typeof(Boolean)));

            dataPhuTung.Columns.Add(new DataColumn("STT", typeof(String)));
            dataPhuTung.Columns.Add(new DataColumn("TenPhuTung", typeof(String)));
            dataPhuTung.Columns.Add(new DataColumn("MaPhuTung", typeof(String)));
            dataPhuTung.Columns.Add(new DataColumn("DonViTinh", typeof(String)));
            dataPhuTung.Columns.Add(new DataColumn("SoLuong", typeof(String)));
            dataPhuTung.Columns.Add(new DataColumn("DonGia", typeof(Decimal)));
            dataPhuTung.Columns.Add(new DataColumn("ThanhTien", typeof(Decimal)));
            dataPhuTung.Columns.Add(new DataColumn("DaThucHien", typeof(Boolean)));

            DataRow rowCongTy = dataCongTy.NewRow();
            DataRow rowKhachHang = dataKhachHang.NewRow();
            DataRow rowBaoGia = dataBaoGia.NewRow();

            rowCongTy["TenCongTy"] = tableThongTinCongTy.Rows[0]["TenCongTy"].ToString();
            rowCongTy["DiaChi"] = tableThongTinCongTy.Rows[0]["DiaChi"].ToString();
            rowCongTy["SoDienThoai"] = "Số ĐT: " + tableThongTinCongTy.Rows[0]["DienThoai"].ToString();
            rowCongTy["SoFax"] = "Số Fax: .................";
            rowCongTy["MaSoThue"] = "MST: .................";

            rowKhachHang["TenKhachHang"] = tableThongTinKhachHang.Rows[0]["HoKH"].ToString() + " " + tableThongTinKhachHang.Rows[0]["TenKH"].ToString();
            rowKhachHang["DiaChi"] = tableThongTinKhachHang.Rows[0]["Diachi"].ToString();
            rowKhachHang["DienThoai"] = tableThongTinKhachHang.Rows[0]["DienThoai"].ToString();
            rowKhachHang["SoDangKy"] = tableThongTinKhachHang.Rows[0]["BienSo"].ToString();
            rowKhachHang["LoaiXe"] = tableThongTinKhachHang.Rows[0]["TenXe"].ToString();
            rowKhachHang["SoKhung"] = tableThongTinKhachHang.Rows[0]["Sokhung"].ToString();
            rowKhachHang["SoMay"] = tableThongTinKhachHang.Rows[0]["SoMay"].ToString();
            rowKhachHang["SoKm"] = tableThongTinKhachHang.Rows[0]["SoKm"].ToString();

            rowBaoGia["SoBaoGia"] = "Báo giá số: " + tableBaoGiaSuaChua.Rows[0]["IdBaoGia"].ToString();
            rowBaoGia["NgayBaoGia"] = "Ngày: " + Convert.ToDateTime(tableBaoGiaSuaChua.Rows[0]["NgayBaoGia"].ToString()).ToString("dd/MM/yyyy");
            rowBaoGia["TongTienVatTu"] = Convert.ToDecimal(tableBaoGiaSuaChua.Rows[0]["TongTienVatTu"].ToString());
            rowBaoGia["TongTienCong"] = Convert.ToDecimal(tableBaoGiaSuaChua.Rows[0]["TongTienCong"].ToString());
            rowBaoGia["VAT"] = Convert.ToDecimal(tableBaoGiaSuaChua.Rows[0]["VAT"].ToString());
            rowBaoGia["TongSauVAT"] = Convert.ToDecimal(tableBaoGiaSuaChua.Rows[0]["TongSauVAT"].ToString());
            rowBaoGia["TongTienBangChu"] = doctien.ChuyenSo(tableBaoGiaSuaChua.Rows[0]["TongSauVAT"].ToString().Split('.')[0]);
            rowBaoGia["CoVanDichVu"] = tableBaoGiaSuaChua.Rows[0]["CoVanDV"].ToString();
            rowBaoGia["TruongPhongDichVu"] = tableBaoGiaSuaChua.Rows[0]["TruongPhongDV"].ToString();

            foreach(DataRow dataRow in tableBaoGiaCongTho.Rows)
            {
                DataRow rowCongTho = dataCongTho.NewRow();

                rowCongTho["STT"] = dataRow["STT"].ToString();
                rowCongTho["MoTaCongViec"] = dataRow["NoiDungCV"].ToString();
                rowCongTho["DonGia"] = Convert.ToDecimal(dataRow["TienCong"].ToString());
                rowCongTho["ThanhTien"] = Convert.ToDecimal(dataRow["TienCong"].ToString());
                rowCongTho["DaThucHien"] = Convert.ToBoolean(dataRow["DaThucHien"].ToString());

                dataCongTho.Rows.Add(rowCongTho);
            }

            foreach(DataRow dataRow in tableBaoGiaPhuTung.Rows)
            {
                DataRow rowPhuTung = dataPhuTung.NewRow();

                rowPhuTung["STT"] = dataRow["STT"].ToString();
                rowPhuTung["TenPhuTung"] = dataRow["TenPT"].ToString();
                rowPhuTung["MaPhuTung"] = dataRow["MaPT"].ToString();
                rowPhuTung["DonViTinh"] = dataRow["DVT"].ToString();
                rowPhuTung["SoLuong"] = dataRow["SoLuong"].ToString();
                rowPhuTung["DonGia"] = Convert.ToDecimal(dataRow["DonGia"].ToString());
                rowPhuTung["ThanhTien"] = Convert.ToDecimal(dataRow["ThanhTien"].ToString());
                rowPhuTung["DaThucHien"] = Convert.ToBoolean(dataRow["DaThucHien"].ToString());

                dataPhuTung.Rows.Add(rowPhuTung);
            }

            dataCongTy.Rows.Add(rowCongTy);
            dataKhachHang.Rows.Add(rowKhachHang);
            dataBaoGia.Rows.Add(rowBaoGia);

            reportViewer.Visible = true;
            reportViewer.ProcessingMode = ProcessingMode.Local;
            reportViewer.LocalReport.ReportPath = Environment.CurrentDirectory + @"\Report\PhieuBaoGiaSuaChua.rdlc";
            reportViewer.LocalReport.EnableExternalImages = true;

            ReportDataSource reportDataSourceCongTy = new ReportDataSource();
            reportDataSourceCongTy.Name = "DataSetThongTinCongTy";
            reportDataSourceCongTy.Value = dataCongTy;

            ReportDataSource reportDataSourceKhachHang = new ReportDataSource();
            reportDataSourceKhachHang.Name = "DataSetThongTinKhachHang";
            reportDataSourceKhachHang.Value = dataKhachHang;

            ReportDataSource reportDataSourceBaoGia = new ReportDataSource();
            reportDataSourceBaoGia.Name = "DataSetThongTinBaoGia";
            reportDataSourceBaoGia.Value = dataBaoGia;

            ReportDataSource reportDataSourceCongTho = new ReportDataSource();
            reportDataSourceCongTho.Name = "DataSetBaoGiaCongTho";
            reportDataSourceCongTho.Value = dataCongTho;

            ReportDataSource reportDataSourcePhuTung = new ReportDataSource();
            reportDataSourcePhuTung.Name = "DataSetBaoGiaPhuTung";
            reportDataSourcePhuTung.Value = dataPhuTung;

            reportViewer.LocalReport.DataSources.Clear();
            reportViewer.LocalReport.DataSources.Add(reportDataSourceCongTy);
            reportViewer.LocalReport.DataSources.Add(reportDataSourceKhachHang);
            reportViewer.LocalReport.DataSources.Add(reportDataSourceBaoGia);
            reportViewer.LocalReport.DataSources.Add(reportDataSourceCongTho);
            reportViewer.LocalReport.DataSources.Add(reportDataSourcePhuTung);
            reportViewer.LocalReport.Refresh();

            this.reportViewer.RefreshReport();
        }
    }
}