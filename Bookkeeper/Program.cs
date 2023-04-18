using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bookkeeper
{
    internal static class Program
    {
        public static bool validLogin = false;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LogOnForm());

            if (validLogin)
            {
                Application.Run(new MainForm());
            }
        }
    }
}
