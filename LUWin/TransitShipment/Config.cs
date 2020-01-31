using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace TransitShipment
{
    public class Config
    {
        public static string ConnString
        {
            get { return ConfigurationManager.ConnectionStrings["LU"].ConnectionString.ToString(); }
        }
           
        public static string cost_file_path
        {
            get { return ConfigurationManager.AppSettings["tmpSavePath"].ToString(); }
        }

    }
}
