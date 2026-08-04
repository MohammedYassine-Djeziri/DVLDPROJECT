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
    public partial class CtrLicenseHistory : UserControl
    {

        public int DriverID = -1;

        DataView List1 = new DataView();

        DataView List2 = new DataView();


        //clsInternationalLicense MyInterLicense = clsInternationalLicense.GetEmptyInternationalLicense();

        public CtrLicenseHistory()
        {
            InitializeComponent();
            if (DriverID != -1)
            {
                List1 = clsLicenses.ListLicensesByDriverID(DriverID).DefaultView;
                List2 = clsInternationalLicense.ListInternationalLicensesByDriverID(DriverID).DefaultView;
            }
        }

        public void RefreshInfo()
        {
            if (DriverID != -1)
            {
                List1 = clsLicenses.ListLicensesByDriverID(DriverID).DefaultView;
                List2 = clsInternationalLicense.ListInternationalLicensesByDriverID(DriverID).DefaultView;
                dataGridView1.DataSource = List1;
                dataGridView2.DataSource = List2;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void CtrLicenseHistory_Load(object sender, EventArgs e)
        {
            RefreshInfo();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
