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
using DVLD_Project.License.Forms;

namespace DVLD_Project.InternationalLicense.Forms
{
    public partial class InternationalLicenseAppForm : Form
    {
        int LicenseID = -1;
        
        public InternationalLicenseAppForm()
        {
            InitializeComponent();
        }

        private void InternationalLicenseAppForm_Load(object sender, EventArgs e)
        {

        }

        private void ctrFilterLicenses1_Load(object sender, EventArgs e)
        {

        }

        private void ctrFilterLicenses1_OnFilterCompleted(int obj)
        {
            btnIssue.Enabled = false;
            linkLabel1.Enabled = false;
            linkLabel2.Enabled = false;
            interLicAppInfo1.InterLicID = -1;
            interLicAppInfo1.RefreshInfo();
            if (obj != -1)
            {
                clsInternationalLicense MyInterLicense = clsInternationalLicense.FindLicenseByLicenseID(obj);
                clsLicenses MyLicense = clsLicenses.FindLicenseByLicenseID(obj);
                if (MyLicense.LicenseClassID == 3)
                {
                 
                    if (clsInternationalLicense.IsDriverAlreadyHaveInternationalLicense(MyLicense.DriverID) && MyInterLicense != null && MyInterLicense.InternationalLicenseID != -1 && (MyInterLicense.ExpirationDate > DateTime.Now))
                    {
                        MessageBox.Show("Person Already have a active International License with Id = " + MyInterLicense.InternationalLicenseID , "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        LicenseID = obj;
                        btnIssue.Enabled = true;

                    }
                }
                else
                {
                    //show an err with warning that only class 3 license can issue international license
                    MessageBox.Show("Only Class 3 License can issue International License" , "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Invalid License ID" , "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnIssue_Click(object sender, EventArgs e)
        {
            {
                clsApplication MyApplication = clsApplication.GetEmptyApplication();
                clsInternationalLicense MyInterLicense = clsInternationalLicense.FindLicenseByLicenseID(LicenseID);
                clsLicenses MyLicense = clsLicenses.FindLicenseByLicenseID(LicenseID);
                if (MyLicense.IsActive)
                {
                    MyApplication.ApplicationStatus = 1;
                    MyApplication.ApplicationFees = clsApplicationTypes.FindAppFeesByAppTypeID(6);
                    MyApplication.ApplicationType = 6;
                    MyApplication.UserId = clsCurrentUser.CurrentUser.UserID;
                    MyApplication.PersonID = clsDriver.FindDriverByDriverID(MyLicense.DriverID).PersonID;
                    MyApplication.Save();
                    MyInterLicense.ApplicationID = MyApplication.ApplicationID;
                    MyInterLicense.DriverID = MyLicense.DriverID;
                    MyInterLicense.LicenseID = MyLicense.LicenseID;
                    MyInterLicense.IsActive = true;
                    MyInterLicense.UserID = clsCurrentUser.CurrentUser.UserID;
                    MyInterLicense.ExpirationDate = DateTime.Now.AddYears(1);
                    MyInterLicense.Save();
                    
                    interLicAppInfo1.LicenseID = LicenseID;
                    interLicAppInfo1.ApplicationID = MyApplication.ApplicationID;
                    interLicAppInfo1.InterLicID = MyInterLicense.InternationalLicenseID;
                    interLicAppInfo1.RefreshInfo();
                    btnIssue.Enabled = false;
                    linkLabel1.Enabled = true;
                    linkLabel2.Enabled = true;
                    this.Refresh();
                }
                else
                {
                    MessageBox.Show("You have an inactive license!", "warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LicenseHistory frm = new LicenseHistory(clsLicenses.FindLicenseByLicenseID(LicenseID).DriverID);

            frm.Size = new Size(1100, 750);
            frm.ShowDialog();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Form frm = new ShowInternationalLicenseForm(clsInternationalLicense.FindLicenseByLicenseID(LicenseID).InternationalLicenseID);
            frm.Size = new Size(900, 900);
            frm.ShowDialog();
        }
    }
}
