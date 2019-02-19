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

public partial class Q_Admin_order_paypal_error_info : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        DataTable dt = Config.ExecuteDataTable("select errkey, erritem from tb_order_paypal_error_info where order_code='"+ OrderCode.ToString() +"'");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("<table><tr><td colspan='2'><b>" + OrderCode.ToString() + "</b><hr size='1'></td></tr>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            sb.Append(string.Format("<tr><td><b>{0}:</b></td><td>{1}</td></tr>", dt.Rows[i][0].ToString(), dt.Rows[i][1].ToString()));
        }
        sb.Append("</table>");
        this.Literal1.Text = sb.ToString();
    }

    public int OrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "order_code", -1); }
    }
}
