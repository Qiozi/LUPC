using SiteApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static SiteApi.Filters.LogonFilter;

namespace SiteApi.Controllers
{
    [Logon(true)]
    public class BaseController : Controller
    {
        public UserInfo UserInfo { get; set; }

        public LU.Data.nicklu2Entities DBContext { get; set; }

        public BaseController()
        {
            this.DBContext = new LU.Data.nicklu2Entities();
        }

        ~BaseController()
        {
            if (this.DBContext != null)
                this.DBContext.Dispose();
        }
    }
}