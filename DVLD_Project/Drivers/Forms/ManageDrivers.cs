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
            //MessageBox.Show(dataGridView1.Columns[0].Name);
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
                        MessageBox.Show("Wrong ID");
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        List.RowFilter = $"DriverID = '{Convert.ToInt32(textBox1.Text)}' ";
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
                        List.RowFilter = $" PersonID = '{Convert.ToInt32(textBox1.Text)}' ";
                    }


                }

                else if (comboBox1.SelectedIndex == 3)
                {

                    List.RowFilter = $"NationalNo like '{textBox1.Text}%'";
                }

                else if (comboBox1.SelectedIndex == 4)
                {

                    List.RowFilter = $"FullName like '{textBox1.Text}%'";
                }

                else if (comboBox1.SelectedIndex == 5)
                {
                    // impossible case

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
    }
}
