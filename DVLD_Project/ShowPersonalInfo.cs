using DVLD_Project.Properties;
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

namespace DVLD_Project
{
    public partial class ShowPersonalInfo : UserControl
    {

        clsPeoples Person = clsPeoples.GetEmptyPerson();
        int PersonID = -1;

        public int Person_ID
        {
            get { return PersonID; }
            set
            {
                PersonID = value;
            }
        }


        public ShowPersonalInfo()
        {
            InitializeComponent();
        }

        public void RefreshInfo()
        {

            this.Person = clsPeoples.FindByPersonalID(PersonID);
            if (Person.PerID != -1)
            {
                lblname.Text = Person.FirstName + " " + Person.SecondName + " " + Person.ThirdName + " " + Person.LastName;
                lblNatno.Text = Person.NationalNub;
                lblPerID.Text = Person.PerID.ToString();
                lblGender.Text = clsPeoples.GetGenderFromCode(Person.Gender);
                lblAddress.Text = Person.Address;
                lblCountry.Text = clsPeoples.GetCountryFromCode(Person.Nationality);
                lblBirthDay.Text = Person.DateOfBirth.ToShortDateString();
                lblEmail.Text = Person.Email;
                lblPhone.Text = Person.Phone;
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
                lblname.Text = "[????]";
                lblNatno.Text = "[????]";
                lblPerID.Text = "[????]";
                lblGender.Text = "[????]";
                lblAddress.Text = "[????]";
                lblCountry.Text = "[????]";
                lblBirthDay.Text ="[????]";
                lblEmail.Text = "[????]";
                lblPhone.Text = "[????]";
                PctBoxImg.Image = Resources.Male_512;
        }

        }
        private void UserControl1_Load(object sender, EventArgs e)
        {
            Person = clsPeoples.FindByPersonalID(Person_ID);
            this.RefreshInfo();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (PersonID != -1)
            {
                Size size = new Size(900, 500);
                Add_Update_PersonForm frm = new Add_Update_PersonForm(PersonID);
                frm.Size = size;
                frm.FunRef += DoSomething;
                frm.ShowDialog();
            }
            else
            {
                MessageBox.Show("You can't edit Unknown person!");
            }
        }

        void DoSomething(int Id)
        {
            Person=clsPeoples.FindByPersonalID(Id);
            this.RefreshInfo();
        }
    }

}