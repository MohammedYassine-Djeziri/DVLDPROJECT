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
using DVLD_Project.License.Forms;
using DVLD_Project.People.Forms;

namespace DVLD_Project.InternationalLicense.Forms
{
    public partial class ManageInlernationalApplicationForm : Form
    {

        DataView List = clsInternationalLicense.ListInternationalLicenses().DefaultView;
        public ManageInlernationalApplicationForm()
        {
            InitializeComponent();
        }

        private void ManageInlernationalApplicationForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            List = clsInternationalLicense.ListInternationalLicenses().DefaultView;
            dataGridView1.DataSource = List;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (comboBox1.SelectedIndex == 1)
                {
                    if (!(int.TryParse(textBox1.Text, out int value)))
                    {
                        MessageBox.Show("Please enter a valid Application ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        List.RowFilter = $"ApplicationID = '{Convert.ToInt32(textBox1.Text)}' ";
                    }


                }


                else if (comboBox1.SelectedIndex == 2)
                {
                    if (!(int.TryParse(textBox1.Text, out int value)))
                    {
                        MessageBox.Show("Please enter a valid Driver ID", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        List.RowFilter = $"DriverID = '{Convert.ToInt32(textBox1.Text)}' ";
                    }


                }



            }
            else
            {
                List = clsInternationalLicense.ListInternationalLicenses().DefaultView;
                dataGridView1.DataSource = List;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InternationalLicenseAppForm frm = new InternationalLicenseAppForm();
            frm.ShowDialog();
            List = clsInternationalLicense.ListInternationalLicenses().DefaultView;
            dataGridView1.DataSource = List;
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonInfo frm = new PersonInfo(clsDriver.FindDriverByDriverID(Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value)).PersonID);
            frm.Size = new Size(900, 900);
            frm.ShowDialog();
            
        }

        private void ShowPersonLicenseHistoryToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form frm = new LicenseHistory(Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value));
            frm.Size = new Size(1100, 750);
            frm.ShowDialog();
        }

        private void ShowLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new ShowInternationalLicenseForm(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
            frm.Size = new Size(900, 900);
            frm.ShowDialog();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0)
            {
                textBox1.Visible = false;
            }
            else
            {
                dataGridView1.DataSource = List;
                textBox1.Visible = true;
            }
        }
    }
}
