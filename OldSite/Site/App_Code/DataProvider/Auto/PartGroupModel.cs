// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-15 0:07:11
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;

[Serializable]
public class PartGroupModel
{
    public static tb_part_group GetPartGroupModel(nicklu2Entities context, int id)
    {
        return context.tb_part_group.Single(me => me.part_group_id.Equals(id));
    }


    public static tb_part_group[] GetPartGroupModelsByProductCategory(nicklu2Entities context, int product_category)
    {
        // return PartGroupModel.FindAllByProperty("product_category", product_category);
        var query = context.tb_part_group.Where(me => me.product_category.Value.Equals(product_category)).ToList();
        return query.ToArray();        
    }

    /// <summary>
    /// 查询
    /// </summary>
    /// <param name="part_group_id"></param>
    /// <returns></returns>
    public static int GetOrderByPartGroupID(int part_group_id)
    {
        DataTable dt = Config.ExecuteDataTable(@"select max(pc.menu_child_order) from tb_part_group_detail pg inner join tb_product p on pg.product_serial_no=p.product_serial_no 
inner join tb_product_category pc on pc.menu_child_serial_no=p.menu_child_serial_no
where part_group_id=" + part_group_id);
        if (dt.Rows.Count == 1)
            return int.Parse(dt.Rows[0][0].ToString());
        return 1;
    }

    /// <summary>
    /// get group of Category
    /// </summary>
    /// <param name="part_id"></param>
    /// <param name="is_ebay"></param>
    /// <returns></returns>
    public static DataTable FindGroupByPartID(int part_id, bool? is_ebay,bool IsOnlyShow)
    {
        string sql = "";
        if (is_ebay == null)
        {
            sql = @"select pg.part_group_id,pg.part_group_comment, pg.product_category
from tb_part_group pg, tb_product p , tb_product_category pc 
where p.menu_child_serial_no=pc.menu_child_serial_no " + (IsOnlyShow ? " and pg.showit=1 " : "") + " and (pg.product_category=pc.menu_child_serial_no or pg.product_category=pc.menu_pre_serial_no) and p.product_serial_no='" + part_id + "'";
        }
        else if (is_ebay == true)
        {
            sql = @"select pg.part_group_id,pg.part_group_comment, pg.product_category
from tb_part_group pg, tb_product p , tb_product_category pc 
where pg.is_ebay=1 and pg.showit=1 and p.menu_child_serial_no=pc.menu_child_serial_no " + (IsOnlyShow ? " and pg.showit=1 " : "") + " and (pg.product_category=pc.menu_child_serial_no or pg.product_category=pc.menu_pre_serial_no) and p.product_serial_no='" + part_id + "'";
        
        }
        else
        {
            sql = @"select pg.part_group_id,pg.part_group_comment, pg.product_category
from tb_part_group pg, tb_product p , tb_product_category pc 
where pg.is_ebay=0 and p.menu_child_serial_no=pc.menu_child_serial_no " + (IsOnlyShow ? " and pg.showit=1 " : "") + " and (pg.product_category=pc.menu_child_serial_no or pg.product_category=pc.menu_pre_serial_no) and p.product_serial_no='" + part_id + "'";
        
        }
        return Config.ExecuteDataTable(sql);
    }

    public static DataTable FindGroupByPartID(int part_id)
    {
        return FindGroupByPartID(part_id, null, false);
    }

    public static DataTable FindPartGroupIDByPart(int part_id)
    {
        return Config.ExecuteDataTable(@"select distinct part_group_id, showit from tb_part_group_detail where product_serial_no='" + part_id + "' ");
    }

    public DataTable FindModelByAll()
    {
        return Config.ExecuteDataTable(@"
select * from (
select -1 part_group_id, ' -- select ---' part_group_comment, 0 menu_child_order , '' part_group_name
union all 
select part_group_id , concat(concat(part_group_name, ' -- '), part_group_comment) part_group_comment, menu_child_order,  part_group_name from tb_part_group pg inner join tb_product_category pc on pc.menu_child_serial_no=pg.product_category ) tmp order by part_group_comment asc ");
    }

}
