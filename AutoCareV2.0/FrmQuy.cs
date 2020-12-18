using AutoCareV2._0.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmQuy : Form
    {
        private decimal _tongTienThu = 0m;
        private int _rowIndexPhieu = -1;

        /// <summary>
        /// Initializes a new instance of the <see cref="frmQuy"/> class.
        /// </summary>
        public frmQuy()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Load event of the frmQuy control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void frmQuy_Load(object sender, EventArgs e)
        {
            dateTimeInputDenNgay.Value = DateTime.Now;
            dateTimeInputTuNgay.Value = DateTime.Now;

            LoadDanhSachCuaHang(CompanyInfo.idcongty);
        }

        /// <summary>
        /// Loads the danh sach cua hang.
        /// </summary>
        /// <param name="IdCongTy">The identifier cong ty.</param>
        /// <exception cref="System.NotImplementedException"></exception>
        private void LoadDanhSachCuaHang(string IdCongTy)
        {
            using (SqlCommand cmd = new SqlCommand("select * from CuaHang where IdCongTy = @IdCongTy"))
            {
                cmd.Parameters.AddWithValue("@IdCongTy", IdCongTy);
                DataTable tblCuaHang = datatabase.getData(cmd);

                cbbCuaHang.ValueMember = "IdCuaHang";
                cbbCuaHang.DisplayMember = "TenCuaHang";
                cbbCuaHang.DataSource = tblCuaHang;
                cbbCuaHang.SelectedValue = Class.EmployeeInfo.IdCuaHang;

                if (Class.EmployeeInfo.Quyen.ToLower() != "qtv")
                    cbbCuaHang.Enabled = false;
                else
                    cbbCuaHang.Enabled = true;
            }
        }

        /// <summary>
        /// Handles the CellClick event of the dgvPhieuThu control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DataGridViewCellEventArgs"/> instance containing the event data.</param>
        private void dgvPhieuThu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 && e.ColumnIndex >= 0)
            {
                dgvPhieuThu.GroupTemplate.Column = dgvPhieuThu.Columns[e.ColumnIndex];
            }
        }

        /// <summary>
        /// Handles the Click event of the btnThongKe control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void btnThongKe_Click(object sender, EventArgs e)
        {
            if (dateTimeInputDenNgay.ValueObject == null || dateTimeInputTuNgay.ValueObject == null)
            {
                MessageBox.Show(@"Bạn chưa nhập thời thời thống kê.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (dateTimeInputDenNgay.Value.AddSeconds(1) < dateTimeInputTuNgay.Value)
            {
                MessageBox.Show(@"Chọn thời gian thống kê không hợp lệ.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            _tongTienThu = 0;
            txtTienChiKhac.Text = @"0";
            txtTienNhapXe.Text = @"0";
            txtTienPhuTung.Text = @"0";
            txtTienThuBanBuon.Text = @"0";
            txtTienThuBanLe.Text = @"0";
            txtTienThuKhac.Text = @"0";
            txtTienThuTraGop.Text = @"0";
            txtTongTienChi.Text = @"0";
            txtTongTienThu.Text = @"0";
            txtTienbaoduong.Text = @"0";
            dgvPhieuChi.Rows.Clear();
            dgvPhieuThu.Rows.Clear();
            lblDenNgay.Text = dateTimeInputDenNgay.Value.ToShortDateString();
            lblTuNgay.Text = dateTimeInputTuNgay.Value.ToShortDateString();
            lbltiendutruocngay.Text = dateTimeInputTuNgay.Value.ToShortDateString();
            lblNgayTongTienDu.Text = dateTimeInputDenNgay.Value.ToShortDateString();

            //Lấy id của hàng đã chọn
            string idCuaHang = cbbCuaHang.SelectedValue != null ? cbbCuaHang.SelectedValue.ToString() : "0";

            #region Tien Thu
            //Load Chi Tiet Phieu Thu
            var cmd = new SqlCommand("sp_ThongKeThuQuy")
            {
                Connection = Class.datatabase.getConnection(),
                CommandType = CommandType.StoredProcedure
            };

            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@IdCuaHang", idCuaHang);
            cmd.Parameters.AddWithValue("@BatDau", dateTimeInputTuNgay.Value.ToString("yyyyMMdd"));
            cmd.Parameters.AddWithValue("@KetThuc", dateTimeInputDenNgay.Value.ToString("yyyyMMdd"));

            if (cmd.Connection.State == ConnectionState.Closed)
                cmd.Connection.Open();

            var ds = new DataSet();
            var adap = new SqlDataAdapter(cmd);
            adap.Fill(ds);

            foreach (DataRow reder in ds.Tables[0].Rows)
            {
                dgvPhieuThu.Rows.Add(reder["TenLoaiPhieuThu"], reder["IdPhieuThu"], reder["SoHoaDon"], reder["SoPhieu"], reder["SoHopDong"], string.Format("{0:dd/MM/yyyy}", reder["NgayHachToan"]), string.Format("{0:0,0}", reder["SoTienThu"]), reder["TenNhanVien"], reder["GhiChu"], reder["IdBaoDuong"]);
            }

            _tongTienThu = Convert.ToDecimal(ds.Tables[2].Rows[0]["TongThu"].ToString());
            txtTongTienThu.Text = string.Format("{0:0,0}", _tongTienThu);

            //Thu ban le
            txtTienThuBanLe.Text = string.Format("{0:0,0}", ds.Tables[2].Rows[0]["ThuBanLe"]);

            //Thu Bao duong
            txtTienbaoduong.Text = string.Format("{0:0,0}", ds.Tables[2].Rows[0]["ThuBaoDuong"]);

            //Thu ban buon
            txtTienThuBanBuon.Text = string.Format("{0:0,0}", ds.Tables[2].Rows[0]["ThuBanBuon"]);

            //Thu tra gop
            txtTienThuTraGop.Text = string.Format("{0:0,0}", ds.Tables[2].Rows[0]["ThuTraGop"]);

            //Thu khac
            txtTienThuKhac.Text = string.Format("{0:0,0}", ds.Tables[2].Rows[0]["ThuKhac"]);

            # endregion

            #region Tien Chi

            //Load Chi Tiet Phieu Chi
            decimal tongTienChi = 0m;

            foreach (DataRow reder in ds.Tables[1].Rows)
            {
                dgvPhieuChi.Rows.Add(reder["TenLoaiPhieuChi"], string.Format("{0:dd/MM/yyyy}", reder["NgayHachToan"]), reder["SoPhieuChi"], reder["SoHoaDon"], string.Format("{0:0,0}", reder["SoTienChi"]), reder["NguoiNhan"], reder["NoiDung"], reder["TenNhanVien"]);
            }

            tongTienChi = Convert.ToDecimal(ds.Tables[2].Rows[0]["TongChi"].ToString());
            txtTongTienChi.Text = string.Format("{0:0,0}", tongTienChi);

            //Chi nhap xe
            txtTienNhapXe.Text = string.Format("{0:0,0}", ds.Tables[2].Rows[0]["ChiNhapXe"]);

            //Chi nhap phu tung
            txtTienPhuTung.Text = string.Format("{0:0,0}", ds.Tables[2].Rows[0]["ChiNhapPT"]);

            //Chi khac
            txtTienChiKhac.Text = string.Format("{0:0,0}", ds.Tables[2].Rows[0]["ChiKhac"]);

            txtDuTheoThoiGian.Text = string.Format("{0:0,0}", _tongTienThu - tongTienChi);

            #endregion Tien Chi

            #region Tong Tien Thu
            decimal tongThu = 0m;

            tongThu = Convert.ToDecimal(ds.Tables[2].Rows[0]["ThuTruocKy"].ToString());

            #endregion Tong Tien Thu

            #region Tong Tien Chi
            decimal tongChi = 0m;

            tongChi = Convert.ToDecimal(ds.Tables[2].Rows[0]["ChiTruocKy"].ToString());

            if (cmd.Connection.State == ConnectionState.Open)
                cmd.Connection.Close();

            #endregion Tong Tien Chi

            txtDuTruocThoiGian.Text = string.Format("{0:0,0}", tongThu - tongChi);
            txtDuToiNgay.Text = string.Format("{0:0,0}", (_tongTienThu - tongTienChi) + (tongThu - tongChi));
        }

        private void dgvPhieuChi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex < 0)
            {
                dgvPhieuChi.GroupTemplate.Column = dgvPhieuChi.Columns[e.ColumnIndex];
            }
        }

        private void dgvPhieuChi_MouseDown(object sender, MouseEventArgs e)
        {
            DataGridView.HitTestInfo hit;
            if (e.Button == MouseButtons.Right)
            {
                hit = dgvPhieuChi.HitTest(e.X, e.Y);
                if (hit.Type == DataGridViewHitTestType.Cell)
                {
                    buttonItem1.Enabled = true;
                    if (!((DataGridViewRow)(dgvPhieuChi.Rows[hit.RowIndex])).Selected)
                    {
                        dgvPhieuChi.ClearSelection();
                        ((DataGridViewRow)(dgvPhieuChi.Rows[hit.RowIndex])).Selected = true;
                    }
                }
            }
            else
            {
                buttonItem1.Enabled = false;
            }
        }

        private void btnXuatThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select TenLoaiPhieuChi,NgayHachToan,SoTienChi,TenNhaCungCap + ' ' + NguoiNhan As TenNhaCungCap,NoiDung from PhieuChi inner join LoaiPhieuChi ON " +
                " PhieuChi.IdLoaiPhieuChi = LoaiPhieuChi.IdLoaiPhieuChi inner join NhaCungCap On NhaCungCap.IdNhaCungCap = PhieuChi.IdNhaCungCap Where PhieuChi.IdCongTy = @IDCongTy And NgayHachToan between Convert(date,@TuNgay) And Convert(date,@DenNgay)";
                cmd.Parameters.AddWithValue("@IDCongTy", CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@TuNgay", dateTimeInputTuNgay.Value);
                cmd.Parameters.AddWithValue("@DenNgay", dateTimeInputDenNgay.Value);
                DataTable dtChiTien2 = datatabase.getData(cmd);
                DataTable dtThoiGian = new DataTable();
                dtThoiGian.Columns.Add("TuNgay", typeof(DateTime));
                dtThoiGian.Columns.Add("DenNgay", typeof(DateTime));
                DataRow rows = dtThoiGian.NewRow();
                rows["TuNgay"] = dateTimeInputTuNgay.Value;
                rows["DenNgay"] = dateTimeInputDenNgay.Value;
                dtThoiGian.Rows.Add(rows);
                frmThongKe frm = new frmThongKe();
                frm.reportViewer1.LocalReport.DataSources.Clear();
                frm.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.ReportThongKeChiTien.rdlc";
                cmd.CommandText = "select * from CongTy where IdCongTy=" + Class.CompanyInfo.idcongty;
                DataTable dtThongTin = new DataTable();
                dtThongTin = Class.datatabase.getData(cmd);
                Microsoft.Reporting.WinForms.ReportDataSource data3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet3", dtThongTin);
                Microsoft.Reporting.WinForms.ReportDataSource data1 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtChiTien2);
                Microsoft.Reporting.WinForms.ReportDataSource data2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtThoiGian);
                frm.reportViewer1.LocalReport.DataSources.Add(data1);
                frm.reportViewer1.LocalReport.DataSources.Add(data2);
                frm.reportViewer1.LocalReport.DataSources.Add(data3);
                frm.ShowDialog();
            }
            catch (Exception ex) { MessageBox.Show(@"Lỗi : " + ex.Message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
        }

        private void inPhieuBaoDuongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rowIndexPhieu == -1) return;
            try
            {
                Class.SelectedCustomer._idbaoduong = dgvPhieuThu.Rows[_rowIndexPhieu].Cells["IdBaoDuong"].Value.ToString();

                if (Convert.ToInt64(Class.CompanyInfo.idcongty) != 31)
                {
                    var frm = new FrmPhieuSuaChuaThangLoi();
                    frm.ShowDialog();
                }
                else
                {
                    var frm = new frmPhieuSuaChuaTM98();
                    frm.ShowDialog();
                }
            }
            catch
            {
                // ignored
            }
            finally { Class.SelectedCustomer._idbaoduong = ""; }
        }

        private void dgvPhieuThu_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                dgvPhieuThu.ContextMenuStrip = contextMenuStrip;

                try
                {
                    if (dgvPhieuThu.CurrentRow == null) return;
                    _rowIndexPhieu = dgvPhieuThu.CurrentRow.Cells["IdBaoDuong"].Value.ToString() != "0"
                        ? dgvPhieuThu.CurrentRow.Index
                        : -1;
                }
                catch (Exception ex)
                {
                    _rowIndexPhieu = -1;
                }
            }
            else
            {
                dgvPhieuThu.ContextMenuStrip = null;
                _rowIndexPhieu = -1;
            }
        }
    }
}