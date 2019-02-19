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
    /// 获取系统零件信息
    /// </summary>
    public class GetSystemPartForCustomizeController : BaseApiController
    {
        /// <summary>
        /// 获取系统零件信息
        /// </summary>
        /// <param name="sku">system sku</param>
        /// <returns></returns>
        public PostResult Get(int sku)
        {
            var context = new LU.Data.nicklu2Entities();
            var query = LU.BLL.ProductProvider.GetSystemPartsForCustmize(DBContext, sku, LU.Model.Enums.CountryType.CAD);
            return new LU.Model.PostResult
            {
                Data = query,
                Success = true
            };
        }
    }
}
