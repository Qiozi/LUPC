// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-25 23:15:50
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_order_facture_state")]
[Serializable]
public class OrderFactureStateModel : ActiveRecordBase<OrderFactureStateModel>
{
    int _order_facture_state_serial_no;
    string _order_code;
    int _facture_state_serial_no;
    DateTime _order_facture_state_create_date;
    string _order_facture_note;

    public OrderFactureStateModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int order_facture_state_serial_no
    {
        get { return _order_facture_state_serial_no; }
        set { _order_facture_state_serial_no = value; }
    }
    public static OrderFactureStateModel GetOrderFactureStateModel(int _order_facture_state_serial_no)
    {
        OrderFactureStateModel[] models = OrderFactureStateModel.FindAllByProperty("order_facture_state_serial_no", _order_facture_state_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new OrderFactureStateModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string order_code
    {
        get { return _order_code; }
        set { _order_code = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int facture_state_serial_no
    {
        get { return _facture_state_serial_no; }
        set { _facture_state_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime order_facture_state_create_date
    {
        get { return _order_facture_state_create_date; }
        set { _order_facture_state_create_date = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string order_facture_note
    {
        get { return _order_facture_note; }
        set { _order_facture_note = value; }
    }

    public static OrderFactureStateModel[] GetModelsByOrderCode(int order_code)
    {
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("order_facture_state_serial_no", false);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        NHibernate.Expression.EqExpression eq = new NHibernate.Expression.EqExpression("order_code", order_code);
        return OrderFactureStateModel.FindAll(oo, eq);
    }

    public static void SaveModels(string order_code, int out_status, string note)
    {
        OrderFactureStateModel model = new OrderFactureStateModel();
        model.order_code = order_code;
        model.facture_state_serial_no = out_status;
        model.order_facture_note = note;
        model.order_facture_state_create_date = DateTime.Now;
        model.Create();
    }
}
