using SiteApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteApi.Controllers
{
    [CmdFilter]
    public class GetValidMatchSkuController : BaseApiController
    {
        //
        // GET: /GerValidMatchSku/

        public Models.PostResult Get(string t)
        {
            var query = (from c in DBContext.tb_other_inc_match_lu_sku
                         select new
                         {
                             c.lu_sku,
                             c.other_inc_sku,
                             c.other_inc_type,
                             c.prodType
                         }).ToList().Distinct();
            return new Models.PostResult
            {
                Success = true,
                ErrMsg = string.Empty,
                Data = query
            };
        }
    }
}
