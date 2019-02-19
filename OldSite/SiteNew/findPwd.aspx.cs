using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class findPwd : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (IsLogin)
            {
                Response.Redirect((string.IsNullOrEmpty(ReqU) || ReqU.ToLower().IndexOf("findpwd.aspx") > -1 || ReqU.ToLower().IndexOf("register") >= 1) ? "/MyAccount.aspx" : ReqU, true);
            }
        }
    }
}