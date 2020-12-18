using AutoCareUtil;
using System;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class FrmImportKhachHangMuaXe : Form
    {
        private DataTable _tableExcel;

        public FrmImportKhachHangMuaXe()
        {
            InitializeComponent();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string path = openFileDialog1.FileName;
            txtFilePath.Text = path;
            const string hdr = "Yes";
            string strConn;
            if (path.Substring(path.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0;HDR=" + hdr + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + path + ";Extended Properties=\"Excel 8.0;HDR=" + hdr + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataTable schemaTable = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
            cbSheets.Items.Clear();
            if (schemaTable != null)
                foreach (DataRow schemaRow in schemaTable.Rows)
                {
                    cbSheets.Items.Add(schemaRow["TABLE_NAME"].ToString());
                }
            conn.Close();
        }

        private void Tmp()
        {
            try
            {
                var oleStringBuilder = new OleDbConnectionStringBuilder(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:\MyExcel.xls;Extended Properties='Excel 8.0;HDR=Yes;IMEX=1';");
                oleStringBuilder.DataSource = @"~\App_Datav\MyExcelWorksheet.xls";

                var excelConection = new OleDbConnection();
                excelConection.ConnectionString = oleStringBuilder.ConnectionString;

                var excelCommand = new OleDbCommand();
                excelCommand.Connection = excelConection;
                excelCommand.CommandText = "Select * From [Sheet1$]";

                excelConection.Open();
                var excelReader = excelCommand.ExecuteReader();

                dataGridView1.DataSource = excelReader;
            }
            catch (Exception args)
            {
                MessageBox.Show(@"Could not open Excel file: " + args.Message);
            }
        }

        private static DataTable Exceldata(string filePath, string sheet)
        {
            DataTable dtexcel = new DataTable();
            const bool hasHeaders = true;
            const string hdr = "Yes";
            string strConn;
            if (filePath.Substring(filePath.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + hdr + ";IMEX=0\"";
            else
                //strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=\"Excel 8.0;HDR=" + hdr + ";IMEX=0\"";
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + ";Extended Properties=\"Excel 12.0;HDR=" + hdr + ";IMEX=0\"";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();

            if (!sheet.EndsWith("_"))
            {
                string query = "SELECT  * FROM [" + sheet + "]";
                OleDbDataAdapter daexcel = new OleDbDataAdapter(query, conn);

                daexcel.Fill(dtexcel);
            }
            conn.Close();
            return dtexcel;
        }

        private void FrmImportKhachHangMuaXe_Load(object sender, EventArgs e)
        {
        }

        private void txtFilePath_MouseDown(object sender, MouseEventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (txtFilePath.Text == "" || cbSheets.Text == "") { MessageBox.Show(@"Bạn chưa chọn file hoặc Sheet"); return; }
            _tableExcel = Exceldata(txtFilePath.Text, cbSheets.Text);
            dataGridView1.DataSource = _tableExcel;

            if (_tableExcel != null && _tableExcel.Rows.Count > 0)
                LoadColumnToCombobox(_tableExcel);
        }

        private void LoadColumnToCombobox(DataTable tableExcel)
        {
            cboBienSo.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboBienSo.SelectedIndex = -1;

            cboCMND.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboCMND.SelectedIndex = -1;

            cboDiaChi.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboDiaChi.SelectedIndex = -1;

            cboDienThoai.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboDienThoai.SelectedIndex = -1;

            cboGia.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboGia.SelectedIndex = -1;

            cboGioiTinh.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboGioiTinh.SelectedIndex = -1;

            cboHoKhachHang.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboHoKhachHang.SelectedIndex = -1;

            cboLoaiKH.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboLoaiKH.SelectedIndex = -1;

            cboMauXe.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboMauXe.SelectedIndex = -1;

            cboNgayMua.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboNgayMua.SelectedIndex = -1;

            cboNgaySinh.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboNgaySinh.SelectedIndex = -1;

            cboSoKhung.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboSoKhung.SelectedIndex = -1;

            cboSoLuong.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboSoLuong.SelectedIndex = -1;

            cboSoMay.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboSoMay.SelectedIndex = -1;

            cboSoSBH.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboSoSBH.SelectedIndex = -1;

            cboTenKhachHang.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboTenKhachHang.SelectedIndex = -1;

            cboTenXe.DataSource = tableExcel.Columns.Cast<DataColumn>().Select(x => x.ColumnName).ToArray();
            cboTenXe.SelectedIndex = -1;
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            #region MyRegion
            try
            {
                int countsuccess = 0, countThayDau = 0;
                int dinhDang = 0;
                if (cbb_DinhDang.Text == @"Ngay/Thang/Nam")
                    dinhDang = 1;
                else if (cbb_DinhDang.Text == @"Thang/Ngay/Nam")
                    dinhDang = 2;

                if (dinhDang == 0)
                {
                    MessageBox.Show(@"Bạn chưa chọn định dạng ngày tháng trong file excel.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                if (cboDienThoai.Text.Trim() == "")
                {
                    MessageBox.Show(@"Số điện thoại khách hàng không được để trống!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboNgayMua.Text.Trim() == "")
                {
                    MessageBox.Show(@"Ngày mua không được để trống!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cboBienSo.Text.Trim() == "" && cboSoKhung.Text.Trim() == "" && cboSoMay.Text.Trim() == "")
                {
                    MessageBox.Show(@"Cần chọn Biển số hoặc Số khung hoặc Số máy!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                try
                {
                    int countXdbExists = 0;
                    int countfail = 0;
                    int dem = 0;
                    using (SqlConnection con = new SqlConnection(Class.datatabase.connect))
                    {
                        con.Open();
                        if (dataGridView1.Rows.Count > 0)
                        {
                            progressBar1.Invoke(new Action(() =>
                            {
                                progressBar1.Visible = true;
                                progressBar1.Minimum = 0;
                                progressBar1.Value = 0;
                                progressBar1.Maximum = dataGridView1.Rows.Count;
                            }));
                        }

                        //dataGridView1.Invoke(new Action(() =>
                        //{
                        foreach (DataRow r in _tableExcel.Rows)
                        {
                            //custinfo
                            string hokh;
                            try { hokh = r[cboHoKhachHang.Text].ToString(); }
                            catch { hokh = ""; }
                            string tenkh;
                            try { tenkh = r[cboTenKhachHang.Text].ToString(); }
                            catch { tenkh = ""; }
                            string ngaysinh;
                            try { ngaysinh = r[cboNgaySinh.Text].ToString(); }
                            catch { ngaysinh = ""; }
                            string gioitinh;
                            try { gioitinh = r[cboGioiTinh.Text].ToString(); }
                            catch { gioitinh = ""; }
                            string dienthoai;
                            try { dienthoai = r[cboDienThoai.Text].ToString(); }
                            catch { dienthoai = ""; }
                            string cmnd;
                            try { cmnd = r[cboCMND.Text].ToString(); }
                            catch { cmnd = ""; }
                            string diachi;
                            try { diachi = r[cboDiaChi.Text].ToString(); }
                            catch { diachi = ""; }
                            string soSbh;
                            try { soSbh = r[cboSoSBH.Text].ToString(); }
                            catch { soSbh = ""; }

                            DateTime dtngaysinh = new DateTime();
                            int varNamsinh = 0, varThangsinh = 0, varNgaysinh = 0;
                            try
                            {
                                var tmpNgaySinh = ngaysinh.Split('/');
                                if (dinhDang == 1)
                                {
                                    varNgaysinh = Convert.ToInt32(tmpNgaySinh[0]);
                                    varThangsinh = Convert.ToInt32(tmpNgaySinh[1]);
                                    varNamsinh = Convert.ToInt32(tmpNgaySinh[2].Split(' ')[0]);
                                }
                                else if (dinhDang == 2)
                                {
                                    varNgaysinh = Convert.ToInt32(tmpNgaySinh[1]);
                                    varThangsinh = Convert.ToInt32(tmpNgaySinh[0]);
                                    varNamsinh = Convert.ToInt32(tmpNgaySinh[2].Split(' ')[0]);
                                }

                                dtngaysinh = new DateTime(varNamsinh, varThangsinh, varNgaysinh, 0, 0, 0);
                            }
                            catch { ngaysinh = ""; }

                            //xeinfo
                            string ngaymua;
                            try { ngaymua = r[cboNgayMua.Text].ToString(); }
                            catch { ngaymua = ""; }
                            string tenxe;
                            try { tenxe = r[cboTenXe.Text].ToString(); }
                            catch { tenxe = ""; }
                            string mauxe;
                            try { mauxe = r[cboMauXe.Text].ToString(); }
                            catch { mauxe = ""; }
                            string bienso;
                            try { bienso = r[cboBienSo.Text].ToString(); }
                            catch { bienso = ""; }
                            string sokhung;
                            try { sokhung = r[cboSoKhung.Text].ToString(); }
                            catch { sokhung = ""; }
                            string somay;
                            try { somay = r[cboSoMay.Text].ToString(); }
                            catch { somay = ""; }
                            decimal gia;
                            try { gia = Convert.ToDecimal(r[cboGia.Text]); }
                            catch { gia = 0; }
                            int soluong;
                            try { soluong = Convert.ToInt32(r[cboSoLuong.Text]); }
                            catch { soluong = 0; }
                            string loaikh;
                            try { loaikh = r[cboLoaiKH.Text].ToString(); }
                            catch { loaikh = "1"; }

                            DateTime dtngaymua = new DateTime();
                            int varNammua = 0, varThangmua = 0, varNgaymua = 0;
                            try
                            {
                                var tmpNgayMua = ngaymua.Split('/');
                                if (dinhDang == 1)
                                {
                                    varNgaymua = Convert.ToInt32(tmpNgayMua[0]);
                                    varThangmua = Convert.ToInt32(tmpNgayMua[1]);
                                    varNammua = Convert.ToInt32(tmpNgayMua[2].Split(' ')[0]);
                                }
                                else if (dinhDang == 2)
                                {
                                    varNgaymua = Convert.ToInt32(tmpNgayMua[1]);
                                    varThangmua = Convert.ToInt32(tmpNgayMua[0]);
                                    varNammua = Convert.ToInt32(tmpNgayMua[2].Split(' ')[0]);
                                }

                                dtngaymua = new DateTime(varNammua, varThangmua, varNgaymua, 0, 0, 0);
                            }
                            catch { ngaymua = ""; }

                            if (String.IsNullOrEmpty(bienso) && String.IsNullOrEmpty(sokhung) && String.IsNullOrEmpty(somay))
                            {
                                progressBar1.Invoke(new Action(() => progressBar1.Value = progressBar1.Value + 1));
                                countfail++;
                                continue;
                            }

                            SqlCommand cmd = new SqlCommand("pro_ImportKhachMuaXe", con);
                            cmd.Parameters.Clear();
                            cmd.CommandType = CommandType.StoredProcedure;

                            cmd.Parameters.AddWithValue("@IdCongty", Convert.ToInt32(Class.CompanyInfo.idcongty));
                            cmd.Parameters.AddWithValue("@HoKH ", hokh);
                            cmd.Parameters.AddWithValue("@TenKH", tenkh);
                            cmd.Parameters.AddWithValue("@GioiTinh", gioitinh);
                            if (ngaysinh == "")
                            {
                                cmd.Parameters.AddWithValue("@NgaySinh", SqlDateTime.Null);
                            }
                            else { cmd.Parameters.AddWithValue("@NgaySinh", dtngaysinh); }
                            cmd.Parameters.AddWithValue("@DienThoai", dienthoai);
                            cmd.Parameters.AddWithValue("@DiaChi", diachi);
                            cmd.Parameters.AddWithValue("@CMND", cmnd);
                            if (ngaymua == "")
                            {
                                cmd.Parameters.AddWithValue("@NgayBan", SqlDateTime.Null);
                            }
                            else
                            {
                                cmd.Parameters.AddWithValue("@NgayBan", dtngaymua);
                            }
                            cmd.Parameters.AddWithValue("@TenXe", tenxe);
                            cmd.Parameters.AddWithValue("@MauXe", mauxe);
                            cmd.Parameters.AddWithValue("@BienSo", bienso);
                            cmd.Parameters.AddWithValue("@SoKhung", sokhung);
                            cmd.Parameters.AddWithValue("@SoMay", somay);
                            cmd.Parameters.AddWithValue("@DonGia", gia);
                            cmd.Parameters.AddWithValue("@SoLuong", soluong);
                            cmd.Parameters.AddWithValue("@SoSBH", soSbh);
                            cmd.Parameters.AddWithValue("@LoaiKH", loaikh);

                            string rslt;

                            try
                            {
                                rslt = cmd.ExecuteScalar().ToString();

                                if (rslt == "0")
                                    countXdbExists++;
                                else
                                    countsuccess++;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi : "+ex.Message +"ngày sinh :"+ dtngaysinh.ToString("yyyy-MM-dd") +" ngày mua :"+dtngaymua);
                                continue;
                            }

                            dem++;

                            #region Nhan tin thay dau

                            if (chk_ThayDau.Checked && rslt != "0")
                            {
                                using (DataTable tblThayDauConfig = new DataTable())
                                {
                                    SqlDataAdapter da = new SqlDataAdapter("Select top 1 * from SMSMoiThayDau_Config where idcongty=" + Class.CompanyInfo.idcongty + " and active=1", con);
                                    object sms = new SqlCommand("select sms from SMSConfig where idcongty=" + Class.CompanyInfo.idcongty + " and Type='Thay dau'", con).ExecuteScalar();

                                    da.Fill(tblThayDauConfig);
                                    if (tblThayDauConfig.Rows.Count > 0 && sms != null)
                                    {
                                        string nhansausongay = tblThayDauConfig.Rows[0]["nhansausongay"].ToString();
                                        string gionhan = tblThayDauConfig.Rows[0]["gionhan"].ToString();

                                        DateTime dt2;
                                        if (ngaymua == "")
                                        {
                                            dt2 = DateTime.Now;
                                        }
                                        else
                                        {
                                            dt2 = dtngaymua;
                                        }

                                        //string _ngaygiaoxe = namgiaoxe + "/" + thanggiaoxe + "/" + ngaygiaoxe;

                                        //DateTime tmp = dt2;
                                        DateTime d = dt2.AddDays(int.Parse(nhansausongay));
                                        DateTime timechedule = new DateTime(d.Year, d.Month, d.Day, int.Parse(gionhan), 0, 0, 0);

                                        //tienlm - 20150917 - chỉ gửi tin nhắn nếu lịch gửi >= ngày hiện tại
                                        if (timechedule != null && timechedule.Date >= DateTime.Now.Date)
                                        {
                                            //string resms = Utilities.smsreplace(sms.ToString(), hokh + " " + tenkh,
                                            //    varNamsinh + "/" + varThangsinh + "/" + varNgaysinh,
                                            //    Class.CompanyInfo.sendername, tenxe, bienso, sokhung, somay, dienthoai,
                                            //    "", timechedule.ToString("dd/MM/yyyy"), "","");
                                            string resms = Utilities.smsreplace(
                                                sms.ToString(), 
                                                hokh + " " + tenkh,
                                                varNamsinh + "/" + varThangsinh + "/" + varNgaysinh,
                                                Class.CompanyInfo.sendername, 
                                                tenxe, 
                                                bienso, 
                                                sokhung, 
                                                somay, 
                                                dienthoai,
                                                "",
                                                timechedule.ToString("dd/MM/yyyy"), "", "");

                                            bool isunicode = Tools.GetDataCoding(resms) == 8;
                                            string sql = @"sp_TinNhan_InsertWithCheckLoop";
                                            cmd = new SqlCommand(sql, con);
                                            cmd.CommandType = CommandType.StoredProcedure;
                                            cmd.Parameters.Clear();
                                            cmd.Parameters.AddWithValue("@sendername", Class.CompanyInfo.sendername);
                                            cmd.Parameters.AddWithValue("@phone", dienthoai);
                                            cmd.Parameters.AddWithValue("@sms", resms);
                                            cmd.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                                            cmd.Parameters.AddWithValue("@smstype", "Thay dau");
                                            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                            cmd.Parameters.AddWithValue("@idkhachhang", rslt);
                                            cmd.Parameters.AddWithValue("@timeSchedule", timechedule);
                                            cmd.Parameters.AddWithValue("@isUnicode", isunicode);

                                            if (cmd.ExecuteNonQuery() > 0) countThayDau++;
                                        }
                                    }
                                }
                            }

                            #endregion Nhan tin thay dau

                            var dem1 = dem;
                            progressBar1.Invoke(new Action(() => progressBar1.Value = dem1));
                        }
                    }
                    MessageBox.Show
                        (
                            @"Import thành công: " + countsuccess + @" xe." + Environment.NewLine
                            + @"Thất bại: " + countfail + @" xe." + Environment.NewLine
                            + @"Xe đã tồn tại: " + countXdbExists + @" xe." + Environment.NewLine
                            + @"Số tin thay dầu thành công: " + countThayDau + @" tin", @"Thông báo import", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    progressBar1.Invoke(new Action(() => progressBar1.Visible = false));
                }
                catch (Exception ex)
                {
                    progressBar1.Invoke(new Action(() => progressBar1.Visible = false));
                    buttonX2.Invoke(new Action(() => buttonX2.Enabled = true));
                    MessageBox.Show(@"Lỗi :" + ex, @"Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                if (_tableExcel != null && _tableExcel.Rows.Count > 0)
                {
                    _tableExcel.Rows.Clear();
                    buttonX2.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Lỗi :" + ex.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            #endregion

            #region For Test

            //DataTable tableData = new DataTable();
            //tableData.Columns.Add(new DataColumn("sms_id", typeof(string)));
            //tableData.Columns.Add(new DataColumn("sms_content", typeof(string)));

            //for (int i = 0; i < _tableExcel.Rows.Count; i++)
            //{
            //    try
            //    {
            //        DataRow dr = tableData.NewRow();
            //        dr["sms_content"] = _tableExcel.Rows[i][0].ToString();
            //        dr["sms_id"] = _tableExcel.Rows[i][1].ToString();

            //        tableData.Rows.Add(dr);
            //    }
            //    catch (Exception)
            //    {
            //        //
            //    }
            //}

            //MessageBox.Show("");

            //using (SqlConnection con = new SqlConnection(Class.datatabase.connect))
            //{
            //    con.Open();

            //    SqlCommand cmd = new SqlCommand("sp_tinnhan_updatecontent", con);
            //    cmd.Parameters.Clear();
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.Parameters.AddWithValue("@smsType", 0);
            //    cmd.Parameters.AddWithValue("@table", tableData);

            //    cmd.ExecuteNonQuery();
            //}
            #endregion
        }

        private static bool IsPhone(string phone)
        {
            Regex objNotWholePattern = new Regex("^[0-9]{9,12}$");
            return objNotWholePattern.IsMatch(phone);
        }

        #region Lọc số điện thoại
        /// <summary>
        /// Lọc số điện thoại
        /// </summary>
        /// <param name="phone"></param>
        /// <returns></returns>
        private static string FilterPhone(string phone)
        {
            string str = "";

            //filter first 
            if (IsPhone(phone))
            {
                Regex patern = new Regex("^[0-9]{1}$");
                str = phone.Where(t => patern.IsMatch(t.ToString()))
                    .Aggregate(str, (current, t) => current + t.ToString());
                phone = str;
            }

            //filter alter
            if (IsPhone(phone))
            {
                if (phone.Length >= 1 && phone.Substring(0, 1) == "0")
                {
                    str = phone.Substring(1); // Loc ky tu 0 o dau
                }
                else if (phone.Length >= 2 && phone.Substring(0, 2) == "84")
                {
                    str = phone.Substring(2); // Loc ki tu 84 o dau
                }
                else
                {
                    str = phone; // giu nguyen
                }
                if (str.Length != 9 && str.Length != 10)
                {
                    str = ""; // loc cac so dai hay ngan qua
                }
            }
            else
            {
                str = "";
            }
            return "84" + str;
        }
        #endregion
    }
}