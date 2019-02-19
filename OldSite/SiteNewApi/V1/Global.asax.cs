using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.IO;
using System.Web.Routing;

namespace V1
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new DefaultContractResolver() { IgnoreSerializableAttribute = true };
        }

        protected void Application_Error()
        {
            // 在出现未处理的错误时运行的代码
            // if (System.Configuration.ConfigurationManager.AppSettings["friendPage"].ToString() == "1")
            {
                Exception ex = Server.GetLastError().GetBaseException();
                var ip = string.Empty;
                var browser = string.Empty;
                var browserVersion = string.Empty;
                var platform = string.Empty;
                var url = string.Empty;
                var referrer = string.Empty;
                var rawUrl = string.Empty;
                try
                {
                    ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") == null ?
                    Request.ServerVariables.Get("Remote_Addr").ToString().Trim() :
                    Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();

                    browser = Request.Browser.Browser.ToString();
                    browserVersion = Request.Browser.MajorVersion.ToString();
                    platform = Request.Browser.Platform.ToString();
                    url = Request.Url.ToString();
                    referrer = Request.UrlReferrer.ToString();
                    rawUrl = Request.RawUrl;
                }
                catch { }
                string logpath = Server.MapPath("~/") + "..\\web.Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt";
                StringBuilder errorString = new StringBuilder();
                errorString.AppendLine(string.Format("========== {0} Application_Error BEGIN ==========", DateTime.Now));
                errorString.AppendLine("Ip:" + ip);
                errorString.AppendLine("浏览器:" + browser);
                errorString.AppendLine("浏览器版本:" + browserVersion);
                errorString.AppendLine("操作系统:" + platform);
                errorString.AppendLine("页面：" + url);
                errorString.AppendLine("错误信息：" + ex.Message);
                errorString.AppendLine("错误源：" + ex.Source);
                errorString.AppendLine("异常方法：" + ex.TargetSite);
                errorString.AppendLine("堆栈信息：" + ex.StackTrace);
                errorString.AppendLine("UrlReferrer: " + referrer);
                errorString.AppendLine("URL:" + rawUrl);
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
                if (Request.Url.ToString().ToLower().IndexOf(LU.BLL.Config.IsLocalHost) == -1)
                {
                    Response.Redirect(LU.BLL.Config.Host);
                }
                else
                {
                    Response.Redirect("/mycustompage.htm");//跳出至自定义页面
                }
            }
        }
    }
}
