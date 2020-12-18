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
    public partial class FrmKhachHang : Form
    {
        #region Variables

        public delegate void ReloadData();
        public new ReloadData Refresh;
        #endregion

        public FrmKhachHang()
        {
            InitializeComponent();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Refresh != null)
                Refresh();
        }
    }
}
