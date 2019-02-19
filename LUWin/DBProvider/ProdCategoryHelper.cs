using LUComputers.DBProvider;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace LUComputers.DBProvider
{
    public class ProdCategoryHelper
    {
        public ProdCategoryHelper() { }

        /// <summary>
        /// 排除的类型
        /// </summary>
        /// <returns></returns>
        public List<int> NotWatchCategoryIds()
        {
            return new List<int>() { 216, 201, 332 };
        }

        public string GetAllValidateCategorySQL()
        {
            return @"update tb_product_category set valid =0;
update tb_product_category set valid=1 where menu_child_serial_no in (


select menu_child_serial_no from (
select menu_child_serial_no, menu_child_name ,menu_child_order,menu_pre_serial_no, page_category from tb_product_category where tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where  tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where menu_parent_serial_no=1 and menu_pre_serial_no =0 and tag=1) and menu_is_exist_sub=1)

union all 
select menu_child_serial_no, menu_child_name ,menu_child_order,menu_pre_serial_no, page_category  from tb_product_category where menu_parent_serial_no=1  and tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where menu_parent_serial_no=1 and menu_pre_serial_no =0 and tag=1) and menu_is_exist_sub=0) tmp order by menu_pre_serial_no asc 
);

select * from (
select menu_child_serial_no, menu_child_name ,menu_child_order,menu_pre_serial_no, page_category from tb_product_category where tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where  tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where menu_parent_serial_no=1 and menu_pre_serial_no =0 and tag=1) and menu_is_exist_sub=1)

union all 
select menu_child_serial_no, menu_child_name ,menu_child_order,menu_pre_serial_no, page_category  from tb_product_category where menu_parent_serial_no=1  and tag=1 and  menu_pre_serial_no in (
select menu_child_serial_no  from tb_product_category where menu_parent_serial_no=1 and menu_pre_serial_no =0 and tag=1) and menu_is_exist_sub=0) tmp order by menu_pre_serial_no asc 

";
        }

        /// <summary>
        /// 获取所有有效的类型 
        /// </summary>
        /// <returns></returns>
        public List<int> GetAllValidCategory()
        {
            var res = new List<int>();
            var sql = GetAllValidateCategorySQL();

            var dt = Config.RemoteExecuteDateTable(sql);

            foreach (DataRow dr in dt.Rows)
            {
                res.Add(int.Parse(dr["menu_child_serial_no"].ToString()));
            }
            return res;
        }
    }
}
