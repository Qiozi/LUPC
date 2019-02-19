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

public partial class Q_Admin_NetCmd_OrderStat : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Stat s = new Stat();
            s.GenerateOrderStatByMonthAgo();
            s.GenerateOrderStat30DayAgo();
            Response.Write("--------------------------stat success !-------------------------------");
        }
    }
}
