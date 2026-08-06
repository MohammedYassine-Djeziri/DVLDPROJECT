using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.License.CustomControls
{
    public partial class ctrFilterLicenses : UserControl
    {

        public int LicenseID = -1;


        public event Action<int> OnFilterCompleted;
        protected virtual void FilterCompleted(int License_ID)
        {
            Action<int> handler = OnFilterCompleted;
            if (handler != null)
            {
                handler(LicenseID);
            }
            //if (OnSearchCompleted != null)
            //{
            //    SearchCompleted(Person.PerID);
            //}
        }

        
        public ctrFilterLicenses()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text != "")
            {
                if(clsLicenses.FindLicenseByLicenseID(Convert.ToInt32( textBox1.Text) ).LicenseID == -1   )
                {
                    MessageBox.Show("License does not exist with this ID");
                }

                else
                {
                    LicenseID = Convert.ToInt32(textBox1.Text);
                    showLicenseInfo1.LicenseID = LicenseID;
                    showLicenseInfo1.RefreshInfo();
                }
            }



                if (OnFilterCompleted != null)
                {
                    FilterCompleted(LicenseID);
                }
            

           
        }

        private void Filter_Enter(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (!(int.TryParse(textBox1.Text, out int value)))
            {
                MessageBox.Show("Id not exist");
                textBox1.Text = string.Empty;
            }
            
        }

        public void DisableAll()
        {
            textBox1.Enabled = false;
            button1 .Enabled = false;
        }

        private void ctrFilterLicenses_Load(object sender, EventArgs e)
        {

        }


        public void PreFilter(int License__ID)
        {
            LicenseID = License__ID;
            textBox1.Text = License__ID.ToString();
            showLicenseInfo1.LicenseID = LicenseID;
            showLicenseInfo1.RefreshInfo();
            if (OnFilterCompleted != null)
            {
                FilterCompleted(LicenseID);
            }
            this.DisableAll();
        }
    }
}
