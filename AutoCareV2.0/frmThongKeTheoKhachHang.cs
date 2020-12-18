using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using AutoCareV2._0.Class;
using  System.Linq;

namespace AutoCareV2._0
{
    public partial class frmThongKeTheoKhachHang : Form
    {
        public frmThongKeTheoKhachHang()
        {
            InitializeComponent();
        }
        private void showthongtincongty()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from CongTy where IdCongTy=" + Class.CompanyInfo.idcongty;
            DataTable dtThongTin = new DataTable();
            dtThongTin = Class.datatabase.getData(cmd);
            Microsoft.Reporting.WinForms.ReportDataSource data3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", dtThongTin);
            reportViewer1.LocalReport.DataSources.Add(data3);
        }
        private void frmThongKeTheoKhachHang_Load(object sender, EventArgs e)
        {
            dateTimeInputDenNgay.Value = DateTime.Now;
            dateTimeInputTuNgay.Value = DateTime.Now;

            showthongtincongty();
            LoadDataCuaHang(CompanyInfo.idcongty);

            this.reportViewer1.RefreshReport();
        }

        private void LoadDataCuaHang(string idCongTy)
        {
            using (SqlCommand cmd = new SqlCommand("select * from CuaHang where IdCongTy = @IdCongTy"))
            {
                cmd.Parameters.AddWithValue("@IdCongTy", idCongTy);
                DataTable tblCuaHang = datatabase.getData(cmd);

                cbbCuaHang.ValueMember = "IdCuaHang";
                cbbCuaHang.DisplayMember = "TenCuaHang"; 

                DataRow dr = tblCuaHang.NewRow();
                dr["IdCuaHang"] = 0;
                dr["TenCuaHang"] = "---Tất cả---";
                tblCuaHang.Rows.InsertAt(dr, 0);

                cbbCuaHang.DataSource = tblCuaHang;
                cbbCuaHang.SelectedIndex = 0;
            }
        }

        private void frmThongKeTheoKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
            
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (dateTimeInputDenNgay.ValueObject == null || dateTimeInputTuNgay.ValueObject == null)
            {
                MessageBox.Show(@"Bạn cần nhập đủ thời gian thống kê.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dateTimeInputTuNgay.Value.CompareTo(dateTimeInputDenNgay.Value.AddSeconds(1)) > 0)
            {
                MessageBox.Show(@"Thời gian tìm kiếm không hợp lệ.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            DataTable dtThoigian = new DataTable();
            DataRow rows = dtThoigian.NewRow();
            dtThoigian.Columns.Add("TuNgay", typeof(DateTime));
            dtThoigian.Columns.Add("DenNgay", typeof(DateTime));
            rows["TuNgay"] = dateTimeInputTuNgay.Value;
            rows["DenNgay"] = dateTimeInputDenNgay.Value;
            dtThoigian.Rows.Add(rows);
            ThoiGianBindingSource.DataSource = dtThoigian;

            SqlCommand cmd = new SqlCommand("sp_ThongTinTienVon2");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TuNgay", dateTimeInputTuNgay.Value);
            cmd.Parameters.AddWithValue("@DenNgay", dateTimeInputDenNgay.Value);
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            DataTable dtTienVon = Class.datatabase.getData(cmd);
       
            cmd.CommandText = "sp_ThongKePhuTungBaoDuong";
            DataTable dtBaoDuong = Class.datatabase.getData(cmd);

            dtBaoDuong.Columns.Add("TienVon", typeof(decimal));
            cmd.CommandText = "sp_ThongTinCongTho";
            DataTable dtCongTho = Class.datatabase.getData(cmd);
            foreach (DataRow r in dtCongTho.Rows)
            {
                DataRow newrow = dtBaoDuong.NewRow();
                newrow["TenPhuTung"] = Convert.ToString(r["NoiDungBD"]);
                newrow["BienSo"] = Convert.ToString(r["BienSo"]);
                newrow["TienVon"] = r["TienCong"];
                newrow["GiaTien"] = r["TienKhachTra"];
                newrow["SoLuong"] = 1;
                newrow["TienLai"] = r["TienLai"];
                newrow["IdCuaHang"] = r["IdCuaHang"];
                dtBaoDuong.Rows.Add(newrow);
            }

            SqlCommand cpp = new SqlCommand
            {
                CommandText =
                    @"select BienSo + '.' + TenXe as BienSo, CongViec,TienThueNgoai,TienLayCuaKH,ISNULL(PhanTramGiam,0) as TienLai, LichSuBaoDuongXe.IdCuaHang from thuengoai 
                                inner join LichSuBaoDuongXe on thuengoai.IdBaoDuong = LichSuBaoDuongXe.IdBaoDuong 
                                inner join LichSuBaoDuongPhieu on LichSuBaoDuongPhieu.idBaoDuong=LichSuBaoDuongXe.IdBaoDuong 
                                where LichSuBaoDuongXe.IdCongty = @IdCongTy And LichSuBaoDuongXe.NgayGiaoXe between @TuNgay And @DenNgay"
            };
            cpp.Parameters.AddWithValue("@TuNgay", dateTimeInputTuNgay.Value.ToString("yyyy-MM-dd") + " 00:00:00");
                              cpp.Parameters.AddWithValue("@DenNgay", dateTimeInputDenNgay.Value.ToString("yyyy-MM-dd") + " 23:59:59");
                              cpp.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            DataTable dtngoai = new DataTable();
            dtngoai = Class.datatabase.getData(cpp);
            foreach (DataRow r in dtngoai.Rows)
            {
                DataRow newrow = dtBaoDuong.NewRow();
                newrow["TenPhuTung"] = Convert.ToString(r["CongViec"]);
                newrow["BienSo"] = Convert.ToString(r["BienSo"]);
                newrow["TienVon"] = r["TienThueNgoai"];
                newrow["GiaTien"] = r["TienLayCuaKH"];
                newrow["TienLai"] = r["TienLai"];
                newrow["IdCuaHang"] = r["IdCuaHang"];
                dtBaoDuong.Rows.Add(newrow);
            }

            foreach (DataRow r in dtBaoDuong.Rows)
            {
                DataRow[] rArray = dtTienVon.Select("IdPT = '" + Convert.ToString(r["IdPhuTung"]) + "'");
                if (rArray.Length > 0)
                {
                    r["TienVon"] = Convert.ToDecimal(rArray[0]["Gia"]) * Convert.ToInt32(r["SoLuong"]);
                }
            }

            string idCuaHang = cbbCuaHang.SelectedValue.ToString();

            if (idCuaHang != "0")
                dtBaoDuong = (from myRow in dtBaoDuong.AsEnumerable()
                    where myRow.Field<long>("IdCuaHang") == long.Parse(idCuaHang)
                    select myRow).CopyToDataTable();

            frmThongKeBaoDuongTheoKhachHangBindingSource.DataSource = dtBaoDuong;
            showthongtincongty();       
            reportViewer1.RefreshReport();
        }

        private void frmThongTinBindingSource_CurrentChanged(object sender, EventArgs e)
        {

        }
    }
}
