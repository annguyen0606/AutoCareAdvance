using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmPhieuXuatKhoNoiBoVietLong : Form
    {
        public string idXuatKho = "";
        public string dateReal = "";
        public string idKho = "";
        public string lichSu = "";
        public string donCheck = "";
        public frmPhieuXuatKhoNoiBoVietLong()
        {
            InitializeComponent();
        }

        private void FrmPhieuXuatKhoNoiBoVietLong_Load(object sender, EventArgs e)
        {
            if (lichSu.Equals("ls"))
            {
                btnXacNhanXuatKho.Enabled = false;
                btnHuyDon.Enabled = false;
            } else if (donCheck.Equals("dck"))
            {
                btnXacNhanXuatKho.Enabled = false;
            }
            else
            {
                btnHuyDon.Enabled = false;
            }
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"Select TenCuaHang from CuaHang ch inner join LichSuDatPhuTung lsdpt on ch.IdCongTy = lsdpt.IdCongTyNhan where lsdpt.IdXuatKho = @idxuatkho and CONVERT(varchar(25),lsdpt.NgayDat,126)  like @ngaydat";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(idXuatKho));
            cmd.Parameters.AddWithValue("@ngaydat", "%" + dateReal + "%");
            DataTable tableName = Class.datatabase.getData(cmd);

            cmd.CommandText = @"Select TenCuaHang from CuaHang ch inner join LichSuDatPhuTung lsdpt on ch.IdCongTy = lsdpt.IdCongTyXuat where lsdpt.IdXuatKho = @idxuatkho and CONVERT(varchar(25),lsdpt.NgayDat,126)  like @ngaydat";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(idXuatKho));
            cmd.Parameters.AddWithValue("@ngaydat", "%" + dateReal + "%");
            DataTable tableNameXuat = Class.datatabase.getData(cmd);

            if (donCheck.Equals("dck"))
            {
                cmd.CommandText = @"SELECT ROW_NUMBER() OVER (ORDER BY MaPT) AS 'STT', * From dbo.LichSuDatPhuTungChiTiet where IdXuatKho = @idxuatkho";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(idXuatKho));
            }
            else
            {
                cmd.CommandText = @"LayChiTietPhuTungTheoDonDat @idxuatkho,@idcongty";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(idXuatKho));
                cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            }
            DataTable dsPhuTung = Class.datatabase.getData(cmd);
            object SumMoney;
            if (dsPhuTung.Rows.Count > 0)
            {
                SumMoney = dsPhuTung.Compute("Sum(TongTien)", "");
            }
            else
            {
                SumMoney = 0;   
            }
            if (lichSu.Equals("ls"))
            {
                cmd.CommandText = @"select * from dbo.LichSuDatPhuTung where IdXuatKho = @idxuatkho and IdCongTyXuat = @idcongty";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(idXuatKho));
                cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                DataTable dsPhuTung1 = Class.datatabase.getData(cmd);

                string ngayGioXuatKho = dsPhuTung1.Rows[0]["NgayXuat"].ToString().Trim();
                string[] arrayNgayGioXuatKho = ngayGioXuatKho.Split(' ');
                string[] arrayNgayXuatKho = arrayNgayGioXuatKho[0].Split('/');
                Microsoft.Reporting.WinForms.ReportParameter[] rPrams = new Microsoft.Reporting.WinForms.ReportParameter[]
                {
                    new Microsoft.Reporting.WinForms.ReportParameter("GioXuatKho",arrayNgayGioXuatKho[1].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("NgayXuatKho", arrayNgayXuatKho[2].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("ThangXuatKho", arrayNgayXuatKho[1].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("NamXuatKho", arrayNgayXuatKho[0].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("KhoXuat", tableNameXuat.Rows[0]["TenCuaHang"].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("KhoNhap", tableName.Rows[0]["TenCuaHang"].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("TongTien", string.Format("{0:#,#.}", Decimal.Parse(SumMoney.ToString()))),
                };
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = dsPhuTung;

                reportViewer1.LocalReport.DataSources.Clear();

                reportViewer1.LocalReport.SetParameters(rPrams);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.RefreshReport();
                this.reportViewer1.RefreshReport();
            }
            else
            {
                Microsoft.Reporting.WinForms.ReportParameter[] rPrams = new Microsoft.Reporting.WinForms.ReportParameter[]
                {
                    new Microsoft.Reporting.WinForms.ReportParameter("GioXuatKho",DateTime.Now.ToString("HH:mm:ss")),
                    new Microsoft.Reporting.WinForms.ReportParameter("NgayXuatKho", DateTime.Now.ToString("dd")),
                    new Microsoft.Reporting.WinForms.ReportParameter("ThangXuatKho", DateTime.Now.ToString("MM")),
                    new Microsoft.Reporting.WinForms.ReportParameter("NamXuatKho", DateTime.Now.ToString("yyyy")),
                    new Microsoft.Reporting.WinForms.ReportParameter("KhoXuat", tableNameXuat.Rows[0]["TenCuaHang"].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("KhoNhap", tableName.Rows[0]["TenCuaHang"].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("TongTien", string.Format("{0:#,#.}", Decimal.Parse(SumMoney.ToString()))),
                };
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = dsPhuTung;

                reportViewer1.LocalReport.DataSources.Clear();

                reportViewer1.LocalReport.SetParameters(rPrams);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.RefreshReport();
                this.reportViewer1.RefreshReport();
            }

            
        }

        private void BtnXacNhanXuatKho_Click(object sender, EventArgs e)
        {
            using (SqlConnection myCon = new SqlConnection(Class.datatabase.connect))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myCon;
                if (myCon.State == ConnectionState.Closed)
                {
                    myCon.Open();
                }
                SqlTransaction transaction;
                transaction = myCon.BeginTransaction();
                cmd.Transaction = transaction;
                try
                {
                    cmd.CommandText = @"select *from dbo.LichSuDatPhuTungChiTiet where IdXuatKho = @idxuatkho and IdCongTy = @idcongty";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(idXuatKho));
                    cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dtPhuTungCanTru = new DataTable();
                    adapter.Fill(dtPhuTungCanTru);

                    for(int i = 0; i < dtPhuTungCanTru.Rows.Count; i++)
                    {
                        cmd.CommandText = @"select * from dbo.PhuTung where MaPT = @MaPT and IdKho = @Idkho and IdCongTy = @IdCongTy";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@MaPT", dtPhuTungCanTru.Rows[i]["MaPT"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@IdKho", int.Parse(idKho));
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataTable phuTung = new DataTable();
                        adap.Fill(phuTung);

                        int soLuongTon = int.Parse(phuTung.Rows[0]["SoLuong"].ToString());
                        int soCanTru = soLuongTon - int.Parse(dtPhuTungCanTru.Rows[i]["SoLuong"].ToString().Trim());
                        cmd.CommandText = @"update dbo.PhuTung Set SoLuong = @soluong where MaPT = @MaPT and IdKho = @Idkho and IdCongTy = @IdCongTy";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@soluong", soCanTru);
                        cmd.Parameters.AddWithValue("@MaPT", dtPhuTungCanTru.Rows[i]["MaPT"].ToString().Trim());
                        cmd.Parameters.AddWithValue("@IdKho", int.Parse(idKho));
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = @"update dbo.LichSuDatPhuTung SET TrangThaiXacNhan = 1, NgayXuat = @ngayxuat where IdXuatKho = @idxuatkho";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(idXuatKho));
                        cmd.Parameters.AddWithValue("@ngayxuat", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    MessageBox.Show("Xác nhận thành công","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                catch
                {
                    transaction.Rollback();
                }
              
            }
        }

        private void BtnHuyDon_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"delete from dbo.LichSuDatPhuTung where IdXuatKho = @idxuatkho";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(idXuatKho));
            if(Class.datatabase.ExcuteNonQuery(cmd) > 0)
            {
                cmd.CommandText = @"delete from dbo.LichSuDatPhuTungChiTiet where IdXuatKho = @idxuatkho";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(idXuatKho));
                if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                {
                    MessageBox.Show("Hủy đơn thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Hủy đơn thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Hủy đơn thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
