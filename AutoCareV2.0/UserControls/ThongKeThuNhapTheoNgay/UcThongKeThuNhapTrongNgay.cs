using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using AutoCareV2._0.Class;
using System.Globalization;

namespace AutoCareV2._0.UserControls.QuanLyXuatKho
{
    public partial class UcThongKeThuNhapTheoNgay : UserControl
    {
        string dateSearchReal = "";
        List<String> danhSachBienSo;
        public UcThongKeThuNhapTheoNgay()
        {
            InitializeComponent();
        }

        private void PanelEx1_Click(object sender, EventArgs e)
        {

        }

        
        #region Tìm kiếm theo xe
        private void BtnSearch_Click(object sender, EventArgs e)
        {
            danhSachBienSo = new List<string>();
            String dateTimeSearch = Convert.ToString(dateTimeInput.Value);
            string[] dateSearch = dateTimeSearch.Split(' ');
            string[] dateSearchArray = dateSearch[0].Split('/');
            dateSearchReal = dateSearchArray[2] + "-" + dateSearchArray[0] + "-" + dateSearchArray[1];
            using (SqlConnection myCon = new SqlConnection(Class.datatabase.connect))
            {
                if (myCon.State == ConnectionState.Closed)
                {
                    myCon.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myCon;
                SqlTransaction transaction;
                transaction = myCon.BeginTransaction();
                cmd.Transaction = transaction;
                try
                {
                    cmd.CommandText = @"select lsbd.TenXe as 'Tên xe', lsbd.BienSo as 'Biển số', pt.SoTienThu as 'Tiền' from dbo.LichSuBaoDuongXe lsbd inner join dbo.PhieuThu pt on lsbd.IdBaoDuong = pt.SoHoaDon where 
                                    lsbd.IdCongty = @IdCongTy and CONVERT(nvarchar(25),lsbd.NgayGiaoXe,126) like @NgayGiaoXe and pt.NgayHachToan like @NgayGiaoXe and pt.IdLoaiPhieuThu = 5";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@NgayGiaoXe", "%" + dateSearchReal.Trim() + "%");
                    cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    DataTable bangPhuTung = new DataTable();
                    dgvDanhSachBaoDuong.DataSource = bangPhuTung;
                    dataAdapter.Fill(bangPhuTung);
                    object SumMoney;
                    if (bangPhuTung.Rows.Count > 0)
                    {
                        SumMoney = bangPhuTung.Compute("Sum(Tiền)", "");
                    }
                    else
                    {
                        SumMoney = 0;
                    }
                    
                    lbTotalMoney.Text = string.Format("{0:#,#.}", Decimal.Parse(SumMoney.ToString()));
                    dgvDanhSachBaoDuong.DataSource = bangPhuTung;
                    for(int i = 0; i < dgvDanhSachBaoDuong.Rows.Count; i++)
                    {
                        dgvDanhSachBaoDuong.Rows[i].Cells[2].Value = string.Format("{0:#,#.}", Convert.ToString(dgvDanhSachBaoDuong.Rows[i].Cells[2].Value));
                    }
                    transaction.Commit();
                    //MessageBox.Show("Thống kê thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lbSoLuong.Text = (dgvDanhSachBaoDuong.Rows.Count).ToString();
                    if (myCon.State == ConnectionState.Open) myCon.Close();
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    if (myCon.State == ConnectionState.Open) myCon.Close();
                    MessageBox.Show(ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }

        }
        #endregion Tìm kiếm theo xe
        private void LabelX3_Click(object sender, EventArgs e)
        {

        }


        private void BtnThongTheoTho_Click(object sender, EventArgs e)
        {
            String dateTimeSearch = Convert.ToString(dateTimeInput.Value);
            string[] dateSearch = dateTimeSearch.Split(' ');
            string[] dateSearchArray = dateSearch[0].Split('/');
            dateSearchReal = dateSearchArray[2] + "-" + dateSearchArray[0] + "-" + dateSearchArray[1];

            using (SqlConnection myCon = new SqlConnection(Class.datatabase.connect))
            {
                if (myCon.State == ConnectionState.Closed)
                {
                    myCon.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myCon;
                SqlTransaction transaction;
                transaction = myCon.BeginTransaction();
                cmd.Transaction = transaction;
                try
                {
                    cmd.CommandText = @"SELECT tdv.tenTho as 'Tên thợ',Sum(pt.SoTienThu) as 'Tiền' from dbo.LichSuBaoDuongXe lsct inner join dbo.ThoDichVu tdv   
                                        on lsct.KYTHUATVIEN = CONVERT(nvarchar(25),tdv.IdTho) left join dbo.PhieuThu pt on pt.SoHoaDon = CONVERT(nvarchar(50),lsct.IdBaoDuong)  
                                        where tdv.IdCongTy = @IdCongTy and tdv.TinhTrangLamViec IS null and  CONVERT(varchar(25),NgayGiaoXe,126)  like @NgayGiaoXe   
                                        group by tdv.tenTho";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@NgayGiaoXe", "%" + dateSearchReal.Trim() + "%");
                    cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);
                    DataTable bangPhuTung = new DataTable();
                    dgvDanhSachBaoDuong.DataSource = bangPhuTung;
                    dataAdapter.Fill(bangPhuTung);
                    object SumMoney;
                    if(bangPhuTung.Rows.Count > 0)
                    {
                        SumMoney = bangPhuTung.Compute("Sum(Tiền)", "");
                    }
                    else
                    {
                        SumMoney = 0;
                    }
                    lbTotalMoney.Text = string.Format("{0:#,#.}", SumMoney);
                    dgvDanhSachBaoDuong.DataSource = bangPhuTung;
                    lbSoLuong.Text = dgvDanhSachBaoDuong.Rows.Count.ToString();
                    transaction.Commit();
                    if (myCon.State == ConnectionState.Open) myCon.Close();
                }
                catch
                {
                    transaction.Rollback();
                    if (myCon.State == ConnectionState.Open) myCon.Close();
                }
            }
        }

        private void BtnExportExcel_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(dgvDanhSachBaoDuong.Columns.Count.ToString());
            
            try
            {
                DataTable dt = (DataTable)dgvDanhSachBaoDuong.DataSource;
                Export(dt,dgvDanhSachBaoDuong.Columns.Count);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void Export(DataTable dt, int soColumns)
        {
            Microsoft.Office.Interop.Excel.Application cExcel = new Microsoft.Office.Interop.Excel.Application();
            cExcel.Application.Workbooks.Add(Type.Missing);
            int j = 2;
            switch (soColumns)
            {
                case 2:
                    cExcel.Cells[1, 1] = "Tên thợ";
                    cExcel.Cells[1, 2] = "Tiền";
                    j = 2;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cExcel.Cells[j, 1] = dt.Rows[i]["Tên thợ"].ToString();
                        cExcel.Cells[j, 2] = dt.Rows[i]["Tiền"].ToString();
                        j++;
                    }
                    cExcel.Columns.AutoFit();
                    cExcel.Visible = true;
                    break;
                case 3:
                    cExcel.Cells[1, 1] = "Tên xe";
                    cExcel.Cells[1, 2] = "Biển số";
                    cExcel.Cells[1, 3] = "Tiền";
                    j = 2;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cExcel.Cells[j, 1] = dt.Rows[i]["Tên xe"].ToString();
                        cExcel.Cells[j, 2] = dt.Rows[i]["Biển số"].ToString();
                        cExcel.Cells[j, 3] = dt.Rows[i]["Tiền"].ToString();
                        j++;
                    }
                    cExcel.Columns.AutoFit();
                    cExcel.Visible = true;
                    break;
                case 5:
                    cExcel.Cells[1, 1] = "Mã phụ tùng";
                    cExcel.Cells[1, 2] = "Tên phụ tùng";
                    cExcel.Cells[1, 3] = "Số lượng";
                    cExcel.Cells[1, 4] = "Đơn giá";
                    cExcel.Cells[1, 5] = "Tiền";
                    j = 2;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        cExcel.Cells[j, 1] = dt.Rows[i]["Mã phụ tùng"].ToString();
                        cExcel.Cells[j, 2] = dt.Rows[i]["Tên phụ tùng"].ToString();
                        cExcel.Cells[j, 3] = dt.Rows[i]["Số lượng"].ToString();
                        cExcel.Cells[j, 4] = dt.Rows[i]["Đơn giá"].ToString();
                        cExcel.Cells[j, 5] = dt.Rows[i]["Tiền"].ToString();
                        j++;
                    }
                    cExcel.Columns.AutoFit();
                    cExcel.Visible = true;
                    break;
                default:
                    break;
            }
            
            
        }

