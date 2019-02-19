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
    /// 支付方法信息
    /// </summary>
    public class PaymentInfoController : BaseApiController
    {
        /// <summary>
        /// 支付方法信息
        /// </summary>
        /// <returns></returns>
        public PostResult Get()
        {
            var query = LU.BLL.ShoppingCartProvider.GetPaymentInfo(DBContext);
            return new PostResult
            {
                Data = query,
                Success = true
            };
        }
    }
}
