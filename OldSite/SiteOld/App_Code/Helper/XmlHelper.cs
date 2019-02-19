using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Xml;

/// <summary>
/// Summary description for XmlHelper
/// </summary>
public class XmlHelper
{
	public XmlHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string ChangeString(string s)
    {
        if (s.Length > 1)
        {
            s = s.Replace("'", "&apos");
            s = s.Replace("\"", "&quot;");
            s = s.Replace("&", "&amp;");
            s = s.Replace("<", "&lt;");
            s = s.Replace(">", "&rt;");
        }
        return s;
    }


    public DataTable GetForXmlFileStore(string filename)
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(System.Web.HttpContext.Current.Server.MapPath("~/q_admin/XmlStore/" + filename));
        XmlNodeReader reader = new XmlNodeReader(doc);
        DataSet ds = new DataSet();
        ds.ReadXml(reader);
        reader.Close();
        return ds.Tables[0];
    }
}
