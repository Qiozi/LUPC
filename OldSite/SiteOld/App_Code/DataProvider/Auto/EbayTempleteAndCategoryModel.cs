
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	5/9/2010 1:26:08 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_templete_and_category")]
[Serializable]
public class EbayTempleteAndCategoryModel : ActiveRecordBase<EbayTempleteAndCategoryModel>
{

    public EbayTempleteAndCategoryModel()
    {

    }

    public static EbayTempleteAndCategoryModel GetEbayTempleteAndCategoryModel(int _id)
    {
        EbayTempleteAndCategoryModel[] models = EbayTempleteAndCategoryModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbayTempleteAndCategoryModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _templete_id;

    [Property]
    public int templete_id
    {
        get { return _templete_id; }
        set { _templete_id = value; }
    }

    int _sys_category_id;

    [Property]
    public int sys_category_id
    {
        get { return _sys_category_id; }
        set { _sys_category_id = value; }
    }

    string _part_brand;

    [Property]
    public string part_brand
    {
        get { return _part_brand; }
        set { _part_brand = value; }
    }

    int _part_category_id;

    [Property]
    public int part_category_id
    {
        get { return _part_category_id; }
        set { _part_category_id = value; }
    }

    int _is_flash;

    [Property]
    public int is_flash
    {
        get { return _is_flash; }
        set { _is_flash = value; }
    }


}

