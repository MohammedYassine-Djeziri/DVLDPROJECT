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
using DVLD_Project.Properties;
namespace DVLD_Project.People.Forms
{

    public partial class Add_Update_PersonForm : Form
    {

        public delegate void FunRefHandler(int PerID);
        public event FunRefHandler FunRef;

        clsPeoples Person = clsPeoples.GetEmptyPerson();
        int PersonID = -1;

        public Add_Update_PersonForm(int per_id)
        {
            PersonID = per_id;
            InitializeComponent();
        }


        public void RefreshInfo()
        {
            Person = clsPeoples.FindByPersonalID(PersonID);

            if (Person.PerID != -1 && Person.Mode == clsPeoples.EnMode.Update)
            {
                Lbl_Add_Edit.Text = "Update Person";
                lblPerID.Text = PersonID.ToString();
                TB_FN.Text = Person.FirstName;
                TB_SN.Text = Person.SecondName;
                TB_TN.Text = Person.ThirdName;
                TB_LN.Text = Person.LastName;
                maskedTextBox1.Text = Person.Email;
                TBPHONE.Text = Person.Phone;
                TB_ADDRESS.Text = Person.Address;
                TBNATNUB.Text = Person.NationalNub;
                dateTimePicker1.Value = Person.DateOfBirth;
                CB_GENDER.SelectedIndex = Person.Gender;
                CB_COUNTRY.SelectedIndex = Person.Nationality;
                if (Person.ImagePath != "")
                {
                    openFileDialog1.FileName = Person.ImagePath;
                    PctBoxImg.ImageLocation = (openFileDialog1.FileName);
                    linklbl_Remove_Img.Visible = true;
                }

                else
                {
                    if (CB_GENDER.SelectedIndex == 1)
                    {
                        PctBoxImg.Image = Resources.Female_512;

                    }

                    else if (CB_GENDER.SelectedIndex == 0)
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
                Person = clsPeoples.GetEmptyPerson();
                Lbl_Add_Edit.Text = "Add New Person";
                CB_COUNTRY.SelectedItem = "Algeria";
            }
        }


        private void AddNewPerson_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Value = DateTime.Now.AddYears(-18);
            dateTimePicker1.MaxDate = DateTime.Now.AddYears(-18);
            DataTable dt = clsPeoples.ListCountries();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                CB_COUNTRY.Items.Add(dt.Rows[i][1]);
            }
            this.RefreshInfo();
        }

        private void add_UpdatePerson1_Load(object sender, EventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.PctBoxImg.ImageLocation = null;
            FunRef?.Invoke(PersonID);
            this.Close();

        }

        private void TBNATNUB_Validating(object sender, CancelEventArgs e)
        {
            if (clsPeoples.IsNationalNumberExists(TBNATNUB.Text) && Person.NationalNub.ToLower() != TBNATNUB.Text.ToLower())
            {
                e.Cancel = true;
                TBNATNUB.Focus();
                errorProvider1.SetError(TBNATNUB, "National Number already exists");
            }
            else
            {
                e.Cancel = false;
                errorProvider1.SetError(TBNATNUB, "");
            }
        }

        private void linklblSetImg_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName != "" && openFileDialog1.FileName != "openFileDialog1" && Person.ImagePath == "")
            {
                PctBoxImg.ImageLocation = (openFileDialog1.FileName);
                linklbl_Remove_Img.Visible = true;
                Person.ImagePath = openFileDialog1.FileName;
            }

            else //update picture
            {
                Person.LastImg = Person.ImagePath;
                PctBoxImg.ImageLocation = (openFileDialog1.FileName);
                linklbl_Remove_Img.Visible = true;
                Person.ImagePath = openFileDialog1.FileName;


            }
        }

        private void linklbl_Remove_Img_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PctBoxImg.ImageLocation = null;
            openFileDialog1.FileName = "";
            Person.LastImg = Person.ImagePath;
            Person.ImagePath = "";
            if (CB_GENDER.SelectedIndex == 1)
            {
                PctBoxImg.Image = Resources.Female_512;

            }

            else if (CB_GENDER.SelectedIndex == 0)
            {
                PctBoxImg.Image = Resources.Male_512;
            }
            else
            {
                PctBoxImg.Image = Resources._19477_1;
            }
            linklbl_Remove_Img.Visible = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (TBNATNUB.Text != "" && TB_FN.Text != "" && TB_SN.Text != "" && TB_LN.Text != "" &&
               TBPHONE.Text != "" && CB_COUNTRY.SelectedItem.ToString() != "" && CB_GENDER.SelectedItem.ToString() != "" &&
                   TB_ADDRESS.Text != "")
            {
                Person.NationalNub = TBNATNUB.Text;
                Person.FirstName = TB_FN.Text;
                Person.SecondName = TB_SN.Text;
                Person.ThirdName = TB_TN.Text;
                Person.LastName = TB_LN.Text;
                Person.Phone = TBPHONE.Text;
                Person.Email = maskedTextBox1.Text;
                Person.Nationality = CB_COUNTRY.SelectedIndex;
                Person.DateOfBirth = dateTimePicker1.Value;
                Person.Gender = CB_GENDER.SelectedIndex;
                Person.Address = TB_ADDRESS.Text;

                if (Person.Mode == clsPeoples.EnMode.New)
                {
                    MessageBox.Show("Person Added Successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                else
                {
                    MessageBox.Show("Person Updated Successfully", "Add", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

                Person.Save();
                PersonID = Person.PerID;


                Lbl_Add_Edit.Text = "Update Person";

            }

            else
            {
                MessageBox.Show("Information not Enough", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void CB_GENDER_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Person.ImagePath == "")
            {
                if (CB_GENDER.SelectedIndex == 1)
                {
                    PctBoxImg.Image =(Resources.Female_512);

                }

                else if (CB_GENDER.SelectedIndex == 0)
                {
                    PctBoxImg.Image = Resources.Male_512;
                }
                else
                {
                    PctBoxImg.Image = Resources._19477_1;
                }
            }
        }

        private void TBNATNUB_TextChanged(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_Validating(object sender, CancelEventArgs e)
        {
            if (!ClsUtil.IsValidEmail(maskedTextBox1.Text)  && maskedTextBox1.Text!="" )
            {
                e.Cancel = true;
                maskedTextBox1.Focus();
                errorProvider2.SetError(maskedTextBox1, "Email not valid format");
            }
            else
            {
                e.Cancel = false;
                errorProvider2.SetError(maskedTextBox1, "");
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}