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
    public partial class EditApplicationType : Form
    {
        public EditApplicationType(int id , string name , float fees )
        {
            InitializeComponent();
            lbl_ID.Text = id.ToString();
            TB_Fees.Text = fees.ToString();
            TB_Title.Text = name.ToString();
        }


        private void button2_Click(object sender, EventArgs e)
        {
            clsApplicationTypes.UpdateApplicationTypes(Convert.ToInt32(lbl_ID.Text), TB_Title.Text, Convert.ToSingle(TB_Fees.Text));
            MessageBox.Show("Application Type Updated Successfully");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
