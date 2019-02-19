using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace LUComputers.Helper
{
    public class Compare
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ltd"></param>
        public static bool ViewCompare(Ltd ltd)
        {
            string ltd_name = new LtdHelper().FilterText(ltd.ToString());
            Watch.ComparePrice CP = new LUComputers.Watch.ComparePrice();
            return CP.Execute(ltd) ? WriteFile(ltd, CP) : false;
        }

        static bool WriteFile(Ltd ltd, Watch.ComparePrice cp)
        {
            HttpHelper HH = new HttpHelper();
            string filename = string.Format(@"{0}{1}", System.AppDomain.CurrentDomain.BaseDirectory, "_s.html");

            if (!File.Exists(filename))
            {
                File.CreateText(filename);
            }

            StreamWriter sw = new StreamWriter(filename);
            sw.Write(HH.GetHttpHead(cp.GETHTMLResult(ltd)));
            sw.Close();
            sw.Dispose();
            return true;
        }
    }
}
