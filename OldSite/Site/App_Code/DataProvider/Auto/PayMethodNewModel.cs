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
public class PayMethodNewModel
{
    public static tb_pay_method_new GetPayMethodNewModel(nicklu2Entities context, int id)
    {
        return context.tb_pay_method_new.FirstOrDefault(me => me.pay_method_serial_no.Equals(id));
    }


    public static tb_pay_method_new [] GetModelsByOrder(nicklu2Entities context)
    {
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("taxis", true);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        //return PayMethodNewModel.FindAll(oo);
        var query = context.tb_pay_method_new.Where(me => me.taxis.Value.Equals(1)).ToList();
        return query.ToArray();

    }
}
