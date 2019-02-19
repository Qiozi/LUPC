using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SiteNewApi2.Controllers.Api
{
    public class TestController : ApiController
    {
        public object Get()
        {
            return new 
            {
                Title="aaaa",
                SSSSS="hhhh"
            };
        }
    }
}
