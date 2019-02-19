using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SiteApi.Controllers
{
    public class GetImagePathController : ApiController
    {
        //
        // GET: /GetImagePath/
        //data-original=""https://o9ozc36tl.qnssl.com/{0}.jpg?imageView/3/w/135/h/135""
        //https://lucomputers.com/pro_img/ebay_gallery/{6}/{0}_ebay_list_t_1.jpg
        public string Get(int sku, int w = 300, int h = 300, int i = 0)
        {
            var host = "https://www.lucomputers.com/";
            if (w == 300)
            {
                return string.Format(@"{1}pro_img/components/{0}_list_1.jpg", sku, host);
            }
            if (w > 300)
            {
                return string.Format(@"{1}pro_img/source_components/{0}.jpg", sku, host);
            }
            return string.Format("https://o9ozc36tl.qnssl.com/{0}{1}.jpg?imageView/3/w/{2}/h/{3}"
                , sku
                , i == 0 ? "" : "_" + i
                , w
                , h);
        }

    }
}
