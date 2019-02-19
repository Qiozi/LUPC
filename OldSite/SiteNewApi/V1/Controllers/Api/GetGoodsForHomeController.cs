using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace V1.Controllers
{
    /// <summary>
    /// 首页促销商品信息
    /// </summary>
    public class GetGoodsForHomeController : BaseApiController
    {
        /// <summary>
        /// 获取促销商品
        /// </summary>
        /// <returns></returns>
        public LU.Model.PostResult Get()
        {
            try
            {
                var context = new LU.Data.nicklu2Entities();
                var query = LU.BLL.ProductProvider.GetHomeProducts(DBContext, LU.Model.Enums.CountryType.CAD);
                return new LU.Model.PostResult
                {
                    Data = query,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new LU.Model.PostResult
                {
                    ErrMsg = ex.Message,
                    Data = null,
                    Success = false
                };
            }
        }
    }
}
