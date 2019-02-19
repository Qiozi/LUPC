using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SiteManager.Filters;

namespace SiteManager.Controllers
{
    [LogonFilter]
    public class BaseController : Controller
    {
        public LU.Data.nicklu2Entities DBContext { get; set; }

        public LU.Model.M.AdminUser UserInfo { get; set; }

        public BaseController()
        {
            this.DBContext = new LU.Data.nicklu2Entities();
        }


        ~BaseController()
        {
            //this.DBContext.();
        }
    }

}