// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	22/02/2009 2:56:15 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_advertise_page")]
[Serializable]
public class AdvertisePageModel : ActiveRecordBase<AdvertisePageModel>
{

    public AdvertisePageModel()
    {

    }

    public static AdvertisePageModel GetAdvertisePageModel(int _id)
    {
        AdvertisePageModel[] models = AdvertisePageModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new AdvertisePageModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    string _summary;

    [Property]
    public string summary
    {
        get { return _summary; }
        set { _summary = value; }
    }
    string _title;

    [Property]
    public string title
    {
        get { return _title; }
        set { _title = value; }
    }
    string _conent;

    [Property]
    public string conent
    {
        get { return _conent; }
        set { _conent = value; }
    }

    DateTime _regdate;
    [Property]
    public DateTime regdate
    {
        get { return _regdate; }
        set { _regdate = value; }
    }

    DateTime _last_regdate;
    [Property]
    public DateTime last_regdate
    {
        get { return _last_regdate; }
        set { _last_regdate = value; }
    }
}

