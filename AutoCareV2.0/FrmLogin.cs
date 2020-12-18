//using GetConnection;

//using GetConnection;
using System;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace AutoCareV2._0 {
    public partial class FrmLogin : Form  {
        #region Variables
        private RSACryptoServiceProvider myrsa = new RSACryptoServiceProvider(512);
        private string cn = Class.datatabase.connect;
        private DataTable dttkdn = new DataTable("TaiKhoanDangNhap");
        private DataTable tblcongty = new DataTable();
        private SqlDataAdapter da = new SqlDataAdapter();
        private SqlConnection con = new SqlConnection();
        #endregion Variables
        private void connect()        {
            try            {
                con = new SqlConnection(cn);
                con.Open();
            }
            catch (Exception)            {
                MessageBox.Show("Lỗi kết nối: kiểm tra lại kết nối đường truyền mạng");
            }
        }
        public FrmLogin()        {
            InitializeComponent();
        }
        private void btndangnhap_Click(object sender, EventArgs e)        {
            try            {
                if (chkConnectOptions.Checked == true)                {
                    //dbconfig dbInfo = new dbconfig("HGUR7339823U43983RHDUHF72GMB938374HNGJDHEU", "MVNFH716188273646589GJFUJF83IU4JHT84IU5898RUT", "99845UU684UJRJTHYEIE83I4U584UIOIRUY84U54Y574I", txttendangnhap.Text.Trim(), txtmatkhau.Text.Trim());
                    //ConnectionDB dbConection = new ConnectionDB(dbInfo);
                    //Class.datatabase.connect = dbConection.GetConnection;
                    //if (dbConection.GetConnection == "")
                    //{
                    //    MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác. Vui lòng kiểm tra lại");
                    //    return;
                    //}
                    //cn = dbConection.GetConnection;
                }
                else                {
                    Microsoft.Win32.RegistryKey ReadKey;
                    ReadKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("AutoCareAccount", true);
                    if (ReadKey != null)                    {
                        string ServerName = AutoCareUtil.Utilities.Decode(ReadKey.GetValue("ServerName").ToString());
                        string DatabaseUser = AutoCareUtil.Utilities.Decode(ReadKey.GetValue("DatabaseUser").ToString());
                        string DatabasePassword = AutoCareUtil.Utilities.Decode(ReadKey.GetValue("DatabasePassword").ToString());
                        string DatabaseName = AutoCareUtil.Utilities.Decode(ReadKey.GetValue("DatabaseName").ToString());

                        cn = "server=" + ServerName + ";uid=" + DatabaseUser + ";pwd=" + DatabasePassword + ";database=" + DatabaseName;
                        Class.datatabase.connect = cn;
                    }
                    //cn = Class.datatabase.connect;
                }
                connect();

                SqlCommand c = new SqlCommand("Select top 1 username,idcongty,idnhanvien,idcuahang,Pass, Quyen, TenNhanVien from NhanVien_TaiKhoanDangNhap where username=@username", con);
                c.Parameters.AddWithValue("@username", txttendangnhap.Text.Trim());
                da = new SqlDataAdapter(c);
                da.Fill(dttkdn);
                if (dttkdn.Rows.Count > 0)                {
                    DataRow nhanvien = dttkdn.Rows[0];
                    Class.EmployeeInfo.idnhanvien = Convert.ToInt32(nhanvien["idnhanvien"]);
                    Class.EmployeeInfo.IdCuaHang = nhanvien["idcuahang"].ToString();
                    Class.EmployeeInfo.TenNhanVien = nhanvien["TenNhanVien"].ToString();
                    Class.EmployeeInfo.IdCongTy = nhanvien["IdCongty"].ToString();
                    Class.EmployeeInfo.Pass = nhanvien["Pass"].ToString();
                    Class.EmployeeInfo.Quyen = nhanvien["Quyen"].ToString();
                    Class.EmployeeInfo.UserName = nhanvien["username"].ToString();

                    string idconty = nhanvien["IdCongty"].ToString();

                    // lay thong tin cong ty
                    SqlCommand com = new SqlCommand("select * from Congty where idcongty = " + idconty, con);
                    da.SelectCommand = com;
                    da.Fill(tblcongty);
                    if (tblcongty.Rows.Count > 0)                   
                    {
                        Class.CompanyInfo.tencongty = tblcongty.Rows[0]["tencongty"].ToString();
                        Class.CompanyInfo.diachi = tblcongty.Rows[0]["diachi"].ToString();
                        Class.CompanyInfo.phone = tblcongty.Rows[0]["Dienthoai"].ToString();
                        idconty = tblcongty.Rows[0]["idCongty"].ToString();
                        Class.CompanyInfo.idcongty = idconty;
                        Class.CompanyInfo.quota = tblcongty.Rows[0]["QuotaRemain"].ToString();
                        Class.CompanyInfo.secretkey = tblcongty.Rows[0]["SecretKey"].ToString();
                        Class.CompanyInfo.sotiennhantinbaoduong = int.Parse(tblcongty.Rows[0]["SoTienNhanTinBaoDuong"].ToString());
                        Class.CompanyInfo.GoiPhanMem = tblcongty.Rows[0]["GoiPhanMem"].ToString();
                    }
                    else                    {
                        MessageBox.Show("Lỗi dữ liệu: Thông tin công ty của bạn chưa được cung cấp");
                        return;
                    }
                    try                    {
                        Class.CompanyInfo.cauhinhdotbaoduong = new SqlCommand("select ThangNhan from SMSMaintenanceConfig where idcongty=" + idconty, con).ExecuteScalar().ToString();
                    }
                    catch { }
                    // lay thong tin cua hang
                    string idscuahang = "";
                    using (SqlDataReader rd = new SqlCommand("select idcuahang from cuahang where idcongty=" + idconty, con).ExecuteReader())                    {
                        while (rd.Read()) { idscuahang += rd[0].ToString() + ","; }
                    }
                    if (idscuahang != "") Class.CompanyInfo.IdsCuaHang = idscuahang.TrimEnd(',');

                    // kiem tra thuong hieu
                    object obj = new SqlCommand("select top 1 thuonghieu from ThuongHieu where idcongty=" + idconty, con).ExecuteScalar();
                    if (obj != null)                    {
                        Class.CompanyInfo.sendername = obj != null ? obj.ToString() : "";
                    }
                    else    {
                        MessageBox.Show("Lỗi Thương hiệu: Công ty bạn chưa được cấp thương hiệu");
                        return;
                    }
                    // kiem tra mat khau
                    string pass = Class.Checksum.GetMd5Hash(txtmatkhau.Text, Class.CompanyInfo.secretkey);
                    SqlCommand cmd = new SqlCommand("select * from Taikhoandangnhap where username=@username and pass=@pass", con);
                    cmd.Parameters.AddWithValue("@username", txttendangnhap.Text);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    bool isPass = false;
                    using (SqlDataReader rd = cmd.ExecuteReader())                    {
                        while (rd.Read())                        {
                            isPass = true;
                            if (chkSaveInfo.Checked)        {
                                //string strencrypt = txttendangnhap.Text + "|" + txtmatkhau.Text;
                                //byte[] strby = myrsa.Encrypt(Encoding.Unicode.GetBytes(strencrypt), false);
                                //File.WriteAllText("info.dat", Convert.ToBase64String(strby));

                                Microsoft.Win32.RegistryKey ReadKey;
                                ReadKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("AutoCareAccount", true);
                                if (ReadKey == null)           {
                                    Microsoft.Win32.RegistryKey key;
                                    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("AutoCareAccount");
                                    key.SetValue("UserName", AutoCareUtil.Utilities.Encode(txttendangnhap.Text.Trim()));
                                    key.SetValue("Password", AutoCareUtil.Utilities.Encode(txtmatkhau.Text.Trim()));
                                    key.SetValue("SavePassword", true);
                                    key.SetValue("ConnectOnline", chkConnectOptions.Checked);
                                    key.Close();
                                }
                                else       {
                                    ReadKey.SetValue("UserName", AutoCareUtil.Utilities.Encode(txttendangnhap.Text.Trim()));
                                    ReadKey.SetValue("Password", AutoCareUtil.Utilities.Encode(txtmatkhau.Text.Trim()));
                                    ReadKey.SetValue("SavePassword", true);
                                    ReadKey.SetValue("ConnectOnline", chkConnectOptions.Checked);
                                    ReadKey.Close();
                                }
                            }
                            else      {
                                try      {
                                    Microsoft.Win32.RegistryKey ReadKey;
                                    ReadKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("AutoCareAccount", true);
                                    ReadKey.SetValue("SavePassword", false);
                                }
                                catch (Exception)   { }
                            }

                            RibMain fmain = new RibMain();
                            fmain.Show();
                            this.Hide();
                        }
                    }
                    if (!isPass)      {
                        MessageBox.Show("Bạn đăng nhập không thành công!\n Hãy kiểm tra lại mật khẩu");
                    }
                }
                else    {
                    MessageBox.Show("Bạn đăng nhập không thành công!\nHãy kiểm tra lại tài khoản đăng nhập.");
                    this.txttendangnhap.Focus();
                }
            }
            catch (Exception ex)  { 
                MessageBox.Show("Bạn đăng nhập không thành công kiểm tra lại thông tin kết nối và đường truyền mạng.\n" + ex.Message); }
            finally    {
                con.Close();
            }
        }
        private void FrmLogin_Load(object sender, EventArgs e)     {
            if (File.Exists("key.xml"))     {
                string xml = File.ReadAllText("key.xml");
                myrsa.FromXmlString(xml);

                Microsoft.Win32.RegistryKey ReadKey;
                ReadKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("AutoCareAccount");

                string UserName = "";
                string Password = "";

                try      {
                    UserName = AutoCareUtil.Utilities.Decode(ReadKey.GetValue("UserName").ToString());
                    Password = AutoCareUtil.Utilities.Decode(ReadKey.GetValue("Password").ToString());
                }
                catch (Exception) { }

                bool SavePass = false;
                try   {
                    SavePass = bool.Parse(ReadKey.GetValue("SavePassword").ToString());
                }
                catch (Exception) { }

                bool ConnectOnline = true;
                try    {
                    //ConnectOnline =bool.Parse(ReadKey.GetValue("ConnectOnline").ToString());
                    ConnectOnline = true;
                }
                catch (Exception)   { }
                chkConnectOptions.Checked = ConnectOnline;

                if (SavePass == true)    {
                    txtmatkhau.Text = Password;
                    txttendangnhap.Text = UserName;
                    chkSaveInfo.Checked = true;
                }
                else    {
                    txtmatkhau.Text = "";
                    txttendangnhap.Text = "";
                    chkSaveInfo.Checked = false;
                }
            }
            else   {
                File.WriteAllText("key.xml", myrsa.ToXmlString(true));
            }
        }
        private void FrmLogin_FormClosed(object sender, FormClosedEventArgs e)   {
            Application.Exit();
        }
        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)  {
            Application.Exit();
        }
        private void button1_Click(object sender, EventArgs e)  {
            Application.Exit();
        }
        private void label3_Click_1(object sender, EventArgs e){
            GetPass frmgetpass = new GetPass();
            frmgetpass.ShowDialog();
        }
        private void chkConnectOptions_CheckedChanged(object sender, EventArgs e)  {
            //if (chkConnectOptions.Checked==true)
            //{
            //    frmServerConfig ServerConfigForm = new frmServerConfig();
            //    ServerConfigForm.ShowDialog();
            //}
        }
        private void labelX1_Click(object sender, EventArgs e)   {
            frmServerConfig ServerConfigForm = new frmServerConfig();
            ServerConfigForm.ShowDialog();
        }
    }
}