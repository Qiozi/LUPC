
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	07/04/2009 12:01:00 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_turbolist")]
[Serializable]
public class EbayTurbolistModel : ActiveRecordBase<EbayTurbolistModel>
{

    public EbayTurbolistModel()
    {

    }

    public static EbayTurbolistModel GetEbayTurbolistModel(int _id)
    {
        EbayTurbolistModel[] models = EbayTurbolistModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbayTurbolistModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    string _sold_status;

    [Property]
    public string sold_status
    {
        get { return _sold_status; }
        set { _sold_status = value; }
    }

    string _item_id;

    [Property]
    public string item_id
    {
        get { return _item_id; }
        set { _item_id = value; }
    }

    string _item_title;

    [Property]
    public string item_title
    {
        get { return _item_title; }
        set { _item_title = value; }
    }

    string _format_type;

    [Property]
    public string format_type
    {
        get { return _format_type; }
        set { _format_type = value; }
    }

    decimal _sale_price;

    [Property]
    public decimal sale_price
    {
        get { return _sale_price; }
        set { _sale_price = value; }
    }

    decimal _start_price;

    [Property]
    public decimal start_price
    {
        get { return _start_price; }
        set { _start_price = value; }
    }

    string _price_unit;

    [Property]
    public string price_unit
    {
        get { return _price_unit; }
        set { _price_unit = value; }
    }

    string _custom_label;

    [Property]
    public string custom_label
    {
        get { return _custom_label; }
        set { _custom_label = value; }
    }

    string _shipping_type;

    [Property]
    public string shipping_type
    {
        get { return _shipping_type; }
        set { _shipping_type = value; }
    }

    string _shipping_service;

    [Property]
    public string shipping_service
    {
        get { return _shipping_service; }
        set { _shipping_service = value; }
    }

    decimal _shipping_cost;

    [Property]
    public decimal shipping_cost
    {
        get { return _shipping_cost; }
        set { _shipping_cost = value; }
    }

    string _shipping_cost_unit;

    [Property]
    public string shipping_cost_unit
    {
        get { return _shipping_cost_unit; }
        set { _shipping_cost_unit = value; }
    }

    decimal _handling_cost;

    [Property]
    public decimal handling_cost
    {
        get { return _handling_cost; }
        set { _handling_cost = value; }
    }

    string _handling_cost_unit;

    [Property]
    public string handling_cost_unit
    {
        get { return _handling_cost_unit; }
        set { _handling_cost_unit = value; }
    }

    string _weight_lbs;

    [Property]
    public string weight_lbs
    {
        get { return _weight_lbs; }
        set { _weight_lbs = value; }
    }

    string _weight_oz;

    [Property]
    public string weight_oz
    {
        get { return _weight_oz; }
        set { _weight_oz = value; }
    }

    int _available_quantity;

    [Property]
    public int available_quantity
    {
        get { return _available_quantity; }
        set { _available_quantity = value; }
    }

    int _quantity;

    [Property]
    public int quantity
    {
        get { return _quantity; }
        set { _quantity = value; }
    }

    DateTime _start_time;

    [Property]
    public DateTime start_time
    {
        get { return _start_time; }
        set { _start_time = value; }
    }


}

