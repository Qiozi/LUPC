using SiteApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Xml;

namespace SiteApi.Controllers
{
    [CmdFilter]
    public class GeteBayTimeController : BaseApiController
    {
        //
        // GET: /GeteBayTime/

        public Models.PostResult Get()
        {
            var query = DBContext.tb_ebay_send_xml_result_history.OrderByDescending(p => p.id).FirstOrDefault(p => p.regdate.HasValue);
            if (query != null)
            {
                var content = query.Content;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(query.Content);
                var ebayTime = DateTime.Parse(doc.ChildNodes[1].FirstChild.InnerText.Replace("T", " ").Replace("Z", ""));
                var diff = DateTime.Now - query.regdate.Value;

                return new Models.PostResult
                {
                    Success = true,
                    Data = (ebayTime + diff).ToString("yyyy-MM-dd HH:mm:ss"),
                    ErrMsg = string.Empty
                };
            }
            return new Models.PostResult
            {
                Success = false,
                ErrMsg = "no data.",
                Data = null
            };
        }

    }
}
