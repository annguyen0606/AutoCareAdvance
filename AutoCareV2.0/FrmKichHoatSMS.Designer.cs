namespace AutoCareV2._0
{
    partial class FrmKichHoatSMS
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chk_BaoDuongDinhKy = new System.Windows.Forms.CheckBox();
            this.chk_BaoDuongToanBo = new System.Windows.Forms.CheckBox();
            this.chk_BaoDuongDV = new System.Windows.Forms.CheckBox();
            this.chk_MuaXe = new System.Windows.Forms.CheckBox();
            this.chk_SinhNhat = new System.Windows.Forms.CheckBox();
            this.chk_TatCa = new System.Windows.Forms.CheckBox();
            this.btn_CapNhat = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chk_BaoDuongDinhKy);
            this.groupBox1.Controls.Add(this.chk_BaoDuongToanBo);
            this.groupBox1.Controls.Add(this.chk_BaoDuongDV);
            this.groupBox1.Controls.Add(this.chk_MuaXe);
            this.groupBox1.Controls.Add(this.chk_SinhNhat);
            this.groupBox1.Controls.Add(this.chk_TatCa);
            this.groupBox1.Location = new System.Drawing.Point(2, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(483, 131);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Cấu hình nhắn tin";
            // 
            // chk_BaoDuongDinhKy
            // 
            this.chk_BaoDuongDinhKy.AutoSize = true;
            this.chk_BaoDuongDinhKy.Location = new System.Drawing.Point(325, 65);
            this.chk_BaoDuongDinhKy.Name = "chk_BaoDuongDinhKy";
            this.chk_BaoDuongDinhKy.Size = new System.Drawing.Size(142, 17);
            this.chk_BaoDuongDinhKy.TabIndex = 0;
            this.chk_BaoDuongDinhKy.Text = "SMS Bảo dưỡng định kỳ";
            this.chk_BaoDuongDinhKy.UseVisualStyleBackColor = true;
            // 
            // chk_BaoDuongToanBo
            // 
            this.chk_BaoDuongToanBo.AutoSize = true;
            this.chk_BaoDuongToanBo.Location = new System.Drawing.Point(173, 88);
            this.chk_BaoDuongToanBo.Name = "chk_BaoDuongToanBo";
            this.chk_BaoDuongToanBo.Size = new System.Drawing.Size(143, 17);
            this.chk_BaoDuongToanBo.TabIndex = 0;
            this.chk_BaoDuongToanBo.Text = "SMS Bảo dưỡng toàn bộ";
            this.chk_BaoDuongToanBo.UseVisualStyleBackColor = true;
            // 
            // chk_BaoDuongDV
            // 
            this.chk_BaoDuongDV.AutoSize = true;
            this.chk_BaoDuongDV.Location = new System.Drawing.Point(24, 88);
            this.chk_BaoDuongDV.Name = "chk_BaoDuongDV";
            this.chk_BaoDuongDV.Size = new System.Drawing.Size(142, 17);
            this.chk_BaoDuongDV.TabIndex = 0;
            this.chk_BaoDuongDV.Text = "SMS Bảo dưỡng dịch vụ";
            this.chk_BaoDuongDV.UseVisualStyleBackColor = true;
            // 
            // chk_MuaXe
            // 
            this.chk_MuaXe.AutoSize = true;
            this.chk_MuaXe.Location = new System.Drawing.Point(173, 65);
            this.chk_MuaXe.Name = "chk_MuaXe";
            this.chk_MuaXe.Size = new System.Drawing.Size(125, 17);
            this.chk_MuaXe.TabIndex = 0;
            this.chk_MuaXe.Text = "SMS Cám ơn mua xe";
            this.chk_MuaXe.UseVisualStyleBackColor = true;
            // 
            // chk_SinhNhat
            // 
            this.chk_SinhNhat.AutoSize = true;
            this.chk_SinhNhat.Location = new System.Drawing.Point(24, 65);
            this.chk_SinhNhat.Name = "chk_SinhNhat";
            this.chk_SinhNhat.Size = new System.Drawing.Size(95, 17);
            this.chk_SinhNhat.TabIndex = 0;
            this.chk_SinhNhat.Text = "SMS sinh nhật";
            this.chk_SinhNhat.UseVisualStyleBackColor = true;
            // 
            // chk_TatCa
            // 
            this.chk_TatCa.AutoSize = true;
            this.chk_TatCa.Location = new System.Drawing.Point(24, 32);
            this.chk_TatCa.Name = "chk_TatCa";
            this.chk_TatCa.Size = new System.Drawing.Size(57, 17);
            this.chk_TatCa.TabIndex = 0;
            this.chk_TatCa.Text = "Tất cả";
            this.chk_TatCa.UseVisualStyleBackColor = true;
            this.chk_TatCa.CheckedChanged += new System.EventHandler(this.chk_TatCa_CheckedChanged);
            // 
            // btn_CapNhat
            // 
            this.btn_CapNhat.Location = new System.Drawing.Point(191, 140);
            this.btn_CapNhat.Name = "btn_CapNhat";
            this.btn_CapNhat.Size = new System.Drawing.Size(90, 35);
            this.btn_CapNhat.TabIndex = 1;
            this.btn_CapNhat.Text = "&Cập nhật";
            this.btn_CapNhat.UseVisualStyleBackColor = true;
            this.btn_CapNhat.Click += new System.EventHandler(this.btn_CapNhat_Click);
            // 
            // FrmKichHoatSMS
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(489, 193);
            this.Controls.Add(this.btn_CapNhat);
            this.Controls.Add(this.groupBox1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmKichHoatSMS";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Kích hoạt tin nhắn";
            this.Load += new System.EventHandler(this.FrmKichHoatSMS_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox chk_BaoDuongDinhKy;
        private System.Windows.Forms.CheckBox chk_BaoDuongToanBo;
        private System.Windows.Forms.CheckBox chk_BaoDuongDV;
        private System.Windows.Forms.CheckBox chk_MuaXe;
        private System.Windows.Forms.CheckBox chk_SinhNhat;
        private System.Windows.Forms.CheckBox chk_TatCa;
        private System.Windows.Forms.Button btn_CapNhat;
    }
}