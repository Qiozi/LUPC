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

public partial class Q_Admin_UC_AlertMessage : CtrlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }
    #region methods
    
    public void InitialDatabase()
    {
        BindOrderCodeLbl();
        BindSystemWarnLbl();
        BindQuestionLbl();

        this.lbl_currency_converter.Text = ConvertPrice.CurrentCurrencyConverter.ToString();
    }

    private void BindOrderCodeLbl()
    {
        //string back_sum = Config.porder_order_status;
        //string new_sum =  Config.new_order_status;
       // OrderHelperModel oh = new OrderHelperModel();

        //oh.FindOrderCodeNOFinished(ref back_sum,ref new_sum);
        //this.lbl_order_sum.Text = back_sum;
        //this.lbl_order_submited_sum .Text= new_sum;
    }

    private void BindSystemWarnLbl()
    {
        this.lbl_system_sum.Text = EbaySystemModel.FindSystemByWarn().Rows.Count.ToString();
        if (lbl_system_sum.Text != "0")
            lbl_system_sum.ForeColor = System.Drawing.Color.FromName("red");
    }

    private void BindQuestionLbl()
    {
       // this.lbl_question_sum.Text = AskQuestionModel.FindNoSentSum().ToString();
   
    }
    #endregion

    #region porperties

    #endregion

    #region Events
    protected void btn_close_Click(object sender, EventArgs e)
    {
        Page.LoginUser.IsShowMessageWin = true;
        Page.LoginUser.CloseOpenMessageWin = false;
    }
    protected void btn_close_none_show_Click(object sender, EventArgs e)
    {
        Page.LoginUser.IsShowMessageWin = false;
        Page.LoginUser.CloseOpenMessageWin = false;
    }
    #endregion
    protected void lbl_order_sum_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/q_admin/sale_order_list6.aspx?order_pre_status=" + Config.porder_order_status );
    }
    protected void lbl_order_submited_sum_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/q_admin/sale_order_list6.aspx?order_pre_status=" + Config.new_order_status);
    }
 
}
