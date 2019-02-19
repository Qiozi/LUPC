using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckOutCreditCard : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            #region 界面数据
            var cusomerSerial = int.Parse(CustomerSerialNo);
            var customer = db.tb_customer.FirstOrDefault(p => p.customer_serial_no.HasValue
                && p.customer_serial_no.Value.Equals(cusomerSerial));
            if (customer != null)
            {

                ZipCode = customer.customer_card_zip_code;
                Email1 = customer.customer_email1;
                BusinessPhone = customer.phone_d;
                HomePhone = customer.phone_n;
                ShippingStateCode = customer.shipping_state_code;
                FirstName = customer.customer_shipping_first_name;
                LastName = customer.customer_shipping_last_name;
                Address = customer.customer_shipping_address;
                City = customer.customer_shipping_city;

                CardFirstName = customer.customer_card_first_name;
                CardLastName = customer.customer_card_last_name;
                CardBillingAddress = customer.customer_card_billing_shipping_address;
                CardCity = customer.customer_card_city;
                CardState = customer.customer_card_state_code;
                CardZipcode = customer.customer_card_zip_code;
                CardTelephone = customer.phone_d;
                CardType = customer.customer_card_type;
                CardNumber = string.Empty;
                CardExpiryDate = string.Empty;
                CardVerificationNumber = string.Empty;
                CardIssuingBank = string.Empty;
                CardIssuingTelephone = customer.customer_card_phone;
            }

            #endregion

            #region state code
            var stateList = db.tb_state_shipping.Where(p => p.system_category_serial_no.HasValue
    && p.system_category_serial_no.Value.Equals(1))
    .OrderBy(p => p.priority).ToList();
            var stateString = "";
            foreach (var state in stateList)
            {
                stateString += "<option value='" + state.state_serial_no + "' ";
                if (state.state_serial_no == ReqStateID)
                    stateString += " selected='true' ";
                stateString += " >" + state.state_name + "</option>";
            }
            stateOption.Text = stateString;
            ltState.Text = stateString;
            #endregion
        }
    }

    public string ShippingStateCode { get; set; }
    public string Email1 { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { set; get; }
    public string Email { get; set; }
    public string BusinessPhone { get; set; }
    public string HomePhone { get; set; }
    public string CustComment { get; set; }

    // card info
    public string CardFirstName { get; set; }
    public string CardLastName { get; set; }
    public string CardBillingAddress { get; set; }
    public string CardCity { get; set; }
    public string CardState { get; set; }
    public string CardZipcode { get; set; }
    public string CardTelephone { get; set; }
    public string CardType { get; set; }
    public string CardNumber { get; set; }
    public string CardExpiryDate { get; set; }
    public string CardVerificationNumber { get; set; }
    public string CardIssuingBank { get; set; }
    public string CardIssuingTelephone { get; set; }
    public string Notes { get; set; }

    public int ReqShippingID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "shippingid", 0); }
    }

    public int ReqStateID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "stateid", 0); }
    }
}