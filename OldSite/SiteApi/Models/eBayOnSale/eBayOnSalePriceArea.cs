using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SiteApi.Models.eBayOnSale
{

    public class eBayOnSalePriceArea
    {
        public class PriceItem
        {
            public PriceItem() { }
            public decimal MinPrice { get; set; }
            public decimal MaxPrice { get; set; }
            public decimal RealPrice { get; set; }
        }

        public static PriceItem PriceArea(decimal discount)
        {
            decimal minPrice = 0M;
            decimal maxPrice = 0M;
            decimal realDiscount = 0M;

            if (discount == 50M)
            {
                minPrice = 500M;
                maxPrice = 1000M;
                realDiscount = 45M;
            }
            else if (discount == 100M)
            {
                minPrice = 1000M;
                maxPrice = 2000M;
                realDiscount = 90M;
            }
            else if (discount == 180M)
            {
                minPrice = 2000M;
                maxPrice = 3600M;
                realDiscount = 160M;
            }
            else if (discount == 10M)
            {
                minPrice = 50;
                maxPrice = 100;
                realDiscount = 8;
            }
            else if (discount == 20M)
            {
                minPrice = 100;
                maxPrice = 200;
                realDiscount = 17;
            }
            else if (discount == 30M)
            {
                minPrice = 200;
                maxPrice = 300;
                realDiscount = 25;
            }
            else if (discount == 40M)
            {
                minPrice = 300;
                maxPrice = 800;
                realDiscount = 35;
            }
            return new PriceItem
            {
                MinPrice = minPrice,
                MaxPrice = maxPrice,
                RealPrice = realDiscount
            };
        }
    }
}