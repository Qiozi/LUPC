using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Model
{
    [Serializable]
    public class Cate : Base.Cate
    {
        public int ParentId { get; set; }

        public Enums.CateType CateType { get; set; }

        public string IconName { get; set; }

        public string IconName2 { get; set; }

        public string Href { set; get; }

        public bool ViewForHome { get; set; }

        public string CateNameLogogram { get; set; }

        public List<Cate> SubCates { get; set; }
    }
}
