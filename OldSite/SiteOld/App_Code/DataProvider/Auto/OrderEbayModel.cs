
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	27/03/2009 3:05:01 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_order_ebay")]
[Serializable]
public class OrderEbayModel : ActiveRecordBase<OrderEbayModel>
{

    public OrderEbayModel()
    {

    }

    public static OrderEbayModel GetOrderEbayModel(int _id)
    {
        OrderEbayModel[] models = OrderEbayModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new OrderEbayModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _sales_record_number;

    [Property]
    public int sales_record_number
    {
        get { return _sales_record_number; }
        set { _sales_record_number = value; }
    }

    string _user_id;

    [Property]
    public string user_id
    {
        get { return _user_id; }
        set { _user_id = value; }
    }

    string _buyer_phone_number;

    [Property]
    public string buyer_phone_number
    {
        get { return _buyer_phone_number; }
        set { _buyer_phone_number = value; }
    }

    string _buyer_email;

    [Property]
    public string buyer_email
    {
        get { return _buyer_email; }
        set { _buyer_email = value; }
    }

    string _buyer_address1;

    [Property]
    public string buyer_address1
    {
        get { return _buyer_address1; }
        set { _buyer_address1 = value; }
    }

    string _buyer_address2;

    [Property]
    public string buyer_address2
    {
        get { return _buyer_address2; }
        set { _buyer_address2 = value; }
    }

    string _buyer_city;

    [Property]
    public string buyer_city
    {
        get { return _buyer_city; }
        set { _buyer_city = value; }
    }

    string _buyer_province;

    [Property]
    public string buyer_province
    {
        get { return _buyer_province; }
        set { _buyer_province = value; }
    }

    string _buyer_postal_code;

    [Property]
    public string buyer_postal_code
    {
        get { return _buyer_postal_code; }
        set { _buyer_postal_code = value; }
    }

    string _buyer_country;

    [Property]
    public string buyer_country
    {
        get { return _buyer_country; }
        set { _buyer_country = value; }
    }

    string _item_number;

    [Property]
    public string item_number
    {
        get { return _item_number; }
        set { _item_number = value; }
    }

    string _item_title;

    [Property]
    public string item_title
    {
        get { return _item_title; }
        set { _item_title = value; }
    }

    string _custom_label;

    [Property]
    public string custom_label
    {
        get { return _custom_label; }
        set { _custom_label = value; }
    }

    int _quantity;

    [Property]
    public int quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    decimal _sale_price;

    [Property]
    public decimal sale_price
    {
        get { return _sale_price; }
        set { _sale_price = value; }
    }

    string _sale_price_unit;

    [Property]
    public string sale_price_unit
    {
        get { return _sale_price_unit; }
        set { _sale_price_unit = value; }
    }

    decimal _shipping_and_handling;

    [Property]
    public decimal shipping_and_handling
    {
        get { return _shipping_and_handling; }
        set { _shipping_and_handling = value; }
    }

    string _shipping_and_handling_unit;

    [Property]
    public string shipping_and_handling_unit
    {
        get { return _shipping_and_handling_unit; }
        set { _shipping_and_handling_unit = value; }
    }

    decimal _insurance;

    [Property]
    public decimal insurance
    {
        get { return _insurance; }
        set { _insurance = value; }
    }

    string _insurance_unit;

    [Property]
    public string insurance_unit
    {
        get { return _insurance_unit; }
        set { _insurance_unit = value; }
    }

    decimal _cash_on_delivery_fee;

    [Property]
    public decimal cash_on_delivery_fee
    {
        get { return _cash_on_delivery_fee; }
        set { _cash_on_delivery_fee = value; }
    }

    string _cash_on_delivery_fee_unit;

    [Property]
    public string cash_on_delivery_fee_unit
    {
        get { return _cash_on_delivery_fee_unit; }
        set { _cash_on_delivery_fee_unit = value; }
    }

    decimal _total_price;

    [Property]
    public decimal total_price
    {
        get { return _total_price; }
        set { _total_price = value; }
    }

    string _total_price_unit;

    [Property]
    public string total_price_unit
    {
        get { return _total_price_unit; }
        set { _total_price_unit = value; }
    }

    string _payment_method;

    [Property]
    public string payment_method
    {
        get { return _payment_method; }
        set { _payment_method = value; }
    }

    DateTime _sale_date;

    [Property]
    public DateTime sale_date
    {
        get { return _sale_date; }
        set { _sale_date = value; }
    }

    DateTime _checkout_date;

    [Property]
    public DateTime checkout_date
    {
        get { return _checkout_date; }
        set { _checkout_date = value; }
    }

    DateTime _paid_on_date;

    [Property]
    public DateTime paid_on_date
    {
        get { return _paid_on_date; }
        set { _paid_on_date = value; }
    }

    DateTime _shipped_on_date;

    [Property]
    public DateTime shipped_on_date
    {
        get { return _shipped_on_date; }
        set { _shipped_on_date = value; }
    }

    string _feedback_left;

    [Property]
    public string feedback_left
    {
        get { return _feedback_left; }
        set { _feedback_left = value; }
    }

    string _feedback_received;

    [Property]
    public string feedback_received
    {
        get { return _feedback_received; }
        set { _feedback_received = value; }
    }

    string _notes_to_yourself;

    [Property]
    public string notes_to_yourself
    {
        get { return _notes_to_yourself; }
        set { _notes_to_yourself = value; }
    }

    string _paypal_transaction_id;

    [Property]
    public string paypal_transaction_id
    {
        get { return _paypal_transaction_id; }
        set { _paypal_transaction_id = value; }
    }

    string _shipping_service;

    [Property]
    public string shipping_service
    {
        get { return _shipping_service; }
        set { _shipping_service = value; }
    }

    string _cash_on_delivery_option;

    [Property]
    public string cash_on_delivery_option
    {
        get { return _cash_on_delivery_option; }
        set { _cash_on_delivery_option = value; }
    }

    string _transaction_id;

    [Property]
    public string transaction_id
    {
        get { return _transaction_id; }
        set { _transaction_id = value; }
    }

    string _order_id;

    [Property]
    public string order_id
    {
        get { return _order_id; }
        set { _order_id = value; }
    }

    int _order_code;

    [Property]
    public int order_code
    {
        get { return _order_code; }
        set { _order_code = value; }
    }

    string _buyer_fullname;
    [Property]
    public string buyer_fullname
    {
        get { return _buyer_fullname; }
        set { _buyer_fullname = value; }
    }


    public OrderEbayModel[] FindModelsByOrderCodeIsNULL()
    {
        NHibernate.Expression.LtExpression lt = new NHibernate.Expression.LtExpression("order_code", 1);

        return OrderEbayModel.FindAll(lt);
    }
}

