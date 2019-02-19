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
using System.Net;
using System.IO;

/// <summary>
/// Summary description for HttpHelper
/// </summary>
public class HttpHelper
{
	public HttpHelper()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    /// <summary>
    /// request page info.
    /// </summary>
    /// <param name="url"></param>
    /// <returns></returns>
    public string HttpGet(string url)
    {
        System.Net.HttpWebRequest req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
        WebResponse resp = req.GetResponse();
        StreamReader sr = new StreamReader(resp.GetResponseStream(), System.Text.Encoding.UTF8);
        string sReturn = sr.ReadToEnd().Trim();
        resp.Close();
        sr.Close();
        return sReturn;
    }
    /// <summary>
    /// post info to remote web site , and get return info.
    /// </summary>
    /// <param name="url"></param>
    /// <param name="parameters"></param>
    /// <param name="ProxyString"></param>
    /// <param name="referer"></param>
    /// <returns></returns>
    public string HttpPost(string url, string parameters, string ProxyString, string referer)
    {
        byte[] bytes = System.Text.Encoding.Default.GetBytes(parameters);

        HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
        req.Proxy = new WebProxy(ProxyString, true);
        req.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.0; en-US; rv:1.8) Gecko/20051111 Firefox/1.5";
        req.Accept = "text/xml,application/xml,application/xhtml+xml,text/html";
        req.KeepAlive = true;
        req.Referer = string.Format(referer);
        req.ContentType = "application/x-www-form-urlencoded";
        req.Method = "POST";

        req.ContentLength = bytes.Length;
        Stream os = req.GetRequestStream();
        os.Write(bytes, 0, bytes.Length);
        os.Close();
        HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
        if (resp == null)
            return null;
        StreamReader sr = new StreamReader(resp.GetResponseStream(), System.Text.Encoding.UTF8);
        string sReturn = sr.ReadToEnd().Trim();
        resp.Close();
        sr.Close();
        return sReturn;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="page_name"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    public string GetPageString(string page_name, string url)
    {
        string p = this.HttpGet(url);
        TempleteReplaceParameterModel[] models = TempleteReplaceParameterModel.FindModelsByPageName(page_name);
        for (int i = 0; i < models.Length; i++)
        {
            if (p.Length > 0)
            {
                p = p.Replace(models[i].source_string, models[i].replace_string);
            }
        }
        return p;
    }
}
