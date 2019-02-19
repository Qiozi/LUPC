// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/30/2007 2:22:40 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_producter")]
[Serializable]
public class ProducterModel : ActiveRecordBase<ProducterModel>
{
    int _producter_serial_no;
    string _producter_name;
    string _producter_web_address;

    public ProducterModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int producter_serial_no
    {
        get { return _producter_serial_no; }
        set { _producter_serial_no = value; }
    }
    public static ProducterModel GetProducterModel(int _producter_serial_no)
    {
        ProducterModel[] models = ProducterModel.FindAllByProperty("producter_serial_no", _producter_serial_no);
        if (models.Length == 1)
            return models[0];
        else
            return new ProducterModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string producter_name
    {
        get { return _producter_name; }
        set { _producter_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string producter_web_address
    {
        get { return _producter_web_address; }
        set { _producter_web_address = value; }
    }

    public static ProducterModel[] GetProducterModelByAll(bool orderasc)
    {
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("producter_serial_no", orderasc);
        NHibernate.Expression.Order[] os = new NHibernate.Expression.Order[] { o };
        return ProducterModel.FindAll(os);
    }
}
