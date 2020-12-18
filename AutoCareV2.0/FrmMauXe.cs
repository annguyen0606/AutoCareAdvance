using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class FrmMauXe : DevComponents.DotNetBar.OfficeForm
    {
        private string idmauxe = null;

        public FrmMauXe()
        {
            InitializeComponent();
        }

        void ResetControls()
        {
            txtGhiChu.Text = "";
            txtMauXe.Text = "";
        }

        private void Loadbangmau()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select MauXeMay.TenMauXe,MauXeMay.IdMauXe,CongTy.TenCongTy,MauXeMay.GhiChu from MauXeMay inner join CongTy on CongTy.IdCongTy=MauXeMay.Idcongty where MauXeMay.IdCongTy=@idct";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@idct", Class.CompanyInfo.idcongty);
            dataGridViewX1.DataSource = Class.datatabase.getData(cmd);
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtMauXe.Text))
            {
                MessageBox.Show("Bạn chưa nhập Tên màu xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMauXe.Focus();

                return;
            }

            SqlCommand cmd = new SqlCommand("insert into MauXeMay (IdCongTy, TenMauXe, GhiChu) values (@IdCongTy, @TenMauXe, @GhiChu)");
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@TenMauXe", txtMauXe.Text);
            cmd.Parameters.AddWithValue("@GhiChu", txtGhiChu.Text);
            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
            {
                MessageBox.Show("Thêm màu thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Loadbangmau();
                ResetControls();
                return;
            }
        }

        private void btnHuyBo_Click(object sender, EventArgs e)
        {
            if (idmauxe == null)
            {
                MessageBox.Show("Bạn chưa chọn màu xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            DialogResult chon = MessageBox.Show("Bạn có chắc chắn muốn xóa màu xe này?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (chon == DialogResult.Yes)
            {
                SqlCommand cmd = new SqlCommand("DELETE MauXeMay WHERE IdCongTy=@IdCongTy AND IdMauXe=@IdMauXe");
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                cmd.Parameters.AddWithValue("@IdMauXe", idmauxe);
                try
                {
                    if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
                    {
                        MessageBox.Show("Xóa màu xe thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Loadbangmau();
                        ResetControls();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
            }
        }

        private void FrmMauXe_Load(object sender, EventArgs e)
        {
            this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            Loadbangmau();
        }

        private void dataGridViewX1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMauXe.Text = dataGridViewX1.Rows[e.RowIndex].Cells["MauXe"].Value.ToString();
                txtGhiChu.Text = dataGridViewX1.Rows[e.RowIndex].Cells["GhiChu"].Value.ToString();
                idmauxe = dataGridViewX1.Rows[e.RowIndex].Cells["IdMauXe"].Value.ToString();
            }
            catch { idmauxe = null; }
        }

        private void btnCapnhat_Click(object sender, EventArgs e)
        {
            if (idmauxe == null)
            {
                MessageBox.Show("Bạn chưa chọn màu xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (String.IsNullOrEmpty(txtMauXe.Text))
            {
                MessageBox.Show("Bạn chưa nhập Tên màu xe!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMauXe.Focus();

                return;
            }

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "update MauXeMay set TenMauXe =@ten, GhiChu=@ghichu where IdMauXe=@idmauxe and IdCongTy = @idcongty";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@ten", txtMauXe.Text);
            cmd.Parameters.AddWithValue("@ghichu", txtGhiChu.Text);
            cmd.Parameters.AddWithValue("@idmauxe", idmauxe);
            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
            {
                MessageBox.Show("Cập nhật thành công", "Thông Báo", MessageBoxButtons.OK);
                ResetControls();
                Loadbangmau();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Thông Báo", MessageBoxButtons.OK);
            }
        }
    }
}