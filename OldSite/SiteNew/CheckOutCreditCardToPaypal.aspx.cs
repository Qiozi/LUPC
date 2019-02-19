using LU.BLL;
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
            var rate = new LU.BLL.PRateProvider(db);
            // 创建新订单
            OrderHelper.CopyToOrder(this.cookiesHelper.CurrOrderCode
              , CurrCustomer.customer_serial_no.Value
              , false
              , this.cookiesHelper.CurrSiteCountry
              , rate.PRate(db)
              , db);

            Response.Redirect("shopping_checkout_paypal_doDirectPayment.asp?orderCode=" + this.cookiesHelper.CurrOrderCode.ToString() + "&cid=" + CurrCustomer.customer_serial_no.ToString() + "&cardCtate=" + CurrCustomer.customer_card_state_code);
        }
    }
}