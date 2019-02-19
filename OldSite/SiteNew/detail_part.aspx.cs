using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.IO;
using LU.BLL;
using LU.Model.Enums;

public partial class detail_part : PageBase
{
    public int ImgSku = 0;
    public string Manufacturer = string.Empty;
    public string ManufacturerCode = string.Empty;
    public int CateID = 0;
    public string AllQtyCateView = string.Empty;
    public string PartTitle = string.Empty;
    public string LogoGallery = string.Empty;
    public string LogoGallerySumHtml = string.Empty;
    public string SuggestString = string.Empty; // 推荐
    public string PartSpecificationString = string.Empty;
    public bool IsDiscount = false;
    public string PriceListString = string.Empty;
    public int ParentCid { get; set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var prod = db.tb_product.FirstOrDefault(p => p.product_serial_no.Equals(ReqSKU));
            if (prod != null)
            {
                metaKeyword.Text = string.Format(@" <meta name=""rebots"" content=""all"">");


                CateID = prod.menu_child_serial_no.Value;
                IsDiscount = prod.product_current_discount.Value > 0;
                #region categories

                var cateMenuInfo = new LU.BLL.CateMenu(db, CateID);
                ltCateNameParent.Text = cateMenuInfo.ParentTitle;
                ltCateName.Text = cateMenuInfo.Title;
                if (cateMenuInfo.SubCates == null)
                {
                    ltCates.Text = "";
                }
                else
                {
                    var cateIndex = 0;
                    foreach (var cate in cateMenuInfo.SubCates)
                    {
                        if (cate.CateType != LU.Model.Enums.CateType.System)
                        {
                            cateIndex++;
                            if (cateIndex % 2 == 1)
                            {
                                ltCates.Text += "<li role='pressntation'><a href='" + LU.BLL.Config.Host + "list_part.aspx?cid=" + cate.Id + "'><i class='iconfont'>" + cate.IconName + "</i> " + cate.Title + "</a>";
                            }
                            else
                            {
                                ltCates.Text += "<a href='" + LU.BLL.Config.Host + "list_part.aspx?cid=" + cate.Id + "'><i class='iconfont'>" + cate.IconName + "</i> " + cate.Title + "</a></li>";
                            }
                        }
                    }
                    if (cateIndex % 2 == 1)
                    {
                        ltCates.Text += "</li>";
                    }
                }
                #endregion

                PartTitle = string.IsNullOrEmpty(prod.product_ebay_name) ? prod.product_name : prod.product_ebay_name;
                ParentCid = cateMenuInfo.ParentCateId;
                this.Title = string.Concat(PartTitle, " - LUComputer SKU ", prod.product_serial_no.ToString());
                this.MetaKeywords = string.Concat(cateMenuInfo.ParentTitle, " ", cateMenuInfo.Title, " ", prod.manufacturer_part_number, " - LU Computer");
                var metaDesc = LU.BLL.eBay.ItemSpecifices.GetPartSpecifices(db, ReqSKU);
                this.MetaDescription = string.IsNullOrEmpty(metaDesc) ? PartTitle : metaDesc;

                ImgSku = !prod.other_product_sku.HasValue ? prod.product_serial_no : (prod.other_product_sku.Value > 0 ? prod.other_product_sku.Value : prod.product_serial_no);
                Manufacturer = prod.producter_serial_no;
                ManufacturerCode = prod.manufacturer_part_number;
                LogoGallery = GetLogoGallery(ImgSku, prod.product_img_sum.Value, Manufacturer, PartTitle);
                LogoGallerySumHtml = prod.product_img_sum > 1 ? "<div class='text-center'><a onclick=\"$('#someBigImage').ekkoLightbox();\" style='cursor:pointer;'>Gallery</a></div>" : "";
                SuggestString = GetSuggests(prod.menu_child_serial_no.Value);


                string filename = "C:\\Workspaces\\Web\\Part_Comment\\" + ReqSKU + "_comment.html";
                if (File.Exists(filename))
                {
                    string cont = File.ReadAllText(filename);

                    PartSpecificationString = LU.Toolkit.DescriptionFilter.Done(cont);
                }

                PriceListString = GetPriceArea(CateID);
            }
        }
    }


    string GetPriceArea(int cid)
    {

        var cateModel = db.tb_product_category.SingleOrDefault(p => p.menu_child_serial_no.Equals(cid));
        var prod = db.tb_product.SingleOrDefault(p => p.product_serial_no.Equals(ReqSKU));

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
                priceListString += string.Format(@"<li class=""list-group-item"">Price <span class=""badge price"">${0}&nbsp;<span class='price-unit'>{2}</span></span></li>
                    <li class=""list-group-item"">Special Cash Price <span class=""badge price"">${1}&nbsp;<span class='price-unit'>{2}</span></span></li>"
               , LU.BLL.FormatProvider.Price(ConvertPrice(prod.product_current_price.Value))
               , prod.product_current_price.HasValue ? LU.BLL.FormatProvider.Price(ConvertPrice(LU.BLL.PRateProvider.Multiply(prod.product_current_price.Value, 1 - setting.CardRate))) : "N/A"
               , this.cookiesHelper.CurrSiteCountry.ToString());
            }
            else
            {
                priceListString += string.Format(@"<li class=""list-group-item"">Price <span class=""badge price"">${0}&nbsp;<span class='price-unit'>{5}</span></span></li>
                    <li class=""list-group-item"">Instant Discount <small style='color:#E37446'>valid: {4}</small><span class=""badge price"" style='color:blue;'>Save ${1}&nbsp;<span class='price-unit'>{5}</span></span></li>
                    <li class=""list-group-item"">New Low Price <span class=""badge price"">${2}&nbsp;<span class='price-unit'>{5}</span></span></li>
                    <li class=""list-group-item"">Special Cash Price <span class=""badge price"">${3}&nbsp;<span class='price-unit'>{5}</span></span></li>"
               , LU.BLL.FormatProvider.Price(ConvertPrice(prod.product_current_price.Value))
               , LU.BLL.FormatProvider.Price(ConvertPrice(prod.product_current_discount.Value))
               , LU.BLL.FormatProvider.Price(ConvertPrice(prod.product_current_price.Value - prod.product_current_discount.Value))
               , prod.product_current_price.HasValue ? LU.BLL.FormatProvider.Price(ConvertPrice(LU.BLL.PRateProvider.Multiply((prod.product_current_price.Value - prod.product_current_discount.Value), 1 - setting.CardRate))) : "N/A"
               , onsale != null ? (onsale[0].b.Value.ToShortDateString() + " to " + onsale[0].e.Value.ToShortDateString()) : ""
               , this.cookiesHelper.CurrSiteCountry.ToString());
            }
        }
        else
        {
            priceListString += string.Format(@"<li class=""list-group-item"">Price <span class=""badge price"">${0}&nbsp;<span class='price-unit'>{2}</span></span></li>
                    <li class=""list-group-item"">Special Cash Price <span class=""badge price"" itemprop=""price"">${1}&nbsp;<span class='price-unit'>{2}</span></span></li>"
            , LU.BLL.FormatProvider.Price(ConvertPrice(prod.product_current_price.Value))
            , prod.product_current_price.HasValue ? LU.BLL.FormatProvider.Price(ConvertPrice(LU.BLL.PRateProvider.Multiply(prod.product_current_price.Value, 1 - setting.CardRate))) : "N/A"
            , this.cookiesHelper.CurrSiteCountry.ToString());
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
            , shippingFee == -1 ? "N/A" : LU.BLL.FormatProvider.Price(ConvertPrice(shippingFee))
            , this.cookiesHelper.CurrSiteCountry.ToString());

        priceListString += string.Format(@"<li class=""list-group-item"">Stock  Status: <span class=""badge "" itemprop=""availability"">{0}</span></li>"
           , ProdStock.GetProdStockString(prod));

        string ebayPriceUnit = "";
        string ebayItemId = "";
        decimal ebayPrice = eBayInfo.GetCurrEbayPrice(db, ProdType.part_product, prod.product_serial_no, ref ebayPriceUnit, ref ebayItemId);
        priceListString += string.Format(@"<li class=""list-group-item"">On eBay Price: <span class=""badge "">{0}</span></li>"
            , ebayPrice == 0M ? "N/A" : LU.BLL.Util.eBayFont() + " id: <a href=\"http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item=" + ebayItemId + "\" target=\"_blank\" style='margin-right:2em;'>" + ebayItemId + "</a><span style='color:blue;'>$" + LU.BLL.FormatProvider.Price(ebayPrice) + "</span> <small>" + ebayPriceUnit + "</small>");

        return priceListString;
    }

    bool ReqShowSpecificationHtml
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "specifType", 0) == 1; }
    }

    string GetSuggests(int cid)
    {
        var filename = Server.MapPath(string.Format("/Computer/suggest-{0}.txt", cid));
        if (File.Exists(filename))
        {
            var content = File.ReadAllText(filename);
            return content;
        }
        return string.Empty;
    }

    string GetLogoGallery(int imgsku, int qty, string mfp, string title)
    {
        var result = string.Empty;
        for (int i = 0; i < qty; i++)
        {
            var imgUrl = LU.BLL.QiNiuImgHelper.Get(imgsku, 420, 420, 1);
            var immUrl2 = LU.BLL.QiNiuImgHelper.Get(imgsku, 600, 600, i + 1);
            result += string.Format(@" <a href=""" + immUrl2 + @"""
                            data-toggle=""lightbox""
                            data-lightbox=""{1}""
                            data-title=""{2}""
                            data-gallery=""{1}""
                            data-footer=""{4}/{5}""
                            {3}
                            onclick=""$(this).ekkoLightbox();return false;"">
                            <img src=""" + imgUrl + @""" width=""100%"" itemprop=""logo""
                                border ='1' data-img=""{0}""/>
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