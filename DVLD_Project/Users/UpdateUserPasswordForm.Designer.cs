namespace DVLD_Project.Users
{
    partial class UpdateUserPasswordForm
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
            this.TB_PassConf = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.TBCurrentPassword = new System.Windows.Forms.TextBox();
            this.TB_Pass = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.errorProvider1 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider2 = new System.Windows.Forms.ErrorProvider(this.components);
            this.errorProvider3 = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnSave = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.userInformation1 = new DVLD_Project.UserInformation();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).BeginInit();
            this.SuspendLayout();
            // 
            // TB_PassConf
            // 
            this.TB_PassConf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_PassConf.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_PassConf.Location = new System.Drawing.Point(606, 998);
            this.TB_PassConf.Name = "TB_PassConf";
            this.TB_PassConf.PasswordChar = '*';
            this.TB_PassConf.Size = new System.Drawing.Size(317, 49);
            this.TB_PassConf.TabIndex = 45;
            this.TB_PassConf.Validating += new System.ComponentModel.CancelEventHandler(this.TB_PassConf_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.Location = new System.Drawing.Point(106, 847);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(344, 42);
            this.label10.TabIndex = 37;
            this.label10.Text = "Current Password:";
            // 
            // TBCurrentPassword
            // 
            this.TBCurrentPassword.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TBCurrentPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TBCurrentPassword.Location = new System.Drawing.Point(606, 841);
            this.TBCurrentPassword.Name = "TBCurrentPassword";
            this.TBCurrentPassword.PasswordChar = '*';
            this.TBCurrentPassword.Size = new System.Drawing.Size(317, 49);
            this.TBCurrentPassword.TabIndex = 44;
            this.TBCurrentPassword.Validating += new System.ComponentModel.CancelEventHandler(this.TBCurrentPassword_Validating);
            // 
            // TB_Pass
            // 
            this.TB_Pass.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.TB_Pass.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_Pass.Location = new System.Drawing.Point(606, 919);
            this.TB_Pass.Name = "TB_Pass";
            this.TB_Pass.PasswordChar = '*';
            this.TB_Pass.Size = new System.Drawing.Size(317, 49);
            this.TB_Pass.TabIndex = 43;
            this.TB_Pass.Validating += new System.ComponentModel.CancelEventHandler(this.TB_Pass_Validating);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(101, 992);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(349, 42);
            this.label7.TabIndex = 39;
            this.label7.Text = "Confirm Password:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(159, 919);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(291, 42);
            this.label6.TabIndex = 38;
            this.label6.Text = "New Password:";
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
            // btnSave
            // 
            this.btnSave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSave.Image = global::DVLD_Project.Properties.Resources.Save_32_1_;
            this.btnSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSave.Location = new System.Drawing.Point(1543, 1157);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(226, 67);
            this.btnSave.TabIndex = 47;
            this.btnSave.Text = "Save";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32_1_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1268, 1157);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(226, 67);
            this.btnClose.TabIndex = 46;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // panel11
            // 
            this.panel11.BackgroundImage = global::DVLD_Project.Properties.Resources.Number_32;
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel11.Location = new System.Drawing.Point(466, 835);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(55, 55);
            this.panel11.TabIndex = 42;
            // 
            // panel6
            // 
            this.panel6.BackgroundImage = global::DVLD_Project.Properties.Resources.Password_32;
            this.panel6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel6.Location = new System.Drawing.Point(466, 913);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(55, 55);
            this.panel6.TabIndex = 41;
            // 
            // panel3
            // 
            this.panel3.BackgroundImage = global::DVLD_Project.Properties.Resources.Password_32;
            this.panel3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel3.Location = new System.Drawing.Point(467, 992);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(55, 55);
            this.panel3.TabIndex = 40;
            // 
            // userInformation1
            // 
            this.userInformation1.Location = new System.Drawing.Point(79, 12);
            this.userInformation1.Name = "userInformation1";
            this.userInformation1.Size = new System.Drawing.Size(1740, 798);
            this.userInformation1.TabIndex = 0;
            // 
            // UpdateUserPasswordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1871, 1262);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.TB_PassConf);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.panel11);
            this.Controls.Add(this.TBCurrentPassword);
            this.Controls.Add(this.TB_Pass);
            this.Controls.Add(this.panel6);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.userInformation1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "UpdateUserPasswordForm";
            this.Text = "UpdateUserPasswordForm";
            this.Load += new System.EventHandler(this.UpdateUserPasswordForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider3)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UserInformation userInformation1;
        private System.Windows.Forms.TextBox TB_PassConf;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.TextBox TBCurrentPassword;
        private System.Windows.Forms.TextBox TB_Pass;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.ErrorProvider errorProvider1;
        private System.Windows.Forms.ErrorProvider errorProvider2;
        private System.Windows.Forms.ErrorProvider errorProvider3;
    }
}