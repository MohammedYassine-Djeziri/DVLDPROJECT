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

namespace DVLD_Project.License.Forms
{
    public partial class LicenseHistory : Form
    {

        int DriverID = -1;
        public LicenseHistory(int driver_id)
        {
            DriverID = driver_id;

            InitializeComponent();
            ctrLicenseHistory1.DriverID = DriverID;
            showPersonalInfo1.Person_ID = clsDriver.FindDriverByDriverID(DriverID).PersonID;
        }

        private void showPersonalInfo1_Load(object sender, EventArgs e)
        {

        }

        private void LicenseHistory_Load(object sender, EventArgs e)
        {
            showPersonalInfo1.Person_ID = clsDriver.FindDriverByDriverID(DriverID).PersonID;
            showPersonalInfo1.RefreshInfo();
            ctrLicenseHistory1.DriverID = DriverID;
            ctrLicenseHistory1.RefreshInfo();
        }
    }
}
