// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/30/2007 2:22:40 PM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class ProducterModel
{

    public static tb_producter GetProducterModel(nicklu2Entities context, int id)
    {
        return context.tb_producter.Single(me => me.producter_serial_no.Equals(id));
    }

    public static tb_producter[] GetProducterModelByAll(nicklu2Entities context, bool orderasc)
    {
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("producter_serial_no", orderasc);
        //NHibernate.Expression.Order[] os = new NHibernate.Expression.Order[] { o };
        //return ProducterModel.FindAll(os);
        var query = context.tb_producter.OrderByDescending(me => me.producter_serial_no).ToList().ToArray();
        return query;
    }
}
