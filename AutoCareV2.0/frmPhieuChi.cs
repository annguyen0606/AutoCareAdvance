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
    public partial class frmPhieuChi : Form
    {
        public DataTable dtPhieuChi;

        public frmPhieuChi()
        {
            InitializeComponent();
        }

        private void frmPhieuChi_Load(object sender, EventArgs e)
        {
            if (dtPhieuChi.Rows.Count > 0)
            {
                DucTri_PhieuChiBindingSource.DataSource = dtPhieuChi;
                this.reportViewer1.RefreshReport();
            }
        }
    }
}
