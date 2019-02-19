using LU.WebApi.Controllers.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LU.WebApi.Controllers.Web
{
    public class CatesController : BaseApiController
    {
        public List<Model.Cate> Get()
        {
            return LU.BLL.CateProvider.GetCates(DBContext);
        }
    }
}
