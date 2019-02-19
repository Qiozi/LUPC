// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-3-8 0:07:11
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;



/// <summary>
/// Summary description for OrderPriceHistoryModel
/// </summary>
[ActiveRecord("tb_order_price_history")]
[Serializable]
public class OrderPriceHistoryModel : ActiveRecordBase<OrderPriceHistoryModel>
{
	public OrderPriceHistoryModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    int _price_history_serial_no;
    int _price_history_ship_method_id;
    decimal _price_history_ship_charge;
    int _price_history_sales_tax_rate;
    decimal _price_history_sales_tax_charge;
    decimal _price_history_sub_total;
    decimal _price_history_special_cash_discount;
    decimal _price_history_on_sale_discount;
    decimal _price_history_rebate_total;
    decimal _price_history_taxable_total;
    decimal _price_history_grand_total;
    int _order_code;
    DateTime _create_datetime;


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int price_history_serial_no
    {
        get { return _price_history_serial_no; }
        set { _price_history_serial_no = value; }
    }
    public static OrderPriceHistoryModel GetOrderPriceHistoryModel(int _price_history_serial_no)
    {
        OrderPriceHistoryModel[] models = OrderPriceHistoryModel.FindAllByProperty("price_history_serial_no", _price_history_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new OrderPriceHistoryModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int price_history_ship_method_id
    {
        get { return _price_history_ship_method_id; }
        set { _price_history_ship_method_id = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal price_history_ship_charge
    {
        get { return _price_history_ship_charge; }
        set { _price_history_ship_charge = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int price_history_sales_tax_rate
    {
        get { return _price_history_sales_tax_rate; }
        set { _price_history_sales_tax_rate = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal price_history_sales_tax_charge
    {
        get { return _price_history_sales_tax_charge; }
        set { _price_history_sales_tax_charge = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal price_history_sub_total
    {
        get { return _price_history_sub_total; }
        set { _price_history_sub_total = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal price_history_special_cash_discount
    {
        get { return _price_history_special_cash_discount; }
        set { _price_history_special_cash_discount = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal price_history_on_sale_discount
    {
        get { return _price_history_on_sale_discount; }
        set { _price_history_on_sale_discount = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal price_history_rebate_total
    {
        get { return _price_history_rebate_total; }
        set { _price_history_rebate_total = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal price_history_taxable_total
    {
        get { return _price_history_taxable_total; }
        set { _price_history_taxable_total = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal price_history_grand_total
    {
        get { return _price_history_grand_total; }
        set { _price_history_grand_total = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int order_code
    {
        get { return _order_code; }
        set { _order_code = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime create_datetime
    {
        get { return _create_datetime; }
        set { _create_datetime = value; }
    }

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
