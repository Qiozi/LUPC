using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace DownloadEBayOrder.BLL
{
    public class GenerateNewSiteProductInfo : Events.EventBase
    {
        public GenerateNewSiteProductInfo()
        {

        }

        List<int> GetCates(nicklu2Entities context)
        {
            var result = new List<int>();
            var query = context.tb_product_category.Where(p => p.tag.HasValue && p.tag.Value.Equals(1)
           && p.menu_pre_serial_no.HasValue &&
           p.menu_pre_serial_no.Value.Equals(0) &&
           p.is_view_menu.HasValue && p.is_view_menu.Value.Equals(true) &&
                     p.menu_child_serial_no != 378)
           .OrderBy(p => p.menu_child_order).ToList();

            var list = (from c in query
                        select new
                        {
                            Id = c.menu_child_serial_no
                        }).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                var item = list[i];
                var ids = (from p in context.tb_product_category
                           where p.tag.HasValue && p.tag.Value.Equals(1) &&
                               p.menu_pre_serial_no.HasValue &&
                               p.menu_pre_serial_no.Value.Equals(item.Id) &&
                               p.menu_child_serial_no != 378
                           orderby p.menu_child_order.Value ascending
                           select new
                           {
                               Id = p.menu_child_serial_no
                           }).Select(p => p.Id).ToList();
                foreach (var id in ids)
                {
                    result.Add(id);
                }
            }
            return result;
        }

        void GenerateSiteMap(nicklu2Entities context)
        {
            SetStatus("create site map.xml");
            var mapsiteXml = @"<?xml version=""1.0"" encoding=""UTF-8""?>";
            mapsiteXml += @"<urlset xmlns=""http://www.sitemaps.org/schemas/sitemap/0.9"">";
            var cateids = GetCates(context);
            var query = context.tb_product.Where(p => p.tag.HasValue && p.tag.Value.Equals(1) 
            && p.menu_child_serial_no.HasValue && cateids.Contains(p.menu_child_serial_no.Value)
            && !string.IsNullOrEmpty(p.manufacturer_part_number)
            && !string.IsNullOrEmpty(p.producter_serial_no)).ToList();
            foreach (var item in query)
            {
                mapsiteXml += string.Format(@"<url><loc>{0}</loc></url>",
                    "https://www.lucomputers.com/computer/" + (GeneratePartHtmlFile.FilterFileName(string.IsNullOrEmpty(item.manufacturer_part_number) ? string.Concat(item.producter_serial_no, item.product_serial_no) : item.manufacturer_part_number)) + "/" + item.product_serial_no + ".html");
            }
            var sysList413 = (from s in context.tb_ebay_selling
                              join sp in context.tb_ebay_system on s.sys_sku.Value equals sp.id
                              where s.sys_sku.HasValue && s.sys_sku.Value > 0 && sp.is_shrink.HasValue && !sp.is_shrink.Value
                               && sp.is_barebone.HasValue && !sp.is_barebone.Value
                               && s.BuyItNowPrice.HasValue && s.BuyItNowPrice.Value > 300M
                              select new
                              {
                                  SysSKU = s.sys_sku,
                                  eBayId = s.ItemID,
                                  eBayPrice = s.BuyItNowPrice.Value,
                                  eBayTitle = s.Title,
                                  Price = 0M,
                                  Discount = 0M
                              }).ToList();
            foreach (var item in sysList413)
            {
                mapsiteXml += string.Format(@"<url><loc>{0}</loc></url>",
                   "https://www.lucomputers.com/computer/system/" + item.SysSKU + ".html");

            }

            // categories
            var categories = context.tb_product_category.Where(p => !string.IsNullOrEmpty(p.menu_child_name_logogram)).ToList();
            foreach(var cate in categories)
            {
                mapsiteXml += string.Format(@"<url><loc>{0}</loc></url>",
                  "https://www.lucomputers.com/computers/" + cate.menu_child_name_logogram + ".html");

            }

            // brand
            var brands = context.tb_producter.Where(p => !string.IsNullOrEmpty(p.logo_url)).ToList();
            foreach (var brand in brands)
            {
                mapsiteXml += string.Format(@"<url><loc>{0}</loc></url>",
                  "https://www.lucomputers.com/brand/" + GeneratePartHtmlFile.FilterFileName(brand.producter_name) + ".html");

            }
            mapsiteXml += "</urlset>";

            string path = System.Configuration.ConfigurationManager.AppSettings["NewSitePricePartHtmlStorePath"];
            string folderName = path.Replace("\\parts_detail", "");
            string filename = string.Concat(folderName, "\\sitemap.xml");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(mapsiteXml);
            doc.Save(filename);

            // File.WriteAllText(filename, mapsiteXml);
        }

        /// <summary>
        /// 生成零件与系统的列表数据
        /// </summary>
        public void GenerateListData(nicklu2Entities context)
        {
            context = new nicklu2Entities();
            string path = System.Configuration.ConfigurationManager.AppSettings["NewSitePricePartHtmlStorePath"];
            string folderName = path.Replace("\\parts_detail", "");
            GenerateSiteMap(context);

            #region parts
            var cateList = context.tb_product_category.Where(p => p.tag.HasValue
                && p.tag.Value.Equals(1)
                ).ToList();

            #region 清除不需要的数据
            var prodQuery = context.tb_product.Where(c =>
                              c.menu_child_serial_no.HasValue &&
                             ((c.split_line.HasValue && c.split_line.Value.Equals(0)) || !c.split_line.HasValue) &&
                             c.tag.HasValue && c.tag.Value.Equals(1) &&
                             ((c.product_store_sum.HasValue && c.product_store_sum.Value > 0) || (c.ltd_stock.HasValue && c.ltd_stock.Value > 0)))
                             .ToList();
            SetStatus("remove part no qty.");

            #endregion

            foreach (var c in cateList)
            {
                SetStatus("create list.txt " + c.menu_child_serial_no.ToString());
                int cid = c.menu_child_serial_no;
                string dirName = folderName + "Parts\\" + cid;
                if (!Directory.Exists(dirName))
                {
                    Directory.CreateDirectory(dirName);
                }
                #region CAD
                {
                    string filename = dirName + "\\list.txt";

                    var prodList =
                        (from p in prodQuery
                         where p.menu_child_serial_no.Value.Equals(cid)
                         orderby p.other_product_sku.Value ascending, p.product_serial_no descending
                         select new
                         {
                             SKU = p.product_serial_no,
                             ShortName = p.product_short_name,
                             Name = !string.IsNullOrEmpty(p.product_ebay_name) ? p.product_ebay_name : p.product_name,
                             Price = p.product_current_price.Value,
                             Discount = p.product_current_discount.HasValue ? p.product_current_discount.Value : 0,
                             Sold = p.product_current_price.Value - p.product_current_discount.Value,
                             ImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no,
                             IsOnsale = p.product_current_discount > 0 ? 1 : 0,
                             IsRebate = 0,
                             priceUnit = "CAD",
                             Keyword = p.keywords.ToLower(),
                             PageUrl = string.IsNullOrEmpty(p.new_href_url) ? "" : p.new_href_url
                         }).ToList();

                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(prodList);
                    File.WriteAllText(filename, json);
                }
                {
                    string filename = dirName + "\\list-us.txt";
                    var rate = BLL.ConvertPrice.Rate(context);
                    var prodList =
                        (from p in prodQuery
                         where p.menu_child_serial_no.Value.Equals(cid)
                         orderby p.other_product_sku.Value ascending, p.product_serial_no descending
                         select new
                         {
                             SKU = p.product_serial_no,
                             ShortName = p.product_short_name,
                             Name = !string.IsNullOrEmpty(p.product_name_long_en) ? p.product_name_long_en : p.product_name,
                             Price = p.product_current_price.Value * rate,
                             Discount = (p.product_current_discount.HasValue ? p.product_current_discount.Value : 0M) * rate,
                             Sold = (p.product_current_price.Value - p.product_current_discount.Value) * rate,
                             ImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no,
                             IsOnsale = p.product_current_discount > 0 ? 1 : 0,
                             IsRebate = 0,
                             priceUnit = "USD",
                             Keyword = p.keywords.ToLower(),
                             PageUrl = string.IsNullOrEmpty(p.new_href_url) ? "" : p.new_href_url
                         }).ToList();

                    string json = Newtonsoft.Json.JsonConvert.SerializeObject(prodList);
                    File.WriteAllText(filename, json);
                }
                #endregion
            }

            #endregion

            #region system

            string dirsys = path.Replace("\\parts_detail", "") + "systems";
            if (!Directory.Exists(dirsys))
            {
                Directory.CreateDirectory(dirsys);
            }
            string dirsys2 = folderName + "systems\\detail";
            if (!Directory.Exists(dirsys2))
            {
                Directory.CreateDirectory(dirsys2);
            }

            string file412 = dirsys + "\\412.txt";
            string file413 = dirsys + "\\413.txt";
            string file414 = dirsys + "\\414.txt";
            // List<int> sysSku = new List<int>();

            // cid= 412 (barebone) 
            // 413 (gaming &overclocking)   价格大于1000
            // 414(business / home pc)      价格小于1000
            // 

            // 412
            var sysListBareone = (from s in context.tb_ebay_selling
                                  join sp in context.tb_ebay_system on s.sys_sku.Value equals sp.id
                                  where s.sys_sku.HasValue && s.sys_sku.Value > 0 && sp.is_shrink.HasValue && !sp.is_shrink.Value
                                   && sp.is_barebone.HasValue && sp.is_barebone.Value
                                  select new
                                  {
                                      SysSKU = s.sys_sku,
                                      eBayId = s.ItemID,
                                      eBayPrice = s.BuyItNowPrice.Value,
                                      eBayTitle = s.Title,
                                      Price = 0M,
                                      Discount = 0M
                                  }).ToList();
            List<SysInfo> sys412 = new List<SysInfo>();
            foreach (var sys in sysListBareone)
            {
                SetStatus("Sys:" + sys.SysSKU.ToString());
                sys412.Add(new SysInfo()
                {
                    Discount = "0",
                    eBayId = sys.eBayId,
                    eBayPrice = sys.eBayPrice,
                    eBayTitle = sys.eBayTitle,
                    Price = "0",
                    SysSKU = sys.SysSKU.Value
                });
            }

            // 413
            var sysList413 = (from s in context.tb_ebay_selling
                              join sp in context.tb_ebay_system on s.sys_sku.Value equals sp.id
                              where s.sys_sku.HasValue && s.sys_sku.Value > 0 && sp.is_shrink.HasValue && !sp.is_shrink.Value
                               && sp.is_barebone.HasValue && !sp.is_barebone.Value
                               && s.BuyItNowPrice.HasValue && s.BuyItNowPrice.Value > 1000M
                              select new
                              {
                                  SysSKU = s.sys_sku,
                                  eBayId = s.ItemID,
                                  eBayPrice = s.BuyItNowPrice.Value,
                                  eBayTitle = s.Title,
                                  Price = 0M,
                                  Discount = 0M
                              }).ToList();
            List<SysInfo> sys413 = new List<SysInfo>();
            foreach (var sys in sysList413)
            {
                SetStatus("Sys:" + sys.SysSKU.ToString());
                sys413.Add(new SysInfo()
                {
                    Discount = "0",
                    eBayId = sys.eBayId,
                    eBayPrice = sys.eBayPrice,
                    eBayTitle = sys.eBayTitle,
                    Price = "0",
                    SysSKU = sys.SysSKU.Value
                });
            }
            // 414
            var sysList414 = (from s in context.tb_ebay_selling
                              join sp in context.tb_ebay_system on s.sys_sku.Value equals sp.id
                              where s.sys_sku.HasValue && s.sys_sku.Value > 0 && sp.is_shrink.HasValue && !sp.is_shrink.Value
                               && sp.is_barebone.HasValue && !sp.is_barebone.Value
                               && s.BuyItNowPrice.HasValue && s.BuyItNowPrice.Value <= 1000M
                              select new
                              {
                                  SysSKU = s.sys_sku,
                                  eBayId = s.ItemID,
                                  eBayPrice = s.BuyItNowPrice.Value,
                                  eBayTitle = s.Title,
                                  Price = 0M,
                                  Discount = 0M
                              }).ToList();

            List<SysInfo> sys414 = new List<SysInfo>();
            foreach (var sys in sysList414)
            {
                SetStatus("Sys:" + sys.SysSKU.ToString());
                sys414.Add(new SysInfo()
                {
                    Discount = "0",
                    eBayId = sys.eBayId,
                    eBayPrice = sys.eBayPrice,
                    eBayTitle = sys.eBayTitle,
                    Price = "0",
                    SysSKU = sys.SysSKU.Value
                });
            }
            // delete
            DirectoryInfo dir = new DirectoryInfo(dirsys2);
            FileInfo[] fs = dir.GetFiles();
            foreach (var f in fs)
            {
                SetStatus("Delete file :" + f.FullName.ToString());
                try
                {
                    File.Delete(f.FullName);
                }
                catch { }
            }

            for (int i = 0; i < sys412.Count; i++)
            {
                int sku = sys412[i].SysSKU;
                string filename = dirsys2 + "\\" + sku + ".txt";
                SetStatus("Create file :" + filename.ToString());
                var sys = (from es in context.tb_ebay_system_parts
                           join ec in context.tb_ebay_system_part_comment on es.comment_id.Value equals ec.id
                           join p in context.tb_product on es.luc_sku.Value equals p.product_serial_no
                           where es.system_sku.HasValue && es.system_sku.Value.Equals(sku) && p.product_serial_no != 16684
                           orderby ec.priority ascending
                           select new
                           {
                               Comment = ec.comment,
                               PartTitle = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_name : p.product_ebay_name,
                               PartImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku : p.product_serial_no,
                               PartSku = p.product_serial_no,
                               PartPrice = p.product_current_price.Value,
                               PartDiscount = p.product_current_discount.Value,
                               ShortNameForSys = p.short_name_for_sys
                           }).ToList();
                decimal price = sys.Select(p => p.PartPrice).Sum();
                decimal dis = sys.Select(p => p.PartDiscount).Sum();
                //Response.Write(price.ToString() + "<br>");
                sys412[i].Price = price.ToString();
                sys412[i].Sold = dis == 0M ? price.ToString() : (price - dis).ToString();
                sys412[i].Discount = dis == 0M ? "0" : dis.ToString();

                File.WriteAllText(filename, Newtonsoft.Json.JsonConvert.SerializeObject(sys));
            }

            for (int i = 0; i < sys413.Count; i++)
            {
                int sku = sys413[i].SysSKU;
                string filename = dirsys2 + "\\" + sku + ".txt";
                SetStatus("Create file :" + filename.ToString());
                var sys = (from es in context.tb_ebay_system_parts
                           join ec in context.tb_ebay_system_part_comment on es.comment_id.Value equals ec.id
                           join p in context.tb_product on es.luc_sku.Value equals p.product_serial_no
                           where es.system_sku.HasValue && es.system_sku.Value.Equals(sku) && p.product_serial_no != 16684
                           orderby ec.priority ascending
                           select new
                           {
                               Comment = ec.comment,
                               PartTitle = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_name : p.product_ebay_name,
                               PartImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku : p.product_serial_no,
                               PartSku = p.product_serial_no,
                               PartPrice = p.product_current_price.Value,
                               PartDiscount = p.product_current_discount.Value,
                               ShortNameForSys = p.short_name_for_sys
                           }).ToList();
                decimal price = sys.Select(p => p.PartPrice).Sum();
                decimal dis = sys.Select(p => p.PartDiscount).Sum();
                sys413[i].Price = price.ToString();
                sys413[i].Sold = dis == 0M ? price.ToString() : (price - dis).ToString();
                sys413[i].Discount = dis == 0M ? "0" : (dis).ToString();

                File.WriteAllText(filename, Newtonsoft.Json.JsonConvert.SerializeObject(sys));
            }

            for (int i = 0; i < sys414.Count; i++)
            {
                int sku = sys414[i].SysSKU;
                string filename = dirsys2 + "\\" + sku + ".txt";
                SetStatus("Create file :" + filename.ToString());
                var sys = (from es in context.tb_ebay_system_parts
                           join ec in context.tb_ebay_system_part_comment on es.comment_id.Value equals ec.id
                           join p in context.tb_product on es.luc_sku.Value equals p.product_serial_no
                           where es.system_sku.HasValue && es.system_sku.Value.Equals(sku) && p.product_serial_no != 16684
                           orderby ec.priority ascending
                           select new
                           {
                               Comment = ec.comment,
                               PartTitle = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_name : p.product_ebay_name,
                               PartImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku : p.product_serial_no,
                               PartSku = p.product_serial_no,
                               PartPrice = p.product_current_price.Value,
                               PartDiscount = p.product_current_discount.Value,
                               ShortNameForSys = p.short_name_for_sys
                           }).ToList();
                decimal price = sys.Select(p => p.PartPrice).Sum();
                decimal dis = sys.Select(p => p.PartDiscount).Sum();
                sys414[i].Price = price.ToString();
                sys414[i].Sold = dis == 0M ? price.ToString() : (price - dis).ToString();
                sys414[i].Discount = dis == 0M ? "0" : (dis).ToString();

                File.WriteAllText(filename, Newtonsoft.Json.JsonConvert.SerializeObject(sys));
            }

            string json412 = Newtonsoft.Json.JsonConvert.SerializeObject(sys412);
            File.WriteAllText(file412, json412);
            string json413 = Newtonsoft.Json.JsonConvert.SerializeObject(sys413);
            File.WriteAllText(file413, json413);
            string json414 = Newtonsoft.Json.JsonConvert.SerializeObject(sys414);
            File.WriteAllText(file414, json414);
            #endregion
        }

        /// <summary>
        /// 删除零件缓存
        /// </summary>
        public void DeletePartPriceCanche()
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["NewSitePricePartHtmlStorePath"];
            string folderName = path.Replace("\\parts_detail", "\\part_price");

            DirectoryInfo dir = new DirectoryInfo(folderName);
            FileInfo[] fis = dir.GetFiles();

            foreach (var f in fis)
            {
                SetStatus("Delete file " + f.FullName);
                File.Delete(f.FullName);
            }
        }

        /// <summary>
        /// 关闭无效的keyword
        /// </summary>
        public void PartKey(nicklu2Entities context, Logs log)
        {
            // 还原状态
            Config.ExecuteDataTable("update tb_product_category_keyword_sub set IsShowOnWebPage=0");
            var query = context.tb_product_category_keyword.Where(p => p.showit.HasValue && p.showit.Value.Equals(true)).ToList();
            foreach (var item in query)
            {
                try
                {
                    var parentId = item.id;
                    var subQuery = context.tb_product_category_keyword_sub.Where(p => p.parent_id.HasValue
                        && p.parent_id.Value.Equals(parentId)).ToList();

                    foreach (var sub in subQuery)
                    {
                        SetStatus("Close valid keyword filter:" + sub.keyword);
                        try
                        {
                            if (string.IsNullOrEmpty(item.keyword))
                            {
                                continue;
                            }
                            var keyword = string.Format("[{0}]", sub.keyword);
                            var cateid = item.category_id.Value;
                            var product = context.tb_product.Count(p => p.keywords.Contains(keyword) && p.tag.HasValue && p.tag.Value.Equals(1)
                                && p.menu_child_serial_no.HasValue && p.menu_child_serial_no.Value.Equals(cateid));
                            if (product > 0)
                            {
                                sub.IsShowOnWebPage = true;
                            }
                            //SetStatus(string.Format("{0} -- {1} ", item.keyword, sub.keyword));
                        }
                        catch (Exception ex)
                        {
                            log.WriteErrorLog(ex);
                        }
                    }
                }
                catch (Exception ex)
                {
                    log.WriteErrorLog(ex);
                }
            }
            context.SaveChanges();
            //SetStatus("part keyword end: " + DateTime.Now.ToString());
        }
    }


}
