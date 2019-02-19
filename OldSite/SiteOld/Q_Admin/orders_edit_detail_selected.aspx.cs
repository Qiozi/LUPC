using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_orders_edit_detail_selected : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
            btn_new_Click(null, null);
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        Response.Write("<script>ParentRemoveLoadWait();</script>");
    }

    int ReqOrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(this, "order_code", -1); }
    }

    protected void btn_new_Click(object sender, EventArgs e)
    {
        Response.Redirect("/q_admin/orders_edit_detail_new.aspx?order_code=" + ReqOrderCode.ToString());
     
    }
    protected void btn_old_Click(object sender, EventArgs e)
    {
        Response.Redirect("/q_admin/orders_edit_detail.aspx?order_code=" + ReqOrderCode.ToString());
  
    }
}