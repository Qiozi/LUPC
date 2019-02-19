using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SiteApp
{
    public partial class MyAccount : Helper.PageBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!Validate())
                    Response.Redirect("login.aspx", true);
            }
        }
    }
}