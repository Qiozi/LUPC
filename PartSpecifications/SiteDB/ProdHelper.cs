using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SiteDB
{
    public class ProdHelper
    {
        public ProdHelper() { }


        /// <summary>
        /// 取得系统
        /// </summary>
        /// <param name="CateId">父类ID</param>
        /// <param name="showit"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static List<SiteModels.View.SysList> GetSysProd(int CateId
            , bool showit
            , int PageSize
            , int PageIndex
            , nicklu2Entities db)
        {
            List<SiteModels.View.SysList> result = new List<SiteModels.View.SysList>();

            // 只传进父类ID
            List<int> cateids = (from c in db.tb_product_category
                                 where c.menu_pre_serial_no.HasValue && c.menu_pre_serial_no.Value.Equals(CateId)
                                 select c.menu_child_serial_no).ToList();


            var list = (from p in db.tb_ebay_system
                        join pp in db.tb_ebay_system_and_category on p.id equals pp.SystemSku
                        where p.showit.HasValue
                        && p.showit.Value.Equals(showit)
                        && pp.eBaySysCategoryID.HasValue && cateids.Contains(pp.eBaySysCategoryID.Value)
                        select new { p.id, p.system_title1, p.selected_ebay_sell }
                       ).OrderByDescending(p => p.id).Skip(PageSize * PageIndex).Take(PageSize).ToList();
            //throw new Exception(list.Count.ToString());
            foreach (var c in list)
            {
                SiteModels.View.SysList m = new SiteModels.View.SysList();
                m.Name = c.system_title1;
                m.Sku = c.id;
                var sublist = (from p in db.tb_ebay_system_parts
                               join pp in db.tb_product on p.luc_sku.Value equals pp.product_serial_no
                               where p.system_sku.HasValue && p.system_sku.Value.Equals(c.id)
                               && p.is_belong_price.HasValue && p.is_belong_price.Value.Equals(true)
                               && p.luc_sku != 16684
                               select new { p.luc_sku, pp.product_short_name, pp.product_current_price, pp.product_current_discount }).ToList();
                List<SiteModels.View.SysListDetail> slist = new List<SiteModels.View.SysListDetail>();
                int caseSku = 0;
                decimal price = 0;
                decimal discount = 0;
                foreach (var sl in sublist)
                {
                    if (caseSku == 0)
                        caseSku = sl.product_short_name.ToLower().IndexOf("case") > -1 ? sl.luc_sku.Value : 0;
                    SiteModels.View.SysListDetail sld = new SiteModels.View.SysListDetail();
                    sld.PartName = sl.product_short_name;
                    sld.PartSku = sl.luc_sku.Value;
                    slist.Add(sld);

                    price += sl.product_current_price.Value;
                    discount += sl.product_current_discount.Value;
                }
                m.ImgUrl = "http://www.lucomputers.com/pro_img/COMPONENTS/" + caseSku + "_system.jpg";
                m.DetailList = slist;
                m.price = price;
                m.discount = discount;
                m.sell = price - discount;
                result.Add(m);
            }

            return result;
        }

        /// <summary>
        /// 取得零件产品
        /// </summary>
        /// <param name="CateId"></param>
        /// <param name="Showit"></param>
        /// <param name="PageSize"></param>
        /// <param name="PageIndex"></param>
        /// <param name="Keyword"></param>
        public static List<SiteModels.View.ProdList> GetProd(int CateId, sbyte Showit, int PageSize, int PageIndex, nicklu2Entities db)
        {
            List<SiteModels.View.ProdList> result = new List<SiteModels.View.ProdList>();
            List<tb_product> list = new List<tb_product>();

            // 如果没有cateid , 就取on sale
            if (CateId > 0)
                list = (from p in db.tb_product
                        where p.menu_child_serial_no.HasValue && p.menu_child_serial_no.Value.Equals(CateId)
                        && p.tag.HasValue && p.tag.Value.Equals(Showit)
                        select p).OrderByDescending(p => p.product_serial_no).Skip(PageSize * PageIndex).Take(PageSize).ToList();
            else
                list = (from p in db.tb_product
                        where p.tag.HasValue && p.tag.Value.Equals(Showit)
                        && p.product_current_discount > 0
                        select p).OrderByDescending(p => p.product_serial_no).Skip(PageSize * PageIndex).Take(PageSize).ToList();

            foreach (var m in list)
            {
                SiteModels.View.ProdList prod = new SiteModels.View.ProdList();
                prod.CateId = (int) m.menu_child_serial_no;
                prod.Name = (m.product_ebay_name??"").Length < 5 ? ((m.product_name_long_en??"").Length < 5 ? (m.product_name??"") : (m.product_name_long_en??"")) : m.product_ebay_name;
                prod.Name = prod.Name.Replace("'", "\\'");
                prod.ImgUrl = GetImgUrl(m);// "http://www.lucomputers.com/pro_img/COMPONENTS/26457_t.jpg";
                prod.Sku = m.product_serial_no;
                prod.discount = m.product_current_discount.Value;
                prod.price = m.product_current_price.Value;
                prod.sell = m.product_current_price.Value - m.product_current_discount.Value;
                result.Add(prod);
            }

            return result;
        }

        /// <summary>
        /// 获取图片地址

        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        static string GetImgUrl(tb_product p)
        {
            return p.other_product_sku > 0 ? "http://www.lucomputers.com/pro_img/COMPONENTS/" + p.other_product_sku + "_t.jpg" : "http://www.lucomputers.com/pro_img/COMPONENTS/" + p.product_serial_no + "_t.jpg";
        }


        /// <summary>
        /// 取得单个商品的价格
        /// 
        /// </summary>
        /// <param name="sku"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public static decimal GetProdSell(int sku, nicklu2Entities db)
        {
            var sell =  (from p in db.tb_product
                   where p.product_serial_no.Equals(sku)
                   select new { price = p.product_current_price.Value - p.product_current_discount.Value }).OrderBy(p=>p.price).Take(1);
        
            foreach (var s in sell)
                return s.price;
            return 0;
        }
    }
}
