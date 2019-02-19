using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Castle.ActiveRecord;

/// <summary>
/// Summary description for TempleteReplaceParameterModel
/// </summary>
[ActiveRecord("tb_templete_replace_parameter")]
[Serializable]
public class TempleteReplaceParameterModel : ActiveRecordBase<TempleteReplaceParameterModel>
{
	public TempleteReplaceParameterModel()
	{
		//
		// TODO: Add constructor logic here
		//
	}
     int _id;
    string _source_string;
    string _replace_string;
    string _page_name;


    /// <summary>
    /// 
    /// </summary>
    [PrimaryKey(PrimaryKeyType.Identity)]
    public int id
    {
        get { return _id; }
        set { _id = value; }
    }
    public static TempleteReplaceParameterModel GetCountryModel(int _id)
    {
        TempleteReplaceParameterModel[] models = TempleteReplaceParameterModel.FindAllByProperty("id", _id);
        if (models.Length == 1)
            return models[0];
        else
            return new TempleteReplaceParameterModel();
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string source_string
    {
        get { return _source_string; }
        set { _source_string = value; }
    }

    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string replace_string
    {
        get { return _replace_string; }
        set { _replace_string = value; }
    }
    /// <summary>
    /// 
    /// </summary>
    [Property]
    public string page_name
    {
        get { return _page_name; }
        set { _page_name = value; }
    }

    public static TempleteReplaceParameterModel[] FindModelsByPageName(string page_name)
    {
        return TempleteReplaceParameterModel.FindAllByProperty("page_name", page_name);
    }
}
