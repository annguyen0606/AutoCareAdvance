using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using AutoCareV2._0.Class;
using System.Data.OleDb;
using System.Globalization;

namespace AutoCareV2._0
{
    public partial class FrmSMSTuyBien : Form
    {
        public string Id = "";
        public DataTable dskh;
        private static DataTable dt = new DataTable();

        public FrmSMSTuyBien()
        {
            InitializeComponent();

        }

        private void FrmSMSTuyBien_Load(object sender, EventArgs e)
        {
            if (dskh != null)
            {
                grv_DsKhachHang.DataSource = dskh;
            }
            else
            {
                dt_NgayNhan.Value = DateTime.Now;
            }
        }
        int choice = 0;
        private void txt_SMS_TextChanged(object sender, EventArgs e)
        {
            int maxchar = 459;
            bool isunicode = Tools.GetDataCoding(txt_SMS.Text) == 8 ? true : false;
            if (isunicode) maxchar = 210;
            if (txt_SMS.Text.Length > maxchar)
            {
                txt_SMS.Text = txt_SMS.Text.Substring(0, maxchar);
            }
            lbl_BanTin.Text = Utilities.CountRealMess(txt_SMS.Text).ToString();
            int countchar = Utilities.CountChar(txt_SMS.Text);
            lbl_KyTuDaNhan.Text = countchar.ToString();
            lbl_KyTuCon.Text = (maxchar - countchar).ToString();
        }

        private void FrmSMSTuyBien_FormClosed(object sender, FormClosedEventArgs e)
        {
            dt.Clear();
        }

        private void cbb_Key_SelectedIndexChanged(object sender, EventArgs e)
        {
            txt_SMS.Text += cbb_Key.Text;
        }

