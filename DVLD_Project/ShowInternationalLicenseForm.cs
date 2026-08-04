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
    public partial class ShowInternationalLicenseForm : Form
    {

        public int InterLicenseID = -1;

        public ShowInternationalLicenseForm(int interLicenseID)
        {
            MessageBox.Show(interLicenseID.ToString());
            InterLicenseID = interLicenseID;
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowInternationalLicenseForm_Load(object sender, EventArgs e)
        {
            ctrInternationaLicenseInfo1.InterLicenseID = InterLicenseID;
            ctrInternationaLicenseInfo1.RefreshInfo();
        }
    }
}
