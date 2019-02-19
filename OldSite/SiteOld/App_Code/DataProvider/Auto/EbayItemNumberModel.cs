
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	07/04/2009 2:30:20 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_item_number")]
[Serializable]
public class EbayItemNumberModel : ActiveRecordBase<EbayItemNumberModel>
{

    public EbayItemNumberModel()
    {

    }

    public static EbayItemNumberModel GetEbayItemNumberModel(int _id)
    {
        EbayItemNumberModel[] models = EbayItemNumberModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbayItemNumberModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    string _item_number;

    [Property]
    public string item_number
    {
        get { return _item_number; }
        set { _item_number = value; }
    }

    int _luc_sku;

    [Property]
    public int luc_sku
    {
        get { return _luc_sku; }
        set { _luc_sku = value; }
    }

    string _luc_type;

    [Property]
    public string luc_type
    {
        get { return _luc_type; }
        set { _luc_type = value; }
    }


}

