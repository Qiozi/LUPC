using LU.Toolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using V1.Controllers;

namespace V1.Filters
{
    /// <summary>
    /// 登入判断过滤器
    /// </summary>
    public class LogonApiAttribute: ActionFilterAttribute
    {
        /// <summary>
        /// 
        /// </summary>
        public bool Require { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="requeire"></param>
        public LogonApiAttribute(bool requeire)
        {
            this.Require = requeire;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            base.OnActionExecuted(actionContext);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (this.Require)
            {
                if (actionContext.Request.Method == HttpMethod.Options)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    var controller = actionContext.ControllerContext.Controller as BaseApiController;
                    var request = actionContext.Request;
                    if (actionContext.Request.Headers.Contains("Authorization"))
                    {
                        var authorization = request.Headers.GetValues("Authorization").FirstOrDefault();

                        if (authorization.Length < 10)
                        {
                            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                        }
                        else
                        {
                            var token = GuidExtension.Base64ToGuid(authorization);

                            var userInfo = LU.BLL.Users.Account.GetUserInfo(controller.DBContext, token);
                            if (userInfo == null)
                            {
                                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                            }
                            else
                            {
                                controller.UserInfo = userInfo;
                                // TODO   加密狗信息处理                            
                            }
                        }
                    }
                    else
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
                    }
                }
            }
            base.OnActionExecuting(actionContext);
        }
    }
}