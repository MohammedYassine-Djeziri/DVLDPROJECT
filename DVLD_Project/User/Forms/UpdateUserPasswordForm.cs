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

namespace DVLD_Project.User.Forms
{
    public partial class UpdateUserPasswordForm : Form
    {

        clsUsers MyUser=clsUsers.GetEmptyUser();
        public UpdateUserPasswordForm(int user_id)
        {
            MyUser=clsUsers.FindUserByUserID(user_id);
            InitializeComponent();
            userInformation1.User = MyUser;
        }

        private void UpdateUserPasswordForm_Load(object sender, EventArgs e)
        {

            TBCurrentPassword.Focus();
        }

        private void TBCurrentPassword_Validating(object sender, CancelEventArgs e)
        {
            if (TBCurrentPassword.Text != MyUser.Password)
            {
                //btnSave.Enabled = false;
                e.Cancel = true;
                TBCurrentPassword.Focus();
                errorProvider1.SetError(TBCurrentPassword, "Set Current Password!");
            }
            else
            {
                //btnSave.Enabled = true;
                e.Cancel = false;
                errorProvider1.SetError(TBCurrentPassword, "");
            }
        }

        private void TB_Pass_Validating(object sender, CancelEventArgs e)
        {
            if (TB_Pass.Text == "" && TBCurrentPassword.Text != "")
            {
                //btnSave.Enabled = false;
                e.Cancel = true;
                TB_Pass.Focus();
                errorProvider2.SetError(TB_Pass, "Set Password");
            }
            else
            {
                //btnSave.Enabled = true;
                e.Cancel = false;
                errorProvider2.SetError(TB_Pass, "");
            }
        }

        private void TB_PassConf_Validating(object sender, CancelEventArgs e)
        {
            if ((TB_PassConf.Text != TB_Pass.Text) && (TB_PassConf.Text != ""))
            {
                //btnSave.Enabled = false;
                e.Cancel = true;
                TB_PassConf.Focus();
                errorProvider3.SetError(TB_PassConf, "Password Confirmation does not much Password");
            }
            else
            {
                //btnSave.Enabled = true;
                e.Cancel = false;
                errorProvider3.SetError(TB_PassConf, "");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(TBCurrentPassword.Text==MyUser.Password &&  TB_Pass.Text!=""&&(TB_PassConf.Text== TB_Pass.Text))
            {
                MyUser.Password = TB_Pass.Text;
                MyUser.Save();
                MessageBox.Show("Password Updated Successfully");
            }
            else
            {
                MessageBox.Show("Please Set Correct Info", "Miss Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
