using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model
{
    public class eBayMiniModel
    {
        public bool IsSys
        {
            get { return Sku.ToString().Length > 5; }
        }
        public int Sku { get; set; }

        public string ItemId { get; set; }

        public decimal BuyItNowPrice { get; set; }

    }
}
