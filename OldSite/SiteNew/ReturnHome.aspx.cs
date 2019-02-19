using LU.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ReturnHome : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Util.GetInt32SafeFromQueryString(Page, "cid", 0) == 2)
            {
                // us
                cookiesHelper.SetCookiesValue("CurrSiteCountry", ((int)CountryType.USD).ToString());
            }
            else
            {
                // ca
                cookiesHelper.SetCookiesValue("CurrSiteCountry", ((int)CountryType.CAD).ToString());
            }
            Response.Redirect(string.IsNullOrEmpty(ReqU) ? LU.BLL.Config.Host : ReqU, true);
        }
    }
}