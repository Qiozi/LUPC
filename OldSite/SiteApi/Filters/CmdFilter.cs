using SiteApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SiteApi.Filters
{
    public class CmdFilter : ActionFilterAttribute
    {
        private bool _logon = true;

        public CmdFilter() { }

        public CmdFilter(bool logon)
        {
            _logon = logon;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.Request.Method == HttpMethod.Options)
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.OK);
            }
            else
            {
                var controller = actionContext.ControllerContext.Controller as BaseApiController;

                if (controller.Request.Method.Method.ToLower() == "post")
                {
                    controller.LoginCmd = true;
                }
                else if (controller.Request.Method.Method.ToLower() == "get")
                {
                    var cmdValue = controller.Request.GetQueryNameValuePairs();//.GetCookies("DK");
                    if (cmdValue.Count() > 0)
                    {
                        foreach (var c in cmdValue)
                        {
                            if (c.Key.Equals("t"))
                            {
                                var cmd = c.Value;
                                var query = controller.DBContext.tb_exchange.ToList();
                                foreach (var item in query)
                                {
                                    if (DateTime.Now.Subtract(item.Regdate).TotalHours >= 3)
                                    {
                                        controller.DBContext.tb_exchange.Remove(item);
                                    }
                                }
                                controller.DBContext.SaveChanges();

                                var exchange = controller.DBContext.tb_exchange.SingleOrDefault(p => p.Pwd.Equals(cmd));
                                controller.LoginCmd = exchange != null;
                                break;
                            }
                        }
                    }
                }

                if (!controller.LoginCmd)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                }

                base.OnActionExecuting(actionContext);
            }
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            //var controller = actionExecutedContext.ActionContext.ControllerContext.Controller as BApiController;
            //actionExecutedContext.Response.Headers.Add("set-cookie", string.Format("DK={0}; domain={1}; expires=Fri, 31-Dec-9999 23:59:59 GMT; path=/",
            //    controller.Session.DeviceKey,
            //    Variable.CookieDomain));
            base.OnActionExecuted(actionExecutedContext);
        }

    }
}