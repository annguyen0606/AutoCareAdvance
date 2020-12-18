namespace AutoCareV2._0
{
    partial class frmPhuTungAlertMess
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboTimeAlert = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblAmount = new System.Windows.Forms.Label();
            this.linkLabelViewDetail = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Gainsboro;
            this.panel1.Controls.Add(this.cboTimeAlert);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.btnOK);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 59);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(338, 35);
            this.panel1.TabIndex = 0;
            // 
            // cboTimeAlert
            // 
            this.cboTimeAlert.FormattingEnabled = true;
            this.cboTimeAlert.Items.AddRange(new object[] {
            "30 giây",
            "1 phút",
            "5 phút",
            "10 phút",
            "30 phút",
            "1 giờ",
            "4 giờ",
            "Không hiển thị lại nữa"});
            this.cboTimeAlert.Location = new System.Drawing.Point(98, 7);
            this.cboTimeAlert.Name = "cboTimeAlert";
            this.cboTimeAlert.Size = new System.Drawing.Size(125, 21);
            this.cboTimeAlert.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 11);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 13);
            this.label3.TabIndex = 1;
            this.label3.Text = "Thông báo lại sau: ";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(256, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.lblAmount);
            this.panel2.Controls.Add(this.linkLabelViewDetail);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(338, 59);
            this.panel2.TabIndex = 1;
            // 
            // lblAmount
            // 
            this.lblAmount.AutoSize = true;
            this.lblAmount.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmount.ForeColor = System.Drawing.Color.Red;
            this.lblAmount.Location = new System.Drawing.Point(287, 15);
            this.lblAmount.Name = "lblAmount";
            this.lblAmount.Size = new System.Drawing.Size(44, 15);
            this.lblAmount.TabIndex = 2;
            this.lblAmount.Text = "[lblSL]";
            // 
            // linkLabelViewDetail
            // 
            this.linkLabelViewDetail.AutoSize = true;
            this.linkLabelViewDetail.Font = new System.Drawing.Font("Times New Roman", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabelViewDetail.Location = new System.Drawing.Point(6, 32);
            this.linkLabelViewDetail.Name = "linkLabelViewDetail";
            this.linkLabelViewDetail.Size = new System.Drawing.Size(65, 14);
            this.linkLabelViewDetail.TabIndex = 1;
            this.linkLabelViewDetail.TabStop = true;
            this.linkLabelViewDetail.Text = "Xem chi tiết";
            this.linkLabelViewDetail.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelViewDetail_LinkClicked);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(283, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "Số Phụ tùng có số lượng thấp dưới ngưỡng cho phép:";
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // frmPhuTungAlertMess
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(338, 94);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPhuTungAlertMess";
            this.ShowIcon = false;
            this.Text = "Báo động số lượng phụ tùng";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPhuTungAlertMess_FormClosing);
            this.Load += new System.EventHandler(this.frmPhuTungAlertMess_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblAmount;
        private System.Windows.Forms.LinkLabel linkLabelViewDetail;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.ComboBox cboTimeAlert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Timer timer;
    }
}