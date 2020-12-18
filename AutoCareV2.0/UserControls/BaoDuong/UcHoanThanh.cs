using AutoCareV2._0.Class;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls.BaoDuong
{
    public partial class UcHoanThanh : UserControl
    {
        #region Variable

        private SqlDataProvider sqlPrv = new SqlDataProvider();
        private ChangeOilByKM ChangeOilKM = new ChangeOilByKM();
        private DataTable dtxeBaoDuong = new DataTable();
        private Class.KhDB classdb = new Class.KhDB();
        private DataTable dtKho = new DataTable();
        private string tenkh = "";
        private string dienthoai = "";
        private string solan = "";
        private string tenxe = "";
        private string ngaysinh = "";
        private string idk = "";
        private int i = 0;
        private int check = 0;
        private string tenxe1 = "";
        private string idbaoduong = "";
        private DataTable dtLichSuBaoDuongXeTam = new DataTable();
        private DataTable dtPhuTungThayThe = new DataTable();
        private DataTable dtChuanDoanXeTam = new DataTable();
        private DataTable dtThoDichVuGioViecTam = new DataTable();
        private DataTable dtThoDichVuTienCongTam = new DataTable();
        private DataTable dtThueNgoaiTam = new DataTable();
        private DataTable dtPhuTung2 = new DataTable();
        private decimal tienThueNgoai, tienPhuTung, tienCongTho;
        private string idbaoduongxe = "";
        private double chietkhau = 1;
        private DataTable tableBaoDuong = new DataTable();
        private DataTable tableBaoGiaTam = new DataTable();
        private DataTable tableBaoGiaCongThoTam = new DataTable();
        private DataTable tableBaoGiaPhuTungTam = new DataTable();
        //xong

        #endregion Variable

        #region Constructor

        public UcHoanThanh()
        {
            InitializeComponent();

            this.VerticalScroll.Visible = false;
            this.VerticalScroll.Enabled = false;
            this.HorizontalScroll.Visible = true;
            this.HorizontalScroll.Enabled = true;
        }

        #endregion Constructor

        #region Làm mới dữ liệu

        private void LoadXeDangBaoDuong()
        {
            LoadXeBaoDuong();
            LoadAutocompleteTextBox();
        }

        #endregion Làm mới dữ liệu

        #region load dữ liệu autocomplete lên textbox

        private void LoadAutocompleteTextBox()
        {
            AutoCompleteStringCollection BienSo = new AutoCompleteStringCollection();
            AutoCompleteStringCollection SoMay = new AutoCompleteStringCollection();
            AutoCompleteStringCollection SoKhung = new AutoCompleteStringCollection();

            tableBaoDuong = (DataTable)dgvDsXeDangBaoDuong1.DataSource;

            foreach (DataRow item in tableBaoDuong.Rows)
            {
                BienSo.Add(item["BienSo"].ToString());
                SoMay.Add(item["SoMay"].ToString());
                SoKhung.Add(item["Sokhung"].ToString());
            }

            txtBienSo1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBienSo1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBienSo1.AutoCompleteCustomSource = BienSo;

            txtSoKhung1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSoKhung1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSoKhung1.AutoCompleteCustomSource = SoKhung;

            txtSoMay1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSoMay1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSoMay1.AutoCompleteCustomSource = SoMay;
        }

        #endregion load dữ liệu autocomplete lên textbox

        #region Lấy danh sách xe bảo dưỡng

        private void LoadXeBaoDuong()
        {
            string sql = "select TenXe,IdBaoDuong,BienSo,Sokhung,SoMay,SoLan,TrangThai,CONVERT(bit,ThayDau) as ThayDau,ThayDauMay,(select tenTho from ThoDichVu where IdTho=l.IdThoDuyet) as ThoDuyet from LichSuBaoDuongXeTam l where IdCongty=@IdCongty and IdCuaHang=@IdCuaHang";
            SqlCommand cmd = new SqlCommand(sql);
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);

            dtxeBaoDuong = Class.datatabase.getData(cmd);
            dgvDsXeDangBaoDuong1.DataSource = dtxeBaoDuong;

            dgvDsXeDangBaoDuong1.Columns[0].HeaderText = "Tên xe";
            dgvDsXeDangBaoDuong1.Columns[1].Visible = false;
            dgvDsXeDangBaoDuong1.Columns[2].HeaderText = "Biển số";
            dgvDsXeDangBaoDuong1.Columns[3].HeaderText = "Số khung";
            dgvDsXeDangBaoDuong1.Columns[4].HeaderText = "Số máy";
            dgvDsXeDangBaoDuong1.Columns[5].HeaderText = "Lần bảo dưỡng";
            dgvDsXeDangBaoDuong1.Columns[6].HeaderText = "Trạng thái";
            dgvDsXeDangBaoDuong1.Columns[7].HeaderText = "Thay dầu";
            dgvDsXeDangBaoDuong1.Columns[8].HeaderText = "Thay dầu máy";
            dgvDsXeDangBaoDuong1.Columns[9].HeaderText = "Thợ duyệt";
        }

        #endregion Lấy danh sách xe bảo dưỡng

        #region Kiem tra khach hang den truoc ky bao duong, co muon nhan tin moi bao duong lan nay ko?

        private void Kiemtra() //----------- Kiem tra khach hang den truoc ky bao duong, co muon nhan tin moi bao duong lan nay ko?
        {
            try
            {
                string ngayban = "", ngaytruoc = "";
                string[] thangnhan = { "" };
                int ngayhientai = 0;
                int thangcannhan = 0;
                int daycheck = 0;
                SqlCommand cmd = new SqlCommand();
                //cmd.CommandText = "select * from XeDaBan where IdKhachHang=" + idk;
                //DataTable dt2 = new DataTable();
                //dt2 = Class.datatabase.getData(cmd);
                // cmd.CommandText = "select ThangNhan,NhanTruocSoNgay from SMSMaintenanceConfig where IdCongTy=" + Class.CompanyInfo.idcongty;
                cmd.CommandText = "select NgayBan,ThangNhan,NhanTruocSoNgay,DATEDIFF(month,ngayban,GETDATE()) as homnay from XeDaBan inner join SMSMaintenanceConfig on XeDaBan.IdCongty=SMSMaintenanceConfig.IdCongTy where XeDaBan.IdKhachHang=@idkhach and SMSMaintenanceConfig.IdCongTy=@idcongty";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@idkhach", idk);
                cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                DataTable dt = Class.datatabase.getData(cmd);
                if (dt.Rows.Count > 0)
                {
                    //lay du lieu
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ngayban = dt.Rows[0]["NgayBan"].ToString();
                        thangnhan = dt.Rows[0]["ThangNhan"].ToString().Split(',');
                        ngaytruoc = dt.Rows[0]["NhanTruocSoNgay"].ToString();
                        ngayhientai = int.Parse(dt.Rows[0]["homnay"].ToString());
                    }

                    //tim thang nhan
                    for (int j = 0; j <= thangnhan.Length; j++)
                    {
                        if (ngayhientai == int.Parse(thangnhan[j]))
                        {
                            thangcannhan = int.Parse(thangnhan[j]);
                            break;
                        }
                        else if (ngayhientai > int.Parse(thangnhan[j]) && ngayhientai < int.Parse(thangnhan[j + 1]))
                        {
                            thangcannhan = int.Parse(thangnhan[j + 1]);
                            break;
                        }
                    }
                    cmd.CommandText = "select DATEDIFF(day,ngayban,dateadd(Day,-" + ngaytruoc + ",dateadd(Month,-" + thangcannhan + ",getdate()))) as ngay from XeDaBan where IdKhachHang=@idkhach and IdCongTy=@idcongty";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idkhach", idk);
                    cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                    DataTable dt2 = new DataTable();
                    dt2 = Class.datatabase.getData(cmd);
                    if (dt2.Rows.Count > 0)
                    {
                        daycheck = int.Parse(dt2.Rows[0]["ngay"].ToString());
                    }
                    DialogResult but = DialogResult.No;
                    if (daycheck != 0 && Math.Abs(daycheck) > int.Parse(ngaytruoc))
                    {
                        but = MessageBox.Show("Khách hàng đến trước hạn bảo dưỡng định kỳ! Bạn có muốn bỏ qua lần nhắn tin nhắc nhớ không?", "Thông Báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    }
                    if (but == DialogResult.Yes)
                    {
                        cmd.CommandText = "insert into SMSBoQua (IdKhachHang,IdCongTy) values (@idkhach,@idct)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idkhach", idk);
                        cmd.Parameters.AddWithValue("@idct", Class.CompanyInfo.idcongty);
                        Class.datatabase.ExcuteNonQuery(cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion Kiem tra khach hang den truoc ky bao duong, co muon nhan tin moi bao duong lan nay ko?

        #region UcHoanThanh_Load

        //xong
        private void UcHoanThanh_Load(object sender, EventArgs e)
        {
            txtBienSo1.Select();

            try
            {
                LoadXeDangBaoDuong();
                dtKho = classdb.LoadTenKho();
                if (dtKho.Rows.Count > 0)
                {
                    this.Kho6.DataSource = dtKho;
                    this.Kho6.ValueMember = "IdKho";
                    this.Kho6.DisplayMember = "TenKho";
                }
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "Select MaPT, IDPT From PhuTung Where IdCongTy = @IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                DataTable dt = Class.datatabase.getData(cmd);
                MaPT.DataSource = dt;
                MaPT.DisplayMember = "MaPT";
                MaPT.ValueMember = "IdPT";
                DataTable dtThoSua = new DataTable();
                cmd.CommandText = "Select IDTho, MaTho + ' - ' + TenTho As TenTho From ThoDichVu Where IDCongTy = @IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                dtThoSua = Class.datatabase.getData(cmd);
                if (dtThoSua.Rows.Count > 0)
                {
                    Tho.DataSource = dtThoSua;
                    Tho.ValueMember = "IdTho";
                    Tho.DisplayMember = "TenTho";
                }
            }
            catch { }
        }

        #endregion UcHoanThanh_Load

        #region DoDuLieuRadgv

        //xong
        private void DoDuLieuRadgv()
        {
            tienThueNgoai = 0;
            tienCongTho = 0;

            foreach (DataRow row in Class.ThongTinTho.dtDSCongTho2.Rows)
            {
                dgvSoPhut.Rows.Add(Convert.ToString(row["IDTho"]), Convert.ToString(row["MaTho"]), Convert.ToString(row["TenTho"]), Convert.ToString(row["IDGioViec"]), Convert.ToString(row["CongViec"]), Convert.ToString(row["SoPhut"]), Convert.ToString(row["GhiChu"]));
            }

            foreach (DataRow row in Class.ThongTinTho.dtDSTienCongTho2.Rows)
            {
                dgvTheoSoTien.Rows.Add(Convert.ToString(row["IDTho"]), Convert.ToString(row["MaTho"]), Convert.ToString(row["TenTho"]), Convert.ToString(row["IDTienCong"]), Convert.ToString(row["NoiDungBD"]), string.Format("{0:0,0}", row["TienCong"]), Convert.ToString(row["GhiChu"]), string.Format("{0:N0}", Convert.ToDecimal(row["TienKhachTra"])));
                tienCongTho += Convert.ToDecimal(row["TienKhachTra"]);
            }

            foreach (DataRow row in Class.ThongTinTho.dtSuaNgoai_TienCongTho2.Rows)
            {
                dgvSuaNgoai.Rows.Add(Convert.ToString(row["IDTho"]), Convert.ToString(row["MaTho"]), Convert.ToString(row["TenTho"]), Convert.ToString(row["CongViec"]), string.Format("{0:0,0}", row["TienThueNgoai"]), string.Format("{0:0,0}", row["TienLayCuaKH"]), string.Format("{0:0,0}", row["TienLai"]), Convert.ToString(row["GhiChu"]));
                tienThueNgoai = Convert.ToDecimal(row["TienLayCuaKH"]);
            }
        }

        #endregion DoDuLieuRadgv

        #region dgvDsXeDangBaoDuong1_CellClick

        private void dgvDsXeDangBaoDuong1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                #region dodulieu

                txtgiamtru.Text = "0";
                txtBienSo1.Text = Convert.ToString(dgvDsXeDangBaoDuong1.Rows[e.RowIndex].Cells["BienSo"].Value);
                txtSoKhung1.Text = Convert.ToString(dgvDsXeDangBaoDuong1.Rows[e.RowIndex].Cells["SoKhung"].Value);
                txtSoMay1.Text = Convert.ToString(dgvDsXeDangBaoDuong1.Rows[e.RowIndex].Cells["SoMay"].Value);
                txtID1.Text = Convert.ToString(dgvDsXeDangBaoDuong1.Rows[e.RowIndex].Cells["IdBaoDuong"].Value);

                SqlCommand cmd = new SqlCommand("Select IdPhuTung,TenPhuTung,SoLuong,Gia,IdKho,IDTho from LichSuBaoDuongChiTiettam2 Where IdBaoDuong = @IdBaoDuong");
                cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);

                try
                {
                    DataTable dt = Class.datatabase.getData(cmd);
                    dgvDSPT.Rows.Clear();
                    tienPhuTung = 0;
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow r in dt.Rows)
                        {
                            tienPhuTung += Convert.ToDecimal(r["SoLuong"]) * Convert.ToDecimal(r["Gia"]);
                            dgvDSPT.Rows.Add(Convert.ToString(r["IdPhuTung"]), Convert.ToString(r["TenPhuTung"]), Convert.ToDecimal(r["Gia"]), Convert.ToString(r["SoLuong"]), Convert.ToInt32(r["SoLuong"]) * Convert.ToDecimal(r["Gia"]), Convert.ToString(r["IdKho"]), Convert.ToString(r["IdTho"]));
                        }
                    }
                }
                catch { }

                dgvSoPhut.Rows.Clear();
                dgvTheoSoTien.Rows.Clear();
                dgvSuaNgoai.Rows.Clear();

                cmd = new SqlCommand("Select * from ThoDichVu_GioViecTam Where IdCongTy = @idcongTy And IDBaoDuong = @IdBaoDuong");
                cmd.Parameters.AddWithValue("@idcongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                Class.ThongTinTho.dtDSCongTho2 = Class.datatabase.getData(cmd);

                cmd.CommandText = "select * from ThoDichVu_TienCongThoTam Where IdCongTy = @idcongTy And IDBaoDuong = @IdBaoDuong";
                Class.ThongTinTho.dtDSTienCongTho2 = Class.datatabase.getData(cmd);

                cmd.CommandText = "Select * from ThueNgoaiTam Where IdCongTy = @idcongTy And IDBaoDuong = @IdBaoDuong";
                Class.ThongTinTho.dtSuaNgoai_TienCongTho2 = Class.datatabase.getData(cmd);

                DoDuLieuRadgv();

                txtTienPhuTung.Text = string.Format("{0:0,0}", tienPhuTung);
                txtTienTho.Text = string.Format("{0:0,0}", tienCongTho);
                txtTienThueNgoai.Text = string.Format("{0:0,0}", tienThueNgoai);
                txtTongTien.Text = string.Format("{0:0,0}", tienPhuTung + tienThueNgoai + tienCongTho);

                #endregion dodulieu

                // lay du lieu bao duong
                cmd.CommandText = "select * from LichSuBaoDuongXeTam lsbdx , KhachHang kh where lsbdx.IdKhachHang=kh.IdKhachHang And lsbdx.IdCongTy=" + Class.CompanyInfo.idcongty + " and lsbdx.IdBaoDuong = " + txtID1.Text;
                DataTable infoid = Class.datatabase.getData(cmd);
                if (infoid.Rows.Count > 0)
                {
                    ngaysinh = infoid.Rows[0]["NgaySinh"].ToString();
                    tenkh = infoid.Rows[0]["TenKH"].ToString();
                    tenxe1 = infoid.Rows[0]["TenXe"].ToString();
                    dienthoai = infoid.Rows[0]["DienThoai"].ToString();
                    solan = infoid.Rows[0]["SoLan"].ToString();
                    idk = infoid.Rows[0]["IdKhachHang"].ToString();
                }
                //lay du lieu ngay thang
            }
        }

        #endregion dgvDsXeDangBaoDuong1_CellClick

        #region XuaTHoaDon

        //xong
        private static void XuaTHoaDon(string idBaoDuong)
        {
            try
            {
                decimal tongtien = 0m;
                string sql = "SELECT IdBaoDuong as ID, lsbdct.MaPT ,lsbdct.TenPhuTung,lsbdct.Soluong as SoLuong,lsbdct.Gia, lsbdct.GiaTien, pt.DVT"
                                    + " FROM LichSuBaoDuongChiTiet2 lsbdct"
                                    + " INNER JOIN PhuTung pt ON pt.IdPT = lsbdct.IdPhuTung and pt.IdKho=lsbdct.IdKho"
                                    + " WHERE IdCongTy = @IdCongTy AND lsbdct.IdBaoDuong=@IdBaoDuong";

                SqlCommand cmd = new SqlCommand(sql);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuong);
                DataTable dt = Class.datatabase.getData(cmd);
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        tongtien += Convert.ToDecimal(r["GiaTien"]);
                    }
                    Class.DocTien docsotien = new Class.DocTien();
                    string doctien = docsotien.ChuyenSo(tongtien.ToString());
                    DataTable dtdoctien = new DataTable();
                    DataColumn cl1 = new DataColumn("TienBangChu");
                    dtdoctien.Columns.Add(cl1);
                    DataRow row = dtdoctien.NewRow();
                    string tienbangchu = docsotien.ChuyenSo(tongtien.ToString());
                    string tien = tienbangchu.ToString().Substring(0, 1).ToUpper() + tienbangchu.ToString().Substring(1); ;
                    row["TienBangChu"] = tien + " đồng"; ;
                    dtdoctien.Rows.Add(row);
                    FrmPhieuXuatKho frm = new FrmPhieuXuatKho();
                    frm.reportViewer1.LocalReport.DataSources.Clear();
                    frm.reportViewer1.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.ReportPhieuXuatKho1.rdlc";
                    Microsoft.Reporting.WinForms.ReportDataSource dataset = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet1", dt);
                    Microsoft.Reporting.WinForms.ReportDataSource datasetdoctien = new Microsoft.Reporting.WinForms.ReportDataSource("DataSetdoctien", dtdoctien);
                    frm.reportViewer1.LocalReport.DataSources.Add(dataset);
                    frm.reportViewer1.LocalReport.DataSources.Add(datasetdoctien);
                    frm.ShowDialog();
                }
                else MessageBox.Show("Không có dữ liệu phụ tùng được xuất.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception Exception) { MessageBox.Show(Exception.Message); }
        }

        #endregion XuaTHoaDon

        #region GuiTinThayDauMay

        private void GuiTinThayDauMay()
        {
            #region nhantinthaydaumay

            try
            {
                string idcongty = Class.CompanyInfo.idcongty;
                DataTable tblThayDauConfig = new DataTable();
                SqlCommand cmo = new SqlCommand();
                cmo.CommandText = "Select top 1 * from SMSMoiThayDauMay_Config where idcongty=" + idcongty + " and active=1";
                tblThayDauConfig = Class.datatabase.getData(cmo);
                cmo.CommandText = "select sms from SMSConfig where idcongty=" + idcongty + " and Type='Thay dau may'";
                DataTable sms = new DataTable();
                sms = Class.datatabase.getData(cmo);
                if (tblThayDauConfig.Rows.Count > 0 && sms.Rows.Count > 0)
                {
                    string nhansausongay = tblThayDauConfig.Rows[0]["nhansausongay"].ToString();
                    string gionhan = tblThayDauConfig.Rows[0]["gionhan"].ToString();
                    SqlCommand cmd2 = new SqlCommand();
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.CommandText = "select GETDATE() as Time";
                    DataTable dttime = Class.datatabase.getData(cmd3);
                    DateTime dt2 = DateTime.Parse(dttime.Rows[0]["Time"].ToString());
                    DateTime d = dt2.AddDays(int.Parse(nhansausongay));
                    DateTime timechedule = new DateTime(d.Year, d.Month, d.Day, int.Parse(gionhan), 0, 0, 0);
                    string ngay = ngaysinh;
                    string resms = Utilities.Smsreplace(sms.Rows[0]["SMS"].ToString(), tenkh, ngay, Class.CompanyInfo.sendername, tenxe1, txtBienSo1.Text, txtSoKhung1.Text, txtSoMay1.Text, dienthoai, solan, "");
                    bool isunicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                    cmd2.CommandText = @"Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule) values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)";
                    cmd2.Parameters.Clear();
                    cmd2.Parameters.AddWithValue("@sendername", Class.CompanyInfo.sendername);
                    cmd2.Parameters.AddWithValue("@phone", dienthoai);
                    cmd2.Parameters.AddWithValue("@sms", resms);
                    cmd2.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                    cmd2.Parameters.AddWithValue("@smstype", "Thay dau may");
                    cmd2.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                    cmd2.Parameters.AddWithValue("@idkhachhang", idk);
                    cmd2.Parameters.AddWithValue("@timeSchedule", timechedule);
                    if (Class.datatabase.ExcuteNonQuery(cmd2) != 0)
                        MessageBox.Show("Gửi tin thay dầu máy được kích hoạt", "Thông Báo");
                }
                else
                {
                    MessageBox.Show("Không thể gửi tin nhắn do chưa cấu hình tự động tin nhắn hoặc không được kích hoạt", "Thông Báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Tin nhắn thất bại. Lỗi :" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            #endregion nhantinthaydaumay
        }

        #endregion GuiTinThayDauMay

        #region checkngaygiotanviet

        private void checkngaygiotanviet()
        {
            DataTable dt1;
            SqlCommand cb = new SqlCommand();
            if (Class.CompanyInfo.idcongty == "29" || Class.CompanyInfo.idcongty == "33")
            {
                cb.CommandText = "select datepart(hh,GETDATE()) as gio ,datepart(mi,GETDATE()) as phut";
                dt1 = Class.datatabase.getData(cb);
                int gio = int.Parse(dt1.Rows[0]["gio"].ToString());
                int phut = int.Parse(dt1.Rows[0]["phut"].ToString());
                if (gio >= 18 && phut >= 10)
                {
                    check = 1;
                }
                else { check = 0; }
            }
        }

        #endregion checkngaygiotanviet

        #region buttonItem1_Click

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Khi hoàn thành sẽ không thể sửa dữ liệu nữa, bạn muốn hoàn thành phiếu sửa chữa này?", "Hoàn thành", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    //

                    #region Lichsubaoduongxe

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select * from lichsubaoduongxetam where Idcongty = @IdCongTy and IdBaoDuong = @IdBaoDuong";
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                    dtLichSuBaoDuongXeTam = Class.datatabase.getData(cmd);
                    ////
                    if (dtLichSuBaoDuongXeTam.Rows.Count <= 0)
                    {
                        MessageBox.Show("Thông tin xe bảo dưỡng không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //
                    cmd.CommandText = "select * from lichsubaoduongchitiettam2 where IdBaoDuong = @IdBaoDuong";
                    dtPhuTungThayThe = Class.datatabase.getData(cmd);
                    //
                    cmd.CommandText = "select * from chuandoanxetam where IdBaoDuong = @IdBaoDuong";
                    dtChuanDoanXeTam = Class.datatabase.getData(cmd);
                    //
                    cmd.CommandText = "select * from ThoDichVu_GioViecTam where IdbaoDuong = @IdBaoDuong And IdCongTy = @IdCongTy";
                    dtThoDichVuGioViecTam = Class.datatabase.getData(cmd);
                    //
                    cmd.CommandText = "select * from ThoDichVu_TienCongThoTam where IdBaoDuong = @IdBaoDuong  And IdCongTy = @IdCongTy";
                    dtThoDichVuTienCongTam = Class.datatabase.getData(cmd);
                    //
                    cmd.CommandText = "select * from ThueNgoaiTam Where IdcongTy = @IdCongTy and IdBaoDuong = @IdBaoDuong And IdCongTy = @IdCongTy";
                    dtThueNgoaiTam = Class.datatabase.getData(cmd);
                    //
                    cmd.CommandText = "Select * from PhuTung WHERE IdCongTy = @IdCongTy";
                    dtPhuTung2 = Class.datatabase.getData(cmd);
                    //
                    cmd = new SqlCommand();
                    cmd.Connection = Class.datatabase.getConnection();
                    cmd.CommandTimeout = 0;
                    cmd.Connection.Open();
                    SqlTransaction tran = cmd.Connection.BeginTransaction();
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = @"insert into LichSuBaoDuongXe(idcuahang,idkhachhang,idcongty,tenxe,bienso,somay,sokhung,ngaybaoduong,ngaygiaoxe,solan,SoKm,ThayDau, YeuCauKH, IdThoDuyet, XuatPT,ThayDauMay,LoaiBaoDuong) values(@idcuahang,@idkhachhang,@idcongty,@tenxe,@bienso,@somay,@sokhung,@ngaybaoduong,Getdate(),@solan,@SoKm,@ThayDau, @YeuCauKH, @IdThoDuyet, @XuatPT, @ThayDauMay,@LoaiBaoDuong) select @@Identity";
                        cmd.Parameters.AddWithValue("@idcuahang", Class.EmployeeInfo.IdCuaHang);
                        cmd.Parameters.AddWithValue("@idkhachhang", dtLichSuBaoDuongXeTam.Rows[0]["IdKhachHang"]);
                        cmd.Parameters.AddWithValue("@Idcongty", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@tenxe", dtLichSuBaoDuongXeTam.Rows[0]["TenXe"]);
                        cmd.Parameters.AddWithValue("@bienso", dtLichSuBaoDuongXeTam.Rows[0]["BienSo"]);
                        cmd.Parameters.AddWithValue("@somay", dtLichSuBaoDuongXeTam.Rows[0]["Somay"]);
                        cmd.Parameters.AddWithValue("@sokhung", dtLichSuBaoDuongXeTam.Rows[0]["Sokhung"]);
                        cmd.Parameters.AddWithValue("@ngaybaoduong", dtLichSuBaoDuongXeTam.Rows[0]["NgayBaoDuong"]);
                        cmd.Parameters.AddWithValue("@solan", dtLichSuBaoDuongXeTam.Rows[0]["Solan"]);
                        cmd.Parameters.AddWithValue("@sokm", dtLichSuBaoDuongXeTam.Rows[0]["SoKm"]);
                        cmd.Parameters.AddWithValue("@YeucauKH", dtLichSuBaoDuongXeTam.Rows[0]["YeuCauKH"]);
                        cmd.Parameters.AddWithValue("@IDThoDuyet", dtLichSuBaoDuongXeTam.Rows[0]["IdThoDuyet"]);
                        cmd.Parameters.AddWithValue("@XuatPT", dtLichSuBaoDuongXeTam.Rows[0]["XuatPT"]);
                        cmd.Parameters.AddWithValue("@ThayDau", dtLichSuBaoDuongXeTam.Rows[0]["ThayDau"]);
                        cmd.Parameters.AddWithValue("@LoaiBaoDuong", dtLichSuBaoDuongXeTam.Rows[0]["LoaiBaoDuong"]);
                        int thay = 0;
                        if (dtLichSuBaoDuongXeTam.Rows[0]["ThayDauMay"].ToString() == "True")
                        {
                            thay = 1;
                        }
                        else { thay = 0; }
                        cmd.Parameters.AddWithValue("@ThayDauMay", thay);
                        idbaoduongxe = cmd.ExecuteScalar().ToString();
                        cmd.CommandText = "delete lichsubaoduongxetam Where IdBaoDuong = @IdBaoDuong And IdCongTy = @IdCongTy";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                        cmd.ExecuteNonQuery();

                    #endregion Lichsubaoduongxe

                        //xua lich su bao duong chi tiet tam cap nhat phu tung

                        #region lich su bao duong chi tiet2

                        if (dtPhuTungThayThe.Rows.Count > 0)
                        {
                            int soLuongSau;

                            foreach (DataRow row in dtPhuTungThayThe.Rows)
                            {
                                cmd.CommandText = "insert into lichsubaoduongchitiet2(IDBaoDuong, MaPT, TenPhuTung,SoLuong,Gia,GiaTien,IDKho,IDTho,IdPhuTung) Values(@IDBaoDuong,@MaPT,@TenPhuTung,@SoLuong,@Gia,@GiaTien,@IdKho,@IdTho,@IdPhuTung)";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdBaoDuong", idbaoduongxe);
                                cmd.Parameters.AddWithValue("@MaPT", Convert.ToString(row["MaPT"]));
                                cmd.Parameters.AddWithValue("@TenPhuTung", Convert.ToString(row["TenPhuTung"]));
                                cmd.Parameters.AddWithValue("@SoLuong", Convert.ToString(row["SoLuong"]));
                                cmd.Parameters.AddWithValue("@Gia", Convert.ToDecimal(row["Gia"]));
                                cmd.Parameters.AddWithValue("@GiaTien", Convert.ToDecimal(row["GiaTien"]));
                                cmd.Parameters.AddWithValue("@IDKho", Convert.ToString(row["IdKho"]));
                                cmd.Parameters.AddWithValue("@IdTho", Convert.ToString(row["IdTho"]));
                                cmd.Parameters.AddWithValue("@IdPhuTung", Convert.ToString(row["IdPhuTung"]));
                                cmd.ExecuteNonQuery();

                                string mapt = Convert.ToString(row["MaPT"]);
                                string sl = Convert.ToString(row["SoLuong"]);
                                string tenpt = Convert.ToString(row["TenPhuTung"]);
                                string idkho2 = Convert.ToString(row["IdKho"]);

                                DataRow[] rows = dtPhuTung2.Select("IdPT = '" + Convert.ToString(row["IdPhuTung"]) + "'");
                                if (rows.Length > 0)
                                {
                                    soLuongSau = Convert.ToInt32(rows[0]["SoLuong"]) - Convert.ToInt32(row["SoLuong"]);
                                    cmd.CommandText = "Update PhuTung Set SoLuong = @SoLuong Where IdCongTy = @IdCongTy and IdPT = @IdPT";
                                    cmd.Parameters.Clear();

                                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    cmd.Parameters.AddWithValue("@IDPT", Convert.ToString(row["IdPhuTung"]));
                                    cmd.Parameters.AddWithValue("@SoLuong", soLuongSau);
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "insert into KhoXuat(MaPT,TenPT,SoLuong,NgayXuat,LoaiXuat,IdKho,IdCongTy) values(@mapt,@tenpt,@soluong,getdate(),'Xuat Ban',@idkho,@idcongty)";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@mapt", mapt);
                                    cmd.Parameters.AddWithValue("@tenpt", tenpt);
                                    cmd.Parameters.AddWithValue("soluong", sl);
                                    cmd.Parameters.AddWithValue("@idkho", idkho2);
                                    cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                            //
                            cmd.CommandText = "delete lichsubaoduongchitiettam2 where Idbaoduong = @idbaoduong";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                            cmd.ExecuteNonQuery();
                        }

                        #endregion lich su bao duong chi tiet2

                        //gui tin nhan thay dau dc hay khong phu tung van phai tru do da lam

                        if (dtLichSuBaoDuongXeTam.Rows[0]["ThayDau"].Equals("True"))
                        {
                            //SqlCommand cmo = new SqlCommand();
                            //string sql = "update LichSuBaoDuongXe set thaydau='true' where idbaoduong=" + idbaoduong;
                            //cmo.CommandText = sql;
                            //i = Class.datatabase.ExcuteNonQuery(cmo);

                            #region nhantinthaydau

                            try
                            {
                                SqlCommand cmo = new SqlCommand();
                                DataTable tblThayDauConfig = new DataTable();
                                cmo.CommandText = "Select top 1 * from SMSMoiThayDau_Config where idcongty=" + Class.CompanyInfo.idcongty + " and active=1";
                                tblThayDauConfig = Class.datatabase.getData(cmo);
                                cmo.CommandText = "select sms from SMSConfig where idcongty=" + Class.CompanyInfo.idcongty + " and Type='Thay dau'";
                                DataTable sms = new DataTable();
                                sms = Class.datatabase.getData(cmo);

                                if (tblThayDauConfig.Rows.Count > 0 && sms.Rows.Count > 0)
                                {
                                    string nhansausongay = tblThayDauConfig.Rows[0]["nhansausongay"].ToString();
                                    string gionhan = tblThayDauConfig.Rows[0]["gionhan"].ToString();
                                    SqlCommand cmd3 = new SqlCommand();
                                    cmd3.CommandText = "select GETDATE() as Time";
                                    DataTable dttime = Class.datatabase.getData(cmd3);
                                    DateTime dt2 = DateTime.Parse(dttime.Rows[0]["Time"].ToString());
                                    DateTime d = dt2.AddDays(int.Parse(nhansausongay));
                                    DateTime timechedule = new DateTime(d.Year, d.Month, d.Day, int.Parse(gionhan), 0, 0, 0);
                                    string ngay = ngaysinh;
                                    string resms = Utilities.Smsreplace(sms.Rows[0]["SMS"].ToString(), tenkh, ngay, Class.CompanyInfo.sendername, tenxe1, txtBienSo1.Text, txtSoKhung1.Text, txtSoMay1.Text, dienthoai, solan, "");
                                    bool isunicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                    SqlCommand cmd2 = new SqlCommand();
                                    cmd2.CommandText = @"Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule) values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)";
                                    cmd2.Parameters.Clear();
                                    cmd2.Parameters.AddWithValue("@sendername", Class.CompanyInfo.sendername);
                                    cmd2.Parameters.AddWithValue("@phone", dienthoai);
                                    cmd2.Parameters.AddWithValue("@sms", resms);
                                    cmd2.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                                    cmd2.Parameters.AddWithValue("@smstype", "Thay dau");
                                    cmd2.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                    cmd2.Parameters.AddWithValue("@idkhachhang", idk);
                                    cmd2.Parameters.AddWithValue("@timeSchedule", timechedule);
                                    if (Class.datatabase.ExcuteNonQuery(cmd2) != 0)
                                    {
                                        MessageBox.Show("Gửi tin thay dầu được kích hoạt", "Thông Báo");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Không thể gửi tin nhắn do chưa cấu hình tự động tin nhắn hoặc không được kích hoạt", "Thông Báo");
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Tin nhắn thất bại. Lỗi :" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            #endregion nhantinthaydau
                        }
                        if (dtLichSuBaoDuongXeTam.Rows[0]["ThayDauMay"].ToString() == "True")
                        {
                            GuiTinThayDauMay();
                        }
                        //xoa lich su tam

                        #region Cong tho khac

                        if (dtThueNgoaiTam.Rows.Count > 0)
                        {
                            cmd.CommandText = "insert into ThueNgoai(CongViec,TienThueNgoai,TienLayCuaKh,TienLai,GhiChu,IdCongTy,IdTho,IDBaoDuong,NgaySuaChua) values(@CongViec,@TienThueNgoai,@TienLayCuaKH,@TienLai,@GhiChu,@IdCongTy,@IdTho,@IdBaoDuong,@NgaySuaChua)";
                            foreach (DataRow row in dtThueNgoaiTam.Rows)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@CongViec", Convert.ToString(row["CongViec"]));
                                cmd.Parameters.AddWithValue("@TienThueNgoai", Convert.ToString(row["TienThueNgoai"]));
                                cmd.Parameters.AddWithValue("@TienLayCuaKh", Convert.ToString(row["TienLayCuaKh"]));
                                cmd.Parameters.AddWithValue("@TienLai", Convert.ToString(row["TienLai"]));
                                cmd.Parameters.AddWithValue("@GhiChu", Convert.ToString(row["GhiChu"]));
                                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd.Parameters.AddWithValue("@IdTho", Convert.ToString(row["IDTho"]));
                                cmd.Parameters.AddWithValue("@NgaySuaChua", Convert.ToDateTime(row["NgaySuaChua"]));
                                cmd.Parameters.AddWithValue("@IdBaoDuong", idbaoduongxe);
                                cmd.ExecuteNonQuery();
                            }
                            cmd.CommandText = "delete ThueNgoaiTam Where IdCongTy = @IdCongTy And IdbaoDuong = @IdBaoDuong";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                            cmd.ExecuteNonQuery();
                        }
                        //
                        if (dtThoDichVuGioViecTam.Rows.Count > 0)
                        {
                            cmd.CommandText = "insert into ThoDichVu_GioViec(IdTho,IdGioViec,NgaySuaChua,GhiChu,IdCongTy,IDBaoDuong) values(@Idtho,@IdGioViec,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong)";
                            foreach (DataRow row in dtThoDichVuGioViecTam.Rows)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdTho", Convert.ToString(row["IdTho"]));
                                cmd.Parameters.AddWithValue("@IdGioViec", Convert.ToString(row["IdGioViec"]));
                                cmd.Parameters.AddWithValue("@NgaySuaChua", Convert.ToDateTime(row["NgaySuaChua"]));
                                cmd.Parameters.AddWithValue("@GhiChu", Convert.ToString(row["GhiChu"]));
                                cmd.Parameters.AddWithValue("@IdCongTy", Convert.ToString(row["IdTho"]));
                                cmd.Parameters.AddWithValue("@IdBaoDuong", idbaoduongxe);
                                cmd.ExecuteNonQuery();
                            }
                            cmd.CommandText = "delete ThoDichVu_GioViecTam Where IdCongTy = @IdCongTy And IdbaoDuong =@idBaoDuong";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                            cmd.ExecuteNonQuery();
                        }
                        if (dtThoDichVuTienCongTam.Rows.Count > 0)
                        {
                            cmd.CommandText = "insert into ThoDichVu_TienCongTho2(IdTho,IdTienCong,NgaySuaChua,GhiChu,IdCongTy,IDBaoDuong,TienCong,TienKhachTra) values(@Idtho,@IdTienCong,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong,@TienCong,@TienKhachTra)";
                            foreach (DataRow row in dtThoDichVuTienCongTam.Rows)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdTho", Convert.ToString(row["IdTho"]));
                                cmd.Parameters.AddWithValue("@IdTienCong", Convert.ToString(row["IdTienCong"]));
                                cmd.Parameters.AddWithValue("@NgaySuaChua", Convert.ToDateTime(row["NgaySuaChua"]));
                                cmd.Parameters.AddWithValue("@GhiChu", Convert.ToString(row["GhiChu"]));
                                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd.Parameters.AddWithValue("@IdBaoDuong", idbaoduongxe);
                                cmd.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(row["TienCong"]));
                                cmd.Parameters.AddWithValue("@TienKhachTra", Convert.ToDecimal(row["TienKhachTra"]));

                                cmd.ExecuteNonQuery();
                            }
                            cmd.CommandText = "delete ThoDichVu_TienCongThoTam Where IdCongTy = @IdCongTy And IdbaoDuong =@idBaoDuong";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                            cmd.ExecuteNonQuery();
                        }
                        cmd.CommandText = "delete lichsubaoduongxetam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                        cmd.ExecuteNonQuery();

                        //Đức Anh:Xóa công việc trong bảng tạm
                        //Xóa việc theo giờ
                        cmd.CommandText = "delete ThoDichVu_GioViecTam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                        cmd.ExecuteNonQuery();

                        //Xóa việc theo tiền
                        cmd.CommandText = "delete ThoDichVu_TienCongThoTam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                        cmd.ExecuteNonQuery();

                        //Xóa việc thuê ngoài
                        cmd.CommandText = "delete ThueNgoaiTam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                        cmd.ExecuteNonQuery();
                        //Đức Anh:Xóa công việc trong bảng tạm

                        cmd.CommandText = "insert into lichsubaoduongphieu(idbaoduong,sophieu,tongtien, TienCongTho, TienPT,NgayGiaoXe,IdCongTy) values(@idbaoduong,@sophieu,@tongtien, @TienCongTho, @TienPT,GetDate(),@IdCongTy)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idbaoduong", idbaoduongxe);
                        cmd.Parameters.AddWithValue("@sophieu", txtSoPhieu.Text);
                        cmd.Parameters.AddWithValue("@tongtien", tienCongTho + tienPhuTung);
                        cmd.Parameters.AddWithValue("@TienCongTho", tienCongTho);
                        cmd.Parameters.AddWithValue("@TienPT", tienPhuTung);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = "Insert into PhieuThu(IdLoaiPhieuThu,SoTienThu,IdCongTy,IdCuaHang,IdNhanVien,NgayHachToan,SoHoaDon) Values(@idLoaiPhieuThu,@SoTienThu,@IdCongTy,@IdCuaHang,@IdNhanVien,Getdate(),@SoHoaDon)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdLoaiPhieuThu", "5");
                        cmd.Parameters.AddWithValue("@SoTienThu", tienCongTho + tienThueNgoai + tienPhuTung);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                        cmd.Parameters.AddWithValue("@IdNhanVien", Class.EmployeeInfo.idnhanvien);
                        cmd.Parameters.AddWithValue("@SoHoaDon", idbaoduongxe);
                        cmd.ExecuteNonQuery();
                        //

                        tran.Commit();
                        cmd.Connection.Close();
                    }

                        #endregion Cong tho khac

                    catch (Exception ex)
                    {
                        tran.Rollback();
                        cmd.Connection.Close();
                        MessageBox.Show("Lưu thông tin thất bại. Lỗi :" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    //
                    DialogResult chon = MessageBox.Show("Lưu thông tin thành công." + Environment.NewLine + "Bạn có muốn in phiếu bảo dưỡng không.?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    LoadXeDangBaoDuong();
                    dgvDSPT.DataSource = null;
                    dgvSoPhut.DataSource = null;
                    dgvSuaNgoai.DataSource = null;
                    dgvTheoSoTien.DataSource = null;
                    txtBienSo1.Text = null;
                    txtSoKhung1.Text = null;
                    txtSoMay1.Text = null;
                    txtID1.Text = null;

                    //   ChonKho();
                    if (chon == DialogResult.Yes)
                    {
                        Class.SelectedCustomer._idbaoduong = idbaoduongxe;
                        if (Class.SelectedCustomer._idbaoduong == null)
                        { MessageBox.Show("Lần bảo dưỡng không tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        else
                        {
                            if (Convert.ToInt64(Class.CompanyInfo.idcongty) != 31)
                            {
                                FrmPhieuSuaChuaThangLoi frm = new FrmPhieuSuaChuaThangLoi();
                                frm.ShowDialog();
                                //frmPhieuSuaChuaTM98 frm = new frmPhieuSuaChuaTM98();
                                //frm.ShowDialog();
                            }
                            else
                            {
                                frmPhieuSuaChuaTM98 frm = new frmPhieuSuaChuaTM98();
                                frm.ShowDialog();
                            }
                        }
                    }
                    //
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi :" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion buttonItem1_Click

        #region buttonItem2_Click

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn Hủy lần bảo dưỡng của Xe: " + txtBienSo1.Text, "Hủy lần bảo dưỡng", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (txtID1.Text != "" && txtID1.Text != null)
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "delete from LichSuBaoDuongXeTam Where IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang and IdBaoDuong=@IdBaoDuong";
                    cmd.Connection = Class.datatabase.getConnection();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                    cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                    Class.datatabase.ExcuteNonQuery(cmd);
                    cmd.CommandText = "delete lichsubaoduongchitiettam2 where IdBaoDuong=@IdBaoDuong";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                    Class.datatabase.ExcuteNonQuery(cmd);
                    cmd.CommandText = "delete ThoDichVu_TienCongThoTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                    Class.datatabase.ExcuteNonQuery(cmd);
                    cmd.CommandText = "delete ThoDichVu_GioViecTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                    Class.datatabase.ExcuteNonQuery(cmd);
                    cmd.CommandText = "delete ThueNgoaiTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                    Class.datatabase.ExcuteNonQuery(cmd);
                    LoadXeDangBaoDuong();
                }
                else
                {
                    MessageBox.Show("Hãy chọn Xe muốn hủy lần bảo dưỡng.");
                }
            }
        }

        #endregion buttonItem2_Click

        #region buttonItem3_Click

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            LoadXeDangBaoDuong();
            dtKho = classdb.LoadTenKho();
            if (dtKho.Rows.Count > 0)
            {
                this.Kho6.DataSource = dtKho;
                this.Kho6.ValueMember = "IdKho";
                this.Kho6.DisplayMember = "TenKho";
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select MaPT, IDPT From PhuTung Where IdCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            DataTable dt = Class.datatabase.getData(cmd);
            MaPT.DataSource = dt;
            MaPT.DisplayMember = "MaPT";
            MaPT.ValueMember = "IdPT";
            DataTable dtThoSua = new DataTable();
            cmd.CommandText = "Select IDTho, MaTho + ' - ' + TenTho As TenTho From ThoDichVu Where IDCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtThoSua = Class.datatabase.getData(cmd);
            if (dtThoSua.Rows.Count > 0)
            {
                Tho.DataSource = dtThoSua;
                Tho.ValueMember = "IdTho";
                Tho.DisplayMember = "TenTho";
            }
        }

        #endregion buttonItem3_Click

        #region buttonItem5_Click

        private void buttonItem5_Click(object sender, EventArgs e)
        {

        }

        #endregion buttonItem5_Click

        #region buttonItem6_Click

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            
        }

        #endregion buttonItem6_Click

        #region buttonItem6_Click

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtID1.Text))
            {
                MessageBox.Show("Bạn chưa chọn xe bảo dưỡng!\nChọn một xe bảo dưỡng trong danh sách xe sửa chữa.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show("Khi hoàn thành sẽ không thể sửa dữ liệu nữa, bạn muốn hoàn thành phiếu sửa chữa này?", "Hoàn thành bảo dưỡng", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    checkngaygiotanviet();

                    try
                    {
                        if (txtgiamtru.Text != "" || txtgiamtru.Text != null)
                            chietkhau = 1 - (int.Parse(txtgiamtru.Text) * 0.01);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Giảm trừ phải là số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    //
                    string _idbaoduongtam = txtID1.Text.Trim();

                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "Select * from lichsubaoduongxetam where Idcongty = @IdCongTy and IdBaoDuong = @IdBaoDuong";
                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@IdBaoDuong", _idbaoduongtam);
                    dtLichSuBaoDuongXeTam = Class.datatabase.getData(cmd);
                    ////
                    if (dtLichSuBaoDuongXeTam.Rows.Count <= 0)
                    {
                        MessageBox.Show("Thông tin xe bảo dưỡng không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    //
                    cmd.CommandText = "select * from lichsubaoduongchitiettam2 where IdBaoDuong = @IdBaoDuong";
                    dtPhuTungThayThe = Class.datatabase.getData(cmd);
                    //
                    cmd.CommandText = "select * from chuandoanxetam where IdBaoDuong = @IdBaoDuong";
                    dtChuanDoanXeTam = Class.datatabase.getData(cmd);
                    //
                    cmd.CommandText = "select * from ThoDichVu_GioViecTam where IdbaoDuong = @IdBaoDuong And IdCongTy = @IdCongTy";
                    dtThoDichVuGioViecTam = Class.datatabase.getData(cmd);
                    //
                    cmd.CommandText = "select * from ThoDichVu_TienCongThoTam where IdBaoDuong = @IdBaoDuong  And IdCongTy = @IdCongTy";
                    dtThoDichVuTienCongTam = Class.datatabase.getData(cmd);
                    //
                    cmd.CommandText = "select * from ThueNgoaiTam Where IdcongTy = @IdCongTy and IdBaoDuong = @IdBaoDuong And IdCongTy = @IdCongTy";
                    dtThueNgoaiTam = Class.datatabase.getData(cmd);
                    //
                    cmd.CommandText = "Select * from PhuTung WHERE IdCongTy = @IdCongTy";
                    dtPhuTung2 = Class.datatabase.getData(cmd);
                    //
                    cmd.CommandText = "SELECT * FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                    tableBaoGiaTam = Class.datatabase.getData(cmd);
                    //
                    if (tableBaoGiaTam.Rows.Count > 0)
                    {
                        cmd.CommandText = "SELECT * FROM BaoGiaCongThoTam WHERE IdBaoGia = @IdBaoGia";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());

                        tableBaoGiaCongThoTam = Class.datatabase.getData(cmd);

                        cmd.CommandText = "SELECT * FROM BaoGiaPhuTungTam WHERE IdBaoGia = @IdBaoGia";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());

                        tableBaoGiaPhuTungTam = Class.datatabase.getData(cmd);
                    }
                    //
                    cmd = new SqlCommand();
                    cmd.Connection = Class.datatabase.getConnection();
                    //cmd.CommandTimeout = 0;
                    cmd.Connection.Open();

                    object _ngaygiaoxe = dtLichSuBaoDuongXeTam.Rows[0]["NgayGiaoXe"];

                    if (check > 0)
                        cmd.CommandText = @"insert into LichSuBaoDuongXe(idcuahang,idkhachhang,idcongty,tenxe,bienso,somay,sokhung,ngaybaoduong,ngaygiaoxe,solan,SoKm,ThayDau, YeuCauKH, IdThoDuyet, XuatPT,ThayDauMay,GhiChu,LoaiBaoDuong)
                                            values(@idcuahang,@idkhachhang,@idcongty,@tenxe,@bienso,@somay,@sokhung,@ngaybaoduong,DATEADD(hh,12,@NgayGiaoXe),@solan,@SoKm,@ThayDau, @YeuCauKH, @IdThoDuyet, @XuatPT, @ThayDauMay, @GhiChu,@LoaiBaoDuong) select @@Identity";
                    else
                        cmd.CommandText = @"insert into LichSuBaoDuongXe(idcuahang,idkhachhang,idcongty,tenxe,bienso,somay,sokhung,ngaybaoduong,ngaygiaoxe,solan,SoKm,ThayDau, YeuCauKH, IdThoDuyet, XuatPT,ThayDauMay, GhiChu,LoaiBaoDuong)
                                            values(@idcuahang,@idkhachhang,@idcongty,@tenxe,@bienso,@somay,@sokhung,@ngaybaoduong,@NgayGiaoXe,@solan,@SoKm,@ThayDau, @YeuCauKH, @IdThoDuyet, @XuatPT, @ThayDauMay, @GhiChu,@LoaiBaoDuong) select @@Identity";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@idcuahang", Class.EmployeeInfo.IdCuaHang);
                    cmd.Parameters.AddWithValue("@idkhachhang", dtLichSuBaoDuongXeTam.Rows[0]["IdKhachHang"]);
                    cmd.Parameters.AddWithValue("@Idcongty", Class.CompanyInfo.idcongty);
                    cmd.Parameters.AddWithValue("@tenxe", dtLichSuBaoDuongXeTam.Rows[0]["TenXe"]);
                    cmd.Parameters.AddWithValue("@bienso", dtLichSuBaoDuongXeTam.Rows[0]["BienSo"]);
                    cmd.Parameters.AddWithValue("@somay", dtLichSuBaoDuongXeTam.Rows[0]["Somay"]);
                    cmd.Parameters.AddWithValue("@sokhung", dtLichSuBaoDuongXeTam.Rows[0]["Sokhung"]);
                    cmd.Parameters.AddWithValue("@ngaybaoduong", dtLichSuBaoDuongXeTam.Rows[0]["NgayBaoDuong"]);
                    cmd.Parameters.AddWithValue("@NgayGiaoXe", dtLichSuBaoDuongXeTam.Rows[0]["NgayGiaoXe"]);
                    cmd.Parameters.AddWithValue("@solan", dtLichSuBaoDuongXeTam.Rows[0]["Solan"]);
                    cmd.Parameters.AddWithValue("@sokm", dtLichSuBaoDuongXeTam.Rows[0]["SoKm"]);
                    cmd.Parameters.AddWithValue("@YeucauKH", dtLichSuBaoDuongXeTam.Rows[0]["YeuCauKH"]);
                    cmd.Parameters.AddWithValue("@IDThoDuyet", dtLichSuBaoDuongXeTam.Rows[0]["IdThoDuyet"]);
                    cmd.Parameters.AddWithValue("@XuatPT", dtLichSuBaoDuongXeTam.Rows[0]["XuatPT"]);
                    cmd.Parameters.AddWithValue("@ThayDau", dtLichSuBaoDuongXeTam.Rows[0]["ThayDau"]);

                    if (!String.IsNullOrEmpty(txtGhiChuBaoDuong.Text))
                        cmd.Parameters.AddWithValue("@GhiChu", txtGhiChuBaoDuong.Text);
                    else
                        cmd.Parameters.AddWithValue("@GhiChu", dtLichSuBaoDuongXeTam.Rows[0]["GhiChu"]);
                    cmd.Parameters.AddWithValue("@LoaiBaoDuong", dtLichSuBaoDuongXeTam.Rows[0]["LoaiBaoDuong"]);

                    int thay = 0;

                    if (dtLichSuBaoDuongXeTam.Rows[0]["ThayDauMay"].ToString() == "True")
                    {
                        thay = 1;
                    }
                    else { thay = 0; }

                    cmd.Parameters.AddWithValue("@ThayDauMay", thay);
                    idbaoduongxe = cmd.ExecuteScalar().ToString(); // --------------IdBaoDuong

                    SqlTransaction tran = cmd.Connection.BeginTransaction();
                    cmd.Transaction = tran;

                    try
                    {
                        #region lich su bao duong chi tiet2

                        if (dtPhuTungThayThe.Rows.Count > 0)
                        {
                            int soLuongSau;
                            foreach (DataRow row in dtPhuTungThayThe.Rows)
                            {
                                cmd.CommandText = "insert into lichsubaoduongchitiet2(IDBaoDuong, MaPT, TenPhuTung,SoLuong,Gia,GiaTien,IDKho,IDTho,IdPhuTung)"
                                                                             + " Values(@IDBaoDuong,@MaPT,@TenPhuTung,@SoLuong,@Gia,@GiaTien,@IdKho,@IdTho,@IdPhuTung)";
                                string mapt = Convert.ToString(row["MaPT"]);
                                string sl = Convert.ToString(row["SoLuong"]);
                                string tenpt = Convert.ToString(row["TenPhuTung"]);
                                string idkho2 = Convert.ToString(row["IdKho"]);
                                string __idpt = Convert.ToString(row["IdPhuTung"]);

                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdBaoDuong", idbaoduongxe);
                                cmd.Parameters.AddWithValue("@MaPT", Convert.ToString(row["MaPT"]));
                                cmd.Parameters.AddWithValue("@TenPhuTung", Convert.ToString(row["TenPhuTung"]));
                                cmd.Parameters.AddWithValue("@SoLuong", Convert.ToString(row["SoLuong"]));
                                cmd.Parameters.AddWithValue("@Gia", Convert.ToDecimal(row["Gia"]));
                                cmd.Parameters.AddWithValue("@GiaTien", Convert.ToDouble(row["GiaTien"]) * chietkhau);
                                cmd.Parameters.AddWithValue("@IDKho", Convert.ToString(row["IdKho"]));
                                cmd.Parameters.AddWithValue("@IdTho", row["IdTho"]);
                                cmd.Parameters.AddWithValue("@IdPhuTung", Convert.ToString(row["IdPhuTung"]));
                                cmd.ExecuteNonQuery();

                                DataRow[] rows = dtPhuTung2.Select("IdPT = '" + Convert.ToString(row["IdPhuTung"]) + "'");
                                if (rows.Length > 0)
                                {
                                    soLuongSau = Convert.ToInt32(rows[0]["SoLuong"]) - Convert.ToInt32(row["SoLuong"]);
                                    cmd.CommandText = "Update PhuTung Set SoLuong = @SoLuong Where IdCongTy = @IdCongTy and IdPT = @IdPT";
                                    cmd.Parameters.Clear();

                                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    cmd.Parameters.AddWithValue("@IDPT", Convert.ToString(row["IdPhuTung"]));
                                    cmd.Parameters.AddWithValue("@SoLuong", soLuongSau);
                                    cmd.ExecuteNonQuery();

                                    cmd.CommandText = "insert into KhoXuat(MaPT,TenPT,SoLuong,NgayXuat,LoaiXuat,IdKho,IdCongTy,IdBaoDuong) values(@mapt,@tenpt,@soluong,getdate(),'Xuat Ban',@idkho,@idcongty,@idbaoduong)";
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@mapt", mapt);
                                    cmd.Parameters.AddWithValue("@tenpt", tenpt);
                                    cmd.Parameters.AddWithValue("soluong", sl);
                                    cmd.Parameters.AddWithValue("@idkho", idkho2);
                                    cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                    cmd.Parameters.AddWithValue("@idbaoduong", idbaoduongxe);
                                    cmd.ExecuteNonQuery();
                                }
                            }
                        }

                        #endregion lich su bao duong chi tiet2

                        #region Gửi tin thay dầu

                        bool TrangThaiThayDauMay = false;
                        bool TrangThaiThayDauThuong = false;

                        try
                        {
                            string ThayDauMay = dtLichSuBaoDuongXeTam.Rows[0]["ThayDauMay"].ToString();
                            TrangThaiThayDauMay = bool.Parse(ThayDauMay);
                        }
                        catch { }

                        try
                        {
                            string ThayDauThuong = dtLichSuBaoDuongXeTam.Rows[0]["ThayDau"].ToString();
                            TrangThaiThayDauThuong = bool.Parse(ThayDauThuong);
                        }
                        catch { }

                        bool ChangeOilByKM = ChangeOilKM.IsUseChangeOilByKM(CompanyInfo.idcongty);

                        //Nếu không thay dầu máy thì nhắn tin thay dầu thường
                        if (TrangThaiThayDauMay == false)
                        {
                            //Nếu có thay dầu thường
                            if (TrangThaiThayDauThuong == true && ChangeOilByKM == false)
                            {
                                #region nhantinthaydau

                                try
                                {
                                    SqlCommand cmo = new SqlCommand();
                                    DataTable tblThayDauConfig = new DataTable();
                                    cmo.CommandText = "Select top 1 * from SMSMoiThayDau_Config where idcongty=" + Class.CompanyInfo.idcongty + " and active=1";
                                    tblThayDauConfig = Class.datatabase.getData(cmo);
                                    cmo.CommandText = "select sms from SMSConfig where idcongty=" + Class.CompanyInfo.idcongty + " and Type='Thay dau'";
                                    DataTable sms = new DataTable();
                                    sms = Class.datatabase.getData(cmo);

                                    if (tblThayDauConfig.Rows.Count > 0 && sms.Rows.Count > 0)
                                    {
                                        string nhansausongay = tblThayDauConfig.Rows[0]["nhansausongay"].ToString();
                                        string gionhan = tblThayDauConfig.Rows[0]["gionhan"].ToString();
                                        SqlCommand cmd3 = new SqlCommand();
                                        cmd3.CommandText = "select GETDATE() as Time";
                                        DataTable dttime = Class.datatabase.getData(cmd3);
                                        DateTime dt2 = DateTime.Parse(dttime.Rows[0]["Time"].ToString());
                                        DateTime d = dt2.AddDays(int.Parse(nhansausongay));
                                        DateTime timechedule = new DateTime(d.Year, d.Month, d.Day, int.Parse(gionhan), 0, 0, 0);
                                        string ngay = ngaysinh;
                                        string resms = Utilities.Smsreplace(sms.Rows[0]["SMS"].ToString(), tenkh, ngay, Class.CompanyInfo.sendername, tenxe1, txtBienSo1.Text, txtSoKhung1.Text, txtSoMay1.Text, dienthoai, solan, "");
                                        bool isunicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                        SqlCommand cmd2 = new SqlCommand();
                                        cmd2.CommandText = @"Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule) values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)";
                                        cmd2.Parameters.Clear();
                                        cmd2.Parameters.AddWithValue("@sendername", Class.CompanyInfo.sendername);
                                        cmd2.Parameters.AddWithValue("@phone", dienthoai);
                                        cmd2.Parameters.AddWithValue("@sms", resms);
                                        cmd2.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                                        cmd2.Parameters.AddWithValue("@smstype", "Thay dau");
                                        cmd2.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                        cmd2.Parameters.AddWithValue("@idkhachhang", idk);
                                        cmd2.Parameters.AddWithValue("@timeSchedule", timechedule);
                                        if (Class.datatabase.ExcuteNonQuery(cmd2) != 0)
                                        {
                                            MessageBox.Show("Gửi tin thay dầu được kích hoạt", "Thông Báo");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Không thể gửi tin nhắn do chưa cấu hình tự động tin nhắn hoặc không được kích hoạt", "Thông Báo");
                                    }
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Tin nhắn thất bại. Lỗi :" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                    return;
                                }

                                #endregion nhantinthaydau
                            }
                            else if (TrangThaiThayDauThuong == false && ChangeOilByKM == true)
                            {
                                ChangeOilKM.SendSmsChangeOilNormal(idk, idbaoduongxe, int.Parse(dtLichSuBaoDuongXeTam.Rows[0]["SoKm"].ToString()), txtSoKhung1.Text, CompanyInfo.idcongty);
                            }
                        }

                        if (TrangThaiThayDauThuong == false)
                        {
                            #region gui tin thay dau may

                            if (TrangThaiThayDauMay == true && ChangeOilByKM == false)
                            {
                                GuiTinThayDauMay();
                            }
                            else if (TrangThaiThayDauMay && ChangeOilByKM == true)
                            {
                                ChangeOilKM.SendSmsChangeOilDauMay(idk, idbaoduongxe, int.Parse(dtLichSuBaoDuongXeTam.Rows[0]["SoKm"].ToString()), txtSoKhung1.Text, CompanyInfo.idcongty);
                            }

                            #endregion gui tin thay dau may
                        }

                        #endregion Gửi tin thay dầu

                        #region Cong tho khac

                        //Thuê noài
                        if (dtThueNgoaiTam.Rows.Count > 0)
                        {
                            cmd.CommandText = "insert into ThueNgoai(CongViec,TienThueNgoai,TienLayCuaKh,TienLai,GhiChu,IdCongTy,IdTho,IDBaoDuong,NgaySuaChua) values(@CongViec,@TienThueNgoai,@TienLayCuaKH,@TienLai,@GhiChu,@IdCongTy,@IdTho,@IdBaoDuong,@NgaySuaChua)";
                            foreach (DataRow row in dtThueNgoaiTam.Rows)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@CongViec", Convert.ToString(row["CongViec"]));
                                cmd.Parameters.AddWithValue("@TienThueNgoai", Convert.ToString(row["TienThueNgoai"]));
                                cmd.Parameters.AddWithValue("@TienLayCuaKh", (Convert.ToDouble(row["TienLayCuaKh"]) * chietkhau).ToString());
                                cmd.Parameters.AddWithValue("@TienLai", (Convert.ToDouble(row["TienLai"]) * chietkhau).ToString());
                                cmd.Parameters.AddWithValue("@GhiChu", Convert.ToString(row["GhiChu"]));
                                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd.Parameters.AddWithValue("@IdTho", Convert.ToString(row["IDTho"]));
                                cmd.Parameters.AddWithValue("@NgaySuaChua", Convert.ToDateTime(row["NgaySuaChua"]));
                                cmd.Parameters.AddWithValue("@IdBaoDuong", idbaoduongxe);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        //Giờ công việc
                        if (dtThoDichVuGioViecTam.Rows.Count > 0)
                        {
                            cmd.CommandText = "insert into ThoDichVu_GioViec(IdTho,IdGioViec,NgaySuaChua,GhiChu,IdCongTy,IDBaoDuong) values(@Idtho,@IdGioViec,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong)";
                            foreach (DataRow row in dtThoDichVuGioViecTam.Rows)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdTho", Convert.ToString(row["IdTho"]));
                                cmd.Parameters.AddWithValue("@IdGioViec", Convert.ToString(row["IdGioViec"]));
                                cmd.Parameters.AddWithValue("@NgaySuaChua", Convert.ToDateTime(row["NgaySuaChua"]));
                                cmd.Parameters.AddWithValue("@GhiChu", Convert.ToString(row["GhiChu"]));
                                cmd.Parameters.AddWithValue("@IdCongTy", Convert.ToString(row["IdTho"]));
                                cmd.Parameters.AddWithValue("@IdBaoDuong", idbaoduongxe);
                                cmd.ExecuteNonQuery();
                            }
                        }

                        //Tiền công viêc
                        if (dtThoDichVuTienCongTam.Rows.Count > 0)
                        {
                            cmd.CommandText = "insert into ThoDichVu_TienCongTho2(IdTho,IdTienCong,NgaySuaChua,GhiChu,IdCongTy,IDBaoDuong,TienCong,TienKhachTra) values(@Idtho,@IdTienCong,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong,@TienCong,@TienKhachTra)";
                            foreach (DataRow row in dtThoDichVuTienCongTam.Rows)
                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdTho", Convert.ToString(row["IdTho"]));
                                cmd.Parameters.AddWithValue("@IdTienCong", Convert.ToString(row["IdTienCong"]));
                                cmd.Parameters.AddWithValue("@NgaySuaChua", Convert.ToDateTime(row["NgaySuaChua"]));
                                cmd.Parameters.AddWithValue("@GhiChu", Convert.ToString(row["GhiChu"]));
                                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd.Parameters.AddWithValue("@IdBaoDuong", idbaoduongxe);
                                cmd.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(row["TienCong"]));
                                cmd.Parameters.AddWithValue("@TienKhachTra", Convert.ToDouble(row["TienKhachTra"]) * chietkhau);

                                cmd.ExecuteNonQuery();
                            }
                        }

                        #endregion Cong tho khac

                        #region Lưu lịch sử báo giá

                        if (tableBaoGiaTam.Rows.Count > 0)
                        {
                            cmd.CommandText = @"INSERT INTO BaoGiaSuaChua
                                                (IdKhachHang, IdBaoDuong, NgayBaoGia, TongTienVatTu, TongTienCong, VAT, TongSauVAT, CoVanDV, TruongPhongDV)
                                                VALUES (@IdKhachHang,@IdBaoDuong,@NgayBaoGia,@TongTienVatTu,@TongTienCong,@VAT,@TongSauVAT,@CoVanDV,@TruongPhongDV)
                                                SELECT @@IDENTITY";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdKhachHang", tableBaoGiaTam.Rows[0]["IdKhachHang"].ToString());
                            cmd.Parameters.AddWithValue("@IdBaoDuong", idbaoduongxe);
                            cmd.Parameters.AddWithValue("@NgayBaoGia", tableBaoGiaTam.Rows[0]["NgayBaoGia"].ToString());
                            cmd.Parameters.AddWithValue("@TongTienVatTu", tableBaoGiaTam.Rows[0]["TongTienVatTu"].ToString());
                            cmd.Parameters.AddWithValue("@TongTienCong", tableBaoGiaTam.Rows[0]["TongTienCong"].ToString());
                            cmd.Parameters.AddWithValue("@VAT", tableBaoGiaTam.Rows[0]["VAT"].ToString());
                            cmd.Parameters.AddWithValue("@TongSauVAT", tableBaoGiaTam.Rows[0]["TongSauVAT"].ToString());
                            cmd.Parameters.AddWithValue("@CoVanDV", tableBaoGiaTam.Rows[0]["CoVanDV"].ToString());
                            cmd.Parameters.AddWithValue("@TruongPhongDV", tableBaoGiaTam.Rows[0]["TruongPhongDV"].ToString());

                            string IdBaoGia = cmd.ExecuteScalar().ToString();

                            foreach (DataRow row in tableBaoGiaCongThoTam.Rows)
                            {
                                cmd.CommandText = @"INSERT INTO BaoGiaCongTho
                                                    (IdBaoGia, IdTienCong, NoiDungCV, TienCong, GhiChu, DaThucHien)
                                                    VALUES (@IdBaoGia,@IdTienCong,@NoiDungCV,@TienCong,@GhiChu,@DaThucHien)";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdBaoGia", IdBaoGia);
                                cmd.Parameters.AddWithValue("@IdTienCong", row["IdTienCong"].ToString());
                                cmd.Parameters.AddWithValue("@NoiDungCV", row["NoiDungCV"].ToString());
                                cmd.Parameters.AddWithValue("@TienCong", row["TienCong"].ToString());
                                cmd.Parameters.AddWithValue("@GhiChu", row["GhiChu"].ToString());
                                cmd.Parameters.AddWithValue("@DaThucHien", row["DaThucHien"].ToString());

                                cmd.ExecuteNonQuery();
                            }

                            foreach (DataRow row in tableBaoGiaPhuTungTam.Rows)
                            {
                                cmd.CommandText = @"INSERT INTO BaoGiaPhuTung
                                                    (IdBaoGia, IdKho, IdPhuTung, MaPT, TenPT, DVT, SoLuong, DonGia, ThanhTien, DaThucHien)
                                                    VALUES (@IdBaoGia,@IdKho,@IdPhuTung,@MaPT,@TenPT,@DVT,@SoLuong,@DonGia,@ThanhTien,@DaThucHien)";
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@IdBaoGia", IdBaoGia);
                                cmd.Parameters.AddWithValue("@IdKho", row["IdKho"].ToString());
                                cmd.Parameters.AddWithValue("@IdPhuTung", row["IdPhuTung"].ToString());
                                cmd.Parameters.AddWithValue("@MaPT", row["MaPT"].ToString());
                                cmd.Parameters.AddWithValue("@TenPT", row["TenPT"].ToString());
                                cmd.Parameters.AddWithValue("@DVT", row["DVT"].ToString());
                                cmd.Parameters.AddWithValue("@SoLuong", row["SoLuong"].ToString());
                                cmd.Parameters.AddWithValue("@DonGia", row["DonGia"].ToString());
                                cmd.Parameters.AddWithValue("@ThanhTien", row["ThanhTien"].ToString());
                                cmd.Parameters.AddWithValue("@DaThucHien", row["DaThucHien"].ToString());

                                cmd.ExecuteNonQuery();
                            }
                        }

                        #endregion Lưu lịch sử báo giá

                        #region Xóa các bảng tạm

                        // Xóa lịch sử bảo dưỡng tạm
                        cmd.CommandText = "delete lichsubaoduongxetam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                        cmd.ExecuteNonQuery();

                        //Xóa lich sử bảo dưỡng chi tiết tam
                        cmd.CommandText = "delete lichsubaoduongchitiettam2 where Idbaoduong = @idbaoduong";
                        cmd.ExecuteNonQuery();

                        //Xóa việc theo giờ
                        cmd.CommandText = "delete ThoDichVu_GioViecTam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                        cmd.ExecuteNonQuery();

                        //Xóa việc theo tiền
                        cmd.CommandText = "delete ThoDichVu_TienCongThoTam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                        cmd.ExecuteNonQuery();

                        //Xóa việc thuê ngoài
                        cmd.CommandText = "delete ThueNgoaiTam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                        cmd.ExecuteNonQuery();

                        if (tableBaoGiaTam.Rows.Count > 0)
                        {
                            //Xóa báo giá công thợ tạm
                            cmd.CommandText = "DELETE FROM BaoGiaCongThoTam WHERE IdBaoGia = @IdBaoGia";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());

                            cmd.ExecuteNonQuery();

                            //Xóa báo giá phụ tùng tạm
                            cmd.CommandText = "DELETE FROM BaoGiaPhuTungTam WHERE IdBaoGia = @IdBaoGia";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());

                            cmd.ExecuteNonQuery();

                            //Xóa báo giá tạm
                            cmd.CommandText = "DELETE FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);

                            cmd.ExecuteNonQuery();
                        }

                        #endregion Xóa các bảng tạm

                        //Insert lich su bao duong phieu
                        if (check > 0)
                            cmd.CommandText = "insert into lichsubaoduongphieu(idbaoduong,sophieu,tongtien, TienCongTho, TienPT,NgayGiaoXe,IdCongTy,PhanTramGiam) values(@idbaoduong,@sophieu,@tongtien, @TienCongTho, @TienPT,DATEADD(hh,12,@NgayGiaoXe),@IdCongTy,@PhanTramGiam)";
                        else
                            cmd.CommandText = "insert into lichsubaoduongphieu(idbaoduong,sophieu,tongtien, TienCongTho, TienPT,NgayGiaoXe,IdCongTy,PhanTramGiam) values(@idbaoduong,@sophieu,@tongtien, @TienCongTho, @TienPT,@NgayGiaoXe,@IdCongTy,@PhanTramGiam)";

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@idbaoduong", idbaoduongxe);
                        cmd.Parameters.AddWithValue("@sophieu", txtSoPhieu.Text);
                        cmd.Parameters.AddWithValue("@tongtien", Convert.ToDouble(tienCongTho + tienPhuTung) * chietkhau);
                        cmd.Parameters.AddWithValue("@TienCongTho", Convert.ToDouble(tienCongTho) * chietkhau);
                        cmd.Parameters.AddWithValue("@TienPT", Convert.ToDouble(tienPhuTung) * chietkhau);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@PhanTramGiam", txtgiamtru.Text);
                        cmd.Parameters.AddWithValue("@NgayGiaoXe", _ngaygiaoxe);
                        cmd.ExecuteNonQuery();

                        //Insert Phiếu thu
                        if (check > 0)
                            cmd.CommandText = "Insert into PhieuThu(IdLoaiPhieuThu,SoTienThu,IdCongTy,IdCuaHang,IdNhanVien,NgayHachToan,SoHoaDon) Values(@idLoaiPhieuThu,@SoTienThu,@IdCongTy,@IdCuaHang,@IdNhanVien,DATEADD(hh,12,@NgayGiaoXe),@SoHoaDon)";
                        else
                            cmd.CommandText = "Insert into PhieuThu(IdLoaiPhieuThu,SoTienThu,IdCongTy,IdCuaHang,IdNhanVien,NgayHachToan,SoHoaDon) Values(@idLoaiPhieuThu,@SoTienThu,@IdCongTy,@IdCuaHang,@IdNhanVien,@NgayGiaoXe,@SoHoaDon)";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdLoaiPhieuThu", "5");
                        cmd.Parameters.AddWithValue("@SoTienThu", Convert.ToDouble(tienCongTho + tienThueNgoai + tienPhuTung) * chietkhau);
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                        cmd.Parameters.AddWithValue("@IdNhanVien", Class.EmployeeInfo.idnhanvien);
                        cmd.Parameters.AddWithValue("@SoHoaDon", idbaoduongxe);
                        cmd.Parameters.AddWithValue("@NgayGiaoXe", _ngaygiaoxe);
                        cmd.ExecuteNonQuery();

                        if (txtNguoiLapPhieu.Text.Trim() != "")
                        {
                            //Thêm người lập phiếu
                            cmd.CommandText = "INSERT INTO NguoiLapPhieu(IdBaoDuong, TenNguoiLapPhieu) VALUES(@IdBaoDuong, @TenNguoiLapPhieu)";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoDuong", idbaoduongxe);
                            cmd.Parameters.AddWithValue("@TenNguoiLapPhieu", txtNguoiLapPhieu.Text);
                            cmd.ExecuteNonQuery();
                        }

                        //Kiemtra();
                        tran.Commit();
                        cmd.Connection.Close();

                        dgvDSPT.DataSource = null;
                        dgvTheoSoTien.DataSource = null;
                        dgvSuaNgoai.DataSource = null;
                        dgvSoPhut.DataSource = null;
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        cmd.Connection.Close();
                        MessageBox.Show("Lưu thông tin thất bại. Lỗi :" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    buttonX3_Click(sender, new EventArgs());

                    DialogResult chon = MessageBox.Show("Lưu thông tin thành công." + Environment.NewLine + "Bạn có muốn in phiếu bảo dưỡng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (chon == DialogResult.Yes)
                    {
                        Class.SelectedCustomer._idbaoduong = idbaoduongxe;
                        if (Class.SelectedCustomer._idbaoduong == null)
                        { MessageBox.Show("Lần bảo dưỡng không tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                        else
                        {
                            if (Convert.ToInt64(Class.CompanyInfo.idcongty) != 31)
                            {
                                FrmPhieuSuaChuaThangLoi frm = new FrmPhieuSuaChuaThangLoi();
                                frm.ShowDialog();
                                //frmPhieuSuaChuaTM98 frm = new frmPhieuSuaChuaTM98();
                                //frm.ShowDialog();
                            }
                            else
                            {
                                frmPhieuSuaChuaTM98 frm = new frmPhieuSuaChuaTM98();
                                frm.ShowDialog();
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi :" + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        #endregion buttonItem6_Click

        #region buttonX2_Click

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn Hủy lần bảo dưỡng của Xe: " + txtBienSo1.Text, "Hủy lần bảo dưỡng", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
                if (txtID1.Text != "" && txtID1.Text != null)
                {
                    SqlCommand cmd = new SqlCommand();

                    cmd.CommandText = "SELECT * FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                    tableBaoGiaTam = Class.datatabase.getData(cmd);

                    cmd.Connection = Class.datatabase.getConnection();
                    if (cmd.Connection.State == 0)
                        cmd.Connection.Open();

                    SqlTransaction tran = cmd.Connection.BeginTransaction();
                    cmd.Transaction = tran;
                    try
                    {
                        cmd.CommandText = "delete from LichSuBaoDuongXeTam Where IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang and IdBaoDuong=@IdBaoDuong";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                        cmd.Parameters.AddWithValue("@IdCuaHang", Class.EmployeeInfo.IdCuaHang);
                        cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "delete lichsubaoduongchitiettam2 where IdBaoDuong=@IdBaoDuong";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "delete ThoDichVu_TienCongThoTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "delete ThoDichVu_GioViecTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                        cmd.ExecuteNonQuery();

                        cmd.CommandText = "delete ThueNgoaiTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                        cmd.ExecuteNonQuery();

                        if (tableBaoGiaTam.Rows.Count > 0)
                        {
                            //Xóa báo giá công thợ tạm
                            cmd.CommandText = "DELETE FROM BaoGiaCongThoTam WHERE IdBaoGia = @IdBaoGia";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());

                            cmd.ExecuteNonQuery();

                            //Xóa báo giá phụ tùng tạm
                            cmd.CommandText = "DELETE FROM BaoGiaPhuTungTam WHERE IdBaoGia = @IdBaoGia";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());

                            cmd.ExecuteNonQuery();

                            //Xóa báo giá tạm
                            cmd.CommandText = "DELETE FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoDuong", txtID1.Text);

                            cmd.ExecuteNonQuery();
                        }

                        tran.Commit();
                        MessageBox.Show("Hủy xe: " + txtBienSo1.Text + " thành công.", "Thông báo");
                    }
                    catch (Exception ex)
                    {
                        tran.Rollback();
                        MessageBox.Show("Thất bại." + ex.Message, "Thông báo");
                    }
                    finally
                    {
                        cmd.Connection.Close();
                        buttonX3_Click(sender, new EventArgs());
                    }
                }
                else
                {
                    MessageBox.Show("Hãy chọn Xe muốn hủy lần bảo dưỡng.");
                }
            }
        }

        #endregion buttonX2_Click

        #region buttonX3_Click

        private void buttonX3_Click(object sender, EventArgs e)
        {
            txtSoPhieu.Clear();
            txtTienPhuTung.Clear();
            txtTienTho.Clear();
            txtTienThueNgoai.Clear();
            txtgiamtru.Text = "0";
            txtTongTien.Clear();
            txtGhiChuBaoDuong.Clear();
            txtNguoiLapPhieu.Clear();

            txtID1.Clear();
            txtBienSo1.Clear();
            txtSoKhung1.Clear();
            txtSoMay1.Clear();
            txtGhiChuBaoDuong.Clear();

            dgvDSPT.Rows.Clear();
            dgvDSPT.Refresh();
            dgvSoPhut.Rows.Clear();
            dgvSoPhut.Refresh();
            dgvSuaNgoai.Rows.Clear();
            dgvSuaNgoai.Refresh();
            dgvTheoSoTien.Rows.Clear();
            dgvTheoSoTien.Refresh();

            LoadXeDangBaoDuong();

            dtKho = classdb.LoadTenKho();
            if (dtKho.Rows.Count > 0)
            {
                this.Kho6.DataSource = dtKho;
                this.Kho6.ValueMember = "IdKho";
                this.Kho6.DisplayMember = "TenKho";
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "Select MaPT, IDPT From PhuTung Where IdCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            DataTable dt = Class.datatabase.getData(cmd);

            MaPT.DataSource = dt;
            MaPT.DisplayMember = "MaPT";
            MaPT.ValueMember = "IdPT";

            DataTable dtThoSua = new DataTable();
            cmd.CommandText = "Select IDTho, MaTho + ' - ' + TenTho As TenTho From ThoDichVu Where IDCongTy = @IdCongTy";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dtThoSua = Class.datatabase.getData(cmd);

            if (dtThoSua.Rows.Count > 0)
            {
                Tho.DataSource = dtThoSua;
                Tho.ValueMember = "IdTho";
                Tho.DisplayMember = "TenTho";
            }
        }

        #endregion buttonX3_Click

        #region txtgiamtru_TextChanged

        private void txtgiamtru_TextChanged(object sender, EventArgs e)
        {
            if (txtgiamtru.Text != "" || txtgiamtru.Text != null)
            {
                try
                {
                    // decimal ia = decimal.Parse(txtgiamtru.Text);
                    if (txtgiamtru.Text != "" || txtgiamtru.Text != null)
                        chietkhau = 1 - double.Parse(txtgiamtru.Text) * 0.01;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Giảm trừ phải là số", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                try
                {
                    double tienPhuTung2 = double.Parse(txtTienPhuTung.Text);
                    double tienCongTho2 = double.Parse(txtTienTho.Text);
                    double tienThueNgoai2 = double.Parse(txtTienThueNgoai.Text);

                    txtTongTien.Text = string.Format("{0:0,0}", Convert.ToDouble(((tienPhuTung2 + tienThueNgoai2 + tienCongTho2) * chietkhau).ToString()));
                }
                catch { }
            }
            else
            {
                try
                {
                    double tienPhuTung2 = double.Parse(txtTienPhuTung.Text);
                    double tienCongTho2 = double.Parse(txtTienTho.Text);
                    double tienThueNgoai2 = double.Parse(txtTienThueNgoai.Text);
                    txtTongTien.Text = string.Format("{0:0,0}", Convert.ToDouble((tienPhuTung2 + tienThueNgoai2 + tienCongTho2).ToString()));
                }
                catch { }
            }
        }

        #endregion txtgiamtru_TextChanged

        #region txtBienSo1_KeyDown

        private void txtBienSo1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchDataOnGridView(sender);
        }

        #endregion txtBienSo1_KeyDown

        #region txtSoKhung1_KeyDown

        private void txtSoKhung1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchDataOnGridView(sender);
        }

        #endregion txtSoKhung1_KeyDown

        #region txtSoMay1_KeyDown

        private void txtSoMay1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchDataOnGridView(sender);
        }

        #endregion txtSoMay1_KeyDown

        #region SearchDataOnGridView

        private void SearchDataOnGridView(object sender)
        {
            string searchBienSoValue = txtBienSo1.Text;
            string searchSoKhungValue = txtSoKhung1.Text;
            string searchSoMayValue = txtSoMay1.Text;

            dgvDsXeDangBaoDuong1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

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

                dgvDsXeDangBaoDuong1.DataSource = tableResult;
            }
            catch
            {
                MessageBox.Show("Không tồn tại xe bảo dưỡng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                buttonX3_Click(sender, new EventArgs());
            }
        }

        #endregion SearchDataOnGridView
    }
}