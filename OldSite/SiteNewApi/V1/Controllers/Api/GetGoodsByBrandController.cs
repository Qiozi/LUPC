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
    /// 获取品牌下的商品
    /// </summary>
    public class GetGoodsByBrandController : BaseApiController
    {
        /// <summary>
        /// 获取品牌下的商品
        /// </summary>
        /// <param name="brandName">品牌(例: Asus)</param>
        /// <returns></returns>
        public PostResult Get(string brandName)
        {
            var context = new LU.Data.nicklu2Entities();
            var query = LU.BLL.ProductProvider.GetPartsByBrand(DBContext, brandName);
            return new PostResult
            {
                Success = true,
                Data = query
            };
        }
    }
}
