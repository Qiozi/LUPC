
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	11/26/2008 4:16:33 AM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class OtherIncMatchLuSkuModel
{
    public static LU.Data.tb_other_inc_match_lu_sku GetOtherIncMatchLuSkuModel(nicklu2Entities context, int id)
    {
        return context.tb_other_inc_match_lu_sku.FirstOrDefault(me => me.id.Equals(id));
    }


}

