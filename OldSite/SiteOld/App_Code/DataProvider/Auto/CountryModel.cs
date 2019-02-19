// // // // // // // // // // // // // // // // 
//
// Author: Qiozi@msn.com 
// Date:	2007-6-4 15:00:26
//
// // // // // // // // // // // // // // // //
using Castle.ActiveRecord;
using System;

[ActiveRecord("tb_country")]
[Serializable]
public class CountryModel : ActiveRecordBase<CountryModel>
{
    int _id;
    string _code;
    string _name;

    public CountryModel()
    {
       
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
    public static CountryModel GetCountryModel(int _id)
    {
        CountryModel[] models = CountryModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new CountryModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string code
    {
        get { return _code; }
        set { _code = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    public static int FindIDByCode(string code)
    {
        CountryModel[] cms = CountryModel.FindAllByProperty("code", code);
        if (cms.Length == 1)
            return cms[0].id;
        return -1;
    }
}
