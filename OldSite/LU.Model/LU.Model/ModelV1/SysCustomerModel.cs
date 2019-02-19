using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model.ModelV1
{
    public class SysCustomerModel
    {
        public SysCustomerModel() { }

        public int SysSKU { get; set; }

        public string CommentName { get; set; }

        public int CommentID { get; set; }

        public int GroupID { get; set; }

        public int PartSKU { get; set; }

        public string PartTitle { get; set; }

        public decimal Sold { get; set; }
    }
}
