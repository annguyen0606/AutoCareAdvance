using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Linq;

namespace AutoCareV2._0.UserControls.BaoDuong
{
    public partial class UcThayPhuTung : UserControl
    {
        private DataTable dtPhuTung = new DataTable();
        private DataTable dtKho = new DataTable();
        private Class.KhDB classdb = new Class.KhDB();
        private SqlDataProvider sqlPrv = new SqlDataProvider();
        private DataTable dtxeBaoDuong = new DataTable();
        private int i = 0;
        private int rownumber = -1;
        private DataTable tableBaoDuong = new DataTable();
        private long _IdBaoDuong = 0;

        //xong
        public UcThayPhuTung()
        {
            InitializeComponent();
        }

        private void Load_AutocompleteTextBox()
        {
            AutoCompleteStringCollection BienSo = new AutoCompleteStringCollection();
            AutoCompleteStringCollection SoMay = new AutoCompleteStringCollection();
            AutoCompleteStringCollection SoKhung = new AutoCompleteStringCollection();

            tableBaoDuong = (DataTable)dgvDsXeDangBaoDuong2.DataSource;

            foreach (DataRow item in tableBaoDuong.Rows)
            {
                BienSo.Add(item["BienSo"].ToString());
                SoMay.Add(item["SoMay"].ToString());
                SoKhung.Add(item["Sokhung"].ToString());
            }

            txtBienSo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBienSo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBienSo.AutoCompleteCustomSource = BienSo;

            txtSoKhung.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSoKhung.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSoKhung.AutoCompleteCustomSource = SoKhung;

            txtSoMay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSoMay.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSoMay.AutoCompleteCustomSource = SoMay;
        }

        private void ChonKho()
        {
            SqlCommand cmd = new SqlCommand("Select RTrim(LTrim(MaPT)) + ' - ' + RTrim(LTrim(TenPT)) As TenPhuTung , SoLuong, DonGia, IdPT from PhuTung Where IdCongTy = @IdCongTy And IDKho = @IdKho");
            cmd.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtPhuTung = new DataTable();
            dtPhuTung = Class.datatabase.getData(cmd);
            cboMaPhuTung.DataSource = dtPhuTung;
            if (dtPhuTung.Rows.Count > 0)
            {
                cboMaPhuTung.ValueMember = "IdPT";
                cboMaPhuTung.DisplayMember = "TenPhuTung";
            }
            txtSoLuong.Clear();
            txtSoLuongXuat.Clear();
            txtDonGia.Clear();
        }

        //xong
        private void LoadXeDangBaoDuong()
        {
            string sql = @"select TenXe,IdBaoDuong,BienSo,Sokhung,SoMay,SoLan,TrangThai,
                           CONVERT(bit,ThayDau) as ThayDau,ThayDauMay,
                           (select tenTho from ThoDichVu where IdTho=l.IdThoDuyet) as ThoDuyet, l.IdKhachHang
                           from LichSuBaoDuongXeTam l where IdCongty=@IdCongty and IdCuaHang=@IdCuaHang";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
            dtxeBaoDuong = Class.datatabase.getData(cmd);

            dgvDsXeDangBaoDuong2.DataSource = dtxeBaoDuong;

            dgvDsXeDangBaoDuong2.Columns[0].HeaderText = "Tên xe";

            dgvDsXeDangBaoDuong2.Columns[1].Visible = false;
            dgvDsXeDangBaoDuong2.Columns[2].HeaderText = "Biển số";
            dgvDsXeDangBaoDuong2.Columns[3].HeaderText = "Số khung";
            dgvDsXeDangBaoDuong2.Columns[4].HeaderText = "Số máy";
            dgvDsXeDangBaoDuong2.Columns[5].HeaderText = "Lần bảo dưỡng";
            dgvDsXeDangBaoDuong2.Columns[6].HeaderText = "Trạng thái";
            dgvDsXeDangBaoDuong2.Columns[7].HeaderText = "Thay dầu";
            dgvDsXeDangBaoDuong2.Columns[8].HeaderText = "Thay dầu máy";
            dgvDsXeDangBaoDuong2.Columns[9].HeaderText = "Thợ duyệt";
            dgvDsXeDangBaoDuong2.Columns[10].Visible = false;
        }

