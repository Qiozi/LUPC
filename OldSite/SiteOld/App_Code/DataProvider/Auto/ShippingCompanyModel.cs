// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-10 19:51:47
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_shipping_company")]
[Serializable]
public class ShippingCompanyModel : ActiveRecordBase<ShippingCompanyModel>
{
    int _shipping_company_id;
    string _shipping_company_name;
    int _qty;

    public ShippingCompanyModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int shipping_company_id
    {
        get { return _shipping_company_id; }
        set { _shipping_company_id = value; }
    }
    public static ShippingCompanyModel GetShippingCompanyModel(int _shipping_company_id)
    {
        ShippingCompanyModel[] models = ShippingCompanyModel.FindAllByProperty("shipping_company_id", _shipping_company_id);
        if (models.Length == 1)
            return models[0];
        else
            return null;
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string shipping_company_name
    {
        get { return _shipping_company_name; }
        set { _shipping_company_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int qty
    {
        get { return _qty; }
        set { _qty  = value; }
    }

    public static ShippingCompanyModel[] GetModelsByOrder()
    {
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("qty", true);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        return ShippingCompanyModel.FindAll(oo);
    }
}
