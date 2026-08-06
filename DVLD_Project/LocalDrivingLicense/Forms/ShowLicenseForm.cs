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

namespace DVLD_Project.LocalDrivingLicense.Forms
{
    public partial class ShowLicenseForm : Form
    {
        public int LicenseID = -1;
       

        public ShowLicenseForm(int LicID)
        {

            LicenseID = LicID;
            MessageBox.Show("license id in form == " + LicenseID.ToString());

            InitializeComponent();
        }

        private void ShowLicenseForm_Load(object sender, EventArgs e)
        {

        showLicenseInfo1.LicenseID = LicenseID;
        showLicenseInfo1.RefreshInfo();

        }

        private void showLicenseInfo1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
