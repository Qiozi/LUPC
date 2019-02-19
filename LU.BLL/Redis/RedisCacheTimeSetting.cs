using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Center.BLL.Redis
{
    public class RedisCacheTimeSetting
    {
        public static string WeiXinLoginUserInfo = "WeiXinLoginUserInfo";

        public static string LoginInfo = "LoginUserInfo";

        public static string UserTokenALL = "UserTokenALL";

        public static string AllUserName = "AllUserName";

        public static string QueueVisit = "QueueVisit";

        /// <summary>
        /// 微信登入后，所存储的用户信息。缓存1天
        /// </summary>
        public static string WxUserInfoAfterLogin = "WxUserInfoAfterLogin";
    }
}