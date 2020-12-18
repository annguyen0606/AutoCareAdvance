using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;
using AutoCareV2._0.Class;
using DevComponents.DotNetBar;

namespace AutoCareV2._0
{
    public partial class FrmXoaLanBaoDuong : OfficeForm
    {
        #region Variables
        private string _idBd = "";
        private DataTable _tableBaoDuong = null;
        private int _rowIndexPhieu = -1;
        #endregion

        public FrmXoaLanBaoDuong()
        {
            InitializeComponent();

            grvDanhSachXeBaoDuong.AutoGenerateColumns = false;
        }
        
        private void LoadBd()
        {
            try
            {
                var cmd = new SqlCommand
                {
                    CommandText = "sp_LichSuChiTietBaoDuong",
                    CommandType = CommandType.StoredProcedure
                };
                cmd.Parameters.AddWithValue("@TuNgay", dt_TuNgay.Value.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@DenNgay", dt_DenNgay.Value.ToString("yyyyMMdd"));
                cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                _tableBaoDuong = datatabase.getData(cmd);

                grvDanhSachXeBaoDuong.DataSource = _tableBaoDuong;
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        private void btn_TimKiem_Click(object sender, EventArgs e)
        {
            LoadBd();
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            if (_idBd != "" || _idBd != null)
            {
                if (MessageBox.Show(@"Khi xóa sẽ ảnh hưởng tới thu chi, bạn có chắc muốn xóa không?", @"Thông báo", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        using (var cnn = datatabase.getConnection())
                        {
                            var cmd = new SqlCommand {Connection = cnn};

                            cmd.Connection.Open();
                            cmd.CommandText = "sp_delete_lichsubaoduong";
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
                            cmd.ExecuteNonQuery();

                            MessageBox.Show(@"Xóa thành công lần bảo dưỡng.", @"Thông báo", MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                            cmd.Connection.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    finally
                    {
                        LoadBd();
                    }
                }
            }
            else
            {
                MessageBox.Show(@"Hãy chọn Lần bảo dưỡng muốn xóa.", @"Thông báo", MessageBoxButtons.OK);
            }
        }

        private void dataGridViewX1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                _idBd = grvDanhSachXeBaoDuong.Rows[e.RowIndex].Cells["IdBaoDuong"].Value.ToString();
            }
            catch { _idBd = ""; }
        }

        private void FrmXoaLanBaoDuong_Load(object sender, EventArgs e)
        {
            dt_TuNgay.Value = DateTime.Now;
            dt_DenNgay.Value = DateTime.Now;
        }

        private void FrmXoaLanBaoDuong_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Control | Keys.F))
            {
                if (grvDanhSachXeBaoDuong.DataSource == null) return;

                var bienSo = new AutoCompleteStringCollection();
                var soMay = new AutoCompleteStringCollection();
                var soKhung = new AutoCompleteStringCollection();

                var arrrayBienSo = _tableBaoDuong.Rows.OfType<DataRow>().Select(k => k["BienSo"].ToString()).ToArray();
                var arrraySoMay = _tableBaoDuong.Rows.OfType<DataRow>().Select(k => k["SoMay"].ToString()).ToArray();
                var arrraySoKhung = _tableBaoDuong.Rows.OfType<DataRow>().Select(k => k["Sokhung"].ToString()).ToArray();

                bienSo.AddRange(arrrayBienSo);
                soMay.AddRange(arrraySoMay);
                soKhung.AddRange(arrraySoKhung);

                var frm = new FrmTimKiemLichSuBaoDuong(bienSo, soMay, soKhung, GetDataFromFrmSearch, _tableBaoDuong);
                frm.ShowDialog();
            }

            if (e.KeyCode == Keys.Escape)
                LoadBd();
        }

        private void GetDataFromFrmSearch(DataTable resultDataTable)
        {
            grvDanhSachXeBaoDuong.DataSource = resultDataTable;
        }

        private void inPhieuBaoDuongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rowIndexPhieu == -1) return;
            try
            {
                Class.SelectedCustomer._idbaoduong = grvDanhSachXeBaoDuong.Rows[_rowIndexPhieu].Cells["IdBaoDuong"].Value.ToString();

                if (Convert.ToInt64(Class.CompanyInfo.idcongty) != 31)
                {
                    var frm = new FrmPhieuSuaChuaThangLoi();
                    frm.ShowDialog();
                }
                else
                {
                    var frm = new frmPhieuSuaChuaTM98();
                    frm.ShowDialog();
                }
            }
            catch
            {
                // ignored
            }
            finally { Class.SelectedCustomer._idbaoduong = ""; }
        }

        private void grvDanhSachXeBaoDuong_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                grvDanhSachXeBaoDuong.ContextMenuStrip = contextMenuStrip;

                try
                {
                    _rowIndexPhieu = grvDanhSachXeBaoDuong.CurrentRow != null
                        ? grvDanhSachXeBaoDuong.CurrentRow.Index
                        : -1;
                }
                catch (Exception)
                {
                    _rowIndexPhieu = -1;
                }
            }
            else
            {
                grvDanhSachXeBaoDuong.ContextMenuStrip = null;
                _rowIndexPhieu = -1;
            }
        }
    }
}