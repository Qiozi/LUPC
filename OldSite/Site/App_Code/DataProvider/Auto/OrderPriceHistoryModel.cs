// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-3-8 0:07:11
//
// // // // // // // // // // // // // // // //
using System;
using System.Data;



/// <summary>
/// Summary description for OrderPriceHistoryModel
/// </summary>

[Serializable]
public class OrderPriceHistoryModel 
{
	
    public static DataTable FindModelsByHistory(int order_code)
    {
        string sql = @"select (select shipping_company_name from tb_shipping_company where shipping_company_id=oh.shipping_company) ship_method, shipping_charge ship_charge, tax_rate sales_tax_rate, tax_charge sales_tax, sub_total sub_total, 
sur_charge special_cash_discount, '' on_sale_discount, '' rebate_total
, taxable_total taxable_total, grand_total, create_datetime from tb_order_helper oh where order_code='" + order_code.ToString() + @"'
union all 
select 	(select shipping_company_name from tb_shipping_company where shipping_company_id=oph.price_history_ship_method_id) ship_method, price_history_ship_charge, 
	price_history_sales_tax_rate, 
	price_history_sales_tax_charge, 
	price_history_sub_total, 
	price_history_special_cash_discount, 
	price_history_on_sale_discount, 
	price_history_rebate_total, 
	price_history_taxable_total, 
	price_history_grand_total, 
	create_datetime
	 
	from 
	tb_order_price_history oph where order_code='" + order_code.ToString() + "'";
        return Config.ExecuteDataTable(sql);
    }
}
