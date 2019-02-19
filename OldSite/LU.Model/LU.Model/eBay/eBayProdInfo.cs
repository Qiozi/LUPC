using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.eBay
{
    public class eBayProdInfo
    {
        public int LUCSku { get; set; }

        public decimal eBayPrice { get; set; }

        public string Title { get; set; }

        public string eBayItemId { get; set; }

        public int ImgSku { get; set; }
    }
}
