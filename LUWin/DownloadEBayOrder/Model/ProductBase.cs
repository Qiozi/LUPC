using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadEBayOrder.Model
{
    public abstract class ProductPriceBase : ProductBase
    {
        public int CateId { get; set; }

        public decimal Price { get; set; }

        public string PriceUnit { get; set; }

        public decimal Discount { get; set; }
    }

    public abstract class ProductBase
    {
        public int Sku { get; set; }

        public string ProduName { get; set; }

        public string ShortName { get; set; }

        public string MFP { get; set; }

        public string MfpForFilename { get; set; }

    }

    public class ProductForSearch : ProductBase
    {

        public string Brand { get; set; }
        
        public string eBayCode { get; set; }
        
        public string Keywords { get; set; }        
        
    }
}
