using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class FrmThemPhuTungBaoDuong : Form
    {
        #region Delegate

        public delegate void LoadDanhSachPhuTung();

        public LoadDanhSachPhuTung LayPhuTungBaoDuong;
        public LoadDanhSachPhuTung CallFromUcBaoDuong;

        #endregion Delegate

        #region Variable

        public string IdBaoDuong = "";
        private SqlCommand cmd = new SqlCommand();
        private decimal _flaDecimal;
        private int _flaInt;

        #endregion Variable

        public FrmThemPhuTungBaoDuong()
        {
            InitializeComponent();
            
            comboBoxKhoPhuTung.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxKhoPhuTung.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            comboBoxPhuTung.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxPhuTung.AutoCompleteMode = AutoCompleteMode.SuggestAppend;

            comboBoxThoDichVu.AutoCompleteSource = AutoCompleteSource.ListItems;
            comboBoxThoDichVu.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private void FrmThemPhuTungBaoDuong_Load(object sender, EventArgs e)
        {
            LoadKhoPhuTung();
            LoadThoDichVu();
            //***********
            //LoadPhuTung(Convert.ToInt64(comboBoxKhoPhuTung.SelectedValue));
            //************
        }

        private void LoadKhoPhuTung()
        {
            cmd.CommandText = @"SELECT IdKho, TenKho FROM dbo.KhoHang WHERE IdCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            comboBoxKhoPhuTung.DisplayMember = "TenKho";
            comboBoxKhoPhuTung.ValueMember = "IdKho";
            comboBoxKhoPhuTung.DataSource = Class.datatabase.getData(cmd);
        }

        private void LoadPhuTung(long idKho)
        {
            //            cmd.CommandText = @"SELECT IdPT, ISNULL(MaPT, '') + '-- ' + ISNULL(TenPT, '') AS TenPhuTung, MaPT, TenPT, SoLuong, DonGia
            //                                FROM PhuTung WHERE IdCongTy = @IdCongTy AND IdKho = @IdKho";
            cmd.CommandText = @"SELECT IdPT, ISNULL(MaPT, '') + '-- ' + ISNULL(TenPT, '') AS TenPhuTung, MaPT, TenPT, SoLuong, DonGia,TienCongTraChoTho
                                FROM dbo.PhuTung WHERE IdCongTy = @IdCongTy AND IdKho = @IdKho";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@IdKho", idKho);
            DataTable tablePhuTung = Class.datatabase.getData(cmd);
            if (tablePhuTung.Rows.Count > 0)
            {
                comboBoxPhuTung.DisplayMember = "TenPhuTung";
                comboBoxPhuTung.ValueMember = "IdPT";
                comboBoxPhuTung.DataSource = tablePhuTung;
            }
            else
            {
                comboBoxPhuTung.DataSource = null;
                textBoxSoLuongCon.Clear();
                textBoxSoLuongXuat.Text = "1";
            }
        }

        private void LoadThoDichVu()
        {
            cmd.CommandText = @"SELECT IdTho, ISNULL(MaTho, '') + ' -- ' + ISNULL(tenTho, '') AS TenTho
                                FROM dbo.ThoDichVu WHERE IdCongTy = @IdCongTy and TinhTrangLamViec is null";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            comboBoxThoDichVu.DisplayMember = "TenTho";
            comboBoxThoDichVu.ValueMember = "IdTho";
            comboBoxThoDichVu.DataSource = Class.datatabase.getData(cmd);
            comboBoxThoDichVu.SelectedIndex = -1;
        }

        private void comboBoxKhoPhuTung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxKhoPhuTung.SelectedValue != null)
                LoadPhuTung(Convert.ToInt64(comboBoxKhoPhuTung.SelectedValue));
        }

        private void comboBoxPhuTung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxPhuTung.SelectedValue != null)
            {
                DataRowView viewPhuTung = (DataRowView)comboBoxPhuTung.SelectedItem;
                var soLuongCon = viewPhuTung["SoLuong"].ToString();
                var donGia = viewPhuTung["DonGia"].ToString();
                //**************************
                var tienCongTraChoTho = viewPhuTung["TienCongTraChoTho"].ToString();
                txtTienCongTraTho.Text = tienCongTraChoTho.Trim().Length > 0 ? tienCongTraChoTho : "0";
                //**************************
                textBoxSoLuongCon.Text = soLuongCon.Trim().Length > 0 ? soLuongCon : "0";
                txtDonGia.Text = donGia.Trim().Length > 0 ? donGia : "0";
                textBoxSoLuongXuat.Text = "1";
            }
            else
                textBoxSoLuongCon.Clear();
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            if (comboBoxPhuTung.SelectedValue == null)
            {
                MessageBox.Show(@"Bạn chưa chọn Phụ tùng!", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (String.IsNullOrEmpty(textBoxSoLuongCon.Text))
            {
                MessageBox.Show(@"Số lượng phụ tùng trong kho không tồn tại!\nVui lòng kiểm tra lại.", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if (String.IsNullOrEmpty(textBoxSoLuongXuat.Text))
            {
                MessageBox.Show(@"Bạn chưa nhập số lượng phụ tùng xuất ra!\nVui lòng kiểm tra lại.", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                if (LayPhuTungBaoDuong != null)
                {
                    cmd.CommandText = @"SELECT * FROM dbo.LichSuBaoDuongChiTiet2 WHERE IdPhuTung = @IdPhuTung AND IdBaoDuong = @IdBaoDuong";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdPhuTung", comboBoxPhuTung.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
                    DataTable tableChiTietBaoDuong = Class.datatabase.getData(cmd);
                    if (tableChiTietBaoDuong.Rows.Count > 0)
                    {
                        MessageBox.Show(@"Phụ tùng đã chọn đã có trong danh sách phụ tùng xuất ra!\nVui lòng kiểm tra lại.", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //Cập nhật số lượng phụ tùng trong kho
                    cmd.CommandText = @"SELECT IdPT, MaPT, TenPT, SoLuong
                                    FROM dbo.PhuTung WHERE IdKho = @IdKho AND IdPT = @IdPT";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdKho", comboBoxKhoPhuTung.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@IdPT", comboBoxPhuTung.SelectedValue.ToString());
                    DataTable tablePhuTung = Class.datatabase.getData(cmd);
                    if (tablePhuTung.Rows.Count > 0)
                    {
                        int soluong = 0;
                        if (String.IsNullOrEmpty(tablePhuTung.Rows[0]["SoLuong"].ToString()))
                            soluong = 0;
                        else
                            soluong = Convert.ToInt32(tablePhuTung.Rows[0]["SoLuong"].ToString());
                        int soluongsau = soluong - Convert.ToInt32(textBoxSoLuongXuat.Text);
                        cmd.CommandText = @"UPDATE PhuTung
                                            SET SoLuong = @SoLuong WHERE IdCongTy = @IdCongTy AND IdPT = @IdPT";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@SoLuong", soluongsau);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdPT", comboBoxPhuTung.SelectedValue.ToString());
                        Class.datatabase.ExcuteNonQuery(cmd);
                    }
                    //Thêm lịch sử bảo dưỡng
                    cmd.CommandText = @"INSERT INTO dbo.LichSuBaoDuongChiTiet2
                                        (IdBaoDuong, MaPT, TenPhuTung, Soluong, Gia, GiaTien, IdKho, idTho, IdPhuTung,TienTraTho)
                                        VALUES (@IdBaoDuong,@MaPT,@TenPhuTung,@Soluong,@Gia,@GiaTien,@IdKho,@idTho,@IdPhuTung,@TienTraTho)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdBaoDuong", Class.SelectedCustomer._idbaoduong);
                    cmd.Parameters.AddWithValue("@MaPT", ((DataRowView)comboBoxPhuTung.SelectedItem)["MaPT"].ToString());
                    cmd.Parameters.AddWithValue("@TenPhuTung", ((DataRowView)comboBoxPhuTung.SelectedItem)["TenPT"].ToString());
                    cmd.Parameters.AddWithValue("@Soluong", textBoxSoLuongXuat.Text);
                    cmd.Parameters.AddWithValue("@Gia", txtDonGia.Text);
                    cmd.Parameters.AddWithValue("@GiaTien", Convert.ToDecimal(txtDonGia.Text) * Convert.ToDecimal(textBoxSoLuongXuat.Text));

                    if (txtTienCongTraTho.Text.Trim().Length > 0)
                        cmd.Parameters.AddWithValue("@TienTraTho", Convert.ToDecimal(txtTienCongTraTho.Text));
                    else
                        cmd.Parameters.AddWithValue("@TienTraTho", 0);
                    cmd.Parameters.AddWithValue("@IdKho", comboBoxKhoPhuTung.SelectedValue.ToString());
                    if (comboBoxThoDichVu.SelectedValue == null)
                        cmd.Parameters.AddWithValue("@idTho", "");
                    else
                        cmd.Parameters.AddWithValue("@idTho", comboBoxThoDichVu.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@IdPhuTung", comboBoxPhuTung.SelectedValue.ToString());

                    Class.datatabase.ExcuteNonQuery(cmd);

                    MessageBox.Show(@"Thêm mới phụ tùng bảo dưỡng thành công!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    
                    LayPhuTungBaoDuong();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxSoLuongXuat_TextChanged(object sender, EventArgs e)
        {
            int flag;

            if (!String.IsNullOrEmpty(textBoxSoLuongXuat.Text))
            {
                if (int.TryParse(textBoxSoLuongXuat.Text, out flag) == false)
                {
                    MessageBox.Show(@"Số lượng phải là kiểu số!", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxSoLuongXuat.Text = "1";
                    return;
                }

                if (int.Parse(textBoxSoLuongXuat.Text) <= 0)
                {
                    MessageBox.Show(@"Số lượng xuất ra phải lớn hơn 0!", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxSoLuongXuat.Text = "1";
                    return;
                }

                //if (!String.IsNullOrEmpty(textBoxSoLuongCon.Text) && !String.IsNullOrEmpty(textBoxSoLuongXuat.Text))
                //{
                //    if(int.Parse(textBoxSoLuongCon.Text) < int.Parse(textBoxSoLuongXuat.Text))
                //    {
                //        MessageBox.Show(@"Số lượng xuất ra không được lớn hơn số lượng tồn!", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //        textBoxSoLuongXuat.Clear();
                //        return;
                //    }
                //}

                TinhToanCongTho();
            }
        }

        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtDonGia.Text))
            {
                if (decimal.TryParse(txtDonGia.Text, out _flaDecimal))
                {
                    txtDonGia.Text = string.Format("{0:N0}", decimal.Parse(txtDonGia.Text));
                    txtDonGia.SelectionStart = txtDonGia.Text.Length;

                    TinhToanCongTho();
                }
                else
                    txtDonGia.Text = @"0";
            }
        }

        /// <summary>
        /// Tính công thợ cho Kuong Ngan
        /// </summary>
        private void TinhToanCongTho()
        {
            if (Class.CompanyInfo.idcongty == "5"
                || Class.CompanyInfo.idcongty == "6"
                || Class.CompanyInfo.idcongty == "44"
                || Class.CompanyInfo.idcongty == "70"
                || Class.CompanyInfo.idcongty == "73"
                || Class.CompanyInfo.idcongty == "50")
            {
                if (!String.IsNullOrEmpty(txtDonGia.Text) && !String.IsNullOrEmpty(textBoxSoLuongXuat.Text))
                {
                    if (decimal.TryParse(txtDonGia.Text, out _flaDecimal)
                        && (int.TryParse(textBoxSoLuongXuat.Text, out _flaInt))
                        && comboBoxThoDichVu.SelectedValue != null)
                    {
                        DataRowView rowPhuTung = (DataRowView)comboBoxPhuTung.SelectedItem;
                        string donGia = rowPhuTung["DonGia"].ToString();
                        if (donGia.Trim().Length > 0)
                        {
                            if (decimal.TryParse(donGia, out _flaDecimal))
                            {
                                decimal congTho = (Convert.ToDecimal(txtDonGia.Text) - Convert.ToDecimal(donGia)) * Convert.ToDecimal(textBoxSoLuongXuat.Text);

                                txtTienCongTraTho.Text = congTho.ToString();
                            }
                        }
                    }
                }
            }            
        }

        private void txtTienCongTraTho_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtTienCongTraTho.Text))
            {
                if (decimal.TryParse(txtTienCongTraTho.Text, out _flaDecimal))
                {
                    txtTienCongTraTho.Text = string.Format("{0:N0}", decimal.Parse(txtTienCongTraTho.Text));
                    txtTienCongTraTho.SelectionStart = txtTienCongTraTho.Text.Length;
                }
                else
                    txtTienCongTraTho.Text = @"0";
            }
        }

        private void comboBoxThoDichVu_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxThoDichVu.SelectedValue != null)
                TinhToanCongTho();
        }

        private void textBoxSoLuongXuat_Enter(object sender, EventArgs e)
        {
            textBoxSoLuongXuat.SelectAll();
        }

       
    }
}