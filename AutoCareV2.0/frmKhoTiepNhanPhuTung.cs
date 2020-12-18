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
    public partial class frmKhoTiepNhanPhuTung : Form
    {
        DataTable bangPhuTung = new DataTable();
        public frmKhoTiepNhanPhuTung()
        {
            InitializeComponent();
            dateTimeInput.Value = DateTime.Now;
            fromDateTimeInput.Value = DateTime.Now;
            toDateTimeInput.Value = DateTime.Now;
        }

        private void BtnTimKiemTheoNgay_Click(object sender, EventArgs e)
        {
            if(cboKho.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String dateTimeSearch = Convert.ToString(dateTimeInput.Value);
            string[] dateSearch = dateTimeSearch.Split(' ');
            string[] dateSearchArray = dateSearch[0].Split('/');
            string dateSearchReal = "";
            dateSearchReal = dateSearchArray[2] + "-" + dateSearchArray[0] + "-" + dateSearchArray[1];
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select kx.MaPT as 'Mã phụ tùng', pt.TenPT as 'Tên phụ tùng', SUM(kx.SoLuong) as 'Tổng số lượng', pt.DonGia as 'Đơn giá', SUM(kx.SoLuong)*pt.DonGia as 'Tiền' 
                                from dbo.KhoXuat kx inner join dbo.PhuTung pt on kx.MaPT = pt.MaPT WHERE kx.IdCongTy = @IdCongTy and  
                                pt.IdCongTy = @IdCongTy and pt.IdKho = @idkho and kx.IdKho = @idkho and CONVERT(varchar(25),NgayXuat,126)  like @NgayXuat group by kx.MaPT, pt.TenPT, pt.DonGia";
            cmd.Parameters.AddWithValue("@NgayXuat", "%" + dateSearchReal + "%");
            cmd.Parameters.AddWithValue("@IdCongTy", int.Parse(Class.CompanyInfo.idcongty.ToString()));
            cmd.Parameters.AddWithValue("@idkho", cboKho.SelectedValue);
            bangPhuTung = Class.datatabase.getData(cmd);

            object SumMoney;
            if(bangPhuTung.Rows.Count > 0)
            {
                SumMoney = bangPhuTung.Compute("Sum(Tiền)", "");
            }
            else
            {
                SumMoney = 0;
            }
            lbTongTien.Text = string.Format("{0:#,#.}", Decimal.Parse(SumMoney.ToString()));
            dataGridViewX1.DataSource = bangPhuTung;
        }
        #region Lấy danh sách những phụ tùng đã được gọi theo xe
        void LoadPhuTungTrongNgay()
        {
            DateTime now = DateTime.Now;
            int IdBaoDuongXe = 0;
            IdBaoDuongXe = 10;

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select distinct ptod.MaPT as 'Mã phụ tùng', pt.TenPT as 'Tên phụ tùng', Sum(ptod.SoLuong) as 'Số lượng gọi', Sum(ptod.SoLuongCurrent) as 'Số lượng dùng' ,Sum(ptod.SoLuong) - SUM(ptod.SoLuongCurrent) as 'Số lượng trả'   
                                from dbo.PhuTungOrder052020  ptod inner join PhuTung pt on ptod.MaPT = pt.MaPT  
                                where ptod.MaPT IN (Select MaPT from dbo.PhuTungOrder052020 where IdCongTy = @idcongty and Ngay like @ngay) and ptod.IdCongTy = @idcongty and pt.IdCongTy = @idcongty group by  
                                ptod.MaPT, pt.TenPT ";

            cmd.Parameters.AddWithValue("@idcuahang", Class.CompanyInfo.IdsCuaHang);
            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@ngay", "%"+DateTime.Now.ToString("yyyy-MM-dd")+"%");
            bangPhuTung = Class.datatabase.getData(cmd);
            dataGridViewX1.DataSource = bangPhuTung;
            for (int i = 0; i < dataGridViewX1.Rows.Count - 1; i++)
            {
                if (int.Parse(dataGridViewX1.Rows[i].Cells[4].Value.ToString().Trim()) == 0)
                {
                    dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                }
                else
                {
                    dataGridViewX1.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                }
            }
        }
        #endregion Lấy danh sách những phụ tùng đã được gọi theo xe

        private void FrmKhoTiepNhanPhuTung_Load(object sender, EventArgs e)
        {
            LayDanhSachKho();
            LoadPhuTungTrongNgay();
        }
        #region ListenData

        #endregion ListenData
        private void BtnExportFileExcel_Click(object sender, EventArgs e)
        {
            if (dataGridViewX1.Columns.Count != 5)
            {
                MessageBox.Show("Bạn chưa thống kê theo ngày!\nHãy thống kê theo ngày", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                DataTable dt = (DataTable)dataGridViewX1.DataSource;
                Export(dt);
            }
            catch { }
        }
        #region XuatFileExcel
        private void Export(DataTable dt)
        {
            Microsoft.Office.Interop.Excel.Application cExcel = new Microsoft.Office.Interop.Excel.Application();
            cExcel.Application.Workbooks.Add(Type.Missing);
            cExcel.Cells[1, 1] = "Mã phụ tùng";
            cExcel.Cells[1, 2] = "Tên phụ tùng";
            cExcel.Cells[1, 3] = "Tổng số lượng";
            cExcel.Cells[1, 4] = "Đơn giá";
            cExcel.Cells[1, 5] = "Thành tiền";
            int j = 2;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                cExcel.Cells[j, 1] = dt.Rows[i]["Mã phụ tùng"].ToString();
                cExcel.Cells[j, 2] = dt.Rows[i]["Tên phụ tùng"].ToString();
                cExcel.Cells[j, 3] = dt.Rows[i]["Tổng số lượng"].ToString();
                cExcel.Cells[j, 4] = dt.Rows[i]["Đơn giá"].ToString();
                cExcel.Cells[j, 5] = dt.Rows[i]["Tiền"].ToString();
                j++;
            }

            cExcel.Columns.AutoFit();
            cExcel.Visible = true;
        }
        #endregion XuatFileExcel


        class DanhSachXeOrder
        {
            public string TenXe { set; get; }
            public int IdBaoDuong { set; get; }
            public string BienSo { set; get; }
            public string SoKhung { set; get; }
            public string SoMay { set; get; }
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

        private void BtnThongKeNhieuNgay_Click(object sender, EventArgs e)
        {
            if (cboKho.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String fromTime = Convert.ToString(fromDateTimeInput.Value);
            string[] fromDateTime = fromTime.Split(' ');
            string[] fromDateArray = fromDateTime[0].Split('/');
            string fromDateSearch = "";
            fromDateSearch = fromDateArray[2] + "-" + fromDateArray[0] + "-" + fromDateArray[1];

            String toTime = Convert.ToString(toDateTimeInput.Value);
            string[] toDateTime = toTime.Split(' ');
            string[] toDateArray = toDateTime[0].Split('/');
            string toDateSearch = "";
            toDateSearch = toDateArray[2] + "-" + toDateArray[0] + "-" + toDateArray[1];

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"select kx.MaPT as 'Mã phụ tùng', pt.TenPT as 'Tên phụ tùng', SUM(kx.SoLuong) as 'Tổng số lượng', pt.DonGia as 'Đơn giá', SUM(kx.SoLuong)*pt.DonGia as 'Tiền'  
                                from dbo.KhoXuat kx inner join dbo.PhuTung pt on kx.MaPT = pt.MaPT WHERE kx.IdCongTy = @IdCongTy and  pt.IdCongTy = @IdCongTy and pt.IdKho = @idkho and kx.IdKho = @idkho and   
                                CONVERT(varchar(25),NgayXuat,126)  between @tungay and @denngay group by kx.MaPT, pt.TenPT, pt.DonGia ";
            cmd.Parameters.AddWithValue("@tungay", fromDateSearch + "%");
            cmd.Parameters.AddWithValue("@denngay", toDateSearch + "%");
            cmd.Parameters.AddWithValue("@IdCongTy", int.Parse(Class.CompanyInfo.idcongty.ToString()));
            cmd.Parameters.AddWithValue("@idkho", cboKho.SelectedValue);
            bangPhuTung = Class.datatabase.getData(cmd);

            object SumMoney;
            if (bangPhuTung.Rows.Count > 0)
            {
                SumMoney = bangPhuTung.Compute("Sum(Tiền)", "");
            }
            else
            {
                SumMoney = 0;
            }
            lbTongTien.Text = string.Format("{0:#,#.}", Decimal.Parse(SumMoney.ToString()));
            dataGridViewX1.DataSource = bangPhuTung;
        }
    }
}
