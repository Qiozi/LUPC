using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LU.Data;

/// <summary>
/// Summary description for eBayPartWorker
/// </summary>
public class eBayPartWorker
{
	public eBayPartWorker()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    /// <summary>
    /// Save part ebay price to storage.
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="cost"></param>
    /// <param name="custom_label"></param>
    /// <param name="title"></param>
    /// <param name="ebay_fee"></param>
    /// <param name="itemid"></param>
    /// <param name="buyItNowPrice"></param>
    /// <param name="mfp"></param>
    /// <param name="profit"></param>
    /// <param name="shipping_fee"></param>
    /// <param name="web_price"></param>
    public static void SaveHistory(nicklu2Entities context, int sku
        ,decimal cost
        , string custom_label
        , string title
        , decimal ebay_fee
        , string itemid
        , decimal buyItNowPrice
        , string mfp
        , decimal profit
        , decimal shipping_fee
        , decimal web_price)
    {
        //
        // Save parts price history records.
        var epphm = new LU.Data.tb_ebay_part_price_history();// EbayPartPriceHistoryModel();
        epphm.cost = cost;
        epphm.custom_label = custom_label;
        epphm.part_ebay_name = title;
        epphm.ebay_fee = ebay_fee;
        epphm.ebay_itemid = itemid;
        epphm.ebay_price = buyItNowPrice;
        epphm.luc_sku = sku;
        epphm.mfp = mfp;
        epphm.profit = profit;
        epphm.shipping = shipping_fee;
        epphm.web_price = web_price;
        //  epphm.Create();
        context.tb_ebay_part_price_history.Add(epphm);
        context.SaveChanges();
    }


}
