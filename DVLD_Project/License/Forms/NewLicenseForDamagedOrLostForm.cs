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
using DVLD_Project.Global;
using DVLD_Project.LocalDrivingLicense.Forms;

namespace DVLD_Project.License.Forms
{
    public partial class NewLicenseForDamagedOrLostForm : Form
    {

        int LicenseID = -1;
        int ReplacementLicenseID = -1;
        public NewLicenseForDamagedOrLostForm()
        {
            InitializeComponent();
        }

        private void rdBDamagLic_CheckedChanged(object sender, EventArgs e)
        {
            lblAppFees.Text = clsApplicationTypes.FindAppFeesByAppTypeID(4).ToString();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void rdBLostLic_CheckedChanged(object sender, EventArgs e)
        {
            lblAppFees.Text = clsApplicationTypes.FindAppFeesByAppTypeID(3).ToString();
        }

        private void NewLicenseForDamagedOrLostForm_Load(object sender, EventArgs e)
        {
            lblAppDate.Text = DateTime.Now.ToShortDateString();
            

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
                    if (MyLicense.IsActive)
                    {
                        lblAppDate.Text = DateTime.Now.ToShortDateString();
                        lblAppFees.Text = clsApplicationTypes.FindAppFeesByAppTypeID(3).ToString();
                        lblOldLicID.Text = MyLicense.LicenseID.ToString();
                        btnIssue.Enabled = true;

                    }

                    else
                    {
                        MessageBox.Show("License is not active");
                    }
                }

                else
                {

                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            {
                clsLicenses MyLicense = clsLicenses.FindLicenseByLicenseID(LicenseID);

                clsLicenses NewLicense = clsLicenses.GetEmptyLicense();
                clsApplication RenewApp = clsApplication.GetEmptyApplication();
                RenewApp.ApplicationFees = Convert.ToSingle(lblAppFees.Text);
                RenewApp.ApplicationStatus = 1;
                RenewApp.ApplicationDate = DateTime.Now;
                RenewApp.LastStatusDate = DateTime.Now;

                RenewApp.ApplicationType = 3;
                NewLicense.IssueReason = 3;
                if (rdBDamagLic.Checked)
                {
                    RenewApp.ApplicationType = 4;
                    NewLicense.IssueReason = 4;

                }
                RenewApp.UserId = clsCurrentUser.CurrentUser.UserID;
                RenewApp.PersonID = clsDriver.FindDriverByDriverID(MyLicense.DriverID).PersonID;
                RenewApp.Save();
                NewLicense.ApplicationID = RenewApp.ApplicationID;
                MyLicense.IsActive = false;
                MyLicense.Save();
                NewLicense.IssueDate =MyLicense.IssueDate;
                NewLicense.Notes = MyLicense.Notes;
                NewLicense.DriverID = MyLicense.DriverID;
                NewLicense.LicenseClassID = MyLicense.LicenseClassID;
                NewLicense.ExpirationDate = MyLicense.ExpirationDate;
                
                NewLicense.IsActive = true;
                NewLicense.PaidFees = clsLicenseClasses.FindLicenseFeesByLicenseClassID(NewLicense.LicenseClassID);
                NewLicense.UserID = clsCurrentUser.CurrentUser.UserID;
                NewLicense.Save();
                lblRenLicAppID.Text = RenewApp.ApplicationID.ToString();
                lblRenLicID.Text = NewLicense.LicenseID.ToString();
                ReplacementLicenseID = NewLicense.LicenseID;
                btnIssue.Enabled = false;
                linkLabel1.Enabled = true;
                linkLabel2.Enabled = true;
                MessageBox.Show("New License has been created");
                ctrFilterLicenses1.DisableAll();
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory frm = new LicenseHistory((clsLicenses.FindLicenseByLicenseID(ReplacementLicenseID)).DriverID);

            frm.Size = new Size(1100, 750);
            frm.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ShowLicenseForm(ReplacementLicenseID);
            frm.Size = new Size(900, 650);
            frm.ShowDialog();
        }
    }
    }
