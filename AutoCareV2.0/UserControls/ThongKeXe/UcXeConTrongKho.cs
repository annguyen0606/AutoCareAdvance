using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls
{
    public partial class UcXeConTrongKho : UserControl
    {
        public UcXeConTrongKho()
        {
            InitializeComponent();
        }

        private SqlCommand cmd;
        private DataTable dtColor = new DataTable();
        private DataTable dtnull = new DataTable();
        private DataTable dtProvider = new DataTable();
        private DataTable dtListDetail = new DataTable();
        private DataTable dtTypeMotor = new DataTable();
        private DataTable dtMotorName = new DataTable();

        private void LoadMotorName()
        {
            cmd = new SqlCommand("Select IdXe, IDXe + ' - ' + TenXe as TenXe From XeMay Where IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtMotorName = Class.datatabase.getData(cmd);
        }

        private void LoadTypeMotor()
        {
            cmd = new SqlCommand("select IdLoaiXe, TenLoaiXe, GhiChu from LoaiXe Where IdCongTy = @IdcongTy");
            cmd.Parameters.AddWithValue("@idCongTy", Class.CompanyInfo.idcongty);
            dtTypeMotor = Class.datatabase.getData(cmd);
        }

        private void LoadListDetailMotor()
        {
            cmd = new SqlCommand(@"Select ChiTietXe.IdKey, ChiTietXe.IdMauXe,ChiTietXe.SoKhung,ChiTietXe.SoMay,ChiTietXe.IdNhaCungCap,ChiTietXe.IdLoaiXe, KhoHang.TenKho, XeMay.TenXe 
                                From ChiTietXe inner join XeMay on XeMay.IdXe = ChiTietXe.IdKey
                                inner join KhoHang on KhoHang.IdKho=ChiTietXe.IdKho Where ChiTietXe.IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtListDetail = Class.datatabase.getData(cmd);
        }

        private void LoadProvider()
        {
            cmd = new SqlCommand("Select IdNhaCungCap, TenNhaCungCap From NhaCungCap Where IdCongTy =@IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtProvider = Class.datatabase.getData(cmd);
        }

        private void LoadColorMotor()
        {
            cmd = new SqlCommand("select IdMauXe, TenMauXe, GhiChu from MauXeMay Where IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtColor = Class.datatabase.getData(cmd);
        }

        private void UcXeConTrongKho_Load(object sender, EventArgs e)
        {
            dtnull.Columns.Add("IdKey", typeof(string));
            dtnull.Columns.Add("IdMauXe", typeof(string));
            dtnull.Columns.Add("SoKhung", typeof(string));
            dtnull.Columns.Add("SoMay", typeof(string));
            dtnull.Columns.Add("IdNhaCungCap", typeof(string));
            dtnull.Columns.Add("IdLoaiXe", typeof(string));
            LoadMotorName();
            //
            cboTenXe.DataSource = dtMotorName;
            cboTenXe.DisplayMember = "TenXe";
            cboTenXe.ValueMember = "IdXe";
            //
            LoadColorMotor();
            LoadProvider();
            NhaCungCap.DataSource = dtProvider;
            NhaCungCap.ValueMember = "IdNhaCungCap";
            NhaCungCap.DisplayMember = "TenNhaCungCap";
            //
            cboNhaCungCap.DataSource = dtProvider;
            cboNhaCungCap.ValueMember = "IdNhaCungCap";
            cboNhaCungCap.DisplayMember = "TenNhaCungCap";
            //
            TenMauXe.DataSource = dtColor;
            TenMauXe.ValueMember = "IdMauXe";
            TenMauXe.DisplayMember = "TenMauXe";
            //
            cboMauXe.DataSource = dtColor;
            cboMauXe.ValueMember = "IdMauXe";
            cboMauXe.DisplayMember = "TenMauXe";
            //
            //LoadListDetailMotor();
            //dgvDanhSachXe.DataSource = dtListDetail;
            LoadTypeMotor();
            cboKieuXe.DataSource = dtTypeMotor;
            cboKieuXe.ValueMember = "IdLoaiXe";
            cboKieuXe.DisplayMember = "TenLoaiXe";
            //
            TenLoaiXe.DataSource = dtTypeMotor;
            TenLoaiXe.ValueMember = "IdLoaiXe";
            TenLoaiXe.DisplayMember = "TenLoaiXe";
        }

        private void cboTenXe_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = dtListDetail.Select("IdXe = '" + Convert.ToString(cboTenXe.SelectedValue) + "'");
                if (rows.Length > 0)
                {
                    dgvDanhSachXe.DataSource = rows.CopyToDataTable();
                }
                else
                {
                    dgvDanhSachXe.DataSource = dtnull;
                }
            }
            catch { }
        }

        private void cboKieuXe_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = dtListDetail.Select("IdLoaiXe = '" + Convert.ToString(cboKieuXe.SelectedValue) + "'");
                if (rows.Length > 0)
                {
                    dgvDanhSachXe.DataSource = rows.CopyToDataTable();
                }
                else
                {
                    dgvDanhSachXe.DataSource = dtnull;
                }
            }
            catch { }
        }

        private void cboMauXe_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataRow[] rows = dtListDetail.Select("idMauXe = '" + Convert.ToString(cboMauXe.SelectedValue) + "'");
                if (rows.Length > 0)
                {
                    dgvDanhSachXe.DataSource = rows.CopyToDataTable();
                }
                else
                {
                    dgvDanhSachXe.DataSource = dtnull;
                }
            }
            catch { }
        }

        private void txtSoKhung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow[] row = dtListDetail.Select("SoKhung = '" + txtSoKhung.Text + "'");
                if (row.Length > 0)
                {
                    dgvDanhSachXe.DataSource = row.CopyToDataTable();
                }
                else
                {
                    dgvDanhSachXe.DataSource = dtnull;
                }
            }
        }

        private void txtSoMay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataRow[] row = dtListDetail.Select("SoMay = '" + txtSoMay.Text + "'");
                if (row.Length > 0)
                {
                    dgvDanhSachXe.DataSource = row.CopyToDataTable();
                }
                else
                {
                    dgvDanhSachXe.DataSource = dtnull;
                }
            }
        }

        private void btnTatCa_Click(object sender, EventArgs e)
        {
            LoadListDetailMotor();
            if (dtListDetail.Rows.Count > 0)
            {
                dgvDanhSachXe.DataSource = dtListDetail;
            }
        }

        private void ReloadXe()
        {
            object obj = new object();
            btnTatCa_Click(obj, new EventArgs());
        }

        private void dgvDanhSachXe_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                try
                {
                    cboNhaCungCap.SelectedValue = dgvDanhSachXe.Rows[e.RowIndex].Cells["NhaCungCap"].Value;
                    cboMauXe.SelectedValue = dgvDanhSachXe.Rows[e.RowIndex].Cells["TenMauXe"].Value;
                    cboTenXe.SelectedValue = dgvDanhSachXe.Rows[e.RowIndex].Cells["MaXe"].Value;
                    cboKieuXe.SelectedValue = dgvDanhSachXe.Rows[e.RowIndex].Cells["TenLoaiXe"].Value;
                    txtSoKhung.Text = Convert.ToString(dgvDanhSachXe.Rows[e.RowIndex].Cells["SoKhung"].Value);
                    txtSoMay.Text = Convert.ToString(dgvDanhSachXe.Rows[e.RowIndex].Cells["SoMay"].Value);
                }
                catch (Exception ex) { MessageBox.Show(ex.Message); }
            }

            if (e.RowIndex >= 0 && e.ColumnIndex == 0)
            {
                if (MessageBox.Show("Bạn có muốn xóa xe đã chọn không?", "Xóa xe trong kho!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string MaXe = dgvDanhSachXe.Rows[e.RowIndex].Cells["MaXe"].Value.ToString();
                    string SoKhung = dgvDanhSachXe.Rows[e.RowIndex].Cells["SoKhung"].Value.ToString();
                    string SoMay = dgvDanhSachXe.Rows[e.RowIndex].Cells["SoMay"].Value.ToString();

                    try
                    {
                        SqlCommand cmDeleteXe = new SqlCommand("delete ChiTietXe where IdCongTy = @IdCongTy and IdKey = @IdKey and SoKhung = @SoKhung and SoMay = @SoMay");
                        cmDeleteXe.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmDeleteXe.Parameters.AddWithValue("@IdKey", MaXe);
                        cmDeleteXe.Parameters.AddWithValue("@SoKhung", SoKhung);
                        cmDeleteXe.Parameters.AddWithValue("@SoMay", SoMay);

                        Class.datatabase.ExcuteNonQuery(cmDeleteXe);

                        MessageBox.Show("Xóa xe thành công!");
                        ReloadXe();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Xóa xe không thành công! Lỗi: " + ex.Message);
                    }                    
                }
            }
        }

        private void btnXuatBaoCao_Click(object sender, EventArgs e)
        {
            if (dtListDetail.Rows.Count > 0)
            {
                Export(dtListDetail, "Danh sach xe", "Danh sách xe còn trong kho");
            }
            else
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }
        }

        private void Export(DataTable dt, string sheetName, string title)
        {
            //Tạo các đối tượng Excel
            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();
            Microsoft.Office.Interop.Excel.Workbooks oBooks;
            Microsoft.Office.Interop.Excel.Sheets oSheets;
            Microsoft.Office.Interop.Excel.Workbook oBook;
            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook
            oExcel.Visible = true;
            oExcel.DisplayAlerts = false;
            oExcel.Application.SheetsInNewWorkbook = 1;
            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));
            oSheets = oBook.Worksheets;
            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);
            oSheet.Name = sheetName;
            try
            {
                // Tạo phần đầu nếu muốn
                Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "O1");
                head.MergeCells = true;
                head.Value2 = title;
                head.Font.Bold = true;
                head.Font.Name = "Tahoma";
                head.Font.Size = "18";
                head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Tạo tiêu đề cột
                Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
                cl1.Value2 = "Tên KH";
                cl1.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
                cl2.Value2 = "Điện Thoại";
                cl2.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
                cl3.Value2 = "Giới Tính";
                cl3.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
                cl4.Value2 = "Ngày Sinh";
                cl4.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
                cl5.Value2 = "Địa Chỉ";
                cl5.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
                cl6.Value2 = "Số CMND";
                cl6.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
                cl7.Value2 = "Biển Số";
                cl7.ColumnWidth = 25.0;

                Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
                cl8.Value2 = "Số Khung";
                cl8.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
                cl9.Value2 = "Số Máy";
                cl9.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl10 = oSheet.get_Range("J3", "J3");
                cl10.Value2 = "Ngày Mua Xe";
                cl10.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl11 = oSheet.get_Range("K3", "K3");
                cl11.Value2 = "Khách Đến Từ";
                cl11.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl12 = oSheet.get_Range("L3", "L3");
                cl12.Value2 = "Lần Bảo Dưỡng";
                cl12.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl13 = oSheet.get_Range("M3", "M3");
                cl13.Value2 = "Ghi Chú";
                cl13.ColumnWidth = 20.0;

                //Microsoft.Office.Interop.Excel.Range cl14 = oSheet.get_Range("N3", "N3");
                //cl14.Value2 = "LanBaoDuong";
                //cl14.ColumnWidth = 10.0;

                //Microsoft.Office.Interop.Excel.Range cl15 = oSheet.get_Range("O3", "O3");
                //cl15.Value2 = "GhiChu";
                //cl15.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "O3");
                rowHead.Font.Bold = true;

                // Kẻ viền
                rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                // Thiết lập màu nền
                rowHead.Interior.ColorIndex = 15;
                rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

                // Tạo mẳng đối tượng để lưu dữ toàn bồ dữ liệu trong DataTable,
                // vì dữ liệu được được gán vào các Cell trong Excel phải thông qua object thuần.
                object[,] arr = new object[dt.Rows.Count, dt.Columns.Count];

                //Chuyển dữ liệu từ DataTable vào mảng đối tượng
                for (int r = 0; r < dt.Rows.Count; r++)
                {
                    DataRow dr = dt.Rows[r];
                    for (int c = 0; c < dt.Columns.Count; c++)
                    {
                        arr[r, c] = dr[c];
                    }
                }

                //Thiết lập vùng điền dữ liệu
                int rowStart = 4;
                int columnStart = 1;

                int rowEnd = rowStart + dt.Rows.Count - 1;
                int columnEnd = dt.Columns.Count;

                // Ô bắt đầu điền dữ liệu
                Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];
                // Ô kết thúc điền dữ liệu
                Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];
                // Lấy về vùng điền dữ liệu
                Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

                //Điền dữ liệu vào vùng đã thiết lập
                range.Value2 = arr;

                // Kẻ viền
                range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;
                // Căn giữa cột STT
                Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];
                Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);
                oSheet.get_Range(c3, c4).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
            }
            catch (Exception theException)
            {
                String errorMessage;
                errorMessage = "Error: ";
                errorMessage = String.Concat(errorMessage, theException.Message);
                errorMessage = String.Concat(errorMessage, " Line: ");
                errorMessage = String.Concat(errorMessage, theException.Source);

                MessageBox.Show(errorMessage, "Error");
            }
        }

        private void dgvDanhSachXe_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string MaXe = dgvDanhSachXe.Rows[e.RowIndex].Cells["MaXe"].Value.ToString();
                string TenXe = dgvDanhSachXe.Rows[e.RowIndex].Cells["TenXe"].Value.ToString();
                string MauXe = dgvDanhSachXe.Rows[e.RowIndex].Cells["TenMauXe"].Value.ToString();
                string LoaiXe = dgvDanhSachXe.Rows[e.RowIndex].Cells["TenLoaiXe"].Value.ToString();
                string SoKhung = dgvDanhSachXe.Rows[e.RowIndex].Cells["SoKhung"].Value.ToString();
                string SoMay = dgvDanhSachXe.Rows[e.RowIndex].Cells["SoMay"].Value.ToString();

                frmCapNhatThongTinXe frm = new frmCapNhatThongTinXe();
                frm.IdKey = MaXe;
                frm.TenXe = TenXe;
                frm.MauXe = MauXe;
                frm.LoaiXe = LoaiXe;
                frm.SoKhung = SoKhung;
                frm.SoMay = SoMay;
                frm.ReloadXe = ReloadXe;
                frm.ShowDialog();
            }
        }
    }
}