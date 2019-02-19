// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-10 19:50:14
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_account")]
[Serializable]
public class AccountModel : ActiveRecordBase<AccountModel>
{
    int _account_id;
    int _shipping_company_id;
    int _product_size_id;
    int _product_category;
    decimal _charge;
    int _qty;

    public AccountModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int account_id
    {
        get { return _account_id; }
        set { _account_id = value; }
    }
    public static AccountModel GetAccountModel(int _account_id)
    {
        AccountModel[] models = AccountModel.FindAllByProperty("account_id", _account_id);
        if (models.Length == 1)
            return models[0];
        else
            return new AccountModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int shipping_company_id
    {
        get { return _shipping_company_id; }
        set { _shipping_company_id = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_size_id
    {
        get { return _product_size_id; }
        set { _product_size_id = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_category
    {
        get { return _product_category; }
        set { _product_category = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public decimal charge
    {
        get { return _charge; }
        set { _charge = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int qty
    {
        get { return _qty; }
        set { _qty = value; }
    }

    public static AccountModel[] GetModelsByOrder(bool order)
    {
        NHibernate.Expression.Order o1 = new NHibernate.Expression.Order("qty", true);
        NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o1 };
        return AccountModel.FindAll(oo);
    }

    public static DataTable GetModelsBySizeAndCompanyProductCategory(int product_size_id, int shipping_company, int product_category)
    {
        string sql = @"select * from tb_account 
where shipping_company_id="+ shipping_company+" and product_size_id="+ product_size_id+" and product_category="+ product_category;
       // throw new Exception(sql);
        return Config.ExecuteDataTable(sql);
    }
}
