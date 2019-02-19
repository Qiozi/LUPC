
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	11/27/2012 10:15:12 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_shipping_settings")]
[Serializable]
public class EbayShippingSettingsModel : ActiveRecordBase<EbayShippingSettingsModel>
{

    public EbayShippingSettingsModel()
    {

    }

    public static EbayShippingSettingsModel GetEbayShippingSettingsModel(int _ID)
    {
        EbayShippingSettingsModel[] models = EbayShippingSettingsModel.FindAllByProperty("ID", _ID);
        if (models.Length == 1)
            return models[0];
        else
            return new EbayShippingSettingsModel();
    }

    int _ID;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int ID
    {
        get { return _ID; }
        set { _ID = value; }
    }

    decimal _shippingFee;

    [Property]
    public decimal shippingFee
    {
        get { return _shippingFee; }
        set { _shippingFee = value; }
    }

    string _shippingCompany;

    [Property]
    public string shippingCompany
    {
        get { return _shippingCompany; }
        set { _shippingCompany = value; }
    }

    bool _IsFree;

    [Property]
    public bool IsFree
    {
        get { return _IsFree; }
        set { _IsFree = value; }
    }

    string _CategoryID;

    [Property]
    public string CategoryID
    {
        get { return _CategoryID; }
        set { _CategoryID = value; }
    }

    string _ShortCategoryName;

    [Property]
    public string ShortCategoryName
    {
        get { return _ShortCategoryName; }
        set { _ShortCategoryName = value; }
    }

    DateTime _regdate;

    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }

    /// <summary>
    /// 澳大利亚笔记本运费， 17寸以上加50，以下加30 
    /// </summary>
    public decimal AuShippingFee
    {
        get;
        set;
    }
}

