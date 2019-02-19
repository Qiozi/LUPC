using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserControlBase
/// </summary>
public class UserControlBase : System.Web.UI.UserControl
{
	public UserControlBase()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    #region 客户信息
    /// <summary>
    /// 客户是否登入
    /// </summary>
    public bool IsLogin
    {
        get { return CustomerID > 0; }

    }

    /// <summary>
    /// 客户ID
    /// </summary>
    public int CustomerID
    {
        get
        {
            int customerID = 0;
            if (Request.Cookies["CustomerID"] != null)
            {
                int.TryParse(Request.Cookies["CustomerID"].Value, out customerID);
            }

            return customerID;
        }
    }
    /// <summary>
    /// 保存客户ID号
    /// </summary>
    /// <param name="v"></param>
    public void SetCustomerID(int v)
    {
        Response.Cookies["CustomerID"].Value = v.ToString();
        Response.Cookies["CustomerID"].Domain = Variable.Domain;
    }
    /// <summary>
    /// 保存用户名
    /// </summary>
    /// <param name="name"></param>
    public void SetCustomerName(string name)
    {
        Response.Cookies["CustomerName"].Value = name;
        Response.Cookies["CustomerName"].Domain = Variable.Domain;
    }
    /// <summary>
    /// 取得用户名称 用于网页头部显示
    /// </summary>
    public string CustomerName
    {
        get
        {
            if (Request.Cookies["CustomerName"] != null)
            {
                return Request.Cookies["CustomerName"].Value;
            }

            return "";
        }
    }

    /// <summary>
    /// 购物车商品数量 
    /// </summary>
    public int CartQty
    {
        get
        {
            if (Request.Cookies["CartQty"] != null)
            {
                return int.Parse(Request.Cookies["CartQty"].Value);
            }

            return 0;
        }
    }

    #endregion
}