        private void btn_Loc_Click(object sender, EventArgs e)
        {
            choice = 0;
            using (SqlConnection mycon = new SqlConnection(Class.datatabase.connect))
            {
                mycon.Open();
                //SqlDataAdapter da = new SqlDataAdapter("select * from KhachHang where IdCongty=" + Class.CompanyInfo.idcongty + " and TenKH like '" + cbb_Search.Text + "%'", mycon);
                SqlDataAdapter da = new SqlDataAdapter("select * from KhachHang where IdCongty=@IdCongTy and TenKH like @TenKH", mycon);
                da.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                da.SelectCommand.Parameters.AddWithValue("@TenKH", cbb_Search.Text + "%");

                dt.Clear();
                da.Fill(dt);
                grv_DsKhachHang.DataSource = dt;
                lbl_SoKH.Text = "Số lượng khách hàng: " + dt.Rows.Count;
                mycon.Close();
                btn_Loc.Enabled = true;
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(Id))
                {
                    int gioNhan;

                    #region dat lich theo du lieu trong database
                    if (int.TryParse(txt_GioNhan.Text.Trim(), out gioNhan))
                    {
                        if (txt_SMS.Text != null && txt_SMS.Text.Trim() != "")
                        {
                            using (SqlConnection mycon = new SqlConnection(Class.datatabase.connect))
                            {
                                if (dt.Rows.Count > 0)
                                {
                                    mycon.Open();

                                    //Insert tbl SMSTyBienStore
                                    SqlCommand cmd = new SqlCommand("INSERT INTO SMSTuyBienStore (IdCongTy, smsType, SMS, Countmes, GioNhan, NgayNhan, NgayDatLich) VALUES (@IdCongTy, @smsType, @SMS, @Countmes, @GioNhan, @NgayNhan, @NgayDatLich) select @@Identity", mycon);
                                    cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    cmd.Parameters.AddWithValue("@smsType", "Tuy bien");
                                    cmd.Parameters.AddWithValue("@SMS", txt_SMS.Text);
                                    cmd.Parameters.AddWithValue("@Countmes", lbl_BanTin.Text.Trim());
                                    cmd.Parameters.AddWithValue("@GioNhan", gioNhan);
                                    cmd.Parameters.AddWithValue("@NgayNhan", dt_NgayNhan.Value.ToString("yyyyMMdd"));
                                    cmd.Parameters.AddWithValue("@NgayDatLich", DateTime.Now);
                                    string idSMSTuyBien = cmd.ExecuteScalar().ToString();

                                    //Insert tbl TinNhan de dat lich gui sms
                                    int year = dt_NgayNhan.Value.Year;
                                    int month = dt_NgayNhan.Value.Month;
                                    int day = dt_NgayNhan.Value.Day;
                                    DateTime timeSchedule = new DateTime(year, month, day, gioNhan, 0, 0, 0);

                                    string tenKH = "", ngaySinh = "", phone = "";
                                    string resms = "";

                                    for (int i = 0; i < dt.Rows.Count; i++)
                                    {
                                        tenKH = dt.Rows[i]["TenKH"].ToString();
                                        ngaySinh = dt.Rows[i]["NgaySinh"].ToString();
                                        phone = dt.Rows[i]["DienThoai"].ToString();
                                        resms = Utilities.smsreplace_TuyBien(txt_SMS.Text, tenKH, ngaySinh, Class.CompanyInfo.sendername, phone);

                                        SqlCommand cmdSMS = new SqlCommand("INSERT INTO TinNhan (phone, sms, countmes, SenderName, isUnicode, IdKhachHang, smstype, timeSchedule, IdCongTy, IdSMSTuyBien) VALUES (@phone, @sms, @countmes, @SenderName, @isUnicode, @IdKhachHang, @smstype, @timeSchedule, @IdCongTy, @IdSMSTuyBien)", mycon);
                                        cmdSMS.Parameters.AddWithValue("@phone", dt.Rows[i]["DienThoai"].ToString());
                                        cmdSMS.Parameters.AddWithValue("@sms", resms);
                                        cmdSMS.Parameters.AddWithValue("@countmes", lbl_BanTin.Text.Trim());
                                        cmdSMS.Parameters.AddWithValue("@SenderName", Class.CompanyInfo.sendername);
                                        cmdSMS.Parameters.AddWithValue("@isUnicode", Tools.GetDataCoding(txt_SMS.Text) == 8 ? true : false);
                                        if (choice == 0)
                                            cmdSMS.Parameters.AddWithValue("@IdKhachHang", dt.Rows[i]["IdKhachHang"]);
                                        else
                                        {
                                            cmdSMS.Parameters.AddWithValue("@IdKhachHang", 0);
                                        }
                                        cmdSMS.Parameters.AddWithValue("@smstype", "Tuy bien");
                                        cmdSMS.Parameters.AddWithValue("@timeSchedule", timeSchedule);
                                        cmdSMS.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                        cmdSMS.Parameters.AddWithValue("@IdSMSTuyBien", idSMSTuyBien);
                                        cmdSMS.ExecuteNonQuery();
                                    }
                                    mycon.Close();
                                    MessageBox.Show("Đặt lịch gửi tin nhắn thành công !");
                                    btn_DatLich.Enabled = false;
                                }
                                else MessageBox.Show("Không có khách hàng nào !");
                            }
                        }
                        else MessageBox.Show("Hãy điền nội dụng tin nhắn !");
                    }
                    else MessageBox.Show("Giờ nhắn phải là số nguyên !");
                    #endregion
                }
                else
                {
                    int gioNhan;

                    if (int.TryParse(txt_GioNhan.Text.Trim(), out gioNhan))
                    {
                        if (txt_SMS.Text != null && txt_SMS.Text.Trim() != "")
                        {
                            using (SqlConnection mycon = new SqlConnection(Class.datatabase.connect))
                            {
                                mycon.Open();

                                //SqlCommand cmd = new SqlCommand("INSERT INTO SMSTuyBienStore (IdCongTy, smsType, SMS, Countmes, GioNhan, NgayNhan, NgayDatLich) VALUES (@IdCongTy, @smsType, @SMS, @Countmes, @GioNhan, @NgayNhan, @NgayDatLich) select @@Identity", mycon);

                                SqlCommand cmd = new SqlCommand(@"UPDATE SMSTuyBienStore SET SMS=@SMS, Countmes=@Countmes, GioNhan=@GioNhan, NgayNhan=@NgayNhan,
                                                                NgayDatLich=@NgayDatLich WHERE IdCongTy=@IdCongTy AND IdSMSTuyBien=@IdSMSTuyBien", mycon);
                                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                cmd.Parameters.AddWithValue("@SMS", txt_SMS.Text);
                                cmd.Parameters.AddWithValue("@Countmes", lbl_BanTin.Text.Trim());
                                cmd.Parameters.AddWithValue("@GioNhan", gioNhan);
                                cmd.Parameters.AddWithValue("@NgayNhan", dt_NgayNhan.Value.ToString("yyyyMMdd"));
                                cmd.Parameters.AddWithValue("@NgayDatLich", DateTime.Now);
                                cmd.Parameters.AddWithValue("@IdSMSTuyBien", Convert.ToInt64(Id));
                                cmd.ExecuteNonQuery();

                                //Insert tbl TinNhan de dat lich gui sms
                                int year = dt_NgayNhan.Value.Year;
                                int month = dt_NgayNhan.Value.Month;
                                int day = dt_NgayNhan.Value.Day;
                                DateTime timeSchedule = new DateTime(year, month, day, gioNhan, 0, 0, 0);

                                string tenKH = "", ngaySinh = "", phone = "";
                                string resms = "";

                                // DoNT ADD 2018/05/07 Start
                                SqlDataAdapter da = new SqlDataAdapter("select * from TinNhan where IdSMSTuyBien=@IdSMSTuyBien", mycon);
                                da.SelectCommand.Parameters.AddWithValue("@IdSMSTuyBien", Convert.ToInt64(Id));

                                dskh.Clear();
                                da.Fill(dskh);
                                // DoNT ADD 2018/05/07 End

                                for (int i = 0; i < dskh.Rows.Count; i++)
                                {
                                    tenKH = dskh.Rows[i]["TenKH"].ToString();
                                    ngaySinh = dskh.Rows[i]["NgaySinh"].ToString();
                                    phone = dskh.Rows[i]["DienThoai"].ToString();
                                    resms = Utilities.smsreplace_TuyBien(txt_SMS.Text, tenKH, ngaySinh, Class.CompanyInfo.sendername, phone);

                                    SqlCommand cmdSMS = new SqlCommand(@"UPDATE TinNhan SET sms=@sms, countmes=@countmes, isUnicode=@isUnicode, timeSchedule=@timeSchedule 
                                                                    WHERE IdKhachHang=@IdKhachHang AND IdCongTy=@IdCongTy AND IdSMSTuyBien=@IdSMSTuyBien", mycon);

                                    cmdSMS.Parameters.AddWithValue("@sms", resms);
                                    cmdSMS.Parameters.AddWithValue("@countmes", lbl_BanTin.Text.Trim());
                                    cmdSMS.Parameters.AddWithValue("@isUnicode", Tools.GetDataCoding(txt_SMS.Text) == 8 ? true : false);
                                    cmdSMS.Parameters.AddWithValue("@IdKhachHang", dskh.Rows[i]["IdKhachHang"]);
                                    cmdSMS.Parameters.AddWithValue("@timeSchedule", timeSchedule);
                                    cmdSMS.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    cmdSMS.Parameters.AddWithValue("@IdSMSTuyBien", Convert.ToInt64(Id));
                                    cmdSMS.ExecuteNonQuery();
                                }
                                mycon.Close();
                                MessageBox.Show("Sửa lịch gửi tin nhắn thành công !");
                                btn_DatLich.Enabled = false;
                            }
                        }
                        else MessageBox.Show("Hãy điền nội dụng tin nhắn !");
                    }
                    else MessageBox.Show("Giờ nhắn phải là số nguyên !");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }

        private void textBox1_MouseDown(object sender, MouseEventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            string path = openFileDialog1.FileName;
            textBox1.Text = path;
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
            cbosheet.Items.Clear();
            foreach (DataRow schemaRow in schemaTable.Rows)
            {
                cbosheet.Items.Add(schemaRow["TABLE_NAME"].ToString());
            }
            conn.Close();
        }
        public static DataTable exceldata(string filePath, string sheet)
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
        private void buttonX1_Click(object sender, EventArgs e)
        {

            choice = 1;
            if (textBox1.Text == "" || cbosheet.Text == "") { MessageBox.Show("Bạn chưa chọn file hoặc Sheet"); return; }
            dt.Clear();
            dt = exceldata(textBox1.Text, cbosheet.Text);
            //string[] cot = new string[tbl.Columns.Count];

            //for (int i = 0; i < tbl.Columns.Count; i++)
            //{


            //    cot[i] = tbl.Columns[i].ColumnName;

            //    ;
            //}
            grv_DsKhachHang.DataSource = dt;
            int i = int.Parse(grv_DsKhachHang.Rows.Count.ToString()) - 1;

            lbl_SoKH.Text = "Số Lượng Khách Hàng Là: " + i;
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_locGioiTinh_Click(object sender, EventArgs e)
        {
            choice = 0;

            if(radioButtonNam.Checked==true)
            {
                using (SqlConnection mycon = new SqlConnection(Class.datatabase.connect))
                {
                    mycon.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("select * from KhachHang where IdCongty=" + Class.CompanyInfo.idcongty + " and TenKH like '" + cbb_Search.Text + "%'", mycon);
                    SqlDataAdapter da = new SqlDataAdapter("select * from KhachHang where IdCongty=@IdCongTy and GioiTinh=@GioiTinh", mycon);
                    da.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    da.SelectCommand.Parameters.AddWithValue("@GioiTinh", "Nam");

                    dt.Clear();
                    da.Fill(dt);
                    grv_DsKhachHang.DataSource = dt;
                    lbl_SoKH.Text = "Số lượng khách hàng: " + dt.Rows.Count;
                    mycon.Close();
                    btn_Loc.Enabled = true;

                    mycon.Close();
                }
            }
            else if(radioButtonNu.Checked==true)
            {
                using (SqlConnection mycon = new SqlConnection(Class.datatabase.connect))
                {
                    mycon.Open();
                    //SqlDataAdapter da = new SqlDataAdapter("select * from KhachHang where IdCongty=" + Class.CompanyInfo.idcongty + " and TenKH like '" + cbb_Search.Text + "%'", mycon);
                    SqlDataAdapter da = new SqlDataAdapter("select * from KhachHang where IdCongty=@IdCongTy and GioiTinh=@GioiTinh", mycon);
                    da.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    da.SelectCommand.Parameters.AddWithValue("@GioiTinh", "Nữ");

                    dt.Clear();
                    da.Fill(dt);
                    grv_DsKhachHang.DataSource = dt;
                    lbl_SoKH.Text = "Số lượng khách hàng: " + dt.Rows.Count;
                    mycon.Close();
                    btn_Loc.Enabled = true;

                    mycon.Close();
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn giới tính cần lọc!", "Thông báo");
            }
        }
    }
}