        //xong
        private void LoadPhuTungBaoDuong()
        {
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = sqlPrv.GetData(@"select IdBaoDuong,MaPT,IdKho,TenPhuTung,Gia,GiaTien,(select TenKho from KhoHang where IdKho=l.IdKho) as Kho,
            (select tenTho from ThoDichVu where IdTho=l.IdTho) as TenTho,
            IdPhuTung,SoLuong,(l.GiaTien) as ThanhTien,
            (select DonGia from PhuTung where IdPT =l.IdPhuTung group by DonGia) as GiaPT
            from lichsubaoduongchitiettam2 l where IdBaoDuong=" + txtID.Text);
        }

        //xong
        private void dgvDsXeDangBaoDuong2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    chkThayDau.Enabled = true;
                    chkThayDauMay.Enabled = true;

                    txtBienSo.Text = Convert.ToString(dgvDsXeDangBaoDuong2.Rows[e.RowIndex].Cells["BienSo"].Value);
                    txtSoKhung.Text = Convert.ToString(dgvDsXeDangBaoDuong2.Rows[e.RowIndex].Cells["SoKhung"].Value);
                    txtSoMay.Text = Convert.ToString(dgvDsXeDangBaoDuong2.Rows[e.RowIndex].Cells["SoMay"].Value);
                    txtID.Text = Convert.ToString(dgvDsXeDangBaoDuong2.Rows[e.RowIndex].Cells["IdBaoDuong"].Value);
                    LoadPhuTungBaoDuong();

                    rownumber = e.RowIndex;
                    if (Convert.ToString(dgvDsXeDangBaoDuong2.Rows[e.RowIndex].Cells["ThayDau"].Value) == "True")
                    {
                        chkThayDau.Checked = true;
                    }
                    else
                        chkThayDau.Checked = false;
                    if (Convert.ToString(dgvDsXeDangBaoDuong2.Rows[e.RowIndex].Cells["ThayDauMay"].Value) == "True")
                    {
                        chkThayDauMay.Checked = true;
                    }
                    else
                    {
                        chkThayDauMay.Checked = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                chkThayDau.Checked = false;
                chkThayDauMay.Checked = false;
                chkThayDau.Enabled = false;
                chkThayDauMay.Enabled = false;
            }
        }

        //xong
        private void UcThayPhuTung_Load(object sender, EventArgs e)
        {
            txtBienSo.Focus();

            ReloadData();
            dtKho = classdb.LoadTenKho();

            if (dtKho.Rows.Count > 0)
            {
                cboKho.DataSource = dtKho;
                cboKho.ValueMember = "IdKho";
                cboKho.DisplayMember = "TenKho";
                cboKho.Text = null;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select MaPT, IDPT From PhuTung Where IdCongTy = @IdCongTy";
            cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);
            DataTable dt = Class.datatabase.getData(cmd);
            MaPT3.DataSource = dt;
            MaPT3.DisplayMember = "MaPT";
            MaPT3.ValueMember = "IdPT";

            try
            {
                cboKho.SelectedIndex = 0;
                ChonKho();
                cboMaPhuTung.Text = null;
            }
            catch (Exception) { MessageBox.Show("Chưa có kho phụ tùng"); }

            DataTable dtThoSua = new DataTable();
            cmd.CommandText = "Select IDTho, MaTho + ' - ' + TenTho As TenTho From ThoDichVu Where IDCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);
            dtThoSua = Class.datatabase.getData(cmd);
            if (dtThoSua.Rows.Count > 0)
            {
                ChonTho.DataSource = dtThoSua;
                ChonTho.ValueMember = "IdTho";
                ChonTho.DisplayMember = "TenTho";
            }
        }

        private void ReloadData()
        {
            LoadXeDangBaoDuong();
            Load_AutocompleteTextBox();
        }

        //xong
        private void cboKho_SelectionChangeCommitted(object sender, EventArgs e)
        {
            ChonKho();
            cboMaPhuTung_SelectionChangeCommitted(sender, e);
        }

