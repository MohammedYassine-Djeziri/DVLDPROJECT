using DVLD_Project.Properties;
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
using System.IO;

namespace DVLD_Project
{
    public partial class ShowLicenseInfo : UserControl
    {
        public int LicenseID = -1;
        clsLicenses MyLicense = clsLicenses.GetEmptyLicense();
        clsDriver MyDriver = clsDriver.GetEmptyDriver();
        clsPeoples Person = clsPeoples.GetEmptyPerson();
        public ShowLicenseInfo()
        {
           
            //MyLicense = clsLicenses.FindLicenseByLicenseID(LicenseID);
            //MyDriver = clsDriver.FindDriverByDriverID(MyLicense.DriverID);
            //Person = clsPeoples.FindByPersonalID(MyDriver.PersonID);
            ////MessageBox.Show(MyLicense.LicenseID.ToString());
            ////MessageBox.Show(MyDriver.PersonID.ToString());
            InitializeComponent();
        }

        public void RefreshInfo()
        {
            //MessageBox.Show(LicenseID.ToString());
            MyLicense = clsLicenses.FindLicenseByLicenseID(LicenseID);
            if (MyLicense.LicenseID != -1)
            {
                //MessageBox.Show("I am in IF");
                MyDriver = clsDriver.FindDriverByDriverID(MyLicense.DriverID);
                Person = clsPeoples.FindByPersonalID(MyDriver.PersonID);

                lblClass.Text = clsLicenseClasses.GetLicenseClassNameFromClassID(
                   MyLicense.LicenseClassID);
                lblBirthDay.Text = Person.DateOfBirth.ToShortDateString();
                lbldriverID.Text = MyDriver.DriverID.ToString();
                lblGender.Text = clsPeoples.GetGenderFromCode(MyDriver.PersonID);
                lblIsActive.Text = "No";
                lblDet.Text = "No";
                if (MyLicense.IsActive)
                {
                    lblIsActive.Text = "Yes";
                }
                if(clsLicenses.IsLicenseDetained(LicenseID))
                {
                    lblDet.Text = "Yes";
                }
                lblExpDate.Text = MyLicense.ExpirationDate.ToShortDateString();
                lblname.Text = clsPeoples.GetPersonFullNameByPersonID(Person.PerID);
                lblNatno.Text = Person.NationalNub;

                lblNotes.Text = MyLicense.Notes;
                if (lblNotes.Text == "")
                {
                    lblNotes.Text = "No Notes";
                }
                lblIssueDate.Text = MyLicense.IssueDate.ToShortDateString();
                lblLicense.Text = MyLicense.LicenseID.ToString();
                lblIssueReason.Text = clsLicenses.GetIssueReasonByCode(MyLicense.IssueReason);
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
                lblBirthDay.Text = "[???]";
                lbldriverID.Text = "[???]";
                lblGender.Text = "[???]";
                lblIsActive.Text = "[???]";
                lblIsActive.Text = "[???]";
                lblDet.Text = "[???]";
                lblExpDate.Text = "[???]";
                lblname.Text = "[???]";
                lblNatno.Text = "[???]";
                lblNotes.Text = "[???]";
                lblNotes.Text = "[???]";
                lblIssueDate.Text = "[???]";
                lblLicense.Text = "[???]";
                lblIssueReason.Text = "[???]";
                PctBoxImg.Image = Resources.Male_512;
            }

        }
        private void lblDet_Click(object sender, EventArgs e)
        {

        }

        private void panel13_Paint(object sender, PaintEventArgs e)
        {

        }

        private void ShowLicenseInfo_Load(object sender, EventArgs e)
        {
            RefreshInfo();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
    }
}
