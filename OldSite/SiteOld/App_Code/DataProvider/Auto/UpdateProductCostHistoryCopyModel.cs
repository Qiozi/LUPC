// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-12-11 23:27:56
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;


[ActiveRecord("tb_update_product_cost_history_copy")]
public class UpdateProductCostHistoryCopyModel : ActiveRecordBase<UpdateProductCostHistoryCopyModel>
{
    int _product_cost_history_id;
    int _product_serial_no;
    decimal _product_cost;
    string _product_factory_sku;
    DateTime _regdate;
    int _staff_id;

    public UpdateProductCostHistoryCopyModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int product_cost_history_id
    {
        get { return _product_cost_history_id; }
        set { _product_cost_history_id = value; }
    }
    public static UpdateProductCostHistoryCopyModel GetUpdateProductCostHistoryCopyModel(int _product_cost_history_id)
    {
        UpdateProductCostHistoryCopyModel[] models = UpdateProductCostHistoryCopyModel.FindAllByProperty("product_cost_history_id", _product_cost_history_id);
        if (models.Length == 1)
            return models[0];
        else
            return new UpdateProductCostHistoryCopyModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_serial_no
    {
        get { return _product_serial_no; }
        set { _product_serial_no = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal product_cost
    {
        get { return _product_cost; }
        set { _product_cost = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string product_factory_sku
    {
        get { return _product_factory_sku; }
        set { _product_factory_sku = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int staff_id
    {
        get { return _staff_id; }
        set { _staff_id = value; }
    }
}
