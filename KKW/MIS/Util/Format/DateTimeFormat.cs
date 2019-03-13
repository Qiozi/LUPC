using System;
using System.Collections.Generic;
using System.Text;

namespace Util
{
    public class DateTimeFormat
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToString(DateTime dt)
        {
            return dt.ToString("yyyyMMddhhmmss");
        }

        /// <summary>
        /// 2000-00-00 00:00:00
        /// 2000-00-00 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateTimeString(string dt)
        {
            return ToDateTimeString(dt, false);
        }
        /// <summary>
        /// 2000-00-00 00:00:00
        /// 2000-00-00 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string ToDateTimeString(string dt, bool IsShortFormat)
        {
            if (dt.Trim().Length != 14 && dt.Trim().Length !=13)
                return null;
            try
            {
                if (IsShortFormat)
                {
                    return string.Format("{0}-{1}-{2}"
                        , dt.Substring(0, 4)
                        , dt.Substring(4, 2)
                        , dt.Substring(6, 2)
                       );
                }
                else
                {
                    return string.Format("{0}-{1}-{2} {3}:{4}:{5}"
                        , dt.Substring(0, 4)
                        , dt.Substring(4, 2)
                        , dt.Substring(6, 2)
                        , dt.Substring(8, 2)
                        , dt.Substring(10, 2)
                        , dt.Substring(12, 2));
                }
            }
            catch { }
            return null;
        }
    }
}
