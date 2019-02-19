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
/// AccountHelper 的摘要说明
/// </summary>
public class AccountHelper
{
	public AccountHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    public static int GetSystemSize(decimal price, product_category pc)
    {
        DataTable dt = ProductSizeModel.GetModelByPrice(price, pc);
        if (dt.Rows.Count == 1)
            return int.Parse(dt.Rows[0]["product_size_id"].ToString());
        return -1;
    }

}
