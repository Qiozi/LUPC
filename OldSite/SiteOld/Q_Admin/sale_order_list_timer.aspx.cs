using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_sale_order_list_timer : PageBase
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
        BindOrderCodeLbl();
        BindSystemWarnLbl();
        BindQuestionLbl();
    }

    private void BindOrderCodeLbl()
    {
        string back_sum = Config.porder_order_status;
        string new_sum = Config.new_order_status;
        OrderHelperModel oh = new OrderHelperModel();

        oh.FindOrderCodeNOFinished(ref back_sum, ref new_sum);
        this.lbl_order_sum.Text = back_sum;
        this.lbl_order_submited_sum.Text = new_sum;
    }

    private void BindSystemWarnLbl()
    {
        this.lbl_system_sum.Text = EbaySystemModel.FindSystemByWarn().Rows.Count.ToString();
        if (lbl_system_sum.Text != "0")
            lbl_system_sum.ForeColor = System.Drawing.Color.FromName("red");
    }

    private void BindQuestionLbl()
    {
        this.lbl_question_sum.Text = AskQuestionModel.FindNoSentSum().ToString();

    }
    #endregion

    #region porperties

    #endregion

    protected void lbl_order_sum_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/q_admin/sale_order_list6.aspx?order_pre_status=" + Config.porder_order_status);
    }
    protected void lbl_order_submited_sum_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/q_admin/sale_order_list6.aspx?order_pre_status=" + Config.new_order_status);
    }
}
