
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	12/7/2008 12:19:03 PM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class ProductShippingFeeModel 
{
    public static tb_product_shipping_fee GetProductShippingFeeModel(nicklu2Entities context, int id)
    {
        return context.tb_product_shipping_fee.Single(me => me.prod_shipping_fee_id.Equals(id));
    }
}

