// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-26 13:03:56
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class PurchaseModel
{
    public static tb_purchase GetPurchaseModel(nicklu2Entities context, int id)
    {
        return context.tb_purchase.Single(me => me.purchase_serial_no.Equals(id));
    }

    public static tb_purchase[] GetPurchaseModels(nicklu2Entities context)
    {
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("purchase_serial_no", true);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        //return PurchaseModel.FindAll(oo);
        return context.tb_purchase.OrderBy(me => me.purchase_serial_no).ToList().ToArray();
    }
}
