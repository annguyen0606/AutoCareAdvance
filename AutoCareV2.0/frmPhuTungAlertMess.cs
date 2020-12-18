using AutoCareV2._0.Class;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AutoCareV2._0
{
    public partial class frmPhuTungAlertMess : Form
    {
        public delegate void DelegateChangeCheckingPhuTung(TimeSpan timeSpan, bool isStopChecking);

        public delegate void ShowForm(Form form);

        public DelegateChangeCheckingPhuTung ChangeChecking;

        private delegate void SetTextCallBack(string text);

        public int SoLuongPhuTung;
        public List<PhuTung> ListPhuTung;
        private static int seconds = 0;

        private frmPhuTungAlertDetail frmAlertDetail = null;

        public frmPhuTungAlertMess()
        {
            InitializeComponent();
        }

        private void SetText(string text)
        {
            if (lblAmount.InvokeRequired)
            {
                SetTextCallBack d = new SetTextCallBack(SetText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                lblAmount.Text = text;
            }
        }

        private void frmPhuTungAlertMess_Load(object sender, EventArgs e)
        {
            SetText(SoLuongPhuTung.ToString());
            cboTimeAlert.SelectedIndex = 0;

            //Đóng form sau 15 giây
            seconds = 15;
            StartTimer();
            btnOK.Text = "(" + seconds + ")" + " OK";
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            seconds--;
            btnOK.Text = "(" + seconds + ")" + " OK";

            if (seconds == 0)
            {
                timer.Stop();
                this.Close();
            }
        }

        private void StartTimer()
        {
            timer.Start();
        }

        private void linkLabelViewDetail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            timer.Stop();

            HienThiForm(frmAlertDetail);
        }

        private void HienThiForm(Form form)
        {
            if (form != null && form.InvokeRequired)
                form.Invoke(new ShowForm(HienThiForm), new object[] { form });
            else
            {
                if (frmAlertDetail == null)
                {
                    frmAlertDetail = new frmPhuTungAlertDetail();
                    frmAlertDetail.ListPhuTung = ListPhuTung;
                    frmAlertDetail._StartTimer = new frmPhuTungAlertDetail.StartTimer(StartTimer);

                    frmAlertDetail.ShowDialog();
                }
                else
                {
                    if (!frmAlertDetail.Visible)
                    {
                        frmAlertDetail.ListPhuTung = ListPhuTung;
                        frmAlertDetail._StartTimer = new frmPhuTungAlertDetail.StartTimer(StartTimer);

                        frmAlertDetail.ShowDialog();
                    }
                }
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            timer.Stop();
            this.Close();
        }

        private void frmPhuTungAlertMess_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (ChangeChecking != null)
                {
                    TimeSpan timeSpan;
                    int index = cboTimeAlert.SelectedIndex;

                    switch (index)
                    {
                        case 0:
                            timeSpan = new TimeSpan(0, 0, 30);
                            ChangeChecking(timeSpan, false);
                            break;

                        case 1:
                            timeSpan = new TimeSpan(0, 1, 0);
                            ChangeChecking(timeSpan, false);
                            break;

                        case 2:
                            timeSpan = new TimeSpan(0, 5, 0);
                            ChangeChecking(timeSpan, false);
                            break;

                        case 3:
                            timeSpan = new TimeSpan(0, 10, 0);
                            ChangeChecking(timeSpan, false);
                            break;

                        case 4:
                            timeSpan = new TimeSpan(0, 30, 0);
                            ChangeChecking(timeSpan, false);
                            break;

                        case 5:
                            timeSpan = new TimeSpan(1, 0, 0);
                            ChangeChecking(timeSpan, false);
                            break;

                        case 6:
                            timeSpan = new TimeSpan(4, 0, 0);
                            ChangeChecking(timeSpan, false);
                            break;

                        case 7:
                            timeSpan = new TimeSpan(0, 0, 30);
                            ChangeChecking(timeSpan, true);
                            break;

                        default:
                            break;
                    }
                }
            }
            catch { }
        }
    }
}