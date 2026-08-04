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

namespace DVLD_Project.Applications
{
    public partial class EditTestType : Form
    {
        public EditTestType(int id, string name,string description ,  float fees)
        {
            InitializeComponent();
            lbl_ID.Text = id.ToString();
            TB_Fees.Text = fees.ToString();
            TB_Title.Text = name.ToString();
            TB_Description.Text = description.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(TB_Description.Text!="" &&  TB_Title.Text!="" && TB_Fees.Text!="")
            {
                clsTestTypes.UpdateTestTypes(Convert.ToInt32(lbl_ID.Text), TB_Title.Text, TB_Description.Text,
                    Convert.ToSingle(TB_Fees.Text));
                MessageBox.Show("Test Type Updated Successfully");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void EditTestType_Load(object sender, EventArgs e)
        {

        }
    }
}
