// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/27/2007 10:41:39 AM
//
// // // // // // // // // // // // // // // //
using System;

[Serializable]
public class MenuChildModel 
{
  
    ///// <summary>
    ///// 根据父类显示子类产品类别
    ///// </summary>
    ///// <param name="menu_pre_serial_no">父类ID</param>
    ///// <returns></returns>
    //public static MenuChildModel[] MenuChildModelsByMenuPreSerialNo(int menu_pre_serial_no)
    //{
    //    NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", 1);
    //    NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("menu_pre_serial_no", menu_pre_serial_no);
    //    NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
    //    return MenuChildModel.FindAll(a);
    //}

    ///// <summary>
    ///// 所有产品类别
    ///// </summary>
    ///// <returns></returns>
    //public static MenuChildModel[] MenuChildModels()
    //{
    //    return MenuChildModel.FindAllByProperty("menu_parent_serial_no", 1);
    //}
    ///// <summary>
    ///// 显示是否可以展示的显产品类别
    ///// </summary>
    ///// <param name="showit"></param>
    ///// <returns></returns>
    //public static MenuChildModel[] MenuChildModelsByShowIt(bool showit)
    //{
    //    NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("menu_parent_serial_no", 1);
    //    NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("tag", showit);
    //    NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
    //    return MenuChildModel.FindAll();
    //}
}
