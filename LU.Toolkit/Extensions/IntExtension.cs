using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Toolkit.Extensions
{
    public static class IntExtension
    {
        public static sbyte ToSbyte(this int val)
        {
            return sbyte.Parse(val.ToString());
        }

        public static int ToInt32(this int? val)
        {
            if (val.HasValue) return val.Value;
            return 0;
        }
    }
}
