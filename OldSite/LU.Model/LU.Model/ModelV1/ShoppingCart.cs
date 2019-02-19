using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.ModelV1
{
    public class ShoppingCart
    {
        public int OrderCode { get; set; }

        public decimal SubTotal { get; set; }

        public string SubTotalText { get; set; }

        public List<ShoppingListModel> Goods { get; set; }
    }
}
