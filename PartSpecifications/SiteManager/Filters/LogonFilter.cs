using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteManager.Controllers;
using SiteManager.Models;

namespace SiteManager.Filters
{
    public class LogonFilter : System.Web.Mvc.FilterAttribute, IActionFilter
    {
        public LogonFilter()
        {
            this.Order = 1;
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;

            if (controller != null && controller.UserInfo != null)
            {
                HttpCookie cookie = new HttpCookie("AdminLoginID", controller.UserInfo.Id.ToString());
                cookie.Domain = LU.BLL.ConfigAdmin.CookiesDomain;
                filterContext.HttpContext.Response.SetCookie(cookie);
                if (filterContext.Result is ViewResult)
                {
                    ViewResult viewResult = filterContext.Result as ViewResult;
                    var model = viewResult.Model as BaseViewModel;
                    model.UserInfo = controller.UserInfo;
                }
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var controller = filterContext.Controller as BaseController;
            HttpRequestBase request = filterContext.HttpContext.Request;
            HttpCookie cookie = request.Cookies["AdminLoginID"];

            if (cookie != null && !string.IsNullOrEmpty(cookie.Value))
            {
                var userId = cookie.Value;
                var id = 0;
                int.TryParse(userId, out id);

                var query = controller.DBContext.tb_staff.Single(p => p.staff_serial_no.Equals(id));
                controller.UserInfo = new LU.Model.M.AdminUser
                {
                    Id = query.staff_serial_no,
                    Realname = query.staff_realname,
                    UserName = query.staff_login_name
                };
            }
            else
            {
                filterContext.Result = new RedirectResult("/Account/Login");
            }
        }
    }
}