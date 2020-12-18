using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class FrmChiTietCongTho : Form
    {       
        #region Delegate
        public delegate void LoadCongTho();

        public LoadCongTho LoadDanhSachCongTho;
        public LoadCongTho CallFromUcBaoDuong;
        #endregion

        #region Variable

        private readonly string _cn = Class.datatabase.connect;
        private SqlConnection _con;
        private SqlCommand _com;

        private int _soPhut;
        public string IdBaoDuong;
        public bool SuaLichSuBaoDuong;
        public bool Check;

        private readonly SqlDataProvider _sqlPrv = new SqlDataProvider();

        #endregion Variable

        public FrmChiTietCongTho()
        {
            InitializeComponent();
        }

        private void Connect()
        {
            try
            {
                _con = new SqlConnection(_cn);
                _con.Open();
            }
            catch
            {
                MessageBox.Show(@"Lỗi kết nối !");
            }
        }

        private DataTable _dt = new DataTable();

        private void FrmChiTietCongTho_Load(object sender, EventArgs e)
        {
            try
            {
                txt_MaTho.Text = Class.ThongTinTho.matho;
                txt_TenTho.Text = Class.ThongTinTho.tentho;

                txtTiencong.Text = @"0";
                txt_SuaNgoai_TienThue.Text = @"0";
                txt_SuaNgoai_TienBan.Text = @"0";

                LoadDataCongViec(Class.CompanyInfo.idcongty);
                AutoCongViecPhut();
                Load_CongViec();
            }
            catch (Exception)
            { 
                // 
            }
        }

        private void LoadDataCongViec(string idCongTy)
        {
            _com = new SqlCommand
            {
                //CommandText = @"SELECT	IdTienCong,
                //                  'NoiDungBD' = CASE	WHEN MaBD is not null and MaBD <> '' THEN MaBD + '-' + NoiDungBD
                //                       ELSE NoiDungBD END, TienCong 
                //                FROM TienCongTho WHERE idcongty=@idcongty"
                CommandText = @"SELECT	IdTienCong,
		                                'NoiDungBD' = CASE	WHEN MaBD is not null and MaBD <> '' THEN MaBD + '-' + NoiDungBD
							                                ELSE NoiDungBD END 
                                FROM TienCongTho WHERE idcongty=@idcongty"
            };
            _com.Parameters.AddWithValue("@idcongty", idCongTy);
            _dt = Class.datatabase.getData(_com);
            cbbCongViec.DataSource = _dt;

            if (_dt.Rows.Count > 0)
            {
                cbbCongViec.ValueMember = "IdTienCong";
                cbbCongViec.DisplayMember = "NoiDungBD";
            }

            cbbCongViec.SelectedIndex = 0;
        }

        private void Load_CongViec()
        {
            Connect();
            SqlCommand cmd = new SqlCommand("SELECT * FROM GioViec WHERE IdCongTy=" + Class.CompanyInfo.idcongty, _con);
            SqlDataAdapter daMaTho = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            daMaTho.Fill(ds, "CongViec");

            cbb_CongViec.DataSource = ds.Tables[0];
            cbb_CongViec.DisplayMember = "PhuLuc";
            cbb_CongViec.ValueMember = "IdGioViec";
        }

        private void AutoCongViecPhut()
        {
            AutoCompleteStringCollection auto1 = new AutoCompleteStringCollection();
            _con = new SqlConnection(_cn);
            SqlCommand cmd = new SqlCommand {Connection = _con, CommandType = CommandType.Text};

            string sql = "SELECT * FROM GioViec WHERE IdCongTy=" + Class.CompanyInfo.idcongty;
            SqlCommand com = new SqlCommand {Connection = _con, CommandText = sql, CommandType = CommandType.Text};

            _con.Open();
            var reader = com.ExecuteReader();
            while (reader.Read())
            {
                auto1.Add(reader["PhuLuc"].ToString());
            }

            cbb_CongViec.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cbb_CongViec.AutoCompleteSource = AutoCompleteSource.CustomSource;
            cbb_CongViec.AutoCompleteCustomSource = auto1;
        }

        #region AutoCongViecTheoTien
        //private void AutoCongViecTien()
        //{
        //    AutoCompleteStringCollection auto2 = new AutoCompleteStringCollection();
        //    con = new SqlConnection(cn);
        //    SqlCommand cmd = new SqlCommand();
        //    cmd.Connection = con;
        //    cmd.CommandType = CommandType.Text;

        //    string sql = "select MaBD, NoiDungBD from TienCongTho where IdCongTy=" + Class.CompanyInfo.idcongty;
        //    SqlCommand com = new SqlCommand();

        //    com.Connection = con;
        //    com.CommandText = sql;
        //    com.CommandType = CommandType.Text;

        //    con.Open();
        //    SqlDataReader reader;
        //    reader = com.ExecuteReader();
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            auto2.Add(reader["MaBD"].ToString() + "-" + reader["NoiDungBD"].ToString());
        //        }

        //        cbb_TienCongViec.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        //        cbb_TienCongViec.AutoCompleteSource = AutoCompleteSource.CustomSource;
        //        cbb_TienCongViec.AutoCompleteCustomSource = auto2;
        //    }
        //}
        #endregion

        private void btn_NhanViec_Click(object sender, EventArgs e)
        {
            if (SuaLichSuBaoDuong == false)
            {
                //ĐứcAnh:Thêm việc thợ theo số phút vào bảng tạm
                try
                {
                    SqlCommand cmd = new SqlCommand("insert ThoDichVu_GioViecTam values(@IdTho,@IdGioViec,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong,@CongViec,@SoPhut,@MaTho,@TenTho)");
                    cmd.Parameters.AddWithValue("@IdTho", Class.ThongTinTho.idtho);
                    cmd.Parameters.AddWithValue("@IdGioViec", cbb_CongViec.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@NgaySuaChua", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@GhiChu", txt_GhiChu.Text);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                    cmd.Parameters.AddWithValue("@CongViec", cbb_CongViec.Text);
                    cmd.Parameters.AddWithValue("@SoPhut", _soPhut);
                    cmd.Parameters.AddWithValue("@MaTho", txt_MaTho.Text);
                    cmd.Parameters.AddWithValue("@TenTho", txt_TenTho.Text);
                    _sqlPrv.ExecuteNonQuery(cmd);

                    MessageBox.Show(@"Thêm việc cho thợ " + txt_TenTho.Text + @" theo thời gian thành công");

                    if(CallFromUcBaoDuong != null)
                    {
                        CallFromUcBaoDuong();
                    }
                    else
                    {
                        DataTable dtViecTheopPhut = _sqlPrv.GetData("select * from ThoDichVu_GioViecTam where IdCongTy=" + Class.CompanyInfo.idcongty + " and IdBaoDuong=" + IdBaoDuong);
                        ((DataGridView)this.Owner.Controls.Find("DtgBaoDuongTheoGio", true).First()).DataSource = dtViecTheopPhut.DefaultView;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Lỗi khi thêm việc cho thợ " + ex.Message);
                }
            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = @"insert ThoDichVu_GioViec values(@IdTho,@IdGioViec,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong)";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdTho", Class.ThongTinTho.idtho);
                    cmd.Parameters.AddWithValue("@IdGioViec", cbb_CongViec.SelectedValue.ToString());
                    cmd.Parameters.AddWithValue("@NgaySuaChua", DateTime.Now.ToString("yyyy-MM-dd"));
                    cmd.Parameters.AddWithValue("@GhiChu", txt_GhiChu.Text);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                    Class.datatabase.ExcuteNonQuery(cmd);

                    MessageBox.Show("Thêm việc cho thợ " + txt_TenTho.Text + " theo thời gian thành công");
                    if (LoadDanhSachCongTho != null)
                    {
                        LoadDanhSachCongTho();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Lỗi khi thêm việc cho thợ!" + ex.Message);
                }
            }
        }

        private void btn_NhanViecTien_Click(object sender, EventArgs e)
        {
            if (SuaLichSuBaoDuong == false)
            {
                //ĐứcAnh:Thêm việc thợ theo tiền vào bảng tạm
                try
                {
                    string idTienCong = GetIdTienCong(Class.CompanyInfo.idcongty,
                        cbbCongViec.SelectedValue != null ? cbbCongViec.SelectedValue.ToString() : "",
                        cbbCongViec.Text, decimal.Parse(txtTiencong.Text));

                    SqlCommand cm = new SqlCommand(@"SELECT * FROM ThoDichVu_TienCongThoTam WHERE IdCongTy=@IdCongTy AND IdBaoDuong=@IdBaoDuong
                                               AND IdTienCong=@IdTienCong ORDER BY TienKhachTra DESC");
                    cm.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cm.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                    cm.Parameters.AddWithValue("@IdTienCong", idTienCong);

                    var dt = Class.datatabase.getData(cm);

                    SqlCommand cm1 = new SqlCommand(@"SELECT * FROM ThoDichVu_TienCongThoTam WHERE IdCongTy=@IdCongTy AND IdBaoDuong=@IdBaoDuong
                                               AND IdTienCong=@IdTienCong AND MaTho=@MaTho");
                    cm1.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cm1.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                    cm1.Parameters.AddWithValue("@IdTienCong", idTienCong);
                    cm1.Parameters.AddWithValue("@MaTho", txt_MaTho.Text);

                    var dt1 = Class.datatabase.getData(cm1);

                    if (dt1.Rows.Count > 0)
                    {
                        MessageBox.Show(@"Bạn đã nhận công việc này, cho thợ này! \nVui lòng kiểm tra lại!", @"Thông báo!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //Chia tiền công cho thợ (các thợ làm cùng một công việc)
                    if (dt.Rows.Count > 0)
                    {
                        int i = dt.Rows.Count + 1;

                        SqlCommand cmd1 = new SqlCommand(@"UPDATE ThoDichVu_TienCongThoTam SET TienCong=@TienCong WHERE IdCongTy=@IdCongTy
                                                       AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong");
                        cmd1.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd1.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                        cmd1.Parameters.AddWithValue("@IdTienCong", idTienCong);
                        cmd1.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(decimal.Parse(txtTiencong.Text) / i));
                        _sqlPrv.ExecuteNonQuery(cmd1);

                        SqlCommand cmd2 = new SqlCommand(@"UPDATE ThoDichVu_TienCongThoTam SET TienKhachTra=@TienKhachTra WHERE IdCongTy=@IdCongTy
                                                       AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong AND MaTho=@MaTho");
                        cmd2.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd2.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                        cmd2.Parameters.AddWithValue("@IdTienCong", idTienCong);
                        cmd2.Parameters.AddWithValue("@MaTho", dt.Rows[0]["MaTho"].ToString());
                        cmd2.Parameters.AddWithValue("@TienKhachTra", decimal.Parse(txt_TienKhachTra_SoTien.Text.Replace(",", "")));
                        _sqlPrv.ExecuteNonQuery(cmd2);

                        SqlCommand cmd = new SqlCommand(@"insert into ThoDichVu_TienCongThoTam (IdTho,IdTienCong,NgaySuaChua,GhiChu,IdCongTy,IdBaoDuong,NoiDungBD,TienCong,MaTho,TenTho,TienKhachTra) 
                                                        values(@IdTho,@IdTienCong,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong,@NoiDungBD,@TienCong,@MaTho,@TenTho,@TienKhachTra)");
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdTho", Class.ThongTinTho.idtho);
                        cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);
                        cmd.Parameters.AddWithValue("@NgaySuaChua", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@GhiChu", txt_GhiChuTien.Text);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                        cmd.Parameters.AddWithValue("@NoiDungBD", cbbCongViec.Text);
                        cmd.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(decimal.Parse(txtTiencong.Text) / i));
                        cmd.Parameters.AddWithValue("@MaTho", txt_MaTho.Text);
                        cmd.Parameters.AddWithValue("@TenTho", txt_TenTho.Text);
                        cmd.Parameters.AddWithValue("@TienKhachTra", 0);

                        _sqlPrv.ExecuteNonQuery(cmd);
                    }
                    else
                    {
                        SqlCommand cmd = new SqlCommand(@"insert into ThoDichVu_TienCongThoTam (IdTho,IdTienCong,NgaySuaChua,GhiChu,IdCongTy,IdBaoDuong,NoiDungBD,TienCong,MaTho,TenTho,TienKhachTra) 
                                                        values(@IdTho,@IdTienCong,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong,@NoiDungBD,@TienCong,@MaTho,@TenTho,@TienKhachTra)");
                        cm.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdTho", Class.ThongTinTho.idtho);
                        cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);
                        cmd.Parameters.AddWithValue("@NgaySuaChua", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@GhiChu", txt_GhiChuTien.Text);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                        cmd.Parameters.AddWithValue("@NoiDungBD", cbbCongViec.Text);
                        //cmd.Parameters.AddWithValue("@TienCong", txtTiencong.Text);
                        cmd.Parameters.AddWithValue("@TienCong", decimal.Parse(txtTiencong.Text.Replace(",", "")));
                        cmd.Parameters.AddWithValue("@MaTho", txt_MaTho.Text);
                        cmd.Parameters.AddWithValue("@TenTho", txt_TenTho.Text);
                        cmd.Parameters.AddWithValue("@TienKhachTra", decimal.Parse(txt_TienKhachTra_SoTien.Text.Replace(",", "")));

                        _sqlPrv.ExecuteNonQuery(cmd);
                    }

                    MessageBox.Show(@"Thêm việc cho thợ " + txt_TenTho.Text + @" theo thời số tiền công");

                    if (CallFromUcBaoDuong != null)
                    {
                        CallFromUcBaoDuong();
                    }
                    else
                    {
                        DataTable dtViecTheoTien = _sqlPrv.GetData("select * from ThoDichVu_TienCongThoTam where IdCongTy=" + Class.CompanyInfo.idcongty + " and IdBaoDuong=" + IdBaoDuong);
                        ((DataGridView)this.Owner.Controls.Find("DtgBaoDuongTheoTien", true).First()).DataSource = dtViecTheoTien.DefaultView;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Lỗi khi thêm việc cho thợ " + ex.Message);
                }
            }
            else
            {
                try
                {
                    SqlCommand cmd = new SqlCommand();

                    string idTienCong = GetIdTienCong(Class.CompanyInfo.idcongty,
                        cbbCongViec.SelectedValue != null ? cbbCongViec.SelectedValue.ToString() : "",
                        cbbCongViec.Text, decimal.Parse(txtTiencong.Text));

                    cmd.CommandText = @"SELECT * FROM ThoDichVu_TienCongTho2 WHERE IdCongTy=@IdCongTy AND IdBaoDuong=@IdBaoDuong
                                        AND IdTienCong=@IdTienCong ORDER BY TienKhachTra DESC";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                    cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);

                    var dataCongTho = Class.datatabase.getData(cmd);

                    //Lấy công việc của thợ
                    cmd.CommandText = @"SELECT * FROM ThoDichVu_TienCongTho2 WHERE IdCongTy=@IdCongTy AND IdBaoDuong=@IdBaoDuong
                                        AND IdTienCong=@IdTienCong AND IdTho=@IdTho";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                    cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);
                    cmd.Parameters.AddWithValue("@IdTho", Class.ThongTinTho.idtho);

                    var dataCongViecCuaTho = Class.datatabase.getData(cmd);

                    if (dataCongViecCuaTho.Rows.Count > 0)
                    {
                        MessageBox.Show(@"Bạn đã nhận công việc này, cho thợ này! \nVui lòng kiểm tra lại!", @"Thông báo!");
                        return;
                    }

                    //Chia tiền công cho thợ (các thợ làm cùng một công việc)
                    if (dataCongTho.Rows.Count > 0)
                    {
                        int i = dataCongTho.Rows.Count + 1;

                        cmd.CommandText = @"UPDATE ThoDichVu_TienCongTho2 SET TienCong=@TienCong WHERE IdCongTy=@IdCongTy
                                            AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                        cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);
                        cmd.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(decimal.Parse(txtTiencong.Text) / i));
                        Class.datatabase.ExcuteNonQuery(cmd);

                        cmd.CommandText = @"UPDATE ThoDichVu_TienCongTho2 SET TienKhachTra=@TienKhachTra WHERE IdCongTy=@IdCongTy
                                            AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong AND IdTho=@IdTho";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                        cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);
                        cmd.Parameters.AddWithValue("@IdTho", Class.ThongTinTho.idtho);
                        cmd.Parameters.AddWithValue("@TienKhachTra", decimal.Parse(txt_TienKhachTra_SoTien.Text.Replace(",", "")));
                        Class.datatabase.ExcuteNonQuery(cmd);

                        cmd.CommandText = @"insert into ThoDichVu_TienCongTho2 (IdTho,IdTienCong,NgaySuaChua,GhiChu,IdCongTy,IdBaoDuong,TienCong,TienKhachTra) 
                                            values(@IdTho,@IdTienCong,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong,@TienCong,@TienKhachTra)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdTho", Class.ThongTinTho.idtho);
                        cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);
                        cmd.Parameters.AddWithValue("@NgaySuaChua", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@GhiChu", txt_GhiChuTien.Text);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                        cmd.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(decimal.Parse(txtTiencong.Text) / i));
                        cmd.Parameters.AddWithValue("@TienKhachTra", 0);
                        Class.datatabase.ExcuteNonQuery(cmd);
                    }
                    else
                    {
                        cmd.CommandText = @"insert into ThoDichVu_TienCongTho2 (IdTho,IdTienCong,NgaySuaChua,GhiChu,IdCongTy,IdBaoDuong,TienCong,TienKhachTra) 
                                            values(@IdTho,@IdTienCong,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong,@TienCong,@TienKhachTra)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdTho", Class.ThongTinTho.idtho);
                        cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);
                        cmd.Parameters.AddWithValue("@NgaySuaChua", DateTime.Now.ToString("yyyy-MM-dd"));
                        cmd.Parameters.AddWithValue("@GhiChu", txt_GhiChuTien.Text);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                        //cmd.Parameters.AddWithValue("@TienCong", txtTiencong.Text);
                        cmd.Parameters.AddWithValue("@TienCong", decimal.Parse(txtTiencong.Text.Replace(",", "")));
                        cmd.Parameters.AddWithValue("@TienKhachTra", decimal.Parse(txt_TienKhachTra_SoTien.Text.Replace(",", "")));
                        Class.datatabase.ExcuteNonQuery(cmd);
                    }

                    MessageBox.Show(@"Nhận công việc thành công!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if(LoadDanhSachCongTho != null)
                    {                        
                        LoadDanhSachCongTho();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Lỗi khi thêm việc cho thợ!" + ex.Message);
                }
            }
        }

        /// <summary>
        /// Gets the identifier tien cong.
        /// </summary>
        /// <param name="idCongTy">The identifier cong ty.</param>
        /// <param name="idTienCong">The identifier tien cong.</param>
        /// <param name="tenCongViec">The ten cong viec.</param>
        /// <param name="tienCong">The tien cong.</param>
        /// <returns></returns>
        private string GetIdTienCong(string idCongTy, string idTienCong, string tenCongViec, decimal tienCong)
        {
            string result;

            if (!String.IsNullOrEmpty(idTienCong))
                return idTienCong;

            using (SqlCommand cmdGetCv = new SqlCommand())
            {
                cmdGetCv.CommandType = CommandType.StoredProcedure;
                cmdGetCv.CommandText = "sp_KiemTraCongViec";
                cmdGetCv.Parameters.Clear();
                cmdGetCv.Parameters.AddWithValue("@IdCongTy", idCongTy);
                cmdGetCv.Parameters.AddWithValue("@NoiDungCV", tenCongViec);
                cmdGetCv.Parameters.AddWithValue("@TienCong", tienCong);

                result = Class.datatabase.ExecuteScalar(cmdGetCv);
            }

            return result;
        }

        private void btn_SuaNgoai_Them_Click(object sender, EventArgs e)
        {
            if (SuaLichSuBaoDuong == false)
            {
                try
                {
                    if (txt_SuaNgoai_TienBan.Text.Trim() == "")
                    {
                        MessageBox.Show(@"Tiền khách trả không được để trống", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    SqlCommand cmd = new SqlCommand("insert ThueNgoaiTam(CongViec,TienThueNgoai,TienLayCuaKH,TienLai,GhiChu,IdCongTy,IdTho,IdBaoDuong,NgaySuaChua,MaTho,TenTho) values(@CongViec,@TienThueNgoai,@TienLayCuaKH,@TienLai,@GhiChu,@IdCongTy,@IdTho,@IdBaoDuong,@NgaySuaChua,@MaTho,@TenTho)");
                    cmd.Parameters.AddWithValue("@CongViec", txt_SuaNgoai_CV.Text);
                    cmd.Parameters.AddWithValue("@TienThueNgoai", decimal.Parse(txt_SuaNgoai_TienBan.Text));
                    cmd.Parameters.AddWithValue("@TienLayCuaKH", decimal.Parse(txt_SuaNgoai_TienThue.Text));
                    cmd.Parameters.AddWithValue("@TienLai", Convert.ToDecimal(double.Parse(txt_SuaNgoai_TienLai.Text)));
                    cmd.Parameters.AddWithValue("@GhiChu", txt_SuaNgoai_GhiChu.Text);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdTho", Convert.ToInt32(Class.ThongTinTho.idtho));
                    cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                    cmd.Parameters.AddWithValue("@NgaySuaChua", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                    cmd.Parameters.AddWithValue("@MaTho", Class.ThongTinTho.matho);
                    cmd.Parameters.AddWithValue("@TenTho", Class.ThongTinTho.tentho);
                    _sqlPrv.ExecuteNonQuery(cmd);

                    MessageBox.Show(@"Thêm việc thuê ngoài thành công", @"Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    if (CallFromUcBaoDuong != null)
                    {
                        CallFromUcBaoDuong();
                    }
                    else
                    {
                        DataTable dtViecThueNgoai = _sqlPrv.GetData("select * from ThueNgoaiTam where IdCongTy=" + Class.CompanyInfo.idcongty + " and IdBaoDuong=" + IdBaoDuong);
                        ((DataGridView)this.Owner.Controls.Find("DtgSuaChuaNgoai", true).First()).DataSource = dtViecThueNgoai.DefaultView;
                    }
                }
                catch (Exception ex) { MessageBox.Show(@"Thêm tiền thuê ngoài thất bại:" + ex.Message, @"Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
            else
            {
                try
                {
                    if (txt_SuaNgoai_TienBan.Text.Trim() == "")
                    {
                        MessageBox.Show(@"Tiền khách trả không được để trống", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    double tienlai = double.Parse(txt_SuaNgoai_TienThue.Text.Trim()) - double.Parse(txt_SuaNgoai_TienBan.Text.Trim());

                    //DoNT UPDATE 2018/05/09 Start
                    //SqlCommand cmd = new SqlCommand("insert ThueNgoai values(@CongViec,@TienThueNgoai,@TienLayCuaKH,@TienLai,@GhiChu,@IdCongTy,@IdTho,@IdBaoDuong,@NgaySuaChua)");
                    SqlCommand cmd = new SqlCommand("insert ThueNgoai (CongViec,TienThueNgoai,TienLayCuaKH,TienLai,GhiChu,IdCongTy,IdTho,IdBaoDuong,NgaySuaChua) values(@CongViec,@TienThueNgoai,@TienLayCuaKH,@TienLai,@GhiChu,@IdCongTy,@IdTho,@IdBaoDuong,@NgaySuaChua)");
                    //DoNT UPDATE 2018/05/09 End

                    cmd.Parameters.AddWithValue("@CongViec", txt_SuaNgoai_CV.Text);
                    cmd.Parameters.AddWithValue("@TienThueNgoai", decimal.Parse(txt_SuaNgoai_TienBan.Text));
                    cmd.Parameters.AddWithValue("@TienLayCuaKH", decimal.Parse(txt_SuaNgoai_TienThue.Text));
                    //cmd.Parameters.AddWithValue("@TienLai", Convert.ToDecimal(double.Parse(txt_SuaNgoai_TienLai.Text) / 2));
                    cmd.Parameters.AddWithValue("@TienLai", Convert.ToDecimal(double.Parse(txt_SuaNgoai_TienLai.Text)));
                    cmd.Parameters.AddWithValue("@GhiChu", txt_SuaNgoai_GhiChu.Text);
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdTho", Convert.ToInt32(Class.ThongTinTho.idtho));
                    cmd.Parameters.AddWithValue("@IdBaoDuong", IdBaoDuong);
                    cmd.Parameters.AddWithValue("@NgaySuaChua", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss.fff"));
                    _sqlPrv.ExecuteNonQuery(cmd);

                    MessageBox.Show(@"Thêm việc thuê ngoài thành công", @"Thông báo", MessageBoxButtons.OK,
                        MessageBoxIcon.Information); 
                    if (LoadDanhSachCongTho != null)
                    {
                        LoadDanhSachCongTho();
                    }
                }
                catch (Exception ex) { MessageBox.Show(@"Thêm tiền thuê ngoài thất bại:" + ex.Message, @"Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            }
        }

        private void btn_SuaNgoai_TaoMoi_Click(object sender, EventArgs e)
        {
            txt_SuaNgoai_CV.Text = "";
            txt_SuaNgoai_GhiChu.Text = "";
            txt_SuaNgoai_TienBan.Text = "";
            txt_SuaNgoai_TienLai.Text = "";
            txt_SuaNgoai_TienThue.Text = "";
        }

        private void FrmChiTietCongTho_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void txt_TienKhachTra_SoTien_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_TienKhachTra_SoTien_TextChanged(object sender, EventArgs e)
        {
            if (txt_TienKhachTra_SoTien.Text.Length > 0)
            {
                try
                {
                    string text = txt_TienKhachTra_SoTien.Text;
                    txt_TienKhachTra_SoTien.Text = String.Format("{0:N0}", decimal.Parse(text));
                    txt_TienKhachTra_SoTien.SelectionStart = txt_TienKhachTra_SoTien.Text.Length;

                    if (txt_SuaNgoai_TienBan.Text.Length > 0)
                    {
                        decimal tiencong = decimal.Parse(txt_TienKhachTra_SoTien.Text) * 45 / 100;

                        if (tiencong > 0)
                            txtTiencong.Text = String.Format("{0:N0}", tiencong);
                        else
                            txtTiencong.Text = String.Format("{0:N0}", 0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Dữ liệu nhập không đúng!\nVui lòng kiểm tra lại.", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
        }

        private void txt_SuaNgoai_TienThue_TextChanged(object sender, EventArgs e)
        {
            if (txt_SuaNgoai_TienThue.Text.Length > 0)
            {
                string text = txt_SuaNgoai_TienThue.Text;
                txt_SuaNgoai_TienThue.Text = String.Format("{0:N0}", decimal.Parse(text));
                txt_SuaNgoai_TienThue.SelectionStart = txt_SuaNgoai_TienThue.Text.Length;

                if (txt_SuaNgoai_TienBan.Text.Length > 0)
                {
                    decimal tienlai = decimal.Parse(txt_SuaNgoai_TienThue.Text) - decimal.Parse(txt_SuaNgoai_TienBan.Text);

                    if (tienlai > 0)
                        txt_SuaNgoai_TienLai.Text = String.Format("{0:N0}", tienlai);
                    else
                        txt_SuaNgoai_TienLai.Text = String.Format("{0:N0}", 0);
                }
            }
        }

        private void txt_SuaNgoai_TienThue_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txt_SuaNgoai_TienBan_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_SuaNgoai_TienThue.Text))
            {
                try
                { 
                    string text = txt_SuaNgoai_TienBan.Text;
                    txt_SuaNgoai_TienBan.Text = String.Format("{0:N0}", decimal.Parse(text));
                    txt_SuaNgoai_TienBan.SelectionStart = txt_SuaNgoai_TienBan.Text.Length;

                    if (txt_SuaNgoai_TienBan.Text.Length > 0)
                    {
                        decimal tienlai = decimal.Parse(txt_SuaNgoai_TienThue.Text) - decimal.Parse(txt_SuaNgoai_TienBan.Text);

                        if (tienlai > 0)
                            txt_SuaNgoai_TienLai.Text = String.Format("{0:N0}", tienlai);
                        else
                            txt_SuaNgoai_TienLai.Text = String.Format("{0:N0}", 0);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Dữ liệu nhập không đúng!\nVui lòng kiểm tra lại.", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
            }
            else
                txt_SuaNgoai_TienBan.Text = "0";
        }

        private void txt_SuaNgoai_TienBan_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cbb_CongViec_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (cbb_CongViec.SelectedIndex >= 0)
            {
                string sql = "SELECT * FROM GioViec WHERE IdGioViec=" + Convert.ToInt32(cbb_CongViec.SelectedValue.ToString());
                _com = new SqlCommand(sql, _con);
                SqlDataAdapter daGioViec = new SqlDataAdapter(_com);
                DataTable dtGioViec = new DataTable();
                daGioViec.Fill(dtGioViec);
                _soPhut = Convert.ToInt32(dtGioViec.Rows[0]["SoPhut"].ToString());
                txtSoPhut.Text = _soPhut.ToString();
            }
        }

        private void txtTiencong_TextChanged(object sender, EventArgs e)
        {
            if (txtTiencong.Text.Length > 0)
            {
                string text = txtTiencong.Text;
                txtTiencong.Text = String.Format("{0:N0}", decimal.Parse(text));
                txtTiencong.SelectionStart = txtTiencong.Text.Length;

                //decimal tienkhachtra = Convert.ToDecimal(txtTiencong.Text) * 2;
                //txt_TienKhachTra_SoTien.Text = tienkhachtra.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void cbbCongViec_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbCongViec.SelectedValue != null)
            {
                var data = (DataRowView)cbbCongViec.SelectedItem;

                txtTiencong.Text = String.Format("{0:N0}", 0);
                txt_TienKhachTra_SoTien.Text = String.Format("{0:N0}", 0);
                //txtTiencong.Text = data["TienCong"].ToString();

            }
        }
    }
}