// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-15 1:15:32
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_part_group_detail")]
[Serializable]
public class PartGroupDetailModel : ActiveRecordBase<PartGroupDetailModel>
{
    int _part_group_detail_id;
    int _part_group_id;
    int _product_serial_no;
    bool _showit;
    int _nominate;

    public PartGroupDetailModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int part_group_detail_id
    {
        get { return _part_group_detail_id; }
        set { _part_group_detail_id = value; }
    }
    public static PartGroupDetailModel GetPartGroupDetailModel(int _part_group_detail_id)
    {
        PartGroupDetailModel[] models = PartGroupDetailModel.FindAllByProperty("part_group_detail_id", _part_group_detail_id);
        if (models.Length == 1)
            return models[0];
        else
            return new PartGroupDetailModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int part_group_id
    {
        get { return _part_group_id; }
        set { _part_group_id = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_serial_no
    {
        get { return _product_serial_no; }
        set { _product_serial_no = value; }
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
    public int nominate
    {
        get { return _nominate; }
        set { _nominate = value; }
    }
    public static PartGroupDetailModel[] GetPartGroupDetailModelsByPartGroupID(int part_group_id)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("showit", 1);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("eq2", part_group_id);
        NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        return PartGroupDetailModel.FindAll(a);
    }

//    public static DataTable GetPartGroupDetailByPartID(int part_group_id, Showit st)
//    {
//        string sql = @"select '-1' as product_serial_no, 'Recommended' as product_name, '1' as split_line, '-1' as part_group_detail_id, '1' as showit, '1' as nominate , '' as  product_short_name, '1' as product_order
//        union all
//";
//        sql += "select * from (select p.product_serial_no, p.product_name, p.split_line, pg.part_group_detail_id, pg.showit, pg.nominate, p.product_short_name, p.product_order from tb_product p inner join tb_part_group_detail pg on p.product_serial_no=pg.product_serial_no where p.tag=1  and part_group_id=" + part_group_id;
//        if (st == Showit.show_true)
//            sql += " and pg.showit=1";
//        sql += " and pg.nominate=1  order by p.product_order asc) as t1";
//        sql += @"
//union all
//            ";

//        sql += "select * from (select p.product_serial_no, p.product_name, p.split_line, pg.part_group_detail_id, pg.showit, pg.nominate,p.product_short_name,p.product_order from tb_product p inner join tb_part_group_detail pg on p.product_serial_no=pg.product_serial_no where p.tag=1  and part_group_id=" + part_group_id;
//        if (st == Showit.show_true)
//            sql += " and pg.showit=1 ";

//       // sql += " and pg.nominate=0 ";
//        sql += " order by p.product_order asc) as t2";
//        //throw new Exception(sql);
//        return Config.ExecuteDataTable(sql);
//    }

    public static DataTable GetPartGroupDetailByPartID(int part_group_id, Showit st)
    {
        string sql = @"select '-1' as product_serial_no, 'Recommended' as product_name, '1' as split_line, '-1' as part_group_detail_id, '1' as showit, '1' as nominate , '' as  product_short_name, '1' as product_order
        union all
";
        sql += @"select * from (select p.product_serial_no, p.product_name, p.split_line, pg.part_group_detail_id, pg.showit, pg.nominate, p.product_short_name, p.product_order from tb_product p inner join tb_part_group_detail pg on p.product_serial_no=pg.product_serial_no inner join tb_product_category pc on 
        pc.menu_child_serial_no = p.menu_child_serial_no where p.tag=1  and part_group_id=" + part_group_id;
        if (st == Showit.show_true)
            sql += " and pg.showit=1";
        sql += " and pg.nominate=1  order by pc.menu_child_order, p.product_order asc) as t1";
        sql += @"
union all
            ";

        sql += @"select * from (select p.product_serial_no, p.product_name, p.split_line, pg.part_group_detail_id, pg.showit, pg.nominate,p.product_short_name,p.product_order from tb_product p inner join tb_part_group_detail pg on p.product_serial_no=pg.product_serial_no inner join tb_product_category pc on 
       pc.menu_child_serial_no = p.menu_child_serial_no where p.tag=1  and part_group_id=" + part_group_id;
        if (st == Showit.show_true)
            sql += " and pg.showit=1 ";

        // sql += " and pg.nominate=0 ";
        sql += " order by pc.menu_child_order, p.product_order asc) as t2";
        //throw new Exception(sql);
        return Config.ExecuteDataTable(sql);
    }

    public static bool IsExistProduct(int part_group_id, int product_id)
    {
        DataTable dt = Config.ExecuteDataTable("select * from tb_part_group_detail where part_group_id="+ part_group_id+" and product_serial_no="+ product_id);
        if (dt.Rows.Count > 0)
            return true;
        else
            return false;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="part_group_id"></param>
    /// <param name="product_id"></param>
    public static void DelByPartIDAndGroupID(int part_group_id, int product_id)
    {
        Config.ExecuteNonQuery("delete from tb_part_group_detail where part_group_id=" + part_group_id + " and product_serial_no=" + product_id);
    }


    public DataTable FindPartIDAndNameByGroupID(int group_id)
    {
        return Config.ExecuteDataTable(@"select p.product_serial_no , concat(p.product_serial_no, '&nbsp;&nbsp;', product_name, '<span style=""color:green;"">($', product_current_price-product_current_discount, ')</span>', '[', ltd_stock  , ']') product_name from tb_part_group_detail pgd inner join tb_product p on p.product_serial_no=pgd.product_serial_no
where pgd.part_group_id='" + group_id + "' and split_line=0 and p.tag=1");

    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="group_id"></param>
    /// <returns></returns>
    public DataTable FindPartIDNameByGroupID(int group_id)
    {
        return Config.ExecuteDataTable(@"select p.product_serial_no , concat(concat(p.product_serial_no, ' --  '), product_name) product_name from tb_part_group_detail pgd inner join tb_product p on p.product_serial_no=pgd.product_serial_no
where pgd.part_group_id='" + group_id + "' and split_line=0 and p.tag=1");

    }


    public DataTable FindPartGroupNameByPartSku(int sku)
    {
        return Config.ExecuteDataTable(string.Format(@"Select pgd.part_group_id, part_group_name from tb_part_group_detail pgd 
inner join tb_part_group pg on pg.part_group_id=pgd.part_group_id where product_serial_no='{0}'
and pgd.showit=1 and pg.showit=1 limit 0,1", sku));
    }
}
