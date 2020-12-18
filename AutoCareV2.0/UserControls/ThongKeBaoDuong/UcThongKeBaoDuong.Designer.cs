namespace AutoCareV2._0.UserControls.ThongKeBaoDuong
{
    partial class UcThongKeBaoDuong
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelLeft = new System.Windows.Forms.Panel();
            this.panelSpace = new System.Windows.Forms.Panel();
            this.panelContent = new System.Windows.Forms.Panel();
            this.panelTittleLeft = new System.Windows.Forms.Panel();
            this.labelTittle = new DevComponents.DotNetBar.LabelX();
            this.panelFunction = new System.Windows.Forms.Panel();
            this.groupBoxThongTinThongKe = new System.Windows.Forms.GroupBox();
            this.panelContentLeft = new System.Windows.Forms.Panel();
            this.panelLeft.SuspendLayout();
            this.panelContent.SuspendLayout();
            this.panelTittleLeft.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelLeft
            // 
            this.panelLeft.Controls.Add(this.panelContentLeft);
            this.panelLeft.Controls.Add(this.panelFunction);
            this.panelLeft.Controls.Add(this.panelTittleLeft);
            this.panelLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelLeft.Location = new System.Drawing.Point(0, 0);
            this.panelLeft.Name = "panelLeft";
            this.panelLeft.Size = new System.Drawing.Size(312, 510);
            this.panelLeft.TabIndex = 0;
            // 
            // panelSpace
            // 
            this.panelSpace.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelSpace.Location = new System.Drawing.Point(312, 0);
            this.panelSpace.Name = "panelSpace";
            this.panelSpace.Size = new System.Drawing.Size(14, 510);
            this.panelSpace.TabIndex = 1;
            // 
            // panelContent
            // 
            this.panelContent.Controls.Add(this.groupBoxThongTinThongKe);
            this.panelContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContent.Location = new System.Drawing.Point(326, 0);
            this.panelContent.Name = "panelContent";
            this.panelContent.Size = new System.Drawing.Size(618, 510);
            this.panelContent.TabIndex = 2;
            // 
            // panelTittleLeft
            // 
            this.panelTittleLeft.Controls.Add(this.labelTittle);
            this.panelTittleLeft.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTittleLeft.Location = new System.Drawing.Point(0, 0);
            this.panelTittleLeft.Name = "panelTittleLeft";
            this.panelTittleLeft.Size = new System.Drawing.Size(312, 48);
            this.panelTittleLeft.TabIndex = 0;
            // 
            // labelTittle
            // 
            // 
            // 
            // 
            this.labelTittle.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelTittle.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelTittle.Location = new System.Drawing.Point(3, 8);
            this.labelTittle.Name = "labelTittle";
            this.labelTittle.Size = new System.Drawing.Size(302, 31);
            this.labelTittle.TabIndex = 1;
            this.labelTittle.Text = "THỐNG KÊ BẢO DƯỠNG";
            this.labelTittle.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // panelFunction
            // 
            this.panelFunction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelFunction.Location = new System.Drawing.Point(0, 423);
            this.panelFunction.Name = "panelFunction";
            this.panelFunction.Size = new System.Drawing.Size(312, 87);
            this.panelFunction.TabIndex = 1;
            // 
            // groupBoxThongTinThongKe
            // 
            this.groupBoxThongTinThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxThongTinThongKe.Location = new System.Drawing.Point(0, 0);
            this.groupBoxThongTinThongKe.Name = "groupBoxThongTinThongKe";
            this.groupBoxThongTinThongKe.Size = new System.Drawing.Size(618, 510);
            this.groupBoxThongTinThongKe.TabIndex = 0;
            this.groupBoxThongTinThongKe.TabStop = false;
            this.groupBoxThongTinThongKe.Text = "Thông tin thống kê";
            // 
            // panelContentLeft
            // 
            this.panelContentLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelContentLeft.Location = new System.Drawing.Point(0, 48);
            this.panelContentLeft.Name = "panelContentLeft";
            this.panelContentLeft.Size = new System.Drawing.Size(312, 375);
            this.panelContentLeft.TabIndex = 2;
            // 
            // UcThongKeBaoDuong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelContent);
            this.Controls.Add(this.panelSpace);
            this.Controls.Add(this.panelLeft);
            this.Font = new System.Drawing.Font("Times New Roman", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "UcThongKeBaoDuong";
            this.Size = new System.Drawing.Size(944, 510);
            this.panelLeft.ResumeLayout(false);
            this.panelContent.ResumeLayout(false);
            this.panelTittleLeft.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelLeft;
        private System.Windows.Forms.Panel panelSpace;
        private System.Windows.Forms.Panel panelContent;
        private System.Windows.Forms.Panel panelTittleLeft;
        private DevComponents.DotNetBar.LabelX labelTittle;
        private System.Windows.Forms.Panel panelFunction;
        private System.Windows.Forms.GroupBox groupBoxThongTinThongKe;
        private System.Windows.Forms.Panel panelContentLeft;
    }
}
