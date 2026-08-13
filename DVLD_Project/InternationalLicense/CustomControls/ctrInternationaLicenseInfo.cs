using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Project.Properties;

namespace DVLD_Project.InternationalLicense.CustomControls
{
    public partial class ctrInternationaLicenseInfo : UserControl
    {

        public int InterLicenseID = -1;
        clsInternationalLicense MyInternationalLicense = clsInternationalLicense.GetEmptyInternationalLicense();
        clsPeoples Person = clsPeoples.GetEmptyPerson();
        public ctrInternationaLicenseInfo()
        {
            InitializeComponent();
        }

        public void RefreshInfo()
        {
            
            MyInternationalLicense = clsInternationalLicense.FindLicenseByInterLicenseID(InterLicenseID);

            if (MyInternationalLicense.InternationalLicenseID != -1)
            {
                MessageBox.Show("I am in IF");
                Person = clsPeoples.FindByPersonalID(clsDriver.FindDriverByDriverID(MyInternationalLicense.DriverID).PersonID);

                lblInterLicID.Text = MyInternationalLicense.InternationalLicenseID.ToString();
                lblBirthDay.Text = Person.DateOfBirth.ToShortDateString();
                lbldriverID.Text = MyInternationalLicense.DriverID.ToString();
                lblGender.Text = clsPeoples.GetGenderFromCode(Person.PerID);
                lblIsActive.Text = "No";
                if (MyInternationalLicense.IsActive)
                {
                    lblIsActive.Text = "Yes";
                }
                lblExpDate.Text = MyInternationalLicense.ExpirationDate.ToShortDateString();
                lblname.Text = clsPeoples.GetPersonFullNameByPersonID(Person.PerID);
                lblNatno.Text = Person.NationalNub;

                lblIssueDate.Text = MyInternationalLicense.IssueDate.ToShortDateString();
               // MessageBox.Show(MyInternationalLicense.LicenseID.ToString());
                lblLicense.Text = MyInternationalLicense.LicenseID.ToString();
                lblAppID.Text = MyInternationalLicense.ApplicationID.ToString();
                if (Person.ImagePath != "")
                {
                    if (File.Exists(Person.ImagePath))
                    {
                        PctBoxImg.ImageLocation = @Person.ImagePath;
                    }
                }
                else
                {
                    if (Person.Gender == 1)
                    {
                        PctBoxImg.Image = Resources.Female_512;

                    }

                    else if (Person.Gender == 0)
                    {
                        PctBoxImg.Image = Resources.Male_512;
                    }
                    else
                    {
                        PctBoxImg.Image = Resources._19477_1;
                    }
                }
            }
            else
            {
                //MessageBox.Show("I am in ELSE");
                lblInterLicID.Text = "[???]";
                lblBirthDay.Text = "[???]";
                lbldriverID.Text = "[???]";
                lblGender.Text = "[???]";
                lblIsActive.Text = "[???]";
                lblIsActive.Text = "[???]";
                lblExpDate.Text = "[???]";
                lblname.Text = "[???]";
                lblNatno.Text = "[???]";
                lblIssueDate.Text = "[???]";
                lblLicense.Text = "[???]";
                lblAppID.Text = "[???]";
                PctBoxImg.Image = Resources.Male_512;
                
            }

        }
        private void ctrInternationaLicenseInfo_Load(object sender, EventArgs e)
        {
            RefreshInfo();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
