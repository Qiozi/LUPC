using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.M
{
    public class SysMiniBase
    {
        public int SysSku { get; set; }

        public string eBayId { get; set; }

        public decimal eBayPrice { get; set; }

        public string eBayTitle { get; set; }

        public decimal Price { get; set; }

        public decimal Discount { get; set; }

        public decimal Sold { get; set; }
    }

    public class SysMiniModel : SysMiniBase
    {
        public List<SysMiniSubPartEBay> SysMiniSubParts { get; set; }

        public int CateId { get; set; }
    }

    public class SysMiniSubPartEBay : SysMiniSubPart
    {
        public string eBayCode { get; set; }

        public string eBayHref { get; set; }
    }

    public class SysMiniSubPart
    {
        public string Comment { get; set; }

        public string ShortNameForSys { get; set; }

        public string PartTitle { get; set; }

        public int PartImgSku { get; set; }

        public int PartSku { get; set; }

        public decimal PartPrice { get; set; }

        public decimal PartDiscount { get; set; }
    }
}
