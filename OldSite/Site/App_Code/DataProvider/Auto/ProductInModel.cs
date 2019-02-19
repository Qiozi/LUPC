// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-27 14:09:12
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class ProductInModel
{
    public static tb_product_in GetProductInModel(nicklu2Entities context, int id)
    {
        return context.tb_product_in.Single(me => me.product_in_serial_no.Equals(id));
    }

    public static tb_product_in[] GetProductInModelsByPurchase(nicklu2Entities context, int purchase_serial_no)
    {
        var serialNO = purchase_serial_no.ToString();
        var query = context.tb_product_in.Where(me => me.purchase_serial_no.Equals(serialNO)).ToList().ToArray();
        return query;// ProductInModel.FindAllByProperty("purchase_serial_no", purchase_serial_no);
    }
}
