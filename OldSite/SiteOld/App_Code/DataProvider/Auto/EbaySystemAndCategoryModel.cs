using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Castle.ActiveRecord;
/// <summary>
/// Summary description for EbaySystemAndCategoryModel
/// </summary>
[ActiveRecord("tb_ebay_system_and_category")]
[Serializable]
public class EbaySystemAndCategoryModel : ActiveRecordBase<EbaySystemAndCategoryModel>
{

    public EbaySystemAndCategoryModel()
    {

    }

    public static EbaySystemAndCategoryModel GetEbaySystemAndCategoryModel(int _ID)
    {
        EbaySystemAndCategoryModel[] models = EbaySystemAndCategoryModel.FindAllByProperty("ID", _ID);
        if (models.Length == 1)
            return models[0];
        else
            return new EbaySystemAndCategoryModel();
    }

    int _ID;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int ID
    {
        get { return _ID; }
        set { _ID = value; }
    }

    int _eBaySysCategoryID;

    [Property]
    public int eBaySysCategoryID
    {
        get { return _eBaySysCategoryID; }
        set { _eBaySysCategoryID = value; }
    }

    int _SystemSku;

    [Property]
    public int SystemSku
    {
        get { return _SystemSku; }
        set { _SystemSku = value; }
    }

    /// <summary>
    /// 取得系统的Category , 一个系统可能会有多个，，但只取一个。
    /// </summary>
    /// <param name="SystemSKU"></param>
    /// <returns></returns>
    public static int GetSystemCategoryId(int SystemSKU)
    {
        EbaySystemAndCategoryModel[] models = EbaySystemAndCategoryModel.FindAllByProperty("SystemSku", SystemSKU);
        if (models.Length >0)
            return models[0].eBaySysCategoryID;
        else
            return -1;
    }
}

