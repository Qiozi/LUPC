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
    /// 用户注册（简易）
    /// </summary>
    public class SignUpController : BaseApiController
    {
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="email">email</param>
        /// <param name="pwd1">password 1</param>
        /// <param name="pwd2">password 2</param>
        /// <param name="returnUrl">登入后返回路径</param>
        /// <returns></returns>
        public PostResult Get(string email, string pwd1, string pwd2, string returnUrl)
        {
            var errMsg = string.Empty;
            var query = LU.BLL.Users.Account.Register(DBContext, email, pwd1, pwd2, returnUrl, ClientIp, out errMsg);
            return new PostResult
            {
                Data = query,
                ErrMsg = errMsg,
                Success = string.IsNullOrEmpty(errMsg),
                ToUrl = returnUrl
            };
        }
    }
}
