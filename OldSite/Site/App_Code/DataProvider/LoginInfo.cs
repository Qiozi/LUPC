
/// <summary>
/// LoginInfo 的摘要说明
/// </summary>
public class LoginInfo
{
    string cookiesDomain = System.Configuration.ConfigurationManager.AppSettings["cookiesDomain"];
    System.Web.HttpContext hc;
    public LoginInfo(System.Web.HttpContext _hc)
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //

        hc = _hc;
    }

    //int _login_id = 1;
    public string LoginID
    {
        get
        {
            if (hc.Request.Cookies["AdminLoginID"] != null)
                return hc.Request.Cookies["AdminLoginID"].Value;
            return null;
        }
        set
        {
            hc.Response.Cookies["AdminLoginID"].Domain = cookiesDomain;
            hc.Response.Cookies["AdminLoginID"].Path = "/";
            hc.Response.Cookies["AdminLoginID"].Value = value;
        }
    }

    public string UserToken
    {
        get
        {
            if (hc.Request.Cookies["Token"] != null)
                return hc.Request.Cookies["Token"].Value;
            return null;
        }
        set
        {
            hc.Response.Cookies["Token"].Domain = cookiesDomain;
            hc.Response.Cookies["Token"].Path = "/";
            hc.Response.Cookies["Token"].Value = value;
        }
    }

    public string RealName
    {
        get
        {
            if (hc.Request.Cookies["AdminRealName"] != null)
                return hc.Request.Cookies["AdminRealName"].Value;
            return null;
        }
        set
        {
            hc.Response.Cookies["AdminRealName"].Domain = cookiesDomain;
            hc.Response.Cookies["AdminRealName"].Path = "/";
            hc.Response.Cookies["AdminRealName"].Value = value; }
    }

    public string UserName
    {
        get
        {
            if (hc.Request.Cookies["AdminUserName"] != null)
                return hc.Request.Cookies["AdminUserName"].Value;
            return null;
        }
        set {
            hc.Response.Cookies["AdminUserName"].Domain = cookiesDomain;
            hc.Response.Cookies["AdminUserName"].Path = "/";
            hc.Response.Cookies["AdminUserName"].Value = value; }
    }

    public int LoginIDInt
    {
        get
        {
            int _id = -1;
            int.TryParse(LoginID, out _id);
            return _id;
        }
    }
    /// <summary>
    /// close Alert Message Win.
    /// </summary>
    public bool IsShowMessageWin
    {
        get
        {
            try
            {
                if (hc.Request.Cookies["AdminIsShowMessageWin"].Value == "1")
                    return true;
                else
                    return false;
            }
            catch { return true; }
        }
        set
        {
            if (value)
                hc.Response.Cookies["AdminIsShowMessageWin"].Value = "1";
            else
                hc.Response.Cookies["AdminIsShowMessageWin"].Value = "0";
            hc.Response.Cookies["AdminIsShowMessageWin"].Domain = cookiesDomain;
            hc.Response.Cookies["AdminIsShowMessageWin"].Path = "/";
        }
    }

    /// <summary>
    /// close Open Message Win.
    /// </summary>
    public bool CloseOpenMessageWin
    {
        get
        {
            try
            {
                if (hc.Request.Cookies["AdminCloseOpenMessageWin"].Value == "1")
                    return true;
                else
                    return false;
            }
            catch { return true; }
        }
        set
        {
            if (value)
                hc.Response.Cookies["AdminCloseOpenMessageWin"].Value = "1";
            else
                hc.Response.Cookies["AdminCloseOpenMessageWin"].Value = "0";
            hc.Response.Cookies["AdminCloseOpenMessageWin"].Domain = cookiesDomain;
            hc.Response.Cookies["AdminCloseOpenMessageWin"].Path = "/";
        }
    }
}
