using SiteApi.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SiteApi.Controllers
{
    [CmdFilter]
    public class GetValidProdController : BaseApiController
    {
        public List<SiteModel.ValidProd> Items { get; set; }
        //
        // GET: /GetValidProd/

        public class Item
        {
            public string t { get; set; }
        }

        public Models.PostResult Get(string t)
        {
            var valid = Validate(t);
            if (!valid.Success)
            {
                return valid;
            }
            var endDate = DateTime.Now.AddDays(6);
            var cateIds = LU.Data.Product.GetValidCategory.GetValidCategoryIds(DBContext);
            var prods = (from c in DBContext.tb_product
                         where c.tag.HasValue && c.tag.Value.Equals(1) &&
                         c.is_non.HasValue && c.is_non.Value.Equals(0) &&
                         c.split_line.HasValue && c.split_line.Value.Equals(0) &&
                         !string.IsNullOrEmpty(c.manufacturer_part_number) &&
                         c.last_regdate.HasValue && c.last_regdate.Value < endDate
                         select new SiteModel.ValidProd
                         {
                             Adjustment = c.adjustment.Value,
                             Brand = c.producter_serial_no,
                             CategoryId = c.menu_child_serial_no.Value,
                             Cost = c.product_current_cost.Value,
                             Discount = c.product_current_discount.Value,
                             Mfp = c.manufacturer_part_number,
                             Price = c.product_current_price.Value,
                             ProdType = c.prodType,
                             Sku = c.product_serial_no,
                             Stock = c.ltd_stock.Value
                         }).ToList();
            return new Models.PostResult
            {
                Success = true,
                Data = prods
            };
        }

    }
}
