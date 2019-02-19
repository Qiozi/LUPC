using LU.Data;
using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_ebayMaster_ebay_part_temp_page_view_noShipping : PageBaseNoInit
{ 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.Title = "part sku:" + ReqSku.ToString();
            try
            {
                if (ReqCmd == "Qiozi@msn.com.xml")
                {
                    Response.Clear();
                    Response.ClearContent();

                    try
                    {
                        Response.Write(new eBayPageHelper(DBContext, this).GetPageString(ReqSku));
                    }
                    catch (Exception ex)
                    {
                        Response.Write(ex.Message);
                    }
                    Response.End();
                }
                else
                {
                    InitialDatabase();// valid login 
                    this.literal_page.Text = new eBayPageHelper(DBContext, this).GetPageString(ReqSku);
                }
            }
            catch (Exception ex)
            {
                Response.Write("<br><br><br><br><br><br><br><br><br>" + ex.Message);
            }
        }
    }   
    

    #region Properties
    public int ReqSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "SKU", -1); }
    }

    public string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }
    #endregion

    protected void btn_download_Click(object sender, EventArgs e)
    {
        Response.ClearHeaders();
        Response.Clear();
        Response.Expires=0;
        Response.Buffer = true;
        Response.AddHeader ("Accept-Language", "utf-8");
        Response.AddHeader("content-disposition", "attachment;filename=part_"+ ReqSku.ToString()+".html");
        Response.ContentType = "application/octet-stream";
        Response.Write(this.literal_page.Text);
       
        Response.End();    
        
    }
}
