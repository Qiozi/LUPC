using System;
using System.Linq;
using System.Data;
using LU.Data;

/// <summary>
/// Summary description for GetEbayPrice
/// </summary>
public class GetEbayPrice
{
    public GetEbayPrice()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Obtain ebay system price
    /// old version.
    /// get price to show on flash coustomize.
    /// </summary>
    /// <param name="sys_sku"></param>
    /// <param name="selected_ebay_sell"></param>
    /// <param name="no_selected_ebay_sell"></param>
    /// <returns></returns>
    public static decimal GetSysPrice(nicklu2Entities context, int sys_sku
        , ref decimal selected_ebay_sell
        , ref decimal no_selected_ebay_sell
        , ref decimal all_ebay_ell)
    {
        var esm = EbaySystemModel.GetEbaySystemModel(context, sys_sku);
        if (esm == null)
            throw new Exception("No find system info.");
        selected_ebay_sell = esm.selected_ebay_sell.Value;
        no_selected_ebay_sell = esm.no_selected_ebay_sell.Value;
        all_ebay_ell = esm.ebay_system_price.Value;
        if (esm.is_shrink.Value)
            return esm.selected_ebay_sell.Value;
        else
            return esm.ebay_system_price.Value;
    }
    ///// <summary>
    ///// old
    ///// ebay system price.
    ///// No include Shipping fee.
    ///// </summary>
    ///// <param name="cost"></param>
    ///// <param name="adjustment"></param>
    ///// <param name="profits"></param>
    ///// <param name="ebay_fee"></param>
    ///// <returns></returns>
    //public static decimal GetEbaySysPriceNoIncludeShipping(decimal cost, decimal adjustment
    //    , ref decimal profits
    //    , ref decimal ebay_fee
    //    , ref decimal shipping_fee)
    //{
    //    return eBayPriceHelper.SysPR(cost, adjustment, ref profits, ref ebay_fee, ref shipping_fee);
    //}

