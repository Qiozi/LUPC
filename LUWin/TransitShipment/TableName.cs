using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TransitShipment
{
    public class TableName
    {
        public static string SynnexAll = "store_all_synnex";
        public static string SynnexAll2 = "store_all_synnex2";

        public static string DandhAll = "store_all_dandh";

        public static string AsiAll = "store_all_asi";

        public static string ALCAll = "store_all_alc";

        /// <summary>
        /// 不带日期
        /// </summary>
        /// <param name="ltdName"></param>
        /// <returns></returns>
        public static string GetPriceTableNamePart(string ltdName)
        {
            if (ltdName.IndexOf("-") > -1)
                ltdName = ltdName.Replace("-", "_");
            return "store_" + ltdName.ToLower() + "_part_";
        }


    }
}
