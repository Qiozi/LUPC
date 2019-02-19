using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShoppingCart : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //if (CurrOrderCode.ToString().Length != 6)
            {
                // Response.Write(CurrOrderCode.ToString());
            }
        }
    }
}