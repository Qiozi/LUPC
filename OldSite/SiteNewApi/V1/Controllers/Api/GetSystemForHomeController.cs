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
    /// 获取首页系统
    /// </summary>
    public class GetSystemForHomeController : BaseApiController
    {
        /// <summary>
        /// 获取首页系统
        /// </summary>
        /// <returns></returns>
        public PostResult Get()
        {
            var context = new LU.Data.nicklu2Entities();
            var systems = LU.BLL.ProductProvider.GetHomeSystemForCustom(DBContext, LU.Model.Enums.CountryType.CAD);
            return new LU.Model.PostResult
            {
                Data = systems,
                Success = true
            };
        }
    }
}
