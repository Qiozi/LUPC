using LU.WebApi.Controllers.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LU.WebApi.Controllers.Web
{
    public class HomeController : BaseApiController
    {
        public class PostResult : Models.Shared.WebPostResult
        {
            public List<Model.Cate> Cates { get; set; }

            public List<Model.Product> Prods { get; set; }
        }

        public PostResult Get(string token = "")
        {
            return new PostResult
            {
                Success = true,
                CartInfo = null,
                Cates = BLL.CateProvider.GetCates(DBContext),
                ErrMsg = string.Empty,
                Prods = BLL.ProductProvider.GetProducts(DBContext),
                ToUrl = string.Empty,
                UserInfo = null
            };
        }
    }
}
