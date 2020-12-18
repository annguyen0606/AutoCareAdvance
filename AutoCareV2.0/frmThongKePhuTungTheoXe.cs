using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmThongKePhuTungTheoXe : Form
    {
        private DataTable dtTho = new DataTable();
        private DataTable dtPhuTung = new DataTable();
        private DataTable dtCongViec = new DataTable();

        public frmThongKePhuTungTheoXe()
        {
            InitializeComponent();
        }

        private void showthongtincongty()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "select * from CongTy where IdCongTy=" + Class.CompanyInfo.idcongty;
            DataTable dtThongTin = new DataTable();
            dtThongTin = Class.datatabase.getData(cmd);
            Microsoft.Reporting.WinForms.ReportDataSource data3 = new Microsoft.Reporting.WinForms.ReportDataSource("DataSet4", dtThongTin);
            reportViewer1.LocalReport.DataSources.Add(data3);
        }


        private void frmThongKePhuTungTheoXe_Load(object sender, EventArgs e)
        {
            dateTimeInputDenNgay.Value = DateTime.Now;
            dateTimeInputTuNgay.Value = DateTime.Now;
            SqlCommand cmd = new SqlCommand("select IDTho, MaTho + ' - ' + TenTho As TenTho From ThoDichVu Where IdCongTy = @IdCongTy");
            cmd.Parameters.AddWithValue("@IDCongTy", Class.CompanyInfo.idcongty);
            dtTho = Class.datatabase.getData(cmd);

            DataRow drTho = dtTho.NewRow();
            drTho["IdTho"] = -1;
            drTho["TenTho"] = "-- Tất cả thợ --";
            dtTho.Rows.InsertAt(drTho, 0);

            cboChonTho.DataSource = dtTho;
            cboChonTho.ValueMember = "IdTho";
            cboChonTho.DisplayMember = "TenTho";
            cmd.CommandText = "Select IdPT, MaPT + ' - ' + TenPT as TenPT From PhuTung Where IdCongTy = @IdCongTy";
            dtPhuTung = Class.datatabase.getData(cmd);

            DataRow drPhuTung = dtPhuTung.NewRow();
            drPhuTung["IdPT"] = -1;
            drPhuTung["TenPT"] = "-- Tất cả phụ tùng --";
            dtPhuTung.Rows.InsertAt(drPhuTung, 0);

            cbovatTu.DataSource = dtPhuTung;
            cbovatTu.DisplayMember = "TenPT";
            cbovatTu.ValueMember = "IdPT";
            cmd.CommandText = "SELECT IDTienCong, MaBD + ' - ' + NoiDungBD as MaBD FROM TienCongTho WHERE IdCongTy = @IDCongTy";
            dtCongViec = Class.datatabase.getData(cmd);

            DataRow drCongViec = dtCongViec.NewRow();
            drCongViec["IDTienCong"] = -1;
            drCongViec["MaBD"] = "-- Tất cả công việc --";
            dtCongViec.Rows.InsertAt(drCongViec, 0);

            showthongtincongty();
            this.reportViewer1.RefreshReport();
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            try
            {
                if (dateTimeInputDenNgay.ValueObject == null && dateTimeInputTuNgay.ValueObject == null)
                {
                    MessageBox.Show("Bạn chưa nhập đủ thời gian thống kê.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (dateTimeInputTuNgay.Value > dateTimeInputDenNgay.Value.AddSeconds(1))
                {
                    MessageBox.Show("Thời gian thống kê không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                SqlCommand cmd = new SqlCommand("sp_ThongKePhuTungTheoTheoThoiGianvaTho2");
                DataTable dtTho2 = new DataTable();
                dtTho2.Columns.Add("TenTho");
                DataTable dtThoiGian = new DataTable();
                dtThoiGian.Columns.Add("TuNgay", typeof(DateTime));
                dtThoiGian.Columns.Add("DenNgay", typeof(DateTime));
                dtThoiGian.Rows.Add(dateTimeInputTuNgay.Value, dateTimeInputDenNgay.Value);
                //*************************************
                if (checkBoxX2.Checked)
                {
                    cmd.CommandText = "sp_ThongKePhuTungTheoTheoCongViecvaTho";
                    if (!cbovatTu.SelectedValue.ToString().Equals("-1"))
                        cmd.Parameters.AddWithValue("@IDTienCong", cbovatTu.SelectedValue);
                    else cmd.Parameters.AddWithValue("@IDTienCong", SqlString.Null);
                }
                else
                {
                    cmd.CommandText = "sp_ThongKePhuTungTheoTheoThoiGianvaTho2";
                    if (!cbovatTu.SelectedValue.ToString().Equals("-1"))
                        cmd.Parameters.AddWithValue("@IdPT", cbovatTu.SelectedValue);
                    else cmd.Parameters.AddWithValue("@IdPT", SqlString.Null);
                }
                //************************************
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@TuNgay", dateTimeInputTuNgay.Value);
                cmd.Parameters.AddWithValue("@DenNgay", dateTimeInputDenNgay.Value);
                cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                DataRow row = dtTho2.NewRow();
                if (cboChonTho.SelectedValue.ToString().Equals("-1"))
                {
                    cmd.Parameters.AddWithValue("@IdTho", SqlString.Null);
                    row["TenTho"] = "Tất cả thợ";
                }
                else
                {
                    string idTho = Convert.ToString(cboChonTho.SelectedValue);
                    cmd.Parameters.AddWithValue("@IdTho", cboChonTho.SelectedValue);
                    try
                    {
                        row["TenTho"] = Convert.ToString(dtTho.Select("IdTho = '" + idTho + "'")[0]["TenTho"]);
                    }
                    catch (Exception ex) { }
                }
                dtTho2.Rows.Add(row);
                DataTable dtThongKe = Class.datatabase.getData(cmd);
                ThongKePhuTungTheoThoiGianVaThoBindingSource.DataSource = dtThongKe;
                ThoiGianBindingSource.DataSource = dtThoiGian;
                ThoBindingSource.DataSource = dtTho2;
                showthongtincongty();
                reportViewer1.RefreshReport();
            }
            //catch { }
            catch (Exception ex)
            {
                MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void checkBoxX2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBoxX2.Checked)
            {
                labelX3.Text = "Công việc:";
                cbovatTu.DataSource = dtCongViec;
                cbovatTu.DisplayMember = "MaBD";
                cbovatTu.ValueMember = "IDTienCong";
                //********
                cbovatTu.Enabled = true;
                //********
            }
            else
            {
                labelX3.Text = "Vật tư:";
                cbovatTu.DataSource = dtPhuTung;
                cbovatTu.DisplayMember = "TenPT";
                cbovatTu.ValueMember = "IdPT";
                //********
                cbovatTu.Enabled = true;
                //********
            }
        }
    }
}