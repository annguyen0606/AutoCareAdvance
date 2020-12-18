using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace AutoCareV2._0.UserControls.KiemTraPhuTungKhoKhac
{
    public partial class UcKiemTraPhuTungKhoKhac : UserControl
    {
        List<DanhSachKho> danhSachCacKhoCuaCongTy = new List<DanhSachKho>();
        private string _idXuatKho = "";
        int idCuaHangXuat = 0;
        int idCongTyXuat = 0;
        string dateSearchReal = "";
        int IdKho = 0;
        public UcKiemTraPhuTungKhoKhac()
        {
            InitializeComponent();
        }

        private void PanelEx1_Click(object sender, EventArgs e)
        {

        }

        private void UcKiemTraPhuTungKhoKhac_Load(object sender, EventArgs e)
        {
            dateTimeInput1.Value = DateTime.Now;
            LoadDanhSachKho();
        }

        private void LoadDanhSachKho()
        {
            DataTable tableKhoHang = new DataTable();
            SqlCommand cpp = new SqlCommand();

            cpp.CommandText = "SELECT DISTINCT * FROM KhoHang WHERE (IdCongTy = @IdCongTy1) or (IdCongTy = @IdCongTy2) or (IdCongTy = @IdCongTy3)";
            //cpp.CommandText = "SELECT DISTINCT * FROM KhoHang WHERE IdCongTy = @idcongty";
            cpp.Parameters.Clear();
            //cpp.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cpp.Parameters.AddWithValue("@IdCongTy1", 92);
            cpp.Parameters.AddWithValue("@IdCongTy2", 94);
            cpp.Parameters.AddWithValue("@IdCongTy3", 95);

            tableKhoHang = Class.datatabase.getData(cpp);

            cboKhoHang.DataSource = tableKhoHang;
            cboKhoHang.DisplayMember = "TenKho";
            cboKhoHang.ValueMember = "IdKho";

            for(int i = 0; i < tableKhoHang.Rows.Count; i++)
            {
                DanhSachKho danhSachKho = new DanhSachKho();
                danhSachKho.idCongTy = int.Parse(tableKhoHang.Rows[i]["IdCongTy"].ToString());
                danhSachKho.idCuaHang = int.Parse(tableKhoHang.Rows[i]["IdCuaHang"].ToString());
                danhSachKho.idKho = int.Parse(tableKhoHang.Rows[i]["IdKho"].ToString());
                danhSachKho.tenKho = tableKhoHang.Rows[i]["TenKho"].ToString();
                danhSachCacKhoCuaCongTy.Add(danhSachKho);
            }
        }

        private void BtnTimKiemPT_Click(object sender, EventArgs e)
        {
            if(textBoxX1.Text.Trim().Length < 1)
            {
                MessageBox.Show("Bạn chưa nhập mã phụ tùng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int idCongTy = 0;
            int idCuaHang = 0;
            foreach (DanhSachKho item in danhSachCacKhoCuaCongTy)
            {
                if (int.Parse(cboKhoHang.SelectedValue.ToString()) == item.idKho)
                {
                    idCuaHang = item.idCuaHang;
                    idCongTy = item.idCongTy;

                    idCuaHangXuat = item.idCuaHang;

                    idCongTyXuat = item.idCongTy;
                }
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select MaPT as 'Mã phụ tùng', TenPT as 'Tên phụ tùng', SoLuong as 'Số lượng', DonGia as 'Đơn giá', PositionO as 'Vị trí' from dbo.PhuTung where MaPT like @mapt and IdCongTy = @idcongty and IdKho = @idkho";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@mapt", "%" + textBoxX1.Text + "%");
            sqlCommand.Parameters.AddWithValue("@idcongty", idCongTy);
            sqlCommand.Parameters.AddWithValue("@idkho", cboKhoHang.SelectedValue);

            DataTable tablePT = null;
            dataGridViewX1.DataSource = tablePT;
            dataGridViewX1.Columns.Clear();
            tablePT = Class.datatabase.getData(sqlCommand);
            dataGridViewX1.DataSource = tablePT;

            lbSoLuongTimkiemduoc.Text = (dataGridViewX1.Rows.Count - 1).ToString();

            if (dataGridViewX1.Columns.Count == 5)
            {
                DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                btn.HeaderText = "Đặt";
                btn.Name = "btnOrder";
                btn.Text = "Đặt";
                btn.UseColumnTextForButtonValue = true;
                dataGridViewX1.Columns.Add(btn);
            }
        }

        class DanhSachKho
        {
            public string tenKho { set; get; }
            public int idKho { set; get; }
            public int idCongTy { set; get; }
            public int idCuaHang { set; get; }
        }

        private void BtnTimkiemtat_Click(object sender, EventArgs e)
        {
            int idCongTy = 0;
            int idCuaHang = 0;
            foreach (DanhSachKho item in danhSachCacKhoCuaCongTy)
            {
                if (int.Parse(cboKhoHang.SelectedValue.ToString()) == item.idKho)
                {
                    idCuaHang = item.idCuaHang;
                    idCuaHangXuat = item.idCuaHang;

                    idCongTyXuat = item.idCongTy;
                    idCongTy = item.idCongTy;
                }
            }

            SqlCommand sqlCommand = new SqlCommand();
            sqlCommand.CommandText = @"Select MaPT as 'Mã phụ tùng', TenPT as 'Tên phụ tùng', SoLuong as 'Số lượng', DonGia as 'Đơn giá', PositionO as 'Vị trí' from dbo.PhuTung where IdCongTy = @idcongty and IdKho = @idkho";
            sqlCommand.Parameters.Clear();
            sqlCommand.Parameters.AddWithValue("@idcongty", idCongTy);
            sqlCommand.Parameters.AddWithValue("@idkho", cboKhoHang.SelectedValue);

            DataTable tablePT = Class.datatabase.getData(sqlCommand);
            dataGridViewX1.DataSource = tablePT;

            lbSoLuongTimkiemduoc.Text = (dataGridViewX1.Rows.Count - 1).ToString();
        }

        private void DataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 5)
            {
                if(_idXuatKho.Length <= 0)
                {
                    MessageBox.Show("Bạn chưa tạo phiếu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if(MessageBox.Show("Bạn có chắc muốn đặt phụ tùng không","Cảnh báo",MessageBoxButtons.OKCancel,MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    if(int.Parse(dataGridViewX1.Rows[e.RowIndex].Cells[2].Value.ToString()) <= 0)
                    {
                        MessageBox.Show("Phụ tùng hiện đã hết", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    frmDatPhuTungTuKhoKhac frm = new frmDatPhuTungTuKhoKhac();
                    frm.idCongTyXuat = idCongTyXuat.ToString();
                    frm.idCuaHangXuat = idCuaHangXuat.ToString();
                    frm.idXuatKho = _idXuatKho;
                    frm.soLuongHienCo = dataGridViewX1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    frm.tenPT = dataGridViewX1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    frm.maPT = dataGridViewX1.Rows[e.RowIndex].Cells[0].Value.ToString();
                    frm.donGia = dataGridViewX1.Rows[e.RowIndex].Cells[3].Value.ToString();
                    frm.ShowDialog();
                }
            }
        }

        private void BtnTaoPhieuDatHang_Click(object sender, EventArgs e)
        {
            foreach (DanhSachKho item in danhSachCacKhoCuaCongTy)
            {
                if (int.Parse(cboKhoHang.SelectedValue.ToString()) == item.idKho)
                {
                    idCuaHangXuat = item.idCuaHang;
                    idCongTyXuat = item.idCongTy;
                }
            }

            if(idCongTyXuat == int.Parse(Class.CompanyInfo.idcongty))
            {
                MessageBox.Show("Đây đã là cửa hàng của bạn\nKhông thể tạo phiếu", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"insert into dbo.LichSuDatPhuTung (IdCuaHangXuat,IdCongTyXuat,IdCongTyNhan,IdCuaHangNhan,NgayDat,TrangThaiXacNhan,IdKho) values(@idcuahangxuat,@idcongtyxuat,@idcongtynhan,@idcuahangnhan,@ngaydat,0,@idkho) select @@IDENTITY";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idcuahangxuat", idCuaHangXuat);
            cmd.Parameters.AddWithValue("@idcongtyxuat", idCongTyXuat);
            cmd.Parameters.AddWithValue("@idcongtynhan", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@idcuahangnhan", Class.CompanyInfo.IdsCuaHang);
            cmd.Parameters.AddWithValue("@idkho", cboKhoHang.SelectedValue);
            cmd.Parameters.AddWithValue("@ngaydat", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
            _idXuatKho = Class.datatabase.ExecuteScalar(cmd);
            if (int.Parse(_idXuatKho) > 0)
            {
                MessageBox.Show("Tạo phiếu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                btnTaoPhieuDatHang.Enabled = false;
                cboKhoHang.Enabled = false;
                btnTimkiemtat.Enabled = false;
            }
            else
            {
                MessageBox.Show("Tạo phiếu thất bại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnHoanTat_Click(object sender, EventArgs e)
        {
            _idXuatKho = "";
            btnTimkiemtat.Enabled = true;
            cboKhoHang.Enabled = true;
            btnTaoPhieuDatHang.Enabled = true;
        }

        private void BtnLayDonDat_Click(object sender, EventArgs e)
        {
            String dateTimeSearch = Convert.ToString(dateTimeInput1.Value);
            
            string[] dateSearch = dateTimeSearch.Split(' ');
            string[] dateSearchArray = dateSearch[0].Split('/');
            dateSearchReal = dateSearchArray[2] + "-" + dateSearchArray[0] + "-" + dateSearchArray[1];

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT ROW_NUMBER() OVER (ORDER BY IdCongTyXuat)as 'STT',IdXuatKho FROM dbo.LichSuDatPhuTung where CONVERT(varchar(25),NgayDat,126)  like @ngaydat and IdCongTyXuat = @idcongtyxuat and TrangThaiXacNhan = 0";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ngaydat", "%"+dateSearchReal+"%");
            cmd.Parameters.AddWithValue("@idcongtyxuat", Class.CompanyInfo.idcongty);
            DataTable danhSachDon = Class.datatabase.getData(cmd);

            cboDonDat.DataSource = danhSachDon;
            cboDonDat.DisplayMember = "STT";
            cboDonDat.ValueMember = "IdXuatKho";
            cboDonDat.SelectedIndex = -1;
        }

        private void BtnXemDon_Click(object sender, EventArgs e)
        {   
            if (cboDonDat.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa lấy danh sách đơn đặt hàng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from dbo.LichSuDatPhuTung where IdXuatKho = @idxuatkho and TrangThaiXacNhan = 0 and IdCongTyXuat = @idcongtyxuat";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idxuatkho", cboDonDat.SelectedValue);
            cmd.Parameters.AddWithValue("@idcongtyxuat", Class.CompanyInfo.idcongty);

            DataTable dt = Class.datatabase.getData(cmd);

            if(dt.Rows.Count > 0)
            {
                frmPhieuXuatKhoNoiBoVietLong frm = new frmPhieuXuatKhoNoiBoVietLong();
                frm.idXuatKho = cboDonDat.SelectedValue.ToString();
                frm.dateReal = dateSearchReal;
                frm.idKho = dt.Rows[0]["IdKho"].ToString().Trim();
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Không thể xem đơn, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }

        private void BtnLayLSDon_Click(object sender, EventArgs e)
        {
            String dateTimeSearch = Convert.ToString(dateTimeInput1.Value);

            string[] dateSearch = dateTimeSearch.Split(' ');
            string[] dateSearchArray = dateSearch[0].Split('/');
            dateSearchReal = dateSearchArray[2] + "-" + dateSearchArray[0] + "-" + dateSearchArray[1];
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT ROW_NUMBER() OVER (ORDER BY IdCongTyXuat)as 'STT',IdXuatKho FROM dbo.LichSuDatPhuTung where CONVERT(varchar(25),NgayDat,126)  like @ngaydat and IdCongTyXuat = @idcongtyxuat and TrangThaiXacNhan = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ngaydat", "%" + dateSearchReal + "%");
            cmd.Parameters.AddWithValue("@idcongtyxuat", Class.CompanyInfo.idcongty);
            DataTable danhSachDon = Class.datatabase.getData(cmd);

            cboDonDat.DataSource = danhSachDon;
            cboDonDat.DisplayMember = "STT";
            cboDonDat.ValueMember = "IdXuatKho";
            cboDonDat.SelectedIndex = -1;
        }

        private void BtnXemDonLS_Click(object sender, EventArgs e)
        {
            if (cboDonDat.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa lấy danh sách đơn đặt hàng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from dbo.LichSuDatPhuTung where IdXuatKho = @idxuatkho and TrangThaiXacNhan = 1";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idxuatkho", cboDonDat.SelectedValue);

            DataTable dt = Class.datatabase.getData(cmd);

            if (dt.Rows.Count > 0)
            {
                frmPhieuXuatKhoNoiBoVietLong frm = new frmPhieuXuatKhoNoiBoVietLong();
                frm.idXuatKho = cboDonDat.SelectedValue.ToString();
                frm.dateReal = dateSearchReal;
                frm.idKho = dt.Rows[0]["IdKho"].ToString().Trim();
                frm.lichSu = "ls";
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đơn đã xác nhận hoặc lỗi không thể xem đơn, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BtnThongKeXuatPT_Click(object sender, EventArgs e)
        {
            String dateTimeSearch = Convert.ToString(dateTimeInput1.Value);

            string[] dateSearch = dateTimeSearch.Split(' ');
            string[] dateSearchArray = dateSearch[0].Split('/');
            dateSearchReal = dateSearchArray[2] + "-" + dateSearchArray[0] + "-" + dateSearchArray[1];
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select lsct.MaPT as 'Mã phụ tùng', lsct.TenPT as 'Tên phụ tùng', SUM(lsct.SoLuong) as 'Tổng số lượng', lsct.DonGia as 'Đơn giá', SUM(lsct.TongTien) as 'Tổng tiền' from dbo.LichSuDatPhuTungChiTiet lsct  
                                where IdXuatKho IN (Select IdXuatKho FROM dbo.LichSuDatPhuTung where convert(nvarchar(25),NgayXuat,126) like @ngayxuat and IdCongTyXuat = @idcongty)  
                                group by lsct.MaPT, lsct.TenPT, lsct.DonGia ";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ngayxuat", "%" + dateSearchReal + "%");
            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            DataTable danhSachpt = null;
            dataGridViewX1.DataSource = danhSachpt;
            dataGridViewX1.Columns.Clear();
            Class.datatabase.getData(cmd);
            dataGridViewX1.DataSource = danhSachpt;
        }

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            if(dataGridViewX1.Columns.Count != 5)
            {
                MessageBox.Show("Bạn chưa thống kê phụ tùng xuất", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                DataTable dt = (DataTable)dataGridViewX1.DataSource;
                Export(dt);
            }
            catch { }
        }

        private void Export(DataTable dt)
        {
            Microsoft.Office.Interop.Excel.Application cExcel = new Microsoft.Office.Interop.Excel.Application();
            cExcel.Application.Workbooks.Add(Type.Missing);
            cExcel.Cells[1, 1] = "Mã phụ tùng";
            cExcel.Cells[1, 2] = "Tên phụ tùng";
            cExcel.Cells[1, 3] = "Tổng số lượng";
            cExcel.Cells[1, 4] = "Đơn giá";
            cExcel.Cells[1, 5] = "Tổng tiền";
            int j = 2;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cExcel.Cells[j, 1] = dt.Rows[i]["Mã phụ tùng"].ToString();
                cExcel.Cells[j, 2] = dt.Rows[i]["Tên phụ tùng"].ToString();
                cExcel.Cells[j, 3] = dt.Rows[i]["Tổng số lượng"].ToString();
                cExcel.Cells[j, 4] = dt.Rows[i]["Đơn giá"].ToString();
                cExcel.Cells[j, 5] = dt.Rows[i]["Tổng tiền"].ToString();
                j++;
            }

            cExcel.Columns.AutoFit();
            cExcel.Visible = true;
        }

        private void BtnCacDonDaDat_Click(object sender, EventArgs e)
        {
            String dateTimeSearch = Convert.ToString(dateTimeInput1.Value);

            string[] dateSearch = dateTimeSearch.Split(' ');
            string[] dateSearchArray = dateSearch[0].Split('/');
            dateSearchReal = dateSearchArray[2] + "-" + dateSearchArray[0] + "-" + dateSearchArray[1];
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT ROW_NUMBER() OVER (ORDER BY IdCongTyXuat)as 'STT',IdXuatKho FROM dbo.LichSuDatPhuTung where CONVERT(varchar(25),NgayDat,126)  like @ngaydat and IdCongTyNhan = @idcongtynhan and TrangThaiXacNhan = 0";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ngaydat", "%" + dateSearchReal + "%");
            cmd.Parameters.AddWithValue("@idcongtynhan", Class.CompanyInfo.idcongty);
            DataTable danhSachDon = Class.datatabase.getData(cmd);

            cboDonDat.DataSource = danhSachDon;
            cboDonDat.DisplayMember = "STT";
            cboDonDat.ValueMember = "IdXuatKho";
            cboDonDat.SelectedIndex = -1;
        }

        private void BtnXemDonDaDat_Click(object sender, EventArgs e)
        {
            if (cboDonDat.SelectedIndex < 0)
            {
                MessageBox.Show("Bạn chưa lấy danh sách đơn đặt hàng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select * from dbo.LichSuDatPhuTung where IdXuatKho = @idxuatkho and TrangThaiXacNhan = 0 and IdCongTyNhan = @idcongtynhan";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idxuatkho", cboDonDat.SelectedValue);
            cmd.Parameters.AddWithValue("@idcongtynhan", Class.CompanyInfo.idcongty);

            DataTable dt = Class.datatabase.getData(cmd);

            if (dt.Rows.Count > 0)
            {
                frmPhieuXuatKhoNoiBoVietLong frm = new frmPhieuXuatKhoNoiBoVietLong();
                frm.idXuatKho = cboDonDat.SelectedValue.ToString();
                frm.dateReal = dateSearchReal;
                frm.idKho = dt.Rows[0]["IdKho"].ToString().Trim();
                frm.donCheck = "dck";
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("Đơn đã xác nhận hoặc lỗi không thể xem đơn, vui lòng kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

        }
    }
}
