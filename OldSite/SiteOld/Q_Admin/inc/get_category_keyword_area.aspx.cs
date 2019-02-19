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

public partial class Q_Admin_inc_get_category_keyword_area : PageBase
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
        WriteKeywordArea();
    }

    public void WriteKeywordArea()
    {
        try
        {
            DataTable dt = Config.ExecuteDataTable(string.Format(@"
                                select * from tb_product_category_keyword where keyword<>'' and category_id='{0}' ", CID));
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("<table cellpadding='0' cellspacing='0' style='width:100%'>");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                sb.Append("<tr>");
                sb.Append(string.Format("<td nowrap=\"nowrap\"><b>{0}</b></td>", dr["keyword"].ToString()));
                DataTable cdt = Config.ExecuteDataTable(string.Format(@"select * from tb_product_category_keyword_sub where parent_id='{0}' order by priority asc", dr["id"].ToString()));
                sb.Append("<td style='background:#ffffff;'>");
                sb.Append(string.Format("<input type='hidden' name='keyword_value_{0}' value='' />", dr["id"].ToString()));
                sb.Append(string.Format("<a name='keyword_{0}' value='' class='selected'>ALL</a>", dr["id"].ToString()));

                for (int j = 0; j < cdt.Rows.Count; j++)
                {
                    DataRow cdr = cdt.Rows[j];

                    sb.Append(string.Format("<a name='keyword_{1}' value='{0}' class='unselected'>{0}</a>"
                        , j < cdt.Rows.Count - 1 ? cdr["keyword"].ToString() + "," : cdr["keyword"].ToString()
                        , dr["id"].ToString()));

                }
                sb.Append("</td>");
                sb.Append("</tr>");
            }
            //
            // other inc
            //
            sb.Append("<tr>");
            sb.Append(string.Format("<td><b>&nbsp;</b></td>"));

            dt = new LtdHelper().LtdHelperToDataTableNoLU(); // Config.ExecuteDataTable(string.Format(@"select * from tb_product_category_keyword_sub where parent_id='{0}'", dr["id"].ToString()));
            sb.Append("<td style='background:#ffffff;'>");
            sb.Append(string.Format("<input type='hidden' name='keyword_other_inc' value='' />"));
            sb.Append(string.Format("<a name='keyword_0' value='' class='selected'>ALL</a>"));

            for (int j = 0; j < dt.Rows.Count; j++)
            {
                DataRow dr = dt.Rows[j];

                sb.Append(string.Format("<a name='keyword_{1}' value='{0}' class='unselected'>{0}</a>"
                    , j < dt.Rows.Count - 1 ? dr["text"].ToString() + "," : dr["text"].ToString()
                    , dr["id"].ToString()));

            }
            sb.Append("</td>");
            sb.Append("</tr>");
            sb.Append("</table>");
            Response.Write(sb.ToString());
            Response.End();
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
            Response.End();
        }
    }

    #region properties
    public int CID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "cid", -1); }
    }

    #endregion
}
