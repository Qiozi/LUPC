
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	11/10/2009 9:42:16 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_templete")]
[Serializable]
public class EbayTempleteModel : ActiveRecordBase<EbayTempleteModel>
{

    public EbayTempleteModel()
    {

    }

    public static EbayTempleteModel GetEbayTempleteModel(int _id)
    {
        EbayTempleteModel[] models = EbayTempleteModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbayTempleteModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    string _templete_comment;

    [Property]
    public string templete_comment
    {
        get { return _templete_comment; }
        set { _templete_comment = value; }
    }

    string _templete_content;

    [Property]
    public string templete_content
    {
        get { return _templete_content; }
        set { _templete_content = value; }
    }

    string _templete_content2;

    [Property]
    public string templete_content2
    {
        get { return _templete_content2; }
        set { _templete_content2 = value; }
    }

    string _templete_info;

    [Property]
    public string templete_info
    {
        get { return _templete_info; }
        set { _templete_info = value; }
    }

    string _templete_top;

    [Property]
    public string templete_top
    {
        get { return _templete_top; }
        set { _templete_top = value; }
    }

    int _templete_type;

    [Property]
    public int templete_type
    {
        get { return _templete_type; }
        set { _templete_type = value; }
    }
    DateTime _last_regdate;

    [Property]
    public DateTime last_regdate
    {
        get { return _last_regdate; }
        set { _last_regdate = value; }
    }

    string _comment;

    [Property]
    public string comment
    {
        get { return _comment; }
        set { _comment = value; }
    }

    string _templete_summ_1;

    [Property]
    public string templete_summ_1
    {
        get { return _templete_summ_1; }
        set { _templete_summ_1 = value; }
    }

    string _templete_summ_2;

    [Property]
    public string templete_summ_2
    {
        get { return _templete_summ_2; }
        set { _templete_summ_2 = value; }
    }


}

