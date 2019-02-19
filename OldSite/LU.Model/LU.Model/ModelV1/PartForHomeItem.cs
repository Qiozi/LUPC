using LU.Model.ModelV1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.M.ModelV1
{
    [Serializable]
    public class PartForHomeItem
    {
        public class Part
        {
            public class BaseInfoItem
            {
                public int Sku { get; set; }

                public string ProductName { get; set; }

                public string ShortName { get; set; }

                public string MFP { get; set; }

                public string MfpForFilename { get; set; }

                public int CateId { get; set; }

                public decimal Price { get; set; }

                public string PriceUnit { get; set; }

                public decimal Discount { get; set; }

                public string Avatar { get; set; }

                public string Brand { get; set; }

            }
            public BaseInfoItem BaseInfo { get; set; }

            public eBayInfo eBayInfo { get; set; }
        }

        public string CateName { get; set; }

        public int CateId { get; set; }

        public Enums.CateType CateType { get; set; }

        public string IconName { get; set; }

        public string IconName2 { get; set; }

        public string WebUrl { set; get; }

        public List<Part> Parts { get; set; }
    }
}
