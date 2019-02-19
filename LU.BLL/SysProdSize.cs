using LU.Data;
using LU.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LU.BLL
{
    /// <summary>
    /// Summary description for SysProdSize
    /// </summary>
    public class SysProdSize
    {
        public SysProdSize()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// 取得产品尺寸 
        /// 
        /// </summary>
        /// <param name="price"></param>
        /// <param name="pt"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int GetSize(decimal price, ProdType pt, nicklu2Entities db)
        {
            var ps = db.tb_product_size.FirstOrDefault(p => p.product_type.HasValue
                && p.product_type.Value.Equals((int)pt)
                && p.begin_price.HasValue
                && price.CompareTo(p.begin_price.Value) == 1
                && p.end_price.HasValue
                && price.CompareTo(p.end_price.Value) < 0);
            // throw new Exception(ps.product_size_id.ToString());
            if (ps != null)
                return ps.product_size_id;
            return -1;
        }
    }
}