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

public partial class Q_Admin_sale_order_list_invalid : PageBase
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
        WebLoad();
    }

    private void WebLoad()
    {
        BindDV();
    }

    private void BindDV()
    {
        string order_code = this.txt_order_code.Text.Trim();
        string first_name = this.txt_first_name.Text.Trim();
        string last_name = this.txt_last_name.Text.Trim();

        OrderHelperModel ohm = new OrderHelperModel();
        this.GridView_order_list.DataSource = ohm.FindOrderInvalid(order_code, first_name, last_name);
        this.GridView_order_list.DataBind();
    }

    protected void btn_search_Click(object sender, EventArgs e)
    {
        BindDV();
    }


    protected void GridView_order_list_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView gv = (GridView)sender;
        switch (e.CommandName)
        {
            case "ChangeValid":
                OrderHelperModel ohm = new OrderHelperModel();                
                int id = int.Parse(gv.Rows[int.Parse(e.CommandArgument.ToString())].Cells[0].Text);
                ohm.SetOrderValid(id);
                BindDV();
                break;
        }
        
    }
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        this.txt_first_name.Text = "";
        this.txt_last_name.Text = "";
        this.txt_order_code.Text = "";
        BindDV();
    }
    protected void GridView_order_list_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Header &&
            e.Row.RowType != DataControlRowType.Pager
            && e.Row.RowType != DataControlRowType.Footer)
        {
            ((LinkButton)e.Row.Cells[4].Controls[0]).Attributes.Add("onclick", "if (!confirm('确认使订单有效吗?')) return false;");
        }
    }
}
