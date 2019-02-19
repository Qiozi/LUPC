
// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	02/06/2009 12:55:37 AM
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;
using System.Data;

[ActiveRecord("tb_business")]
[Serializable]
public class BusinessModel : ActiveRecordBase<BusinessModel>
{

    public BusinessModel()
    {

    }

    public static BusinessModel GetBusinessModel(int _id)
    {
        BusinessModel[] models = BusinessModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new BusinessModel();
    }

    int _id;

    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }

    string _business_title;

    [Property]
    public string business_title
    {
        get { return _business_title; }
        set { _business_title = value; }
    }

    string _business_img_url;

    [Property]
    public string business_img_url
    {
        get { return _business_img_url; }
        set { _business_img_url = value; }
    }

    string _business_content;

    [Property]
    public string business_content
    {
        get { return _business_content; }
        set { _business_content = value; }
    }


}

