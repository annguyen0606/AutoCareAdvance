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
    public partial class frmCapNhatThongTinXe : Form
    {
        public delegate void ReLoadDanhSachXe();
        public ReLoadDanhSachXe ReloadXe;
        public string IdKey = "";
        public string TenXe = "";
        public string MauXe = "";
        public string LoaiXe = "";
        public string SoKhung = "";
        public string SoMay = "";
        public bool fromHoaDonNhapXe = false;

        public frmCapNhatThongTinXe()
        {
            InitializeComponent();
        }

        private void frmCapNhatThongTinXe_Load(object sender, EventArgs e)
        {
            LoadMauXe();
            LoadLoaiXe();

            try
            {
                if (IdKey.Trim().Length > 0)
                    txtMaXe.Text = IdKey;
                if (TenXe.Trim().Length > 0)
                    txtTenXe.Text = TenXe;
                if (MauXe.Trim().Length > 0 && !fromHoaDonNhapXe)
                    cboMauXe.SelectedValue = Convert.ToInt32(MauXe);
                if (MauXe.Trim().Length > 0 && fromHoaDonNhapXe)
                    cboMauXe.Text = MauXe;
                if (LoaiXe.Trim().Length > 0 && !fromHoaDonNhapXe)
                    cboLoaiXe.SelectedValue = Convert.ToInt32(LoaiXe);
                if (LoaiXe.Trim().Length > 0 && fromHoaDonNhapXe)
                    cboLoaiXe.Text = LoaiXe;
                if (SoKhung.Trim().Length > 0)
                    txtSoKhung.Text = SoKhung;
                if (SoMay.Trim().Length > 0)
                    txtSoMay.Text = SoMay;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }            
        }

        private void LoadMauXe()
        {
            SqlCommand cmd = new SqlCommand("select IdMauXe, TenMauXe, GhiChu from MauXeMay Where IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            cboMauXe.DataSource = Class.datatabase.getData(cmd);
            cboMauXe.ValueMember = "IdMauXe";
            cboMauXe.DisplayMember = "TenMauXe";
        }

        private void LoadLoaiXe()
        {
            SqlCommand cmd = new SqlCommand("select IdLoaiXe, TenLoaiXe, GhiChu from LoaiXe Where IdCongTy = @IdcongTy");
            cmd.Parameters.AddWithValue("@idCongTy", Class.CompanyInfo.idcongty);

            cboLoaiXe.DataSource = Class.datatabase.getData(cmd);
            cboLoaiXe.ValueMember = "IdLoaiXe";
            cboLoaiXe.DisplayMember = "TenLoaiXe";
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(txtTenXe.Text.Trim().Length < 0)
            {
                MessageBox.Show("Bạn chưa nhập vào tên xe!");
                txtTenXe.Focus();
                return;
            }

            if (cboMauXe.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa chọn vào màu xe!");
                cboMauXe.Focus();
                return;
            }

            if (cboLoaiXe.Text.Trim().Length < 0)
            {
                MessageBox.Show("Bạn chưa chọn vào loại xe!");
                cboLoaiXe.Focus();
                return;
            }

            if (txtSoKhung.Text.Trim().Length < 0)
            {
                MessageBox.Show("Bạn chưa nhập vào số khung!");
                txtSoKhung.Focus();
                return;
            }

            if (txtSoMay.Text.Trim().Length < 0)
            {
                MessageBox.Show("Bạn chưa nhập vào số máy!");
                txtSoMay.Focus();
                return;
            }

            try
            {
                if (!fromHoaDonNhapXe)
                {
                    bool isHave = false;
                    //Check exists
                    if (txtSoKhung.Text.Trim() != SoKhung || txtSoMay.Text.Trim() != SoMay)
                    {
                        SqlCommand cmdCheck = new SqlCommand("select count(*) from ChiTietXe where IdCongTy = @IdCongTy and SoKhung = @SoKhung and SoMay = @SoMay and IdKey = @IdKey");
                        cmdCheck.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmdCheck.Parameters.AddWithValue("@SoKhung", txtSoKhung.Text);
                        cmdCheck.Parameters.AddWithValue("@SoMay", txtSoMay.Text);
                        cmdCheck.Parameters.AddWithValue("@IdKey", IdKey);

                        if (Convert.ToInt32(Class.datatabase.ExecuteScalar(cmdCheck)) > 0)
                            isHave = true;
                    }

                    if (isHave)
                    {
                        MessageBox.Show("Số khung và số máy đã tồn tại! Vui lòng kiểm tra lại!");
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("update ChiTietXe set IdMauXe = @IdMauXe, IdLoaiXe = @IdLoaiXe, SoKhung = @SoKhung, SoMay = @SoMay where IdCongTy=@IdCongTy and IdKey = @IdKey and SoKhung = @SoKhungTruoc and SoMay = @SoMayTruoc");
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdMauXe", cboMauXe.SelectedValue);
                    cmd.Parameters.AddWithValue("@IdLoaiXe", cboLoaiXe.SelectedValue);
                    cmd.Parameters.AddWithValue("@SoKhung", txtSoKhung.Text);
                    cmd.Parameters.AddWithValue("@SoMay", txtSoMay.Text);
                    cmd.Parameters.AddWithValue("@IdKey", IdKey);
                    cmd.Parameters.AddWithValue("@SoKhungTruoc", SoKhung);
                    cmd.Parameters.AddWithValue("@SoMayTruoc", SoMay);
                    Class.datatabase.ExcuteNonQuery(cmd);

                    MessageBox.Show("Cập nhật thông tin xe thành công!");
                    this.Close();
                }
                else
                {
                    bool isHave = false;
                    //Check exists
                    if (txtSoKhung.Text.Trim() != SoKhung || txtSoMay.Text.Trim() != SoMay)
                    {
                        SqlCommand cmdCheck = new SqlCommand("select count(*) from ChiTietNhapXe where IdCongTy = @IdCongTy and SoKhung = @SoKhung and SoMay = @SoMay and IdXe = @IdXe");
                        cmdCheck.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmdCheck.Parameters.AddWithValue("@SoKhung", txtSoKhung.Text);
                        cmdCheck.Parameters.AddWithValue("@SoMay", txtSoMay.Text);
                        cmdCheck.Parameters.AddWithValue("@IdXe", IdKey);

                        if (Convert.ToInt32(Class.datatabase.ExecuteScalar(cmdCheck)) > 0)
                            isHave = true;
                    }

                    if (isHave)
                    {
                        MessageBox.Show("Số khung và số máy đã tồn tại! Vui lòng kiểm tra lại!");
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("update ChiTietNhapXe set IdMauXe = @IdMauXe, IdLoaiXe = @IdLoaiXe, SoKhung = @SoKhung, SoMay = @SoMay where IdCongTy=@IdCongTy and IdXe = @IdXe and SoKhung = @SoKhungTruoc and SoMay = @SoMayTruoc");
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdMauXe", cboMauXe.SelectedValue);
                    cmd.Parameters.AddWithValue("@IdLoaiXe", cboLoaiXe.SelectedValue);
                    cmd.Parameters.AddWithValue("@SoKhung", txtSoKhung.Text);
                    cmd.Parameters.AddWithValue("@SoMay", txtSoMay.Text);
                    cmd.Parameters.AddWithValue("@IdXe", IdKey);
                    cmd.Parameters.AddWithValue("@SoKhungTruoc", SoKhung);
                    cmd.Parameters.AddWithValue("@SoMayTruoc", SoMay);
                    Class.datatabase.ExcuteNonQuery(cmd);

                    SqlCommand cmdUpdateXeTrongKho = new SqlCommand("update ChiTietXe set IdMauXe = @IdMauXe, IdLoaiXe = @IdLoaiXe, SoKhung = @SoKhung, SoMay = @SoMay where IdCongTy=@IdCongTy and IdKey = @IdKey and SoKhung = @SoKhungTruoc and SoMay = @SoMayTruoc");
                    cmdUpdateXeTrongKho.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmdUpdateXeTrongKho.Parameters.AddWithValue("@IdMauXe", cboMauXe.SelectedValue);
                    cmdUpdateXeTrongKho.Parameters.AddWithValue("@IdLoaiXe", cboLoaiXe.SelectedValue);
                    cmdUpdateXeTrongKho.Parameters.AddWithValue("@SoKhung", txtSoKhung.Text);
                    cmdUpdateXeTrongKho.Parameters.AddWithValue("@SoMay", txtSoMay.Text);
                    cmdUpdateXeTrongKho.Parameters.AddWithValue("@IdKey", IdKey);
                    cmdUpdateXeTrongKho.Parameters.AddWithValue("@SoKhungTruoc", SoKhung);
                    cmdUpdateXeTrongKho.Parameters.AddWithValue("@SoMayTruoc", SoMay);
                    Class.datatabase.ExcuteNonQuery(cmdUpdateXeTrongKho);

                    MessageBox.Show("Cập nhật thông tin xe thành công!");
                    this.Close();
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Cập nhật thông tin xe không thành công! Lỗi: " + ex.Message);
            }            
        }

        private void frmCapNhatThongTinXe_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (ReloadXe != null)
                ReloadXe();
        }
    }
}
