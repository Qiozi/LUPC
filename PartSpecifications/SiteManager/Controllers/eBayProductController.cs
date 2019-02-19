using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteManager.Controllers
{
    public class eBayProductController : Controller
    {
        // GET: eBayProduct
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult EditSpecifics()
        {
            return View();
        }
    }
}