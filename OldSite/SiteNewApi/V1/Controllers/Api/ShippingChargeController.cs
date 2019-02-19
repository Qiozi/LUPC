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
    /// 运费
    /// </summary>
    public class ShippingChargeController : BaseApiController
    {
        /// <summary>
        /// 计算运费， 
        /// </summary>
        /// <param name="shippingId">运输公司 Id</param>
        /// <param name="stateId">收货地址所在地区 Id</param>
        /// <param name="paymentId">支付方式 Id</param>
        /// <returns></returns>
        public PostResult Get(int shippingId, int stateId, int paymentId)
        {
            var query = LU.BLL.ShoppingCartProvider.GetOrderCharge(DBContext, UserInfo.ShoppingInfo.OrderCode, shippingId, stateId, paymentId);
            return new PostResult
            {
                Data = query,
                Success = true
            };
        }
    }
}
