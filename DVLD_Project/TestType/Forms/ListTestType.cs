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

namespace DVLD_Project.TestType.Forms
{
    public partial class ListTestType : Form
    {
        public ListTestType()
        {
            InitializeComponent();
        }

        private void ListTestType_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource = clsTestTypes.ListTestTypes();
            
            lblNubRec.Text = dataGridView1.Rows.Count.ToString();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTestType frm = new EditTestType(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value),
                dataGridView1.CurrentRow.Cells[1].Value.ToString(), dataGridView1.CurrentRow.Cells[2].Value.ToString(),
                Convert.ToSingle(dataGridView1.CurrentRow.Cells[3].Value));
            frm.ShowDialog();
            dataGridView1.DataSource = clsTestTypes.ListTestTypes();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();   
        }
    }
}
