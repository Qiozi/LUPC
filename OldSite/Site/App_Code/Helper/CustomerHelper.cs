using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using LU.Data;

/// <summary>
/// Summary description for CustomerHelper
/// </summary>
public class CustomerHelper
{
    LU.Data.nicklu2Entities _context;
    public CustomerHelper(nicklu2Entities context)
    {
        //
        // TODO: Add constructor logic here
        //
        _context = context;

    }


    public void CopyCustomer(string order_code, int customer_id)
    {
        var cm = CustomerModel.GetCustomerModel(_context, customer_id);

        var csm = new tb_customer_store();
        csm.create_datetime = DateTime.Now;
        csm.busniess_website = cm.busniess_website;
        csm.create_datetime = cm.create_datetime;
        csm.customer_address1 = cm.customer_address1;
        csm.customer_business_telephone = cm.customer_business_telephone;
        csm.customer_card_billing_shipping_address = cm.customer_card_billing_shipping_address;
        csm.customer_card_city = cm.customer_card_city;
        csm.customer_card_country = cm.customer_card_country;
        csm.customer_card_issuer = cm.customer_card_issuer;
        csm.customer_card_phone = cm.customer_card_phone;
        csm.customer_card_state = cm.customer_card_state;
        csm.customer_card_type = cm.customer_card_type;
        csm.customer_card_zip_code = cm.customer_card_zip_code;
        csm.customer_city = cm.customer_city;
        csm.customer_comment_note = cm.customer_comment_note;
        csm.customer_company = cm.customer_company;
        csm.customer_country = cm.customer_country;
        csm.customer_credit_card = cm.customer_credit_card;
        csm.customer_email1 = cm.customer_email1;
        csm.customer_email2 = cm.customer_email2;
        csm.customer_expiry = cm.customer_expiry;
        csm.customer_fax = cm.customer_fax;
        csm.customer_first_name = cm.customer_first_name;
        csm.customer_last_name = cm.customer_last_name;
        csm.customer_login_name = cm.customer_login_name;
        csm.customer_note = cm.customer_note;
        csm.customer_password = cm.customer_password;
        csm.customer_rumor = cm.customer_rumor;
        csm.customer_serial_no = cm.customer_serial_no;
        csm.customer_shipping_address = cm.customer_shipping_address;
        csm.customer_shipping_city = cm.customer_shipping_city;
        csm.customer_shipping_country = cm.customer_shipping_country;
        csm.customer_shipping_first_name = cm.customer_shipping_first_name;
        csm.customer_shipping_last_name = cm.customer_shipping_last_name;
        csm.customer_shipping_state = cm.customer_shipping_state;
        csm.customer_shipping_zip_code = cm.customer_shipping_zip_code;
        csm.EBay_ID = cm.EBay_ID;
        csm.my_purchase_order = cm.my_purchase_order;
        csm.news_latter_subscribe = sbyte.Parse(cm.news_latter_subscribe.ToString());
        int oc;
        int.TryParse(order_code, out oc);
        csm.order_code = oc;
        csm.pay_method = cm.pay_method;
        csm.phone_c = cm.phone_c;
        csm.phone_d = cm.phone_d;
        csm.phone_n = cm.phone_n;
        csm.state_serial_no = cm.state_serial_no;
        csm.store_create_datetime = DateTime.Now;
        csm.system_category_serial_no = cm.system_category_serial_no;
        csm.tag = sbyte.Parse(cm.tag.ToString());
        csm.tax_execmtion = cm.tax_execmtion;
        csm.zip_code = cm.zip_code;
        csm.source = cm.source;
        csm.card_verification_number = cm.card_verification_number;
        csm.customer_business_address = cm.customer_business_address;
        csm.customer_business_city = cm.customer_business_city;
        csm.customer_business_country_code = cm.customer_business_country_code;
        csm.customer_business_state_code = cm.customer_business_state_code;
        csm.customer_business_zip_code = cm.customer_business_zip_code;
        csm.customer_card_country_code = cm.customer_card_country_code;
        csm.customer_card_state_code = cm.customer_card_state_code;
        csm.customer_card_first_name = cm.customer_card_first_name;
        csm.customer_card_last_name = cm.customer_card_last_name;
        csm.customer_country_code = cm.customer_country_code;
        csm.customer_card_state_code = cm.customer_card_state_code;
        csm.shipping_country_code = cm.shipping_country_code;
        csm.shipping_state_code = cm.shipping_state_code;
        csm.state_code = cm.state_code;
        csm.is_all_tax_execmtion = cm.is_all_tax_execmtion;
        // csm.Create();

        _context.tb_customer_store.Add(csm);
        _context.SaveChanges();
    }

}
