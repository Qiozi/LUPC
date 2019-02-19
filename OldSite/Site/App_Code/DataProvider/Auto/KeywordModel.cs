using LU.Data;
using System;
using System.Linq;

/// <summary>
/// KeywordModel 的摘要说明
/// </summary>
[Serializable]
public class KeywordModel
{
    public static tb_keyword GetKeywordModelModel(nicklu2Entities context, int id)
    {
        return context.tb_keyword.Single(me => me.id.Equals(id));
    }

    public static tb_keyword[] FindAllByOrder(nicklu2Entities context)
    {
        //NHibernate.Expression.Order o = new NHibernate.Expression.Order("id", order);
        //NHibernate.Expression.Order[] oo = new NHibernate.Expression.Order[] { o };
        var query = context.tb_keyword.OrderBy(me => me.id).ToList();
        return query.ToArray();
    }
}

