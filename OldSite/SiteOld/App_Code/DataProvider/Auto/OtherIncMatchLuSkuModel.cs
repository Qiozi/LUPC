
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	11/26/2008 4:16:33 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_other_inc_match_lu_sku")]
[Serializable]
public class OtherIncMatchLuSkuModel : ActiveRecordBase<OtherIncMatchLuSkuModel>
{

    public OtherIncMatchLuSkuModel()
    {

    }

    public static OtherIncMatchLuSkuModel GetOtherIncMatchLuSkuModel(int _id)
    {
        OtherIncMatchLuSkuModel[] models = OtherIncMatchLuSkuModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new OtherIncMatchLuSkuModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _lu_sku;

    [Property]
    public int lu_sku
    {
        get { return _lu_sku; }
        set { _lu_sku = value; }
    }

    string _other_inc_sku;

    [Property]
    public string other_inc_sku
    {
        get { return _other_inc_sku; }
        set { _other_inc_sku = value; }
    }

    int _other_inc_type;

    [Property]
    public int other_inc_type
    {
        get { return _other_inc_type; }
        set { _other_inc_type = value; }
    }


}

