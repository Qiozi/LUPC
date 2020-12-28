using LU.Toolkit;
using SiteApi.Controllers;
using SiteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SiteApi.Filters
{
    public class LogonFilter
    {
        public class LogonAttribute : FilterAttribute, IActionFilter
        {
            public bool Require { get; set; }

            public LogonAttribute(bool requeire)
            {
                this.Require = requeire;
                this.Order = 1;
            }

            public void OnActionExecuted(ActionExecutedContext filterContext)
            {
                // TODO
                var controller = filterContext.Controller as BaseController;
                var cookie = filterContext.HttpContext.Request.Cookies["Token"];
                if (filterContext.Result is ViewResult && cookie != null)
                {
                    ViewResult viewResult = filterContext.Result as ViewResult;
                    BaseViewModel model = viewResult.Model as BaseViewModel;
                    if (model == null)
                    {
                        model = new BaseViewModel();
                    }
                    // 读用户数据
                    //var userIdStr = cookie.Value;
                    //var id = 0;
                    //int.TryParse(userIdStr, out id);
                    model.UserInfo = controller.UserInfo;// HY.BLL.Providers.User.GetUserInfo(controller.DBContext, id);
                }

            }

            public void OnActionExecuting(ActionExecutingContext filterContext)
            {
                var controller = filterContext.Controller as BaseController;
                // GetAppSettingFormCenter.InitData(controller.Request.Url.Scheme, controller.Request.Url.Authority);

                var request = filterContext.HttpContext.Request;
                var authority = request.Url.Authority;

                #region Domain
                var domain = authority.Substring(authority.IndexOf("."));
                if (domain.IndexOf(":") > -1)
                {
                    domain = domain.Substring(0, domain.IndexOf(":"));
                }
                #endregion

                if (this.Require)
                {
                    if (controller.ModelState.Count > 0)
                    {
                        var errMsg = string.Empty;
                        foreach (var item in controller.ModelState.Values)
                        {
                            if (item.Errors.Count > 0)
                            {
                                foreach (var err in item.Errors)
                                {
                                    errMsg += err.ErrorMessage;
                                }
                            }
                        }
                        if (!string.IsNullOrEmpty(errMsg))
                        {
                            filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Msg", action = "ErrorModel", errMsg = errMsg }));
                            return;
                        }
                    }


                    HttpCookie cookie = request.Cookies["Token"];


                    if (cookie.HasKeys == false || string.IsNullOrEmpty(cookie.Value))
                    {
                        filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Msg", action = "NoLogin" }));
                    }
                    else
                    {
                        var tokenStr = cookie.Value;

                        Guid token = GuidExtension.Base64ToGuid(tokenStr);
                        try
                        {
                            var userInfo = controller.DBContext.tb_user_token.SingleOrDefault(me => me.Token == token);

                            if (userInfo == null)
                            {
                                filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Msg", action = "UserNoExist" }));
                            }
                            else
                            {
                                controller.UserInfo = new UserInfo
                                {
                                    Id = userInfo.UserId,
                                    UserName = controller.DBContext.tb_staff.Single(me => me.staff_serial_no == userInfo.UserId).staff_login_name
                                };
                            }
                        }
                        catch (Exception ex)
                        {
                            filterContext.Result = new RedirectToRouteResult("Default", new RouteValueDictionary(new { controller = "Msg", action = "NoLogin" }));
                        }
                    }
                }
            }
        }
    }
}