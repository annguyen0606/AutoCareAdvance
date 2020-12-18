using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class FrmQuanlynhomKH : Form
    {
        private string cn = Class.datatabase.connect;
        private DataTable dtnkh = new DataTable("NhomKhachHang");
        private DataTable dtct = new DataTable("CongTy");
        private SqlDataAdapter da = new SqlDataAdapter();
        private SqlConnection con;

        private void connect()
        {
            try
            {
                con = new SqlConnection(cn);
                con.Open();
            }
            catch
            {
                MessageBox.Show("Lỗi kết nối");
            }
        }

        private void getdata()
        {
            connect();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = @"select manhomkh as 'Mã nhóm KH',tennhomkh as 'Tên nhom KH',idcongty as'Mã công ty' from NhomKhachHang  where idcongty=" + Class.CompanyInfo.idcongty;
            da.SelectCommand = cmd;
            dtnkh = new DataTable();
            da.Fill(dtnkh);
            dtgvnhomkh.DataSource = dtnkh;

            cmd.CommandText = @"Select IdCongTy, TenCongTy from CongTy where idcongty=" + Class.CompanyInfo.idcongty;
            da.SelectCommand = cmd;
            da.Fill(dtct);
            con.Close();
            cmd.Connection.Close();
            cmd.Connection.Dispose();
            cbomacongty.DataSource = dtct;
            cbomacongty.DisplayMember = "TenCongTy";
            cbomacongty.ValueMember = "IdCongTy";

        }

        public FrmQuanlynhomKH()
        {
            InitializeComponent();
        }

        private void FrmQuanlynhomKH_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'autoCareDataSet.NhomKhachHang' table. You can move, or remove it, as needed.
            // this.nhomKhachHangTableAdapter.Fill(this.autoCareDataSet.NhomKhachHang);
            //connect();
            getdata();
            binding();
        }

        private void binding()
        {
            txtmanhomkh.DataBindings.Clear();
            txtmanhomkh.DataBindings.Add("Text", dtnkh, "Mã nhóm KH");
            txttennhomkh.DataBindings.Clear();
            txttennhomkh.DataBindings.Add("Text", dtnkh, "Tên nhom KH");
        }

        private void btnthem_Click(object sender, EventArgs e)
        {
            connect();
            DataRow row = dtnkh.NewRow();
            row["Tên nhom KH"] = txttennhomkh.Text;
            row["Mã công ty"] = Convert.ToInt64(cbomacongty.SelectedValue);
            dtnkh.RejectChanges();
            dtnkh.Rows.Add(row);
            SqlCommand cmdinsert = new SqlCommand();
            cmdinsert.Connection = con;
            cmdinsert.CommandText = @"Insert NhomKhachHang(TenNhomKH,IdCongty)
                                        values(@TenNhomKH,@IdCongty)";
            cmdinsert.Parameters.Add("@TenNhomKH", SqlDbType.NVarChar, 50, "Tên nhom KH");
            cmdinsert.Parameters.Add("@IdCongty", SqlDbType.BigInt, 50, "Mã công ty");

            da.InsertCommand = cmdinsert;
            da.Update(dtnkh);
            con.Close();
            MessageBox.Show("Bạn đã thêm thành công !");
            Reset();
        }

        private void btnxoa_Click(object sender, EventArgs e)
        {
            if (txtmanhomkh.Text.Length > 0)
            {
                DataRow row = dtnkh.Select("[Mã nhóm KH] = " + Convert.ToInt32(txtmanhomkh.Text))[0];
                row.BeginEdit();
                row.Delete();
                row.EndEdit();
                connect();
                SqlCommand commandDelete = new SqlCommand();
                commandDelete.Connection = con;
                commandDelete.CommandType = System.Data.CommandType.Text;
                commandDelete.CommandText = "Delete From NhomKhachHang where MaNhomKH = @MaNhomKH";

                commandDelete.Parameters.Add("@MaNhomKH", SqlDbType.BigInt, 50, "Mã nhóm KH");
                da.DeleteCommand = commandDelete;
                da.Update(dtnkh);
                con.Close();
                con.Dispose();
                MessageBox.Show("Bạn đã xóa thành công !", "THÔNG BÁO", MessageBoxButtons.OK);
                Reset();
            }
        }

        private void btnclear_Click(object sender, EventArgs e)
        {
            txtmanhomkh.Text = "";
            txttennhomkh.Text = "";
            cbomacongty.Text = "";
        }

        private void ResetControls()
        {
            txtmanhomkh.Text = "";
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txttennhomkh.Text))
            {
                MessageBox.Show("Bạn chưa nhập Tên nhóm khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txttennhomkh.Focus();
                ResetControls();
                return;
            }

            try
            {
                DataRow row = dtnkh.NewRow();
                row["Tên nhom KH"] = txttennhomkh.Text;
                row["Mã công ty"] = Convert.ToInt64(cbomacongty.SelectedValue);
                dtnkh.RejectChanges();
                dtnkh.Rows.Add(row);
                connect();
                SqlCommand cmdinsert = new SqlCommand();
                cmdinsert.Connection = con;
                cmdinsert.CommandText = @"Insert NhomKhachHang(TenNhomKH,IdCongty)
                                    values(@TenNhomKH,@IdCongty)";
                cmdinsert.Parameters.Add("@TenNhomKH", SqlDbType.NVarChar, 50, "Tên nhom KH");
                cmdinsert.Parameters.Add("@IdCongty", SqlDbType.BigInt, 50, "Mã công ty");

                da.InsertCommand = cmdinsert;
                da.Update(dtnkh);
                con.Close();
                con.Dispose();
                MessageBox.Show("Bạn đã thêm thành công!");
                getdata();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo");
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            if (txtmanhomkh.Text.Length > 0)
            {
                try
                {
                    DataRow row = dtnkh.Select("[Mã nhóm KH] = " + Convert.ToInt32(txtmanhomkh.Text))[0];
                    row.BeginEdit();
                    row.Delete();
                    row.EndEdit();
                    connect();
                    SqlCommand commandDelete = new SqlCommand();
                    commandDelete.Connection = con;
                    commandDelete.CommandType = System.Data.CommandType.Text;
                    commandDelete.CommandText = "Delete From NhomKhachHang where MaNhomKH = @MaNhomKH";

                    commandDelete.Parameters.Add("@MaNhomKH", SqlDbType.BigInt, 50, "Mã nhóm KH");
                    da.DeleteCommand = commandDelete;
                    da.Update(dtnkh);
                    con.Close();
                    con.Dispose();
                    MessageBox.Show("Bạn đã xóa thành công !", "THÔNG BÁO", MessageBoxButtons.OK);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message, "THÔNG BÁO");
                }
            }
            else
            {
                MessageBox.Show("Bạn chưa chọn Nhóm khách hàng!", "Thông báo");

                return;
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            txtmanhomkh.Text = "";
            txttennhomkh.Text = "";
            cbomacongty.Text = "";

            getdata();
        }

        private void dtgvnhomkh_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtmanhomkh.Text = dtgvnhomkh.Rows[e.RowIndex].Cells[0].Value.ToString();
                txttennhomkh.Text = dtgvnhomkh.Rows[e.RowIndex].Cells[1].Value.ToString();
            }
            catch { }
        }
    }
}