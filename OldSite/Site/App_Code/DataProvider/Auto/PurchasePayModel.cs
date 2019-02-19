// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-27 17:29:12
//
// // // // // // // // // // // // // // // //

using LU.Data;
using System;
using System.Linq;

[Serializable]
public class PurchasePayModel 
{    

    public static tb_purchase_pay[] GetModelsByPurchaseSerial(nicklu2Entities context, int purchase_id)
    {
        // return PurchasePayModel.FindAllByProperty("purchase_serial_no", purchase_id);
        var query = context.tb_purchase_pay.Where(me => me.purchase_serial_no.Value.Equals(purchase_id)).ToList().ToArray();
        return query;
    }
}
