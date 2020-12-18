using AutoCareV2._0.Class;
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Z.Dapper.Plus;
//using System.Web.UI;

namespace AutoCareV2._0
{
    public partial class frmQuanLyPhuTung : Form
    {
        private static DataTable bangPhuTung = new DataTable();
        private DataTable dtCongTy = new DataTable();
        private string _MaPT;
        private int idPhuTung = 0;
        private bool kq = false;
        private string strIdCoSo = "";
        private string strDuongDan = "";
        private int soluong;
        private decimal tien;
        private int sl = 0;
        private DataTableCollection tables;
        List<PhuTung042020> listPhuTungDuaLenDB;
        List<PhuTungBarcode042020> listPhuTungBarcodeDuaLenDB;

        public frmQuanLyPhuTung()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Kiểm tra mã phụ tùng trong kho
        /// </summary>
        /// <param name="ma">Mã phụ tùng</param>
        /// <returns>True: Nếu mã phụ tùng đã tồn tại; False: Nếu mã phụ tùng không tồn tại trong kho</returns>
        public bool CheckPhuTung(string ma)
        {
            bool kq = false;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.datatabase.getConnection();
            cmd.CommandType = CommandType.Text;
            SqlDataReader reader;

            //cmd.CommandText = "select IdPT from PhuTung where MaPT='" + ma + "' and IdCongty=" + Class.CompanyInfo.idcongty;
            cmd.CommandText = "select IdPT from PhuTung where MaPT=@MaPT and IdCongty=@IdCongTy";
            cmd.Parameters.AddWithValue("@MaPT", ma);
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            cmd.Connection.Open();
            reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    kq = true;
                }
            }
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            return kq;
        }

        /// <summary>
        /// Lấy thông tin công ty theo mã công ty
        /// </summary>
        /// <param name="idCongTy">Mã công ty</param>
        private void LayThongTinCongTy(string idCongTy)
        {
            SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
            myCon.Open();
            string sql = "SELECT TenCongTy, DiaChi, DienThoai FROM CongTy WHERE IdCongty=" + Convert.ToInt32(idCongTy);
            SqlDataAdapter da = new SqlDataAdapter(sql, myCon);

            dtCongTy.Clear();
            da.Fill(dtCongTy);
            myCon.Close();
            myCon.Dispose();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtMaPhuTung.Text))
                {
                    MessageBox.Show("Mã PT không được để trống !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtMaPhuTung.Focus();
                    return;
                }
                if (String.IsNullOrEmpty(txtDonGia.Text))
                {
                    MessageBox.Show("Đơn giá không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtDonGia.Focus();
                    return;
                }
                if (cboTenKho.SelectedValue == null)
                {
                    MessageBox.Show("Bạn chưa chọn Kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cboTenKho.Focus();
                    return;
                }
                int _soluong = 0;

                try
                {
                    _soluong = Convert.ToInt32(txtSoLuongTon.Text);
                }
                catch { }

                SqlConnection myCon = new SqlConnection(Class.datatabase.connect);

                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myCon;
                myCon.Open();

                cmd.CommandText = @"select * from dbo.PhuTung where MaPT = @MaPT and IdKho = @Idkho and IdCongTy = @IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@MaPT", txtMaPhuTung.Text.Trim());
                cmd.Parameters.AddWithValue("@IdKho", cboTenKho.SelectedValue);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable phuTung = new DataTable();
                adapter.Fill(phuTung);
                if(phuTung.Rows.Count > 0)
                {
                    //int soLuongTon = int.Parse(phuTung.Rows[0]["SoLuong"].ToString());
                    //int soCanCong = soLuongTon + int.Parse(txtSoLuongTon.Text.Trim());
                    //cmd.CommandText = @"update dbo.PhuTung Set SoLuong = @soluong, DonGia = @dongia where MaPT = @MaPT and IdKho = @Idkho and IdCongTy = @IdCongTy";
                    //cmd.Parameters.Clear();
                    //cmd.Parameters.AddWithValue("@soluong", soCanCong);
                    //cmd.Parameters.AddWithValue("@dongia", float.Parse(txtDonGia.Text.Trim()));
                    //cmd.Parameters.AddWithValue("@MaPT", txtMaPhuTung.Text.Trim());
                    //cmd.Parameters.AddWithValue("@IdKho", cboTenKho.SelectedValue);
                    //cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    //cmd.ExecuteNonQuery();
                    MessageBox.Show("Phụ tùng đã tồn tại, không thể thêm mới", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    myCon.Close();
                    myCon.Dispose();
                    return;
                }
                else
                {
                    cmd.CommandText = @"if not exists(select 1 From PhuTung Where MaPT = @MaPT and Idkho = @Idkho And IdCongTy = @IdCongTy) begin
                                      INSERT INTO PhuTung (MaPT, MaPT1, MaThayThe, TenPT, TenTiengAnh, Model, DonGia, SoLuong, NguongSoLuong, IdKho, DVT, IdCongTy,TienCongTraChoTho)
                                      VALUES (@MaPT, @MaPT1, @MaThayThe, @TenPT, @TenTiengAnh, @Model, @DonGia, @SoLuong, @NguongSoLuong, @IdKho, @DVT, @IdCongTy,@TienCongTraChoTho) select @@Identity end";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@MaPT", txtMaPhuTung.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaPT1", txtMaPhuTung1.Text.Trim());
                    cmd.Parameters.AddWithValue("@MaThayThe", txtMaThayThe.Text.Trim());
                    cmd.Parameters.AddWithValue("@TenPT", txtTenPhuTung.Text);
                    cmd.Parameters.AddWithValue("@TenTiengAnh", txtTenTiengAnh.Text);
                    cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                    cmd.Parameters.AddWithValue("@DonGia", Convert.ToSingle(txtDonGia.Text));
                    cmd.Parameters.AddWithValue("@SoLuong", _soluong);
                    cmd.Parameters.AddWithValue("@DVT", txtDonViTinh.Text);
                    cmd.Parameters.AddWithValue("@IdKho", cboTenKho.SelectedValue);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    try
                    {
                        cmd.Parameters.AddWithValue("@NguongSoLuong", Convert.ToInt32(txtNguongSoLuong.Text));
                    }
                    catch
                    {
                        cmd.Parameters.AddWithValue("@NguongSoLuong", 0);
                    }
                    //*****************
                    cmd.Parameters.AddWithValue("@TienCongTraChoTho", !string.IsNullOrEmpty(txtTienCongTraTho.Text) ? Convert.ToSingle(txtTienCongTraTho.Text) : 0);
                    //*******************
                    string i = Convert.ToString(cmd.ExecuteScalar());
                    if (String.IsNullOrEmpty(i))
                    {
                        MessageBox.Show("Mã phụ tùng đã tồn tại trong kho này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaPhuTung.SelectAll();
                        txtMaPhuTung.Focus();
                        myCon.Close();
                        myCon.Dispose();
                        return;
                    }
                    int id = int.Parse(i);
                    voidCapNhatBangPhuTungForm();
                }
                myCon.Close();
                myCon.Dispose();
                MessageBox.Show("Thêm phụ tùng mới thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonX10_Click(object sender, EventArgs e)
        {
            buttonX9.Enabled = false;
            try
            {
                DataTable dt = (DataTable)dgvPhuTung.DataSource;
                Export(dt, "Danh sach", "DANH SÁCH PHỤ TÙNG");
            }
            catch { }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                if (idPhuTung != 0)
                {
                    if (String.IsNullOrEmpty(txtMaPhuTung.Text))
                    {
                        MessageBox.Show("Mã PT không được để trống !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtMaPhuTung.Focus();
                        return;
                    }
                    if (String.IsNullOrEmpty(txtDonGia.Text))
                    {
                        MessageBox.Show("Đơn giá không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtDonGia.Focus();
                        return;
                    }
                    if (String.IsNullOrEmpty(txtSoLuongTon.Text))
                    {
                        MessageBox.Show("Số lượng Tồn không được để trống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        txtSoLuongTon.Focus();
                        return;
                    }
                    if (cboTenKho.SelectedValue == null)
                    {
                        MessageBox.Show("Bạn chưa chọn Kho!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        cboTenKho.Focus();
                        return;
                    }
                    if (txtMaPhuTung.Text != _MaPT)
                    {
                        kq = CheckPhuTung(txtMaPhuTung.Text);
                        if (kq == false)
                        {
                            SqlCommand cmd = new SqlCommand();
                            //cmd.CommandText = "UPDATE PhuTung SET MaPT=@MaPT, MaPT1=@MaPT1, MaThayThe=@MaThayThe, TenPT=@TenPT, TenTiengAnh=@TenTiengAnh, Model=@Model, DonGia=@DonGia, IdKho=@IdKho, DVT=@DVT, SoLuong=@SoLuong, NguongSoLuong=@NguongSoLuong WHERE IdPT=@IdPT";
                            //cmd.CommandText = "UPDATE PhuTung SET MaPT1=@MaPT1, MaThayThe=@MaThayThe, TenPT=@TenPT, TenTiengAnh=@TenTiengAnh, Model=@Model, DonGia=@DonGia, IdKho=@IdKho, DVT=@DVT, SoLuong=@SoLuong WHERE IdPT=@IdPT";
                            cmd.CommandText = "UPDATE PhuTung SET MaPT=@MaPT, MaPT1=@MaPT1, MaThayThe=@MaThayThe, TenPT=@TenPT, TenTiengAnh=@TenTiengAnh, Model=@Model, DonGia=@DonGia, IdKho=@IdKho, DVT=@DVT, SoLuong=@SoLuong, NguongSoLuong=@NguongSoLuong,TienCongTraChoTho=@TienCongTraChoTho WHERE IdPT=@IdPT";
                            cmd.Parameters.AddWithValue("@MaPT", txtMaPhuTung.Text.Trim());
                            cmd.Parameters.AddWithValue("@MaPT1", txtMaPhuTung1.Text.Trim());
                            cmd.Parameters.AddWithValue("@MaThayThe", txtMaThayThe.Text.Trim());
                            cmd.Parameters.AddWithValue("@TenPT", txtTenPhuTung.Text);
                            cmd.Parameters.AddWithValue("@TenTiengAnh", txtTenTiengAnh.Text.Trim());
                            cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                            cmd.Parameters.AddWithValue("@DonGia", Convert.ToSingle(txtDonGia.Text));
                            cmd.Parameters.AddWithValue("@IdKho", cboTenKho.SelectedValue);
                            cmd.Parameters.AddWithValue("@DVT", txtDonViTinh.Text);
                            cmd.Parameters.AddWithValue("@IdPT", idPhuTung);
                            cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(txtSoLuongTon.Text));
                            cmd.Parameters.AddWithValue("@IDCongTy", Class.CompanyInfo.idcongty);
                            //***********
                            cmd.Parameters.AddWithValue("@TienCongTraChoTho", Convert.ToSingle(txtTienCongTraTho.Text));
                            //***********
                            try
                            {
                                cmd.Parameters.AddWithValue("@NguongSoLuong", Convert.ToInt32(txtNguongSoLuong.Text));
                            }
                            catch
                            {
                                cmd.Parameters.AddWithValue("@NguongSoLuong", 0);
                            }
                            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                            {
                                voidCapNhatBangPhuTungForm();
                                MessageBox.Show("Cập nhật phụ tùng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show("Cập nhật thất bại, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật thất bại, mã phụ tùng đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            txtMaPhuTung.SelectAll();
                            txtMaPhuTung.Focus();
                            return;
                        }
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand();
                        //cmd.CommandText = "UPDATE PhuTung SET MaPT=@MaPT, MaPT1=@MaPT1, MaThayThe=@MaThayThe, TenPT=@TenPT, TenTiengAnh=@TenTiengAnh, Model=@Model, DonGia=@DonGia, IdKho=@IdKho, DVT=@DVT, SoLuong=@SoLuong, NguongSoLuong=@NguongSoLuong WHERE IdPT=@IdPT";
                        //cmd.CommandText = "UPDATE PhuTung SET MaPT1=@MaPT1, MaThayThe=@MaThayThe, TenPT=@TenPT, TenTiengAnh=@TenTiengAnh, Model=@Model, DonGia=@DonGia, IdKho=@IdKho, DVT=@DVT, SoLuong=@SoLuong WHERE IdPT=@IdPT";
                        cmd.CommandText = "UPDATE PhuTung SET MaPT=@MaPT, MaPT1=@MaPT1, MaThayThe=@MaThayThe, TenPT=@TenPT, TenTiengAnh=@TenTiengAnh, Model=@Model, DonGia=@DonGia, IdKho=@IdKho, DVT=@DVT, SoLuong=@SoLuong, NguongSoLuong=@NguongSoLuong,TienCongTraChoTho=@TienCongTraChoTho WHERE IdPT=@IdPT";
                        cmd.Parameters.AddWithValue("@MaPT", txtMaPhuTung.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaPT1", txtMaPhuTung1.Text.Trim());
                        cmd.Parameters.AddWithValue("@MaThayThe", txtMaThayThe.Text.Trim());
                        cmd.Parameters.AddWithValue("@TenPT", txtTenPhuTung.Text);
                        cmd.Parameters.AddWithValue("@TenTiengAnh", txtTenTiengAnh.Text.Trim());
                        cmd.Parameters.AddWithValue("@Model", txtModel.Text);
                        cmd.Parameters.AddWithValue("@DonGia", Convert.ToSingle(txtDonGia.Text));
                        cmd.Parameters.AddWithValue("@IdKho", cboTenKho.SelectedValue);
                        cmd.Parameters.AddWithValue("@DVT", txtDonViTinh.Text);
                        cmd.Parameters.AddWithValue("@IdPT", idPhuTung);
                        cmd.Parameters.AddWithValue("@SoLuong", Convert.ToInt32(txtSoLuongTon.Text));
                        cmd.Parameters.AddWithValue("@IDCongTy", Class.CompanyInfo.idcongty);
                        //***********
                        cmd.Parameters.AddWithValue("@TienCongTraChoTho", (txtTienCongTraTho.Text != "") ? Convert.ToSingle(txtTienCongTraTho.Text) : 0);
                        //***********
                        try
                        {
                            cmd.Parameters.AddWithValue("@NguongSoLuong", Convert.ToInt32(txtNguongSoLuong.Text));
                        }
                        catch
                        {
                            cmd.Parameters.AddWithValue("@NguongSoLuong", 0);
                        }
                        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                        {
                            voidCapNhatBangPhuTungForm();
                            MessageBox.Show("Cập nhật phụ tùng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Cập nhật phụ tùng thất bại, vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }
                }
                else { MessageBox.Show("Bạn chưa chọn Phụ tùng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            try
            {
                if (_MaPT != null)
                {
                    DialogResult chon = MessageBox.Show("Bạn có chắc chắn muốn xóa phụ tùng này.", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (chon == DialogResult.Yes)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "DELETE FROM PhuTung WHERE MaPT=@MaPT and IdCongTy=@IdCongTy";

                        cmd.Parameters.AddWithValue("@MaPT", _MaPT);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                        {
                            voidCapNhatBangPhuTungForm();
                            MessageBox.Show("Xóa phụ tùng thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ClearForm();
                        }
                    }
                }
                else { MessageBox.Show("Hãy chọn phụ tùng muốn xóa !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                //}
            }
            catch (Exception ex) { MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            ClearForm();
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            buttonX9.Enabled = false;
            if (String.IsNullOrEmpty(txtTimKiemMaPT.Text.Trim()) && String.IsNullOrEmpty(txtTimKiemSoLuong.Text.Trim()))
            {
                MessageBox.Show("Bạn phải nhập vào Mã phụ tùng.\nHoặc Số lượng Phụ tùng còn để bắt đầu tìm kiếm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTimKiemMaPT.Focus();
                return;
            }
            
            try
            {
                SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
                if(myCon.State == ConnectionState.Closed)
                {
                    myCon.Open();
                }

                if (txtTimKiemMaPT.Text.Trim() == "" && txtTimKiemSoLuong.Text.Trim() == "")
                {
                    myCon.Close();
                    myCon.Dispose();
                    return;
                }
                else
                {
                    if (txtTimKiemMaPT.Text.Trim() != "" && txtTimKiemSoLuong.Text.Trim() == "")
                    {
                        string sql = "";

                        if (cboCuaHang.SelectedValue != null && Convert.ToInt32(cboCuaHang.SelectedValue) != 0)
                        {
                            sql = "SELECT DISTINCT IdPT, MaPT,TenPT,DVT,(select top 1 DonGia FROM PhuTung pt inner JOIN KhoHang ON pt.IdKho=KhoHang.IdKho WHERE pt.MaPT like @MaPT" +
                            "  AND KhoHang.IdCuaHang='" + Convert.ToInt32(cboCuaHang.SelectedValue) + "'order by pt.IdPT desc)" +
                            " as DonGia, sum(SoLuong) SoLuong,NguongSoLuong,pt.IdKho,KhoHang.TenKho,MaPT1,MaThayThe,TenTiengAnh,Model FROM PhuTung pt INNER JOIN KhoHang ON pt.IdKho=KhoHang.IdKho WHERE pt.IdCongTy=@IdCongTy AND pt.MaPT like @MaPT";
                            sql += " AND KhoHang.IdCuaHang='" + Convert.ToInt32(cboCuaHang.SelectedValue) + "'";
                        }
                        else
                        {
                            sql = "SELECT DISTINCT IdPT, MaPT,TenPT,DVT,(select top 1 DonGia FROM PhuTung pt inner JOIN KhoHang ON pt.IdKho=KhoHang.IdKho WHERE pt.MaPT like @MaPT order by pt.IdPT desc) as DonGia, sum(SoLuong) SoLuong,NguongSoLuong,pt.IdKho,KhoHang.TenKho,MaPT1,MaThayThe,TenTiengAnh,Model FROM PhuTung pt INNER JOIN KhoHang ON pt.IdKho=KhoHang.IdKho WHERE pt.IdCongTy=@IdCongTy AND pt.MaPT like @MaPT";
                        }

                        sql += " group by IdPT, TenPT, MaPT, DVT, NguongSoLuong, pt.IdKho, KhoHang.TenKho,"
                            + " MaPT1, MaThayThe, TenTiengAnh, Model, TienCongTraChoTho, PositionKe,"
                            + " PositionTang, PositionO";

                        SqlDataAdapter da = new SqlDataAdapter(sql, myCon);
                        da.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        da.SelectCommand.Parameters.AddWithValue("@MaPT", "%" + txtTimKiemMaPT.Text.Trim() + "%");

                        bangPhuTung.Clear();
                        da.Fill(bangPhuTung);
                        lbl_SL.Text = Convert.ToString(bangPhuTung.Rows.Count);
                        if (bangPhuTung.Rows.Count > 0)
                        {
                            dgvPhuTung.DataSource = bangPhuTung;
                            myCon.Close();
                            myCon.Dispose();
                        }
                        else { 
                            MessageBox.Show("Mã phụ tùng này không tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            myCon.Close();
                            myCon.Dispose();
                        }
                    }
                    else if (txtTimKiemMaPT.Text.Trim() == "" && txtTimKiemSoLuong.Text.Trim() != "")
                    {
                        try
                        {
                            //string sql = "SELECT * FROM PhuTung WHERE IdCongTy=" + Class.CompanyInfo.idcongty + " AND SoLuong " + txtTimKiemSoLuong.Text.Trim();
                            string sql = "SELECT DISTINCT MaPT,TenPT,DVT,(select top 1 DonGia FROM PhuTung pt inner JOIN KhoHang ON pt.IdKho=KhoHang.IdKho WHERE pt.MaPT like @MaPT AND KhoHang.IdCuaHang='" + Convert.ToInt32(cboCuaHang.SelectedValue) + "'" + " order by pt.IdPT desc) as DonGia,sum(SoLuong) SoLuong,NguongSoLuong,pt.IdKho,KhoHang.TenKho,MaPT1,MaThayThe,TenTiengAnh,Model FROM PhuTung pt INNER JOIN KhoHang ON pt.IdKho=KhoHang.IdKho WHERE pt.IdCongTy=@IdCongTy";

                            if (cboCuaHang.SelectedValue != null && Convert.ToInt32(cboCuaHang.SelectedValue) != 0)
                            {
                                sql += " AND KhoHang.IdCuaHang='" + Convert.ToInt32(cboCuaHang.SelectedValue) + "'";
                            }

                            sql += " group by TenPT, MaPT, DVT, NguongSoLuong, pt.IdKho, KhoHang.TenKho,"
                            + " MaPT1, MaThayThe, TenTiengAnh, Model, TienCongTraChoTho, PositionKe,"
                            + " PositionTang, PositionO HAVING SUM(SoLuong)" + txtTimKiemSoLuong.Text.Trim();


                            SqlDataAdapter da = new SqlDataAdapter(sql, myCon);
                            da.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            //da.SelectCommand.Parameters.AddWithValue("@SoLuong", txtTimKiemSoLuong.Text.Trim());

                            bangPhuTung.Clear();
                            da.Fill(bangPhuTung);
                            lbl_SL.Text = Convert.ToString(bangPhuTung.Rows.Count);
                            if (bangPhuTung.Rows.Count > 0)
                            {
                                dgvPhuTung.DataSource = bangPhuTung;
                                myCon.Close();
                                myCon.Dispose();
                            }
                            else { 
                                MessageBox.Show("Không có Phụ tùng nào thỏa mãi điều kiện !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                myCon.Close();
                                myCon.Dispose();
                            }

                        }
                        catch (Exception ex) { 
                            MessageBox.Show("Điều kiện tìm kiếm 'Số lượng phụ tùng còn' không đúng (> < = <>)" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            myCon.Close();
                            myCon.Dispose();
                        };
                    }
                    else if (txtTimKiemMaPT.Text.Trim() != "" && txtTimKiemSoLuong.Text.Trim() != "")
                    {
                        try
                        {
                            string sql = "select DISTINCT MaPT,TenPT,DVT,(select top 1 DonGia FROM PhuTung pt inner JOIN KhoHang ON pt.IdKho=KhoHang.IdKho WHERE pt.MaPT like @MaPT AND KhoHang.IdCuaHang='" + Convert.ToInt32(cboCuaHang.SelectedValue) + "'" + " order by pt.IdPT desc) as DonGia,sum(SoLuong) SoLuong,NguongSoLuong,pt.IdKho,KhoHang.TenKho,MaPT1,MaThayThe,TenTiengAnh,Model" +
                                " FROM PhuTung pt INNER JOIN KhoHang ON pt.IdKho=KhoHang.IdKho where pt.IdCongTy=@IdCongTy AND pt.MaPT like @MaPT";

                            if (cboCuaHang.SelectedValue != null && Convert.ToInt32(cboCuaHang.SelectedValue) != 0)
                            {
                                sql += " AND KhoHang.IdCuaHang='" + Convert.ToInt32(cboCuaHang.SelectedValue) + "'";
                            }

                            sql += " group by TenPT, MaPT, DVT, NguongSoLuong, pt.IdKho, KhoHang.TenKho,"
                            + "MaPT1, MaThayThe, TenTiengAnh, Model, TienCongTraChoTho, PositionKe,"
                            + "PositionTang, PositionO HAVING SUM(SoLuong)" + txtTimKiemSoLuong.Text.Trim();

                            SqlDataAdapter da = new SqlDataAdapter(sql, myCon);
                            da.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            da.SelectCommand.Parameters.AddWithValue("@MaPT", txtTimKiemMaPT.Text.Trim() + "%");
                            //da.SelectCommand.Parameters.AddWithValue("@SoLuong", txtTimKiemSoLuong.Text.Trim());

                            bangPhuTung.Clear();
                            da.Fill(bangPhuTung);
                            lbl_SL.Text = Convert.ToString(bangPhuTung.Rows.Count);
                            if (bangPhuTung.Rows.Count > 0)
                            {
                                dgvPhuTung.DataSource = bangPhuTung;
                                myCon.Close();
                                myCon.Dispose();
                            }
                            else { 
                                MessageBox.Show("Không có Phụ tùng nào thỏa mãi điều kiện !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                myCon.Close();
                                myCon.Dispose();
                            }

                        }
                        catch (Exception ex) { 
                            MessageBox.Show("Điều kiện tìm kiếm 'Số lượng phụ tùng còn' không đúng (> < = <>)" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            myCon.Close();
                            myCon.Dispose();
                        };
                    }
                }
                dgvPhuTung.Columns["IdKho"].Visible = false;
                //dgvPhuTung.Columns["IdCongTy"].Visible = false;
                //dgvPhuTung.Columns["IdCongtyDoiTac"].Visible = false;
                dgvPhuTung.Columns["DonGia"].DefaultCellStyle.Format = "0,0";

            }
            catch (Exception ex) { 
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void buttonX6_Click(object sender, EventArgs e)
        {
            buttonX9.Enabled = false;
            voidCapNhatBangPhuTungForm();
        }

        private void buttonX7_Click(object sender, EventArgs e)
        {
            buttonX9.Enabled = false;
            #region comment

            //try
            //{
            //    SqlCommand cmd = new SqlCommand("sp_ThongTinTienVon3");
            //    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    DataTable dtTienVon = Class.datatabase.getData(cmd);
            //    if (!dtPhuTung.Columns.Contains("TienVon"))
            //    {
            //        dtPhuTung.Columns.Add("TienVon", typeof(decimal));
            //    }
            //    if (!dtPhuTung.Columns.Contains("Gia"))
            //    {
            //        dtPhuTung.Columns.Add("Gia", typeof(decimal));
            //    }
            //    foreach (DataRow r in dtPhuTung.Rows)
            //    {
            //        DataRow[] rows = dtTienVon.Select("IdPT = '" + Convert.ToString(r["IdPT"]) + "'");
            //        try
            //        {
            //            r["TienVon"] = Convert.ToDecimal(rows[0]["Gia"]) * Convert.ToDecimal(r["SoLuong"]);

            //        }
            //        catch { r["TienVon"] = 0; }
            //        try
            //        {
            //            r["Gia"] = rows[0]["Gia"];
            //        }
            //        catch { }

            //    }
            //    frmThongKe frm = new frmThongKe();
            //    frm.reportViewer1.LocalReport.DataSources.Clear();
            //    frm.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2.0.Report.ReportBaoCaoPhuTungTrongKho.rdlc";
            //    Microsoft.Reporting.WinForms.ReportDataSource data1 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dtPhuTung);
            //    frm.reportViewer1.LocalReport.DataSources.Add(data1);
            //    frm.reportViewer1.RefreshReport();
            //    frm.Show();
            //}
            //catch (Exception ex) { MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }

            #endregion comment

            try
            {
                SqlCommand cmd = new SqlCommand("sp_ThongTinTienVon3");
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.CommandType = CommandType.StoredProcedure;
                DataTable dtTienVon = Class.datatabase.getData(cmd);
                if (!bangPhuTung.Columns.Contains("TienVon"))
                {
                    bangPhuTung.Columns.Add("TienVon", typeof(decimal));
                }
                if (!bangPhuTung.Columns.Contains("Gia"))
                {
                    bangPhuTung.Columns.Add("Gia", typeof(decimal));
                }
                foreach (DataRow r in bangPhuTung.Rows)
                {
                    DataRow[] rows = dtTienVon.Select("IdPT = '" + Convert.ToString(r["IdPT"]) + "'");
                    try
                    {
                        r["TienVon"] = Convert.ToDecimal(rows[0]["Gia"]) * Convert.ToDecimal(r["SoLuong"]);
                    }
                    catch { r["TienVon"] = 0; }
                    try
                    {
                        r["Gia"] = rows[0]["Gia"];
                    }
                    catch { }
                }
                cmd.CommandText = "select Tencongty as tencongty from CongTy where IdCongTy=@id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", Class.CompanyInfo.idcongty);
                cmd.CommandType = CommandType.Text;

                DataTable dtthongtin = Class.datatabase.getData(cmd);
                frmThongKe frm = new frmThongKe();
                frm.reportViewer1.LocalReport.DataSources.Clear();
                frm.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.ReportBaoCaoPhuTungTrongKho.rdlc";
                Microsoft.Reporting.WinForms.ReportDataSource data1 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", bangPhuTung);
                Microsoft.Reporting.WinForms.ReportDataSource data2 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet2", dtthongtin);
                frm.reportViewer1.LocalReport.DataSources.Add(data1);
                frm.reportViewer1.LocalReport.DataSources.Add(data2);
                frm.reportViewer1.RefreshReport();
                frm.Show();
            }
            catch (Exception ex) { MessageBox.Show("Lỗi : " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        private void buttonX8_Click(object sender, EventArgs e)
        {
            //OpenFileDialog choose = new OpenFileDialog();
            //choose.Filter = "File Excel|*.xls;*xlsx";
            //if (choose.ShowDialog() == DialogResult.OK)
            //{
            //    strDuongDan = choose.FileName;
            //    txtPart.Text = choose.FileName;
            //}

            using (OpenFileDialog choose = new OpenFileDialog() { Filter = "File Excel|*.xls;*xlsx" })
            {
                if (choose.ShowDialog() == DialogResult.OK)
                {
                    strDuongDan = choose.FileName;
                    txtPart.Text = choose.FileName;
                    try
                    {
                        using (var stream = File.Open(strDuongDan, FileMode.Open, FileAccess.Read))
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
                                cboTenSheets.Items.Clear();
                                foreach (DataTable table in tables)
                                {
                                    cboTenSheets.Items.Add(table.TableName);
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

        private void buttonX9_Click(object sender, EventArgs e)
        {
            if (cboKhoHang.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn kho phụ tùng!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (cboTenSheets.Text.Equals(""))
            {
                MessageBox.Show("Bạn chưa chọn sheet!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                try
                {
                    Insert();
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        public void Insert()
        {
            int phuTungUpdate = 0;
            int tongPhuTung = 0;
            buttonX9.Enabled = false;
            tongPhuTung = listPhuTungDuaLenDB.Count;


            using (SqlConnection myCon = new SqlConnection(Class.datatabase.connect))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = myCon;
                cmd.CommandTimeout = 0;
                if (myCon.State == ConnectionState.Closed)
                {
                    myCon.Open();
                }
                SqlTransaction transaction;
                transaction = myCon.BeginTransaction();
                cmd.Transaction = transaction;
                try
                {
                    foreach (PhuTung042020 item in listPhuTungDuaLenDB)
                    {
                        int soLuongConLai = 0;
                        cmd.Parameters.Clear();
                        //cmd.CommandText = @"if not exists(select 1 From PhuTungTestANC Where MaPT = @MaPT and Idkho = @Idkho And IdCongTy = @IdCongTy) begin
                        //                  INSERT INTO PhuTungTestANC (MaPT, TenPT, DonGia, SoLuong, IdKho, IdCongTy, PositionO)
                        //                  VALUES (@MaPT, @TenPT, @DonGia, @SoLuong, @IdKho, @IdCongTy, @PositionO) end";
                        /*Kiem tra su ton cua phu tung*/
                        cmd.CommandText = @"select SoLuong From dbo.PhuTung Where MaPT = @MaPT and Idkho = @Idkho And IdCongTy = @IdCongTy";
                        cmd.Parameters.AddWithValue("@MaPT", item.MaPT.ToString().Trim());
                        cmd.Parameters.AddWithValue("@IdKho", item.IdKho);
                        cmd.Parameters.AddWithValue("@IdCongTy", item.IdCongTy);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dtSoLuong = new DataTable();
                        adapter.Fill(dtSoLuong);
                        /*
                         * Neu ton tai thi lay so luong cu roi cong them so luong moi
                         * Neu khong ton tai thi insert vao*/
                        if (dtSoLuong.Rows.Count > 0)
                        {
                            soLuongConLai = int.Parse(dtSoLuong.Rows[0]["SoLuong"].ToString()) + item.SoLuong;
                            
                            if (chkGia.Checked)
                            {
                                cmd.CommandText = "UPDATE dbo.PhuTung SET SoLuong = @SoLuong, DonGia = @DonGia WHERE MaPT = @MaPT and Idkho = @Idkho And IdCongTy = @IdCongTy";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@SoLuong", soLuongConLai);
                                cmd.Parameters.AddWithValue("@DonGia", item.DonGia);
                                cmd.Parameters.AddWithValue("@MaPT", item.MaPT.ToString().Trim());
                                cmd.Parameters.AddWithValue("@IdKho", item.IdKho);
                                cmd.Parameters.AddWithValue("@IdCongTy", item.IdCongTy);
                            }
                            else
                            {
                                cmd.CommandText = "UPDATE dbo.PhuTung SET SoLuong = @SoLuong WHERE MaPT = @MaPT and Idkho = @Idkho And IdCongTy = @IdCongTy";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@SoLuong", soLuongConLai);
                                cmd.Parameters.AddWithValue("@MaPT", item.MaPT.ToString().Trim());
                                cmd.Parameters.AddWithValue("@IdKho", item.IdKho);
                                cmd.Parameters.AddWithValue("@IdCongTy", item.IdCongTy);
                            }
                        }
                        else
                        {
                            cmd.Parameters.Clear();
                            cmd.CommandText = "INSERT INTO dbo.PhuTung (MaPT, TenPT, DonGia, SoLuong, IdKho, IdCongTy, PositionO) " +
                                "VALUES (@MaPT, @TenPT, @DonGia, @SoLuong, @IdKho, @IdCongTy, @PositionO)";
                            cmd.Parameters.AddWithValue("@MaPT", item.MaPT.ToString().Trim());
                            cmd.Parameters.AddWithValue("@TenPT", item.TenPT.ToString().Trim());
                            cmd.Parameters.AddWithValue("@DonGia", item.DonGia);
                            cmd.Parameters.AddWithValue("@SoLuong", item.SoLuong);
                            cmd.Parameters.AddWithValue("@IdKho", item.IdKho);
                            cmd.Parameters.AddWithValue("@IdCongTy", item.IdCongTy);
                            cmd.Parameters.AddWithValue("@PositionO", item.PositionO.ToString().Trim());
                        }
                        cmd.ExecuteNonQuery();
                    }
                    transaction.Commit();
                    myCon.Close();
                    myCon.Dispose();
                    MessageBox.Show("Nhập thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    //Rollback
                    transaction.Rollback();
                    myCon.Close();
                    myCon.Dispose();
                    MessageBox.Show("Nhập không thành công, kiểm tra lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                
            }
        }
        private void ClearForm()
        {
            idPhuTung = 0;
            _MaPT = "";
            txtDonGia.Clear();
            txtMaPhuTung.Clear();
            txtMaPhuTung1.Clear();
            txtMaThayThe.Clear();
            txtModel.Clear();
            txtDonViTinh.Clear();
            txtSoLuongTon.Clear();
            txtTenPhuTung.Clear();
            txtTenTiengAnh.Clear();
            txtSoLuongTon.ReadOnly = false;
            txtTienCongTraTho.Clear();
        }

        private void dgvPhuTung_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    DataGridViewRow r = dgvPhuTung.Rows[e.RowIndex];

                    txtMaPhuTung.Text = Convert.ToString(r.Cells["MaPT"].Value);
                    _MaPT = Convert.ToString(r.Cells["MaPT"].Value);
                    txtMaPhuTung1.Text = Convert.ToString(r.Cells["MaPT1"].Value);
                    txtMaThayThe.Text = Convert.ToString(r.Cells["MaThayThe"].Value);
                    txtModel.Text = Convert.ToString(r.Cells["Model"].Value);
                    txtDonViTinh.Text = Convert.ToString(r.Cells["DVT"].Value);
                    txtSoLuongTon.Text = Convert.ToString(r.Cells["SoLuong"].Value);
                    txtTenPhuTung.Text = Convert.ToString(r.Cells["TenPT"].Value);
                    txtDonGia.Text = Convert.ToString(r.Cells["DonGia"].Value);
                    idPhuTung = Convert.ToInt32(r.Cells["IDPT"].Value);
                    txtTenTiengAnh.Text = Convert.ToString(r.Cells["TenTiengAnh"].Value);
                    cboTenKho.SelectedValue = Convert.ToString(r.Cells["IDKho"].Value);
                    soluong = Convert.ToInt32(txtSoLuongTon.Text);
                    txtNguongSoLuong.Text = r.Cells["NguongSoLuong"].Value.ToString();
                    //*****************
                    txtTienCongTraTho.Text = Convert.ToString(r.Cells["TienCongTraChoTho"].Value);
                    //*****************

                    if (Class.EmployeeInfo.Quyen.ToLower() == "qtv")
                        txtSoLuongTon.ReadOnly = false;
                    else
                        txtSoLuongTon.ReadOnly = true;
                }
            }
            catch { }
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
                //Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");
                //cl1.Value2 = "IdPT";
                //cl1.ColumnWidth = 10.5;

                Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");
                cl2.Value2 = "MaPT";
                cl2.ColumnWidth = 20.0;

                Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");
                cl3.Value2 = "TenPT";
                cl3.ColumnWidth = 20.0;

                //Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");
                //cl4.Value2 = "DVT";
                //cl4.ColumnWidth = 10.0;

                Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");
                cl5.Value2 = "DonGia";
                cl5.ColumnWidth = 15.0;

                Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");
                cl6.Value2 = "SoLuong";
                cl6.ColumnWidth = 15.0;

                //Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");
                //cl7.Value2 = "IdKho";
                //cl7.ColumnWidth = 25.0;

                //Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");
                //cl8.Value2 = "TenKho";
                //cl8.ColumnWidth = 15.0;

                //Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");
                //cl9.Value2 = "MaPT1";
                //cl9.ColumnWidth = 15;

                //Microsoft.Office.Interop.Excel.Range cl10 = oSheet.get_Range("J3", "J3");
                //cl10.Value2 = "MaThayThe";
                //cl10.ColumnWidth = 10.0;

                //Microsoft.Office.Interop.Excel.Range cl11 = oSheet.get_Range("K3", "K3");
                //cl11.Value2 = "TenTiengAnh";
                //cl11.ColumnWidth = 15.0;

                //Microsoft.Office.Interop.Excel.Range cl12 = oSheet.get_Range("L3", "L3");
                //cl12.Value2 = "Model";
                //cl12.ColumnWidth = 15.0;

                //Microsoft.Office.Interop.Excel.Range cl13 = oSheet.get_Range("M3", "M3");
                //cl13.Value2 = "Số khung";
                //cl13.ColumnWidth = 15.0;

                //Microsoft.Office.Interop.Excel.Range cl14 = oSheet.get_Range("N3", "N3");
                //cl14.Value2 = "Số máy";
                //cl14.ColumnWidth = 15.0;

                //Microsoft.Office.Interop.Excel.Range cl15 = oSheet.get_Range("O3", "O3");
                //cl15.Value2 = "Loại KH";
                //cl15.ColumnWidth = 10.0;

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
                    arr[r, 1] = dr[1];
                    arr[r, 2] = dr[2];
                    arr[r, 4] = dr[4];
                    arr[r, 5] = dr[5];
                    //for (int c = 0; c < dt.Columns.Count; c++)
                    //{
                    //    arr[r, c] = dr[c];
                    //}
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

        private void frmPhuTung_Load(object sender, EventArgs e)
        {
            buttonX9.Enabled = false;
            if (Class.EmployeeInfo.UserName == "vietlong2khoadmin"
                || Class.EmployeeInfo.UserName == "vietlong3khoadmin"
                || Class.EmployeeInfo.UserName == "vietlong1khoadmin"
                || Class.EmployeeInfo.UserName == "vietlong1saleadmin"
                || Class.EmployeeInfo.UserName == "vietlong2saleadmin"
                || Class.EmployeeInfo.UserName == "vietlong3saleadmin"
                || Class.EmployeeInfo.UserName == "demo")
            {

            }
            else
            {
                buttonX8.Enabled = false;
                buttonX9.Enabled = false;
                cboTenSheets.Enabled = false;
                cboKhoHang.Enabled = false;
                buttonX3.Enabled = false;
                buttonX2.Enabled = false;
                buttonX7.Enabled = false;
            }
            if (Class.CompanyInfo.idcongty == "1" || Class.CompanyInfo.idcongty == "30")
            {
                groupBoxCoSo.Enabled = true;
            }
            else
            {
                groupBoxCoSo.Enabled = false;
            }

            Load_cboCuaHang();

            if (cboCuaHang.SelectedValue != null)
                Load_cboKhoHang(Convert.ToInt32(cboCuaHang.SelectedValue));

            txtMaPhuTung.Focus();
            txtTenCoSo.Text = Class.CompanyInfo.tencongty;
            txtDiaChi.Text = Class.CompanyInfo.diachi;
            txtSoDienThoai.Text = Class.CompanyInfo.phone;

            //Load_ddlTenKho(Class.CompanyInfo.idcongty);
            //PhuTung();
        }

        private void Load_cboCuaHang()
        {
            DataTable tbCuaHang = new DataTable();

            SqlCommand cpp = new SqlCommand();
            cpp.CommandText = "SELECT DISTINCT IdCuaHang, TenCuaHang FROM CuaHang WHERE IdCongTy = @IdCongTy";
            cpp.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            tbCuaHang = Class.datatabase.getData(cpp);

            DataRow dr = tbCuaHang.NewRow();
            dr[0] = 0;
            dr[1] = "---Tất cả---";

            tbCuaHang.Rows.InsertAt(dr, 0);

            cboCuaHang.DisplayMember = "TenCuaHang";
            cboCuaHang.ValueMember = "IdCuaHang";

            cboCuaHang.DataSource = tbCuaHang;

            if (Class.EmployeeInfo.Quyen == "QTV")
            {
                cboCuaHang.SelectedIndex = 0;
                cboCuaHang.Enabled = true;
            }
            else
            {
                cboCuaHang.SelectedValue = Class.EmployeeInfo.IdCuaHang;
                cboCuaHang.Enabled = false;
            }
        }

        //Lấy kho hàng theo của hàng của công ty
        private void Load_cboKhoHang(int _IdCuaHang)
        {
            DataTable tableKhoHang = new DataTable();
            SqlCommand cpp = new SqlCommand();

            cpp.CommandText = "SELECT DISTINCT IdKho, TenKho FROM KhoHang WHERE IdCongTy = @IdCongTy";

            if (_IdCuaHang != 0)
            {
                cpp.CommandText += " AND IdCuaHang='" + _IdCuaHang + "'";
            }

            cpp.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            tableKhoHang = Class.datatabase.getData(cpp);

            cboKhoHang.DataSource = tableKhoHang;
            cboKhoHang.DisplayMember = "TenKho";
            cboKhoHang.ValueMember = "IdKho";

            cboTenKho.DataSource = tableKhoHang;
            cboTenKho.DisplayMember = "TenKho";
            cboTenKho.ValueMember = "IdKho";
        }

        private void frmQuanLyPhuTung_FormClosing(object sender, FormClosingEventArgs e)
        {
            Class.Closing.tontai_PhuTung = false;
        }

        //Lấy tất cả kho hàng của công ty
        private void Load_ddlTenKho(string idcongty)
        {
            SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
            myCon.Open();
            string tencongty = "SELECT IdKho, IdCongTy, IdCuaHang, TenKho, DienGiai FROM KhoHang WHERE IdCongty=" + Convert.ToInt32(idcongty);
            SqlDataAdapter da = new SqlDataAdapter(tencongty, myCon);
            DataTable dt = new DataTable();
            da.Fill(dt);
            myCon.Close();
            myCon.Dispose();
            cboTenKho.DataSource = dt;
            cboTenKho.DisplayMember = "TenKho";
            cboTenKho.ValueMember = "IdKho";
            cboTenKho.Text = "";
        }

        private void voidCapNhatBangPhuTungForm()
        {
            try
            {
                if (strIdCoSo != "")
                {
                    SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
                    myCon.Open();
                    //                    string sql = @"select DISTINCT IdPT, MaPT,TenPT,DVT,DonGia,SoLuong,NguongSoLuong,phutung.IdKho,khohang.TenKho,MaPT1,MaThayThe,TenTiengAnh,Model
                    //                                 from phutung inner join KhoHang on phutung.IdKho=KhoHang.IdKho where phutung.IdCongTy=@IdCongTy";
                    //string sql = @"select DISTINCT IdPT, MaPT,TenPT,DVT,DonGia,SoLuong,NguongSoLuong,TienCongTraChoTho,PhuTung.IdKho,KhoHang.TenKho,MaPT1,MaThayThe,TenTiengAnh,Model
                    //             from PhuTung inner join KhoHang on PhuTung.IdKho=KhoHang.IdKho where PhuTung.IdCongTy=@IdCongTy";
                    string sql = @"select DISTINCT IdPT, MaPT,TenPT,DVT,DonGia,SoLuong,NguongSoLuong,TienCongTraChoTho,PhuTung.IdKho,KhoHang.TenKho,MaPT1,MaThayThe,TenTiengAnh,Model
                                 from PhuTung inner join KhoHang on PhuTung.IdKho=KhoHang.IdKho where PhuTung.IdCongTy=@IdCongTy and PhuTung.IdKho = @IdKho";
                    SqlDataAdapter dataAdapter = new SqlDataAdapter(sql, myCon);
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@IdCongTy", Convert.ToInt32(strIdCoSo));
                    dataAdapter.SelectCommand.Parameters.AddWithValue("@IdKho", Int32.Parse(cboTenKho.SelectedValue.ToString()));
                    bangPhuTung.Clear();
                    dataAdapter.Fill(bangPhuTung);
                    dgvPhuTung.DataSource = bangPhuTung;
                    dgvPhuTung.Columns["IdPT"].Visible = false;
                    dgvPhuTung.Columns["IdKho"].Visible = false;
                    // dgvPhuTung.Columns["IdCongTy"].Visible = false;
                    //  dgvPhuTung.Columns["IdCongtyDoiTac"].Visible = false;
                    dgvPhuTung.Columns["DonGia"].DefaultCellStyle.Format = "0,0";
                    lbl_SL.Text = bangPhuTung.Rows.Count.ToString();
                    myCon.Close();
                    myCon.Dispose();
                }
                else
                {
                    SqlConnection myCon = new SqlConnection(Class.datatabase.connect);
                    myCon.Open();
                    //                    string sql = @"select DISTINCT IdPT, MaPT,TenPT,DVT,DonGia,SoLuong,NguongSoLuong,phutung.IdKho,khohang.TenKho,MaPT1,MaThayThe,TenTiengAnh,Model
                    //                                 from phutung inner join KhoHang on phutung.IdKho=KhoHang.IdKho
                    //                                 where phutung.IdCongTy=@IdCongTy";
                    string sql = @"select DISTINCT IdPT, MaPT,TenPT,DVT,DonGia,SoLuong,NguongSoLuong,TienCongTraChoTho,PhuTung.IdKho,KhoHang.TenKho,MaPT1,MaThayThe,TenTiengAnh,Model
                                 from PhuTung inner join KhoHang on PhuTung.IdKho=KhoHang.IdKho
                                 where PhuTung.IdCongTy=@IdCongTy and PhuTung.IdKho = @IdKho";
                    if (cboCuaHang.SelectedValue != null && Convert.ToInt32(cboCuaHang.SelectedValue) != 0)
                    {
                        sql += @" and KhoHang.IdCuaHang=" + Convert.ToInt32(cboCuaHang.SelectedValue);
                    }
                    SqlDataAdapter da = new SqlDataAdapter(sql, myCon);
                    da.SelectCommand.Parameters.AddWithValue("@IdCongTy", Convert.ToInt32(Class.CompanyInfo.idcongty));
                    da.SelectCommand.Parameters.AddWithValue("@IdKho", Int32.Parse(cboTenKho.SelectedValue.ToString()));
                    bangPhuTung.Clear();
                    da.Fill(bangPhuTung);
                    dgvPhuTung.DataSource = bangPhuTung;
                    dgvPhuTung.Columns["IdPT"].Visible = false;
                    dgvPhuTung.Columns["IdKho"].Visible = false;
                    // dgvPhuTung.Columns["IdCongTy"].Visible = false;
                    //  dgvPhuTung.Columns["IdCongtyDoiTac"].Visible = false;
                    dgvPhuTung.Columns["DonGia"].DefaultCellStyle.Format = "0,0";
                    lbl_SL.Text = bangPhuTung.Rows.Count.ToString();
                    myCon.Close();
                    myCon.Dispose();
                }
            }
            catch { }
        }

        private void cbb_CoSo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbb_CoSo.SelectedIndex == 0)
            {
                strIdCoSo = "1";

                LayThongTinCongTy(strIdCoSo);

                txtTenCoSo.Text = dtCongTy.Rows[0]["TenCongTy"].ToString();
                txtDiaChi.Text = dtCongTy.Rows[0]["DiaChi"].ToString();
                txtSoDienThoai.Text = dtCongTy.Rows[0]["DienThoai"].ToString();

                Load_ddlTenKho(strIdCoSo);

                if (strIdCoSo != Class.CompanyInfo.idcongty)
                {
                    groupBoxFunction.Enabled = false;
                    groupBoxImport.Enabled = false;
                }
                else
                {
                    groupBoxFunction.Enabled = true;
                    groupBoxImport.Enabled = true;
                }
            }
            if (cbb_CoSo.SelectedIndex == 1)
            {
                strIdCoSo = "30";

                LayThongTinCongTy(strIdCoSo);

                txtTenCoSo.Text = dtCongTy.Rows[0]["TenCongTy"].ToString();
                txtDiaChi.Text = dtCongTy.Rows[0]["DiaChi"].ToString();
                txtSoDienThoai.Text = dtCongTy.Rows[0]["DienThoai"].ToString();

                Load_ddlTenKho(strIdCoSo);

                if (strIdCoSo != Class.CompanyInfo.idcongty)
                {
                    groupBoxFunction.Enabled = false;
                    groupBoxImport.Enabled = false;
                }
                else
                {
                    groupBoxFunction.Enabled = true;
                    groupBoxImport.Enabled = true;
                }
            }
        }

        private void txtMaPhuTung_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtTenPhuTung.Focus();
        }

        private void txtMaPhuTung1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtMaThayThe.Focus();
        }

        private void txtMaThayThe_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtTenTiengAnh.Focus();
        }

        private void txtTenPhuTung_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtDonGia.Focus();
        }

        private void txtTenTiengAnh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtModel.Focus();
        }

        private void txtModel_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtDonGia.Focus();
        }

        private void txtDonGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtDonViTinh.Focus();
        }

        private void txtDonViTinh_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtSoLuongTon.Focus();
        }

        private void txtSoLuongTon_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtNguongSoLuong.Focus();
        }

        private void txtTimKiemMaPT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtTimKiemSoLuong.Focus();
        }

        private void txtTimKiemSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                buttonX5.Focus();
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtDonGia.Text))
                {
                    tien = 0;
                }
                else
                {
                    tien = Convert.ToDecimal(txtDonGia.Text);
                }
            }
            catch { MessageBox.Show("Đơn giá phải là kiểu số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            txtDonGia.Text = tien.ToString("0,0");
            txtDonGia.SelectionStart = txtDonGia.Text.Length;
        }

        private void txtSoLuongTon_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtSoLuongTon.Text))
            {
                try
                {
                    sl = Convert.ToInt32(txtSoLuongTon.Text);
                }
                catch
                {
                    MessageBox.Show("Số lượng phải là kiểu số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtSoLuongTon.Text = "0";
                    txtSoLuongTon.SelectAll();
                    txtSoLuongTon.Focus();
                }
            }
        }

        private void cboCuaHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboCuaHang.SelectedValue != null)
            {
                Load_cboKhoHang(Convert.ToInt32(cboCuaHang.SelectedValue));
            }
        }

        private void txtNguongSoLuong_TextChanged(object sender, EventArgs e)
        {
            int flag;

            if (!String.IsNullOrEmpty(txtNguongSoLuong.Text))
            {
                if (int.TryParse(txtNguongSoLuong.Text, out flag))
                {
                    if (txtSoLuongTon.Text.Trim() == "")
                    {
                        txtSoLuongTon.Text = "0";
                    }
                    sl = Convert.ToInt32(txtSoLuongTon.Text);
                }
                else
                {
                    MessageBox.Show("Số lượng phải là kiểu số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtNguongSoLuong.Text = "0";
                    txtNguongSoLuong.SelectAll();
                    txtNguongSoLuong.Focus();
                }
            }
        }

        private void txtNguongSoLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                cboTenKho.Focus();
        }

        private void cboTenKho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtMaPhuTung1.Focus();
        }

        private void txtTienCongTraTho_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txtTienCongTraTho.Text))
                {
                    tien = 0;
                }
                else
                {
                    tien = Convert.ToDecimal(txtTienCongTraTho.Text);
                }
            }
            catch { MessageBox.Show("Tiền công trả cho thợ phải là kiểu số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            txtTienCongTraTho.Text = tien.ToString("0,0");
            txtTienCongTraTho.SelectionStart = txtTienCongTraTho.Text.Length;
        }

        private void txtTienCongTraTho_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                txtDonViTinh.Focus();
        }

        private void CboTenSheets_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonX9.Enabled = true;
            DataTable dataTable = tables[cboTenSheets.SelectedItem.ToString()];
            try
            {
                if (dataTable != null)
                {
                    listPhuTungDuaLenDB = new List<PhuTung042020>();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        PhuTung042020 obj = new PhuTung042020();
                        obj.MaPT = dataTable.Rows[i]["MÃ PHỤ TÙNG"].ToString();
                        obj.TenPT = dataTable.Rows[i]["TÊN PHỤ TÙNG"].ToString();
                        obj.TenKho = cboTenKho.Text.ToString();
                        obj.DonGia = float.Parse(dataTable.Rows[i]["GIÁ VỐN"].ToString());
                        obj.SoLuong = Int32.Parse(dataTable.Rows[i]["SỐ LƯỢNG"].ToString());
                        obj.IdCongTy = Int32.Parse(Class.CompanyInfo.idcongty);
                        obj.IdKho = Int32.Parse(cboTenKho.SelectedValue.ToString());
                        obj.PositionO = dataTable.Rows[i]["VỊ TRÍ"].ToString();
                        listPhuTungDuaLenDB.Add(obj);
                    }
                    dgvPhuTung.DataSource = listPhuTungDuaLenDB;
                    dgvPhuTung.Columns[5].Visible = false;
                    dgvPhuTung.Columns[6].Visible = false;
                    dgvPhuTung.Columns[0].HeaderText = "Mã phụ tùng";
                    dgvPhuTung.Columns[1].HeaderText = "Tên phụ tùng";
                    dgvPhuTung.Columns[2].HeaderText = "Tên kho";
                    dgvPhuTung.Columns[3].HeaderText = "Đơn giá";
                    dgvPhuTung.Columns[4].HeaderText = "Số lượng";
                    dgvPhuTung.Columns[7].HeaderText = "Vị trí";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
        }

        private void CboKhoHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboTenSheets.Text.Equals(""))
            {
                DataTable dataTable = tables[cboTenSheets.SelectedItem.ToString()];
                if (dataTable != null)
                {
                    listPhuTungDuaLenDB = new List<PhuTung042020>();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        PhuTung042020 obj = new PhuTung042020();
                        obj.MaPT = dataTable.Rows[i]["MÃ PHỤ TÙNG"].ToString();
                        obj.TenPT = dataTable.Rows[i]["TÊN PHỤ TÙNG"].ToString();
                        obj.TenKho = cboTenKho.Text.ToString();
                        obj.DonGia = Int32.Parse(dataTable.Rows[i]["GIÁ VỐN"].ToString());
                        obj.SoLuong = Int32.Parse(dataTable.Rows[i]["SỐ LƯỢNG"].ToString());
                        obj.IdCongTy = Int32.Parse(Class.CompanyInfo.idcongty);
                        obj.IdKho = Int32.Parse(cboTenKho.SelectedValue.ToString());
                        obj.PositionO = dataTable.Rows[i]["VỊ TRÍ"].ToString();
                        listPhuTungDuaLenDB.Add(obj);
                    }
                    dgvPhuTung.DataSource = listPhuTungDuaLenDB;
                    dgvPhuTung.Columns[5].Visible = false;
                    dgvPhuTung.Columns[6].Visible = false;
                    dgvPhuTung.Columns[0].HeaderText = "Mã phụ tùng";
                    dgvPhuTung.Columns[1].HeaderText = "Tên phụ tùng";
                    dgvPhuTung.Columns[2].HeaderText = "Tên kho";
                    dgvPhuTung.Columns[3].HeaderText = "Đơn giá";
                    dgvPhuTung.Columns[4].HeaderText = "Số lượng";
                    dgvPhuTung.Columns[7].HeaderText = "Vị trí";
                }
            }
        }

        private void CboTenKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!cboTenSheets.Text.Equals(""))
            {
                DataTable dataTable = tables[cboTenSheets.SelectedItem.ToString()];
                if (dataTable != null)
                {
                    listPhuTungDuaLenDB = new List<PhuTung042020>();
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        PhuTung042020 obj = new PhuTung042020();
                        obj.MaPT = dataTable.Rows[i]["MÃ PHỤ TÙNG"].ToString();
                        obj.TenPT = dataTable.Rows[i]["TÊN PHỤ TÙNG"].ToString();
                        obj.TenKho = cboTenKho.Text.ToString();
                        obj.DonGia = Int32.Parse(dataTable.Rows[i]["GIÁ VỐN"].ToString());
                        obj.SoLuong = Int32.Parse(dataTable.Rows[i]["SỐ LƯỢNG"].ToString());
                        obj.IdCongTy = Int32.Parse(Class.CompanyInfo.idcongty);
                        obj.IdKho = Int32.Parse(cboTenKho.SelectedValue.ToString());
                        obj.PositionO = dataTable.Rows[i]["VỊ TRÍ"].ToString();
                        listPhuTungDuaLenDB.Add(obj);
                    }
                    dgvPhuTung.DataSource = listPhuTungDuaLenDB;
                }
            }
        }

    }
}