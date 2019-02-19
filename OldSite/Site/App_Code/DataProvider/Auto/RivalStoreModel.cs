// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-8-20 10:34:30
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Data;
using System.Linq;


[Serializable]
public class RivalStoreModel 
{

    public tb_rival_store[] FindModelsByManufactureCode(nicklu2Entities context, string manufacture_code)
    {
        // return RivalStoreModel.FindAllByProperty("rival_manufacture_code", manufacture_code);
        return context.tb_rival_store.Where(me => me.rival_manufacture_code.Equals(manufacture_code)).ToList().ToArray();
    }

    public DataTable FindModelsByManufactureCode(int lu_sku, string manufacture_code)
    {
        return Config.ExecuteDataTable(@"select rival_ltd_id, rival_manufacture_code, rival_price, regdate from tb_rival_store where rival_manufacture_code='" + manufacture_code + @"'");
    }
}

