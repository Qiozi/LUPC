
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	5/9/2010 12:46:07 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_product_category_new")]
[Serializable]
public class ProductCategoryNewModel : ActiveRecordBase<ProductCategoryNewModel>
{

    public ProductCategoryNewModel()
    {

    }

    public static ProductCategoryNewModel GetProductCategoryNewModel(int _category_id)
    {
        ProductCategoryNewModel[] models = ProductCategoryNewModel.FindAllByProperty("category_id", _category_id);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductCategoryNewModel();
    }

    int _category_id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int category_id
    {
        get { return _category_id; }
        set { _category_id = value; }
    }

    string _category_name;

    [Property]
    public string category_name
    {
        get { return _category_name; }
        set { _category_name = value; }
    }

    int _category_type;

    [Property]
    public int category_type
    {
        get { return _category_type; }
        set { _category_type = value; }
    }

    int _is_ebay;

    [Property]
    public int is_ebay
    {
        get { return _is_ebay; }
        set { _is_ebay = value; }
    }

    int _priority;

    [Property]
    public int priority
    {
        get { return _priority; }
        set { _priority = value; }
    }

    int _parent_category_id;

    [Property]
    public int parent_category_id
    {
        get { return _parent_category_id; }
        set { _parent_category_id = value; }
    }

    bool _showit;

    [Property]
    public bool showit
    {
        get { return _showit; }
        set { _showit = value; }
    }

    int _view_count;

    [Property]
    public int view_count
    {
        get { return _view_count; }
        set { _view_count = value; }
    }


}

