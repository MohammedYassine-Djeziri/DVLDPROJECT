using DVLD_Project.People.CustomControls;
using DVLD_Project.Properties;
namespace DVLD_Project.LocalDrivingLicenseApplication.Forms
{
    partial class NewLocalDrivingLicenseApplication
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.showPersonalInfo1 = new DVLD_Project.People.CustomControls.ShowPersonalInfo();
            this.findPerson1 = new DVLD_Project.People.CustomControls.FindPerson();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.CB_LicenseClass = new System.Windows.Forms.ComboBox();
            this.lbl_UserID = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.lbl_Fees = new System.Windows.Forms.Label();
            this.lbl_Date = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.lbl_DLAppID = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(311, 35);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(1118, 73);
            this.lblTitle.TabIndex = 46;
            this.lblTitle.Text = "New Local Driving Licence Application";
            this.lblTitle.Click += new System.EventHandler(this.lblTitle_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Century Gothic", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 135);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1802, 929);
            this.tabControl1.TabIndex = 45;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.btnNext);
            this.tabPage1.Controls.Add(this.showPersonalInfo1);
            this.tabPage1.Controls.Add(this.findPerson1);
            this.tabPage1.Location = new System.Drawing.Point(8, 58);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1786, 863);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Personal Info";
            // 
            // btnNext
            // 
            this.btnNext.BackColor = System.Drawing.Color.Transparent;
            this.btnNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNext.Font = new System.Drawing.Font("Century Gothic", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnNext.Image = global::DVLD_Project.Properties.Resources.Next_32;
            this.btnNext.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnNext.Location = new System.Drawing.Point(1482, 776);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(263, 71);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // showPersonalInfo1
            // 
            this.showPersonalInfo1.AutoSize = true;
            this.showPersonalInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showPersonalInfo1.Location = new System.Drawing.Point(54, 165);
            this.showPersonalInfo1.Margin = new System.Windows.Forms.Padding(7);
            this.showPersonalInfo1.Name = "showPersonalInfo1";
            this.showPersonalInfo1.Person_ID = -1;
            this.showPersonalInfo1.Size = new System.Drawing.Size(1709, 602);
            this.showPersonalInfo1.TabIndex = 3;
            this.showPersonalInfo1.Load += new System.EventHandler(this.showPersonalInfo1_Load);
            // 
            // findPerson1
            // 
            this.findPerson1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.findPerson1.Location = new System.Drawing.Point(75, 34);
            this.findPerson1.Margin = new System.Windows.Forms.Padding(16, 15, 16, 15);
            this.findPerson1.Name = "findPerson1";
            this.findPerson1.Size = new System.Drawing.Size(1670, 118);
            this.findPerson1.TabIndex = 2;
            this.findPerson1.OnSearchCompleted += new System.Action<int>(this.findPerson1_OnSearchCompleted);
            this.findPerson1.OnAddPersonCompleted += new System.Action<int>(this.findPerson1_OnAddPersonCompleted);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.CB_LicenseClass);
            this.tabPage2.Controls.Add(this.lbl_UserID);
            this.tabPage2.Controls.Add(this.panel1);
            this.tabPage2.Controls.Add(this.label11);
            this.tabPage2.Controls.Add(this.lbl_Fees);
            this.tabPage2.Controls.Add(this.lbl_Date);
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.panel11);
            this.tabPage2.Controls.Add(this.lbl_DLAppID);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.panel6);
            this.tabPage2.Controls.Add(this.panel3);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Location = new System.Drawing.Point(8, 58);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1786, 863);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Login Info";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // CB_LicenseClass
            // 
            this.CB_LicenseClass.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.CB_LicenseClass.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.CB_LicenseClass.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CB_LicenseClass.FormattingEnabled = true;
            this.CB_LicenseClass.Location = new System.Drawing.Point(560, 307);
            this.CB_LicenseClass.Name = "CB_LicenseClass";
            this.CB_LicenseClass.Size = new System.Drawing.Size(397, 47);
            this.CB_LicenseClass.TabIndex = 40;
            this.CB_LicenseClass.SelectedIndexChanged += new System.EventHandler(this.CB_LicenseClass_SelectedIndexChanged_1);
            // 
            // lbl_UserID
            // 
            this.lbl_UserID.AutoSize = true;
            this.lbl_UserID.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_UserID.Location = new System.Drawing.Point(553, 458);
            this.lbl_UserID.Name = "lbl_UserID";
            this.lbl_UserID.Size = new System.Drawing.Size(83, 42);
            this.lbl_UserID.TabIndex = 39;
            this.lbl_UserID.Text = "N/A";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::DVLD_Project.Properties.Resources.User_32__2;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(420, 456);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(48, 48);
            this.panel1.TabIndex = 38;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(178, 458);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(226, 42);
            this.label11.TabIndex = 35;
            this.label11.Text = "Created By:";
            // 
            // lbl_Fees
            // 
            this.lbl_Fees.AutoSize = true;
            this.lbl_Fees.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Fees.Location = new System.Drawing.Point(553, 384);
            this.lbl_Fees.Name = "lbl_Fees";
            this.lbl_Fees.Size = new System.Drawing.Size(83, 42);
            this.lbl_Fees.TabIndex = 34;
            this.lbl_Fees.Text = "N/A";
            // 
            // lbl_Date
            // 
            this.lbl_Date.AutoSize = true;
            this.lbl_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Date.Location = new System.Drawing.Point(553, 235);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.Size = new System.Drawing.Size(83, 42);
            this.lbl_Date.TabIndex = 32;
            this.lbl_Date.Text = "N/A";
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::DVLD_Project.Properties.Resources.Number_32;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Location = new System.Drawing.Point(421, 159);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(48, 48);
            this.panel5.TabIndex = 27;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(87, 235);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(317, 42);
            this.label10.TabIndex = 24;
            this.label10.Text = "Application Date:";
            // 
            // panel11
            // 
            this.panel11.BackgroundImage = global::DVLD_Project.Properties.Resources.Calendar_32_1_;
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.Location = new System.Drawing.Point(420, 233);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(48, 48);
            this.panel11.TabIndex = 32;
            // 
            // lbl_DLAppID
            // 
            this.lbl_DLAppID.AutoSize = true;
            this.lbl_DLAppID.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DLAppID.Location = new System.Drawing.Point(553, 159);
            this.lbl_DLAppID.Name = "lbl_DLAppID";
            this.lbl_DLAppID.Size = new System.Drawing.Size(83, 42);
            this.lbl_DLAppID.TabIndex = 31;
            this.lbl_DLAppID.Text = "N/A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(71, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(333, 42);
            this.label8.TabIndex = 30;
            this.label8.Text = "D.L ApplicationID:";
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::DVLD_Project.Properties.Resources.LocalDriving_License;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Location = new System.Drawing.Point(420, 307);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(48, 48);
            this.panel6.TabIndex = 29;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::DVLD_Project.Properties.Resources.money_32;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(421, 381);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(48, 48);
            this.panel3.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(82, 384);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(322, 42);
            this.label7.TabIndex = 26;
            this.label7.Text = "Application Fees:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(128, 309);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(276, 42);
            this.label6.TabIndex = 25;
            this.label6.Text = "License Class:";
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::DVLD_Project.Properties.Resources.Save_32_1_;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1580, 1080);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(226, 67);
            this.btnSave.TabIndex = 48;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32_1_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1305, 1080);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(226, 67);
            this.btnClose.TabIndex = 47;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // NewLocalDrivingLicenseApplication
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1823, 1183);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Name = "NewLocalDrivingLicenseApplication";
            this.Text = "NewLocalDrivingLicenceApplication";
            this.Load += new System.EventHandler(this.NewLocalDrivingLicenseApplication_Load_1);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private FindPerson findPerson1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.Button btnNext;
        private ShowPersonalInfo showPersonalInfo1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label lbl_DLAppID;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Label lbl_Fees;
        private System.Windows.Forms.Label lbl_Date;
        private System.Windows.Forms.Label lbl_UserID;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.ComboBox CB_LicenseClass;
    }
}