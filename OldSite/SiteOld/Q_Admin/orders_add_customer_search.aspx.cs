using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_orders_add_customer_search : PageBase
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
        InitPage();
    }

    void InitPage()
    {

    }

    protected void lb_search_customer_Click(object sender, EventArgs e)
    {
        BindCustomerDG(true);
    }

    private void BindCustomerDG(bool is_have_keyword)
    {
        string keyword = this.txt_customer_search_keyword.Text.Trim();

        // 是否有查询条件
        if (!is_have_keyword)
        {
            keyword = "";
        }
        DataTable dt = CustomerModel.GetModelsBySearch(keyword);
        this.dg_customer.DataSource = dt;
        this.dg_customer.DataBind();
        SetCustomerCountLabel(dt.Rows.Count.ToString());
    }

    private void SetCustomerCountLabel(string count)
    {
        this.lbl_customer_count.Text = count;
    }
    protected void lb_clear_search_Click(object sender, EventArgs e)
    {
        this.BindCustomerDG(false);
    }
    protected void dg_customer_SelectedIndexChanged(object sender, EventArgs e)
    {
        Response.Write("<script>parent.location.href='/q_admin/orders_add_customer.aspx?cid="+dg_customer.SelectedRow.Cells[1].Text+"';</script>");
    }
}