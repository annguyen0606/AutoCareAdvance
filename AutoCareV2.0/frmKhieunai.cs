using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmKhieunai : Form
    {
        public string IdBaoDuong = "";
        private SqlCommand cmd = new SqlCommand();

        public frmKhieunai()
        {
            InitializeComponent();
            
        }
        private void Loadthongtinkhieunai()
        {
            cmd.CommandText = @"select * from LichSuBaoDuongXe WHERE idbaoduong = @idbaoduong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idbaoduong", IdBaoDuong);
            DataTable _minfo = Class.datatabase.getData(cmd);
            if (_minfo!=null)
            {
                try
                {
                    dateTimePicker1.Value = DateTime.Parse(_minfo.Rows[0]["NGAYKHIEUNAI"].ToString());
                }
                catch { }
                txtnoidung.Text = _minfo.Rows[0]["NOIDUNGKHIEUNAI"].ToString();
                txtbienphap.Text = _minfo.Rows[0]["BIENPHAP"].ToString();
                txtketqua.Text = _minfo.Rows[0]["KETQUAKN"].ToString();
            }
        }

        private void buttonThem_Click(object sender, EventArgs e)
        {
            cmd.CommandText = @"UPDATE LichSuBaoDuongXe
                                            SET NGAYKHIEUNAI ='"+ dateTimePicker1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "',NOIDUNGKHIEUNAI=N'" + txtnoidung.Text + "'," +
                                            "BIENPHAP=N'"+ txtbienphap.Text + "',KETQUAKN=N'"+ txtketqua.Text + "'" +
                                            " WHERE idbaoduong = "+ IdBaoDuong;
            cmd.Parameters.Clear();           
            Class.datatabase.ExcuteNonQuery(cmd);

            MessageBox.Show(@"Cập nhật thông tin khiếu nại thành công!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void frmKhieunai_Load(object sender, EventArgs e)
        {
            Loadthongtinkhieunai();
        }

        private void buttonThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
