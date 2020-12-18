using ExcelDataReader;
using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmPhieuXuatKhoNgoai : Form
    {
        public string __idXuatKho = "";
        private string _idKho = "";
        private string _idCongTy = "";
        private string _idCuaHang = "";
        public string _trangThaiPhieu = "";
        private DataTableCollection tables;
        List<PhuTungXuatNgoai> listPhuTungDuaLenDB;
        public frmPhieuXuatKhoNgoai()
        {
            InitializeComponent();
        }

        private void FrmPhieuXuatKhoNgoai_Load(object sender, EventArgs e)
        {
            if (_trangThaiPhieu.Equals("dx"))
            {
                btnHuyPhieu.Enabled = false;
                button1.Enabled = false;
                cboSheet.Enabled = false;
                btnXacNhanPhieu.Enabled = false;
                btnTaiLaiPhieu.Enabled = false;
            }
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"Select *from dbo.LichSuXuatKhoNgoai where IdXuatKho = @idxuatkho";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(__idXuatKho));
            DataTable trangthai = Class.datatabase.getData(cmd);
            if(trangthai.Rows.Count > 0)
            {
                _idKho = trangthai.Rows[0]["IdKhoXuat"].ToString().Trim();
                _idCongTy = trangthai.Rows[0]["IdCongTy"].ToString().Trim();
                _idCuaHang = trangthai.Rows[0]["IdCuaHang"].ToString().Trim();
                if(int.Parse(trangthai.Rows[0]["TrangThaiXuat"].ToString().Trim()) == 1)
                {
                    label2.Text = "Phiếu này đã xuất";
                }
                else
                {
                    label2.Text = "Phiếu này chưa xuất";
                }
            }
            else
            {
                MessageBox.Show("Tải phiếu lỗi hoặc phiếu đã bị xóa\nHãy thao tác lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            cmd.CommandText = @"SELECT ROW_NUMBER() OVER (ORDER BY MaPT) AS 'STT', MaPT, TenPT, SoLuong, DonGia, SoLuong*DonGia as 'TongTien',IdCongTy,IdKho From dbo.LichSuXuatKhoNgoaiChiTiet where IdXuatKho = @idxuatkho";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(__idXuatKho));

            DataTable dsPhuTung = Class.datatabase.getData(cmd);
            object SumMoney;
            if(dsPhuTung.Rows.Count > 0)
            {
                SumMoney = dsPhuTung.Compute("Sum(TongTien)", "");
            }
            else
            {
                SumMoney = 0;
            }
            string[] ngayGioXuatPhieu = trangthai.Rows[0]["NgayXuat"].ToString().Split(' ');
            string[] ngayThangXuat = ngayGioXuatPhieu[0].Split('/');
            if (_trangThaiPhieu.Equals("dx"))
            {
                Microsoft.Reporting.WinForms.ReportParameter[] rPrams = new Microsoft.Reporting.WinForms.ReportParameter[]
                {
                    new Microsoft.Reporting.WinForms.ReportParameter("GioXuat",ngayGioXuatPhieu[1] +" " +ngayGioXuatPhieu[2]),
                    new Microsoft.Reporting.WinForms.ReportParameter("NgayXuat", ngayThangXuat[2]),
                    new Microsoft.Reporting.WinForms.ReportParameter("ThangXuat", ngayThangXuat[1]),
                    new Microsoft.Reporting.WinForms.ReportParameter("NamXuat", ngayThangXuat[0]),
                    new Microsoft.Reporting.WinForms.ReportParameter("KhoXuat", Class.CompanyInfo.tencongty),
                    new Microsoft.Reporting.WinForms.ReportParameter("KhoNhap", trangthai.Rows[0]["TenCongTyNhan"].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("TongTien", string.Format("{0:#,#.}", Decimal.Parse(SumMoney.ToString()))),
                };
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = dsPhuTung;

                reportViewer1.LocalReport.DataSources.Clear();

                reportViewer1.LocalReport.SetParameters(rPrams);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.RefreshReport();
                this.reportViewer1.RefreshReport();
            }
            else
            {
                Microsoft.Reporting.WinForms.ReportParameter[] rPrams = new Microsoft.Reporting.WinForms.ReportParameter[]
            {
                    new Microsoft.Reporting.WinForms.ReportParameter("GioXuat",DateTime.Now.ToString("HH:mm:ss")),
                    new Microsoft.Reporting.WinForms.ReportParameter("NgayXuat", DateTime.Now.ToString("dd")),
                    new Microsoft.Reporting.WinForms.ReportParameter("ThangXuat", DateTime.Now.ToString("MM")),
                    new Microsoft.Reporting.WinForms.ReportParameter("NamXuat", DateTime.Now.ToString("yyyy")),
                    new Microsoft.Reporting.WinForms.ReportParameter("KhoXuat", Class.CompanyInfo.tencongty),
                    new Microsoft.Reporting.WinForms.ReportParameter("KhoNhap", trangthai.Rows[0]["TenCongTyNhan"].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("TongTien", string.Format("{0:#,#.}", Decimal.Parse(SumMoney.ToString()))),
            };
                ReportDataSource reportDataSource = new ReportDataSource();
                reportDataSource.Name = "DataSet1";
                reportDataSource.Value = dsPhuTung;

                reportViewer1.LocalReport.DataSources.Clear();

                reportViewer1.LocalReport.SetParameters(rPrams);
                reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                reportViewer1.RefreshReport();
                this.reportViewer1.RefreshReport();
            }
        }

        private void BtnHuyPhieu_Click(object sender, EventArgs e)
        {
            using (SqlConnection myCon = new SqlConnection(Class.datatabase.connect))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myCon;
                if (myCon.State == ConnectionState.Closed)
                {
                    myCon.Open();
                }
                SqlTransaction transaction;
                transaction = myCon.BeginTransaction();
                cmd.Transaction = transaction;
                try
                {
                    cmd.CommandText = @"delete from dbo.LichSuXuatKhoNgoai where IdXuatKho = @idxuatkho";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(__idXuatKho));
                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataTable xoa = new DataTable();
                    ad.Fill(xoa);

                    cmd.CommandText = @"delete dbo.LichSuXuatKhoNgoaiChiTiet where IdXuatKho = @idxuatkho";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(__idXuatKho));
                    SqlDataAdapter dapter = new SqlDataAdapter(cmd);
                    xoa = new DataTable();
                    dapter.Fill(xoa);

                    transaction.Commit();
                    MessageBox.Show("Hủy thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnXacNhanPhieu.Enabled = false;
                    btnTaiLaiPhieu.Enabled = false;
                    button1.Enabled = false;
                    cboSheet.Enabled = false;
                }
                catch(Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog choose = new OpenFileDialog() { Filter = "File Excel|*.xls;*xlsx" })
            {
                if (choose.ShowDialog() == DialogResult.OK)
                {
                    txbDuongDanFileExcel.Text = choose.FileName;
                    try
                    {
                        using (var stream = File.Open(txbDuongDanFileExcel.Text, FileMode.Open, FileAccess.Read))
                        {
                            using (IExcelDataReader reader = ExcelReaderFactory.CreateReader(stream))
                            {
                                DataSet result = reader.AsDataSet(new ExcelDataSetConfiguration()
                                {
                                    ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                                    {
                                        UseHeaderRow = true
                                    }
                                }); ;
                                tables = result.Tables;
                                cboSheet.Items.Clear();
                                foreach (DataTable table in tables)
                                {
                                    cboSheet.Items.Add(table.TableName);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
        }
        
        private void CboSheet_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dataTable = tables[cboSheet.SelectedItem.ToString()];
            if (dataTable != null)
            {
                listPhuTungDuaLenDB = new List<PhuTungXuatNgoai>();
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    PhuTungXuatNgoai obj = new PhuTungXuatNgoai();
                    obj.MaPT = dataTable.Rows[i]["MÃ PHỤ TÙNG"].ToString();
                    obj.TenPT = dataTable.Rows[i]["TÊN PHỤ TÙNG"].ToString();
                    obj.SoLuong = Int32.Parse(dataTable.Rows[i]["SỐ LƯỢNG"].ToString());
                    obj.DonGia = dataTable.Rows[i]["ĐƠN GIÁ"].ToString();
                    obj.ThanhTien = dataTable.Rows[i]["THÀNH TIỀN"].ToString();
                    listPhuTungDuaLenDB.Add(obj);
                }
                dataGridView1.DataSource = listPhuTungDuaLenDB;
                dataGridView1.Columns[0].HeaderText = "Mã phụ tùng";
                dataGridView1.Columns[1].HeaderText = "Tên phụ tùng";
                dataGridView1.Columns[2].HeaderText = "Số lượng";
                dataGridView1.Columns[3].HeaderText = "Đơn giá";
                dataGridView1.Columns[4].HeaderText = "Thành tiền";
            }
        }

        class PhuTungXuatNgoai
        {
            public string MaPT { set; get; }
            public string TenPT { set; get; }
            public int SoLuong { set; get; }
            public string DonGia { set; get; }
            
            public string ThanhTien { set; get; }
        }

        private void BtnTaiLaiPhieu_Click(object sender, EventArgs e)
        {
            btnTaiLaiPhieu.Enabled = false;
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
                    for (int i = 0; i < dataGridView1.Rows.Count; i++)
                    {
                        cmd.CommandText = @"Select * from dbo.LichSuXuatKhoNgoaiChiTiet where MaPT = @MaPT and IdCongTy = @IdCongTy and IdKho = @IdKho and IdXuatKho = @idxuatkho";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@MaPT", dataGridView1.Rows[i].Cells[0].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdKho", int.Parse(_idKho));
                        cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(__idXuatKho));

                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataTable phuTungTemp = new DataTable();

                        adap.Fill(phuTungTemp);

                        if (phuTungTemp.Rows.Count > 0)
                        {
                            int soluongtruoc = 0;
                            int soLuongtru = 0;
                            int soLuongSau = 0;
                            soluongtruoc = Convert.ToInt32(phuTungTemp.Rows[0]["SoLuong"]);
                            soLuongtru = int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim());
                            soLuongSau = soluongtruoc + soLuongtru;

                            cmd.CommandText = @"Update dbo.LichSuXuatKhoNgoaiChiTiet SET SoLuong = @SoLuong where MaPT = @MaPT and IdCongTy = @IdCongTy and IdKho = @IdKho and IdXuatKho = @idxuatkho";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@SoLuong", soLuongSau);
                            cmd.Parameters.AddWithValue("@MaPT", dataGridView1.Rows[i].Cells[0].Value.ToString().Trim());
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdKho", int.Parse(_idKho));
                            cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(__idXuatKho));

                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            cmd.CommandText = @"insert into dbo.LichSuXuatKhoNgoaiChiTiet(IdXuatKho, MaPT, TenPT, SoLuong, DonGia, IdKho, IdCongTy) values 
                                        (@idxuatkho, @MaPT, @TenPT, @SoLuong, @dongia, @idkho, @idcongty)";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(__idXuatKho));
                            cmd.Parameters.AddWithValue("@MaPT", dataGridView1.Rows[i].Cells[0].Value.ToString().Trim());
                            cmd.Parameters.AddWithValue("@TenPT", dataGridView1.Rows[i].Cells[1].Value.ToString().Trim());
                            cmd.Parameters.AddWithValue("@SoLuong", int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim()));
                            cmd.Parameters.AddWithValue("@dongia", int.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString().Trim()));
                            cmd.Parameters.AddWithValue("@idkho", int.Parse(_idKho));
                            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    cmd.CommandText = @"Select *from dbo.LichSuXuatKhoNgoai where IdXuatKho = @idxuatkho";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(__idXuatKho));
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable trangthai = new DataTable();
                    adapter.Fill(trangthai);
                    if (trangthai.Rows.Count > 0)
                    {

                    }
                    else
                    {
                        MessageBox.Show("Tải phiếu lỗi hoặc phiếu đã bị xóa\nHãy thao tác lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    cmd.CommandText = @"SELECT ROW_NUMBER() OVER (ORDER BY MaPT) AS 'STT', MaPT, TenPT, SoLuong, DonGia, SoLuong*DonGia as 'TongTien',IdCongTy,IdKho From dbo.LichSuXuatKhoNgoaiChiTiet where IdXuatKho = @idxuatkho";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(__idXuatKho));

                    SqlDataAdapter ad = new SqlDataAdapter(cmd);
                    DataTable pt = new DataTable();
                    ad.Fill(pt);
                    object SumMoney;
                    if(pt.Rows.Count > 0)
                    {
                        SumMoney = pt.Compute("Sum(TongTien)", "");
                    }
                    else
                    {
                        SumMoney = 0;
                    }
                    Microsoft.Reporting.WinForms.ReportParameter[] rPrams = new Microsoft.Reporting.WinForms.ReportParameter[]
                    {
                    new Microsoft.Reporting.WinForms.ReportParameter("GioXuat",DateTime.Now.ToString("HH:mm:ss")),
                    new Microsoft.Reporting.WinForms.ReportParameter("NgayXuat", DateTime.Now.ToString("dd")),
                    new Microsoft.Reporting.WinForms.ReportParameter("ThangXuat", DateTime.Now.ToString("MM")),
                    new Microsoft.Reporting.WinForms.ReportParameter("NamXuat", DateTime.Now.ToString("yyyy")),
                    new Microsoft.Reporting.WinForms.ReportParameter("KhoXuat", Class.CompanyInfo.tencongty),
                    new Microsoft.Reporting.WinForms.ReportParameter("KhoNhap", trangthai.Rows[0]["TenCongTyNhan"].ToString()),
                    new Microsoft.Reporting.WinForms.ReportParameter("TongTien", string.Format("{0:#,#.}", Decimal.Parse(SumMoney.ToString()))),
                    };
                    ReportDataSource reportDataSource = new ReportDataSource();
                    reportDataSource.Name = "DataSet1";
                    reportDataSource.Value = pt;
                    reportViewer1.LocalReport.DataSources.Clear();

                    reportViewer1.LocalReport.SetParameters(rPrams);
                    reportViewer1.LocalReport.DataSources.Add(reportDataSource);
                    reportViewer1.RefreshReport();
                    this.reportViewer1.RefreshReport();

                    dataGridView1.DataSource = pt;
                    dataGridView1.Columns.Remove("STT");
                    dataGridView1.Columns.Remove("IdCongTy");
                    dataGridView1.Columns.Remove("IdKho");
                    transaction.Commit();
                    MessageBox.Show("Tải thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
                finally { }
            }
        }

        private void BtnXacNhanPhieu_Click(object sender, EventArgs e)
        {
            btnHuyPhieu.Enabled = false;
            btnXacNhanPhieu.Enabled = false;
            if(btnTaiLaiPhieu.Enabled == true)
            {
                MessageBox.Show("Hãy tải phiếu trước khi xuất kho", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                btnHuyPhieu.Enabled = true;
                btnXacNhanPhieu.Enabled = true;
                return;
            }
            using (SqlConnection myCon = new SqlConnection(Class.datatabase.connect))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myCon;
                if (myCon.State == ConnectionState.Closed)
                {
                    myCon.Open();
                }
                SqlTransaction transaction;
                transaction = myCon.BeginTransaction();
                cmd.Transaction = transaction;
                try
                {
                    for(int i = 0; i < dataGridView1.Rows.Count - 1; i++)
                    {
                        cmd.CommandText = @"Select * from dbo.PhuTung where MaPT = @MaPT and IdCongTy = @IdCongTy and IdKho = @IdKho";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@MaPT", dataGridView1.Rows[i].Cells[0].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdKho", int.Parse(_idKho));

                        SqlDataAdapter adp = new SqlDataAdapter(cmd);
                        DataTable phuTungTemp = new DataTable();

                        adp.Fill(phuTungTemp);
                        if (phuTungTemp.Rows.Count == 1)
                        {
                            int soluongtruoc = 0;
                            int soLuongtru = 0;
                            int soLuongSau = 0;
                            soluongtruoc = Convert.ToInt32(phuTungTemp.Rows[0]["SoLuong"]);
                            soLuongtru = int.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString().Trim());
                            soLuongSau = soluongtruoc - soLuongtru;

                            cmd.CommandText = @"Update dbo.PhuTung SET SoLuong = @SoLuong where MaPT = @MaPT and IdCongTy = @IdCongTy and IdKho = @IdKho";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@SoLuong", soLuongSau);
                            cmd.Parameters.AddWithValue("@MaPT", dataGridView1.Rows[i].Cells[0].Value.ToString().Trim());
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdKho", int.Parse(_idKho));

                            cmd.ExecuteNonQuery();
                        }
                    }
                    cmd.CommandText = @"Update dbo.LichSuXuatKhoNgoai SET NgayXuat = @ngayxuat, TrangThaiXuat = 1 where IdCongTy = @IdCongTy and IdKhoXuat = @IdKho and IdXuatKho = @idxuatkho";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idxuatkho", int.Parse(__idXuatKho));
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdKho", int.Parse(_idKho));
                    cmd.Parameters.AddWithValue("@ngayxuat", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
                    cmd.ExecuteNonQuery();
                    transaction.Commit();
                    MessageBox.Show("Xác nhận thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
