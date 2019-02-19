using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;

public partial class Q_Admin_product_helper_update_product : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        Response.Write("此功能暂时关闭;");
        Response.End();
        if (!IsPostBack)
        {
        }

    }
}