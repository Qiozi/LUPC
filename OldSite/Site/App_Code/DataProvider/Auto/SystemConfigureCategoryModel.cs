// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-5-22 15:28:18
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class SystemConfigureCategoryModel
{
    public static tb_system_configure_category GetSystemConfigureCategoryModel(nicklu2Entities context, int id)
    {
        return context.tb_system_configure_category.Single(me => me.system_configure_category_serial_no.Equals(id));
    }
}