        //xong
        private void cboMaPhuTung_SelectionChangeCommitted(object sender, EventArgs e)
        {
            try
            {
                DataTable slDaDung = new DataTable();
                DataRow[] row = dtPhuTung.Select("IdPT = '" + cboMaPhuTung.SelectedValue.ToString() + "'");
                txtSoLuong.Text = row[0]["SoLuong"].ToString();
                txtDonGia.Text = Convert.ToDecimal(row[0]["DonGia"]).ToString("0,0");
                txtGiaBan.Text = txtDonGia.Text;
                foreach (DataGridViewRow rows in dataGridView1.Rows)
                {
                    if (rows.Cells["MaPT3"].Value.ToString() == cboMaPhuTung.SelectedValue.ToString())
                    {
                        txtSoLuong.Text = (Convert.ToInt32(txtSoLuong.Text) - Convert.ToInt32(rows.Cells["SoLuongxuat3"].Value)).ToString();
                        break;
                    }
                }
                txtSoLuongXuat.Text = "1";
            }
            catch { }
        }

        //xong
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (cboMaPhuTung.SelectedValue == null)
            {
                MessageBox.Show("Bạn chưa chọn phụ tùng");
                return;
            }
            if (txtID.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn xe bảo dưỡng");
                return;
            }
            try
            {
                decimal giatien = 0;

                if (!decimal.TryParse(txtDonGia.Text, out giatien))
                {
                    MessageBox.Show("Giá tiền phải  là số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (giatien < 0)
                {
                    MessageBox.Show("Giá tiền tối thiểu là 0.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (String.IsNullOrEmpty(Convert.ToString(cboMaPhuTung.SelectedValue)))
                {
                    MessageBox.Show("Bạn chưa chọn phụ tùng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                int soLuongKho, soLuongXuat, ktSoLuong;
                soLuongKho = Convert.ToInt32(txtSoLuong.Text);
                if (int.TryParse(txtSoLuongXuat.Text, out soLuongXuat))
                {
                    if (soLuongXuat <= soLuongKho)
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandText = "insert into lichsubaoduongchitiettam2(idbaoduong, MaPT, tenphutung,soluong,gia, GiaTien,IdKho,IdTho,IdPhuTung) values(@idbaoduong, @MaPT, @tenphutung,@soluong,@gia, @GiaTien, @IdKho,@IDTho,@IdPhuTung)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idbaoduong", txtID.Text);
                        cmd.Parameters.AddWithValue("@MaPT", cboMaPhuTung.Text.Split('-')[0]);
                        cmd.Parameters.AddWithValue("@tenphutung", cboMaPhuTung.Text);
                        cmd.Parameters.AddWithValue("@soluong", txtSoLuongXuat.Text);
                        cmd.Parameters.AddWithValue("@gia", decimal.Parse(txtGiaBan.Text));
                        cmd.Parameters.AddWithValue("@GiaTien", decimal.Parse(txtGiaBan.Text) * int.Parse(txtSoLuongXuat.Text));
                        cmd.Parameters.AddWithValue("@IdKho", cboKho.SelectedValue);
                        cmd.Parameters.AddWithValue("@IDTho", "");
                        cmd.Parameters.AddWithValue("@IdPhuTung", cboMaPhuTung.SelectedValue);
                        sqlPrv.ExecuteNonQuery(cmd);

                        LoadPhuTungBaoDuong();
                    }
                    else
                    {
                        MessageBox.Show("Số lượng nhập vào lớn hơn số lượng hiện có.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    txtSoLuong.Text = (Convert.ToInt32(txtSoLuong.Text) - Convert.ToInt32(txtSoLuongXuat.Text)).ToString();
                }
                else
                    MessageBox.Show("Số lượng phải là kiểu số.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        //xong
        private void txtDonGia_TextChanged(object sender, EventArgs e)
        {
            decimal donGia;
            if (decimal.TryParse(txtDonGia.Text.Trim(), out donGia))
            {
                txtDonGia.Text = donGia.ToString("0,0");
                txtDonGia.SelectionStart = txtDonGia.Text.Length;
            }
        }

        //xong
        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            #region Code thừa

            //if (txtID.Text == "")
            //{
            //    MessageBox.Show("Bạn chưa chọn xe bảo dưỡng");
            //    return;
            //}
            //try
            //{
            //    btnCapNhat.Enabled = false;
            //    string thayDau = "False";
            //    int thaydaumay = 0;
            //    SqlCommand cmd = new SqlCommand();

            //    cmd.Connection = Class.datatabase.getConnection();
            //    cmd.Connection.Open();
            //    SqlTransaction tran = cmd.Connection.BeginTransaction();
            //    cmd.Transaction = tran;
            //    try
            //    {
            //        if (chkThayDau.Checked)
            //        {
            //            thayDau = "True";
            //        }
            //        if (chkThayDauMay.Checked)
            //        {
            //            thaydaumay = 1;
            //        }
            //        if (dataGridView1.Rows.Count > 0)
            //        {
            //            foreach (DataGridViewRow rows in dataGridView1.Rows)
            //            {
            //                cmd.CommandText = "update lichsubaoduongxetam Set XuatPT = 'True', ThayDau = @ThayDau ,ThayDauMay=@ThayDauMay Where IdBaoDuong = @IdBaoDuong";
            //                cmd.Parameters.Clear();
            //                cmd.Parameters.AddWithValue("@IdBaoDuong", txtID.Text);
            //                cmd.Parameters.AddWithValue("@ThayDau", thayDau);
            //                cmd.Parameters.AddWithValue("@ThayDauMay", thaydaumay);
            //                cmd.ExecuteNonQuery();
            //            }
            //        }
            //        else
            //        {
            //            cmd.CommandText = "update lichsubaoduongxetam Set XuatPT = 'False', ThayDau = 'False' ,ThayDauMay=0 Where IdBaoDuong = @IdBaoDuong";
            //            cmd.Parameters.Clear();
            //            cmd.Parameters.AddWithValue("@IdBaoDuong", txtID.Text);
            //            cmd.ExecuteNonQuery();
            //            if (chkThayDau.Checked)
            //            {
            //                chkThayDau.Checked = false;
            //                thayDau = "False";
            //                MessageBox.Show("Chưa chọn dầu thay.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                return;
            //            }
            //            if (chkThayDauMay.Checked)
            //            {
            //                chkThayDauMay.Checked = false;
            //                thaydaumay = 0;
            //                MessageBox.Show("Chưa chọn dầu máy thay.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //                return;
            //            }

            //        }
            //        tran.Commit();

            //        MessageBox.Show("Cập nhật phụ tùng thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        txtSoLuong.Clear();
            //        txtSoLuongXuat.Clear();
            //        cboMaPhuTung.Text = "";
            //        txtDonGia.Clear();
            //        cboKho.SelectedIndex = 0;
            //    }
            //    catch (Exception ex)
            //    {
            //        tran.Rollback();
            //        MessageBox.Show("Cập nhật phụ tùng thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    }
            //    finally { cmd.Connection.Close(); }
            //    dgvDsXeDangBaoDuong2.Rows[rownumber].Cells["ThayDau"].Value = thayDau;
            //    dgvDsXeDangBaoDuong2.Rows[rownumber].Cells["ThayDauMay"].Value = thaydaumay;
            //}
            //catch { }
            //finally { btnCapNhat.Enabled = true; }

            #endregion Code thừa
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridView1.Columns["xoa4"].Index)
                    {
                        if (Convert.ToString(cboMaPhuTung.SelectedValue) == dataGridView1.Rows[e.RowIndex].Cells["MaPT3"].Value.ToString())
                        {
                            txtSoLuong.Text = (Convert.ToInt32(txtSoLuong.Text) + Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["SoLuongxuat3"].Value)).ToString();
                        }

                        string IdKho = dataGridView1.Rows[e.RowIndex].Cells["IdKho"].Value.ToString();
                        string IdBaoDuong = dataGridView1.Rows[e.RowIndex].Cells["IdBaoDuong"].Value.ToString();
                        string MaPT = dataGridView1.Rows[e.RowIndex].Cells["MaPT3"].Value.ToString();

                        sqlPrv.ExecuteNonQuery("delete lichsubaoduongchitiettam2 where IdKho='" + IdKho + "' and IdBaoDuong='" + IdBaoDuong + "' and MaPT=N'" + MaPT + "'");

                        //Load lại danh sách phụ tùng bảo dưỡng
                        LoadPhuTungBaoDuong();

                        //Cập nhật lại báo giá
                        var result = from myRow in ((DataTable)dataGridView1.DataSource).AsEnumerable()
                                     where myRow.Field<long>("IdKho") == Convert.ToInt64(IdKho)
                                     && myRow.Field<string>("MaPT") == MaPT && myRow.Field<long>("IdBaoDuong") == Convert.ToInt64(IdBaoDuong)
                                     select myRow;

                        if(result.Count() <= 0)
                        {
                            SqlCommand cmd = new SqlCommand();
                            DataTable tableBaoGia = new DataTable();

                            cmd.CommandText = @"SELECT IdBaoGia, IdKhachHang, IdBaoDuong
                                                FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);

                            tableBaoGia = Class.datatabase.getData(cmd);

                            if(tableBaoGia.Rows.Count > 0)
                            {
                                cmd.CommandText = @"UPDATE BaoGiaPhuTungTam
                                                    SET DaThucHien = @DaThucHien
                                                    WHERE IdKho = @IdKho AND MaPT = @MaPT AND IdBaoGia = @IdBaoGia";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@DaThucHien", false);
                                cmd.Parameters.AddWithValue("@IdKho", IdKho);
                                cmd.Parameters.AddWithValue("@MaPT", MaPT);
                                cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGia.Rows[0]["IdBaoGia"].ToString());

                                Class.datatabase.ExcuteNonQuery(cmd);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("" + ex.Message); }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                string Text = txtGiaBan.Text;
                txtGiaBan.Text = String.Format("{0:N0}", decimal.Parse(Text));
                txtGiaBan.SelectionStart = txtGiaBan.Text.Length;
            }
            catch { }
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == 0 && e.ColumnIndex == 8)
            {
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    row.Cells["ChonTho"].Value = dataGridView1.Rows[0].Cells["ChonTho"].Value;
                }
            }
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                if (e.ColumnIndex == dataGridView1.Columns["ChonTho"].Index)
                {
                    dataGridView1.Rows[e.RowIndex].Cells["ChonTho"].Value = dataGridView1.Rows[e.RowIndex].Cells["ChonTho"].Value;

                    string IdKho = dataGridView1.Rows[e.RowIndex].Cells["IdKho"].Value.ToString();
                    string IdBaoDuong = dataGridView1.Rows[e.RowIndex].Cells["IdBaoDuong"].Value.ToString();
                    string MaPT = dataGridView1.Rows[e.RowIndex].Cells["MaPT3"].Value.ToString();

                    string IdTho_Pt = dataGridView1.Rows[e.RowIndex].Cells["ChonTho"].Value.ToString();
                    sqlPrv.ExecuteNonQuery("update lichsubaoduongchitiettam2 set IdTho='" + IdTho_Pt + "' where IdKho='" + IdKho + "' and IdBaoDuong='" + IdBaoDuong + "' and MaPT=N'" + MaPT + "'");
                }
            }
        }

