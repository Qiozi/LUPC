using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteModel
{
    public class PartPriceChangeSetting
    {
        public int CateId { get; set; }

        public decimal CostMin{get;set;}

        public decimal CostMax { get; set; }

        public decimal Rate { get; set; }

        public bool IsPercent { get; set; }
    }
}
