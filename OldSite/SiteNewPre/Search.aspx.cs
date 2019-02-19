using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;

public partial class Search : PageBase
{

    public int SearchQty = 0;
    public string SearchNote = "";
    public string ResultString = "";
    public string CateTypeName = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsLocalHostFrom)
            {
                Response.Write("NO");
                Response.End();
            }
            if (string.IsNullOrEmpty(ReqKey))
            {
                ResultString = ("<p class='well '>please input keyword.</p>");
            }
            else if (ReqKey.Trim().Length < 4)
            {
                ResultString = ("<p class='well'>Keywords length can not be less than 4</p>");
            }
            else
            {
                switch (ReqCate)
                {
                    case (int)ProdType.noebooks:
                        #region notebook

                        CateTypeName = "Mobile Computer ";

                        SearchQty = (from p in db.tb_product
                                     where p.tag.HasValue && p.tag.Value.Equals(1)
                                     && (p.product_ebay_name.Contains(ReqKey)
                                     || p.product_name.Contains(ReqKey)
                                     || p.manufacturer_part_number.Equals(ReqKey)
                                     || p.product_serial_no.Equals(ReqKeyInt)
                                     || p.short_name_for_sys.Contains(ReqKey)
                                     || p.UPC.Equals(ReqKey)
                                     || p.product_short_name.Contains(ReqKey)
                                     )
                                     && p.menu_child_serial_no.Value.Equals(350)
                                     select new
                                     {
                                         p.product_serial_no
                                     }).Count();
                        var noteList = (from p in db.tb_product
                                        where p.tag.HasValue && p.tag.Value.Equals(1)
                                        && (p.product_ebay_name.Contains(ReqKey)
                                        || p.product_name.Contains(ReqKey)
                                        || p.manufacturer_part_number.Equals(ReqKey)
                                        || p.product_serial_no.Equals(ReqKeyInt)
                                        || p.short_name_for_sys.Contains(ReqKey)
                                        || p.UPC.Equals(ReqKey)
                                        || p.product_short_name.Contains(ReqKey)
                                        )
                                        && p.menu_child_serial_no.Value.Equals(350)
                                        orderby p.product_serial_no descending
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
                                            Keyword = p.keywords.ToLower()
                                        }).Take(30).ToList();
                        if (noteList.Count == 1)
                        {
                            Response.Redirect("/detail_part.aspx?sku=" + noteList[0].SKU, true);
                        }
                        else
                        {
                            List<ProdViewModel> SearchResultProdList = new List<ProdViewModel>();
                            foreach (var m in noteList)
                            {
                                ProdViewModel pvm = new ProdViewModel();
                                pvm.Discount = m.Discount;
                                pvm.ImgSku = m.ImgSku;
                                pvm.Name = m.Name;
                                pvm.Price = m.Price;
                                pvm.PriceUnit = CurrSiteCountry.ToString();
                                pvm.ShortName = m.ShortName;
                                pvm.SKU = m.SKU;
                                pvm.Sold = m.Sold;
                                SearchResultProdList.Add(pvm);
                            }
                            ResultString = GenerateProdListString(SearchResultProdList);
                        }

                        #endregion
                        break;
                    case (int)ProdType.part_product:
                        #region 零件
                        CateTypeName = "Parts & Peripherals ";
                        SearchQty = (from p in db.tb_product
                                     where p.tag.HasValue && p.tag.Value.Equals(1)
                                     && (p.product_ebay_name.Contains(ReqKey)
                                     || p.product_name.Contains(ReqKey)
                                     || p.manufacturer_part_number.Equals(ReqKey)
                                     || p.product_serial_no.Equals(ReqKeyInt)
                                     || p.short_name_for_sys.Contains(ReqKey)
                                     || p.UPC.Equals(ReqKey)
                                     || p.product_short_name.Contains(ReqKey)
                                     )
                                     && !p.menu_child_serial_no.Value.Equals(350)
                                     select new
                                     {
                                         SKU = p.product_serial_no

                                     }).Count();
                        var partList = (from p in db.tb_product
                                        where p.tag.HasValue && p.tag.Value.Equals(1)
                                        && (p.product_ebay_name.Contains(ReqKey)
                                        || p.product_name.Contains(ReqKey)
                                        || p.manufacturer_part_number.Equals(ReqKey)
                                        || p.product_serial_no.Equals(ReqKeyInt)
                                        || p.short_name_for_sys.Contains(ReqKey)
                                        || p.UPC.Equals(ReqKey)
                                        || p.product_short_name.Contains(ReqKey)
                                        )
                                        && !p.menu_child_serial_no.Value.Equals(350)
                                        orderby p.product_serial_no descending
                                        select new
                                        {
                                            SKU = p.product_serial_no,
                                            ShortName = p.product_short_name,
                                            Name = !string.IsNullOrEmpty(p.product_ebay_name) ? p.product_ebay_name : p.product_name_long_en,
                                            Price = p.product_current_price.Value,
                                            Discount = p.product_current_discount.HasValue ? p.product_current_discount.Value : 0,
                                            Sold = p.product_current_price.Value - p.product_current_discount.Value,
                                            ImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no,
                                            IsOnsale = p.product_current_discount > 0 ? 1 : 0,
                                            IsRebate = 0,
                                            Keyword = p.keywords.ToLower()
                                        }).Take(30).ToList();

                        if (partList.Count == 1)
                        {
                            Response.Redirect("/detail_part.aspx?sku=" + partList[0].SKU, true);
                        }
                        else
                        {
                            List<ProdViewModel> SearchResultProdList = new List<ProdViewModel>();
                            foreach (var m in partList)
                            {
                                ProdViewModel pvm = new ProdViewModel();
                                pvm.Discount = m.Discount;
                                pvm.ImgSku = m.ImgSku;
                                pvm.Name = m.Name;
                                pvm.Price = m.Price;
                                pvm.PriceUnit = CurrSiteCountry.ToString();
                                pvm.ShortName = m.ShortName;
                                pvm.SKU = m.SKU;
                                pvm.Sold = m.Sold;
                                SearchResultProdList.Add(pvm);
                            }
                            ResultString = GenerateProdListString(SearchResultProdList);
                        }
                        #endregion
                        break;
                    case (int)ProdType.system_product:
                        #region system
                        CateTypeName = "System ";
                        ResultString = "";
                        var customSys = db.tb_sp_tmp.SingleOrDefault(s => s.sys_tmp_code.Equals(ReqKey));
                        if (customSys == null)
                        {
                            var comments = (from c in db.tb_ebay_system_part_comment
                                            select new
                                            {
                                                c.id,
                                                c.comment
                                            }).ToList();

                            var query = (from s in db.tb_ebay_system
                                         where (s.id.Equals(ReqKeyInt) ||
                                                s.system_title1.Contains(ReqKey)) &&
                                                s.showit.HasValue && s.showit.Value.Equals(true) &&
                                                s.is_online.HasValue && s.is_online.Value.Equals(true) &&
                                                s.is_barebone.HasValue && s.is_barebone.Value.Equals(false) &&
                                                s.is_shrink.HasValue && s.is_shrink.Value.Equals(false)
                                         orderby s.id descending
                                         select new
                                         {
                                             sysSku = s.id,
                                             Title = s.system_title1
                                         }).Take(30).ToList();
                            if (query != null)
                            {
                                SearchQty = query.Count;
                                foreach (var sys in query)
                                {
                                    var spTmpDetailQuery = (from c in db.tb_ebay_system_parts
                                                            join p in db.tb_product on c.luc_sku.Value equals p.product_serial_no
                                                            where c.system_sku.HasValue &&
                                                            c.system_sku.Value.Equals(sys.sysSku) &&
                                                            p.product_serial_no != 16684
                                                            orderby c.id ascending
                                                            select new
                                                            {
                                                                SKU = p.product_serial_no,
                                                                ShortName = p.product_short_name,
                                                                PartImgSku = p.other_product_sku.HasValue && p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no,
                                                                CommentId = c.comment_id,
                                                                PartTitle = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_short_name : p.product_ebay_name,
                                                                Price = p.product_current_price.Value,
                                                                Discount = p.product_current_discount.Value,
                                                                NameForSys = p.short_name_for_sys
                                                            }).ToList();

                                    var sysList = new List<SysProdModel>();
                                    var parts = new List<SysProdPart>();
                                    var sysTitle = sys.Title;
                                    foreach (var item in spTmpDetailQuery)
                                    {
                                        var comment = comments.Single(p => p.id.Equals(item.CommentId));
                                        parts.Add(new SysProdPart
                                        {
                                            Title = item.PartTitle,
                                            ImgSku = item.PartImgSku,
                                            Comment = comment.comment,
                                            Sku = item.SKU
                                        });
                                    }
                                    sysList.Add(new SysProdModel()
                                    {
                                        Sku = sys.sysSku,
                                        Title = sysTitle,
                                        Price = spTmpDetailQuery.Sum(p => p.Price),
                                        Discount = spTmpDetailQuery.Sum(p => p.Discount),
                                        Parts = parts
                                    });

                                    ResultString += GenerateSysProdListString(sysList, CurrSiteCountry);
                                }
                            }
                            //||
                            //    s.system_title1.Contains(ReqKey)).ToList();
                        }
                        else
                        {
                            SearchQty = 1;
                            var spTmpDetailQuery = (from c in db.tb_sp_tmp_detail
                                                    join p in db.tb_product on c.product_serial_no.Value equals p.product_serial_no
                                                    where c.sys_tmp_code.Equals(ReqKey) &&
                                                    p.product_serial_no != 16684
                                                    select new
                                                    {
                                                        SKU = p.product_serial_no,
                                                        ShortName = p.product_short_name,
                                                        PartImgSku = p.other_product_sku.HasValue && p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no,
                                                        Comment = c.cate_name,
                                                        PartTitle = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_short_name : p.product_ebay_name,
                                                        Price = p.product_current_price.Value,
                                                        Discount = p.product_current_discount.Value,
                                                        NameForSys = p.short_name_for_sys
                                                    }).ToList();

                            var sysList = new List<SysProdModel>();
                            var parts = new List<SysProdPart>();
                            var sysTitle = string.Empty;
                            foreach (var item in spTmpDetailQuery)
                            {
                                parts.Add(new SysProdPart
                                {
                                    Title = item.PartTitle,
                                    ImgSku = item.PartImgSku,
                                    Comment = item.Comment,
                                    Sku = item.SKU
                                });
                                SysTitle(item.Comment, item.NameForSys, ReqKeyInt, ref sysTitle);
                            }
                            sysList.Add(new SysProdModel()
                            {
                                Sku = int.Parse(ReqKey),
                                Title = sysTitle,
                                Price = spTmpDetailQuery.Sum(p => p.Price),
                                Discount = spTmpDetailQuery.Sum(p => p.Discount),
                                Parts = parts
                            });

                            ResultString = GenerateSysProdListString(sysList, CurrSiteCountry);
                        }
                        #endregion
                        break;
                    default:
                        Response.Write(ReqKey);
                        break;
                }
            }
            if (SearchQty > 30)
            {
                SetNote();
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="commentName"></param>
    /// <param name="nameForSys"></param>
    /// <param name="sku"></param>
    /// <param name="sysTitle"></param>
    /// <returns></returns>
    void SysTitle(string commentName, string nameForSys, int sku, ref string sysTitle)
    {
        if (commentName.Trim().ToLower() == "cpu")
        {
            if (string.IsNullOrEmpty(nameForSys))
            {
                sysTitle = string.Concat("System#: ", sku);
            }
            else
            {
                sysTitle = string.Concat(nameForSys, " System");
            }
        }
    }

    /// <summary>
    /// 系统产品列表
    /// </summary>
    /// <param name="list"></param>
    /// <param name="countryType"></param>
    /// <returns></returns>
    string GenerateSysProdListString(List<SysProdModel> list, CountryType countryType)
    {
        foreach (var item in list)
        {
            var partListString = "<table class='table table-condensed table-striped'>";

            var logoString = "";
            var sysPriceString = "";
            var sysTitle = item.Title;
            if (item.Discount > 0M)
            {
                sysPriceString += "     <span class='price'><del>" + PriceRate.Format(ConvertPrice(item.Price)) + "</del></span>";
            }
            sysPriceString += "     <span class='priceBig'>$" + PriceRate.Format(ConvertPrice(item.Price - item.Discount)) + "</span><span class='price-unit'>" + (countryType == CountryType.CAD ? "CAD" : "USD") + "</span>";

            foreach (var part in item.Parts)
            {
                logoString += "<img class=\"lazy\" src='../images/logo1.png' data-original=\"" + setting.ImgHost + "pro_img/ebay_gallery/" + part.ImgSku.ToString().Substring(0, 1) + "/" + part.ImgSku + "_ebay_list_t_1.jpg\" width=\"75\" alt=\"\">";
                partListString += "<tr><td><a class='' style='color:#000;'>" + part.Comment + "</a></td>";
                partListString += "<td><a class='' style='color:#666;'>" + part.Title + "</a></td></tr>";
            }
            partListString += "</table>";
            partListString += "<div class='row'>";

            partListString += "<div class='col-md-6' id='sys-price-area-" + item.Sku.ToString() + "'>";
            partListString += sysPriceString;
            partListString += "</div>";
            partListString += " <div class='col-md-6 txtRight'>";
            partListString += "     <a class='btn btn-default' href='ShoppingCartTo.aspx?sku=" + item.Sku.ToString() + "'><span class='glyphicon glyphicon-shopping-cart'> Buy It Now</a>";
            partListString += "     <a class='btn btn-default' href='detail_sys_customize.aspx?sku=" + item.Sku.ToString() + "'><span class='glyphicon glyphicon-wrench'></span> Customize It</a>";
            partListString += "     <a class='btn btn-default' href='detail_sys.aspx?sku=" + ReqKey + "'><span class='glyphicon glyphicon-calendar'></span> Detail</a>";
            partListString += " </div>";
            partListString += "<div class='row'><div class='col-md-12'>&nbsp;</div></div>";
            partListString += "</div>";
            //result += "<li class='list-group-item row text-center active' style='color:white;'> ---- end ----</li>";
            partListString += ("</ul>");

            var resultString = "<h4 class='sysTitle'>[SKU.<span class='skuColor' >" + item.Sku + "</span>] " + sysTitle + "</h4>";
            resultString += "<div class=\"row\">";
            resultString += "   <div class=\"col-md-3 sysLogoList\" sku='" + item.Sku + "'>";
            resultString += logoString;
            resultString += "   </div>";
            resultString += "   <div class=\"col-md-9 sysPartList\" price='' discount='' sold=''>";
            resultString += partListString;
            resultString += "   </div>";
            resultString += "</div>";
            return resultString;
        }
        return string.Empty;
    }

    /// <summary>
    /// 商品列表字符串
    /// </summary>
    /// <param name="list"></param>
    /// <returns></returns>
    string GenerateProdListString(List<ProdViewModel> list)
    {
        var result = ("<ul class=\"list-group\">");

        foreach (var item in list)
        {

            result += "<li class='list-group-item row'><a href='detail_part.aspx?sku=" + item.SKU + "'>";
            result += "<div class='col-xs-5 col-sm-2 col-md-2'><img class=\"lazy\" src='../images/logo1.png' data-original=\"https://lucomputers.com/pro_img/COMPONENTS/" + item.ImgSku + "_list_1.jpg\" width=\"120\" alt=\"...\"></div>";
            result += "<div class='col-xs-5 col-sm-7 col-md-7'>";
            result += "<h4 class='list-group-item-heading'>" + item.ShortName + "</h4>";
            result += "<p class='list-group-item-text'>" + item.Name + "</p>";
            result += "</div>";
            result += "<div class='col-xs-2 col-sm-3 col-md-3'>";
            result += "<p class='list-group-item-text'> ";
            result += "<ul class='price_list'><li>SKU: " + item.SKU + "</li>";
            if (item.Discount == 0)
                result += "<li>Special: <span class='price'>$" + item.Sold + "</span><span class='price-unit'> " + CurrSiteCountry.ToString() + "</span></li>";
            else
            {

                result += "<li>Save: <span class='price' style='color:blue;'>$" + item.Discount + "</span> <span class='price-unit'> " + CurrSiteCountry.ToString() + "</span></li>";
                result += "<li>Special: <span class='price'>$" + item.Sold + "</span><span class='price-unit'> " + CurrSiteCountry.ToString() + "</span></li>";
            }
            result += "<li>&nbsp;</li>";
            result += "<li><a href='/ShoppingCartTo.aspx?sku=" + item.SKU + "' class='btn btn-default'><span class='glyphicon glyphicon-shopping-cart'></span> Add to Shopping Cart</a></li>";
            result += "</ul>";
            result += "</p>";
            result += "</div>";
            result += ("</a></li>");
        }
        result += "</ul>";
        return result;
    }

    void SetNote()
    {
        SearchNote = "<div class=\"alert alert-warning\" role=\"alert\">Search record is limited 30. </div>";
    }

    public string ReqKey
    {
        get { return Util.GetStringSafeFromQueryString(Page, "key").Trim(); }
    }

    int ReqKeyInt
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "key", 0); }
    }

    int ReqCate
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "cate", 0); }
    }
}

