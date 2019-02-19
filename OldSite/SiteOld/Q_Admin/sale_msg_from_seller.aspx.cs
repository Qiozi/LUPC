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

public partial class Q_Admin_sale_msg_from_seller : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.add_order);
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        SetControls(OrderCode);
    }
    
    private void SetControls(int order_code)
    {
        OrderHelperModel[] ohm = OrderHelperModel.GetModelsByOrderCode(order_code);

        if (ohm.Length == 1)
        {
            this.TextBox1.Text = ohm[0].Msg_from_Seller;

        }

    }

    protected void lb_save_Click(object sender, EventArgs e)
    {
        OrderHelperModel[] ohm = OrderHelperModel.GetModelsByOrderCode(OrderCode);

        if (ohm.Length == 1)
        {
            ohm[0].Msg_from_Seller = this.TextBox1.Text;
            ohm[0].Update();
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
    }
    protected void lb_clore_Click(object sender, EventArgs e)
    {
        AnthemHelper.Close();
    }

    public int OrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page,"order_code", -1); }
    }
}
