using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Toolkit.Extensions
{
    public static class BoolExtension
    {
        public static sbyte ToSbyte(this bool val)
        {
            return val ? sbyte.Parse("1") : sbyte.Parse("0");
        }

        public static sbyte ToSbyte(this bool? val)
        {
            if (val.HasValue == false) return sbyte.Parse("0");

            return val.Value ? sbyte.Parse("1") : sbyte.Parse("0");
        }

        public static bool ToBool(this bool? val)
        {
            return val.HasValue ? val.Value : false;
        }
    }
}
