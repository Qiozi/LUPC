
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	9/6/2011 10:17:54 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_store_order_code")]
[Serializable]
public class StoreOrderCodeModel : ActiveRecordBase<StoreOrderCodeModel>
{

    public StoreOrderCodeModel()
    {

    }

    public static StoreOrderCodeModel GetStoreOrderCodeModel(int _ID)
    {
        StoreOrderCodeModel[] models = StoreOrderCodeModel.FindAllByProperty("ID", _ID);
        if (models.Length == 1)
            return models[0];
        else
            return new StoreOrderCodeModel();
    }

    int _ID;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int ID
    {
        get { return _ID; }
        set { _ID = value; }
    }

    int _OrderCode;

    [Property]
    public int OrderCode
    {
        get { return _OrderCode; }
        set { _OrderCode = value; }
    }


}

