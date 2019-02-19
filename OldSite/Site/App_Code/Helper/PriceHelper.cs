using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using LU.Data;

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
    public bool SaveSpecialCash(nicklu2Entities context, int luc_sku, decimal special_cash)
    {
        var pm = ProductModel.GetProductModel(context, luc_sku);

        var pps = context.tb_part_price_change_setting.Where(me => me.category_id.Value.Equals(pm.menu_child_serial_no)).ToList();// PartPriceChangeSettingModel.FindAllByProperty("category_id", pm.menu_child_serial_no);

        decimal new_cost_total = pm.product_current_cost.Value + pm.adjustment.Value;

        for (int i = 0; i < pps.Count; i++)
        {
            // 取得Cost+adjustment的价格范围（区间）
            if (new_cost_total > pps[i].cost_min
                && new_cost_total <= pps[i].cost_max)
            {
                if (pps[i].is_percent.Value)
                {

                    new_cost_total = special_cash / decimal.Parse(pps[i].rate.ToString()) * 100M;
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
    public decimal GetCardPrice(nicklu2Entities context, decimal cost_adjustment, int categoryID)
    {
        decimal cardPrice = 0M;
        var pps = context.tb_part_price_change_setting.Where(me => me.category_id.Value.Equals(categoryID)).ToList();// PartPriceChangeSettingModel.FindAllByProperty("category_id", categoryID);

        for (int i = 0; i < pps.Count; i++)
        {
            // 取得Cost+adjustment的价格范围（区间）
            if (cost_adjustment > pps[i].cost_min
                && cost_adjustment <= pps[i].cost_max)
            {
                if (pps[i].is_percent.Value)
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
    public bool SaveAdjust(nicklu2Entities context, int luc_sku, decimal adjust, DateTime adjustEndDate)
    {
        return SaveAdjust(context, luc_sku, adjust, null, adjustEndDate);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="luc_sku"></param>
    /// <param name="adjust"></param>
    /// <returns></returns>
    public bool SaveAdjust(nicklu2Entities context, int luc_sku, decimal adjust, tb_product pm, DateTime adjustEndDate)
    {
        if (pm == null)
            pm = ProductModel.GetProductModel(context, luc_sku);

        var pps = context.tb_part_price_change_setting.Where(me => me.category_id.Value.Equals(pm.menu_child_serial_no.Value)).ToList();// PartPriceChangeSettingModel.FindAllByProperty("category_id", pm.menu_child_serial_no);

        decimal new_cost_total = pm.product_current_cost.Value + adjust;

        //throw new Exception(special_cash.ToString());
        pm.adjustment = adjust;
        pm.adjustment_enddate = adjustEndDate == DateTime.MinValue ? DateTime.Parse("1971-01-01") : adjustEndDate;
        pm.adjustment_regdate = adjust > 0M ? DateTime.Now : DateTime.Parse("1971-01-01");

        pm.product_current_price = ConvertPrice.SpecialCashPriceConvertToCardPrice(GetCardPrice(context, new_cost_total, pm.menu_child_serial_no.Value)) + pm.product_current_discount;
        pm.product_current_special_cash_price = ConvertPrice.ChangePriceToNotCard(pm.product_current_price.Value - pm.product_current_discount.Value);
        // pm.Update();
        context.SaveChanges();
        return true;
    }
    /// <summary>
    /// 修改关联产品价格
    /// </summary>
    /// <param name="luc_sku"></param>
    /// <returns></returns>
    public void ModifyRelevancePrice(nicklu2Entities context, int luc_sku, decimal cost, decimal adjustment, DateTime adjustEndDate)
    {
        var pm = ProductModel.GetProductModel(context, luc_sku);
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("price_sku", luc_sku);
        var pms = context.tb_product.Where(me=>me.price_sku.Value.Equals(luc_sku)).ToList();// ProductModel.FindAll(eq1);

        foreach (tb_product p in pms)
        {
            p.product_current_cost = pm.product_current_cost * p.price_sku_quantity;
            p.adjustment = pm.adjustment * p.price_sku_quantity;
            context.SaveChanges();
            SaveAdjust(context, p.product_serial_no, p.adjustment.Value, p, adjustEndDate);
        }
    }

}
