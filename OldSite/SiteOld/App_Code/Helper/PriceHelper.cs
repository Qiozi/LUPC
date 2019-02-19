using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;


/// <summary>
/// Summary description for PriceHelper
/// </summary>
public class PriceHelper
{
	public PriceHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// 
    /// </summary>
    /// <param name="luc_sku"></param>
    /// <param name="special_cash"></param>
    /// <returns>new Special Cash Value</returns>
    public bool SaveSpecialCash(int luc_sku, decimal special_cash)
    {
        ProductModel pm = ProductModel.GetProductModel(luc_sku);

        PartPriceChangeSettingModel[] pps = PartPriceChangeSettingModel.FindAllByProperty("category_id", pm.menu_child_serial_no);

        decimal new_cost_total = pm.product_current_cost + pm.adjustment;

        for (int i = 0; i < pps.Length; i++)
        {
            // 取得Cost+adjustment的价格范围（区间）
            if (new_cost_total > pps[i].cost_min
                && new_cost_total <= pps[i].cost_max)   
            {
                if (pps[i].is_percent)
                {
                    
                    new_cost_total = special_cash / decimal.Parse(pps[i].rate.ToString()) *100M ;
                }
                else
                {
                    new_cost_total = special_cash - decimal.Parse(pps[i].rate.ToString());
                }
                break;
            }
        }

        return true;
    }

    /// <summary>
    /// 通过cost, adjustment, categoryID 匹配相应的价格变化公式
    /// </summary>
    /// <param name="cost_adjustment"></param>
    /// <param name="categoryID"></param>
    /// <returns></returns>
    public decimal GetCardPrice(decimal cost_adjustment, int categoryID)
    {
        decimal cardPrice = 0M;
        PartPriceChangeSettingModel[] pps = PartPriceChangeSettingModel.FindAllByProperty("category_id", categoryID);

        for (int i = 0; i < pps.Length; i++)
        {
            // 取得Cost+adjustment的价格范围（区间）
            if (cost_adjustment > pps[i].cost_min
                && cost_adjustment <= pps[i].cost_max)
            {
                if (pps[i].is_percent)
                {
                    cardPrice = cost_adjustment * decimal.Parse(pps[i].rate.ToString()) / 100M;
                }
                else
                {
                    cardPrice = cost_adjustment + decimal.Parse(pps[i].rate.ToString());
                }
                break;
            }
        }

        if (cardPrice == 0M)
            throw new ArgumentNullException("Price, cost Area is null");
        return cardPrice;
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="luc_sku"></param>
    /// <param name="adjust"></param>
    /// <returns></returns>
    public bool SaveAdjust(int luc_sku, decimal adjust, DateTime adjustEndDate)
    {
        return SaveAdjust(luc_sku, adjust, null, adjustEndDate);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="luc_sku"></param>
    /// <param name="adjust"></param>
    /// <returns></returns>
    public bool SaveAdjust(int luc_sku, decimal adjust, ProductModel pm, DateTime adjustEndDate)
    {
        if (pm == null)
            pm = ProductModel.GetProductModel(luc_sku);

        PartPriceChangeSettingModel[] pps = PartPriceChangeSettingModel.FindAllByProperty("category_id", pm.menu_child_serial_no);

        decimal new_cost_total = pm.product_current_cost + adjust;

        //throw new Exception(special_cash.ToString());
        pm.adjustment = adjust;
        pm.adjustment_enddate = adjustEndDate == DateTime.MinValue ? DateTime.Parse("1971-01-01") : adjustEndDate;
        pm.adjustment_regdate = adjust > 0M ? DateTime.Now : DateTime.Parse("1971-01-01");

        pm.product_current_price = ConvertPrice.SpecialCashPriceConvertToCardPrice(GetCardPrice(new_cost_total, pm.menu_child_serial_no)) + pm.product_current_discount;
        pm.product_current_special_cash_price = ConvertPrice.ChangePriceToNotCard(pm.product_current_price - pm.product_current_discount);
        pm.Update();

        return true;
    }
    /// <summary>
    /// 修改关联产品价格
    /// </summary>
    /// <param name="luc_sku"></param>
    /// <returns></returns>
    public void ModifyRelevancePrice(int luc_sku, decimal cost, decimal adjustment, DateTime adjustEndDate)
    {
        ProductModel pm = ProductModel.GetProductModel(luc_sku);
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("price_sku", luc_sku);
        ProductModel[] pms = ProductModel.FindAll(eq1);

        foreach (ProductModel p in pms)
        {
            p.product_current_cost = pm.product_current_cost * p.price_sku_quantity;
            p.adjustment = pm.adjustment * p.price_sku_quantity;
            p.Update();
            SaveAdjust(p.product_serial_no, p.adjustment, p, adjustEndDate);
        }
    }
    
}
