// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-12-12 23:53:13
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;



[ActiveRecord("tb_update_table_fields")]
public class UpdateTableFieldsModel : ActiveRecordBase<UpdateTableFieldsModel>
{
    int _update_table_field_id;
    string _update_field_comment;
    string _update_table_field_name;
    string _update_table_name;

    public UpdateTableFieldsModel()
    {

    }


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int update_table_field_id
    {
        get { return _update_table_field_id; }
        set { _update_table_field_id = value; }
    }
    public static UpdateTableFieldsModel GetUpdateTableFieldsModel(int _update_table_field_id)
    {
        UpdateTableFieldsModel[] models = UpdateTableFieldsModel.FindAllByProperty("update_table_field_id", _update_table_field_id);
        if (models.Length == 1)
            return models[0];
        else
            return new UpdateTableFieldsModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string update_field_comment
    {
        get { return _update_field_comment; }
        set { _update_field_comment = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string update_table_field_name
    {
        get { return _update_table_field_name; }
        set { _update_table_field_name = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string update_table_name
    {
        get { return _update_table_name; }
        set { _update_table_name = value; }
    }

    //public static UpdateTableFieldsModel[] FindModels(string comment)
    //{
    //    return UpdateTableFieldsModel.FindAllByProperty("update_field_comment", comment);
    //}

    ///// <summary>
    ///// Comment is in database
    ///// </summary>
    ///// <param name="comment"></param>
    ///// <returns></returns>
    //public static bool IsExistComment(string comment)
    //{
    //    if (FindModels(comment).Length > 0)
    //        return true;
    //    return false;
    //}

    public static UpdateTableFieldsModel[] FindModels(string table_name)
    {
        return UpdateTableFieldsModel.FindAllByProperty("update_table_name", table_name);
    }

    public static UpdateTableFieldsModel[] FindProductTableFields()
    {
        return UpdateTableFieldsModel.FindModels("tb_product");

    }

    public static UpdateTableFieldsModel[] FindUpdateTableFields()
    {
        return UpdateTableFieldsModel.FindModels("tb_update_product_store");
    }
}
