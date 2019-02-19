using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using iTextSharp;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.IO;
using GDI = System.Drawing;

public partial class Q_Admin_download_order_pdf : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int orderCode = Util.GetInt32SafeFromQueryString(Page, "order_code", -1);
            string error = "";
            string pdf_path_file = "";
            PDFHelper ph = new PDFHelper();
            ph.CreatePDF(orderCode, ref pdf_path_file, ref error, this.Page);


            download(pdf_path_file);

            if(error != "")
                Response.Write(error); ;
        }
    }


    /// <summary>
    /// 
    /// </summary>
    /// <param name="filename">绝对路径</param>
    public void download(string filename)
    {
        //string path = Server.MapPath(filename);
        string path = filename;
        if (!File.Exists(path))
        {
            Response.Write("Sorry！File is not exist！！！" + path);
            return;
        }
        System.IO.FileInfo file = new System.IO.FileInfo(path);
        string fileFilt = ".asp|.aspx|.php|.jsp|.ascx|.config|.asa|......";  //不可下载的文件，务必要过滤干净
        string fileName = file.Name;
        string fileExt = fileName.Substring(fileName.LastIndexOf("."), fileName.Length - fileName.LastIndexOf(".")).Trim().ToLower();
        if (fileFilt.IndexOf(fileExt) != -1)
        {
            Response.Write("Sorry！system file don't download！！");
        }
        else
        {
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + HttpUtility.UrlEncode(fileName));
            Response.AddHeader("Content-Length", file.Length.ToString());
            FileHelper fh = new FileHelper();
            Response.ContentType = fh.checktype(HttpUtility.UrlEncode(fileExt));
            Response.WriteFile(file.FullName, true);
            Response.End();
        }
    }

    public int CustomerState
    {
        get
        {
            return Util.GetInt32SafeFromQueryString(Page, "customer_state", -1);
        }
    }
}
