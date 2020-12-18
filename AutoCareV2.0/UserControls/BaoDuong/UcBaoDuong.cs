using AutoCareV2._0.Class;
using Microsoft.AspNetCore.SignalR.Client;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutoCareV2._0.UserControls.BaoDuong
{
    public partial class UcBaoDuong : UserControl
    {
        #region Variable
        private string Error_SignalR = string.Empty;
        private SqlCommand _cmd = new SqlCommand();

        private readonly DataGridView _gridviewAutocompleteSoMay = new DataGridView();
        private readonly DataGridView _gridviewAutocompleteSoKhung = new DataGridView();

        private DataTable _tableSoKhungSoMay = new DataTable();
        private DataTable _dtxeBaoDuong = new DataTable();
        private DataTable _tableBaoDuong = new DataTable();
        private DataTable _tableXeDaGiao = new DataTable();

        private DataTable _dtPhuTungThayThe = new DataTable();
        private DataTable _dtChuanDoanXeTam = new DataTable();
        private DataTable _dtThoDichVuGioViecTam = new DataTable();
        private DataTable _dtThoDichVuTienCongTam = new DataTable();
        private DataTable _dtThueNgoaiTam = new DataTable();
        private DataTable _dtPhuTung = new DataTable();
        private DataTable _tableBaoGiaTam = new DataTable();
        private DataTable _tableBaoGiaCongThoTam = new DataTable();
        private DataTable _tableBaoGiaPhuTungTam = new DataTable();

        private string _idKhachHang = "";
        private string _idBd = "";
        private string _idBaoDuong = "";
        private string _ngaysinh = "";
        private string _idLichSuBaoDuong = "";
        private string _idXe = "";
        private string _idBaoDuongTam = "";
        private string idBaoDuongTam = "";

        private string _tenkh = "";
        private string _dienthoai = "";
        private string _solan = "";
        private string _tenxe = "";
        private double _chietkhau = 1;
        private int _check;

        private int _rowIndexPhieu = -1;
        private int _rowIndexGiaoTrongNgay = -1;
        private int _rowIndexCvTheoTien = -1;
        private int _rowIndexBaoGia = -1;
        private int _rowIndexThoTheoTien = -1;

        private EnumerableRowCollection<DataRow> _congviec;

        private readonly ChangeOilByKM _changeOilKm = new ChangeOilByKM();
        /***********************************************/
        #endregion Variable

        #region UcBaoDuong

        public UcBaoDuong()
        {
            InitializeComponent();
            /*SignalR*/
            connection = new HubConnectionBuilder()
                            .WithUrl("http://nfcapi.conek.net/AutoCare/SignalRServer")
                            .Build();
            connection.Closed += async (error) =>
            {
                await Task.Delay(new Random().Next(0, 5) * 1000);
                await connection.StartAsync();
            };
            /**/
            if (Class.EmployeeInfo.UserName == "vietlong2sale"
                || Class.EmployeeInfo.UserName == "vietlong3sale"
                || Class.EmployeeInfo.UserName == "vietlong1sale")
            {
                btnKhoTiepNhanPhuTung.Enabled = false;
            }
            if (Class.EmployeeInfo.UserName == "vietlong2kho"
                || Class.EmployeeInfo.UserName == "vietlong3kho"
                || Class.EmployeeInfo.UserName == "vietlong1kho")
            {
                btnLayPhieuBaoDuongDinhKy.Enabled = false;
                btnLayPhieuSuaChua.Enabled = false;
            }
            /*******************************************************/
            dataGridViewLichSuBaoDuong.AutoGenerateColumns = false;
            dataGridViewPhuTungBaoDuong.AutoGenerateColumns = false;
            dataGridViewPhuTungBaoDuong.ScrollBars = ScrollBars.Both;
            dataGridViewTheoGio.AutoGenerateColumns = false;
            dataGridViewTheoTien.AutoGenerateColumns = false;
            dataGridViewThueNgoai.AutoGenerateColumns = false;
            dataGridViewXeGiaoTrongNgay.AutoGenerateColumns = false;

            dateTimeInputNgaySinh.Text = "";
            dateTimeInputNgayMua.Text = "";
            dateTimeInputNgayVao.Value = DateTime.Now;
            dateTimeInputNgayRa.Value = DateTime.Now;
            dateTimeInputGioVao.Value = DateTime.Now;
            //

            dateTimeInputGioRa.Value = DateTime.Now;

            #region Events

            Load += UcBaoDuong_Load;
            comboBoxTimKiemLoaiBaoDuong.SelectedValueChanged += comboBoxLoaiBaoDuong_SelectedValueChanged;
            superTabControlBaoDuong.SelectedTabChanged += superTabControlBaoDuong_SelectedTabChanged;

            buttonTimKiem.Click += buttonTimKiem_Click;
            buttonHoanTat.Click += buttonHoanTat_Click;
            buttonHuyBo.Click += buttonHuyBo_Click;
            buttonInPhieu.Click += buttonInPhieu_Click;
            buttonThemXeBaoDuong.Click += buttonThemXeBaoDuong_Click;
            buttonCapNhatKhachHang.Click += buttonCapNhatKhachHang_Click;
            buttonXoaChu.Click += buttonXoaChu_Click;
            buttonThemCongViec.Click += buttonThemCongViec_Click;
            buttonLamMoiDanhSach.Click += buttonLamMoiDanhSach_Click;

            _gridviewAutocompleteSoMay.KeyDown += gridviewAutocompleteSoMay_KeyDown;
            _gridviewAutocompleteSoMay.KeyUp += gridviewAutocompleteSoMay_KeyUp;
            _gridviewAutocompleteSoMay.KeyPress += gridviewAutocompleteSoMay_KeyPress;

            textBoxTimKiemSoMay.KeyDown += textBoxTimKiemSoMay_KeyDown;
            textBoxTimKiemSoMay.KeyUp += textBoxTimKiemSoMay_KeyUp;
            textBoxTimKiemSoMay.KeyPress += textBoxTimKiemSoMay_KeyPress;
            textBoxTimKiemSoMay.Leave += textBoxTimKiemSoMay_Leave;
            textBoxTimKiemSoMay.TextChanged += textBoxTimKiemSoMay_TextChanged;

            _gridviewAutocompleteSoKhung.KeyDown += gridviewAutocompleteSoKhung_KeyDown;
            _gridviewAutocompleteSoKhung.KeyUp += gridviewAutocompleteSoKhung_KeyUp;
            _gridviewAutocompleteSoKhung.KeyPress += gridviewAutocompleteSoKhung_KeyPress;

            textBoxTimKiemSoKhung.KeyDown += textBoxTimKiemSoKhung_KeyDown;
            textBoxTimKiemSoKhung.KeyUp += textBoxTimKiemSoKhung_KeyUp;
            textBoxTimKiemSoKhung.KeyPress += textBoxTimKiemSoKhung_KeyPress;
            textBoxTimKiemSoKhung.Leave += textBoxTimKiemSoKhung_Leave;
            textBoxTimKiemSoKhung.TextChanged += textBoxTimKiemSoKhung_TextChanged;

            textBoxTimNhanhBienSo.KeyDown += textBoxTimNhanhBienSo_KeyDown;
            textBoxTimNhanhSoKhung.KeyDown += textBoxTimNhanhSoKhung_KeyDown;
            textBoxTimNhanhSoMay.KeyDown += textBoxTimNhanhSoMay_KeyDown;

            txtXeDaGiao_BienSo.KeyDown += txtXeDaGiao_BienSo_KeyDown;
            txtXeDaGiao_SoKhung.KeyDown += txtXeDaGiao_SoKhung_KeyDown;
            txtXeDaGiao_SoMay.KeyDown += txtXeDaGiao_SoMay_KeyDown;

            textBoxTienPhuTung.TextChanged += textBoxTienPhuTung_TextChanged;
            textBoxTienCongTho.TextChanged += textBoxTienCongTho_TextChanged;
            textBoxTienThueNgoai.TextChanged += textBoxTienThueNgoai_TextChanged;
            textBoxTongTien.TextChanged += textBoxTongTien_TextChanged;

            dataGridViewDanhSachXeDangBaoDuong.CellClick += dataGridViewDanhSachXeDangBaoDuong_CellClick;

            dataGridViewPhuTungBaoDuong.CellContentClick += dataGridViewPhuTungBaoDuong_CellContentClick;
            dataGridViewPhuTungBaoDuong.CellValueChanged += dataGridViewPhuTungBaoDuong_CellValueChanged;
            dataGridViewTheoTien.CellContentClick += dataGridViewTheoTien_CellContentClick;
            dataGridViewTheoGio.CellContentClick += dataGridViewTheoGio_CellContentClick;
            dataGridViewThueNgoai.CellContentClick += dataGridViewThueNgoai_CellContentClick;
            dataGridViewDanhSachXeDangBaoDuong.CellContentClick += dataGridViewDanhSachXeDangBaoDuong_CellContentClick;

            dataGridViewPhuTungBaoDuong.DataBindingComplete += dataGridViewPhuTungBaoDuong_DataBindingComplete;
            dataGridViewTheoTien.DataBindingComplete += dataGridViewTheoTien_DataBindingComplete;
            dataGridViewThueNgoai.DataBindingComplete += dataGridViewThueNgoai_DataBindingComplete;

            textBoxLanBaoDuong.TextChanged += textBoxLanBaoDuong_TextChanged;
            textBoxSoKm.TextChanged += textBoxSoKm_TextChanged;
            textBoxChietKhau.TextChanged += textBoxChietKhau_TextChanged;
            textBoxTienTrietKhau.TextChanged += TextBoxTienTrietKhau_TextChanged;

            dataGridViewXeGiaoTrongNgay.CellDoubleClick += dataGridViewXeGiaoTrongNgay_CellDoubleClick;
            dataGridViewXeGiaoTrongNgay.CellMouseDown += dataGridViewXeGiaoTrongNgay_CellMouseDown;
            dataGridViewLichSuBaoDuong.CellDoubleClick += dataGridViewLichSuBaoDuong_CellDoubleClick;
            dataGridViewLichSuBaoDuong.CellMouseDown += dataGridViewLichSuBaoDuong_CellMouseDown;
            dataGridViewDanhSachXeDangBaoDuong.CellMouseDown += dataGridViewDanhSachXeDangBaoDuong_CellMouseDown;
            dataGridViewTheoTien.CellMouseDown += dataGridViewTheoTien_CellMouseDown;

            InPhieuBaoDuongToolStripMenuItem.Click += InPhieuBaoDuongToolStripMenuItem_Click;
            InPhieuBaoGiaToolStripMenuItem.Click += InPhieuBaoGiaToolStripMenuItem_Click;
            SuaLanBaoDuongToolStripMenuItem.Click += SuaLanBaoDuongToolStripMenuItem_Click;
            LapPhieuBaoGiaToolStripMenuItem.Click += LapPhieuBaoGiaToolStripMenuItem_Click;
            KNToolStripMenuItem.Click += khieunai_Click;


            CopyCongViecToolStripMenuItem.Click += CopyCongViecToolStripMenuItem_Click;

            dataGridViewTheoTien.EditingControlShowing += dataGridViewTheoTien_EditingControlShowing;
            dataGridViewTheoTien.DataError += dataGridViewTheoTien_DataError;



            #endregion Events
        }

        private void TextBoxTienTrietKhau_TextChanged(object sender, EventArgs e)
        {
            decimal flag;

            if (!String.IsNullOrEmpty(textBoxTienTrietKhau.Text))
            {
                if (decimal.TryParse(textBoxTienTrietKhau.Text, out flag) == false)
                {
                    MessageBox.Show("Chiết khấu phải là kiểu số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    textBoxTienTrietKhau.Text = "0";
                    return;
                }

                textBoxTienTrietKhau.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienTrietKhau.Text));
                textBoxTienTrietKhau.SelectionStart = textBoxTienTrietKhau.Text.Length;
            }

            TinhTongTien();
        }

        #region Tìm kiếm nhanh xe đã giao

        private void txtXeDaGiao_SoMay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                XeDaGiaoOnDataGridview(sender);
        }

        private void txtXeDaGiao_SoKhung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                XeDaGiaoOnDataGridview(sender);
        }

        private void txtXeDaGiao_BienSo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                XeDaGiaoOnDataGridview(sender);
        }

        private void XeDaGiaoOnDataGridview(object sender)
        {
            string searchBienSoValue = txtXeDaGiao_BienSo.Text;
            string searchSoKhungValue = txtXeDaGiao_SoKhung.Text;
            string searchSoMayValue = txtXeDaGiao_SoMay.Text;

            try
            {
                var results = from myRow in _tableXeDaGiao.AsEnumerable()
                              select myRow;

                if (!String.IsNullOrEmpty(searchBienSoValue))
                    results = from myRow in results
                              where myRow.Field<string>("BienSo") == searchBienSoValue.Trim()
                              select myRow;

                if (!String.IsNullOrEmpty(searchSoKhungValue))
                    results = from myRow in results
                              where myRow.Field<string>("SoKhung") == searchSoKhungValue.Trim()
                              select myRow;

                if (!String.IsNullOrEmpty(searchSoMayValue))
                    results = from myRow in results
                              where myRow.Field<string>("SoMay") == searchSoMayValue.Trim()
                              select myRow;

                DataTable tableResult = results.CopyToDataTable();

                dataGridViewXeGiaoTrongNgay.DataSource = tableResult;
            }
            catch
            {
                MessageBox.Show("Không tồn tại xe đã giao!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LayDanhSachXeDaGiaoTrongNgay();
            }
        }

        #endregion Tìm kiếm nhanh xe đã giao

        private void dataGridViewXeGiaoTrongNgay_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                dataGridViewXeGiaoTrongNgay.ContextMenuStrip = contextMenuStripLichSuBaoDuong;

                try
                {
                    _rowIndexGiaoTrongNgay = dataGridViewXeGiaoTrongNgay.CurrentRow.Index;
                }
                catch (Exception ex)
                {
                    _rowIndexGiaoTrongNgay = -1;
                }
            }
            else
            {
                dataGridViewXeGiaoTrongNgay.ContextMenuStrip = null;
                _rowIndexGiaoTrongNgay = -1;
            }
        }

        private void dataGridViewXeGiaoTrongNgay_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewXeGiaoTrongNgay.DataSource != null && dataGridViewXeGiaoTrongNgay.Rows.Count > 0)
            {
                if (dataGridViewXeGiaoTrongNgay.CurrentRow != null)
                    _idLichSuBaoDuong = dataGridViewXeGiaoTrongNgay.CurrentRow.Cells["TrongNgay_IDBaoDuong"].Value.ToString();

                LayPhuTungBaoDuong();
                LayCongViecBaoDuong();
            }
        }

        private void dataGridViewLichSuBaoDuong_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridViewLichSuBaoDuong.DataSource != null && dataGridViewLichSuBaoDuong.Rows.Count > 0)
            {
                if (dataGridViewLichSuBaoDuong.CurrentRow != null)
                    _idLichSuBaoDuong = dataGridViewLichSuBaoDuong.CurrentRow.Cells["IdBaoDuong"].Value.ToString();

                LayPhuTungBaoDuong();
                LayCongViecBaoDuong();

                panelPhuTung.Enabled = true;
                dataGridViewPhuTungBaoDuong.ReadOnly = true;
                XoaPT.Visible = false;
            }
        }

        #endregion UcBaoDuong

        #region Load form

        private async void UcBaoDuong_Load(object sender, EventArgs e)
        {
            LoadData();
            
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                this.Invoke((Action)(() =>
                {
                    var newMessage = $"{user}: {message}";
                    Error_SignalR = newMessage;
                    //MessageBox.Show(Error_SignalR);
                    string[] dataRevSignal = Error_SignalR.Split(':');
                    
                    Error_SignalR = dataRevSignal[1].Split(' ')[1];
                    if (superTabControlBaoDuong.SelectedTab == superTabItemXeDangBaoDuong && Error_SignalR.Equals(Class.CompanyInfo.idcongty.ToString()))
                    {
                        LoadDangBaoDuong();
                        //MessageBox.Show(Class.CompanyInfo.tencongty);
                    }
                }));
            });


            try
            {
                await connection.StartAsync();
                Error_SignalR = "Connection started";
                //connectButton.IsEnabled = false;
                //sendButton.IsEnabled = true;
            }
            catch (Exception ex)
            {
                Error_SignalR = ex.Message;
            }

            MessageBox.Show(Error_SignalR);
        }

        private void LoadData()
        {
            //Load_comboboxMaPT();
            //Load_comboboxPhuTung();
            //Load_comboboxKhoPhuTung();
            Load_comboboxThoDichVu();
            Load_comboboxCongViecTheoTien();
            Load_comboboxCongViecTheoGio();

            //Load_comboboxkythuat();
            comboBoxTimKiemLoaiBaoDuong.SelectedItem = comboItemBaoDuongDinhKy;

            //superTabControlBaoDuong.SelectedTab = superTabItemXeDangBaoDuong;
            superTabControlBaoDuong.SelectedTab = superTabItemLichSuBaoDuong;
            
            Load_SoKhung_SoMay();

            comboBoxTimKiemLoaiBaoDuong.Focus();

            AddAutocompleDataGridview();
            AddAutoBienSo();

            LoadMaintenanceTypes();
            Loadtrangthaibaoduong();
            try
            {
                txtkythuat.SelectedIndex = 26;
            }
            catch { txtkythuat.SelectedIndex = 0; }
        }

        #endregion Load form

        #region Data GridView Error

        private void dataGridViewTheoTien_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //Data Error
        }

        #endregion Data GridView Error

        #region DataGridView EditingControlShowing

        private void dataGridViewTheoTien_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            try
            {
                comboBoxThoSuaChua.SelectedIndex = -1;

                var dataGridViewColumn = dataGridViewTheoTien.Columns["comboBoxTenTho_TheoTien"];
                if (dataGridViewColumn != null &&
                    dataGridViewColumn.Index == dataGridViewTheoTien.CurrentCell.ColumnIndex)
                {
                    ComboBox comboBoxChonThoDv = e.Control as ComboBox;

                    _rowIndexThoTheoTien = dataGridViewTheoTien.CurrentCell.RowIndex;

                    if (comboBoxChonThoDv != null)
                    {
                        comboBoxChonThoDv.SelectedValueChanged -= comboBoxChonThoDV_SelectedValueChanged;
                        comboBoxChonThoDv.SelectedValueChanged += comboBoxChonThoDV_SelectedValueChanged;
                    }
                }
            }
            catch
            {


                //
            }
            finally { comboBoxThoSuaChua.SelectedIndex = -1; }
        }

        private void comboBoxChonThoDV_SelectedValueChanged(object sender, EventArgs e)
        {
            comboBoxThoSuaChua.SelectedIndex = -1;

            try
            {
                ComboBox comboBox = sender as ComboBox;

                if (_rowIndexThoTheoTien != -1)
                {
                    try
                    {
                        if (comboBox != null && comboBox.SelectedValue != null && comboBox.SelectedValue.ToString() != dataGridViewTheoTien.Rows[_rowIndexThoTheoTien].Cells["comboBoxTenTho_TheoTien"].Value.ToString())
                        {
                            var result = from myRow in ((DataTable)dataGridViewTheoTien.DataSource).AsEnumerable()
                                         where myRow.Field<int?>("IdTho") == Convert.ToInt32(comboBox.SelectedValue)
                                         && myRow.Field<int?>("IdTienCong") == Convert.ToInt32(dataGridViewTheoTien.SelectedRows[0].Cells["comboBoxCongViec_TheoTien"].Value.ToString())
                                         select myRow;

                            if (result != null && result.Count() > 0)
                            {
                                //MessageBox.Show("Bạn đã nhận Công việc này cho Thợ này!\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);

                                if (dataGridViewTheoTien.Rows[_rowIndexThoTheoTien].Cells["comboBoxTenTho_TheoTien"].Value != null)
                                    comboBox.SelectedValue = dataGridViewTheoTien.Rows[_rowIndexThoTheoTien].Cells["comboBoxTenTho_TheoTien"].Value;
                                else
                                {
                                    DataGridViewCell cell = dataGridViewTheoTien.Rows[_rowIndexThoTheoTien].Cells["comboBoxTenTho_TheoTien"];

                                    if (cell.GetType() == typeof(DataGridViewComboBoxCell))
                                    {
                                        cell.Value = DBNull.Value;
                                    }
                                }

                                return;
                            }

                            SqlCommand sqlCommand = new SqlCommand();

                            sqlCommand.CommandText = "UPDATE ThoDichVu_TienCongThoTam SET IdTho = @IdTho, MaTho=@MaTho, TenTho = @TenTho WHERE Id=@Id";
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.AddWithValue("@IdTho", ((DataRowView)comboBox.SelectedItem).Row["IdTho"].ToString());
                            sqlCommand.Parameters.AddWithValue("@MaTho", ((DataRowView)comboBox.SelectedItem).Row["MaTho"].ToString());
                            sqlCommand.Parameters.AddWithValue("@TenTho", ((DataRowView)comboBox.SelectedItem).Row["tenTho"].ToString());
                            sqlCommand.Parameters.AddWithValue("@Id", dataGridViewTheoTien.SelectedRows[0].Cells["Id_TheoTien"].Value.ToString());

                            datatabase.ExcuteNonQuery(sqlCommand);

                            sqlCommand.CommandText = @"SELECT Id, IdTho, IdTienCong, NgaySuaChua, GhiChu, IdCongTy, IdBaoDuong, NoiDungBD, TienCong, TienKhachTra
                            FROM ThoDichVu_TienCongThoTam WHERE IdBaoDuong = @IdBaoDuong";
                            sqlCommand.Parameters.Clear();
                            sqlCommand.Parameters.AddWithValue("@IdBaoDuong", _idBd);

                            dataGridViewTheoTien.DataSource = datatabase.getData(sqlCommand);
                            dataGridViewTheoTien.ClearSelection();
                        }
                    }
                    catch { }
                    finally { _rowIndexThoTheoTien = -1; comboBoxThoSuaChua.SelectedIndex = -1; }
                }
            }
            catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
        }

        #endregion DataGridView EditingControlShowing

        #region Context Menu Click

        #region In phiếu báo giá

        private void InPhieuBaoGiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rowIndexPhieu != -1)
            {
                try
                {
                    SelectedCustomer._idbaoduong = dataGridViewLichSuBaoDuong.Rows[_rowIndexPhieu].Cells["IdBaoDuong"].Value.ToString();

                    if (String.IsNullOrEmpty(SelectedCustomer._idbaoduong))
                    {
                        MessageBox.Show("Lần bảo dưỡng không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region Lấy thông tin báo giá

                    SqlCommand cmd = new SqlCommand();
                    DataTable tableBaoGia = new DataTable();

                    cmd.CommandText = @"SELECT IdBaoGia, IdKhachHang, IdBaoDuong
                                        FROM BaoGiaSuaChua WHERE IdBaoDuong = @IdBaoDuong";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdBaoDuong", SelectedCustomer._idbaoduong);

                    tableBaoGia = datatabase.getData(cmd);

                    #endregion Lấy thông tin báo giá

                    if (tableBaoGia.Rows.Count <= 0)
                    {
                        MessageBox.Show("Thông tin báo giá không tồn tại!\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    FrmInBangBaoGia frmInBaoGia = new FrmInBangBaoGia();
                    frmInBaoGia.IdBaoDuong = Convert.ToInt64(SelectedCustomer._idbaoduong);

                    frmInBaoGia.ShowDialog();
                }
                catch { }
                finally { SelectedCustomer._idbaoduong = ""; }
            }
            if (_rowIndexGiaoTrongNgay != -1)
            {
                try
                {
                    SelectedCustomer._idbaoduong = dataGridViewXeGiaoTrongNgay.Rows[_rowIndexGiaoTrongNgay].Cells["TrongNgay_IDBaoDuong"].Value.ToString();

                    if (String.IsNullOrEmpty(SelectedCustomer._idbaoduong))
                    {
                        MessageBox.Show("Lần bảo dưỡng không tồn tại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    #region Lấy thông tin báo giá

                    SqlCommand cmd = new SqlCommand();
                    DataTable tableBaoGia = new DataTable();

                    cmd.CommandText = @"SELECT IdBaoGia, IdKhachHang, IdBaoDuong
                                        FROM BaoGiaSuaChua WHERE IdBaoDuong = @IdBaoDuong";
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@IdBaoDuong", SelectedCustomer._idbaoduong);

                    tableBaoGia = datatabase.getData(cmd);

                    #endregion Lấy thông tin báo giá

                    if (tableBaoGia.Rows.Count <= 0)
                    {
                        MessageBox.Show("Thông tin báo giá không tồn tại!\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                        return;
                    }

                    FrmInBangBaoGia frmInBaoGia = new FrmInBangBaoGia();
                    frmInBaoGia.IdBaoDuong = Convert.ToInt64(SelectedCustomer._idbaoduong);

                    frmInBaoGia.ShowDialog();
                }
                catch { }
                finally { SelectedCustomer._idbaoduong = ""; }
            }
        }

        #endregion In phiếu báo giá

        #region In phiếu bảo dưỡng

        private void InPhieuBaoDuongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rowIndexPhieu != -1)
            {
                try
                {
                    SelectedCustomer._idbaoduong = dataGridViewLichSuBaoDuong.Rows[_rowIndexPhieu].Cells["IdBaoDuong"].Value.ToString();
                    if (int.Parse(Class.CompanyInfo.idcongty) == 94)
                    {
                        frmPhieuThanhToanVietLong2 frm = new frmPhieuThanhToanVietLong2();
                        frm.ShowDialog();
                    }
                    else if (int.Parse(Class.CompanyInfo.idcongty) == 95)
                    {
                        frmPhieuThanhToanVietLong3 frm = new frmPhieuThanhToanVietLong3();
                        frm.ShowDialog();
                    }
                    else
                    {
                        FrmPhieuSuaChuaThangLoi frm = new FrmPhieuSuaChuaThangLoi();
                        frm.ShowDialog();
                    }
                    return;
                }
                catch { }
                finally { SelectedCustomer._idbaoduong = ""; }
            }
            if (_rowIndexGiaoTrongNgay != -1)
            {
                try
                {
                    SelectedCustomer._idbaoduong = dataGridViewXeGiaoTrongNgay.Rows[_rowIndexGiaoTrongNgay].Cells["TrongNgay_IDBaoDuong"].Value.ToString();

                    if (int.Parse(Class.CompanyInfo.idcongty) == 94)
                    {
                        frmPhieuThanhToanVietLong2 frm = new frmPhieuThanhToanVietLong2();
                        frm.ShowDialog();
                    }
                    else if (int.Parse(Class.CompanyInfo.idcongty) == 95)
                    {
                        frmPhieuThanhToanVietLong3 frm = new frmPhieuThanhToanVietLong3();
                        frm.ShowDialog();
                    }
                    else
                    {
                        FrmPhieuSuaChuaThangLoi frm = new FrmPhieuSuaChuaThangLoi();
                        frm.ShowDialog();
                    }

                    //*************************
                    _cmd.CommandText = @"UPDATE LichSuBaoDuongXe SET isPrinted = 1 WHERE IdBaoDuong = @IdBaoDuong";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@IdBaoDuong", SelectedCustomer._idbaoduong);

                    datatabase.ExcuteNonQuery(_cmd);
                    //Xóa thông tin xe bảo dưỡng
                    XoaThongTinXeBaoDuong();

                    panelTimKiemNhanh.Visible = false;
                    panelThongBaoLichSuBaoDuong.Visible = false;
                    panelXeGiaoTrongNgay.Visible = true;

                    LayDanhSachXeDaGiaoTrongNgay();

                    txtXeDaGiao_BienSo.Clear();
                    txtXeDaGiao_SoKhung.Clear();
                    txtXeDaGiao_SoMay.Clear();

                    if (dataGridViewXeGiaoTrongNgay.DataSource != null && dataGridViewXeGiaoTrongNgay.Rows.Count > 0)
                    {
                        if (dataGridViewXeGiaoTrongNgay.CurrentRow != null)
                            _idLichSuBaoDuong = dataGridViewXeGiaoTrongNgay.CurrentRow.Cells["TrongNgay_IDBaoDuong"].Value.ToString();
                    }
                    else
                        _idLichSuBaoDuong = "";

                    //LayPhuTungBaoDuong();
                    //LayCongViecBaoDuong();
                    //*****************************
                    return;
                }
                catch (Exception ex)
                { MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                finally { SelectedCustomer._idbaoduong = ""; }
            }
        }

        #endregion In phiếu bảo dưỡng

        #region In phiếu bảo dưỡng

        private void khieunai_Click(object sender, EventArgs e)
        {
            if (_rowIndexPhieu != -1)
            {
                try
                {
                    SelectedCustomer._idbaoduong = dataGridViewLichSuBaoDuong.Rows[_rowIndexPhieu].Cells["IdBaoDuong"].Value.ToString();

                }
                catch { }
                // finally { SelectedCustomer._idbaoduong = ""; }
            }
            if (_rowIndexGiaoTrongNgay != -1)
            {
                try
                {
                    SelectedCustomer._idbaoduong = dataGridViewXeGiaoTrongNgay.Rows[_rowIndexGiaoTrongNgay].Cells["TrongNgay_IDBaoDuong"].Value.ToString();
                }
                catch (Exception ex)
                { MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                //  finally { SelectedCustomer._idbaoduong = ""; }
            }
            if (SelectedCustomer._idbaoduong != "")
            {
                frmKhieunai frm = new frmKhieunai();
                frm.IdBaoDuong = SelectedCustomer._idbaoduong;
                frm.ShowDialog();
                return;
            }
        }

        #endregion In phiếu bảo dưỡng

        #region Sửa lần bảo dưỡng

        private void SuaLanBaoDuongToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rowIndexPhieu >= 0)
            {
                try
                {
                    if (EmployeeInfo.Quyen.ToUpper() == "QTV" || EmployeeInfo.Quyen.ToUpper() == "DISABLE_KH")
                    {

                        SelectedCustomer._idbaoduong = dataGridViewLichSuBaoDuong.Rows[_rowIndexPhieu].Cells["IdBaoDuong"].Value.ToString();

                        FrmCapNhatLichSuBaoDuong frmCapNhatBaoDuong = new FrmCapNhatLichSuBaoDuong();
                        frmCapNhatBaoDuong.ShowDialog();
                        return;
                    }
                    else
                    {
                        MessageBox.Show("Lần bảo dưỡng đã hoàn thành!\nBạn không có quyền sửa lần bảo dưỡng.\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    }
                        
                }
                catch { }
                finally { SelectedCustomer._idbaoduong = ""; }
                return;
            }
            if (_rowIndexGiaoTrongNgay >= 0)
            {
                try
                {
                    var NgayBD = DateTime.Parse(dataGridViewXeGiaoTrongNgay.Rows[_rowIndexGiaoTrongNgay].Cells["TrongNgay_NgayBD"].Value.ToString());

                    if ((EmployeeInfo.Quyen.ToUpper() == "QTV" || EmployeeInfo.Quyen.ToUpper() == "DISABLE_KH")
                        && NgayBD.Date == DateTime.Now.Date
                        && (int.Parse(DateTime.Now.ToString("HH")) <= 19))
                    {
                        //if (EmployeeInfo.QuyenEdit.ToUpper() == "CONFRIM")
                        //{

                        SelectedCustomer._idbaoduong = dataGridViewXeGiaoTrongNgay.Rows[_rowIndexGiaoTrongNgay].Cells["TrongNgay_IDBaoDuong"].Value.ToString();

                        FrmCapNhatLichSuBaoDuong frmCapNhatBaoDuong = new FrmCapNhatLichSuBaoDuong();
                        frmCapNhatBaoDuong.ShowDialog();
                        return;
                    }
                    else
                        MessageBox.Show("Lần bảo dưỡng đã hoàn thành!\nBạn không có quyền sửa lần bảo dưỡng.\nVui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                catch { }
                finally { SelectedCustomer._idbaoduong = ""; }
                return;
            }
        }

        #endregion Sửa lần bảo dưỡng

        #region Copy công việc bảo dưỡng

        private void CopyCongViecToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rowIndexCvTheoTien != -1)
            {
                try
                {
                    if (_congviec.Count() > 0)
                    {
                        //Copy công việc (chia tiền công cho các thợ làm cùng công việc)
                        decimal tiencong = _congviec.Sum(m => m.Field<decimal>("TienCong"));
                        _cmd.Connection = datatabase.getConnection();
                        _cmd.Connection.Open();
                        SqlTransaction tran = _cmd.Connection.BeginTransaction();
                        _cmd.Transaction = tran;
                        try
                        {
                            _cmd.CommandText = @"UPDATE ThoDichVu_TienCongThoTam
                                            SET TienCong = @TienCong WHERE IdTienCong = @IdTienCong AND IdBaoDuong = @IdBaoDuong";
                            _cmd.Parameters.Clear();
                            _cmd.Parameters.AddWithValue("@TienCong", tiencong / (_congviec.Count() + 1));
                            _cmd.Parameters.AddWithValue("@IdTienCong", _congviec.FirstOrDefault().Field<int>("IdTienCong"));
                            _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
                            _cmd.ExecuteNonQuery();
                            tran.Commit();
                            _cmd.Connection.Close();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message);
                            tran.Rollback();
                            _cmd.Connection.Close();

                        }

                        _cmd = new SqlCommand();
                        _cmd.Connection = datatabase.getConnection();
                        _cmd.Connection.Open();
                        SqlTransaction tranThoDichVu_TienCongThoTam = _cmd.Connection.BeginTransaction();
                        _cmd.Transaction = tranThoDichVu_TienCongThoTam;
                        try
                        {
                            _cmd.CommandText = @"INSERT INTO ThoDichVu_TienCongThoTam
                                            (IdTienCong, NgaySuaChua, IdCongTy, IdBaoDuong, NoiDungBD, TienCong, TienKhachTra)
                                            VALUES (@IdTienCong,@NgaySuaChua,@IdCongTy,@IdBaoDuong,@NoiDungBD,@TienCong,@TienKhachTra)";
                            _cmd.Parameters.Clear();
                            _cmd.Parameters.AddWithValue("@IdTienCong", _congviec.FirstOrDefault().Field<int>("IdTienCong"));
                            _cmd.Parameters.AddWithValue("@NgaySuaChua", _congviec.FirstOrDefault().Field<DateTime>("NgaySuaChua"));
                            _cmd.Parameters.AddWithValue("@IdCongTy", _congviec.FirstOrDefault().Field<int>("IdCongTy"));
                            _cmd.Parameters.AddWithValue("@IdBaoDuong", _congviec.FirstOrDefault().Field<int>("IdBaoDuong"));
                            _cmd.Parameters.AddWithValue("@NoiDungBD", _congviec.FirstOrDefault().Field<string>("NoiDungBD"));
                            _cmd.Parameters.AddWithValue("@TienCong", tiencong / (_congviec.Count() + 1));
                            _cmd.Parameters.AddWithValue("@TienKhachTra", 0);
                            _cmd.ExecuteNonQuery();
                            tranThoDichVu_TienCongThoTam.Commit();
                            _cmd.Connection.Close();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message);
                            tranThoDichVu_TienCongThoTam.Rollback();
                            _cmd.Connection.Close();

                        }
                        _cmd = new SqlCommand();
                        _cmd.Connection = datatabase.getConnection();
                        _cmd.Connection.Open();
                        SqlTransaction tranSelectThoDichVu_TienCongThoTam = _cmd.Connection.BeginTransaction();
                        _cmd.Transaction = tranSelectThoDichVu_TienCongThoTam;
                       
                        try
                        {
                            _cmd.CommandText = "select * from ThoDichVu_TienCongThoTam where IdCongTy=@IdCongTy and IdBaoDuong=@IdBaoDuong";
                            _cmd.Parameters.Clear();
                            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                            _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
                            DataTable tableCvTheoTien = new DataTable();
                            SqlDataAdapter adap = new SqlDataAdapter(_cmd);
                            adap.Fill(tableCvTheoTien);
                            dataGridViewTheoTien.DataSource = tableCvTheoTien;
                            tranSelectThoDichVu_TienCongThoTam.Commit();
                            _cmd.Connection.Close();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Lỗi: " + ex.Message);
                            tranSelectThoDichVu_TienCongThoTam.Rollback();
                            _cmd.Connection.Close();
                        }

                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("Lỗi: " + ex.Message);
                }
                finally
                {
                    _cmd.Connection.Close();
                    _rowIndexCvTheoTien = -1;
                    _congviec = null;
                }
            }
        }

        #endregion Copy công việc bảo dưỡng

        #region Lập phiếu báo giá sửa chữa

        private void LapPhieuBaoGiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_rowIndexBaoGia != 1)
            {
                FrmLapBaoGiaSuaChua frmLapBaoGia = new FrmLapBaoGiaSuaChua();
                frmLapBaoGia.IdBaoDuong = Convert.ToInt64(dataGridViewDanhSachXeDangBaoDuong.Rows[_rowIndexBaoGia].Cells["IdXeBaoDuong"].Value);
                frmLapBaoGia.LayPhuTung = new FrmLapBaoGiaSuaChua.LayDanhSachPhuTung(LayPhuTungBaoDuong);
                frmLapBaoGia.LayCongViec = new FrmLapBaoGiaSuaChua.LayDanhSachCongViec(LayCongViecBaoDuong);

                frmLapBaoGia.ShowDialog();
            }
        }

        #endregion Lập phiếu báo giá sửa chữa

        #endregion Context Menu Click

        #region DataGridView Cell MouseDown

        private void dataGridViewLichSuBaoDuong_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                dataGridViewLichSuBaoDuong.ContextMenuStrip = contextMenuStripLichSuBaoDuong;

                try
                {
                    if (dataGridViewLichSuBaoDuong.CurrentRow != null)
                        _rowIndexPhieu = dataGridViewLichSuBaoDuong.CurrentRow.Index;
                }
                catch (Exception ex)
                {
                    _rowIndexPhieu = -1;
                }
            }
            else
            {
                dataGridViewLichSuBaoDuong.ContextMenuStrip = null;
                _rowIndexPhieu = -1;
            }
        }

        private void dataGridViewDanhSachXeDangBaoDuong_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                dataGridViewDanhSachXeDangBaoDuong.ContextMenuStrip = contextMenuStripXeDangBaoDuong;

                try
                {
                    _rowIndexBaoGia = dataGridViewDanhSachXeDangBaoDuong.CurrentRow.Index;
                }
                catch (Exception ex)
                {
                    _rowIndexBaoGia = -1;
                }
            }
            else
            {
                dataGridViewDanhSachXeDangBaoDuong.ContextMenuStrip = null;
                _rowIndexBaoGia = -1;
            }
        }

        private void dataGridViewTheoTien_CellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && e.RowIndex != -1)
            {
                dataGridViewTheoTien.ContextMenuStrip = contextMenuStripCongViecBaoDuong;

                try
                {
                    _rowIndexCvTheoTien = dataGridViewTheoTien.CurrentRow.Index;

                    _congviec = from row in ((DataTable)dataGridViewTheoTien.DataSource).AsEnumerable()
                                where row.Field<int>("IdTienCong") == Convert.ToInt32(dataGridViewTheoTien.Rows[e.RowIndex].Cells["comboBoxCongViec_TheoTien"].Value)
                                select row;
                }
                catch (Exception ex)
                {
                    _rowIndexCvTheoTien = -1;
                }
            }
            else
            {
                dataGridViewTheoTien.ContextMenuStrip = null;
                _rowIndexCvTheoTien = -1;
            }
        }

        #endregion DataGridView Cell MouseDown

        #region Validation TextBox Change

        private void textBoxChietKhau_TextChanged(object sender, EventArgs e)
        {
            float flag;

            if (!String.IsNullOrEmpty(textBoxChietKhau.Text))
            {
                if (float.TryParse(textBoxChietKhau.Text, out flag) == false)
                {
                    MessageBox.Show("Chiết khấu phải là kiểu số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    textBoxChietKhau.Text = "0";
                    return;
                }
            }

            TinhTongTien();
        }

        private void textBoxSoKm_TextChanged(object sender, EventArgs e)
        {
            float flag;

            if (!String.IsNullOrEmpty(textBoxSoKm.Text))
            {
                if (float.TryParse(textBoxSoKm.Text, out flag) == false)
                {
                    MessageBox.Show("Số Km phải là kiểu số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    textBoxSoKm.Clear();
                    return;
                }
            }
        }

        private void textBoxLanBaoDuong_TextChanged(object sender, EventArgs e)
        {
            int flag;

            if (!String.IsNullOrEmpty(textBoxLanBaoDuong.Text))
            {
                if (int.TryParse(textBoxLanBaoDuong.Text, out flag) == false)
                {
                    MessageBox.Show("Lần bảo dưỡng phải là kiểu số!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    textBoxLanBaoDuong.Clear();
                    return;
                }
            }
        }

        #endregion Validation TextBox Change

        #region DataGridview CellContent Click

        private void dataGridViewDanhSachXeDangBaoDuong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    //Tích chọn xe đã xong
                    if (e.ColumnIndex == dataGridViewDanhSachXeDangBaoDuong.Columns["XeDaXong"].Index)
                    {
                        
                    }

                    //Tích chọn thay dầu máy
                    if (e.ColumnIndex == dataGridViewDanhSachXeDangBaoDuong.Columns["TrangThaiThayDau"].Index)
                    {
                        DataGridViewCheckBoxCell cell = dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["TrangThaiThayDau"] as DataGridViewCheckBoxCell;
                        if (String.IsNullOrEmpty(cell.Value.ToString()))
                            cell.Value = true;
                        else if ((bool)cell.Value == false)
                            cell.Value = true;
                        else
                            cell.Value = false;
                        //Cập nhật trạng thái thay dầu
                        _cmd.CommandText = @"UPDATE dbo.LichSuBaoDuongXeTam SET ThayDau = @ThayDau
                                            WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@ThayDau", cell.Value);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["IdXeBaoDuong"].Value);
                        datatabase.ExcuteNonQuery(_cmd);
                    }
                    //Tích chọn trạng thái
                    if (e.ColumnIndex == dataGridViewDanhSachXeDangBaoDuong.Columns["TrangThai"].Index)
                    {
                        DataGridViewCheckBoxCell cell = dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["TrangThai"] as DataGridViewCheckBoxCell;

                        if (String.IsNullOrEmpty(cell.Value.ToString()))
                            cell.Value = true;
                        else if ((bool)cell.Value == false)
                            cell.Value = true;
                        else
                            cell.Value = false;

                        //Cập nhật trạng thái
                        _cmd.CommandText = @"UPDATE dbo.LichSuBaoDuongXeTam
                                            SET TrangThai = @TrangThai
                                            WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@TrangThai", cell.Value);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["IdXeBaoDuong"].Value);

                        datatabase.ExcuteNonQuery(_cmd);
                    }

                    //Tích chọn thay dầu máy
                    if (e.ColumnIndex == dataGridViewDanhSachXeDangBaoDuong.Columns["TrangThaiThayDauMay"].Index)
                    {
                        DataGridViewCheckBoxCell cell = dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["TrangThaiThayDauMay"] as DataGridViewCheckBoxCell;

                        if (String.IsNullOrEmpty(cell.Value.ToString()))
                            cell.Value = true;
                        else if ((bool)cell.Value == false)
                            cell.Value = true;
                        else
                            cell.Value = false;

                        //Cập nhật trạng thái thay dầu máy
                        _cmd.CommandText = @"UPDATE dbo.LichSuBaoDuongXeTam
                                            SET ThayDauMay = @ThayDauMay
                                            WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@ThayDauMay", cell.Value);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["IdXeBaoDuong"].Value);

                        datatabase.ExcuteNonQuery(_cmd);
                    }

                    //Tích chọn nhông xích
                    if (e.ColumnIndex == dataGridViewDanhSachXeDangBaoDuong.Columns["NhongXich"].Index)
                    {
                        DataGridViewCheckBoxCell cell = dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["NhongXich"] as DataGridViewCheckBoxCell;

                        if (String.IsNullOrEmpty(cell.Value.ToString()))
                            cell.Value = true;
                        else if ((bool)cell.Value == false)
                            cell.Value = true;
                        else
                            cell.Value = false;

                        //Cập nhật trạng thái thay dầu máy
                        _cmd.CommandText = @"UPDATE dbo.LichSuBaoDuongXeTam
                                            SET NhongXich = @NhongXich
                                            WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@NhongXich", cell.Value);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["IdXeBaoDuong"].Value);

                        datatabase.ExcuteNonQuery(_cmd);
                    }

                    //Tích chọn làm máy
                    if (e.ColumnIndex == dataGridViewDanhSachXeDangBaoDuong.Columns["LamMay"].Index)
                    {
                        DataGridViewCheckBoxCell cell = dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["LamMay"] as DataGridViewCheckBoxCell;

                        if (String.IsNullOrEmpty(cell.Value.ToString()))
                            cell.Value = true;
                        else if ((bool)cell.Value == false)
                            cell.Value = true;
                        else
                            cell.Value = false;

                        //Cập nhật trạng thái thay dầu máy
                        _cmd.CommandText = @"UPDATE dbo.LichSuBaoDuongXeTam
                                            SET LamMay = @LamMay
                                            WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@LamMay", cell.Value);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["IdXeBaoDuong"].Value);

                        datatabase.ExcuteNonQuery(_cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewThueNgoai_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridViewThueNgoai.Columns["Xoa_ThueNgoai"].Index)
                    {
                        if (MessageBox.Show("Bạn có muốn xóa công việc bảo dưỡng đã chọn không?", "Thông báo xóa công việc?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string idBaoDuongXe = dataGridViewThueNgoai.Rows[e.RowIndex].Cells["IdBaoDuong_ThueNgoai"].Value.ToString();
                            string idTho = dataGridViewThueNgoai.Rows[e.RowIndex].Cells["comboBoxTho_ThueNgoai"].Value.ToString();

                            _cmd.CommandText = @"delete ThueNgoaiTam where IdCongTy=@IdCongTy and IdTho=@IdTho and IdBaoDuong=@IdBaoDuong and CongViec=@congviec";
                            _cmd.Parameters.Clear();
                            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                            _cmd.Parameters.AddWithValue("@IdTho", idTho);
                            _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuongXe);
                            _cmd.Parameters.AddWithValue("@congviec", dataGridViewThueNgoai.Rows[e.RowIndex].Cells["CongViec_ThueNgoai"].Value.ToString().Trim());
                            datatabase.ExcuteNonQuery(_cmd);

                            LayCongViecBaoDuong();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewTheoGio_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridViewTheoGio.Columns["Xoa_TheoGio"].Index)
                    {
                        if (MessageBox.Show("Bạn có muốn xóa công việc bảo dưỡng đã chọn không?", "Thông báo xóa công việc?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            string idBaoDuongXe = dataGridViewTheoGio.Rows[rowIndex].Cells["IdBaoDuong_TheoGio"].Value.ToString();
                            string idTho = dataGridViewTheoGio.Rows[rowIndex].Cells["comboBoxTho_TheoGio"].Value.ToString();

                            _cmd.CommandText = @"delete ThoDichVu_GioViecTam where IdCongTy=@IdCongTy and IdTho=@IdTho and IdBaoDuong=@IdBaoDuong";
                            _cmd.Parameters.Clear();
                            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                            _cmd.Parameters.AddWithValue("@IdTho", idTho);
                            _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuongXe);
                            datatabase.ExcuteNonQuery(_cmd);

                            LayCongViecBaoDuong();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewTheoTien_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridViewTheoTien.Columns["Xoa_TheoTien"].Index)
                    {
                        if (MessageBox.Show("Bạn có muốn xóa công việc bảo dưỡng đã chọn không?", "Thông báo xóa công việc?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            int rowIndex = e.RowIndex;
                            string idBaoDuongXe = dataGridViewTheoTien.Rows[rowIndex].Cells["IdBaoDuong_TheoTien"].Value.ToString();
                            string id = dataGridViewTheoTien.Rows[rowIndex].Cells["Id_TheoTien"].Value.ToString();
                            string idTho = dataGridViewTheoTien.Rows[rowIndex].Cells["comboBoxTenTho_TheoTien"].Value.ToString();
                            string idTienCong = dataGridViewTheoTien.Rows[e.RowIndex].Cells["comboBoxCongViec_TheoTien"].Value.ToString();

                            _cmd.CommandText = @"SELECT * FROM ThoDichVu_TienCongThoTam WHERE IdCongTy=@IdCongTy AND IdBaoDuong=@IdBaoDuong
                                                AND IdTienCong=@IdTienCong ORDER BY TienKhachTra DESC";
                            _cmd.Parameters.Clear();
                            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                            _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuongXe);
                            _cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);

                            DataTable dataThoTheoTien = new DataTable();
                            dataThoTheoTien = datatabase.getData(_cmd);

                            if (dataThoTheoTien.Rows.Count > 1)
                            {
                                int i = dataThoTheoTien.Rows.Count - 1;

                                decimal tiencong = Convert.ToDecimal(Convert.ToDecimal(dataThoTheoTien.Rows[0]["TienCong"].ToString()) / i);
                                decimal tienkhachtra = Convert.ToDecimal(dataThoTheoTien.Rows[0]["TienKhachTra"].ToString());

                                if (id != dataThoTheoTien.Rows[0]["Id"].ToString())
                                {
                                    _cmd.CommandText = @"UPDATE ThoDichVu_TienCongThoTam SET TienCong=@TienCong WHERE IdCongTy=@IdCongTy
                                                        AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuongXe);
                                    _cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);
                                    _cmd.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(Convert.ToDecimal(dataThoTheoTien.Rows[0]["TienCong"].ToString()) + tiencong));

                                    datatabase.ExcuteNonQuery(_cmd);

                                    _cmd.CommandText = "delete ThoDichVu_TienCongThoTam where Id = @Id";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@Id", id);

                                    datatabase.ExcuteNonQuery(_cmd);
                                }
                                else
                                {
                                    _cmd.CommandText = @"UPDATE ThoDichVu_TienCongThoTam SET TienCong=@TienCong WHERE IdCongTy=@IdCongTy
                                                        AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuongXe);
                                    _cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);
                                    _cmd.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(Convert.ToDecimal(dataThoTheoTien.Rows[0]["TienCong"].ToString()) + tiencong));
                                    datatabase.ExcuteNonQuery(_cmd);

                                    _cmd.CommandText = @"UPDATE ThoDichVu_TienCongThoTam SET TienKhachTra=@TienKhachTra WHERE IdCongTy=@IdCongTy
                                                        AND IdBaoDuong=@IdBaoDuong AND IdTienCong=@IdTienCong AND Id=@Id";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuongXe);
                                    _cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);
                                    _cmd.Parameters.AddWithValue("@TienKhachTra", Convert.ToDecimal(dataThoTheoTien.Rows[0]["TienKhachTra"].ToString()));
                                    _cmd.Parameters.AddWithValue("@Id", Convert.ToDecimal(dataThoTheoTien.Rows[1]["Id"].ToString()));
                                    datatabase.ExcuteNonQuery(_cmd);

                                    _cmd.CommandText = @"delete ThoDichVu_TienCongThoTam where Id=@Id";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@Id", id);
                                    datatabase.ExcuteNonQuery(_cmd);
                                }
                            }
                            else
                            {
                                _cmd.CommandText = @"delete ThoDichVu_TienCongThoTam where Id=@Id";
                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@Id", id);
                                datatabase.ExcuteNonQuery(_cmd);
                            }

                            LayCongViecBaoDuong();

                            //Cập nhật lại thông tin báo giá
                            var result = from myRow in ((DataTable)dataGridViewTheoTien.DataSource).AsEnumerable()
                                         where myRow.Field<int>("IdTienCong") == Convert.ToInt32(idTienCong)
                                         select myRow;

                            if (result.Count() <= 0)
                            {
                                DataTable tableBaoGia = new DataTable();

                                _cmd.CommandText = @"SELECT IdBaoGia, IdKhachHang, IdBaoDuong
                                                    FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuongXe);

                                tableBaoGia = datatabase.getData(_cmd);

                                if (tableBaoGia.Rows.Count > 0)
                                {
                                    _cmd.CommandText = @"UPDATE BaoGiaCongThoTam
                                                        SET DaThucHien = @DaThucHien
                                                        WHERE IdTienCong = @IdTienCong AND IdBaoGia = @IdBaoGia";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@DaThucHien", false);
                                    _cmd.Parameters.AddWithValue("@IdTienCong", idTienCong);
                                    _cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGia.Rows[0]["IdBaoGia"].ToString());

                                    datatabase.ExcuteNonQuery(_cmd);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void dataGridViewPhuTungBaoDuong_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            
            //**************************
            if (e.ColumnIndex == dataGridViewPhuTungBaoDuong.Columns["DaXong"].Index)
            {
                bool isCheck = Convert.ToBoolean(dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["DaXong"].Value);

                //Cập nhật trạng thái
                if (isCheck)
                    _cmd.CommandText = @"UPDATE LichSuBaoDuongChiTietTam2 SET GhiChu = N'Đã xong. ' WHERE IdBaoDuong = @IdBaoDuong AND IdKho = @IdKho AND IdPhuTung = @IdPhuTung";
                else _cmd.CommandText = @"UPDATE LichSuBaoDuongChiTietTam2 SET GhiChu = N'' WHERE IdBaoDuong = @IdBaoDuong AND IdKho = @IdKho AND IdPhuTung = @IdPhuTung";
                _cmd.Parameters.Clear();

                string idKho = dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["comboBoxKhoPhuTung"].Value.ToString();
                string idBaoDuong = dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["IdBaoDuong_PT"].Value.ToString();
                string idPt = dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["IdPhuTung"].Value.ToString();
                _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuong);
                _cmd.Parameters.AddWithValue("@IdPhuTung", idPt);
                _cmd.Parameters.AddWithValue("@IdKho", idKho);
                datatabase.ExcuteNonQuery(_cmd);
                //********************

                bool checkAllPhuTung = true;
                foreach (DataGridViewRow rowPhuTung in dataGridViewPhuTungBaoDuong.Rows)
                {
                    bool isChecked = Convert.ToBoolean(rowPhuTung.Cells["DaXong"].Value);

                    if (!isChecked)
                    {
                        for (int i = 0; i < dataGridViewPhuTungBaoDuong.ColumnCount; i++)
                            rowPhuTung.Cells[i].Style.BackColor = Color.FromName("White");
                        checkAllPhuTung = false;
                    }
                    else
                        for (int i = 0; i < dataGridViewPhuTungBaoDuong.ColumnCount; i++)
                            rowPhuTung.Cells[i].Style.BackColor = Color.FromName("Orange");
                }

                if (checkAllPhuTung)
                {
                    if (superTabControlBaoDuong.SelectedTab == superTabItemXeDangBaoDuong)
                    {
                        string ghiChuBaoDuong = dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["GhiChu"].Value.ToString();
                        //string ghiChuBaoDuong = "";
                        ghiChuBaoDuong += "Đã xong.";
                        for (int i = 0; i < dataGridViewDanhSachXeDangBaoDuong.ColumnCount; i++)
                            dataGridViewDanhSachXeDangBaoDuong.CurrentRow.Cells[i].Style.BackColor = Color.FromName("Orange");

                        //Cập nhật trạng thái
                        _cmd.CommandText = @"UPDATE LichSuBaoDuongXeTam SET GhiChu = @GhiChu WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@GhiChu", ghiChuBaoDuong);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewDanhSachXeDangBaoDuong.CurrentRow.Cells["IdXeBaoDuong"].Value);
                        datatabase.ExcuteNonQuery(_cmd);
                    }else if (superTabControlBaoDuong.SelectedTab == superTabItemXeBaoDuongDaiHan)
                    {
                        string ghiChuBaoDuong = dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["GhiChu"].Value.ToString();
                        //string ghiChuBaoDuong = "";
                        ghiChuBaoDuong += "Đã xong.";
                        for (int i = 0; i < dataGridViewXeBaoDuongDaiHan.ColumnCount; i++)
                            dataGridViewXeBaoDuongDaiHan.CurrentRow.Cells[i].Style.BackColor = Color.FromName("Orange");

                        //Cập nhật trạng thái
                        _cmd.CommandText = @"UPDATE LichSuBaoDuongXeTam SET GhiChu = @GhiChu WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@GhiChu", ghiChuBaoDuong);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewXeBaoDuongDaiHan.CurrentRow.Cells["IdBaoDuongDaiHan"].Value);
                        datatabase.ExcuteNonQuery(_cmd);
                    }
                }
                else
                {
                    if (superTabControlBaoDuong.SelectedTab == superTabItemXeDangBaoDuong)
                    {
                        for (int i = 0; i < dataGridViewDanhSachXeDangBaoDuong.ColumnCount; i++)
                            dataGridViewDanhSachXeDangBaoDuong.CurrentRow.Cells[i].Style.BackColor = Color.FromName("White");

                        // Cập nhật trạng thái
                        _cmd.CommandText = @"UPDATE LichSuBaoDuongXeTam SET GhiChu = @GhiChu WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@GhiChu", "");
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewDanhSachXeDangBaoDuong.CurrentRow.Cells["IdXeBaoDuong"].Value);
                        datatabase.ExcuteNonQuery(_cmd);
                    }else if (superTabControlBaoDuong.SelectedTab == superTabItemXeBaoDuongDaiHan)
                    {
                        for (int i = 0; i < dataGridViewXeBaoDuongDaiHan.ColumnCount; i++)
                            dataGridViewXeBaoDuongDaiHan.CurrentRow.Cells[i].Style.BackColor = Color.FromName("White");

                        // Cập nhật trạng thái
                        _cmd.CommandText = @"UPDATE LichSuBaoDuongXeTam SET GhiChu = @GhiChu WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@GhiChu", "");
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewXeBaoDuongDaiHan.CurrentRow.Cells["IdBaoDuongDaiHan"].Value);
                        datatabase.ExcuteNonQuery(_cmd);
                    }
                }
            }
        }

        private void dataGridViewPhuTungBaoDuong_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {
                    if (e.ColumnIndex == dataGridViewPhuTungBaoDuong.Columns["XoaPT"].Index)
                    {
                        if (MessageBox.Show("Bạn có muốn xóa phụ tùng khỏi danh sách phụ tùng bảo dưỡng?", "Thông báo xóa phụ tùng", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string idKho = dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["comboBoxKhoPhuTung"].Value.ToString();
                            string idBaoDuong = dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["IdBaoDuong_PT"].Value.ToString();
                            string idPt = dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["IdPhuTung"].Value.ToString();
                            string soLuongGoi = dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();

                            _cmd= new SqlCommand();
                            _cmd.Connection = datatabase.getConnection();
                            _cmd.Connection.Open();
                            SqlTransaction tranPhuTung = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranPhuTung;
                            DataTable phuTungTemp = new DataTable();
                            try
                            {
                                _cmd.CommandText = @"Select SoLuong from dbo.PhuTung where IdPT = @IdPT and IdCongTy = @IdCongTy and IdKho = @IdKho";
                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@IdPT", int.Parse(idPt));
                                _cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                _cmd.Parameters.AddWithValue("@IdKho", int.Parse(idKho));
                                SqlDataAdapter adapter = new SqlDataAdapter(_cmd);
                                adapter.Fill(phuTungTemp);
                                tranPhuTung.Commit();
                                _cmd.Connection.Close();

                            }
                            catch(Exception ex)
                            {
                                tranPhuTung.Rollback();
                                _cmd.Connection.Close();
                            }
                            if (phuTungTemp.Rows.Count == 1)
                            {
                                int soLuongTruoc = int.Parse(phuTungTemp.Rows[0]["SoLuong"].ToString());
                                int soLuongCong = int.Parse(soLuongGoi);
                                int soLuongSau = soLuongTruoc + soLuongCong;
                                _cmd = new SqlCommand();
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranUDPhuTung = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranUDPhuTung;
                                try
                                {
                                    _cmd.CommandText = @"Update dbo.PhuTung SET SoLuong = @SoLuong where IdPT = @IdPT and IdCongTy = @IdCongTy and IdKho = @IdKho";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@SoLuong", soLuongSau);
                                    _cmd.Parameters.AddWithValue("@IdPT", int.Parse(idPt));
                                    _cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                                    _cmd.Parameters.AddWithValue("@IdKho", int.Parse(idKho));
                                    _cmd.ExecuteNonQuery();
                                    tranUDPhuTung.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    tranUDPhuTung.Rollback();
                                    _cmd.Connection.Close();
                                }

                                //Xóa phụ tùng bảo dưỡng
                                _cmd = new SqlCommand();
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranLichSuBaoDuongChiTietTam2 = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranLichSuBaoDuongChiTietTam2;
                                try
                                {
                                    _cmd.CommandText = @"DELETE FROM LichSuBaoDuongChiTietTam2 WHERE IdBaoDuong = @IdBaoDuong AND IdPhuTung = @IdPhuTung AND IdKho = @IdKho";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuong);
                                    _cmd.Parameters.AddWithValue("@IdPhuTung", idPt);
                                    _cmd.Parameters.AddWithValue("@IdKho", idKho);
                                    _cmd.ExecuteNonQuery();
                                    tranLichSuBaoDuongChiTietTam2.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch(Exception ex)
                                {
                                    tranLichSuBaoDuongChiTietTam2.Rollback();
                                    _cmd.Connection.Close();
                                }

                                //Xóa phụ tùng bảo dưỡng
                                _cmd = new SqlCommand();
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranKhoXuat = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranKhoXuat;
                                try
                                {
                                    _cmd.CommandText = @"DELETE FROM KhoXuat WHERE IdBaoDuongTam = @IdBaoDuong AND IdPT = @idpt AND IdKho = @IdKho";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuong);
                                    _cmd.Parameters.AddWithValue("@idpt", idPt);
                                    _cmd.Parameters.AddWithValue("@IdKho", idKho);

                                    _cmd.ExecuteNonQuery();
                                    tranKhoXuat.Commit();
                                    _cmd.Connection.Close();

                                }
                                catch (Exception ex)
                                {
                                    tranKhoXuat.Rollback();
                                    _cmd.Connection.Close();
                                }
                                //datatabase.ExcuteNonQuery(_cmd);
                                _cmd = new SqlCommand();
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranPhuTungOrder052020 = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranPhuTungOrder052020;
                                try
                                {
                                    _cmd.CommandText = @"UPDATE dbo.PhuTungOrder052020 SET TranThaiTiepNhan = 0, TrangThaiGoi = 1, SoLuongCurrent = 0 WHERE IdBaoDuong = @IdBaoDuong " +
                                            "and IdPhuTung = @IdPhuTung and IdKho = @IdKho";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuong);
                                    _cmd.Parameters.AddWithValue("@IdPhuTung", idPt);
                                    _cmd.Parameters.AddWithValue("@IdKho", idKho);
                                    _cmd.ExecuteNonQuery();
                                    tranPhuTungOrder052020.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch(Exception ex)
                                {
                                    tranPhuTungOrder052020.Rollback();
                                    _cmd.Connection.Close();
                                }
                                //datatabase.ExcuteNonQuery(_cmd);

                                //Cập nhật lại báo giá
                                var result = from myRow in ((DataTable)dataGridViewPhuTungBaoDuong.DataSource).AsEnumerable()
                                             where myRow.Field<long>("IdKho") == Convert.ToInt64(idKho)
                                             && myRow.Field<string>("IdPhuTung") == idPt && myRow.Field<long>("IdBaoDuong") == Convert.ToInt64(idBaoDuong)
                                             select myRow;

                                if (result.Count() <= 0)
                                {
                                    DataTable tableBaoGia = new DataTable();

                                    _cmd = new SqlCommand();
                                    _cmd.Connection = datatabase.getConnection();
                                    _cmd.Connection.Open();
                                    SqlTransaction tranBaoGiaSuaChuaTam = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranBaoGiaSuaChuaTam;
                                    try
                                    {
                                        _cmd.CommandText = @"SELECT IdBaoGia, IdKhachHang, IdBaoDuong
                                                    FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdBaoDuong", idBaoDuong);
                                        SqlDataAdapter adp = new SqlDataAdapter(_cmd);
                                        adp.Fill(tableBaoGia);
                                        tranBaoGiaSuaChuaTam.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        tranBaoGiaSuaChuaTam.Rollback();
                                        _cmd.Connection.Close();
                                    }
                                    //tableBaoGia = datatabase.getData(_cmd);

                                    if (tableBaoGia.Rows.Count > 0)
                                    {
                                        _cmd = new SqlCommand();
                                        _cmd.Connection = datatabase.getConnection();
                                        _cmd.Connection.Open();
                                        SqlTransaction tranBaoGiaPhuTungTam = _cmd.Connection.BeginTransaction();
                                        _cmd.Transaction = tranBaoGiaPhuTungTam;
                                        try
                                        {
                                            _cmd.CommandText = @"UPDATE BaoGiaPhuTungTam
                                                        SET DaThucHien = @DaThucHien
                                                        WHERE IdKho = @IdKho AND IdPhuTung = @IdPhuTung AND IdBaoGia = @IdBaoGia";
                                            _cmd.Parameters.Clear();
                                            _cmd.Parameters.AddWithValue("@DaThucHien", false);
                                            _cmd.Parameters.AddWithValue("@IdKho", idKho);
                                            _cmd.Parameters.AddWithValue("@IdPhuTung", idPt);
                                            _cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGia.Rows[0]["IdBaoGia"].ToString());
                                            _cmd.ExecuteNonQuery();
                                            tranBaoGiaPhuTungTam.Commit();
                                            _cmd.Connection.Close();
                                            //datatabase.ExcuteNonQuery(_cmd);
                                        }
                                        catch(Exception ex)
                                        {
                                            tranBaoGiaPhuTungTam.Rollback();
                                            _cmd.Connection.Close();
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { _cmd.Connection.Close(); }
            //Load lại danh sách phụ tùng
            LayPhuTungBaoDuong();
        }

        #endregion DataGridview CellContent Click

        #region Load data cho Combobox trong gridview

        private void Load_comboboxMaPT()
        {
            _cmd.CommandText = @"SELECT IdPT, MaPT AS MaPhuTung
                                FROM PhuTung WHERE IdCongTy = @IdCongTy";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);

            DataTable tableMaPT = datatabase.getData(_cmd);

            comboBoxMaPT.DisplayMember = "MaPhuTung";
            comboBoxMaPT.ValueMember = "IdPT";
            comboBoxMaPT.DataSource = tableMaPT;
        }
        private void Load_comboboxkythuat()
        {
            _cmd.CommandText = @"Select IDTho, MaTho + ' - ' + TenTho As TenTho From ThoDichVu Where IDCongTy = @IdCongTy and TinhTrangLamViec is null";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);

            DataTable tableMaPT = datatabase.getData(_cmd);

            txtkythuat.DisplayMember = "TenTho";
            txtkythuat.ValueMember = "IDTho";
            txtkythuat.DataSource = tableMaPT;

        }
        private void Load_comboboxPhuTung()
        {
            _cmd.CommandText = @"SELECT IdPT, TenPT AS TenPhuTung
                                FROM PhuTung WHERE IdCongTy = @IdCongTy";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);

            DataTable tablePhuTung = datatabase.getData(_cmd);

            comboBoxPhuTung.DisplayMember = "TenPhuTung";
            comboBoxPhuTung.ValueMember = "IdPT";
            comboBoxPhuTung.DataSource = tablePhuTung;
        }

        private void Load_comboboxKhoPhuTung()
        {
            _cmd.CommandText = @"SELECT IdKho, TenKho
                                FROM KhoHang WHERE IdCongTy = @IdCongTy";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);

            DataTable tableKhoPhuTung = datatabase.getData(_cmd);

            comboBoxKhoPhuTung.DisplayMember = "TenKho";
            comboBoxKhoPhuTung.ValueMember = "IdKho";
            comboBoxKhoPhuTung.DataSource = tableKhoPhuTung;
        }

        private void Load_comboboxThoDichVu()
        {
            _cmd.CommandText = @"SELECT IdTho, MaTho, tenTho, ISNULL(CONVERT(nvarchar(10), MaTho), '') + '---' + tenTho AS TenThoDV
                                FROM dbo.ThoDichVu WHERE IdCongTy = @IdCongTy and TinhTrangLamViec is null";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);

            DataTable tableThoDichVu = datatabase.getData(_cmd);

            comboBoxTho_PT.DisplayMember = "TenThoDV";
            comboBoxTho_PT.ValueMember = "IdTho";
            comboBoxTho_PT.DataSource = tableThoDichVu;

            comboBoxTenTho_TheoTien.DisplayMember = "TenThoDV";
            comboBoxTenTho_TheoTien.ValueMember = "IdTho";
            comboBoxTenTho_TheoTien.DataSource = tableThoDichVu;

            comboBoxTho_TheoGio.DisplayMember = "TenThoDV";
            comboBoxTho_TheoGio.ValueMember = "IdTho";
            comboBoxTho_TheoGio.DataSource = tableThoDichVu;

            comboBoxTho_ThueNgoai.DisplayMember = "TenThoDV";
            comboBoxTho_ThueNgoai.ValueMember = "IdTho";
            comboBoxTho_ThueNgoai.DataSource = tableThoDichVu;

            comboBoxThoSuaChua.DisplayMember = "TenThoDV";
            comboBoxThoSuaChua.ValueMember = "IdTho";
            comboBoxThoSuaChua.DataSource = tableThoDichVu;
            comboBoxThoSuaChua.SelectedIndex = -1;

            txtkythuat.DisplayMember = "TenThoDV";
            txtkythuat.ValueMember = "IdTho";
            txtkythuat.DataSource = tableThoDichVu;
        }

        private void Load_comboboxCongViecTheoTien()
        {
            _cmd.CommandText = @"SELECT IdTienCong, ISNULL(CONVERT(nvarchar(50), MaBD), '') + '---' + NoiDungBD AS NoiDungCV
                                FROM dbo.TienCongTho WHERE IdCongTy = @IdCongTy";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);

            DataTable tableCongViecTheoTien = datatabase.getData(_cmd);

            comboBoxCongViec_TheoTien.DisplayMember = "NoiDungCV";
            comboBoxCongViec_TheoTien.ValueMember = "IdTienCong";
            comboBoxCongViec_TheoTien.DataSource = tableCongViecTheoTien;
        }

        private void Load_comboboxCongViecTheoGio()
        {
            _cmd.CommandText = @"SELECT IdGioViec, ISNULL(CONVERT(nvarchar(10), MaGioViec), '') + '---' + PhuLuc AS NoiDungCV
                                FROM dbo.GioViec WHERE IdCongTy = @IdCongTy";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);

            DataTable tableCongViecTheoGio = datatabase.getData(_cmd);

            comboboxCongViec_TheoGio.DisplayMember = "NoiDungCV";
            comboboxCongViec_TheoGio.ValueMember = "IdGioViec";
            comboboxCongViec_TheoGio.DataSource = tableCongViecTheoGio;
        }

        #endregion Load data cho Combobox trong gridview

        #region TextBoxChange Decimal Type

        private void TinhTongTien()
        {
            decimal tongtienPt = 0;
            decimal tongtienCongTho = 0;
            decimal tongtienThueNgoai = 0;
            decimal chietkhau = 0;
            decimal tienTrietKhau = 0;
            decimal tongtien = 0;

            if (!String.IsNullOrEmpty(textBoxTienPhuTung.Text))
                tongtienPt = Convert.ToDecimal(textBoxTienPhuTung.Text);
            if (!String.IsNullOrEmpty(textBoxTienCongTho.Text))
                tongtienCongTho = Convert.ToDecimal(textBoxTienCongTho.Text);
            if (!String.IsNullOrEmpty(textBoxTienThueNgoai.Text))
                tongtienThueNgoai = Convert.ToDecimal(textBoxTienThueNgoai.Text);
            if (!String.IsNullOrEmpty(textBoxChietKhau.Text))
                chietkhau = Convert.ToDecimal(textBoxChietKhau.Text);
            if (!String.IsNullOrEmpty(textBoxTienTrietKhau.Text))
                tienTrietKhau = Convert.ToDecimal(textBoxTienTrietKhau.Text);

            tongtien = (tongtienPt + tongtienCongTho + tongtienThueNgoai) - (((tongtienPt + tongtienCongTho + tongtienThueNgoai)) * (chietkhau / 100)) - tienTrietKhau;

            textBoxTongTien.Text = tongtien.ToString();
        }

        private void textBoxTongTien_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTongTien.Text))
            {
                textBoxTongTien.Text = string.Format("{0:N0}", decimal.Parse(textBoxTongTien.Text));
                textBoxTongTien.SelectionStart = textBoxTongTien.Text.Length;
            }
        }

        private void textBoxTienThueNgoai_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTienThueNgoai.Text))
            {
                textBoxTienThueNgoai.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienThueNgoai.Text));
                textBoxTienThueNgoai.SelectionStart = textBoxTienThueNgoai.Text.Length;

                TinhTongTien();
            }
        }

        private void textBoxTienCongTho_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTienCongTho.Text))
            {
                textBoxTienCongTho.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienCongTho.Text));
                textBoxTienCongTho.SelectionStart = textBoxTienCongTho.Text.Length;

                TinhTongTien();
            }
        }

        private void textBoxTienPhuTung_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTienPhuTung.Text))
            {
                textBoxTienPhuTung.Text = string.Format("{0:N0}", decimal.Parse(textBoxTienPhuTung.Text));
                textBoxTienPhuTung.SelectionStart = textBoxTienPhuTung.Text.Length;

                TinhTongTien();
            }
        }

        #endregion TextBoxChange Decimal Type

        #region DataGridView DataBindingComplete

        private void dataGridViewThueNgoai_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal tongtienthuengoai = 0;

            if (dataGridViewThueNgoai.DataSource != null && superTabControlBaoDuong.SelectedTab != superTabItemXeDaGiaoTrongNgay)
                tongtienthuengoai = Convert.ToDecimal(((DataTable)dataGridViewThueNgoai.DataSource).AsEnumerable().Sum(m => m.Field<double>("TienLayCuaKH")));

            textBoxTienThueNgoai.Text = tongtienthuengoai.ToString();
        }

        private void dataGridViewTheoTien_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal tongtientheotien = 0;

            if (dataGridViewTheoTien.DataSource != null && superTabControlBaoDuong.SelectedTab != superTabItemXeDaGiaoTrongNgay)
                tongtientheotien = ((DataTable)dataGridViewTheoTien.DataSource).AsEnumerable().Sum(m => m.Field<decimal>("TienKhachTra"));

            textBoxTienCongTho.Text = tongtientheotien.ToString();
        }

        private void dataGridViewPhuTungBaoDuong_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            decimal tongtienphutung = 0;

            if (dataGridViewPhuTungBaoDuong.DataSource != null && superTabControlBaoDuong.SelectedTab != superTabItemXeDaGiaoTrongNgay)
                tongtienphutung = ((DataTable)dataGridViewPhuTungBaoDuong.DataSource).AsEnumerable().Sum(m => m.Field<decimal>("GiaTien"));

            textBoxTienPhuTung.Text = tongtienphutung.ToString();
        }

        #endregion DataGridView DataBindingComplete

        #region DataGridView CellClick

        private void dataGridViewDanhSachXeDangBaoDuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                _idBd = Convert.ToString(dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["IdXeBaoDuong"].Value);
                idBaoDuongTam = _idBd;
                textBoxTimNhanhBienSo.Text = Convert.ToString(dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["BienSoXe"].Value);
                textBoxTimNhanhSoKhung.Text = Convert.ToString(dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["SoKhung"].Value);
                textBoxTimNhanhSoMay.Text = Convert.ToString(dataGridViewDanhSachXeDangBaoDuong.Rows[e.RowIndex].Cells["SoMay"].Value);
                if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDichVu)
                    _cmd.CommandText =
                        @"select ls.IdBaoDuong, kh.NgaySinh, kh.HoKH, kh.TenKH, kh.GioiTinh, ls.TenXe, kh.DienThoai,
                                    kh.Diachi, xb.NgayBan, kh.KhachDenTu, ls.BienSo, ls.Sokhung, ls.SoMay,
                                    ls.NgayBaoDuong, ls.SoLan, ls.SoKm, ls.LoaiBaoDuong, ls.IdKhachHang, xb.IdXeDaBan,ls.TGDUKIEN,ls.TTBAODUONG,
                                    ls.KYTHUATVIEN,ls.BANNANG, ls.GhiChu  
                                    from dbo.LichSuBaoDuongXeTam ls
                                    inner join dbo.KhachHang kh on ls.IdKhachHang = kh.IdKhachHang
                                    left join dbo.XeDaBan xb on xb.IdKhachHang = kh.IdKhachHang
                                    where ls.IdCongty = @IdCongTy and ls.IdBaoDuong = @IdBaoDuong";
                if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDinhKy)
                    _cmd.CommandText =
                        @"select ls.IdBaoDuong, kh.NgaySinh, kh.HoKH, kh.TenKH, kh.GioiTinh, ls.TenXe, kh.DienThoai,
                                    kh.Diachi, xb.NgayBan, kh.KhachDenTu, ls.BienSo, ls.Sokhung, ls.SoMay,
                                    ls.NgayBaoDuong, ls.SoLan, ls.SoKm, ls.LoaiBaoDuong, ls.IdKhachHang, xb.IdXeDaBan,ls.TGDUKIEN,ls.TTBAODUONG,ls.KYTHUATVIEN,ls.BANNANG,ls.GhiChu  
                                    from dbo.LichSuBaoDuongXeTam ls
                                    inner join dbo.KhachHang kh on ls.IdKhachHang = kh.IdKhachHang
                                    left join dbo.XeDaBan xb on xb.IdKhachHang = kh.IdKhachHang
                                    where ls.IdCongty = @IdCongTy and ls.IdBaoDuong = @IdBaoDuong";

                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
                if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDichVu)
                    _cmd.Parameters.AddWithValue("@BienSo", textBoxTimNhanhBienSo.Text.Trim());
                else
                    _cmd.Parameters.AddWithValue("@BienSo", "");
                _cmd.Parameters.AddWithValue("@SoKhung", textBoxTimNhanhSoKhung.Text.Trim());
                _cmd.Parameters.AddWithValue("@SoMay", textBoxTimNhanhSoMay.Text.Trim());

                DataTable infoid = datatabase.getData(_cmd);
                if (infoid.Rows.Count > 0)
                {
                    dateTimeInputNgaySinh.Text = _ngaysinh = infoid.Rows[0]["NgaySinh"].ToString();
                    _tenkh = infoid.Rows[0]["TenKH"].ToString();
                    textBoxTenKH.Text = infoid.Rows[0]["HoKH"].ToString() + " " + infoid.Rows[0]["TenKH"].ToString();
                    comboBoxGioiTinh.Text = infoid.Rows[0]["GioiTinh"].ToString();
                    textBoxTenXe.Text = _tenxe = infoid.Rows[0]["TenXe"].ToString();
                    textBoxDienThoai.Text = _dienthoai = infoid.Rows[0]["DienThoai"].ToString();
                    textBoxDiaChi.Text = infoid.Rows[0]["DiaChi"].ToString();
                    dateTimeInputNgayMua.Text = infoid.Rows[0]["NgayBan"].ToString();
                    comboBoxKhachDenTu.Text = infoid.Rows[0]["KhachDenTu"].ToString();
                    textBoxBienSo.Text = infoid.Rows[0]["BienSo"].ToString();
                    textBoxSoKhung.Text = infoid.Rows[0]["SoKhung"].ToString();
                    textBoxSoMay.Text = infoid.Rows[0]["SoMay"].ToString();
                    dateTimeInputNgayVao.Text = infoid.Rows[0]["NgayBaoDuong"].ToString();
                    dateTimeInputGioVao.Text = infoid.Rows[0]["NgayBaoDuong"].ToString();
                    dateTimeInputNgayRa.Value = DateTime.Now;
                    dateTimeInputGioRa.Value = DateTime.Now;
                    textBoxLanBaoDuong.Text = _solan = infoid.Rows[0]["SoLan"].ToString();
                    textBoxSoKm.Text = infoid.Rows[0]["SoKm"].ToString();
                    comboBoxLoaiBaoDuong.SelectedValue = infoid.Rows[0]["LoaiBaoDuong"].ToString();
                    _idKhachHang = infoid.Rows[0]["IdKhachHang"].ToString();
                    _idXe = infoid.Rows[0]["IdXeDaBan"].ToString();
                    _idBaoDuongTam = infoid.Rows[0]["IdBaoDuong"].ToString();
                    cbtrangthai.SelectedValue = infoid.Rows[0]["TTBAODUONG"].ToString();
                    txtTgdutinh.Text = infoid.Rows[0]["TGDUKIEN"].ToString();
                    txtbannang.Text = infoid.Rows[0]["BANNANG"].ToString();
                    textBoxGhiChuBaoDuong.Text = infoid.Rows[0]["GhiChu"].ToString();
                    if (infoid.Rows[0]["KYTHUATVIEN"].ToString() != "")
                        txtkythuat.SelectedValue = infoid.Rows[0]["KYTHUATVIEN"].ToString();
                    txbTenKhDiBaoDuong.Text = textBoxTenKH.Text;
                    txbDienThoaiKHDiBaoDuong.Text = textBoxDienThoai.Text;
                    txbDiaChiKHDiBaoDuong.Text = textBoxDiaChi.Text;
                }
                LayPhuTungBaoDuong();
                LayCongViecBaoDuong();

            }
        }

        #endregion DataGridView CellClick

        #region Lấy danh sách phụ tùng bảo dưỡng

        private void LayPhuTungBaoDuong()
        {
            if (superTabControlBaoDuong.SelectedTab == superTabItemXeDangBaoDuong || superTabControlBaoDuong.SelectedTab == superTabItemXeBaoDuongDaiHan)
            {
                _cmd.CommandText = @"SELECT * FROM dbo.LichSuBaoDuongChiTietTam2 lsct INNER JOIN dbo.KhoHang kh on lsct.IdKho = kh.IdKho WHERE IdBaoDuong = @IdBaoDuong";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
            }
            if (superTabControlBaoDuong.SelectedTab == superTabItemLichSuBaoDuong || superTabControlBaoDuong.SelectedTab == superTabItemXeDaGiaoTrongNgay)
            {
                _cmd.CommandText = @"SELECT * FROM dbo.LichSuBaoDuongChiTiet2 lsct INNER JOIN dbo.KhoHang kh on lsct.IdKho = kh.IdKho WHERE IdBaoDuong = @IdBaoDuong";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idLichSuBaoDuong);
            }

            DataTable tableChiTietBaoDuong = datatabase.getData(_cmd);
            comboBoxMaPT.DisplayMember = "MaPT";
            comboBoxMaPT.ValueMember = "IdPhuTung";
            comboBoxMaPT.DataSource = tableChiTietBaoDuong;

            comboBoxKhoPhuTung.DisplayMember = "TenKho";
            comboBoxKhoPhuTung.ValueMember = "IdKho";
            comboBoxKhoPhuTung.DataSource = tableChiTietBaoDuong;

            dataGridViewPhuTungBaoDuong.DataSource = tableChiTietBaoDuong;

            //*****************************
            if (tableChiTietBaoDuong.Columns.Count > 7)
            {
                for (int j = 0; j < tableChiTietBaoDuong.Rows.Count; j++)
                {
                    string strTenBien4 = tableChiTietBaoDuong.Rows[j]["GhiChu"].ToString();
                    if (strTenBien4.Contains("Đã xong"))
                    {
                        dataGridViewPhuTungBaoDuong.Rows[j].Cells["DaXong"].Value = true;
                        for (int i = 0; i < dataGridViewPhuTungBaoDuong.ColumnCount; i++)
                            dataGridViewPhuTungBaoDuong.Rows[j].Cells[i].Style.BackColor = Color.FromName("Orange");
                    }
                    else for (int i = 0; i < dataGridViewPhuTungBaoDuong.ColumnCount; i++)
                            dataGridViewPhuTungBaoDuong.Rows[j].Cells[i].Style.BackColor = Color.FromName("White");
                }
            }
            //*****************************
            dataGridViewPhuTungBaoDuong.ClearSelection();
        }

        #endregion Lấy danh sách phụ tùng bảo dưỡng

        #region Lấy công việc bảo dưỡng

        private void LayCongViecBaoDuong()
        {
            LayCongViecTheoTien();
            LayCongViecTheoGio();
            LayCongViecThueNgoai();
        }

        #endregion Lấy công việc bảo dưỡng

        #region Lấy công việc theo tiền

        private void LayCongViecTheoTien()
        {
            Load_comboboxCongViecTheoTien();

            if (superTabControlBaoDuong.SelectedTab == superTabItemXeDangBaoDuong || superTabControlBaoDuong.SelectedTab == superTabItemXeBaoDuongDaiHan)
            {
                _cmd.CommandText = @"SELECT Id, IdTho, IdTienCong, NgaySuaChua, GhiChu, IdCongTy, IdBaoDuong, NoiDungBD, TienCong, TienKhachTra
                                    FROM ThoDichVu_TienCongThoTam WHERE IdBaoDuong = @IdBaoDuong";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
            }
            if (superTabControlBaoDuong.SelectedTab == superTabItemLichSuBaoDuong || superTabControlBaoDuong.SelectedTab == superTabItemXeDaGiaoTrongNgay)
            {
                _cmd.CommandText = @"SELECT Id, IdTho, IdTienCong, NgaySuaChua, GhiChu, IdCongTy, IdBaoDuong, TienCong, TienKhachTra
                                    FROM ThoDichVu_TienCongTho2 WHERE IdBaoDuong = @IdBaoDuong";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idLichSuBaoDuong);
            }

            DataTable tableCongViecTheoTien = datatabase.getData(_cmd);

            dataGridViewTheoTien.DataSource = tableCongViecTheoTien;
            dataGridViewTheoTien.ClearSelection();
        }

        #endregion Lấy công việc theo tiền

        #region Lấy công việc theo giờ

        private void LayCongViecTheoGio()
        {
            if (superTabControlBaoDuong.SelectedTab == superTabItemXeDangBaoDuong || superTabControlBaoDuong.SelectedTab == superTabItemXeBaoDuongDaiHan)
            {
                _cmd.CommandText = @"SELECT IdTho, IdGioViec, SoPhut, NgaySuaChua, GhiChu, IdCongTy, IdBaoDuong
                                    FROM ThoDichVu_GioViecTam WHERE IdBaoDuong = @IdBaoDuong";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
            }
            if (superTabControlBaoDuong.SelectedTab == superTabItemLichSuBaoDuong || superTabControlBaoDuong.SelectedTab == superTabItemXeDaGiaoTrongNgay)
            {
                _cmd.CommandText = @"SELECT IdTho, IdGioViec, NgaySuaChua, GhiChu, IdCongTy, IdBaoDuong
                                    FROM ThoDichVu_GioViec WHERE IdBaoDuong = @IdBaoDuong";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idLichSuBaoDuong);
            }

            DataTable tableCongViecTheoGio = datatabase.getData(_cmd);

            dataGridViewTheoGio.DataSource = tableCongViecTheoGio;
            dataGridViewTheoGio.ClearSelection();
        }

        #endregion Lấy công việc theo giờ

        #region Lấy công việc thuê ngoài

        private void LayCongViecThueNgoai()
        {
            if (superTabControlBaoDuong.SelectedTab == superTabItemXeDangBaoDuong || superTabControlBaoDuong.SelectedTab == superTabItemXeBaoDuongDaiHan)
            {
                _cmd.CommandText = @"SELECT CongViec, TienThueNgoai, TienLayCuaKH, TienLai, GhiChu, IdCongTy, IdTho, IdBaoDuong, NgaySuaChua
                                    FROM ThueNgoaiTam WHERE IdBaoDuong = @IdBaoDuong";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
            }
            if (superTabControlBaoDuong.SelectedTab == superTabItemLichSuBaoDuong || superTabControlBaoDuong.SelectedTab == superTabItemXeDaGiaoTrongNgay)
            {
                _cmd.CommandText = @"SELECT CongViec, TienThueNgoai, TienLayCuaKH, TienLai, GhiChu, IdCongTy, IdTho, IdBaoDuong, NgaySuaChua
                                    FROM ThueNgoai WHERE IdBaoDuong = @IdBaoDuong";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idLichSuBaoDuong);
            }

            DataTable tableCongViecThueNgoai = datatabase.getData(_cmd);

            dataGridViewThueNgoai.DataSource = tableCongViecThueNgoai;
            dataGridViewThueNgoai.ClearSelection();
        }

        #endregion Lấy công việc thuê ngoài

        #region Sự kiện tìm nhanh xe bảo dưỡng trong danh sách xe bảo dưỡng

        private void textBoxTimNhanhSoMay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchOnDataGridview(sender);
        }

        private void textBoxTimNhanhSoKhung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchOnDataGridview(sender);
        }

        private void textBoxTimNhanhBienSo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                SearchOnDataGridview(sender);
        }

        private void SearchOnDataGridview(object sender)
        {
            string searchBienSoValue = textBoxTimNhanhBienSo.Text;
            string searchSoKhungValue = textBoxTimNhanhSoKhung.Text;
            string searchSoMayValue = textBoxTimNhanhSoMay.Text;

            try
            {
                var results = from myRow in _tableBaoDuong.AsEnumerable()
                              select myRow;

                if (!String.IsNullOrEmpty(searchBienSoValue))
                    results = from myRow in results
                              where myRow.Field<string>("BienSo") == searchBienSoValue.Trim()
                              select myRow;

                if (!String.IsNullOrEmpty(searchSoKhungValue))
                    results = from myRow in results
                              where myRow.Field<string>("SoKhung") == searchSoKhungValue.Trim()
                              select myRow;

                if (!String.IsNullOrEmpty(searchSoMayValue))
                    results = from myRow in results
                              where myRow.Field<string>("SoMay") == searchSoMayValue.Trim()
                              select myRow;

                DataTable tableResult = results.CopyToDataTable();

                dataGridViewDanhSachXeDangBaoDuong.DataSource = tableResult;
            }
            catch
            {
                MessageBox.Show(@"Không tồn tại xe bảo dưỡng!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                LoadDangBaoDuong();
            }
        }

        #endregion Sự kiện tìm nhanh xe bảo dưỡng trong danh sách xe bảo dưỡng

        #region Xóa thông tin khách hàng

        private void XoaThongTinKhachHang()
        {
            _idKhachHang = "";
            _idXe = "";
            _idBaoDuongTam = "";
            dataGridViewLichSuBaoDuong.DataSource = null;
            textBoxTenKH.Clear();
            dateTimeInputNgaySinh.Text = "";
            comboBoxGioiTinh.SelectedIndex = -1;
            textBoxDienThoai.Clear();
            textBoxDiaChi.Clear();
            dateTimeInputNgayMua.Text = "";
            comboBoxKhachDenTu.SelectedIndex = -1;
            textBoxTenXe.Clear();
            textBoxBienSo.Clear();
            textBoxSoKhung.Clear();
            textBoxSoMay.Clear();
            dateTimeInputNgayVao.Value = DateTime.Now;
            dateTimeInputNgayRa.Value = DateTime.Now;
            textBoxLanBaoDuong.Clear();
            textBoxSoKm.Clear();
            comboBoxLoaiBaoDuong.SelectedIndex = -1;
            cbtrangthai.SelectedIndex = -1;
            txtTgdutinh.Text = "10";
            labelThongBaoLichSuBaoDuong.Text = "";
            labelThongBaoKetQuaTimKiem.Text = "";
        }

        #endregion Xóa thông tin khách hàng

        #region Lấy loại bảo dưỡng

        private void LoadMaintenanceTypes()
        {
            Dictionary<string, string> maintenanceTypes = new Dictionary<string, string>();
            maintenanceTypes.Add(Keywords.MaintenanceTypes.VehicleWarranty, "Xe bảo hành");
            maintenanceTypes.Add(Keywords.MaintenanceTypes.LightMaintenance, "Bảo dưỡng nhẹ");
            maintenanceTypes.Add(Keywords.MaintenanceTypes.HevyMaintenance, "Bảo dưỡng nặng");
            comboBoxLoaiBaoDuong.DisplayMember = "Value";
            comboBoxLoaiBaoDuong.ValueMember = "Key";
            comboBoxLoaiBaoDuong.DataSource = new BindingSource(maintenanceTypes, null);
            comboBoxLoaiBaoDuong.SelectedIndex = -1;
        }

        #endregion Lấy loại bảo dưỡng

        #region Lấy loại bảo dưỡng

        private void Loadtrangthaibaoduong()
        {
            Dictionary<string, string> lsttrangthaibd = new Dictionary<string, string>();
            _cmd.CommandText =
                     @"select * from dbo.TbltrangthaiBD where ISACTIVE=1";
            cbtrangthai.Items.Clear();
            DataTable _mdtrangthai = datatabase.getData(_cmd);
            if (_mdtrangthai != null && _mdtrangthai.Rows.Count > 0)
            {
                for (int j = 0; j < _mdtrangthai.Rows.Count; j++)
                {
                    lsttrangthaibd.Add(_mdtrangthai.Rows[j]["ID"].ToString(), _mdtrangthai.Rows[j]["SNAME"].ToString());
                }
                cbtrangthai.DisplayMember = "Value";
                cbtrangthai.ValueMember = "Key";
                cbtrangthai.DataSource = new BindingSource(lsttrangthaibd, null);
                cbtrangthai.SelectedIndex = 1;

            }

            Dictionary<string, string> lstcongviec = new Dictionary<string, string>();

            _cmd.CommandText =
                     @"select * from TblCongviec";
            cbcongviec.Items.Clear();

            DataTable _mcv = datatabase.getData(_cmd);
            if (_mcv != null && _mcv.Rows.Count > 0)
            {
                for (int j = 0; j < _mcv.Rows.Count; j++)
                {

                    lstcongviec.Add(_mcv.Rows[j]["ID"].ToString(), _mcv.Rows[j]["CVNAME"].ToString());
                }
                cbcongviec.DisplayMember = "Value";
                cbcongviec.ValueMember = "Key";
                cbcongviec.DataSource = new BindingSource(lstcongviec, null);
                cbcongviec.SelectedIndex = 1;
            }
        }

        #endregion Lấy loại bảo dưỡng
        #region Tìm kiếm khách hàng bảo dưỡng dịch vụ

        private void TimBaoDuongDichVu()
        {
            //Xóa thông tin khách hàng
            XoaThongTinKhachHang();

            #region Yêu cầu Thắng Lợi

            //Cho cho nhập vào: Biển số, Số điện thoại. Tìm kiếm theo Biển số, Số điện thoại hoặc cả hai.

            //Điều kiện 1: Chỉ nhập Biển số

            #region "Tìm kiếm theo biển số"

            if (!String.IsNullOrEmpty(textBoxTimKiemBienSo.Text) && String.IsNullOrEmpty(textBoxTimKiemSoDienThoai.Text))
            {
                try
                {
                    _cmd.CommandText = @"select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                        WHERE lsbdx.IdCongTy=@IdCongTy and lsbdx.BienSo like @TKBienSo";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    _cmd.Parameters.AddWithValue("@TKBienSo", textBoxTimKiemBienSo.Text.Trim());

                    DataTable dt = datatabase.getData(_cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            textBoxTenKH.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            _idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { textBoxTenKH.Text = ""; }
                        try { dateTimeInputNgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
                        catch
                        {
                            dateTimeInputNgaySinh.Text = "";
                        }
                        try
                        {
                            textBoxDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { textBoxDiaChi.Text = ""; }
                        try
                        {
                            textBoxDienThoai.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { textBoxDienThoai.Text = ""; }

                        try
                        {
                            textBoxTenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { textBoxTenXe.Text = ""; }

                        try
                        {
                            textBoxSoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { textBoxSoKhung.Text = ""; }
                        try
                        {
                            textBoxSoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { textBoxSoMay.Text = ""; }
                        try
                        {
                            textBoxBienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { textBoxBienSo.Text = ""; }
                        try
                        {
                            comboBoxGioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { comboBoxGioiTinh.Text = ""; }
                        try
                        {
                            comboBoxKhachDenTu.Text = dt.Rows[0]["KhachDenTu"].ToString();
                        }
                        catch { comboBoxKhachDenTu.Text = ""; }

                        string sql = @"select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.Sokhung, lsbdx.SoMay,
                                    lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay,
                                    cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet,
                                    lsbdx.IdBaoDuong as IdBaoDuong, lsbdx.GhiChu, ktv.KyThuatVien from LichSuBaoDuongXe lsbdx
                                    left join KyThuatVien_BaoDuong ktv on ktv.IdBaoDuong = lsbdx.IdBaoDuong
                                    left join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong
                                    left join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong
                                    left join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho
                                    where lsbdx.IdCongTy=@IdCongTy and BienSo like @TKBienSo";
                        _cmd = new SqlCommand(sql);
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                        _cmd.Parameters.AddWithValue("@TKBienSo", textBoxTimKiemBienSo.Text.Trim());

                        DataTable dtLichSu = datatabase.getData(_cmd);

                        dataGridViewLichSuBaoDuong.DataSource = dtLichSu;
                        int solan = 0;
                        solan = Convert.ToInt32(dtLichSu.Rows[0]["SoLan"]) + 1;
                        _idBaoDuongTam = dtLichSu.Rows[0]["IdBaoDuong"].ToString();
                        textBoxLanBaoDuong.Text = Convert.ToString(solan);

                        //Đóng kết nối
                        _cmd.Connection.Close();
                    }
                    else
                    {
                        MessageBox.Show(@"Thông tin biển số không tồn tại lịch sử bảo dưỡng!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxTimKiemBienSo.SelectAll();
                        textBoxTimKiemBienSo.Focus();

                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo");
                }
            }

            #endregion "Tìm kiếm theo biển số"

            //Điều kiện 2: Chỉ nhập vào Số điện thoại

            #region "Tìm kiếm theo số điện thoại"

            if (String.IsNullOrEmpty(textBoxTimKiemBienSo.Text) && !String.IsNullOrEmpty(textBoxTimKiemSoDienThoai.Text))
            {
                try
                {
                    _cmd.CommandText = @"select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                        WHERE lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    _cmd.Parameters.AddWithValue("@TKSoDienThoai", textBoxTimKiemSoDienThoai.Text.Trim());

                    DataTable dt = datatabase.getData(_cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            textBoxTenKH.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            _idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { textBoxTenKH.Text = ""; }
                        try
                        {
                            dateTimeInputNgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]);
                        }
                        catch
                        {
                            dateTimeInputNgaySinh.Text = "";
                        }
                        try
                        {
                            textBoxDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { textBoxDiaChi.Text = ""; }
                        try
                        {
                            textBoxDienThoai.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { textBoxDienThoai.Text = ""; }

                        try
                        {
                            textBoxTenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { textBoxTenXe.Text = ""; }

                        try
                        {
                            textBoxSoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { textBoxSoKhung.Text = ""; }
                        try
                        {
                            textBoxSoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { textBoxSoMay.Text = ""; }
                        try
                        {
                            textBoxBienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { textBoxBienSo.Text = ""; }
                        try
                        {
                            comboBoxGioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { comboBoxGioiTinh.Text = ""; }

                        string sql = @"select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.Sokhung, lsbdx.SoMay,
                                    lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay,
                                    cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet,
                                    lsbdx.IdBaoDuong, lsbdx.GhiChu, ktv.KyThuatVien from LichSuBaoDuongXe lsbdx
                                    left join KyThuatVien_BaoDuong ktv on ktv.IdBaoDuong = lsbdx.IdBaoDuong
                                    left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong
                                    left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong
                                    left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho
                                    left outer join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                    where lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai";
                        _cmd = new SqlCommand(sql);
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                        _cmd.Parameters.AddWithValue("@TKSoDienThoai", textBoxTimKiemSoDienThoai.Text.Trim());

                        DataTable dtLichSu = datatabase.getData(_cmd);

                        dataGridViewLichSuBaoDuong.DataSource = dtLichSu;
                        int solan = 0;
                        solan = Convert.ToInt32(dtLichSu.Rows[0]["SoLan"]) + 1;
                        textBoxLanBaoDuong.Text = Convert.ToString(solan);

                        _cmd.Connection.Close();
                    }
                    else
                    {
                        MessageBox.Show(@"Thông tin số điện thoại không tồn trong tại lịch sử bảo dưỡng!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxTimKiemSoDienThoai.SelectAll();
                        textBoxTimKiemSoDienThoai.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo");
                }
            }

            #endregion "Tìm kiếm theo số điện thoại"

            //Điều kiện 3: Nhập vào cả Biển số và Số điện thoại

            #region "Tìm kiếm theo số điện thoại và Biển số"

            if (!String.IsNullOrEmpty(textBoxTimKiemBienSo.Text) && !String.IsNullOrEmpty(textBoxTimKiemSoDienThoai.Text))
            {
                try
                {
                    _cmd.CommandText = @"select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                        WHERE lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai and BienSo like @TKBienSo";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    _cmd.Parameters.AddWithValue("@TKSoDienThoai", textBoxTimKiemSoDienThoai.Text.Trim());
                    _cmd.Parameters.AddWithValue("@TKBienSo", textBoxTimKiemBienSo.Text.Trim());

                    DataTable dt = datatabase.getData(_cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            textBoxTenKH.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            _idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { textBoxTenKH.Text = ""; }
                        try
                        {
                            dateTimeInputNgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]);
                        }
                        catch
                        {
                            dateTimeInputNgaySinh.Text = "";
                        }
                        try
                        {
                            textBoxDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { textBoxDiaChi.Text = ""; }
                        try
                        {
                            textBoxDienThoai.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { textBoxDienThoai.Text = ""; }

                        try
                        {
                            textBoxTenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { textBoxTenXe.Text = ""; }

                        try
                        {
                            textBoxSoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { textBoxSoKhung.Text = ""; }
                        try
                        {
                            textBoxSoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { textBoxSoMay.Text = ""; }
                        try
                        {
                            textBoxBienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { textBoxBienSo.Text = ""; }
                        try
                        {
                            comboBoxGioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { comboBoxGioiTinh.Text = ""; }

                        string sql = @"select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.Sokhung, lsbdx.SoMay, lsbdx.NgayBaoDuong,
                                    lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan',
                                    lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong,
                                    lsbdx.GhiChu, ktv.KyThuatVien from LichSuBaoDuongXe lsbdx
                                    left join KyThuatVien_BaoDuong ktv on ktv.IdBaoDuong = lsbdx.IdBaoDuong
                                    left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong
                                    left outer join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong
                                    left outer join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho
                                    left outer join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                    Where lsbdx.IdCongTy=@IdCongTy and kh.DienThoai like @TKSoDienThoai and lsbdx.BienSo like @TKBienSo";
                        _cmd = new SqlCommand(sql);
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                        _cmd.Parameters.AddWithValue("@TKSoDienThoai", textBoxTimKiemSoDienThoai.Text.Trim());
                        _cmd.Parameters.AddWithValue("@TKBienSo", textBoxTimKiemBienSo.Text.Trim());

                        DataTable dtLichSu = datatabase.getData(_cmd);

                        dataGridViewLichSuBaoDuong.DataSource = dtLichSu;
                        int solan = 0;
                        solan = Convert.ToInt32(dtLichSu.Rows[0]["SoLan"]) + 1;
                        textBoxLanBaoDuong.Text = Convert.ToString(solan);

                        _cmd.Connection.Close();
                    }
                    else
                    {
                        MessageBox.Show(@"Thông tin Số điện thoại và Biển số không tồn tại trong lịch sử bảo dưỡng!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBoxTimKiemBienSo.SelectAll();
                        textBoxTimKiemBienSo.Focus();
                        return;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo");
                }
            }

            #endregion "Tìm kiếm theo số điện thoại và Biển số"

            if (String.IsNullOrEmpty(textBoxTimKiemBienSo.Text) && String.IsNullOrEmpty(textBoxTimKiemSoDienThoai.Text))
            {
                MessageBox.Show(@"Bạn chưa nhập vào thông tin tìm kiếm!", @"Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxTimKiemBienSo.Focus();
                return;
            }

            #endregion Yêu cầu Thắng Lợi

            tblBaoDuong.idkhachhang = _idKhachHang;
        }

        #endregion Tìm kiếm khách hàng bảo dưỡng dịch vụ

        #region Tìm kiếm khách hàng bảo dưỡng định kỳ

        private void TimBaoDuongDinhKy()
        {
            XoaThongTinKhachHang();

            //Cho cho nhập vào: Số khung, số may. Tìm kiếm theo Số khung, Số máy hoặc cả hai.

            //Điều kiện 1: Chỉ nhập số máy

            #region "Tìm theo số máy"

            if (!String.IsNullOrEmpty(textBoxTimKiemSoMay.Text) && String.IsNullOrEmpty(textBoxTimKiemSoKhung.Text))
            {
                try
                {
                    _cmd.CommandText = @"select top 1 * from XeDaBan lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                        WHERE lsbdx.IdCongTy=@IdCongTy and SoMay like @TKSoMay";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    _cmd.Parameters.AddWithValue("@TKSoMay", "%" + textBoxTimKiemSoMay.Text.Trim() + "%");

                    DataTable dt = datatabase.getData(_cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            textBoxTenKH.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            _idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { textBoxTenKH.Text = ""; }
                        try
                        {
                            dateTimeInputNgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]);
                        }
                        catch
                        {
                            dateTimeInputNgaySinh.Text = "";
                        }
                        try
                        {
                            textBoxDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { textBoxDiaChi.Text = ""; }
                        try
                        {
                            textBoxDienThoai.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { textBoxDienThoai.Text = ""; }

                        try
                        {
                            textBoxTenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { textBoxTenXe.Text = ""; }

                        try
                        {
                            textBoxSoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { textBoxSoKhung.Text = ""; }
                        try
                        {
                            textBoxSoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { textBoxSoMay.Text = ""; }
                        try
                        {
                            textBoxBienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { textBoxBienSo.Text = ""; }
                        try
                        {
                            comboBoxGioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { comboBoxGioiTinh.Text = ""; }

                        //Ngày mua xe
                        try
                        {
                            dateTimeInputNgayMua.Text = dt.Rows[0]["NgayBan"].ToString();
                        }
                        catch { dateTimeInputNgayMua.Text = Convert.ToString(DateTime.Now); }

                        //Loại khách hàng
                        try
                        {
                            comboBoxKhachDenTu.Text = dt.Rows[0]["KhachDenTu"].ToString();
                        }
                        catch { comboBoxKhachDenTu.Text = ""; }

                        string sql = @"select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe,
                                    lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan,CONVERT(bit, lsbdx.ThayDau) as ThayDau,
                                    lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet,
                                    tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu, ktv.KyThuatVien from LichSuBaoDuongXe lsbdx
                                    left join KyThuatVien_BaoDuong ktv on ktv.IdBaoDuong = lsbdx.IdBaoDuong
                                    left join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong
                                    left join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong
                                    left join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho
                                    where lsbdx.IdCongTy=@IdCongTy and SoMay like @TKSoMay";
                        _cmd = new SqlCommand(sql);
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                        _cmd.Parameters.AddWithValue("@TKSoMay", "%" + textBoxTimKiemSoMay.Text.Trim() + "%");

                        DataTable dtLichSu = datatabase.getData(_cmd);

                        dataGridViewLichSuBaoDuong.DataSource = dtLichSu;
                        int solan = 0;

                        try
                        {
                            solan = Convert.ToInt32(dtLichSu.Rows[0]["SoLan"]) + 1;
                            textBoxLanBaoDuong.Text = Convert.ToString(solan);
                        }
                        catch
                        {
                            textBoxLanBaoDuong.Text = "1";
                        }

                        _cmd.Connection.Close();
                    }
                    else
                    {
                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show(@"Thông tin số máy không tồn tại.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBoxTimKiemSoMay.SelectAll();
                            textBoxTimKiemSoMay.Focus();
                            return;
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo"); }
            }

            #endregion "Tìm theo số máy"

            //Điều kiện 2: Chỉ nhập vào số khung.

            #region "Tìm theo số khung"

            if (String.IsNullOrEmpty(textBoxTimKiemSoMay.Text) && !String.IsNullOrEmpty(textBoxTimKiemSoKhung.Text))
            {
                try
                {
                    _cmd.CommandText = @"select top 1 * from XeDaBan lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                        WHERE lsbdx.IdCongTy=@IdCongTy and SoKhung like @TKSoKhung";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    _cmd.Parameters.AddWithValue("@TKSoKhung", "%" + textBoxTimKiemSoKhung.Text.Trim() + "%");

                    DataTable dt = datatabase.getData(_cmd);

                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            textBoxTenKH.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            _idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { textBoxTenKH.Text = ""; }
                        try
                        {
                            dateTimeInputNgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]);
                        }
                        catch
                        {
                            dateTimeInputNgaySinh.Text = "";
                        }
                        try
                        {
                            textBoxDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { textBoxDiaChi.Text = ""; }
                        try
                        {
                            textBoxDienThoai.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { textBoxDienThoai.Text = ""; }

                        try
                        {
                            textBoxTenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { textBoxTenXe.Text = ""; }

                        try
                        {
                            textBoxSoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { textBoxSoKhung.Text = ""; }
                        try
                        {
                            textBoxSoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { textBoxSoMay.Text = ""; }
                        try
                        {
                            textBoxBienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { textBoxBienSo.Text = ""; }
                        try
                        {
                            comboBoxGioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { comboBoxGioiTinh.Text = ""; }

                        //Ngày mua xe
                        try
                        {
                            dateTimeInputNgayMua.Text = dt.Rows[0]["NgayBan"].ToString();
                        }
                        catch { dateTimeInputNgayMua.Text = Convert.ToString(DateTime.Now); }

                        //Loại khách hàng
                        try
                        {
                            comboBoxKhachDenTu.Text = dt.Rows[0]["KhachDenTu"].ToString();
                        }
                        catch { comboBoxKhachDenTu.Text = ""; }

                        string sql = @"select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe,
                                    lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan,CONVERT(bit, lsbdx.ThayDau) as ThayDau,
                                    lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet,
                                    tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu, ktv.KyThuatVien from LichSuBaoDuongXe lsbdx
                                    left join KyThuatVien_BaoDuong ktv on ktv.IdBaoDuong = lsbdx.IdBaoDuong
                                    left join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong
                                    left join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong
                                    left join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho
                                    where lsbdx.IdCongTy=@IdCongTy and SoKhung like @TKSoKhung";
                        _cmd = new SqlCommand(sql);
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                        _cmd.Parameters.AddWithValue("@TKSoKhung", "%" + textBoxTimKiemSoKhung.Text.Trim() + "%");

                        DataTable dtLichSu = datatabase.getData(_cmd);

                        dataGridViewLichSuBaoDuong.DataSource = dtLichSu;
                        int solan = 0;

                        try
                        {
                            solan = Convert.ToInt32(dtLichSu.Rows[0]["SoLan"]) + 1;
                            textBoxLanBaoDuong.Text = Convert.ToString(solan);
                        }
                        catch
                        {
                            textBoxLanBaoDuong.Text = "1";
                        }

                        _cmd.Connection.Close();
                    }
                    else
                    {
                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show(@"Thông tin số khung không tồn tại.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBoxTimKiemSoKhung.SelectAll();
                            textBoxTimKiemSoKhung.Focus();
                            return;
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo"); }
            }

            #endregion "Tìm theo số khung"

            //Điều kiện 3: Nhập vào cả số khung và số máy

            #region "Tìm kiếm theo số khung và số máy"

            if (!String.IsNullOrEmpty(textBoxTimKiemSoMay.Text) && !String.IsNullOrEmpty(textBoxTimKiemSoKhung.Text))
            {
                try
                {
                    _cmd.CommandText = @"select top 1 * from XeDaBan lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                        WHERE lsbdx.IdCongTy=@IdCongTy and SoKhung like @TKSoKhung and SoMay like @TKSoMay";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                    _cmd.Parameters.AddWithValue("@TKSoKhung", "%" + textBoxTimKiemSoKhung.Text.Trim() + "%");
                    _cmd.Parameters.AddWithValue("@TKSoMay", "%" + textBoxTimKiemSoMay.Text.Trim() + "%");

                    DataTable dt = datatabase.getData(_cmd);
                    if (dt.Rows.Count > 0)
                    {
                        try
                        {
                            textBoxTenKH.Text = dt.Rows[0]["HoKH"].ToString() + " " + dt.Rows[0]["TenKH"].ToString();
                            _idKhachHang = dt.Rows[0]["IdKhachHang"].ToString();
                        }
                        catch { textBoxTenKH.Text = ""; }
                        try { dateTimeInputNgaySinh.Value = Convert.ToDateTime(dt.Rows[0]["NgaySinh"]); }
                        catch { dateTimeInputNgaySinh.Text = ""; }
                        try
                        {
                            textBoxDiaChi.Text = dt.Rows[0]["DiaChi"].ToString();
                        }
                        catch { textBoxDiaChi.Text = ""; }
                        try
                        {
                            textBoxDienThoai.Text = dt.Rows[0]["DienThoai"].ToString();
                        }
                        catch { textBoxDienThoai.Text = ""; }

                        try
                        {
                            textBoxTenXe.Text = dt.Rows[0]["TenXe"].ToString();
                        }
                        catch { textBoxTenXe.Text = ""; }

                        try
                        {
                            textBoxSoKhung.Text = dt.Rows[0]["SoKhung"].ToString();
                        }
                        catch { textBoxSoKhung.Text = ""; }
                        try
                        {
                            textBoxSoMay.Text = dt.Rows[0]["SoMay"].ToString();
                        }
                        catch { textBoxSoMay.Text = ""; }
                        try
                        {
                            textBoxBienSo.Text = dt.Rows[0]["BienSo"].ToString();
                        }
                        catch { textBoxBienSo.Text = ""; }
                        try
                        {
                            comboBoxGioiTinh.Text = dt.Rows[0]["GioiTinh"].ToString();
                        }
                        catch { comboBoxGioiTinh.Text = ""; }

                        //Ngày mua xe
                        try
                        {
                            dateTimeInputNgayMua.Text = dt.Rows[0]["NgayBan"].ToString();
                        }
                        catch { dateTimeInputNgayMua.Text = Convert.ToString(DateTime.Now); }

                        //Loại khách hàng
                        try
                        {
                            comboBoxKhachDenTu.Text = dt.Rows[0]["KhachDenTu"].ToString();
                        }
                        catch { comboBoxKhachDenTu.Text = ""; }

                        string sql = @"select Row_Number() over(order by solan desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe,
                                    lsbdx.BienSo, lsbdx.NgayBaoDuong, lsbdx.NgayGiaoXe, lsbdx.SoLan,CONVERT(bit, lsbdx.ThayDau) as ThayDau,
                                    lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan', lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet,
                                    tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong, lsbdx.GhiChu, ktv.KyThuatVien from LichSuBaoDuongXe lsbdx
                                    left join KyThuatVien_BaoDuong ktv on ktv.IdBaoDuong = lsbdx.IdBaoDuong
                                    left outer join LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong
                                    left join ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong
                                    left join ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho
                                    where lsbdx.IdCongTy=@IdCongTy and SoKhung like @TKSoKhung and SoMay like @TKSoMay";
                        _cmd = new SqlCommand(sql);
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                        _cmd.Parameters.AddWithValue("@TKSoKhung", "%" + textBoxTimKiemSoKhung.Text.Trim() + "%");
                        _cmd.Parameters.AddWithValue("@TKSoMay", "%" + textBoxTimKiemSoMay.Text.Trim() + "%");

                        DataTable dtLichSu = datatabase.getData(_cmd);

                        dataGridViewLichSuBaoDuong.DataSource = dtLichSu;
                        int solan = 0;

                        try
                        {
                            solan = Convert.ToInt32(dtLichSu.Rows[0]["SoLan"]) + 1;
                            textBoxLanBaoDuong.Text = Convert.ToString(solan);
                        }
                        catch
                        {
                            textBoxLanBaoDuong.Text = "1";
                        }

                        _cmd.Connection.Close();
                    }
                    else
                    {
                        if (dt.Rows.Count <= 0)
                        {
                            MessageBox.Show("Thông tin số khung hoặc số máy không tồn tại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            textBoxTimKiemSoMay.SelectAll();
                            textBoxTimKiemSoMay.Focus();

                            return;
                        }
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message, "Thông Báo"); }
            }

            #endregion "Tìm kiếm theo số khung và số máy"

            if (String.IsNullOrEmpty(textBoxTimKiemSoMay.Text) && String.IsNullOrEmpty(textBoxTimKiemSoKhung.Text))
            {
                MessageBox.Show("Bạn chưa nhập vào thông tin tìm kiếm!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxTimKiemSoMay.Focus();

                return;
            }

            tblBaoDuong.idkhachhang = _idKhachHang;
        }

        #endregion Tìm kiếm khách hàng bảo dưỡng định kỳ

        #region TextBox Leave

        #region textBoxTimKiemSoKhung_Leave

        private void textBoxTimKiemSoKhung_Leave(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = _gridviewAutocompleteSoKhung.CurrentCell.RowIndex;

                textBoxTimKiemSoKhung.Text = _gridviewAutocompleteSoKhung.Rows[rowIndex].Cells[1].Value.ToString();
                _gridviewAutocompleteSoKhung.Visible = false;
            }
            catch { }
        }

        #endregion textBoxTimKiemSoKhung_Leave

        #region textBoxTimKiemSoMay_Leave

        private void textBoxTimKiemSoMay_Leave(object sender, EventArgs e)
        {
            try
            {
                int rowIndex = _gridviewAutocompleteSoMay.CurrentCell.RowIndex;

                textBoxTimKiemSoMay.Text = _gridviewAutocompleteSoMay.Rows[rowIndex].Cells[2].Value.ToString();
                _gridviewAutocompleteSoMay.Visible = false;
            }
            catch { }
        }

        #endregion textBoxTimKiemSoMay_Leave

        #endregion TextBox Leave

        #region textBoxTimKiemSoKhung_TextChanged

        private void textBoxTimKiemSoKhung_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTimKiemSoKhung.Text))
            {
                AutocompleteSoKhung();

                if (_gridviewAutocompleteSoKhung.DataSource != null)
                {
                    _gridviewAutocompleteSoKhung.Visible = true;
                    _gridviewAutocompleteSoKhung.BringToFront();
                }
                else
                {
                    _gridviewAutocompleteSoKhung.Visible = false;
                }
            }
            else
            {
                _gridviewAutocompleteSoKhung.DataSource = null;
                _gridviewAutocompleteSoKhung.Visible = false;
            }
        }

        #endregion textBoxTimKiemSoKhung_TextChanged

        #region textBoxTimKiemSoMay_TextChanged

        private void textBoxTimKiemSoMay_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(textBoxTimKiemSoMay.Text))
            {
                AutocompleteSoMay();

                if (_gridviewAutocompleteSoMay.DataSource != null)
                {
                    _gridviewAutocompleteSoMay.Visible = true;
                    _gridviewAutocompleteSoMay.BringToFront();
                }
                else
                {
                    _gridviewAutocompleteSoMay.Visible = false;
                }
            }
            else
            {
                _gridviewAutocompleteSoMay.DataSource = null;
                _gridviewAutocompleteSoMay.Visible = false;
            }
        }

        #endregion textBoxTimKiemSoMay_TextChanged

        #region textBoxTimKiemSoKhung_KeyPress

        private void textBoxTimKiemSoKhung_KeyPress(object sender, KeyPressEventArgs e)
        {
            gridviewAutocompleteSoKhung_KeyPress(sender, e);
        }

        #endregion textBoxTimKiemSoKhung_KeyPress

        #region textBoxTimKiemSoKhung_KeyUp

        private void textBoxTimKiemSoKhung_KeyUp(object sender, KeyEventArgs e)
        {
            gridviewAutocompleteSoKhung_KeyUp(sender, e);
        }

        #endregion textBoxTimKiemSoKhung_KeyUp

        #region textBoxTimKiemSoKhung_KeyDown

        private void textBoxTimKiemSoKhung_KeyDown(object sender, KeyEventArgs e)
        {
            gridviewAutocompleteSoKhung_KeyDown(sender, e);
        }

        #endregion textBoxTimKiemSoKhung_KeyDown

        #region gridviewAutocompleteSoKhung_KeyPress

        private void gridviewAutocompleteSoKhung_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    int rowIndex = _gridviewAutocompleteSoKhung.CurrentCell.RowIndex;

                    textBoxTimKiemSoKhung.Text = _gridviewAutocompleteSoKhung.Rows[rowIndex].Cells[1].Value.ToString();
                    _gridviewAutocompleteSoKhung.Visible = false;
                }
                catch { }
            }
        }

        #endregion gridviewAutocompleteSoKhung_KeyPress

        #region gridviewAutocompleteSoKhung_KeyUp

        private void gridviewAutocompleteSoKhung_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _gridviewAutocompleteSoKhung.DataSource = null;
                _gridviewAutocompleteSoKhung.Visible = false;
            }
        }

        #endregion gridviewAutocompleteSoKhung_KeyUp

        #region gridviewAutocompleteSoKhung_KeyDown

        private void gridviewAutocompleteSoKhung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Up))
            {
                MoveUp(_gridviewAutocompleteSoKhung);
            }
            if (e.KeyCode.Equals(Keys.Down))
            {
                MoveDown(_gridviewAutocompleteSoKhung);
            }
            e.Handled = true;
        }

        #endregion gridviewAutocompleteSoKhung_KeyDown

        #region textBoxTimKiemSoMay_KeyPress

        private void textBoxTimKiemSoMay_KeyPress(object sender, KeyPressEventArgs e)
        {
            gridviewAutocompleteSoMay_KeyPress(sender, e);
        }

        #endregion textBoxTimKiemSoMay_KeyPress

        #region textBoxTimKiemSoMay_KeyUp

        private void textBoxTimKiemSoMay_KeyUp(object sender, KeyEventArgs e)
        {
            gridviewAutocompleteSoMay_KeyUp(sender, e);
        }

        #endregion textBoxTimKiemSoMay_KeyUp

        #region textBoxTimKiemSoMay_KeyDown

        private void textBoxTimKiemSoMay_KeyDown(object sender, KeyEventArgs e)
        {
            gridviewAutocompleteSoMay_KeyDown(sender, e);
        }

        #endregion textBoxTimKiemSoMay_KeyDown

        #region gridviewAutocompleteSoMay_KeyPress

        private void gridviewAutocompleteSoMay_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                try
                {
                    int rowIndex = _gridviewAutocompleteSoMay.CurrentCell.RowIndex;

                    textBoxTimKiemSoMay.Text = _gridviewAutocompleteSoMay.Rows[rowIndex].Cells[2].Value.ToString();
                    _gridviewAutocompleteSoMay.Visible = false;
                }
                catch { }
            }
        }

        #endregion gridviewAutocompleteSoMay_KeyPress

        #region gridviewAutocompleteSoMay_KeyUp

        private void gridviewAutocompleteSoMay_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                _gridviewAutocompleteSoMay.DataSource = null;
                _gridviewAutocompleteSoMay.Visible = false;
            }
        }

        #endregion gridviewAutocompleteSoMay_KeyUp

        #region gridviewAutocompleteSoMay_KeyDown

        private void gridviewAutocompleteSoMay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Up))
            {
                MoveUp(_gridviewAutocompleteSoMay);
            }
            if (e.KeyCode.Equals(Keys.Down))
            {
                MoveDown(_gridviewAutocompleteSoMay);
            }
            e.Handled = true;
        }

        #endregion gridviewAutocompleteSoMay_KeyDown

        #region Button Click

        #region Làm mới danh sách xe đang bảo dưỡng

        private void buttonLamMoiDanhSach_Click(object sender, EventArgs e)
        {
            try
            {
                buttonLamMoiDanhSach.Enabled = false;

                LoadDangBaoDuong();
            }
            catch { }
            finally { buttonLamMoiDanhSach.Enabled = true; }
        }

        #endregion Làm mới danh sách xe đang bảo dưỡng

        #region Nhấn nút tìm kiếm

        private void buttonTimKiem_Click(object sender, EventArgs e)
        {
            dateTimeInputNgayVao.Value = DateTime.Now;
            dateTimeInputNgayRa.Value = DateTime.Now;
            dateTimeInputGioVao.Value = DateTime.Now;
            dateTimeInputGioRa.Value = DateTime.Now;

            try
            {
                buttonTimKiem.Enabled = false;

                if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDichVu)
                {
                    TimBaoDuongDichVu();
                }
                if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDinhKy)
                {
                    TimBaoDuongDinhKy();
                }

                if (dataGridViewLichSuBaoDuong.DataSource != null && dataGridViewLichSuBaoDuong.Rows.Count > 0)
                {
                    superTabControlBaoDuong.SelectedTab = superTabItemLichSuBaoDuong;
                    labelThongBaoLichSuBaoDuong.Text = "Tìm thấy: " + dataGridViewLichSuBaoDuong.Rows.Count + " lịch sử bảo dưỡng của xe!";
                }
                else
                {
                    labelThongBaoLichSuBaoDuong.Text = "";
                }
                txbTenKhDiBaoDuong.Text = textBoxTenKH.Text;
                txbDiaChiKHDiBaoDuong.Text = textBoxDiaChi.Text;
                txbDienThoaiKHDiBaoDuong.Text = textBoxDienThoai.Text;
            }
            catch { }
            finally { buttonTimKiem.Enabled = true; }
        }

        #endregion Nhấn nút tìm kiếm

        #region Thêm công việc bảo dưỡng

        private void buttonThemCongViec_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_idBd))
            {
                MessageBox.Show(@"Bạn chưa chọn xe bảo dưỡng!\nVui lòng kiểm tra lại.", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (comboBoxThoSuaChua.SelectedValue == null)
            {
                MessageBox.Show(@"Bạn chưa chọn thợ sửa chữa!\nVui lòng kiểm tra lại.", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            try
            {
                buttonThemCongViec.Enabled = false;

                ThongTinTho.idtho = Convert.ToString(comboBoxThoSuaChua.SelectedValue);
                ThongTinTho.matho = ((DataRowView)comboBoxThoSuaChua.SelectedItem).Row["MaTho"].ToString();
                ThongTinTho.tentho = ((DataRowView)comboBoxThoSuaChua.SelectedItem).Row["tenTho"].ToString();

                FrmChiTietCongTho frmCongTho = new FrmChiTietCongTho();
                frmCongTho.CallFromUcBaoDuong = new FrmChiTietCongTho.LoadCongTho(LayCongViecBaoDuong);
                frmCongTho.IdBaoDuong = _idBd;

                frmCongTho.ShowDialog();
            }
            catch
            {
                //
            }
            finally { buttonThemCongViec.Enabled = true; }
        }

        #endregion Thêm công việc bảo dưỡng

        #region Thêm phụ tùng bảo dưỡng

       

        #endregion Thêm phụ tùng bảo dưỡng

        #region Xóa thông tin hiển thị của khách hàng

        private void buttonXoaChu_Click(object sender, EventArgs e)
        {
            try
            {
                buttonXoaChu.Enabled = false;

                XoaThongTinKhachHang();
            }
            catch { }
            finally { buttonXoaChu.Enabled = true; }
        }

        #endregion Xóa thông tin hiển thị của khách hàng

        #region Cập nhật thông tin khách hàng

        private void buttonCapNhatKhachHang_Click(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(_idKhachHang))
                {
                    MessageBox.Show(@"Thông tin Khách hàng không tồn tại!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                buttonCapNhatKhachHang.Enabled = false;
                try
                {
                    #region capnhatthongtinkhachhang

                    var ngaySinh = "NULL";
                    if (!String.IsNullOrEmpty(dateTimeInputNgaySinh.Text))
                        ngaySinh = "'" + dateTimeInputNgaySinh.Value.ToString("yyyy-MM-dd") + "'";
                    _cmd = new SqlCommand();
                    _cmd.Connection = datatabase.getConnection();
                    _cmd.Connection.Open();
                    SqlTransaction tranKhachHang = _cmd.Connection.BeginTransaction();
                    _cmd.Transaction = tranKhachHang;
                    try
                    {
                        _cmd.CommandText = @"UPDATE KhachHang SET TenKH=@hoten,GioiTinh=@gioitinh,NgaySinh=" + ngaySinh + @",DienThoai=@dienthoai,Diachi=@diachi,
                                        KhachDenTu=@KhachDenTu WHERE IdKhachHang=@idkhachhang AND IdCongty=@idcongty";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@hoten", textBoxTenKH.Text.Trim());
                        _cmd.Parameters.AddWithValue("@gioitinh", comboBoxGioiTinh.Text.Trim());
                        _cmd.Parameters.AddWithValue("@dienthoai", textBoxDienThoai.Text.Trim());
                        _cmd.Parameters.AddWithValue("@diachi", textBoxDiaChi.Text.Trim());
                        _cmd.Parameters.AddWithValue("@idkhachhang", _idKhachHang);
                        _cmd.Parameters.AddWithValue("@idcongty", CompanyInfo.idcongty);
                        _cmd.Parameters.AddWithValue("@KhachDenTu", comboBoxKhachDenTu.Text.Trim());
                        _cmd.ExecuteNonQuery();
                        tranKhachHang.Commit();
                        _cmd.Connection.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Lỗi "+ex.Message);
                        tranKhachHang.Rollback();
                        _cmd.Connection.Close();
                    }

                    #endregion capnhatthongtinkhachhang

                    if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDichVu)
                    {
                        if (String.IsNullOrEmpty(_idBaoDuongTam))
                        {
                            MessageBox.Show(@"Thông tin biển số không tồn tại!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        _cmd = new SqlCommand();
                        _cmd.Connection = datatabase.getConnection();
                        _cmd.Connection.Open();
                        SqlTransaction tranLichSuBaoDuongXe = _cmd.Connection.BeginTransaction();
                        _cmd.Transaction = tranLichSuBaoDuongXe;
                        DataTable dt = new DataTable();
                        try
                        {
                            _cmd.CommandText = @"select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                            WHERE lsbdx.IdCongTy=" + CompanyInfo.idcongty + " and IdBaoDuong = '" + _idBaoDuongTam + "'";
                            SqlDataAdapter adap = new SqlDataAdapter(_cmd);
                            adap.Fill(dt);
                            tranLichSuBaoDuongXe.Commit();
                            _cmd.Connection.Close();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Lỗi " + ex.Message);
                            tranLichSuBaoDuongXe.Rollback();
                            _cmd.Connection.Close();

                        }

                        if (dt.Rows.Count > 0)
                        {
                            _cmd = new SqlCommand();
                            _cmd.Connection = datatabase.getConnection();
                            _cmd.Connection.Open();
                            SqlTransaction tranUDLichSuBaoDuongXe = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranUDLichSuBaoDuongXe;
                            try
                            {
                                _cmd.CommandText = @"update LichSuBaoDuongXe Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @SoMay, SoKm = @SoKm, BANNANG=@BANNANG, KYTHUATVIEN=@KYTHUATVIEN Where IDCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@TenXe", textBoxTenXe.Text);
                                _cmd.Parameters.AddWithValue("@BienSo", textBoxBienSo.Text);
                                _cmd.Parameters.AddWithValue("@SoKhung", textBoxSoKhung.Text);
                                _cmd.Parameters.AddWithValue("@SoMay", textBoxSoMay.Text);
                                _cmd.Parameters.AddWithValue("@SoKm", textBoxSoKm.Text);
                                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuongTam);
                                _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                _cmd.Parameters.AddWithValue("@BANNANG", txtbannang.Text);
                                _cmd.Parameters.AddWithValue("@KYTHUATVIEN", txtkythuat.SelectedValue);
                                _cmd.ExecuteNonQuery();
                                tranUDLichSuBaoDuongXe.Commit();
                                _cmd.Connection.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranUDLichSuBaoDuongXe.Rollback();
                                _cmd.Connection.Close();

                            }
                        }
                        _cmd = new SqlCommand();
                        _cmd.Connection = datatabase.getConnection();
                        _cmd.Connection.Open();
                        SqlTransaction tranLichSuBaoDuongXeTam = _cmd.Connection.BeginTransaction();
                        _cmd.Transaction = tranLichSuBaoDuongXeTam;
                        DataTable dtresult = new DataTable();
                        try
                        {
                            _cmd.CommandText = @"select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                                WHERE lsbdx.IdCongTy=" + CompanyInfo.idcongty + " and IdBaoDuong = '" + _idBaoDuongTam + "'";
                            SqlDataAdapter adapter = new SqlDataAdapter(_cmd);
                            adapter.Fill(dtresult);
                            tranLichSuBaoDuongXeTam.Commit();
                            _cmd.Connection.Close();
                        }
                        catch(Exception ex)
                        {
                            MessageBox.Show("Lỗi " + ex.Message);
                            tranLichSuBaoDuongXeTam.Rollback();
                            _cmd.Connection.Close();
                        }
                        if (dtresult.Rows.Count > 0)
                        {
                            string ttbaoduong = "0";
                            if (cbtrangthai.SelectedItem != null)
                                ttbaoduong = ((KeyValuePair<string, string>)cbtrangthai.SelectedItem).Key;
                            string cviec = "0";
                            if (cbcongviec.SelectedItem != null)
                                cviec = ((KeyValuePair<string, string>)cbcongviec.SelectedItem).Key;
                            string tgdukien = "10";
                            if (txtTgdutinh.Text != "")
                            {
                                tgdukien = txtTgdutinh.Text;
                            }
                            _cmd = new SqlCommand();
                            _cmd.Connection = datatabase.getConnection();
                            _cmd.Connection.Open();
                            SqlTransaction tranUDLichSuBaoDuongXeTam = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranUDLichSuBaoDuongXeTam;
                            try
                            {
                                _cmd.CommandText = @"update LichSuBaoDuongXeTam Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay, SoKm = @SoKm, ttbaoduong=@ttbaoduong,TGDUKIEN=@tgdukien,GIOHOANTHANH = (CASE WHEN @ttbaoduong=5 THEN GETDATE() ELSE NULL END), cviec=@cviec,
BANNANG=@BANNANG,KYTHUATVIEN=@KYTHUATVIEN, BDAU = (CASE WHEN BDAU IS NULL AND @BANNANG<>'' THEN GETDATE() ELSE BDAU END)

                                                    Where IDCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@TenXe", textBoxTenXe.Text);
                                _cmd.Parameters.AddWithValue("@BienSo", textBoxBienSo.Text);
                                _cmd.Parameters.AddWithValue("@SoKhung", textBoxSoKhung.Text);
                                _cmd.Parameters.AddWithValue("@SoMay", textBoxSoMay.Text);
                                _cmd.Parameters.AddWithValue("@SoKm", textBoxSoKm.Text);
                                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuongTam);
                                _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                _cmd.Parameters.AddWithValue("@ttbaoduong", ttbaoduong);
                                _cmd.Parameters.AddWithValue("@tgdukien", tgdukien);
                                _cmd.Parameters.AddWithValue("@cviec", cviec);
                                _cmd.Parameters.AddWithValue("@BANNANG", txtbannang.Text);
                                _cmd.Parameters.AddWithValue("@KYTHUATVIEN", txtkythuat.SelectedValue);
                                _cmd.ExecuteNonQuery();
                                tranUDLichSuBaoDuongXeTam.Commit();
                                _cmd.Connection.Close();
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranUDLichSuBaoDuongXeTam.Rollback();
                                _cmd.Connection.Close();
                            }
                        }
                    }
                    if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDinhKy)
                    {
                        if (!String.IsNullOrEmpty(_idXe))
                        {
                            var ngayBan = "NULL";
                            if (!String.IsNullOrEmpty(dateTimeInputNgayMua.Text))
                                ngayBan = "'" + dateTimeInputNgayMua.Value.ToString("yyyy-MM-dd") + "'";
                            _cmd = new SqlCommand();
                            _cmd.Connection = datatabase.getConnection();
                            _cmd.Connection.Open();
                            SqlTransaction tranXeDaBan = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranXeDaBan;
                            try
                            {
                                _cmd.CommandText = @"update XeDaBan Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay, NgayBan=" + ngayBan + @" Where IDCongTy = @IdCongTy And IdXeDaBan = @IdXeDaBan";
                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@TenXe", textBoxTenXe.Text);
                                _cmd.Parameters.AddWithValue("@BienSo", textBoxBienSo.Text);
                                _cmd.Parameters.AddWithValue("@SoKhung", textBoxSoKhung.Text);
                                _cmd.Parameters.AddWithValue("@SoMay", textBoxSoMay.Text);
                                _cmd.Parameters.AddWithValue("@IdXeDaBan", _idXe);
                                _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                _cmd.ExecuteNonQuery();
                                tranXeDaBan.Commit();
                                _cmd.Connection.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranXeDaBan.Rollback();
                                _cmd.Connection.Close();
                            }
                            _cmd = new SqlCommand();
                            _cmd.Connection = datatabase.getConnection();
                            DataTable dt = new DataTable();
                            _cmd.Connection.Open();
                            SqlTransaction tranLichSuBaoDuongXe = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranLichSuBaoDuongXe;
                            try
                            {
                                _cmd.CommandText = @"select top 1 * from LichSuBaoDuongXe lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                                WHERE lsbdx.IdCongTy=" + CompanyInfo.idcongty + " and IdBaoDuong = '" + _idBaoDuongTam + "'";

                                SqlDataAdapter adap = new SqlDataAdapter(_cmd);
                                adap.Fill(dt);
                                tranLichSuBaoDuongXe.Commit();
                                _cmd.Connection.Close();
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranLichSuBaoDuongXe.Rollback();
                                _cmd.Connection.Close();
                            }

                            if (dt.Rows.Count > 0)
                            {
                                _cmd = new SqlCommand();
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranUDLichSuBaoDuongXe = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranUDLichSuBaoDuongXe;
                                try
                                {
                                    _cmd.CommandText = @"update LichSuBaoDuongXe Set TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay Where IDCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@TenXe", textBoxTenXe.Text);
                                    _cmd.Parameters.AddWithValue("@BienSo", textBoxBienSo.Text);
                                    _cmd.Parameters.AddWithValue("@SoKhung", textBoxSoKhung.Text);
                                    _cmd.Parameters.AddWithValue("@SoMay", textBoxSoMay.Text);
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuongTam);
                                    _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                    _cmd.ExecuteNonQuery();
                                    tranUDLichSuBaoDuongXe.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show("Lỗi " + ex.Message);
                                    tranUDLichSuBaoDuongXe.Rollback();
                                    _cmd.Connection.Close();

                                }
                            }
                            else
                            {
                                DataTable dtresult = new DataTable();
                                _cmd = new SqlCommand();
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranLichSuBaoDuongXeTam = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranLichSuBaoDuongXeTam;
                                try
                                {
                                    _cmd.CommandText = @"select top 1 * from LichSuBaoDuongXeTam lsbdx inner join KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                                    WHERE lsbdx.IdCongTy=" + CompanyInfo.idcongty + " and IdBaoDuong = '" + _idBaoDuongTam + "'";
                                    SqlDataAdapter adapter = new SqlDataAdapter(_cmd);
                                    adapter.Fill(dtresult);
                                    tranLichSuBaoDuongXeTam.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi " + ex.Message);
                                    tranLichSuBaoDuongXeTam.Rollback();
                                    _cmd.Connection.Close();
                                }
                                if (dtresult.Rows.Count > 0)
                                {
                                    string ttbaoduong = "0";
                                    if (cbtrangthai.SelectedItem != null)
                                        ttbaoduong = ((KeyValuePair<string, string>)cbtrangthai.SelectedItem).Key;
                                    string cviec = "0";
                                    if (cbcongviec.SelectedItem != null)
                                        cviec = ((KeyValuePair<string, string>)cbcongviec.SelectedItem).Key;
                                    string tgdukien = "10";
                                    if (txtTgdutinh.Text != "")
                                    {
                                        tgdukien = txtTgdutinh.Text;
                                    }
                                    _cmd = new SqlCommand();
                                    _cmd.Connection = datatabase.getConnection();
                                    _cmd.Connection.Open();
                                    SqlTransaction tranUDLichSuBaoDuongXeTam = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranUDLichSuBaoDuongXeTam;
                                    try
                                    {
                                        _cmd.CommandText = @"update LichSuBaoDuongXeTam Set cviec=@cviec,TenXe = @TenXe, BienSo = @BienSo, SoKhung = @SoKhung, SoMay = @Somay, SoKm = @SoKm, ttbaoduong=@ttbaoduong,TGDUKIEN=@tgdukien,GIOHOANTHANH = (CASE WHEN @ttbaoduong=5 THEN GETDATE() ELSE NULL END)
                                                       ,BANNANG=@BANNANG,KYTHUATVIEN=@KYTHUATVIEN
, BDAU = (CASE WHEN BDAU IS NULL AND @BANNANG<>'' THEN GETDATE() ELSE BDAU END)
Where IDCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@TenXe", textBoxTenXe.Text);
                                        _cmd.Parameters.AddWithValue("@BienSo", textBoxBienSo.Text);
                                        _cmd.Parameters.AddWithValue("@SoKhung", textBoxSoKhung.Text);
                                        _cmd.Parameters.AddWithValue("@SoMay", textBoxSoMay.Text);
                                        _cmd.Parameters.AddWithValue("@SoKm", textBoxSoKm.Text);
                                        _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuongTam);
                                        _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                        _cmd.Parameters.AddWithValue("@ttbaoduong", ttbaoduong);
                                        _cmd.Parameters.AddWithValue("@tgdukien", tgdukien);
                                        _cmd.Parameters.AddWithValue("@cviec", cviec);
                                        _cmd.Parameters.AddWithValue("@BANNANG", txtbannang.Text);
                                        _cmd.Parameters.AddWithValue("@KYTHUATVIEN", txtkythuat.SelectedValue);
                                        _cmd.ExecuteNonQuery();
                                        tranUDLichSuBaoDuongXeTam.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Lỗi " + ex.Message);
                                        tranUDLichSuBaoDuongXeTam.Rollback();
                                        _cmd.Connection.Close();
                                    }
                                }
                            }
                        }
                    }
                    _cmd = new SqlCommand();
                    _cmd.Connection = datatabase.getConnection();
                    _cmd.Connection.Open();
                    SqlTransaction tranThongTinNguoiDiBaoDuong = _cmd.Connection.BeginTransaction();
                    _cmd.Transaction = tranThongTinNguoiDiBaoDuong;
                    try
                    {
                        _cmd.CommandText = @"update dbo.ThongTinNguoiDiBaoDuong set TenKH=@tenKH, DienThoai=@dienThoai, DiaChi=@diachi WHERE IdBaoDuongTam=@idbaoduong and IdCongTy=@idcongty ";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@tenKH", txbTenKhDiBaoDuong.Text.Trim());
                        _cmd.Parameters.AddWithValue("@dienthoai", txbDienThoaiKHDiBaoDuong.Text.Trim());
                        _cmd.Parameters.AddWithValue("@diachi", txbDiaChiKHDiBaoDuong.Text.Trim());
                        _cmd.Parameters.AddWithValue("@idcongty", CompanyInfo.idcongty);
                        _cmd.Parameters.AddWithValue("@idbaoduong", _idBaoDuongTam);
                        _cmd.ExecuteNonQuery();
                        tranThongTinNguoiDiBaoDuong.Commit();
                        _cmd.Connection.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Lỗi " + ex.Message);
                        tranThongTinNguoiDiBaoDuong.Rollback();
                        _cmd.Connection.Close();
                    }

                    MessageBox.Show(@"Cập nhật thông tin xe bảo dưỡng thành công.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                   
                    //Lấy lại danh sách xe đang bảo dưỡng
                    LoadDangBaoDuong();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                   
                }
                finally
                {
                    _cmd.Connection.Close();
                }
            }
            catch { }
            finally { buttonCapNhatKhachHang.Enabled = true; }
        }

        #endregion Cập nhật thông tin khách hàng

        #region Thêm xe bảo dưỡng

        private void buttonThemXeBaoDuong_Click(object sender, EventArgs e)
        {
            if (_changeOilKm.IsUseChangeOilByKM(CompanyInfo.idcongty) == true && textBoxSoKm.Text.Trim() == "")
            {
                MessageBox.Show(@"Bạn cần nhập số km của xe!", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            if(textBoxSoKm.Text.Trim().Length < 1)
            {
                MessageBox.Show(@"Bạn cần nhập số km của xe!", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            //if(txbDiaChiKHDiBaoDuong.Text.Trim().Length < 1)
            //{
            //    MessageBox.Show(@"Bạn cần nhập địa chỉ của khách hàng!", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            //if (txbDienThoaiKHDiBaoDuong.Text.Trim().Length < 1)
            //{
            //    MessageBox.Show(@"Bạn cần nhập điện thoại của khách hàng!", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            //if (txbTenKhDiBaoDuong.Text.Trim().Length < 1)
            //{
            //    MessageBox.Show(@"Bạn cần nhập tên của khách hàng!", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            //    return;
            //}
            try
            {
                buttonThemXeBaoDuong.Enabled = false;

                if (String.IsNullOrEmpty(_idKhachHang))
                {
                    if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDinhKy)
                    {
                        if (String.IsNullOrEmpty(textBoxSoKhung.Text.Trim()) && String.IsNullOrEmpty(textBoxSoMay.Text.Trim()))
                        {
                            MessageBox.Show(@"Bạn chưa nhập Số khung hoặc Số máy!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            textBoxSoKhung.Focus();
                            return;
                        }

                            var ngaySinh = "NULL";
                            if (!String.IsNullOrEmpty(dateTimeInputNgaySinh.Text))
                                ngaySinh = "'" + dateTimeInputNgaySinh.Value.ToString("yyyy-MM-dd") + "'";

                            //Thêm khách hàng

                            _cmd.Connection = datatabase.getConnection();
                            _cmd.Connection.Open();
                            SqlTransaction tranKH = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranKH;
                            try
                            {
                                _cmd.CommandText = @"insert into KhachHang(idcongty,TenKH,Ngaysinh,dienthoai,diachi,gioitinh,LoaiKH, KhachDenTu)
                                            values(@Idcongty,@TenKH," + ngaySinh + @",@dienthoai,@diachi,@gioitinh,@LoaiKH, @KhachDenTu) select @@Identity";

                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@Idcongty", CompanyInfo.idcongty);
                                _cmd.Parameters.AddWithValue("@TenKH", textBoxTenKH.Text);
                                _cmd.Parameters.AddWithValue("@dienthoai", textBoxDienThoai.Text);
                                _cmd.Parameters.AddWithValue("@diachi", textBoxDiaChi.Text);
                                _cmd.Parameters.AddWithValue("@gioitinh", Convert.ToString(comboBoxGioiTinh.Text));
                                _cmd.Parameters.AddWithValue("@LoaiKH", "1");
                                _cmd.Parameters.AddWithValue("@KhachDenTu", Convert.ToString(comboBoxKhachDenTu.Text));
                            _idKhachHang = _cmd.ExecuteScalar().ToString();
                                tranKH.Commit();
                                _cmd.Connection.Close();
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tranKH.Rollback();

                            }
                            finally
                            {
                                _cmd.Connection.Close();
                                _cmd.Connection.Dispose();
                            }
                            var ngayMua = "NULL";
                            if (!String.IsNullOrEmpty(dateTimeInputNgayMua.Text))
                                ngayMua = "'" + dateTimeInputNgayMua.Value.ToString("yyyy-MM-dd") + "'";

                            //Thêm vào xe đã bán

                            _cmd.Connection = datatabase.getConnection();
                            _cmd.Connection.Open();
                            SqlTransaction tranXeDaBan = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranXeDaBan;
                            try
                            {
                                _cmd.CommandText = @"insert into XeDaBan(TenXe, BienSo, NgayBan, IdKhachHang, IdCongTy, SoKhung, SoMay)
                                            values(@TenXe, @BienSo, " + ngayMua + @", @IdKhachHang, @IdCongTy, @SoKhung, @SoMay)";

                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@Idcongty", CompanyInfo.idcongty);
                                _cmd.Parameters.AddWithValue("@TenXe", textBoxTenXe.Text);
                                _cmd.Parameters.AddWithValue("@BienSo", textBoxBienSo.Text);
                                _cmd.Parameters.AddWithValue("@IdkhachHang", _idKhachHang);
                                _cmd.Parameters.AddWithValue("@SoKhung", textBoxSoKhung.Text);
                                _cmd.Parameters.AddWithValue("@SoMay", textBoxSoMay.Text);
                                _cmd.ExecuteNonQuery();
                                tranXeDaBan.Commit();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                tranXeDaBan.Rollback();
                            }
                            finally
                            {
                                _cmd.Connection.Close();
                                _cmd.Connection.Dispose();
                            }
                    }
                    if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDichVu)
                    {
                        if (String.IsNullOrEmpty(textBoxBienSo.Text.Trim()) || String.IsNullOrEmpty(textBoxDienThoai.Text.Trim()))
                        {
                            MessageBox.Show(@"Bạn chưa nhập Biển số hoặc Số điện thoại.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            textBoxBienSo.Focus();
                            return;
                        }
                        if (textBoxTenXe.Text.Trim() == "")
                        {
                            MessageBox.Show(@"Bạn chưa nhập tên xe!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        var ngaySinh = "NULL";
                        if (!String.IsNullOrEmpty(dateTimeInputNgaySinh.Text))
                            ngaySinh = "'" + dateTimeInputNgaySinh.Value.ToString("yyyy-MM-dd") + "'";

                        _cmd.CommandText = @"insert into KhachHang(idcongty,TenKH,Ngaysinh,dienthoai,diachi,gioitinh,LoaiKH)
                                            values(@Idcongty,@TenKH," + ngaySinh + @",@dienthoai,@diachi,@gioitinh,@LoaiKH) select @@Identity";

                        _cmd.Connection = datatabase.getConnection();
                        _cmd.Connection.Open();

                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@Idcongty", CompanyInfo.idcongty);
                        _cmd.Parameters.AddWithValue("@TenKH", textBoxTenKH.Text);
                        _cmd.Parameters.AddWithValue("@dienthoai", textBoxDienThoai.Text);
                        _cmd.Parameters.AddWithValue("@diachi", textBoxDiaChi.Text);
                        _cmd.Parameters.AddWithValue("@gioitinh", Convert.ToString(comboBoxGioiTinh.Text));
                        _cmd.Parameters.AddWithValue("@LoaiKH", "2");

                        _idKhachHang = _cmd.ExecuteScalar().ToString();
                        _cmd.Connection.Close();
                    }
                }

                string loaiBaoDuong = "";
                string ttbaoduong = "0";
                if (comboBoxLoaiBaoDuong.SelectedItem != null)
                    loaiBaoDuong = ((KeyValuePair<string, string>)comboBoxLoaiBaoDuong.SelectedItem).Key;
                if (cbtrangthai.SelectedItem != null)
                    ttbaoduong = ((KeyValuePair<string, string>)cbtrangthai.SelectedItem).Key;
                string cviec = "0";
                if (cbcongviec.SelectedItem != null)
                    cviec = ((KeyValuePair<string, string>)cbcongviec.SelectedItem).Key;
                string tgdukien = "10";
                if (txtTgdutinh.Text != "")
                {
                    tgdukien = txtTgdutinh.Text;
                }
                _cmd.CommandText = @"insert into LichSuBaoDuongXeTam(idcuahang,idkhachhang,idcongty,tenxe,bienso,somay,sokhung,ngaybaoduong,solan,GhiChu,SoKm,LoaiBaoDuong,ttbaoduong,TGDUKIEN,cviec,BANNANG,KYTHUATVIEN,BDAU)
                                    values(@idcuahang, @idkhachhang, @idcongty, @tenxe, @bienso,@somay,@sokhung,@ngaybaoduong, @solan,@GhiChu,@SoKm,@LoaiBaoDuong,@ttbaoduong,@tgdukien,@cviec,@BANNANG,@KYTHUATVIEN,(CASE WHEN @BANNANG<>'' THEN GETDATE() ELSE NULL END)) select @@Identity";

                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@idcuahang", CompanyInfo.IdsCuaHang);
                _cmd.Parameters.AddWithValue("@idkhachhang", _idKhachHang);
                _cmd.Parameters.AddWithValue("@idcongty", CompanyInfo.idcongty);
                _cmd.Parameters.AddWithValue("@tenxe", textBoxTenXe.Text);
                _cmd.Parameters.AddWithValue("@bienso", textBoxBienSo.Text);
                _cmd.Parameters.AddWithValue("@somay", textBoxSoMay.Text);
                _cmd.Parameters.AddWithValue("@sokhung", textBoxSoKhung.Text);
                _cmd.Parameters.AddWithValue("@ngaybaoduong", dateTimeInputNgayVao.Value.Date.Add(new TimeSpan(int.Parse(dateTimeInputGioVao.Text.Trim().Split(':')[0]), int.Parse(dateTimeInputGioVao.Text.Trim().Split(':')[1]), 0)));
                _cmd.Parameters.AddWithValue("@SoKm", textBoxSoKm.Text);
                _cmd.Parameters.AddWithValue("@LoaiBaoDuong", loaiBaoDuong);
                _cmd.Parameters.AddWithValue("@GhiChu", textBoxGhiChuBaoDuong.Text);
                _cmd.Parameters.AddWithValue("@ttbaoduong", ttbaoduong);
                _cmd.Parameters.AddWithValue("@tgdukien", tgdukien);
                _cmd.Parameters.AddWithValue("@cviec", cviec);
                _cmd.Parameters.AddWithValue("@BANNANG", txtbannang.Text);
                _cmd.Parameters.AddWithValue("@KYTHUATVIEN", txtkythuat.SelectedValue);
                if (String.IsNullOrEmpty(textBoxLanBaoDuong.Text))
                {
                    _cmd.Parameters.AddWithValue("@solan", "1");
                }
                else
                {
                    _cmd.Parameters.AddWithValue("@solan", textBoxLanBaoDuong.Text);
                }
                idBaoDuongTam = Class.datatabase.ExecuteScalar(_cmd);
                if (idBaoDuongTam.Length > 0)
                {
                    _cmd.CommandText = @"insert into dbo.ThongTinNguoiDiBaoDuong (TenKH,DienThoai,DiaChi,IdBaoDuongTam,IdCongTy,IdCuaHang) values(@TenKH,@DienThoai,@DiaChi,@IdBaoDuongTam,@IdCongTy,@IdCuaHang)";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@TenKH", txbTenKhDiBaoDuong.Text);
                    _cmd.Parameters.AddWithValue("@DienThoai", txbDienThoaiKHDiBaoDuong.Text);
                    _cmd.Parameters.AddWithValue("@DiaChi", txbDiaChiKHDiBaoDuong.Text);
                    _cmd.Parameters.AddWithValue("@IdBaoDuongTam", int.Parse(idBaoDuongTam));
                    _cmd.Parameters.AddWithValue("@IdCongTy", Class.CompanyInfo.idcongty);
                    _cmd.Parameters.AddWithValue("@IdCuaHang", Class.CompanyInfo.IdsCuaHang);

                    if (Class.datatabase.ExcuteNonQuery(_cmd) > 0)
                    {
                        MessageBox.Show(@"Nhập xe bảo dưỡng thành công.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txbDiaChiKHDiBaoDuong.Text = "";
                        txbDienThoaiKHDiBaoDuong.Text = "";
                        txbTenKhDiBaoDuong.Text = "";
                        btnLayPhieuBaoDuongDinhKy.Enabled = true;
                    }
                    else
                    {
                        MessageBox.Show(@"Nhập xe bảo dưỡng thất bại!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                //if (_cmd.ExecuteNonQuery() > 0/*datatabase.ExcuteNonQuery(_cmd) > 0*/)
                //{
                //    MessageBox.Show(@"Nhập xe bảo dưỡng thành công.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //}
                //else
                //{
                //    MessageBox.Show(@"Nhập xe bảo dưỡng thất bại!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //}

                _cmd = new SqlCommand("Select * from LichSuBaoDuongXeTam Where IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang");
                _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                _cmd.Parameters.AddWithValue("@IdCuaHang", CompanyInfo.IdsCuaHang);

                tblBaoDuong.lsBaoduongxetam = datatabase.getData(_cmd);

                //Lấy lại danh sách xe đang bảo dưỡng
                LoadDangBaoDuong();
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                buttonThemXeBaoDuong.Enabled = true;
                _idKhachHang = "";
            }
        }

        #endregion Thêm xe bảo dưỡng

        #region In phiếu bảo dưỡng

        private void buttonInPhieu_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(SelectedCustomer._idbaoduong))
            {
                MessageBox.Show(@"Phiếu bảo dưỡng không tồn tại!\nVui lòng kiểm tra lại.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            
            try
            {
                buttonInPhieu.Enabled = false;
                if (int.Parse(Class.CompanyInfo.idcongty) == 94)
                {
                    frmPhieuThanhToanVietLong2 frm = new frmPhieuThanhToanVietLong2();
                    frm.LoaiHinhBaoDuong = comboBoxTimKiemLoaiBaoDuong.Text;
                    frm.ShowDialog();
                }
                else if (int.Parse(Class.CompanyInfo.idcongty) == 95)
                {
                    frmPhieuThanhToanVietLong3 frm = new frmPhieuThanhToanVietLong3();
                    frm.LoaiHinhBaoDuong = comboBoxTimKiemLoaiBaoDuong.Text;
                    frm.ShowDialog();
                }
                else
                {
                    FrmPhieuSuaChuaThangLoi frm = new FrmPhieuSuaChuaThangLoi();
                    frm.LoaiHinhBaoDuong = comboBoxTimKiemLoaiBaoDuong.Text;
                    frm.ShowDialog();
                }
                
                //*************************
                _cmd.CommandText = @"UPDATE LichSuBaoDuongXe SET isPrinted = 1 WHERE IdBaoDuong = @IdBaoDuong";
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);

                datatabase.ExcuteNonQuery(_cmd);
                //Xóa thông tin xe bảo dưỡng
                XoaThongTinXeBaoDuong();
                //Lấy lại danh sách xe đang bảo dưỡng
                LoadDangBaoDuong();

                panelThongBaoLichSuBaoDuong.Visible = false;
                panelXeGiaoTrongNgay.Visible = false;
                panelTimKiemNhanh.Visible = true;

                textBoxTimNhanhBienSo.Clear();
                textBoxTimNhanhSoKhung.Clear();
                textBoxTimNhanhSoMay.Clear();

                textBoxTimNhanhBienSo.Focus();

                panelPhuTung.Enabled = true;
                panelCongViec.Enabled = true;
                //*****************************
            }
            catch
            {
                //
            }
            finally { buttonInPhieu.Enabled = true; }
        }

        #endregion In phiếu bảo dưỡng
      
        #region Hủy bỏ lần bảo dưỡng

        private void buttonHuyBo_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_idBd))
            {
                MessageBox.Show(@"Bạn chưa chọn xe bảo dưỡng!\nVui lòng kiểm tra lại!", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (MessageBox.Show(@"Bạn muốn Hủy lần bảo dưỡng của Xe: " + textBoxTimNhanhBienSo.Text, @"Hủy lần bảo dưỡng", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                try
                {
                    buttonHuyBo.Enabled = false;

                    _cmd.CommandText = "SELECT * FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                    _cmd.Parameters.Clear();
                    _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
                    DataTable tableBaoGiaTam = datatabase.getData(_cmd);

                    try
                    {
                        if(superTabControlBaoDuong.SelectedTab == superTabItemXeDangBaoDuong)
                        {
                            foreach (DataGridViewRow row in dataGridViewDanhSachXeDangBaoDuong.SelectedRows)
                            {
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranLichSuBaoDuongXeTam = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranLichSuBaoDuongXeTam;
                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                _cmd.Parameters.AddWithValue("@IdCuaHang", CompanyInfo.IdsCuaHang);
                                _cmd.Parameters.AddWithValue("@IdBaoDuong",
                                    Convert.ToString(row.Cells["IdXeBaoDuong"].Value));
                                try
                                {
                                    _cmd.CommandText =
                                        "delete from LichSuBaoDuongXeTam Where IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang and IdBaoDuong=@IdBaoDuong";
                                    _cmd.ExecuteNonQuery();
                                    tranLichSuBaoDuongXeTam.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi: " + ex.Message);
                                    tranLichSuBaoDuongXeTam.Rollback();
                                    _cmd.Connection.Close();

                                }

                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranlichsubaoduongchitiettam2 = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranlichsubaoduongchitiettam2;
                                try
                                {

                                    _cmd.CommandText = "delete lichsubaoduongchitiettam2 where IdBaoDuong=@IdBaoDuong";
                                    _cmd.ExecuteNonQuery();
                                    tranlichsubaoduongchitiettam2.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show("Lỗi: " + ex.Message);
                                    tranlichsubaoduongchitiettam2.Rollback();
                                    _cmd.Connection.Close();
                                }
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranThoDichVu_TienCongThoTam = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranThoDichVu_TienCongThoTam;
                                try
                                {
                                    _cmd.CommandText =
                                    "delete ThoDichVu_TienCongThoTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                                    _cmd.ExecuteNonQuery();
                                    tranThoDichVu_TienCongThoTam.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi: " + ex.Message);
                                    tranThoDichVu_TienCongThoTam.Rollback();
                                    _cmd.Connection.Close();
                                }
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranThoDichVu_GioViecTam = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranThoDichVu_GioViecTam;
                                try
                                {
                                    _cmd.CommandText =
                                    "delete ThoDichVu_GioViecTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                                    _cmd.ExecuteNonQuery();
                                    tranThoDichVu_GioViecTam.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi: " + ex.Message);
                                    tranThoDichVu_GioViecTam.Rollback();
                                    _cmd.Connection.Close();

                                }
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranThueNgoaiTam = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranThueNgoaiTam;
                                try
                                {
                                    _cmd.CommandText =
                                    "delete ThueNgoaiTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                                    _cmd.ExecuteNonQuery();
                                    tranThueNgoaiTam.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show("Lỗi: " + ex.Message);
                                    tranThueNgoaiTam.Rollback();
                                    _cmd.Connection.Close();
                                }
                                if (tableBaoGiaTam.Rows.Count > 0)
                                {
                                    //Xóa báo giá công thợ tạm
                                    _cmd.Connection = datatabase.getConnection();
                                    _cmd.Connection.Open();
                                    SqlTransaction tranBaoGiaCongThoTam = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranBaoGiaCongThoTam;
                                    try
                                    {
                                        _cmd.CommandText = "DELETE FROM BaoGiaCongThoTam WHERE IdBaoGia = @IdBaoGia";
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());
                                        _cmd.ExecuteNonQuery();
                                        tranBaoGiaCongThoTam.Commit();
                                        _cmd.Connection.Close();

                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Lỗi: " + ex.Message);
                                        tranBaoGiaCongThoTam.Rollback();
                                        _cmd.Connection.Close();

                                    }

                                    _cmd.Connection = datatabase.getConnection();
                                    _cmd.Connection.Open();
                                    SqlTransaction tranBaoGiaPhuTungTam = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranBaoGiaPhuTungTam;
                                    try
                                    {
                                        //Xóa báo giá phụ tùng tạm
                                        _cmd.CommandText = "DELETE FROM BaoGiaPhuTungTam WHERE IdBaoGia = @IdBaoGia";
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());
                                        _cmd.ExecuteNonQuery();
                                        tranBaoGiaPhuTungTam.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Lỗi: " + ex.Message);
                                        tranBaoGiaPhuTungTam.Rollback();
                                        _cmd.Connection.Close();
                                    }

                                    //Xóa báo giá tạm
                                    _cmd.Connection = datatabase.getConnection();
                                    _cmd.Connection.Open();
                                    SqlTransaction tranBaoGiaSuaChuaTam = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranBaoGiaSuaChuaTam;
                                    try
                                    {
                                        _cmd.CommandText = "DELETE FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdBaoDuong",
                                            Convert.ToString(row.Cells["IdXeBaoDuong"].Value));
                                        _cmd.ExecuteNonQuery();
                                        tranBaoGiaSuaChuaTam.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Lỗi: " + ex.Message);
                                        tranBaoGiaSuaChuaTam.Rollback();
                                        _cmd.Connection.Close();
                                    }
                                }
                            }
                        }
                        if(superTabControlBaoDuong.SelectedTab == superTabItemXeBaoDuongDaiHan)
                        {
                            foreach (DataGridViewRow row in dataGridViewXeBaoDuongDaiHan.SelectedRows)
                            {
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranLichSuBaoDuongXeTam = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranLichSuBaoDuongXeTam;
                                try
                                {
                                    _cmd.CommandText =
                                    "delete from LichSuBaoDuongXeTam Where IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang and IdBaoDuong=@IdBaoDuong";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                    _cmd.Parameters.AddWithValue("@IdCuaHang", CompanyInfo.IdsCuaHang);
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong",
                                        Convert.ToString(row.Cells["IdBaoDuongDaiHan"].Value.ToString()));
                                    int a = _cmd.ExecuteNonQuery();
                                    tranLichSuBaoDuongXeTam.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show("Lỗi: " + ex.Message);
                                    tranLichSuBaoDuongXeTam.Rollback();
                                    _cmd.Connection.Close();
                                }
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranlichsubaoduongchitiettam2 = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranlichsubaoduongchitiettam2;
                                try
                                {
                                    _cmd.CommandText = "delete lichsubaoduongchitiettam2 where IdBaoDuong=@IdBaoDuong";
                                    int b = _cmd.ExecuteNonQuery();
                                    tranlichsubaoduongchitiettam2.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi: " + ex.Message);
                                    tranlichsubaoduongchitiettam2.Rollback();
                                    _cmd.Connection.Close();

                                }
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranThoDichVu_TienCongThoTam = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranThoDichVu_TienCongThoTam;
                                try
                                {
                                    _cmd.CommandText =
                                        "delete ThoDichVu_TienCongThoTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                                    int c = _cmd.ExecuteNonQuery();
                                    tranThoDichVu_TienCongThoTam.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi: " + ex.Message);
                                    tranThoDichVu_TienCongThoTam.Rollback();
                                    _cmd.Connection.Close();

                                }
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                SqlTransaction tranThoDichVu_GioViecTam = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranThoDichVu_GioViecTam;
                                try
                                {
                                    _cmd.CommandText =
                                    "delete ThoDichVu_GioViecTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                                    int d = _cmd.ExecuteNonQuery();
                                    tranThoDichVu_GioViecTam.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch(Exception ex)
                                {
                                    MessageBox.Show("Lỗi: " + ex.Message);
                                    tranThoDichVu_GioViecTam.Rollback();
                                    _cmd.Connection.Close();
                                }
                                _cmd.Connection = datatabase.getConnection();
                                _cmd.Connection.Open();
                                _cmd.CommandText =
                                    "delete ThueNgoaiTam where IdCongTy = @IdCongTy and IdBaoDuong=@IdBaoDuong";
                                int f = _cmd.ExecuteNonQuery();
                                _cmd.Connection.Close();

                                if (tableBaoGiaTam.Rows.Count > 0)
                                {
                                    _cmd.Connection = datatabase.getConnection();
                                    _cmd.Connection.Open();
                                    SqlTransaction tranBaoGiaCongThoTam = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranBaoGiaCongThoTam;
                                    try
                                    {
                                        //Xóa báo giá công thợ tạm
                                        _cmd.CommandText = "DELETE FROM BaoGiaCongThoTam WHERE IdBaoGia = @IdBaoGia";
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());
                                        _cmd.ExecuteNonQuery();
                                        tranBaoGiaCongThoTam.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Lỗi: " + ex.Message);
                                        tranBaoGiaCongThoTam.Rollback();
                                        _cmd.Connection.Close();
                                    }

                                    //Xóa báo giá phụ tùng tạm
                                    _cmd.Connection = datatabase.getConnection();
                                    _cmd.Connection.Open();
                                    SqlTransaction tranBaoGiaPhuTungTam = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranBaoGiaPhuTungTam;
                                    try
                                    {
                                        _cmd.CommandText = "DELETE FROM BaoGiaPhuTungTam WHERE IdBaoGia = @IdBaoGia";
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdBaoGia", tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());
                                        _cmd.ExecuteNonQuery();
                                        tranBaoGiaPhuTungTam.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Lỗi: " + ex.Message);
                                        tranBaoGiaPhuTungTam.Rollback();
                                        _cmd.Connection.Close();
                                    }

                                    //Xóa báo giá tạm
                                    _cmd.Connection = datatabase.getConnection();
                                    _cmd.Connection.Open();
                                    SqlTransaction tranBaoGiaSuaChuaTam = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranBaoGiaSuaChuaTam;
                                    try
                                    {
                                        _cmd.CommandText = "DELETE FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdBaoDuong",
                                            Convert.ToString(row.Cells["IdBaoDuongDaiHan"].Value));
                                        _cmd.ExecuteNonQuery();
                                        tranBaoGiaSuaChuaTam.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Lỗi: " + ex.Message);
                                        tranBaoGiaSuaChuaTam.Rollback();
                                        _cmd.Connection.Close();
                                    }
                                }
                            }
                        }

                        MessageBox.Show(@"Hủy bảo dưỡng xe: " + textBoxTimNhanhBienSo.Text + @" thành công.", @"Thông báo",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"Hủy bảo dưỡng thất bại. " + ex.Message, @"Thông báo", MessageBoxButtons.OK,
                            MessageBoxIcon.Warning);
                    }
                    finally
                    {
                        LoadDangBaoDuong();
                    }
                }
                catch
                {
                    //
                }
                finally { buttonHuyBo.Enabled = true; }
            }
        }

        #endregion Hủy bỏ lần bảo dưỡng

        #region Hoàn tất lần bảo dưỡng (Lưu lịch sử bảo dưỡng)

        private void buttonHoanTat_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(_idBd))
            {
                MessageBox.Show(@"Bạn chưa chọn xe bảo dưỡng!\nVui lòng kiểm tra lại.", @"Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }
            try
            {
                buttonHoanTat.Enabled = false;
                if (MessageBox.Show(@"Bạn muốn hoàn thành phiếu sửa chữa này?", @"Hoàn thành bảo dưỡng", MessageBoxButtons.OKCancel) == DialogResult.OK)
                {
                    try
                    {
                        #region khai báo biến lưu trữ thông tin khách hàng để đẩy lên caresoft
                        //ThongTinKhachHang ttkh = new ThongTinKhachHang();
                        //List<Parameter> danhsach = new List<Parameter>();
                        #endregion
                        Checkngaygiotanviet();
                        if (!String.IsNullOrEmpty(textBoxChietKhau.Text))
                            _chietkhau = 1 - (int.Parse(textBoxChietKhau.Text) * 0.01);
                        decimal soTienTrietKhau = 0;
                        if (!String.IsNullOrEmpty(textBoxTienTrietKhau.Text))
                            soTienTrietKhau = decimal.Parse(textBoxTienTrietKhau.Text);
                        _cmd.CommandText = @"Select * from lichsubaoduongxetam where Idcongty = @IdCongTy and IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
                        DataTable dtLichSuBaoDuongXeTam = datatabase.getData(_cmd);
                        if (dtLichSuBaoDuongXeTam.Rows.Count <= 0)
                        {
                            MessageBox.Show(@"Thông tin xe bảo dưỡng không tồn tại!\nVui lòng kiểm tra lại.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                        _cmd.CommandText = "select * from lichsubaoduongchitiettam2 where IdBaoDuong = @IdBaoDuong";
                        _dtPhuTungThayThe = datatabase.getData(_cmd);
                        //
                        _cmd.CommandText = "select * from chuandoanxetam where IdBaoDuong = @IdBaoDuong";
                        _dtChuanDoanXeTam = datatabase.getData(_cmd);
                        //
                        _cmd.CommandText = "select * from ThoDichVu_GioViecTam where IdbaoDuong = @IdBaoDuong And IdCongTy = @IdCongTy";
                        _dtThoDichVuGioViecTam = datatabase.getData(_cmd);
                        //
                        _cmd.CommandText = "select * from ThoDichVu_TienCongThoTam where IdBaoDuong = @IdBaoDuong  And IdCongTy = @IdCongTy";
                        _dtThoDichVuTienCongTam = datatabase.getData(_cmd);
                        //
                        _cmd.CommandText = "select * from ThueNgoaiTam Where IdcongTy = @IdCongTy and IdBaoDuong = @IdBaoDuong And IdCongTy = @IdCongTy";
                        _dtThueNgoaiTam = datatabase.getData(_cmd);
                        //
                        _cmd.CommandText = "Select * from PhuTung WHERE IdCongTy = @IdCongTy";
                        _dtPhuTung = datatabase.getData(_cmd);
                        //
                        _cmd.CommandText = "SELECT * FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                        _tableBaoGiaTam = datatabase.getData(_cmd);

                        if (_tableBaoGiaTam.Rows.Count > 0)
                        {
                            _cmd.CommandText = "SELECT * FROM BaoGiaCongThoTam WHERE IdBaoGia = @IdBaoGia";
                            _cmd.Parameters.Clear();
                            _cmd.Parameters.AddWithValue("@IdBaoGia", _tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());
                            _tableBaoGiaCongThoTam = datatabase.getData(_cmd);
                            _cmd.CommandText = "SELECT * FROM BaoGiaPhuTungTam WHERE IdBaoGia = @IdBaoGia";
                            _cmd.Parameters.Clear();
                            _cmd.Parameters.AddWithValue("@IdBaoGia", _tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());
                            _tableBaoGiaPhuTungTam = datatabase.getData(_cmd);
                        }

                        try
                        {
                          
                                var ngaygiaoxe = dateTimeInputNgayRa.Value.Date.Add(new TimeSpan(int.Parse(dateTimeInputGioRa.Text.Trim().Split(':')[0]), int.Parse(dateTimeInputGioRa.Text.Trim().Split(':')[1]), 0));
                                if (_check > 0)
                                    _cmd.CommandText = @"insert into LichSuBaoDuongXe(idcuahang,idkhachhang,idcongty,tenxe,bienso,somay,sokhung,ngaybaoduong,ngaygiaoxe,solan,SoKm,ThayDau, YeuCauKH, IdThoDuyet, XuatPT,ThayDauMay,GhiChu,LoaiBaoDuong,NhongXich,LamMay,GIOVAOXE,TGDUKIEN,TTBAODUONG,GIOHOANTHANH,cviec,bannang,kythuatvien,TuVanSuaChua)
                                                    values(@idcuahang,@idkhachhang,@idcongty,@tenxe,@bienso,@somay,@sokhung,@ngaybaoduong,DATEADD(hh,12,@NgayGiaoXe),@solan,@SoKm,@ThayDau, @YeuCauKH, @IdThoDuyet, @XuatPT, @ThayDauMay, @GhiChu,@LoaiBaoDuong,@NhongXich,@LamMay,@GIOVAOXE,@TGDUKIEN,@TTBAODUONG,@GIOHOANTHANH,@cviec,@BANNANG,@KYTHUATVIEN,@TuVanSuaChua) select @@Identity";
                                else
                                    _cmd.CommandText = @"insert into LichSuBaoDuongXe(idcuahang,idkhachhang,idcongty,tenxe,bienso,somay,sokhung,ngaybaoduong,ngaygiaoxe,solan,SoKm,ThayDau, YeuCauKH, IdThoDuyet, XuatPT,ThayDauMay, GhiChu,LoaiBaoDuong,NhongXich,LamMay,GIOVAOXE,TGDUKIEN,TTBAODUONG,GIOHOANTHANH,cviec,bannang,kythuatvien,TuVanSuaChua)
                                                    values(@idcuahang,@idkhachhang,@idcongty,@tenxe,@bienso,@somay,@sokhung,@ngaybaoduong,@NgayGiaoXe,@solan,@SoKm,@ThayDau, @YeuCauKH, @IdThoDuyet, @XuatPT, @ThayDauMay, @GhiChu,@LoaiBaoDuong,@NhongXich,@LamMay,@GIOVAOXE,@TGDUKIEN,@TTBAODUONG,@GIOHOANTHANH,@cviec,@BANNANG,@KYTHUATVIEN,@TuVanSuaChua) select @@Identity";
                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@idcuahang", CompanyInfo.IdsCuaHang);
                                _cmd.Parameters.AddWithValue("@idkhachhang", dtLichSuBaoDuongXeTam.Rows[0]["IdKhachHang"]);
                                _cmd.Parameters.AddWithValue("@Idcongty", CompanyInfo.idcongty);
                                _cmd.Parameters.AddWithValue("@tenxe", dtLichSuBaoDuongXeTam.Rows[0]["TenXe"]);
                                _cmd.Parameters.AddWithValue("@bienso", dtLichSuBaoDuongXeTam.Rows[0]["BienSo"]);
                                _cmd.Parameters.AddWithValue("@somay", dtLichSuBaoDuongXeTam.Rows[0]["Somay"]);
                                _cmd.Parameters.AddWithValue("@sokhung", dtLichSuBaoDuongXeTam.Rows[0]["Sokhung"]);
                                _cmd.Parameters.AddWithValue("@ngaybaoduong", dtLichSuBaoDuongXeTam.Rows[0]["NgayBaoDuong"]);
                                //Ngay bao duong dateTimeInputNgayVao.Value.Date.Add(new TimeSpan(int.Parse(dateTimeInputGioVao.Text.Trim().Split(':')[0]), int.Parse(dateTimeInputGioVao.Text.Trim().Split(':')[1]), 0))
                                _cmd.Parameters.AddWithValue("@NgayGiaoXe", ngaygiaoxe);
                                _cmd.Parameters.AddWithValue("@solan", dtLichSuBaoDuongXeTam.Rows[0]["Solan"]);
                                _cmd.Parameters.AddWithValue("@sokm", dtLichSuBaoDuongXeTam.Rows[0]["SoKm"]);
                                _cmd.Parameters.AddWithValue("@YeucauKH", dtLichSuBaoDuongXeTam.Rows[0]["YeuCauKH"]);
                                _cmd.Parameters.AddWithValue("@IDThoDuyet", dtLichSuBaoDuongXeTam.Rows[0]["IdThoDuyet"]);
                                _cmd.Parameters.AddWithValue("@XuatPT", dtLichSuBaoDuongXeTam.Rows[0]["XuatPT"]);
                                _cmd.Parameters.AddWithValue("@ThayDau", dtLichSuBaoDuongXeTam.Rows[0]["ThayDau"]);
                                _cmd.Parameters.AddWithValue("@NhongXich", dtLichSuBaoDuongXeTam.Rows[0]["NhongXich"]);
                                _cmd.Parameters.AddWithValue("@LamMay", dtLichSuBaoDuongXeTam.Rows[0]["LamMay"]);
                                _cmd.Parameters.AddWithValue("@GhiChu", textBoxGhiChuBaoDuong.Text);
                                _cmd.Parameters.AddWithValue("@LoaiBaoDuong", dtLichSuBaoDuongXeTam.Rows[0]["LoaiBaoDuong"]);
                                _cmd.Parameters.AddWithValue("@BANNANG", dtLichSuBaoDuongXeTam.Rows[0]["BANNANG"]);
                                _cmd.Parameters.AddWithValue("@KYTHUATVIEN", dtLichSuBaoDuongXeTam.Rows[0]["KYTHUATVIEN"]);
                                _cmd.Parameters.AddWithValue("@TuVanSuaChua", dtLichSuBaoDuongXeTam.Rows[0]["TuVanSuaChua"]);
                                if (dtLichSuBaoDuongXeTam.Rows[0]["GIOVAOXE"].ToString() != "")
                                    _cmd.Parameters.AddWithValue("@GIOVAOXE", dtLichSuBaoDuongXeTam.Rows[0]["GIOVAOXE"]);
                                else
                                    _cmd.Parameters.AddWithValue("@GIOVAOXE", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff", CultureInfo.InvariantCulture));

                                _cmd.Parameters.AddWithValue("@TGDUKIEN", dtLichSuBaoDuongXeTam.Rows[0]["TGDUKIEN"]);
                                _cmd.Parameters.AddWithValue("@cviec", dtLichSuBaoDuongXeTam.Rows[0]["cviec"]);
                                _cmd.Parameters.AddWithValue("@TTBAODUONG", "5");
                                if (dtLichSuBaoDuongXeTam.Rows[0]["GIOHOANTHANH"].ToString() != "")
                                    _cmd.Parameters.AddWithValue("@GIOHOANTHANH", dtLichSuBaoDuongXeTam.Rows[0]["GIOHOANTHANH"]);
                                else
                                    _cmd.Parameters.AddWithValue("@GIOHOANTHANH", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff", CultureInfo.InvariantCulture));

                                int thay;
                                if (dtLichSuBaoDuongXeTam.Rows[0]["ThayDauMay"].ToString() == "True")
                                {
                                    thay = 1;
                                }
                                else { thay = 0; }
                                _cmd.Parameters.AddWithValue("@ThayDauMay", thay);


                            //Lấy Id bảo dưỡng
                            _cmd.Connection = datatabase.getConnection();
                            if (_cmd.Connection.State == ConnectionState.Closed)
                                _cmd.Connection.Open();
                            SqlTransaction tranLichSuBaoDuongXe = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranLichSuBaoDuongXe;
                            try
                            {
                                _idBaoDuong = _cmd.ExecuteScalar().ToString();
                                tranLichSuBaoDuongXe.Commit();
                                _cmd.Connection.Close();
                            }catch(Exception ex)
                            {
                                MessageBox.Show("Lỗi "+ex.Message);
                                tranLichSuBaoDuongXe.Rollback();
                                _cmd.Connection.Close();
                            }

                            #region lich su bao duong chi tiet2

                            if (_dtPhuTungThayThe.Rows.Count > 0)
                            {
                                foreach (DataRow row in _dtPhuTungThayThe.Rows)
                                {
                                    
                                        _cmd.CommandText = @"insert into lichsubaoduongchitiet2(IDBaoDuong, MaPT, TenPhuTung,SoLuong,Gia,GiaTien,IDKho,IDTho,IdPhuTung,TienTraTho)
                                                        Values(@IDBaoDuong,@MaPT,@TenPhuTung,@SoLuong,@Gia,@GiaTien,@IdKho,@IdTho,@IdPhuTung,@TienTraTho)";
                                        string mapt = Convert.ToString(row["MaPT"]);
                                        string sl = Convert.ToString(row["SoLuong"]);
                                        string tenpt = Convert.ToString(row["TenPhuTung"]);
                                        string idkho = Convert.ToString(row["IdKho"]);

                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuong);
                                        _cmd.Parameters.AddWithValue("@MaPT", Convert.ToString(row["MaPT"]));
                                        _cmd.Parameters.AddWithValue("@TenPhuTung", Convert.ToString(row["TenPhuTung"]));
                                        _cmd.Parameters.AddWithValue("@SoLuong", Convert.ToString(row["SoLuong"]));
                                        _cmd.Parameters.AddWithValue("@Gia", Convert.ToDecimal(row["Gia"]));
                                        _cmd.Parameters.AddWithValue("@GiaTien", Convert.ToDouble(row["GiaTien"]) * _chietkhau);
                                        _cmd.Parameters.AddWithValue("@IDKho", Convert.ToString(row["IdKho"]));
                                        _cmd.Parameters.AddWithValue("@IdTho", row["IdTho"]);
                                        _cmd.Parameters.AddWithValue("@IdPhuTung", Convert.ToString(row["IdPhuTung"]));
                                        _cmd.Parameters.AddWithValue("@TienTraTho", Convert.ToString(row["TienTraTho"]));
                                    _cmd.Connection = datatabase.getConnection();
                                    if (_cmd.Connection.State == ConnectionState.Closed)
                                        _cmd.Connection.Open();
                                    SqlTransaction tranlichsubaoduongchitiet2 = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranlichsubaoduongchitiet2;
                                    try
                                    {
                                        _cmd.ExecuteNonQuery();
                                        tranlichsubaoduongchitiet2.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Lỗi " + ex.Message);
                                        tranlichsubaoduongchitiet2.Rollback();
                                        _cmd.Connection.Close();

                                    }

                                    DataRow[] rows = _dtPhuTung.Select("IdPT = '" + Convert.ToString(row["IdPhuTung"]) + "'");
                                    if (rows.Length > 0)
                                    {
                                        int soluongtruoc;

                                        try
                                        {
                                            soluongtruoc = Convert.ToInt32(rows[0]["SoLuong"]);
                                        }
                                        catch
                                        {
                                            soluongtruoc = 0;
                                        }
                                        _cmd.Connection = datatabase.getConnection();
                                        if (_cmd.Connection.State == ConnectionState.Closed)
                                            _cmd.Connection.Open();
                                        SqlTransaction tranKhoXuat = _cmd.Connection.BeginTransaction();
                                        _cmd.Transaction = tranKhoXuat;
                                        try
                                        {
                                            _cmd.CommandText = @"update dbo.KhoXuat SET IdBaoDuong = @IdBaoDuong where IdBaoDuongTam = @IdBaoDuongTam and IdCongTy = @IdCongTy and IdKho = @IdKho";
                                            _cmd.Parameters.Clear();
                                            _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuong);
                                            _cmd.Parameters.AddWithValue("@IdKho", idkho);
                                            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                            _cmd.Parameters.AddWithValue("@IdBaoDuongTam", _idBd);
                                            _cmd.ExecuteNonQuery();
                                            tranKhoXuat.Commit();
                                            _cmd.Connection.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Lỗi " + ex.Message);
                                            tranKhoXuat.Rollback();
                                            _cmd.Connection.Close();
                                        }
                                    }
                                    
                                }
                            }
                            _cmd.Connection = datatabase.getConnection();
                            if (_cmd.Connection.State == ConnectionState.Closed)
                                _cmd.Connection.Open();
                            SqlTransaction tranThongTinNguoiDiBaoDuong = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranThongTinNguoiDiBaoDuong;
                            try
                            {
                                _cmd.CommandText = @"Update dbo.ThongTinNguoiDiBaoDuong SET IdBaoDuong = @IdBaoDuong where IdBaoDuongTam = @IdBaoDuongTam and IdCongTy = @IdCongTy and IdCuaHang = @IdCuaHang";
                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuong);
                                _cmd.Parameters.AddWithValue("@IdCuaHang", Class.CompanyInfo.IdsCuaHang);
                                _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                _cmd.Parameters.AddWithValue("@IdBaoDuongTam", _idBd);
                                _cmd.ExecuteNonQuery();
                                tranThongTinNguoiDiBaoDuong.Commit();
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranThongTinNguoiDiBaoDuong.Rollback();
                                _cmd.Connection.Close();

                            }

                            #endregion lich su bao duong chi tiet2

                            #region Gửi tin thay dầu

                            bool trangThaiThayDauMay = false;
                            bool trangThaiThayDauThuong = false;

                            try
                            {
                                string thayDauMay = dtLichSuBaoDuongXeTam.Rows[0]["ThayDauMay"].ToString();
                                trangThaiThayDauMay = bool.Parse(thayDauMay);
                            }
                            catch
                            {
                                //
                            }

                            try
                            {
                                string thayDauThuong = dtLichSuBaoDuongXeTam.Rows[0]["ThayDau"].ToString();
                                trangThaiThayDauThuong = bool.Parse(thayDauThuong);
                            }
                            catch
                            {
                                //
                            }

                            bool changeOilByKm = _changeOilKm.IsUseChangeOilByKM(CompanyInfo.idcongty);

                            //Nếu không thay dầu máy thì nhắn tin thay dầu thường
                            if (trangThaiThayDauMay == false)
                            {
                                //Nếu có thay dầu thường
                                if (trangThaiThayDauThuong && changeOilByKm == false)
                                {
                                    #region nhantinthaydau

                                    try
                                    {
                                        DataTable tblThayDauConfig = new DataTable();
                                        _cmd.Connection = datatabase.getConnection();
                                        if (_cmd.Connection.State == ConnectionState.Closed)
                                            _cmd.Connection.Open();
                                        SqlTransaction tranSMSMoiThayDau_Config = _cmd.Connection.BeginTransaction();
                                        _cmd.Transaction = tranSMSMoiThayDau_Config;
                                        try
                                        {
                                            _cmd.CommandText = "Select top 1 * from SMSMoiThayDau_Config where idcongty=" + CompanyInfo.idcongty + " and active=1";
                                            SqlDataAdapter adapThayDauConfig = new SqlDataAdapter(_cmd);
                                            adapThayDauConfig.Fill(tblThayDauConfig);
                                            tranSMSMoiThayDau_Config.Commit();
                                            _cmd.Connection.Close();
                                        }
                                        catch(Exception ex)
                                        {
                                            MessageBox.Show("Lỗi " + ex.Message);
                                            tranSMSMoiThayDau_Config.Rollback();
                                            _cmd.Connection.Close();
                                        }
                                        DataTable sms = new DataTable();
                                        _cmd.Connection = datatabase.getConnection();
                                        if (_cmd.Connection.State == ConnectionState.Closed)
                                            _cmd.Connection.Open();
                                        SqlTransaction tranSMSConfig = _cmd.Connection.BeginTransaction();
                                        _cmd.Transaction = tranSMSConfig;
                                        try
                                        {
                                            _cmd.CommandText = "select sms from SMSConfig where idcongty=" + CompanyInfo.idcongty + " and Type='Thay dau'";
                                            SqlDataAdapter adapsms = new SqlDataAdapter(_cmd);
                                            adapsms.Fill(sms);
                                            tranSMSConfig.Commit();
                                            _cmd.Connection.Close();
                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show("Lỗi " + ex.Message);
                                            tranSMSConfig.Rollback();
                                            _cmd.Connection.Close();

                                        }
                                        if ((CompanyInfo.idcongty == "92" || CompanyInfo.idcongty == "94" || CompanyInfo.idcongty == "95" || CompanyInfo.idcongty == "3") && sms.Rows.Count > 0)
                                        {
                                            string nhansausongay = "45";
                                            string gionhan = "10";

                                            DateTime day = DateTime.Now.AddDays(int.Parse(nhansausongay));
                                            //MessageBox.Show(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                                            DateTime timechedule = new DateTime(day.Year, day.Month, day.Day, int.Parse(gionhan), 0, 0, 0);
                                            string ngay = _ngaysinh;
                                            string resms = Utilities.Smsreplace(sms.Rows[0]["SMS"].ToString(), _tenkh,
                                                ngay, CompanyInfo.sendername, _tenxe, textBoxTimNhanhBienSo.Text,
                                                textBoxTimNhanhSoKhung.Text, textBoxTimNhanhSoMay.Text, textBoxDienThoai.Text, _solan,
                                                "");
                                            bool isunicode = Tools.GetDataCoding(resms) == 8;

                                            if (timechedule.Date >= DateTime.Now.Date)
                                            {
                                                bool isCheck = false;
                                                // hẹn lịch 6 lần gửi thay dầu
                                                for (int i = 0; i < 6; i++)
                                                {
                                                    // send sms
                                                    if (i > 0) timechedule = timechedule.AddDays(45);
                                                    _cmd.Connection = datatabase.getConnection();
                                                    if (_cmd.Connection.State == ConnectionState.Closed)
                                                        _cmd.Connection.Open();
                                                    SqlTransaction tranTinNhan = _cmd.Connection.BeginTransaction();
                                                    _cmd.Transaction = tranTinNhan;
                                                    try
                                                    {
                                                        _cmd.CommandText = @"Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule)
                                                                values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)";
                                                        _cmd.Parameters.Clear();
                                                        _cmd.Parameters.AddWithValue("@sendername", CompanyInfo.sendername);
                                                        _cmd.Parameters.AddWithValue("@phone", textBoxDienThoai.Text.Trim());
                                                        _cmd.Parameters.AddWithValue("@sms", resms);
                                                        _cmd.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                                                        _cmd.Parameters.AddWithValue("@smstype", "Thay dau");
                                                        _cmd.Parameters.AddWithValue("@idcongty", CompanyInfo.idcongty);
                                                        _cmd.Parameters.AddWithValue("@idkhachhang", _idKhachHang);
                                                        _cmd.Parameters.AddWithValue("@timeSchedule", timechedule);
                                                        _cmd.ExecuteNonQuery();
                                                        tranTinNhan.Commit();
                                                        _cmd.Connection.Close();
                                                        isCheck = true;
                                                    }
                                                    catch(Exception ex)
                                                    {
                                                        MessageBox.Show("Lỗi " + ex.Message);
                                                        tranTinNhan.Rollback();
                                                        _cmd.Connection.Close();
                                                    }
                                                }
                                                if (isCheck)
                                                {
                                                    MessageBox.Show(@"Gửi tin thay dầu được kích hoạt!", @"Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                            }
                                        }
                                        else if (tblThayDauConfig.Rows.Count > 0 && sms.Rows.Count > 0)
                                        {
                                            string nhansausongay = tblThayDauConfig.Rows[0]["nhansausongay"].ToString();
                                            string gionhan = tblThayDauConfig.Rows[0]["gionhan"].ToString();

                                            DateTime day = DateTime.Now.AddDays(int.Parse(nhansausongay));
                                            //MessageBox.Show(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

                                            DateTime timechedule = new DateTime(day.Year, day.Month, day.Day, int.Parse(gionhan), 0, 0, 0);
                                            string ngay = _ngaysinh;
                                            string resms = Utilities.Smsreplace(sms.Rows[0]["SMS"].ToString(), _tenkh,
                                                ngay, CompanyInfo.sendername, _tenxe, textBoxTimNhanhBienSo.Text,
                                                textBoxTimNhanhSoKhung.Text, textBoxTimNhanhSoMay.Text, textBoxDienThoai.Text, _solan,
                                                "");
                                            bool isunicode = Tools.GetDataCoding(resms) == 8;

                                            if (timechedule.Date >= DateTime.Now.Date)
                                            {
                                                _cmd.Connection = datatabase.getConnection();
                                                if (_cmd.Connection.State == ConnectionState.Closed)
                                                    _cmd.Connection.Open();
                                                SqlTransaction tranTinNhan = _cmd.Connection.BeginTransaction();
                                                _cmd.Transaction = tranTinNhan;
                                                try
                                                {
                                                    _cmd.CommandText = @"Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule)
                                                                values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)";
                                                    _cmd.Parameters.Clear();
                                                    _cmd.Parameters.AddWithValue("@sendername", CompanyInfo.sendername);
                                                    _cmd.Parameters.AddWithValue("@phone", textBoxDienThoai.Text.Trim());
                                                    _cmd.Parameters.AddWithValue("@sms", resms);
                                                    _cmd.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                                                    _cmd.Parameters.AddWithValue("@smstype", "Thay dau");
                                                    _cmd.Parameters.AddWithValue("@idcongty", CompanyInfo.idcongty);
                                                    _cmd.Parameters.AddWithValue("@idkhachhang", _idKhachHang);
                                                    _cmd.Parameters.AddWithValue("@timeSchedule", timechedule);
                                                    if (_cmd.ExecuteNonQuery() != 0)
                                                    {
                                                        MessageBox.Show(@"Gửi tin thay dầu được kích hoạt!", @"Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    }
                                                    tranTinNhan.Commit();
                                                    _cmd.Connection.Close();
                                                }
                                                catch(Exception ex)
                                                {
                                                    MessageBox.Show("Lỗi " + ex.Message);
                                                    tranTinNhan.Rollback();
                                                    _cmd.Connection.Close();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show(@"Không thể gửi tin nhắn!\nChưa cấu hình tự động tin nhắn hoặc không được kích hoạt.", @"Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(@"Tin nhắn thất bại. Lỗi :" + ex.Message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                        return;
                                    }

                                    #endregion nhantinthaydau
                                }
                                else if (trangThaiThayDauThuong == false && changeOilByKm)
                                {
                                    _changeOilKm.SendSmsChangeOilNormal(_idKhachHang, _idBaoDuong, int.Parse(dtLichSuBaoDuongXeTam.Rows[0]["SoKm"].ToString()), textBoxTimNhanhSoKhung.Text, CompanyInfo.idcongty);
                                }
                            }

                            if (trangThaiThayDauThuong == false)
                            {
                                #region gui tin thay dau may

                                if (trangThaiThayDauMay && changeOilByKm == false)
                                {
                                    GuiTinThayDauMay();
                                }
                                else if (trangThaiThayDauMay && changeOilByKm)
                                {
                                    _changeOilKm.SendSmsChangeOilDauMay(_idKhachHang, _idBaoDuong, int.Parse(dtLichSuBaoDuongXeTam.Rows[0]["SoKm"].ToString()), textBoxTimNhanhSoKhung.Text, CompanyInfo.idcongty);
                                }

                                #endregion gui tin thay dau may
                            }

                            #endregion Gửi tin thay dầu

                            #region Gửi tin kiểm tra nhông xích

                            var guiTinKiemTraNhongXich = false;

                            try
                            {
                                guiTinKiemTraNhongXich = bool.Parse(dtLichSuBaoDuongXeTam.Rows[0]["NhongXich"].ToString());
                            }
                            catch { }

                            if (guiTinKiemTraNhongXich)
                            {
                                try
                                {
                                    var tblCauHinhTinNhanNhongXich = new DataTable();
                                    _cmd.Connection = datatabase.getConnection();
                                    if (_cmd.Connection.State == ConnectionState.Closed)
                                        _cmd.Connection.Open();
                                    SqlTransaction tranCauHinhTinNhan = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranCauHinhTinNhan;
                                    try
                                    {
                                        _cmd.CommandText = "select top 1 * from CauHinhTinNhan where idcongty=" + CompanyInfo.idcongty + " and dangHoatDong=1 and maLoaiTin = 'NhongXich'";
                                        SqlDataAdapter adap = new SqlDataAdapter(_cmd);
                                        adap.Fill(tblCauHinhTinNhanNhongXich);
                                        tranCauHinhTinNhan.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Lỗi " + ex.Message);
                                        tranCauHinhTinNhan.Rollback();
                                        _cmd.Connection.Close();
                                    }

                                    if (tblCauHinhTinNhanNhongXich != null && tblCauHinhTinNhanNhongXich.Rows.Count > 0)
                                    {
                                        var nhansausongay = tblCauHinhTinNhanNhongXich.Rows[0]["sauSoNgay"].ToString();
                                        var gionhan = tblCauHinhTinNhanNhongXich.Rows[0]["gioNhan"].ToString();
                                        var sms = tblCauHinhTinNhanNhongXich.Rows[0]["noiDungTinNhan"].ToString();

                                        DateTime ngayNhan = DateTime.Now.AddDays(int.Parse(nhansausongay));

                                        DateTime timechedule = new DateTime(ngayNhan.Year, ngayNhan.Month, ngayNhan.Day, int.Parse(gionhan), 0, 0, 0);
                                        string ngay = _ngaysinh;
                                        string resms = Utilities.Smsreplace(sms, _tenkh,
                                            ngay, CompanyInfo.sendername, _tenxe, textBoxTimNhanhBienSo.Text,
                                            textBoxTimNhanhSoKhung.Text, textBoxTimNhanhSoMay.Text, textBoxDienThoai.Text, _solan,
                                            "");
                                        bool isunicode = Tools.GetDataCoding(resms) == 8;

                                        if (timechedule.Date >= DateTime.Now.Date)
                                        {
                                            _cmd.Connection = datatabase.getConnection();
                                            if (_cmd.Connection.State == ConnectionState.Closed)
                                                _cmd.Connection.Open();
                                            SqlTransaction tranTinNhan = _cmd.Connection.BeginTransaction();
                                            _cmd.Transaction = tranTinNhan;
                                            try
                                            {
                                                _cmd.CommandText = @"Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule)
                                                                values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)";
                                                _cmd.Parameters.Clear();
                                                _cmd.Parameters.AddWithValue("@sendername", CompanyInfo.sendername);
                                                _cmd.Parameters.AddWithValue("@phone", textBoxDienThoai.Text.Trim());
                                                _cmd.Parameters.AddWithValue("@sms", resms);
                                                _cmd.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                                                _cmd.Parameters.AddWithValue("@smstype", "Nhong Xich");
                                                _cmd.Parameters.AddWithValue("@idcongty", CompanyInfo.idcongty);
                                                _cmd.Parameters.AddWithValue("@idkhachhang", _idKhachHang);
                                                _cmd.Parameters.AddWithValue("@timeSchedule", timechedule);

                                                if (_cmd.ExecuteNonQuery() != 0)
                                                {
                                                    MessageBox.Show(@"Gửi tin kiểm tra Nhông Xích được kích hoạt!", @"Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                tranTinNhan.Commit();
                                                _cmd.Connection.Close();
                                            }
                                            catch(Exception ex)
                                            {
                                                MessageBox.Show("Lỗi " + ex.Message);
                                                tranTinNhan.Rollback();
                                                _cmd.Connection.Close();
                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //
                                }
                            }

                            #endregion Gửi tin kiểm tra nhông xích

                            #region Gửi tin kiểm tra máy quay

                            var guiTinKiemTraLamMay = false;

                            try
                            {
                                guiTinKiemTraLamMay = bool.Parse(dtLichSuBaoDuongXeTam.Rows[0]["LamMay"].ToString());
                            }
                            catch { }

                            if (guiTinKiemTraLamMay)
                            {
                                try
                                {
                                    var tblCauHinhTinNhanLamMay = new DataTable();
                                    _cmd.Connection = datatabase.getConnection();
                                    if (_cmd.Connection.State == ConnectionState.Closed)
                                        _cmd.Connection.Open();
                                    SqlTransaction tranCauHinhTinNhan = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranCauHinhTinNhan;
                                    try
                                    {
                                        _cmd.CommandText = "select top 1 * from CauHinhTinNhan where idcongty=" + CompanyInfo.idcongty + " and dangHoatDong=1 and maLoaiTin = 'LamMay'";
                                        SqlDataAdapter adap = new SqlDataAdapter(_cmd);
                                        adap.Fill(tblCauHinhTinNhanLamMay);
                                        tranCauHinhTinNhan.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show("Lỗi " + ex.Message);
                                        tranCauHinhTinNhan.Rollback();
                                        _cmd.Connection.Close();
                                    }


                                    if (tblCauHinhTinNhanLamMay != null && tblCauHinhTinNhanLamMay.Rows.Count > 0)
                                    {
                                        var nhansausongay = tblCauHinhTinNhanLamMay.Rows[0]["sauSoNgay"].ToString();
                                        var gionhan = tblCauHinhTinNhanLamMay.Rows[0]["gioNhan"].ToString();
                                        var sms = tblCauHinhTinNhanLamMay.Rows[0]["noiDungTinNhan"].ToString();

                                        DateTime ngayNhan = DateTime.Parse(ngaygiaoxe.ToString()).AddDays(int.Parse(nhansausongay));

                                        DateTime timechedule = new DateTime(ngayNhan.Year, ngayNhan.Month, ngayNhan.Day, int.Parse(gionhan), 0, 0, 0);
                                        string ngay = _ngaysinh;
                                        string resms = Utilities.Smsreplace(sms, _tenkh,
                                            ngay, CompanyInfo.sendername, _tenxe, textBoxTimNhanhBienSo.Text,
                                            textBoxTimNhanhSoKhung.Text, textBoxTimNhanhSoMay.Text, textBoxDienThoai.Text, _solan,
                                            "");
                                        bool isunicode = Tools.GetDataCoding(resms) == 8;

                                        if (timechedule.Date >= DateTime.Now.Date)
                                        {
                                            _cmd.Connection = datatabase.getConnection();
                                            if (_cmd.Connection.State == ConnectionState.Closed)
                                                _cmd.Connection.Open();
                                            SqlTransaction tranTinNhan = _cmd.Connection.BeginTransaction();
                                            _cmd.Transaction = tranTinNhan;
                                            try
                                            {
                                                _cmd.CommandText = @"Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule)
                                                                values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)";
                                                _cmd.Parameters.Clear();
                                                _cmd.Parameters.AddWithValue("@sendername", CompanyInfo.sendername);
                                                _cmd.Parameters.AddWithValue("@phone", textBoxDienThoai.Text.Trim());
                                                _cmd.Parameters.AddWithValue("@sms", resms);
                                                _cmd.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                                                _cmd.Parameters.AddWithValue("@smstype", "May Quay");
                                                _cmd.Parameters.AddWithValue("@idcongty", CompanyInfo.idcongty);
                                                _cmd.Parameters.AddWithValue("@idkhachhang", _idKhachHang);
                                                _cmd.Parameters.AddWithValue("@timeSchedule", timechedule);

                                                if (_cmd.ExecuteNonQuery() != 0)
                                                {
                                                    MessageBox.Show(@"Gửi tin kiểm tra Máy Quay được kích hoạt!", @"Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                }
                                                tranTinNhan.Rollback();
                                                _cmd.Connection.Close();
                                            }
                                            catch(Exception ex)
                                            {

                                                MessageBox.Show("Lỗi " + ex.Message);
                                                tranTinNhan.Rollback();
                                                _cmd.Connection.Close();

                                            }
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                    //
                                }
                            }

                            #endregion Gửi tin kiểm tra máy quay

                            #region Cong tho khac

                            //Thuê ngoài
                            if (_dtThueNgoaiTam.Rows.Count > 0)
                            {
                                _cmd.CommandText = @"insert into ThueNgoai(CongViec,TienThueNgoai,TienLayCuaKh,TienLai,GhiChu,IdCongTy,IdTho,IDBaoDuong,NgaySuaChua)
                                                    values(@CongViec,@TienThueNgoai,@TienLayCuaKH,@TienLai,@GhiChu,@IdCongTy,@IdTho,@IdBaoDuong,@NgaySuaChua)";

                                foreach (DataRow row in _dtThueNgoaiTam.Rows)
                                {
                                    _cmd.Connection = datatabase.getConnection();
                                    if (_cmd.Connection.State == ConnectionState.Closed)
                                        _cmd.Connection.Open();
                                    SqlTransaction tranThueNgoai = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranThueNgoai;
                                    try
                                    {
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@CongViec", Convert.ToString(row["CongViec"]));
                                        _cmd.Parameters.AddWithValue("@TienThueNgoai", Convert.ToString(row["TienThueNgoai"]));
                                        //_cmd.Parameters.AddWithValue("@TienLayCuaKh", (Convert.ToDouble(row["TienLayCuaKh"]) * _chietkhau).ToString(CultureInfo.InvariantCulture));
                                        //_cmd.Parameters.AddWithValue("@TienLai", (Convert.ToDouble(row["TienLai"]) * _chietkhau).ToString(CultureInfo.InvariantCulture));
                                        _cmd.Parameters.AddWithValue("@TienLayCuaKh", (Convert.ToDouble(row["TienLayCuaKh"])).ToString(CultureInfo.InvariantCulture));
                                        _cmd.Parameters.AddWithValue("@TienLai", (Convert.ToDouble(row["TienLai"])).ToString(CultureInfo.InvariantCulture));
                                        _cmd.Parameters.AddWithValue("@GhiChu", Convert.ToString(row["GhiChu"]));
                                        _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                        _cmd.Parameters.AddWithValue("@IdTho", Convert.ToString(row["IDTho"]));
                                        _cmd.Parameters.AddWithValue("@NgaySuaChua", dateTimeInputNgayVao.Value.Date.Add(
                                            new TimeSpan(
                                                int.Parse(dateTimeInputGioVao.Text.Trim().Split(':')[0]),
                                                int.Parse(dateTimeInputGioVao.Text.Trim().Split(':')[1]), 0)));
                                        _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuong);
                                        _cmd.ExecuteNonQuery();
                                        tranThueNgoai.Commit();
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Lỗi " + ex.Message);
                                        tranThueNgoai.Rollback();
                                        _cmd.Connection.Close();

                                    }
                                }
                            }

                            //Giờ công việc
                            if (_dtThoDichVuGioViecTam.Rows.Count > 0)
                            {

                                _cmd.CommandText = @"insert into ThoDichVu_GioViec(IdTho,IdGioViec,NgaySuaChua,GhiChu,IdCongTy,IDBaoDuong)
                                                    values(@Idtho,@IdGioViec,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong)";

                                foreach (DataRow row in _dtThoDichVuGioViecTam.Rows)
                                {
                                    _cmd.Connection = datatabase.getConnection();
                                    if (_cmd.Connection.State == ConnectionState.Closed)
                                        _cmd.Connection.Open();
                                    SqlTransaction tranThoDichVu_GioViec = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranThoDichVu_GioViec;
                                    try
                                    {
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdTho", Convert.ToString(row["IdTho"]));
                                        _cmd.Parameters.AddWithValue("@IdGioViec", Convert.ToString(row["IdGioViec"]));
                                        _cmd.Parameters.AddWithValue("@NgaySuaChua", dateTimeInputNgayVao.Value.Date.Add(
                                            new TimeSpan(
                                                int.Parse(dateTimeInputGioVao.Text.Trim().Split(':')[0]),
                                                int.Parse(dateTimeInputGioVao.Text.Trim().Split(':')[1]), 0)));
                                        _cmd.Parameters.AddWithValue("@GhiChu", Convert.ToString(row["GhiChu"]));
                                        _cmd.Parameters.AddWithValue("@IdCongTy", Convert.ToString(row["IdTho"]));
                                        _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuong);
                                        _cmd.ExecuteNonQuery();
                                        tranThoDichVu_GioViec.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Lỗi " + ex.Message);
                                        tranThoDichVu_GioViec.Rollback();
                                        _cmd.Connection.Close();
                                    }
                                }
                            }

                            //Tiền công viêc
                            if (_dtThoDichVuTienCongTam.Rows.Count > 0)
                            {
                                _cmd.CommandText = @"insert into ThoDichVu_TienCongTho2(IdTho,IdTienCong,NgaySuaChua,GhiChu,IdCongTy,IDBaoDuong,TienCong,TienKhachTra)
                                                    values(@Idtho,@IdTienCong,@NgaySuaChua,@GhiChu,@IdCongTy,@IdBaoDuong,@TienCong,@TienKhachTra)";
                                foreach (DataRow row in _dtThoDichVuTienCongTam.Rows)
                                {
                                    _cmd.Connection = datatabase.getConnection();
                                    if (_cmd.Connection.State == ConnectionState.Closed)
                                        _cmd.Connection.Open();
                                    SqlTransaction tranThoDichVu_TienCongTho2 = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranThoDichVu_TienCongTho2;
                                    try
                                    {
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdTho", Convert.ToString(row["IdTho"]));
                                        _cmd.Parameters.AddWithValue("@IdTienCong", Convert.ToString(row["IdTienCong"]));
                                        _cmd.Parameters.AddWithValue("@NgaySuaChua", DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.fff", CultureInfo.InvariantCulture));
                                        _cmd.Parameters.AddWithValue("@GhiChu", Convert.ToString(row["GhiChu"]));
                                        _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                        _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuong);
                                        _cmd.Parameters.AddWithValue("@TienCong", Convert.ToDecimal(row["TienCong"]));
                                        //_cmd.Parameters.AddWithValue("@TienKhachTra", Convert.ToDouble(row["TienKhachTra"]) * _chietkhau);
                                        _cmd.Parameters.AddWithValue("@TienKhachTra", Convert.ToDouble(row["TienKhachTra"]));
                                        _cmd.ExecuteNonQuery();
                                        tranThoDichVu_TienCongTho2.Commit();
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Lỗi " + ex.Message);
                                        tranThoDichVu_TienCongTho2.Rollback();
                                        _cmd.Connection.Close();
                                    }
                                }
                            }

                            #endregion Cong tho khac

                            #region Lưu lịch sử báo giá

                            if (_tableBaoGiaTam.Rows.Count > 0)
                            {
                                string idBaoGia = "";
                                _cmd.Connection = datatabase.getConnection();
                                if (_cmd.Connection.State == ConnectionState.Closed)
                                    _cmd.Connection.Open();
                                SqlTransaction tranBaoGiaSuaChua = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranBaoGiaSuaChua;
                                try
                                {
                                    _cmd.CommandText = @"INSERT INTO BaoGiaSuaChua
                                                    (IdKhachHang, IdBaoDuong, NgayBaoGia, TongTienVatTu, TongTienCong, VAT, TongSauVAT, CoVanDV, TruongPhongDV)
                                                    VALUES (@IdKhachHang,@IdBaoDuong,@NgayBaoGia,@TongTienVatTu,@TongTienCong,@VAT,@TongSauVAT,@CoVanDV,@TruongPhongDV)
                                                    SELECT @@IDENTITY";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdKhachHang", _tableBaoGiaTam.Rows[0]["IdKhachHang"].ToString());
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuong);
                                    _cmd.Parameters.AddWithValue("@NgayBaoGia", _tableBaoGiaTam.Rows[0]["NgayBaoGia"].ToString());
                                    _cmd.Parameters.AddWithValue("@TongTienVatTu", _tableBaoGiaTam.Rows[0]["TongTienVatTu"].ToString());
                                    _cmd.Parameters.AddWithValue("@TongTienCong", _tableBaoGiaTam.Rows[0]["TongTienCong"].ToString());
                                    _cmd.Parameters.AddWithValue("@VAT", _tableBaoGiaTam.Rows[0]["VAT"].ToString());
                                    _cmd.Parameters.AddWithValue("@TongSauVAT", _tableBaoGiaTam.Rows[0]["TongSauVAT"].ToString());
                                    _cmd.Parameters.AddWithValue("@CoVanDV", _tableBaoGiaTam.Rows[0]["CoVanDV"].ToString());
                                    _cmd.Parameters.AddWithValue("@TruongPhongDV", _tableBaoGiaTam.Rows[0]["TruongPhongDV"].ToString());

                                    idBaoGia = _cmd.ExecuteScalar().ToString();
                                    tranBaoGiaSuaChua.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi " + ex.Message);
                                    tranBaoGiaSuaChua.Rollback();
                                    _cmd.Connection.Close();

                                }

                                foreach (DataRow row in _tableBaoGiaCongThoTam.Rows)
                                {
                                    _cmd.Connection = datatabase.getConnection();
                                    if (_cmd.Connection.State == ConnectionState.Closed)
                                        _cmd.Connection.Open();
                                    SqlTransaction tranBaoGiaCongTho = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranBaoGiaCongTho;
                                    try
                                    {
                                        _cmd.CommandText = @"INSERT INTO BaoGiaCongTho
                                                        (IdBaoGia, IdTienCong, NoiDungCV, TienCong, GhiChu, DaThucHien)
                                                        VALUES (@IdBaoGia,@IdTienCong,@NoiDungCV,@TienCong,@GhiChu,@DaThucHien)";
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdBaoGia", idBaoGia);
                                        _cmd.Parameters.AddWithValue("@IdTienCong", row["IdTienCong"].ToString());
                                        _cmd.Parameters.AddWithValue("@NoiDungCV", row["NoiDungCV"].ToString());
                                        _cmd.Parameters.AddWithValue("@TienCong", row["TienCong"].ToString());
                                        _cmd.Parameters.AddWithValue("@GhiChu", row["GhiChu"].ToString());
                                        _cmd.Parameters.AddWithValue("@DaThucHien", row["DaThucHien"].ToString());
                                        _cmd.ExecuteNonQuery();
                                        tranBaoGiaCongTho.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Lỗi " + ex.Message);
                                        tranBaoGiaCongTho.Rollback();
                                        _cmd.Connection.Close();
                                    }
                                }

                                foreach (DataRow row in _tableBaoGiaPhuTungTam.Rows)
                                {
                                    _cmd.Connection = datatabase.getConnection();
                                    if (_cmd.Connection.State == ConnectionState.Closed)
                                        _cmd.Connection.Open();
                                    SqlTransaction tranBaoGiaPhuTung = _cmd.Connection.BeginTransaction();
                                    _cmd.Transaction = tranBaoGiaPhuTung;
                                    try
                                    {
                                        _cmd.CommandText = @"INSERT INTO BaoGiaPhuTung
                                                        (IdBaoGia, IdKho, IdPhuTung, MaPT, TenPT, DVT, SoLuong, DonGia, ThanhTien, DaThucHien)
                                                        VALUES (@IdBaoGia,@IdKho,@IdPhuTung,@MaPT,@TenPT,@DVT,@SoLuong,@DonGia,@ThanhTien,@DaThucHien)";
                                        _cmd.Parameters.Clear();
                                        _cmd.Parameters.AddWithValue("@IdBaoGia", idBaoGia);
                                        _cmd.Parameters.AddWithValue("@IdKho", row["IdKho"].ToString());
                                        _cmd.Parameters.AddWithValue("@IdPhuTung", row["IdPhuTung"].ToString());
                                        _cmd.Parameters.AddWithValue("@MaPT", row["MaPT"].ToString());
                                        _cmd.Parameters.AddWithValue("@TenPT", row["TenPT"].ToString());
                                        _cmd.Parameters.AddWithValue("@DVT", row["DVT"].ToString());
                                        _cmd.Parameters.AddWithValue("@SoLuong", row["SoLuong"].ToString());
                                        _cmd.Parameters.AddWithValue("@DonGia", row["DonGia"].ToString());
                                        _cmd.Parameters.AddWithValue("@ThanhTien", row["ThanhTien"].ToString());
                                        _cmd.Parameters.AddWithValue("@DaThucHien", row["DaThucHien"].ToString());
                                        _cmd.ExecuteNonQuery();
                                        tranBaoGiaPhuTung.Commit();
                                        _cmd.Connection.Close();
                                    }
                                    catch(Exception ex)
                                    {
                                        MessageBox.Show("Lỗi " + ex.Message);
                                        tranBaoGiaPhuTung.Rollback();
                                        _cmd.Connection.Close();
                                    }
                                }
                            }

                            #endregion Lưu lịch sử báo giá

                            #region Xóa các bảng tạm

                            // Xóa lịch sử bảo dưỡng tạm
                            _cmd.Connection = datatabase.getConnection();
                            if (_cmd.Connection.State == ConnectionState.Closed)
                                _cmd.Connection.Open();
                            SqlTransaction tranlichsubaoduongxetam = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranlichsubaoduongxetam;
                            try
                            {
                                _cmd.CommandText = "delete lichsubaoduongxetam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
                                _cmd.ExecuteNonQuery();
                                tranlichsubaoduongxetam.Commit();
                                _cmd.Connection.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranlichsubaoduongxetam.Rollback();
                                _cmd.Connection.Close();

                            }

                            //Xóa lich sử bảo dưỡng chi tiết tam
                            _cmd.Connection = datatabase.getConnection();
                            if (_cmd.Connection.State == ConnectionState.Closed)
                                _cmd.Connection.Open();
                            SqlTransaction tranlichsubaoduongchitiettam2 = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranlichsubaoduongchitiettam2;
                            try
                            {
                                _cmd.CommandText = "delete lichsubaoduongchitiettam2 where Idbaoduong = @idbaoduong";
                                _cmd.ExecuteNonQuery();
                                tranlichsubaoduongchitiettam2.Commit();
                                _cmd.Connection.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranlichsubaoduongchitiettam2.Rollback();
                                _cmd.Connection.Close();

                            }


                            //Xóa việc theo giờ
                            _cmd.Connection = datatabase.getConnection();
                            if (_cmd.Connection.State == ConnectionState.Closed)
                                _cmd.Connection.Open();
                            SqlTransaction tranThoDichVu_GioViecTam = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranThoDichVu_GioViecTam;
                            try
                            {
                                _cmd.CommandText = "delete ThoDichVu_GioViecTam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                                _cmd.ExecuteNonQuery();
                                tranThoDichVu_GioViecTam.Commit();
                                _cmd.Connection.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranThoDichVu_GioViecTam.Rollback();
                                _cmd.Connection.Close();

                            }

                            //Xóa việc theo tiền
                            _cmd.Connection = datatabase.getConnection();
                            if (_cmd.Connection.State == ConnectionState.Closed)
                                _cmd.Connection.Open();
                            SqlTransaction tranThoDichVu_TienCongThoTam = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranThoDichVu_TienCongThoTam;
                            try
                            {
                                _cmd.CommandText = "delete ThoDichVu_TienCongThoTam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                                _cmd.ExecuteNonQuery();
                                tranThoDichVu_TienCongThoTam.Commit();
                                _cmd.Connection.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranThoDichVu_TienCongThoTam.Rollback();
                                _cmd.Connection.Close();

                            }


                            //Xóa việc thuê ngoài
                            _cmd.Connection = datatabase.getConnection();
                            if (_cmd.Connection.State == ConnectionState.Closed)
                                _cmd.Connection.Open();
                            SqlTransaction tranThueNgoaiTam = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranThueNgoaiTam;
                            try
                            {
                                _cmd.CommandText = "delete ThueNgoaiTam Where IdCongTy = @IdCongTy And IdBaoDuong = @IdBaoDuong";
                                _cmd.ExecuteNonQuery();
                                tranThueNgoaiTam.Commit();
                                _cmd.Connection.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranThueNgoaiTam.Rollback();
                                _cmd.Connection.Close();

                            }

                            if (_tableBaoGiaTam.Rows.Count > 0)
                            {
                                //Xóa báo giá công thợ tạm
                                _cmd.Connection = datatabase.getConnection();
                                if (_cmd.Connection.State == ConnectionState.Closed)
                                    _cmd.Connection.Open();
                                SqlTransaction tranBaoGiaCongThoTam = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranBaoGiaCongThoTam;
                                try
                                {
                                    _cmd.CommandText = "DELETE FROM BaoGiaCongThoTam WHERE IdBaoGia = @IdBaoGia";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdBaoGia", _tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());

                                    _cmd.ExecuteNonQuery();
                                    tranBaoGiaCongThoTam.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi " + ex.Message);
                                    tranBaoGiaCongThoTam.Rollback();
                                    _cmd.Connection.Close();

                                }
                                //Xóa báo giá phụ tùng tạm
                                _cmd.Connection = datatabase.getConnection();
                                if (_cmd.Connection.State == ConnectionState.Closed)
                                    _cmd.Connection.Open();
                                SqlTransaction tranBaoGiaPhuTungTam = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranBaoGiaPhuTungTam;
                                try
                                {
                                    _cmd.CommandText = "DELETE FROM BaoGiaPhuTungTam WHERE IdBaoGia = @IdBaoGia";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdBaoGia", _tableBaoGiaTam.Rows[0]["IdBaoGia"].ToString());

                                    _cmd.ExecuteNonQuery();
                                    tranBaoGiaPhuTungTam.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi " + ex.Message);
                                    tranBaoGiaPhuTungTam.Rollback();
                                    _cmd.Connection.Close();

                                }

                                //Xóa báo giá tạm
                                _cmd.Connection = datatabase.getConnection();
                                if (_cmd.Connection.State == ConnectionState.Closed)
                                    _cmd.Connection.Open();
                                SqlTransaction tranBaoGiaSuaChuaTam = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranBaoGiaSuaChuaTam;
                                try
                                {
                                    _cmd.CommandText = "DELETE FROM BaoGiaSuaChuaTam WHERE IdBaoDuong = @IdBaoDuong";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);

                                    _cmd.ExecuteNonQuery();
                                    tranBaoGiaSuaChuaTam.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi " + ex.Message);
                                    tranBaoGiaSuaChuaTam.Rollback();
                                    _cmd.Connection.Close();

                                }
                            }

                            #endregion Xóa các bảng tạm

                            //Insert lich su bao duong phieu
                            _cmd.Connection = datatabase.getConnection();
                            if (_cmd.Connection.State == ConnectionState.Closed)
                                _cmd.Connection.Open();
                            SqlTransaction tranlichsubaoduongphieu = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranlichsubaoduongphieu;
                            try
                            {
                                if (_check > 0)
                                    _cmd.CommandText = @"insert into lichsubaoduongphieu(idbaoduong,sophieu,tongtien, TienCongTho, TienPT,NgayGiaoXe,IdCongTy,PhanTramGiam,TienTrietKhau)
                                                    values(@idbaoduong,@sophieu,@tongtien, @TienCongTho, @TienPT,DATEADD(hh,12,@NgayGiaoXe),@IdCongTy,@PhanTramGiam,@TienTrietKhau)";
                                else
                                    _cmd.CommandText = @"insert into lichsubaoduongphieu(idbaoduong,sophieu,tongtien, TienCongTho, TienPT,NgayGiaoXe,IdCongTy,PhanTramGiam,TienTrietKhau)
                                                    values(@idbaoduong,@sophieu,@tongtien, @TienCongTho, @TienPT,@NgayGiaoXe,@IdCongTy,@PhanTramGiam,@TienTrietKhau)";

                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@idbaoduong", _idBaoDuong);
                                _cmd.Parameters.AddWithValue("@sophieu", textBoxSoPhieu.Text);
                                _cmd.Parameters.AddWithValue("@tongtien", (Convert.ToDouble(textBoxTienCongTho.Text) + Convert.ToDouble(textBoxTienPhuTung.Text)) * _chietkhau - (double)soTienTrietKhau);
                                //_cmd.Parameters.AddWithValue("@TienCongTho", Convert.ToDouble(textBoxTienCongTho.Text) * _chietkhau);
                                //_cmd.Parameters.AddWithValue("@TienPT", Convert.ToDouble(textBoxTienPhuTung.Text) * _chietkhau);
                                _cmd.Parameters.AddWithValue("@TienCongTho", Convert.ToDouble(textBoxTienCongTho.Text));
                                _cmd.Parameters.AddWithValue("@TienPT", Convert.ToDouble(textBoxTienPhuTung.Text));
                                _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);

                                if (String.IsNullOrEmpty(textBoxChietKhau.Text))
                                    _cmd.Parameters.AddWithValue("@PhanTramGiam", 0);
                                else
                                    _cmd.Parameters.AddWithValue("@PhanTramGiam", textBoxChietKhau.Text);

                                _cmd.Parameters.AddWithValue("@TienTrietKhau", soTienTrietKhau);
                                _cmd.Parameters.AddWithValue("@NgayGiaoXe", ngaygiaoxe);
                                _cmd.ExecuteNonQuery();
                                tranlichsubaoduongphieu.Commit();
                                _cmd.Connection.Close();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranlichsubaoduongphieu.Rollback();
                                _cmd.Connection.Close();

                            }

                            //Insert Phiếu thu
                            _cmd.Connection = datatabase.getConnection();
                            if (_cmd.Connection.State == ConnectionState.Closed)
                                _cmd.Connection.Open();
                            SqlTransaction tranPhieuThu = _cmd.Connection.BeginTransaction();
                            _cmd.Transaction = tranPhieuThu;
                            try
                            {
                                if (_check > 0)
                                    _cmd.CommandText = @"Insert into PhieuThu(IdLoaiPhieuThu,SoTienThu,IdCongTy,IdCuaHang,IdNhanVien,NgayHachToan,SoHoaDon)
                                                    Values(@idLoaiPhieuThu,@SoTienThu,@IdCongTy,@IdCuaHang,@IdNhanVien,DATEADD(hh,12,@NgayGiaoXe),@SoHoaDon)";
                                else
                                    _cmd.CommandText = @"Insert into PhieuThu(IdLoaiPhieuThu,SoTienThu,IdCongTy,IdCuaHang,IdNhanVien,NgayHachToan,SoHoaDon)
                                                    Values(@idLoaiPhieuThu,@SoTienThu,@IdCongTy,@IdCuaHang,@IdNhanVien,@NgayGiaoXe,@SoHoaDon)";
                                _cmd.Parameters.Clear();
                                _cmd.Parameters.AddWithValue("@IdLoaiPhieuThu", "5");
                                _cmd.Parameters.AddWithValue("@SoTienThu", Convert.ToDouble(textBoxTongTien.Text) * _chietkhau - (double)soTienTrietKhau);
                                _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                                _cmd.Parameters.AddWithValue("@IdCuaHang", CompanyInfo.IdsCuaHang);
                                _cmd.Parameters.AddWithValue("@IdNhanVien", EmployeeInfo.idnhanvien);
                                _cmd.Parameters.AddWithValue("@SoHoaDon", _idBaoDuong);
                                _cmd.Parameters.AddWithValue("@NgayGiaoXe", ngaygiaoxe);
                                _cmd.ExecuteNonQuery();
                                tranPhieuThu.Commit();
                                _cmd.Connection.Close();
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("Lỗi " + ex.Message);
                                tranPhieuThu.Rollback();
                                _cmd.Connection.Close();
                            }

                            if (textBoxTenLapPhieu.Text.Trim() != "")
                            {
                                //Thêm người lập phiếu
                                _cmd.Connection = datatabase.getConnection();
                                if (_cmd.Connection.State == ConnectionState.Closed)
                                    _cmd.Connection.Open();
                                SqlTransaction tranNguoiLapPhieu = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranNguoiLapPhieu;
                                try
                                {
                                    _cmd.CommandText = @"INSERT INTO NguoiLapPhieu(IdBaoDuong, TenNguoiLapPhieu)
                                                    VALUES(@IdBaoDuong, @TenNguoiLapPhieu)";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuong);
                                    _cmd.Parameters.AddWithValue("@TenNguoiLapPhieu", textBoxTenLapPhieu.Text);
                                    _cmd.ExecuteNonQuery();
                                    tranNguoiLapPhieu.Commit();
                                    _cmd.Connection.Close();

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi " + ex.Message);
                                    tranNguoiLapPhieu.Rollback();
                                    _cmd.Connection.Close();
                                }
                            }

                            if (!String.IsNullOrEmpty(txtKyThuatVien.Text.Trim()))
                            {
                                //Thêm kỹ thuật viên
                                _cmd.Connection = datatabase.getConnection();
                                if (_cmd.Connection.State == ConnectionState.Closed)
                                    _cmd.Connection.Open();
                                SqlTransaction tranKyThuatVien_BaoDuong = _cmd.Connection.BeginTransaction();
                                _cmd.Transaction = tranKyThuatVien_BaoDuong;
                                try
                                {
                                    _cmd.CommandText = @"INSERT INTO KyThuatVien_BaoDuong(IdBaoDuong, KyThuatVien)
                                                    VALUES(@IdBaoDuong, @KyThuatVien)";
                                    _cmd.Parameters.Clear();
                                    _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBaoDuong);
                                    _cmd.Parameters.AddWithValue("@KyThuatVien", txtKyThuatVien.Text);
                                    _cmd.ExecuteNonQuery();
                                    tranKyThuatVien_BaoDuong.Commit();
                                    _cmd.Connection.Close();
                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show("Lỗi " + ex.Message);
                                    tranKyThuatVien_BaoDuong.Rollback();
                                    _cmd.Connection.Close();
                                }
                            }

                            MessageBox.Show(@"Lưu lần bảo dưỡng thành công!\nNhấn In phiếu để in phiếu bảo dưỡng.", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            #region đẩy dữ liệu thông tin khách hàng nên caresoft thông qua api
                            //ttkh.username = textBoxTenKH.Text;
                            //ttkh.phone_no = textBoxDienThoai.Text;
                            //if (comboBoxGioiTinh.SelectedText=="Nam") {
                            //    ttkh.gender = 0;

                            //}
                            //else
                            //{
                            //    ttkh.gender = 1;
                            //}
                            
                            //ttkh.address = textBoxDiaChi.Text;
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.NgayBD,
                            //    value = dateTimeInputNgayVao.Value.ToString("yyyy/MM/dd"),
                            //}) ;
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.SoKhung,
                            //    value = textBoxSoKhung.Text,
                            //});
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.SoMay,
                            //    value = textBoxSoMay.Text,
                            //});
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.CPThanhToan,
                            //    value = textBoxTongTien.Text,
                            //});
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.CPThanhToan,
                            //    value = textBoxTongTien.Text,
                            //});
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.DiaChiKH,
                            //    value = textBoxDiaChi.Text,
                            //});
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.NgayBanXe,
                            //    value = dateTimeInputNgayMua.Value.ToString("yyyy/MM/dd"),
                            //});
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.BienSoXe,
                            //    value = textBoxBienSo.Text,
                            //});
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.NgaySinh,
                            //    value = dateTimeInputNgaySinh.Value.ToString("yyyy/MM/dd"),
                            //});
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.GioiTinh,
                            //    value = ttkh.gender.ToString(),
                            //});
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.HTKhachHang,
                            //    value = textBoxTenKH.Text,
                            //});
                            //danhsach.Add(new Parameter
                            //{
                            //    id = Define.SoHD,
                            //    value = textBoxSoPhieu.Text,
                            //});
                            //ttkh.custom_fields = danhsach;
                            //int iddulieu = 0;
                            //string contact = JsonConvert.SerializeObject( new{contact = ttkh });
                            //using (HttpClient httpClient = new HttpClient())
                            //{
                            //    httpClient.BaseAddress = new Uri(Define.api);
                            //    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                            //    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", Define.token);
                            //    HttpRequestMessage resquestMessage = new HttpRequestMessage(HttpMethod.Post, "api/v1/contacts");
                            //    resquestMessage.Content = new StringContent(contact, Encoding.UTF8, "application/json");
                            //    HttpResponseMessage responseMessage = httpClient.PostAsync("api/v1/contacts", resquestMessage.Content).Result;
                            //    if (responseMessage.IsSuccessStatusCode)
                            //    {
                            //        MessageBox.Show("Import thành công dữ liệu");
                            //    }
                            //    else
                            //    {
                            //        MessageBox.Show("Thất bại " + responseMessage.RequestMessage);
                            //        if (responseMessage.ReasonPhrase.Equals("Bad Request"))
                            //        {
                            //            ErrorRequest response = JsonConvert.DeserializeObject<ErrorRequest>(responseMessage.Content.ReadAsStringAsync().Result);
                            //            iddulieu = int.Parse(response.extra_data.duplicate_id.ToString());
                            //        }
                            //    }
                            //    if (iddulieu > 0)
                            //    {
                            //        HttpRequestMessage resquestMessage1 = new HttpRequestMessage(HttpMethod.Put, "/api/v1/contacts/" + iddulieu);
                            //        resquestMessage1.Content = new StringContent(contact, Encoding.UTF8, "application/json");
                            //        HttpResponseMessage responseMessage1 = httpClient.PutAsync("api/v1/contacts/" + iddulieu, resquestMessage1.Content).Result;
                            //        if (responseMessage1.IsSuccessStatusCode)
                            //        {
                            //            MessageBox.Show("Import thành công dữ liệu");
                            //        }
                            //        else
                            //        {
                            //            MessageBox.Show("Thất bai");
                            //        }
                            //    }
                            //}
                            #endregion
                            SelectedCustomer._idbaoduong = _idBaoDuong;
                            LoadDangBaoDuong();
                            idBaoDuongTam = "";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(@"Lỗi :" + ex.Message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        finally
                        {
                            _cmd.Connection.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Lỗi: " + ex.Message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                buttonHoanTat.Enabled = true;
            }
        }

#endregion Hoàn tất lần bảo dưỡng (Lưu lịch sử bảo dưỡng)

#region Gửi tin thay dầu máy

        private void GuiTinThayDauMay()
        {
#region nhantinthaydaumay

            try
            {
                string idcongty = CompanyInfo.idcongty;

                SqlCommand sqlCmd = new SqlCommand();
                sqlCmd.CommandText = "Select top 1 * from SMSMoiThayDauMay_Config where idcongty=" + idcongty + " and active=1";

                var tblThayDauConfig = datatabase.getData(sqlCmd);

                sqlCmd.CommandText = "select sms from SMSConfig where idcongty=" + idcongty + " and Type='Thay dau may'";

                var sms = datatabase.getData(sqlCmd);

                if (tblThayDauConfig.Rows.Count > 0 && sms.Rows.Count > 0)
                {
                    string nhansausongay = tblThayDauConfig.Rows[0]["nhansausongay"].ToString();
                    string gionhan = tblThayDauConfig.Rows[0]["gionhan"].ToString();

                    SqlCommand cmdInsert = new SqlCommand();
                    SqlCommand cmdGetDate = new SqlCommand();

                    cmdGetDate.CommandText = "select GETDATE() as Time";

                    DataTable dttime = datatabase.getData(cmdGetDate);
                    DateTime date = DateTime.Parse(dttime.Rows[0]["Time"].ToString());
                    DateTime day = date.AddDays(int.Parse(nhansausongay));
                    DateTime timechedule = new DateTime(day.Year, day.Month, day.Day, int.Parse(gionhan), 0, 0, 0);

                    string ngay = _ngaysinh;

                    string resms = Utilities.Smsreplace(sms.Rows[0]["SMS"].ToString(), _tenkh, ngay, CompanyInfo.sendername, _tenxe, textBoxTimNhanhBienSo.Text, textBoxTimNhanhSoKhung.Text, textBoxTimNhanhSoMay.Text, _dienthoai, _solan, "");
                    bool isunicode = Tools.GetDataCoding(resms) == 8;

                    cmdInsert.CommandText = @"Insert into TinNhan(sendername,phone,sms,countmes,smstype,idcongty,idkhachhang,timeSchedule) values (@sendername,@phone,@sms,@countmes,@smstype,@idcongty,@idkhachhang,@timeSchedule)";
                    cmdInsert.Parameters.Clear();
                    cmdInsert.Parameters.AddWithValue("@sendername", CompanyInfo.sendername);
                    cmdInsert.Parameters.AddWithValue("@phone", textBoxDienThoai);
                    cmdInsert.Parameters.AddWithValue("@sms", resms);
                    cmdInsert.Parameters.AddWithValue("@countmes", Utilities.CountMess(resms, isunicode));
                    cmdInsert.Parameters.AddWithValue("@smstype", "Thay dau may");
                    cmdInsert.Parameters.AddWithValue("@idcongty", CompanyInfo.idcongty);
                    cmdInsert.Parameters.AddWithValue("@idkhachhang", _idKhachHang);
                    cmdInsert.Parameters.AddWithValue("@timeSchedule", timechedule);

                    if (datatabase.ExcuteNonQuery(cmdInsert) != 0)
                        MessageBox.Show(@"Gửi tin thay dầu máy được kích hoạt", @"Thông Báo");
                }
                else
                {
                    MessageBox.Show(@"Không thể gửi tin nhắn do chưa cấu hình tự động tin nhắn hoặc không được kích hoạt", @"Thông Báo");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Tin nhắn thất bại. Lỗi :" + ex.Message, @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

#endregion nhantinthaydaumay
        }

#endregion Gửi tin thay dầu máy

#region Check ngày giờ Tân Việt

        private void Checkngaygiotanviet()
        {
            DataTable tableDatePart;

            if (CompanyInfo.idcongty == "29" || CompanyInfo.idcongty == "33")
            {
                _cmd.CommandText = "select datepart(hh,GETDATE()) as gio ,datepart(mi,GETDATE()) as phut";
                tableDatePart = datatabase.getData(_cmd);

                int gio = int.Parse(tableDatePart.Rows[0]["gio"].ToString());
                int phut = int.Parse(tableDatePart.Rows[0]["phut"].ToString());
                if (gio >= 18 && phut >= 10)
                {
                    _check = 1;
                }
                else { _check = 0; }
            }
        }

#endregion Check ngày giờ Tân Việt

#endregion Button Click

#region Lấy danh sách xe đang bảo dưỡng

        private void LayDanhSachXeDangBaoDuong()
        {
            _cmd.CommandText = @"select TenXe,IdBaoDuong,BienSo,Sokhung,SoMay,SoLan,TrangThai,CONVERT(bit,ThayDau) as ThayDau,GhiChu,
                                ThayDauMay,(select tenTho from dbo.ThoDichVu where IdTho=l.IdThoDuyet) as ThoDuyet,NhongXich,LamMay,l.NgayBaoDuong from dbo.LichSuBaoDuongXeTam l
                                where IdCongty=@IdCongty and CONVERT(nvarchar(25),NgayBaoDuong,126) like @NgayBaoDuong";
            // and IdCuaHang=@IdCuaHang
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            _cmd.Parameters.AddWithValue("@NgayBaoDuong", "%" + DateTime.Now.ToString("yyyy-MM-dd") + "%");
            //_cmd.Parameters.AddWithValue("@IdCuaHang", CompanyInfo.IdsCuaHang);

            _dtxeBaoDuong = datatabase.getData(_cmd);
            dataGridViewDanhSachXeDangBaoDuong.DataSource = _dtxeBaoDuong;

            for (int i = 0; i < _dtxeBaoDuong.Rows.Count; i++)
            {

                if (_dtxeBaoDuong.Rows[i]["GhiChu"].ToString().Contains("Đã xong"))
                {
                    DataGridViewCheckBoxCell cell = dataGridViewDanhSachXeDangBaoDuong.Rows[i].Cells["XeDaXong"] as DataGridViewCheckBoxCell;
                    cell.Value = true;
                    for (int j = 0; j < dataGridViewDanhSachXeDangBaoDuong.ColumnCount; j++)
                        dataGridViewDanhSachXeDangBaoDuong.Rows[i].Cells[j].Style.BackColor = Color.FromName("Orange");
                }

                if (_dtxeBaoDuong.Rows[i]["GhiChu"].ToString().Contains("Đã in phiếu. "))
                    for (int j = 0; j < dataGridViewDanhSachXeDangBaoDuong.ColumnCount; j++)
                        dataGridViewDanhSachXeDangBaoDuong.Rows[i].Cells[j].Style.BackColor = Color.FromName("Red");
            }
            dataGridViewDanhSachXeDangBaoDuong.Columns["GhiChu"].Visible = false;
        }

        private void LayDanhSachXeBaoDuongDaiHan()
        {
            _cmd.CommandText = @"select TenXe,IdBaoDuong,BienSo,Sokhung,SoMay,SoLan,TrangThai,CONVERT(bit,ThayDau) as ThayDau,GhiChu,  
                                ThayDauMay,(select tenTho from dbo.ThoDichVu where IdTho=l.IdThoDuyet) as ThoDuyet,NhongXich,LamMay,l.NgayBaoDuong from dbo.LichSuBaoDuongXeTam l  
                                where IdCongty=@IdCongty except select TenXe,IdBaoDuong,BienSo,Sokhung,SoMay,SoLan,TrangThai,CONVERT(bit,ThayDau) as ThayDau,GhiChu,   
                                ThayDauMay,(select tenTho from dbo.ThoDichVu where IdTho=l.IdThoDuyet) as ThoDuyet,NhongXich,LamMay,l.NgayBaoDuong from dbo.LichSuBaoDuongXeTam l   
                                where IdCongty=@IdCongty and CONVERT(nvarchar(25),NgayBaoDuong,126) like @NgayBaoDuong";
            // and IdCuaHang=@IdCuaHang
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            _cmd.Parameters.AddWithValue("@NgayBaoDuong","%"+DateTime.Now.ToString("yyyy-MM-dd")+"%");
            //_cmd.Parameters.AddWithValue("@IdCuaHang", CompanyInfo.IdsCuaHang);

            _dtxeBaoDuong = datatabase.getData(_cmd);
            dataGridViewXeBaoDuongDaiHan.DataSource = _dtxeBaoDuong;

            for (int i = 0; i < _dtxeBaoDuong.Rows.Count; i++)
            {

                if (_dtxeBaoDuong.Rows[i]["GhiChu"].ToString().Contains("Đã xong"))
                {
                    DataGridViewCheckBoxCell cell = dataGridViewXeBaoDuongDaiHan.Rows[i].Cells["XeDaXongXeDaiHan"] as DataGridViewCheckBoxCell;
                    cell.Value = true;
                    for (int j = 0; j < dataGridViewXeBaoDuongDaiHan.ColumnCount; j++)
                        dataGridViewXeBaoDuongDaiHan.Rows[i].Cells[j].Style.BackColor = Color.FromName("Orange");
                }

                if (_dtxeBaoDuong.Rows[i]["GhiChu"].ToString().Contains("Đã in phiếu. "))
                    for (int j = 0; j < dataGridViewXeBaoDuongDaiHan.ColumnCount; j++)
                        dataGridViewXeBaoDuongDaiHan.Rows[i].Cells[j].Style.BackColor = Color.FromName("Red");
            }
            dataGridViewXeBaoDuongDaiHan.Columns["GhiChu"].Visible = false;
        }

        private void LoadDangBaoDuong()
        {
            textBoxTimNhanhBienSo.Clear();
            textBoxTimNhanhSoKhung.Clear();
            textBoxTimNhanhSoMay.Clear();

            //XoaThongTinXeBaoDuong();
            if(superTabControlBaoDuong.SelectedTab == superTabItemXeDangBaoDuong)
            {
                LayDanhSachXeDangBaoDuong();
                LoadAutocomplete_TimKiemNhanh();
            }
            if(superTabControlBaoDuong.SelectedTab == superTabItemXeBaoDuongDaiHan)
            {
                LayDanhSachXeBaoDuongDaiHan();
                LoadAutocomplete_TimKiemNhanhXeBDDaiHan();
            }


            textBoxTimNhanhBienSo.Select();
        }

#endregion Lấy danh sách xe đang bảo dưỡng

#region Load Auto tìm kiếm nhanh

        private void LoadAutocomplete_TimKiemNhanh()
        {
            AutoCompleteStringCollection bienSo = new AutoCompleteStringCollection();
            AutoCompleteStringCollection soMay = new AutoCompleteStringCollection();
            AutoCompleteStringCollection soKhung = new AutoCompleteStringCollection();

            _tableBaoDuong = (DataTable)dataGridViewDanhSachXeDangBaoDuong.DataSource;

            string[] arrrayBienSo = _tableBaoDuong.Rows.OfType<DataRow>().Select(k => k["BienSo"].ToString()).ToArray();
            string[] arrraySoMay = _tableBaoDuong.Rows.OfType<DataRow>().Select(k => k["SoMay"].ToString()).ToArray();
            string[] arrraySoKhung = _tableBaoDuong.Rows.OfType<DataRow>().Select(k => k["Sokhung"].ToString()).ToArray();

            bienSo.AddRange(arrrayBienSo);
            soMay.AddRange(arrraySoMay);
            soKhung.AddRange(arrraySoKhung);

            textBoxTimNhanhBienSo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxTimNhanhBienSo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxTimNhanhBienSo.AutoCompleteCustomSource = bienSo;

            textBoxTimNhanhSoKhung.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxTimNhanhSoKhung.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxTimNhanhSoKhung.AutoCompleteCustomSource = soKhung;

            textBoxTimNhanhSoMay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxTimNhanhSoMay.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxTimNhanhSoMay.AutoCompleteCustomSource = soMay;
        }

        private void LoadAutocomplete_TimKiemNhanhXeBDDaiHan()
        {
            AutoCompleteStringCollection bienSo = new AutoCompleteStringCollection();
            AutoCompleteStringCollection soMay = new AutoCompleteStringCollection();
            AutoCompleteStringCollection soKhung = new AutoCompleteStringCollection();

            _tableBaoDuong = (DataTable)dataGridViewXeBaoDuongDaiHan.DataSource;

            string[] arrrayBienSo = _tableBaoDuong.Rows.OfType<DataRow>().Select(k => k["BienSo"].ToString()).ToArray();
            string[] arrraySoMay = _tableBaoDuong.Rows.OfType<DataRow>().Select(k => k["SoMay"].ToString()).ToArray();
            string[] arrraySoKhung = _tableBaoDuong.Rows.OfType<DataRow>().Select(k => k["Sokhung"].ToString()).ToArray();

            bienSo.AddRange(arrrayBienSo);
            soMay.AddRange(arrraySoMay);
            soKhung.AddRange(arrraySoKhung);

            textBoxTimNhanhBienSo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxTimNhanhBienSo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxTimNhanhBienSo.AutoCompleteCustomSource = bienSo;

            textBoxTimNhanhSoKhung.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxTimNhanhSoKhung.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxTimNhanhSoKhung.AutoCompleteCustomSource = soKhung;

            textBoxTimNhanhSoMay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxTimNhanhSoMay.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxTimNhanhSoMay.AutoCompleteCustomSource = soMay;
        }

#endregion Load Auto tìm kiếm nhanh

#region Xóa thông tin của xe bảo dưỡng

        private void XoaThongTinXeBaoDuong()
        {
            _idBd = "";
            _idLichSuBaoDuong = "";
            textBoxSoPhieu.Clear();
            textBoxTienCongTho.Clear();
            textBoxTienPhuTung.Clear();
            textBoxTienThueNgoai.Clear();
            textBoxChietKhau.Clear();
            textBoxTongTien.Clear();
            textBoxGhiChuBaoDuong.Clear();
            textBoxTenLapPhieu.Clear();
            comboBoxThoSuaChua.SelectedIndex = -1;

            dataGridViewPhuTungBaoDuong.DataSource = null;
            dataGridViewTheoTien.DataSource = null;
            dataGridViewTheoGio.DataSource = null;
            dataGridViewThueNgoai.DataSource = null;
        }

        #endregion Xóa thông tin của xe bảo dưỡng

        #region SqlDependency
        //public string m_connect = @"Data Source=125.212.201.52;Initial Catalog=Autocare_Sercurity;Persist Security Info=True;User ID=autocare;Password=autocare@2020;MultipleActiveResultSets=True";
        //delegate void UpdateData();

        //void LoadDataDependency()
        //{
        //    using (SqlConnection cn = new SqlConnection(m_connect))
        //    {
        //        if (cn.State == ConnectionState.Closed)
        //            cn.Open();
        //        SqlCommand cmd = new SqlCommand("select ls.IdBaoDuong from LichSuBaoDuongXeTam ls where IdCongty = "+ Class.CompanyInfo.idcongty.ToString(), cn);
        //        cmd.Notification = null;
        //        SqlDependency sqlDependency = new SqlDependency(cmd);
        //        sqlDependency.OnChange += new OnChangeEventHandler(OnDependencyChange);
        //        //DataTable dt = new DataTable("Dependency");

        //        //dt.Load(cmd.ExecuteReader());
        //        using (SqlDataReader reader = cmd.ExecuteReader())
        //        {
        //            // Process the DataReader.
        //        }
        //        cn.Close();
        //        //LoadDangBaoDuong();
        //        LayDanhSachXeDangBaoDuong();

        //    }
        //}
        //public void OnDependencyChange(object sender, SqlNotificationEventArgs e)
        //{
        //    SqlDependency sqlDependency = sender as SqlDependency;
        //    sqlDependency.OnChange -= OnDependencyChange;
        //    UpdateData updateData = new UpdateData(LoadDataDependency);
        //    this.Invoke(updateData, null);
        //    //MessageBox.Show("data changed");
        //    //LayDanhSachXeDangBaoDuong();
        //    //if (superTabControlBaoDuong.SelectedTab == superTabItemXeDangBaoDuong)
        //    //{
        //    //    LayDanhSachXeDangBaoDuong();
        //    //    LoadAutocomplete_TimKiemNhanh();
        //    //}

        //}
        HubConnection connection;
        #endregion
        #region superTabControlBaoDuong_SelectedTabChanged

        private async void superTabControlBaoDuong_SelectedTabChanged(object sender, DevComponents.DotNetBar.SuperTabStripSelectedTabChangedEventArgs e)
        {
            
            
            if (superTabControlBaoDuong.SelectedTab == superTabItemXeDangBaoDuong)
            {
                //Xóa thông tin xe bảo dưỡng
                XoaThongTinXeBaoDuong();
                
                //Lấy lại danh sách xe đang bảo dưỡng
                LoadDangBaoDuong();

                panelThongBaoLichSuBaoDuong.Visible = false;
                panelXeGiaoTrongNgay.Visible = false;
                panelTimKiemNhanh.Visible = true;

                textBoxTimNhanhBienSo.Clear();
                textBoxTimNhanhSoKhung.Clear();
                textBoxTimNhanhSoMay.Clear();

                textBoxTimNhanhBienSo.Focus();

                panelPhuTung.Enabled = true;
                dataGridViewPhuTungBaoDuong.ReadOnly = false;
                XoaPT.Visible = true;
                panelCongViec.Enabled = true;
            }
            if (superTabControlBaoDuong.SelectedTab == superTabItemLichSuBaoDuong)
            {
                
                //await connection.StopAsync();
                //Xóa thông tin xe bảo dưỡng
                XoaThongTinXeBaoDuong();

                panelTimKiemNhanh.Visible = false;
                panelXeGiaoTrongNgay.Visible = false;
                panelThongBaoLichSuBaoDuong.Visible = true;

                if (dataGridViewLichSuBaoDuong.DataSource != null && dataGridViewLichSuBaoDuong.Rows.Count > 0)
                {
                    if (dataGridViewLichSuBaoDuong.CurrentRow != null)
                        _idLichSuBaoDuong = dataGridViewLichSuBaoDuong.CurrentRow.Cells["IdBaoDuong"].Value.ToString();
                }
                else
                    _idLichSuBaoDuong = "";

                //LayPhuTungBaoDuong();
                //LayCongViecBaoDuong();

                panelPhuTung.Enabled = false;
                panelCongViec.Enabled = false;
            }
            if (superTabControlBaoDuong.SelectedTab == superTabItemXeDaGiaoTrongNgay)
            {
                //Xóa thông tin xe bảo dưỡng
                //await connection.StopAsync();
                XoaThongTinXeBaoDuong();

                panelTimKiemNhanh.Visible = false;
                panelThongBaoLichSuBaoDuong.Visible = false;
                panelXeGiaoTrongNgay.Visible = true;

                LayDanhSachXeDaGiaoTrongNgay();

                txtXeDaGiao_BienSo.Clear();
                txtXeDaGiao_SoKhung.Clear();
                txtXeDaGiao_SoMay.Clear();

                if (dataGridViewXeGiaoTrongNgay.DataSource != null && dataGridViewXeGiaoTrongNgay.Rows.Count > 0)
                {
                    if (dataGridViewXeGiaoTrongNgay.CurrentRow != null)
                        _idLichSuBaoDuong = dataGridViewXeGiaoTrongNgay.CurrentRow.Cells["TrongNgay_IDBaoDuong"].Value.ToString();
                }
                else
                    _idLichSuBaoDuong = "";

                //LayPhuTungBaoDuong();
                //LayCongViecBaoDuong();

                panelPhuTung.Enabled = true;
                dataGridViewPhuTungBaoDuong.ReadOnly = false;
                XoaPT.Visible = true;
            }
            if(superTabControlBaoDuong.SelectedTab == superTabItemXeBaoDuongDaiHan)
            {
                //await connection.StopAsync();
                //Xóa thông tin xe bảo dưỡng
                XoaThongTinXeBaoDuong();

                //Lấy lại danh sách xe đang bảo dưỡng
                textBoxTimNhanhBienSo.Clear();
                textBoxTimNhanhSoKhung.Clear();
                textBoxTimNhanhSoMay.Clear();

                //XoaThongTinXeBaoDuong();

                LayDanhSachXeBaoDuongDaiHan();
                LoadAutocomplete_TimKiemNhanhXeBDDaiHan();

                textBoxTimNhanhBienSo.Select();

                panelThongBaoLichSuBaoDuong.Visible = false;
                panelXeGiaoTrongNgay.Visible = false;
                panelTimKiemNhanh.Visible = true;

                textBoxTimNhanhBienSo.Clear();
                textBoxTimNhanhSoKhung.Clear();
                textBoxTimNhanhSoMay.Clear();

                textBoxTimNhanhBienSo.Focus();

                panelPhuTung.Enabled = true;
                dataGridViewPhuTungBaoDuong.ReadOnly = false;
                XoaPT.Visible = true;
                panelCongViec.Enabled = true;
            }
        }

#region Lấy danh sách xe đã giao trong ngày

        private void LayDanhSachXeDaGiaoTrongNgay()
        {
            _cmd.CommandText = @"select Row_Number() over(order by lsbdx.IdBaoDuong desc) as 'STT' ,lsbdp.SoPhieu, lsbdx.TenXe, lsbdx.BienSo, lsbdx.Sokhung, lsbdx.SoMay, lsbdx.NgayBaoDuong,
                                lsbdx.NgayGiaoXe, lsbdx.SoLan, CONVERT(bit, lsbdx.ThayDau) as ThayDau,lsbdx.ThayDauMay, cdx.Khac as 'ChuanDoan',
                                lsbdx.YeuCauKH, lsbdp.TongTien, tdv.MaTho as MaThoDuyet, tdv.TenTho as TenThoDuyet, lsbdx.IdBaoDuong,
                                lsbdx.GhiChu, 'KyThuatVien' = case when ktv.KyThuatVien is not null then ktv.KyThuatVien
						        when (select top 1 b.TenTho from dbo.ThoDichVu_TienCongTho2 a inner join dbo.ThoDichVu b on a.IdTho = b.IdTho and a.IdBaoDuong = lsbdx.IdBaoDuong) is not null
                                then (select top 1 b.TenTho from dbo.ThoDichVu_TienCongTho2 a inner join dbo.ThoDichVu b on a.IdTho = b.IdTho and a.IdBaoDuong = lsbdx.IdBaoDuong)
                                else (select top 1 b.TenTho from dbo.ThueNgoai a inner join dbo.ThoDichVu b on a.IdTho = b.IdTho and a.IdBaoDuong = lsbdx.IdBaoDuong)
						        end, isnull(isPrinted,0) as 'isPrinted' from dbo.LichSuBaoDuongXe lsbdx left join dbo.KyThuatVien_BaoDuong ktv on ktv.IdBaoDuong = lsbdx.IdBaoDuong
                                left outer join dbo.LichSuBaoDuongPhieu lsbdp on lsbdx.IdBaoDuong=lsbdp.idBaoDuong
                                left outer join dbo.ChuanDoanXe cdx on lsbdx.IdBaoDuong=cdx.IdBaoDuong
                                left outer join dbo.ThoDichVu tdv on lsbdx.IdThoDuyet=tdv.IdTho
                                left outer join dbo.KhachHang kh on lsbdx.IdKhachHang=kh.IdKhachHang
                                Where lsbdx.IdCongTy=@IdCongTy and lsbdx.NgayGiaoXe between @NgayGiaoXeTu and @NgayGiaoXeDen order by lsbdx.NgayGiaoXe desc";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
            _cmd.Parameters.AddWithValue("@NgayGiaoXeTu", DateTime.Now.ToString("yyyyMMdd"));
            _cmd.Parameters.AddWithValue("@NgayGiaoXeDen", DateTime.Now.ToString("yyyyMMdd") + " 23:59:59");

            _tableXeDaGiao = datatabase.getData(_cmd);
            dataGridViewXeGiaoTrongNgay.DataSource = _tableXeDaGiao;

            for (int i = 0; i < _tableXeDaGiao.Rows.Count; i++)
            {
                if (!Convert.ToBoolean(_tableXeDaGiao.Rows[i]["isPrinted"].ToString()))
                {
                    for (int j = 0; j < dataGridViewXeGiaoTrongNgay.ColumnCount; j++)
                        dataGridViewXeGiaoTrongNgay.Rows[i].Cells[j].Style.BackColor = Color.FromName("Yellow");
                }
            }

            AutoCompleteStringCollection bienSo = new AutoCompleteStringCollection();
            AutoCompleteStringCollection soMay = new AutoCompleteStringCollection();
            AutoCompleteStringCollection soKhung = new AutoCompleteStringCollection();

            string[] arrrayBienSo = _tableXeDaGiao.Rows.OfType<DataRow>().Select(k => k["BienSo"].ToString()).ToArray();
            string[] arrraySoMay = _tableXeDaGiao.Rows.OfType<DataRow>().Select(k => k["SoMay"].ToString()).ToArray();
            string[] arrraySoKhung = _tableXeDaGiao.Rows.OfType<DataRow>().Select(k => k["Sokhung"].ToString()).ToArray();

            bienSo.AddRange(arrrayBienSo);
            soMay.AddRange(arrraySoMay);
            soKhung.AddRange(arrraySoKhung);

            txtXeDaGiao_BienSo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtXeDaGiao_BienSo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtXeDaGiao_BienSo.AutoCompleteCustomSource = bienSo;

            txtXeDaGiao_SoKhung.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtXeDaGiao_SoKhung.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtXeDaGiao_SoKhung.AutoCompleteCustomSource = soKhung;

            txtXeDaGiao_SoMay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtXeDaGiao_SoMay.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtXeDaGiao_SoMay.AutoCompleteCustomSource = soMay;

            txtXeDaGiao_BienSo.Focus();
            txtXeDaGiao_BienSo.Select();
        }

#endregion Lấy danh sách xe đã giao trong ngày

#endregion superTabControlBaoDuong_SelectedTabChanged

#region Đẩy dữ liệu vào gridview dùng co Autocomplete

        private void AddAutocompleDataGridview()
        {
            _gridviewAutocompleteSoMay.RowHeadersVisible = false;
            _gridviewAutocompleteSoMay.BorderStyle = BorderStyle.Fixed3D;
            _gridviewAutocompleteSoMay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _gridviewAutocompleteSoMay.AllowUserToAddRows = false;
            _gridviewAutocompleteSoMay.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _gridviewAutocompleteSoMay.Width = 400;
            _gridviewAutocompleteSoMay.Height = 110;
            _gridviewAutocompleteSoMay.Location = new Point(buttonTimKiem.Location.X - textBoxTimKiemSoMay.Width - 5, buttonTimKiem.Location.Y + textBoxTimKiemSoMay.Height + 5);

            _gridviewAutocompleteSoKhung.RowHeadersVisible = false;
            _gridviewAutocompleteSoKhung.BorderStyle = BorderStyle.Fixed3D;
            _gridviewAutocompleteSoKhung.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            _gridviewAutocompleteSoKhung.AllowUserToAddRows = false;
            _gridviewAutocompleteSoKhung.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            _gridviewAutocompleteSoKhung.Width = 400;
            _gridviewAutocompleteSoKhung.Height = 110;
            _gridviewAutocompleteSoKhung.Location = new Point(buttonTimKiem.Location.X - textBoxTimKiemSoKhung.Width * 2 - 60, buttonTimKiem.Location.Y + textBoxTimKiemSoKhung.Height + 5);

            Controls.Add(_gridviewAutocompleteSoMay);
            Controls.Add(_gridviewAutocompleteSoKhung);
        }

#endregion Đẩy dữ liệu vào gridview dùng co Autocomplete

#region Đẩy dữ liệu Autocomplete biển số

        private void AddAutoBienSo()
        {
            AutoCompleteStringCollection bienSo = new AutoCompleteStringCollection();

            SqlCommand cmd = new SqlCommand();
            cmd.Connection = datatabase.getConnection();
            cmd.CommandType = CommandType.Text;

            //Bao dưỡng dịch vụ
            cmd.CommandText = "select distinct BienSo from dbo.LichSuBaoDuongXe where IdCongty=" + CompanyInfo.idcongty;
            cmd.Connection.Open();
            var dtBienSo = datatabase.getData(cmd);
            cmd.Connection.Close();

            string[] arrray = dtBienSo.Rows.OfType<DataRow>().Select(k => k[0].ToString()).ToArray();
            bienSo.AddRange(arrray);

            textBoxTimKiemBienSo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            textBoxTimKiemBienSo.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBoxTimKiemBienSo.AutoCompleteCustomSource = bienSo;
        }

#endregion Đẩy dữ liệu Autocomplete biển số

#region Xóa dữ liệu tìm kiếm

        private void Clear_TimKiem()
        {
            textBoxTimKiemBienSo.Clear();
            textBoxTimKiemSoDienThoai.Clear();
            textBoxTimKiemSoKhung.Clear();
            textBoxTimKiemSoMay.Clear();

            _gridviewAutocompleteSoMay.DataSource = null;
            _gridviewAutocompleteSoKhung.DataSource = null;
        }

#endregion Xóa dữ liệu tìm kiếm

#region Chọn loại bảo dưỡng

        private void comboBoxLoaiBaoDuong_SelectedValueChanged(object sender, EventArgs e)
        {
            //Clear text
            Clear_TimKiem();
            XoaThongTinKhachHang();

            if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDinhKy)
                panelTimKiemBaoDuongDichVu.Visible = false;
            if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDichVu)
                panelTimKiemBaoDuongDichVu.Visible = true;
        }

#endregion Chọn loại bảo dưỡng

#region Load danh sách Số khung và Số máy

        private void Load_SoKhung_SoMay()
        {
            //_cmd.CommandText = "SELECT distinct TenXe, SoKhung, SoMay FROM dbo.XeDaBan WHERE IdCongTy = @IdCongTy";
            _cmd.CommandText = "SELECT TenXe, SoKhung, SoMay FROM dbo.XeDaBan WHERE IdCongTy = @IdCongTy";
            _cmd.Parameters.Clear();
            _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);

            _tableSoKhungSoMay = datatabase.getData(_cmd);
        }

#endregion Load danh sách Số khung và Số máy

#region Autocomplete số máy

        private void AutocompleteSoMay()
        {
            if (_tableSoKhungSoMay.Rows.Count > 0)
            {
                var result = _tableSoKhungSoMay.Clone();

                var view = new DataView(_tableSoKhungSoMay) { Sort = "SoMay" };

                foreach (DataRow row in _tableSoKhungSoMay.Rows)
                {
                    if (row["SoMay"].ToString().Contains(textBoxTimKiemSoMay.Text))
                        result.ImportRow(row);
                }

                //var result = _tableSoKhungSoMay.Select("SoMay like '%"+ textBoxTimKiemSoMay.Text + "%'");

                //_gridviewAutocompleteSoMay.DataSource = result.Any() ? result.CopyToDataTable() : null;

                _gridviewAutocompleteSoMay.DataSource = result;
            }
            else
                _gridviewAutocompleteSoMay.DataSource = null;
        }

#endregion Autocomplete số máy

#region Autocomplete số khung

        private void AutocompleteSoKhung()
        {
            if (_tableSoKhungSoMay.Rows.Count > 0)
            {
                var result = _tableSoKhungSoMay.Clone();

                var view = new DataView(_tableSoKhungSoMay) { Sort = "SoKhung" };

                foreach (DataRow row in _tableSoKhungSoMay.Rows)
                {
                    if (row["SoKhung"].ToString().Contains(textBoxTimKiemSoKhung.Text))
                        result.ImportRow(row);
                }

                //var result = _tableSoKhungSoMay.Select("SoKhung like '%" + textBoxTimKiemSoKhung.Text + "%'");

                //_gridviewAutocompleteSoKhung.DataSource = result.Any() ? result.CopyToDataTable() : null;

                _gridviewAutocompleteSoKhung.DataSource = result;
            }
            else
                _gridviewAutocompleteSoKhung.DataSource = null;
        }

#endregion Autocomplete số khung

#region Gridview MoveDown

        private void MoveDown(DataGridView dgv)
        {
            try
            {
                if (dgv.CurrentRow == null) return;
                if (dgv.CurrentRow.Index + 1 <= dgv.Rows.Count - 1)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentRow.Index + 1].Cells[0];
                    dgv.Rows[dgv.CurrentCell.RowIndex].Selected = true;
                }
            }
            catch
            {
                //
            }
        }

#endregion Gridview MoveDown

#region Gridview MoveUp

        private static void MoveUp(DataGridView dgv)
        {
            try
            {
                if (dgv.CurrentRow == null) return;
                if (dgv.CurrentRow.Index - 1 >= 0)
                {
                    dgv.CurrentCell = dgv.Rows[dgv.CurrentRow.Index - 1].Cells[0];
                    dgv.Rows[dgv.CurrentCell.RowIndex].Selected = true;
                }
            }
            catch
            {
                //
            }
        }

#endregion Gridview MoveUp


#region Nút Tiếp nhận phụ tùng
        private void BtnTiepNhanPhuTung_Click(object sender, EventArgs e)
        {
            if (idBaoDuongTam.Length < 1)
            {
                MessageBox.Show("Chưa chọn xe\nHãy chọn xe", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            try
            {
                btnTiepNhanPhuTung.Enabled = false;

                frmQuanLyPhuTungDaGoi frmQuanLyPhuTungOrder = new frmQuanLyPhuTungDaGoi();
                frmQuanLyPhuTungOrder.CallFromUcBaoDuong = new frmQuanLyPhuTungDaGoi.LoadDanhSachPhuTung(LayPhuTungBaoDuong);
                frmQuanLyPhuTungOrder.IdBaoDuong = idBaoDuongTam;
                frmQuanLyPhuTungOrder.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { btnTiepNhanPhuTung.Enabled = true; }
        }
#endregion Nút Tiếp nhận phụ tùng
#region Nút kho tiếp nhận phụ tùng
        private void BtnKhoTiepNhanPhuTung_Click(object sender, EventArgs e)
        {
            try
            {
                //btnKhoTiepNhanPhuTung.Enabled = false;
                frmKhoTiepNhanPhuTung frmKhoTiepNhanPhuTung = new frmKhoTiepNhanPhuTung();
                frmKhoTiepNhanPhuTung.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally { /*btnKhoTiepNhanPhuTung.Enabled = true; */}
        }
#endregion Nút kho tiếp nhận phụ tùng
        private void ButtonHoanTat_Click_1(object sender, EventArgs e)
        {

        }

        private void BtnLayPhieuBaoDuongDinhKy_Click(object sender, EventArgs e)
        {

            if (idBaoDuongTam.Length < 1)
            {
                MessageBox.Show("Chưa chọn xe\nHãy chọn xe", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnLayPhieuBaoDuongDinhKy.Enabled = false;
            try
            {
                if(int.Parse(Class.CompanyInfo.idcongty) == 94)
                {
                    frmPhieuBaoDuongDinhKyVietLong2 frm = new frmPhieuBaoDuongDinhKyVietLong2();
                    frm.idBaoDuongTamThoi = idBaoDuongTam;
                    frm.ShowDialog();
                }
                else if (int.Parse(Class.CompanyInfo.idcongty) == 95)
                {
                    frmPhieuBaoDuongDinhKyVietLong3 frm = new frmPhieuBaoDuongDinhKyVietLong3();
                    frm.idBaoDuongTamThoi = idBaoDuongTam;
                    frm.ShowDialog();
                }
                else
                {
                    frmPhieuBaoDuongDinhKy frm = new frmPhieuBaoDuongDinhKy();
                    frm.idBaoDuongTamThoi = idBaoDuongTam;
                    frm.ShowDialog();
                }
            }
            catch
            {

            }
            finally { btnLayPhieuBaoDuongDinhKy.Enabled = true; }
        }

        private void BtnLayPhieuSuaChua_Click(object sender, EventArgs e)
        {
            if (idBaoDuongTam.Length < 1)
            {
                MessageBox.Show("Chưa chọn xe\nHãy chọn xe", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            btnLayPhieuSuaChua.Enabled = false;
            try
            {
               
                if (int.Parse(Class.CompanyInfo.idcongty) == 94)
                {
                    frmPhieuSuaChuaVietLong2 frm = new frmPhieuSuaChuaVietLong2();
                    frm.idBaoDuongTamThoi = idBaoDuongTam;
                    frm.ThoiGianDuKien = txtTgdutinh.Text;
                    frm.ShowDialog();
                }
                else if (int.Parse(Class.CompanyInfo.idcongty) == 95)
                {
                    frmPhieuSuaChuaVietLong3 frm = new frmPhieuSuaChuaVietLong3();
                    frm.idBaoDuongTamThoi = idBaoDuongTam;
                    frm.ThoiGianDuKien = txtTgdutinh.Text;
                    frm.ShowDialog();
                }
                else
                {
                    frmPhieuSuaChuaHondaVietLong frm = new frmPhieuSuaChuaHondaVietLong();
                    frm.idBaoDuongTamThoi = idBaoDuongTam;
                    frm.ThoiGianDuKien = txtTgdutinh.Text;
                    frm.ShowDialog();
                }
            }
            catch
            {

            }
            finally { btnLayPhieuSuaChua.Enabled = true; }
            
        }

        private void DataGridViewXeBaoDuongDaiHan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                {

                    //Tích chọn thay dầu máy
                    if (e.ColumnIndex == dataGridViewXeBaoDuongDaiHan.Columns["ThayDauXeDaiHan"].Index)
                    {
                        DataGridViewCheckBoxCell cell = dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["ThayDauXeDaiHan"] as DataGridViewCheckBoxCell;
                        if (String.IsNullOrEmpty(cell.Value.ToString()))
                            cell.Value = true;
                        else if ((bool)cell.Value == false)
                            cell.Value = true;
                        else
                            cell.Value = false;
                        //Cập nhật trạng thái thay dầu
                        _cmd.CommandText = @"UPDATE LichSuBaoDuongXeTam SET ThayDau = @ThayDau
                                            WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@ThayDau", cell.Value);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["IdBaoDuongDaiHan"].Value);
                        datatabase.ExcuteNonQuery(_cmd);
                    }
                    //Tích chọn trạng thái
                    if (e.ColumnIndex == dataGridViewXeBaoDuongDaiHan.Columns["TrangThaiBDXeDaiHan"].Index)
                    {
                        DataGridViewCheckBoxCell cell = dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["TrangThaiBDXeDaiHan"] as DataGridViewCheckBoxCell;

                        if (String.IsNullOrEmpty(cell.Value.ToString()))
                            cell.Value = true;
                        else if ((bool)cell.Value == false)
                            cell.Value = true;
                        else
                            cell.Value = false;

                        //Cập nhật trạng thái
                        _cmd.CommandText = @"UPDATE LichSuBaoDuongXeTam
                                            SET TrangThai = @TrangThai
                                            WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@TrangThai", cell.Value);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["IdBaoDuongDaiHan"].Value);

                        datatabase.ExcuteNonQuery(_cmd);
                    }

                    //Tích chọn thay dầu máy
                    if (e.ColumnIndex == dataGridViewXeBaoDuongDaiHan.Columns["ThayDauMayXeDaiHan"].Index)
                    {
                        DataGridViewCheckBoxCell cell = dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["ThayDauMayXeDaiHan"] as DataGridViewCheckBoxCell;

                        if (String.IsNullOrEmpty(cell.Value.ToString()))
                            cell.Value = true;
                        else if ((bool)cell.Value == false)
                            cell.Value = true;
                        else
                            cell.Value = false;

                        //Cập nhật trạng thái thay dầu máy
                        _cmd.CommandText = @"UPDATE LichSuBaoDuongXeTam
                                            SET ThayDauMay = @ThayDauMay
                                            WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@ThayDauMay", cell.Value);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["IdBaoDuongDaiHan"].Value);

                        datatabase.ExcuteNonQuery(_cmd);
                    }

                    //Tích chọn nhông xích
                    if (e.ColumnIndex == dataGridViewXeBaoDuongDaiHan.Columns["NhongXichXeDaiHan"].Index)
                    {
                        DataGridViewCheckBoxCell cell = dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["NhongXichXeDaiHan"] as DataGridViewCheckBoxCell;

                        if (String.IsNullOrEmpty(cell.Value.ToString()))
                            cell.Value = true;
                        else if ((bool)cell.Value == false)
                            cell.Value = true;
                        else
                            cell.Value = false;

                        //Cập nhật trạng thái thay dầu máy
                        _cmd.CommandText = @"UPDATE LichSuBaoDuongXeTam
                                            SET NhongXich = @NhongXich
                                            WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@NhongXich", cell.Value);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["IdBaoDuongDaiHan"].Value);

                        datatabase.ExcuteNonQuery(_cmd);
                    }

                    //Tích chọn làm máy
                    if (e.ColumnIndex == dataGridViewXeBaoDuongDaiHan.Columns["LamMayXeDaiHan"].Index)
                    {
                        DataGridViewCheckBoxCell cell = dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["LamMayXeDaiHan"] as DataGridViewCheckBoxCell;

                        if (String.IsNullOrEmpty(cell.Value.ToString()))
                            cell.Value = true;
                        else if ((bool)cell.Value == false)
                            cell.Value = true;
                        else
                            cell.Value = false;

                        //Cập nhật trạng thái thay dầu máy
                        _cmd.CommandText = @"UPDATE LichSuBaoDuongXeTam
                                            SET LamMay = @LamMay
                                            WHERE IdBaoDuong = @IdBaoDuong";
                        _cmd.Parameters.Clear();
                        _cmd.Parameters.AddWithValue("@LamMay", cell.Value);
                        _cmd.Parameters.AddWithValue("@IdBaoDuong", dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["IdBaoDuongDaiHan"].Value);

                        datatabase.ExcuteNonQuery(_cmd);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DataGridViewXeBaoDuongDaiHan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0)
            {
                _idBd = Convert.ToString(dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["IdBaoDuongDaiHan"].Value);
                idBaoDuongTam = _idBd;
                textBoxTimNhanhBienSo.Text = Convert.ToString(dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["BienSoXeDaiHan"].Value);
                textBoxTimNhanhSoKhung.Text = Convert.ToString(dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["SoKhungXeDaiDan"].Value);
                textBoxTimNhanhSoMay.Text = Convert.ToString(dataGridViewXeBaoDuongDaiHan.Rows[e.RowIndex].Cells["SoMayXeDaiHan"].Value);
                if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDichVu)
                {
                    _cmd.CommandText =
                        @"select ls.IdBaoDuong, kh.NgaySinh, kh.HoKH, kh.TenKH, kh.GioiTinh, ls.TenXe, kh.DienThoai,
                                    kh.Diachi, xb.NgayBan, kh.KhachDenTu, ls.BienSo, ls.Sokhung, ls.SoMay,
                                    ls.NgayBaoDuong, ls.SoLan, ls.SoKm, ls.LoaiBaoDuong, ls.IdKhachHang, xb.IdXeDaBan,ls.TGDUKIEN,ls.TTBAODUONG,
                                    ls.KYTHUATVIEN,ls.BANNANG,ls.GhiChu    
                                    from LichSuBaoDuongXeTam ls
                                    inner join KhachHang kh on ls.IdKhachHang = kh.IdKhachHang
                                    left join XeDaBan xb on xb.IdKhachHang = kh.IdKhachHang
                                    where ls.IdCongty = @IdCongTy and ls.IdBaoDuong = @IdBaoDuong";
                }
                if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDinhKy)
                {
                    _cmd.CommandText =
                        @"select ls.IdBaoDuong, kh.NgaySinh, kh.HoKH, kh.TenKH, kh.GioiTinh, ls.TenXe, kh.DienThoai,
                                    kh.Diachi, xb.NgayBan, kh.KhachDenTu, ls.BienSo, ls.Sokhung, ls.SoMay,
                                    ls.NgayBaoDuong, ls.SoLan, ls.SoKm, ls.LoaiBaoDuong, ls.IdKhachHang, xb.IdXeDaBan,ls.TGDUKIEN,ls.TTBAODUONG,ls.KYTHUATVIEN,ls.BANNANG,ls.GhiChu    
                                    from LichSuBaoDuongXeTam ls
                                    inner join KhachHang kh on ls.IdKhachHang = kh.IdKhachHang
                                    left join XeDaBan xb on xb.IdKhachHang = kh.IdKhachHang
                                    where ls.IdCongty = @IdCongTy and ls.IdBaoDuong = @IdBaoDuong";
                }
               
                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                _cmd.Parameters.AddWithValue("@IdBaoDuong", _idBd);
                if (comboBoxTimKiemLoaiBaoDuong.SelectedItem == comboItemBaoDuongDichVu)
                    _cmd.Parameters.AddWithValue("@BienSo", textBoxTimNhanhBienSo.Text.Trim());
                else
                    _cmd.Parameters.AddWithValue("@BienSo", "");
                _cmd.Parameters.AddWithValue("@SoKhung", textBoxTimNhanhSoKhung.Text.Trim());
                _cmd.Parameters.AddWithValue("@SoMay", textBoxTimNhanhSoMay.Text.Trim());

                DataTable infoid = datatabase.getData(_cmd);
                if (infoid.Rows.Count > 0)
                {
                    dateTimeInputNgaySinh.Text = _ngaysinh = infoid.Rows[0]["NgaySinh"].ToString();
                    _tenkh = infoid.Rows[0]["TenKH"].ToString();
                    textBoxTenKH.Text = infoid.Rows[0]["HoKH"].ToString() + " " + infoid.Rows[0]["TenKH"].ToString();
                    comboBoxGioiTinh.Text = infoid.Rows[0]["GioiTinh"].ToString();
                    textBoxTenXe.Text = _tenxe = infoid.Rows[0]["TenXe"].ToString();
                    textBoxDienThoai.Text = _dienthoai = infoid.Rows[0]["DienThoai"].ToString();
                    textBoxDiaChi.Text = infoid.Rows[0]["DiaChi"].ToString();
                    dateTimeInputNgayMua.Text = infoid.Rows[0]["NgayBan"].ToString();
                    comboBoxKhachDenTu.Text = infoid.Rows[0]["KhachDenTu"].ToString();
                    textBoxBienSo.Text = infoid.Rows[0]["BienSo"].ToString();
                    textBoxSoKhung.Text = infoid.Rows[0]["SoKhung"].ToString();
                    textBoxSoMay.Text = infoid.Rows[0]["SoMay"].ToString();
                    dateTimeInputNgayVao.Text = infoid.Rows[0]["NgayBaoDuong"].ToString();
                    dateTimeInputGioVao.Text = infoid.Rows[0]["NgayBaoDuong"].ToString();
                    dateTimeInputNgayRa.Value = DateTime.Now;
                    dateTimeInputGioRa.Value = DateTime.Now;
                    textBoxLanBaoDuong.Text = _solan = infoid.Rows[0]["SoLan"].ToString();
                    textBoxSoKm.Text = infoid.Rows[0]["SoKm"].ToString();
                    comboBoxLoaiBaoDuong.SelectedValue = infoid.Rows[0]["LoaiBaoDuong"].ToString();
                    _idKhachHang = infoid.Rows[0]["IdKhachHang"].ToString();
                    _idXe = infoid.Rows[0]["IdXeDaBan"].ToString();
                    _idBaoDuongTam = infoid.Rows[0]["IdBaoDuong"].ToString();
                    cbtrangthai.SelectedValue = infoid.Rows[0]["TTBAODUONG"].ToString();
                    txtTgdutinh.Text = infoid.Rows[0]["TGDUKIEN"].ToString();
                    txtbannang.Text = infoid.Rows[0]["BANNANG"].ToString();
                    textBoxGhiChuBaoDuong.Text = infoid.Rows[0]["GhiChu"].ToString();
                    if (infoid.Rows[0]["KYTHUATVIEN"].ToString() != "")
                        txtkythuat.SelectedValue = infoid.Rows[0]["KYTHUATVIEN"].ToString();
                    txbTenKhDiBaoDuong.Text = textBoxTenKH.Text;
                    txbDienThoaiKHDiBaoDuong.Text = textBoxDienThoai.Text;
                    txbDiaChiKHDiBaoDuong.Text = textBoxDiaChi.Text;
                }

                LayPhuTungBaoDuong();
                LayCongViecBaoDuong();

            }
        }

        private void DataGridViewPhuTungBaoDuong_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex == -1)
            {
                return;
            }
            txbSuaLaiGiaTien.Text = dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["DonGia"].Value.ToString().Trim();
            DataTable dttsc = (DataTable)comboBoxThoSuaChua.DataSource;
            for(int i = 0; i < dttsc.Rows.Count; i++)
            {
                if (int.Parse(dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["comboBoxTho_PT"].Value.ToString()) == int.Parse(dttsc.Rows[i]["IdTho"].ToString().Trim()))
                {
                    comboBoxThoSuaChua.SelectedIndex = i;
                }
            }
            __idBDChangePT = dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["IdBaoDuong_PT"].Value.ToString();
            __idPTChangePT = dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["comboBoxMaPT"].Value.ToString();
            __soLuongChange = dataGridViewPhuTungBaoDuong.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();
        }

        String __idBDChangePT = "";
        String __idPTChangePT = "";
        String __soLuongChange = "";
        private void BtnLuuThoSuaChuaPhuTung_Click(object sender, EventArgs e)
        {
            if(__idPTChangePT.Length < 1 && __idBDChangePT.Length < 1)
            {
                MessageBox.Show("Bạn chưa chọn phụ tùng cần đổi tên thợ", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"UPDATE LichSuBaoDuongChiTietTam2 Set idTho = @idTho where IdBaoDuong = @idbaoduong and IdPhuTung = @idphutung";
            cmd.Parameters.AddWithValue("@idTho", comboBoxThoSuaChua.SelectedValue);
            cmd.Parameters.AddWithValue("@idbaoduong", int.Parse(__idBDChangePT));
            cmd.Parameters.AddWithValue("@idphutung", int.Parse(__idPTChangePT));
            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
            {
                MessageBox.Show("Lưu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                __idBDChangePT = "";
                __idPTChangePT = "";
                LayPhuTungBaoDuong();
            }
            else
            {
                MessageBox.Show("Lưu thất bại", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

     

        private void BtnLuuLaiSoTienCuaPhuTung_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = @"update dbo.LichSuBaoDuongChiTietTam2 set SoLuong = @soluong,Gia = @gia, GiaTien = @giatien WHERE IdPhuTung = @idpt AND IdBaoDuong = @IdBaoDuong";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@soluong", int.Parse(__soLuongChange));
            cmd.Parameters.AddWithValue("@gia", Convert.ToSingle(txbSuaLaiGiaTien.Text));
            cmd.Parameters.AddWithValue("@giatien", Convert.ToSingle(txbSuaLaiGiaTien.Text) * int.Parse(__soLuongChange));
            cmd.Parameters.AddWithValue("@idpt", int.Parse(__idPTChangePT));
            cmd.Parameters.AddWithValue("@IdBaoDuong", int.Parse(__idBDChangePT));
            if (Class.datatabase.ExcuteNonQuery(cmd) > 0)
            {
                MessageBox.Show("Update thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LayPhuTungBaoDuong();
            }
            else
            {
                MessageBox.Show("Update không thành công, Hãy làm lại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void TextBoxTenKH_TextChanged(object sender, EventArgs e)
        {
            txbTenKhDiBaoDuong.Text = textBoxTenKH.Text;
        }

        private void TextBoxDienThoai_TextChanged(object sender, EventArgs e)
        {
            txbDienThoaiKHDiBaoDuong.Text = textBoxDienThoai.Text;
        }

        private void TextBoxDiaChi_TextChanged(object sender, EventArgs e)
        {
            txbDiaChiKHDiBaoDuong.Text = textBoxDiaChi.Text;
        }
        private decimal tien;
        private void TxbSuaLaiGiaTien_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(txbSuaLaiGiaTien.Text))
                {
                    tien = 0;
                }
                else
                {
                    tien = Convert.ToDecimal(txbSuaLaiGiaTien.Text);
                }
            }
            catch { MessageBox.Show("Đơn giá phải là kiểu số!", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            txbSuaLaiGiaTien.Text = tien.ToString("0,0");
            txbSuaLaiGiaTien.SelectionStart = txbSuaLaiGiaTien.Text.Length;
        }

        private void ButtonCapNhatKhachHang_Click_1(object sender, EventArgs e)
        {

        }

        private void buttonThemXeBaoDuong_Click_1(object sender, EventArgs e)
        {

        }
    }
}