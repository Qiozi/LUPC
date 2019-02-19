using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AutoExecute
{
    public class config
    {
        public static string ServerIP
        {
            get { return ConfigurationManager.AppSettings["ServerIP"].ToString(); }
        }

        public static string ServerPort
        {
            get { return ConfigurationManager.AppSettings["ServerPort"].ToString(); }
        }

        public static List<KeyValuePair<string, string>> CmdList
        {
            get
            {
                string str = ConfigurationManager.AppSettings["RunList"].ToString();
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                string[] strs = str.Split(new char[] { ';' });
                foreach (var s in strs)
                {
                    if (s.IndexOf(',') > 0)
                        list.Add(new KeyValuePair<string, string>(s.Split(new char[] { ',' })[0], s.Split(new char[] { ',' })[1]));
                }
                return list;
            }
        }
    }
}
