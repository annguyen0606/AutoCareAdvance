using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls.BaoDuong
{
    public partial class UcChonTho : UserControl
    {
        private DataTable dtTho = new DataTable();
        private DataTable dtThoDuyet = new DataTable();
        private SqlDataProvider sqlPrv = new SqlDataProvider();
        private DataTable tableBaoDuong = new DataTable();
        private long _IdBaoDuong = 0;
        private long _IdCongViecTienCong = 0;
        private DataTable tableCongViecTienCong = new DataTable();
        private EnumerableRowCollection<DataRow> congviec;

        public UcChonTho()
        {
            InitializeComponent();
        }

        //xong
        public void LoadXeDangBaoDuong()
        {
            DataTable dtxeBaoDuong = new DataTable();
            string sql = "select TenXe,IdBaoDuong,BienSo,Sokhung,SoMay,SoLan,TrangThai,CONVERT(bit,ThayDau) as ThayDau,ThayDauMay,(select tenTho from ThoDichVu where IdTho=l.IdThoDuyet) as ThoDuyet from LichSuBaoDuongXeTam l where IdCongty=@IdCongty and IdCuaHang=@IdCuaHang";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
            dtxeBaoDuong = Class.datatabase.getData(cmd);

            dgvDsXeDangBaoDuong3.DataSource = dtxeBaoDuong;

            dgvDsXeDangBaoDuong3.Columns[0].HeaderText = "Tên xe";
            dgvDsXeDangBaoDuong3.Columns[1].Visible = false;
            dgvDsXeDangBaoDuong3.Columns[2].HeaderText = "Biển số";
            dgvDsXeDangBaoDuong3.Columns[3].HeaderText = "Số khung";
            dgvDsXeDangBaoDuong3.Columns[4].HeaderText = "Số máy";
            dgvDsXeDangBaoDuong3.Columns[5].HeaderText = "Lần bảo dưỡng";
            dgvDsXeDangBaoDuong3.Columns[6].HeaderText = "Trạng thái";
            dgvDsXeDangBaoDuong3.Columns[7].HeaderText = "Thay dầu";
            dgvDsXeDangBaoDuong3.Columns[8].HeaderText = "Thay dầu máy";
            dgvDsXeDangBaoDuong3.Columns[9].HeaderText = "Thợ duyệt";
        }

        private void LoadThoDichVu()
        {
            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = @"SELECT IdTho, MaTho, (MaTho + ' -- ' + tenTho) AS TenTho FROM ThoDichVu WHERE IdCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);

            DataTable tableThoDv = new DataTable();
            tableThoDv = Class.datatabase.getData(cmd);

            cboThoSua.DataSource = tableThoDv;
            cboThoSua.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cboThoSua.AutoCompleteSource = AutoCompleteSource.ListItems;
            cboThoSua.DisplayMember = "TenTho";
            cboThoSua.ValueMember = "MaTho";
        }

        //xong
        private void UcChonTho_Load(object sender, EventArgs e)
        {
            txtBienSo3.Select();

            LoadThoDichVu();

            DtgSuaChuaNgoai.AutoGenerateColumns = false;
            DtgBaoDuongTheoTien.AutoGenerateColumns = false;
            DtgBaoDuongTheoGio.AutoGenerateColumns = false;
            cboMaThoDuyet.Enabled = false;

            ReloadData();

            dateTimeInput1.Value = DateTime.Now;
            SqlCommand cmd;
            cmd = new SqlCommand("SELECT IdTho, TenTho, MaTho FROM ThoDichVu WHERE IdCongTy= @IdCongTy");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtTho = Class.datatabase.getData(cmd);
            cmd.CommandText = "Select IDTho, MaTho + ' - ' + TenTho As TenTho From ThoDichVu Where IDCongTy = @IdCongTy";
            dtThoDuyet = Class.datatabase.getData(cmd);
            if (dtTho.Rows.Count > 0)
            {
                cboMaTho.DataSource = dtTho;
                cboMaTho.DisplayMember = "MaTho";
                cboMaTho.ValueMember = "IdTho";
                if (dtThoDuyet.Rows.Count > 0)
                {
                    cboMaThoDuyet.DataSource = dtThoDuyet;
                    cboMaThoDuyet.ValueMember = "IdTho";
                    cboMaThoDuyet.DisplayMember = "TenTho";
                }
                try
                {
                    cboMaThoDuyet.SelectedValue = 28;
                    txtTenThoDuyet.Text = Convert.ToString(dtThoDuyet.Select("IdTho = '" + Convert.ToString(cboMaThoDuyet.SelectedValue) + "'")[0]["TenTho"]);
                }
                catch
                {
                    cboMaThoDuyet.SelectedIndex = 0;
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa có thợ dịch vụ. \nThêm thợ dịch vụ trước!", "Thông báo!");
                return;
            }
        }

        private void Load_AutocompleteTextbox()
        {
            AutoCompleteStringCollection BienSo = new AutoCompleteStringCollection();
            AutoCompleteStringCollection SoMay = new AutoCompleteStringCollection();
            AutoCompleteStringCollection SoKhung = new AutoCompleteStringCollection();

            tableBaoDuong = (DataTable)dgvDsXeDangBaoDuong3.DataSource;

            foreach (DataRow item in tableBaoDuong.Rows)
            {
                BienSo.Add(item["BienSo"].ToString());
                SoMay.Add(item["SoMay"].ToString());
                SoKhung.Add(item["Sokhung"].ToString());
            }

            txtBienSo3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBienSo3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBienSo3.AutoCompleteCustomSource = BienSo;

            txtSoKhung3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSoKhung3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSoKhung3.AutoCompleteCustomSource = SoKhung;

            txtSoMay3.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSoMay3.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSoMay3.AutoCompleteCustomSource = SoMay;
        }

        private void ReloadData()
        {
            LoadXeDangBaoDuong();
            Load_AutocompleteTextbox();
        }

        //xong
        private void DoDuLieuRadgv1(string IdCongTy, string IdBaoDuongXe)
        {
            //Lấy bảo dưỡng theo giờ
            SqlCommand cmd = new SqlCommand("select * from ThoDichVu_GioViecTam where IdCongTy=@IdCongTy and IdBaoDuong=@IdBaoDuong");
            cmd.Parameters.AddWithValue("@IdCongTy", IdCongTy);
            cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
            DataTable dtBaoDuongTheoGio = Class.datatabase.getData(cmd);
            DtgBaoDuongTheoGio.DataSource = dtBaoDuongTheoGio;

            //Lấy bảo dưỡng theo tiền
            cmd.CommandText = "select * from ThoDichVu_TienCongThoTam where IdCongTy=@IdCongTy and IdBaoDuong=@IdBaoDuong";
            DataTable dtBaoDuongTheoTienCong = Class.datatabase.getData(cmd);
            DtgBaoDuongTheoTien.DataSource = dtBaoDuongTheoTienCong;

            //Lấy bảo dưỡng thuê ngoài
            cmd.CommandText = "select * from ThueNgoaiTam where IdCongTy=@IdCongTy and IdBaoDuong=@IdBaoDuong";
            DataTable dtBaoDuongNgoai = Class.datatabase.getData(cmd);
            DtgSuaChuaNgoai.DataSource = dtBaoDuongNgoai;
        }

        //xong
        private void dgvDsXeDangBaoDuong3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
                {
                    txtBienSo3.Text = Convert.ToString(dgvDsXeDangBaoDuong3.Rows[e.RowIndex].Cells["BienSo"].Value);
                    txtSoKhung3.Text = Convert.ToString(dgvDsXeDangBaoDuong3.Rows[e.RowIndex].Cells["SoKhung"].Value);
                    txtSoMay3.Text = Convert.ToString(dgvDsXeDangBaoDuong3.Rows[e.RowIndex].Cells["SoMay"].Value);
                    txtID3.Text = Convert.ToString(dgvDsXeDangBaoDuong3.Rows[e.RowIndex].Cells["IdBaoDuong"].Value);

                    if (!String.IsNullOrEmpty(txtID3.Text))
                    {
                        btnThemTho.Enabled = true;
                        cboMaTho.Enabled = true;
                        cboMaThoDuyet.Enabled = true;
                    }
                    else
                    {
                        btnThemTho.Enabled = false;
                        cboMaTho.Enabled = true;
                        cboMaThoDuyet.Enabled = false;
                    }
                    DoDuLieuRadgv1(Class.CompanyInfo.idcongty, txtID3.Text);
                }
            }
            catch
            { };
        }

        //xong
        private void cboMaThoDuyet_SelectionChangeCommitted_1(object sender, EventArgs e)
        {
            if (cboMaThoDuyet.SelectedIndex >= 0)
            {
                txtTenThoDuyet.Text = Convert.ToString(dtThoDuyet.Select("IdTho = '" + Convert.ToString(cboMaThoDuyet.SelectedValue) + "'")[0]["TenTho"]);

                if (dateTimeInput1.ValueObject == null)
                {
                    MessageBox.Show("Bạn chưa nhập ngày bảo dưỡng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Class.datatabase.getConnection();
                cmd.Connection.Open();
                //SqlTransaction tran = cmd.Connection.BeginTransaction();
                //cmd.Transaction = tran;
                try
                {
                    if (txtID3.Text != "")
                    {
                        cmd.CommandText = "update lichsubaoduongxetam Set IdThoDuyet = @IdThoDuyet Where IdCongTy = @IdCongTy and IdBaoDuong = @IdBaoDuong";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdBaoDuong", txtID3.Text);
                        cmd.Parameters.AddWithValue("@IdThoDuyet", cboMaThoDuyet.SelectedValue);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.ExecuteNonQuery();

                        //tran.Commit();
                        cmd.Connection.Close();
                        LoadXeDangBaoDuong();
                        //MessageBox.Show("Thêm thông tin thợ thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        //MessageBox.Show("Chưa chọn xe cần cập nhật", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    //tran.Rollback();
                    cmd.Connection.Close();
                    MessageBox.Show("Thêm thông tin thợ thất bại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally { btnThemTho.Enabled = true; }
            }
        }

        private void btnThemTho_Click(object sender, EventArgs e)
        {
        }

        private void cboMaTho_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboMaTho.SelectedIndex >= 0)
                {
                    txtTenTho.Text = Convert.ToString(dtTho.Select("IdTho = '" + cboMaTho.SelectedValue.ToString() + "'")[0]["TenTho"]);
                    Class.ThongTinTho.idtho = cboMaTho.SelectedValue.ToString();
                    Class.ThongTinTho.tentho = txtTenTho.Text;
                    Class.ThongTinTho.matho = cboMaTho.Text;

                    FrmChiTietCongTho frmCongTho = new FrmChiTietCongTho();
                    frmCongTho.Owner = this.ParentForm;
                    //Đức Anh đẩy Id bảo dưỡng sang form FrmChiTietCongTho
                    frmCongTho.IdBaoDuong = txtID3.Text;
                    frmCongTho.Check = true;
                    //DoDuLieuRadgv1(Class.CompanyInfo.idcongty, txtID3.Text);
                    if (frmCongTho.ShowDialog() == DialogResult.OK)
                    {
                        DoDuLieuRadgv1(Class.CompanyInfo.idcongty, txtID3.Text);
                    }
                }
            }
            catch (Exception ex) { }
        }

        private void DtgBaoDuongTheoTien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == DtgBaoDuongTheoTien.Columns["Xoa3"].Index)
                {
                    if (MessageBox.Show("Bạn có muốn xóa việc này không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        int RowIndex = e.RowIndex;
                        string IdBaoDuongXe = txtID3.Text;
                        string Id = DtgBaoDuongTheoTien.Rows[RowIndex].Cells["Id"].Value.ToString();
                        string IdTho = DtgBaoDuongTheoTien.Rows[RowIndex].Cells["IDTho6"].Value.ToString();
                        string IdTienCong = DtgBaoDuongTheoTien.Rows[e.RowIndex].Cells["IDTienCong6"].Value.ToString();

                        SqlCommand cm = new SqlCommand(@"SELECT * FROM ThoDichVu_TienCongThoTam WHERE IdCongTy=@IdCongTy AND IdBaoDuong=@IdBaoDuong
                                                         AND IdTienCong=@IdTienCong ORDER BY TienKhachTra DESC");
                        cm.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cm.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                        cm.Parameters.AddWithValue("@IdTienCong", IdTienCong);
                        //cm.Parameters.AddWithValue("@MaTho", txt_MaTho.Text);

                        DataTable dt = new DataTable();
                        dt = Class.datatabase.getData(cm);

                        if (dt.Rows.Count > 1)
                        {
                            int i = dt.Rows.Count - 1;

                            decimal tiencong = Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["TienCong"].ToString()) / i);
                            decimal tienkhachtra = Convert.ToDecimal(dt.Rows[0]["TienKhachTra"].ToString());

                            if (Id != dt.Rows[0]["Id"].ToString())
                            {
                                SqlCommand cmd1 = new SqlCommand(@"UPDATE ThoDichVu_TienCongThoTam SET TienCong=@TienCong WHERE IdCongTy=@IdCongTy
                                                                   AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong");
                                cmd1.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd1.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                                cmd1.Parameters.AddWithValue("@IdTienCong", IdTienCong);
                                cmd1.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["TienCong"].ToString()) + tiencong));
                                //cmd1.Parameters.AddWithValue("@TienKhachTra", Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["TienKhachTra"].ToString()) + tienkhachtra));
                                sqlPrv.ExecuteNonQuery(cmd1);

                                SqlCommand cmd = new SqlCommand("delete ThoDichVu_TienCongThoTam where Id = @Id");
                                cmd.Parameters.AddWithValue("@Id", Id);
                                sqlPrv.ExecuteNonQuery(cmd);

                                DoDuLieuRadgv1(Class.CompanyInfo.idcongty, txtID3.Text);
                            }
                            else
                            {
                                SqlCommand cmd1 = new SqlCommand(@"UPDATE ThoDichVu_TienCongThoTam SET TienCong=@TienCong WHERE IdCongTy=@IdCongTy
                                                                   AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong");
                                cmd1.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd1.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                                cmd1.Parameters.AddWithValue("@IdTienCong", IdTienCong);
                                cmd1.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["TienCong"].ToString()) + tiencong));
                                //cmd1.Parameters.AddWithValue("@TienKhachTra", Convert.ToDecimal(Convert.ToDecimal(dt.Rows[0]["TienKhachTra"].ToString()) + tienkhachtra));
                                sqlPrv.ExecuteNonQuery(cmd1);

                                SqlCommand cmd2 = new SqlCommand(@"UPDATE ThoDichVu_TienCongThoTam SET TienKhachTra=@TienKhachTra WHERE IdCongTy=@IdCongTy
                                                                   AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong AND Id=@Id");
                                cmd2.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd2.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);
                                cmd2.Parameters.AddWithValue("@IdTienCong", IdTienCong);
                                cmd2.Parameters.AddWithValue("@TienKhachTra", Convert.ToDecimal(dt.Rows[0]["TienKhachTra"].ToString()));
                                cmd2.Parameters.AddWithValue("@Id", Convert.ToDecimal(dt.Rows[1]["Id"].ToString()));
                                sqlPrv.ExecuteNonQuery(cmd2);

                                SqlCommand cmd = new SqlCommand("delete ThoDichVu_TienCongThoTam where Id=@Id");
                                cmd.Parameters.AddWithValue("@Id", Id);
                                sqlPrv.ExecuteNonQuery(cmd);

                                DoDuLieuRadgv1(Class.CompanyInfo.idcongty, txtID3.Text);
                            }
                        }
                        else
                        {
                            SqlCommand cmd = new SqlCommand("delete ThoDichVu_TienCongThoTam where Id=@Id");
                            cmd.Parameters.AddWithValue("@Id", Id);
                            sqlPrv.ExecuteNonQuery(cmd);
                            DoDuLieuRadgv1(Class.CompanyInfo.idcongty, txtID3.Text);
                        }

                        //Cập nhật lại thông tin báo giá
                        var result = from myRow in ((DataTable)DtgBaoDuongTheoTien.DataSource).AsEnumerable()
                                     where myRow.Field<int>("IdTienCong") == Convert.ToInt32(IdTienCong)
                                     select myRow;

                        if (result.Count() <= 0)
                        {
                            SqlCommand cmd = new SqlCommand();
                            DataTable tableBaoGia = new DataTable();

                            cmd.CommandText = @"SELECT IdBaoGia, IdKhachHang, IdBaoDuong
                                                FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuongXe);

                            tableBaoGia = Class.datatabase.getData(cmd);

                            if (tableBaoGia.Rows.Count > 0)
                            {
                                cmd.CommandText = @"UPDATE BaoGiaCongThoTam
                                                    SET DaThucHien = @DaThucHien
                                                    WHERE IdTienCong = @IdTienCong AND IdBaoGia = @IdBaoGia";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@DaThucHien", false);
                                cmd.Parameters.AddWithValue("@IdTienCong", IdTienCong);
                                cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGia.Rows[0]["IdBaoGia"].ToString());

                                Class.datatabase.ExcuteNonQuery(cmd);
                            }
                        }
                    }
                }
            }
        }

        private void cboMaTho_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cboMaThoDuyet.SelectedIndex >= 0)
            {
                txtTenThoDuyet.Text = Convert.ToString(dtThoDuyet.Select("IdTho = '" + Convert.ToString(cboMaThoDuyet.SelectedValue) + "'")[0]["TenTho"]);
            }
        }

        private void DtgBaoDuongTheoGio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                if (e.ColumnIndex == DtgBaoDuongTheoGio.Columns["Xoa2"].Index)
                {
                    if (MessageBox.Show("Bạn có muốn xóa việc này không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        int RowIndex = e.RowIndex;
                        string IdBaoDuongXe = txtID3.Text;
                        string IdTho = DtgBaoDuongTheoGio.Rows[RowIndex].Cells["IDTho5"].Value.ToString();

                        SqlCommand cmd = new SqlCommand("delete ThoDichVu_GioViecTam where IdCongTy=@IdCongTy and IdTho=@IdTho and IdBaoDuong=@IdBaoDuong");
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdTho", IdTho);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", txtID3.Text);
                        sqlPrv.ExecuteNonQuery(cmd);
                        DoDuLieuRadgv1(Class.CompanyInfo.idcongty, txtID3.Text);
                    }
                }
            }
        }

        private void DtgSuaChuaNgoai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
            {
                //int RowIndex = e.RowIndex;
                if (e.ColumnIndex == DtgSuaChuaNgoai.Columns["Xoa1"].Index)
                {
                    if (MessageBox.Show("Bạn có muốn xóa việc này không?", "Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    {
                        string IdBaoDuongXe = txtID3.Text;
                        string IdTho = DtgSuaChuaNgoai.Rows[e.RowIndex].Cells["IDTho7"].Value.ToString();

                        SqlCommand cmd = new SqlCommand("delete ThueNgoaiTam where IdCongTy=@IdCongTy and IdTho=@IdTho and IdBaoDuong=@IdBaoDuong");
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdTho", IdTho);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", txtID3.Text);
                        sqlPrv.ExecuteNonQuery(cmd);
                        DoDuLieuRadgv1(Class.CompanyInfo.idcongty, txtID3.Text);
                    }
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtBienSo3.Text = "";
            txtID3.Text = "";
            txtSoKhung3.Text = "";
            txtSoMay3.Text = "";

            txtBienSo3.Focus();

            ReloadData();

            dateTimeInput1.Value = DateTime.Now;
            DtgBaoDuongTheoGio.DataSource = null;
            DtgBaoDuongTheoTien.DataSource = null;
            DtgSuaChuaNgoai.DataSource = null;

            cboMaTho.Enabled = false;
            cboMaThoDuyet.Enabled = false;
        }

        private void cboMaThoDuyet_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void txtBienSo3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchOnDataGridview(sender);
        }

        private void txtSoKhung3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchOnDataGridview(sender);
        }

        private void txtSoMay3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchOnDataGridview(sender);
        }

        private void SearchOnDataGridview(object sender)
        {
            string searchBienSoValue = txtBienSo3.Text;
            string searchSoKhungValue = txtSoKhung3.Text;
            string searchSoMayValue = txtSoMay3.Text;

            dgvDsXeDangBaoDuong3.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            try
            {
                var results = from myRow in tableBaoDuong.AsEnumerable()
                              select myRow;

                if (!String.IsNullOrEmpty(searchBienSoValue))
                    results = from myRow in results
                              where myRow.Field<string>("BienSo") == searchBienSoValue
                              select myRow;

                if (!String.IsNullOrEmpty(searchSoKhungValue))
                    results = from myRow in results
                              where myRow.Field<string>("Sokhung") == searchSoKhungValue
                              select myRow;

                if (!String.IsNullOrEmpty(searchSoMayValue))
                    results = from myRow in results
                              where myRow.Field<string>("SoMay") == searchSoMayValue
                              select myRow;

                DataTable tableResult = results.CopyToDataTable();

                dgvDsXeDangBaoDuong3.DataSource = tableResult;
            }
            catch
            {
                MessageBox.Show("Không tồn tại xe bảo dưỡng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ReloadData();
            }
        }

        private void txtID3_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtID3.Text))
            {
                btnThemTho.Enabled = true;
                cboMaTho.Enabled = true;
                cboMaThoDuyet.Enabled = true;
            }
            else
            {
                btnThemTho.Enabled = false;
                cboMaTho.Enabled = true;
                cboMaThoDuyet.Enabled = false;
            }
        }

        private void dgvDsXeDangBaoDuong3_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                FrmLapBaoGiaSuaChua frmLapBaoGia = new FrmLapBaoGiaSuaChua();
                frmLapBaoGia.IdBaoDuong = Convert.ToInt64(dgvDsXeDangBaoDuong3.Rows[e.RowIndex].Cells["IdBaoDuong"].Value);

                frmLapBaoGia.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DtgBaoDuongTheoTien_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                ComboBox comboBox = e.Control as ComboBox;
                if (comboBox != null)
                {
                    comboBox.SelectedValueChanged += comboBox_SelectedValueChanged;
                }
                return;
            }
            catch { }
        }

        private void comboBox_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                ComboBox comboBox = sender as ComboBox;

                if (comboBox != null && comboBox.SelectedValue != null)
                {
                    var result = from myRow in ((DataTable)DtgBaoDuongTheoTien.DataSource).AsEnumerable()
                                 where myRow.Field<string>("MaTho") == comboBox.SelectedValue.ToString()
                                 && myRow.Field<int>("IdTienCong") == Convert.ToInt32(DtgBaoDuongTheoTien.SelectedRows[0].Cells["IDTienCong6"].Value.ToString())
                                 select myRow;

                    if(result.Count() > 0)
                    {
                        MessageBox.Show("Bạn đã nhận Công việc này cho Thợ này!\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        comboBox.SelectedIndex = -1;
                        return;
                    }

                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = @"SELECT IdTho, MaTho, tenTho, IdCongTy
                                       FROM ThoDichVu WHERE IdCongTy = @IdCongTy AND MaTho = @MaTho";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@MaTho", comboBox.SelectedValue);

                    DataTable tableTho = Class.datatabase.getData(cmd);

                    cmd.CommandText = "UPDATE ThoDichVu_TienCongThoTam SET IdTho = @IdTho, MaTho=@MaTho, TenTho = @TenTho WHERE Id=@Id";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdTho", tableTho.Rows[0]["IdTho"].ToString());
                    cmd.Parameters.AddWithValue("@MaTho", tableTho.Rows[0]["MaTho"].ToString());
                    cmd.Parameters.AddWithValue("@TenTho", tableTho.Rows[0]["tenTho"].ToString());
                    cmd.Parameters.AddWithValue("@Id", DtgBaoDuongTheoTien.SelectedRows[0].Cells[0].Value.ToString());

                    Class.datatabase.ExcuteNonQuery(cmd);

                    cmd.CommandText = "select * from ThoDichVu_TienCongThoTam where IdCongTy=@IdCongTy and IdBaoDuong=@IdBaoDuong";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdBaoDuong", txtID3.Text);

                    DtgBaoDuongTheoTien.DataSource = Class.datatabase.getData(cmd);
                }
            }
            catch (Exception ex) { MessageBox.Show("" + ex.Message); }
        }

        private void dgvDsXeDangBaoDuong3_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                dgvDsXeDangBaoDuong3.ContextMenuStrip = contextMenuStrip;

                try
                {
                    _IdBaoDuong = Convert.ToInt64(dgvDsXeDangBaoDuong3.Rows[e.RowIndex].Cells["IdBaoDuong"].Value);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _IdBaoDuong = 0;
                }
            }
            else
            {
                dgvDsXeDangBaoDuong3.ContextMenuStrip = null;
                _IdBaoDuong = 0;
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

        private void DtgBaoDuongTheoTien_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                DtgBaoDuongTheoTien.ContextMenuStrip = contextMenuStripCongViec;

                try
                {
                    _IdCongViecTienCong = Convert.ToInt64(DtgBaoDuongTheoTien.Rows[e.RowIndex].Cells["Id"].Value);

                    congviec = from row in ((DataTable)DtgBaoDuongTheoTien.DataSource).AsEnumerable()
                               where row.Field<int>("IdTienCong") == Convert.ToInt32(DtgBaoDuongTheoTien.Rows[e.RowIndex].Cells["IDTienCong6"].Value)
                               select row;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    _IdCongViecTienCong = 0;
                }
            }
            else
            {
                DtgBaoDuongTheoTien.ContextMenuStrip = null;
            }
        }

        private void ToolStripMenuItemCopyCongViec_Click(object sender, EventArgs e)
        {
            if (congviec.Count() > 0 && _IdCongViecTienCong != 0)
            {
                //Copy công việc (chia tiền công cho các thợ làm cùng công việc)
                decimal tiencong = congviec.Sum(m => m.Field<decimal>("TienCong"));

                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = @"UPDATE ThoDichVu_TienCongThoTam
                                    SET TienCong = @TienCong WHERE IdTienCong = @IdTienCong AND IdBaoDuong = @IdBaoDuong";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@TienCong", tiencong / (congviec.Count() + 1));
                cmd.Parameters.AddWithValue("@IdTienCong", congviec.FirstOrDefault().Field<int>("IdTienCong"));
                cmd.Parameters.AddWithValue("@IdBaoDuong", txtID3.Text);

                Class.datatabase.ExcuteNonQuery(cmd);

                cmd.CommandText = @"INSERT INTO ThoDichVu_TienCongThoTam
                                            (IdTienCong, NgaySuaChua, IdCongTy, IdBaoDuong, NoiDungBD, TienCong, TienKhachTra)
                                            VALUES (@IdTienCong,@NgaySuaChua,@IdCongTy,@IdBaoDuong,@NoiDungBD,@TienCong,@TienKhachTra)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdTienCong", congviec.FirstOrDefault().Field<int>("IdTienCong"));
                cmd.Parameters.AddWithValue("@NgaySuaChua", congviec.FirstOrDefault().Field<DateTime>("NgaySuaChua"));
                cmd.Parameters.AddWithValue("@IdCongTy", congviec.FirstOrDefault().Field<int>("IdCongTy"));
                cmd.Parameters.AddWithValue("@IdBaoDuong", congviec.FirstOrDefault().Field<int>("IdBaoDuong"));
                cmd.Parameters.AddWithValue("@NoiDungBD", congviec.FirstOrDefault().Field<string>("NoiDungBD"));
                cmd.Parameters.AddWithValue("@TienCong", tiencong / (congviec.Count() + 1));
                cmd.Parameters.AddWithValue("@TienKhachTra", 0);

                Class.datatabase.ExcuteNonQuery(cmd);

                cmd.CommandText = "select * from ThoDichVu_TienCongThoTam where IdCongTy=@IdCongTy and IdBaoDuong=@IdBaoDuong";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IdBaoDuong", txtID3.Text);

                DtgBaoDuongTheoTien.DataSource = Class.datatabase.getData(cmd);
            }
        }
    }
}