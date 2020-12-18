using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoCareV2._0.Class;

namespace AutoCareV2._0
{
    public partial class frmThongKeTheoTho : Form
    {
        public frmThongKeTheoTho()
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
            //frmThongTinBindingSource.DataSource = dtThongTin;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateTimeInputDenNgay.ValueObject == null || dateTimeInputTuNgay.ValueObject == null)
                {
                    MessageBox.Show("Bạn cần nhập đủ thời gian thống kê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimeInputDenNgay.Focus();

                    return;
                }
                if (dateTimeInputTuNgay.Value.CompareTo(dateTimeInputDenNgay.Value.AddSeconds(1)) > 0)
                {
                    MessageBox.Show("Thời gian tìm kiếm không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dateTimeInputTuNgay.Focus();

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
                //check tat
                if (!chkTheotho.Checked)
                {
                    SqlCommand cmd = new SqlCommand("sp_ThongTinTienVon2");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TuNgay", dateTimeInputTuNgay.Value);
                    cmd.Parameters.AddWithValue("@DenNgay", dateTimeInputDenNgay.Value);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    DataTable dtTienVon = Class.datatabase.getData(cmd);

                    cmd.CommandText = "sp_ThongKePhuTungBaoDuongTheoThoNew_vietlong";
                    DataTable dtBaoDuong = Class.datatabase.getData(cmd);
                    //bang Bao Duong gom co cac cot TenPhuTung,Soluong,GiaTien,TenTho,LoaiSua

                    //dtBaoDuong.Columns.Add("TienVon", typeof(decimal));
                    //dtBaoDuong.Columns.Add("BienSo", typeof(string));
                    dtBaoDuong.Columns.Add("SoKhung", typeof(string));
                    dtBaoDuong.Columns.Add("GioVaoRa", typeof(string));
                    //them vao bang Bao Duong 4 cot tren.

                    cmd.CommandText = "sp_ThongTinCongThoTheoTho";
                    DataTable dtCongTho = Class.datatabase.getData(cmd);
                    foreach (DataRow r in dtCongTho.Rows)
                    {
                        DataRow newrow = dtBaoDuong.NewRow();
                        newrow["TenPhuTung"] = Convert.ToString(r["NoiDungBD"]);
                        newrow["TenTho"] = Convert.ToString(r["TenTho"]);
                        newrow["TienVon"] = r["TienCong"];
                        newrow["GiaTien"] = r["TienKhachTra"];
                        newrow["SoLuong"] = 1;
                        newrow["LoaiSua"] = "Sửa lẻ";
                        newrow["BienSo"] = r["BienSo"];
                        newrow["SoKhung"] = r["Sokhung"];
                        newrow["GioVaoRa"] = "-" + DateTime.Parse(r["NgayBaoDuong"].ToString()).ToString("dd/MM/yyy HH:mm") + "\r\n" + "-" + DateTime.Parse(r["NgayGiaoXe"].ToString()).ToString("dd/MM/yyy HH:mm");
                        dtBaoDuong.Rows.Add(newrow);
                    }
                    //them sua ngoai theo tung tho
                    SqlCommand cpp = new SqlCommand();
                    cpp.CommandText = "select CongViec,TienThueNgoai,TienLai,TienLayCuaKH,MaTho +'.' + tenTho as TenTho, LichSuBaoDuongXe.BienSo, LichSuBaoDuongXe.Sokhung, LichSuBaoDuongXe.NgayBaoDuong, LichSuBaoDuongXe.NgayGiaoXe from LichSuBaoDuongXe"
                                        + " inner join ThueNgoai on ThueNgoai.IdBaoDuong = LichSuBaoDuongXe.IdBaoDuong"
                                        + " inner join ThoDichVu On ThoDichVu.IdTho = ThueNgoai.IdTho"
                                        + " Where convert(date,LichSuBaoDuongXe.NgayGiaoXe) between convert(date,@TuNgay) And convert(date,@DenNgay) And LichSuBaoDuongXe.IdCongty = @IdCongTy"
                                        + " order by TenTho";
                    cpp.Parameters.AddWithValue("@TuNgay", dateTimeInputTuNgay.Value);
                    cpp.Parameters.AddWithValue("@DenNgay", dateTimeInputDenNgay.Value);
                    cpp.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    DataTable dtngoai = new DataTable();
                    dtngoai = Class.datatabase.getData(cpp);
                    foreach (DataRow r in dtngoai.Rows)
                    {
                        DataRow newrow = dtBaoDuong.NewRow();
                        newrow["TenPhuTung"] = Convert.ToString(r["CongViec"]);
                        newrow["TenTho"] = Convert.ToString(r["TenTho"]);
                        newrow["TienVon"] = r["TienLai"];
                        newrow["GiaTien"] = Convert.ToDecimal(r["TienLayCuaKH"].ToString()) - Convert.ToDecimal(r["TienThueNgoai"].ToString());
                        newrow["SoLuong"] = 1;
                        newrow["LoaiSua"] = "Thuê ngoài";
                        newrow["BienSo"] = r["BienSo"];
                        newrow["SoKhung"] = r["Sokhung"];
                        newrow["GioVaoRa"] = "-" + DateTime.Parse(r["NgayBaoDuong"].ToString()).ToString("dd/MM/yyy HH:mm") + "\r\n" + "-" + DateTime.Parse(r["NgayGiaoXe"].ToString()).ToString("dd/MM/yyy HH:mm");
                        dtBaoDuong.Rows.Add(newrow);
                    }

                    //foreach (DataRow r in dtBaoDuong.Rows)
                    //{
                    //    DataRow[] rArray = dtTienVon.Select("IdPT = '" + Convert.ToString(r["IdPhuTung"]) + "'");
                    //    if (rArray.Length > 0)
                    //    {
                    //        r["TienVon"] = Convert.ToDecimal(rArray[0]["Gia"]) * Convert.ToInt32(r["SoLuong"]);
                    //    }
                    //}
                    //*******************************************
                    //foreach (DataRow r in dtBaoDuong.Rows)
                    //{
                    //    SqlCommand cmd1 = new SqlCommand("select TOP 1 TienCongTraChoTho FROM PhuTung WHERE IdPT= @idpt ");
                    //    cmd1.CommandType = CommandType.Text;
                    //    if (!r["IdPhuTung"].ToString().Equals(""))
                    //    {
                    //        cmd1.Parameters.AddWithValue("@idpt", Convert.ToInt32(r["IdPhuTung"]));
                    //        DataTable dtTienCongTraTho = Class.datatabase.getData(cmd1);
                    //        if (dtTienCongTraTho.Rows.Count > 0 && (!dtTienCongTraTho.Rows[0]["TienCongTraChoTho"].ToString().Equals("")))
                    //        {
                    //            r["TienVon"] = Convert.ToDecimal(dtTienCongTraTho.Rows[0]["TienCongTraChoTho"]);
                    //        }
                    //    }
                    //}
                    //*********************************************
                    frmThongKeBaoDuongTheoKhachHangBindingSource.DataSource = dtBaoDuong;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("sp_ThongTinTienVon2");
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@TuNgay", dateTimeInputTuNgay.Value);
                    cmd.Parameters.AddWithValue("@DenNgay", dateTimeInputDenNgay.Value);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    DataTable dtTienVon = Class.datatabase.getData(cmd);

                    cmd.CommandText = "sp_ThongKePhuTungBaoDuongTheoTho2_vietlong";

                    if (cboThongtintho.SelectedValue != null)
                    {
                        cmd.Parameters.AddWithValue("@IdTho", cboThongtintho.SelectedValue.ToString());
                    }
                    else
                    {
                        MessageBox.Show("Bạn chưa chọn Thợ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboThongtintho.Focus();

                        return;
                    }

                    DataTable dtBaoDuong = Class.datatabase.getData(cmd);

                    dtBaoDuong.Columns.Add("TienVon", typeof(decimal));
                    //dtBaoDuong.Columns.Add("BienSo", typeof(string));
                    dtBaoDuong.Columns.Add("SoKhung", typeof(string));
                    dtBaoDuong.Columns.Add("GioVaoRa", typeof(string));

                    cmd.CommandText = "sp_ThongTinCongThoTheoTho2";
                    DataTable dtCongTho = Class.datatabase.getData(cmd);
                    foreach (DataRow r in dtCongTho.Rows)
                    {
                        DataRow newrow = dtBaoDuong.NewRow();
                        newrow["TenPhuTung"] = Convert.ToString(r["NoiDungBD"]);
                        newrow["TenTho"] = Convert.ToString(r["TenTho"]);
                        newrow["TienVon"] = r["TienCong"];
                        newrow["GiaTien"] = r["TienKhachTra"];
                        newrow["SoLuong"] = 1;
                        newrow["LoaiSua"] = "Sửa lẻ";
                        newrow["BienSo"] = r["BienSo"];
                        newrow["SoKhung"] = r["Sokhung"];
                        newrow["GioVaoRa"] = "-" + DateTime.Parse(r["NgayBaoDuong"].ToString()).ToString("dd/MM/yyy HH:mm") + "\r\n" + "-" + DateTime.Parse(r["NgayGiaoXe"].ToString()).ToString("dd/MM/yyy HH:mm");
                        dtBaoDuong.Rows.Add(newrow);
                    }
                    //sua theo tung tho
                    SqlCommand cpp = new SqlCommand();
                    cpp.CommandText = "select CongViec,TienThueNgoai,TienLai,TienLayCuaKH,MaTho +'.' + tenTho as TenTho, LichSuBaoDuongXe.BienSo, LichSuBaoDuongXe.Sokhung, LichSuBaoDuongXe.NgayBaoDuong, LichSuBaoDuongXe.NgayGiaoXe from LichSuBaoDuongXe"
                                        + " inner join ThueNgoai on ThueNgoai.IdBaoDuong = LichSuBaoDuongXe.IdBaoDuong"
                                        + " inner join ThoDichVu On ThoDichVu.IdTho = ThueNgoai.IdTho"
                                        + " Where Convert(date,LichSuBaoDuongXe.NgayGiaoXe) between Convert(date,@TuNgay) And Convert(date,@DenNgay) And LichSuBaoDuongXe.IdCongty = @IdCongTy And ThueNgoai.IdTho=@IdTho"
                                        + " order by TenTho";
                    cpp.Parameters.AddWithValue("@TuNgay", dateTimeInputTuNgay.Value);
                    cpp.Parameters.AddWithValue("@DenNgay", dateTimeInputDenNgay.Value);
                    cpp.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cpp.Parameters.AddWithValue("@IdTho", cboThongtintho.SelectedValue.ToString());

                    DataTable dtngoai = new DataTable();
                    dtngoai = Class.datatabase.getData(cpp);
                    foreach (DataRow r in dtngoai.Rows)
                    {
                        DataRow newrow = dtBaoDuong.NewRow();
                        newrow["TenPhuTung"] = Convert.ToString(r["CongViec"]);
                        newrow["TenTho"] = Convert.ToString(r["TenTho"]);
                        newrow["TienVon"] = r["TienLai"];
                        newrow["GiaTien"] = Convert.ToDecimal(r["TienLayCuaKH"].ToString()) - Convert.ToDecimal(r["TienThueNgoai"].ToString());
                        newrow["SoLuong"] = 1;
                        newrow["LoaiSua"] = "Thuê ngoài";
                        newrow["BienSo"] = r["BienSo"];
                        newrow["SoKhung"] = r["Sokhung"];
                        newrow["GioVaoRa"] = "-" + DateTime.Parse(r["NgayBaoDuong"].ToString()).ToString("dd/MM/yyy HH:mm") + "\r\n" + "-" + DateTime.Parse(r["NgayGiaoXe"].ToString()).ToString("dd/MM/yyy HH:mm");
                        dtBaoDuong.Rows.Add(newrow);
                    }

                    //foreach (DataRow r in dtBaoDuong.Rows)
                    //{
                    //    DataRow[] rArray = dtTienVon.Select("IdPT = '" + Convert.ToString(r["IdPhuTung"]) + "'");
                    //    if (rArray.Length > 0)
                    //    {
                    //        r["TienVon"] = Convert.ToDecimal(rArray[0]["Gia"]) * Convert.ToInt32(r["SoLuong"]);
                    //    }
                    //}
                    //********************
                    foreach (DataRow r in dtBaoDuong.Rows)
                    {
                        SqlCommand cmd1 = new SqlCommand("select TOP 1 TienCongTraChoTho FROM PhuTung WHERE IdPT= @idpt ");
                        cmd1.CommandType = CommandType.Text;
                        if (!r["IdPhuTung"].ToString().Equals(""))
                        {
                            cmd1.Parameters.AddWithValue("@idpt", Convert.ToInt32(r["IdPhuTung"]));
                            DataTable dtTienCongTraTho = Class.datatabase.getData(cmd1);
                            if (dtTienCongTraTho.Rows.Count > 0 && (!dtTienCongTraTho.Rows[0]["TienCongTraChoTho"].ToString().Equals("")))
                            {
                                r["TienVon"] = Convert.ToDecimal(dtTienCongTraTho.Rows[0]["TienCongTraChoTho"]);
                            }
                        }
                    }
                    //***********************
                    frmThongKeBaoDuongTheoKhachHangBindingSource.DataSource = dtBaoDuong;
                }
                showthongtincongty();
                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo");
            }
        }

        private void frmThongKeTheoTho_FormClosing(object sender, FormClosingEventArgs e)
        {
            reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }

        private void frmThongKeTheoTho_Load_1(object sender, EventArgs e)
        {
            dateTimeInputDenNgay.Value = DateTime.Now;
            dateTimeInputTuNgay.Value = DateTime.Now;
            SqlCommand cm = new SqlCommand("select  IdTho,tenTho  from ThoDichVu where IdCongTy=@IdCongTy");
            cm.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            DataTable dt = Class.datatabase.getData(cm);
            cboThongtintho.DataSource = dt;
            cboThongtintho.DisplayMember = "tenTho";
            cboThongtintho.ValueMember = "IdTho";
            showthongtincongty();
            this.reportViewer1.RefreshReport();
        }
    }
}