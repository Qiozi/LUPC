
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	5/7/2010 11:01:14 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_ebay_system_main_comment")]
[Serializable]
public class EbaySystemMainCommentModel : ActiveRecordBase<EbaySystemMainCommentModel>
{

    public EbaySystemMainCommentModel()
    {

    }

    public static EbaySystemMainCommentModel GetEbaySystemMainCommentModel(int _id)
    {
        EbaySystemMainCommentModel[] models = EbaySystemMainCommentModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new EbaySystemMainCommentModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    int _sub_id;

    [Property]
    public int sub_id
    {
        get { return _sub_id; }
        set { _sub_id = value; }
    }

    string _comm_name;

    [Property]
    public string comm_name
    {
        get { return _comm_name; }
        set { _comm_name = value; }
    }

    string _comment;

    [Property]
    public string comment
    {
        get { return _comment; }
        set { _comment = value; }
    }


}

