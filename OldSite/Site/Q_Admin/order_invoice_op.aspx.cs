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

public partial class Q_Admin_order_invoice_op : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            DataTable dt = Config.ExecuteDataTable(string.Format("select order_invoice from tb_order_helper where order_code='{0}'", OrderCode));
            if (dt.Rows.Count > 0)
                this.lbl_invoice_no.Text = dt.Rows[0][0].ToString();
        }
    }
    

    public int OrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "order_code", -1); }
    }

    protected void btn_cancel_order_Click(object sender, EventArgs e)
    {
        OrderHelperModel ohm = new OrderHelperModel();
        ohm.CancelInvoiceOfOrder(OrderCode);
        CH.RunJavaScript("window.close();", this.Literal1);
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        OrderHelperModel ohm = new OrderHelperModel();
        ohm.SetInvoiceToOrder(OrderCode, true, true);
        CH.RunJavaScript("window.close();", this.Literal1);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        OrderHelperModel ohm = new OrderHelperModel();
        ohm.SetInvoiceToOrder(OrderCode, true, false);
        CH.RunJavaScript("window.close();", this.Literal1);
    }
}
