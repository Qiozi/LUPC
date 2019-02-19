using System.Collections.Generic;
using System.Linq;

namespace SiteDB
{
    public class ProdCateHelper
    {

        public ProdCateHelper() { }

        /// <summary>
        /// 取得商品目录列表
        /// 
        /// </summary>
        /// <param name="CateId"></param>
        /// <param name="Showit"></param>
        /// <returns></returns>
        public static IQueryable<tb_product_category> GetProdCates(int CateId, sbyte Showit, nicklu2Entities db)
        {
            var list = from p in db.tb_product_category
                       where p.menu_pre_serial_no.HasValue && p.menu_pre_serial_no.Value.Equals(CateId)
                       && p.tag.HasValue && p.tag.Value.Equals(Showit)
                       select p;
           
            return list;
        }
    }
  
}
