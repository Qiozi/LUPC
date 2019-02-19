
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	06/05/2009 7:30:12 AM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;

[Serializable]
public class CartTempModel 
{

    public static LU.Data.tb_cart_temp[] GetModelsByTmeCode(nicklu2Entities context, int tmp_code)
    {
        //return CartTempModel.FindAllByProperty("cart_temp_code", tmp_code);
        return context.tb_cart_temp.Where(me => me.cart_temp_code.Value.Equals(tmp_code)).ToList().ToArray();
    }

    public static DataTable GetModelsDTByTmeCode(int tmp_code)
    {
        return Config.ExecuteDataTable("select * from tb_cart_temp where cart_temp_code='" + tmp_code + "' ");
    }
}
