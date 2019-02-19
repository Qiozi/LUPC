using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class SetPaypalToken : System.Web.UI.Page
{
    PaypalStoreInfo psi = new PaypalStoreInfo(System.Web.HttpContext.Current);
    protected void Page_Load(object sender, EventArgs e)
    {
        string token = Server.UrlDecode(Request.QueryString["token"].ToString());

       // SaveToken(psi, token, Request.Url.AbsolutePath);
        //Response.Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" + Server.UrlDecode(Request.QueryString["token"].ToString()));
        Response.Redirect("https://www.sandbox.paypal.com/cgi-bin/webscr?cmd=_express-checkout&token=" + Server.UrlDecode(Request.QueryString["token"].ToString()));

    }
}
