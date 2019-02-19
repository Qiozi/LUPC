using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for PageBase
/// </summary>

public class PageBase : System.Web.UI.Page
{
    public LU.Data.nicklu2Entities db = new LU.Data.nicklu2Entities();
    public CookiesHelper cookiesHelper;

    public PageBase()
    {

        //throw new Exception(this.Context.Request.Url.Host);
        if (this.Context.Request.Url.Host.IndexOf(System.Configuration.ConfigurationManager.AppSettings["hostName"].ToString()) == -1)
        {
            this.Context.Response.Redirect("/405.html", true);
            this.Context.Response.End();
        };
        cookiesHelper = new CookiesHelper(this.Context);
    }

    public int ReqSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", 0); }
    }

    public string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    public string ReqU
    {
        get
        {
            try
            {
                var url = Util.GetStringSafeFromQueryString(Page, "u");
                if (string.IsNullOrEmpty(url))
                {
                    if (Context.Request.UrlReferrer != null)
                        if (Context.Request.UrlReferrer.AbsoluteUri.ToLower().IndexOf(LU.BLL.Config.Host.ToLower()) > -1)
                            url = Context.Request.UrlReferrer.AbsoluteUri;
                }
                return url;
            }
            catch
            {

            }
            return "";
        }
    }

    /// <summary>
    ///  当前网站国家
    /// </summary>   

    public bool IsLocal
    {
        get { return Request.Url.ToString().ToLower().IndexOf(LU.BLL.Config.IsLocalHost) > -1; }
    }

    /// <summary>
    /// 是否从本站进入
    /// </summary>
    public bool IsLocalHostFrom
    {
        get
        {
            if (Request.ServerVariables["HTTP_REFERER"] == null)
            {
                return false;
            }

            string refererStr = Request.ServerVariables["HTTP_REFERER"].ToString();
            return IsLocal ? true : refererStr.ToLower().IndexOf(System.Configuration.ConfigurationManager.AppSettings["hostName"].ToString()) > -1;
        }
    }

    
    /// <summary>
    /// 判断当前是CAD 还是USD ，
    /// 如果是美金，
    /// 然后加币转换为美金
    /// </summary>
    /// <param name="CADPrice"></param>
    /// <returns></returns>
    public decimal ConvertPrice(decimal CADPrice)
    {
        var reateProvider = new LU.BLL.PRateProvider(db);
        return reateProvider.ConvertPrice(CADPrice, cookiesHelper.CurrSiteCountry);
    }


    #region 客户信息
    /// <summary>
    /// 客户是否登入
    /// </summary>
    public bool IsLogin
    {
        get
        {
            var tokenStr = cookiesHelper.UserToken;
            if (tokenStr == Guid.Empty)
            {
                return false;
            }
            else
            {
                var query = LU.BLL.Users.UserToken.GetUserId(db, tokenStr);
                return query > 0;
            }
        }
    }

    public void IsSignIn()
    {
        if (!IsLogin)
        {
            Response.Redirect(string.Concat(LU.BLL.Config.Host, "Login.aspx"));
        }
    }

    private LU.Data.tb_customer _customer { get; set; }
    /// <summary>
    /// 当前客户信息
    /// </summary>
    public LU.Data.tb_customer CurrCustomer
    {
        get
        {
            if (_customer == null)
            {
                var cid = LU.BLL.Users.UserToken.GetUserId(db, cookiesHelper.UserToken);
                _customer = db.tb_customer.SingleOrDefault(p => p.ID.Equals(cid));              
            }
            return _customer;
        }
    }

    public string CustomerName
    {
        get
        {
            if(CurrCustomer == null)
            {
                return string.Empty;
            }
            else
            {
                if (string.IsNullOrEmpty(CurrCustomer.customer_first_name) && string.IsNullOrEmpty(CurrCustomer.customer_last_name))
                    return CurrCustomer.customer_login_name;
                return string.Concat(CurrCustomer.customer_first_name, " ", CurrCustomer.customer_last_name);
            }
        }
    }
    #endregion

}