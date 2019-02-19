using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for GeteBayOnsalePriceAdjust
/// </summary>
public class GeteBayOnsalePriceAdjust
{
	public GeteBayOnsalePriceAdjust()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static decimal GetEbayOnsalePrice(int sku)
    {
        var dt = Config.ExecuteDataTable("select SavePrice from tb_ebay_promotional_items where luc_sku='" + sku + "'");
        if (dt.Rows.Count == 1)
        {
            return decimal.Parse(dt.Rows[0][0].ToString());
        }
        return 0M;
    }
}