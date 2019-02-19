// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-4 23:45:26
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;


[ActiveRecord("tb_order_product_history")]
[Serializable]
public class OrderProductHistoryModel : ActiveRecordBase<OrderProductHistoryModel>
{
    int _serial_no;
    string _order_code;
    int _product_serial_no;
    int _order_product_sum;
    decimal _order_product_price;
    byte _tag;
    decimal _order_product_cost;
    string _sku;
    int _menu_pre_serial_no;
    int _menu_child_serial_no;
    int _product_type;
    string _product_name;
    decimal _order_product_sold;
    decimal _product_current_price_rate;
    string _product_type_name;
    DateTime _create_datetime;
    bool _add_del;

    public OrderProductHistoryModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int serial_no
    {
        get { return _serial_no; }
        set { _serial_no = value; }
    }
    public static OrderProductHistoryModel GetOrderProductModel(int _serial_no)
    {
        OrderProductHistoryModel[] models = OrderProductHistoryModel.FindAllByProperty("serial_no", _serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new OrderProductHistoryModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string order_code
    {
        get { return _order_code; }
        set { _order_code = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_serial_no
    {
        get { return _product_serial_no; }
        set { _product_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int order_product_sum
    {
        get { return _order_product_sum; }
        set { _order_product_sum = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal order_product_price
    {
        get { return _order_product_price; }
        set { _order_product_price = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public byte tag
    {
        get { return _tag; }
        set { _tag = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal order_product_cost
    {
        get { return _order_product_cost; }
        set { _order_product_cost = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string sku
    {
        get { return _sku; }
        set { _sku = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int menu_pre_serial_no
    {
        get { return _menu_pre_serial_no; }
        set { _menu_pre_serial_no = value; }
    }
    /// <summary>
    ///
    /// </summary>
    [Property]
    public int menu_child_serial_no
    {
        get { return _menu_child_serial_no; }
        set { _menu_child_serial_no = value; }
    }

    [Property]
    public int product_type
    {
        get { return _product_type; }
        set { _product_type = value; }
    }

    [Property]
    public string product_name
    {
        get { return _product_name; }
        set { _product_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal order_product_sold
    {
        get { return _order_product_sold; }
        set { _order_product_sold = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal product_current_price_rate
    {
        get { return _product_current_price_rate; }
        set { _product_current_price_rate = value; }
    }
    [Property]
    public string product_type_name
    {
        get { return _product_type_name; }
        set { _product_type_name = value; }
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
    [Property]
    public bool add_del
    {
        get { return _add_del; }
        set { _add_del = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public static OrderProductHistoryModel[] FindModelsByOrder(string order_code)
    {
        return OrderProductHistoryModel.FindAllByProperty("order_code", order_code);
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public static int FindModelsCountByOrder(string order_code)
    {
        return Config.ExecuteScalarInt32(string.Format("select count(serial_no) from tb_order_product_history where order_code='{0}'", order_code));
    }
}
