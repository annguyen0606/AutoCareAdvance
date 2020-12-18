using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace AutoCareV2._0
{
    public partial class FrmKichHoatSMS : Form
    {
        public FrmKichHoatSMS()
        {
            InitializeComponent();
        }

        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "update smsbirthdayconfig set HoatDong=@HoatDong where IdCongTy=@IdCongTy";
                cmd.Parameters.Add("@HoatDong", chk_SinhNhat.Checked);
                cmd.Parameters.Add("@IdCongTy", Class.CompanyInfo.idcongty);
                Class.datatabase.ExcuteNonQuery(cmd);

                cmd.CommandText = "update SMSCaringConfig set HoatDong=@HoatDong where IdCongTy=@IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@HoatDong", chk_MuaXe.Checked);
                cmd.Parameters.Add("@IdCongTy", Class.CompanyInfo.idcongty);
                Class.datatabase.ExcuteNonQuery(cmd);

                cmd.CommandText = "update SMSMaintenanceConfig set HoatDong=@HoatDong where IdCongTy=@IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@HoatDong", chk_BaoDuongDinhKy.Checked);
                cmd.Parameters.Add("@IdCongTy", Class.CompanyInfo.idcongty);
                Class.datatabase.ExcuteNonQuery(cmd);

                cmd.CommandText = "update SMSNhanDichVuConfig set HoatDong=@HoatDong where IdCongTy=@IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@HoatDong", chk_BaoDuongDV.Checked);
                cmd.Parameters.Add("@IdCongTy", Class.CompanyInfo.idcongty);
                Class.datatabase.ExcuteNonQuery(cmd);

                cmd.CommandText = "update SMSMaintenanceConfigALL set HoatDong=@HoatDong where IdCongTy=@IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.Add("@HoatDong", chk_BaoDuongToanBo.Checked);
                cmd.Parameters.Add("@IdCongTy", Class.CompanyInfo.idcongty);
                Class.datatabase.ExcuteNonQuery(cmd);

                MessageBox.Show("Cập nhật thành công.", "Thông báo");
            }
            catch (Exception ex) { MessageBox.Show("Cập nhật thất bại." + ex.Message, "Thông báo"); }
        }

        private void FrmKichHoatSMS_Load(object sender, EventArgs e)
        {
            try
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "sp_HoatDongSMS_Select";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@IdCongTy", Class.CompanyInfo.idcongty);
                DataTable dt = new DataTable();
                dt = Class.datatabase.getData(cmd);

                try
                {
                    chk_SinhNhat.Checked = bool.Parse(dt.Rows[0]["SinhNhat"].ToString());
                }
                catch { }
                try
                {
                    chk_MuaXe.Checked = bool.Parse(dt.Rows[0]["MuaXe"].ToString());
                }
                catch { }
                try
                {
                    chk_BaoDuongDinhKy.Checked = bool.Parse(dt.Rows[0]["BDdinhky"].ToString());
                }
                catch { }
                try
                {
                    chk_BaoDuongDV.Checked = bool.Parse(dt.Rows[0]["BDdichvu"].ToString());
                }
                catch { }
                try
                {
                    chk_BaoDuongToanBo.Checked = bool.Parse(dt.Rows[0]["BDtoanbo"].ToString());
                }
                catch { }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void chk_TatCa_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_TatCa.Checked)
            {
                chk_SinhNhat.Checked = true;
                chk_MuaXe.Checked = true;
                chk_BaoDuongDinhKy.Checked = true;
                chk_BaoDuongDV.Checked = true;
                chk_BaoDuongToanBo.Checked = true;
            }
            else
            {
                chk_SinhNhat.Checked = false;
                chk_MuaXe.Checked = false;
                chk_BaoDuongDinhKy.Checked = false;
                chk_BaoDuongDV.Checked = false;
                chk_BaoDuongToanBo.Checked = false;
            }
        }
    }
}
