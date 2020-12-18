using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class FrmTimKiemLichSuBaoDuong : Form
    {
        #region Delegate

        public delegate void GetDataSearch(DataTable resultDataTable);

        private GetDataSearch _sendBackDataSearch;
        #endregion

        #region Data autocomplete data

        private readonly AutoCompleteStringCollection _bienSo;
        private readonly AutoCompleteStringCollection _soMay;
        private readonly AutoCompleteStringCollection _soKhung;
        private readonly DataTable _tableBaoDuobg;
        #endregion

        public FrmTimKiemLichSuBaoDuong(AutoCompleteStringCollection bienSo, AutoCompleteStringCollection soMay, AutoCompleteStringCollection soKhung, GetDataSearch sendBackDataSearch, DataTable tableBaoDuobg)
        {
            _bienSo = bienSo;
            _soMay = soMay;
            _soKhung = soKhung;
            _sendBackDataSearch = sendBackDataSearch;
            _tableBaoDuobg = tableBaoDuobg;

            InitializeComponent();
        }

        private void frmTimKiemLichSuBaoDuong_Load(object sender, EventArgs e)
        {
            txtBienSoXe.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtBienSoXe.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtBienSoXe.AutoCompleteCustomSource = _bienSo;

            txtSoKhung.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSoKhung.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSoKhung.AutoCompleteCustomSource = _soKhung;

            txtSoMay.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            txtSoMay.AutoCompleteSource = AutoCompleteSource.CustomSource;
            txtSoMay.AutoCompleteCustomSource = _soMay;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            SearchOnDataGridview();
        }

        private void SearchOnDataGridview()
        {
            var searchBienSoValue = txtBienSoXe.Text;
            var searchSoKhungValue = txtSoKhung.Text;
            var searchSoMayValue = txtSoMay.Text;

            try
            {
                var results = from myRow in _tableBaoDuobg.AsEnumerable()
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

                var tableResult = results.CopyToDataTable();

                if (_sendBackDataSearch != null)
                    _sendBackDataSearch(tableResult);
            }
            catch
            {
                MessageBox.Show(@"Không tồn tại xe bảo dưỡng!", @"Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void txtBienSoXe_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
                btnTimKiem_Click(sender, new EventArgs());
        }

        private void txtSoMay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTimKiem_Click(sender, new EventArgs());
        }

        private void txtSoKhung_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnTimKiem_Click(sender, new EventArgs());
        }

        private void FrmTimKiemLichSuBaoDuong_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                btnThoat_Click(sender, new EventArgs());
        }
    }
}
