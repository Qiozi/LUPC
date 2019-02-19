using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Specifications.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Product
        public JsonResult Index(int cid = 0)
        {
            var cates = LU.BLL.CateProvider.GetCates(DBContext).ToList();
            var cateIds = new List<int>();
            if (cid == 0)
            {
                foreach (var cate in cates)
                {
                    foreach (var subcate in cate.SubCates)
                        cateIds.Add(subcate.Id);
                }
            }
            else
            {
                cateIds.Add(cid);
            }
            var query = LU.BLL.ProductProvider.GetAllProducts(DBContext, 0, LU.Model.Enums.CountryType.CAD, cateIds.ToArray());
            if (cid == 0)
            {
                query = query.Where(p => !string.IsNullOrEmpty(p.eBayCode)).OrderByDescending(p => p.Sku).Take(30).ToList();
            }

            return Json(new LU.Model.PostResult
            {
                Data = query,
                Success = true,
                ErrMsg = string.Empty,
                ToUrl = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Detail(int sku)
        {
            var prod = LU.BLL.ProductProvider.GetProduct(DBContext, sku, LU.Model.Enums.CountryType.CAD);
            return Json(new LU.Model.PostResult
            {
                Data = prod != null ? prod : null,
                ErrMsg = prod == null ? null : "No find data.",
                Success = prod != null,
                ToUrl = string.Empty
            }, JsonRequestBehavior.AllowGet);
        }
    }
}