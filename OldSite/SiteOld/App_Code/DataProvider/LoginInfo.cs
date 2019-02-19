
/// <summary>
/// LoginInfo 的摘要说明
/// </summary>
public class LoginInfo
{
    string cookiesDomain = System.Configuration.ConfigurationManager.AppSettings["cookieDomain"].ToString();
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
            if (hc.Request.Cookies["LoginID"] != null)
            {
                return hc.Request.Cookies["LoginID"].Value;
            }

            return null;
        }
        set
        {
            hc.Response.Cookies["LoginID"].Domain = cookiesDomain;
            hc.Response.Cookies["LoginID"].Path = "/";
            hc.Response.Cookies["LoginID"].Value = value;
        }
    }

    public string RealName
    {
        get
        {
            if (hc.Request.Cookies["RealName"] != null)
                return hc.Request.Cookies["RealName"].Value;
            return null;
        }
        set
        {
            hc.Response.Cookies["RealName"].Domain = cookiesDomain;
            hc.Response.Cookies["RealName"].Path = "/";
            hc.Response.Cookies["RealName"].Value = value;
        }
    }

    public string UserName
    {
        get
        {
            if (hc.Request.Cookies["UserName"] != null)
                return hc.Request.Cookies["UserName"].Value;
            return null;
        }
        set
        {
            hc.Response.Cookies["UserName"].Domain = cookiesDomain;
            hc.Response.Cookies["UserName"].Path = "/";
            hc.Response.Cookies["UserName"].Value = value; }
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
                if (hc.Request.Cookies["IsShowMessageWin"].Value == "1")
                    return true;
                else
                    return false;
            }
            catch { return true; }
        }
        set
        {
            if (value)
                hc.Response.Cookies["IsShowMessageWin"].Value = "1";
            else
                hc.Response.Cookies["IsShowMessageWin"].Value = "0";
            hc.Response.Cookies["IsShowMessageWin"].Domain = cookiesDomain;
            hc.Response.Cookies["IsShowMessageWin"].Path = "/";
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
                if (hc.Request.Cookies["CloseOpenMessageWin"].Value == "1")
                    return true;
                else
                    return false;
            }
            catch { return true; }
        }
        set
        {
            if (value)
                hc.Response.Cookies["CloseOpenMessageWin"].Value = "1";
            else
                hc.Response.Cookies["CloseOpenMessageWin"].Value = "0";
            hc.Response.Cookies["CloseOpenMessageWin"].Domain = cookiesDomain;
            hc.Response.Cookies["CloseOpenMessageWin"].Path = "/";
        }
    }
}
