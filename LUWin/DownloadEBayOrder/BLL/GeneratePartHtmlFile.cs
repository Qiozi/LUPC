using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Data;

namespace DownloadEBayOrder.BLL
{
    public class GeneratePartHtmlFile : Events.EventBase
    {
        public GeneratePartHtmlFile()
        {

        }

        public void Run(nicklu2Entities context, Logs log)
        {
            string path = System.Configuration.ConfigurationManager.AppSettings["NewSitePricePartHtmlStorePath"];

            // 删除已存在的
            var dir = new DirectoryInfo(path);
            FileInfo[] fis = dir.GetFiles();
            //foreach (var f in fis)
            //{
            //    SetStatus("Delete file:" + f.FullName);
            //    if (DateTime.Now.DayOfWeek == DayOfWeek.Friday)
            //    {
            //        File.Delete(f.FullName);
            //    }
            //}
            //var fileContent = File.ReadAllText(Application.StartupPath + "\\doc\\part_templete.html");
            var allProd = context.tb_product.ToList();
            foreach (var prod in allProd)
            {
                prod.new_href_url = string.Empty;
            }
            context.SaveChanges();

            List<int> cateParents = new List<int>();
            cateParents.Add(1);
            cateParents.Add(2);

            foreach (var cateparentid in cateParents)
            {
                var parentTitle = context.tb_product_category.Single(p => p.menu_child_serial_no.Equals(cateparentid)).menu_child_name;

                var parentCate = context.tb_product_category.Where(p => p.menu_pre_serial_no.Value.Equals(cateparentid)
                    && p.tag.Value.Equals(1)
                    && p.page_category.Value.Equals(1)
                    && p.is_view_menu.Value.Equals(true))
                    .ToList();

                var parentCates = parentCate.Select(p => p.menu_child_serial_no).ToList();
                var cateHrefList = "";
                foreach (var c in parentCate)
                {
                    cateHrefList += "<li><a href='/list_part.aspx?cid=" + c.menu_child_serial_no + "'>" + c.menu_child_name + "</a></li>";
                }

                foreach (var cate in parentCates)
                {
                    var cateTitle = parentCate.Single(p => p.menu_child_serial_no.Equals(cate)).menu_child_name;

                    var prodlist = context.tb_product.Where(p =>
                          p.menu_child_serial_no.HasValue
                            && p.menu_child_serial_no.Value.Equals(cate)
                            && p.tag.HasValue
                            && p.tag.Value.Equals(1)).ToList();

                    foreach (var prod in prodlist)
                    {
                        // var title = string.IsNullOrEmpty(prod.product_ebay_name) ? prod.product_name : prod.product_ebay_name;
                        var sku = prod.product_serial_no;
                        var cid = prod.menu_child_serial_no.Value;
                        var mfp = prod.manufacturer_part_number;
                        var band = prod.producter_serial_no;
                        var ImgSku = prod.other_product_sku > 0 ? prod.other_product_sku.Value : prod.product_serial_no;
                        // var content = WebClientHelper.GetPage(WebUrl.GetPartDetailUrl(sku));

                        try
                        {
                            var title = FilterFileName(mfp);
                            title += "/computer/" + mfp + "/" + sku + ".html";
                            SetStatus("Create file:" + title);
                            prod.new_href_url = title;

                            // ile.WriteAllText(saveFilename, content);
                        }
                        catch (Exception ex)
                        {
                            log.WriteErrorLog(ex);
                        }
                    }

                    GenerateSuggestList(context, parentCate.Single(p => p.menu_child_serial_no.Equals(cate)));

                    context.SaveChanges();
                }
            }
        }

