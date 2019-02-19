using System;
using System.Collections.Generic;
using System.Text;

namespace LUComputers.Helper
{

    public class eBayPriceHelper
    {
        public eBayPriceHelper() { }

        /// <summary>
        /// each additional item Price
        /// </summary>
        /// <param name="shipping_fee"></param>
        /// <returns></returns>
        public static decimal eachAddItemShipping(decimal shipping_fee)
        {
            if (shipping_fee == 0M) return 0M;
            return decimal.Parse((shipping_fee / 1.33M).ToString("###.##"));
        }

        public static decimal eBayPartPrice(decimal cost, decimal screen, decimal adjustment)
        {
            decimal shipping_fee = BasicShippingFee(screen);
            return decimal.Parse(PR(cost, shipping_fee, adjustment).ToString("000")) - 0.01M;
        }

        /// <summary>
        /// 计算发布到eBay 上的价格
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="shipping_fee"></param>
        /// <param name="adjustment"></param>
        /// <returns></returns>
        public static decimal PR(decimal cost, decimal shipping_fee, decimal adjustment)
        {
            decimal a = cost;
            decimal b = shipping_fee;
            decimal e = adjustment;
            decimal c = 0M;
            if (a > 0M && a < 500M)
            {
                c = (a + 23M) * 1.022M;
            }
            else if (a > 500M && a < 1500M)
            {
                c = a * 1.05M * 1.022M;
            }
            else
            {
                c = a * 1.045M * 1.022M;
            }


            c = c + e;
            if (c > 0M && c < 1000M)
                return c + (4M + (c - 50M) * 0.05M) * 1.022M;
            else
                return c + (4M + 950M * 0.05M + (c - 1000M) * 0.02M) * 1.022M;
        }
        /// <summary>
        /// Basic shipping
        /// </summary>
        /// <param name="ScanSize">屏目尺寸</param>
        /// <returns></returns>
        public static decimal BasicShippingFee(decimal ScanSize)
        {
            if (ScanSize > 1M && ScanSize < 11.99M)
            {
                return 20M;
            }
            else if (ScanSize >= 12 && ScanSize < 14M)
            {
                return 25M;
            }
            else if (ScanSize >= 14 && ScanSize < 17M)
            {
                return 30M;
            }
            else
                return 40M;

        }

        /// <summary>
        /// expedited Canada, UPS 3days
        /// </summary>
        /// <param name="ScanSize">屏目尺寸</param>
        /// <returns></returns>
        public static decimal ExpeditedCanada_UPS3Days_ShippingFee(decimal ScanSize)
        {
            if (ScanSize > 1M && ScanSize < 11.99M)
            {
                return 35M;
            }
            else if (ScanSize >= 12M && ScanSize < 14M)
            {
                return 35M;
            }
            else if (ScanSize >= 14M && ScanSize < 17M)
            {
                return 45M;
            }
            else
                return 55M;
        }

        /// <summary>
        /// US 2days, World expedited
        /// </summary>
        /// <param name="ScanSize">屏目尺寸</param>
        /// <returns></returns>
        public static decimal US2days_WorldExpedited_ShippingFee(decimal ScanSize)
        {
            if (ScanSize > 1M && ScanSize < 11.99M)
            {
                return 55M;
            }
            else if (ScanSize >= 12M && ScanSize < 14M)
            {
                return 65M;
            }
            else if (ScanSize >= 14M && ScanSize < 17M)
            {
                return 95M;
            }
            else
                return 115M;
        }

        /// <summary>
        /// WorldExpress
        /// </summary>
        /// <param name="ScanSize">屏目尺寸</param>
        /// <returns></returns>
        public static decimal WorldExpedited_ShippingFee(decimal ScanSize)
        {
            if (ScanSize > 1M && ScanSize < 11.99M)
            {
                return 75M;
            }
            else if (ScanSize >= 12M && ScanSize < 14M)
            {
                return 85M;
            }
            else if (ScanSize >= 14M && ScanSize < 17M)
            {
                return 125M;
            }
            else
                return 185M;
        }
    }
}
