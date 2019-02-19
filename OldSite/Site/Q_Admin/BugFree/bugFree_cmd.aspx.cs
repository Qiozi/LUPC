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

public partial class Q_Admin_BugFree_bugFree_cmd : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Clear();
            Response.ClearContent();
            RunCmd();
        }
    }

    void RunCmd()
    {
        switch (ReqCmd)
        {
            case "GetTaskCount":
                Response.Write(Config.ExecuteScalarInt32("Select Count(BugID) from buginfo where AssignedTo='" + LoginUser.UserName + "'").ToString());
                //Response.Write("Select Count(*) from buginfo where AsignedTo='" + LoginUser.UserName + "'");
                break;
        }
        Response.End();
    }

    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }
}
