using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.ModelV1
{
    public class PartForBrandListItem
    {
        public string logo { get; set; }
        public string name { get; set; }
        public decimal price { get; set; }
        public string webHref { get; set; }
        public int Sku { get; set; }
        public string eBayCode { get; set; }
        public string eBayHref { get; set; }
        public decimal eBayPrice { get; set; }

        public string MFP { get; set; }
    }
}