        private void btclammoi_Click(object sender, EventArgs e)
        {
            ReloadData();

            txtBienSo.Text = "";
            txtDonGia.Text = "";
            txtID.Text = "";
            txtSoKhung.Text = "";
            txtSoMay.Text = "";
            dtKho = classdb.LoadTenKho();

            if (dtKho.Rows.Count > 0)
            {
                cboKho.DataSource = dtKho;
                cboKho.ValueMember = "IdKho";
                cboKho.DisplayMember = "TenKho";
                cboKho.Text = null;
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select MaPT, IDPT From PhuTung Where IdCongTy = @IdCongTy";
            cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);
            DataTable dt = Class.datatabase.getData(cmd);
            MaPT3.DataSource = dt;
            MaPT3.DisplayMember = "MaPT";
            MaPT3.ValueMember = "IdPT";

            try
            {
                cboKho.SelectedIndex = 0;
                ChonKho();
                cboMaPhuTung.Text = null;
            }
            catch { }

            DataTable dtThoSua = new DataTable();
            cmd.CommandText = "Select IDTho, MaTho + ' - ' + TenTho As TenTho From ThoDichVu Where IDCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);
            dtThoSua = Class.datatabase.getData(cmd);
            if (dtThoSua.Rows.Count > 0)
            {
                ChonTho.DataSource = dtThoSua;
                ChonTho.ValueMember = "IdTho";
                ChonTho.DisplayMember = "TenTho";
            }

            //datagridview phụ tùng
            dataGridView1.DataSource = null;

            chkThayDau.Checked = false;
            chkThayDauMay.Checked = false;
            chkThayDau.Enabled = false;
            chkThayDauMay.Enabled = false;
        }

