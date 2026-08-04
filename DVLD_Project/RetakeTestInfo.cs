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
    public partial class RetakeTestInfo : UserControl
    {
        public int RetakeTestAppID { get; set; } = -1;
        public float TotalFees { get; set; } = 0;
        public float Fees { get; set; } = 0;
        public RetakeTestInfo()
        {
            InitializeComponent();
        }

        private void RetakeTestInfo_Load(object sender, EventArgs e)
        {
            RefreshInfo();
        }

        public void RefreshInfo()
        {
            lbl_AppID.Text = RetakeTestAppID.ToString();
            lbl_Fees.Text = Fees.ToString();
            lbl_TotalFees.Text = TotalFees.ToString();
        }
    }
}
