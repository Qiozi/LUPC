using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class PRateProvider
    {
        static decimal _prate = 3M;

        public PRateProvider(Data.nicklu2Entities context)
        {
            PRate(context);
        }
        
        /// <summary>
        /// price rate
        /// </summary>
        public decimal PRate(Data.nicklu2Entities context)
        {
            if (_prate == 3M)
            {
                var rate = context.tb_currency_convert
                                  .Single(p => p.is_current.HasValue && 
                                               p.is_current.Value.Equals(true));
                _prate = rate.currency_usd.Value;
            }
            return _prate;
        }

        /// <summary>
        /// 两个decimal 相乖 确定到两位小数
        /// </summary>
        /// <param name="p1"></param>
        /// <param name="p2"></param>
        /// <returns></returns>
        public static decimal Multiply(decimal p1, decimal p2)
        {
            return decimal.Parse((p1 * p2).ToString("0.00"));
        }

        /// <summary>
        /// 价格转换， 如果是美国，转为美金价
        /// </summary>
        /// <param name="CADPrice"></param>
        /// <param name="CT"></param>
        /// <param name="Rate"></param>
        /// <returns></returns>
        public static decimal ConvertPrice(decimal CADPrice, LU.Model.Enums.CountryType currSite, decimal Rate)
        {
            if (currSite == Model.Enums.CountryType.CAD)
                return CADPrice;
            else
                return Multiply(CADPrice, Rate); // USD
        }

        public decimal ConvertPrice(decimal CADPrice, LU.Model.Enums.CountryType currSite)
        {
            return ConvertPrice(CADPrice, currSite, _prate);
        }
    }
}
