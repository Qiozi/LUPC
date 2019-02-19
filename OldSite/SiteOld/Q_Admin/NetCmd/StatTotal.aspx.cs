using System;

public partial class Q_Admin_NetCmd_StatTotal : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if(ReqCmd == "Qiozi@msn.com")
        {
            //
            // order stat 30 day ago.
            //
            Stat s = new Stat();
            s.GenerateOrderStat30DayAgo();
            s.GenerateOrderStatByMonthAgo();
            s.DeleteInvalidData();

            DeleteOrderPdf();
            
            Response.Clear();


            Response.Write("Hello world. "+ DateTime.Now.ToString());
            Response.End();
        }
    }

    void DeleteOrderPdf()
    {
        System.IO.DirectoryInfo dir = new System.IO.DirectoryInfo(Server.MapPath("/order_pdf"));
        System.IO.FileInfo[] fi = dir.GetFiles();

        for (int i = 0; i < fi.Length; i++)
        {
            if (fi[i].Extension.IndexOf("pdf") > -1)
                System.IO.File.Delete(fi[i].FullName);
        }
    }

    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }
}
