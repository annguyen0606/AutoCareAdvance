namespace AutoCareV2._0
{
    partial class frmServerConfig
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
            this.txtServerName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDatabaseUser = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDatabasePass = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtDatabaseName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // txtServerName
            // 
            // 
            // 
            // 
            this.txtServerName.Border.Class = "TextBoxBorder";
            this.txtServerName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtServerName.Location = new System.Drawing.Point(162, 34);
            this.txtServerName.Name = "txtServerName";
            this.txtServerName.Size = new System.Drawing.Size(248, 20);
            this.txtServerName.TabIndex = 0;
            // 
            // txtDatabaseUser
            // 
            // 
            // 
            // 
            this.txtDatabaseUser.Border.Class = "TextBoxBorder";
            this.txtDatabaseUser.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDatabaseUser.Location = new System.Drawing.Point(162, 80);
            this.txtDatabaseUser.Name = "txtDatabaseUser";
            this.txtDatabaseUser.Size = new System.Drawing.Size(248, 20);
            this.txtDatabaseUser.TabIndex = 1;
            // 
            // txtDatabasePass
            // 
            // 
            // 
            // 
            this.txtDatabasePass.Border.Class = "TextBoxBorder";
            this.txtDatabasePass.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDatabasePass.Location = new System.Drawing.Point(162, 126);
            this.txtDatabasePass.Name = "txtDatabasePass";
            this.txtDatabasePass.PasswordChar = '*';
            this.txtDatabasePass.Size = new System.Drawing.Size(248, 20);
            this.txtDatabasePass.TabIndex = 2;
            // 
            // txtDatabaseName
            // 
            // 
            // 
            // 
            this.txtDatabaseName.Border.Class = "TextBoxBorder";
            this.txtDatabaseName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtDatabaseName.Location = new System.Drawing.Point(162, 172);
            this.txtDatabaseName.Name = "txtDatabaseName";
            this.txtDatabaseName.Size = new System.Drawing.Size(248, 20);
            this.txtDatabaseName.TabIndex = 3;
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Location = new System.Drawing.Point(31, 33);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(75, 23);
            this.labelX1.TabIndex = 4;
            this.labelX1.Text = "Server Name";
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Location = new System.Drawing.Point(31, 79);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(75, 23);
            this.labelX2.TabIndex = 5;
            this.labelX2.Text = "DataBase User";
            // 
            // labelX3
            // 
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Location = new System.Drawing.Point(31, 125);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(110, 23);
            this.labelX3.TabIndex = 6;
            this.labelX3.Text = "DataBase Password";
            // 
            // labelX4
            // 
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Location = new System.Drawing.Point(31, 171);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(110, 23);
            this.labelX4.TabIndex = 7;
            this.labelX4.Text = "Database Name";
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSave.Location = new System.Drawing.Point(162, 231);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Location = new System.Drawing.Point(257, 231);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Cancel";
            // 
            // frmServerConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(466, 294);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.labelX4);
            this.Controls.Add(this.labelX3);
            this.Controls.Add(this.labelX2);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtDatabaseName);
            this.Controls.Add(this.txtDatabasePass);
            this.Controls.Add(this.txtDatabaseUser);
            this.Controls.Add(this.txtServerName);
            this.Name = "frmServerConfig";
            this.Text = "frmServerConfig";
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txtServerName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDatabaseUser;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDatabasePass;
        private DevComponents.DotNetBar.Controls.TextBoxX txtDatabaseName;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.ButtonX btnCancel;
    }
}