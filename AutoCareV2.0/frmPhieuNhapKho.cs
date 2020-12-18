using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Windows.Forms;
using AutoCareV2._0.Class;
using Microsoft.Reporting.WinForms;

namespace AutoCareV2._0
{
    public partial class FrmPhieuNhapKho : Form
    {
        private readonly string _idNhaCungCap;
        private readonly string _idKey;
        private readonly DocTien _docTien = new DocTien();
        private SqlCommand _cmd;

        public FrmPhieuNhapKho(string idNhaCungCap, string idKey)
        {
            _idNhaCungCap = idNhaCungCap;
            _idKey = idKey;

            InitializeComponent();
        }

        private void frmPhieuNhapKho_Load(object sender, EventArgs e)
        {
            try
            {
                _cmd = new SqlCommand("sp_NhaCungCap_GetById") { CommandType = CommandType.StoredProcedure };

                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@idNhaCungCap", _idNhaCungCap);
                var tableNhaCungCap = datatabase.getData(_cmd);

                _cmd = new SqlCommand("sp_LayThongTinChiTietNhapPhuTung") { CommandType = CommandType.StoredProcedure };

                _cmd.Parameters.Clear();
                _cmd.Parameters.AddWithValue("@idKey", _idKey);
                var dsHoaDon = datatabase.GetDataSet(_cmd);

                if (dsHoaDon != null && dsHoaDon.Tables.Count > 0)
                {
                    var date = DateTime.Parse(dsHoaDon.Tables[0].Rows[0][0].ToString());
                    var soPhieu = dsHoaDon.Tables[0].Rows[0][1].ToString();
                    var tongTien = Decimal.Parse(dsHoaDon.Tables[0].Rows[0][2].ToString());
                    var tienDoc = _docTien.ChuyenSo(tongTien.ToString(CultureInfo.InvariantCulture).Split('.')[0]);

                    var paraNgayThang = new ReportParameter("NgayNhapKho", "Ngày " + date.Day + " tháng " + date.Month + " năm " + date.Year);
                    var paraSoPhieu = new ReportParameter("SoPhieu", "Số phiếu: " + soPhieu);
                    var paraTienDoc = new ReportParameter("DocTien", tienDoc);

                    reportViewerPhieuNhapKho.LocalReport.SetParameters(new[] { paraNgayThang, paraSoPhieu, paraTienDoc });

                    PhieuNhapKhoBindingSource.DataSource = dsHoaDon.Tables[1];
                }

                ThongTinNhaCungCapBindingSource.DataSource = tableNhaCungCap;

                reportViewerPhieuNhapKho.RefreshReport();
            }
            catch (Exception)
            {
                //
            }
        }
    }
}
