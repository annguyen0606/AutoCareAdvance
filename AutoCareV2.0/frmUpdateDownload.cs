using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace AutoCareV2._0
{
    public partial class frmUpdateDownload : Form
    {
        private WebClient webClient;

        private BackgroundWorker bgWorker;

        private string temFile;
        SqlDataProvider sqlPrv = new SqlDataProvider();

        private string IdSoftware, OldVersion, newVersion;
        internal string TemFilePath
        {
            get { return this.temFile; }
        }

        public frmUpdateDownload(Uri location, string fileName, string IdSoftware, string OldVersion, string newVersion)
        {
            InitializeComponent();
            this.IdSoftware=IdSoftware;
            this.OldVersion = OldVersion;
            this.newVersion = newVersion;

            webClient = new WebClient();
            
            bgWorker = new BackgroundWorker();
            bgWorker.DoWork += new DoWorkEventHandler(bgWorker_DoWork);
            bgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bgWorker_RunWorkerCompleted);
            string FileLocation= location.AbsoluteUri;
            fileName += FileLocation.Substring(FileLocation.LastIndexOf("."), FileLocation.Length - FileLocation.LastIndexOf("."));
            
            webClient.DownloadFileCompleted += new AsyncCompletedEventHandler(webClient_DownloadFileCompleted);
            webClient.DownloadProgressChanged += new DownloadProgressChangedEventHandler(webClient_DownloadProgressChanged);
            webClient.DownloadFileAsync(location, Path.GetTempPath()+fileName);
            
            temFile = Path.GetTempPath() + fileName;
        }

        private void bgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.DialogResult = (DialogResult)e.Result;
            Process.Start(temFile);
            InsertUpdateHistory(IdSoftware, OldVersion, newVersion);
            Application.Exit();
        }

        void InsertUpdateHistory(string IdSoftware,string OldVersion,string newVersion)
        {
            SqlCommand cmd = new SqlCommand(@"INSERT INTO Software_UpdateHistory(SoftwareId, CompanyId, UpdateTime, OldVersion, NewVersion)
                                            VALUES(@SoftwareId, @CompanyId, @UpdateTime, @OldVersion, @NewVersion)");

            cmd.Parameters.AddWithValue("@SoftwareId", Convert.ToInt64(IdSoftware));
            cmd.Parameters.AddWithValue("@CompanyId", Class.CompanyInfo.idcongty);
            cmd.Parameters.AddWithValue("@UpdateTime", DateTime.Now);
            cmd.Parameters.AddWithValue("@OldVersion", OldVersion);
            cmd.Parameters.AddWithValue("@NewVersion", newVersion);
            sqlPrv.ExecuteNonQuery(cmd);
        }

        private void bgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = DialogResult.OK;
        }

        private void webClient_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {            
            if (e.Error != null)
            {
                this.DialogResult = DialogResult.No;
                this.Close();
            }
            else if (e.Cancelled)
            {
                this.DialogResult = DialogResult.Abort;
                this.Close();
            }
            else
            {
                lblProgress.Text = "Verifying Download...";
                progressBar.Style = ProgressBarStyle.Marquee;

                bgWorker.RunWorkerAsync();
            }
        }

        private void webClient_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            this.progressBar.Value = e.ProgressPercentage;
            this.lblProgress.Text = String.Format("Downloaded {0} of {1}", FormatByte(e.BytesReceived, 1, true), FormatByte(e.TotalBytesToReceive, 1, true));
        }

        private string FormatByte(long bytes, int decemalPlaces, bool showByteType)
        {
            double newBytes = bytes;
            string formatString = "{0";
            string byteType = "B";

            if (newBytes > 1024 && newBytes < 1048576)
            {
                newBytes /= 1024;
                byteType = "KB";
            }
            else if (newBytes > 1048576 && newBytes < 1073741824)
            {
                newBytes /= 1048576;
                byteType = "MB";
            }
            else
            {
                newBytes /= 1073741824;
                byteType = "GB";
            }

            if (decemalPlaces > 0)
                formatString += ":0.";

            for (int i = 0; i < decemalPlaces; i++)
                formatString += "0";

            formatString += "}";

            if (showByteType)
                formatString += byteType;

            return string.Format(formatString, newBytes);
        }

        private void frmUpdateDownload_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (webClient.IsBusy)
            {
                webClient.CancelAsync();
                this.DialogResult = DialogResult.Abort;
            }
            if (bgWorker.IsBusy)
            {
                bgWorker.CancelAsync();
                this.DialogResult = DialogResult.Abort;
            }
        }
    }
}
