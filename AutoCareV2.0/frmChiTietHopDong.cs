using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoCareV2._0.Class;
using System.Data.SqlClient;
namespace AutoCareV2._0
{
    public partial class frmChiTietHopDong : Form
    {
        public frmChiTietHopDong()
        {
            InitializeComponent();
        }

        private void dataGridViewX1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            dataGridViewX1.Rows[e.RowIndex].HeaderCell.Value = Convert.ToString(e.RowIndex + 1);
        }
        public string soHopDong = "";
        DataTable dtNganHang = new DataTable();
        DataTable dtChiNhanhNganHang = new DataTable();
        DataTable dtHopDong = new DataTable();
        KhDB classdb = new KhDB();
        private void frmChiTietHopDong_Load(object sender, EventArgs e)
        {
            try
            {
                txtSoHopDong.Text = soHopDong;
                SqlCommand cmd = new SqlCommand("Select * from HopDongTraGop Where IdCongTy = @IdCongTy And SoHopDongTraGop = @SoHopDong");
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@SoHopDong", txtSoHopDong.Text);
                dtHopDong = Class.datatabase.getData(cmd);
                if (dtHopDong.Rows.Count > 0)
                {

                    dtNganHang = classdb.LayDanhSachNganHang();
                    dtChiNhanhNganHang = classdb.LayDanhSachChiNhanhNganHang();
                    dateTimeInputNgaLapHopDong.Value = Convert.ToDateTime(dtHopDong.Rows[0]["NgayLapHopDong"]);
                    cboThoiHanTraGop.SelectedIndex = Convert.ToInt32(dtHopDong.Rows[0]["ThoiGianTraGop"]) - 1;
                    txtLaiSuat.Text = dtHopDong.Rows[0]["LaiSuat"].ToString();
                    txtNgayHenTra.Text = string.Format("{0:dd/MM/yyyy}",dtHopDong.Rows[0]["NgayHenTra"]);
                    txtSoTienTraGop.Text = string.Format("{0:0,0}", dtHopDong.Rows[0]["SoTienTraGop"]);
                    txtNguoiDaiDien.Text = dtHopDong.Rows[0]["NguoiDaiDien"].ToString();
                    double laisuat = Convert.ToDouble(txtLaiSuat.Text) / 100;
                    double kihan = Convert.ToDouble(cboThoiHanTraGop.SelectedIndex + 1);
                    double tong = (laisuat * Convert.ToDouble(txtSoTienTraGop.Text) * kihan) + Convert.ToDouble(txtSoTienTraGop.Text);
                    txtTienTraHangThang.Text = (tong / kihan).ToString("0,0");
                    txtTongNo.Text = tong.ToString("0,0");
                    txtNganHang.Text = (dtNganHang.Select("IdNganHang = '" + Convert.ToString(dtHopDong.Rows[0]["IdNganHang"]) + "'"))[0]["TenNganHang"].ToString();
                    txtChiNhanh.Text = (dtChiNhanhNganHang.Select("IdNganHang = '" + Convert.ToString(dtHopDong.Rows[0]["IdNganHang"]) + "' And IdChiNhanhNganHang = '" + Convert.ToString(dtHopDong.Rows[0]["IDChiNhanh"]) + "'"))[0]["TenChiNhanh"].ToString();
                    cmd = new SqlCommand("Select IdPhieuThu, SoHoaDon,NgayHachToan,SoTienThu From PhieuThu WHERE IDCongTy = @IDCongTy And SoHopDong = @SoHopDong");
                    cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@SoHopDong", soHopDong);
                    dataGridViewX1.DataSource = Class.datatabase.getData(cmd);
                    decimal tongtien = 0;
                    foreach (DataGridViewRow r in dataGridViewX1.Rows)
                    {
                        tongtien += Convert.ToDecimal(r.Cells["TienThu"].Value);
                    }
                    txtTienDaThu.Text = string.Format("{0:0,0}", tongtien);
                }
                else
                {
                    MessageBox.Show("Số hợp đồng " + soHopDong + " không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.Close();
                }

            }
            catch { }
        }

        private void txtSoHopDong_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SqlCommand cmd = new SqlCommand("Select * from HopDongTraGop Where IdCongTy = @IdCongTy And SoHopDongTraGop = @SoHopDong");
                    cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@SoHopDong", txtSoHopDong.Text);
                    dtHopDong = Class.datatabase.getData(cmd);
                    if (dtHopDong.Rows.Count > 0)
                    {
                        dtNganHang = classdb.LayDanhSachNganHang();
                        dtChiNhanhNganHang = classdb.LayDanhSachChiNhanhNganHang();
                        dateTimeInputNgaLapHopDong.Value = Convert.ToDateTime(dtHopDong.Rows[0]["NgayLapHopDong"]);
                        cboThoiHanTraGop.SelectedIndex = Convert.ToInt32(dtHopDong.Rows[0]["ThoiGianTraGop"]) - 1;
                        txtLaiSuat.Text = dtHopDong.Rows[0]["LaiSuat"].ToString();
                        txtNgayHenTra.Text = string.Format("{0:dd/MM/yyyy}", dtHopDong.Rows[0]["NgayHenTra"]);
                        txtSoTienTraGop.Text = string.Format("{0:0,0}", dtHopDong.Rows[0]["SoTienTraGop"]);
                        txtNguoiDaiDien.Text = dtHopDong.Rows[0]["NguoiDaiDien"].ToString();
                        double laisuat = Convert.ToDouble(txtLaiSuat.Text) / 100;
                        double kihan = Convert.ToDouble(cboThoiHanTraGop.SelectedIndex + 1);
                        double tong = (laisuat * Convert.ToDouble(txtSoTienTraGop.Text) * kihan) + Convert.ToDouble(txtSoTienTraGop.Text);
                        txtTienTraHangThang.Text = (tong / kihan).ToString("0,0");
                        txtTongNo.Text = tong.ToString("0,0");
                        txtNganHang.Text = (dtNganHang.Select("IdNganHang = '" + Convert.ToString(dtHopDong.Rows[0]["IdNganHang"]) + "'"))[0]["TenNganHang"].ToString();
                        txtChiNhanh.Text = (dtChiNhanhNganHang.Select("IdNganHang = '" + Convert.ToString(dtHopDong.Rows[0]["IdNganHang"]) + "' And IdChiNhanhNganHang = '" + Convert.ToString(dtHopDong.Rows[0]["IDChiNhanh"]) + "'"))[0]["TenChiNhanh"].ToString();
                        cmd = new SqlCommand("Select IdPhieuThu, SoHoaDon,NgayHachToan,SoTienThu From PhieuThu WHERE IDCongTy = @IDCongTy And SoHopDong = @SoHopDong");
                        cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@SoHopDong", soHopDong);
                        dataGridViewX1.DataSource = Class.datatabase.getData(cmd);
                        decimal tongtien = 0;
                        foreach (DataGridViewRow r in dataGridViewX1.Rows)
                        {
                            tongtien += Convert.ToDecimal(r.Cells["TienThu"].Value);
                        }
                        txtTienDaThu.Text = string.Format("{0:0,0}", tongtien);
                    }
                    else
                    {
                        MessageBox.Show("Số hợp đồng " + soHopDong + " không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

            }
            catch { }
        }
    }
}