        private void txtGiaBan_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                txtGiaBan.Text = Decimal.Parse(txtGiaBan.Text).ToString("0,0");
            }
            catch
            { }
        }

        private void chkThayDauMay_CheckedChanged(object sender, EventArgs e)
        {
            if (chkThayDauMay.Checked == true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update lichsubaoduongxetam Set XuatPT = 'True', ThayDauMay=@ThayDauMay Where IdBaoDuong = @IdBaoDuong";
                cmd.Parameters.AddWithValue("@ThayDauMay", chkThayDauMay.Checked);
                cmd.Parameters.AddWithValue("@IdBaoDuong", txtID.Text);
                Class.datatabase.ExcuteNonQuery(cmd);
                chkThayDau.Checked = false;
            }

            if (chkThayDauMay.Checked == false)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update lichsubaoduongxetam Set XuatPT = 'False', ThayDauMay=@ThayDauMay Where IdBaoDuong = @IdBaoDuong";
                cmd.Parameters.AddWithValue("@ThayDauMay", chkThayDauMay.Checked);
                cmd.Parameters.AddWithValue("@IdBaoDuong", txtID.Text);
                Class.datatabase.ExcuteNonQuery(cmd);
            }
        }

        private void chkThayDau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkThayDau.Checked == true)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update lichsubaoduongxetam Set XuatPT = 'True', ThayDau=@ThayDau Where IdBaoDuong = @IdBaoDuong";
                cmd.Parameters.AddWithValue("@ThayDau", chkThayDau.Checked);
                cmd.Parameters.AddWithValue("@IdBaoDuong", txtID.Text);
                Class.datatabase.ExcuteNonQuery(cmd);
                chkThayDauMay.Checked = false;
            }
            if (chkThayDau.Checked == false)
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update lichsubaoduongxetam Set XuatPT = 'False', ThayDau=@ThayDau Where IdBaoDuong = @IdBaoDuong";
                cmd.Parameters.AddWithValue("@ThayDau", chkThayDau.Checked);
                cmd.Parameters.AddWithValue("@IdBaoDuong", txtID.Text);
                Class.datatabase.ExcuteNonQuery(cmd);
            }
        }

        private void txtBienSo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchDataOnGridView();
        }

        private void txtSoKhung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchDataOnGridView();
        }

        private void txtSoMay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchDataOnGridView();
        }

        private void SearchDataOnGridView()
        {
            string searchBienSoValue = txtBienSo.Text;
            string searchSoKhungValue = txtSoKhung.Text;
            string searchSoMayValue = txtSoMay.Text;

            try
            {
                var result = from myRow in tableBaoDuong.AsEnumerable()
                             select myRow;

                if (!String.IsNullOrEmpty(searchBienSoValue))
                    result = from myRow in result
                             where myRow.Field<string>("BienSo") == searchBienSoValue
                             select myRow;

                if (!String.IsNullOrEmpty(searchSoKhungValue))
                    result = from myRow in result
                             where myRow.Field<string>("Sokhung") == searchSoKhungValue
                             select myRow;

                if (!String.IsNullOrEmpty(searchSoMayValue))
                    result = from myRow in result
                             where myRow.Field<string>("SoMay") == searchSoMayValue
                             select myRow;

                DataTable tableResult = result.CopyToDataTable();

                dgvDsXeDangBaoDuong2.DataSource = tableResult;
            }
            catch
            {
                MessageBox.Show("Không tồn tại thông tin xe bảo dưỡng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ReloadData();
            }
        }

        private void dgvDsXeDangBaoDuong2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FrmLapBaoGiaSuaChua frmLapBaoGia = new FrmLapBaoGiaSuaChua();
                frmLapBaoGia.IdBaoDuong = Convert.ToInt64(dgvDsXeDangBaoDuong2.Rows[e.RowIndex].Cells["IdBaoDuong"].Value);

                frmLapBaoGia.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void ToolStripMenuItemLapBaoGia_Click(object sender, EventArgs e)
        {
            if (_IdBaoDuong != 0)
            {
                FrmLapBaoGiaSuaChua frmLapBaoGia = new FrmLapBaoGiaSuaChua();
                frmLapBaoGia.IdBaoDuong = _IdBaoDuong;

                frmLapBaoGia.ShowDialog();
            }
        }

        private void dgvDsXeDangBaoDuong2_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                dgvDsXeDangBaoDuong2.ContextMenuStrip = contextMenuStrip;

                try
                {
                    _IdBaoDuong = Convert.ToInt64(dgvDsXeDangBaoDuong2.Rows[e.RowIndex].Cells["IdBaoDuong"].Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _IdBaoDuong = 0;
                }
            }
            else
            {
                dgvDsXeDangBaoDuong2.ContextMenuStrip = null;
                _IdBaoDuong = 0;
            }
        }
    }
}