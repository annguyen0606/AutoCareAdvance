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
    public partial class frmMessUpdateSW : Form
    {
        public string NameVersion = "";
        public string OldVerSion = "";
        public string NewVersion = "";
        public string ChangeLogs = "";
        public string UpdateLocation = "";
        public string FileSize = "";
        public string md5 = "";
        public frmMessUpdateSW()
        {
            InitializeComponent();
        }

        private void btn_Detail_Click(object sender, EventArgs e)
        {
            frmUpdateSWInfo frm = new frmUpdateSWInfo();
            frm.lblversions.Text = "Phiên bản hiện tại: " + OldVerSion + "\nPhiên bản cập nhật: " + NewVersion;
            frm.txtDescription.Text = ChangeLogs;

            frm.ShowDialog();
        }
    }
}
