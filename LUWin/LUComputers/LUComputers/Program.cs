using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace LUComputers
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            try
            {
                //XmlConfigurationSource source = new XmlConfigurationSource(AppDomain.CurrentDomain.BaseDirectory + "\\castel.xml");
                //ActiveRecordStarter.Initialize(typeof(TempleteReplaceParameterModel).Assembly, source);

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1(args));

            }
            catch (Exception ex)
            {
                Helper.Logs.WriteErrorLog(ex);
            }
        }
    }
}
