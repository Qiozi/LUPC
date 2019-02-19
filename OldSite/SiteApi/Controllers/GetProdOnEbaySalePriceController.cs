using SiteApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteApi.Controllers
{
    [CmdFilter]
    public class GetProdOnEbaySalePriceController : BaseApiController
    {
        //
        // GET: /GetProdOnEbaySalePrice/

        public Models.PostResult Get(string t, int sku)
        {
            var query = DBContext.tb_ebay_promotional_items.FirstOrDefault(p => p.luc_sku.Equals(sku));
            return new Models.PostResult
            {
                Success = true,
                Data = query == null ? "N/A" : query.SavePrice.ToString()
            };
        }

    }
}
