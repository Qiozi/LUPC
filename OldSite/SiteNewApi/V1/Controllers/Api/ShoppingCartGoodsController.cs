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
    /// 获取购物车商品信息
    /// </summary>
    [V1.Filters.LogonApi(true)]
    public class ShoppingCartGoodsController : BaseApiController
    {
        /// <summary>
        /// 获取购物车商品信息
        /// </summary>
        /// <returns></returns>
        public PostResult Get()
        {
            if (UserInfo.ShoppingInfo == null || UserInfo.ShoppingInfo.OrderCode < 1)
            {
                return new PostResult
                {
                    Data = null,
                    Success = true,
                    ErrMsg = string.Empty
                };
            }
            else
            {
                var query = LU.BLL.ShoppingCartProvider.GetShoppingCartGoodsList(DBContext, UserInfo.ShoppingInfo.OrderCode, UserInfo.Id, UserInfo.CountryType, LU.BLL.ConfigV1.Rate);
                return new PostResult
                {
                    Data = query,
                    Success = true,
                    ErrMsg = string.Empty
                };
            }

        }

        /// <summary>
        /// 删除购物车商品
        /// </summary>
        /// <param name="id"></param>
        public PostResult Delete(int id)
        {
            var query = LU.BLL.ShoppingCartProvider.Del(DBContext, id);
            if(query)
            {
                var goods = LU.BLL.ShoppingCartProvider.GetShoppingCartGoodsList(DBContext, UserInfo.ShoppingInfo.OrderCode, UserInfo.Id, UserInfo.CountryType, LU.BLL.ConfigV1.Rate);
                return new PostResult
                {
                    Data = goods,
                    Success = true,
                    ErrMsg = string.Empty
                };
            }
            else
            {
                return new PostResult
                {
                    Success = false,
                    Data = false,
                    ErrMsg = "Shopping cart update is fail."
                };
            }
        }
    }
}
