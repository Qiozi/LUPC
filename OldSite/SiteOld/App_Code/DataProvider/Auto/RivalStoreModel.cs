// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-8-20 10:34:30
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_rival_store")]
[Serializable]
public class RivalStoreModel : ActiveRecordBase<RivalStoreModel>
{

    public RivalStoreModel()
    {

    }

    public static RivalStoreModel GetRivalStoreModel(int _serial_no)
    {
        RivalStoreModel[] models = RivalStoreModel.FindAllByProperty("Rival_id", _serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new RivalStoreModel();
    }
    int _rival_id;
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int rival_id
    {
        get { return _rival_id; }
        set { _rival_id = value; }
    }
    int _rival_ltd_id;
    [Property]
    public int rival_ltd_id
    {
        get { return _rival_ltd_id; }
        set { _rival_ltd_id = value; }
    }
    string _rival_sku;
    [Property]
    public string rival_sku
    {
        get { return _rival_sku; }
        set { _rival_sku = value; }
    }
    string _rival_manufacture_code;
    [Property]
    public string rival_manufacture_code
    {
        get { return _rival_manufacture_code; }
        set { _rival_manufacture_code = value; }
    }
    string _rival_part_name;
    [Property]
    public string rival_part_name
    {
        get { return _rival_part_name; }
        set { _rival_part_name = value; }
    }
    decimal _rival_price;
    [Property]
    public decimal rival_price
    {
        get { return _rival_price; }
        set { _rival_price = value; }
    }
    DateTime _regdate;
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }

    public RivalStoreModel[] FindModelsByManufactureCode(string manufacture_code)
    {
        return RivalStoreModel.FindAllByProperty("rival_manufacture_code", manufacture_code);
    }

    public DataTable FindModelsByManufactureCode(int lu_sku, string manufacture_code)
    {
        return Config.ExecuteDataTable(@"select rival_ltd_id, rival_manufacture_code, rival_price, regdate from tb_rival_store where rival_manufacture_code='" + manufacture_code + @"'");
    }
}

