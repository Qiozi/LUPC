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
    /// 获取商品信息
    /// </summary>
    public class GetGoodsController : BaseApiController
    {
        /// <summary>
        /// 获取商品信息
        /// </summary>
        /// <param name="Id">商品Id(SKU)</param>
        /// <returns></returns>
        public PostResult Get(int Id)
        {
            var context = new LU.Data.nicklu2Entities();
            var query = LU.BLL.ProductProvider.GetProduct(DBContext, Id, LU.Model.Enums.CountryType.CAD, true);
            return new PostResult
            {
                Success = true,
                Data = query
            };
        }
    }
}
