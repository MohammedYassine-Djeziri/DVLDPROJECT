namespace DVLD_Project
{
    partial class PersonInfo
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
            this.btnClose = new System.Windows.Forms.Button();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.showPersonalInfo1 = new DVLD_Project.ShowPersonalInfo();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Image = global::DVLD_Project.Properties.Resources.Close_32_1_;
            this.btnClose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClose.Location = new System.Drawing.Point(1485, 758);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(226, 67);
            this.btnClose.TabIndex = 42;
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(1403, 183);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(244, 37);
            this.linkLabel1.TabIndex = 43;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Edit Person Info";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // showPersonalInfo1
            // 
            this.showPersonalInfo1.Location = new System.Drawing.Point(12, 12);
            this.showPersonalInfo1.Name = "showPersonalInfo1";
            this.showPersonalInfo1.Person_ID = -1;
            this.showPersonalInfo1.Size = new System.Drawing.Size(1725, 723);
            this.showPersonalInfo1.TabIndex = 0;
            // 
            // PersonInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1746, 855);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.showPersonalInfo1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "PersonInfo";
            this.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Text = "PersonInfo";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private ShowPersonalInfo showPersonalInfo1;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.LinkLabel linkLabel1;
    }
}