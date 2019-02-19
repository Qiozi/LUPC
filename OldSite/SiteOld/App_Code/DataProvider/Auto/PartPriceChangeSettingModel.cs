
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	5/16/2010 11:29:06 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_part_price_change_setting")]
[Serializable]
public class PartPriceChangeSettingModel : ActiveRecordBase<PartPriceChangeSettingModel>
{

    public PartPriceChangeSettingModel()
    {

    }

    public static PartPriceChangeSettingModel GetPartPriceChangeSettingModel(int _id)
    {
        PartPriceChangeSettingModel[] models = PartPriceChangeSettingModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new PartPriceChangeSettingModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _category_id;

    [Property]
    public int category_id
    {
        get { return _category_id; }
        set { _category_id = value; }
    }

    decimal _cost_min;

    [Property]
    public decimal cost_min
    {
        get { return _cost_min; }
        set { _cost_min = value; }
    }

    decimal _cost_max;

    [Property]
    public decimal cost_max
    {
        get { return _cost_max; }
        set { _cost_max = value; }
    }

    int _rate;

    [Property]
    public int rate
    {
        get { return _rate; }
        set { _rate = value; }
    }

    bool _is_percent;

    [Property]
    public bool is_percent
    {
        get { return _is_percent; }
        set { _is_percent = value; }
    }


}

