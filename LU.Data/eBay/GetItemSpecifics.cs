using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.Data.eBay
{
    public class GetItemSpecifics
    {
        public GetItemSpecifics()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 取得系统规格
        /// </summary>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> GetSystemSpecifics(nicklu2Entities context, int sysSku)
        {
            var result = new List<KeyValuePair<string, string>>();

            result.Add(new KeyValuePair<string, string>("Brand", "Custom, Whitebox"));
            result.Add(new KeyValuePair<string, string>("MPN", "Does not apply"));
            result.Add(new KeyValuePair<string, string>("UPC", "Does not apply"));

            //DataTable dt = Config.ExecuteDataTable("Select ItemSpecificsName, ItemSpecificsValue from tb_ebay_system_item_specifics where system_sku='" + sysSku + "'");
            var items = (from c in context.tb_ebay_system_item_specifics
                         where c.system_sku.Equals(sysSku)
                         select new
                         {
                             c.ItemSpecificsName,
                             c.ItemSpecificsValue
                         }).ToList();

            foreach (var item in items)
            {
                if (!string.IsNullOrEmpty(item.ItemSpecificsValue) && item.ItemSpecificsName.ToLower() != "brand")
                {
                    result.Add(new KeyValuePair<string, string>(item.ItemSpecificsName, item.ItemSpecificsValue));
                }
            }
            if (result.Count == 3)
            {
                var partTitles = (from c in context.tb_ebay_system_parts
                                  join p in context.tb_product on c.luc_sku.Value equals p.product_serial_no
                                  join t in context.tb_ebay_system_part_comment on c.comment_id.Value equals t.id
                                  select new
                                  {
                                      t.comment,
                                      p.short_name_for_sys
                                  }).ToList();
                string hdd = "";
                foreach (var item in partTitles)
                {
                    if (item.comment == "Memory")
                    {
                        result.Add(new KeyValuePair<string, string>("Memory", string.IsNullOrEmpty(item.short_name_for_sys) ? "Not Included" : item.short_name_for_sys));
                    }

                    if (item.comment == "Windows OS")
                    {

                        result.Add(new KeyValuePair<string, string>("Operating System", string.IsNullOrEmpty(item.short_name_for_sys) ? "Not Included" : item.short_name_for_sys));
                    }

                    if (item.comment == "Windows OS")
                    {
                        result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", ""));
                    }

                    if (item.comment == "CPU")
                    {
                        result.Add(new KeyValuePair<string, string>("Processor Type", string.IsNullOrEmpty(item.short_name_for_sys) ? "Not Included" : item.short_name_for_sys));
                    }

                    if (item.comment == "Optical Drive")
                    {
                        result.Add(new KeyValuePair<string, string>("Primary Drive", string.IsNullOrEmpty(item.short_name_for_sys) ? "Not Included" : item.short_name_for_sys));

                    }

                    if (item.comment == "Video")
                    {
                        result.Add(new KeyValuePair<string, string>("Graphics Processing Type", string.IsNullOrEmpty(item.short_name_for_sys) ? "Not Included" : item.short_name_for_sys));
                    }

                    if (item.comment == "Power Supply")
                    {
                        result.Add(new KeyValuePair<string, string>("Power Supply", string.IsNullOrEmpty(item.short_name_for_sys) ? "Not Included" : item.short_name_for_sys));
                    }

                    if (item.comment == "SSD")
                    {
                        result.Add(new KeyValuePair<string, string>("SSD", string.IsNullOrEmpty(item.short_name_for_sys) ? "Not Included" : item.short_name_for_sys));
                    }
                    if (item.comment.IndexOf("Hard Drive") > -1)
                    {
                        hdd += item.short_name_for_sys + " ";
                    }
                }
                if (hdd != "")
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive", hdd));

                }
            }
            return result;
        }

        /// <summary>
        /// 取得零件，笔记本规格
        /// </summary>
        /// <param name="LucSku"></param>
        /// <returns></returns>
        public static List<KeyValuePair<string, string>> GeneratePartSpecifics(nicklu2Entities context, tb_product pm)
        {
            List<KeyValuePair<string, string>> result = new List<KeyValuePair<string, string>>();

            result.Add(new KeyValuePair<string, string>("UPC", string.IsNullOrEmpty(pm.UPC) ? "Does not apply" : pm.UPC));
            result.Add(new KeyValuePair<string, string>("MPN", string.IsNullOrEmpty(pm.manufacturer_part_number) ? "Does not apply" : pm.manufacturer_part_number));

            if (!string.IsNullOrEmpty(pm.producter_serial_no))
            {
                result.Add(new KeyValuePair<string, string>("Brand", pm.producter_serial_no));
            }
            var partCate = context.tb_product_category.SingleOrDefault(p => p.menu_child_serial_no.Equals(pm.menu_child_serial_no.Value) &&
                p.is_noebook.HasValue && p.is_noebook.Value.Equals(1));
            if (partCate != null)
            {
                if (pm.screen_size.HasValue && pm.screen_size.Value > 0M)
                {
                    result.Add(new KeyValuePair<string, string>("Screen Size", pm.screen_size.Value.ToString().TrimEnd(new char[] { '0', '.' }) + "\""));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("ultrabook") > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Type", "Ultrabook"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("notebook") > -1 ||
                    pm.product_ebay_name.ToLower().IndexOf("laptop") > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Type", "Notebook"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("tablet") > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Type", "Tablet"));
                }
                if (pm.product_ebay_name.ToLower().IndexOf("notbook") > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Type", "Netbook"));

                }
                if (pm.product_ebay_name.ToLower().IndexOf("w8") > -1 ||
                    pm.product_ebay_name.ToLower().IndexOf("win8") > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Operating", "Windows 8"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("w10") > -1 ||
                    pm.product_ebay_name.ToLower().IndexOf("win10") > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Operating", "Windows 10"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("w7") > -1
                    || pm.product_ebay_name.ToLower().IndexOf("win7") > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Operating", "Windows 7"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("4GB RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "4 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("4G RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "4 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("6GB RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "6 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("6G RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "6 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("12GB RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "12 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("12G RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "12 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("16GB RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "16 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("16G RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "16 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("8GB RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "8 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("8G RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "8 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("24GB RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "24 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("24G RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "24 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("32GB RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "32 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("32G RAM".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Memory", "32 GB"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("128G SSD".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "128 GB SSD"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("120G SSD".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "120 GB SSD"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("240G SSD".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "240 GB SSD"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("512G SSD".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "512 GB SSD"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("1TB SSD".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "1TB SSD"));
                }

                if (pm.product_ebay_name.ToLower().IndexOf("320G HDD".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "320 GB"));
                }
                if (pm.product_ebay_name.ToLower().IndexOf("500G HDD".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "500 GB"));
                }
                if (pm.product_ebay_name.ToLower().IndexOf("640G HDD".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "640 GB"));
                }
                if (pm.product_ebay_name.ToLower().IndexOf("750G HDD".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "750 GB"));
                }
                if (pm.product_ebay_name.ToLower().IndexOf("1TB HDD".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "1 TB"));
                }
                if (pm.product_ebay_name.ToLower().IndexOf("1.5TB HDD".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Hard Drive Capacity", "1.5 TB"));
                }
                if (pm.product_ebay_name.ToLower().IndexOf(" i3".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Processor Type", "Intel Core i3"));
                }
                if (pm.product_ebay_name.ToLower().IndexOf(" i5".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Processor Type", "Intel Core i5"));
                }
                if (pm.product_ebay_name.ToLower().IndexOf(" i7".ToLower()) > -1)
                {
                    result.Add(new KeyValuePair<string, string>("Processor Type", "Intel Core i7"));
                }
            }
            return result;
        }

        public static string GetPartSpecifics(nicklu2Entities context, tb_product pm)
        {
            var list = GeneratePartSpecifics(context, pm);

            if (list.Count > 1)
            {
                string str = "<ItemSpecifics>";
                foreach (var k in list)
                {
                    str += string.Format(@"
        <NameValueList>
            <Name>{0}</Name>
            <Value>{1}</Value>
        </NameValueList>
  ", k.Key, k.Value);
                }
                str += "</ItemSpecifics>";
                return str;
            }
            else
                return "";
        }
    }
}