    /// <summary>
    /// Obtain ebay system price.
    /// New
    /// for Version that the label on flash.
    /// </summary>
    /// <param name="cost"></param>
    /// <param name="adjustment"></param>
    /// <param name="profits"></param>
    /// <param name="ebay_fee"></param>
    /// <returns></returns>
    public static decimal GetEbaySysPrice(decimal cost, decimal adjustment
        , ref decimal profits
        , ref decimal ebay_fee
        , ref decimal shipping_fee
        , bool isIncludShippingFee)
    {
        shipping_fee = eBayShipping.SysShippingCa(cost);
        return eBayPriceHelper.SysPRNew(cost, shipping_fee, adjustment, ref profits, ref ebay_fee);
    }
    /// <summary>
    /// get eBay System Cost.
    /// p.tag=1 or 0
    /// is_belong_price
    /// </summary>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    public static void GetEbaySysCost(int system_sku
        , ref decimal belong_cost
        , ref decimal belong_price
        , ref decimal web_cost
        , ref decimal web_price)
    {

        string sql = @"Select  sum(product_current_cost*es.part_quantity) cost 
                        , sum((product_current_price-product_current_discount)*es.part_quantity) price 
                        from tb_product p inner join  tb_ebay_system_parts es on es.luc_sku=p.product_serial_no 
                        where p.split_line=0 and es.system_sku='" + system_sku.ToString() + "'";

        DataTable dt = Config.ExecuteDataTable(sql);
        if (dt.Rows.Count == 1)
        {
            web_price = decimal.Parse(dt.Rows[0]["price"].ToString());
            web_cost = decimal.Parse(dt.Rows[0][0].ToString());
        }
        else
        {
            throw new ArgumentNullException("Find no eBay System SKU.");
        }

        sql = @"Select  sum(product_current_cost*es.part_quantity) cost 
                        , sum((product_current_price-product_current_discount)*es.part_quantity) price 
                        from tb_product p inner join  tb_ebay_system_parts es on es.luc_sku=p.product_serial_no 
                        where p.split_line=0 and es.is_belong_price=1 and es.system_sku='" + system_sku.ToString() + "'";

        dt = Config.ExecuteDataTable(sql);
        if (dt.Rows.Count == 1)
        {
            belong_price = decimal.Parse(dt.Rows[0]["price"].ToString());
            belong_cost = decimal.Parse(dt.Rows[0][0].ToString());
        }
        else
        {
            throw new ArgumentNullException("Find no eBay System SKU.");
        }
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="sysSku"></param>
    /// <returns></returns>
    public static decimal GetEbaySysCost(int sysSku)
    {
        string sql = @"Select  sum(product_current_cost*es.part_quantity) cost 
                        , sum((product_current_price-product_current_discount)*es.part_quantity) price 
                        from tb_product p inner join  tb_ebay_system_parts es on es.luc_sku=p.product_serial_no 
                        where p.split_line=0 and es.system_sku='" + sysSku.ToString() + "'";

        DataTable dt = Config.ExecuteDataTable(sql);
        if (dt.Rows.Count == 1)
        {
            return decimal.Parse(dt.Rows[0][0].ToString());
        }
        else
        {
            throw new ArgumentNullException("Find no eBay System SKU.");
        }
    }
    /// <summary>
    /// get eBay System Adjustment.
    /// if null then return 0M;
    /// </summary>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    public static decimal GetEbaySysAdjustment(nicklu2Entities context, int system_sku, ref tb_ebay_system esm)
    {
        esm = EbaySystemModel.GetEbaySystemModel(context, system_sku);
        if (esm != null)
            return esm.adjustment.Value;
        else
        {
            string sql = @"Select  sum(p.adjustment) adjustment 
                        from tb_product p inner join  tb_ebay_system_parts es on es.luc_sku=p.product_serial_no 
                        where p.split_line=0 and es.system_sku='" + system_sku.ToString() + "'";

            DataTable dt = Config.ExecuteDataTable(sql);
            if (dt.Rows.Count == 1)
            {
                decimal adjustment;
                decimal.TryParse(dt.Rows[0][0].ToString(), out adjustment);
                return adjustment;
            }
            return 0M;
        }
    }

    /// <summary>
    /// Formation on ebay sales price.
    /// 
    /// </summary>
    /// <param name="PartSKU"></param>
    /// <returns></returns>
    public static decimal GetPartEbayPrice(nicklu2Entities context, tb_product pm
        , decimal ebayOnSaleAdjust
        , ref decimal shipping_fee
        , ref decimal profit
        , ref decimal ebay_fee
        , ref decimal bank_fee)
    {
        //var pm = ProductModel.GetProductModel(PartSKU);
        if (pm == null)
            return 100000M;

        shipping_fee = GetPartMinShippingFee(context, pm);
        ebay_fee = 0M;

        decimal paypal = eBayPriceHelper.PaypalFee(pm.product_current_price.Value - pm.product_current_discount.Value + ebayOnSaleAdjust);
        decimal fvf = eBayPriceHelper.FVF(pm.product_current_price.Value - pm.product_current_discount.Value, shipping_fee, 0M);
        ebay_fee = paypal + fvf;
        // throw new Exception(ebayFee.ToString());
        profit = (pm.product_current_price.Value - pm.product_current_discount.Value) / 1.022M - pm.product_current_cost.Value;
        bank_fee = pm.product_current_price.Value - pm.product_current_discount.Value - pm.product_current_cost.Value - profit;

        return pm.product_current_price.Value - pm.product_current_discount.Value + ebay_fee + shipping_fee + ebayOnSaleAdjust;
    }
    /// <summary>
    /// Formation on ebay sales price.
    /// </summary>
    /// <param name="PartSKU"></param>
    /// <param name="shipping_fee"></param>
    /// <param name="profit"></param>
    /// <param name="ebay_fee"></param>
    /// <param name="bank_fee"></param>
    /// <returns></returns>
    public static decimal GetPartEbayPrice(nicklu2Entities context, int PartSKU
       , ref decimal shipping_fee
       , ref decimal profit
       , ref decimal ebay_fee
       , ref decimal bank_fee)
    {
        var pm = ProductModel.GetProductModel(context, PartSKU);
        return GetPartEbayPrice(context, pm, GeteBayOnsalePriceAdjust.GetEbayOnsalePrice(pm.product_serial_no)
            , ref shipping_fee, ref profit, ref ebay_fee, ref bank_fee);
    }
    /// <summary>
    /// 最低运费
    /// </summary>
    /// <param name="pm"></param>
    /// <returns></returns>
    public static decimal GetPartMinShippingFee(nicklu2Entities context, tb_product pm)
    {

        decimal shippFeeEconomy = 100M;
        decimal shippFeeUPSStandard = 100m;
        decimal price = pm.product_current_price.Value - pm.product_current_discount.Value;
        decimal minShipping = 100M;

        decimal shippFeeEconomyUS = 100M;
        decimal shippFeeUPSStandardUS = 100m;


        string shipCate = "";
        DataTable shipDt = Config.ExecuteDataTable("select ShippingCategoryId from tb_part_and_shipping where sku='" + pm.product_serial_no.ToString() + "' limit 1");
        if (shipDt.Rows.Count == 1)
        {
            shipCate = shipDt.Rows[0][0].ToString();
        }

        var list = context.tb_ebay_shipping_settings.Where(me=>me.CategoryID.Equals(shipCate)).ToList();// EbayShippingSettingsModel.FindAllByProperty("CategoryID", shipCate);
        var query = list.Where(p => p.shippingFee > 0).OrderBy(p => p.shippingFee).ToList();
        if (query.Count > 0)
        {
            return query[0].shippingFee.Value;
        }
        foreach (var m in list)
        {
            if (m.shippingFee > 0)
            {
                if (price < 80 || pm.menu_child_serial_no == 216) //216 OS software
                {
                    if (m.shippingCompany == "CA_EconomyShipping")
                    {

                        shippFeeEconomy = m.shippingFee.Value;
                    }

                    if (m.shippingCompany == "CA_StandardInternational")
                    {
                        shippFeeEconomyUS = m.shippingFee.Value;
                    }

                    if (shippFeeEconomy > shippFeeEconomyUS)
                        minShipping = shippFeeEconomyUS;
                    else
                        minShipping = shippFeeEconomy;
                }
                if (price >= 80 || minShipping == 100M || pm.menu_child_serial_no == 216)
                {
                    if (m.shippingCompany == "CA_StandardShipping")
                    {

                        shippFeeUPSStandard = m.shippingFee.Value;
                    }
                    if (m.shippingCompany == "CA_UPSStandardUnitedStates")
                    {
                        shippFeeUPSStandardUS = m.shippingFee.Value;
                    }
                    if (shippFeeUPSStandard > shippFeeUPSStandardUS)
                        minShipping = shippFeeUPSStandardUS;
                    else
                        minShipping = shippFeeUPSStandard;
                }
            }
        }

        return minShipping;
    }
}
