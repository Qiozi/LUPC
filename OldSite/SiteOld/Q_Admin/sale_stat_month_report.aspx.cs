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

public partial class Q_Admin_sale_stat_month_report : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Redirect("sale_stat_month_report_2.aspx?menu_id=2");
        Response.End();
        if (!IsPostBack)
        {
            InitialDatabase();

        }
    }

   
}
