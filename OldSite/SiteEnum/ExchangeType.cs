using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteEnum
{
    public enum ExchangeType
    {
        None = 0,
        [Description("Get eBay onsale list")]
        GetEbayOnSaleList = 1,
        [Description("change eBay stock online.")]
        ChangeeBayStockQuantityOnline = 1
    }
}
