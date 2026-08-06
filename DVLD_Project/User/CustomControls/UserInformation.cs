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

namespace DVLD_Project.User.CustomControls
{
    public partial class UserInformation : UserControl
    {
        public clsUsers User=clsUsers.GetEmptyUser();

        public void RefreshInfo()
        {
            showPersonalInfo1.Person_ID = User.Person.PerID;
            showPersonalInfo1.RefreshInfo();
            lblUserID.Text=User.UserID.ToString();
            lblUserName.Text=User.UserName;
            if(User.IsActive)
            {
                lblIsActive.Text = "Yes";
            }
            else
            {
                lblIsActive.Text = "No";
            }

        }
        public UserInformation()
        {
            InitializeComponent();
        }



        private void UserInformation_Load(object sender, EventArgs e)
        {
            RefreshInfo();
        }
    }
}
