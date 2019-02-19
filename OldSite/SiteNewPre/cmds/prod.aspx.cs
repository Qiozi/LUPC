using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class cmds_prod : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (ReqCmd)
            {
                case "list":

                    #region list
                    var searchPredicate = PredicateBuilder.True<nicklu2Model.tb_product>();
                    searchPredicate.And(p => true);
                    if (ReqKeys.IndexOf(",") == -1)
                    {
                        string key = ReqKeys.Trim();
                        if (!string.IsNullOrEmpty(key))
                            searchPredicate = searchPredicate.And(p => p.product_ebay_name.Contains(key) || p.keywords.Contains(key));

                    }
                    else
                    {
                        var ks = ReqKeys.Split(new char[] { ',' });
                        foreach (var k in ks)
                        {
                            string ke = k.Trim();
                            searchPredicate.And(p => p.product_ebay_name.Contains(ke)
                                || p.keywords.Contains(ke)
                                );
                        }
                    }

                    var prodList =
                        (from p in db.tb_product
                         where p.tag.HasValue
                         && p.tag.Value.Equals(1)
                         && p.menu_child_serial_no.HasValue
                         && p.menu_child_serial_no.Value.Equals(ReqCid)
                         && p.product_current_price.HasValue
                         && p.product_current_price.Value > 0
                         orderby p.product_serial_no descending
                         select p).Where(searchPredicate).Take(ReqRow * ReqPage).Skip(ReqRow * (ReqPage - 1)).ToList();

                    string result = "";//"<div class=\"row\">";
                    foreach (var item in prodList)
                    {
                        result += ("<div class=\"grid\" onclick=''><a href='/detail_part.aspx?sku=" + item.product_serial_no + "'>");
                        // result += ("<div>");
                        result += (string.Format(@"<div class=""imgholder"">
                            <img class=""lazy""
                                 src='https://lucomputers.com/pro_img/ebay_gallery/9/999999_ebay_list_t_1.jpg' 
                                 data-original='https://lucomputers.com/pro_img/ebay_gallery/{1}/{0}_ebay_list_t_1.jpg' 
                                width=""135"" alt=""..."">
                         </div>
                        "
                            , item.other_product_sku > 0 ? item.other_product_sku.Value : item.product_serial_no
                               , item.other_product_sku > 0 ? item.other_product_sku.Value.ToString().Substring(0, 1) : item.product_serial_no.ToString().Substring(0, 1)
                            ));
                        //result += ("<div class=\"caption\" style=\"\">");
                        result += ("<strong>" + item.producter_serial_no + "</strong>");
                        result += ("<p>" + item.product_name_long_en + "</p>");
                        result += ("<div class=\"meta\">");
                        if (item.product_current_discount > 0)
                            result += "Save: <small style='color:blue;'>$" + item.product_current_discount + "</small>";
                        result += ("<span class=\"price\">$" + PriceRate.Format(item.product_current_price.Value - item.product_current_discount.Value) + "</span>&nbsp;&nbsp;&nbsp;");
                        if (item.product_current_discount > 0)
                            result += "<br>";
                        result += ("<span class=\"text-right\" ><a href='/ShoppingCartTo.aspx?sku=" + item.product_serial_no + "'><span class='glyphicon glyphicon-shopping-cart'></span> Buy</a></span>");
                        result += ("</div></a>");
                        //result += ("</div>");
                        // result += ("</div>");
                        result += ("</div>");
                    }
                    //result += ("</div>");
                    Response.Write(result);
                    #endregion

                    break;

                case "getPartCateAllQty":
                    #region 展示数量，当前几个
                    var AllQtyCateView = "";
                    var allCate = db.tb_product.Where(p => p.tag.HasValue
                        && p.tag.Value.Equals(1)
                        && p.menu_child_serial_no.HasValue
                        && p.menu_child_serial_no.Value.Equals(ReqCid)
                        && ((p.product_store_sum.HasValue && p.product_store_sum.Value > 0) || (p.ltd_stock.HasValue && p.ltd_stock.Value > 0)))
                        .OrderByDescending(p => p.product_serial_no)
                        .Select(p => p.product_serial_no).ToList();
                    var allqty = allCate.Count;

                    for (int i = 0; i < allCate.Count; i++)
                    {
                        if (allCate[i] == ReqSKU)
                        {
                            AllQtyCateView = string.Format(@"

        <a class=""btn btn-default {3}"" {2}><span class=""glyphicon glyphicon-chevron-left""></span></a>
        <a class='btn btn-default disabled'>  {0} / {1} </a>
        <a class=""btn btn-default {5}"" {4}><span class=""glyphicon glyphicon-chevron-right""></span></a> "
                                , i + 1
                                , allqty
                                , i > 0 ? " href='/detail_part.aspx?sku=" + allCate[i - 1] + "'" : ""
                                , i < 1 ? " disabled" : ""
                                 , i < allqty - 1 ? "href='/detail_part.aspx?sku=" + allCate[i + 1] + "'" : ""
                                , i > allqty - 1 ? " disabled" : ""
                                );


                        }
                    }
                    Response.Write(AllQtyCateView);
                    #endregion
                    break;

                case "getPartPriceArea":
                    #region 商品明细界面，价格

                    var cateModel = db.tb_product_category.SingleOrDefault(p => p.menu_child_serial_no.Equals(ReqCid));
                    var prod = db.tb_product.SingleOrDefault(p => p.product_serial_no.Equals(ReqSku));

                    string priceListString = "";

                    if (prod.product_current_discount.HasValue && prod.product_current_discount.Value > 0)
                    {
                        int sku = prod.product_serial_no;
                        var now = DateTime.Now;
                        var onsale = (from c in db.tb_on_sale
                                      where
                                      c.product_serial_no.HasValue
                                      && c.product_serial_no.Value.Equals(sku)
                                      && now <= c.end_datetime.Value
                                      && now >= c.begin_datetime.Value
                                      select new
                                      {
                                          b = c.begin_datetime,
                                          e = c.end_datetime
                                      }).Take(1).ToList();

                        if (onsale.Count == 0)
                        {
                            priceListString += string.Format(@"<li class=""list-group-item"">Price <span class=""badge price"">${0}<span class='price-unit'>{2}</span></span></li>
                    <li class=""list-group-item"">Special Cash Price <span class=""badge price"">${1}<span class='price-unit'>{2}</span></span></li>"
                       , PriceRate.Format(ConvertPrice(prod.product_current_price.Value))
                       , prod.product_current_price.HasValue ? PriceRate.Format(ConvertPrice(PriceRate.Multiply(prod.product_current_price.Value, 1 - setting.CardRate))) : "N/A"
                       , CurrSiteCountry.ToString());
                        }
                        else
                        {
                            priceListString += string.Format(@"<li class=""list-group-item"">Price <span class=""badge price"">${0} <span class='price-unit'>{5}</span></span></li>
                    <li class=""list-group-item"">Instant Discount <small style='color:#E37446'>valid: {4}</small><span class=""badge price"" style='color:blue;'>Save ${1} <span class='price-unit'>{5}</span></span></li>
                    <li class=""list-group-item"">New Low Price <span class=""badge price"">${2} <span class='price-unit'>{5}</span></span></li>
                    <li class=""list-group-item"">Special Cash Price <span class=""badge price"">${3} <span class='price-unit'>{5}</span></span></li>"
                           , PriceRate.Format(ConvertPrice(prod.product_current_price.Value))
                           , PriceRate.Format(ConvertPrice(prod.product_current_discount.Value))
                           , PriceRate.Format(ConvertPrice(prod.product_current_price.Value - prod.product_current_discount.Value))
                           , prod.product_current_price.HasValue ? PriceRate.Format(ConvertPrice(PriceRate.Multiply((prod.product_current_price.Value - prod.product_current_discount.Value), 1 - setting.CardRate))) : "N/A"
                           , onsale != null ? (onsale[0].b.Value.ToShortDateString() + " to " + onsale[0].e.Value.ToShortDateString()) : ""
                           , CurrSiteCountry.ToString());
                        }
                    }
                    else
                    {
                        priceListString += string.Format(@"<li class=""list-group-item"">Price <span class=""badge price"">${0}<span class='price-unit'>{2}</span></span></li>
                    <li class=""list-group-item"">Special Cash Price <span class=""badge price"">${1}<span class='price-unit'>{2}</span></span></li>"
                        , PriceRate.Format(ConvertPrice(prod.product_current_price.Value))
                        , prod.product_current_price.HasValue ? PriceRate.Format(ConvertPrice(PriceRate.Multiply(prod.product_current_price.Value, 1 - setting.CardRate))) : "N/A"
                        , CurrSiteCountry.ToString());
                    }

                    #region shipping fee
                    decimal shippingFee = 0;
                    try
                    {
                        shippingFee = new AccountOrder().AccountChargeOne(cateModel.is_noebook.Value == 1 ? ProdType.noebooks : ProdType.part_product
                            , prod.product_serial_no
                            , 1
                            , cateModel.is_noebook.Value == 1
                            , 8
                            , db);
                    }
                    catch
                    {
                        shippingFee = -1M;  //  -1M 显示 N/A
                    }

                    #endregion

                    priceListString += string.Format(@"<li class=""list-group-item"">CANADA & USA SHIPPING FROM: <span class=""badge price"">${0} <span class='price-unit'>{1}</span></span></li>"
                        , shippingFee == -1 ? "N/A" : PriceRate.Format(ConvertPrice(shippingFee))
                        , CurrSiteCountry.ToString());

                    priceListString += string.Format(@"<li class=""list-group-item"">Stock  Status: <span class=""badge "">{0}</span></li>"
                       , ProdStock.GetProdStockString(prod));

                    string ebayPriceUnit = "";
                    string ebayItemId = "";
                    decimal ebayPrice = eBayInfo.GetCurrEbayPrice(db, ProdType.part_product, prod.product_serial_no, ref ebayPriceUnit, ref ebayItemId);
                    priceListString += string.Format(@"<li class=""list-group-item"">On eBay Price: <span class=""badge "">{0}</span></li>"
                        , ebayPrice == 0M ? "N/A" : "eBay Item id: <a href=\"http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item=" + ebayItemId + "\" target=\"_blank\" style='margin-right:2em;'>" + ebayItemId + "</a><span style='color:blue;'>$" + PriceRate.Format(ebayPrice) + "</span> <small>" + ebayPriceUnit + "</small>");

                    Response.Write(priceListString);

                    #endregion
                    break;
                case "getSinglePrice":
                    bool writed = false;
                    string priceFile = Server.MapPath("~/Computer/part_price/" + ReqSku + "_" + CurrSiteCountry.ToString() + ".txt");

                    if (File.Exists(priceFile))
                    {
                        FileInfo fi = new FileInfo(priceFile);
                        if (fi.LastWriteTime.Date == DateTime.Now.Date)
                        {
                            Response.Write(File.ReadAllText(priceFile));
                            writed = true;
                        }
                    }

                    if (!writed)
                    {
                        var prodList2 = (from p in db.tb_product
                                         where p.product_serial_no.Equals(ReqSku)
                                         select new
                                         {
                                             Price = p.product_current_price.Value,
                                             Save = p.product_current_discount.Value
                                         }).ToList();
                        if (prodList2 != null && prodList2.Count == 1)
                        {
                            string priceStr = "{price:'" + ConvertPrice(prodList2[0].Price - prodList2[0].Save) + "',save:'" + ConvertPrice(prodList2[0].Save) + "',unit:'" + (CurrSiteCountry == CountryType.CAD ? "CAD" : "USD") + "'}";

                            Response.Write(priceStr);
                            File.WriteAllText(priceFile, priceStr);
                        }
                    }
                    break;
            }
        }
        Response.End();
    }

    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    int ReqSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", 0); }
    }

    int ReqCid
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "cid", 0); }
    }

    int ReqPage
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "page", 1); }
    }

    int ReqRow
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "row", 20); }
    }

    string ReqKeys
    {
        get { return Util.GetStringSafeFromQueryString(Page, "key").Replace("[", "").Replace("]", ""); }
    }
}