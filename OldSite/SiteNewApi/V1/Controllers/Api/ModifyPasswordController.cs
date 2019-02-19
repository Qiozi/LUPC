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
    /// 修改密码
    /// </summary>
    public class ModifyPasswordController : BaseApiController
    {
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="oldPwd"></param>
        /// <param name="newPwd1"></param>
        /// <param name="newPwd2"></param>
        /// <returns></returns>
        public PostResult Get(string oldPwd, string newPwd1, string newPwd2)
        {
            var errMsg = string.Empty;
            var query = LU.BLL.Users.Account.ModifyPassword(DBContext, UserInfo.Id, oldPwd, newPwd1, newPwd2, out errMsg);
            return new PostResult
            {
                Data = query,
                ErrMsg = errMsg,
                Success = string.IsNullOrEmpty(errMsg)
            };
        }
    }
}
