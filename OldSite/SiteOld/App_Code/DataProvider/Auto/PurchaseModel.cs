// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-26 13:03:56
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_purchase")]
[Serializable]
public class PurchaseModel : ActiveRecordBase<PurchaseModel>
{
    int _purchase_serial_no;
    string _purchase_invoice;
    string _purchase_net_amount;
    string _purchase_gst;
    string _purchase_pst;
    string _purchase_paid_amount;
    string _purchase_check_no;
    string _purchase_bank;
    DateTime _purchase_date;
    string _purchase_note;
    int _vendor_serial_no;
    string _staff_serial_no;
    int _system_category_serial_no;
    string _purchase_product_list;

    public PurchaseModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int purchase_serial_no
    {
        get { return _purchase_serial_no; }
        set { _purchase_serial_no = value; }
    }
    public static PurchaseModel GetPurchaseModel(int _purchase_serial_no)
    {
        PurchaseModel[] models = PurchaseModel.FindAllByProperty("purchase_serial_no", _purchase_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new PurchaseModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string purchase_invoice
    {
        get { return _purchase_invoice; }
        set { _purchase_invoice = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string purchase_net_amount
    {
        get { return _purchase_net_amount; }
        set { _purchase_net_amount = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string purchase_gst
    {
        get { return _purchase_gst; }
        set { _purchase_gst = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string purchase_pst
    {
        get { return _purchase_pst; }
        set { _purchase_pst = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string purchase_paid_amount
    {
        get { return _purchase_paid_amount; }
        set { _purchase_paid_amount = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string purchase_check_no
    {
        get { return _purchase_check_no; }
        set { _purchase_check_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string purchase_bank
    {
        get { return _purchase_bank; }
        set { _purchase_bank = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime purchase_date
    {
        get { return _purchase_date; }
        set { _purchase_date = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string purchase_note
    {
        get { return _purchase_note; }
        set { _purchase_note = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int vendor_serial_no
    {
        get { return _vendor_serial_no; }
        set { _vendor_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string staff_serial_no
    {
        get { return _staff_serial_no; }
        set { _staff_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int system_category_serial_no
    {
        get { return _system_category_serial_no; }
        set { _system_category_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string purchase_product_list
    {
        get { return _purchase_product_list; }
        set { _purchase_product_list = value; }
    }

    public static PurchaseModel[] GetPurchaseModels()
    {
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("purchase_serial_no", true);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        return PurchaseModel.FindAll(oo);
    }
}
