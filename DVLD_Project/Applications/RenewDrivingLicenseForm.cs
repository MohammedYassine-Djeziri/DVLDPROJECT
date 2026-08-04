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

namespace DVLD_Project.Applications
{
    public partial class RenewDrivingLicenseForm : Form
    {


        int LicenseID = -1;

        int NewLicID = -1;
        public RenewDrivingLicenseForm()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void RenewDrivingLicenseForm_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblAppFees.Text = clsApplicationTypes.FindAppFeesByAppTypeID(2).ToString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lbl_UserName.Text = clsCurrentUser.CurrentUser.UserName;
        }

        private void ctrFilterLicenses1_OnFilterCompleted(int obj)
        {
            {
                btnIssue.Enabled = false;
                linkLabel1.Enabled = false;
                linkLabel2.Enabled = false;
                if (obj != -1)
                {
                    LicenseID = obj;
                    clsLicenses MyLicense = clsLicenses.FindLicenseByLicenseID(LicenseID);
                    if (MyLicense.IsActive && MyLicense.ExpirationDate < DateTime.Now)
                    {
                        lblAppDate.Text = DateTime.Now.ToShortDateString();
                        lblAppFees.Text = clsApplicationTypes.FindAppFeesByAppTypeID(2).ToString();
                        lblExpDate.Text = DateTime.Now.AddYears(clsLicenseClasses.GetLicenseValidityLengthFromClassID(clsLicenseClasses.GetLicenseClassNameFromClassID(MyLicense.LicenseClassID))).ToShortDateString();
                        lblIssueDate.Text = DateTime.Now.ToShortDateString();
                        lblLicFees.Text = clsLicenseClasses.FindLicenseFeesByLicenseClassID(MyLicense.LicenseClassID).ToString();
                        lblOldLicID.Text = MyLicense.LicenseID.ToString();
                        lblTotalFees.Text = (Convert.ToSingle(lblAppFees.Text) + Convert.ToSingle(lblLicFees.Text)).ToString();
                        btnIssue.Enabled = true;
                       
                    }

                    else
                    {
                        MessageBox.Show("License Exp date is not available or license is no active");
                    }
                }

                else
                {

                }

            }
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            {
                clsLicenses MyLicense = clsLicenses.FindLicenseByLicenseID(LicenseID);

                clsLicenses NewLicense = clsLicenses.GetEmptyLicense();
                clsApplication RenewApp = clsApplication.GetEmptyApplication();
                RenewApp.ApplicationFees = clsApplicationTypes.FindAppFeesByAppTypeID(2);
                RenewApp.ApplicationStatus = 1;
                RenewApp.ApplicationDate = DateTime.Now;
                RenewApp.LastStatusDate = DateTime.Now;
                RenewApp.ApplicationType = 2;
                RenewApp.UserId = clsCurrentUser.CurrentUser.UserID;
                RenewApp.PersonID = clsDriver.FindDriverByDriverID(MyLicense.DriverID).PersonID;
                RenewApp.Save();
                NewLicense.ApplicationID = RenewApp.ApplicationID;
                MyLicense.IsActive = false;
                MyLicense.Save();
                NewLicense.IssueDate = DateTime.Now;
                NewLicense.Notes = MyLicense.Notes;
                NewLicense.DriverID = MyLicense.DriverID;
                NewLicense.LicenseClassID = MyLicense.LicenseClassID;
                NewLicense.ExpirationDate = DateTime.Now.AddYears(clsLicenseClasses.GetLicenseValidityLengthFromClassID(clsLicenseClasses.GetLicenseClassNameFromClassID(MyLicense.LicenseClassID)));
                NewLicense.IssueReason = 2;
                NewLicense.IsActive = true;
                NewLicense.PaidFees = clsLicenseClasses.FindLicenseFeesByLicenseClassID(NewLicense.LicenseClassID);
                NewLicense.Notes = TBNotes.Text;
                NewLicense.UserID = clsCurrentUser.CurrentUser.UserID;
                NewLicense.Save();
                lblRenLicAppID.Text = RenewApp.ApplicationID.ToString();
                lblRenLicID.Text = NewLicense.LicenseID.ToString();
                NewLicID = NewLicense.LicenseID;
                btnIssue.Enabled = false;
                linkLabel1.Enabled = true;
                linkLabel2.Enabled = true;
                MessageBox.Show("New License has been created");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ShowLicenseForm(NewLicID);
            frm.Size = new Size(900, 650);
            frm.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory frm = new LicenseHistory((clsLicenses.FindLicenseByLicenseID(NewLicID)).DriverID);

            frm.Size = new Size(1100, 750);
            frm.ShowDialog();
        }
    }
}
