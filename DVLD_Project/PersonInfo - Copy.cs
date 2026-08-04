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
    public partial class PersonInfo : Form
    {

        int PersonID = -1;
        public PersonInfo(int id)
        {
            PersonID=id;
            InitializeComponent();
            showPersonalInfo1.Person_ID = id;
        }


        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Size size = new Size(900, 500);
            Add_Update_PersonForm frm = new Add_Update_PersonForm(PersonID);
            frm.Size = size;
            this.Close();
            frm.ShowDialog();


        }


    }
}
