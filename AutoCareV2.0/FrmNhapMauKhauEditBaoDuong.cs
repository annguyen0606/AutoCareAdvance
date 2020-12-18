using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class FrmNhapMauKhauEditBaoDuong : Form
    {
        #region Delegate

        public delegate void LoadDanhSachPhuTung();

        public LoadDanhSachPhuTung LayPhuTungBaoDuong;
        public LoadDanhSachPhuTung CallFromUcBaoDuong;

        #endregion Delegate

        #region Variable

        public string IdBaoDuong = "";
        private SqlCommand cmd = new SqlCommand();



        #endregion Variable

        public FrmNhapMauKhauEditBaoDuong()
        {
            InitializeComponent();
        }

        private void Confirm_Click(object sender, EventArgs e)
        {
            string passNew = Class.Checksum.GetMd5Hash(password.Text.Trim(), Class.CompanyInfo.secretkey);
            if (password.Text.Trim() != "")
            {
                cmd.CommandText = @"SELECT * FROM MatKhauEdit WHERE pass = @pass AND IdCongTy = @IdCongTy";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@pass", Class.Checksum.GetMd5Hash(password.Text, Class.CompanyInfo.secretkey));
                cmd.Parameters.AddWithValue("@IdCongTy", Class.EmployeeInfo.IdCongTy);
                DataTable tableMatKhauEdit = Class.datatabase.getData(cmd);
                if (tableMatKhauEdit.Rows.Count > 0)
                {
                    MessageBox.Show("Chúc mừng bạn có quyền edit Bảo Dưỡng !");
                    Class.EmployeeInfo.QuyenEdit = "CONFRIM";
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Mật khẩu không đúng !");
                }
            }
            else 
            {
                 MessageBox.Show("Bạn phải nhập mật khẩu !");
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}