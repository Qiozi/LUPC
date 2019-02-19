using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.eBay
{
    public class eBayPartItem
    {
        public string Title { get; set; }

        public string ItemId { get; set; }

        public int Sku { get; set; }

        public string CustomLabel { get; set; }

        public decimal BuyItNowPrice { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }


    public class eBayAllGoods
    {
        public List<eBayGoodsItem> Goods { get; set; }
    }

    public class eBayGoodsItem
    {
        public Goods.Category Category { get; set; }

        public List<eBayPartItem> Children { get; set; }
    }
}
