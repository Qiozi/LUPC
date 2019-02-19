// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-15 0:07:11
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_part_group")]
[Serializable]
public class PartGroupModel : ActiveRecordBase<PartGroupModel>
{
    int _part_group_id;
    int _product_category;
    string _part_group_name;
    bool _showit;
    string _part_group_comment;
    bool _is_ebay = false;

    public PartGroupModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int part_group_id
    {
        get { return _part_group_id; }
        set { _part_group_id = value; }
    }
    public static PartGroupModel GetPartGroupModel(int _part_group_id)
    {
        PartGroupModel[] models = PartGroupModel.FindAllByProperty("part_group_id", _part_group_id);
        if (models.Length == 1)
            return models[0];
        else
            return new PartGroupModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_category
    {
        get { return _product_category; }
        set { _product_category = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string part_group_name
    {
        get { return _part_group_name; }
        set { _part_group_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public bool showit
    {
        get { return _showit; }
        set { _showit = value; }
    }
    [Property]
    public string part_group_comment
    {
        get { return _part_group_comment; }
        set { _part_group_comment = value; }
    }

    [Property]
    public bool is_ebay
    {
        get { return _is_ebay; }
        set { _is_ebay = value; }
    }

    public static PartGroupModel[] GetPartGroupModelsByProductCategory(int product_category)
    {
        return PartGroupModel.FindAllByProperty("product_category", product_category);
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
