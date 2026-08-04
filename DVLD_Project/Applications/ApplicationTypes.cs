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
    public partial class ApplicationTypes : Form
    {
        public ApplicationTypes()
        {
            InitializeComponent();
        }

        private void ApplicationTypes_Load(object sender, EventArgs e)
        {
            dataGridView1.DataSource= clsApplicationTypes.ListApplicationTypes(); 
            //dataGridView1.Rows.Count.ToSt;
            // Allow User to add row --> false
            //dataGridView1.Rows.Remove(dataGridView1.Rows[(dataGridView1.Rows.Count-1)]);
            lblNubRec.Text = dataGridView1.Rows.Count.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditApplicationType frm = new EditApplicationType( Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value),
                dataGridView1.CurrentRow.Cells[1].Value.ToString() , Convert.ToSingle (dataGridView1.CurrentRow.Cells[2].Value));
            frm.ShowDialog();
            dataGridView1.DataSource = clsApplicationTypes.ListApplicationTypes();
        }
    }
}
