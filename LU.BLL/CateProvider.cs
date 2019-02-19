using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL
{
    public class CateProvider
    {
        public static List<Model.Cate> GetCates(LU.Data.nicklu2Entities context)
        {
            var query = context.tb_product_category
                                    .Where(p => p.tag.HasValue && 
                                                p.tag.Value.Equals(1) && 
                                                p.menu_pre_serial_no.HasValue &&
                                                p.menu_pre_serial_no.Value.Equals(0) &&
                                                p.is_view_menu.HasValue && 
                                                p.is_view_menu.Value.Equals(true) &&
                                                p.menu_child_serial_no != 378)

                                     .OrderBy(p => p.menu_child_order)
                                     .ToList();

            var list = (from c in query
                        select new Model.Cate
                        {
                            ParentId = c.menu_pre_serial_no.HasValue ? c.menu_pre_serial_no.Value : 0,
                            Id = c.menu_child_serial_no,
                            Title = c.menu_child_name,
                            IconName = c.menu_child_name_f,
                            ViewForHome = true,
                            CateType = c.page_category.HasValue && c.page_category.Value == 0
                                            ? Model.Enums.CateType.System :
                                            (c.is_noebook.HasValue && c.is_noebook.Value.Equals(1)
                                                ? Model.Enums.CateType.Notebook : Model.Enums.CateType.Part)
                        }).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                item.SubCates = (from p in context.tb_product_category
                                 where p.tag.HasValue && p.tag.Value.Equals(1) &&
                                     p.menu_pre_serial_no.HasValue &&
                                     p.menu_pre_serial_no.Value.Equals(item.Id) &&
                                     p.menu_child_serial_no != 378 

                                 orderby p.menu_child_order.Value ascending
                                 select new Model.Cate
                                 {
                                     CateNameLogogram = p.menu_child_name_logogram,
                                     ParentId = p.menu_pre_serial_no.HasValue ? p.menu_pre_serial_no.Value : 0,
                                     Id = p.menu_child_serial_no,
                                     Title = p.menu_child_name,
                                     IconName = p.menu_child_name_f,
                                     IconName2 = p.font_name,
                                     ViewForHome = p.is_view_menu.HasValue && p.is_view_menu.Value && p.is_left_view.HasValue && p.is_left_view.Value,
                                     CateType = p.page_category.HasValue && p.page_category.Value == 0
                                            ? Model.Enums.CateType.System :
                                            (p.is_noebook.HasValue && p.is_noebook.Value.Equals(1)
                                                ? Model.Enums.CateType.Notebook : Model.Enums.CateType.Part)
                                 }).ToList();
            }
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = 0; j < list[i].SubCates.Count; j++)
                {
                    list[i].SubCates[j].Href = (list[i].SubCates[j].CateType == Model.Enums.CateType.System ?
                        LU.BLL.Util.CateUrl(list[i].SubCates[j].Id, "", Model.Enums.CateType.System) :
                        LU.BLL.Util.CateUrl(list[i].SubCates[j].Id, list[i].SubCates[j].CateNameLogogram, Model.Enums.CateType.Part));
                }
            }
            return list;
        }

        public static List<Model.Cate> GetCatesForHome(LU.Data.nicklu2Entities context)
        {
            return null;
        }
    }
}
