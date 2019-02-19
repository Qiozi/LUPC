
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	5/10/2010 3:13:23 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_error_log")]
[Serializable]
public class ErrorLogModel : ActiveRecordBase<ErrorLogModel>
{

    public ErrorLogModel()
    {

    }

    public static ErrorLogModel GetErrorLogModel(int _id)
    {
        ErrorLogModel[] models = ErrorLogModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new ErrorLogModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    string _comment;

    [Property]
    public string comment
    {
        get { return _comment; }
        set { _comment = value; }
    }

    string _summary;

    [Property]
    public string summary
    {
        get { return _summary; }
        set { _summary = value; }
    }

    DateTime _regdate;
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }
}

