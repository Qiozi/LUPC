using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using LU.Data;
/// <summary>
/// PageBase 的摘要说明
/// </summary>
/// 
public class PageBaseNoInit : System.Web.UI.Page
{
    private nicklu2Entities _DBContext;

    public nicklu2Entities DBContext
    {
        get
        {
            if (_DBContext == null)
            {
                _DBContext = new nicklu2Entities();
            }
            return _DBContext;
        }
    }

    public PageBaseNoInit()
    {
        Context.Response.Cache.SetCacheability(HttpCacheability.NoCache);
    }

    public virtual void InitialDatabase()
    {
        string staff_serial_no = LoginUser.LoginID;
        if (staff_serial_no == "" || staff_serial_no == null)
        {
            Response.Redirect("/q_admin/no_login.aspx");
            Response.End();
        }

    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    /// <summary>
    /// 订单保存计算价格后，转到新的界面
    /// </summary>
    public void OrdersSavePageRedirect(int OrderCode, bool OnlyAccount)
    {
        nicklu2Entities context = new nicklu2Entities();
        var work = new OrderPriceWork(context, OrderCode);
        work.AccountOrder();


        if (!OnlyAccount)
        {
            Response.Write(Request.Url.PathAndQuery);
            Response.Redirect(Request.Url.PathAndQuery, true);

        }
    }
    /// <summary>
    /// 订单保存计算价格后，转到新的界面
    /// </summary>
    public void OrdersSavePageRedirect(int OrderCode)
    {
        OrdersSavePageRedirect(OrderCode, true);
    }

    public void InsertTraceInfo(nicklu2Entities context, string comment)
    {
        TrackModel.InsertInfo(context, comment, LoginUser.LoginIDInt);
    }

    public virtual void ValidateLoginRule(Role r)
    {
        try
        {
            nicklu2Entities context = new nicklu2Entities();
            var rms = RuleModel.FindModelsByStaffAndModel(context, int.Parse(LoginUser.LoginID), RoleHelper.Rolevalue(r));
            if (rms.Length == 0 || rms == null)
            {
                Response.Redirect("/q_admin/no_rule.aspx");
                Response.End();
            }
        }
        catch { }
    }

    public Command Cmd
    {
        get
        {
            object o = ViewState["Cmd"];
            if (o != null)
                return (Command)o;
            return Command.create;
        }
        set { ViewState["Cmd"] = value; }
    }

    public LoginInfo LoginUser
    {
        get
        {
            HttpContext hc = new HttpContext(this.Request, this.Response);
            return new LoginInfo(hc);
        }
    }
    ControlHelper _ch;
    public ControlHelper CH
    {
        get
        {
            if (_ch == null)
                _ch = new ControlHelper();
            return _ch;
        }
    }


    //public ILog logger = LogManager.GetLogger(typeof(PageBase).GetType());

    //public void LogWrite(string str)
    //{        
    //    logger.Info(str);
    //}
}
public class PageBase : PageBaseNoInit
{
    protected override void OnInit(EventArgs e)
    {
        InitialDatabase();
        base.OnInit(e);
    }

}
public class CtrlBase : System.Web.UI.UserControl
{
    new public PageBase Page
    {
        get
        {
            return (PageBase)base.Page;
        }
    }
}