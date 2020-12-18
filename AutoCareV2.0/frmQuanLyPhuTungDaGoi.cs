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
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmQuanLyPhuTungDaGoi : Form
    {
        public string IdBaoDuong = "";
        List<DanhSachXeOrder> LayDanhSachXeBaoDuongTrongNgay;
        public delegate void LoadDanhSachPhuTung();

        public LoadDanhSachPhuTung LayPhuTungBaoDuong;
        public LoadDanhSachPhuTung CallFromUcBaoDuong;
        public frmQuanLyPhuTungDaGoi()
        {
            InitializeComponent();
            this.ActiveControl = txtBarcode;
            txtBarcode.Focus();

        }
        private void FrmQuanLyPhuTungDaGoi_Load(object sender, EventArgs e)
        {
            if (Class.EmployeeInfo.UserName == "vietlong2kho"
                || Class.EmployeeInfo.UserName == "vietlong3kho"
                || Class.EmployeeInfo.UserName == "vietlong1kho"
                || Class.EmployeeInfo.UserName == "vietlong2khoadmin"
                || Class.EmployeeInfo.UserName == "vietlong3khoadmin"
                || Class.EmployeeInfo.UserName == "vietlong1khoadmin")
            {
                btnXacNhanXuatKho.Enabled = false;
            }
            if (Class.EmployeeInfo.UserName == "vietlong2sale"
                || Class.EmployeeInfo.UserName == "vietlong3sale"
                || Class.EmployeeInfo.UserName == "vietlong1sale"
                || Class.EmployeeInfo.UserName == "vietlong2saleadmin"
                || Class.EmployeeInfo.UserName == "vietlong3saleadmin"
                || Class.EmployeeInfo.UserName == "vietlong1saleadmin")
            {
                btnXacNhanNhapLaiKho.Enabled = false;
            }
            //Tạo data grid view để nhận mã barcode sau khi quét
            dgvPhuTungTiepNhan.ColumnCount = 2;
            dgvPhuTungTiepNhan.Columns[0].Name = "Mã phụ tùng";
            dgvPhuTungTiepNhan.Columns[1].Name = "Số lượng";
            btnRevertPhuTung.Enabled = false;
            LayDanhSachXe();
            LayDanhSachKho();
            LoadThoDichVu();
            LoadPhuTung(int.Parse(cboKho.SelectedValue.ToString()));
        }

        #region Kiem Tra Tien Do Lay Phu Tung
        private void KiemTraTienDoGiaoNhan()
        {
            if (cbDanhSachXe.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa lấy danh sách xe!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            else
            {
                if (cbDanhSachXe.Text.Equals(""))
                {
                    //MessageBox.Show("Bạn chưa chọn xe", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    return;
                }
                else
                {
                    DateTime now = DateTime.Now;
                    int IdBaoDuongXe = 0;
                    IdBaoDuongXe = int.Parse(cbDanhSachXe.SelectedValue.ToString().Trim());
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = @"Select IdBaoDuong, MaPT as 'Mã phụ tùng', TenPT as 'Tên phụ tùng', SoLuong as 'Số lượng',IdCongTy, IdCuaHang, IdKho, TrangThaiGoi, TranThaiTiepNhan, Ngay, SoLuongCurrent, Gia, IdPhuTung, idTho, TienTraTho from dbo.PhuTungOrder052020 " +
                        "WHERE IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang and IdBaoDuong = @IdBaoDuong and TrangThaiGoi = 0";
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdCuaHang", Class.CompanyInfo.IdsCuaHang);
                    //cmd.Parameters.AddWithValue("@IdKho", int.Parse(cboKho.SelectedValue.ToString()));
                    cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                    dgvPhuTungOrder.Columns.Clear();

                    DataTable dt = null;
                    dgvPhuTungOrder.DataSource = dt;
                    dt = Class.datatabase.getData(cmd);
                    dgvPhuTungOrder.DataSource = dt;

                    dgvPhuTungOrder.Columns["TrangThaiGoi"].Visible = false;
                    dgvPhuTungOrder.Columns["TranThaiTiepNhan"].Visible = false;
                    dgvPhuTungOrder.Columns["IdKho"].Visible = false;
                    dgvPhuTungOrder.Columns["IdCongTy"].Visible = false;
                    dgvPhuTungOrder.Columns["IdCuaHang"].Visible = false;
                    dgvPhuTungOrder.Columns["IdBaoDuong"].Visible = false;
                    dgvPhuTungOrder.Columns["SoLuongCurrent"].Visible = false;
                    dgvPhuTungOrder.Columns["Gia"].Visible = false;
                    dgvPhuTungOrder.Columns["IdPhuTung"].Visible = false;
                    dgvPhuTungOrder.Columns["idTho"].Visible = false;
                    dgvPhuTungOrder.Columns["TienTraTho"].Visible = false;

                    for (int i = 0; i < dgvPhuTungOrder.Rows.Count - 1; i++)
                    {
                        int statusOrder = int.Parse(dgvPhuTungOrder.Rows[i].Cells[7].Value.ToString());
                        int statusConfirm = int.Parse(dgvPhuTungOrder.Rows[i].Cells[8].Value.ToString());
                        if (statusOrder == 0 && statusConfirm == 0)
                        {
                            dgvPhuTungOrder.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                        }
                        else if (statusOrder == 0 && statusConfirm == 1)
                        {
                            dgvPhuTungOrder.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                        }
                        else if (statusOrder == 1 && statusConfirm == 0)
                        {
                            dgvPhuTungOrder.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                        }
                        if (dgvPhuTungOrder.Columns.Count == 15)
                        {
                            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                            btn.HeaderText = "Xóa";
                            btn.Name = "btnXoaPTOrder";
                            btn.Text = "Xóa";
                            btn.UseColumnTextForButtonValue = true;
                            dgvPhuTungOrder.Columns.Add(btn);
                        }
                    }
                }
            }
        }
        #endregion Kiem Tra Tien Do Lay Phu Tung
        #region Lay Danh sach kho
        public void LayDanhSachKho()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT IdKho, TenKho FROM dbo.KhoHang WHERE IdCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cboKho.DisplayMember = "TenKho";
            cboKho.ValueMember = "IdKho";
            cboKho.DataSource = Class.datatabase.getData(cmd);

            cboKho.SelectedIndex = 0;
            cboKho.Enabled = false;
        }
        #endregion Lay Danh sach kho
        #region Lay Danh sach xe
        public void LayDanhSachXe()
        {
            DataTable _dtxeBaoDuong = null;
            List<String> danhSachBienSo = new List<string>();
            LayDanhSachXeBaoDuongTrongNgay = new List<DanhSachXeOrder>();
            SqlCommand _cmd = new SqlCommand();

            _cmd.CommandText = @"select TenXe,BienSo,Sokhung,SoMay from dbo.LichSuBaoDuongXeTam
                                where IdCongty=@IdCongty and IdBaoDuong = @idbaoduong";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            _cmd.Parameters.AddWithValue("@idbaoduong", Int64.Parse(IdBaoDuong.ToString().Trim()));

            _dtxeBaoDuong = Class.datatabase.getData(_cmd);
            cbDanhSachXe.DisplayMember = "BienSo";
            cbDanhSachXe.ValueMember = "IdBaoDuong";
            cbDanhSachXe.DataSource = _dtxeBaoDuong;
            cbDanhSachXe.Enabled = false;
        }
        #endregion Lay Danh sach xe


        private void DgvPhuTungOrder_CellBorderStyleChanged(object sender, EventArgs e)
        {

        }
        #region Xoa phu tung order
        private void DgvPhuTungOrder_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        #endregion Xoa phu tung order
        #region Xac nhận nhận phụ tùng từ kho
        private void BtnXacNhanXuatKho_Click(object sender, EventArgs e)
        {
            if (dgvPhuTungOrder.Rows.Count < 1)
            {
                MessageBox.Show("Chưa kiểm tra phụ tùng xuất kho, nhập kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnXacNhanXuatKho.Enabled = false;
            int IdBaoDuongXe = 0;
            IdBaoDuongXe = int.Parse(IdBaoDuong.ToString().Trim());
            if (IdBaoDuongXe == 0)
            {
                MessageBox.Show("Bạn chưa chọn xe để xuất phụ tùng", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBox.Show("Bạn có thật sự muốn xuất kho", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Class.datatabase.getConnection();
                cmd.Connection.Open();
                SqlTransaction sqlTransaction = cmd.Connection.BeginTransaction();
                cmd.Transaction = sqlTransaction;
                cmd.CommandTimeout = 0;
                try
                {
                    for (int i = 0; i < dgvPhuTungOrder.Rows.Count - 1; i++)
                    {
                        if (dgvPhuTungOrder.Rows[i].Cells[10].Value.ToString().Equals("OK"))
                        {
                            SqlDataAdapter adp;
                            DataTable phuTungTemp = new DataTable();
                            SqlCommand cmdPhuTung = new SqlCommand();
                            cmdPhuTung.Connection = Class.datatabase.getConnection();
                            cmdPhuTung.Connection.Open();
                            SqlTransaction tranPhuTung = cmdPhuTung.Connection.BeginTransaction();
                            cmdPhuTung.Transaction = tranPhuTung;
                            try
                            {
                                /*Trừ số lượng phụ tùng trong kho*/
                                cmdPhuTung.CommandText = @"Select IdPT, MaPT, TenPT, SoLuong from dbo.PhuTung where MaPT = @MaPT and IdCongTy = @IdCongTy and IdKho = @IdKho";
                                cmdPhuTung.Parameters.Clear();
                                cmdPhuTung.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                cmdPhuTung.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmdPhuTung.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                                adp = new SqlDataAdapter(cmdPhuTung);
                                adp.Fill(phuTungTemp);
                                tranPhuTung.Commit();
                                cmdPhuTung.Connection.Close();
                                cmdPhuTung.Connection.Dispose();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(" Lỗi " + ex.Message);
                                tranPhuTung.Rollback();
                                //sqlTransaction.Rollback();
                                cmdPhuTung.Connection.Close();
                                cmdPhuTung.Connection.Dispose();
                            }
                            if (phuTungTemp.Rows.Count == 1)
                            {
                                int soluongtruoc = 0;
                                int soLuongtru = 0;
                                int soLuongSau = 0;
                                soluongtruoc = Convert.ToInt32(phuTungTemp.Rows[0]["SoLuong"]);
                                soLuongtru = int.Parse(dgvPhuTungOrder.Rows[i].Cells[2].Value.ToString().Trim());
                                soLuongSau = soluongtruoc - soLuongtru;
                                SqlCommand cmdUDPhuTung = new SqlCommand();
                                cmdUDPhuTung.Connection = Class.datatabase.getConnection();
                                cmdUDPhuTung.Connection.Open();
                                SqlTransaction tranUDPhuTung = cmdUDPhuTung.Connection.BeginTransaction();
                                cmdUDPhuTung.Transaction = tranUDPhuTung;
                                try
                                {
                                    cmdUDPhuTung.CommandText = @"Update dbo.PhuTung SET SoLuong = @SoLuong where MaPT = @MaPT and IdCongTy = @IdCongTy and IdKho = @IdKho";
                                    cmdUDPhuTung.Parameters.Clear();
                                    cmdUDPhuTung.Parameters.AddWithValue("@SoLuong", soLuongSau);
                                    cmdUDPhuTung.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                    cmdUDPhuTung.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    cmdUDPhuTung.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                                    cmdUDPhuTung.ExecuteNonQuery();
                                    tranUDPhuTung.Commit();
                                    cmdUDPhuTung.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(" Lỗi " + ex.Message);
                                    tranUDPhuTung.Rollback();
                                    //sqlTransaction.Rollback();
                                    cmdUDPhuTung.Connection.Close();
                                    cmdUDPhuTung.Connection.Dispose();
                                }


                            }
                            phuTungTemp.Rows.Clear();

                            phuTungTemp = new DataTable();
                            SqlCommand cmdKhoXuat = new SqlCommand();
                            cmdKhoXuat.Connection = Class.datatabase.getConnection();
                            cmdKhoXuat.Connection.Open();
                            SqlTransaction trancmdKhoXuat = cmdKhoXuat.Connection.BeginTransaction();
                            cmdKhoXuat.Transaction = trancmdKhoXuat;
                            try
                            {
                                cmdKhoXuat.CommandText = @"Select MaPT, SoLuong from dbo.KhoXuat where MaPT = @MaPT and IdCongTy = @IdCongTy and IdKho = @IdKho and IdBaoDuongTam = @idbaoduongtam";
                                cmdKhoXuat.Parameters.Clear();
                                cmdKhoXuat.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                cmdKhoXuat.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmdKhoXuat.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                                cmdKhoXuat.Parameters.AddWithValue("@idbaoduongtam", IdBaoDuongXe);
                                adp = new SqlDataAdapter(cmdKhoXuat);
                                adp.Fill(phuTungTemp);
                                trancmdKhoXuat.Commit();
                                cmdKhoXuat.Connection.Close();
                                cmdKhoXuat.Connection.Dispose();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(" Lỗi " + ex.Message);
                                trancmdKhoXuat.Rollback();
                                //sqlTransaction.Rollback();
                                cmdKhoXuat.Connection.Close();
                                cmdKhoXuat.Connection.Dispose();

                            }

                            if (phuTungTemp.Rows.Count > 0)
                            {
                                int soluongtruoc = 0;
                                int soLuongtru = 0;
                                int soLuongSau = 0;
                                soluongtruoc = Convert.ToInt32(phuTungTemp.Rows[0]["SoLuong"]);
                                soLuongtru = int.Parse(dgvPhuTungOrder.Rows[i].Cells[2].Value.ToString().Trim());
                                soLuongSau = soluongtruoc + soLuongtru;

                                SqlCommand cmdUDKhoXuat = new SqlCommand();
                                cmdUDKhoXuat.Connection = Class.datatabase.getConnection();
                                cmdUDKhoXuat.Connection.Open();
                                SqlTransaction tranUDKhoXuat = cmdUDKhoXuat.Connection.BeginTransaction();
                                cmdUDKhoXuat.Transaction = tranUDKhoXuat;
                                try
                                {
                                    cmdUDKhoXuat.CommandText = @"Update dbo.KhoXuat SET SoLuong = @SoLuong where MaPT = @MaPT and IdCongTy = @IdCongTy and IdKho = @IdKho and IdBaoDuongTam = @idbaoduongtam";
                                    cmdUDKhoXuat.Parameters.Clear();
                                    cmdUDKhoXuat.Parameters.AddWithValue("@SoLuong", soLuongSau);
                                    cmdUDKhoXuat.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                    cmdUDKhoXuat.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    cmdUDKhoXuat.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                                    cmdUDKhoXuat.Parameters.AddWithValue("@idbaoduongtam", IdBaoDuongXe);
                                    cmdUDKhoXuat.ExecuteNonQuery();
                                    tranUDKhoXuat.Commit();
                                    cmdUDKhoXuat.Connection.Close();
                                    cmdUDKhoXuat.Connection.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(" Lỗi " + ex.Message);
                                    tranUDKhoXuat.Rollback();
                                    //sqlTransaction.Rollback();
                                    cmdUDKhoXuat.Connection.Close();
                                    cmdUDKhoXuat.Connection.Dispose();
                                }
                            }
                            else
                            {
                                SqlCommand cmdUDKhoXuat = new SqlCommand();
                                cmdUDKhoXuat.Connection = Class.datatabase.getConnection();
                                cmdUDKhoXuat.Connection.Open();
                                SqlTransaction tranUDKhoXuat = cmdUDKhoXuat.Connection.BeginTransaction();
                                cmdUDKhoXuat.Transaction = tranUDKhoXuat;
                                try
                                {
                                    cmdUDKhoXuat.CommandText = @"insert into dbo.KhoXuat(MaPT, TenPT, SoLuong, NgayXuat, IdKho, IdCongTy, IdBaoDuongTam,IdPT) values 
                                        (@MaPT, @TenPT, @SoLuong, @NgayXuat, @IdKho, @IdCongTy, @IdBaoDuongTam, @idpt)";
                                    cmdUDKhoXuat.Parameters.Clear();
                                    cmdUDKhoXuat.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                    cmdUDKhoXuat.Parameters.AddWithValue("@TenPT", dgvPhuTungOrder.Rows[i].Cells[1].Value.ToString().Trim());
                                    cmdUDKhoXuat.Parameters.AddWithValue("@SoLuong", int.Parse(dgvPhuTungOrder.Rows[i].Cells[2].Value.ToString().Trim()));
                                    cmdUDKhoXuat.Parameters.AddWithValue("@NgayXuat", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
                                    cmdUDKhoXuat.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    cmdUDKhoXuat.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                                    cmdUDKhoXuat.Parameters.AddWithValue("@IdBaoDuongTam", IdBaoDuongXe);
                                    cmdUDKhoXuat.Parameters.AddWithValue("@idpt", int.Parse(dgvPhuTungOrder.Rows[i].Cells[7].Value.ToString().Trim()));
                                    cmdUDKhoXuat.ExecuteNonQuery();
                                    tranUDKhoXuat.Commit();
                                    cmdUDKhoXuat.Connection.Close();
                                    cmdUDKhoXuat.Connection.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(" Lỗi " + ex.Message);
                                    tranUDKhoXuat.Rollback();
                                    //sqlTransaction.Rollback();
                                    cmdUDKhoXuat.Connection.Close();
                                    cmdUDKhoXuat.Connection.Dispose();
                                }
                            }
                            SqlCommand cmdPhuTungOrder052020 = new SqlCommand();
                            cmdPhuTungOrder052020.Connection = Class.datatabase.getConnection();
                            cmdPhuTungOrder052020.Connection.Open();
                            SqlTransaction tranPhuTungOrder052020 = cmdPhuTungOrder052020.Connection.BeginTransaction();
                            cmdPhuTungOrder052020.Transaction = tranPhuTungOrder052020;
                            try
                            {
                                cmdPhuTungOrder052020.CommandText = @"Select MaPT, SoLuong, SoLuongCurrent from dbo.PhuTungOrder052020 where MaPT = @MaPT and IdCongTy = @IdCongTy and IdKho = @IdKho and IdBaoDuong = @idbaoduong";
                                cmdPhuTungOrder052020.Parameters.Clear();
                                cmdPhuTungOrder052020.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                cmdPhuTungOrder052020.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmdPhuTungOrder052020.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                                cmdPhuTungOrder052020.Parameters.AddWithValue("@idbaoduong", IdBaoDuongXe);
                                phuTungTemp.Rows.Clear();
                                SqlDataAdapter dapter = new SqlDataAdapter(cmdPhuTungOrder052020);
                                phuTungTemp = new DataTable();
                                dapter.Fill(phuTungTemp);
                                tranPhuTungOrder052020.Commit();
                                cmdPhuTungOrder052020.Connection.Close();
                                cmdPhuTungOrder052020.Connection.Dispose();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(" Lỗi " + ex.Message);
                                tranPhuTungOrder052020.Rollback();
                                //sqlTransaction.Rollback();
                                cmdPhuTungOrder052020.Connection.Close();
                                cmdPhuTungOrder052020.Connection.Dispose();
                            }
                            if (phuTungTemp.Rows.Count > 0)
                            {
                                int soluongtruoc = 0;
                                int soLuongCong = 0;
                                int soLuongSau = 0;
                                int soLuongGiaoThucTe = 0;
                                soluongtruoc = Convert.ToInt32(phuTungTemp.Rows[0]["SoLuong"]);
                                soLuongGiaoThucTe = Convert.ToInt32(phuTungTemp.Rows[0]["SoLuongCurrent"]);
                                soLuongCong = int.Parse(dgvPhuTungOrder.Rows[i].Cells[2].Value.ToString().Trim());
                                soLuongSau = soluongtruoc + soLuongCong;

                                SqlCommand cmdUDPhuTungOrder052020 = new SqlCommand();
                                cmdUDPhuTungOrder052020.Connection = Class.datatabase.getConnection();
                                cmdUDPhuTungOrder052020.Connection.Open();
                                SqlTransaction tranUDPhuTungOrder052020 = cmdUDPhuTungOrder052020.Connection.BeginTransaction();
                                cmdUDPhuTungOrder052020.Transaction = tranUDPhuTungOrder052020;
                                try
                                {
                                    cmdUDPhuTungOrder052020.CommandText = @"Update dbo.PhuTungOrder052020 SET SoLuong = @SoLuong, TranThaiTiepNhan = 2, SoLuongCurrent = @soluongcurrent where MaPT = @MaPT and IdCongTy = @IdCongTy and IdKho = @IdKho and IdBaoDuong = @idbaoduong";
                                    cmdUDPhuTungOrder052020.Parameters.Clear();
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@SoLuong", soLuongSau);
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@soluongcurrent", soLuongGiaoThucTe + soLuongCong);
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@idbaoduong", IdBaoDuongXe);
                                    cmdUDPhuTungOrder052020.ExecuteNonQuery();
                                    tranUDPhuTungOrder052020.Commit();
                                    cmdUDPhuTungOrder052020.Connection.Close();
                                    cmdUDPhuTungOrder052020.Connection.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(" Lỗi " + ex.Message);
                                    tranUDPhuTungOrder052020.Rollback();
                                    //sqlTransaction.Rollback();
                                    cmdUDPhuTungOrder052020.Connection.Close();
                                    cmdUDPhuTungOrder052020.Connection.Dispose();

                                }
                            }
                            else
                            {
                                SqlCommand cmdUDPhuTungOrder052020 = new SqlCommand();
                                cmdUDPhuTungOrder052020.Connection = Class.datatabase.getConnection();
                                cmdUDPhuTungOrder052020.Connection.Open();
                                SqlTransaction tranUDPhuTungOrder052020 = cmdUDPhuTungOrder052020.Connection.BeginTransaction();
                                cmdUDPhuTungOrder052020.Transaction = tranUDPhuTungOrder052020;
                                try
                                {
                                    cmdUDPhuTungOrder052020.CommandText = @"insert into dbo.PhuTungOrder052020(IdBaoDuong, MaPT, TenPT, SoLuong, IdKho, IdCongTy, IdCuaHang, TrangThaiGoi, TranThaiTiepNhan, Ngay, IdPhuTung, SoLuongCurrent) values 
                                        (@idbaoduong, @MaPT, @TenPT, @SoLuong, @idkho, @idcongty, @idcuahang, 0, 1, @ngay, @idphu,@soluongcurrent)";
                                    cmdUDPhuTungOrder052020.Parameters.Clear();
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@idbaoduong", IdBaoDuongXe);
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@TenPT", dgvPhuTungOrder.Rows[i].Cells[1].Value.ToString().Trim());
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@SoLuong", int.Parse(dgvPhuTungOrder.Rows[i].Cells[2].Value.ToString().Trim()));
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@idkho", cboKho.SelectedValue);
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@idcuahang", Class.CompanyInfo.IdsCuaHang);
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@ngay", DateTime.Now.ToString("MM/dd/yyyy"));
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@idphu", int.Parse(dgvPhuTungOrder.Rows[i].Cells[7].Value.ToString().Trim()));
                                    cmdUDPhuTungOrder052020.Parameters.AddWithValue("@soluongcurrent", int.Parse(dgvPhuTungOrder.Rows[i].Cells[2].Value.ToString().Trim()));
                                    cmdUDPhuTungOrder052020.ExecuteNonQuery();
                                    tranUDPhuTungOrder052020.Commit();
                                    cmdUDPhuTungOrder052020.Connection.Close();
                                    cmdUDPhuTungOrder052020.Connection.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(" Lỗi " + ex.Message);
                                    tranUDPhuTungOrder052020.Rollback();
                                    //sqlTransaction.Rollback();
                                    cmdUDPhuTungOrder052020.Connection.Close();
                                    cmdUDPhuTungOrder052020.Connection.Dispose();
                                }
                            }

                            if (CallFromUcBaoDuong != null)
                            {
                                phuTungTemp = new DataTable();
                                SqlCommand cmdSLLichSuBaoDuongChiTietTam2 = new SqlCommand();
                                cmdSLLichSuBaoDuongChiTietTam2.Connection = Class.datatabase.getConnection();
                                cmdSLLichSuBaoDuongChiTietTam2.Connection.Open();
                                SqlTransaction tranSLLichSuBaoDuongChiTietTam2 = cmdSLLichSuBaoDuongChiTietTam2.Connection.BeginTransaction();
                                cmdSLLichSuBaoDuongChiTietTam2.Transaction = tranSLLichSuBaoDuongChiTietTam2;
                                try
                                {
                                    cmdSLLichSuBaoDuongChiTietTam2.CommandText = @"SELECT MaPT, SoLuong  FROM dbo.LichSuBaoDuongChiTietTam2 WHERE MaPT = @mapt AND IdBaoDuong = @IdBaoDuong and IdKho = @idkho";
                                    cmdSLLichSuBaoDuongChiTietTam2.Parameters.Clear();
                                    cmdSLLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@mapt", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                    cmdSLLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                                    cmdSLLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@idkho", cboKho.SelectedValue);
                                    phuTungTemp.Rows.Clear();
                                    SqlDataAdapter adapter = new SqlDataAdapter(cmdSLLichSuBaoDuongChiTietTam2);
                                    adapter.Fill(phuTungTemp);
                                    tranSLLichSuBaoDuongChiTietTam2.Commit();
                                    cmdSLLichSuBaoDuongChiTietTam2.Connection.Close();

                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show("Lỗi"+ex.Message);
                                    tranSLLichSuBaoDuongChiTietTam2.Rollback();
                                    cmdSLLichSuBaoDuongChiTietTam2.Connection.Close();
                                    cmdSLLichSuBaoDuongChiTietTam2.Connection.Dispose();

                                }
                                if (phuTungTemp.Rows.Count > 0)
                                {
                                    int soLuongHienTai = int.Parse(phuTungTemp.Rows[0]["SoLuong"].ToString().Trim());
                                    int soLuongCong = int.Parse(dgvPhuTungOrder.Rows[0].Cells[2].Value.ToString().Trim());
                                    int soLuongSau = soLuongHienTai + soLuongCong;

                                    SqlCommand cmdLichSuBaoDuongChiTietTam2 = new SqlCommand();
                                    cmdLichSuBaoDuongChiTietTam2.Connection = Class.datatabase.getConnection();
                                    cmdLichSuBaoDuongChiTietTam2.Connection.Open();
                                    SqlTransaction tranLichSuBaoDuongChiTietTam2 = cmdLichSuBaoDuongChiTietTam2.Connection.BeginTransaction();
                                    cmdLichSuBaoDuongChiTietTam2.Transaction = tranLichSuBaoDuongChiTietTam2;
                                    try
                                    {
                                        cmdLichSuBaoDuongChiTietTam2.CommandText = @"update dbo.LichSuBaoDuongChiTietTam2 set SoLuong = @soluong, GiaTien = @giatien WHERE MaPT = @mapt AND IdBaoDuong = @IdBaoDuong and IdKho = @idkho";
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.Clear();
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@soluong", soLuongSau);
                                        //cmd.Parameters.AddWithValue("@giatien", soLuongSau * int.Parse(dgvPhuTungOrder.Rows[i].Cells[3].Value.ToString().Trim()));
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@giatien", soLuongSau * Convert.ToSingle(dgvPhuTungOrder.Rows[i].Cells[3].Value.ToString().Trim()));
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@mapt", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@idkho", cboKho.SelectedValue);
                                        cmdLichSuBaoDuongChiTietTam2.ExecuteNonQuery();
                                        tranLichSuBaoDuongChiTietTam2.Commit();
                                        cmdLichSuBaoDuongChiTietTam2.Connection.Close();
                                        cmdLichSuBaoDuongChiTietTam2.Connection.Dispose();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(" Lỗi " + ex.Message);
                                        tranLichSuBaoDuongChiTietTam2.Rollback();
                                        //sqlTransaction.Rollback();
                                        cmdLichSuBaoDuongChiTietTam2.Connection.Close();
                                        cmdLichSuBaoDuongChiTietTam2.Connection.Dispose();
                                    }
                                }
                                else
                                {
                                    SqlCommand cmdLichSuBaoDuongChiTietTam2 = new SqlCommand();
                                    cmdLichSuBaoDuongChiTietTam2.Connection = Class.datatabase.getConnection();
                                    cmdLichSuBaoDuongChiTietTam2.Connection.Open();
                                    SqlTransaction tranLichSuBaoDuongChiTietTam2 = cmdLichSuBaoDuongChiTietTam2.Connection.BeginTransaction();
                                    cmdLichSuBaoDuongChiTietTam2.Transaction = tranLichSuBaoDuongChiTietTam2;
                                    try
                                    {
                                        cmdLichSuBaoDuongChiTietTam2.CommandText = @"INSERT INTO dbo.LichSuBaoDuongChiTietTam2
                                        (IdBaoDuong, MaPT, TenPhuTung, Soluong, Gia, GiaTien, IdKho, IdTho, IdPhuTung,TienTraTho)
                                        VALUES (@IdBaoDuong,@MaPT,@TenPhuTung,@Soluong,@Gia,@GiaTien,@IdKho,@IdTho,@IdPhuTung,@TienTraTho)";
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.Clear();
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@TenPhuTung", dgvPhuTungOrder.Rows[i].Cells[1].Value.ToString().Trim());
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@Soluong", int.Parse(dgvPhuTungOrder.Rows[i].Cells[2].Value.ToString().Trim()));
                                        //cmd.Parameters.AddWithValue("@Gia", int.Parse(dgvPhuTungOrder.Rows[i].Cells[3].Value.ToString().Trim()));
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@Gia", Convert.ToSingle(dgvPhuTungOrder.Rows[i].Cells[3].Value.ToString().Trim()));
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@IdPhuTung", dgvPhuTungOrder.Rows[i].Cells[7].Value.ToString().Trim());
                                        //cmd.Parameters.AddWithValue("@GiaTien", int.Parse(dgvPhuTungOrder.Rows[i].Cells[2].Value.ToString().Trim()) * int.Parse(dgvPhuTungOrder.Rows[i].Cells[3].Value.ToString().Trim()));
                                        cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@GiaTien", int.Parse(dgvPhuTungOrder.Rows[i].Cells[2].Value.ToString().Trim()) * Convert.ToSingle(dgvPhuTungOrder.Rows[i].Cells[3].Value.ToString().Trim()));
                                        if (String.IsNullOrEmpty(dgvPhuTungOrder.Rows[i].Cells[6].Value.ToString()))
                                            cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@idTho", "");
                                        else
                                            cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@idTho", int.Parse(cboThoSuaChua.SelectedValue.ToString()));
                                        if (int.Parse(dgvPhuTungOrder.Rows[i].Cells[7].Value.ToString()) > 0)
                                            cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@TienTraTho", Convert.ToDecimal(dgvPhuTungOrder.Rows[i].Cells[7].Value.ToString()));
                                        else
                                            cmdLichSuBaoDuongChiTietTam2.Parameters.AddWithValue("@TienTraTho", 0);
                                        cmdLichSuBaoDuongChiTietTam2.ExecuteNonQuery();
                                        tranLichSuBaoDuongChiTietTam2.Commit();
                                        cmdLichSuBaoDuongChiTietTam2.Connection.Close();
                                        cmdLichSuBaoDuongChiTietTam2.Connection.Dispose();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(" Lỗi " + ex.Message);
                                        tranLichSuBaoDuongChiTietTam2.Rollback();
                                        //sqlTransaction.Rollback();
                                        cmdLichSuBaoDuongChiTietTam2.Connection.Close();
                                        cmdLichSuBaoDuongChiTietTam2.Connection.Dispose();
                                    }
                                }
                            }
                        }
                    }
                    sqlTransaction.Commit();
                    CallFromUcBaoDuong();
                    MessageBox.Show("Xác nhận thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvPhuTungOrder.Rows.Clear();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    sqlTransaction.Rollback();
                }
                finally
                {
                    btnXacNhanXuatKho.Enabled = true;
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                }

            }
        }
        #endregion Xac nhận nhận phụ tùng từ kho
        #region Thêm từng mã barcode vào bảng chứa barcode
        private void TxtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ArrayList row = new ArrayList();
                for (int i = 0; i < dgvPhuTungTiepNhan.Rows.Count - 1; i++)
                {
                    if (dgvPhuTungTiepNhan.Rows[i].Cells[0].Value.ToString().Trim().Equals(txtBarcode.Text.Trim()))
                    {
                        int soLuongThucTe = int.Parse(dgvPhuTungTiepNhan.Rows[i].Cells[1].Value.ToString().Trim());
                        dgvPhuTungTiepNhan.Rows[i].Cells[1].Value = (soLuongThucTe + 1).ToString();
                        txtBarcode.Text = "";
                        return;
                    }
                }
                row = new ArrayList();
                row.Add(txtBarcode.Text);
                row.Add("1");
                dgvPhuTungTiepNhan.Rows.Add(row.ToArray());
                txtBarcode.Text = "";
            }
        }
        #endregion Thêm từng mã barcode vào bảng chứa barcode
        #region class danh sách xe bảo dưỡng
        class DanhSachXeOrder
        {
            public string TenXe { set; get; }
            public int IdBaoDuong { set; get; }
            public string BienSo { set; get; }
            public string SoKhung { set; get; }
            public string SoMay { set; get; }
        }
        #endregion class danh sách xe bảo dưỡng
        #region class phụ tùng theo xe bảo dưỡng
        class PhuTungOrderTheoXe
        {
            public int IdBaoDuong { set; get; }
            public string MaPhuTung { set; get; }
            public string TenPhuTung { set; get; }
            public int SoLuong { set; get; }
            public int IdCongTy { set; get; }
            public int IdKho { set; get; }
            public int IdCuaHang { set; get; }
            public int TrangThaiGoi { set; get; }
            public int TrangThaiTiepNhan { set; get; }
            public string NgayThang { set; get; }
            public int Gia { set; get; }
            public string IdPhuTung { set; get; }
        }
        #endregion class phụ tùng theo xe bảo dưỡng

        private decimal tien2;
        private decimal tien;
        private void LoadThoDichVu()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"Select *from dbo.LichSuBaoDuongXeTam where IdCongTy = @IdCongTy and IdBaoDuong = @idbaoduong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@idbaoduong", Int64.Parse(IdBaoDuong.Trim()));

            DataTable abc = Class.datatabase.getData(cmd);
            if (abc.Rows.Count > 0)
            {

            }
            else
            {
                MessageBox.Show("Không có Mã bảo dưỡng này", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cmd.CommandText = @"SELECT IdTho, ISNULL(MaTho, '') + ' -- ' + ISNULL(tenTho, '') AS TenTho
                                FROM dbo.ThoDichVu WHERE IdCongTy = @IdCongTy and TinhTrangLamViec is null and IdTho = @idTho";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@idTho", int.Parse(abc.Rows[0]["KYTHUATVIEN"].ToString().Trim()));
            cboThoSuaChua.DisplayMember = "TenTho";
            cboThoSuaChua.ValueMember = "IdTho";
            cboThoSuaChua.DataSource = Class.datatabase.getData(cmd);
            cboThoSuaChua.Enabled = false;
        }

        private void BtnXacNhanNhapLaiKho_Click(object sender, EventArgs e)
        {
            if (dgvPhuTungOrder.Rows.Count < 1)
            {
                MessageBox.Show("Chưa kiểm tra phụ tùng xuất kho, nhập kho", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnXacNhanNhapLaiKho.Enabled = false;
            using (SqlConnection myCon = new SqlConnection(Class.datatabase.connect))
            {

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myCon;
                if (myCon.State == ConnectionState.Closed)
                {
                    myCon.Open();
                }
                cmd.CommandTimeout = 0;
                SqlTransaction transaction;
                transaction = myCon.BeginTransaction();
                cmd.Transaction = transaction;
                try
                {
                    for (int i = 0; i < dgvPhuTungOrder.Rows.Count - 1; i++)
                    {
                        cmd.CommandText = @"select * from dbo.LichSuBaoDuongChiTietTam2 where MaPT = @MaPT and IdKho = @Idkho and IdBaoDuong = @idbaoduong";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                        cmd.Parameters.AddWithValue("@idbaoduong", Int32.Parse(IdBaoDuong.ToString().Trim()));
                        SqlDataAdapter adap = new SqlDataAdapter(cmd);
                        DataTable phuTung = new DataTable();
                        adap.Fill(phuTung);
                        if (phuTung.Rows.Count > 0)
                        {
                            int soLuongGoi = int.Parse(phuTung.Rows[0]["SoLuong"].ToString().Trim());
                            int soLuongTru = int.Parse(dgvPhuTungOrder.Rows[i].Cells[2].Value.ToString().Trim());
                            if (soLuongTru < soLuongGoi)
                            {
                                cmd.CommandText = @"update dbo.LichSuBaoDuongChiTietTam2 Set SoLuong = @soluong, GiaTien = @giatien where MaPT = @MaPT and IdKho = @Idkho and IdBaoDuong = @idbaoduong";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@soluong", soLuongGoi - soLuongTru);
                                //cmd.Parameters.AddWithValue("@giatien", (soLuongGoi - soLuongTru)*float.Parse(phuTung.Rows[0]["Gia"].ToString().Trim()));
                                cmd.Parameters.AddWithValue("@giatien", (soLuongGoi - soLuongTru) * Convert.ToSingle(phuTung.Rows[0]["Gia"].ToString().Trim()));
                                cmd.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                cmd.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                                cmd.Parameters.AddWithValue("@idbaoduong", Int32.Parse(IdBaoDuong.ToString().Trim()));
                                cmd.ExecuteNonQuery();

                                cmd.CommandText = @"select * from dbo.PhuTungOrder052020 where MaPT = @MaPT and IdKho = @Idkho and IdCongTy = @IdCongTy and IdBaoDuong = @idbaoduong";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                cmd.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd.Parameters.AddWithValue("@idbaoduong", Int32.Parse(IdBaoDuong.ToString().Trim()));
                                SqlDataAdapter ad = new SqlDataAdapter(cmd);
                                phuTung = new DataTable();
                                ad.Fill(phuTung);
                                if (phuTung.Rows.Count > 0)
                                {
                                    int soLuongCurrent = int.Parse(phuTung.Rows[0]["SoLuongCurrent"].ToString().Trim());
                                    cmd.CommandText = @"update dbo.PhuTungOrder052020 Set SoLuongCurrent = @soluong, TranThaiTiepNhan = 3 where MaPT = @MaPT and IdKho = @Idkho and IdCongTy = @IdCongTy and IdBaoDuong = @idbaoduong";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@soluong", soLuongCurrent - soLuongTru);
                                    cmd.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                                    cmd.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    cmd.Parameters.AddWithValue("@idbaoduong", Int32.Parse(IdBaoDuong.ToString().Trim()));
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            else if (soLuongTru == soLuongGoi)
                            {
                                MessageBox.Show("Không hỗ trợ chức năng này\nXin xóa bỏ phụ tùng này và thực hiện lại quy trình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                            else
                            {
                                MessageBox.Show("Số liệu xuất kho và số liệu trả lại không khớp nhau\nXin thực hiện lại quy trình này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Phụ tùng chưa xuất kho, không thể trả lại xin kiểm tra lại\nXin thực hiện lại quy trình", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        cmd.CommandText = @"select * from dbo.PhuTung where MaPT = @MaPT and IdKho = @Idkho and IdCongTy = @IdCongTy";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                        cmd.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        phuTung = new DataTable();
                        adapter.Fill(phuTung);
                        if (phuTung.Rows.Count > 0)
                        {
                            int soLuongTon = int.Parse(phuTung.Rows[0]["SoLuong"].ToString());
                            int soCanCong = soLuongTon + int.Parse(dgvPhuTungOrder.Rows[i].Cells[2].Value.ToString().Trim());
                            cmd.CommandText = @"update dbo.PhuTung Set SoLuong = @soluong where MaPT = @MaPT and IdKho = @Idkho and IdCongTy = @IdCongTy";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@soluong", soCanCong);
                            cmd.Parameters.AddWithValue("@MaPT", dgvPhuTungOrder.Rows[i].Cells[0].Value.ToString().Trim());
                            cmd.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    CallFromUcBaoDuong();
                    MessageBox.Show("Nhập lại thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvPhuTungOrder.Rows.Clear();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    cmd.Connection.Close();
                    cmd.Connection.Dispose();
                    MessageBox.Show(ex.Message);
                }
                finally { btnXacNhanNhapLaiKho.Enabled = true; }
            }
        }

        private void CbDanhSachXe_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CboKho_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void DgvPhuTungOrder_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            dgvPhuTungOrder.CurrentRow.Cells[4].Value = (Convert.ToSingle(dgvPhuTungOrder.CurrentRow.Cells[2].Value.ToString().Trim()) * Convert.ToSingle(dgvPhuTungOrder.CurrentRow.Cells[3].Value.ToString().Trim())).ToString();
            try
            {
                if (String.IsNullOrEmpty(dgvPhuTungOrder.CurrentRow.Cells[4].Value.ToString()))
                {
                    tien = 0;
                }
                else
                {
                    tien = Convert.ToDecimal(dgvPhuTungOrder.CurrentRow.Cells[4].Value.ToString());
                }
                if (String.IsNullOrEmpty(dgvPhuTungOrder.CurrentRow.Cells[3].Value.ToString()))
                {
                    tien2 = 0;
                }
                else
                {
                    tien2 = Convert.ToDecimal(dgvPhuTungOrder.CurrentRow.Cells[3].Value.ToString());
                }
            }
            catch { MessageBox.Show("Đơn giá phải là kiểu số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            dgvPhuTungOrder.CurrentRow.Cells[4].Value = tien.ToString("0,0");
            dgvPhuTungOrder.CurrentRow.Cells[3].Value = tien2.ToString("0,0");
        }

        private void LoadPhuTung(long idKho)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"SELECT IdPT, ISNULL(MaPT, '') + '-- ' + ISNULL(TenPT, '') AS TenPhuTung  
                                FROM dbo.PhuTung WHERE IdCongTy = @IdCongTy AND IdKho = @IdKho";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@IdKho", idKho);
            DataTable tablePhuTung = Class.datatabase.getData(cmd);
            if (tablePhuTung.Rows.Count > 0)
            {
                cboPhuTung.DisplayMember = "TenPhuTung";
                cboPhuTung.ValueMember = "IdPT";
                cboPhuTung.DataSource = tablePhuTung;
                cboPhuTung.SelectedIndex = -1;
            }
        }

        private void CboPhuTung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = @"select MaPT from dbo.PhuTung where IdCongTy = @idcongty and IdKho = @idkho and IdPT = @idpt";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@idkho", cboKho.SelectedValue);
                cmd.Parameters.AddWithValue("@idpt", cboPhuTung.SelectedValue);
                DataTable dttb = Class.datatabase.getData(cmd);
                if (dttb.Rows.Count > 0)
                {
                    ArrayList row = new ArrayList();
                    for (int i = 0; i < dgvPhuTungTiepNhan.Rows.Count - 1; i++)
                    {
                        if (dgvPhuTungTiepNhan.Rows[i].Cells[0].Value.ToString().Trim().Equals(dttb.Rows[0]["MaPT"].ToString().Trim()))
                        {
                            int soLuongThucTe = int.Parse(dgvPhuTungTiepNhan.Rows[i].Cells[1].Value.ToString().Trim());
                            dgvPhuTungTiepNhan.Rows[i].Cells[1].Value = (soLuongThucTe + 1).ToString();
                            cboPhuTung.SelectedIndex = -1;
                            return;
                        }
                    }
                    row = new ArrayList();
                    row.Add(dttb.Rows[0]["MaPT"].ToString().Trim());
                    row.Add("1");
                    dgvPhuTungTiepNhan.Rows.Add(row.ToArray());
                    cboPhuTung.SelectedIndex = -1;
                }
                else
                {
                    MessageBox.Show("Không tìm thấy phụ tùng\nThao tác lại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }
        bool permissChangeMoney = false;
        private void DgvPhuTungOrder_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DgvPhuTungOrder_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
        }

        private void DgvPhuTungOrder_CellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
        {

        }

        private void DgvPhuTungOrder_Validating(object sender, CancelEventArgs e)
        {
        }

        private void DgvPhuTungOrder_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
        }

        private void btnConvertPhuTung_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(cboThoSuaChua.Text))
            {
                MessageBox.Show("Bạn chưa chọn thợ, kiểm tra lại", "Cảnh bảo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            cboThoSuaChua.Enabled = false;
            cbDanhSachXe.Enabled = false;
            cboKho.Enabled = false;
            dgvPhuTungOrder.Rows.Clear();
            btnConvertPhuTung.Enabled = false;

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.datatabase.getConnection();
            cmd.Connection.Open();
            cmd.CommandTimeout = 0;
            SqlTransaction sqlTransaction = cmd.Connection.BeginTransaction();
            cmd.Transaction = sqlTransaction;
            try
            {
                for (int i = 0; i < dgvPhuTungTiepNhan.Rows.Count - 1; i++)
                {
                    cmd.CommandText = @"Select MaPT, TenPT, DonGia, IdPT, SoLuong from dbo.PhuTung where IdCongTy = @idcongty and MaPT = @mapt and IdKho = @idkho";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@mapt", dgvPhuTungTiepNhan.Rows[i].Cells[0].Value.ToString().Trim());
                    cmd.Parameters.AddWithValue("@idkho", cboKho.SelectedValue);
                    SqlDataAdapter ada = new SqlDataAdapter(cmd);
                    DataTable phutung = new DataTable();
                    ada.Fill(phutung);
                    if (phutung.Rows.Count > 0)
                    {
                        int rowCurrent = dgvPhuTungOrder.Rows.Count;
                        dgvPhuTungOrder.Rows.Add();
                        dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[0].Value = phutung.Rows[0]["MaPT"].ToString();
                        dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[1].Value = phutung.Rows[0]["TenPT"].ToString();
                        dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[2].Value = dgvPhuTungTiepNhan.Rows[i].Cells[1].Value.ToString();
                        dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[3].Value = phutung.Rows[0]["DonGia"].ToString();
                        dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[4].Value = (int.Parse(dgvPhuTungTiepNhan.Rows[i].Cells[1].Value.ToString()) * int.Parse(phutung.Rows[0]["DonGia"].ToString())).ToString();
                        dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[5].Value = cboKho.Text;
                        dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[6].Value = "0";
                        dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[7].Value = phutung.Rows[0]["IdPT"].ToString(); ;
                        dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[8].Value = cbDanhSachXe.Text;
                        dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[9].Value = phutung.Rows[0]["SoLuong"].ToString();
                        if (int.Parse(dgvPhuTungTiepNhan.Rows[i].Cells[1].Value.ToString()) <= int.Parse(phutung.Rows[0]["SoLuong"].ToString()))
                        {
                            dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[10].Value = "OK";
                        }
                        else
                        {
                            dgvPhuTungOrder.Rows[rowCurrent - 1].Cells[10].Value = "NOT OK";
                        }
                    }

                }
                sqlTransaction.Commit();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
                dgvPhuTungTiepNhan.Rows.Clear();
            }
            catch
            {
                sqlTransaction.Rollback();
                cmd.Connection.Close();
                cmd.Connection.Dispose();
            }
            finally { btnConvertPhuTung.Enabled = true; }

            for (int i = 0; i < dgvPhuTungOrder.RowCount - 1; i++)
            {
                try
                {
                    if (String.IsNullOrEmpty(dgvPhuTungOrder.Rows[i].Cells[3].Value.ToString()))
                    {
                        tien = 0;
                    }
                    else
                    {
                        tien = Convert.ToDecimal(dgvPhuTungOrder.Rows[i].Cells[3].Value.ToString());
                    }

                    if (String.IsNullOrEmpty(dgvPhuTungOrder.Rows[i].Cells[4].Value.ToString()))
                    {
                        tien2 = 0;
                    }
                    else
                    {
                        tien2 = Convert.ToDecimal(dgvPhuTungOrder.Rows[i].Cells[4].Value.ToString());
                    }
                }
                catch { MessageBox.Show("Đơn giá phải là kiểu số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                dgvPhuTungOrder.Rows[i].Cells[3].Value = tien.ToString("0,0");
                dgvPhuTungOrder.Rows[i].Cells[4].Value = tien2.ToString("0,0");
                //dgvPhuTungOrder.CurrentRow.Cells[3].Value.SelectionStart = txbSuaLaiGiaTien.Text.Length;
            }
            btnRevertPhuTung.Enabled = true;
            dgvPhuTungOrder.ClearSelection();

        }

        private void btnRevertPhuTung_Click(object sender, EventArgs e)
        {
            int rowIndex = dgvPhuTungOrder.CurrentRow.Index;
            //MessageBox.Show(dgvPhuTungOrder.Rows[rowIndex].Cells[0].Value.ToString(), "Thong Bao", MessageBoxButtons.OK, MessageBoxIcon.Information);
            if (rowIndex >= dgvPhuTungOrder.Rows.Count - 1)
            {
                return;
            }
            else
            {
                dgvPhuTungTiepNhan.Rows.Add();
                int rowMax = dgvPhuTungTiepNhan.Rows.Count;
                dgvPhuTungTiepNhan.Rows[rowMax - 2].Cells[0].Value = dgvPhuTungOrder.Rows[rowIndex].Cells[0].Value.ToString();

                dgvPhuTungTiepNhan.Rows[rowMax - 2].Cells[1].Value = dgvPhuTungOrder.Rows[rowIndex].Cells[2].Value.ToString();
                dgvPhuTungOrder.Rows.RemoveAt(rowIndex);
            }
        }
    }
}
