using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cmds_GetShippingCartQty : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var cartQty = new CookiesHelper(this.Context).CartQty;
            //var badge = "<span class='badge'>" + cartQty + "</span>";
            Response.Write(cartQty > 0 ? cartQty.ToString() : "");
            Response.End();
        }
    }
}