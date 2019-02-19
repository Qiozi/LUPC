using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace SiteNewApi2
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            LU.BLL.Config.WebSitePhysicalPath = Server.MapPath("~/").TrimEnd('\\') + "\\";

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_Error()
        {
            StringBuilder errorString = new StringBuilder();
            string logpath = Server.MapPath("~/") + "..\\HY.Log\\" + DateTime.Now.ToString("yyyy-MM-dd") + "_Error.log"; // 错误记录文件所在地
            try
            {
                Exception ex = Server.GetLastError().GetBaseException();
                string ip = Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR") == null ?
                Request.ServerVariables.Get("Remote_Addr").ToString().Trim() :
                Request.ServerVariables.Get("HTTP_X_FORWARDED_FOR").ToString().Trim();


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

                errorString.AppendLine("Cookies：" + Request.Cookies["Token"].Value.ToString());
            }
            catch
            {

            }
            errorString.AppendLine("========== Application_Error END ===================");
            lock (logpath)
            {
                try
                {
                    using (var writer = new StreamWriter(logpath, true, Encoding.UTF8))
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
            Response.Write(@"{{ ""Data"":{{ 
                                            ""List"":""网络错误 。""
                                         }},
                                ""ErrMsg"":""网络错误 。"",
                                ""Success"":false}}");//跳出至自定义页面
        }
    }
}
