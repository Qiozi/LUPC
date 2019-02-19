// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-08-05 19:50:14
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System.Linq;
using System;

/// <summary>
/// Summary description for ProductCoefficientCategory
/// </summary>
[Serializable]
public class ProductCoefficientCategory
{
    public tb_product_coefficient_category[] FindModelsByCategoryID(nicklu2Entities context, int categoryID)
    {
        //   return ProductCoefficientCategory.FindAllByProperty("menu_child_serial_no", categoryID);

        var query = context.tb_product_coefficient_category.Where(me => me.menu_child_serial_no.Value.Equals(categoryID)).ToList().ToArray();
        return query;
    }
}
