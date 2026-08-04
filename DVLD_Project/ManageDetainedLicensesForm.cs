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
    public partial class ManageDetainedLicensesForm : Form
    {
        DataView List = new DataView();
        public ManageDetainedLicensesForm()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            List = clsDetainedLicense.ListDetainedLicenses().DefaultView;
            if (comboBox1.SelectedIndex != 0 && comboBox1.SelectedIndex != 2)
            {
                textBox1.Visible = true;
                comboBox2.Visible = false;
            }

            else if (comboBox1.SelectedIndex == 2)
            {
                textBox1.Visible = false;
                comboBox2.Visible = true;
            }

            else
            {
                comboBox2.Visible = false;
                textBox1.Visible = false;
            }

        }

        private void ManageDetainedLicensesForm_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            List = clsDetainedLicense.ListDetainedLicenses().DefaultView;
            dataGridView1.DataSource = List;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.SelectedIndex == 0)
            {
                List.RowFilter = null;
            }
            else if (comboBox2.SelectedIndex == 1)
            {
                List.RowFilter = $"[IsReleased] = '{true}'";
            }
            else if (comboBox2.SelectedIndex == 2)
            {
                List.RowFilter = $"[IsReleased] = '{false}'";
            }
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
                        MessageBox.Show("Wrong ID");
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("HELLO");
                        List.RowFilter = $"D.ID = '{Convert.ToInt32(textBox1.Text)}' ";
                    }


                }

                else if (comboBox1.SelectedIndex == 2)
                {


                    if (!(int.TryParse(textBox1.Text, out int value)))
                    {
                        MessageBox.Show("Wrong ID");
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        MessageBox.Show("HELLO");
                        List.RowFilter = $"D.ID = '{Convert.ToInt32(textBox1.Text)}' ";
                    }


                }

                else if (comboBox1.SelectedIndex == 5)
                {
                    if (!(int.TryParse(textBox1.Text, out int value)))
                    {
                        MessageBox.Show("Wrong ID");
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        List.RowFilter = $" [Release App.ID] = '{Convert.ToInt32(textBox1.Text)}' ";
                    }


                }

                else if (comboBox1.SelectedIndex == 3)
                {

                    List.RowFilter = $"N.No like '{textBox1.Text}%'";
                }

                else if (comboBox1.SelectedIndex == 4)
                {

                    List.RowFilter = $" [Full Name] like '{textBox1.Text}%'";
                }

                dataGridView1.DataSource = List;





            }
            else
            {
                List = clsDetainedLicense.ListDetainedLicenses().DefaultView;
                dataGridView1.DataSource = List;
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {
            DetainLicenseForm frm = new DetainLicenseForm();

            frm.Size = new Size(1000, 950);
            frm.ShowDialog();
            List = clsDetainedLicense.ListDetainedLicenses().DefaultView;
            dataGridView1.DataSource = List;
            dataGridView1.Refresh();
            this.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenseForm frm = new ReleaseDetainedLicenseForm();

            frm.Size = new Size(898, 800);
            frm.ShowDialog();
            List = clsDetainedLicense.ListDetainedLicenses().DefaultView;
            dataGridView1.DataSource = List;
            dataGridView1.Refresh();
            this.Refresh();
        }

        private void PersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PersonInfo frm = new PersonInfo(clsPeoples.FindPersonByNationalNumber(Convert.ToString(dataGridView1.CurrentRow.Cells[6].Value)).PerID);
            frm.Size = new Size(900, 650);
            frm.ShowDialog();
        }

        private void ShowLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Form frm = new ShowLicenseForm(Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value));
            frm.Size = new Size(900, 650);
            frm.ShowDialog();
        }

        private void ShowPersonLicenseHistoryToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            Form frm = new LicenseHistory(clsLicenses.FindLicenseByLicenseID( Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value)).DriverID);
            frm.Size = new Size(1100, 750);
            frm.ShowDialog();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            if(clsLicenses.IsLicenseDetained(Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value)))
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = true;    
            }
            else
            {
                releaseDetainedLicenseToolStripMenuItem.Enabled = false;
            }
        }

        private void releaseDetainedLicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReleaseDetainedLicenseForm frm = new ReleaseDetainedLicenseForm();

            frm.Size = new Size(898, 800);
            frm.ReleaseLicense(Convert.ToInt32(dataGridView1.CurrentRow.Cells[1].Value));
            frm.ShowDialog();
            List = clsDetainedLicense.ListDetainedLicenses().DefaultView;
            dataGridView1.DataSource = List;
            dataGridView1.Refresh();
            this.Refresh();
        }
    }
}
