// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-12-15 0:15:24
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;


[ActiveRecord("tb_top")]
public class TopModel : ActiveRecordBase<TopModel>
{
    int _top_id;
 
    string _top_comment;
    int _top_sku;
    int _top_type;

    public TopModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int top_id
    {
        get { return _top_id; }
        set { _top_id = value; }
    }
    public static TopModel GetTopModel(int _top_id)
    {
        TopModel[] models = TopModel.FindAllByProperty("top_id", _top_id);
        if (models.Length == 1)
            return models[0];
        else
            return new TopModel();
    }


    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string top_comment
    {
        get { return _top_comment; }
        set { _top_comment = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public int top_sku
    {
        get { return _top_sku; }
        set { _top_sku = value; }
    }


    [Property]
    public int top_type
    {
        get { return _top_type; }
        set { _top_type = value; }
    }
}
