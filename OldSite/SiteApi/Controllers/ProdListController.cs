using SiteApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteApi.Controllers
{
    [CmdFilter]
    public class ProdListController : BaseApiController
    {
        //
        // GET: /ProdList/

        public Models.PostResult Get()
        {
            //return "Hello";
            var prod = DBContext.tb_product.First();
            return new Models.PostResult
            {
                Success = true,
                Data = prod.product_name
            };
        }

    }
}
