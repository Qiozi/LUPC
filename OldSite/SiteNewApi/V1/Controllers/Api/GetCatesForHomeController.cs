using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace V1.Controllers
{
    /// <summary>
    /// 首页商品类别信息
    /// </summary>
    public class GetCatesForHomeController : BaseApiController
    {
        /// <summary>
        /// 获取商品类别
        /// </summary>
        /// <returns></returns>
        public LU.Model.PostResult Get()
        {
            var context = new LU.Data.nicklu2Entities();
            var query = LU.BLL.CateProvider.GetCates(DBContext);
            return new LU.Model.PostResult
            {
                Data = query,
                Success = true
            };
        }
    }
}
