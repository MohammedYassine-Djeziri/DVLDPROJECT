namespace DVLD_Project.Users
{
    partial class ShowUserInfo
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
            this.userInformation1 = new DVLD_Project.UserInformation();
            this.SuspendLayout();
            // 
            // userInformation1
            // 
            this.userInformation1.Location = new System.Drawing.Point(37, 12);
            this.userInformation1.Name = "userInformation1";
            this.userInformation1.Size = new System.Drawing.Size(1724, 814);
            this.userInformation1.TabIndex = 0;
            // 
            // ShowUserInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1817, 882);
            this.Controls.Add(this.userInformation1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ShowUserInfo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.WindowsDefaultBounds;
            this.Text = "ShowUserInfo";
            this.Load += new System.EventHandler(this.ShowUserInfo_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UserInformation userInformation1;
    }
}