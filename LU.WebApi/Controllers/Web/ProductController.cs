using LU.WebApi.Controllers.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace LU.WebApi.Controllers.Web
{
    public class ProductController : BaseApiController
    {

        public LU.Model.PostResult Get(LU.Model.Enums.GetProdListType t)
        {
            object result = null;
            switch (t)
            {
                case Model.Enums.GetProdListType.forWebHome:
                    result = LU.BLL.ProductProvider.GetProducts(DBContext);
                    break;
            }

            return new Model.PostResult
            {
                Data = result,
                Success = true
            };
        }
    }
}
