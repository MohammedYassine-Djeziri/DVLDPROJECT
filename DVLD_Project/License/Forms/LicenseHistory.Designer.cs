using DVLD_Project.License.CustomControls;
using DVLD_Project.People.CustomControls;
using DVLD_Project.Properties;
namespace DVLD_Project.License.Forms
{
    partial class LicenseHistory
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
            this.label7 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ctrLicenseHistory1 = new DVLD_Project.License.CustomControls.CtrLicenseHistory();
            this.showPersonalInfo1 = new DVLD_Project.People.CustomControls.ShowPersonalInfo();
            this.SuspendLayout();
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.label7.Location = new System.Drawing.Point(785, 18);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(548, 55);
            this.label7.TabIndex = 45;
            this.label7.Text = "License History Details:";
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = global::DVLD_Project.Properties.Resources.PersonLicenseHistory_512;
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(12, 247);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(372, 364);
            this.panel1.TabIndex = 47;
            // 
            // ctrLicenseHistory1
            // 
            this.ctrLicenseHistory1.Location = new System.Drawing.Point(78, 686);
            this.ctrLicenseHistory1.Name = "ctrLicenseHistory1";
            this.ctrLicenseHistory1.Size = new System.Drawing.Size(1984, 666);
            this.ctrLicenseHistory1.TabIndex = 49;
            // 
            // showPersonalInfo1
            // 
            this.showPersonalInfo1.AutoSize = true;
            this.showPersonalInfo1.Location = new System.Drawing.Point(390, 131);
            this.showPersonalInfo1.Name = "showPersonalInfo1";
            this.showPersonalInfo1.Person_ID = -1;
            this.showPersonalInfo1.Size = new System.Drawing.Size(1728, 594);
            this.showPersonalInfo1.TabIndex = 48;
            // 
            // LicenseHistory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2105, 1380);
            this.Controls.Add(this.ctrLicenseHistory1);
            this.Controls.Add(this.showPersonalInfo1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label7);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "LicenseHistory";
            this.Text = "LicenseHistory";
            this.Load += new System.EventHandler(this.LicenseHistory_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel1;
        private ShowPersonalInfo showPersonalInfo1;
        private CtrLicenseHistory ctrLicenseHistory1;
    }
}