// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-4 15:00:26
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class CountryModel
{
    public static tb_country GetCountryModel(nicklu2Entities context, int id)
    {
        return context.tb_country.Single(me => me.id.Equals(id));
    }
}
