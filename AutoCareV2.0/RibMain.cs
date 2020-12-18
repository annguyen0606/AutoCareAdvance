using AutoCareV2._0.Class;
using AutoCareV2._0.UserControls;
using AutoCareV2._0.UserControls.BaoDuong;
using AutoCareV2._0.UserControls.KhachHang;
using AutoCareV2._0.UserControls.KiemTraPhuTungKhoKhac;
using AutoCareV2._0.UserControls.QuanLyPhuTung;
using AutoCareV2._0.UserControls.QuanLyXuatKho;
using AutoCareV2._0.UserControls.ThongKeBaoDuong;
using AutoCareV2._0.UserControls.ThongKePhuTungDaNhap;
using AutoCareV2._0.UserControls.ThongKeXe;
using AutoCareV2._0.UserControls.Tin;
using DevComponents.DotNetBar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class RibMain : DevComponents.DotNetBar.RibbonForm
    {
        public delegate void ShowForm();

        public string oldversion = AutoCareV2._0.Class.SoftwareVersions.CurrentVersion;
        private static bool isStopCheckPhuTung = false;
        private static TimeSpan TimeDelay = new TimeSpan(0, 0, 30);
        private frmPhuTungAlertMess frmAlertMess = null;

        private BackgroundWorker bgAutoUpdate;
        private BackgroundWorker bgLayPhuTung;
        private BackgroundWorker bgKiemTraPhuTung;

        public RibMain()
        {
            InitializeComponent();

            buttonItem1.Text = "Autocare " + oldversion;

            string Header = "Công ty: " + Class.CompanyInfo.tencongty + " | Thương hiệu: " + Class.CompanyInfo.sendername + " | Số Quota còn: " + Class.CompanyInfo.quota;
            this.Text = Header;
            lbl_TaiKhoan.Text = Class.EmployeeInfo.TenNhanVien;

            string GoiPhanMem = Class.CompanyInfo.GoiPhanMem;
            if (GoiPhanMem.ToLower() == "nhan tin")
            {
                //Ẩn tab
                RBTItemDanhMuc.Visible = false;
                RBTItemChucNang.Select();

                //Ẩn nút
                RBBPhieu.Visible = false;
                RBBQuanLyBaoDuongXe.Visible = false;
                RBBQuanLyPhuTung.Visible = false;
                RBBQuanLyXe.Visible = false;
                RBBQuanLyKhachHang.Left = 0;

                RBBXe.Visible = false;
                RBBQuy.Visible = false;
                RBBThongKePhuTung.Visible = false;

                RBBBaoDuong.Left = 0;

                btn_BaoDuongTheoXe.Visible = false;
                btn_BaoDuongTheoTho.Visible = false;
                btn_PhuTung_Tho_ThoiGian.Visible = false;
                btnXoabaoduong.Visible = false;
            }
            else if (GoiPhanMem.ToLower() == "bao duong")
            {
                //RBBQuanLyXe.Visible = false;
                //RBBQuanLyKhachHang.Visible = false;
                //RBBXe1.Visible = false;
                //RBBXe.Visible = true;

                //btnquanlynhomkh.Visible = false;
            }

            if (Class.EmployeeInfo.Quyen.ToLower() == "qtv")
            {
                btnQuanLyCV.Enabled = true;
                btnQuanLyThoDichVu.Enabled = true;
                btn_KichHoat.Enabled = true;
                rib_BackUp.Visible = true;
                btn_DieuChuyen.Enabled = true;
                btnXoabaoduong.Enabled = true;
            }
            else if(Class.EmployeeInfo.Quyen.ToLower() == "disable_kh")
            {
                btnQuanLyCV.Enabled = true;
                btnQuanLyThoDichVu.Enabled = true;
                btn_KichHoat.Enabled = true;
                rib_BackUp.Visible = true;
                btn_DieuChuyen.Enabled = true;
                btnXoabaoduong.Enabled = true;
                btnDsKhách.Enabled = false;
                btn_XeDaBan.Enabled = false;
            }
            else
            {
                btnQuanLyCV.Enabled = false;
                btnQuanLyThoDichVu.Enabled = false;
                btn_KichHoat.Enabled = false;
                rib_BackUp.Visible = false;
                btn_DieuChuyen.Enabled = false;
                btnXoabaoduong.Enabled = false;
            }

            if (Class.EmployeeInfo.UserName == "vietlong2sale"
                || Class.EmployeeInfo.UserName == "vietlong3sale"
                || Class.EmployeeInfo.UserName == "vietlong1sale")
            {
                //RBBQuanLyPhuTung.Visible = false;
                RBBPhieu.Visible = false;
                btn_NhapMoi.Visible = false;
                btn_DieuChuyen.Visible = false;
                btn_DanhSach.Visible = false;
                btn_capnhat.Visible = false;
                btnKiemTraPhuTung.Visible = false;
                btnXuatKhoNgoai.Visible = false;
                //RBBXe.Visible = false;
                RBBThongKePhuTung.Visible = false;
                RBBQuy.Visible = false;
            }

            if (Class.EmployeeInfo.UserName == "vietlong2kho"
                || Class.EmployeeInfo.UserName == "vietlong3kho"
                || Class.EmployeeInfo.UserName == "vietlong1kho"
                || Class.EmployeeInfo.UserName == "vietlong1khoadmin"
                || Class.EmployeeInfo.UserName == "vietlong2khoadmin"
                || Class.EmployeeInfo.UserName == "vietlong3khoadmin")
            {
                //RBBQuanLyBaoDuongXe.Visible = false;
                btnKiemTraXuatKho.Visible = false;
                RBBPhieu.Visible = false;
                RBBQuanLyKhachHang.Visible = false;
                RBTItemTinNhan.Visible = false;
                RBBQuy.Visible = false;
                btn_KHdenBD.Visible = false;
                btnKhachKhongDenBD.Visible = false;
                btn_BaoDuongTheoXe.Visible = false;
                btn_BaoDuongTheoTho.Visible = false;
                btnXoabaoduong.Visible = false;
            }
        }

        public Assembly ApplicationAssembly
        {
            get { return Assembly.GetExecutingAssembly(); }
        }

        private bool KiemTraTenTabConTrol(string name)
        {
            for (int i = 0; i < superTabControl1.Tabs.Count; i++)
            {
                if (superTabControl1.Tabs[i].Text == name)
                {
                    superTabControl1.SelectedTabIndex = i;
                    return true;
                }
            }
            return false;
        }

        private void btn_NhapXe_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Nhập xe") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Nhập xe");
                UcNhapXe frm = new UcNhapXe();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_BanXe_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Bán xe") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Bán xe");
                UcBanXe frm = new UcBanXe();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_TiepNhan_Click(object sender, EventArgs e)
        {
            if (Class.CompanyInfo.idcongty != "0") //60 - DongA
            {
                if (KiemTraTenTabConTrol("&Bảo dưỡng xe") == false)
                {
                    SuperTabItem Tab = superTabControl1.CreateTab("&Bảo dưỡng xe");

                    UcBaoDuong frm = new UcBaoDuong();
                    frm.Dock = DockStyle.Fill;
                    Tab.AttachedControl.Controls.Add(frm);
                    frm.Show();

                    superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
                }
            }
            else
            {
                if (KiemTraTenTabConTrol("&Tiếp nhận xe") == false)
                {
                    SuperTabItem Tab = superTabControl1.CreateTab("&Tiếp nhận xe");

                    UcTiepNhanDongA frm = new UcTiepNhanDongA();
                    frm.Dock = DockStyle.Fill;
                    Tab.AttachedControl.Controls.Add(frm);
                    frm.Show();

                    superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
                }
            }
        }

        private void btn_NhapMoi_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Nhập mới phụ tùng") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Nhập mới phụ tùng");
                UcNhapMoiPhuTung frm = new UcNhapMoiPhuTung();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_PhuTungDaNhap_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Thống kê phụ tùng đã nhập") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Thống kê phụ tùng đã nhập");
                UcPhuTungDaNhap frm = new UcPhuTungDaNhap();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_LoaiXe_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Quản lý kiểu xe") == false)
            {
                frmQuanLyKieuXe frm = new frmQuanLyKieuXe();
                frm.ShowDialog();
            }
        }

        private void btn_DanhSachXe_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Quản lý thông tin xe") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Quản lý thông tin xe");
                UcDanhSachXe frm = new UcDanhSachXe();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_PhieuThu_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Phiếu thu") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Phiếu thu");
                UcPhieuThu frm = new UcPhieuThu();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_PhieuChi_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Phiếu chi") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Phiếu chi");
                UcPhieuChi frm = new UcPhieuChi();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_XeDaNhap_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Quản lý xe đã nhập") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Quản lý xe đã nhập");
                UcXeDaNhap frm = new UcXeDaNhap();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_XeConTrongKho_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Quản lý xe còn trong kho") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Quản lý xe còn trong kho");
                UcXeConTrongKho frm = new UcXeConTrongKho();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_Quy_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Quỹ") == false)
            {
                frmQuy frm = new frmQuy();
                frm.ShowDialog();
            }
        }

        private void btn_ThayPhuTung_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Thay phụ tùng") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Thay phụ tùng");
                UcThayPhuTung frm = new UcThayPhuTung();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_ChonThoSua_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Chọn thợ sửa") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Chọn thợ sửa");
                UcChonTho frm = new UcChonTho();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_HoanThanh_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Hoàn thành") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Hoàn thành");
                UcHoanThanh frm = new UcHoanThanh();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void RibMain_Load(object sender, EventArgs e)
        {
            #region Quy trình bảo dưỡng rút gọn

            if (Class.CompanyInfo.idcongty != "0") //60 - DongA
            {
                btn_ThayPhuTung.Visible = false;
                btn_ChonThoSua.Visible = false;
                btn_HoanThanh.Visible = false;
                btn_TiepNhan.Visible = true;
                btn_TiepNhan.Text = "&Quy trình bảo dưỡng";
                RBBQuanLyBaoDuongXe.Width = 110;
            }

            #endregion Quy trình bảo dưỡng rút gọn

            RBTItemDanhMuc.Focus();

            #region bỏ

            //Thread threadAutoUpdate = new Thread(new ThreadStart(Function_KiemTraAutoUpdate));
            //Thread threadLayPhuTung = new Thread(new ThreadStart(LayDanhSachPhuTung));
            //Thread threadKiemTraPhuTung = new Thread(new ThreadStart(Function_KiemTraPhuTung));

            //threadAutoUpdate.IsBackground = true;
            //threadLayPhuTung.IsBackground = true;
            //threadKiemTraPhuTung.IsBackground = true;

            //threadAutoUpdate.Start();
            //threadLayPhuTung.Start();
            //threadKiemTraPhuTung.Start();

            #endregion bỏ

            #region khởi tạo các luồng kiểm tra phiên bản - kiểm tra số lượng phụ tùng

            bgAutoUpdate = new BackgroundWorker();
            bgAutoUpdate.DoWork += bgAutoUpdate_DoWork;
            bgAutoUpdate.RunWorkerCompleted += bgAutoUpdate_RunWorkerCompleted;

            bgLayPhuTung = new BackgroundWorker();
            bgLayPhuTung.DoWork += bgLayPhuTung_DoWork;
            bgLayPhuTung.RunWorkerCompleted += bgLayPhuTung_RunWorkerCompleted;

            bgKiemTraPhuTung = new BackgroundWorker();
            bgKiemTraPhuTung.DoWork += bgKiemTraPhuTung_DoWork;
            bgKiemTraPhuTung.RunWorkerCompleted += bgKiemTraPhuTung_RunWorkerCompleted;

            //bgAutoUpdate.RunWorkerAsync();
            bgLayPhuTung.RunWorkerAsync();
            bgKiemTraPhuTung.RunWorkerAsync();

            #endregion khởi tạo các luồng kiểm tra phiên bản - kiểm tra số lượng phụ tùng
        }

        private void bgKiemTraPhuTung_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void bgKiemTraPhuTung_DoWork(object sender, DoWorkEventArgs e)
        {
            while (isStopCheckPhuTung == false)
            {
                Thread.Sleep(TimeDelay);

                if (KiemTraPhuTung.KiemTraSoLuongPhuTung(KiemTraPhuTung.ListPhuTung, Convert.ToInt32(CompanyInfo.idcongty)).Count > 0)
                {
                    HienThiThongBaoPhuTung();
                }
            }
        }

        private void bgLayPhuTung_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void bgLayPhuTung_DoWork(object sender, DoWorkEventArgs e)
        {
            int flag;

            DataTable tablePhuTung = new DataTable();
            TimeSpan interval = new TimeSpan(0, 0, 10);

            while (isStopCheckPhuTung == false)
            {
                try
                {
                    Thread.Sleep(interval);
                    using (SqlConnection cnn = Class.datatabase.getConnection())
                    {
                        cnn.Open();
                        //                        SqlDataAdapter adap = new SqlDataAdapter(@"select DISTINCT IdPT, MaPT,TenPT,DVT,DonGia,SoLuong,NguongSoLuong,phutung.IdKho,khohang.TenKho
                        //                             from phutung inner join KhoHang on phutung.IdKho=KhoHang.IdKho where phutung.IdCongTy=@IdCongTy", cnn);
                        SqlDataAdapter adap = new SqlDataAdapter(@"select DISTINCT IdPT, MaPT,TenPT,DVT,DonGia,SoLuong,phutung.IdKho,khohang.TenKho
                             from phutung inner join KhoHang on phutung.IdKho=KhoHang.IdKho where phutung.IdCongTy=@IdCongTy", cnn);
                        adap.SelectCommand.Parameters.AddWithValue("@IdCongTy", CompanyInfo.idcongty);
                        tablePhuTung.Clear();
                        adap.Fill(tablePhuTung);

                        List<PhuTung> listPhuTung = tablePhuTung.AsEnumerable().AsParallel().Select(r => new PhuTung()
                        {
                            IdPhuTung = int.Parse(r["IdPT"].ToString()),
                            MaPhuTung = Convert.ToString(r["MaPT"]),
                            TenPhuTung = Convert.ToString(r["TenPT"]),
                            IdKho = Convert.ToString(r["IdKho"]) != "" ? int.Parse(r["IdKho"].ToString()) : 0,
                            TenKho = Convert.ToString(r["TenKho"]),
                            SoLuong = Convert.ToString(r["SoLuong"]) != "" ? int.Parse(r["SoLuong"].ToString()) : 0,
                            //NguongSoLuong = Convert.ToString(r["NguongSoLuong"]) != "" ? int.Parse(r["NguongSoLuong"].ToString()) : 0,
                            IdCongTy = Convert.ToInt32(CompanyInfo.idcongty)
                        }).ToList();

                        KiemTraPhuTung.ListPhuTung = listPhuTung;

                        cnn.Close();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message); }
            }
        }

        private void bgAutoUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
        }

        private void bgAutoUpdate_DoWork(object sender, DoWorkEventArgs e)
        {
            TimeSpan interval = new TimeSpan(0, 0, 10);

            Thread.Sleep(interval);
            Microsoft.Win32.RegistryKey ReadKey;
            ReadKey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("AutoCareUpdate");
            if (ReadKey == null)
            {
                Microsoft.Win32.RegistryKey key;
                key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey("AutoCareUpdate");
                key.SetValue("AutoUpdate", true);
                key.Close();
            }

            bool AutoUpdate = false;
            try
            {
                AutoUpdate = bool.Parse(ReadKey.GetValue("AutoUpdate").ToString());
            }
            catch (Exception) { }

            if (AutoUpdate == true)
            {
                try
                {
                    DataTable dt = new DataTable();

                    using (SqlConnection cnn = Class.datatabase.getConnection())
                    {
                        cnn.Open();

                        SqlDataAdapter adap = new SqlDataAdapter(@"SELECT TOP 1 * FROM Software_Update ORDER BY SoftwareId DESC", cnn);

                        adap.Fill(dt);

                        if (dt.Rows.Count > 0)
                        {
                            if (dt.Rows[0][3].ToString() != oldversion)
                            {
                                frmMessUpdateSW frm = new frmMessUpdateSW();
                                frm.NameVersion = dt.Rows[0][1].ToString();
                                frm.OldVerSion = oldversion;
                                frm.NewVersion = dt.Rows[0][3].ToString();
                                frm.ChangeLogs = dt.Rows[0][4].ToString();
                                frm.UpdateLocation = dt.Rows[0][5].ToString();
                                frm.FileSize = dt.Rows[0][6].ToString();
                                frm.md5 = dt.Rows[0][7].ToString();

                                Uri location = new Uri(dt.Rows[0][5].ToString());
                                string fileName = dt.Rows[0][1].ToString();
                                string idsoftware = dt.Rows[0][0].ToString();
                                string NewVersion = dt.Rows[0][3].ToString();

                                if (frm.ShowDialog() == DialogResult.OK)
                                {
                                    frmUpdateDownload form = new frmUpdateDownload(location, fileName, idsoftware, oldversion, NewVersion);
                                    DialogResult result = form.ShowDialog();

                                    if (result == DialogResult.OK)
                                    {
                                        dt.Dispose();
                                    }
                                    else if (result == DialogResult.Abort)
                                    {
                                        MessageBox.Show("Tải về bản cập nhật đã bị hủy bỏ!\nChương trình chưa được cập nhật!", "Hủy bỏ tải về cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        dt.Dispose();
                                    }
                                    else
                                    {
                                        MessageBox.Show(" Đã xảy ra vấn đề trong lúc tải về bản cập nhật!\nVui lòng thử lại sau!", "Lỗi tải về bản cập nhật", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        dt.Dispose();
                                    }
                                }
                            }
                        }
                        cnn.Close();
                    }
                }
                catch (Exception ex) { MessageBox.Show("Lỗi: " + ex.Message + "\nVui lòng kiểm tra lại đường truyền mạng!"); }
            }
        }

        private void btn_MauXe_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Màu xe") == false)
            {
                // SuperTabItem Tab = superTabControl1.CreateTab("&Màu xe");
                FrmMauXe frm = new FrmMauXe();
                frm.ShowDialog();
                // superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_NhaCungCap_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Nhà cung cấp") == false)
            {
                // SuperTabItem Tab = superTabControl1.CreateTab("&Màu xe");
                frmQuanLyNhaCungCap frm = new frmQuanLyNhaCungCap();
                frm.ShowDialog();
                // superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_KhoPhuTung_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Kho phụ tùng") == false)
            {
                // SuperTabItem Tab = superTabControl1.CreateTab("&Màu xe");
                frmQuanLyKhoPhuTung frm = new frmQuanLyKhoPhuTung();
                frm.ShowDialog();
                // superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_DieuChuyen_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Điều chuyển") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Điều chuyển");
                UcDieuChuyen frm = new UcDieuChuyen();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_DanhSach_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Danh sách") == false)
            {
                frmQuanLyPhuTung frm = new frmQuanLyPhuTung();
                //.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frm.Show();
            }
        }

        private void btn_XeDaBan_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Xe đã bán") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Xe đã bán");

                UcXeDaBan frm = new UcXeDaBan();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_KHdenBD_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Khách hàng đến bảo dưỡng") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Khách hàng đến bảo dưỡng");

                UcKhachDenBaoDuong frm = new UcKhachDenBaoDuong();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_BaoDuongTheoXe_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Bảo dưỡng theo xe") == false)
            {
                frmThongKeTheoKhachHang frm = new frmThongKeTheoKhachHang();

                frm.ShowDialog();
            }
        }

        private void btn_BaoDuongTheoTho_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Bảo dưỡng theo thợ") == false)
            {
                frmThongKeTheoTho frm = new frmThongKeTheoTho();
                frm.ShowDialog();
            }
        }

        private void btn_PhuTung_Tho_ThoiGian_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Phụ tùng, thời, thời gian") == false)
            {
                frmThongKePhuTungTheoXe frm = new frmThongKePhuTungTheoXe();
                frm.ShowDialog();
            }
        }

        private void btnXoabaoduong_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Xóa lần bảo dưỡng") == false)
            {
                FrmXoaLanBaoDuong frm = new FrmXoaLanBaoDuong();
                frm.ShowDialog();
            }
        }

        private void btnDsKhách_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Danh sách khách hàng") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Danh sách khách hàng");

                UcQuanLyKhachHang frm = new UcQuanLyKhachHang();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
            //if (KiemTraTenTabConTrol("&Danh sách khách hàng") == false)
            //{
            //    SuperTabItem Tab = superTabControl1.CreateTab("&Danh sách khách hàng");

            //    UcQuanLyKhachHangNew frm = new UcQuanLyKhachHangNew();
            //    frm.Dock = DockStyle.Fill;
            //    Tab.AttachedControl.Controls.Add(frm);
            //    frm.Show();
            //    superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            //}
        }

        private void btnthemkhach_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Thêm khách mua xe") == false)
            {
                FrmImportKhachHangMuaXe frm = new FrmImportKhachHangMuaXe();
                // frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frm.ShowDialog();
            }
        }

        private void btnkhachbaoduong_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Thêm khách bảo dưỡng") == false)
            {
                FrmImportXeBaoDuong frm = new FrmImportXeBaoDuong();
                // frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frm.ShowDialog();
            }
        }

        private void buttonItem4_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Sinh nhật khách") == false)
            {
                FrmTNMungsinhnhatKH frm = new FrmTNMungsinhnhatKH();
                // frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frm.ShowDialog();
            }
        }

        private void btnquanlynhomkh_Click_1(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Quản lý nhóm khách hàng") == false)
            {
                FrmQuanlynhomKH frm = new FrmQuanlynhomKH();
                // frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                frm.ShowDialog();
            }
        }

        private void buttonItem13_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            this.Visible = false;
            FrmLogin LoginForm = new FrmLogin();
            LoginForm.Visible = true;
        }

        private void btnChangePass_Click(object sender, EventArgs e)
        {
        }

        private void btnChangePass_Click_1(object sender, EventArgs e)
        {
            FrmDoiPass FormDoiPass = new FrmDoiPass();
            FormDoiPass.ShowDialog();
        }

        private void btnTinnhan_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Thống kê tin nhắn") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Thống kê tin nhắn");

                UCThongKeTin frm = new UCThongKeTin();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void buttonItem2_Click(object sender, EventArgs e)
        {
            isStopCheckPhuTung = true;
            Application.Exit();
        }

        private void buttonItem3_Click(object sender, EventArgs e)
        {
            isStopCheckPhuTung = true;

            FrmLogin frm = new FrmLogin();
            frm.Show();
            this.Dispose();
        }

        private void superTabControlPanel1_Click(object sender, EventArgs e)
        {
        }

        private void ribbonPanel5_Click(object sender, EventArgs e)
        {
        }

        private void ribbonBar4_ItemClick(object sender, EventArgs e)
        {
        }

        private void RBBXe1_ItemClick(object sender, EventArgs e)
        {
        }

        private void ribbonPanel2_Click(object sender, EventArgs e)
        {
        }

        private void ribbonBar9_ItemClick(object sender, EventArgs e)
        {
        }

        private void ribbonPanel3_Click(object sender, EventArgs e)
        {
        }

        private void RBBThongKePhuTung_ItemClick(object sender, EventArgs e)
        {
        }

        private void RBBQuy_ItemClick(object sender, EventArgs e)
        {
        }

        private void RBBXe_ItemClick(object sender, EventArgs e)
        {
        }

        private void ribbonPanel4_Click(object sender, EventArgs e)
        {
        }

        private void RBBQuanLyKhachHang_ItemClick(object sender, EventArgs e)
        {
        }

        private void RBBPhieu_ItemClick(object sender, EventArgs e)
        {
        }

        private void RBBQuanLyBaoDuongXe_ItemClick(object sender, EventArgs e)
        {
        }

        private void RBBQuanLyXe_ItemClick(object sender, EventArgs e)
        {
        }

        private void superTabControl1_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
        {
        }

        private void applicationButton1_Click(object sender, EventArgs e)
        {
        }

        private void RBTItemDanhMuc_Click(object sender, EventArgs e)
        {
        }

        private void RBTItemTinNhan_Click(object sender, EventArgs e)
        {
        }

        private void RBTItemThongKe_Click(object sender, EventArgs e)
        {
        }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
        }

        private void BtnGioiThieu_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Giới Thiệu") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Giới Thiệu");

                GioiThieu frm = new GioiThieu();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btn_capnhat_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Cập nhật") == false)
            {
                PhuTungUpdate frm = new PhuTungUpdate();
                frm.ShowDialog();
            }
        }

        private void superTabControl1_TabIndexChanged(object sender, EventArgs e)
        {
        }

        private void btn_DatLich_Click(object sender, EventArgs e)
        {
            FrmSMSTuyBien frm = new FrmSMSTuyBien();
            frm.ShowDialog();
        }

        private void btn_LichSu_Click(object sender, EventArgs e)
        {
            FrmSMSTuyBienStore frm = new FrmSMSTuyBienStore();
            frm.ShowDialog();
        }

        private void btnQuanLyCV_Click(object sender, EventArgs e)
        {
            frmQuanLyCongViec frm = new frmQuanLyCongViec();
            // frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            frm.ShowDialog();
        }

        private void btnQLPhuKien_Click(object sender, EventArgs e)
        {
            frmQuanLyPhuKien frm = new frmQuanLyPhuKien();
            // frm.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            frm.ShowDialog();
        }

        private void buttonItem4_Click_1(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Khách hàng đến bảo dưỡng") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Khách hàng đến bảo dưỡng");

                UcKhachDenBaoDuong frm = new UcKhachDenBaoDuong();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void btnKhachKhongDenBD_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Khách không đến bảo dưỡng") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Khách không đến bảo dưỡng");

                UcKhachKhongDenBaoDuong frm = new UcKhachKhongDenBaoDuong();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void RibMain_FormClosed(object sender, FormClosedEventArgs e)
        {
        }

        private void btnQuanLyThoDichVu_Click(object sender, EventArgs e)
        {
            frmQuanLyThoDichVu frm = new frmQuanLyThoDichVu();
            frm.ShowDialog();
        }

        private void buttonItem4_Click_2(object sender, EventArgs e)
        {
            FrmBackUp frm = new FrmBackUp();
            frm.ShowDialog();
        }

        private void btn_KichHoat_Click(object sender, EventArgs e)
        {
            FrmKichHoatSMS frm = new FrmKichHoatSMS();
            frm.ShowDialog();
        }

        private void buttonItem5_Click(object sender, EventArgs e)
        {
            frmUpdateSoftware frm = new frmUpdateSoftware();
            frm.textBoxCurentVersion.Text = oldversion;
            frm.ShowDialog();
        }

        private void Function_KiemTraPhuTung()
        {
        }

        private void HienThiThongBaoPhuTung()
        {
            if (frmAlertMess == null)
            {
                frmAlertMess = new frmPhuTungAlertMess();
            }

            if (frmAlertMess.InvokeRequired)
            {
                frmAlertMess.Invoke(new ShowForm(HienThiThongBaoPhuTung));
            }
            else
            {
                if (!frmAlertMess.Visible)
                {
                    frmAlertMess.SoLuongPhuTung = KiemTraPhuTung.KiemTraSoLuongPhuTung(KiemTraPhuTung.ListPhuTung, Convert.ToInt32(CompanyInfo.idcongty)).Count;
                    frmAlertMess.ListPhuTung = KiemTraPhuTung.KiemTraSoLuongPhuTung(KiemTraPhuTung.ListPhuTung, Convert.ToInt32(CompanyInfo.idcongty));

                    frmAlertMess.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
                    frmAlertMess.Location = new System.Drawing.Point(this.Width - frmAlertMess.Width - 35, this.Height - frmAlertMess.Height - 35);
                    frmAlertMess.ChangeChecking = new frmPhuTungAlertMess.DelegateChangeCheckingPhuTung(Function_CapNhatKiemTra);

                    frmAlertMess.ShowDialog();
                }
            }
        }

        private void LayDanhSachPhuTung()
        {
        }

        private void Function_KiemTraAutoUpdate()
        {
        }

        private void Function_CapNhatKiemTra(TimeSpan _timeSpan, bool _isStop)
        {
            TimeDelay = _timeSpan;
            isStopCheckPhuTung = _isStop;
        }

        private void RibMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            isStopCheckPhuTung = true;

            Application.Exit();
        }

        private void btntTransferStatistic_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Thống kê phụ tùng điều chuyển") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Thống kê phụ tùng điều chuyển");
                ucThongKeDieuChuyen frm = new ucThongKeDieuChuyen();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void buttonItem6_Click(object sender, EventArgs e)
        {
            frmBangCongTho frmCongTho = new frmBangCongTho();
            frmCongTho.ShowDialog();
        }

        private void buttonItem7_Click(object sender, EventArgs e)
        {
            FrmThongKeKhachBdTheoTinNhan frm = new FrmThongKeKhachBdTheoTinNhan();
            frm.ShowDialog();
        }

        private void BtnKiemTraXuatKho_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Thống kê") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Thống kê");
                UcThongKeThuNhapTheoNgay frm = new UcThongKeThuNhapTheoNgay();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void BtnKiemTraPhuTung_Click(object sender, EventArgs e)
        {
            if (KiemTraTenTabConTrol("&Kiểm tra PT") == false)
            {
                SuperTabItem Tab = superTabControl1.CreateTab("&Kiểm tra PT");
                UcKiemTraPhuTungKhoKhac frm = new UcKiemTraPhuTungKhoKhac();
                frm.Dock = DockStyle.Fill;
                Tab.AttachedControl.Controls.Add(frm);
                frm.Show();
                superTabControl1.SelectedTabIndex = superTabControl1.Tabs.Count - 1;
            }
        }

        private void BtnXuatKhoNgoai_Click(object sender, EventArgs e)
        {
            frmQuanLyXuatPhuTungNgoai frm = new frmQuanLyXuatPhuTungNgoai();
            frm.ShowDialog();
        }

        private void RibbonControl1_Click(object sender, EventArgs e)
        {

        }
    }
}