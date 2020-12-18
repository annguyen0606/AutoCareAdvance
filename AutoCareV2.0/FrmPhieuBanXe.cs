using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.SqlClient;

namespace AutoCareV2._0
{
    public partial class FrmPhieuBanXe : DevComponents.DotNetBar.OfficeForm
    {
        public DataTable dtHoaDon;

        public FrmPhieuBanXe()
        {
            InitializeComponent();
        }

        private void FrmPhieuBanXe_Load(object sender, EventArgs e)
        {
            if(dtHoaDon.Rows.Count > 0)
            {
                PhieuBanXeBindingSource.DataSource = dtHoaDon;
                this.reportViewer1.RefreshReport();
            }
            else
            { 
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = Class.datatabase.getConnection();

                cmd.CommandText = "sp_Report_BanXe";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SoHoaDonBanHang", Class.Phieu.soPhieuBanXe);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                DataColumn cl1 = new DataColumn("TienDaThanhToan");
                cl1.DataType = typeof(decimal);
                dt.Columns.Add(cl1);

                DataRow dtrow = dt.NewRow();
                dtrow["TienDaThanhToan"] = Class.Phieu.tienDaThanhToan;
                dt.Rows.Add(dtrow);

                PhieuBanXeBindingSource.DataSource = dt;
                this.reportViewer1.RefreshReport();
            }
        }
    }
}