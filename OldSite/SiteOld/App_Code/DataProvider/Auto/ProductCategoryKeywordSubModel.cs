
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	20/03/2009 5:38:42 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_product_category_keyword_sub")]
[Serializable]
public class ProductCategoryKeywordSubModel : ActiveRecordBase<ProductCategoryKeywordSubModel>
{

    public ProductCategoryKeywordSubModel()
    {

    }

    public static ProductCategoryKeywordSubModel GetProductCategoryKeywordSubModel(int _id)
    {
        ProductCategoryKeywordSubModel[] models = ProductCategoryKeywordSubModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductCategoryKeywordSubModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _parent_id;

    [Property]
    public int parent_id
    {
        get { return _parent_id; }
        set { _parent_id = value; }
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
    DateTime _regdate;
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }
}

