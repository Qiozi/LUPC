// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-20 23:14:06
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_pre_status")]
[Serializable]
public class PreStatusModel : ActiveRecordBase<PreStatusModel>
{
    int _pre_status_serial_no;
    string _pre_status_name;
    bool _showit;
    int _priority;
    string _back_color;

    public PreStatusModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int pre_status_serial_no
    {
        get { return _pre_status_serial_no; }
        set { _pre_status_serial_no = value; }
    }
    public static PreStatusModel GetPreStatusModel(int _pre_status_serial_no)
    {
        PreStatusModel[] models = PreStatusModel.FindAllByProperty("pre_status_serial_no", _pre_status_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new PreStatusModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string pre_status_name
    {
        get { return _pre_status_name; }
        set { _pre_status_name = value; }
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
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int priority
    {
        get { return _priority; }
        set { _priority = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string back_color
    {
        get { return _back_color; }
        set { _back_color = value; }
    }
    public static PreStatusModel[] FindModelsByShowit()
    {
        NHibernate.Expression.EqExpression eq = new NHibernate.Expression.EqExpression("showit", true);
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("priority", true);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        return PreStatusModel.FindAll(oo, eq);
    }
}
