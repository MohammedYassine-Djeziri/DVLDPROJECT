
using DVLDBusinessLayer;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;



namespace DVLD_Project.Users
{
    public partial class LogInScreen : Form
    {
        public MainMenu frm = null;
        public LogInScreen(MainMenu fr_m)
        {
            InitializeComponent();
            if (fr_m != null)
            {
                fr_m.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_LOGIN_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(DateTime.Now.AddYears(-1).ToShortDateString());
            //just a test for the admin user to login without checking the database
            //MessageBox.Show(UserName.Text + " " + Password.Text);
            // if (UserName.Text == "admin"|| Password.Text == "admin")
            // {
            //     //clsCurrentUser.CurrentUser = new clsUsers("admin", "admin", -1, true);
            //     Size size = new Size();
            //     size.Width = 1400;
            //     size.Height = 900;
            //     this.Hide();
            //     if (frm != null)
            //     {
            //         frm.Size = size;
            //         frm.Show();
            //     }
            //     else
            //     {
            //         MainMenu frm2 = new MainMenu(333, 444, this);
            //         frm2.Size = size;
            //         frm2.Show();
            //     }
            // }
            // else{
            clsCurrentUser.CurrentUser=clsUsers.FindByUserNamePass(UserName.Text, Password.Text);
            if(clsCurrentUser.CurrentUser == null )
            {
                MessageBox.Show("Invalid UserName/Password");
            }
            else
            {
                if(checkBox1.Checked)
                {
                    string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\LogInInfo";
                    string UserNameShortcut = "UserName";
                    string UserNameValue = UserName.Text;
                    string PasswordShortcut = "Password";
                    string PasswordValue = Password.Text;


                    try
                    {
                        // Write the value to the Registry
                        Registry.SetValue(keyPath, UserNameShortcut, UserNameValue, RegistryValueKind.String);
                        Registry.SetValue(keyPath, PasswordShortcut, PasswordValue, RegistryValueKind.String);


                        //Console.WriteLine($"Value {valueName} successfully written to the Registry.");
                    }
                    catch (Exception ex)
                    {
                       
                    }



                                  }
                if (clsCurrentUser.CurrentUser.IsActive)
                {
                    //MainMenu frm = new MainMenu(333, 444 , this);
                    Size size = new Size();
                    size.Width = 1400;
                    size.Height = 900;
                    
                    this.Hide();
                    if (frm != null)
                    {
                        frm.Size = size;
                        frm.Show();
                    }
                    else
                    {
                        MainMenu frm2 = new MainMenu(333, 444, this);
                        frm2.Size = size;
                        frm2.Show();
                    }
                }
                else
                {
                    MessageBox.Show("Your account is not active please contact your admin");
                }

                
            }
            //}
        }
        
        private void LogInScreen_Load(object sender, EventArgs e)
        {

            string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\LogInInfo";
            string UserNameShortcut = "UserName";
            string PasswordShortcut = "Password";


            try
            {
                // Read the value from the Registry
                string UserNameValue  = Registry.GetValue(keyPath, UserNameShortcut, null) as string;

                string PasswordValue =  Registry.GetValue(keyPath, PasswordShortcut, null) as string;

                if (UserNameValue != null && PasswordValue != null)
                {
                    UserName.Text = UserNameValue;
                    Password.Text = PasswordValue;
                    checkBox1.Checked = true;
                }
                
            }
            catch (Exception ex)
            {
                
            }

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(!checkBox1.Checked)
            {
                string keyPath = @"HKEY_CURRENT_USER\SOFTWARE\LogInInfo";
                string UserNameShortcut = "UserName";
                string PasswordShortcut = "Password";


                try
                {
                    // Write the value to the Registry
                    Registry.SetValue(keyPath, UserNameShortcut, "", RegistryValueKind.String);
                    Registry.SetValue(keyPath, PasswordShortcut, "" , RegistryValueKind.String);


                    //Console.WriteLine($"Value {valueName} successfully written to the Registry.");
                }
                catch (Exception ex)
                {

                }


            }
        }
    }
}