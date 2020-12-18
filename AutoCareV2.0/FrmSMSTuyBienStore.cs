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
    public partial class FrmSMSTuyBienStore : Form
    {
        private static string ID = "";
        public FrmSMSTuyBienStore()
        {
            InitializeComponent();
        }

        private void LichDangNhan()
        {
            using (SqlConnection mycon = new SqlConnection(AutoCareV2._0.Class.datatabase.connect))
            {
                mycon.Open();
                //Lay cac IdSMSTuyBien dang gui
                SqlDataAdapter daIdTyBien = new SqlDataAdapter("select DISTINCT IdSMSTuyBien from TinNhan Where IdCongTy=" + AutoCareV2._0.Class.CompanyInfo.idcongty + " and IdSMSTuyBien is not null", mycon);
                DataTable dtIdTuyBien = new DataTable();
                daIdTyBien.Fill(dtIdTuyBien);
                string IdSMSTuyBien = "";
                for (int i = 0; i < dtIdTuyBien.Rows.Count; i++)
                {
                    if (i == dtIdTuyBien.Rows.Count - 1)
                        IdSMSTuyBien += dtIdTuyBien.Rows[i]["IdSMSTuyBien"].ToString();
                    else
                        IdSMSTuyBien += dtIdTuyBien.Rows[i]["IdSMSTuyBien"].ToString() + ",";
                }
                if (IdSMSTuyBien != "")
                {
                    SqlDataAdapter da = new SqlDataAdapter("select * from SMSTuyBienStore Where IdCongTy=" + AutoCareV2._0.Class.CompanyInfo.idcongty + " and IdSMSTuyBien in (" + IdSMSTuyBien + ")", mycon);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    grv_LichDangNhan.DataSource = dt;
                }
                else grv_LichDangNhan.DataSource = null; ;
                mycon.Close();
            }
        }

        private void LichDaNhan()
        {
            using (SqlConnection mycon = new SqlConnection(AutoCareV2._0.Class.datatabase.connect))
            {
                mycon.Open();
                //Lay cac IdSMSTuyBien dang gui
                SqlDataAdapter daIdTyBien = new SqlDataAdapter("select DISTINCT IdSMSTuyBien from TinNhan Where IdCongTy=" + AutoCareV2._0.Class.CompanyInfo.idcongty + " and IdSMSTuyBien is not null", mycon);
                DataTable dtIdTuyBien = new DataTable();
                daIdTyBien.Fill(dtIdTuyBien);
                string IdSMSTuyBien = "";
                for (int i = 0; i < dtIdTuyBien.Rows.Count; i++)
                {
                    if (i == dtIdTuyBien.Rows.Count - 1)
                        IdSMSTuyBien += dtIdTuyBien.Rows[i]["IdSMSTuyBien"].ToString();
                    else
                        IdSMSTuyBien += dtIdTuyBien.Rows[i]["IdSMSTuyBien"].ToString() + ",";
                }
                string sql = "";
                if (IdSMSTuyBien != "")
                {
                    sql = "select * from SMSTuyBienStore Where IdCongTy=" + AutoCareV2._0.Class.CompanyInfo.idcongty + " and IdSMSTuyBien not in (" + IdSMSTuyBien + ")";
                }
                else sql = "select * from SMSTuyBienStore Where IdCongTy=" + AutoCareV2._0.Class.CompanyInfo.idcongty;

                SqlDataAdapter da = new SqlDataAdapter(sql, mycon);
                DataTable dt = new DataTable();
                da.Fill(dt);
                grv_LichDaNhan.DataSource = dt;
                mycon.Close();
            }
        }

        private void FrmSMSTuyBienStore_Load(object sender, EventArgs e)
        {
            LichDangNhan();
            LichDaNhan();
        }

        private void grv_LichDangNhan_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (grv_LichDangNhan.SelectedRows.Count > 0)
                {
                    ID = grv_LichDangNhan.SelectedRows[0].Cells["IdSMSTuyBien"].Value.ToString();
                }
            }
            catch { }
        }

        private void btn_Huy_Click(object sender, EventArgs e)
        {
            if (ID != "")
            {
                if (MessageBox.Show("Bạn có hủy lịch gửi tin ?", "Lịch gửi tin", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                {
                    using (SqlConnection mycon = new SqlConnection(AutoCareV2._0.Class.datatabase.connect))
                    {
                        mycon.Open();
                        SqlCommand cmd = new SqlCommand("delete from TinNhan where IdSMSTuyBien=@IdSMSTuyBien", mycon);
                        cmd.Parameters.AddWithValue("@IdSMSTuyBien", ID);
                        cmd.ExecuteNonQuery();
                        mycon.Close();
                        ID = "";
                        LichDangNhan();
                    }
                }
            }
            else MessageBox.Show("Hãy chọn lịch muốn hủy !");
        }

        private void btn_Moi_Click(object sender, EventArgs e)
        {
            ID = "";
            LichDangNhan();
            LichDaNhan();
        }

        private void btn_Sua_Click(object sender, EventArgs e)
        {
            if (ID != "")
            {
                DataTable dt = new DataTable();
                DataTable dskh = new DataTable();

                using(SqlConnection cnn = Class.datatabase.getConnection())
                {
                    cnn.Open();

                    SqlDataAdapter adap = new SqlDataAdapter(@"select * from SMSTuyBienStore Where IdCongTy=@IdCongTy and IdSMSTuyBien=@IdSMSTuyBien", cnn);
                    adap.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    adap.SelectCommand.Parameters.AddWithValue("@IdSMSTuyBien", Convert.ToInt64(ID));

                    adap.Fill(dt);

                    SqlDataAdapter adap1 = new SqlDataAdapter(@"select kh.* from KhachHang kh, TinNhan tn Where kh.IdCongTy=@IdCongTy and tn.IdKhachHang=kh.IdKhachHang and tn.IdSMSTuyBien=@IdSMSTuyBien", cnn);
                    adap1.SelectCommand.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    adap1.SelectCommand.Parameters.AddWithValue("@IdSMSTuyBien", Convert.ToInt64(ID));

                    adap1.Fill(dskh);

                    cnn.Close();
                }

                FrmSMSTuyBien frm = new FrmSMSTuyBien();
                frm.Id = ID;
                frm.txt_GioNhan.Text = dt.Rows[0][5].ToString();
                frm.txt_SMS.Text = dt.Rows[0][3].ToString();
                frm.dt_NgayNhan.Value = Convert.ToDateTime(dt.Rows[0][6].ToString());
                frm.dskh = dskh;

                frm.ShowDialog();
            }
            else MessageBox.Show("Hãy chọn lịch muốn sửa!");

        }
    }
}
