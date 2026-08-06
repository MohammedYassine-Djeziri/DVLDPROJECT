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

namespace DVLD_Project.User.CustomControls
{
    public partial class FilterUsers : UserControl
    {
        public event Action<DataView> OnFilterCompleted;
        protected virtual void FilterCompleted(DataView list)
        {
            Action<DataView> handler = OnFilterCompleted;
            if (handler != null)
            {
                handler(list);
            }
        }

        private DataView List = new DataView();
        public FilterUsers()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (comboBox1.SelectedIndex == 1  || comboBox1.SelectedIndex == 3)
            {
                if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;
                }
            }

            if (OnFilterCompleted != null)
            {
                FilterCompleted(List);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            List = clsUsers.LisUsers().DefaultView;
            if (comboBox1.SelectedIndex != 0 && comboBox1.SelectedIndex != 5)
            {
                textBox1.Visible = true;
                comboBox2.Visible = false;
            }

            else if(comboBox1.SelectedIndex==5)
            {
                textBox1.Visible= false;
                comboBox2.Visible = true;
            }

            else
            {
                comboBox2.Visible = false;
                textBox1.Visible = false;
            }

            if (OnFilterCompleted != null)
            {
                FilterCompleted(List);
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
                        List.RowFilter = $" UserID = '{Convert.ToInt32(textBox1.Text)}' ";
                    }


                }

                else if (comboBox1.SelectedIndex == 3)
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

                else if (comboBox1.SelectedIndex == 2)
                {

                    List.RowFilter = $"UserName like '{textBox1.Text}%'";
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
                List = clsUsers.LisUsers().DefaultView;
            }

            if (OnFilterCompleted != null)
            {
                FilterCompleted(List);
            }

        }

       

        private void FilterUsers_Load(object sender, EventArgs e)
        {
            List=clsUsers.LisUsers().DefaultView;
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox2.SelectedIndex == 0)
            {
                List.RowFilter = null;
            }
            else if(comboBox2.SelectedIndex == 1)
            {
                List.RowFilter = $"IsActive = '{true}'";
            }
            else if(comboBox2.SelectedIndex == 2)
            {
                List.RowFilter = $"IsActive = '{false}'";
            }
            if (OnFilterCompleted != null)
            {
                FilterCompleted(List);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
