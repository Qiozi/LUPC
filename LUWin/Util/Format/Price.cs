using System;
using System.Collections.Generic;
using System.Text;

namespace Util.Format
{
    public class Price
    {
        public static string FormatString(decimal price)
        {
            return price.ToString("$###,###.00");
        }
    }
}
