using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// OrderPageBase 的摘要说明
/// </summary>
public class OrderPageBase : PageBase
{
	public OrderPageBase()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public int CurrentCustomerID
    {
        get
        {
            object o = ViewState["CurrentCustomerID"];
            if (o != null)
                return int.Parse(o.ToString());
            return -1;
        }
        set { ViewState["CurrentCustomerID"] = value; }
    }

    public string OrderCode
    {
        get
        {
            object o = ViewState["OrderCode"];
            if (o != null)
                return o.ToString();
            return "";
        }
        set { ViewState["OrderCode"] = value; }
    }

    public int OrderCodeRequest
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "order_code", -1); }
    }

    public void RedirectEditOrder()
    {
        Response.Redirect("Sale_add_order.aspx?menu_id=2");
    }
}

public class OrderCtrlBase : System.Web.UI.UserControl
{
    public OrderPageBase OrderPageBase
    {
        get { return (OrderPageBase)base.Page; }
    }
}