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
using DVLD_Project.Application.Forms;
using DVLD_Project.Global;
using DVLD_Project.License.Forms;
using DVLD_Project.LocalDrivingLicense.Forms;
using DVLD_Project.TestAppointment.Forms;

namespace DVLD_Project.LocalDrivingLicenseApplication.Forms
{
    public partial class LocalDrivingLicenseApplicationManagement : Form
    {
        clsApplication TempApplication = clsApplication.GetEmptyApplication();
        DataView List =clsApplication.ListLDLApplication().DefaultView; 
        public LocalDrivingLicenseApplicationManagement()
        {
            InitializeComponent();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            NewLocalDrivingLicenseApplication frm = new NewLocalDrivingLicenseApplication();
            frm.ShowDialog();
            List = clsApplication.ListLDLApplication().DefaultView;
            dataGridView1.DataSource = List;
        }

        private void LocalDrivingLicenseApplicationManagement_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            dataGridView1.DataSource = List;
        }

        private void sssToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (comboBox1.SelectedIndex == 1)
                {
                    if (!(int.TryParse(textBox1.Text, out int value)))
                    {
                        MessageBox.Show("Id not exist");
                        textBox1.Text = string.Empty;
                    }
                    else
                    {
                        List.RowFilter = $" LDLAppID = '{Convert.ToInt32(textBox1.Text)}' ";
                    }


                }

                else if (comboBox1.SelectedIndex == 2)
                {

                    List.RowFilter = $"NationalNo like '{textBox1.Text}%'";
                }


                else if (comboBox1.SelectedIndex == 3)
                {

                    List.RowFilter = $"FullName like '{textBox1.Text}%'";
                }

