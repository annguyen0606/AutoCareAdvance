using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Data.OleDb;
using System.Data.Common;
using System.Data.SqlClient;

namespace AutoCareV2._0
{
    public partial class PhuTungUpdate : DevComponents.DotNetBar.OfficeForm
    {
        public PhuTungUpdate()
        {
            InitializeComponent();
        }

        string path = "";
        private void btn_ChonFile_Click(object sender, EventArgs e)        {
            OpenFileDialog choose = new OpenFileDialog();
            choose.Filter = "File Excel|*.xls;*xlsx";
            if (choose.ShowDialog() == DialogResult.OK)            {
                path = choose.FileName;
                txtPart.Text = choose.FileName;
                btn_Import.Enabled = true;
            }
        }

        private void UpdatePhuTung()        {
            SqlDataAdapter da;
            string maPTold = "", maPTnew = "";
            string IDKHOold = "", IDKHOnew = "";
            string slNew = "";
            string gia = "";
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.datatabase.getConnection();
            cmd.CommandText = "Select * from PhuTungUpdate Where idcongty=@idcongty";
            cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
            DataTable dtUpdate = new DataTable();
            dtUpdate = Class.datatabase.getData(cmd);
            cmd.Connection.Open();
            SqlTransaction tran = cmd.Connection.BeginTransaction();
            cmd.Transaction = tran;
            try            {
                cmd.CommandText = "select * from PhuTung where idcongty=@idcongty";

                DataTable dtNew = new DataTable();
                da = new SqlDataAdapter(cmd);
                da.Fill(dtNew);

                string where = "";

                if (chk_CapNhatGia.Checked == true && chk_CapNhatSoLuong.Checked == true)
                    where = " Dongia=@DonGia, SoLuong=@SoLuong ";
                else if (chk_CapNhatGia.Checked == true && chk_CapNhatSoLuong.Checked == false)
                    where = " DonGia=@DonGia ";
                else if (chk_CapNhatGia.Checked == false && chk_CapNhatSoLuong.Checked == true)
                    where = " SoLuong=@SoLuong ";

                if (where != "")                {
                    cmd.CommandText = "Update PhuTung set " + where + " Where idcongty=@idcongty and MaPT=@MaPT and IdKho=@IdKho";

                    for (int i = 0; i < dtUpdate.Rows.Count; i++)                    {
                        maPTold = dtUpdate.Rows[i]["MaPT"].ToString();
                        IDKHOold = dtUpdate.Rows[i]["IdKho"].ToString();
                        slNew = dtUpdate.Rows[i]["SoLuong"].ToString();
                        gia = dtUpdate.Rows[i]["DonGia"].ToString();

                        for (int j = 0; j < dtNew.Rows.Count; j++)                        {
                            IDKHOnew = dtNew.Rows[j]["IdKho"].ToString();
                            maPTnew = dtNew.Rows[j]["MaPT"].ToString();

                            if (maPTold == maPTnew && IDKHOold == IDKHOnew)                            {
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@SoLuong", slNew);
                                cmd.Parameters.AddWithValue("@gia", gia);
                                cmd.Parameters.AddWithValue("@MaPT", maPTold);
                                cmd.Parameters.AddWithValue("@IdKho", IDKHOold);
                                cmd.Parameters.AddWithValue("@idcongty", Class.CompanyInfo.idcongty);
                                cmd.ExecuteNonQuery();
                            }
                        }
                    }

                    tran.Commit();
                    cmd.Connection.Close();
                    DeletePhuTungUpdate();
                    MessageBox.Show("Cập nhật số lượng phụ tùng thành công.", "Thông báo");
                }
                else                {
                    cmd.Connection.Close();
                    MessageBox.Show("Hãy chọn thông tin cần cập nhật.", "Thông báo");
                }
            }
            catch (Exception ex)            {
                tran.Rollback();
                cmd.Connection.Close();
                MessageBox.Show("Cập nhật số lượng Phụ tùng thất bại. Lỗi :" + ex.Message, "Thông báo");
            }
        }

        private void DeletePhuTungUpdate()        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = Class.datatabase.getConnection();
            cmd.CommandText = "delete from PhuTungUpdate Where IdCongTy=@IdCongTy";
            cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            Class.datatabase.ExcuteNonQuery(cmd);
        }

        private void Load_PhuTungUpdate()        {
            SqlCommand cm = new SqlCommand();
            cm.Connection = Class.datatabase.getConnection();
            cm.CommandText = "Select * from PhuTungUpdate Where IdCongTy=@IdCongTy";
            cm.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
            dataGridView1.DataSource = Class.datatabase.getData(cm);
        }

        private void btn_Import_Click(object sender, EventArgs e)        {
            try            {
                if (!String.IsNullOrEmpty(path))                {
                    string excelConnectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0};Extended Properties=Excel 8.0", path);

                    // Create Connection to Excel Workbook 
                    using (OleDbConnection connection =
                                 new OleDbConnection(excelConnectionString))                    {
                        OleDbCommand command = new OleDbCommand
                                ("Select * FROM [Sheet1$]", connection);
                        connection.Open();
                        // Create DbDataReader to Data Worksheet 
                        using (DbDataReader dr = command.ExecuteReader())                        {
                            // SQL Server Connection String 
                            string sqlConnectionString = Class.datatabase.connect;

                            // Bulk Copy to SQL Server 
                            using (SqlBulkCopy bulkCopy =
                                       new SqlBulkCopy(sqlConnectionString))                            {
                                bulkCopy.DestinationTableName = "PhuTungUpdate";
                                bulkCopy.WriteToServer(dr);
                                MessageBox.Show("Import thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    Load_PhuTungUpdate();
                    btn_Import.Enabled = false;
                }
                else MessageBox.Show("Bạn chưa chọn file excel.", "Thông báo");
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }

        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn muốn cập nhật số lượng phụ tùng ?", "Cập nhật số lượng phụ tùng", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
                UpdatePhuTung();

        }

        private void btn_HuyBo_Click(object sender, EventArgs e)
        {
            DeletePhuTungUpdate();
            this.Close();
        }

        private void PhuTungUpdate_FormClosing(object sender, FormClosingEventArgs e)
        {
            DeletePhuTungUpdate();
        }

        private void PhuTungUpdate_FormClosed(object sender, FormClosedEventArgs e)
        {
            DeletePhuTungUpdate();
        }
    }
}