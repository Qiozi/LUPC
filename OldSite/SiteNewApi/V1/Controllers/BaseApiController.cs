using LU.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace V1.Controllers
{
    /// <summary>
    /// base controller
    /// </summary>
    public class BaseApiController : ApiController
    {
        /// <summary>
        /// 用户基本信息
        /// </summary>
        public LU.Model.ModelV1.UserInfo UserInfo { get; set; }

        /// <summary>
        /// 数据库访问实例 
        /// </summary>
        public nicklu2Entities DBContext { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public BaseApiController()
        {
            this.DBContext = new nicklu2Entities();

        }

        /// <summary>
        /// 
        /// </summary>
        ~BaseApiController()
        {
           
        }
        /// <summary>
        /// 客户端Ip
        /// </summary>
        public string ClientIp
        {
            get
            {
                return ((System.Web.HttpContextWrapper)Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }
        }
    }
}
