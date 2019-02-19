// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-10 19:51:47
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class ShippingCompanyModel 
{   
    public static tb_shipping_company GetShippingCompanyModel(nicklu2Entities context, int id)
    {
        return context.tb_shipping_company.SingleOrDefault(me => me.shipping_company_id.Equals(id));
    }
    public static tb_shipping_company[] GetModelsByOrder(nicklu2Entities context)
    {
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("qty", true);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        //return ShippingCompanyModel.FindAll(oo);
        var query = context.tb_shipping_company.OrderBy(me => me.qty).ToList();
        return query.ToArray();

    }
}
