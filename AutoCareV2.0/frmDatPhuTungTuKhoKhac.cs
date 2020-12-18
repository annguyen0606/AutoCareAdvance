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
    public partial class frmDatPhuTungTuKhoKhac : Form
    {
        public string idCongTyXuat = "";
        public string idCuaHangXuat = "";
        public string idXuatKho = "";
        public string maPT = "";
        public string tenPT = "";
        public string soLuongHienCo = "";
        public string donGia = "";

        public frmDatPhuTungTuKhoKhac()
        {
            InitializeComponent();
        }

        private void FrmDatPhuTungTuKhoKhac_Load(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from dbo.CuaHang where IdCongTy = @idcongty and IdCuaHang = @idcuahang";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idcongty", int.Parse(idCongTyXuat));
            cmd.Parameters.AddWithValue("@idcuahang", int.Parse(idCuaHangXuat));
            DataTable tenCty = Class.datatabase.getData(cmd);

            txbNameCuaHang.Text = tenCty.Rows[0]["TenCuaHang"].ToString();
            txbMaPT.Text = maPT;
            txbTenPT.Text = tenPT;
            txbSoLuongCurrent.Text = soLuongHienCo;
            txbDonGia.Text = donGia;
        }

        private void BtnXacNhanDat_Click(object sender, EventArgs e)
        {
            txbTongTien.Text = (int.Parse(txbDonGia.Text.Trim()) * int.Parse(txbSoLuongOrder.Text.Trim())).ToString();
            if (int.Parse(txbSoLuongCurrent.Text.Trim()) < int.Parse(txbSoLuongOrder.Text.Trim()))
            {
                MessageBox.Show("Không thể đặt vượt quá số lượng hiện có", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"insert into dbo.LichSuDatPhuTungChiTiet (IdXuatKho,MaPT,TenPT,SoLuong,IdCongTy,DonGia,TongTien) values (@idxuatkho,@mapt,@tenpt,@soluong,@idcongty,@dongia,@tongtien)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(idXuatKho));
            cmd.Parameters.AddWithValue("@mapt", maPT);
            cmd.Parameters.AddWithValue("@tenpt", tenPT);
            cmd.Parameters.AddWithValue("@soluong", int.Parse(txbSoLuongOrder.Text.Trim()));
            cmd.Parameters.AddWithValue("@idcongty", int.Parse(idCongTyXuat.ToString()));
            cmd.Parameters.AddWithValue("@dongia", int.Parse(txbDonGia.Text.ToString().Trim()));
            cmd.Parameters.AddWithValue("@tongtien", int.Parse(txbTongTien.Text.ToString().Trim()));
            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
            {
                MessageBox.Show("Đặt thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đặt thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxbSoLuongOrder_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txbTongTien.Text = (int.Parse(txbDonGia.Text.Trim()) * int.Parse(txbSoLuongOrder.Text.Trim())).ToString();
            }
        }
    }
}
