using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class ConfigAdmin
    {
        public static  string CookiesDomain
        {
            get
            {
                return System.Configuration.ConfigurationManager.AppSettings["CookiesDomain"].ToString();
            }
        }
    }
}