        private void BtnThongKeTheoPhuTung_Click(object sender, EventArgs e)
        {
            if(cboKho.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn kho", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            String dateTimeSearch = Convert.ToString(dateTimeInput.Value);
            string[] dateSearch = dateTimeSearch.Split(' ');
            string[] dateSearchArray = dateSearch[0].Split('/');
            dateSearchReal = dateSearchArray[2] + "-" + dateSearchArray[0] + "-" + dateSearchArray[1];

            using (SqlConnection myCon = new SqlConnection(Class.datatabase.connect))
            {
                if (myCon.State == ConnectionState.Closed)
                {
                    myCon.Open();
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myCon;
                SqlTransaction transaction;
                transaction = myCon.BeginTransaction();
                cmd.Transaction = transaction;
                try
                {
                    cmd.CommandText = @"SELECT lsct.MaPT as 'Mã phụ tùng', pt.TenPT as 'Tên phụ tùng', SUM(lsct.Soluong) as 'Số lượng', lsct.Gia as 'Đơn giá', SUM(lsct.GiaTien) as 'Tiền' from dbo.LichSuBaoDuongChiTiet2 lsct    
                                      inner join dbo.PhuTung pt on pt.MaPT = lsct.MaPT where IdBaoDuong In (SELECT IdBaoDuong From dbo.LichSuBaoDuongXe where CONVERT(varchar(25),NgayGiaoXe,126)   
                                      like @NgayGiaoXe and IdCongty = @IdCongty)  and lsct.IdKho = @IdKho and pt.IdKho = @IdKho GROUP BY lsct.MaPT, pt.TenPT, lsct.Gia";
                    cmd.Parameters.Clear();
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
                    cmd.Parameters.AddWithValue("@NgayGiaoXe", "%" + dateSearchReal.Trim() + "%");
                    cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                    DataTable bangPhuTung = new DataTable();
                    dgvDanhSachBaoDuong.DataSource = bangPhuTung;
                    dataAdapter.Fill(bangPhuTung);
                    object SumMoney;
                    if(bangPhuTung.Rows.Count > 0)
                    {
                        SumMoney = bangPhuTung.Compute("Sum(Tiền)", "");
                    }
                    else
                    {
                        SumMoney = 0;
                    }
                    lbTotalMoney.Text = string.Format("{0:#,#.}", SumMoney);
                    dgvDanhSachBaoDuong.DataSource = bangPhuTung;
                    lbSoLuong.Text = dgvDanhSachBaoDuong.Rows.Count.ToString();
                    transaction.Commit();
                    if (myCon.State == ConnectionState.Open) myCon.Close();
                }
                catch
                {
                    transaction.Rollback();
                    if (myCon.State == ConnectionState.Open) myCon.Close();
                }
                
            }
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

        private void UcThongKeThuNhapTheoNgay_Load(object sender, EventArgs e)
        {
            dateTimeInput.Value = DateTime.Now;
            LayDanhSachKho();
        }
    }
}
