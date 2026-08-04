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

namespace DVLD_Project.Users
{
    public partial class Add_Update_UserForm : Form
    {
        
        public int PerID = -1;

        public clsUsers MyUser = clsUsers.GetEmptyUser();  

        public Add_Update_UserForm(int User_id)
        {
            MyUser=clsUsers.FindUserByUserID(User_id);
            if (MyUser != null)
            {
                PerID = MyUser.Person.PerID;
            }
            InitializeComponent();
        }



        private void findPerson1_OnAddPersonCompleted(int obj)
        {
            PerID = obj;
            showPersonalInfo1.Person_ID = PerID;
            showPersonalInfo1.RefreshInfo();

        }

        private void findPerson1_OnSearchCompleted_1(int obj)
        {
            //MessageBox.Show("hi");
            PerID = obj;
            showPersonalInfo1.Person_ID = PerID;
            showPersonalInfo1.RefreshInfo();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {

            if (PerID != -1)
            {
                if (!clsUsers.IsUserExistByPersonID(PerID))
                {
                    MyUser=clsUsers.GetEmptyUser();
                    MyUser.Person=clsPeoples.FindByPersonalID(PerID);
                    tabControl1.SelectedIndex = 1;
                    TBUserName.Focus();
                }
                else if(clsUsers.IsUserExistByPersonID(PerID) && this.Tag.ToString()=="Add")
                {
                    MessageBox.Show("Person ID Already has User!");
                }
                else
                {
                    tabControl1.SelectedIndex = 1;
                    TBUserName.Focus();
                    lbl.Text=MyUser.UserID.ToString();
                    TBUserName.Text = MyUser.UserName;
                    TB_Pass.Text = MyUser.Password;
                    TB_PassConf.Text = MyUser.Password;
                    checkBox1.Checked = MyUser.IsActive;
                }
            }
            
            else
            {
                MessageBox.Show("Person ID UnKnown!");
            }
        }

        private void TBNATNUB_Validating(object sender, CancelEventArgs e)
        {
            if (TBUserName.Text == "")
            {
                btnSave.Enabled = false;
                e.Cancel = true;
                TBUserName.Focus();
                errorProvider1.SetError(TBUserName, "Set UserName");
            }
            else
            {
                btnSave.Enabled = true;
                e.Cancel = false;
                errorProvider1.SetError(TBUserName, "");
            }
        }

        private void TB_Pass_Validating(object sender, CancelEventArgs e)
        {
            if (TB_Pass.Text == "" && TBUserName.Text != "")
            {
                btnSave.Enabled = false;
                e.Cancel = true;
                TB_Pass.Focus();
                errorProvider2.SetError(TB_Pass, "Set Password");
            }
            else
            {
                btnSave.Enabled = true;
                e.Cancel = false;
                errorProvider2.SetError(TB_Pass, "");
            }
        }

        private void TB_PassConf_Validating(object sender, CancelEventArgs e)
        {
            if ((TB_PassConf.Text != TB_Pass.Text)&&(TB_PassConf.Text !="") )
            {
                btnSave.Enabled = false;
                e.Cancel = true;
                TB_PassConf.Focus();
                errorProvider3.SetError(TB_PassConf, "Password Confirmation does not much Password");
            }
            else
            {
                btnSave.Enabled = true;
                e.Cancel = false;
                errorProvider3.SetError(TB_PassConf, "");
            }
        }

        private void AddNewUserForm_Load(object sender, EventArgs e)
        {
           
            if (MyUser != null)
            {
                lblTitle.Text = "Update User";
                showPersonalInfo1.Person_ID = PerID;
                showPersonalInfo1.RefreshInfo();
                findPerson1.Enabled = false;
                lblTitle.Text = "Update User";
                this.Tag = "Update";
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (MyUser.Person != null && TBUserName.Text != "" && TB_Pass.Text != "" && TB_PassConf.Text != "")
            {
                MyUser.Password= TB_Pass.Text;
                MyUser.IsActive = checkBox1.Checked;
                MyUser.UserName= TBUserName.Text;
                if(MyUser.UserID==-1)
                {
                    MessageBox.Show("User Added Successfully");
                }
                else
                {
                    MessageBox.Show("User Updated Successfully");
                }

                MyUser.Save();
                lblTitle.Text = "Update User";
                findPerson1.Enabled = false;
                lbl.Text = MyUser.UserID.ToString();

            }

            else
            {
                MessageBox.Show("Please Enter All User Info" , "Miss Info" , MessageBoxButtons.OK , MessageBoxIcon.Warning);     
            }

            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void findPerson1_Load(object sender, EventArgs e)
        {

        }

        private void showPersonalInfo1_Load(object sender, EventArgs e)
        {

        }
    }
}
