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

namespace DVLD_Project
{
    public partial class ReleaseDetainedLicenseForm : Form
    {

        int LicenseID = -1 ;
        public ReleaseDetainedLicenseForm()
        {
            InitializeComponent();
        }

        public void ReleaseLicense(int License__ID)
        {
            LicenseID = License__ID;
            ctrFilterLicenses1.PreFilter(LicenseID);

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
                    if (clsLicenses.IsLicenseDetained(MyLicense.LicenseID))
                    {
                        lbl_UserName.Text = clsCurrentUser.CurrentUser.UserName;
                        btnIssue.Enabled = true;
                        lblAppFees.Text = clsApplicationTypes.FindAppFeesByAppTypeID(5).ToString();
                        clsDetainedLicense DetLicObj = clsDetainedLicense.FindDetainedLicenseByLicenseID(LicenseID);
                        lblDetDate.Text = DetLicObj.DetainDate.ToShortDateString();
                        lblDetID.Text = DetLicObj.DetainedID.ToString();
                        lblFees.Text = DetLicObj.FineFees.ToString();
                        lblTotalFees.Text = (Convert.ToSingle(lblFees.Text) + Convert.ToSingle(lblAppFees.Text)).ToString();


                    }

                    else
                    {
                        MessageBox.Show("License is not detained");
                    }
                }

                else
                {

                }
                
            }
        }

        private void ReleaseDestainedLicenseForm_Load(object sender, EventArgs e)
        {
            lblDetDate.Text = DateTime.Now.ToShortDateString();
            lbl_UserName.Text = clsCurrentUser.CurrentUser.UserName;

        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            clsDetainedLicense DetLicObj = clsDetainedLicense.FindDetainedLicenseByLicenseID(LicenseID);

            clsApplication NewApp = clsApplication.GetEmptyApplication();
            NewApp.ApplicationDate = DateTime.Now;
            NewApp.ApplicationStatus = 1;
            NewApp.ApplicationFees = clsApplicationTypes.FindAppFeesByAppTypeID(5);
            NewApp.ApplicationType = 5;
            NewApp.UserId = clsCurrentUser.CurrentUser.UserID;
            NewApp.PersonID = clsDriver.FindDriverByDriverID(clsLicenses.FindLicenseByLicenseID(LicenseID).DriverID).PersonID;
            NewApp.LastStatusDate = DateTime.Now;
            NewApp.Save();
            DetLicObj.ApplicationID = NewApp.ApplicationID;
            DetLicObj.LicenseID = LicenseID;
            DetLicObj.ReleaseDate = DateTime.Now;
            DetLicObj.ReleasedUserID = clsCurrentUser.CurrentUser.UserID;
            DetLicObj.IsReleased = true;
            DetLicObj.Save();
            lblAppID.Text = NewApp.ApplicationID.ToString();

            btnIssue.Enabled = false;
            linkLabel1.Enabled = true;
            linkLabel2.Enabled = true;
            MessageBox.Show("License has been Released");
            ctrFilterLicenses1.DisableAll();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
