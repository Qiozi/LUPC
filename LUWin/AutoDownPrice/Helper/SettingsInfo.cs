using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoDownPrice.Helper
{
    public class SettingsInfo
    {
        public static string tmpSavePath
        {

            get { return System.Configuration.ConfigurationManager.AppSettings["tmpSavePath"].ToString(); }
        }

        public static int beginHour
        {
            get { return int.Parse(ConfigurationManager.AppSettings["beginHour"].ToString()); }
        }

        public static int endHour
        {
            get { return int.Parse(ConfigurationManager.AppSettings["endHour"].ToString()); }
        }

        public static int intervalSecond
        {
            get { return int.Parse(ConfigurationManager.AppSettings["intervalSecond"].ToString()); }
        }

        public static bool AutoRun
        {
            get { return ConfigurationManager.AppSettings["AutoRun"].ToString() == "1"; }
        }
    }
}
