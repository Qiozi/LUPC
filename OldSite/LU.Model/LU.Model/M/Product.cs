using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model
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

    public class PartInfoForCache : ProductBase
    {

    }

    public class PriceForCache : ProductPriceBase
    {

    }

    public class Product : ProductPriceBase
    {
        public string Brand { get; set; }

        public string Avatar { get; set; }

        public string eBayCode { get; set; }

        public decimal eBayPrice { get; set; }

        public string Keywords { get; set; }

        public string Specifications { get; set; }

        public string eBayPriceText
        {
            get { return eBayPrice == 0M ? "--" : eBayPrice.ToString("$#,##0.00"); }
        }

        public string SoldText
        {
            get
            {
                return (Price - Discount).ToString("$#,##0.00");
            }
        }

        public bool HaveDiscount
        {
            get
            {
                return Discount > 0;
            }
        }

        public bool HaveEbay
        {
            get { return !string.IsNullOrEmpty(eBayCode); }
        }

        public string WebHref
        {
            get
            {
                return string.Concat("computer", "/", MfpForFilename, "/", Sku, ".html");
            }
        }

        public List<string> AvatarGallery { get; set; }        
    }
}
