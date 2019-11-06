using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YunStore.Model
{
    public class CommomItem
    {
        [Display(Name = "ID")]
        public int Sid { get; set; }

        [Display(Name = "名称")]
        public string Name { get; set; }

        [Display(Name = "是否被选中")]
        public bool Selected { get; set; }
    }
}
