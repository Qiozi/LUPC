// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/27/2007 10:41:39 AM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;

[Serializable]
public class MenuModel 
{
 
    /// <summary>
    /// 
    /// </summary>
    /// <param name="menu_pre_serial_no"></param>
    /// <returns></returns>
    public static tb_menu[] MenuModelsByMenuParentSerialNo(nicklu2Entities context, int menu_parent_serial_no, bool tag)
    {
        //NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", menu_parent_serial_no);
        //NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("tag", tag);
        //NHibernate.Expression.AndExpression aa = new NHibernate.Expression.AndExpression(eq1, eq2);

        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("menu_child_order", true);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        //return MenuModel.FindAll(oo, aa);
        var tagValue = tag ? sbyte.Parse("1") : sbyte.Parse("0");
        var query = context.tb_menu.Where(me => me.menu_parent_serial_no.Value.Equals(menu_parent_serial_no) && me.tag.Value.Equals(tagValue))
                            .OrderBy(me => me.menu_child_order.Value)
                            .ToList();
        return query.ToArray();
    }

    public static DataTable FindMenuTopLevel()
    {
        return Config.ExecuteDataTable(@"select menu_child_serial_no,menu_child_name, menu_child_href,menu_is_exist_sub 
from tb_menu where menu_parent_serial_no=0 and tag=1  order by menu_child_order asc ");

    }
}
