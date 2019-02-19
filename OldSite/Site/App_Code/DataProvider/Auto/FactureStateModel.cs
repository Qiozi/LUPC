// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-20 20:58:51
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class FactureStateModel
{
    public static tb_facture_state GetFactureStateModel(nicklu2Entities context, int id)
    {
        return context.tb_facture_state.First(me => me.facture_state_serial_no.Equals(id));
    }

    public static tb_facture_state[] FindModelsByShowit(nicklu2Entities context)
    {
        //NHibernate.Expression.EqExpression eq = new NHibernate.Expression.EqExpression("showit", true);
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("priority", true);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        //return FactureStateModel.FindAll(oo, eq);

        var query = context.tb_facture_state.Where(me => me.showit.Value.Equals(true)).ToList();
        return query.ToArray();
    }
}
