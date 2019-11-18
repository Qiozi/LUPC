using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunStore.BLL
{
    public class Config
    {
        public static string StaffName { get; set; }

        public static Guid StaffGid { get; set; }

        /// <summary>
        /// 完全路径。
        /// </summary>
        public static string DBFullname
        {
            get
            {
                var dbPath = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('\\') + "\\db";
                if (!Directory.Exists(dbPath))
                {
                    Directory.CreateDirectory(dbPath);
                }                

                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db\\kkw.db");
            }
        }
    }
}
