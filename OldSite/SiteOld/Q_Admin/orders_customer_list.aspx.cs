using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_orders_customer_list : PageBase
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
    }

    #region Properties
    public CusQueryType CurrentQueryType
    {
        get { return (CusQueryType)ViewState["QueryCustomer"]; }
        set { ViewState["QueryCustomer"] = value; }
    }

    #endregion

    #region bind customer list
    //private void BindCustomer()
    //{


    //}

    private void QueryCustomer( CusQueryType cqt) 
    {
        string sql_params = " 1 = 1 ";
        switch (cqt)
        {
            case CusQueryType.login_name:
                sql_params = " customer_login_name ='"+ this.txt_login_name.Text.Trim() +"' ";
                break;
            case CusQueryType.first_name:
                sql_params = " customer_first_name ='" + this.txt_first_name.Text.Trim() + "' ";
                break;

            case CusQueryType.last_name:
                sql_params = " customer_last_name ='" + this.txt_last_name.Text.Trim() + "' ";
                break;

            case CusQueryType.shipping_first_name:
                sql_params = " customer_shipping_first_name ='" + this.txt_shipping_first_name.Text.Trim() + "' ";
                break;

            case CusQueryType.shipping_last_name:
                sql_params = " customer_shipping_last_name ='" + this.txt_shipping_last_name.Text.Trim() + "' ";
                break;
            case CusQueryType.phone:
                sql_params = " (phone_d ='" + this.txt_phone.Text.Trim() + "'  or phone_n ='" + this.txt_phone.Text.Trim() + "'  or phone_c ='" + this.txt_phone.Text.Trim() + "') ";
                break;
            case CusQueryType.email:
                sql_params = " (customer_email1 ='" + this.txt_email.Text.Trim() + "'  or customer_email2 ='" + this.txt_email.Text.Trim() + "') ";
                break;
            case CusQueryType.customer_id:
                sql_params = " c.customer_serial_no ='" + this.txt_customer.Text.Trim() + "'";
                
                break;
        }
       DataTable dt = Config.ExecuteDataTable(string.Format(@"
select * from (
select c.customer_serial_no,c.customer_first_name,c.phone_d,c.phone_n,c.customer_card_state,c.customer_card_zip_code
,c.customer_login_name,c.customer_password
,0 order_count
 from tb_customer c 

where {0} and tag=1 order by customer_serial_no desc limit 0,30) customer left join tb_state_shipping ss on ss.state_serial_no=customer.customer_card_state", sql_params));

        this.gv_customer.DataSource = dt;
        this.gv_customer.DataBind();


    }

    #endregion

   
    protected void btn_clear_search_Click(object sender, EventArgs e)
    {
        this.gv_customer.DataSource = null;
        this.gv_customer.DataBind();
        this.txt_email.Text = "";
        this.txt_first_name.Text = "";
        this.txt_last_name.Text = "";
        this.txt_login_name.Text = "";
        this.txt_phone.Text = "";
        this.txt_shipping_first_name.Text = "";
        this.txt_shipping_last_name.Text = "";
        CH.CloseParentWatting(this.gv_customer);
    }
    protected void btn_search_login_name_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentQueryType = CusQueryType.login_name;
            QueryCustomer(CusQueryType.login_name);
            CH.CloseParentWatting(this.gv_customer);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.gv_customer);
            CH.Alert(ex.Message, this.gv_customer);
        }
    }
    protected void btn_search_first_name_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentQueryType = CusQueryType.first_name;
            QueryCustomer(CusQueryType.first_name);
            CH.CloseParentWatting(this.gv_customer);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.gv_customer);
            CH.Alert(ex.Message, this.gv_customer);
        }
    }
    protected void btn_search_last_name_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentQueryType = CusQueryType.last_name;
            QueryCustomer(CusQueryType.last_name);
            CH.CloseParentWatting(this.gv_customer);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.gv_customer);
            CH.Alert(ex.Message, this.gv_customer);
        }
    }
    protected void btn_search_shipping_first_name_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentQueryType = CusQueryType.shipping_first_name;
            QueryCustomer(CusQueryType.shipping_first_name);
            CH.CloseParentWatting(this.gv_customer);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.gv_customer);
            CH.Alert(ex.Message, this.gv_customer);
        }
    }
    protected void btn_search_shipping_last_name_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentQueryType = CusQueryType.shipping_last_name;
            QueryCustomer(CusQueryType.shipping_last_name);
            CH.CloseParentWatting(this.gv_customer);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.gv_customer);
            CH.Alert(ex.Message, this.gv_customer);
        }
    }
    protected void btn_search_phone_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentQueryType = CusQueryType.phone;
            QueryCustomer(CusQueryType.phone);
            CH.CloseParentWatting(this.gv_customer);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.gv_customer);
            CH.Alert(ex.Message, this.gv_customer);
        }
    }
    protected void btn_search_email_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentQueryType = CusQueryType.email;
            QueryCustomer(CusQueryType.email);
            CH.CloseParentWatting(this.gv_customer);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.gv_customer);
            CH.Alert(ex.Message, this.gv_customer);
        }
    }
    protected void btn_search_state_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentQueryType = CusQueryType.state;
            QueryCustomer(CusQueryType.state);
            CH.CloseParentWatting(this.gv_customer);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.gv_customer);
            CH.Alert(ex.Message, this.gv_customer);
        }
    }
    protected void gv_customer_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Footer
            && e.Row.RowType != DataControlRowType.Header
            && e.Row.RowType != DataControlRowType.Pager)
        {
            Literal _literal_order_code = (Literal)e.Row.Cells[9].FindControl("_literal_order_code");
            int customer_id;
            int.TryParse(e.Row.Cells[1].Text, out customer_id);
            _literal_order_code.Text = GetOrderListString(customer_id);
        }
    }

    private string GetOrderListString(int customer_id)
    {
            string order_list = @"<ul class=""ul_parent"">
                                                                <li> ";

            DataTable dt = Config.ExecuteDataTable(string.Format(@"select cs.order_code, cs.store_create_datetime order_date from tb_customer_store cs inner join tb_order_helper oh on oh.order_code=cs.order_code 
and oh.tag=1 and oh.is_ok=1 and cs.customer_serial_no='{0}' and oh.pre_status_serial_no not in (" + Config.notStatOrderStatus + @") ", customer_id));

        if(dt.Rows.Count >0)
            order_list += "<span style='color:green;'>"+ dt.Rows.Count.ToString() + @"</span>
                                                                        <div style=""border: 1px solid #ff9900; padding: 10px; width: 220px; left: -100px;"">
                                                                               ";
        else
            order_list += dt.Rows.Count .ToString() + @"
                                                                        <div style=""border: 1px solid #ff9900; padding: 10px; width: 220px; left: -100px;"">
                                                                               ";
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                order_list += string.Format(@"<a href=""orders_edit_detail.aspx?order_code={0}"" onclick=""OpenOrderDetail('{0}'); return false;"" style=""display:block"">{0} &nbsp;&nbsp;{1}</a>", dr["order_code"].ToString(), dr["order_date"].ToString());
            }
            if (dt.Rows.Count == 0)
                order_list += " No Match Data";
            order_list += @"
                                                                        </div>
                                                                </li>
                                                        </ul>   ";
            return order_list;
    }
    protected void btn_cutomerID_Click(object sender, EventArgs e)
    {
        try
        {
            CurrentQueryType = CusQueryType.customer_id;
            QueryCustomer(CusQueryType.customer_id);
            CH.CloseParentWatting(this.gv_customer);
        }
        catch (Exception ex)
        {
            CH.CloseParentWatting(this.gv_customer);
            CH.Alert(ex.Message, this.gv_customer);
        }
    }
    protected void gv_customer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "NewOrder":
                int customerID = int.Parse(e.CommandArgument.ToString());
                int OrderCode = new OrderHelper().CreateNewDefaultOrder(customerID);
                Response.Write(OrderCode.ToString() + "OK");
                InsertTraceInfo("Create One Order (" + OrderCode.ToString() + ")");
                Response.Redirect("/q_admin/orders_edit_detail_new.aspx?order_code=" + OrderCode.ToString());
                Response.End();
                break;               
        }
    }
}
public enum CusQueryType
{
    login_name,
    first_name,
    last_name,
    shipping_first_name,
    shipping_last_name,
    phone,
    email,
    state,
    customer_id
}