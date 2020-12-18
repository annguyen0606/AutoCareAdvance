using System;
using System.Collections;
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
    public partial class frmQuanLyXuatPhuTungNgoai : Form
    {
        string _idXuatKho = "";
        public frmQuanLyXuatPhuTungNgoai()
        {
            InitializeComponent();
        }

        private void FrmQuanLyXuatPhuTungNgoai_Load(object sender, EventArgs e)
        {
            btnXemPhieu.Enabled = false;
            dateTimeInput1.Value = DateTime.Now;
            LayDanhSachKho();
        }
        public void LayDanhSachKho()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT IdKho, TenKho FROM KhoHang WHERE IdCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cboKho.DisplayMember = "TenKho";
            cboKho.ValueMember = "IdKho";
            cboKho.DataSource = Class.datatabase.getData(cmd);
            cboKho.SelectedIndex = -1;
        }

      

      

        private void BtnTaoPhieu_Click(object sender, EventArgs e)
        {
            btnTaoPhieu.Enabled = false;
            cboKho.Enabled = false;
            txbTenCuaHangNhan.Enabled = false;
            if (String.IsNullOrEmpty(txbTenCuaHangNhan.Text))
            {
                MessageBox.Show("Chưa nhập tên cửa hàng nhận\nKhông thể tạo phiếu", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboKho.Enabled = true;
                btnTaoPhieu.Enabled = true;
                txbTenCuaHangNhan.Enabled = true ;
                return;
            }
            if(cboKho.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn kho", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboKho.Enabled = true;
                txbTenCuaHangNhan.Enabled = true;
                btnTaoPhieu.Enabled = true;
                return;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"insert into dbo.LichSuXuatKhoNgoai (IdCongTy,IdCuaHang,IdKhoXuat,TrangThaiXuat,NgayTaoPhieu,TenCongTyNhan) values(@idcongty,@idcuahang,@idkhoxuat,0,@ngaytaophieu,@tencongtynhan) select @@IDENTITY";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@idcuahang", Class.CompanyInfo.IdsCuaHang);
            cmd.Parameters.AddWithValue("@idkhoxuat", cboKho.SelectedValue);
            cmd.Parameters.AddWithValue("@ngaytaophieu", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
            cmd.Parameters.AddWithValue("@tencongtynhan", txbTenCuaHangNhan.Text.Trim());
            _idXuatKho = Class.datatabase.ExecuteScalar(cmd);
            if (int.Parse(_idXuatKho) > 0)
            {
                MessageBox.Show("Tạo phiếu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Tạo phiếu thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        

        private void LayDanhSachPhieuChuaXuLy()
        {
            String dateTimeSearch = Convert.ToString(dateTimeInput1.Value);
            String dateSearchReal = "";
            string[] dateSearch = dateTimeSearch.Split(' ');
            string[] dateSearchArray = dateSearch[0].Split('/');
            dateSearchReal = dateSearchArray[2] + "-" + dateSearchArray[0] + "-" + dateSearchArray[1];
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT ROW_NUMBER() OVER (ORDER BY TrangThaiXuat)as 'STT',IdXuatKho FROM dbo.LichSuXuatKhoNgoai where IdCongTy = @idcongty and IdCuaHang = @idcuahang and IdKhoXuat = @idkhoxuat and CONVERT(nvarchar(25),NgayTaoPhieu,126) like @ngaytaophieu and TrangThaiXuat = 0";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@idcuahang", Class.CompanyInfo.IdsCuaHang);
            cmd.Parameters.AddWithValue("@idkhoxuat", cboKho.SelectedValue);
            cmd.Parameters.AddWithValue("@ngaytaophieu", "%"+dateSearchReal+"%");
            DataTable danhSachDon = Class.datatabase.getData(cmd);

            cboDanhSachPhieu.DataSource = danhSachDon;
            cboDanhSachPhieu.DisplayMember = "STT";
            cboDanhSachPhieu.ValueMember = "IdXuatKho";
            cboDanhSachPhieu.SelectedIndex = -1;
            cboDanhSachPhieu.Text = "";
        }

        private void LayDanhSachPhieuDaXuat()
        {
            String dateTimeSearch = Convert.ToString(dateTimeInput1.Value);
            String dateSearchReal = "";
            string[] dateSearch = dateTimeSearch.Split(' ');
            string[] dateSearchArray = dateSearch[0].Split('/');
            dateSearchReal = dateSearchArray[2] + "-" + dateSearchArray[0] + "-" + dateSearchArray[1];
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT ROW_NUMBER() OVER (ORDER BY TrangThaiXuat)as 'STT',IdXuatKho FROM dbo.LichSuXuatKhoNgoai where IdCongTy = @idcongty and IdCuaHang = @idcuahang and IdKhoXuat = @idkhoxuat and CONVERT(nvarchar(25),NgayTaoPhieu,126) like @ngaytaophieu and TrangThaiXuat = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@idcuahang", Class.CompanyInfo.IdsCuaHang);
            cmd.Parameters.AddWithValue("@idkhoxuat", cboKho.SelectedValue);
            cmd.Parameters.AddWithValue("@ngaytaophieu", "%" + dateSearchReal + "%");
            DataTable danhSachDon = Class.datatabase.getData(cmd);

            cboDanhSachPhieu.DataSource = danhSachDon;
            cboDanhSachPhieu.DisplayMember = "STT";
            cboDanhSachPhieu.ValueMember = "IdXuatKho";
            cboDanhSachPhieu.SelectedIndex = -1;
            cboDanhSachPhieu.Text = "";
        }



        private void BtnLayPhieuDaXuLy_Click(object sender, EventArgs e)
        {
            btnXemPhieu.Enabled = true;
            if (cboKho.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LayDanhSachPhieuDaXuat();
        }

        private void BtnXemPhieu_Click(object sender, EventArgs e)
        {
            if (int.Parse(cboDanhSachPhieu.SelectedIndex.ToString()) == -1)
            {
                MessageBox.Show("Không có phiếu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from dbo.LichSuXuatKhoNgoai where IdXuatKho = @idxuatkho and TrangThaiXuat = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idxuatkho", cboDanhSachPhieu.SelectedValue);

            DataTable dt = Class.datatabase.getData(cmd);
            if (dt.Rows.Count > 0)
            {
                frmPhieuXuatKhoNgoai frm = new frmPhieuXuatKhoNgoai();
                frm.__idXuatKho = cboDanhSachPhieu.SelectedValue.ToString().Trim();
                frm._trangThaiPhieu = "dx";
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Phiếu không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void BtnPhieuChuaXuLy_Click(object sender, EventArgs e)
        {
            btnXemPhieu.Enabled = true;
            if (cboKho.SelectedIndex == -1)
            {
                MessageBox.Show("Chưa chọn kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            LayDanhSachPhieuChuaXuLy();
        }

        private void BtnXemPhieuChuaXuLy_Click(object sender, EventArgs e)
        {
            if (int.Parse(cboDanhSachPhieu.SelectedIndex.ToString()) == -1)
            {
                MessageBox.Show("Không có phiếu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from dbo.LichSuXuatKhoNgoai where IdXuatKho = @idxuatkho and TrangThaiXuat = 0";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idxuatkho", cboDanhSachPhieu.SelectedValue);

            DataTable dt = Class.datatabase.getData(cmd);
            if (dt.Rows.Count > 0)
            {
                frmPhieuXuatKhoNgoai frm = new frmPhieuXuatKhoNgoai();
                frm.__idXuatKho = cboDanhSachPhieu.SelectedValue.ToString().Trim();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Phiếu không tồn tại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
