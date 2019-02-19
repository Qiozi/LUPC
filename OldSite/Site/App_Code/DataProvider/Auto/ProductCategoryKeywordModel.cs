
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	20/03/2009 5:39:40 PM
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System;
using System.Linq;

[Serializable]
public class ProductCategoryKeywordModel 
{
    public static tb_product_category_keyword GetProductCategoryKeywordModel(nicklu2Entities context, int id)
    {
        return context.tb_product_category_keyword.Single(me => me.id.Equals(id));
    }


}

