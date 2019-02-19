using LU.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using V1.Areas.HelpPage.ModelDescriptions;
using V1.Filters;

namespace V1.Controllers
{
    /// <summary>
    /// 获取购物车商品数量
    /// </summary>
    [LogonApi(true)]
    public class ShoppingCartQtyController : BaseApiController
    {
        /// <summary>
        /// 购物车商品数量参数
        /// </summary>
        [ModelName("ShoppingCart Goods Qty params")]
        public class Item
        {
            /// <summary>
            /// Id
            /// </summary>
            public int Id { get; set; }

            /// <summary>
            /// 数量
            /// </summary>
            public int Qty { get; set; }
        }
        /// <summary>
        /// 获取购物车商品数量
        /// </summary>
        /// <returns></returns>
        public PostResult Get()
        {
            var query = LU.BLL.ShoppingCartProvider.GetShoppingCartQty(DBContext, UserInfo.ShoppingInfo.OrderCode);
            return new PostResult
            {
                Data = query,
                Success = true
            };
        }

        /// <summary>
        /// 保存商品数量
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public PostResult Post([FromBody]Item item)
        {
            var query = LU.BLL.ShoppingCartProvider.ChangeQty(DBContext, item.Id, item.Qty);
            if (query)
            {
                var goods = LU.BLL.ShoppingCartProvider.GetShoppingCartGoodsList(DBContext, UserInfo.ShoppingInfo.OrderCode, UserInfo.Id, UserInfo.CountryType, LU.BLL.ConfigV1.Rate);
                return new PostResult
                {
                    Data = goods,
                    Success = true
                };
            }
            else
            {
                return new PostResult
                {
                    Data = false,
                    Success = false,
                    ErrMsg = "Goods quantity update is fail."
                };
            }
        }
    }
}
