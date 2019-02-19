// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-11-30 13:46:05
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

/// <summary>
/// StaffModel 的摘要说明
/// </summary>
/// 
[Serializable]
public class RuleModel
{
  
    public static tb_rule[] FindModelsByStaffAndModel(nicklu2Entities context, int staff_id, int model_id)
    {
        //NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("staff_serial_no", staff_id);
        //NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("model_id", model_id);
        //NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);

        //return RuleModel.FindAll(a);
        var query = context.tb_rule.Where(me => me.staff_serial_no.Value.Equals(staff_id) && me.model_id.Value.Equals(model_id)).ToList().ToArray();
        return query;
    }

    public static tb_rule[] FindModelsByStaffAndModel(nicklu2Entities context, int staff_id)
    {
        //return RuleModel.FindAllByProperty("staff_serial_no", staff_id);
        var query = context.tb_rule.Where(me => me.staff_serial_no.Value.Equals(staff_id));
        return query.ToList().ToArray();
    }

    public static  void DeleteModelsByStaff(int staff_id)
    {
        Config.ExecuteNonQuery("delete from tb_rule where staff_serial_no=" + staff_id);
    }
}
