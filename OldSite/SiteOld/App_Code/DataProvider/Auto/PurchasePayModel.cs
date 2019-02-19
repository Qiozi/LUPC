// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-27 17:29:12
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_purchase_pay")]
[Serializable]
public class PurchasePayModel : ActiveRecordBase<PurchasePayModel>
{
    int _amount;
    int _purchase_pay_serial_no;
    int _pay_method_serial_no;
    string _check_code;
    DateTime _date;
    double _balance;
    int _tag;
    DateTime _create_datetime;
    int _purchase_serial_no;

    public PurchasePayModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int amount
    {
        get { return _amount; }
        set { _amount = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int purchase_pay_serial_no
    {
        get { return _purchase_pay_serial_no; }
        set { _purchase_pay_serial_no = value; }
    }
    public static PurchasePayModel GetPurchasePayModel(int _purchase_pay_serial_no)
    {
        PurchasePayModel[] models = PurchasePayModel.FindAllByProperty("purchase_pay_serial_no", _purchase_pay_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new PurchasePayModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int pay_method_serial_no
    {
        get { return _pay_method_serial_no; }
        set { _pay_method_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string check_code
    {
        get { return _check_code; }
        set { _check_code = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime date
    {
        get { return _date; }
        set { _date = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public double balance
    {
        get { return _balance; }
        set { _balance = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int tag
    {
        get { return _tag; }
        set { _tag = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime create_datetime
    {
        get { return _create_datetime; }
        set { _create_datetime = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int purchase_serial_no
    {
        get { return _purchase_serial_no; }
        set { _purchase_serial_no = value; }
    }

    public static PurchasePayModel[] GetModelsByPurchaseSerial(int purchase_id)
    {
        return PurchasePayModel.FindAllByProperty("purchase_serial_no", purchase_id);
    }
}
