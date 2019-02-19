using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DownloadEBayOrder.BLL
{
    public class HomePordModel
    {
        public int other_product_sku { get; set; }
        public int product_serial_no { get; set; }
        public string product_ebay_name { get; set; }
        public string new_href_url { get; set; }
        public string product_short_name { get; set; }
    }

    public class GenerateNewSiteHomeData : Events.EventBase
    {
        public GenerateNewSiteHomeData() { }

        public void Run(nicklu2Entities context)
        {
            SetStatus("Save all_cate.json file.");
            string path = System.Configuration.ConfigurationManager.AppSettings["NewSitePricePartHtmlStorePath"];
            path = path.Replace("\\parts_detail", "");
            path += "all_cate.json";

            var list = from c in context.tb_product_category
                       where c.menu_pre_serial_no.HasValue && c.menu_pre_serial_no.Value.Equals(0)
                       && c.tag.HasValue && c.tag.Value.Equals(1)
                       && c.is_view_menu.HasValue && c.is_view_menu.Value.Equals(true)
                       && c.menu_child_serial_no != 378
                       orderby c.menu_child_order ascending
                       select new
                       {
                           ID = c.menu_child_serial_no,
                           Name = c.menu_child_name,
                           SubList = (from s in context.tb_product_category where s.menu_pre_serial_no.HasValue && s.menu_child_serial_no != 378 && s.menu_pre_serial_no.Value.Equals(c.menu_child_serial_no) && s.tag.HasValue && s.tag.Value.Equals(1) orderby s.menu_child_order ascending select new { ID = s.menu_child_serial_no, Name = s.menu_child_name, PageType = s.page_category })
                       };

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(list.ToList());
            File.WriteAllText(path, json, Encoding.UTF8);
        }


        public void GenerateHomeCateFile(nicklu2Entities context)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["NewSitePricePartHtmlStorePath"];
            path = path.Replace("\\parts_detail", "");

            // 首页
            #region home
            SetStatus("Save HomeCate.txt file.");
            var catehome = (from pc in context.tb_product_category
                            where pc.tag.HasValue && pc.tag.Value.Equals(1)
                && pc.menu_pre_serial_no.HasValue
                && pc.is_view_menu.HasValue && pc.is_view_menu.Value.Equals(true)
                && pc.is_left_view.HasValue && pc.is_left_view.Value.Equals(true)
                            orderby pc.menu_child_order ascending
                            select new
                            {
                                Value = pc.menu_child_serial_no,
                                Text = pc.menu_child_name,
                                LogoFont = pc.menu_child_name_f
                            }).ToList();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(catehome);
            File.WriteAllText(path + "\\homeCate.txt", json);
            #endregion

            var cateList = context.tb_product_category.Where(p =>
                p.tag.HasValue && p.tag.Value.Equals(1)
                && p.menu_pre_serial_no.HasValue
                && p.is_view_menu.HasValue && p.is_view_menu.Value.Equals(true)
                && p.is_left_view.HasValue && p.is_left_view.Value.Equals(true)
               )
                .OrderBy(p => p.menu_child_order).ToList();

            foreach (var c in cateList)
            {
                var prodList = (from pl in context.tb_product
                                where pl.menu_child_serial_no.HasValue
                                   && pl.menu_child_serial_no.Value.Equals(c.menu_child_serial_no)
                                   && pl.tag.HasValue && pl.tag.Value.Equals(1)
                                   && (!pl.price_sku_quantity.HasValue || pl.price_sku_quantity < 2)
                                   && (!pl.other_product_sku.HasValue || pl.other_product_sku != 999999)
                                orderby pl.product_serial_no descending
                                select new HomePordModel
                                {
                                    other_product_sku = pl.other_product_sku.Value,
                                    product_serial_no = pl.product_serial_no,
                                    product_ebay_name = pl.product_ebay_name,
                                    new_href_url = pl.new_href_url,
                                    product_short_name = pl.product_short_name
                                }).Take(80).ToList();

                var prodList2 = (from pl in prodList
                                 join t in context.tb_ebay_selling on pl.product_serial_no equals t.luc_sku
                                 orderby t.WatchCount.Value descending
                                 select new HomePordModel
                                 {
                                     other_product_sku = pl.other_product_sku,
                                     product_serial_no = pl.product_serial_no,
                                     product_ebay_name = pl.product_ebay_name,
                                     new_href_url = pl.new_href_url,
                                     product_short_name = pl.product_short_name
                                 }).ToList();

                if (prodList2.Count < 6)
                {
                    var existSku = prodList2.Select(p => p.product_serial_no).ToList();
                    foreach (var item in prodList)
                    {
                        if (prodList2.Count(p => p.product_serial_no.Equals(item.product_serial_no)) == 0)
                        {
                            prodList2.Add(new HomePordModel
                            {
                                other_product_sku = item.other_product_sku,
                                product_serial_no = item.product_serial_no,
                                new_href_url = item.new_href_url,
                                product_ebay_name = item.product_ebay_name,
                                product_short_name = item.product_short_name
                            });
                        }
                    }
                }

                string cateListStr = "";
                int okcount = 0;

                var homeProdQuery = context.tb_pre_index_page_setting.Where(p => p.CateId.HasValue &&
                p.CateId.Value.Equals(c.menu_child_serial_no)).ToList();
                foreach (var m in homeProdQuery)
                {
                    //context.DeleteObject(m);
                    context.tb_pre_index_page_setting.Remove(m);
                }
                context.SaveChanges();
                foreach (var p in prodList2)
                {                    
                    if (okcount >= 4)
                    {
                        continue;
                    }
                    var model = new tb_pre_index_page_setting
                    {
                        case_p_X = 0,
                        case_p_Y = 0,
                        CateId = c.menu_child_serial_no,
                        Isys = false,
                        lcd_p_X = 0,
                        lcd_p_Y = 0,
                        sku = p.product_serial_no,
                        title = string.Empty,
                        LCDImage = string.Empty,
                        priority = 0
                    };
                    //context.AddTotb_pre_index_page_setting(model);
                    context.tb_pre_index_page_setting.Add(model);
                    context.SaveChanges();

                    string imageFilename = string.Format(@"C:\Workspaces\Web\pro_img\ebay_gallery\{0}\{1}_ebay_list_t_1.jpg"
                , p.other_product_sku > 0 ? p.other_product_sku.ToString().Substring(0, 1) : p.product_serial_no.ToString().Substring(0, 1)
                , p.other_product_sku > 0 ? p.other_product_sku : p.product_serial_no);
                    if (!File.Exists(imageFilename))
                    {
                        continue;
                    }
                    //
                    //data-original=""https://o9ozc36tl.qnssl.com/{0}.jpg?imageView/3/w/135/h/135""
                    //https://lucomputers.com/pro_img/ebay_gallery/{6}/{0}_ebay_list_t_1.jpg
                    okcount++;
                    cateListStr += string.Format(@"
                        <div class=""col-xs-12 col-sm-6 col-md-3""><a href=""{4}"">                            
                            <div class=""thumbnail"" >
                              <img class=""lazy"" src='https://lucomputers.com/pro_img/ebay_gallery/9/999999_ebay_list_t_1.jpg' data-original=""{6}"" width=""135"" alt=""..."">
                              <div class=""caption"" style="""">
                                <h5>{1}</h5>
                                {7}
								<a class='btn text-center priceAre' href='ShoppingCartTo.aspx?sku={5}'>
										<del>{2}</del>
										<span class=""price itemprice"" sku='{5}'>{3}</span>
										<span class='glyphicon glyphicon-shopping-cart'></span> Buy
								</a>
                              </div>
                            </div>
                            </a>
                          </div>"
                        , p.other_product_sku > 0 ? p.other_product_sku : p.product_serial_no
                        , string.IsNullOrEmpty(p.product_ebay_name) ? p.product_short_name : p.product_ebay_name
                        , "..."
                        , "..."
                        , (p.new_href_url ?? "").Length > 6 ? "/computer/parts_detail/" + p.new_href_url : "/detail_part.aspx?sku=" + p.product_serial_no
                        , p.product_serial_no
                        , GetImgFullname.Get(p.other_product_sku > 0 ? p.other_product_sku : p.product_serial_no, 135, 135, 0)
                        , GetEbayButtonString(context, p.product_serial_no)
                        );
                }
                SetStatus("Save home_cate_list_detail" + c.menu_child_serial_no + ".txt file.");
                File.WriteAllText(path + "home_cate_list_detail_" + c.menu_child_serial_no + ".txt", cateListStr, System.Text.Encoding.UTF8);
            }
        }

        static string GetEbayButtonString(nicklu2Entities context, int partSku)
        {
            if (partSku < 1)
                return "<a class='partEbayArea'>&nbsp;</a>";
            var query = context.tb_ebay_selling.SingleOrDefault(e => e.luc_sku.HasValue && e.luc_sku.Value.Equals(partSku));
            //return query != null ? @" <p class='partEbayArea'><a target=""_blank"" href=""http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item=" + query.ItemID + @"""><span title='eBay Price'>eBay: <span class='price'>$" + query.BuyItNowPrice + @"</span></span></a></p>" : "";
            return query != null ? @"<a class='partEbayArea' target=""_blank"" href=""http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item=" + query.ItemID + @"""><span title='eBay Price'><strong><span style='color:red'>e</span><span style='color:blue'>B</span><span style='color:#FF742E;'>a</span><span style='color:green;'>y</span>.ca:</strong> <span class='price'>$" + query.BuyItNowPrice + @"</span></span></a>" : "<a class='partEbayArea'>&nbsp;</a>";

        }
    }
}
