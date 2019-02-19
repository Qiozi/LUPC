using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Reflection;
using System.Security;

/// <summary>
/// PropertiesAttribute 的摘要说明
/// </summary>
[System.AttributeUsage(AttributeTargets.Property | AttributeTargets.Field,
         AllowMultiple = true)]
public class PropertiesAttribute : Attribute
{
    public PropertiesAttribute(string name, string description)
    {
        this._name = name;
        this._description = description;
    }

    public PropertiesAttribute(string name, string description, string url)
    {
        this._name = name;
        this._description = description;
        this._url = url;
    }

    string _name = "";
    string _description = "";
    string _url = "";

    public string name
    {
        get { return _name; }
        set { _name = value; }
    }

    public string description
    {
        get { return _description; }
        set { _description = value; }
    }

    public string url
    {
        get { return _url; }
        set { _url = value; }
    }
}

public class KeyText
{
    public static string value = "value";
    public static string description = "description";
    public static string name = "name";
}

public class PropertiesAttributeHelper
{
    public static DataTable EnumToDatatable(Type t)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add(KeyText.value);
        dt.Columns.Add(KeyText.name);
        dt.Columns.Add(KeyText.description);

        Type tattr = typeof(PropertiesAttribute);
        FieldInfo[] fis = t.GetFields(BindingFlags.Public |
            BindingFlags.Static);
        System.Collections.ArrayList names = new System.Collections.ArrayList();

        foreach (string name in Enum.GetNames(t))
        {
            names.Add(name.ToLower());
        }

        for (int i = 0; i < names.Count; i++)
        {
            foreach (FieldInfo fi in fis)
            {
                if (string.Compare(fi.Name, names[i].ToString(), true) != 0)
                    continue;


                PropertiesAttribute[] pa = (PropertiesAttribute[])fi.GetCustomAttributes(tattr, false);
                if (pa.Length == 0)
                    throw new Exception("Enum Attribute 没有标记");
                PropertiesAttribute attr = pa[0];
                object value = new object();
                foreach (int o in Enum.GetValues(t))
                {
                    if (Enum.GetName(t, o).ToString() == fi.GetValue(t).ToString())
                    {
                        value = o;
                        break;
                    }
                }

                DataRow row = dt.NewRow();
                row[KeyText.description] = attr.description;
                row[KeyText.name] = attr.name;
                row[KeyText.value] = value.ToString();// fi.GetValue(enumtype);
                if (attr.description != null)
                    dt.Rows.Add(row);
            }

        }
        return dt;
    }
}