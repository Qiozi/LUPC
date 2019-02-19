using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class eBayProvider
    {
        public static string GetEBayHref(string eBayCode)
        {
            if (string.IsNullOrEmpty(eBayCode))
                return string.Empty;
            else
                return string.Concat(BLL.Config.eBayUrl, eBayCode);
        }

        public static List<Model.eBayMiniModel> GetAllParts(Data.nicklu2Entities context)
        {
            return (from c in context.tb_ebay_selling
                    where (c.luc_sku.HasValue && c.luc_sku.Value > 0) ||
                    (c.sys_sku.HasValue && c.sys_sku.Value > 0)
                    select new Model.eBayMiniModel
                    {
                        ItemId = c.ItemID,
                        Sku = (c.luc_sku.HasValue && c.luc_sku.Value > 0) ? c.luc_sku.Value : c.sys_sku.Value,
                        BuyItNowPrice = c.BuyItNowPrice.HasValue ? c.BuyItNowPrice.Value : 0M
                    }).ToList();
        }


        public static string GetItemIdByPartSku(Data.nicklu2Entities context, int partSku)
        {
            var query = CacheProvider.GeteBayCodes(context).FirstOrDefault(p => p.Sku.Equals(partSku));
            return query == null ? string.Empty : query.ItemId;
        }
    }
}
