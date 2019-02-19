using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckOutPickup : PageBase
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsLogin)
            {
                Response.Redirect("/", true);
            }

            #region 界面数据
            var cusomerSerial = int.Parse(CustomerSerialNo);
            var customer = db.tb_customer.FirstOrDefault(p => p.customer_serial_no.HasValue
                && p.customer_serial_no.Value.Equals(cusomerSerial));
            if (customer != null)
            {
                CardType = customer.customer_card_type;
                CardNumber = customer.customer_credit_card;
                CardExpiryMonth = customer.customer_expiry.Trim().Length ==6? customer.customer_expiry.Trim().Substring(0, 2):"";
                CardExpiryYear = customer.customer_expiry.Trim().Length == 6 ? customer.customer_expiry.Trim().Substring( 2) : "";
                CardIssuer = customer.customer_card_issuer;
                CardIssuerTelephone = customer.customer_card_phone;
                CardFirstName = customer.customer_card_first_name;
                CardLastName = customer.customer_card_last_name;
                CardBillingShippingAddress = customer.customer_card_billing_shipping_address;
                CardCity = customer.customer_card_city;
                CardState = customer.customer_card_state_code;
                ZipCode = customer.customer_card_zip_code;
                Email1 = customer.customer_email1;
                BusinessPhone = customer.phone_d;
                HomePhone = customer.phone_n;

            }

            #endregion

            #region state code
            var stateList = db.tb_state_shipping.Where(p => p.system_category_serial_no.HasValue
    && p.system_category_serial_no.Value.Equals(1))
    .OrderBy(p => p.priority).ToList();
            var statString = "";
            foreach (var state in stateList)
            {
                statString += "<option value='" + state.state_serial_no + "' ";
                if (state.state_code == CardState)
                    statString += " selected='true' ";
                statString += " >" + state.state_name + "</option>";
            }
            stateOption.Text = statString;
            #endregion
        }
    }

    public string CardType { get; set; }
    public string CardNumber { get; set; }
    public string CardExpiryMonth { get; set; }
    public string CardExpiryYear { get; set; }
    public string CardIssuer { get; set; }
    public string CardIssuerTelephone { get; set; }
    public string CardFirstName { get; set; }
    public string CardLastName { get; set; }
    public string CardBillingShippingAddress { get; set; }
    public string CardCity { get; set; }
    public string CardState { get; set; }
    public string ZipCode { set; get; }
    public string Email1 { get; set; }
    public string BusinessPhone { get; set; }
    public string HomePhone { get; set; }

    public int ReqShippingID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "shippingid", 0); }
    }

    public int ReqStateID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "stateid", 0); }
    }

}