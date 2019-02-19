using LU.Data;
using LU.Model.eBay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LU.BLL.eBay
{
    public class ModifyEbayInfoBLL
    {
        private nicklu2Entities _context;

        public ModifyEbayInfoBLL(nicklu2Entities context)
        {
            _context = context;

        }

        public bool SaveEbayTitle()
        {
            return false;
        }

        /// <summary>
        /// 获取所有ebay 在售产品
        /// </summary>
        /// <returns></returns>
        public List<eBayPartItem> GetAllEbayForModifyTitle()
        {
            var sql = string.Format(@"select 
	e.SKU as CustomLabel, 
    e.Title as Title, 
    BuyItNowPrice, 
    ItemId, 
    s.id as Sku, 
    s.category_id as CategoryId ,
    1 as IsSystem,
    pc.category_name as CategoryName
from tb_ebay_selling e 
inner join tb_ebay_system s on e.sys_sku = s.id
left join tb_product_category_new pc on pc.category_id = s.category_id

union all

select 
	e.SKU as CustomLabel, 
    e.Title as Title, 
    BuyItNowPrice, 
    ItemId, 
    s.product_serial_no as Sku, 
    s.menu_child_serial_no as CategoryId ,
    0 as IsSystem,
    pc.menu_child_name as CategoryName
from tb_ebay_selling e 
inner join tb_product s on e.luc_sku = s.product_serial_no
left join tb_product_category pc on pc.menu_child_serial_no = s.menu_child_serial_no");
            var items = _context.Database.SqlQuery<eBayPartItem>(sql).ToList();

            return items;
        }
    }
}
