using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Login : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsLogin)
            {
                Response.Redirect((string.IsNullOrEmpty(ReqU) || ReqU.ToLower().IndexOf("findpwd.aspx") > -1 || ReqU.ToLower().IndexOf("register") >= 1) ? "/MyAccount.aspx" : ReqU, true);
            }

            panelSiginFirst.Visible = IsToCart();
        }
    }

    public bool IsToCart()
    {
        return ReqU.ToLower().Contains("shoppingcartto");
    }
    public string ReturnUrl
    {
        get
        {
            if (IsToCart())
            {
                return string.Concat(ReqU, "&", "toCart=1");
            }
            else
            {
                return ReqU;
            }
        }
    }
}