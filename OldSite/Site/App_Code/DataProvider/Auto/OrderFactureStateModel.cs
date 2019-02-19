// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-25 23:15:50
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class OrderFactureStateModel
{
   
    public static tb_order_facture_state[] GetModelsByOrderCode(nicklu2Entities context, int order_code)
    {
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("order_facture_state_serial_no", false);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        //NHibernate.Expression.EqExpression eq = new NHibernate.Expression.EqExpression("order_code", order_code);
        //return OrderFactureStateModel.FindAll(oo, eq);
        var oc = order_code.ToString();
        var query = context.tb_order_facture_state.Where(me => me.order_code.Equals(oc)).OrderByDescending(me=>me.order_facture_state_serial_no).ToList();
        return query.ToArray();

    }

    public static void SaveModels(nicklu2Entities context, string order_code, int out_status, string note)
    {
        //OrderFactureStateModel model = new OrderFactureStateModel();
        //model.order_code = order_code;
        //model.facture_state_serial_no = out_status;
        //model.order_facture_note = note;
        //model.order_facture_state_create_date = DateTime.Now;
        //model.Create();
        var model = new tb_order_facture_state();
        model.order_code = order_code;
        model.facture_state_serial_no = out_status;
        model.order_facture_note = note;
        model.order_facture_state_create_date = DateTime.Now;
        context.tb_order_facture_state.Add(model);
        context.SaveChanges();
    }
}
