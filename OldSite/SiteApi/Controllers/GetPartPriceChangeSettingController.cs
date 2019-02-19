using SiteApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SiteApi.Controllers
{
    [CmdFilter]
    public class GetPartPriceChangeSettingController : BaseApiController
    {
        //
        // GET: /GetPartPriceChangeSetting
        public Models.PostResult Get(string t)
        {
            var items = (from c in DBContext.tb_part_price_change_setting
                         select new SiteModel.PartPriceChangeSetting
                         {
                             CateId = c.category_id.Value,
                             CostMax = c.cost_max.Value,
                             CostMin = c.cost_min.Value,
                             IsPercent = c.is_percent.Value,
                             Rate = c.rate.Value
                         }).ToList();
            return new Models.PostResult
            {
                Success = true,
                Data = items
            };
        }

    }
}
