using SiteApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteApi.Controllers
{
    [CmdFilter]
    public class SetProdToEbaySaleController : BaseApiController
    {
        //
        // GET: /SetProdToEbaySale/

        public Models.PostResult Get(string t, int sku)
        {
            var eBaySelling = DBContext.tb_ebay_selling.SingleOrDefault(p => p.luc_sku.HasValue &&
                p.luc_sku.Value.Equals(sku));
            var result = Add(eBaySelling.luc_sku.Value, eBaySelling.BuyItNowPrice.Value, eBaySelling.ItemID);
            return new Models.PostResult
            {
                Success = result,
                ErrMsg = result ? "Success" : "Fail"
            };
        }

        bool Add(int sku, decimal price, string itemId)
        {
            var priceItem = GetPriceItem(price);
            var item = new LU.Data.tb_ebay_promotional_items
            {
                IsSys = false,
                ItemId = itemId,//TODO
                luc_sku = sku,
                Regdate = DateTime.Now,
                SaleId = string.Empty,
                SavePrice = priceItem.RealPrice
            };

            DBContext.tb_ebay_promotional_items.Add(item);
            DBContext.SaveChanges();
            return true;
        }

        Models.eBayOnSale.eBayOnSalePriceArea.PriceItem GetPriceItem(decimal price)
        {
            if (price < 100M && price > 40M)
            {
                return Models.eBayOnSale.eBayOnSalePriceArea.PriceArea(10M);
            }

            else if (price > 100M && price < 200M)// (discount == 20M)
            {
                return Models.eBayOnSale.eBayOnSalePriceArea.PriceArea(20M);
            }
            else if (price > 200M && price < 300M)// (discount == 30M)
            {
                return Models.eBayOnSale.eBayOnSalePriceArea.PriceArea(30M);
            }
            else if (price > 300M && price < 800M)// (discount == 40M)
            {
                return Models.eBayOnSale.eBayOnSalePriceArea.PriceArea(40M);
            }
            throw new FormatException("no exist price area.");
        }
    }
}
