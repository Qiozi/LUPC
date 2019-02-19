using LU.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cmds_systemProd : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsLocalHostFrom)
            {
                // get
                switch (ReqCmd)
                {
                    case "getGroupDetail":
                        WriteGroupDetail();
                        break;

                    case "getSingleSysPrice":
                        WriteSysPrice(ReqSKU, ReqIsFormat);
                        break;

                }
                // post
                if (Util.GetStringSafeFromString(Page, "cmd") == "createSysFormCustomize")
                {
                    CreateSysFromCustomize(Util.GetStringSafeFromString(Page, "parts")
                        , Util.GetInt32SafeFromString(Page, "syssku", 0));
                }
            }
            else
            {
                Response.Write("No local.");
                Response.End();
            }
        }
        Response.End();
    }

    /// <summary>
    /// 输出单个系统的价格
    /// 
    /// </summary>
    /// <param name="sysSku"></param>
    /// <param name="isFormat"></param>
    void WriteSysPrice(int sysSku, bool isFormat)
    {
        var sysPartList = (from ep in db.tb_ebay_system_parts
                           join p in db.tb_product on ep.luc_sku.Value equals p.product_serial_no
                           where ep.system_sku.HasValue && ep.system_sku.Value.Equals(sysSku)
                           select new
                           {
                               PartPrice = p.product_current_price.Value,
                               PartDiscount = p.product_current_discount.Value
                           }).ToList();

        Response.Write(string.Format(@"{{""Price"":""{0}"",""Discount"":""{1}"",""Sold"":""{2}"", ""Unit"":""{3}""}}"
            , isFormat ? LU.BLL.FormatProvider.Price(ConvertPrice(sysPartList.Sum(p => p.PartPrice))) : ConvertPrice(sysPartList.Sum(p => p.PartPrice)).ToString()
            , isFormat ? LU.BLL.FormatProvider.Price(ConvertPrice(sysPartList.Sum(p => p.PartDiscount))) : ConvertPrice(sysPartList.Sum(p => p.PartDiscount)).ToString()
            , isFormat ? LU.BLL.FormatProvider.Price(ConvertPrice(sysPartList.Sum(p => p.PartPrice - p.PartDiscount))) : ConvertPrice(sysPartList.Sum(p => p.PartPrice - p.PartDiscount)).ToString()
            , this.cookiesHelper.CurrSiteCountry == CountryType.CAD ? "CAD" : "USD"));
    }


    /// <summary>
    /// 创建新系统
    /// </summary>
    /// <param name="parts"></param>
    /// <param name="sysSku"></param>
    void CreateSysFromCustomize(string parts, int sysSku)
    {
        Response.Write(SysProd.CustomizeNewSys(parts
            , sysSku
            , ""
            , Request.UserHostAddress
            , this.cookiesHelper.CurrSiteCountry
            , true
            , db));
    }

    /// <summary>
    /// 输出系统零件群主明细
    /// </summary>
    void WriteGroupDetail()
    {
        var sysGroupDetail = (from pgd in db.tb_part_group_detail
                              join p in db.tb_product on pgd.product_serial_no.Value equals p.product_serial_no
                              where (p.tag.HasValue && p.tag.Value.Equals(1)
                              && pgd.part_group_id.HasValue
                              && pgd.part_group_id.Value.Equals(ReqPartGroupId) &&
                              ((p.is_fixed.HasValue && p.is_fixed.Value.Equals(true)) || (p.for_sys.HasValue && p.for_sys.Value.Equals(true)))
                              )
                              orderby pgd.nominate.Value descending
                              orderby p.producter_serial_no ascending
                              orderby p.product_ebay_name ascending
                              select new
                              {
                                  Title = string.IsNullOrEmpty(p.product_ebay_name) ? p.product_name_long_en : p.product_ebay_name,
                                  Price = p.product_current_price.Value,
                                  Sold = p.product_current_price.Value - p.product_current_discount.Value,
                                  Discount = p.product_current_discount.Value,
                                  SKU = p.product_serial_no,
                                  ImgSku = p.other_product_sku.Value > 0 ? p.other_product_sku.Value : p.product_serial_no

                              }).ToList();

        string result = "<div class=\"list-group \">";

        // 
        //
        //  none select  排在最前面
        foreach (var p in sysGroupDetail)
        {
            if (p.SKU == setting.NoneSelectedID)
            {
                result += string.Format(@"<a class=""list-group-item {3}"" onclick='selectedPart($(this));return false;'
sysSku='{4}'
partSku='{5}'
partgroupid='{6}' 
style='cursor:pointer;'
>
    <img src='{1}'>
    <span class='itemTitle'> {0}</span>
    <span class='note itemPrice'>${2}</span>
</a>"
               , p.Title
               , setting.ImgHost + "pro_img/COMPONENTS/" + p.ImgSku + "_t.jpg"
               , p.Sold
               , p.SKU == ReqPartSKU ? "list-group-item-info" : ""
               , ReqSysSku
               , p.SKU
               , ReqPartGroupId
               );

                break;
            }
        }

        foreach (var p in sysGroupDetail)
        {
            if (p.SKU == setting.NoneSelectedID)
                continue;
            result += string.Format(@"<a class=""list-group-item sys-group-item {3} "" onclick='selectedPart($(this));return false;'
sysSku='{4}'
partSku='{5}'
partgroupid='{6}' 
style='cursor:pointer;'
>
    <img src='{1}'>
    <span class='itemTitle'> {0}</span>
    <span class='note itemPrice'>${2}</span>
</a>"
                , p.Title
                , setting.ImgHost + "pro_img/COMPONENTS/" + p.ImgSku + "_t.jpg"
                , p.Sold
                , p.SKU == ReqPartSKU ? "list-group-item-info" : ""
                , ReqSysSku
                , p.SKU
                , ReqPartGroupId
                );
        }
        if (sysGroupDetail.Count == 0)
            result += "<div class='alert alert-warning'>No data.</div>";
        result += "</div>";

        Response.Write(result);
    }

    int ReqPartGroupId
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "partgroupid", 0); }
    }

    int ReqPartSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "partsku", 0); }
    }

    int ReqSysSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "syssku", 0); }
    }

    /// <summary>
    /// 是否格式化价格
    /// </summary>
    bool ReqIsFormat
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "isformat", 0) == 1; }
    }
}