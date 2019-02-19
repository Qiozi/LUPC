using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCartGoSubmit : PageBase
{
    public string email = "The e-mail address is incorrect";

    public string ORDER_CODE = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ORDER_CODE = this.cookiesHelper.CurrOrderCode.ToString();
            if (ReqOrderCode > 0)
            {
                ORDER_CODE = ReqOrderCode.ToString();
            }
            if ((IsLocalHostFrom && IsLogin && this.cookiesHelper.CurrOrderCode > 0) || ReqOrderCode > 0)
            {
                var cus = db.tb_customer_store.FirstOrDefault(p => p.order_code.HasValue
                    && p.order_code.Value.Equals(this.cookiesHelper.CurrOrderCode));
                if (cus != null)
                {
                    if (cus.customer_login_name.IndexOf("@") > 1)
                        email = cus.customer_login_name;
                    else if (cus.customer_email1.IndexOf("@") > 1)
                        email = cus.customer_email1;
                    else if (cus.customer_email2.IndexOf("@") > 1)
                        email = cus.customer_email2;
                }
                this.cookiesHelper.CurrOrderCode = 0;
                Response.Cookies["CartQty"].Value = "0";
                Response.Cookies["CartQty"].Domain = Variable.Domain;
            }
            else
            {
                this.cookiesHelper.CurrOrderCode = 0;
                Response.Redirect("/");
            }
        }

    }

    int ReqOrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "ordercode", 0); }
    }
}