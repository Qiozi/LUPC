using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for ProdStock
/// </summary>
public class ProdStock
{
    public ProdStock()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// 取得商品库存状态字符串
    /// </summary>
    /// <param name="stock"></param>
    /// <returns></returns>
    public static string GetProdStockString(int stock)
    {        
        if (stock == 1)
            return "<span style='color:green; font-size:8.5pt'>In Stock</span>";
        else if (stock == 2)
            return "<span style='color:green; font-size:8.5pt'>Stock Available</span>";
        else if (stock == 3)
            return "<span style='color:green; font-size:8.5pt'>Stock Low(Call)</span>";
        else if (stock == 4)
            return "<span style='color:#B9C7B9; font-size:8.5pt'>Out of Stock</span>";
        else
            return "<span style='color:#B9C7B9; font-size:8.5pt'>Back Order</span>";
    }

    /// <summary>
    /// 取得商品库存状态字符串
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public static string GetProdStockString(nicklu2Model.tb_product product)
    {
        int storeSum ;
        int.TryParse(product.product_store_sum.ToString(),out storeSum);

        int ltdStock;
        int.TryParse(product.ltd_stock.ToString(),out ltdStock);


        int currStock = 0;
        if(storeSum>2)
            currStock =  2;
        else if(ltdStock>2)
            currStock =  2;
        else if((storeSum + ltdStock)>2)
            currStock = 2;
        else if (storeSum<=2 && storeSum >0 )
            currStock =3;
        else if ((storeSum + ltdStock)<=2 
            && (storeSum + ltdStock >0))
            currStock = 3;
        else if(ltdStock <=2 && ltdStock >0)
            currStock = 3;
        else if((storeSum + ltdStock) ==0)
            currStock = 4;
        else if ((storeSum + ltdStock) <0)
            currStock = 5;

        return GetProdStockString(currStock);
    }
}