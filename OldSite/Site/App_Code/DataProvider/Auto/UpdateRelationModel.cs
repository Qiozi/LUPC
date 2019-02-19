// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-12-11 23:26:26
//
// // // // // // // // // // // // // // // //
using LU.Data;
using System.Data;
using System.Linq;


public class UpdateRelationModel
{

    public static LU.Data.tb_update_relation[] FindModels(nicklu2Entities context, string comment)
    {
        //return UpdateRelationModel.FindAllByProperty("comment", comment);
        return context.tb_update_relation.Where(me => me.comment.Equals(comment)).ToList().ToArray();
    }

    public static bool IsExistComment(nicklu2Entities context, string comment)
    {
        return FindModels(context, comment).Length > 0;
    }

    public static DataTable FindModelsByCommentDistinct()
    {
        return Config.ExecuteDataTable("select distinct comment from tb_update_relation");
    }


    //public static UpdateRelationModel[] FindModelsByRelationTableName(string field_name, string comment)
    //{
    //    //NHibernate.Expression.EqExpression eq1 = new NHibernate.Expression.EqExpression("update_relation_table_b_fields", field_name);
    //    //NHibernate.Expression.EqExpression eq2 = new NHibernate.Expression.EqExpression("update_relation_table_b", Config.update_table_name);

    //    //NHibernate.Expression.AndExpression a = new NHibernate.Expression.AndExpression(eq1, eq2);
    //    //NHibernate.Expression.EqExpression eq3 = new NHibernate.Expression.EqExpression("comment", comment);

    //    //NHibernate.Expression.AndExpression a2 = new NHibernate.Expression.AndExpression(a, eq3);
    //    //return UpdateRelationModel.FindAll(a2);
    //}
}
