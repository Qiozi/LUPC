using SiteApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SiteApi.Controllers
{
    [CmdFilter]
    public class LoginController : BaseApiController
    {
        //
        // GET: /Account/

        public class PostInfo
        {
            public string t { get; set; }
            public string Username { set; get; }

            public string Pwd { get; set; }

        }

        public Models.PostResult Post([FromBody]PostInfo value)
        {
            var valid = Validate(value.t);
            if (!valid.Success)
            {
                return valid;
            }

            var user = DBContext.tb_staff.SingleOrDefault(u => u.staff_login_name.Equals(value.Username) &&
                u.staff_password.Equals(value.Pwd));

            return user == null ?
                new Models.PostResult
                {
                    Success = false,
                    ErrMsg = "no find user info."
                } :
                new Models.PostResult
                {
                    Success = true
                };
        }
    }
}
