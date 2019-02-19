using LU.Data;
using System;
using System.Linq;

/// <summary>
/// Summary description for EbaySystemAndCategoryModel
/// </summary>
[Serializable]
public class EbaySystemAndCategoryModel
{

    /// <summary>
    /// 取得系统的Category , 一个系统可能会有多个，，但只取一个。
    /// </summary>
    /// <param name="SystemSKU"></param>
    /// <returns></returns>
    public static int GetSystemCategoryId(nicklu2Entities context, int SystemSKU)
    {
        //var  models = EbaySystemAndCategoryModel.FindAllByProperty("SystemSku", SystemSKU);
        var query = context.tb_ebay_system_and_category.FirstOrDefault(me => me.SystemSku.Value.Equals(SystemSKU));
        if(query == null)
        {
            return -1;
        }
        else
        {
            return query.eBaySysCategoryID.Value;
        }
        //if (models.Length >0)
        //    return models[0].eBaySysCategoryID;
        //else
        //    return -1;
    }
}

