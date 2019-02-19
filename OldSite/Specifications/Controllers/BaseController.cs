using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Specifications.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public LU.Data.nicklu2Entities DBContext { get; set; }

        public BaseController()
        {
            this.DBContext = new LU.Data.nicklu2Entities();
        }

        ~BaseController()
        {

        }
    }
}