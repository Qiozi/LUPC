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

public partial class Q_Admin_UC_Top : CtrlBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lbl_username.Text = Page.LoginUser.RealName;
            if (System.IO.File.Exists(Server.MapPath("~/soft_img/match_ebay_price.txt")))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(Server.MapPath("~/soft_img/match_ebay_price.txt"));
                string content = sr.ReadToEnd();
                sr.Close();
                Literal1.Text = "&nbsp;&nbsp;&nbsp;&nbsp; Last validation eBay price time:&nbsp;&nbsp;" + content.ToString();
            }
            
        }
    }
    protected void Timer1_Tick(object sender, EventArgs e)
    {
       
         //if (Page.LoginUser.IsShowMessageWin)
         //   {
         //       AnthemHelper.OpenWin("system_alert.aspx", 400, 300, 300, 200);
         //       Page.LoginUser.IsShowMessageWin = false;
         //   }
         //this.Timer1.Enabled = false;
    }


}
