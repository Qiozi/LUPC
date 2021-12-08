using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Toolkit.Extensions
{
    public static class DecimalExtension
    {        
        public static decimal ToDecimal(this decimal? val)
        {
            if (val.HasValue) return val.Value;
            return 0M;
        }

    }
}
