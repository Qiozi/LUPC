using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Castle.ActiveRecord;
/// <summary>
/// KeywordModel 的摘要说明
/// </summary>
[ActiveRecord("tb_keyword")]
[Serializable]
public class KeywordModel : ActiveRecordBase<KeywordModel>
{
    int _id;
    string _keyword;
    public KeywordModel()
    {
        //
        // TODO: 在此处添加构造函数逻辑
        //
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
    public static KeywordModel GetKeywordModelModel(int _id)
    {
        KeywordModel[] models = KeywordModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new KeywordModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string keyword
    {
        get { return _keyword; }
        set { _keyword = value; }
    }

    public static KeywordModel[] FindAllByOrder(bool order)
    {
        NHibernate.Expression.Order o = new NHibernate.Expression.Order("id", order);
        NHibernate.Expression.Order[] oo= new NHibernate.Expression.Order[]{o};

       return  KeywordModel.FindAll(oo);
    }
}

