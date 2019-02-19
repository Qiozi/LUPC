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

public partial class Q_Admin_sale_add_order_pay_method : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindPayMethodsDDL();
        }
    }

    #region methods
    private void BindPayMethodsDDL()
    {
        this.ddl_pay_method.DataSource = PayMethodNewModel.GetModelsByOrder(DBContext);
        this.ddl_pay_method.DataTextField = "pay_method_name";
        this.ddl_pay_method.DataValueField = "pay_method_serial_no";
        this.ddl_pay_method.DataBind();

       var oh = OrderHelperModel.GetModelsByOrderCode(DBContext, OrderCode);
        if (oh.Length == 1)
        {
            int pay_method = -1;
            if (oh[0].pay_method != null)
            {
                int.TryParse(oh[0].pay_method.ToString(), out pay_method);
                if (pay_method != -1)
                    this.ddl_pay_method.SelectedValue = oh[0].pay_method.ToString();
            }
        }
    }
    #endregion


    #region Events
    protected void btn_submit_Click(object sender, EventArgs e)
    {
        var oh = OrderHelperModel.GetModelsByOrderCode(DBContext, OrderCode);
        var csm = CustomerStoreModel.FindModelsByOrderCode(DBContext, OrderCode.ToString());

        for (int i = 0; i < oh.Length; i++)
        {
            oh[i].pay_method = this.ddl_pay_method.SelectedValue.ToString();          

            csm[0].pay_method = int.Parse(this.ddl_pay_method.SelectedValue.ToString());
            DBContext.SaveChanges();
        }
        //AnthemHelper.Redirect("sales_edit_order_detail.aspx?order_code="+ this.OrderCode.ToString()+"&pay_method="+ this.ddl_pay_method.SelectedValue);
        AnthemHelper.Redirect("sale_edit_order.aspx?menu_id=2&order_code=" + this.OrderCode.ToString() + "&pay_method=" + this.ddl_pay_method.SelectedValue);
    }
    #endregion

    #region porperties
    public int OrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "order_code", -1); }
    }
    #endregion
}
