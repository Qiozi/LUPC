using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunStore.Toolkits
{
    public class Util
    {
        public string GetCurrDateTime
        {
            get { return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fffff"); }
        }

        public static string FormatDateTime(string regdate)
        {
            if (string.IsNullOrEmpty(regdate) ||
                regdate.Length < 19)
                return regdate;

            return DateTime.Parse(regdate.Substring(0, 19)).ToString("yyyy-MM-dd HH:mm");
        }

        public static string FormatPrice(decimal price)
        {
            return price.ToString("##,###,###,###,##0.00");
        }
    }
}
