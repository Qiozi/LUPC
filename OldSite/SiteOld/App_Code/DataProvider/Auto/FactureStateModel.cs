// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-20 20:58:51
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_facture_state")]
[Serializable]
public class FactureStateModel : ActiveRecordBase<FactureStateModel>
{
    int _facture_state_serial_no;
    string _facture_state_name;
    bool _showit;
    int _priority;
    string _back_color;
    public FactureStateModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int facture_state_serial_no
    {
        get { return _facture_state_serial_no; }
        set { _facture_state_serial_no = value; }
    }
    public static FactureStateModel GetFactureStateModel(int _facture_state_serial_no)
    {
        FactureStateModel[] models = FactureStateModel.FindAllByProperty("facture_state_serial_no", _facture_state_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new FactureStateModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string facture_state_name
    {
        get { return _facture_state_name; }
        set { _facture_state_name = value; }
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
    public static FactureStateModel[] FindModelsByShowit()
    {
        NHibernate.Expression.EqExpression eq = new NHibernate.Expression.EqExpression("showit", true);
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("priority", true);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        return FactureStateModel.FindAll(oo,eq);
    }
}
