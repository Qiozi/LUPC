
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	06/05/2009 7:30:12 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_cart_temp")]
[Serializable]
public class CartTempModel : ActiveRecordBase<CartTempModel>
{

    public CartTempModel()
    {

    }

    public static CartTempModel GetCartTempModel(int _cart_temp_serial_no)
    {
        CartTempModel[] models = CartTempModel.FindAllByProperty("cart_temp_serial_no", _cart_temp_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new CartTempModel();
    }

    int _cart_temp_serial_no;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int cart_temp_serial_no
    {
        get { return _cart_temp_serial_no; }
        set { _cart_temp_serial_no = value; }
    }

    int _cart_temp_code;

    [Property]
    public int cart_temp_code
    {
        get { return _cart_temp_code; }
        set { _cart_temp_code = value; }
    }

    int _product_serial_no;

    [Property]
    public int product_serial_no
    {
        get { return _product_serial_no; }
        set { _product_serial_no = value; }
    }

    int _menu_child_serial_no;

    [Property]
    public int menu_child_serial_no
    {
        get { return _menu_child_serial_no; }
        set { _menu_child_serial_no = value; }
    }

    DateTime _create_datetime;

    [Property]
    public DateTime create_datetime
    {
        get { return _create_datetime; }
        set { _create_datetime = value; }
    }

    string _ip;

    [Property]
    public string ip
    {
        get { return _ip; }
        set { _ip = value; }
    }

    string _customer;

    [Property]
    public string customer
    {
        get { return _customer; }
        set { _customer = value; }
    }

    int _cart_temp_Quantity;

    [Property]
    public int cart_temp_Quantity
    {
        get { return _cart_temp_Quantity; }
        set { _cart_temp_Quantity = value; }
    }

    int _customer_serial_no;

    [Property]
    public int customer_serial_no
    {
        get { return _customer_serial_no; }
        set { _customer_serial_no = value; }
    }

    int _shipping_company;

    [Property]
    public int shipping_company
    {
        get { return _shipping_company; }
        set { _shipping_company = value; }
    }

    int _state_shipping;

    [Property]
    public int state_shipping
    {
        get { return _state_shipping; }
        set { _state_shipping = value; }
    }

    decimal _shipping_charge;

    [Property]
    public decimal shipping_charge
    {
        get { return _shipping_charge; }
        set { _shipping_charge = value; }
    }

    decimal _sale_tax;

    [Property]
    public decimal sale_tax
    {
        get { return _sale_tax; }
        set { _sale_tax = value; }
    }

    decimal _price;

    [Property]
    public decimal price
    {
        get { return _price; }
        set { _price = value; }
    }

    int _is_noebook;

    [Property]
    public int is_noebook
    {
        get { return _is_noebook; }
        set { _is_noebook = value; }
    }

    string _product_name;

    [Property]
    public string product_name
    {
        get { return _product_name; }
        set { _product_name = value; }
    }

    decimal _old_price;

    [Property]
    public decimal old_price
    {
        get { return _old_price; }
        set { _old_price = value; }
    }

    int _country_id;

    [Property]
    public int country_id
    {
        get { return _country_id; }
        set { _country_id = value; }
    }

    int _pay_method;

    [Property]
    public int pay_method
    {
        get { return _pay_method; }
        set { _pay_method = value; }
    }

    DateTime _pick_datetime_1;

    [Property]
    public DateTime pick_datetime_1
    {
        get { return _pick_datetime_1; }
        set { _pick_datetime_1 = value; }
    }

    DateTime _pick_datetime_2;

    [Property]
    public DateTime pick_datetime_2
    {
        get { return _pick_datetime_2; }
        set { _pick_datetime_2 = value; }
    }

    decimal _save_price;

    [Property]
    public decimal save_price
    {
        get { return _save_price; }
        set { _save_price = value; }
    }

    decimal _price_rate;

    [Property]
    public decimal price_rate
    {
        get { return _price_rate; }
        set { _price_rate = value; }
    }

    decimal _cost;

    [Property]
    public decimal cost
    {
        get { return _cost; }
        set { _cost = value; }
    }

    string _shipping_state_code;

    [Property]
    public string shipping_state_code
    {
        get { return _shipping_state_code; }
        set { _shipping_state_code = value; }
    }
    string _shipping_country_code;

    [Property]
    public string shipping_country_code
    {
        get { return _shipping_country_code; }
        set { _shipping_country_code = value; }
    }

    string _price_unit;

    [Property]
    public string price_unit
    {
        get { return _price_unit; }
        set { _price_unit = value; }
    }

    int _current_system;

    [Property]
    public int current_system
    {
        get { return _current_system; }
        set { _current_system = value; }
    }




    public static CartTempModel[] GetModelsByTmeCode(int tmp_code)
    {
        return CartTempModel.FindAllByProperty("cart_temp_code", tmp_code);
    }

    public static DataTable GetModelsDTByTmeCode(int tmp_code)
    {
        return Config.ExecuteDataTable("select * from tb_cart_temp where cart_temp_code='" + tmp_code + "' ");
    }
}
