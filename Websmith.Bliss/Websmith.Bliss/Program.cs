﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Websmith.Bliss
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new frmSocketServer());
            //Application.Run(new frmSocketTest());
            Application.Run(new frmEmployeeLogin());            
            //Application.Run(new Form1());
        }
    }
}
