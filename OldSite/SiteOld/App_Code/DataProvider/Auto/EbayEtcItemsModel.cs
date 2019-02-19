
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2013-11-10 9:55:46 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_etc_items")]
[Serializable]
public class EbayEtcItemsModel : ActiveRecordBase<EbayEtcItemsModel>
{

    public EbayEtcItemsModel()
    {

    }

    public static EbayEtcItemsModel GetEbayEtcItemsModel(int _ID)
    {
        EbayEtcItemsModel[] models = EbayEtcItemsModel.FindAllByProperty("ID", _ID);
        if (models.Length == 1)
            return models[0];
        else
            return new EbayEtcItemsModel();
    }

    int _ID;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int ID
    {
        get { return _ID; }
        set { _ID = value; }
    }

    string _ItemID;

    [Property]
    public string ItemID
    {
        get { return _ItemID; }
        set { _ItemID = value; }
    }

    string _ItemTitle;

    [Property]
    public string ItemTitle
    {
        get { return _ItemTitle; }
        set { _ItemTitle = value; }
    }

    decimal _ItemPrice;

    [Property]
    public decimal ItemPrice
    {
        get { return _ItemPrice; }
        set { _ItemPrice = value; }
    }

    int _LUC_eBay_Sys_Sku;

    [Property]
    public int LUC_eBay_Sys_Sku
    {
        get { return _LUC_eBay_Sys_Sku; }
        set { _LUC_eBay_Sys_Sku = value; }
    }

    DateTime _Regdate;

    [Property]
    public DateTime Regdate
    {
        get { return _Regdate; }
        set { _Regdate = value; }
    }


}

