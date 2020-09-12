using LU.Data;
using System;
using System.Data;

/// <summary>
/// Summary description for EbayHelper
/// </summary>
public class EbayHelper
{
    const int Default_TEMPLETE_ID = 7;
	public EbayHelper()
	{
        
		//
		// TODO: Add constructor logic here
		//
    }

    #region eBay Shipping Fee
    /// <summary>
    /// 
    /// </summary>
    /// <param name="total"></param>
    /// <returns></returns>
    public decimal GetShippingFee(decimal total)
    {
        if (total <= 700)
        {
            return 40M;
        }
        else if (total > 700 && total <= 1050)
            return 45;
        else if (total > 1050 && total <= 1440)
            return 55;
        else
            return 65;
    }
    #endregion

    #region eBay system Price


    /// <summary>
    /// 系统价格是按系统的cost的大小，运费，以及adjustment来决定的。
    /// </summary>
    /// <param name="sys_sku"></param>
    /// <returns></returns>
    public decimal GetSysPriceAccount(int sys_sku)
    {
        decimal price = 0M;
        decimal cost = GetSysPartEbayPriceSum(sys_sku);
        decimal adajustment = GetAdajustment(sys_sku);
        decimal shipping_fee = GetShippingFee(cost+adajustment);
        decimal total = cost + adajustment;
        decimal total1;
        decimal total2;
        //cost = GetSysCost(sys_sku);

    // 'a: cost
    // 'e: price adjustment
   
    
     
    // '1.1 1.08 1.065 profit
    // '1.022 with paypal fee
    // 'response.Write total
        if (total > 0 && total <= 1000)
            total = total * 1.1M * 1.022M;
        else if (total > 1000 && total <= 1500)
            total = total * 1.08M * 1.022M;
        else
            total = total * 1.065M * 1.022M;

//    '1.055 when sale price <=1000, ebay fee
//    '1.025 when sale price >1000, ebay fee
    
//    'c1 = c * 1.055
//    'c2 = c * 1.025
        total1 = total * 1.055M;
        total2 = total * 1.025M;

        cost = total + shipping_fee * 1.022M;

        if (total2 >= 1 && total2 <= 1000
            && cost > 1000)
            price = cost;
        else if (total2 >= 1 && total2 <= 1000
            && cost <= 1000)
            price = total1;
        else
            price = total2;

        return price;
    }

