
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	3/23/2010 7:58:16 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_system_parts")]
[Serializable]
public class EbaySystemPartsModel : ActiveRecordBase<EbaySystemPartsModel>
{

    public EbaySystemPartsModel()
    {

    }

    public static EbaySystemPartsModel GetEbaySystemPartsModel(int _id)
    {
        EbaySystemPartsModel[] models = EbaySystemPartsModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbaySystemPartsModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _system_sku;

    [Property]
    public int system_sku
    {
        get { return _system_sku; }
        set { _system_sku = value; }
    }

    int _luc_sku;

    [Property]
    public int luc_sku
    {
        get { return _luc_sku; }
        set { _luc_sku = value; }
    }

    int _comment_id;

    [Property]
    public int comment_id
    {
        get { return _comment_id; }
        set { _comment_id = value; }
    }

    string _comment_name;

    [Property]
    public string comment_name
    {
        get { return _comment_name; }
        set { _comment_name = value; }
    }

    decimal _price;

    [Property]
    public decimal price
    {
        get { return _price; }
        set { _price = value; }
    }

    decimal _cost;

    [Property]
    public decimal cost
    {
        get { return _cost; }
        set { _cost = value; }
    }

    int _part_quantity;

    [Property]
    public int part_quantity
    {
        get { return _part_quantity; }
        set { _part_quantity = value; }
    }

    int _max_quantity;

    [Property]
    public int max_quantity
    {
        get { return _max_quantity; }
        set { _max_quantity = value; }
    }

    string _compatibility_parts;

    [Property]
    public string compatibility_parts
    {
        get { return _compatibility_parts; }
        set { _compatibility_parts = value; }
    }

    int _part_group_id;

    [Property]
    public int part_group_id
    {
        get { return _part_group_id; }
        set { _part_group_id = value; }
    }


}

