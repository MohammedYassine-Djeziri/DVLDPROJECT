using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


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

        private void PersonInfo_Load(object sender, EventArgs e)
        {
        showPersonalInfo1.RefreshInfo();
        }

    private void PersonInfo_Load_1(object sender, EventArgs e)
    {

    }
}

