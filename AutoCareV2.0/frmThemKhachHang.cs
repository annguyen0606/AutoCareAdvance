using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoCareV2._0.Class;

namespace AutoCareV2._0
{
    public partial class frmThemKhachHang : Form
    {
        public frmThemKhachHang()
        {
            InitializeComponent();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if ( String.IsNullOrEmpty(txtTenKH.Text))
            {
                MessageBox.Show(@"Họ và tên khách hàng phải nhập đủ.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoKH.Focus();

                return;
            }

            if (String.IsNullOrEmpty(cboLoaiKH.Text))
            {
                MessageBox.Show(@"Bạn chưa chọn Loại khách hàng!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboLoaiKH.Focus();

                return;
            }
            if (String.IsNullOrEmpty(txtDienThoai.Text))
            {
                MessageBox.Show(@"Số điện thoại không được để trống.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoKH.Focus();

                return;
            }
            KhachHang.idkhachhang = "";
            using (var con = datatabase.getConnection())
            {
                var cmd =
                    new SqlCommand(
                        "insert into KhachHang(IdCongTy,HoKH,TenKH,GioiTinh,NgaySinh,DienThoai,MaNhomKH,DiaChi,NgheNghiep,SoSBH,LoaiKH) Values(@IdCongTy,@HoKH,@TenKH,@GioiTinh,@NgaySinh,@DienThoai,@MaNhomKH,@DiaChi,@NgheNghiep,@SoSBH,@LoaiKH) select @@Identity",
                        con);
                try
                {
                    if(con.State != ConnectionState.Open)
                        con.Open();

                    cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@HoKH", txtHoKH.Text);
                    cmd.Parameters.AddWithValue("@TenKH", txtTenKH.Text);
                    cmd.Parameters.AddWithValue("@GioiTinh", cboGioiTinh.Text);
                    if (dateTimeInputNgaySinh.ValueObject == null)
                    {
                        cmd.Parameters.AddWithValue("@NgaySinh", SqlDateTime.Null);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@NgaySinh", dateTimeInputNgaySinh.Value);
                    }
                    cmd.Parameters.AddWithValue("@DienThoai", txtDienThoai.Text);

                    if (cboNhomKH.SelectedValue != null)
                    {
                        cmd.Parameters.AddWithValue("@MaNhomKH", cboNhomKH.SelectedValue);
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("@MaNhomKH", 0);
                    }

                    string diaChi = "";

                    if (txtSoNha.Text.Trim().Length > 0)
                        diaChi = diaChi + txtSoNha.Text.Trim();
                    if (txtPhuongXa.Text.Trim().Length > 0)
                    {
                        if (!string.IsNullOrEmpty(diaChi))
                        {
                            diaChi = diaChi + "," + txtPhuongXa.Text.Trim();
                        }
                        else
                        {
                            diaChi=diaChi+ txtPhuongXa.Text.Trim();
                        }
                       
                    }
                    if (txtQuanHuyen.Text.Trim().Length > 0)
                    {
                        if (!string.IsNullOrEmpty(diaChi))
                        {
                            diaChi = diaChi + "," + txtQuanHuyen.Text.Trim();
                        }
                        else
                        {
                            diaChi = diaChi + txtQuanHuyen.Text.Trim();
                        }
                       
                    }

                    if (txtTinhTp.Text.Trim().Length > 0)
                    {
                        if (!string.IsNullOrEmpty(diaChi))
                        {
                            diaChi = diaChi + "," + txtTinhTp.Text.Trim();
                        }
                        else
                        {
                            diaChi=diaChi+ txtTinhTp.Text.Trim();
                        }
                      
                    }
                    cmd.Parameters.AddWithValue("@DiaChi", diaChi);
                    cmd.Parameters.AddWithValue("@NgheNghiep", txtNgheNghiep.Text);
                    cmd.Parameters.AddWithValue("@SoSBH", txtSosoBH.Text);
                    cmd.Parameters.AddWithValue("@LoaiKH", cboLoaiKH.SelectedIndex + 1);

                    var idKhachHang = cmd.ExecuteScalar().ToString();
                    KhachHang.idkhachhang = idKhachHang;

                    //Thêm địa chỉ khách hàng
                    var sqlInsertAddressCus =
                        new SqlCommand(
                            "insert into tblDiaChiKhachHang(IdKhachHang,SoNha,Phuong_Xa,Quan_Huyen,Tinh) values(@idKhachHang,@soNha,@phuongXa,@quanHuyen,@tinhTp",
                            con);
                    sqlInsertAddressCus.Parameters.AddWithValue("@idKhachHang", idKhachHang);
                    sqlInsertAddressCus.Parameters.AddWithValue("@soNha", txtSoNha.Text.Trim());
                    sqlInsertAddressCus.Parameters.AddWithValue("@phuongXa", txtPhuongXa.Text.Trim());
                    sqlInsertAddressCus.Parameters.AddWithValue("@quanHuyen", txtQuanHuyen.Text.Trim());
                    sqlInsertAddressCus.Parameters.AddWithValue("@tinhTp", txtTinhTp.Text.Trim());

                    MessageBox.Show(@"Thêm khách hàng mới thành công.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Thêm khách hàng mới không thành công. " + ex.Message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private DataTable dtNhomKhachHang = new DataTable();

        private void frmThemKhachHang_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select MaNhomKH,TenNhomKH From NhomKhachHang Where IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            dtNhomKhachHang = datatabase.getData(cmd);
            cboNhomKH.DataSource = dtNhomKhachHang;
            cboNhomKH.ValueMember = "MaNhomKH";
            cboNhomKH.DisplayMember = "TenNhomKh";
        }

        private void frmThemKhachHang_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }
    }
}