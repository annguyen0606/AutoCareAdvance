using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;
namespace AutoCareV2._0.Class
{
    public class KhDB
    {
        public DataTable LoadTenKho()
        {
            string sql = "SELECT IdKho, TenKho FROM KhoHang WHERE IdCongty=" + Convert.ToInt32(CompanyInfo.idcongty);
            SqlCommand cmd = new SqlCommand(sql);
            return Class.datatabase.getData(cmd);
        }
        public DataTable LayNhaSanXuat()
        {
            SqlCommand cmd = new SqlCommand("SELECT * From NhaSanXuat");
            return Class.datatabase.getData(cmd);
        }
        public DataTable LayTenXe()
        {
            SqlCommand cmd = new SqlCommand("Select IdXe, IdXe + ' - ' + TenXe as TenXe,HangSanXuat,DVT From XeMay WHere IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            return Class.datatabase.getData(cmd);
        }

        public DataTable LayTenPhuKien()
        {
            SqlCommand cmd = new SqlCommand("SELECT IdPhuKien, TenPhuKien, DVT FROM PhuKien WHERE IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            return Class.datatabase.getData(cmd);
        }

        public DataTable LayChiTietXe()
        {
            SqlCommand cmd = new SqlCommand("Select * From ChiTietXe WHere IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            return Class.datatabase.getData(cmd);
        }

        public DataTable LayChiTietPhuKien()
        {
            SqlCommand cmd = new SqlCommand("Select * From ChiTietPhuKien WHere IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            return Class.datatabase.getData(cmd);
        }

        public DataTable LayLoaiHoaDon()
        {
            SqlCommand cmd = new SqlCommand("Select * From LoaiHoaDon");
            return Class.datatabase.getData(cmd);
        }
        public DataTable LayDanhSachKhachHang()
        {
            SqlCommand cmd = new SqlCommand("SELECT IdKhachHang, HoKH + ' ' + TenKH as HTKhachHang,Diachi,GioiTinh,DienThoai,NgaySinh,NgheNghiep From KhachHang WHere IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            return Class.datatabase.getData(cmd);
        }
        public DataTable LayThongTinXeTrongKho()
        {
            SqlCommand cmd = new SqlCommand("Select * from ChiTietXe Where IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            return Class.datatabase.getData(cmd);
        }
        public DataTable LayDanhSachNganHang()
        {
            SqlCommand cmd = new SqlCommand("Select * from NganHang");
            return Class.datatabase.getData(cmd);
        }
        public DataTable LayDanhSachChiNhanhNganHang()
        {
            SqlCommand cmd = new SqlCommand("Select * from ChiNhanhNganHang");
            return Class.datatabase.getData(cmd);
        }
        public DataTable LoaiPhieuThu()
        {
            SqlCommand cmd = new SqlCommand("Select * from LoaiPhieuThu");
            return Class.datatabase.getData(cmd);
        }
        public DataTable LoaiPhieuChi()
        {
            SqlCommand cmd = new SqlCommand("Select * from LoaiPhieuChi");
            return Class.datatabase.getData(cmd);
        }
        public DataTable NhaCungCap()
        {
            SqlCommand cmd = new SqlCommand("Select * from NhaCungCap WHERE IDCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            return Class.datatabase.getData(cmd);
        }
        public DataTable SelectDataFromExcel(string sheet, string part)
        {
            string location = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + part.Trim() + ";Extended Properties=Excel 8.0";
            OleDbConnection conn = new OleDbConnection(location);
            string sql = "SELECT * FROM [" + sheet + "$]";
            OleDbCommand cmd = new OleDbCommand(sql, conn);
            DataTable dt = new DataTable();
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);
            adapter.Fill(dt);
            conn.Close();
            conn.Dispose();
            return dt;
        }
        public DataTable LayThongTinPhieuThu()
        {
            SqlCommand cmd = new SqlCommand("Select * from PhieuThu WHERE IDCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            return datatabase.getData(cmd);
        }
        public DataTable KieuNhapXe()
        {
            SqlCommand cmd = new SqlCommand("Select * from KieuNhapXe");
            return datatabase.getData(cmd);
        }
    }
}
