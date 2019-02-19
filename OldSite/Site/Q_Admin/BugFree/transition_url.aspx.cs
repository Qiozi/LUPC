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

public partial class Q_Admin_BugFree_transition_url : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Transition();
        }
    }
    

    void Transition()
    {
        string userName = new LoginInfo(this.Context).UserName;
        DataTable dt = Config.ExecuteDataTable("Select * from buguser where UserName='" + userName + "'");
        if (dt.Rows.Count > 0)
        {
            string password = dt.Rows[0]["Email"].ToString();
            Response.Redirect("/bugfree/login.php?BugUserName=" + userName + "&BugUserPWD=" + password + "&Lang=English&CssStyle=Default");
            Response.End();
        }
    }
}
