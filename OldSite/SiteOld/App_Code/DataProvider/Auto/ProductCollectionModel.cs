// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008/1/9 2:28:11
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_product_collection")]
public class ProductCollectionModel : ActiveRecordBase<ProductCollectionModel>
{
    int _product_collection_id;
    string _product_collections;
    string _product_collection_topic;
    int _product_collection_redirect_path_type;
    string _product_collection_title;
    string _product_collection_title_path;
    DateTime _regdate;

    public ProductCollectionModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int product_collection_id
    {
        get { return _product_collection_id; }
        set { _product_collection_id = value; }
    }
    public static ProductCollectionModel GetProductCollectionModel(int _product_collection_id)
    {
        ProductCollectionModel[] models = ProductCollectionModel.FindAllByProperty("product_collection_id", _product_collection_id);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductCollectionModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string product_collections
    {
        get { return _product_collections; }
        set { _product_collections = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string product_collection_topic
    {
        get { return _product_collection_topic; }
        set { _product_collection_topic = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int product_collection_redirect_path_type
    {
        get { return _product_collection_redirect_path_type; }
        set { _product_collection_redirect_path_type = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string product_collection_title
    {
        get { return _product_collection_title; }
        set { _product_collection_title = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string product_collection_title_path
    {
        get { return _product_collection_title_path; }
        set { _product_collection_title_path = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }
}
