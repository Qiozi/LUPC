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

public partial class Q_Admin_sales_customer_history : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Redirect("/q_admin/orders_add_customer.aspx?cid="+ReqCustomerID, true);
            this.ValidateLoginRule(Role.order_list);
            InitialDatabase();
        }
    }
    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindOrderDG(false);
        SetLabelValue();
    }

    private void BindOrderDG(bool autoUpdate)
    {
        //
        Total = 0;
        DataTable dt = OrderHelperModel.FindOrderByCustomerID(ReqCustomerID);

        for (int i = 0; i < dt.Rows.Count; i++)
        {
            string _total = dt.Rows[i]["total"].ToString();
            if (_total != "")
            {
                try
                {
                    Total += decimal.Parse(_total);
                }
                catch { }
            }

        }
        this.dg_order_list.DataSource = dt;
        this.dg_order_list.DataBind();
        
    }

    private void SetLabelValue()
    {
        // 用户名
        CustomerModel m = CustomerModel.GetCustomerModel(ReqCustomerID);
        this.lbl_name.Text = m.customer_shipping_first_name + "&nbsp;" + m.customer_shipping_last_name;

        this.lbl_record_count.Text = this.dg_order_list.Items.Count.ToString();
        this.lbl_total.Text = Config.ConvertPrice( Total);

    }

    protected void dg_order_list_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        System.Web.UI.WebControls.LinkButton lb = (System.Web.UI.WebControls.LinkButton)e.CommandSource;
        switch (lb.Text)
        {
            case "View":
                AnthemHelper.OpenWinModelDialog("sales_order_detail.aspx?code=" + AnthemHelper.GetAnthemDataGridCellText(e.Item, 2), 800, 700, 200, 200);
                break;
            case "Edit":
               //AnthemHelper.Redirect("sale_add_order.aspx?menu_id=2&order_code=" + AnthemHelper.GetAnthemDataGridCellText(e.Item, 5));
                break;
            case "Cancel":
               // OrderHelperModel.UpdateTag(false, AnthemHelper.GetAnthemDataGridCellText(e.Item, 0));
               // BindOrderDG(true);
                break;

            case "...":
                //AnthemHelper.OpenWinModelDialog("order_status_history.aspx?order_code=" + AnthemHelper.GetAnthemDataGridCellText(e.Item, 5), 800, 700, 200, 200);
                break;
            case "....":
               // AnthemHelper.OpenWinModelDialog("sale_msg_from_seller.aspx?order_code=" + AnthemHelper.GetAnthemDataGridCellText(e.Item, 5), 440, 330, 400, 400);
                break;
        }
    }
    protected void dg_order_list_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {

    }
    protected void dg_order_list_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            int out_status = int.Parse(e.Item.Cells[5].Text);
            int pre_status = int.Parse(e.Item.Cells[6].Text);

            e.Item.Cells[5].Text = FactureStateModel.GetFactureStateModel(out_status).facture_state_name;            
            e.Item.Cells[6].Text = PreStatusModel.GetPreStatusModel(pre_status).pre_status_name;

            int state;
            int.TryParse(e.Item.Cells[8].Text, out state);
            e.Item.Cells[8].Text = StateShippingModel.GetStateShippingModel(state).state_name;
        }
    }

    public int ReqCustomerID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "customer_id", -1); }
    }

    /// <summary>
    /// 消费总金额
    /// </summary>
    public decimal Total
    {
        get
        {
            object o = ViewState["Total"];
            if (o != null)
                return decimal.Parse(o.ToString());
            return 0;
        }
        set { ViewState["Total"] = value; }
    }
}
