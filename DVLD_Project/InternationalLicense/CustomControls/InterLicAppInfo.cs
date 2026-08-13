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

namespace DVLD_Project.InternationalLicense.CustomControls
{
    public partial class InterLicAppInfo : UserControl
    {

        public int InterLicID = -1;
        public int LicenseID = -1;
        public int ApplicationID = -1;
        public InterLicAppInfo()
        {
            InitializeComponent();
        }

        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        public void RefreshInfo()
        {

            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblInterAppID.Text = "[???]";
            lblInterLicID.Text = "[???]";
            lbl_Fees.Text = clsApplicationTypes.FindAppFeesByAppTypeID(6).ToString();
            lbl_UserName.Text = clsCurrentUser.CurrentUser.UserName;
            lblLicenseID.Text = "[???]";

            if (LicenseID!=-1)
            {
                lblLicenseID.Text = LicenseID.ToString();
                

            }
            if(ApplicationID!=-1) 
                {
                    lblInterAppID.Text = ApplicationID.ToString();
                    
                }
            if (InterLicID!=-1)
                    {
                        lblInterLicID.Text = InterLicID.ToString(); 
                    }
            
        }

        private void InterLicAppInfo_Load(object sender, EventArgs e)
        {

            lblAppDate.Text = DateTime.Now.ToShortDateString();
            lblIssueDate.Text = DateTime.Now.ToShortDateString();
            lblExpDate.Text = DateTime.Now.AddYears(1).ToShortDateString();
            lblInterAppID.Text = "[???]";
            lblInterLicID.Text = "[???]";
            lbl_Fees.Text = clsApplicationTypes.FindAppFeesByAppTypeID(6).ToString();
            lbl_UserName.Text = clsCurrentUser.CurrentUser.UserName;
            lblLicenseID.Text = "[???]";
        }
    }
}