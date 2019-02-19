using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteDB.Product
{
    public class GetValidCategory
    {
        public static List<int> GetValidCategoryIds(nicklu2Entities context)
        {
            var parentIds = (from c in context.tb_product_category
                             where c.tag.HasValue && c.tag.Value.Equals(1) &&
                             c.menu_pre_serial_no.HasValue && c.menu_pre_serial_no.Value.Equals(0)
                             select new
                             {
                                Id =  c.menu_child_serial_no
                             }).ToList().Select(p=>p.Id).ToList();
            return (from c in context.tb_product_category
                    where c.tag.HasValue && c.tag.Value.Equals(1) &&
                    c.menu_pre_serial_no.HasValue && parentIds.Contains(c.menu_pre_serial_no.Value)
                    select new
                    {
                        Id = c.menu_child_serial_no
                    }).ToList().Select(p=>p.Id).ToList();
        }
    }
}
