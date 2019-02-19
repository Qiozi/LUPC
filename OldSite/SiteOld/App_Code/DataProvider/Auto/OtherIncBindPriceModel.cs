
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2/7/2009 8:47:15 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_other_inc_bind_price")]
[Serializable]
public class OtherIncBindPriceModel : ActiveRecordBase<OtherIncBindPriceModel>
{

    public OtherIncBindPriceModel()
    {

    }

    public static OtherIncBindPriceModel GetOtherIncBindPriceModel(int _id)
    {
        OtherIncBindPriceModel[] models = OtherIncBindPriceModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new OtherIncBindPriceModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _bind_type;

    [Property]
    public int bind_type
    {
        get { return _bind_type; }
        set { _bind_type = value; }
    }

    int _category_id;

    [Property]
    public int category_id
    {
        get { return _category_id; }
        set { _category_id = value; }
    }

    string _manufactory;

    [Property]
    public string manufactory
    {
        get { return _manufactory; }
        set { _manufactory = value; }
    }

    int _priority;

    [Property]
    public int priority
    {
        get { return _priority; }
        set { _priority = value; }
    }

    int _other_inc_id;

    [Property]
    public int other_inc_id
    {
        get { return _other_inc_id; }
        set { _other_inc_id = value; }
    }

    int _luc_sku;

    [Property]
    public int luc_sku
    {
        get { return _luc_sku; }
        set { _luc_sku = value; }
    }

    bool _is_single;

    [Property]
    public bool is_single
    {
        get { return _is_single; }
        set { _is_single = value; }
    }

    bool _is_relating;

    [Property]
    public bool is_relating
    {
        get { return _is_relating; }
        set { _is_relating = value; }
    }


}

