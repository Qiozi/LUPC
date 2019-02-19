using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteDB
{
    public  class HelperOrder
    {
        public HelperOrder() { }

        /// <summary>
        /// 保存临时订单
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="IsSys"></param>
        /// <param name="db"></param>
        /// <returns>返回订单商品数量</returns>
        public static int SaveTmpOrder(int sku, bool IsSys, int orderCode, string IP, nicklu2Entities db)
        {
            tb_cart_temp cart = tb_cart_temp.Createtb_cart_temp(0);
            cart.cart_temp_code = orderCode;
            cart.product_serial_no = sku;
            cart.ip = IP;
            cart.create_datetime = DateTime.Now;
            cart.cart_temp_Quantity = 1;
            db.AddTotb_cart_temp(cart);
            db.SaveChanges();

            return db.tb_cart_temp.Count(p => p.cart_temp_code.HasValue && p.cart_temp_code.Value.Equals(orderCode));
        }

        /// <summary>
        /// 取得购物车订单有几个商品
        /// 
        /// </summary>
        /// <param name="orderCode"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static int GetTmpOrderListQty(int orderCode, nicklu2Entities db)
        {
            if (orderCode < 100000)
                return 0;

            return db.tb_cart_temp.Count(p => p.cart_temp_code.HasValue && p.cart_temp_code.Value.Equals(orderCode));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="OrderCode"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static List<tb_cart_temp> GetTmpOrderList(int OrderCode, nicklu2Entities db)
        {
            var list = db.tb_cart_temp.Where(p => p.cart_temp_code.HasValue && p.cart_temp_code.Value.Equals(OrderCode)).ToList();
            foreach (var m in list)
            {
               // m.price = ProdHelper.GetProdSell(m.product_serial_no, db);

            }

            return list;
        }
    }
}
