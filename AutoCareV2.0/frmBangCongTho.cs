using Microsoft.Reporting.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmBangCongTho : Form
    {
        public frmBangCongTho()
        {
            InitializeComponent();
        }

        private void frmBangCongTho_Load(object sender, EventArgs e)
        {
            dpkDateFrom.Value = dpkDateTo.Value = DateTime.Now;
        }

        private void btnStatistic_Click(object sender, EventArgs e)
        {
            if(dpkDateFrom.Value == null || dpkDateTo.Value == null)
            {
                MessageBox.Show("Bạn cần chọn thời gian lập bảng chấm công!\nVui lòng kiểm tra lại.", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                string idCongTy = Class.CompanyInfo.idcongty;
                string dateFrom = dpkDateFrom.Value.ToString("MM/dd/yyyy") + " 00:00:00";
                string datTo = dpkDateTo.Value.ToString("MM/dd/yyyy") + " 23:59:59";

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_BangChamCongThoDichVu";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@idcongty", idCongTy);
                cmd.Parameters.AddWithValue("@tungay", dateFrom);
                cmd.Parameters.AddWithValue("@denngay", datTo);

                DataTable tableResult = Class.datatabase.getData(cmd);

                SqlCommand cmdCongThoPhuTung = new SqlCommand();
                cmdCongThoPhuTung.CommandText = "sp_BangChamCongThoPhuTung";
                cmdCongThoPhuTung.CommandType = CommandType.StoredProcedure;
                cmdCongThoPhuTung.Parameters.AddWithValue("@idcongty", idCongTy);
                cmdCongThoPhuTung.Parameters.AddWithValue("@tungay", dateFrom);
                cmdCongThoPhuTung.Parameters.AddWithValue("@denngay", datTo);

                DataTable tableResultCongThoPhuTung = Class.datatabase.getData(cmdCongThoPhuTung);
                var maxRow = tableResult.Select("stt = MAX(stt)");
                int curentStt = 1;
                if (maxRow.Count() > 0)
                {
                    curentStt = curentStt + Convert.ToInt32(maxRow[0][0].ToString());
                }
                    

                foreach (DataRow rowCongThoPhuTung in tableResultCongThoPhuTung.Rows)
                {
                    bool isSeen = false;
                    foreach (DataRow rowCongThoCongViec in tableResult.Rows)
                    {
                        if(rowCongThoCongViec["idtho"] == rowCongThoPhuTung["IdTho"]
                            && rowCongThoCongViec["matho"] == rowCongThoPhuTung["MaTho"]
                            && rowCongThoCongViec["tentho"] == rowCongThoPhuTung["tenTho"])
                        {
                            isSeen = true;

                            rowCongThoCongViec["doanhthu"] = Convert.ToDecimal(rowCongThoCongViec["doanhthu"]) + Convert.ToDecimal(rowCongThoPhuTung["DoanhThu"]);
                            rowCongThoCongViec["congtho"] = Convert.ToDecimal(rowCongThoCongViec["congtho"]) + Convert.ToDecimal(rowCongThoPhuTung["TienCong"]);
                        }
                    }

                    if(!isSeen)
                    {
                        DataRow dr = tableResult.NewRow();
                        dr["stt"] = curentStt;
                        dr["idtho"] = rowCongThoPhuTung["IdTho"];
                        dr["matho"] = rowCongThoPhuTung["MaTho"];
                        dr["tentho"] = rowCongThoPhuTung["tenTho"];
                        dr["dinhky"] = "0";
                        dr["thanhtien"] = "0";
                        dr["doanhthu"] = Convert.ToDecimal(rowCongThoPhuTung["DoanhThu"]);
                        dr["congtho"] = Convert.ToDecimal(rowCongThoPhuTung["TienCong"]);
                        dr["baohanh"] = "0";

                        tableResult.Rows.Add(dr);
                        curentStt++;
                    }
                }

                reportViewerCongTho.Visible = true;
                reportViewerCongTho.ProcessingMode = ProcessingMode.Local;
                reportViewerCongTho.LocalReport.ReportEmbeddedResource = "AutoCareV2._0.Report.TemplateCongTho.rdlc";
                //reportViewerCongTho.LocalReport.ReportPath = Environment.CurrentDirectory + @"/Report\TemplateCongTho.rdlc";

                PermissionSet permissions = new PermissionSet(PermissionState.None);
                permissions.AddPermission(new FileIOPermission(PermissionState.Unrestricted));
                permissions.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
                reportViewerCongTho.LocalReport.SetBasePermissionsForSandboxAppDomain(permissions);

                ReportDataSource reportData = new ReportDataSource();
                reportData.Name = "DataCongTho";
                reportData.Value = tableResult;

                string _ThoiGian = "Từ ngày: " + dpkDateFrom.Value.ToString("dd/MM/yyyy") + " đến ngày: " + dpkDateTo.Value.ToString("dd/MM/yyyy");

                ReportParameter reportTenCty = new ReportParameter("TenCongTy", Class.CompanyInfo.tencongty);
                ReportParameter reportThoiGian = new ReportParameter("ThoiGian", _ThoiGian);

                reportViewerCongTho.LocalReport.DataSources.Clear();
                reportViewerCongTho.LocalReport.SetParameters(reportTenCty);
                reportViewerCongTho.LocalReport.SetParameters(reportThoiGian);
                reportViewerCongTho.LocalReport.DataSources.Add(reportData);
                reportViewerCongTho.LocalReport.Refresh();

                this.reportViewerCongTho.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frmBangCongTho_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.reportViewerCongTho.LocalReport.ReleaseSandboxAppDomain();
        }
    }
}
