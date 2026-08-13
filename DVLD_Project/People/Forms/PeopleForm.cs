using DVLDBusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;


namespace DVLD_Project.People.Forms
{
    public partial class PeopleForm : Form
    {
        private static DataView List = new DataView();

        public  PeopleForm()
        {
            InitializeComponent();
            GetPeoplesList();
        }



        private void GetPeoplesList()
        {
  
            List = clsPeoples.ListPeoples().DefaultView;
            dataGridView1.DataSource = List;
            dataGridView1.Refresh();
        }

        private void btn_AddPer_Click(object sender, EventArgs e)
        {
            Size size = new Size(900, 500);
            Add_Update_PersonForm frm= new Add_Update_PersonForm(-1);
            frm.Size = size;
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size size = new Size(900, 500);
            Add_Update_PersonForm frm = new Add_Update_PersonForm((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.Size = size;
            frm.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you went to delete this person?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                if (!(clsPeoples.DeletePerson((int)dataGridView1.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("Failed to delete person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show("Person Deleted Succefully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List = clsPeoples.ListPeoples().DefaultView;
                    dataGridView1.DataSource = List;
                    this.Refresh();
                }
            }
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size size = new Size(900, 500);
            PersonInfo frm = new PersonInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.Size= size;
            frm.ShowDialog();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size size = new Size(900, 500);
            Add_Update_PersonForm frm = new Add_Update_PersonForm(-1);
            frm.Size= size;
            frm.ShowDialog();
        }

        private void searchPerson1_OnFilterCompleted(DataView List)
        {
            dataGridView1.DataSource = List;
            dataGridView1.Refresh();
        }


        
       
    }
}
