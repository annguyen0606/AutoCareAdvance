using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0.Class
{
    public partial class NgayThangNam : UserControl
    {
        public NgayThangNam()
        {
            InitializeComponent();
        }
        public bool IsDate
        {
            get
            {
                try
                {
                    new DateTime(int.Parse(comboBox3.Text), int.Parse(comboBox2.Text), int.Parse(comboBox1.Text)); //yyyy/MM/dd
                }
                catch
                {
                    return false;
                }
                return true;
            }
        }

        public DateTime Value
        {
            
            set
            {
                if (value != null)
                {
                    comboBox1.Text = value.Day.ToString();
                    comboBox2.Text = value.Month.ToString();
                    comboBox3.Text = value.Year.ToString();
                }
                else
                {
                    comboBox1.Text = "";
                    comboBox2.Text = "";
                    comboBox3.Text = "";
                }
            }
        }
        public string GetValue
        {
            get
            {
                DateTime dt; 
                try
                {
                   dt= new DateTime(int.Parse(comboBox3.Text), int.Parse(comboBox2.Text), int.Parse(comboBox1.Text)); //yyyy/MM/dd
                   return dt.ToShortDateString();
                }
                catch { return ""; }
            }
        }
        public void SetNull()
        {
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
        }
        private void NgayThangNam_Load(object sender, EventArgs e)
        {
            //for (int i = 1; i <= 31; i++)
            //{
            //    comboBox1.Items.Add(i.ToString());
            //}
            //for (int i = 1; i <= 12; i++)
            //{
            //    comboBox2.Items.Add(i.ToString());
            //}
            //for (int i = 1900; i <= 2999; i++)
            //{
            //    comboBox3.Items.Add(i.ToString());
            //}
        }
    }
}
