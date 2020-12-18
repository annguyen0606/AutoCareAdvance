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
    public partial class GetPass : Form
    {
        public GetPass()
        {
            InitializeComponent();
            textBox2.Text = Class.CompanyInfo.secretkey;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string pass = textBox1.Text;
            string repass = Class.Checksum.GetMd5Hash(pass, textBox2.Text);

            textBox1.Text = repass;
        }
    }
}
