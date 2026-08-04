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
    public partial class SearchPerson : UserControl
    {

        public event Action<DataView> OnFilterCompleted;
        protected virtual void FilterCompleted(DataView list)
        {
            Action<DataView> handler = OnFilterCompleted;
            if(handler != null)
            {
                handler(list);
            }
        }
        
        public DataView List= new DataView();
        public SearchPerson()
        {
            InitializeComponent();
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = "";
            List = clsPeoples.ListPeoples().DefaultView;
            if (comboBox1.SelectedIndex != 0)
            {
                textBox1.Visible = true;

            }

            else
            {
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
                        MessageBox.Show("Id nort exist");
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
                List = clsPeoples.ListPeoples().DefaultView;

            }
            //dataGridView1.DataSource = List;
            if (OnFilterCompleted != null)
            {
                FilterCompleted(List);
            }

        }
        private void SearchPerson_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            List = clsPeoples.ListPeoples().DefaultView;
            textBox1.Visible = false;
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
            if(OnFilterCompleted!= null)
            {
                FilterCompleted(List);
            }
        }

        //private bool IsUsed()
        //{
        //    bool f = false;
        //    if(comboBox1.SelectedIndexChanged)
        //}
    }
}
