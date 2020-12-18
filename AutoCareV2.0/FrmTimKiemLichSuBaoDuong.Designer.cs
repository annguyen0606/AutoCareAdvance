namespace AutoCareV2._0
{
    partial class FrmTimKiemLichSuBaoDuong
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmTimKiemLichSuBaoDuong));
            this.lblBienSoXe = new DevComponents.DotNetBar.LabelX();
            this.txtBienSoXe = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.txtSoKhung = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.txtSoMay = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnTimKiem = new DevComponents.DotNetBar.ButtonX();
            this.btnThoat = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // lblBienSoXe
            // 
            // 
            // 
            // 
            this.lblBienSoXe.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblBienSoXe.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBienSoXe.Location = new System.Drawing.Point(11, 9);
            this.lblBienSoXe.Name = "lblBienSoXe";
            this.lblBienSoXe.Size = new System.Drawing.Size(132, 24);
            this.lblBienSoXe.TabIndex = 0;
            this.lblBienSoXe.Text = "Nhập vào biển số xe:";
            // 
            // txtBienSoXe
            // 
            // 
            // 
            // 
            this.txtBienSoXe.Border.Class = "TextBoxBorder";
            this.txtBienSoXe.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBienSoXe.Location = new System.Drawing.Point(11, 36);
            this.txtBienSoXe.Name = "txtBienSoXe";
            this.txtBienSoXe.Size = new System.Drawing.Size(356, 23);
            this.txtBienSoXe.TabIndex = 0;
            this.txtBienSoXe.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBienSoXe_KeyDown);
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.Location = new System.Drawing.Point(11, 111);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(132, 24);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "Nhập vào số khung:";
            // 
            // txtSoKhung
            // 
            // 
            // 
            // 
            this.txtSoKhung.Border.Class = "TextBoxBorder";
            this.txtSoKhung.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSoKhung.Location = new System.Drawing.Point(11, 138);
            this.txtSoKhung.Name = "txtSoKhung";
            this.txtSoKhung.Size = new System.Drawing.Size(356, 23);
            this.txtSoKhung.TabIndex = 2;
            this.txtSoKhung.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSoKhung_KeyDown);
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX2.Location = new System.Drawing.Point(11, 61);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(132, 24);
            this.labelX2.TabIndex = 7;
            this.labelX2.Text = "Nhập vào số máy:";
            // 
            // txtSoMay
            // 
            // 
            // 
            // 
            this.txtSoMay.Border.Class = "TextBoxBorder";
            this.txtSoMay.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSoMay.Location = new System.Drawing.Point(11, 86);
            this.txtSoMay.Name = "txtSoMay";
            this.txtSoMay.Size = new System.Drawing.Size(356, 23);
            this.txtSoMay.TabIndex = 1;
            this.txtSoMay.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSoMay_KeyDown);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnTimKiem.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnTimKiem.Location = new System.Drawing.Point(185, 169);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(87, 27);
            this.btnTimKiem.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnTimKiem.TabIndex = 3;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnThoat.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnThoat.Location = new System.Drawing.Point(280, 169);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(87, 27);
            this.btnThoat.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // FrmTimKiemLichSuBaoDuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(378, 208);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnTimKiem);
            this.Controls.Add(this.txtSoMay);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.txtSoKhung);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtBienSoXe);
            this.Controls.Add(this.lblBienSoXe);
            this.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmTimKiemLichSuBaoDuong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tìm kiếm lịch sử bảo dưỡng";
            this.Load += new System.EventHandler(this.frmTimKiemLichSuBaoDuong_Load);
            //this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmTimKiemLichSuBaoDuong_KeyDown);
            //Chu y cho nay
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.LabelX lblBienSoXe;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBienSoXe;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSoKhung;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSoMay;
        private DevComponents.DotNetBar.ButtonX btnTimKiem;
        private DevComponents.DotNetBar.ButtonX btnThoat;
    }
}