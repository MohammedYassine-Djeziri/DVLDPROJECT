using DVLD_Project.InternationalLicense.CustomControls;
using DVLD_Project.Properties;
namespace DVLD_Project.InternationalLicense.Forms
{
    partial class ShowInternationalLicenseForm
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
            this.lbl_TestType = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.ctrInternationaLicenseInfo1 = new DVLD_Project.InternationalLicense.CustomControls.ctrInternationaLicenseInfo();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_TestType
            // 
            this.lbl_TestType.AutoSize = true;
            this.lbl_TestType.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_TestType.ForeColor = System.Drawing.Color.Red;
            this.lbl_TestType.Location = new System.Drawing.Point(504, 240);
            this.lbl_TestType.Name = "lbl_TestType";
            this.lbl_TestType.Size = new System.Drawing.Size(734, 55);
            this.lbl_TestType.TabIndex = 48;
            this.lbl_TestType.Text = "Driver International License Info";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::DVLD_Project.Properties.Resources.LicenseView_400;
            this.pictureBox1.Location = new System.Drawing.Point(711, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(317, 198);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 47;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::DVLD_Project.Properties.Resources.Close_32_1_;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(1481, 903);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(245, 66);
            this.button1.TabIndex = 46;
            this.button1.Text = "Close";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ctrInternationaLicenseInfo1
            // 
            this.ctrInternationaLicenseInfo1.Location = new System.Drawing.Point(50, 311);
            this.ctrInternationaLicenseInfo1.Name = "ctrInternationaLicenseInfo1";
            this.ctrInternationaLicenseInfo1.Size = new System.Drawing.Size(1676, 548);
            this.ctrInternationaLicenseInfo1.TabIndex = 49;
            // 
            // ShowInternationalLicenseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1774, 1030);
            this.Controls.Add(this.ctrInternationaLicenseInfo1);
            this.Controls.Add(this.lbl_TestType);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.button1);
            this.Name = "ShowInternationalLicenseForm";
            this.Text = "ShowInternationalLicenseForm";
            this.Load += new System.EventHandler(this.ShowInternationalLicenseForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lbl_TestType;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button button1;
        private ctrInternationaLicenseInfo ctrInternationaLicenseInfo1;
    }
}