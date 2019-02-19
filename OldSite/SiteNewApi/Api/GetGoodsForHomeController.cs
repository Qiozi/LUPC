using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace V1.Controllers
{
    public class GetGoodsForHomeController : ApiController
    {
        public LU.Model.PostResult Get()
        {
            var context = new LU.Data.nicklu2Entities();
            var query = LU.BLL.ProductProvider.GetHomeProducts(context, LU.Model.Enums.CountryType.CAD);
            return new LU.Model.PostResult
            {
                Data = query,
                Success = true
            };
        }
    }
}
