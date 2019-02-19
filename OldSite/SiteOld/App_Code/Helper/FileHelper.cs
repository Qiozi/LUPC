using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// FileHelper 的摘要说明
/// </summary>
public class FileHelper
{
	public FileHelper()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

   

    /// 
    /// 下载类型
    /// fileexp">文件扩展名
    /// 
    public string checktype(string fileExt)
    {
        string ContentType;
        switch (fileExt)
        {
            case ".asf":
                ContentType = "video/x-ms-asf"; break;
            case ".avi":
                ContentType = "video/avi"; break;
            case ".doc":
                ContentType = "application/msword"; break;
            case ".zip":
                ContentType = "application/zip"; break;
            case ".xls":
                ContentType = "application/vnd.ms-excel"; break;
            case ".gif":
                ContentType = "image/gif"; break;
            case ".jpg":
                ContentType = "image/jpeg"; break;
            case "jpeg":
                ContentType = "image/jpeg"; break;
            case ".wav":
                ContentType = "audio/wav"; break;
            case ".mp3":
                ContentType = "audio/mpeg3"; break;
            case ".mpg":
                ContentType = "video/mpeg"; break;
            case ".mepg":
                ContentType = "video/mpeg"; break;
            case ".rtf":
                ContentType = "application/rtf"; break;
            case ".html":
                ContentType = "text/html"; break;
            case ".htm":
                ContentType = "text/html"; break;
            case ".txt":
                ContentType = "text/plain"; break;
            default:
                ContentType = "application/octet-stream";
                break;
        }
        return ContentType;
    }

    public DataTable FindAllFilenameByPath(string path)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("filename");
        dt.Columns.Add("modify_datetime");
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(path);
        System.IO.FileInfo[] fi = dir.GetFiles();
        for (int i = 0; i < fi.Length; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = fi[i].Name;
            dr[1] = fi[i].LastWriteTime;
            dt.Rows.Add(dr);
        }
        return dt;

    }

    public static void GenerateFile(string  url, string content)
    {
        System.IO.StreamWriter sw = new System.IO.StreamWriter(url, false, System.Text.Encoding.UTF8);
        sw.Write(content);
        sw.Close();
    }

    public static string ReadFile(string url)
    {
        string str = "";
        System.IO.StreamReader sr = new System.IO.StreamReader( url);
        str = sr.ReadToEnd();
        sr.Close();
        sr.Dispose();
        return str;
    }
}
