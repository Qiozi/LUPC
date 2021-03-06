﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace DownloadEBayOrder
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Logs log = new Logs(Application.StartupPath);
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
            catch (Exception e)
            {
                log.WriteErrorLog(e);
            }
        }
    }
}
