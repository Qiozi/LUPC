using LU.Model.Account;
using LU.Toolkit;
using SiteNewApi2.Controllers;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace SiteNewApi2.Filters
{
    public class LogonApiAttribute : ActionFilterAttribute
    {
        public bool Required { get; set; }

        public LogonApiAttribute(bool requeire)
        {
            this.Required = requeire;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionContext)
        {
            this.OnActionExecuted(actionContext);
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var controller = actionContext.ControllerContext.Controller as BaseApiController;
            controller.Done = new PostResultHelper(controller.Request);

            // 验证不通过
            if (actionContext.ModelState.IsValid == false)
            {
                actionContext.Response = actionContext.Request.CreateErrorResponse(
                    System.Net.HttpStatusCode.BadRequest, actionContext.ModelState);
            }
            else
            {
                var req = actionContext.Request;
                try
                {
                    if (actionContext.Request.Headers.Contains("Authorization"))
                    {
                        var authorization = req.Headers.GetValues("Authorization").FirstOrDefault();
                        controller.UserToken = GuidExtension.Base64ToGuid(authorization);
                    }
                }
                catch
                {
                    controller.UserToken = Guid.Empty;
                }

                if (actionContext.Request.Method == HttpMethod.Options)
                {
                    actionContext.Response = new HttpResponseMessage(HttpStatusCode.OK);
                }
                else
                {
                    if (Required)
                    {
                        var request = actionContext.Request;

                        // 验证用户
                        if (controller.UserToken != Guid.Empty)
                        {
                            var userInfo =  new UserInfoAdmin(); // TODO 读取用户信息

                            if (userInfo == null)
                            {
                                throw new Exception("5000000");
                            }
                            else
                            {
                                controller.UserInfo = userInfo;
                            }
                        }
                        else
                        {
                            throw new Exception("5000000");
                        }
                    }
                }
            }
            base.OnActionExecuting(actionContext);
        }
    }
}