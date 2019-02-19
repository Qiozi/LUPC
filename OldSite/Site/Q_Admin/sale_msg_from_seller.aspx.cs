using System;

public partial class Q_Admin_sale_msg_from_seller : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.add_order);

            SetControls(OrderCode);
        }
    }


    private void SetControls(int order_code)
    {
        var ohm = OrderHelperModel.GetModelsByOrderCode(DBContext, order_code);

        if (ohm.Length == 1)
        {
            this.TextBox1.Text = ohm[0].Msg_from_Seller;

        }

    }

    protected void lb_save_Click(object sender, EventArgs e)
    {
        var ohm = OrderHelperModel.GetModelsByOrderCode(DBContext, OrderCode);

        if (ohm.Length == 1)
        {
            ohm[0].Msg_from_Seller = this.TextBox1.Text;
            DBContext.SaveChanges();
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
    }
    protected void lb_clore_Click(object sender, EventArgs e)
    {
        AnthemHelper.Close();
    }

    public int OrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "order_code", -1); }
    }
}
