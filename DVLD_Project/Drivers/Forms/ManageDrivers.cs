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

namespace DVLD_Project.Drivers.Forms
{
    public partial class ManageDrivers : Form
    {
        private static DataView List = new DataView();
        public ManageDrivers()
        {
            InitializeComponent();
        }

        private void ManageDrivers_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            List = clsDriver.ListDrivers().DefaultView;
            dataGridView1.DataSource = List;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            List = clsDriver.ListDrivers().DefaultView;
            if (comboBox1.SelectedIndex != 0 && comboBox1.SelectedIndex != 5)
            {
                textBox1.Visible = true;
                
            }

            else if (comboBox1.SelectedIndex == 5)
            {
                textBox1.Visible = false;
            }

            else
            {
                textBox1.Visible = false;
            }

            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (comboBox1.SelectedIndex == 1)
                {
                    if (!(int.TryParse(textBox1.Text, out int value)))
                    {
                        MessageBox.Show("Please enter a valid Driver ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                       try
                        {
                            List.RowFilter = $"DriverID = '{Convert.ToInt32(textBox1.Text)}' ";
                            
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred while filtering the data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }


                }

                else if (comboBox1.SelectedIndex == 2)
                {
                    if (!(int.TryParse(textBox1.Text, out int value)))
                    {
                        MessageBox.Show("Please enter a valid Person ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        try
                        {
                            List.RowFilter = $"PersonID = '{Convert.ToInt32(textBox1.Text)}' ";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("An error occurred while filtering the data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }


                }

                else if (comboBox1.SelectedIndex == 3)
                {
                    try
                    {
                        List.RowFilter = $"FullName like '{textBox1.Text}%'";
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while filtering the data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                else if (comboBox1.SelectedIndex == 4)
                {

                   try
                    {
                        List.RowFilter = $"NationalNub like '{textBox1.Text}%'";
                        
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("An error occurred while filtering the data: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }





            }
            else
            {
                List = clsDriver.ListDrivers().DefaultView;
            }
            dataGridView1.DataSource = List;
            dataGridView1.Refresh();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void showDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size size = new Size(900, 500);
            PersonInfo frm = new PersonInfo((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.Size = size;
            frm.ShowDialog();
        }



        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Size size = new Size(900, 500);
            Add_Update_PersonForm frm = new Add_Update_PersonForm((int)dataGridView1.CurrentRow.Cells[1].Value);
            frm.Size = size;
            frm.ShowDialog();
            dataGridView1.DataSource = clsDriver.ListDrivers();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure that you went to delete this person?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                if (!(clsPeoples.DeletePerson((int)dataGridView1.CurrentRow.Cells[1].Value)))
                {
                    MessageBox.Show("Failed to delete person", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                }
                else
                {
                    MessageBox.Show("Person Deleted Succefully", "Delete", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    List = clsDriver.ListDrivers().DefaultView;
                    dataGridView1.DataSource = List;
                    this.Refresh();
                }
            }
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be available in the next version of the system", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This feature will be available in the next version of the system", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
