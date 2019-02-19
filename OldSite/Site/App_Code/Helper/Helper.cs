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
/// Helper 的摘要说明
/// </summary>
public class Helper
{
	public Helper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static string RemoveSpace(string text)
    {
        return text.Replace(" ", "");
    }

    public static string CustomerHistoryHref(string text,int customer_id)
    {
        return "<a href=\"sales_customer_history.aspx?menu_id=2&customer_id=" + customer_id + "\">" + text + "</a>";
    }
}
