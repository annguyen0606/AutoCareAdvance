using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Security.Cryptography;

namespace AutoCareV2._0
{
    public partial class FrmCauHinhKetNoi : Form
    {
        public FrmCauHinhKetNoi()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string connectDD = @"Data Source=" + txtTenMay.Text + ";Initial Catalog=" + txtCSDL.Text + ";Persist Security Info=True;User ID=" + txtUser.Text + ";Password=" + txtPass.Text;

                //Ghi file
                FileInfo myfile = new FileInfo("C:/OneSMS/DB.txt");
                StreamWriter tex = myfile.CreateText();
                tex.Write(tex.NewLine);
                tex.WriteLine(GetConnectDB(connectDD));
                tex.Close();
                MessageBox.Show("Tạo kết nối thành công!");
            }
            catch (Exception ex) { MessageBox.Show("Thất bại." + ex.Message); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txtTenMay.Text = null;
            txtUser.Text = null;
            txtPass.Text = null;
        }

        private static string GetMD5(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] bHash = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            StringBuilder sbHash = new StringBuilder();
            foreach (byte b in bHash)
            {
                sbHash.Append(String.Format("{0:x2}", b));
            }
            return sbHash.ToString();
        }

        private static string GetConnectDB(string str)
        {
            byte[] byteArray = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
            string base64String = System.Convert.ToBase64String(byteArray);
            return base64String;
        }
    }
}
