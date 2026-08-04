using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class ShowApplicationDetails : UserControl
    {
        public int ID=-1;
        clsApplication MyApplication = clsApplication.GetEmptyApplication();
        clsLocalDrivingLicenseApp LDApp= clsLocalDrivingLicenseApp.GetEmptyLocalDrivingLicenseApplication();
        public int PassedTest = 0;
        public ShowApplicationDetails()
        {
            InitializeComponent();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        public void RefreshInfo()
        {
            LDApp = clsLocalDrivingLicenseApp.FindLDLAppByLDLAppID(ID);
            MyApplication = clsApplication.FindApplicationByAppID(LDApp.ApplicationID);
            if (MyApplication.ApplicationID != -1)
            {
                lbl_AppID.Text = MyApplication.ApplicationID.ToString();
                lbl_Date.Text = MyApplication.ApplicationDate.ToString();
                switch (MyApplication.ApplicationStatus)
                {
                    case 1:
                        lbl_Status.Text = "New";
                        break;
                    case 2:
                        lbl_Status.Text = "Cancelled";
                        break;
                    case 3:
                        lbl_Status.Text = "Completed";
                        break;
                }
                lbl_StatusDate.Text = MyApplication.LastStatusDate.ToString();
                lbl_Fees.Text = MyApplication.ApplicationFees.ToString();
                lbl_Person.Text = clsPeoples.FindByPersonalID(MyApplication.PersonID).FullName();
                lbl_UserName.Text = clsUsers.FindUserByUserID(MyApplication.UserId).UserName;
                lbl_AppType.Text = clsApplicationTypes.GetApplicationTypeNameByAppTypeID(MyApplication.ApplicationType);
            }

            else if(MyApplication.ApplicationID == -1)
            {
                lbl_AppID.Text = "???";
                lbl_Date.Text = "???";
                lbl_Status.Text = "???";
                lbl_StatusDate.Text = "???";
                lbl_Fees.Text = "???";
                lbl_Person.Text = "???";
                lbl_UserName.Text = "???";
                lbl_AppType.Text = "???";
            }

            if (LDApp.LocalDrivingLicenseAppID != -1)
            {
                lbl_DLApp.Text = LDApp.LocalDrivingLicenseAppID.ToString();
                lbl_Tests.Text = PassedTest.ToString() + "/3";
                lbl_LicenseClass.Text = clsLicenseClasses.GetLicenseClassNameFromClassID(LDApp.LicenseClassID);
                
            }

            else if (LDApp.LocalDrivingLicenseAppID == -1)
            {
                lbl_DLApp.Text = "???";
                lbl_Tests.Text = "???";
                lbl_LicenseClass.Text = "???";
            }



        }

        private void ShowApplicationDetails_Load(object sender, EventArgs e)
        {
            LDApp = clsLocalDrivingLicenseApp.FindLDLAppByLDLAppID(ID);
            MyApplication = clsApplication.FindApplicationByAppID(LDApp.ApplicationID);
            RefreshInfo();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (MyApplication.ApplicationID != -1)
            {
                PersonInfo frm = new PersonInfo(MyApplication.PersonID);
                frm.Size = new Size(900, 500);
                frm.ShowDialog();
            }
        }
    }
}
