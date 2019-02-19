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
            if (IsLocalHostFrom && IsLogin)
            {
                if (CurrOrderCode == 0)
                {
                    return;
                }

                OrderHelper.CopyToOrder(CurrOrderCode
                    , CurrCustomer.customer_serial_no.Value
                    , false
                    , CurrSiteCountry
                    , PRate
                    , db);

                Response.Redirect("ShoppingCartGoSubmit.aspx", true);
            }
        }
    }
}