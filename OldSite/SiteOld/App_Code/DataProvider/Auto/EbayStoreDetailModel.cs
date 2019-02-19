// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-10-01 15:00:26
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_ebay_store_detail")]
[Serializable]
public class EbayStoreDetailModel : ActiveRecordBase<EbayStoreDetailModel>
{
	public EbayStoreDetailModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    public static EbayStoreDetailModel GetEbayStoreDetailModel(int _id)
    {
        EbayStoreDetailModel[] models = EbayStoreDetailModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbayStoreDetailModel();
    }

    int _id;
    int _lu_sku;
    [Property]
    public int lu_sku
    {
        get { return _lu_sku; }
        set { _lu_sku = value; }
    }
    string _part_comment;
    [Property]
    public string part_comment
    {
        get { return _part_comment; }
        set { _part_comment = value; }
    }
    string _part_name;
    [Property]
    public string part_name
    {
        get { return _part_name; }
        set { _part_name = value; }
    }
    int _ebay_store_id;
    [Property]
    public int ebay_store_id
    {
        get { return _ebay_store_id; }
        set { _ebay_store_id = value; }
    }
    DateTime _regdate;
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }
    DateTime _last_regdate;
    [Property]
    public DateTime last_regdate
    {
        get { return _last_regdate; }
        set { _last_regdate = value; }
    }
    int _part_group_id;
    [Property]
    public int part_group_id
    {
        get { return _part_group_id; }
        set { _part_group_id = value; }
    }

}
