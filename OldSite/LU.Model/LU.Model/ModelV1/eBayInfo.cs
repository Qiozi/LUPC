using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.ModelV1
{
    public class eBayInfo
    {
        public string Title { get; set; }

        public string Code { get; set; }

        public decimal Price { get; set; }

        public string PriceText
        {
            get { return Price == 0M ? "--" : Price.ToString("$#,##0.00"); }
        }
    }
}
