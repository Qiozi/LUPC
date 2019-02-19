using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCartGo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (!IsLocalHostFrom || !IsLogin)
            {
                Response.Write("No...");
                Response.End();
            }

            var oc = CurrOrderCode;
            var cartTempList = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue
                && p.cart_temp_code.Value.Equals(oc)).ToList();
            var state = db.tb_state_shipping.FirstOrDefault(p => p.state_serial_no.Equals(ReqStateID));
            foreach (var cart in cartTempList)
            {
                cart.pay_method = ReqPayment;
                cart.state_shipping = ReqStateID;
                cart.shipping_company = ReqShipCompany;
                if (state != null)
                {
                    cart.shipping_country_code = state.Country;
                    cart.shipping_state_code = state.state_code;
                }
                db.SaveChanges();
            }

            var hostName = string.Format("{0}{1}/"
                , Request.Url.Host.ToLower().ToString().IndexOf("lucomputers.com") > -1 ? "https://lucomputers.com" : string.Concat("http://", Request.Url.Host)
                , Request.Url.Port == 80 ? "" : string.Concat(":", Request.Url.Port));

            if (ReqPayment == setting.PaymentPaypal)
            {
                Response.Redirect(hostName + "ShoppingCartGoView.aspx?istopaypal=1", true);
            }
            else if (ReqPayment == setting.PaymentPaypalCard)
            {
                Response.Redirect(hostName + "checkoutcreditcard.aspx?shippingid=" + ReqShipCompany + "&stateid=" + ReqStateID, true);
            }
            else if (setting.LocalAll.Contains(ReqPayment))
            {
                Response.Redirect(hostName + "checkoutpickup.aspx?shippingid=" + ReqShipCompany + "&stateid=" + ReqStateID, true);
            }
            else if (ReqPayment == setting.PaymentEmailTransfer)
            {
                Response.Redirect(hostName + "checkoutemailtransfer.aspx?shippingid=" + ReqShipCompany + "&stateid=" + ReqStateID, true);
            }
            else if (ReqPayment == setting.PaymentBankTransfer)
            {
                Response.Redirect(hostName + "checkoutbanktransfer.aspx?shippingid=" + ReqShipCompany + "&stateid=" + ReqStateID, true);
            }
            else if (ReqPayment == setting.PaymentCashDeposit)
            {
                Response.Redirect(hostName + "checkoutcashdeposit.aspx?shippingid=" + ReqShipCompany + "&stateid=" + ReqStateID, true);
            }
            else if (ReqPayment == setting.PaymentMoneyOrder)
            {
                Response.Redirect(hostName + "checkoutmoneyorder.aspx?shippingid=" + ReqShipCompany + "&stateid=" + ReqStateID, true);
            }
            else if (ReqPayment == setting.PaymentPersonalCheckCompanyCheck)
            {
                Response.Redirect(hostName + "CheckOutPersonalCheckCompanyCheck.aspx?shippingid=" + ReqShipCompany + "&stateid=" + ReqStateID, true);
            }
        }
    }

    int ReqPayment
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "payment", 0); }
    }

    int ReqShipCompany
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "shipid", 0); }
    }

    int ReqStateID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "stateid", 0); }
    }
}