using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteApi.Controllers
{
    public class MsgController : Controller
    {
        // GET: Msg
        public ActionResult NoLogin()
        {
            return Content("No login.");
        }
    }
}