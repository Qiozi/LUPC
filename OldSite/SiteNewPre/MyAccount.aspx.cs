using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MyAccount : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsLogin && IsLocalHostFrom)
            {
                //Response.Write(CustomerID.ToString());
                var cust = db.tb_customer.FirstOrDefault(p => p.ID.Equals(CustomerID));
                if (cust != null)
                {

                    var stateList = db.tb_state_shipping.Where(p => p.IsOtherCountry.HasValue
                        && p.IsOtherCountry.Value.Equals(false)).OrderBy(p => p.priority).ToList();
                    foreach (var s in stateList)
                    {

                    }

                    LoginName = cust.customer_login_name;
                    eBaYID = cust.EBay_ID;
                    FirstName = cust.customer_first_name;
                    LastName = cust.customer_last_name;
                    HomePhone = cust.phone_n;
                    BussinessPhone = cust.phone_d;
                    MobilePhone = cust.phone_c;
                    Country = cust.customer_country_code;
                    CountryCode = cust.customer_country_code;
                    Address = cust.customer_address1;
                    City = cust.customer_city;
                    State = cust.state_code;
                    CountryText = Country;
                    StateText = State;

                    CountryState = GetStateOptionString(cust.state_serial_no.Value, cust.state_code, stateList);
                    Zip = cust.zip_code;
                    Email1 = cust.customer_email1;
                    Email2 = cust.customer_email2;


                    ShippingAddress = cust.customer_shipping_address;
                    ShippingCity = cust.customer_shipping_city;
                    ShippingCountry = cust.shipping_country_code.ToString();
                    ShippingFirstName = cust.customer_shipping_first_name;
                    ShippingLastName = cust.customer_shipping_last_name;
                    ShippingState = cust.shipping_state_code.ToString();
                    ShippingZip = cust.customer_shipping_zip_code;
                    ShippingCountryText = ShippingCountry;
                    ShippingStateText = ShippingState;
                    ShippingCountryState = GetStateOptionString((cust.customer_shipping_state??0), cust.shipping_state_code, stateList);

                    BusinessAddress = cust.customer_business_address;
                    BusinessCity = cust.customer_business_city;
                    BusinessCompanyName = cust.customer_company;
                    BusinessCountry = cust.customer_business_country_code;
                    BusinessCountryText = BusinessCountry;
                    BusinessState = cust.customer_business_state_code;
                    BusinessStateText = BusinessState;
                    BusinessTaxExectionNumber = cust.tax_execmtion;
                    BusinessTelephone = cust.phone_d;
                    BusinessWebsiteAddress = cust.busniess_website;
                    BusinessZip = cust.customer_business_zip_code;
                    BussinessPhone = cust.phone_c;
                    BusinessCountryState = GetStateOptionString(0,cust.customer_business_state_code, stateList);
                    
                }
            }
        }
    }

    string GetStateOptionString(int StateID,string StateCode, List<nicklu2Model. tb_state_shipping> list)
    {
        string result = "<optgroup label='Other Country'><option value='0' " + (list.FirstOrDefault(p => p.state_code.Equals(StateCode) || p.state_serial_no.Equals(StateID)) != null ? "" : " selected='selected' ") + ">Other Country</option></optgroup>";
       
        var caList = list.Where(p => p.system_category_serial_no.HasValue && p.system_category_serial_no.Value.Equals(1)).ToList();
        var usList = list.Where(p => p.system_category_serial_no.HasValue && p.system_category_serial_no.Value.Equals(2)).ToList();

        result += "<optgroup label='Canada'>";
        for (int i = 0; i < caList.Count; i++)
        {
            result += "<option  value='" + caList[i].state_serial_no + "' " + (caList[i].state_serial_no == StateID ? " selected='selected' " : "") + ">" + caList[i].state_name + "</option>";
        }
        result += "<optgroup>";
        result += "<optgroup label='United States'>";
        for (int i = 0; i < usList.Count; i++)
        {
            result += "<option  value='"+usList[i].state_serial_no+"' " + (usList[i].state_serial_no == StateID ? " selected='selected' " : "") + ">" + usList[i].state_name + "</option>";
        }
        result += "<optgroup>";

        return result;
    }

    public string LoginName { get; set; }
    public string eBaYID { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string HomePhone { get; set; }
    public string BussinessPhone { get; set; }
    public string MobilePhone { get; set; }
    public string Country { get; set; }
    public string CountryState { get; set; }
    public string CountryCode { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string CountryText { get; set; }
    public string StateText { get; set; }
    public string Zip { get; set; }
    public string Email1 { get; set; }
    public string Email2 { get; set; }

    public string ShippingFirstName { get; set; }
    public string ShippingLastName { get; set; }
    public string ShippingCountry { get; set; }
    public string ShippingAddress { get; set; }
    public string ShippingCity { get; set; }
    public string ShippingState { get; set; }
    public string ShippingZip { get; set; }
    public string ShippingCountryText { get; set; }
    public string ShippingStateText { get; set; }
    public string ShippingCountryState { get; set; }

    public string BusinessCompanyName { get; set; }
    public string BusinessTelephone { get; set; }
    public string BusinessCountry { get; set; }
    public string BusinessAddress { get; set; }
    public string BusinessCountryText { get; set; }
    public string BusinessStateText { get; set; }
    public string BusinessCity { get; set; }
    public string BusinessState { get; set; }
    public string BusinessZip { get; set; }
    public string BusinessTaxExectionNumber { get; set; }
    public string BusinessWebsiteAddress { get; set; }
    public string BusinessCountryState { get; set; }
}