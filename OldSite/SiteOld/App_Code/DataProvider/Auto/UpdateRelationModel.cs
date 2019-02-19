// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-12-11 23:26:26
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;


[ActiveRecord("tb_update_relation")]
public class UpdateRelationModel : ActiveRecordBase<UpdateRelationModel>
{
    int _update_relation_id;
    string _update_relation_table_a;
    string _update_relation_table_a_fields;
    string _update_relation_table_b;
    string _update_relation_table_b_fields;
    string _comment;
    DateTime _regdate;
    string _update_relation_table_a_fields_comment;
    string _update_relation_table_b_fields_comment;

    public UpdateRelationModel()
    {

    }


    

    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int update_relation_id
    {
        get { return _update_relation_id; }
        set { _update_relation_id = value; }
    }
    public static UpdateRelationModel GetUpdateRelationModel(int _update_relation_id)
    {
        UpdateRelationModel[] models = UpdateRelationModel.FindAllByProperty("update_relation_id", _update_relation_id);
        if (models.Length == 1)
            return models[0];
        else
            return new UpdateRelationModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string update_relation_table_a
    {
        get { return _update_relation_table_a; }
        set { _update_relation_table_a = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string update_relation_table_a_fields
    {
        get { return _update_relation_table_a_fields; }
        set { _update_relation_table_a_fields = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string update_relation_table_b
    {
        get { return _update_relation_table_b; }
        set { _update_relation_table_b = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string update_relation_table_b_fields
    {
        get { return _update_relation_table_b_fields; }
        set { _update_relation_table_b_fields = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string comment
    {
        get { return _comment; }
        set { _comment = value; }
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
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string update_relation_table_a_fields_comment
    {
        get { return _update_relation_table_a_fields_comment; }
        set { _update_relation_table_a_fields_comment = value; }
    }

    [Property]
    public string update_relation_table_b_fields_comment
    {
        get { return _update_relation_table_b_fields_comment; }
        set { _update_relation_table_b_fields_comment = value; }
    }

    public static UpdateRelationModel[] FindModels(string comment)
    {
        return UpdateRelationModel.FindAllByProperty("comment", comment);
    }

    public static bool IsExistComment(string comment)
    {
        if (UpdateRelationModel.FindModels(comment).Length > 0)
            return true;
        return false;
    }

    public static DataTable FindModelsByCommentDistinct()
    {
        return Config.ExecuteDataTable("select distinct comment from tb_update_relation");
    }


    public static UpdateRelationModel[] FindModelsByRelationTableName(string field_name, string comment)
    {
        NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("update_relation_table_b_fields", field_name);
        NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("update_relation_table_b", Config.update_table_name);
      
        NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
        NHibernate.Expression.EqExpression eq3 = new NHibernate.Expression.EqExpression("comment", comment);

        NHibernate.Expression.AndExpression a2 = new NHibernate.Expression.AndExpression(a, eq3);
        return UpdateRelationModel.FindAll(a2);
    }
}
