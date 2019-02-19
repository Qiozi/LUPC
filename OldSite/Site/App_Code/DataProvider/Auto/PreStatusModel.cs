// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-20 23:14:06
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class PreStatusModel
{
    public static tb_pre_status GetPreStatusModel(nicklu2Entities context, int pre_status_serial_no)
    {
        return context.tb_pre_status.Single(me => me.pre_status_serial_no.Equals(pre_status_serial_no));
    }
    public static tb_pre_status[] FindModelsByShowit(nicklu2Entities context)
    {
        //NHibernate.Expression.EqExpression eq = new NHibernate.Expression.EqExpression("showit", true);
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("priority", true);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        //return PreStatusModel.FindAll(oo, eq);
        var query = context.tb_pre_status.Where(me => me.showit.Value.Equals(true)).OrderBy(me => me.priority.Value).ToList();
        return query.ToArray();
    }
}
