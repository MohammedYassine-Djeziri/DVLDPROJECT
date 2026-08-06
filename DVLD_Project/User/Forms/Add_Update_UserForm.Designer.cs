using DVLD_Project.People.CustomControls;
using DVLD_Project.Properties;
namespace DVLD_Project.User.Forms
{
    partial class Add_Update_UserForm
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btnNext = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.TB_PassConf = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.lbl = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.TBUserName = new System.Windows.Forms.TextBox();
            this.TB_Pass = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.showPersonalInfo1 = new DVLD_Project.People.CustomControls.ShowPersonalInfo();
            this.findPerson1 = new DVLD_Project.People.CustomControls.FindPerson();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Century Gothic", 13.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(12, 134);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1802, 929);
            this.tabControl1.TabIndex = 0;
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
            this.btnNext.Location = new System.Drawing.Point(1498, 775);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(263, 71);
            this.btnNext.TabIndex = 4;
            this.btnNext.Text = "Next";
            this.btnNext.UseVisualStyleBackColor = false;
            this.btnNext.Click += new System.EventHandler(this.btnNext_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.checkBox1);
            this.tabPage2.Controls.Add(this.panel5);
            this.tabPage2.Controls.Add(this.TB_PassConf);
            this.tabPage2.Controls.Add(this.label10);
            this.tabPage2.Controls.Add(this.panel11);
            this.tabPage2.Controls.Add(this.lbl);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.TBUserName);
            this.tabPage2.Controls.Add(this.TB_Pass);
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
            this.tabPage2.Click += new System.EventHandler(this.tabPage2_Click);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("Century Gothic", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBox1.Location = new System.Drawing.Point(560, 500);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(167, 40);
            this.checkBox1.TabIndex = 38;
            this.checkBox1.Text = "Is Active";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // panel5
            // 
            this.panel5.BackgroundImage = global::DVLD_Project.Properties.Resources.png_transparent_computer_icons_user_profile_social_web_others_blue_social_media_desktop_wallpaper_thumbnail;
            this.panel5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel5.Location = new System.Drawing.Point(421, 149);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(55, 55);
            this.panel5.TabIndex = 27;
            // 
            // TB_PassConf
            // 
            this.TB_PassConf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_PassConf.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_PassConf.Location = new System.Drawing.Point(560, 391);
            this.TB_PassConf.Name = "TB_PassConf";
            this.TB_PassConf.PasswordChar = '*';
            this.TB_PassConf.Size = new System.Drawing.Size(317, 49);
            this.TB_PassConf.TabIndex = 36;
            this.TB_PassConf.Validating += new System.ComponentModel.CancelEventHandler(this.TB_PassConf_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(178, 228);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(226, 42);
            this.label10.TabIndex = 24;
            this.label10.Text = "User Name:";
            // 
            // panel11
            // 
            this.panel11.BackgroundImage = global::DVLD_Project.Properties.Resources.Person_32;
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.Location = new System.Drawing.Point(420, 228);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(55, 55);
            this.panel11.TabIndex = 32;
            // 
            // lbl
            // 
            this.lbl.AutoSize = true;
            this.lbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl.Location = new System.Drawing.Point(553, 159);
            this.lbl.Name = "lbl";
            this.lbl.Size = new System.Drawing.Size(83, 42);
            this.lbl.TabIndex = 31;
            this.lbl.Text = "N/A";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(199, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(205, 42);
            this.label8.TabIndex = 30;
            this.label8.Text = "    User ID:";
            // 
            // TBUserName
            // 
            this.TBUserName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBUserName.Location = new System.Drawing.Point(560, 234);
            this.TBUserName.Name = "TBUserName";
            this.TBUserName.Size = new System.Drawing.Size(317, 49);
            this.TBUserName.TabIndex = 35;
            this.TBUserName.Validating += new System.ComponentModel.CancelEventHandler(this.TBNATNUB_Validating);
            // 
            // TB_Pass
            // 
            this.TB_Pass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_Pass.Location = new System.Drawing.Point(560, 312);
            this.TB_Pass.Name = "TB_Pass";
            this.TB_Pass.PasswordChar = '*';
            this.TB_Pass.Size = new System.Drawing.Size(317, 49);
            this.TB_Pass.TabIndex = 34;
            this.TB_Pass.Validating += new System.ComponentModel.CancelEventHandler(this.TB_Pass_Validating);
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::DVLD_Project.Properties.Resources.Password_32;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Location = new System.Drawing.Point(420, 306);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(55, 55);
            this.panel6.TabIndex = 29;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::DVLD_Project.Properties.Resources.Password_32;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(421, 385);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(55, 55);
            this.panel3.TabIndex = 28;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(55, 385);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(349, 42);
            this.label7.TabIndex = 26;
            this.label7.Text = "Confirm Password:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(202, 306);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(202, 42);
            this.label6.TabIndex = 25;
            this.label6.Text = "Password:";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblTitle.Location = new System.Drawing.Point(668, 45);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(444, 73);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "Add New User";
            // 
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::DVLD_Project.Properties.Resources.Save_32_1_;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1580, 1079);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(226, 67);
            this.btnSave.TabIndex = 44;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32_1_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1305, 1079);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(226, 67);
            this.btnClose.TabIndex = 43;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // errorProvider1
            // 
            this.errorProvider1.ContainerControl = this;
            // 
            // errorProvider2
            // 
            this.errorProvider2.ContainerControl = this;
            // 
            // errorProvider3
            // 
            this.errorProvider3.ContainerControl = this;
            // 
            // showPersonalInfo1
            // 
            this.showPersonalInfo1.AutoSize = true;
            this.showPersonalInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.showPersonalInfo1.Location = new System.Drawing.Point(52, 163);
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
            this.findPerson1.Location = new System.Drawing.Point(73, 32);
            this.findPerson1.Margin = new System.Windows.Forms.Padding(16, 15, 16, 15);
            this.findPerson1.Name = "findPerson1";
            this.findPerson1.Size = new System.Drawing.Size(1670, 118);
            this.findPerson1.TabIndex = 2;
            this.findPerson1.OnSearchCompleted += new System.Action<int>(this.findPerson1_OnSearchCompleted_1);
            this.findPerson1.OnAddPersonCompleted += new System.Action<int>(this.findPerson1_OnAddPersonCompleted);
            this.findPerson1.Load += new System.EventHandler(this.findPerson1_Load);
            // 
            // Add_Update_UserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(1846, 1174);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.tabControl1);
            this.Name = "Add_Update_UserForm";
            this.Tag = "Add";
            this.Text = "AddNewUserForm";
            this.Load += new System.EventHandler(this.AddNewUserForm_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private ShowPersonalInfo showPersonalInfo1;
        private FindPerson findPerson1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button btnNext;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.TextBox TB_PassConf;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TextBox TBUserName;
        private System.Windows.Forms.TextBox TB_Pass;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
    }
}