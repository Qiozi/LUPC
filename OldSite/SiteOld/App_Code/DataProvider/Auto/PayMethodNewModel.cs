// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-10 19:51:47
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_pay_method_new")]
[Serializable]
public class PayMethodNewModel : ActiveRecordBase<PayMethodNewModel>
{
    int _pay_method_serial_no;
    string _pay_method_name;
    string _pay_check_path;
    int _taxis;
    string _pay_method_short_name;

    public PayMethodNewModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int pay_method_serial_no
    {
        get { return _pay_method_serial_no; }
        set { _pay_method_serial_no = value; }
    }
    public static PayMethodNewModel GetPayMethodNewModel(int _pay_method_serial_no)
    {
        PayMethodNewModel[] models = PayMethodNewModel.FindAllByProperty("pay_method_serial_no", _pay_method_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new PayMethodNewModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string pay_method_name
    {
        get { return _pay_method_name; }
        set { _pay_method_name = value; }
    }       
          /// <summary>
    /// 
    /// </summary>
    [Property]
    public string pay_check_path
    {
        get { return _pay_check_path; }
        set { _pay_check_path = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int taxis
    {
        get { return _taxis; }
        set { _taxis = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string pay_method_short_name
    {
        get { return _pay_method_short_name; }
        set { _pay_method_short_name = value; }
    }
    public static PayMethodNewModel[] GetModelsByOrder()
    {
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("taxis", true);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        return PayMethodNewModel.FindAll(oo);
    }
}
