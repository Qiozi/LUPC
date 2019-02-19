using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_ebayMaster_ebay_notebook_get_ebayPrice : PageBaseNoInit
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (ReqLUCSku > 0)
            {
                eBayPriceHelper eH = new eBayPriceHelper();

                // Config.ExecuteNonQuery("Update tb_product set screen_size='" + Screen.ToString() + "',is_modify=1 where product_serial_no='" + LUC_Sku.ToString() + "'");
                Response.Clear();
                Response.ClearContent();
                decimal adjustment = Adjustment;
                decimal shipping_fee = 0M;
                decimal profit = 0M;
                decimal ebay_fee = 0M;
                decimal bank_fee = 0M;

                if (OnlyEbayPrice)
                {
                    var pm = ProductModel.GetProductModel(DBContext, ReqLUCSku);
                    if (pm != null)
                    {
                        decimal ebayPrice = eH.eBayNetbookPartPrice(pm.product_current_cost.Value + GeteBayOnsalePriceAdjust.GetEbayOnsalePrice(pm.product_serial_no)
                            , pm.screen_size.Value
                            , pm.adjustment.Value
                            , ref shipping_fee
                            , ref profit
                            , ref ebay_fee
                            , ref bank_fee);

                        Response.Write((ebayPrice + EbaySettings.ebayAccessoriesPrice).ToString());
                    }
                    else
                        Response.Write("no product of db.");
                }
                else
                {
                    var pm = ProductModel.GetProductModel(DBContext, ReqLUCSku);
                    if (ProductCategoryModel.IsNotebook(DBContext, pm.menu_child_serial_no.Value))
                    {

                        decimal ebayPrice = eH.eBayNetbookPartPrice(pm.product_current_cost.Value + GeteBayOnsalePriceAdjust.GetEbayOnsalePrice(pm.product_serial_no)
                            , pm.screen_size.Value, pm.adjustment.Value
                            , ref shipping_fee, ref profit, ref ebay_fee, ref bank_fee);

                        Response.Write(string.Format(@"[{{
            
            'ebayPrice':'{0}'
            ,'shipping_fee':'{1}'
            ,'profit':'{2}'
            ,'ebay_fee':'{3}'           
            
}}]"
                      , ConvertPrice.RoundPrice(ebayPrice + EbaySettings.ebayAccessoriesPrice)
                      , ConvertPrice.RoundPrice(shipping_fee)
                      , ConvertPrice.RoundPrice(profit)
                      , ConvertPrice.RoundPrice(ebay_fee)

                      ));
                    }
                    else
                    {
                        //decimal ebayPrice = eH.eBayNetbookPartPrice(Cost, Screen, adjustment
                        //   , ref shipping_fee, ref profit, ref ebay_fee);
                        Response.Write(string.Format(@"[{{
            
            'ebayPrice':'{0}'
            ,'shipping_fee':'{1}'
            ,'profit':'{2}'
            ,'ebay_fee':'{3}'           
            
}}]"
                      , ConvertPrice.RoundPrice(GetEbayPrice.GetPartEbayPrice(DBContext, pm, GeteBayOnsalePriceAdjust.GetEbayOnsalePrice(pm.product_serial_no), ref shipping_fee, ref profit, ref ebay_fee, ref bank_fee) + EbaySettings.ebayAccessoriesPrice)
                      , ConvertPrice.RoundPrice(0M)
                      , ConvertPrice.RoundPrice(0M)
                      , ConvertPrice.RoundPrice(0M)));
                    }
                }

                Response.End();
            }
        }
    }


    #region properties
    public decimal Cost
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "Cost", -1M); }
    }
    public decimal Screen
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "Screen", -1M); }
    }
    public decimal Adjustment
    {
        get { return Util.GetDecimalSafeFromQueryString(Page, "Adjustment", -1M); }
    }
    public int ReqLUCSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "LUC_Sku", -1); }
    }
    /// <summary>
    /// 按sku 只输ebay price.
    /// </summary>
    bool OnlyEbayPrice
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "OnlyEbayPrice", -1) == 1; }
    }
    #endregion
}
