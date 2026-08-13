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
using DVLD_Project.People.Forms;

namespace DVLD_Project.People.CustomControls
{
    public partial class FindPerson : UserControl
    {

        public event Action<int> OnSearchCompleted;
        public event Action<int> OnAddPersonCompleted;

        protected virtual void SearchCompleted(int PerID)
        {
            Action<int> handler = OnSearchCompleted;
            if (handler != null)
            {
                handler(PerID);
            }
        }

        protected virtual void AddPersonCompleted(int PerID)
        {
            Action<int> handler = OnAddPersonCompleted;
            if (handler != null)
            {
                handler(PerID);
            }
        }

        private clsPeoples Person=null;
        public FindPerson()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
        }

        private void btnSearchPerson_Click(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0)
            {
                if (!(int.TryParse(textBox1.Text, out int value)))
                {
                    MessageBox.Show("Please enter a valid Personal ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox1.Text = string.Empty;
                }
                else
                {
                    Person = clsPeoples.FindByPersonalID(Convert.ToInt32(textBox1.Text));
                }
                
            }
            else if(comboBox1.SelectedIndex == 1)
            {
                Person = clsPeoples.FindPersonByNationalNumber(textBox1.Text);
            }

            if(Person!= null)
            {
                if(OnSearchCompleted != null)
                {
                    SearchCompleted(Person.PerID);
                }
            }

            else if (Person == null)
            {
                if (OnSearchCompleted != null)
                {
                    SearchCompleted(-1);
                }
            }

        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            Size size = new Size(900, 500);
            Add_Update_PersonForm frm = new Add_Update_PersonForm(-1);
            frm.Size = size;
            frm.FunRef += DoSomething;
            frm.ShowDialog();

            if (Person != null)
            {
                if (OnSearchCompleted != null)
                {
                    AddPersonCompleted(Person.PerID);
                }
            }

            else if (Person == null)
            {
                if (OnSearchCompleted != null)
                {
                    AddPersonCompleted(-1);
                }
            }

        }

        private void DoSomething(int P)
        {
            Person = clsPeoples.FindByPersonalID(P);
        }

        private void FindPerson_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 1;
        }

        public void DisableAll()
        {
            groupBox1.Enabled = false;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
