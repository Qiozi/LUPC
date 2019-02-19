using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class cmds_TopCountry : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (CurrSiteCountry == CountryType.CAD)
            {
                Response.Write(string.Format(@"        <li>
            <a title='Canada'   href=""http://ca.lucomputers.com/ReturnHome.aspx""><img src=""/images/ca24.png"" /></a>
        </li>
        <li>
            <a title='USA' href=""http://us.lucomputers.com/ReturnHome.aspx""><img src=""/images/usa24-2.png"" /></a>
        </li>
        <li>
          <a href=""/bContactUs.aspx""><span class=""glyphicon glyphicon-phone-alt""></span>1866.999.7828&nbsp;&nbsp;416.446.7743</a>
        </li>"));
            }
            else
            {
                Response.Write(string.Format(@"        <li>
            <a title='Canada'  href=""http://ca.lucomputers.com/ReturnHome.aspx""><img src=""/images/ca24-2.png"" /></a>
        </li>
        <li>
            <a title='USA' href=""http://us.lucomputers.com/ReturnHome.aspx""><img src=""/images/usa24.png"" /></a>
        </li>
        <li>
          <a href=""/bContactUs.aspx""><span class=""glyphicon glyphicon-phone-alt""></span>1866.999.7828&nbsp;&nbsp;416.446.7743</a>
        </li>"));
            }
        }
        Response.End();
    }
}