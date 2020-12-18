using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AutoCareV2._0.Class;
using System.Windows.Threading;

namespace AutoCareV2._0
{
    public partial class FrmImportXeBaoDuong : Form
    {
        private string[] thangloi_company = { "22", "45", "46" };
        //quan
        private string[] vietA_company = { "102" };
        //end
        DataTable tableExcel;

        public FrmImportXeBaoDuong()
        {
            InitializeComponent();
        }

        #region mofileloi

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string path = openFileDialog1.FileName;
            txtFilePath.Text = path;
            bool hasHeaders = true;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (path.Substring(path.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            cbSheets.Items.Clear();
            foreach (DataRow schemaRow in schemaTable.Rows)
            {
                cbSheets.Items.Add(schemaRow["TABLE_NAME"].ToString());
            }
            conn.Close();
        }

        #endregion mofileloi

        private void txtFile_MouseDown(object sender, MouseEventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private DataTable exceldata(string filePath, string sheet)
        {
            DataTable dtexcel = new DataTable();
            bool hasHeaders = true;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();

            if (!sheet.EndsWith("_"))
            {
                string query = "SELECT  * FROM [" + sheet + "]";
                OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);
                dtexcel.Locale = CultureInfo.CurrentCulture;
                daexcel.Fill(dtexcel);
            }
            conn.Close();
            return dtexcel;
        }

        private void FrmImportXeBaoDuong_Load(object sender, EventArgs e)
        {
        }

        //bo code nay
        private void openFileDialog2_FileOk(object sender, CancelEventArgs e)
        {
            string path = openFileDialog2.FileName;
            txtFilePath.Text = path;
            bool hasHeaders = true;
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (path.Substring(path.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            cbSheets.Items.Clear();
            foreach (DataRow schemaRow in schemaTable.Rows)
            {
                cbSheets.Items.Add(schemaRow["TABLE_NAME"].ToString());
            }
            conn.Close();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            buttonX2.Enabled = true;
            if (txtFilePath.Text == "" || cbSheets.Text == "") 
            { 
                MessageBox.Show("Bạn chưa chọn file hoặc Sheet");
                return; 
            }

            tableExcel = exceldata(txtFilePath.Text, cbSheets.Text);
            dataGridView1.DataSource = tableExcel;

            if (tableExcel != null && tableExcel.Rows.Count > 0)
                LoadColumnToCombobox(tableExcel);
        }

        private void LoadColumnToCombobox(DataTable cot)
        {
            cboBienSoXe.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboBienSoXe.SelectedIndex = -1;

            cboCMND.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboCMND.SelectedIndex = -1;

            cboDiaChi.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboDiaChi.SelectedIndex = -1;

            cboGioiTinh.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboGioiTinh.SelectedIndex = -1;

            cboHoKhachHang.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboHoKhachHang.SelectedIndex = -1;

            cboKyThuatVien.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboKyThuatVien.SelectedIndex = -1;

            cboLoaiKH.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboLoaiKH.SelectedIndex = -1;

            cboMauXe.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboMauXe.SelectedIndex = -1;

            cboNgayBaoDuong.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboNgayBaoDuong.SelectedIndex = -1;

            cboNgayGiaoXe.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboNgayGiaoXe.SelectedIndex = -1;

            cboNgaySinh.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboNgaySinh.SelectedIndex = -1;

            cboSoDienThoai.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboSoDienThoai.SelectedIndex = -1;

            cboSoKhung.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboSoKhung.SelectedIndex = -1;

            cboSoLanBD.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboSoLanBD.SelectedIndex = -1;

            cboSoMay.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboSoMay.SelectedIndex = -1;

            cboSoKm.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboSoKm.SelectedIndex = -1;

            cboSoPhieu.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboSoPhieu.SelectedIndex = -1;

            cboSoSBH.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboSoSBH.SelectedIndex = -1;

            cboTenKhachHang.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboTenKhachHang.SelectedIndex = -1;

            cboTenXe.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboTenXe.SelectedIndex = -1;

            cboThayDau.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboThayDau.SelectedIndex = -1;

            cboTongTien.DataSource = cot.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboTongTien.SelectedIndex = -1;

            string[] loaibaoduong = { "Bảo dưỡng nặng", "Bảo dưỡng nhẹ" };
            cboLoaiBaoDuong.DataSource = loaibaoduong;
            cboLoaiBaoDuong.SelectedIndex = -1;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            buttonX2.Enabled = false;
            if (tableExcel != null || tableExcel.Rows.Count > 0)
                DoImport();
            else
                MessageBox.Show(@"Không tồn tại dữ liệu import!\nVui lòng kiểm tra lại.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            buttonX2.Enabled = true;
        }

        private void DoImport()
        {
            int countsuccess = 0; int countKHExists = 0; int countBDExists = 0; int countThayDauSMS = 0;
            int countfail = 0; string status = ""; int countsmsThanksBaoDuong = 0; int countsmsNextBaoDuong = 0;
            int dem = 0;

            int count = dataGridView1.Rows.Count;
            int dinhDang = 0;
            if (cbb_DinhDang.Text == @"Ngày/Tháng/Năm")
                dinhDang = 1;
            else if (cbb_DinhDang.Text == @"Tháng/Ngày/Năm")
                dinhDang = 2;

            if (dinhDang == 0)
            {
                MessageBox.Show(@"Bạn chưa chọn định dạng ngày tháng trong file excel.", @"Thông báo");
                return;
            }
            if (String.IsNullOrEmpty(cboSoDienThoai.Text))
            {
                MessageBox.Show(@"Số điện thoại khách hàng không được để trống!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (String.IsNullOrEmpty(cboNgayGiaoXe.Text))
            {
                MessageBox.Show(@"Ngày giao xe không được để trống!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {
                using (SqlConnection con = new SqlConnection(Class.datatabase.connect))
                {
                    con.Open();
                    if (count > 0)
                    {
                        progressBar1.Invoke(new Action(() =>
                        {
                            progressBar1.Visible = true;
                            progressBar1.Minimum = 0;
                            progressBar1.Value = 0;
                            progressBar1.Maximum = count;
                        }));
                    }

                    //cusinfo
                    string tenkh, hokh, ngaysinh, thangsinh, namsinh, gioitinh, dienthoai, cmnd, diachi, SoSBH, LoaiKH, LoaiBaoDuong;
                    tenkh = hokh = ngaysinh = thangsinh = namsinh = gioitinh = dienthoai = cmnd = diachi = SoSBH = LoaiKH = LoaiBaoDuong = "";
                    //xeinfo
                    string ngaybaoduong, ngaygiaoxe, tenxe, mauxe, bienso, sokhung, somay, sokm, solanbaoduong, sophieu, tongtien, thaydau, kythuatvien;
                    ngaybaoduong = ngaygiaoxe = tenxe = mauxe = bienso = sokhung = somay = sokm = solanbaoduong = sophieu = tongtien = thaydau = kythuatvien = "";

                    foreach (DataRow r in tableExcel.Rows)
                    {
                        //custinfo
                        try { hokh = r[cboHoKhachHang.Text].ToString(); }
                        catch { hokh = ""; }
                        try { tenkh = r[cboTenKhachHang.Text].ToString(); }
                        catch { tenkh = ""; }
                        try { ngaysinh = r[cboNgaySinh.Text].ToString(); }
                        catch { ngaysinh = ""; }
                        try { gioitinh = r[cboGioiTinh.Text].ToString(); }
                        catch { gioitinh = ""; }
                        try { dienthoai = r[cboSoDienThoai.Text].ToString(); }
                        catch { dienthoai = ""; }
                        try { cmnd = r[cboCMND.Text].ToString(); }
                        catch { cmnd = ""; }
                        try { diachi = r[cboDiaChi.Text].ToString(); }
                        catch { diachi = ""; }
                        try { SoSBH = r[cboSoSBH.Text].ToString(); }
                        catch { SoSBH = ""; }
                        try { LoaiKH = r[cboLoaiKH.Text].ToString(); }
                        catch { LoaiKH = "2"; }

                        string[] tmpNgaySinh;
                        try
                        {
                            tmpNgaySinh = ngaysinh.Split('/');
                            if (dinhDang == 1)
                            {
                                ngaysinh = tmpNgaySinh[0].ToString();
                                thangsinh = tmpNgaySinh[1].ToString();
                                namsinh = tmpNgaySinh[2].ToString().Split(' ')[0];
                            }
                            else if (dinhDang == 2)
                            {
                                ngaysinh = tmpNgaySinh[1].ToString();
                                thangsinh = tmpNgaySinh[0].ToString();
                                namsinh = tmpNgaySinh[2].ToString().Split(' ')[0];
                            }
                        }
                        catch { ngaysinh = ""; }

                        DateTime dtbirthday;
                        if (DateTime.TryParse(ngaysinh, out dtbirthday))
                        {
                            ngaysinh = dtbirthday.Day.ToString();
                            thangsinh = dtbirthday.Month.ToString();
                            namsinh = dtbirthday.Year.ToString();
                        }

                        try { ngaybaoduong = r[cboNgayBaoDuong.Text].ToString(); }
                        catch { ngaybaoduong = ""; }
                        try { ngaygiaoxe = r[cboNgayGiaoXe.Text].ToString(); }
                        catch { ngaygiaoxe = ""; }
                        try { tenxe = r[cboTenXe.Text].ToString(); }
                        catch { tenxe = ""; }
                        try { mauxe = r[cboMauXe.Text].ToString(); }
                        catch { mauxe = ""; }
                        try { bienso = r[cboBienSoXe.Text].ToString(); }
                        catch { bienso = ""; }
                        try { sokhung = r[cboSoKhung.Text].ToString(); }
                        catch { sokhung = ""; }
                        try { somay = r[cboSoMay.Text].ToString(); }
                        catch { somay = ""; }
                        try { sokm = r[cboSoKm.Text].ToString(); }
                        catch { sokm = ""; }
                        try { solanbaoduong = r[cboSoLanBD.Text].ToString(); }
                        catch { solanbaoduong = ""; }
                        try { sophieu = r[cboSoPhieu.Text].ToString(); }
                        catch { sophieu = ""; }
                        try { tongtien = r[cboTongTien.Text].ToString(); }
                        catch { tongtien = "0"; }
                        try { thaydau = r[cboThayDau.Text].ToString(); }
                        catch { thaydau = ""; }
                        try { kythuatvien = r[cboKyThuatVien.Text].ToString(); }
                        catch { kythuatvien = ""; }
                        try { LoaiBaoDuong = cboLoaiBaoDuong.Text == @"Bảo dưỡng nặng" ? Keywords.MaintenanceTypes.HevyMaintenance : Keywords.MaintenanceTypes.LightMaintenance;}
                        catch { LoaiBaoDuong = ""; }

                        string[] tmpNgayBaoDuong;
                        DateTime _dtngaybaoduong = new DateTime();
                        int _ngaybaoduong = 0, _thangbaoduong = 0, _nambaoduong = 0;
                        try
                        {
                            tmpNgayBaoDuong = ngaybaoduong.Split('/');
                            if (dinhDang == 1)
                            {
                                _ngaybaoduong = Convert.ToInt32(tmpNgayBaoDuong[0].ToString());
                                _thangbaoduong = Convert.ToInt32(tmpNgayBaoDuong[1].ToString());
                                _nambaoduong = Convert.ToInt32(tmpNgayBaoDuong[2].ToString().Split(' ')[0]);
                            }
                            else if (dinhDang == 2)
                            {
                                _ngaybaoduong = Convert.ToInt32(tmpNgayBaoDuong[1].ToString());
                                _thangbaoduong = Convert.ToInt32(tmpNgayBaoDuong[0].ToString());
                                _nambaoduong = Convert.ToInt32(tmpNgayBaoDuong[2].ToString().Split(' ')[0]);
                            }
                            _dtngaybaoduong = new DateTime(_nambaoduong, _thangbaoduong, _ngaybaoduong, 0, 0, 0);
                        }
                        catch { ngaybaoduong = ""; }

                        string[] tmpNgayGiaoXe;
                        DateTime _dtngaygiaoxe = new DateTime();
                        int _ngaygiaoxe = 0, _thanggiaoxe = 0, _namgiaoxe = 0;
                        try
                        {
                            tmpNgayGiaoXe = ngaygiaoxe.Split('/');
                            if (dinhDang == 1)
                            {
                                _ngaygiaoxe = Convert.ToInt32(tmpNgayGiaoXe[0].ToString());
                                _thanggiaoxe = Convert.ToInt32(tmpNgayGiaoXe[1].ToString());
                                _namgiaoxe = Convert.ToInt32(tmpNgayGiaoXe[2].ToString().Split(' ')[0]);
                            }
                            else if (dinhDang == 2)
                            {
                                _ngaygiaoxe = Convert.ToInt32(tmpNgayGiaoXe[1].ToString());
                                _thanggiaoxe = Convert.ToInt32(tmpNgayGiaoXe[0].ToString());
                                _namgiaoxe = Convert.ToInt32(tmpNgayGiaoXe[2].ToString().Split(' ')[0]);
                            }
                            _dtngaygiaoxe = new DateTime(_namgiaoxe, _thanggiaoxe, _ngaygiaoxe, 0, 0, 0);
                        }
                        catch { ngaygiaoxe = ""; }

                        SqlCommand cmd = new SqlCommand();
                        cmd.Connection = con;

                        #region Thắng lợi
                        if (thangloi_company.Any(Class.CompanyInfo.idcongty.Contains))
                        {
                            cmd.CommandText = "ImportXeBaoDuong_PhanLoai_ThangLoi";
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@HoKH ", hokh);
                            cmd.Parameters.AddWithValue("@TenKH", tenkh);
                            cmd.Parameters.AddWithValue("@GioiTinh", gioitinh);
                            if (ngaysinh == "")
                                cmd.Parameters.AddWithValue("@NgaySinh", SqlDateTime.Null);
                            else
                                cmd.Parameters.AddWithValue("@NgaySinh", namsinh + "/" + thangsinh + "/" + ngaysinh);
                            cmd.Parameters.AddWithValue("@DienThoai", dienthoai);
                            cmd.Parameters.AddWithValue("@DiaChi", diachi);
                            cmd.Parameters.AddWithValue("@CMND", cmnd);
                            cmd.Parameters.AddWithValue("@SoSBH", SoSBH);
                            cmd.Parameters.AddWithValue("@LoaiKH", LoaiKH);

                            if (ngaybaoduong == "")
                                cmd.Parameters.AddWithValue("@NgayBaoDuong", SqlDateTime.Null);
                            else
                                cmd.Parameters.AddWithValue("@NgayBaoDuong", _dtngaybaoduong);

                            if (ngaygiaoxe == "")
                                cmd.Parameters.AddWithValue("@NgayGiaoXe", SqlDateTime.Null);
                            else
                                cmd.Parameters.AddWithValue("@NgayGiaoXe", _dtngaygiaoxe);

                            cmd.Parameters.AddWithValue("@TenXe", tenxe);
                            cmd.Parameters.AddWithValue("@MauXe", mauxe);
                            cmd.Parameters.AddWithValue("@BienSo", bienso);
                            cmd.Parameters.AddWithValue("@SoKhung", sokhung);
                            cmd.Parameters.AddWithValue("@SoMay", somay);
                            cmd.Parameters.AddWithValue("@SoKM", sokm);
                            cmd.Parameters.AddWithValue("@SoLan", solanbaoduong);
                            cmd.Parameters.AddWithValue("@SoPhieu", sophieu);//9 tham so
                            cmd.Parameters.AddWithValue("@TongTien", tongtien); //1
                            cmd.Parameters.AddWithValue("@ThayDau", thaydau);
                            cmd.Parameters.AddWithValue("@LoaiBaoDuong", LoaiBaoDuong);
                            cmd.Parameters.AddWithValue("@KyThuatVien", kythuatvien);

                            string[] obj; // mảng chứa idkhachhang|idbaoduong|isExistKhachHang|isExistsBaoDuong.

                            try
                            {
                                object result = cmd.ExecuteScalar();
                                dem++;
                                progressBar1.Invoke(new Action(() => progressBar1.Value = dem));
                                if (result != null && result.ToString() != "")
                                {
                                    countsuccess++; // dem so khach hang import thanh cong
                                    obj = result.ToString().Split('|');
                                    string idkhachang = obj[0];
                                    string idbaoduong = obj[1];
                                    countKHExists += (obj[2] == "yes") ? 1 : 0;
                                    countBDExists += (obj[3] == "yes") ? 1 : 0;

                                    if (obj != null && idkhachang != "0" && idbaoduong != "0")
                                    {
                                        string sendername = Class.CompanyInfo.sendername;
                                        string idcongty = Class.CompanyInfo.idcongty;

                                        DataTable tblCamOnBDConfig = new DataTable();
                                        SqlDataAdapter daCamOnBD = new SqlDataAdapter("Select top 1 * from SMSKiemTraBaoDuongConfig where idcongty=" + idcongty, con);
                                        daCamOnBD.Fill(tblCamOnBDConfig);

                                        #region nhancamonbaoduong
                                        if (Class.CompanyInfo.idcongty == "8")// nhan tin cho khach hang cam on da den bao duong cho thai honda
                                        {
                                            object sms = new SqlCommand("select top 1 sms from smsconfig where type='Cam on bao duong' and idcongty=" + idcongty, con).ExecuteScalar();
                                            if (sms != null && sms.ToString() != "")
                                            {
                                                string resms = Utilities.Smsreplace(sms.ToString(), hokh + " " + tenkh, namsinh + "/" + thangsinh + "/" + ngaysinh, sendername, tenxe, bienso, sokhung, somay, dienthoai, solanbaoduong, _dtngaybaoduong.ToString());
                                                bool isUnicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                                byte countmes = Utilities.CountMess(resms, isUnicode);
                                                cmd = new SqlCommand("Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,isUnicode)values(@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang, @isUnicode)", con);
                                                cmd.Parameters.Clear();
                                                cmd.Parameters.AddWithValue("@sendername", sendername);
                                                cmd.Parameters.AddWithValue("@phone", dienthoai);
                                                cmd.Parameters.AddWithValue("@sms", resms);
                                                cmd.Parameters.AddWithValue("@countmes", countmes);
                                                cmd.Parameters.AddWithValue("@smstype", "Cam on bao duong");
                                                cmd.Parameters.AddWithValue("@idcongty", idcongty);
                                                cmd.Parameters.AddWithValue("@idkhachhang", obj[0]);
                                                cmd.Parameters.AddWithValue("@isUnicode", isUnicode);
                                                if (cmd.ExecuteNonQuery() > 0)
                                                    countsmsThanksBaoDuong++;
                                            }
                                        }
                                        else if (tblCamOnBDConfig != null && tblCamOnBDConfig.Rows.Count > 0) //nhắn tin cám ơn bảo dưỡng nếu có cấu hình
                                        {
                                            int flag;

                                            if (int.TryParse(tblCamOnBDConfig.Rows[0]["GioNhan"].ToString(), out flag) && int.TryParse(tblCamOnBDConfig.Rows[0]["NhanSauKhiBaoDuong"].ToString(), out flag))
                                            {
                                                if (!String.IsNullOrEmpty(ngaygiaoxe))
                                                {
                                                    DateTime timeSchedule = _dtngaygiaoxe.AddDays(int.Parse(tblCamOnBDConfig.Rows[0]["NhanSauKhiBaoDuong"].ToString())).AddHours(int.Parse(tblCamOnBDConfig.Rows[0]["GioNhan"].ToString()));

                                                    if (timeSchedule != null && timeSchedule.Date >= DateTime.Now.Date)
                                                    {
                                                        object sms = new SqlCommand("select top 1 sms from smsconfig where type='Cam on bao duong' and idcongty=" + idcongty, con).ExecuteScalar();
                                                        if (sms != null && sms.ToString() != "")
                                                        {
                                                            string resms = Utilities.Smsreplace(sms.ToString(), hokh + " " + tenkh, namsinh + "/" + thangsinh + "/" + ngaysinh, sendername, tenxe, bienso, sokhung, somay, dienthoai, solanbaoduong, _dtngaybaoduong.ToString());
                                                            bool isUnicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                                            byte countmes = Utilities.CountMess(resms, isUnicode);
                                                            cmd = new SqlCommand("Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,isUnicode, timeSchedule)values(@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang, @isUnicode, @timeSchedule)", con);
                                                            cmd.Parameters.Clear();
                                                            cmd.Parameters.AddWithValue("@sendername", sendername);
                                                            cmd.Parameters.AddWithValue("@phone", dienthoai);
                                                            cmd.Parameters.AddWithValue("@sms", resms);
                                                            cmd.Parameters.AddWithValue("@countmes", countmes);
                                                            cmd.Parameters.AddWithValue("@smstype", "Cam on bao duong");
                                                            cmd.Parameters.AddWithValue("@idcongty", idcongty);
                                                            cmd.Parameters.AddWithValue("@idkhachhang", obj[0]);
                                                            cmd.Parameters.AddWithValue("@isUnicode", isUnicode);
                                                            cmd.Parameters.AddWithValue("@timeSchedule", timeSchedule);
                                                            if (cmd.ExecuteNonQuery() > 0)
                                                                countsmsThanksBaoDuong++;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (int.Parse(tongtien) >= Class.CompanyInfo.sotiennhantinbaoduong) // nhan tin cho khach hang cam on da den bao duong theo so tien
                                        {
                                            object sms = new SqlCommand("select top 1 sms from smsconfig where type='Cam on bao duong' and idcongty=" + idcongty, con).ExecuteScalar();
                                            if (sms != null && sms.ToString() != "")
                                            {
                                                string resms = Utilities.Smsreplace(sms.ToString(), hokh + " " + tenkh, namsinh + "/" + thangsinh + "/" + ngaysinh, sendername, tenxe, bienso, sokhung, somay, dienthoai, solanbaoduong, _dtngaybaoduong.ToString());
                                                bool isUnicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                                byte countmes = Utilities.CountMess(resms, isUnicode);
                                                cmd = new SqlCommand("Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,isUnicode)values(@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang, @isUnicode)", con);
                                                cmd.Parameters.Clear();
                                                cmd.Parameters.AddWithValue("@sendername", sendername);
                                                cmd.Parameters.AddWithValue("@phone", dienthoai);
                                                cmd.Parameters.AddWithValue("@sms", resms);
                                                cmd.Parameters.AddWithValue("@countmes", countmes);
                                                cmd.Parameters.AddWithValue("@smstype", "Cam on bao duong");
                                                cmd.Parameters.AddWithValue("@idcongty", idcongty);
                                                cmd.Parameters.AddWithValue("@idkhachhang", obj[0]);
                                                cmd.Parameters.AddWithValue("@isUnicode", isUnicode);
                                                if (cmd.ExecuteNonQuery() > 0)
                                                    countsmsThanksBaoDuong++;
                                            }
                                        }
                                        #endregion

                                        //nhan tin moi thay dau dot tiep neu thoa mãn.
                                        #region nhantinthaydau
                                        //if (thaydau.ToUpper() == "1" || thaydau.ToUpper() == "CO" || thaydau.ToUpper() == "YES" || thaydau.ToUpper() == "TRUE" || thaydau.ToUpper() == "OK")
                                        //{
                                        if (chk_NhanThayDau.Checked)
                                        {
                                            using (DataTable tblThayDauConfig = new DataTable())
                                            {
                                                SqlDataAdapter da = new SqlDataAdapter("Select top 1 * from SMSMoiThayDau_Config where idcongty=" + idcongty + " and active=1", con);
                                                object sms = new SqlCommand("select sms from SMSConfig where idcongty=" + idcongty + " and Type='Thay dau'", con).ExecuteScalar();

                                                da.Fill(tblThayDauConfig);
                                                if (tblThayDauConfig.Rows.Count > 0 && sms != null)
                                                {
                                                    string nhansausongay = tblThayDauConfig.Rows[0]["nhansausongay"].ToString();
                                                    string gionhan = tblThayDauConfig.Rows[0]["gionhan"].ToString();

                                                    //string _ngaygiaoxe = namgiaoxe + "/" + thanggiaoxe + "/" + ngaygiaoxe;

                                                    DateTime dt2;

                                                    if (ngaygiaoxe != "")
                                                    {
                                                        dt2 = _dtngaygiaoxe;

                                                        //DateTime tmp = dt2;
                                                        DateTime d = dt2.AddDays(int.Parse(nhansausongay));
                                                        DateTime timechedule = new DateTime(d.Year, d.Month, d.Day, int.Parse(gionhan), 0, 0, 0);

                                                        //tienlm - 20150917 - chỉ gửi tin nhắn nếu lịch gửi >= ngày hiện tại
                                                        if (timechedule != null && timechedule.Date >= DateTime.Now.Date)
                                                        {
                                                            string resms = Utilities.Smsreplace(sms.ToString(), hokh + " " + tenkh, namsinh + "/" + thangsinh + "/" + ngaysinh, sendername, tenxe, bienso, sokhung, somay, dienthoai, solanbaoduong, _dtngaybaoduong.ToString());
                                                            bool isunicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                                            string sql = @"Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule) values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)"
                                                                + " update LichSuBaoDuongXe set thaydau=1 where idbaoduong=" + idbaoduong;
                                                            cmd = new SqlCommand(sql, con); cmd.Parameters.Clear();
                                                            cmd.Parameters.AddWithValue("@sendername", sendername);
                                                            cmd.Parameters.AddWithValue("@phone", dienthoai);
                                                            cmd.Parameters.AddWithValue("@sms", resms);
                                                            cmd.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                                                            cmd.Parameters.AddWithValue("@smstype", "Thay dau");
                                                            cmd.Parameters.AddWithValue("@idcongty", idcongty);
                                                            cmd.Parameters.AddWithValue("@idkhachhang", idkhachang);
                                                            cmd.Parameters.AddWithValue("@timeSchedule", timechedule);

                                                            if (cmd.ExecuteNonQuery() > 0) countThayDauSMS++;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        //}
                                        #endregion
                                    }
                                }
                                else
                                    countfail++;
                            }
                            catch (Exception ex)
                            {
                                status += "\n" + hokh + " " + tenkh + " | " + ex.Message;
                            }
                        }
                        #endregion

                        //quan
                        #region Việt Á
                        else if (vietA_company.Any(Class.CompanyInfo.idcongty.Contains))
                        {
                            cmd.CommandText = "Quan_ImportXeBaoDuong_PhanLoai_VietA";
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@HoKH ", hokh);
                            cmd.Parameters.AddWithValue("@TenKH", tenkh);
                            cmd.Parameters.AddWithValue("@GioiTinh", gioitinh);

                            if (ngaysinh == "")
                            {
                                cmd.Parameters.AddWithValue("@NgaySinh", SqlDateTime.Null);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@NgaySinh", namsinh + "/" + thangsinh + "/" + ngaysinh);
                            }
                            cmd.Parameters.AddWithValue("@DienThoai", dienthoai);
                            cmd.Parameters.AddWithValue("@DiaChi", diachi);
                            cmd.Parameters.AddWithValue("@CMND", cmnd);

                            cmd.Parameters.AddWithValue("@SoSBH", SoSBH);
                            cmd.Parameters.AddWithValue("@LoaiKH", LoaiKH);
                            if (ngaybaoduong == "")
                            {
                                cmd.Parameters.AddWithValue("@NgayBaoDuong", SqlDateTime.Null);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@NgayBaoDuong", _dtngaybaoduong);
                            }

                            if (ngaygiaoxe == "")
                            {
                                cmd.Parameters.AddWithValue("@NgayGiaoXe", SqlDateTime.Null);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@NgayGiaoXe", _dtngaygiaoxe);
                            }

                            cmd.Parameters.AddWithValue("@TenXe", tenxe);
                            cmd.Parameters.AddWithValue("@MauXe", mauxe);
                            cmd.Parameters.AddWithValue("@BienSo", bienso);
                            cmd.Parameters.AddWithValue("@SoKhung", sokhung);
                            cmd.Parameters.AddWithValue("@SoMay", somay);
                            cmd.Parameters.AddWithValue("@SoKM", sokm);
                            cmd.Parameters.AddWithValue("@SoLan", solanbaoduong);
                            cmd.Parameters.AddWithValue("@SoPhieu", sophieu);//9 tham so
                            cmd.Parameters.AddWithValue("@TongTien", tongtien); //1
                            cmd.Parameters.AddWithValue("@ThayDau", thaydau);
                            cmd.Parameters.AddWithValue("@LoaiBaoDuong", LoaiBaoDuong);

                            string[] obj; // mảng chứa idkhachhang|idbaoduong|isExistKhachHang|isExistsBaoDuong.

                            try
                            {
                                object result = cmd.ExecuteScalar();
                                dem++;
                                progressBar1.Invoke(new Action(() => progressBar1.Value = dem));
                                if (result != null && result.ToString() != "")
                                {
                                    countsuccess++; // dem so khach hang import thanh cong
                                    obj = result.ToString().Split('|');
                                    string idkhachang = obj[0];
                                    string idbaoduong = obj[1];
                                    countKHExists += (obj[2] == "yes") ? 1 : 0;
                                    countBDExists += (obj[3] == "yes") ? 1 : 0;

                                    if (obj != null && idkhachang != "0" && idbaoduong != "0")
                                    {
                                        string sendername = Class.CompanyInfo.sendername;
                                        string idcongty = Class.CompanyInfo.idcongty;

                                        DataTable tblCamOnBDConfig = new DataTable();
                                        SqlDataAdapter daCamOnBD = new SqlDataAdapter("Select top 1 * from SMSKiemTraBaoDuongConfig where idcongty=" + idcongty, con);
                                        daCamOnBD.Fill(tblCamOnBDConfig);

                                        #region nhancamonbaoduong
                                        //if (Class.CompanyInfo.idcongty == "8")// nhan tin cho khach hang cam on da den bao duong cho thai honda
                                        //{
                                        //    object sms = new SqlCommand("select top 1 sms from smsconfig where type='Cam on bao duong' and idcongty=" + idcongty, con).ExecuteScalar();
                                        //    if (sms != null && sms.ToString() != "")
                                        //    {
                                        //        string resms = Utilities.Smsreplace(sms.ToString(), hokh + " " + tenkh,
                                        //            namsinh + "/" + thangsinh + "/" + ngaysinh, sendername, tenxe,
                                        //            bienso, sokhung, somay, dienthoai, solanbaoduong,
                                        //            _dtngaybaoduong.ToString("dd/MM/yyyy"));

                                        //        bool isUnicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                        //        byte countmes = Utilities.CountMess(resms, isUnicode);
                                        //        cmd = new SqlCommand("sp_TinNhan_InsertWithCheckLoop", con);
                                        //        cmd.Parameters.Clear();
                                        //        cmd.Parameters.AddWithValue("@sendername", sendername);
                                        //        cmd.Parameters.AddWithValue("@phone", dienthoai);
                                        //        cmd.Parameters.AddWithValue("@sms", resms);
                                        //        cmd.Parameters.AddWithValue("@countmes", countmes);
                                        //        cmd.Parameters.AddWithValue("@smstype", "Cam on bao duong");
                                        //        cmd.Parameters.AddWithValue("@idcongty", idcongty);
                                        //        cmd.Parameters.AddWithValue("@idkhachhang", obj[0]);
                                        //        cmd.Parameters.AddWithValue("@timeSchedule", DateTime.Now);
                                        //        cmd.Parameters.AddWithValue("@isUnicode", isUnicode);
                                        //        if (cmd.ExecuteNonQuery() > 0)
                                        //            countsmsThanksBaoDuong++;
                                        //    }
                                        //}
                                        //else 
                                        if (tblCamOnBDConfig != null && tblCamOnBDConfig.Rows.Count > 0) //nhắn tin cám ơn bảo dưỡng nếu có cấu hình
                                        {
                                            int flag;

                                            if (int.TryParse(tblCamOnBDConfig.Rows[0]["GioNhan"].ToString(), out flag) && int.TryParse(tblCamOnBDConfig.Rows[0]["NhanSauKhiBaoDuong"].ToString(), out flag))
                                            {
                                                if (!String.IsNullOrEmpty(ngaygiaoxe))
                                                {
                                                    DateTime timeSchedule = _dtngaygiaoxe.AddDays(int.Parse(tblCamOnBDConfig.Rows[0]["NhanSauKhiBaoDuong"].ToString())).AddHours(int.Parse(tblCamOnBDConfig.Rows[0]["GioNhan"].ToString()));

                                                    if (timeSchedule != null && timeSchedule.Date >= DateTime.Now.Date)
                                                    {
                                                        object sms = new SqlCommand("select top 1 sms from smsconfig where type='Cam on bao duong' and idcongty=" + idcongty, con).ExecuteScalar();
                                                        if (sms != null && sms.ToString() != "")
                                                        {
                                                            string resms = Utilities.Smsreplace(sms.ToString(), hokh + " " + tenkh, namsinh + "/" + thangsinh + "/" + ngaysinh, sendername, tenxe, bienso, sokhung, somay, dienthoai, solanbaoduong, _dtngaybaoduong.ToString("dd/MM/yyyy"));
                                                            bool isUnicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                                            byte countmes = Utilities.CountMess(resms, isUnicode);
                                                            SqlCommand cmdSms = new SqlCommand("sp_TinNhan_InsertWithCheckLoop", con);
                                                            cmdSms.Parameters.Clear();
                                                            cmdSms.CommandType = CommandType.StoredProcedure;
                                                            cmdSms.Parameters.AddWithValue("@sendername", sendername);
                                                            cmdSms.Parameters.AddWithValue("@phone", dienthoai);
                                                            cmdSms.Parameters.AddWithValue("@sms", resms);
                                                            cmdSms.Parameters.AddWithValue("@countmes", countmes);
                                                            cmdSms.Parameters.AddWithValue("@smstype", "Cam on bao duong");
                                                            cmdSms.Parameters.AddWithValue("@idcongty", idcongty);
                                                            cmdSms.Parameters.AddWithValue("@idkhachhang", obj[0]);
                                                            cmdSms.Parameters.AddWithValue("@isUnicode", isUnicode);
                                                            cmdSms.Parameters.AddWithValue("@timeSchedule", timeSchedule);
                                                            if (cmdSms.ExecuteNonQuery() > 0)
                                                                countsmsThanksBaoDuong++;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (int.Parse(tongtien) >= Class.CompanyInfo.sotiennhantinbaoduong) // nhan tin cho khach hang cam on da den bao duong theo so tien
                                        {
                                            object sms = new SqlCommand("select top 1 sms from smsconfig where type='Cam on bao duong' and idcongty=" + idcongty, con).ExecuteScalar();
                                            if (sms != null && sms.ToString() != "")
                                            {
                                                string resms = Utilities.Smsreplace(sms.ToString(), hokh + " " + tenkh, namsinh + "/" + thangsinh + "/" + ngaysinh, sendername, tenxe, bienso, sokhung, somay, dienthoai, solanbaoduong, _dtngaybaoduong.ToString());
                                                bool isUnicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                                byte countmes = Utilities.CountMess(resms, isUnicode);
                                                var cmdSms = new SqlCommand("sp_TinNhan_InsertWithCheckLoop", con);
                                                cmdSms.Parameters.Clear();
                                                cmdSms.Parameters.AddWithValue("@sendername", sendername);
                                                cmdSms.Parameters.AddWithValue("@phone", dienthoai);
                                                cmdSms.Parameters.AddWithValue("@sms", resms);
                                                cmdSms.Parameters.AddWithValue("@countmes", countmes);
                                                cmdSms.Parameters.AddWithValue("@smstype", "Cam on bao duong");
                                                cmdSms.Parameters.AddWithValue("@idcongty", idcongty);
                                                cmdSms.Parameters.AddWithValue("@idkhachhang", obj[0]);
                                                cmdSms.Parameters.AddWithValue("@isUnicode", isUnicode);
                                                if (cmdSms.ExecuteNonQuery() > 0)
                                                    countsmsThanksBaoDuong++;
                                            }
                                        }
                                        #endregion

                                        //nhan tin moi thay dau dot tiep neu thoa mãn.
                                        #region nhantinthaydau
                                        //if (thaydau.ToUpper() == "1" || thaydau.ToUpper() == "CO" || thaydau.ToUpper() == "YES" || thaydau.ToUpper() == "TRUE" || thaydau.ToUpper() == "OK")
                                        //{
                                        if (chk_NhanThayDau.Checked || chkChiNhanTinThayDau.Checked)
                                        {
                                            using (DataTable tblThayDauConfig = new DataTable())
                                            {
                                                SqlDataAdapter da = new SqlDataAdapter("Select top 1 * from SMSMoiThayDau_Config where idcongty=" + idcongty + " and active=1", con);
                                                object sms = new SqlCommand("select sms from SMSConfig where idcongty=" + idcongty + " and Type='Thay dau'", con).ExecuteScalar();

                                                da.Fill(tblThayDauConfig);
                                                if (tblThayDauConfig.Rows.Count > 0 && sms != null)
                                                {
                                                    string nhansausongay = tblThayDauConfig.Rows[0]["nhansausongay"].ToString();
                                                    string gionhan = tblThayDauConfig.Rows[0]["gionhan"].ToString();

                                                    //string _ngaygiaoxe = namgiaoxe + "/" + thanggiaoxe + "/" + ngaygiaoxe;

                                                    DateTime dt2;

                                                    if (ngaygiaoxe != "")
                                                    {
                                                        dt2 = _dtngaygiaoxe;

                                                        //DateTime tmp = dt2;
                                                        DateTime d = dt2.AddDays(int.Parse(nhansausongay));
                                                        DateTime timechedule = new DateTime(d.Year, d.Month, d.Day, int.Parse(gionhan), 0, 0, 0);

                                                        //tienlm - 20150917 - chỉ gửi tin nhắn nếu lịch gửi >= ngày hiện tại
                                                        if (timechedule != null && timechedule.Date >= DateTime.Now.Date)
                                                        {
                                                            string resms = Utilities.Smsreplace(sms.ToString(),
                                                                hokh + " " + tenkh,
                                                                namsinh + "/" + thangsinh + "/" + ngaysinh, sendername,
                                                                tenxe, bienso, sokhung, somay, dienthoai, solanbaoduong,
                                                                timechedule.ToString("dd/MM/yyyy"));

                                                            bool isunicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                                            string sql = " if not exists (select smsid from TinNhan with (nolock) where phone = @phone "
                                                                         +
                                                                         " and sms = @sms and SenderName = @sendername "
                                                                         +
                                                                         " and smstype = @smstype and IdCongTy = @idcongty and IdKhachHang = @idkhachhang "
                                                                         + " and timeSchedule = @timeSchedule) begin"
                                                                         +
                                                                         " Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule) "
                                                                         +
                                                                         " values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)"
                                                                         +
                                                                         "; end update LichSuBaoDuongXe set thaydau=1 where idbaoduong=@idbaoduong"
                                                                         +
                                                                         "; update KhachHang set NhanTinThayDau = @nhantinthaydau, ChiNhanTinThayDau = @chinhantinthaydau "
                                                                         +
                                                                         " where IdKhachHang = @idkhachang and idcongty = @idcongty";
                                                            cmd = new SqlCommand(sql, con); cmd.Parameters.Clear();
                                                            cmd.Parameters.AddWithValue("@sendername", sendername);
                                                            cmd.Parameters.AddWithValue("@phone", dienthoai);
                                                            cmd.Parameters.AddWithValue("@sms", resms);
                                                            cmd.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                                                            cmd.Parameters.AddWithValue("@smstype", "Thay dau");
                                                            cmd.Parameters.AddWithValue("@idcongty", idcongty);
                                                            cmd.Parameters.AddWithValue("@idkhachhang", idkhachang);
                                                            cmd.Parameters.AddWithValue("@timeSchedule", timechedule);
                                                            cmd.Parameters.AddWithValue("@idbaoduong", idbaoduong);
                                                            cmd.Parameters.AddWithValue("@idkhachang", idkhachang);
                                                            cmd.Parameters.AddWithValue("@nhantinthaydau", chk_NhanThayDau.Checked);
                                                            cmd.Parameters.AddWithValue("@chinhantinthaydau", chkChiNhanTinThayDau.Checked);

                                                            if (cmd.ExecuteNonQuery() > 0) countThayDauSMS++;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        //}
                                        #endregion
                                    }
                                }
                                else
                                    countfail++;
                            }
                            catch (Exception ex)
                            {
                                status += "\n" + hokh + " " + tenkh + " | " + ex.Message;
                            }
                        }
                        #endregion
                        //end

                        #region khác
                        else
                        {
                            cmd.CommandText = "ImportXeBaoDuong_PhanLoai";
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@IdCongty", Class.CompanyInfo.idcongty);
                            cmd.Parameters.AddWithValue("@HoKH ", hokh);
                            cmd.Parameters.AddWithValue("@TenKH", tenkh);
                            cmd.Parameters.AddWithValue("@GioiTinh", gioitinh);

                            if (ngaysinh == "")
                            {
                                cmd.Parameters.AddWithValue("@NgaySinh", SqlDateTime.Null);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@NgaySinh", namsinh + "/" + thangsinh + "/" + ngaysinh);
                            }
                            cmd.Parameters.AddWithValue("@DienThoai", dienthoai);
                            cmd.Parameters.AddWithValue("@DiaChi", diachi);
                            cmd.Parameters.AddWithValue("@CMND", cmnd);

                            cmd.Parameters.AddWithValue("@SoSBH", SoSBH);
                            cmd.Parameters.AddWithValue("@LoaiKH", LoaiKH);
                            if (ngaybaoduong == "")
                            {
                                cmd.Parameters.AddWithValue("@NgayBaoDuong", SqlDateTime.Null);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@NgayBaoDuong", _dtngaybaoduong);
                            }

                            if (ngaygiaoxe == "")
                            {
                                cmd.Parameters.AddWithValue("@NgayGiaoXe", SqlDateTime.Null);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@NgayGiaoXe", _dtngaygiaoxe);
                            }

                            cmd.Parameters.AddWithValue("@TenXe", tenxe);
                            cmd.Parameters.AddWithValue("@MauXe", mauxe);
                            cmd.Parameters.AddWithValue("@BienSo", bienso);
                            cmd.Parameters.AddWithValue("@SoKhung", sokhung);
                            cmd.Parameters.AddWithValue("@SoMay", somay);
                            cmd.Parameters.AddWithValue("@SoKM", sokm);
                            cmd.Parameters.AddWithValue("@SoLan", solanbaoduong);
                            cmd.Parameters.AddWithValue("@SoPhieu", sophieu);//9 tham so
                            cmd.Parameters.AddWithValue("@TongTien", tongtien); //1
                            cmd.Parameters.AddWithValue("@ThayDau", thaydau);
                            cmd.Parameters.AddWithValue("@LoaiBaoDuong", LoaiBaoDuong);

                            string[] obj; // mảng chứa idkhachhang|idbaoduong|isExistKhachHang|isExistsBaoDuong.

                            try
                            {
                                object result = cmd.ExecuteScalar();
                                dem++;
                                progressBar1.Invoke(new Action(() => progressBar1.Value = dem));
                                if (result != null && result.ToString() != "")
                                {
                                    countsuccess++; // dem so khach hang import thanh cong
                                    obj = result.ToString().Split('|');
                                    string idkhachang = obj[0];
                                    string idbaoduong = obj[1];
                                    countKHExists += (obj[2] == "yes") ? 1 : 0;
                                    countBDExists += (obj[3] == "yes") ? 1 : 0;

                                    if (obj != null && idkhachang != "0" && idbaoduong != "0")
                                    {
                                        string sendername = Class.CompanyInfo.sendername;
                                        string idcongty = Class.CompanyInfo.idcongty;

                                        DataTable tblCamOnBDConfig = new DataTable();
                                        SqlDataAdapter daCamOnBD = new SqlDataAdapter("Select top 1 * from SMSKiemTraBaoDuongConfig where idcongty=" + idcongty, con);
                                        daCamOnBD.Fill(tblCamOnBDConfig);

                                        #region nhancamonbaoduong
                                        //if (Class.CompanyInfo.idcongty == "8")// nhan tin cho khach hang cam on da den bao duong cho thai honda
                                        //{
                                        //    object sms = new SqlCommand("select top 1 sms from smsconfig where type='Cam on bao duong' and idcongty=" + idcongty, con).ExecuteScalar();
                                        //    if (sms != null && sms.ToString() != "")
                                        //    {
                                        //        string resms = Utilities.Smsreplace(sms.ToString(), hokh + " " + tenkh,
                                        //            namsinh + "/" + thangsinh + "/" + ngaysinh, sendername, tenxe,
                                        //            bienso, sokhung, somay, dienthoai, solanbaoduong,
                                        //            _dtngaybaoduong.ToString("dd/MM/yyyy"));

                                        //        bool isUnicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                        //        byte countmes = Utilities.CountMess(resms, isUnicode);
                                        //        cmd = new SqlCommand("sp_TinNhan_InsertWithCheckLoop", con);
                                        //        cmd.Parameters.Clear();
                                        //        cmd.Parameters.AddWithValue("@sendername", sendername);
                                        //        cmd.Parameters.AddWithValue("@phone", dienthoai);
                                        //        cmd.Parameters.AddWithValue("@sms", resms);
                                        //        cmd.Parameters.AddWithValue("@countmes", countmes);
                                        //        cmd.Parameters.AddWithValue("@smstype", "Cam on bao duong");
                                        //        cmd.Parameters.AddWithValue("@idcongty", idcongty);
                                        //        cmd.Parameters.AddWithValue("@idkhachhang", obj[0]);
                                        //        cmd.Parameters.AddWithValue("@timeSchedule", DateTime.Now);
                                        //        cmd.Parameters.AddWithValue("@isUnicode", isUnicode);
                                        //        if (cmd.ExecuteNonQuery() > 0)
                                        //            countsmsThanksBaoDuong++;
                                        //    }
                                        //}
                                        //else 
                                        if (tblCamOnBDConfig != null && tblCamOnBDConfig.Rows.Count > 0) //nhắn tin cám ơn bảo dưỡng nếu có cấu hình
                                        {
                                            int flag;

                                            if (int.TryParse(tblCamOnBDConfig.Rows[0]["GioNhan"].ToString(), out flag) && int.TryParse(tblCamOnBDConfig.Rows[0]["NhanSauKhiBaoDuong"].ToString(), out flag))
                                            {
                                                if (!String.IsNullOrEmpty(ngaygiaoxe))
                                                {
                                                    DateTime timeSchedule = _dtngaygiaoxe.AddDays(int.Parse(tblCamOnBDConfig.Rows[0]["NhanSauKhiBaoDuong"].ToString())).AddHours(int.Parse(tblCamOnBDConfig.Rows[0]["GioNhan"].ToString()));

                                                    if (timeSchedule != null && timeSchedule.Date >= DateTime.Now.Date)
                                                    {
                                                        object sms = new SqlCommand("select top 1 sms from smsconfig where type='Cam on bao duong' and idcongty=" + idcongty, con).ExecuteScalar();
                                                        if (sms != null && sms.ToString() != "")
                                                        {
                                                            string resms = Utilities.Smsreplace(sms.ToString(), hokh + " " + tenkh, namsinh + "/" + thangsinh + "/" + ngaysinh, sendername, tenxe, bienso, sokhung, somay, dienthoai, solanbaoduong, _dtngaybaoduong.ToString("dd/MM/yyyy"));
                                                            bool isUnicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                                            byte countmes = Utilities.CountMess(resms, isUnicode);
                                                            SqlCommand cmdSms = new SqlCommand("sp_TinNhan_InsertWithCheckLoop", con);
                                                            cmdSms.Parameters.Clear();
                                                            cmdSms.CommandType = CommandType.StoredProcedure;
                                                            cmdSms.Parameters.AddWithValue("@sendername", sendername);
                                                            cmdSms.Parameters.AddWithValue("@phone", dienthoai);
                                                            cmdSms.Parameters.AddWithValue("@sms", resms);
                                                            cmdSms.Parameters.AddWithValue("@countmes", countmes);
                                                            cmdSms.Parameters.AddWithValue("@smstype", "Cam on bao duong");
                                                            cmdSms.Parameters.AddWithValue("@idcongty", idcongty);
                                                            cmdSms.Parameters.AddWithValue("@idkhachhang", obj[0]);
                                                            cmdSms.Parameters.AddWithValue("@isUnicode", isUnicode);
                                                            cmdSms.Parameters.AddWithValue("@timeSchedule", timeSchedule);
                                                            if (cmdSms.ExecuteNonQuery() > 0)
                                                                countsmsThanksBaoDuong++;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else if (int.Parse(tongtien) >= Class.CompanyInfo.sotiennhantinbaoduong) // nhan tin cho khach hang cam on da den bao duong theo so tien
                                        {
                                            object sms = new SqlCommand("select top 1 sms from smsconfig where type='Cam on bao duong' and idcongty=" + idcongty, con).ExecuteScalar();
                                            if (sms != null && sms.ToString() != "")
                                            {
                                                string resms = Utilities.Smsreplace(sms.ToString(), hokh + " " + tenkh, namsinh + "/" + thangsinh + "/" + ngaysinh, sendername, tenxe, bienso, sokhung, somay, dienthoai, solanbaoduong, _dtngaybaoduong.ToString());
                                                bool isUnicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                                byte countmes = Utilities.CountMess(resms, isUnicode);
                                                var cmdSms = new SqlCommand("sp_TinNhan_InsertWithCheckLoop", con);
                                                cmdSms.Parameters.Clear();
                                                cmdSms.Parameters.AddWithValue("@sendername", sendername);
                                                cmdSms.Parameters.AddWithValue("@phone", dienthoai);
                                                cmdSms.Parameters.AddWithValue("@sms", resms);
                                                cmdSms.Parameters.AddWithValue("@countmes", countmes);
                                                cmdSms.Parameters.AddWithValue("@smstype", "Cam on bao duong");
                                                cmdSms.Parameters.AddWithValue("@idcongty", idcongty);
                                                cmdSms.Parameters.AddWithValue("@idkhachhang", obj[0]);
                                                cmdSms.Parameters.AddWithValue("@isUnicode", isUnicode);
                                                if (cmdSms.ExecuteNonQuery() > 0)
                                                    countsmsThanksBaoDuong++;
                                            }
                                        }
                                        #endregion

                                        //nhan tin moi thay dau dot tiep neu thoa mãn.
                                        #region nhantinthaydau
                                        //if (thaydau.ToUpper() == "1" || thaydau.ToUpper() == "CO" || thaydau.ToUpper() == "YES" || thaydau.ToUpper() == "TRUE" || thaydau.ToUpper() == "OK")
                                        //{
                                        if (chk_NhanThayDau.Checked || chkChiNhanTinThayDau.Checked)
                                        {
                                            using (DataTable tblThayDauConfig = new DataTable())
                                            {
                                                SqlDataAdapter da = new SqlDataAdapter("Select top 1 * from SMSMoiThayDau_Config where idcongty=" + idcongty + " and active=1", con);
                                                object sms = new SqlCommand("select sms from SMSConfig where idcongty=" + idcongty + " and Type='Thay dau'", con).ExecuteScalar();

                                                da.Fill(tblThayDauConfig);
                                                if (tblThayDauConfig.Rows.Count > 0 && sms != null)
                                                {
                                                    string nhansausongay = tblThayDauConfig.Rows[0]["nhansausongay"].ToString();
                                                    string gionhan = tblThayDauConfig.Rows[0]["gionhan"].ToString();

                                                    //string _ngaygiaoxe = namgiaoxe + "/" + thanggiaoxe + "/" + ngaygiaoxe;

                                                    DateTime dt2;

                                                    if (ngaygiaoxe != "")
                                                    {
                                                        dt2 = _dtngaygiaoxe;

                                                        //DateTime tmp = dt2;
                                                        DateTime d = dt2.AddDays(int.Parse(nhansausongay));
                                                        DateTime timechedule = new DateTime(d.Year, d.Month, d.Day, int.Parse(gionhan), 0, 0, 0);

                                                        //tienlm - 20150917 - chỉ gửi tin nhắn nếu lịch gửi >= ngày hiện tại
                                                        if (timechedule != null && timechedule.Date >= DateTime.Now.Date)
                                                        {
                                                            string resms = Utilities.Smsreplace(sms.ToString(),
                                                                hokh + " " + tenkh,
                                                                namsinh + "/" + thangsinh + "/" + ngaysinh, sendername,
                                                                tenxe, bienso, sokhung, somay, dienthoai, solanbaoduong,
                                                                timechedule.ToString("dd/MM/yyyy"));

                                                            bool isunicode = Tools.GetDataCoding(resms) == 8 ? true : false;
                                                            string sql = " if not exists (select smsid from TinNhan with (nolock) where phone = @phone "
                                                                         +
                                                                         " and sms = @sms and SenderName = @sendername "
                                                                         +
                                                                         " and smstype = @smstype and IdCongTy = @idcongty and IdKhachHang = @idkhachhang "
                                                                         + " and timeSchedule = @timeSchedule) begin"
                                                                         +
                                                                         " Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule) "
                                                                         +
                                                                         " values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)"
                                                                         +
                                                                         "; end update LichSuBaoDuongXe set thaydau=1 where idbaoduong=@idbaoduong"
                                                                         +
                                                                         "; update KhachHang set NhanTinThayDau = @nhantinthaydau, ChiNhanTinThayDau = @chinhantinthaydau "
                                                                         +
                                                                         " where IdKhachHang = @idkhachang and idcongty = @idcongty";
                                                            cmd = new SqlCommand(sql, con); cmd.Parameters.Clear();
                                                            cmd.Parameters.AddWithValue("@sendername", sendername);
                                                            cmd.Parameters.AddWithValue("@phone", dienthoai);
                                                            cmd.Parameters.AddWithValue("@sms", resms);
                                                            cmd.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                                                            cmd.Parameters.AddWithValue("@smstype", "Thay dau");
                                                            cmd.Parameters.AddWithValue("@idcongty", idcongty);
                                                            cmd.Parameters.AddWithValue("@idkhachhang", idkhachang);
                                                            cmd.Parameters.AddWithValue("@timeSchedule", timechedule);
                                                            cmd.Parameters.AddWithValue("@idbaoduong", idbaoduong);
                                                            cmd.Parameters.AddWithValue("@idkhachang", idkhachang);
                                                            cmd.Parameters.AddWithValue("@nhantinthaydau", chk_NhanThayDau.Checked);
                                                            cmd.Parameters.AddWithValue("@chinhantinthaydau", chkChiNhanTinThayDau.Checked);

                                                            if (cmd.ExecuteNonQuery() > 0) countThayDauSMS++;
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        //}
                                        #endregion
                                    }
                                }
                                else
                                    countfail++;
                            }
                            catch (Exception ex)
                            {
                                status += "\n" + hokh + " " + tenkh + " | " + ex.Message;
                            }
                        }
                        #endregion
                    }

                    MessageBox.Show(@"Bạn đã Import : " + (countsuccess).ToString()
                        + @" khách hàng,  khách hàng lỗi: " + countfail.ToString()
                        + @", khách hàng đã tồn tại: " + countKHExists.ToString()
                        + @", trùng lần bảo dưỡng: " + countBDExists.ToString()
                        + @"\n Đã nhắn cảm ơn bảo dưỡng: " + countsmsThanksBaoDuong.ToString()
                        + @"\n Đã đặt lịch mời bảo dưỡng: " + countsmsNextBaoDuong.ToString()
                        + @"\n Đã đặt lịch mời thay dầu:  " + countThayDauSMS.ToString()
                        + @"\n Thông tin khách hàng Lỗi:\n" + status);
                    progressBar1.Invoke(new Action(() => progressBar1.Visible = false));

                    //Close connection
                    con.Close();
                }//end using connection.
            }
            catch (Exception ex)
            {
                progressBar1.Invoke(new Action(() => progressBar1.Visible = false));
                buttonX2.Enabled = true;
                MessageBox.Show(@"Lỗi : " + ex.Message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            if (tableExcel != null && tableExcel.Rows.Count > 0)
            {
                tableExcel.Rows.Clear();
                buttonX2.Enabled = true;
            }
        }

        private void chk_NhanThayDau_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_NhanThayDau.Checked)
            {
                chkChiNhanTinThayDau.Checked = false;
                chkChiNhanTinThayDau.Enabled = false;
            }
            else
            {
                chkChiNhanTinThayDau.Enabled = true;
            }
        }

        private void chkChiNhanTinThayDau_CheckedChanged(object sender, EventArgs e)
        {
            if (chkChiNhanTinThayDau.Checked)
            {
                chk_NhanThayDau.Checked = false;
                chk_NhanThayDau.Enabled = false;
            }
            else
            {
                chk_NhanThayDau.Enabled = true;
            }
        }
    }
}