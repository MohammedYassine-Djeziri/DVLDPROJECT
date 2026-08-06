using DVLD_Project.Properties;
namespace DVLD_Project.License.CustomControls
{
    partial class ctrFilterLicenses
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
            this.Filter = new System.Windows.Forms.GroupBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.showLicenseInfo1 = new DVLD_Project.License.CustomControls.ShowLicenseInfo();
            this.Filter.SuspendLayout();
            this.SuspendLayout();
            // 
            // Filter
            // 
            this.Filter.Controls.Add(this.button1);
            this.Filter.Controls.Add(this.textBox1);
            this.Filter.Controls.Add(this.label1);
            this.Filter.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Filter.Location = new System.Drawing.Point(93, 36);
            this.Filter.Name = "Filter";
            this.Filter.Size = new System.Drawing.Size(1168, 143);
            this.Filter.TabIndex = 0;
            this.Filter.TabStop = false;
            this.Filter.Text = "Filter";
            this.Filter.Enter += new System.EventHandler(this.Filter_Enter);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.button1.BackgroundImage = global::DVLD_Project.Properties.Resources.License_View_32;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.Location = new System.Drawing.Point(1000, 24);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(137, 108);
            this.button1.TabIndex = 2;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.Location = new System.Drawing.Point(353, 45);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(496, 56);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 13.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(29, 54);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(204, 42);
            this.label1.TabIndex = 0;
            this.label1.Text = "LicenseID:";
            // 
            // showLicenseInfo1
            // 
            this.showLicenseInfo1.Location = new System.Drawing.Point(25, 212);
            this.showLicenseInfo1.Name = "showLicenseInfo1";
            this.showLicenseInfo1.Size = new System.Drawing.Size(1700, 726);
            this.showLicenseInfo1.TabIndex = 1;
            // 
            // ctrFilterLicenses
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.showLicenseInfo1);
            this.Controls.Add(this.Filter);
            this.Name = "ctrFilterLicenses";
            this.Size = new System.Drawing.Size(1761, 952);
            this.Load += new System.EventHandler(this.ctrFilterLicenses_Load);
            this.Filter.ResumeLayout(false);
            this.Filter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox Filter;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private ShowLicenseInfo showLicenseInfo1;
    }
}
