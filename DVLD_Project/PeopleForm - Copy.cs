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

namespace DVLD_Project
{
    public partial class PeopleForm : Form
    {
        private static DataView List = new DataView();
        //private DataView;
        public PeopleForm()
        {
            InitializeComponent();
        }

        private void PeopleForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            List=clsPeoples.ListPeoples().DefaultView;
            dataGridView1.DataSource = List;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
           if( comboBox1.SelectedIndex !=0)
            {
                textBox1.Visible = true;
            }

            else
            {
                textBox1.Visible=false;
            }
        }
        //None Persone ID National No First Name Second Name  Third Name  Last Name  Nationality  Gender  Phone  Email
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (comboBox1.SelectedIndex == 1)
                {
                    if (!(int.TryParse(textBox1.Text, out int value)))
                    {
                        MessageBox.Show("hi");
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        List.RowFilter = $" PersonID = '{Convert.ToInt32(textBox1.Text)}' ";
                    }


                }

                else if (comboBox1.SelectedIndex == 2)
                {

                    List.RowFilter = $"NationalNo like '{textBox1.Text}%'";
                }


                else if (comboBox1.SelectedIndex == 3)
                {

                    List.RowFilter = $"FirstName like '{textBox1.Text}%'";
                }

                else if (comboBox1.SelectedIndex == 4)
                {

                    List.RowFilter = $"SecondName like '{textBox1.Text}%'";
                }

                else if (comboBox1.SelectedIndex == 5)
                {

                    List.RowFilter = $"ThirdName like '{textBox1.Text}%'";
                }

                else if (comboBox1.SelectedIndex == 6)
                {

                    List.RowFilter = $"LastName like '{textBox1.Text}%'";
                }

                else if (comboBox1.SelectedIndex == 7)
                {

                    List.RowFilter = $"Nationality like '{textBox1.Text}%'";
                }

                else if (comboBox1.SelectedIndex == 8)
                {

                    List.RowFilter = $"Gender like '{textBox1.Text}%'";
                }

                else if (comboBox1.SelectedIndex == 9)
                {

                    List.RowFilter = $"Phone like '{textBox1.Text}%'";
                }

                else if (comboBox1.SelectedIndex == 10)
                {

                    List.RowFilter = $"Email like '{textBox1.Text}%'";
                }

               


            }
            else
            {
                List=clsPeoples.ListPeoples().DefaultView;
                dataGridView1.DataSource = List;
            }

        }

        private void btn_AddPer_Click(object sender, EventArgs e)
        {
            Size size = new Size(900, 500);
            Add_Update_PersonForm frm= new Add_Update_PersonForm(-1);
            frm.Size = size;
            frm.ShowDialog();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1.SelectedIndex == 1)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }
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
                    MessageBox.Show("You can't delete this person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
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
    }
}
