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
    public partial class ShowUserInfo : Form
    {
        public clsUsers User = clsUsers.GetEmptyUser();
        public ShowUserInfo( int user_id)
        {
            InitializeComponent();
            User=clsUsers.FindUserByUserID(user_id);
        }

        private void ShowUserInfo_Load(object sender, EventArgs e)
        {
            userInformation1.User = User;
            userInformation1.RefreshInfo();
        }
    }
}
