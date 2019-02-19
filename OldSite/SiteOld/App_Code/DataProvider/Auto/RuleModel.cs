// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-11-30 13:46:05
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

/// <summary>
/// StaffModel 的摘要说明
/// </summary>
[ActiveRecord("tb_rule")]
[Serializable]
public class RuleModel : ActiveRecordBase<RuleModel>
{
    int _rule_serial_no;
    int _staff_serial_no;
    int _model_id;


    public RuleModel()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
    }
    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int rule_serial_no
    {
        get { return _rule_serial_no; }
        set { _rule_serial_no = value; }
    }
    public static RuleModel GetRuleModel(int _staff_serial_no)
    {
        RuleModel[] models = RuleModel.FindAllByProperty("staff_serial_no", _staff_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new RuleModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int staff_serial_no
    {
        get { return _staff_serial_no; }
        set { _staff_serial_no = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int model_id
    {
        get { return _model_id; }
        set { _model_id = value; }
    }

    public static  RuleModel[] FindModelsByStaffAndModel(int staff_id, int model_id)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("staff_serial_no", staff_id);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("model_id", model_id);
        NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);

        return RuleModel.FindAll(a);
    }

    public static RuleModel[] FindModelsByStaffAndModel(int staff_id)
    {
        return RuleModel.FindAllByProperty("staff_serial_no", staff_id);
    }

    public static  void DeleteModelsByStaff(int staff_id)
    {
        Config.ExecuteNonQuery("delete from tb_rule where staff_serial_no=" + staff_id);
    }
}
