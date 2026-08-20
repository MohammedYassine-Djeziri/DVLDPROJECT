
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
using DVLD_Project.Global;
using DVLD_Project.Global.Forms;
using MainMenuForm = DVLD_Project.Global.Forms.MainMenu;



namespace DVLD_Project.User.Forms
{
    public partial class LogInScreen : Form
    {
        public LogInScreen()
        {
            InitializeComponent();
            
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_LOGIN_Click(object sender, EventArgs e)
        {
            
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
                    // Store the stored hash (never the plain-text password)
                    // so "Remember me" can re-log-in without keeping the
                    // real password around. CurrentUser.Password holds the
                    // salted hash returned by the login verification.
                    string PasswordValue = clsCurrentUser.CurrentUser.Password;


                    try
                    {
                        // Write the value to the Registry
                        Registry.SetValue(keyPath, UserNameShortcut, UserNameValue, RegistryValueKind.String);
                        Registry.SetValue(keyPath, PasswordShortcut, PasswordValue, RegistryValueKind.String);
                    }
                    catch (Exception ex)
                    {
                       
                    }



                                  }
                if (clsCurrentUser.CurrentUser.IsActive)
                {
                    // Always create a fresh MainMenu and show it modally.
                    //
                    // NOTE: We must NOT reuse an already-existing MainMenu
                    // instance via ShowDialog(). After a sign-out the previous
                    // MainMenu was only Hidden (never closed), so it is still in
                    // a modal state. Calling ShowDialog() on an already-modal
                    // form throws:
                    //   "Form that is already displayed modally cannot be
                    //    displayed as a modal dialog box."
                    // Instead, sign-out closes the MainMenu (which returns from
                    // the ShowDialog below) and we simply re-show the login.
                    Size size = new Size();
                    size.Width = 1400;
                    size.Height = 900;

                    this.Hide();
                    MainMenuForm frm2 = new MainMenuForm(size.Width, size.Height);
                    frm2.Size = size;
                    frm2.ShowDialog();

                    // The MainMenu dialog just closed. Either the user signed out
                    // (re-show login for a new session) or closed the window
                    // (exit the application).
                    if (frm2.SignOutRequested && !this.IsDisposed)
                    {
                        this.Show();
                    }
                    else if (!this.IsDisposed)
                    {
                        // Real exit -> close the application's main form.
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Your account is not active please contact your admin");
                }

                
            }
            
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

       
    }
}