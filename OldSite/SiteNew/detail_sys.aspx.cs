using LU.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class detail_sys : PageBase
{
    public int ImgSku = 0;
    public int CateID = 0;
    public string AllQtyCateView = "";
    public string SysListString = "";
    public string LogoGallery = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var sysSku = ReqSKU;
            if (sysSku.ToString().Length == 8)
            {
                var comments = LU.BLL.CacheProvider.GetSysCommentList(db);
                sysSku = SysProd.ConvertSys8ToSys(db, sysSku, comments);
            }

            var prod = db.tb_ebay_system.FirstOrDefault(p => p.id.Equals(sysSku));

            if (prod != null)
            {
                var title = string.IsNullOrEmpty(prod.ebay_system_name) ? prod.system_title1 : prod.ebay_system_name;
                metaKeyword.Text = string.Format(@"<meta name=""rebots"" content=""all"">");
                CateID = SysProdCate.GetSysCate(sysSku, prod.is_barebone.Value, db);
                #region categories

                // var cateModel = LU.BLL.CacheProvider.GetAllCates(db).FirstOrDefault(p => p.Equals(CateID));
                var cateMenuInfo = new LU.BLL.CateMenu(db, CateID);
                ltCateNameParent.Text = cateMenuInfo.ParentTitle;
                ltCateName.Text = cateMenuInfo.Title;
                foreach (var cate in cateMenuInfo.SubCates)
                {
                    if (cate.CateType == LU.Model.Enums.CateType.System)
                        ltCates.Text += "<li role='pressntation'><a href='" + LU.BLL.Config.Host + "list_sys.aspx?cid=" + cate.Id + "'>" + cate.Title + "</a></li>";
                    //else
                    //    ltCates.Text += "<li role='pressntation'><a href='/list_part.aspx?cid=" + cate.Id + "'>" + cate.Title + "</a></li>";
                }
                #endregion

                this.Title = string.Concat(title, " - LUComputer SKU ", prod.id.ToString());
                this.MetaKeywords = string.Concat(cateMenuInfo.ParentTitle, " ", cateMenuInfo.Title, " ", title, " - LU Computer");
                var metaDesc = LU.BLL.eBay.ItemSpecifices.GetPartSpecifices(db, ReqSKU);
                this.MetaDescription = string.IsNullOrEmpty(metaDesc) ? title : metaDesc;


                ltTitle.Text = string.IsNullOrEmpty(prod.ebay_system_name) ? prod.system_title1 : prod.ebay_system_name;
                var systemModel = LU.BLL.ProductProvider.GetSystems(db, new int[] { sysSku }, cookiesHelper.CurrSiteCountry);
                if (systemModel.Count == 1)
                {
                    var sysPartList = systemModel[0].Parts;
                    //var sysPartList = (from ep in db.tb_ebay_system_parts                                  
                    //                   where ep.system_sku.HasValue
                    //                   && ep.system_sku.Value.Equals(sysSku)
                    //                   select new
                    //                   {
                    //                       CaseImgSku = es.is_case.Value ? (p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no) : 0,
                    //                       PartImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no,
                    //                       PartSKU = p.product_serial_no,
                    //                       PartDiscount = p.product_current_discount.Value,
                    //                       PartPrice = p.product_current_price.Value,
                    //                       CommName = es.comment,
                    //                       Title = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_name : p.product_ebay_name,
                    //                       PartMFP = p.manufacturer_part_number
                    //                   }).ToList();
                    SysListString += "<table class='table table-striped'>";
                    for (int i = 0; i < sysPartList.Count; i++)
                    {
                        if (sysPartList[i].Sku == setting.NoneSelectedID)
                            continue;
                        SysListString += string.Format(@"
<tr >
    <td >{0}</td>
    <td><strong>{1}</strong></td>
    <td><a href='{3}' itemprop=""{1}"">{2}</></td>
</tr>
"
                            , "<img src='https://lucomputers.com/pro_img/COMPONENTS/" + sysPartList[i].ImgSku + "_t.jpg' alt='...' width='30'>"
                            , sysPartList[i].CommentName
                            , sysPartList[i].Title
                            , sysPartList[i].WebHref);
                    }
                    SysListString += "</table>";

                    ImgSku = sysPartList.FirstOrDefault(p => p.CommentName.ToLower().Equals("case")) != null ? sysPartList.FirstOrDefault(p => p.CommentName.ToLower().Equals("case")).ImgSku : 999999;
                    string priceListString = "";
                    string priceUnitString = this.cookiesHelper.CurrSiteCountry == LU.Model.Enums.CountryType.CAD ? "<span class='price-unit'>CAD</span>" : "<span class='price-unit'>USD</span>";
                    if (sysPartList.Sum(p => p.Discount) > 0)
                    {
                        int sku = sysSku;
                        var now = DateTime.Now;

                        priceListString += string.Format(@"<li class=""list-group-item"">Price <span class=""badge price"">${0}{5}</span></li>
                    <li class=""list-group-item"">Instant Discount <small style='color:#E37446'>valid: {4}</small><span class=""badge price"" style='color:blue;'>Save ${1}{5}</span></li>
                    <li class=""list-group-item"">New Low Price <span class=""badge price"">${2}{5}</span></li>
                    <li class=""list-group-item"">Special Cash Price <span class=""badge price"">${3}{5}</span></li>"
                       , LU.BLL.FormatProvider.Price(ConvertPrice(sysPartList.Sum(p => p.Price)))
                       , LU.BLL.FormatProvider.Price(ConvertPrice(sysPartList.Sum(p => p.Discount)))
                       , LU.BLL.FormatProvider.Price(ConvertPrice(sysPartList.Sum(p => p.Price - p.Discount)))
                       , LU.BLL.FormatProvider.Price(ConvertPrice(LU.BLL.PRateProvider.Multiply(sysPartList.Sum(p => p.Price - p.Discount), 1 - setting.CardRate)))
                       , ""
                       , priceUnitString);
                    }
                    else
                    {
                        priceListString += string.Format(@"<li class=""list-group-item"">Price <span class=""badge price"">${0}{2}</span></li>
                    <li class=""list-group-item"">Special Cash Price <span class=""badge price"" itemprop=""price"">${1}{2}</span></li>"
                        , LU.BLL.FormatProvider.Price(ConvertPrice(sysPartList.Sum(p => p.Price)))
                        , LU.BLL.FormatProvider.Price(ConvertPrice(LU.BLL.PRateProvider.Multiply(sysPartList.Sum(p => p.Price), 1 - setting.CardRate)))
                        , priceUnitString);
                    }

                    #region shipping fee
                    decimal shippingFee = 0;
                    //try
                    //{
                    //    //shippingFee = new AccountOrder().AccountChargeOne(cateModel.is_noebook.Value == 1 ? ProdType.noebooks : ProdType.part_product
                    //    //    , prod.product_serial_no
                    //    //    , 1
                    //    //    , cateModel.is_noebook.Value == 1
                    //    //    , 8
                    //    //    , db);

                    //}
                    //catch (Exception ex)
                    //{
                    //    shippingFee = -1M;  //  -1M 显示 N/A
                    //}

                    #endregion

                    priceListString += string.Format(@"<li class=""list-group-item"">CANADA & USA SHIPPING FROM: <span class=""badge price""> {0}</span></li>"
                        , shippingFee < 1 ? "N/A" : string.Concat("$", LU.BLL.FormatProvider.Price(ConvertPrice(shippingFee))));

                    string ebayPriceUnit = "";
                    string ebayItemId = string.Empty;
                    decimal ebayPrice = eBayInfo.GetCurrEbayPrice(db, ProdType.system_product, sysSku, ref ebayPriceUnit, ref ebayItemId);
                    priceListString += string.Format(@"<li class=""list-group-item"">On eBay Price: <span class=""badge "">{0}</span></li>"
                        , ebayPrice == 0M ? "N/A" : LU.BLL.Util.eBayFont() + " Item id: <a href=\"http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item=" + ebayItemId + "\" target=\"_blank\" style='margin-right:2em;'>" + ebayItemId + "</a>$" + LU.BLL.FormatProvider.Price(ebayPrice) + " <small class='price'>" + ebayPriceUnit + "</small>");

                    ltPriceList.Text = priceListString;
                }
                else
                {
                    Response.Write("no find system info.");
                }


                #region 展示数量，当几个
                var allCate = (from es in db.tb_ebay_selling
                               join esp in db.tb_ebay_system on es.sys_sku.Value equals esp.id
                               where es.sys_sku.HasValue && es.sys_sku > 0
                               && esp.is_shrink.HasValue
                               && !esp.is_shrink.Value
                               orderby esp.id descending
                               select new
                               {
                                   SysSku = es.sys_sku.Value
                               }).ToList();
                var allqty = allCate.Count;

                for (int i = 0; i < allCate.Count; i++)
                {
                    if (allCate[i].SysSku == sysSku)
                    {
                        AllQtyCateView = string.Format(@"

        <a class=""btn btn-default {3}"" {2}><span class=""glyphicon glyphicon-chevron-left""></span></a>
        <a class='btn btn-default disabled'>  {0} / {1} </a>
        <a class=""btn btn-default {5}"" {4}><span class=""glyphicon glyphicon-chevron-right""></span></a> "
                            , i + 1
                            , allqty
                            , i > 0 ? " href='/computer/system/" + allCate[i - 1].SysSku + ".html'" : ""
                            , i < 1 ? " disabled" : ""
                             , i < allqty - 1 ? "href='/computer/system/" + allCate[i + 1].SysSku + ".html'" : ""
                            , i > allqty - 1 ? " disabled" : ""
                            );

                        break;
                    }
                }
                #endregion
                //if (ImgSku != 0)
                {
                    var imgModel = db.tb_product.Single(p => p.product_serial_no.Equals(ImgSku));
                    LogoGallery = GetLogoGallery(ImgSku, imgModel.product_img_sum.Value, ImgSku.ToString(), string.Empty);
                }
            }
        }
    }
    string GetLogoGallery(int imgsku, int qty, string mfp, string title)
    {
        var result = string.Empty;
        for (int i = 0; i < qty; i++)
        {
            result += string.Format(@" <a href=""" + LU.BLL.Config.ResHost + @"pro_img/COMPONENTS/{0}_g_{4}.jpg""
                            data-toggle=""lightbox""
                            data-lightbox=""{1}""
                            data-title=""{2}""
                            data-gallery=""{1}""
                            data-footer=""{4}/{5}""
                            {3}
                            onclick=""$(this).ekkoLightbox();return false;"">
                            <img src=""" + LU.BLL.Config.ResHost + @"pro_img/COMPONENTS/{0}_g_{4}.jpg"" width=""100%"" itemprop=""logo""
                                border='1' />
                        </a>"
                , imgsku
                , mfp
                , title
                , i == 0 ? "id=\"someBigImage\"" : " class=\"hide\""
                , i + 1
                , qty);

        }
        return result;
    }
}