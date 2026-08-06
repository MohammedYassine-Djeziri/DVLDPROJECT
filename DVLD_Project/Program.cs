using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_Project.User.Forms;

namespace DVLD_Project
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            Form form = new LogInScreen(null);
            Size size = new Size();
            size.Width = 755;
            size.Height = 580;
            form.Size = size;
            System.Windows.Forms.Application.Run(form);
        }
    }
}
