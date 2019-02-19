// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2008-8-20 10:34:30
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_rival_url")]
[Serializable]
public class RivalUrlModel : ActiveRecordBase<RivalUrlModel>
{

    public RivalUrlModel()
    {

    }

    public static RivalUrlModel GetRivalUrlModel(int id)
    {
        RivalUrlModel[] models = RivalUrlModel.FindAllByProperty("id", id);
        if (models.Length == 1)
            return models[0];
        else
            return new RivalUrlModel();
    }
    int _id;
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    int _rival_ltd_id;
    [Property]
    public int rival_ltd_id
    {
        get { return _rival_ltd_id; }
        set { _rival_ltd_id = value; }
    }
    string _rival_url;
    [Property]
    public string rival_url
    {
        get { return _rival_url; }
        set { _rival_url = value; }
    }
    
}

