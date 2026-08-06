using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.People.Forms
{
    public partial class UpdatePersonForm : Form
    {
       public static int ID=-1;
        public UpdatePersonForm(int iD)
        {  
            ID = iD;
            InitializeComponent();
            this.add_UpdatePerson1.Person_ID = ID;
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void add_UpdatePerson1_Load(object sender, EventArgs e)
        {

        }
    }
}
