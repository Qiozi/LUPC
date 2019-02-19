using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckOutCreditCardToPaypal : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // 创建新订单
            OrderHelper.CopyToOrder(CurrOrderCode
              , CurrCustomer.customer_serial_no.Value
              , false
              , CurrSiteCountry
              , PRate
              , db);

            Response.Redirect("shopping_checkout_paypal_doDirectPayment.asp?orderCode=" + CurrOrderCode.ToString() + "&cid=" + CurrCustomer.customer_serial_no.ToString() + "&cardCtate=" + CurrCustomer.customer_card_state_code);
        }
    }
}