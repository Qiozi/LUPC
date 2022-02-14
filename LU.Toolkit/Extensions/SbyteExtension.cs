using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Toolkit.Extensions
{
    public static class SbyteExtension
    {
        public static int ToInt(this sbyte val)
        {
            return int.Parse(val.ToString());
        }

        public static bool ToBoolean(this sbyte val)
        {
            return val == 1;
        }

        public static bool ToBoolean(this sbyte? val)
        {
            if (val.HasValue)
                return val.Value == 1;
            return false;
        }

        public static sbyte ToSbyte(this sbyte? val)
        {
            if (val.HasValue)
                return val.Value;
            return 0;
        }
    }
}
