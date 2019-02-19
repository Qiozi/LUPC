using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_orders_edit_detail_history : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindOrderProductHistory(ReqOrderCode.ToString());
        }
    }
    

    public int ReqOrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(this, "OrderCode", -1); }
    }

    #region history
    private void BindOrderProductHistory(string order_code)
    {
      var ophm = OrderProductHistoryModel.FindModelsByOrder(DBContext, order_code);
        this.gv_order_product_history.DataSource = ophm;
        this.gv_order_product_history.DataBind();
        if (ophm == null || ophm.Length <1)
        {
            Response.Write("No Data");
            Response.End();
        }
    }
    protected void gv_order_product_history_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType != DataControlRowType.Pager &&
            e.Row.RowType != DataControlRowType.Header &&
             e.Row.RowType != DataControlRowType.Footer)
        {
            string cmd = e.Row.Cells[4].Text;
            if (cmd == "True")
                e.Row.Cells[4].Text = "Add";
            else
            {
                e.Row.Cells[4].Text = "Delete";
                e.Row.Cells[2].Text = "-" + e.Row.Cells[2].Text;
            }
        }
    }
    #endregion
}