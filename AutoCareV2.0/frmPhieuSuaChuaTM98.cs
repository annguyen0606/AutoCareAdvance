using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmPhieuSuaChuaTM98 : Form
    {
        SqlConnection con;
        string cn = Class.datatabase.connect;
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

        public frmPhieuSuaChuaTM98()
        {
            InitializeComponent();
        }

        private void frmPhieuSuaChuaTM98_Load(object sender, EventArgs e)
        {

            try
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
                cmd.CommandText = "sp_Report_ThoDichVu_TienCong3";
                da.Fill(dtTienCong);              

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
                DataColumn cl1 = new DataColumn("NoiDungBD");
                DataColumn cl2 = new DataColumn("TienCong");
                cl2.DataType = typeof(decimal);
                dttiengiam.Columns.Add(cl1);
                dttiengiam.Columns.Add(cl2);

                if (dtTienCong.Rows.Count > 0)
                {
                    foreach (DataRow rows in dtTienCong.Rows)
                    {
                        if (dttiengiam.Rows.Count > index)
                        {
                            dttiengiam.Rows[index]["TienCong"] = rows["TienKhachTra"];
                            dttiengiam.Rows[index]["NoiDungBD"] = rows["NoiDungBD"];
                            index += 1;
                        }
                        else
                        {
                            DataRow drnew = dttiengiam.NewRow();
                            drnew["TienCong"] = rows["TienKhachTra"].ToString();
                            drnew["NoiDungBD"] = rows["NoiDungBD"].ToString();
                            dttiengiam.Rows.Add(drnew);
                            index++;
                        }
                    }
                }

                if (dtngoai.Rows.Count > 0)
                {
                    foreach (DataRow rows in dtngoai.Rows)
                    {
                        if (dttiengiam.Rows.Count > index)
                        {
                            dttiengiam.Rows[index]["TienCong"] = rows["TienLayCuaKH"];
                            dttiengiam.Rows[index]["NoiDungBD"] = rows["CongViec"];

                            index += 1;
                        }
                        else
                        {
                            DataRow drnew = dttiengiam.NewRow();
                            drnew["TienCong"] = rows["TienLayCuaKH"].ToString();
                            drnew["NoiDungBD"] = rows["CongViec"].ToString();

                            dttiengiam.Rows.Add(drnew);
                            index++;
                        }
                    }
                }

                #region
                SqlCommand cmdthosua = new SqlCommand();
                cmdthosua.CommandText = @"SELECT DISTINCT ThoDichVu.tenTho FROM ThoDichVu
                                        LEFT JOIN ThoDichVu_GioViec ON ThoDichVu_GioViec.IdTho=ThoDichVu.IdTho
                                        LEFT JOIN ThoDichVu_TienCongTho2 ON ThoDichVu_TienCongTho2.IdTho=ThoDichVu.IdTho
                                        LEFT JOIN LichSuBaoDuongChiTiet2 ON ThoDichVu.IdTho=LichSuBaoDuongChiTiet2.idTho
                                        WHERE ThoDichVu.IdCongTy=@IdCongTy AND (ThoDichVu_GioViec.IdBaoDuong=@IdBaoDuong OR ThoDichVu_TienCongTho2.IdBaoDuong=@IdBaoDuong
                                        OR LichSuBaoDuongChiTiet2.IdBaoDuong=@IdBaoDuong)";
                cmdthosua.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
                cmdthosua.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                DataTable dtthosua = new DataTable();
                dtthosua = Class.datatabase.getData(cmdthosua);
                #endregion

                PhanTramGiamTruBindingSource.DataSource = dttiengiam;

                BaoCaoPhuTungThayTheBindingSource.DataSource = dtPhuTung;
                foreach (DataRow rows in dtPhuTung.Rows)
                {
                    if (!String.IsNullOrEmpty(rows["GiaTien"].ToString()))
                    {
                        tien += Convert.ToDecimal(rows["GiaTien"]);
                    }
                }
                foreach (DataRow rows in dttiengiam.Rows)
                {
                    if (!String.IsNullOrEmpty(rows["TienCong"].ToString()))
                    {
                        tien += Convert.ToDecimal(rows["TienCong"]);
                    }
                }

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
                DocTienBindingSource.DataSource = dtTien;
                //them thong tin
                DataTable dtthongtin = new DataTable();
                SqlCommand cmo = new SqlCommand();
                cmo.CommandText = "select DiaChi as diachi,DienThoai as didong,DienThoaiBan as dtban,TenLapPhieu as lapphieu,TenQuanLy as quanly,TenCongTy as tencongty from congty where IdCongTy=@idct";
                cmo.Parameters.Clear();
                cmo.Parameters.AddWithValue("@idct", Class.CompanyInfo.idcongty);
                dtthongtin = Class.datatabase.getData(cmo);

                DataColumn thosua = new DataColumn("TenThoSua");
                dtthongtin.Columns.Add(thosua);

                int idx = 0;

                #region
                if (dtthosua.Rows.Count > 0)
                {
                    foreach (DataRow rows in dtthosua.Rows)
                    {
                        if (dtthongtin.Rows.Count > idx)
                        {
                            dtthongtin.Rows[idx]["TenThoSua"] = rows["tenTho"];

                            idx += 1;
                        }
                        else
                        {
                            DataRow drnew = dtthongtin.NewRow();
                            drnew["TenThoSua"] = rows["tenTho"].ToString();

                            dtthongtin.Rows.Add(drnew);
                            idx++;
                        }
                    }
                }
                #endregion

                ThongTinCongTyHienThiBindingSource.DataSource = dtthongtin;

                this.reportViewer1.RefreshReport();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message, "Thông báo"); }
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

        private void frmPhieuSuaChuaTM98_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}
