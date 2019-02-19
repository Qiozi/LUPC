
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	1/16/2011 9:59:08 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_part_price_history")]
[Serializable]
public class EbayPartPriceHistoryModel : ActiveRecordBase<EbayPartPriceHistoryModel>
{

    public EbayPartPriceHistoryModel()
    {

    }

    public static EbayPartPriceHistoryModel GetEbayPartPriceHistoryModel(int _id)
    {
        EbayPartPriceHistoryModel[] models = EbayPartPriceHistoryModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbayPartPriceHistoryModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _luc_sku;

    [Property]
    public int luc_sku
    {
        get { return _luc_sku; }
        set { _luc_sku = value; }
    }

    string _mfp;

    [Property]
    public string mfp
    {
        get { return _mfp; }
        set { _mfp = value; }
    }

    string _part_ebay_name;

    [Property]
    public string part_ebay_name
    {
        get { return _part_ebay_name; }
        set { _part_ebay_name = value; }
    }

    string _custom_label;

    [Property]
    public string custom_label
    {
        get { return _custom_label; }
        set { _custom_label = value; }
    }

    string _ebay_itemid;

    [Property]
    public string ebay_itemid
    {
        get { return _ebay_itemid; }
        set { _ebay_itemid = value; }
    }

    decimal _cost;

    [Property]
    public decimal cost
    {
        get { return _cost; }
        set { _cost = value; }
    }

    decimal _web_price;

    [Property]
    public decimal web_price
    {
        get { return _web_price; }
        set { _web_price = value; }
    }

    decimal _shipping;

    [Property]
    public decimal shipping
    {
        get { return _shipping; }
        set { _shipping = value; }
    }

    decimal _profit;

    [Property]
    public decimal profit
    {
        get { return _profit; }
        set { _profit = value; }
    }

    decimal _ebay_fee;

    [Property]
    public decimal ebay_fee
    {
        get { return _ebay_fee; }
        set { _ebay_fee = value; }
    }

    decimal _ebay_price;

    [Property]
    public decimal ebay_price
    {
        get { return _ebay_price; }
        set { _ebay_price = value; }
    }

}

