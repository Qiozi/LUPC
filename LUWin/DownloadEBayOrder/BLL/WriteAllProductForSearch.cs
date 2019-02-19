using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DownloadEBayOrder.BLL
{
    public class WriteAllProductForSearch
    {
        public static void Done(nicklu2Entities context)
        {
            var cates = GetCates(context);
            string path = System.Configuration.ConfigurationManager.AppSettings["NewSitePricePartHtmlStorePath"];
            string folderName = path.Replace("\\parts_detail", "");
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(cates);
            string filename = folderName + "\\validCates.txt";
            File.WriteAllText(filename, json);

            var products = GetAllProducts(context, cates.Select(me => me.Id).ToList());
            json = Newtonsoft.Json.JsonConvert.SerializeObject(products);
            filename = folderName + "\\ForSearch\\parts.txt";
            File.WriteAllText(filename, json);
        }

        /// <summary>
        /// all valid category info
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static List<Model.Cate> GetCates(nicklu2Entities context)
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
                            CateType = c.page_category.HasValue && c.page_category.Value == 0
                                            ? Enums.CateType.System :
                                            (c.is_noebook.HasValue && c.is_noebook.Value.Equals(1)
                                                ? Enums.CateType.Notebook : Enums.CateType.Part)
                        }).ToList();
            var parentIds = list.Select(me => me.Id).ToList();

            for (int i = 0; i < parentIds.Count; i++)
            {
                var item = parentIds[i];
                var subCates = (from p in context.tb_product_category
                                where p.tag.HasValue && p.tag.Value.Equals(1) &&
                                    p.menu_pre_serial_no.HasValue &&
                                    p.menu_pre_serial_no.Value.Equals(item) &&
                                    p.menu_child_serial_no != 378
                                orderby p.menu_child_order.Value ascending
                                select new Model.Cate
                                {

                                    ParentId = p.menu_pre_serial_no.HasValue ? p.menu_pre_serial_no.Value : 0,
                                    Id = p.menu_child_serial_no,
                                    Title = p.menu_child_name,
                                    CateType = p.page_category.HasValue && p.page_category.Value == 0
                                           ? Enums.CateType.System :
                                           (p.is_noebook.HasValue && p.is_noebook.Value.Equals(1)
                                               ? Enums.CateType.Notebook : Enums.CateType.Part)
                                }).ToList();
                list.AddRange(subCates);
            }

            return list;
        }

        public static List<Model.ProductForSearch> GetAllProducts(nicklu2Entities context,
            List<int> cateIds)
        {
            var ebayCode = context.tb_ebay_selling
                                  .Where(me =>
                                        (me.luc_sku.HasValue && me.luc_sku.Value > 0) ||
                                        (me.sys_sku.HasValue && me.sys_sku.Value > 0)).ToList();

            var query = context.tb_product.Where(p =>
                             p.tag.HasValue
                            && p.tag.Value.Equals(1)
                            && p.menu_child_serial_no.HasValue
                            && p.product_current_price.HasValue
                            && p.product_current_price.Value > 0
                            && cateIds.Contains(p.menu_child_serial_no.Value));


            // query.OrderBy(p=>p.other_product_sku).ThenBy(p => p.product_serial_no).ToList();

            var result = (from c in query.ToList()
                          orderby c.other_product_sku.Value ascending, c.product_serial_no descending
                          select new Model.ProductForSearch
                          {
                              ProduName = string.IsNullOrEmpty(c.product_ebay_name) || c.product_ebay_name.ToLower() == "null" ? c.product_name : c.product_ebay_name,
                              Sku = c.product_serial_no,
                              ShortName = c.product_short_name,
                              MFP = c.manufacturer_part_number,
                              MfpForFilename = c.manufacturer_part_number,
                              eBayCode = string.Empty,
                              Brand = c.producter_serial_no,
                              Keywords = c.keywords

                          }).ToList();
            for (int i = 0; i < result.Count; i++)
            {
                var ebayItem = ebayCode.FirstOrDefault(p => p.luc_sku.Value.Equals(result[i].Sku));
                if (ebayItem != null)
                {
                    result[i].eBayCode = ebayItem.ItemID;
                }
            }
            return result;
        }

    }
}
