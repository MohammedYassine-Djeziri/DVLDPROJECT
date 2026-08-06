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

namespace DVLD_Project.LocalDrivingLicenseApplication.Forms
{
    public partial class NewLocalDrivingLicenseApplication : Form
    {
        int Per_ID = -1;

        clsApplication Application = clsApplication.GetEmptyApplication();
        clsLocalDrivingLicenseApp LDLApp = clsLocalDrivingLicenseApp.GetEmptyLocalDrivingLicenseApplication();
        public NewLocalDrivingLicenseApplication()
        {
            InitializeComponent();
        }

        private void lblTitle_Click(object sender, EventArgs e)
        {

        }

        private void NewLocalDrivingLicenseApplication_Load_1(object sender, EventArgs e)
        {
            tabControl1.Appearance = TabAppearance.FlatButtons;
            tabControl1.ItemSize = new Size(0, 1);
            tabControl1.SizeMode = TabSizeMode.Fixed;
            DataTable dt = clsLicenseClasses.ListLicenseClasses();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CB_LicenseClass.Items.Add(dt.Rows[i][1]);
            }
            CB_LicenseClass.SelectedIndex = 2;
            
            Application.ApplicationDate = DateTime.Now;
            Application.LastStatusDate = DateTime.Now;
            Application.UserId = clsCurrentUser.CurrentUser.UserID;
            Application.ApplicationStatus = 1;
            Application.ApplicationType = 1;
            Application.ApplicationFees = clsApplicationTypes.FindAppFeesByAppTypeID(1);
            lbl_Fees.Text = Application.ApplicationFees.ToString();
            lbl_UserID.Text = clsCurrentUser.CurrentUser.UserName;
            lbl_Date.Text = DateTime.Now.ToShortDateString().ToString();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(Application.IsLicenseClassAlreadyUsed(CB_LicenseClass.SelectedIndex+1))
            {
                MessageBox.Show("License Class Already Active Please Enter another License Class" , "Error" ,MessageBoxButtons.OK
                    , MessageBoxIcon.Exclamation);
            }
            else
            {
                if(Application.ApplicationID==-1)
                {
                    MessageBox.Show("Application added successfully");
                    lblTitle.Text = "Update Local Driving License Application";
                }
                else
                {
                    MessageBox.Show("Application Updated successfully");
                }
                Application.Save();
                LDLApp.LicenseClassID = CB_LicenseClass.SelectedIndex + 1;
                LDLApp.ApplicationID = Application.ApplicationID;
                LDLApp.Save();
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(Per_ID!=-1)
            {
                tabControl1.SelectedIndex = 1;
                Application.PersonID = showPersonalInfo1.Person_ID;
            }
            else
            {
                MessageBox.Show("Enter a Person");
            }
        }

        private void findPerson1_OnSearchCompleted(int obj)
        {
            Per_ID = obj;
            showPersonalInfo1.Person_ID= obj;
            showPersonalInfo1.RefreshInfo();
        }

        private void findPerson1_OnAddPersonCompleted(int obj)
        {
            Per_ID = obj;
            showPersonalInfo1.Person_ID = obj;
            showPersonalInfo1.RefreshInfo();
        }


        private void CB_LicenseClass_SelectedIndexChanged_1(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void showPersonalInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}