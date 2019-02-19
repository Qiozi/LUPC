using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteModels.View
{
    [Serializable]
    public class ProdList
    {
        public ProdList() { }

        public string ImgUrl { get; set; }

        public string Name { get; set; }

        public int Sku { get; set; }

        public int CateId { get; set; }

        public decimal price { get; set; }

        public decimal discount { get; set; }

        public decimal sell { get; set; }
    }
}
