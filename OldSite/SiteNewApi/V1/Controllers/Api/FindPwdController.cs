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
    /// 找回密码
    /// </summary>
    public class FindPwdController : BaseApiController
    {
        /// <summary>
        /// 找回密码
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public PostResult Get(string email)
        {
            var errMsg = string.Empty;
            var query = LU.BLL.Users.Account.FindPwd(DBContext, email, this.ClientIp, out errMsg);
            return new PostResult
            {
                Data = string.IsNullOrEmpty(errMsg),
                ErrMsg = errMsg,
                Success = string.IsNullOrEmpty(errMsg)
            };
        }
    }
}
