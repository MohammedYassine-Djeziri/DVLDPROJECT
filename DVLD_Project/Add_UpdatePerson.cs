using DVLD_Project.Properties;
using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project
{
    public partial class Add_UpdatePerson : UserControl
    {
        clsPeoples Person=clsPeoples.GetEmptyPerson();
        int PersonID = -1;

        public int Person_ID
        {
            get { return PersonID; }
            set
            {
                PersonID = value;
            }
        }
        public Add_UpdatePerson()
        {  
            InitializeComponent();
        }

        public void RefreshInfo()
        {
            Person = clsPeoples.FindByPersonalID(PersonID);

            if (Person != null)
            {
                Lbl_Add_Edit.Text = "Update Person";
                lblPerID.Text = PersonID.ToString();
                TB_FN.Text = Person.FirstName;
                TB_SN.Text = Person.SecondName;
                TB_TN.Text = Person.ThirdName;
                TB_LN.Text = Person.LastName;
                TBEMAIL.Text = Person.Email;
                TBPHONE.Text = Person.Phone;
                TB_ADDRESS.Text = Person.Address;
                TBNATNUB.Text = Person.NationalNub;
                dateTimePicker1.Value = Person.DateOfBirth;
                CB_GENDER.SelectedIndex = Person.Gender;
                CB_COUNTRY.SelectedIndex = Person.Nationality;
                if (Person.ImagePath != "")
                {
                    openFileDialog1.FileName = Person.ImagePath;
                    panelImage.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
                    linklbl_Remove_Img.Visible = true;
                }

                else
                {
                    if (CB_GENDER.SelectedIndex == 1)
                    {
                        panelImage.BackgroundImage = Resources.Female_512;

                    }

                    else if (CB_GENDER.SelectedIndex == 0)
                    {
                        panelImage.BackgroundImage = Resources.Male_512;
                    }
                    else
                    {
                        panelImage.BackgroundImage = Resources._19477_1;
                    }
                }

            }
            else
            {
                Person=clsPeoples.GetEmptyPerson();
            }
        }


        private void linklblSetImg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
          openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "" && openFileDialog1.FileName != "openFileDialog1" && Person.ImagePath=="")
            {
                panelImage.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
                linklbl_Remove_Img.Visible = true;
                Guid guid = Guid.NewGuid();
                File.Copy(openFileDialog1.FileName, @"C:\Users\mohya\source\repos\ProjectBackUp\DVLD_Project\ImageCopy\" + guid.ToString() + ".png");
                Person.ImagePath = @"C:\Users\mohya\source\repos\ProjectBackUp\DVLD_Project\ImageCopy\" + guid.ToString()+".png";
            }

            else
            {
                panelImage.BackgroundImage = Image.FromFile(openFileDialog1.FileName);
                linklbl_Remove_Img.Visible = true;
                string temp = Person.ImagePath;
                File.Delete(@temp);
                File.Copy(openFileDialog1.FileName, temp);

            }

        }

        private void Add_UpdatePerson_Load(object sender, EventArgs e)
        {
            dateTimePicker1.MaxDate = new DateTime(DateTime.Now.Year - 18, DateTime.Now.Month, DateTime.Now.Day);
            DataTable dt = clsPeoples.ListCountries();
            for(int i =0; i < dt.Rows.Count; i++)
            {
                CB_COUNTRY.Items.Add(dt.Rows[i][1]);
            }

            this.RefreshInfo();

        }

        private void linklbl_Remove_Img_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            panelImage.BackgroundImage = null;
            openFileDialog1.FileName = "";
            System.Threading.Thread.Sleep(1000); // Sleep for 1 second
            File.Delete((@Person.ImagePath));
            Person.ImagePath = "";
            if (CB_GENDER.SelectedIndex == 1)
            {
                panelImage.BackgroundImage = Resources.Female_512;

            }

            else if (CB_GENDER.SelectedIndex == 0)
            {
                panelImage.BackgroundImage = Resources.Male_512;
            }
            else
            {
                panelImage.BackgroundImage = Resources._19477_1;
            }
            linklbl_Remove_Img.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        { 
            if (TBNATNUB.Text !="" && TB_FN.Text != "" && TB_SN.Text != "" &&  TB_LN.Text != "" && 
                TBPHONE.Text != ""&& CB_COUNTRY.SelectedItem.ToString() != "" && CB_GENDER.SelectedItem.ToString() != "" &&
                    TB_ADDRESS.Text != "" )
            {
                //if (Person == null)
                //{
                //    Person = clsPeoples.GetEmptyPerson();
                //}
                Person.NationalNub = TBNATNUB.Text;
                Person.FirstName=  TB_FN.Text;
                Person.SecondName = TB_SN.Text;
                Person.ThirdName =  TB_TN.Text;
                Person.LastName =  TB_LN.Text;
                Person.Phone =  TBPHONE.Text;
                Person.Email =  TBEMAIL.Text;
                Person.Nationality =  CB_COUNTRY.SelectedIndex;
                Person.DateOfBirth =  dateTimePicker1.Value;
                Person.Gender =  CB_GENDER.SelectedIndex;
                Person.Address =  TB_ADDRESS.Text;
                //if (/*openFileDialog1.FileName != "" &&*/ openFileDialog1.FileName != "openFileDialog1")
                //{
                //    Person.ImagePath = openFileDialog1.FileName.ToString();



                    
                //}

                if(Person.Mode==clsPeoples.EnMode.New)
                {
                    MessageBox.Show("Person Added Succefully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show("Person Updated Succefully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                Person.Save();



                Lbl_Add_Edit.Text = "Update Person";
                //MessageBox.Show(openFileDialog1.FileName);
                //File.Copy(openFileDialog1.FileName, @"C:\ImageCopy\New Text Document.txt");
            }

            else
            {
                MessageBox.Show("Information not Enough", "Error",MessageBoxButtons.OK, MessageBoxIcon.Error);
                
            }

        }

        private void TBNATNUB_Validating(object sender, CancelEventArgs e)
        {
            if (clsPeoples.IsNationalNumberExists(TBNATNUB.Text))
            {
                e.Cancel=true;
                TBNATNUB.Focus();
                errorProvider1.SetError(TBNATNUB, "National Number already exists");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(TBNATNUB, "");
            }
        }

        private void CB_GENDER_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Person.ImagePath == "")
            {
                if (CB_GENDER.SelectedIndex == 1)
                {
                    panelImage.BackgroundImage = Resources.Female_512;

                }

                else if (CB_GENDER.SelectedIndex == 0)
                {
                    panelImage.BackgroundImage = Resources.Male_512;
                }
                else
                {
                    panelImage.BackgroundImage = Resources._19477_1;
                }
            }
        }
    }
}
