using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadEBayOrder.Model
{
    public class Cate 
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int ParentId { get; set; }

        public Enums.CateType CateType { get; set; }              
        
    }
}
