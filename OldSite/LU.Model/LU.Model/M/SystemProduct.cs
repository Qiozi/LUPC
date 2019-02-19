using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model
{
    public class SystemProduct
    {
        public int Id { get; set; }

        public int LogoSku { get; set; }

        public string LogoUrl
        {
            get; set;
        }
        public string PriceUnit { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Sold
        {
            get { return Price - Discount; }
        }

        public string Title { get; set; }

        public string eBayCode { get; set; }

        public List<SysPart> Parts { get; set; }

        public decimal eBaySold { get; set; }

        public string eBayHref { get; set; }

        public int OldCateId { get; set; }

        public string CateTitle { get; set; }

        public int Priority { get; set; }
    }

    public class SysPart
    {
        public int Sku { get; set; }

        public string Title
        {
            get
            {
                return string.IsNullOrEmpty(eBayTitle) ? WebTitle : eBayTitle;
            }
        }

        public int ImgSku { get; set; }

        public string MFP { get; set; }

        public string eBayTitle { get; set; }

        public string WebTitle { get; set; }

        public int CommentId { get; set; }

        public string CommentName { get; set; }

        public string eBayCode { get; set; }

        public decimal Price { get; set; }

        public string PriceUnit { get; set; }

        public decimal Discount { get; set; }

        public string eBayHref { get; set; }

        public string WebHref { get; set; }
        
        public int GroupId { get; set; }
    }
}
