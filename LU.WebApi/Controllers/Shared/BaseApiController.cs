using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LU.WebApi.Controllers.Shared
{
    public class BaseApiController : ApiController
    {
        public LU.Data.nicklu2Entities DBContext { get; set; }

        public BaseApiController()
        {
            this.DBContext = new LU.Data.nicklu2Entities();
        }

        ~BaseApiController()
        {
           // this.DBContext.Dispose();
        }

    }
}
