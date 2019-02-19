// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	4/29/2007 5:26:15 PM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_part_comment")]
[Serializable]
public class ProductDescModel : ActiveRecordBase<ProductDescModel>
{
    int _id;
    //int _menu_child_serial_no;
    string _part_comment;
    int _part_sku;
    string _part_short_comment;

    [Property]
    public int part_sku
    {
        get { return _part_sku; }
        set { _part_sku = value; }
    }

    public ProductDescModel()
    {

    }

    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    public static ProductDescModel GetProductModel(int _id)
    {
        ProductDescModel[] models = ProductDescModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new ProductDescModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string part_comment
    {
        get { return _part_comment; }
        set { _part_comment = value; }
    }

    [Property]
    public string part_short_comment
    {
        get { return _part_short_comment; }
        set { _part_short_comment = value; }
    }


    public void SavePartComment(int sku, string comment, string comment_short)
    {
        ProductDescModel.DeleteAll(" part_sku = '" + sku.ToString() + "' ");
        ProductDescModel pdm = new ProductDescModel();
        pdm.part_comment = comment;
        pdm.part_sku = sku;
        if (comment_short != null)
            pdm.part_short_comment = comment_short;
        pdm.Create();
    }
}
