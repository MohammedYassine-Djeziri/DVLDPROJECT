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
    public partial class UsersManagementForm : Form
    {

        

        DataView List = new DataView();
        public UsersManagementForm()
        {
            InitializeComponent();
        }

        private void filterUsers1_OnFilterCompleted(DataView obj)
        {
            List=obj;
            dataGridView1.DataSource = List;
        }

        private void UsersManagementForm_Load(object sender, EventArgs e)
        {
            List=clsUsers.LisUsers().DefaultView;
            dataGridView1.DataSource = List;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Add_Update_UserForm form = new Add_Update_UserForm(-1);
            form.ShowDialog();
            List = clsUsers.LisUsers().DefaultView;
            dataGridView1.DataSource = List;
            this.Refresh();
        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you went to delete this User?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                if (!(clsUsers.DeleteUser((int)dataGridView1.CurrentRow.Cells[0].Value)))
                {
                    MessageBox.Show("You can't delete this person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show("User Deleted Successfully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List = clsUsers.LisUsers().DefaultView;
                    dataGridView1.DataSource = List;
                    this.Refresh();
                }
            }
        }

        private void addNewPersonToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Add_Update_UserForm form = new Add_Update_UserForm(-1);
            form.ShowDialog();
            List = clsUsers.LisUsers().DefaultView;
            dataGridView1.DataSource = List;
            this.Refresh();
        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size size = new Size(1772, 901);
            ShowUserInfo frm = new ShowUserInfo((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.Size = size;
            frm.ShowDialog();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size size = new Size(975, 700);
            UpdateUserPasswordForm frm = new UpdateUserPasswordForm((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.Size = size;
            frm.ShowDialog();
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size size = new Size(900, 600);
            Add_Update_UserForm frm = new Add_Update_UserForm((int)dataGridView1.CurrentRow.Cells[0].Value);
            frm.Size = size;
            frm.ShowDialog();
            List = clsUsers.LisUsers().DefaultView;
            dataGridView1.DataSource = List;
            this.Refresh();
        }
    }
}
