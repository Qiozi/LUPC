using LU.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCartGoOrder : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            IsSignIn();
            if (IsLocalHostFrom && IsLogin)
            {
                if (this.cookiesHelper.CurrOrderCode == 0)
                {
                    return;
                }
                var rate = new LU.BLL.PRateProvider(db);
                OrderHelper.CopyToOrder(this.cookiesHelper.CurrOrderCode
                    , CurrCustomer.customer_serial_no.Value
                    , false
                    , this.cookiesHelper.CurrSiteCountry
                    , rate.PRate(db)
                    , db);

                Response.Redirect("ShoppingCartGoSubmit.aspx", true);
            }
        }
    }
}