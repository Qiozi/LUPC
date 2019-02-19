
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	9/6/2011 10:16:57 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_store_customer_code")]
[Serializable]
public class StoreCustomerCodeModel : ActiveRecordBase<StoreCustomerCodeModel>
{

    public StoreCustomerCodeModel()
    {

    }

    public static StoreCustomerCodeModel GetStoreCustomerCodeModel(int _ID)
    {
        StoreCustomerCodeModel[] models = StoreCustomerCodeModel.FindAllByProperty("ID", _ID);
        if (models.Length == 1)
            return models[0];
        else
            return new StoreCustomerCodeModel();
    }

    int _ID;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int ID
    {
        get { return _ID; }
        set { _ID = value; }
    }

    string _CustomerCode;

    [Property]
    public string CustomerCode
    {
        get { return _CustomerCode; }
        set { _CustomerCode = value; }
    }


}

