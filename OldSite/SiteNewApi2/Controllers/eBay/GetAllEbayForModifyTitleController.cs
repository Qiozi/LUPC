using LU.Model.eBay;
using LU.Model.Share;
using SiteNewApi2.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SiteNewApi2.Controllers
{
    [LogonApi(true)]
    public class GetAllEbayForModifyTitleController : BaseApiController
    {
        public PostResult Post([FromBody]eBayPartItem item)
        {
            try
            {
                var res = new LU.BLL.eBay.ModifyEbayInfoBLL(DBContext).GetAllEbayForModifyTitle();
                return Done.End(res, string.Empty);
            }
            catch (Exception ex)
            {
                Done.WriteErrorLog(ex);
                return Done.Error(ex.Message);
            }
        }
    }
}
