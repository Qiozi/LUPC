using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL.eBay
{
    public class ItemSpecifices
    {
        public static string GetPartSpecifices(LU.Data.nicklu2Entities context, int sku)
        {
            var result = string.Empty;
            var query = context.tb_ebay_system_item_specifics.Where(p => p.system_sku.Equals(sku)).ToList();
            foreach (var item in query)
            {
                result += string.Concat(" ", item.ItemSpecificsName.Replace("\"", ""), " : ", item.ItemSpecificsValue.Replace("\"", ""), " ;");
            }
            return result;
        }
    }
}
