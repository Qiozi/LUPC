using LU.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace V1.Controllers
{
    /// <summary>
    /// 获取品牌信息
    /// </summary>
    public class GetBrandInfosController : BaseApiController
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public PostResult Get()
        {
            var query = LU.BLL.ProductProvider.GetBrandForHome(DBContext);

            return new PostResult
            {
                Data = query,
                Success = true
            };
        }
    }
}
