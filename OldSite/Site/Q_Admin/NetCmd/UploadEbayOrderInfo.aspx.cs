using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

public partial class Q_Admin_NetCmd_UploadEbayOrderInfo : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            WriteFile();
        }
    }
    
    void WriteFile()
    {
        string path = "~/ebay/watchs";
        if (!Directory.Exists(Server.MapPath(path)))
            Directory.CreateDirectory(Server.MapPath(path));
        DirectoryInfo dir = new DirectoryInfo(Server.MapPath(path));
        FileInfo[] fis = dir.GetFiles();
        for (int i = 0; i < fis.Length; i++)
        {
            if (fis[i].Extension.ToLower().IndexOf("pdf") > -1)
                File.Delete(fis[i].FullName);
        }

        //StreamWriter sw = new StreamWriter(Server.MapPath(path + "/ebayOrder.txt"),false);
        //sw.Write(ReqSKU);
        //sw.Close();
        //sw.Dispose();
        if (ReqSKU.Trim().ToLower() == "all")
        {
            Config.ExecuteNonQuery("delete from tb_timer where Cmd = '1' ");
            Config.ExecuteNonQuery("Insert into tb_timer (CmdContent, Cmd,regdate,status ) values ('" + ReqSKU + "','1',now(),1)");
        }
        else
        {
            Config.ExecuteNonQuery("delete from tb_timer where Cmd = '2' ");
            Config.ExecuteNonQuery("Insert into tb_timer (CmdContent, Cmd,regdate,status ) values ('" + ReqSKU + "','2',now(),1)");
        }
        Response.Write("<script> alert('服务器3秒后执行更新.稍后请刷新界面'); window.close();</script>");
    }

    string ReqSKU
    {
        get { return Util.GetStringSafeFromQueryString(Page, "sku"); }
    }
}