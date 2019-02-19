using LU.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using V1.Filters;

namespace V1.Controllers
{
    /// <summary>
    /// 运输公司 
    /// </summary>
    [LogonApi(true)]
    public class ShippingCompanyController : BaseApiController
    {
        /// <summary>
        /// 获取运输公司信息
        /// </summary>
        /// <returns></returns>
        public PostResult Get()
        {
            var query = LU.BLL.ShoppingCartProvider.GetShippingCompany(DBContext);
            return new PostResult
            {
                Data = query,
                Success = true
            };
        }
    }
}
