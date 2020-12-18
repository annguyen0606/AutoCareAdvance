using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmUpdateSoftware : Form
    {
        public string idsoftware = "";

        public frmUpdateSoftware()
        {
            InitializeComponent();
        }

        public Assembly ApplicationAssembly
        {
            get { return Assembly.GetExecutingAssembly(); }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonUpdate_Click(object sender, EventArgs e)
        {
            DoUpdate();
        }

        private void DoUpdate()
        {
            #region Do Update
            try
            {
                DataTable dt = new DataTable();

                using (SqlConnection cnn = Class.datatabase.getConnection())
                {
                    cnn.Open();

                    SqlDataAdapter adap = new SqlDataAdapter(@"SELECT TOP 1 * FROM Software_Update ORDER BY SoftwareId DESC", cnn);

                    adap.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["NewVersion"].ToString() != textBoxCurentVersion.Text)
                        {
                            frmMessUpdateSW frm = new frmMessUpdateSW();
                            frm.NameVersion = dt.Rows[0]["SWName"].ToString();
                            frm.OldVerSion = textBoxCurentVersion.Text;
                            frm.NewVersion = dt.Rows[0]["NewVersion"].ToString();
                            frm.ChangeLogs = dt.Rows[0]["ChangeLogs"].ToString();
                            frm.UpdateLocation = dt.Rows[0]["UpdateLocation"].ToString();
                            frm.FileSize = dt.Rows[0]["FileSize"].ToString();
                            frm.md5 = dt.Rows[0]["md5"].ToString();

                            Uri location = new Uri(dt.Rows[0]["UpdateLocation"].ToString());
                            string fileName = dt.Rows[0]["SWName"].ToString();
                            idsoftware = dt.Rows[0]["SoftwareId"].ToString();

                            if (frm.ShowDialog() == DialogResult.OK)
                            {
                                frmUpdateDownload form = new frmUpdateDownload(location, fileName, idsoftware, textBoxCurentVersion.Text, dt.Rows[0]["NewVersion"].ToString());
                                DialogResult result = form.ShowDialog();

                                if (result == DialogResult.OK)
                                {
                                    dt.Dispose();
                                    cnn.Close();   
                                }
                                else if (result == DialogResult.Abort)
                                {
                                    MessageBox.Show("Tải về bản cập nhật đã bị hủy bỏ!\nChương trình chưa được cập nhật!", "Hủy bỏ tải về cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                else
                                {
                                    MessageBox.Show("Đã xảy ra vấn đề trong lúc tải về bản cập nhật!\nVui lòng thử lại sau!", "Lỗi tải về bản cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("Bạn đang dùng phiên bản mới nhất của phần mềm Autocare");
                        }
                    }
                    cnn.Close();
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message + "\nVui lòng kiểm tra lại đường truyền Internet!"); }
            #endregion
        }

        private void frmUpdateSoftware_Load(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey ReadKey;
            ReadKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("AutoCareUpdate");

            bool AutoUpdate = false;
            try
            {
                AutoUpdate = bool.Parse(ReadKey.GetValue("AutoUpdate").ToString());
            }
            catch (Exception) { }

            if (AutoUpdate == true)
            {
                checkBoxAutoUpdate.Checked = true;
            }
            else
            {
                checkBoxAutoUpdate.Checked = false;
            }
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            if (checkBoxAutoUpdate.Checked == true)
            {
                this.Hide();

                Microsoft.Win32.RegistryKey ReadKey;
                ReadKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("AutoCareUpdate", true);
                if (ReadKey == null)
                {
                    Microsoft.Win32.RegistryKey key;
                    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("AutoCareUpdate");
                    key.SetValue("AutoUpdate", true);
                    key.Close();
                }
                else
                {
                    ReadKey.SetValue("AutoUpdate", true);
                    ReadKey.Close();
                }

                DoUpdate();
            }
            else
            {
                Microsoft.Win32.RegistryKey ReadKey;
                ReadKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("AutoCareUpdate", true);
                if (ReadKey == null)
                {
                    Microsoft.Win32.RegistryKey key;
                    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("AutoCareUpdate");
                    key.SetValue("AutoUpdate", false);
                    key.Close();
                }
                else
                {
                    ReadKey.SetValue("AutoUpdate", false);
                    ReadKey.Close();
                }
            }
            this.Close();
        }
    }
}
