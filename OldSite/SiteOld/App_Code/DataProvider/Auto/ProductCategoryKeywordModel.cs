
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	20/03/2009 5:39:40 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_product_category_keyword")]
[Serializable]
public class ProductCategoryKeywordModel : ActiveRecordBase<ProductCategoryKeywordModel>
{

    public ProductCategoryKeywordModel()
    {

    }

    public static ProductCategoryKeywordModel GetProductCategoryKeywordModel(int _id)
    {
        ProductCategoryKeywordModel[] models = ProductCategoryKeywordModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductCategoryKeywordModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _category_id;

    [Property]
    public int category_id
    {
        get { return _category_id; }
        set { _category_id = value; }
    }

    string _keyword;

    [Property]
    public string keyword
    {
        get { return _keyword; }
        set { _keyword = value; }
    }

    bool _showit;

    [Property]
    public bool showit
    {
        get { return _showit; }
        set { _showit = value; }
    }


}

