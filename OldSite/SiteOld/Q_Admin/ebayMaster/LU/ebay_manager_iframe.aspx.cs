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

public partial class Q_Admin_ebayMaster_LU_ebay_manager_iframe : PageBase
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
        BindTREEView();
    }

    private void BindTREEView()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        DataTable dt = Config.ExecuteDataTable("select category_id, category_name from tb_product_category_new where parent_category_id=0 and showit=1");
        sb.Append("<ul id=\"browser\" class=\"filetree\">");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            DataTable subdt = Config.ExecuteDataTable(string.Format("select category_id, category_name from tb_product_category_new where parent_category_id='{0}' and showit=1", dr["category_id"].ToString()));
            if (subdt.Rows.Count > 0)
            {
                sb.Append(string.Format(@"<li><span class=""folder"" id=""treeview_category_id_{1}"">{0}</span><ul>", dr["category_name"].ToString(), dr["category_id"].ToString()));
                for (int j = 0; j < subdt.Rows.Count; j++)
                {
                    sb.Append(string.Format(@" <li><span class=""folder"" id=""treeview_category_id_{1}"">{0}</span> <ul>", subdt.Rows[j]["category_name"].ToString(), subdt.Rows[j]["category_id"].ToString()));
                    DataTable chdt = Config.ExecuteDataTable("select id from tb_ebay_system where category_id='" + subdt.Rows[j]["category_id"].ToString() + "' and showit=1 order by id desc ");
                    for (int x = 0; x < chdt.Rows.Count; x++)
                    {
                        sb.Append(string.Format(@"<li><span class=""file"" >{0}</span></li>", chdt.Rows[x]["id"].ToString()));
                    }
                    sb.Append("</ul></li>");
                }
                sb.Append("</ul></li>");
            }
            else
            {
                sb.Append(string.Format(@"<li><span class=""file"">{0}</span></li>", dr["category_name"].ToString()));
            }
        }
        sb.Append("</lu>");
        this.literal_treeview.Text = sb.ToString();
    }

}
