using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for eBayInfo
/// </summary>
public class eBayInfo
{
	public eBayInfo()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// 取得当前ebay 在线的价格
    /// </summary>
    /// <param name="db"></param>
    /// <param name="pt"></param>
    /// <param name="SKU"></param>
    /// <param name="priceUnit"></param>
    /// <param name="itemId"></param>
    /// <returns></returns>
    public static decimal GetCurrEbayPrice(nicklu2Model.nicklu2Entities db
        , ProdType pt
        , int SKU
        , ref string priceUnit
        , ref string itemId)
    {
        var ebayList = db.tb_ebay_selling.FirstOrDefault(p =>
            pt == ProdType.system_product ? (p.sys_sku.HasValue && p.sys_sku.Value.Equals(SKU))
            : (p.luc_sku.HasValue && p.luc_sku.Value.Equals(SKU)));
        if (ebayList != null)
        {
            priceUnit = ebayList.BuyItNowPrice_currencyID;
            itemId = ebayList.ItemID;
            return ebayList.BuyItNowPrice.Value;

        }
        return 0M;
    }
}