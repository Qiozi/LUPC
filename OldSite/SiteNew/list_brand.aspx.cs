using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class list_brand : PageBase
{
    public string BrandHref = string.Empty;
    public string BrandLogo = string.Empty;
    public string BrandName = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!string.IsNullOrEmpty(ReqBrandName))
            {
                var query = db.tb_producter.Where(p => !string.IsNullOrEmpty(p.logo_url)).ToList();
                foreach (var brand in query)
                {
                    if (LU.BLL.Util.FilterName(brand.producter_name) == ReqBrandName)
                    {
                        BrandHref = brand.producter_web_address;
                        BrandLogo = brand.logo_url;
                        BindList(brand.producter_name);
                    }
                }
            }
        }
    }

    void BindList(string brand)
    {
        BrandName = brand;
        var query = db.tb_product.Where(p => p.tag.HasValue && p.tag.Value.Equals(1) &&
        p.producter_serial_no.Equals(brand)).OrderBy(p => p.menu_child_serial_no.Value).ToList();

        var cateIds = query.Select(p => p.menu_child_serial_no.Value).ToList();

        var cates = db.tb_product_category.Where(p => cateIds.Contains(p.menu_child_serial_no) && !string.IsNullOrEmpty(p.menu_child_name_f)).ToList();
        this.rptList.DataSource = cates;
        this.rptList.DataBind();
        this.rptList2.DataSource = cates;
        this.rptList2.DataBind();
    }

    public string ReqBrandName
    {
        get { return Util.GetStringSafeFromQueryString(Page, "brand"); }
    }

    protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header &&
            e.Item.ItemType != ListItemType.Footer)
        {
            var subRpt = e.Item.FindControl("_rptSub") as Repeater;
            var brandName = (e.Item.FindControl("brandName") as HiddenField).Value;
            var cateId = int.Parse((e.Item.FindControl("cateId") as HiddenField).Value);
            var query = db.tb_product.Where(p => p.producter_serial_no.Equals(BrandName)
                                && p.menu_child_serial_no.HasValue
                                && p.menu_child_serial_no.Value.Equals(cateId)
                                && p.tag.HasValue
                                && p.tag.Value.Equals(1)).OrderByDescending(p=>p.product_serial_no).ToList();
            var dbSource = (from c in query
                            select new LU.Model.ModelV1.PartForBrandListItem
                            {
                                logo = LU.BLL.QiNiuImgHelper.Get(LU.BLL.Util.GetImgSku(c.product_serial_no, c.other_product_sku), 350, 350),
                                name = string.IsNullOrEmpty(c.product_ebay_name) ? c.product_name : c.product_ebay_name,
                                price = c.product_current_price.Value - c.product_current_discount.Value,
                                webHref = LU.BLL.Util.PartUrl(c.product_serial_no, c.manufacturer_part_number),
                                Sku = c.product_serial_no,
                                eBayCode = "",
                                eBayHref = "",
                                eBayPrice = 0,
                                MFP  = c.manufacturer_part_number
                            }).ToList();
            var prodEbayItems = LU.BLL.CacheProvider.GeteBayCodes(db);
            for (int i=0; i<dbSource.Count; i++)
            {
                var ebayItem = prodEbayItems.FirstOrDefault(p => p.Sku.Equals(dbSource[i].Sku));
                if (ebayItem != null)
                {
                    dbSource[i].eBayCode = ebayItem.ItemId;
                    dbSource[i].eBayPrice = ebayItem.BuyItNowPrice;
                    dbSource[i].eBayHref = LU.BLL.Util.eBayUrl(ebayItem.ItemId);
                }
            }
            subRpt.DataSource = dbSource;
            subRpt.DataBind();
        }
    }
}

