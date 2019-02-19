using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckOutBankTransfer : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {

            #region 界面数据

            var customer = CurrCustomer;

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
                ZipCode = customer.customer_shipping_zip_code;

            }

            #endregion

            #region state code
            var stateList = db.tb_state_shipping
                                    .Where(p => p.system_category_serial_no.HasValue && 
                                                p.system_category_serial_no.Value.Equals(1))
                                    .OrderBy(p => p.priority)
                                    .ToList();
            var statString = "";
            foreach (var state in stateList)
            {
                statString += "<option value='" + state.state_serial_no + "' ";
                if (state.state_serial_no == ReqStateID)
                    statString += " selected='true' ";
                statString += " >" + state.state_name + "</option>";
            }
            stateOption.Text = statString;
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

    public int ReqShippingID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "shippingid", 0); }
    }

    public int ReqStateID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "stateid", 0); }
    }
}