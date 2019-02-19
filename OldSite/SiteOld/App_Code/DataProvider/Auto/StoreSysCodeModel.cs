
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	9/6/2011 10:18:18 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_store_sys_code")]
[Serializable]
public class StoreSysCodeModel : ActiveRecordBase<StoreSysCodeModel>
{

    public StoreSysCodeModel()
    {

    }

    public static StoreSysCodeModel GetStoreSysCodeModel(int _ID)
    {
        StoreSysCodeModel[] models = StoreSysCodeModel.FindAllByProperty("ID", _ID);
        if (models.Length == 1)
            return models[0];
        else
            return new StoreSysCodeModel();
    }

    int _ID;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int ID
    {
        get { return _ID; }
        set { _ID = value; }
    }

    string _SysCode;

    [Property]
    public string SysCode
    {
        get { return _SysCode; }
        set { _SysCode = value; }
    }


}

