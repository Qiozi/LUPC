using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LU.WebApi.Models.Shared
{
    public class WebPostResult : LU.Model.PostResult
    {
        public string ToUrl { get; set; }
        

        public LU.Model.UserInfo UserInfo { get; set; }


        public LU.Model.CartInfo CartInfo { get; set; }
    }
}