using LU.Model.Account;
using System;
using System.Web.Http;

namespace SiteNewApi2.Controllers
{
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 点击请求 token
        /// </summary>
        public Guid HttpToken { get; set; }

        /// <summary>
        /// 用户登入 token
        /// </summary>
        public Guid UserToken { get; set; }

        public LU.Data.nicklu2Entities DBContext { get; set; }

        public PostResultHelper Done { get; set; }

        public UserInfoAdmin UserInfo { get; set; }

        public BaseApiController()
        {
            DBContext = new LU.Data.nicklu2Entities();
        }
        ~BaseApiController()
        {
            DBContext.Dispose();
        }

        public string ClientIp
        {
            get
            {
                return ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
        }

        /// <summary>
        /// 错误信息
        /// </summary>
        /// <param name="errMsg"></param>
        /// <returns></returns>


        public string Authority
        {
            get
            {
                return this.Request.RequestUri.Authority;
            }
        }

        public string Scheme
        {
            get
            {
                return this.Request.RequestUri.Scheme;
            }
        }
    }
}
