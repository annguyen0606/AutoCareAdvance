using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmServerConfig : Form
    {
        public frmServerConfig()
        {
            InitializeComponent();
            try
            {
                Microsoft.Win32.RegistryKey ReadKey;
                ReadKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("AutoCareAccount", true);
                if (ReadKey != null)
                {
                    string ServerName = AutoCareUtil.Utilities.Decode(ReadKey.GetValue("ServerName").ToString());
                    string DatabaseUser = AutoCareUtil.Utilities.Decode(ReadKey.GetValue("DatabaseUser").ToString());
                    string DatabasePassword = AutoCareUtil.Utilities.Decode(ReadKey.GetValue("DatabasePassword").ToString());
                    string DatabaseName = AutoCareUtil.Utilities.Decode(ReadKey.GetValue("DatabaseName").ToString());

                    txtDatabaseName.Text = DatabaseName;
                    txtDatabasePass.Text = DatabasePassword;
                    txtDatabaseUser.Text = DatabaseUser;
                    txtServerName.Text = ServerName;
                }
            }
            catch (Exception)
            {}
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey ReadKey;
            ReadKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("AutoCareAccount", true);
            if (ReadKey == null)
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("AutoCareAccount");
                key.SetValue("ServerName",AutoCareUtil.Utilities.Encode(txtServerName.Text.Trim()));
                key.SetValue("DatabaseUser", AutoCareUtil.Utilities.Encode(txtDatabaseUser.Text.Trim()));
                key.SetValue("DatabasePassword", AutoCareUtil.Utilities.Encode(txtDatabasePass.Text.Trim()));
                key.SetValue("DatabaseName", AutoCareUtil.Utilities.Encode(txtDatabaseName.Text.Trim()));
                key.Close();
            }
            else
            {
                ReadKey.SetValue("ServerName", AutoCareUtil.Utilities.Encode(txtServerName.Text.Trim()));
                ReadKey.SetValue("DatabaseUser", AutoCareUtil.Utilities.Encode(txtDatabaseUser.Text.Trim()));
                ReadKey.SetValue("DatabasePassword", AutoCareUtil.Utilities.Encode(txtDatabasePass.Text.Trim()));
                ReadKey.SetValue("DatabaseNAme", AutoCareUtil.Utilities.Encode(txtDatabaseName.Text.Trim()));
                ReadKey.Close();
            }
            MessageBox.Show("Đã lưu thông tin cấu hình");
        }
    }
}
