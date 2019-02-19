using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteModel
{
    public class ValidProd
    {
        public int Sku { get; set; }

        public string Mfp { get; set; }

        public decimal Price { get; set; }

        public decimal Cost { get; set; }

        public decimal Discount { get; set; }

        public int Stock { get; set; }

        public int CategoryId { get; set; }

        public string Brand { get; set; }

        public decimal Adjustment { get; set; }

        public string ProdType { get; set; }
    }
}
