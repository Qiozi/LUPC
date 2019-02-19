using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SiteApi.Controllers
{
    public class BaseApiController : ApiController
    {
        public bool LoginCmd { get; set; }

        public SiteDB.nicklu2Entities DBContext { get; set; }

        public BaseApiController()
        {
            this.DBContext = new SiteDB.nicklu2Entities();
        }

        ~BaseApiController()
        {
            this.DBContext.Dispose();
        }

        public Models.PostResult Validate(string t)
        {
            var query = DBContext.tb_exchange.ToList();
            foreach (var item in query)
            {
                if (DateTime.Now.Subtract(item.Regdate).TotalHours >= 3)
                {
                    DBContext.tb_exchange.Remove(item);
                }
            }
            DBContext.SaveChanges();

            var exchange = DBContext.tb_exchange.SingleOrDefault(p => p.Pwd.Equals(t));
            return exchange == null ? new Models.PostResult
            {
                Success = false,
                ErrMsg = "No token"
            } : new Models.PostResult
            {
                Success = true
            };
        }

    }
}