        void GenerateSuggestList(nicklu2Entities context, tb_product_category pc)
        {
            var qty = 5;
            var cid = pc.menu_child_serial_no;
            var result = string.Empty;
            var orderProd = context.tb_order_product.OrderByDescending(p => p.serial_no).Where(p => p.menu_child_serial_no.HasValue && p.menu_child_serial_no.Value.Equals(cid) &&
                p.product_serial_no.HasValue).Select(p => p.product_serial_no).Distinct().OrderByDescending(p => p.Value).Take(qty).ToList();
            if (orderProd.Count == qty)
            {
                foreach (var pp in orderProd)
                {
                    var prod = context.tb_product.Single(p => p.product_serial_no.Equals(pp.Value));
                    result += GetSuggest(prod);
                }

            }
            else
            {
                var ban = qty - orderProd.Count;
                var prods = context.tb_product.Where(p => p.menu_child_serial_no.HasValue &&
                    p.menu_child_serial_no.Value.Equals(cid) &&
                    p.tag.HasValue && p.tag.Value.Equals(1)).OrderByDescending(p => p.product_serial_no).Take(ban).ToList();
                foreach (var pp in prods)
                {
                    result += GetSuggest(pp);
                }

            }
            string path = System.Configuration.ConfigurationManager.AppSettings["NewSitePricePartHtmlStorePath"];
            path = path.Replace("\\parts_detail", "");
            var filename = string.Concat(path, "suggest-" + cid + ".txt");
            if (File.Exists(filename))
            {
                File.Delete(filename);
            }
            File.WriteAllText(filename
                , string.Format(@"
                <div>
                    <h4>May We Also Suggest</h4>
                    <div class=""suggestList"">{0}</div>
                </div>", result)
                   , UTF8Encoding.UTF8);
        }

        string GetSuggest(tb_product prod)
        {
            return GetSuggest(prod.product_serial_no
                        , prod.other_product_sku.Value > 0 ? prod.other_product_sku.Value : prod.product_serial_no
                        , string.IsNullOrEmpty(prod.product_ebay_name) ? prod.product_name : prod.product_ebay_name
                        , "/computer/" + (FilterFileName(prod.manufacturer_part_number)) + "/" + prod.product_serial_no + ".html"
                        , prod.product_current_price.Value - prod.product_current_discount.Value);
        }

        string GetSuggest(int sku, int imgSku, string title, string tourl, decimal price)
        {
            return string.Format(@" 
                        <table width=""222"">
                            <tr>
                                <td>
                                    <a href='{3}'>
                                        <img width=""150"" src=""https://www.lucomputers.com/pro_img/components/{0}_list_1.jpg"" alt=""{1}"" style='border:0;' onerror=""this.src='https://www.lucomputers.com/pro_img/source_components/99999.jpg';"" />
                                    </a>  
                                </td>
                            </tr>
                            <tr>
                                <td style='height: 70px;overflow: hidden;width: 200px;word-break: break-all;display: -webkit-box;-webkit-line-clamp: 3;-webkit-box-orient: vertical;overflow: hidden;'>
                                    <a href='{3}'>
                                       {1}
                                    </a>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                     <a href='{3}'><label class=""price"">Price: ${2}</label></a>
                                </td>
                            </tr>
                        </table>
                    "
                , imgSku
                , title
                , price
                , tourl);
        }

        public void ForApp51ccoe(nicklu2Entities context, Logs log)
        {
            #region 右边最热门商品

            string hot10 = "";// GenerateHot10();
            string hot10Default = GenerateHot1051ccoe(context, new List<int>());
            #endregion

            var list = (from p in context.tb_product
                        from m in context.tb_product_category
                        from mc in context.tb_part_app_category
                        where p.menu_child_serial_no.HasValue && p.menu_child_serial_no.Value.Equals(m.menu_child_serial_no)
                         && mc.CateID.Equals(m.menu_child_serial_no)
                         && p.tag.HasValue && p.tag.Value.Equals(1)
                         && p.is_non.HasValue && p.is_non.Value.Equals(0)

                        orderby p.product_serial_no descending
                        select new
                        {
                            p.product_serial_no,
                            p.manufacturer_part_number,
                            m.menu_child_name,
                            producter_serial_no = p.producter_serial_no == "" ? "None" : p.producter_serial_no,
                            p.product_short_name,
                            imgsku = p.other_product_sku > 0 ? p.other_product_sku : p.product_serial_no,
                            price = p.product_current_price - p.product_current_discount,
                            m.menu_child_serial_no
                        }).ToList();

            string path_source = System.Configuration.ConfigurationManager.AppSettings["PriceAppGeneratePartHtmlSourcePath"];
            string path_to = System.Configuration.ConfigurationManager.AppSettings["PriceAppGeneratePartHtmlToPath"];

            string detail_templete_file = System.Configuration.ConfigurationManager.AppSettings["PriceAppGeneratePartHtmlPartDetail"]; // 明细界面模板文件

            string detail_templete_file_cont = "";//
            if (File.Exists(detail_templete_file))
            {
                detail_templete_file_cont = File.ReadAllText(detail_templete_file);
            }
            string PriceAppGeneratePartHtmlPartDetailToPath = System.Configuration.ConfigurationManager.AppSettings["PriceAppGeneratePartHtmlPartDetailToPath"]; // 明细界面存储路径

            foreach (var m in list)
            {
                string filename = path_source + m.product_serial_no + "_comment.html";
                string newfilename = path_to + m.product_serial_no + "_c.html";
                string comment_cont = "";

                SetStatus("Create file:" + filename);

                if (File.Exists(filename))
                {
                    string filecont = File.ReadAllText(filename);

                    int specIndex = filecont.IndexOf("<font color=\"#F25413\" size=\"2\">Specifications</font>");
                    if (specIndex == -1) continue;
                    filecont = filecont.Substring(specIndex);

                    string[] fileconts = filecont.Split(new string[] { @"<td><div>", @"</div>
    </td>
  </tr>
</table>" }
          , StringSplitOptions.None);
                    if (fileconts.Length > 1)
                    {
                        if (File.Exists(newfilename))
                            File.Delete(newfilename);
                        comment_cont = fileconts[1];
                        File.WriteAllText(newfilename, comment_cont);
                        //break;
                    }
                    else
                    {

                    }
                }
                else
                {

                    continue; //  正式环境使用，，没有描述，不生成文件。

                }


                // 生成明细界面
                if (detail_templete_file_cont.Length < 100)
                    continue;

                string detailNewFilePath = string.Format("{0}\\{1}"
                    , PriceAppGeneratePartHtmlPartDetailToPath
                    , BLL.ProdConvertTitle.ChangeName(m.producter_serial_no));
                if (!Directory.Exists(detailNewFilePath))
                    Directory.CreateDirectory(detailNewFilePath);

                string detailNewFilename = string.Format("{3}\\{0}-{1}{2}.html"
                    , BLL.ProdConvertTitle.ChangeName(m.menu_child_name)
                    , ""
                    , BLL.ProdConvertTitle.ChangeName(m.manufacturer_part_number)
                    , detailNewFilePath
                    );
                if (File.Exists(detailNewFilename))
                    File.Delete(detailNewFilename);
                // 右边广告;
                //var bannerToPath = System.Configuration.ConfigurationManager.AppSettings["PriceAppGeneratePartBannerSkuStorePath"].ToString();
                //string bannerFilename = string.Format(@"{0}{1}-{2}.html"
                //        , bannerToPath
                //        , m.menu_child_serial_no
                //        , (int)( m.menu_child_serial_no == 350 ? (m.price / 100) : (m.price/50)));
                //if (File.Exists(bannerFilename))
                //    hot10 = File.ReadAllText(bannerFilename);
                hot10 = GenerateAllHot51ccoe(context, m.menu_child_serial_no, m.price.Value, m.product_serial_no);
                if (hot10 == "")
                    hot10 = hot10Default;

                string newDetailCont = detail_templete_file_cont
                    .Replace("[part specification]", comment_cont)
                    .Replace("[part title]", m.product_short_name.Replace("\"", "&quot;") + (m.product_short_name.ToLower().IndexOf(m.manufacturer_part_number.ToLower()) == -1 ? " " + m.manufacturer_part_number : ""))
                    .Replace("[part logo]", "http://img.51ccoe.com/" + (m.imgsku.Value.ToString().Substring(0, 1)) + "/" + m.imgsku.Value + "_ebay_list_g_1.jpg")
                    .Replace("[right-banner-detail]", hot10)
                    .Replace("[meta keywords]", string.Format("{0} {1} {2}", m.producter_serial_no, m.menu_child_name, m.manufacturer_part_number))
                    .Replace("[part price list]", @"<li class=""list-group-item"">
                                    <span class=""badge"">$" + m.price + @"</span>
                                    <a href=""http://manager.lucomputers.com/site/product_parts_detail.asp?pro_class=" + m.menu_child_serial_no + "&id=" + m.product_serial_no + "&cid=" + m.menu_child_serial_no + @""" target=""_blank"">LU Computers</a>
                                  </li>");

                File.WriteAllText(detailNewFilename, newDetailCont, Encoding.UTF8);

                string urlPath = string.Format("{3}{0}-{1}{2}.html"
                    , BLL.ProdConvertTitle.ChangeName(m.menu_child_name)
                    , ""
                    , BLL.ProdConvertTitle.ChangeName(m.manufacturer_part_number)
                    , "parts/" + BLL.ProdConvertTitle.ChangeName(m.producter_serial_no) + "/"
                    );
                tb_part_app_page_url ap = context.tb_part_app_page_url.FirstOrDefault(p => p.SKU.Equals(m.product_serial_no));
                if (ap == null)
                {
                    //ap = tb_part_app_page_url.Createtb_part_app_page_url(0, m.product_serial_no, urlPath, DateTime.Now);
                    ap = new tb_part_app_page_url
                    {
                        SKU = m.product_serial_no,
                        PageUrl = urlPath,
                        regdate = DateTime.Now
                    };
                    context.tb_part_app_page_url.Add(ap);
                    //context.AddTotb_part_app_page_url(ap);
                    context.SaveChanges();
                }
                else
                {
                    ap.PageUrl = urlPath;
                    context.SaveChanges();
                }
            }

            IndexPage51ccoe(context);
        }

        /// <summary>
        /// 首页数据
        /// </summary>
        static void IndexPage51ccoe(nicklu2Entities context)
        {
            string templeteFile = System.Configuration.ConfigurationManager.AppSettings["PriceAppGeneratePartIndexPageTempleteFile"];
            if (!File.Exists(templeteFile))
            {
                return;
            }
            string templeteCont = File.ReadAllText(templeteFile);
            string toPath = System.Configuration.ConfigurationManager.AppSettings["PriceAppGeneratePartIndexPageStorePath"];
            if (!Directory.Exists(toPath))
                Directory.CreateDirectory(toPath);

            var list = (from p in context.tb_product
                        from pl in context.tb_part_app_page_url
                        where p.product_serial_no.Equals(pl.SKU)
                        orderby p.product_serial_no descending
                        select new
                        {
                            p.product_name_long_en,
                            p.product_short_name,
                            imgSku = p.other_product_sku > 0 ? p.other_product_sku : p.product_serial_no
                            ,
                            pl.PageUrl
                        }).ToList();

            int count = 0;
            System.Text.StringBuilder sb = new StringBuilder();
            if (list.Count == 0)
            {
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                sb.Append(string.Format(templeteCont
                    , "http://img.51ccoe.com/" + list[i].imgSku.ToString().Substring(0, 1) + "/" + list[i].imgSku + "_ebay_list_t_1.jpg"
                    , list[i].product_short_name
                    , list[i].product_name_long_en
                    , "http://www.51ccoe.com/" + list[i].PageUrl));

                if (i % 30 == 0)
                {
                    File.WriteAllText(toPath + "\\" + count.ToString() + ".html", sb.ToString());

                    #region 创建首页
                    if (count == 1) // 
                    {
                        string IndexTempleteFilename = System.Configuration.ConfigurationManager.AppSettings["PriceAppGeneratePartHtmlIndexPageTempleteFile"];
                        if (File.Exists(IndexTempleteFilename))
                        {
                            string cont = File.ReadAllText(IndexTempleteFilename);
                            FileInfo fi = new FileInfo(IndexTempleteFilename);

                            cont = cont.Replace("[page container]", sb.ToString());

                            File.WriteAllText(IndexTempleteFilename.Replace("templete\\index.html", "index.aspx"), cont, Encoding.UTF8);

                        }
                    }
                    #endregion

                    sb = new StringBuilder();

                    count += 1;
                }
            }
            File.WriteAllText(toPath + "\\" + count.ToString() + ".html", sb.ToString());
            sb = new StringBuilder();
        }

        /// <summary>
        /// 
        /// </summary>
        static string GenerateHot1051ccoe(nicklu2Entities context, List<int> Skus)
        {

            string templeteFilename = new FileInfo(System.Configuration.ConfigurationManager.AppSettings["PriceAppGeneratePartHtmlPartDetail"]).DirectoryName + "\\right_detail.html";

            string templeteString = File.ReadAllText(templeteFilename);
            DataTable dt = Config.ExecuteDataTable(hotSql(Skus));
            string hotString = "";
            foreach (DataRow dr in dt.Rows)
            {
                hotString += string.Format(templeteString
                    , dr["product_short_name"].ToString().Length > 30 ? dr["product_short_name"].ToString().Substring(0, 30) + "..." : dr["product_short_name"].ToString()
                    , dr["price"].ToString()
                    , "http://img.51ccoe.com/" + dr["imgSku"].ToString().Substring(0, 1) + "/" + dr["imgSku"].ToString() + "_ebay_list_t_1.jpg"
                    , dr["product_short_name"].ToString()
                    , "http://www.51ccoe.com/" + dr["PageUrl"].ToString()
                    );
            }
            return hotString;
        }

        /// <summary>
        /// 最热门前十商品
        /// </summary>
        /// <returns></returns>
        static string hotSql(List<int> Skus)
        {
            if (Skus.Count == 0)
                return @"select p.product_short_name, pa.PageUrl
, p.product_current_price-product_current_discount price
, case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end imgSku 
from tb_product p inner join (select distinct product_serial_no, count(product_serial_no) c from tb_order_product  
where length(product_serial_no)<6 and product_serial_no >0 and menu_child_serial_no in (22,31,29,456,25,41,240,33,258,28,26)
group by product_serial_no
order by serial_no desc limit 300
) t on p.product_serial_no = t.product_serial_no 
    inner join tb_part_app_page_url pa on pa.sku=p.product_serial_no order by c desc limit 10";
            else
            {
                return @"select p.product_short_name, pa.PageUrl
, p.product_current_price-product_current_discount price
, case when p.other_product_sku>0 then p.other_product_sku else p.product_serial_no end imgSku 
from tb_product p 
    inner join (select * from tb_part_app_page_url where sku in (" + (string.Join(",", Skus)) + ")) pa on pa.sku=p.product_serial_no";
            }
        }

        /// <summary>
        /// 生成左边广告所有的文件
        /// </summary>
        static string GenerateAllHot51ccoe(nicklu2Entities context, int Cateid, decimal price, int sku)
        {
            //var toPath = System.Configuration.ConfigurationManager.AppSettings["PriceAppGeneratePartBannerSkuStorePath"].ToString();

            decimal SplitPrice = 50M;
            if (Cateid == 350M) // notebook
            {
                SplitPrice = 100M;
            }

            decimal minv = price - SplitPrice;
            decimal maxv = price + SplitPrice;

            var sublist = context.tb_product.Where(p => p.menu_child_serial_no.HasValue
                && p.menu_child_serial_no.Value.Equals(Cateid)
                && p.tag.HasValue && p.tag.Value.Equals(1)
                && p.is_non.HasValue && p.is_non.Value.Equals(0)
                && p.product_current_price.HasValue
                && p.product_current_price.Value < maxv
                && p.product_current_price.Value > minv
                && p.product_serial_no != sku
                ).OrderByDescending(p => p.product_serial_no).Take(12).ToList();

            if (sublist.Count == 0)
                return "";

            if (sublist.Count < 6)
                sublist = context.tb_product.Where(p => p.menu_child_serial_no.HasValue
                && p.menu_child_serial_no.Value.Equals(Cateid)
                && p.tag.HasValue && p.tag.Value.Equals(1)
                && p.is_non.HasValue && p.is_non.Value.Equals(0)
                && p.product_current_price.HasValue
                ).OrderByDescending(p => p.product_serial_no).Take(12).ToList();

            //string storeFilename = string.Format(@"{0}{1}-{2}.html"
            //    , toPath
            //    , Cateid
            //    , sku);
            //if (File.Exists(storeFilename))
            //    File.Delete(storeFilename);
            string cont = GenerateHot1051ccoe(context, sublist.Select(p => p.product_serial_no).ToList());
            //StreamWriter sw = new StreamWriter(storeFilename, false, Encoding.UTF8);
            ////foreach (var subm in sublist)
            ////{
            ////    sw.WriteLine(subm.product_serial_no);
            ////}
            //sw.Write(cont);
            //sw.Close();
            //sw.Dispose();
            return cont;
        }
        /// <summary>
        /// 
        /// </summary>
        public void OldSite(nicklu2Entities context, Logs log)
        {
            var hh = new HttpHelper();

            var orderEbayList = context.tb_product.Where(e => e.tag == 1).ToList();
            foreach (var m in orderEbayList)
            {
                try
                {
                    SetStatus("Create file" + m.product_serial_no.ToString());
                    if (!string.IsNullOrEmpty(m.href_url)) continue;

                    if (!File.Exists(Config.generatePartHtmlFileStorePath + "Part_Comment\\" + m.product_serial_no.ToString() + "_comment.html"))
                        continue;

                    string s = hh.HttpGet(string.Format(Config.generatePartHtmlHost + "site/product_parts_detail.asp?pro_class={0}&id={1}&cid={0}"
                        , m.menu_child_serial_no
                        , m.product_serial_no));


                    if (Directory.Exists(Config.generatePartHtmlFileStorePath))
                    {

                        if (!string.IsNullOrEmpty(m.manufacturer_part_number)
                            && !string.IsNullOrEmpty(m.producter_serial_no))
                        {
                            string filename = string.Format("/{0}/{1}/{2}"
                                , "parts"
                                , FilterFileName(m.producter_serial_no)
                                , FilterFileName(m.manufacturer_part_number) + ".htm");
                            string subDir = Config.generatePartHtmlFileStorePath + "Parts";
                            if (!Directory.Exists(Config.generatePartHtmlFileStorePath + "Parts"))
                                Directory.CreateDirectory(Config.generatePartHtmlFileStorePath + "Parts");

                            if (!Directory.Exists(Config.generatePartHtmlFileStorePath + "Parts\\" + FilterFileName(m.producter_serial_no)))
                                Directory.CreateDirectory(Config.generatePartHtmlFileStorePath + "Parts\\" + FilterFileName(m.producter_serial_no));

                            StreamWriter sw = new StreamWriter(Config.generatePartHtmlFileStorePath + filename, false);
                            sw.Write(s);
                            sw.Close();

                            m.href_url = filename;
                        }
                    }

                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    log.WriteErrorLog(e);
                }
            }

            GenerateIndexHtml(context, orderEbayList.OrderBy(p => p.menu_child_serial_no).ToList());

        }

        /// <summary>
        /// 生成索引目录文件
        /// 
        /// </summary>
        void GenerateIndexHtml(nicklu2Entities context, List<tb_product> prodList)
        {
            string filename = Config.generatePartHtmlFileStorePath + "Parts\\index.html";
            System.Text.StringBuilder sb = new StringBuilder();
            sb.Append(@"<!DOCTYPE html>
<html lang=""en-us"">
<head>
    <meta charset=""utf-8"" />
    <meta http-equiv=""X-UA-Compatible"" content=""IE=edge"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"" />
    <meta name=""description"" content=""LU Computers"" />
    <meta name=""keywords"" content=""audio, video, battery, dvd-r, dvd+r, dvd, PC, GeForce, Radeon, parallel, scsi, motherboard, core, duo, Pentium, drives, storage, monitors, SIMM, DIMM, site development, processor upgrades, cables, adapters, surge protectors, UPC, PC Cards, mobile computing, disks, cartridges, Pentium, Athlon, SDRAM, DDR, DDR2, DDR3, AM2, LGA775, computers, hardware, software, notebook, laptop, networking, flash, LCD, monitor, cpu, pda, memory, ram, digital, camera, modems, printers, scanners, cd, palmtop, purchase, ethernet, hubs, routers, cdrom drives, Internet, server, buy, order, inkjet, laser, pci, USB, PCI-E, PCI Express, secure, microSD, HDMI, HDCP, Widescreen, projector, DVI, Firewall, VISA, Mastercard, Amex, Paypal, secure, Canada, 3Com, AMD, Acer, Antec, AOpen, Asus, ATI, Adobe, Abit, BFG, BenQ, Canon, CoolerMaster, Corsair, Cisco, Coolit, Creative, Labs, Crucial, D-Tek, Danger Den, DFI, Dlink, D-link, Diamond, Drobo, ECS, Enlight, Epson, EVGA, Galaxy, Gigabyte, Hewlett Packard, HIS, Hitachi, HP, IBM, Intel, Kingston, Koolance, Kef, LG, Lian-Li, Lian, Li, Logitech, Linksys, LiteOn, Lite, On, Lexmark, Micron, Microsoft, MSI, Mitsumi, NGear, NEC, nVidia, OCZ, Optoma, Onkyo, Palit, Panasonic, Philips, Pioneer, Plextor, Promise, Razer, RedHat, Samsung, Seagate, Shuttle, Sapphire, Silverstone, Sony, Swiftech, Symantec, Supermicro, Tekram, Toshiba, Thermaltake, Thermalright, Vantec, Viewsonic, Western Digital, WD, XFX, Zalman"" />
     <title>	LUComputers </title>
</head>
<body>
<h1> LU Computers </h1>
");
            List<tb_product_category> pcList = context.tb_product_category.Where(p => p.tag.HasValue && p.tag.Value.Equals(1)).ToList();
            foreach (var m in prodList)
            {
                if (m.menu_child_serial_no < 1)
                    continue;
                if (string.IsNullOrEmpty(m.producter_serial_no))
                    continue;
                string cateName = "";
                foreach (var pc in pcList)
                {
                    if (pc.menu_child_serial_no.Equals(m.menu_child_serial_no))
                    {
                        cateName = pc.menu_child_name;
                        break;
                    }
                }
                if (string.IsNullOrEmpty(cateName))
                {
                    continue;
                }
                //sb.AppendLine("<h5>" + cateName + " " + m.producter_serial_no + " " + m.manufacturer_part_number + "</h5>");
                sb.AppendLine(string.Format(@"<div>" + cateName + " " + m.producter_serial_no + " " + m.manufacturer_part_number + @" <a href='http://manager.lucomputers.com{1}' title='{0}' target='_blank'>{0}</a> <price>(${2})</price></div>"
                    , m.product_name
                    , (m.href_url ?? "").Length > 5 ? (m.href_url ?? "") : "/site/product_parts_detail.asp?pro_class=" + m.menu_child_serial_no + "&id=" + m.product_serial_no + "&cid=" + m.menu_child_serial_no
                    , m.product_current_price - m.product_current_discount
                    ));
            }

            sb.AppendLine("</body></html>");
            SetStatus("Create file" + filename);
            File.WriteAllText(filename, sb.ToString());
        }

        public static string FilterFileName(string str)
        {
            //var result = str.Replace("$", "-")
            //    .Replace("#", "-")
            //    .Replace("&", "-")
            //    .Replace("[", "-")
            //    .Replace("]", "-")
            //    .Replace("~", "-")
            //    .Replace("^", "-")
            //    .Replace("/", "-")
            //    .Replace("\\", "-")
            //    .Replace("*", "-")
            //    .Replace("?", "-")
            //    .Replace("+", "-")
            //    .Replace(" ", "")
            //    .Replace(".","");
            return BLL.ProdConvertTitle.ChangeName(str).Replace(" ", "")
                .Replace(".", "");
            //return result;
        }

    }
}
