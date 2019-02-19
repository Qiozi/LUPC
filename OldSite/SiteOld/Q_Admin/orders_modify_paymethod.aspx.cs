using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_orders_modify_paymethod : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    #region methods
    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindPayMethodsDDL();
    }

    private void BindPayMethodsDDL()
    {
        XmlStore xs = new XmlStore();

        this.RadioButtonList_paymethod.DataSource = xs.FindPayMethods();
        this.RadioButtonList_paymethod.DataTextField = "pay_method_name";
        this.RadioButtonList_paymethod.DataValueField = "pay_method_serial_no";
        this.RadioButtonList_paymethod.DataBind();
        this.RadioButtonList_paymethod.SelectedValue = Config.pay_method_pick_up_id_default.ToString();
        //OrderHelperModel[] oh = OrderHelperModel.GetModelsByOrderCode(OrderCode);
        //if (oh.Length == 1)
        //{
        //    int pay_method = -1;
        //    if (oh[0].pay_method != null)
        //    {
        //        int.TryParse(oh[0].pay_method.ToString(), out pay_method);
        //        if (pay_method != -1)
        //            this.RadioButtonList_paymethod.SelectedValue = oh[0].pay_method.ToString();
        //        else
        //            this.RadioButtonList_paymethod.SelectedValue = Config.pay_method_pick_up_id_default.ToString();
               
        //    }
        //}
    }
    #endregion

    #region Events
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        OrderHelperModel[] oh = OrderHelperModel.GetModelsByOrderCode(OrderCode);
        CustomerStoreModel[] csm = CustomerStoreModel.FindModelsByOrderCode(OrderCode.ToString());

        for (int i = 0; i < oh.Length; i++)
        {
            oh[i].pay_method = this.RadioButtonList_paymethod.SelectedValue.ToString();
            oh[i].price_unit = this.RadioButtonList_country.SelectedValue.ToString();
            oh[i].current_system = oh[i].price_unit == "CAD" ? "1" : "2";
            oh[i].Update();

            csm[0].pay_method = int.Parse(this.RadioButtonList_paymethod.SelectedValue.ToString());
            csm[0].Update();
        }
        CH.Redirect("orders_edit_detail_selected.aspx?order_code=" + this.OrderCode.ToString() + "&pay_method=" + this.RadioButtonList_paymethod.SelectedValue, this.btn_submit);
    }
    #endregion

    #region porperties
    public int OrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "order_code", -1); }
    }
    #endregion
}
