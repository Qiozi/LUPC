<%@ Application Language="C#" %>
<%@ Import Namespace="System.IO" %>

<script RunAt="server">

    void Application_Start(object sender, EventArgs e)
    {
        // 在应用程序启动时运行的代码
        //Castle.ActiveRecord.Framework.IConfigurationSource source =
        //    System.Configuration.ConfigurationManager.GetSection("activerecord") as
        //    Castle.ActiveRecord.Framework.IConfigurationSource;
        //Castle.ActiveRecord.ActiveRecordStarter.Initialize(typeof(StaffModel).Assembly, source);

        Config.AppPath = Server.MapPath("~/").TrimEnd('\\') + "\\";

        System.Data.DataTable dt = Config.ExecuteDataTable("Select currency_usd from tb_currency_convert Where is_current=1 order by regdate desc limit 0,1");
        if (dt.Rows.Count > 0)
        {
            decimal currency;
            decimal.TryParse(dt.Rows[0][0].ToString(), out currency);
            ConvertPrice.CurrentCurrencyConverter = currency;
        }
    }

    void Application_End(object sender, EventArgs e)
    {
        //  在应用程序关闭时运行的代码

    }

    void Application_Error(object sender, EventArgs e)
    {
        // 在出现未处理的错误时运行的代码
        if (System.Configuration.ConfigurationManager.AppSettings["friendPage"].ToString() == "1")
        {
            Exception ex = Server.GetLastError().GetBaseException();
            string ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") == null ?
            Request.ServerVariables.Get("Remote_Addr").ToString().Trim() :
            Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();
            string logpath = Server.MapPath("~/") + "..\\web.Log\\M-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"; // 错误记录文件所在地
            StringBuilder errorString = new StringBuilder();
            errorString.AppendLine(string.Format("========== {0} Application_Error BEGIN ==========", DateTime.Now));
            errorString.AppendLine("Ip:" + ip);
            errorString.AppendLine("浏览器:" + Request.Browser.Browser.ToString());
            errorString.AppendLine("浏览器版本:" + Request.Browser.MajorVersion.ToString());
            errorString.AppendLine("操作系统:" + Request.Browser.Platform.ToString());
            errorString.AppendLine("页面：" + Request.Url.ToString());
            errorString.AppendLine("错误信息：" + ex.Message);
            errorString.AppendLine("错误源：" + ex.Source);
            errorString.AppendLine("异常方法：" + ex.TargetSite);
            errorString.AppendLine("堆栈信息：" + ex.StackTrace);
            errorString.AppendLine("========== Application_Error END ===================");
            lock (logpath)
            {
                try
                {
                    using (var writer = new StreamWriter(logpath, true))
                    {
                        writer.Write(errorString);
                    }
                }
                catch
                {
                    // 防止写文件时，文件被人为打开无法写入等
                    // 记录日志报错不做处理，不应影响用户继续使用
                }
            }
            Server.ClearError();
            Response.Redirect("/404.html");//跳出至自定义页面
        }
    }

    void Session_Start(object sender, EventArgs e)
    {
        // 在新会话启动时运行的代码
    }

    void Session_End(object sender, EventArgs e)
    {
        // 在会话结束时运行的代码。 
        // 注意: 只有在 Web.config 文件中的 sessionstate 模式设置为
        // InProc 时，才会引发 Session_End 事件。如果会话模式设置为 StateServer 
        // 或 SQLServer，则不会引发该事件。

    }

</script>
