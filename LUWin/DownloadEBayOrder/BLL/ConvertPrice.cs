using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DownloadEBayOrder.BLL
{
    public class ConvertPrice
    {
        /// <summary>
        /// 当前美金汇率
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
        public static decimal Rate(nicklu2Entities db)
        {
            var rate = db.tb_currency_convert.Single(p => p.is_current.HasValue && p.is_current.Value.Equals(true));
            return rate.currency_usd.Value;
        }

        /// <summary>
        /// 判断当前是CAD 还是USD ，
        /// 如果是美金，
        /// 然后加币转换为美金
        /// </summary>
        /// <param name="CADPrice"></param>
        /// <param name="Rate"></param>
        /// <returns></returns>
        public static decimal Converter(decimal CADPrice, decimal Rate, bool IsCAD)
        {
            if (IsCAD)
                return CADPrice;
            else
                return Multiply(CADPrice, Rate);
        }

        public static decimal Multiply(decimal p1, decimal p2)
        {
            return decimal.Parse((p1 * p2).ToString("0.00"));
        }
    }
}