                else if (comboBox1.SelectedIndex == 4)
                {

                    List.RowFilter = $"Status like '{textBox1.Text}%'";
                }
                
            }
            else
            {
                List = clsApplication.ListLDLApplication().DefaultView;
                dataGridView1.DataSource= List;

            }
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
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox1.SelectedIndex == 0) 
            {
                textBox1.Visible = false;
            }
            else
            {
                dataGridView1.DataSource = List;
                textBox1.Visible = true;
            }
        }

        private void showApplicationDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowApplicationInfoForm frm = new ShowApplicationInfoForm(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value),
                Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value));
            frm.Size = new Size(800, 500);
            frm.ShowDialog();


        }
        private void cancelApplicationToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            TempApplication = clsApplication.FindApplicationByLDLID(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));

            if (MessageBox.Show("Are you Sure that you want to cancel this application?", "???", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if(TempApplication.ChangeStatus(2))
                {
                    MessageBox.Show("Application has been Cancelled Successfully");
                }
                else
                {
                    
                        MessageBox.Show("can't cancel application because it has already completed or cancelled");
                    
                }
            }

            List = clsApplication.ListLDLApplication().DefaultView;
            dataGridView1.DataSource = List;
            
        }

        private void deleteApplicationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToString(dataGridView1.CurrentRow.Cells[6].Value) == "New")
            {
                if (MessageBox.Show("Are you Sure that you want to delete this application?", "???", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (clsApplication.DeleteApplicationByLDLID(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)))
                    {
                        MessageBox.Show("Application has been Deleted Successfully");
                    }
                    else
                    {
                        MessageBox.Show("We got error");
                    }
                }

                List = clsApplication.ListLDLApplication().DefaultView;
                dataGridView1.DataSource = List;
            }
            else
            {

            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            ScheduleTestsMenuItem.Enabled = true;
            editApplicationToolStripMenuItem.Enabled = true;
            cancelApplicationToolStripMenuItem.Enabled = true;
            ShowLicenseToolStripMenuItem1.Enabled = true;
            IssueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
            ShowPersonLicenseHistoryToolStripMenuItem3.Enabled = true;
            scheduleVisionTestToolStripMenuItem.Enabled = true;
            scheduleWrittenTestToolStripMenuItem.Enabled = true;
            scheduleStreetTestToolStripMenuItem.Enabled = true;
            deleteApplicationToolStripMenuItem.Enabled = true;
            cancelApplicationToolStripMenuItem.Enabled = true;

            if ((dataGridView1.CurrentRow.Cells[6].Value).ToString() == "Cancelled")
            {
                ScheduleTestsMenuItem.Enabled = false;
                editApplicationToolStripMenuItem.Enabled = false;
                cancelApplicationToolStripMenuItem.Enabled = false;
                ShowLicenseToolStripMenuItem1.Enabled = false;
                IssueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                ShowPersonLicenseHistoryToolStripMenuItem3.Enabled = false;
            }
           
            else
            {
                

                int test = Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value);
                if (test == 0)
                {
                    scheduleVisionTestToolStripMenuItem.Enabled = true;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                }

                else if (test == 1)
                {
                    scheduleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = true;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                }

                else if (test == 2)
                {
                    scheduleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = true;
                }

                else
                {
                    scheduleVisionTestToolStripMenuItem.Enabled = false;
                    scheduleWrittenTestToolStripMenuItem.Enabled = false;
                    scheduleStreetTestToolStripMenuItem.Enabled = false;
                }


                if ((dataGridView1.CurrentRow.Cells[6].Value).ToString() == "Completed")
                {
                    //ShowLicenseToolStripMenuItem1.Enabled = true;
                    //if (!clsLocalDrivingLicense.IsLicenseExistsByLDLAppID(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)))
                    //{
                    //    IssueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = true;
                    //}
                    //else
                    //{
                    //    IssueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                    //}
                    cancelApplicationToolStripMenuItem.Enabled = false;
                    deleteApplicationToolStripMenuItem .Enabled = false;
                    ScheduleTestsMenuItem.Enabled = false;
                }

                else
                {
                    ShowLicenseToolStripMenuItem1.Enabled = false;
                    IssueDrivingLicenseFirstTimeToolStripMenuItem.Enabled = false;
                }
            }

            {
                clsApplication TempApplication2 = clsApplication.FindApplicationByLDLID(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));

                if (!clsLicenses.IsLicenseHasCreatedFirstTime(TempApplication2.ApplicationID) &&  Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value) == 3)
                {
                    IssueDrivingLicenseFirstTimeToolStripMenuItem.Enabled=true;


                }

                else
                {
                    IssueDrivingLicenseFirstTimeToolStripMenuItem.Enabled=false;
                }


            }
        }


        private void scheduleVisionTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointmentForm frm = new TestAppointmentForm(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)
                , Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value) +1 );
            frm.Size = new Size(950, 700);
            frm.ShowDialog();
            List = clsApplication.ListLDLApplication().DefaultView; 
            //List = clsApplication.ListLDLApplication().DefaultView;
            //List = clsApplication.ListLDLApplication().DefaultView;
            dataGridView1.DataSource = List;

        }

        private void scheduleWrittenTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointmentForm frm = new TestAppointmentForm(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)
                , Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value) + 1);
            frm.Size = new Size(950, 700);
            frm.ShowDialog();
            List = clsApplication.ListLDLApplication().DefaultView;
            //List = clsApplication.ListLDLApplication().DefaultView;
            //List = clsApplication.ListLDLApplication().DefaultView;
            dataGridView1.DataSource = List;
        }

        private void scheduleStreetTestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TestAppointmentForm frm = new TestAppointmentForm(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)
                , Convert.ToInt32(dataGridView1.CurrentRow.Cells[5].Value) + 1);
            frm.Size = new Size(950, 700);
            frm.ShowDialog();
            List = clsApplication.ListLDLApplication().DefaultView;
            //List = clsApplication.ListLDLApplication().DefaultView;
            //List = clsApplication.ListLDLApplication().DefaultView;
            dataGridView1.DataSource = List;
        }

        private void IssueDrivingLicenseFirstTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            {
                clsApplication TempApplication2 = clsApplication.FindApplicationByLDLID(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));

                if ( ! clsLicenses.IsLicenseHasCreatedFirstTime(TempApplication2.ApplicationID)  )
                {

                    {
                        clsPeoples person = clsPeoples.FindPersonByNationalNumber(Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value));

                       
                        
                        clsDriver TempDriver = clsDriver.FindDriverExistByPersonID(person.PerID);

                       

                        if (TempDriver.DriverID == -1)
                        {
                            TempDriver.CreatedDate = DateTime.Now;
                            TempDriver.UserID = clsCurrentUser.CurrentUser.UserID;
                            TempDriver.PersonID = person.PerID;
                            TempDriver.Save();
                            MessageBox.Show("i have -1 driver id");
                        }

                        clsLocalDrivingLicenseApp MyLDLapp = clsLocalDrivingLicenseApp.FindLDLAppByLDLAppID(Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value));
                        clsLicenses License = clsLicenses.GetEmptyLicense();
                        License.Notes = "";
                        License.ApplicationID = MyLDLapp.ApplicationID;
                        License.LicenseClassID = MyLDLapp.LicenseClassID;
                        License.DriverID = -1;
                        License.PaidFees =  clsLicenseClasses.FindLicenseFeesByLicenseClassName(dataGridView1.CurrentRow.Cells[1].Value.ToString());
                        License.IssueDate = DateTime.Now;
                        License.ExpirationDate = DateTime.Now.AddYears(clsLicenseClasses.GetLicenseValidityLengthFromClassID(Convert.ToString(dataGridView1.CurrentRow.Cells[1].Value)));
                        License.IssueReason = 1;
                        License.DriverID = TempDriver.DriverID;
                        License.IsActive = true;
                        License.UserID = clsCurrentUser.CurrentUser.UserID;
                        License.Save();
                        TempApplication2.ChangeStatus(3);
                        MessageBox.Show(TempApplication2.ApplicationStatus.ToString());
                        MessageBox.Show("License has been created");
                        List = clsApplication.ListLDLApplication().DefaultView;
                        dataGridView1.DataSource = List;
                        dataGridView1.Refresh();
                    }
                }

                else
                {
                    MessageBox.Show("Person Already have License");
                }
            }
        }

        private void ShowLicenseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ShowLicenseForm frm = new ShowLicenseForm( clsLicenses.GetLicenseIDByAppID(clsLocalDrivingLicenseApp.FindLDLAppByLDLAppID
                (Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value)).ApplicationID ) );
            frm.Size = new Size(900, 900);
            frm.ShowDialog();
            
        }

        private void ShowPersonLicenseHistoryToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            MessageBox.Show(clsDriver.FindDriverExistByPersonID(clsPeoples.FindPersonByNationalNumber(Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value)).PerID).DriverID.ToString());

            LicenseHistory frm = new LicenseHistory(clsDriver.FindDriverExistByPersonID(clsPeoples.FindPersonByNationalNumber(Convert.ToString(dataGridView1.CurrentRow.Cells[2].Value)).PerID).DriverID);
            
            frm.Size = new Size(1100, 750);
            frm.ShowDialog();
        }
    }
}
