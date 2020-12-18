namespace AutoCareV2._0
{
    partial class FrmLichSuTichDiem
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmLichSuTichDiem));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.cb_Type = new System.Windows.Forms.ComboBox();
            this.panelLichSu = new System.Windows.Forms.Panel();
            this.panelThemDiem = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTenKH = new System.Windows.Forms.TextBox();
            this.txtSDT = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_AddPoint = new System.Windows.Forms.Button();
            this.txtSoTien = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtQuyDoi = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtDiem = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.btnQuyDoi = new System.Windows.Forms.Button();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.diem = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.loai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ngayTao = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.panelLichSu.SuspendLayout();
            this.panelThemDiem.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.diem,
            this.loai,
            this.ngayTao});
            resources.ApplyResources(this.dataGridView1, "dataGridView1");
            this.dataGridView1.Name = "dataGridView1";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // cb_Type
            // 
            this.cb_Type.FormattingEnabled = true;
            resources.ApplyResources(this.cb_Type, "cb_Type");
            this.cb_Type.Name = "cb_Type";
            this.cb_Type.SelectedIndexChanged += new System.EventHandler(this.cb_Type_SelectedIndexChanged);
            // 
            // panelLichSu
            // 
            this.panelLichSu.Controls.Add(this.cb_Type);
            this.panelLichSu.Controls.Add(this.label1);
            resources.ApplyResources(this.panelLichSu, "panelLichSu");
            this.panelLichSu.Name = "panelLichSu";
            // 
            // panelThemDiem
            // 
            this.panelThemDiem.Controls.Add(this.label12);
            this.panelThemDiem.Controls.Add(this.label11);
            this.panelThemDiem.Controls.Add(this.btnQuyDoi);
            this.panelThemDiem.Controls.Add(this.label10);
            this.panelThemDiem.Controls.Add(this.txtDiem);
            this.panelThemDiem.Controls.Add(this.label9);
            this.panelThemDiem.Controls.Add(this.label8);
            this.panelThemDiem.Controls.Add(this.label7);
            this.panelThemDiem.Controls.Add(this.txtQuyDoi);
            this.panelThemDiem.Controls.Add(this.label6);
            this.panelThemDiem.Controls.Add(this.txtSoTien);
            this.panelThemDiem.Controls.Add(this.label5);
            this.panelThemDiem.Controls.Add(this.btn_AddPoint);
            this.panelThemDiem.Controls.Add(this.txtSDT);
            this.panelThemDiem.Controls.Add(this.label4);
            this.panelThemDiem.Controls.Add(this.txtTenKH);
            this.panelThemDiem.Controls.Add(this.label2);
            resources.ApplyResources(this.panelThemDiem, "panelThemDiem");
            this.panelThemDiem.Name = "panelThemDiem";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // txtTenKH
            // 
            resources.ApplyResources(this.txtTenKH, "txtTenKH");
            this.txtTenKH.Name = "txtTenKH";
            this.txtTenKH.ReadOnly = true;
            // 
            // txtSDT
            // 
            resources.ApplyResources(this.txtSDT, "txtSDT");
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.ReadOnly = true;
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // btn_AddPoint
            // 
            this.btn_AddPoint.BackColor = System.Drawing.Color.Transparent;
            this.btn_AddPoint.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_AddPoint.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btn_AddPoint.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.btn_AddPoint, "btn_AddPoint");
            this.btn_AddPoint.Name = "btn_AddPoint";
            this.btn_AddPoint.UseVisualStyleBackColor = false;
            this.btn_AddPoint.Click += new System.EventHandler(this.btn_AddPoint_Click);
            // 
            // txtSoTien
            // 
            resources.ApplyResources(this.txtSoTien, "txtSoTien");
            this.txtSoTien.Name = "txtSoTien";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // txtQuyDoi
            // 
            resources.ApplyResources(this.txtQuyDoi, "txtQuyDoi");
            this.txtQuyDoi.Name = "txtQuyDoi";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // txtDiem
            // 
            resources.ApplyResources(this.txtDiem, "txtDiem");
            this.txtDiem.Name = "txtDiem";
            this.txtDiem.ReadOnly = true;
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // btnQuyDoi
            // 
            this.btnQuyDoi.BackColor = System.Drawing.Color.Transparent;
            this.btnQuyDoi.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnQuyDoi.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnQuyDoi.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            resources.ApplyResources(this.btnQuyDoi, "btnQuyDoi");
            this.btnQuyDoi.Name = "btnQuyDoi";
            this.btnQuyDoi.UseVisualStyleBackColor = false;
            this.btnQuyDoi.Click += new System.EventHandler(this.btnQuyDoi_Click);
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // diem
            // 
            this.diem.DataPropertyName = "diem";
            resources.ApplyResources(this.diem, "diem");
            this.diem.Name = "diem";
            this.diem.ReadOnly = true;
            // 
            // loai
            // 
            this.loai.DataPropertyName = "loai";
            resources.ApplyResources(this.loai, "loai");
            this.loai.Name = "loai";
            this.loai.ReadOnly = true;
            // 
            // ngayTao
            // 
            this.ngayTao.DataPropertyName = "ngayTao";
            resources.ApplyResources(this.ngayTao, "ngayTao");
            this.ngayTao.Name = "ngayTao";
            this.ngayTao.ReadOnly = true;
            // 
            // FrmLichSuTichDiem
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelThemDiem);
            this.Controls.Add(this.panelLichSu);
            this.Controls.Add(this.dataGridView1);
            this.Name = "FrmLichSuTichDiem";
            this.Load += new System.EventHandler(this.FrmCauHinhTichDiem_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.panelLichSu.ResumeLayout(false);
            this.panelLichSu.PerformLayout();
            this.panelThemDiem.ResumeLayout(false);
            this.panelThemDiem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cb_Type;
        private System.Windows.Forms.Panel panelLichSu;
        private System.Windows.Forms.Panel panelThemDiem;
        private System.Windows.Forms.TextBox txtTenKH;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSDT;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_AddPoint;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtQuyDoi;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtSoTien;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDiem;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnQuyDoi;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.DataGridViewTextBoxColumn diem;
        private System.Windows.Forms.DataGridViewTextBoxColumn loai;
        private System.Windows.Forms.DataGridViewTextBoxColumn ngayTao;
    }
}