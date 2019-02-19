using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UC_bottom : System.Web.UI.UserControl
{
    public string GodaddySSL = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Url.ToString().IndexOf(LU.BLL.Config.IsLocalHost) == -1)
        {
             GodaddySSL = @"<center><script type=""text/javascript"" src=""https://seal.godaddy.com/getSeal?sealID=m1rXgddaupGKJemjFI0QslvJUzAlm8xCPKQIS8YF5EQySbD7W50Rjths2lK""></script></center>";
        }
    }
}