using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.M
{
    public class SystemBase
    {
        public int PartSku { get; set; }

        public decimal PartPrice { get; set; }

        public string PartName { get; set; }

        public int ImgSku { get; set; }

        public string ShortNameForSystem { get; set; }

        public string eBayCode { get; set; }
    }
}
