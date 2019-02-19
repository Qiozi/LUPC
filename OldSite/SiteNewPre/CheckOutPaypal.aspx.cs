using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class CheckOutPaypal : PageBase
{


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var test = new NVPAPICaller();

            if (HttpContext.Current.Session["payment_amt"] != null && HttpContext.Current.Session["payment_unit"] != null)
            {
                string amt = HttpContext.Current.Session["payment_amt"].ToString();
                string priceUnit = HttpContext.Current.Session["payment_unit"].ToString();
                string paypalOrderCode = HttpContext.Current.Session["paypalOrderCode"].ToString();

                // 创建新订单
                OrderHelper.CopyToOrder(int.Parse(paypalOrderCode)
                  , CurrCustomer.customer_serial_no.Value
                  , false
                  , CurrSiteCountry
                  , PRate
                  , db);

                Response.Redirect("/checkout_paypal_website_payments_pro_setExpressCheckout.asp?paypal=true&orderCode=" + paypalOrderCode + "&amt=" + amt + "&unit=" + priceUnit);
            }
            else
            {
                Response.Redirect("APIError.aspx?ErrorCode=AmtMissing");
            }
        }
    }


}