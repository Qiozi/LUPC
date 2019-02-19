using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class FormatProvider
    {
        public static string Price(decimal price)
        {
            return price.ToString("#,###,###,##0.00");
        }
    }
}
