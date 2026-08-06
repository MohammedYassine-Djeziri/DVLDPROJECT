using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD_Project.Application.Forms
{
    public partial class ShowApplicationInfoForm : Form
    {

        public ShowApplicationInfoForm(int id , int passed_test)
        {
            InitializeComponent();
            showApplicationDetails1.ID = id;
            showApplicationDetails1.PassedTest = passed_test;
        }

        private void ShowApplicationInfoForm_Load(object sender, EventArgs e)
        {
            showApplicationDetails1.RefreshInfo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
