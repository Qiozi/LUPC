using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KKWStore
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //XmlConfigurationSource source = new XmlConfigurationSource("castel.xml");
            //ActiveRecordStarter.Initialize(typeof(ProductModel).Assembly, source);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
