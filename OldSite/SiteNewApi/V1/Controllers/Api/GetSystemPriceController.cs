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
    /// 取得系统的价格信息
    /// </summary>
    public class GetSystemPriceController : BaseApiController
    {
        /// <summary>
        /// 获取系统价格信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public PostResult Get(int id)
        {
            var query = LU.BLL.ProductProvider.GetSingleSystemPrice(DBContext, id, UserInfo.CountryType);
            return new PostResult
            {
                Data = query,
                Success = true
            };
        }
    }
}
