using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace DVLD_Project
{
    public partial class DetainLicenseForm : Form
    {

        int LicenseID = -1;
        public DetainLicenseForm()
        {
            InitializeComponent();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            if (TBNotes.Text != "")
            {
                if (Convert.ToSingle(TBNotes.Text) > 0)
                {
                    clsDetainedLicense DetLicenseObj = clsDetainedLicense.GetEmptyLicense();
                    DetLicenseObj.ApplicationID = -1;
                    DetLicenseObj.LicenseID = LicenseID;
                    DetLicenseObj.DetainDate = DateTime.Now;
                    DetLicenseObj.ReleaseDate = DateTime.Now.AddYears(-1);
                    DetLicenseObj.DetainedUserID = clsCurrentUser.CurrentUser.UserID;
                    DetLicenseObj.ReleasedUserID = -1;
                    DetLicenseObj.FineFees = Convert.ToSingle(TBNotes.Text);
                    DetLicenseObj.IsReleased = false;
                    DetLicenseObj.Save();
                    lblDetID.Text = DetLicenseObj.DetainedID.ToString();

                    btnIssue.Enabled = false;
                    linkLabel1.Enabled = true;
                    linkLabel2.Enabled = true;
                    MessageBox.Show("License has been detained");
                    ctrFilterLicenses1.DisableAll();
                }
            }
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrFilterLicenses1_OnFilterCompleted(int obj)
        {

            {
                btnIssue.Enabled = false;
                linkLabel1.Enabled = false;
                linkLabel2.Enabled = false;
                if (obj != -1)
                {
                    lblLicID.Text = obj.ToString(); 
                    LicenseID = obj;
                    clsLicenses MyLicense = clsLicenses.FindLicenseByLicenseID(LicenseID);
                    if (MyLicense.IsActive && (! clsLicenses.IsLicenseDetained(MyLicense.LicenseID)))
                    {
                        lbl_UserName.Text = clsCurrentUser.CurrentUser.UserName;   
                        btnIssue.Enabled = true;

                    }

                    else
                    {
                        MessageBox.Show("License is not active or already detained");
                    }
                }

                else
                {

                }
            }
        }

        private void TBNotes_TextChanged(object sender, EventArgs e)
        {
            if (TBNotes.Text != "")
            {

                if (!(int.TryParse(TBNotes.Text, out int value)))
                {
                    MessageBox.Show("you can not enter characters");
                    TBNotes.Text = string.Empty;
                }

            }
        }

        private void DetainLicenseForm_Load(object sender, EventArgs e)
        {
            lblDetDate.Text = DateTime.Now.ToShortDateString();
            lbl_UserName.Text = clsCurrentUser.CurrentUser.UserName;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ShowLicenseForm(LicenseID);
            frm.Size = new Size(900, 650);
            frm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory frm = new LicenseHistory((clsLicenses.FindLicenseByLicenseID(LicenseID)).DriverID);

            frm.Size = new Size(1100, 750);
            frm.ShowDialog();
        }
    }
}
