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
    public partial class FrmPhieuXuatKho : Form
    {
        public FrmPhieuXuatKho()
        {
            InitializeComponent();
        }

        private void FrmPhieuXuatKho_Load(object sender, EventArgs e)
        {
            this.reportViewer1.RefreshReport();
          
        }

        private void FrmPhieuXuatKho_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewer1.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}
