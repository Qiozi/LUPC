
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	5/9/2010 12:46:07 PM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class ProductCategoryNewModel 
{
    public static tb_product_category_new GetProductCategoryNewModel(nicklu2Entities context, int id)
    {
        return context.tb_product_category_new.Single(me => me.category_id.Equals(id));
    }

}

