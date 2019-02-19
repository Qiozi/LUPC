using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Specifications.Controllers
{
    public class CateController : BaseController
    {
        // GET: Cate
        public JsonResult Index()
        {
            var query = LU.BLL.CateProvider.GetCates(DBContext);

            var result = new List<LU.Model.Cate>();
            foreach (var c in query)
            {
                foreach (var sub in c.SubCates)
                {
                    if (!string.IsNullOrEmpty(sub.IconName2))
                    {
                        result.Add(new LU.Model.Cate
                        {
                            CateType = sub.CateType,
                            IconName = string.Concat("#", sub.IconName2),
                            ParentId = sub.ParentId,
                            Title = sub.Title,
                            ViewForHome = sub.ViewForHome,
                            Id = sub.Id
                        });
                    }
                }
            }
            return Json(new LU.Model.PostResult
            {
                Data = result,
                Success = true,
                ErrMsg = string.Empty,
                ToUrl = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }
    }
}