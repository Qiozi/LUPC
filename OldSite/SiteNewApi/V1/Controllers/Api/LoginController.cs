using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace V1.Controllers
{
    /// <summary>
    /// 用户登入
    /// </summary>
    public class LoginController : ApiController
    {
        /// <summary>
        /// 传入参数
        /// </summary>
        public class Item
        {
            /// <summary>
            /// 用户名称: email
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// 密码
            /// </summary>
            public string Pwd { get; set; }

            /// <summary>
            /// 登入后跳转路径
            /// </summary>
            public string ReturnUrl { get; set; }
        }

        /// <summary>
        /// 用户登入
        /// </summary>
        /// <returns></returns>
        public LU.Model.PostResult Post([FromBody]Item item)
        {
            var context = new LU.Data.nicklu2Entities();
            var errMsg = string.Empty;
            var query = LU.BLL.Users
                          .Account.Login(context,
                                        item.UserName,
                                        item.Pwd,
                                        item.ReturnUrl,
                                        string.Empty,// TODO
                                        out errMsg);

            return new LU.Model.PostResult
            {
                Data = query,
                Success = !string.IsNullOrEmpty(query),
                ErrMsg = string.IsNullOrEmpty(query) ? errMsg : string.Empty
            };
        }
    }
}