    /// <summary>
    /// 获取 eBay Sys 的 Cost. 零件的售价。
    /// </summary>
    /// <param name="sys_sku"></param>
    /// <returns></returns>
    public decimal GetSysPartEbayPriceSum(int sys_sku)
    {
        DataTable dt = Config.ExecuteDataTable(string.Format(@"
select sum(p.part_ebay_price) from tb_ebay_system_parts es 
    inner join tb_product p on p.product_serial_no=es.luc_sku
    where p.split_line=0 and es.system_sku='{0}'"
            , sys_sku));
        if (dt.Rows.Count == 1)
        {
            decimal cost;
            decimal.TryParse(dt.Rows[0][0].ToString(), out cost);

            return cost;
        }
        else
            throw new ArgumentException("Don't find eBay Sys");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sys_sku"></param>
    /// <returns></returns>
    public decimal GetAdajustment(int sys_sku)
    {
        DataTable dt = Config.ExecuteDataTable("Select ifnull(adjustment,0) from tb_ebay_system where id='" + sys_sku.ToString() + "'");
        if (dt.Rows.Count == 1)
        {
            decimal price;
            decimal.TryParse(dt.Rows[0][0].ToString(), out price);
            return price;
        }
        else
            throw new ArgumentNullException("Sys SKU is not exist.(" + sys_sku.ToString() + ")");
    }
    #endregion

    /// <summary>
    /// 
    /// </summary>
    /// <param name="system_sku"></param>
    /// <returns></returns>
    public int GetCategoryID(int system_sku)
    {
        DataTable dt = Config.ExecuteDataTable("select category_id from tb_ebay_system where id='" + system_sku.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            return int.Parse(dt.Rows[0][0].ToString());
        }
        return 0;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="isSystem"></param>
    /// <param name="sku"></param>
    /// <returns></returns>
    public string GetProdCustomLabel(nicklu2Entities context, bool isSystem, int sku)
    {
        if (!isSystem)
        {
            DataTable dt = Config.ExecuteDataTable(@"select ebay_comment from tb_ebay_part_comment where part_sku='"+ sku.ToString()+"'");
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString() ;//+ " (" + sku.ToString() + ")";
            else
            {
                var pm = ProductModel.GetProductModel(context, sku);
                return pm.producter_serial_no;// +" (" + sku.ToString() + ")";
            }
        }
        else
        {
            DataTable dt = Config.ExecuteDataTable(string.Format(@"select case when length(cutom_label)>2 then cutom_label 
else  ebay_system_name end as cutom_label from tb_ebay_system where id='{0}'", sku));
            if (dt.Rows.Count > 0)
                return dt.Rows[0][0].ToString() + " (" + sku.ToString() + ")";
            else
                return sku.ToString();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="category_id"></param>
    /// <returns></returns>
    public int GetTempleteID(int category_id)
    {
        DataTable dt = Config.ExecuteDataTable("Select templete_id from tb_ebay_templete_and_category where sys_category_id='" + category_id.ToString() + "'");
        if (dt.Rows.Count > 0)
        {
            return int.Parse(dt.Rows[0][0].ToString());
        }
        return Default_TEMPLETE_ID;
    }


    /// <summary>
    /// 取得return policy  字符串
    /// xml格式
    /// 
    /// 除去  <RefundOption>MoneyBack</RefundOption> 因为只有美国有效。 （2020.09.12）
    /// </summary>
    /// <param name="luc_sku"></param>
    /// <returns></returns>
    public static string GetReturnPolicy(int luc_sku)
    {
        DataTable dt = Config.ExecuteDataTable("select pc.is_noebook from tb_product p inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no where p.product_serial_no='" + luc_sku + "'");
        if (dt.Rows.Count == 1)
        {
            if (dt.Rows[0][0].ToString() == "1")
            {
                return @"<ReturnPolicy>
            <ReturnsAcceptedOption>ReturnsAccepted</ReturnsAcceptedOption>     
          <ReturnsWithinOption>Days_30</ReturnsWithinOption>
          <Description>Box not open, seal not broken, 15% restocking charge, shipping fees not refundable.</Description>
          <ShippingCostPaidByOption>Buyer</ShippingCostPaidByOption>
        </ReturnPolicy>";
            }
           
        }
         return @"<ReturnPolicy>
            <ReturnsAcceptedOption>ReturnsAccepted</ReturnsAcceptedOption>      
          <ReturnsWithinOption>Days_30</ReturnsWithinOption>
          <Description> 15% restocking charge will be applied, shipping fees not refundable.</Description>
          <ShippingCostPaidByOption>Buyer</ShippingCostPaidByOption>
        </ReturnPolicy>";
    }

    /// <summary>
    /// 验证 part 是否已发布
    /// system  的验证没有做
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="isSystem"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    public static bool ValidateItemIssue(int sku, bool isSystem)
    {
        bool result = false;
        if (!isSystem)
        {
            if (Config.ExecuteScalarInt32(string.Format(@"select count(ec.id) c from tb_ebay_code_and_luc_sku  ec
 inner join tb_product p on p.product_serial_no=ec.sku and ec.is_sys=0 
where ec.is_online=1 and ec.sku='{0}'", sku)) >0 )
                return true;
            else
                return false;
        }

        return result;
    }

    /// <summary>
    /// 验证item 的 ebay title 是否可用
    /// 
    /// </summary>
    /// <param name="sku"></param>
    /// <param name="isSystem"></param>
    /// <returns></returns>
    public static bool ValidateItemTitle(int sku, bool isSystem, string title)
    {
        bool result = false;
        if (!isSystem)
        {
            if (Config.ExecuteScalarInt32(string.Format(@"select count(product_serial_no) from tb_product where product_ebay_name = '{1}' and product_serial_no<>'{0}'  ", sku, title.Trim())) == 0)
                return true;
            else
                return false;
        }

        return result;
    }
}
