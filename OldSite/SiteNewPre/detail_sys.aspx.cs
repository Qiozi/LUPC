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
                var comments = db.tb_ebay_system_part_comment.ToList();
                sysSku = SysProd.ConvertSys8ToSys(db, sysSku, comments);
            }

            var prod = db.tb_ebay_system.FirstOrDefault(p => p.id.Equals(sysSku));


            if (prod != null)
            {
                var CateID = SysProdCate.GetSysCate(sysSku, prod.is_barebone.Value, db);

                #region categories

                var cateModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(CateID));
                if (cateModel != null)
                {
                    var preId = cateModel.menu_pre_serial_no.Value;
                    var subCateModel = db.tb_product_category.FirstOrDefault(p => p.menu_child_serial_no.Equals(preId));
                    if (subCateModel != null)
                    {
                        ltCateNameParent.Text = subCateModel.menu_child_name + "";// +cateModel.menu_child_name;
                        ltCateName.Text = cateModel.menu_child_name;

                        #region cate list
                        var parentCateList = db.tb_product_category.Where(p => p.menu_pre_serial_no.HasValue
                            && p.menu_pre_serial_no.Value.Equals(0)
                            && p.tag.HasValue
                            && p.tag.Value.Equals(1)
                            ).OrderBy(p => p.menu_child_order).ToList();
                        for (int i = 0; i < parentCateList.Count; i++)
                        {
                            //ltCatesParent.Text += "<li>" + parentCateList[i].menu_child_name + "</li>";
                        }

                        var cateList = db.tb_product_category.Where(p => p.menu_pre_serial_no.HasValue
                            && p.menu_pre_serial_no.Value.Equals(preId)
                            && p.tag.HasValue
                            && p.tag.Value.Equals(1)
                            ).OrderBy(p => p.menu_child_order).ToList();
                        for (int i = 0; i < cateList.Count; i++)
                        {
                            if (cateList[i].page_category == 0)
                                ltCates.Text += "<li role='pressntation'><a href='list_sys.aspx?cid=" + cateList[i].menu_child_serial_no + "'>" + cateList[i].menu_child_name + "</a></li>";

                            else
                                ltCates.Text += "<li role='pressntation'><a href='list_part.aspx?cid=" + cateList[i].menu_child_serial_no + "'>" + cateList[i].menu_child_name + "</a></li>";
                        }
                        #endregion
                    }

                }
                #endregion

                ltTitle.Text = string.IsNullOrEmpty(prod.ebay_system_name) ? prod.system_title1 : prod.ebay_system_name;

                var sysPartList = (from ep in db.tb_ebay_system_parts
                                   join p in db.tb_product on ep.luc_sku.Value equals p.product_serial_no
                                   join es in db.tb_ebay_system_part_comment on ep.comment_id.Value equals es.id
                                   where ep.system_sku.HasValue
                                   && ep.system_sku.Value.Equals(sysSku)
                                   select new
                                   {
                                       CaseImgSku = es.is_case.Value ? (p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no) : 0,
                                       PartImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no,
                                       PartSKU = p.product_serial_no,
                                       PartDiscount = p.product_current_discount.Value,
                                       PartPrice = p.product_current_price.Value,
                                       CommName = es.comment,
                                       Title = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_name : p.product_ebay_name
                                   }).ToList();
                SysListString += "<table class='table table-striped'>";
                for (int i = 0; i < sysPartList.Count; i++)
                {
                    if (sysPartList[i].PartSKU == setting.NoneSelectedID)
                        continue;
                    SysListString += string.Format(@"
<tr >
    <td>{0}</td>
    <td><strong>{1}</strong></td>
    <td>{2}</td>
</tr>
"
                        , sysPartList[i].PartImgSku > 0 ? "<img src='https://lucomputers.com/pro_img/COMPONENTS/" + sysPartList[i].PartImgSku + "_t.jpg' alt='...' width='30'>" : ""
                        , sysPartList[i].CommName
                        , sysPartList[i].Title);
                }
                SysListString += "</table>";

                ImgSku = sysPartList.FirstOrDefault(p => p.CaseImgSku > 0) != null ? sysPartList.FirstOrDefault(p => p.CaseImgSku > 0).CaseImgSku : 999999;
                string priceListString = "";
                string priceUnitString = CurrSiteCountry == CountryType.CAD ? "<span class='price-unit'>CAD</span>" : "<span class='price-unit'>USD</span>";
                if (sysPartList.Sum(p => p.PartDiscount) > 0)
                {
                    int sku = sysSku;
                    var now = DateTime.Now;

                    priceListString += string.Format(@"<li class=""list-group-item"">Price <span class=""badge price"">${0}{5}</span></li>
                    <li class=""list-group-item"">Instant Discount <small style='color:#E37446'>valid: {4}</small><span class=""badge price"" style='color:blue;'>Save ${1}{5}</span></li>
                    <li class=""list-group-item"">New Low Price <span class=""badge price"">${2}{5}</span></li>
                    <li class=""list-group-item"">Special Cash Price <span class=""badge price"">${3}{5}</span></li>"
                   , PriceRate.Format(ConvertPrice(sysPartList.Sum(p => p.PartPrice)))
                   , PriceRate.Format(ConvertPrice(sysPartList.Sum(p => p.PartDiscount)))
                   , PriceRate.Format(ConvertPrice(sysPartList.Sum(p => p.PartPrice - p.PartDiscount)))
                   , PriceRate.Format(ConvertPrice(PriceRate.Multiply(sysPartList.Sum(p => p.PartPrice - p.PartDiscount), 1 - setting.CardRate)))
                   , ""
                   , priceUnitString);
                }
                else
                {
                    priceListString += string.Format(@"<li class=""list-group-item"">Price <span class=""badge price"">${0}{2}</span></li>
                    <li class=""list-group-item"">Special Cash Price <span class=""badge price"">${1}{2}</span></li>"
                    , PriceRate.Format(ConvertPrice(sysPartList.Sum(p => p.PartPrice)))
                    , PriceRate.Format(ConvertPrice(PriceRate.Multiply(sysPartList.Sum(p => p.PartPrice), 1 - setting.CardRate)))
                    , priceUnitString);
                }

                #region shipping fee
                decimal shippingFee = 0;
                try
                {
                    //shippingFee = new AccountOrder().AccountChargeOne(cateModel.is_noebook.Value == 1 ? ProdType.noebooks : ProdType.part_product
                    //    , prod.product_serial_no
                    //    , 1
                    //    , cateModel.is_noebook.Value == 1
                    //    , 8
                    //    , db);

                }
                catch (Exception ex)
                {
                    shippingFee = -1M;  //  -1M 显示 N/A
                }

                #endregion

                priceListString += string.Format(@"<li class=""list-group-item"">CANADA & USA SHIPPING FROM: <span class=""badge price"">${0}</span></li>"
                    , shippingFee == -1 ? "N/A" : PriceRate.Format(ConvertPrice(shippingFee)));

                string ebayPriceUnit = "";
                string ebayItemId = string.Empty;
                decimal ebayPrice = eBayInfo.GetCurrEbayPrice(db, ProdType.system_product, sysSku, ref ebayPriceUnit, ref ebayItemId);
                priceListString += string.Format(@"<li class=""list-group-item"">On eBay Price: <span class=""badge "">{0}</span></li>"
                    , ebayPrice == 0M ? "N/A" : "eBay Item id: <a href=\"http://www.ebay.ca/itm/ws/eBayISAPI.dll?ViewItem&Item=" + ebayItemId + "\" target=\"_blank\" style='margin-right:2em;'>" + ebayItemId + "</a>" + PriceRate.Format(ebayPrice) + " <small class='price'>$" + ebayPriceUnit + "</small>");

                ltPriceList.Text = priceListString;

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
        <a class=""btn btn-default {5}"" {4}><span class=""glyphicon glyphicon-chevron-right""></span></a>


                     

                            "
                            , i + 1
                            , allqty
                            , i > 0 ? " href='detail_sys.aspx?sku=" + allCate[i - 1].SysSku + "'" : ""
                            , i < 1 ? " disabled" : ""
                             , i < allqty - 1 ? "href='detail_sys.aspx?sku=" + allCate[i + 1].SysSku + "'" : ""
                            , i > allqty - 1 ? " disabled" : ""
                            );

                        break;
                    }
                }
                #endregion

                var imgModel = db.tb_product.Single(p => p.product_serial_no.Equals(ImgSku));
                LogoGallery = GetLogoGallery(ImgSku, imgModel.product_img_sum.Value, ImgSku.ToString(), string.Empty);
            }
        }
    }
    string GetLogoGallery(int imgsku, int qty, string mfp, string title)
    {
        var result = string.Empty;
        for (int i = 0; i < qty; i++)
        {
            result += string.Format(@" <a href=""https://lucomputers.com/pro_img/COMPONENTS/{0}_g_{4}.jpg""
                            data-toggle=""lightbox""
                            data-lightbox=""{1}""
                            data-title=""{2}""
                            data-gallery=""{1}""
                            data-footer=""{4}/{5}""
                            {3}
                            onclick=""$(this).ekkoLightbox();return false;"">
                            <img src=""https://lucomputers.com/pro_img/COMPONENTS/{0}_g_{4}.jpg"" width=""420""
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