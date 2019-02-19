// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-9-12 23:30:44
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Collections.Generic;
using System.Data;

[ActiveRecord("tb_customer_store")]
[Serializable]
public class CustomerStoreModel : ActiveRecordBase<CustomerStoreModel>
{
    public CustomerStoreModel()
    {

    }

    public static CustomerStoreModel GetCustomerStoreModel(int _serial_no)
    {
        CustomerStoreModel[] models = CustomerStoreModel.FindAllByProperty("serial_no", _serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new CustomerStoreModel();
    }

    string _zip_code;

    [Property]
    public string zip_code
    {
        get { return _zip_code; }
        set { _zip_code = value; }
    }

    int _customer_serial_no;

    [Property]
    public int customer_serial_no
    {
        get { return _customer_serial_no; }
        set { _customer_serial_no = value; }
    }

    string _customer_login_name;

    [Property]
    public string customer_login_name
    {
        get { return _customer_login_name; }
        set { _customer_login_name = value; }
    }

    string _customer_business_country_code;

    [Property]
    public string customer_business_country_code
    {
        get { return _customer_business_country_code; }
        set { _customer_business_country_code = value; }
    }

    string _customer_business_state_code;

    [Property]
    public string customer_business_state_code
    {
        get { return _customer_business_state_code; }
        set { _customer_business_state_code = value; }
    }

    string _customer_business_city;

    [Property]
    public string customer_business_city
    {
        get { return _customer_business_city; }
        set { _customer_business_city = value; }
    }

    string _customer_business_telephone;

    [Property]
    public string customer_business_telephone
    {
        get { return _customer_business_telephone; }
        set { _customer_business_telephone = value; }
    }

    string _customer_business_zip_code;

    [Property]
    public string customer_business_zip_code
    {
        get { return _customer_business_zip_code; }
        set { _customer_business_zip_code = value; }
    }

    string _customer_business_address;

    [Property]
    public string customer_business_address
    {
        get { return _customer_business_address; }
        set { _customer_business_address = value; }
    }

    string _phone_d;

    [Property]
    public string phone_d
    {
        get { return _phone_d; }
        set { _phone_d = value; }
    }

    string _phone_n;

    [Property]
    public string phone_n
    {
        get { return _phone_n; }
        set { _phone_n = value; }
    }

    string _customer_address1;

    [Property]
    public string customer_address1
    {
        get { return _customer_address1; }
        set { _customer_address1 = value; }
    }

    string _customer_city;

    [Property]
    public string customer_city
    {
        get { return _customer_city; }
        set { _customer_city = value; }
    }

    string _customer_country_code;

    [Property]
    public string customer_country_code
    {
        get { return _customer_country_code; }
        set { _customer_country_code = value; }
    }

    string _customer_country;

    [Property]
    public string customer_country
    {
        get { return _customer_country; }
        set { _customer_country = value; }
    }

    string _customer_email1;

    [Property]
    public string customer_email1
    {
        get { return _customer_email1; }
        set { _customer_email1 = value; }
    }

    string _customer_email2;

    [Property]
    public string customer_email2
    {
        get { return _customer_email2; }
        set { _customer_email2 = value; }
    }

    string _customer_credit_card;

    [Property]
    public string customer_credit_card
    {
        get { return _customer_credit_card; }
        set { _customer_credit_card = value; }
    }

    string _customer_expiry;

    [Property]
    public string customer_expiry
    {
        get { return _customer_expiry; }
        set { _customer_expiry = value; }
    }

    string _customer_company;

    [Property]
    public string customer_company
    {
        get { return _customer_company; }
        set { _customer_company = value; }
    }

    string _customer_fax;

    [Property]
    public string customer_fax
    {
        get { return _customer_fax; }
        set { _customer_fax = value; }
    }

    string _customer_note;

    [Property]
    public string customer_note
    {
        get { return _customer_note; }
        set { _customer_note = value; }
    }

    string _customer_password;

    [Property]
    public string customer_password
    {
        get { return _customer_password; }
        set { _customer_password = value; }
    }

    int _state_serial_no;

    [Property]
    public int state_serial_no
    {
        get { return _state_serial_no; }
        set { _state_serial_no = value; }
    }

    string _state_code;

    [Property]
    public string state_code
    {
        get { return _state_code; }
        set { _state_code = value; }
    }

    int _tag;

    [Property]
    public int tag
    {
        get { return _tag; }
        set { _tag = value; }
    }

    int _system_category_serial_no;

    [Property]
    public int system_category_serial_no
    {
        get { return _system_category_serial_no; }
        set { _system_category_serial_no = value; }
    }

    string _customer_first_name;

    [Property]
    public string customer_first_name
    {
        get { return _customer_first_name; }
        set { _customer_first_name = value; }
    }

    string _customer_card_first_name;

    [Property]
    public string customer_card_first_name
    {
        get { return _customer_card_first_name; }
        set { _customer_card_first_name = value; }
    }

    string _customer_last_name;

    [Property]
    public string customer_last_name
    {
        get { return _customer_last_name; }
        set { _customer_last_name = value; }
    }

    string _customer_card_last_name;

    [Property]
    public string customer_card_last_name
    {
        get { return _customer_card_last_name; }
        set { _customer_card_last_name = value; }
    }

    string _customer_rumor;

    [Property]
    public string customer_rumor
    {
        get { return _customer_rumor; }
        set { _customer_rumor = value; }
    }

    string _customer_card_type;

    [Property]
    public string customer_card_type
    {
        get { return _customer_card_type; }
        set { _customer_card_type = value; }
    }

    string _customer_card_phone;

    [Property]
    public string customer_card_phone
    {
        get { return _customer_card_phone; }
        set { _customer_card_phone = value; }
    }

    string _EBay_ID;

    [Property]
    public string EBay_ID
    {
        get { return _EBay_ID; }
        set { _EBay_ID = value; }
    }

    int _news_latter_subscribe;

    [Property]
    public int news_latter_subscribe
    {
        get { return _news_latter_subscribe; }
        set { _news_latter_subscribe = value; }
    }

    DateTime _create_datetime;

    [Property]
    public DateTime create_datetime
    {
        get { return _create_datetime; }
        set { _create_datetime = value; }
    }

    string _customer_card_issuer;

    [Property]
    public string customer_card_issuer
    {
        get { return _customer_card_issuer; }
        set { _customer_card_issuer = value; }
    }

    string _customer_card_billing_shipping_address;

    [Property]
    public string customer_card_billing_shipping_address
    {
        get { return _customer_card_billing_shipping_address; }
        set { _customer_card_billing_shipping_address = value; }
    }

    string _customer_card_city;

    [Property]
    public string customer_card_city
    {
        get { return _customer_card_city; }
        set { _customer_card_city = value; }
    }

    string _customer_card_state_code;

    [Property]
    public string customer_card_state_code
    {
        get { return _customer_card_state_code; }
        set { _customer_card_state_code = value; }
    }

    int _customer_card_state;

    [Property]
    public int customer_card_state
    {
        get { return _customer_card_state; }
        set { _customer_card_state = value; }
    }

    string _customer_card_zip_code;

    [Property]
    public string customer_card_zip_code
    {
        get { return _customer_card_zip_code; }
        set { _customer_card_zip_code = value; }
    }

    int _pay_method;

    [Property]
    public int pay_method
    {
        get { return _pay_method; }
        set { _pay_method = value; }
    }

    string _customer_shipping_city;

    [Property]
    public string customer_shipping_city
    {
        get { return _customer_shipping_city; }
        set { _customer_shipping_city = value; }
    }

    int _customer_shipping_state;

    [Property]
    public int customer_shipping_state
    {
        get { return _customer_shipping_state; }
        set { _customer_shipping_state = value; }
    }

    string _customer_shipping_address;

    [Property]
    public string customer_shipping_address
    {
        get { return _customer_shipping_address; }
        set { _customer_shipping_address = value; }
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

    string _customer_card_country_code;

    [Property]
    public string customer_card_country_code
    {
        get { return _customer_card_country_code; }
        set { _customer_card_country_code = value; }
    }

    int _customer_card_country;

    [Property]
    public int customer_card_country
    {
        get { return _customer_card_country; }
        set { _customer_card_country = value; }
    }

    string _my_purchase_order;

    [Property]
    public string my_purchase_order
    {
        get { return _my_purchase_order; }
        set { _my_purchase_order = value; }
    }

    string _customer_shipping_first_name;

    [Property]
    public string customer_shipping_first_name
    {
        get { return _customer_shipping_first_name; }
        set { _customer_shipping_first_name = value; }
    }

    string _customer_shipping_last_name;

    [Property]
    public string customer_shipping_last_name
    {
        get { return _customer_shipping_last_name; }
        set { _customer_shipping_last_name = value; }
    }

    int _customer_shipping_country;

    [Property]
    public int customer_shipping_country
    {
        get { return _customer_shipping_country; }
        set { _customer_shipping_country = value; }
    }

    string _customer_shipping_zip_code;

    [Property]
    public string customer_shipping_zip_code
    {
        get { return _customer_shipping_zip_code; }
        set { _customer_shipping_zip_code = value; }
    }

    string _tax_execmtion;

    [Property]
    public string tax_execmtion
    {
        get { return _tax_execmtion; }
        set { _tax_execmtion = value; }
    }

    string _busniess_website;

    [Property]
    public string busniess_website
    {
        get { return _busniess_website; }
        set { _busniess_website = value; }
    }

    string _phone_c;

    [Property]
    public string phone_c
    {
        get { return _phone_c; }
        set { _phone_c = value; }
    }

    string _customer_comment_note;

    [Property]
    public string customer_comment_note
    {
        get { return _customer_comment_note; }
        set { _customer_comment_note = value; }
    }

    int _serial_no;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int serial_no
    {
        get { return _serial_no; }
        set { _serial_no = value; }
    }

    int _order_code;

    [Property]
    public int order_code
    {
        get { return _order_code; }
        set { _order_code = value; }
    }

    DateTime _store_create_datetime;

    [Property]
    public DateTime store_create_datetime
    {
        get { return _store_create_datetime; }
        set { _store_create_datetime = value; }
    }

    int _is_old;

    [Property]
    public int is_old
    {
        get { return _is_old; }
        set { _is_old = value; }
    }

    string _card_verification_number;

    [Property]
    public string card_verification_number
    {
        get { return _card_verification_number; }
        set { _card_verification_number = value; }
    }

    int _source;

    [Property]
    public int source
    {
        get { return _source; }
        set { _source = value; }
    }

    bool _is_all_tax_execmtion;
    [Property]
    public bool is_all_tax_execmtion
    {
        get { return _is_all_tax_execmtion; }
        set { _is_all_tax_execmtion = value; }
    }

    public static CustomerStoreModel[] FindModelsByOrderCode(string order_code)
    {
        return CustomerStoreModel.FindAllByProperty("order_code", order_code);
    }
    /// <summary>
    /// 查找订单用户信息
    /// </summary>
    /// <param name="order_code"></param>
    /// <returns></returns>
    public static CustomerStoreModel FindByOrderCode(string order_code)
    {
        CustomerStoreModel[] ms = CustomerStoreModel.FindAllByProperty("order_code", order_code);
        if (ms != null)
            return ms[0];
        return null;
    }

    /// <summary>
    /// 取得客户所有的订单编号
    /// 
    /// </summary>
    /// <param name="customer_serial_no"></param>
    /// <returns></returns>
    public static List<KeyValuePair<string, string>> GetOrders(string customer_serial_no)
    {
        List<KeyValuePair<string, string>> orders = new List<KeyValuePair<string, string>>();
        if (string.IsNullOrEmpty(customer_serial_no))
            return orders;

        DataTable dt = Config.ExecuteDataTable("select order_code,date_format(store_create_datetime, '%Y-%d-%m') store_create_datetime from tb_customer_store where customer_serial_no='" + customer_serial_no + "'");
        foreach (DataRow dr in dt.Rows)
        {
            KeyValuePair<string, string> k = new KeyValuePair<string, string>(dr["order_code"].ToString(), dr["store_create_datetime"].ToString());
            orders.Add(k);
        }
        return orders;
    }
}
