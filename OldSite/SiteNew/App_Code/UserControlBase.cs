using System;
using System.Linq;

/// <summary>
/// Summary description for UserControlBase
/// </summary>
public class UserControlBase : System.Web.UI.UserControl
{
    public CookiesHelper cookiesHelper;

    private LU.Data.nicklu2Entities _context;
    public LU.Data.nicklu2Entities DBContext
    {
        get
        {
            if (_context == null)
                _context = new LU.Data.nicklu2Entities();
            return _context;
        }
    }

    public UserControlBase()
    {
        //
        // TODO: Add constructor logic here
        //
        cookiesHelper = new CookiesHelper(this.Context);
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
        get
        {
            return CurrCustomer != null;
        }
    }
    private LU.Data.tb_customer _customer;
    /// <summary>
    /// 当前客户信息
    /// </summary>
    public LU.Data.tb_customer CurrCustomer
    {
        get
        {
            if (_customer == null)
                _customer = LU.BLL.Users.UserToken.GetCustomerWithToken(DBContext, cookiesHelper.UserToken);
            return _customer;
        }
    }

    public string CurrCustomerName
    {
        get
        {
            if (CurrCustomer == null)
            {
                return string.Empty;
            }
            return (CurrCustomer.customer_first_name + " " + CurrCustomer.customer_last_name).Trim();
        }
    }
    #endregion
}