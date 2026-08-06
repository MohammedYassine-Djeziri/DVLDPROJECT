using DVLD_Project.Global.Forms;
using DVLD_Project.People.CustomControls;
namespace DVLD_Project.People.Forms
{
    partial class UpdatePersonForm
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
        /// 
         #endregion

        private Add_UpdatePerson add_UpdatePerson1;
        private void InitializeComponent()
        {
            this.add_UpdatePerson1 = new DVLD_Project.People.CustomControls.Add_UpdatePerson();
            this.SuspendLayout();
            // 
            // add_UpdatePerson1
            // 
            this.add_UpdatePerson1.Location = new System.Drawing.Point(58, 23);
            this.add_UpdatePerson1.Name = "add_UpdatePerson1";
            this.add_UpdatePerson1.Person_ID = -1;
            this.add_UpdatePerson1.Size = new System.Drawing.Size(1589, 812);
            this.add_UpdatePerson1.TabIndex = 0;
            this.add_UpdatePerson1.Load += new System.EventHandler(this.add_UpdatePerson1_Load);
            // 
            // UpdatePersonForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1717, 893);
            this.Controls.Add(this.add_UpdatePerson1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "UpdatePersonForm";
            this.Text = "Form2";
            this.Load += new System.EventHandler(this.Form2_Load);
            this.ResumeLayout(false);

        }

       
    }
}