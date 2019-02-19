﻿using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Q_Admin_Logout : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        TrackModel.InsertInfo("Logout", int.Parse(LoginUser.LoginID));
        LoginUser.LoginID = "";
        LoginUser.RealName = "";
        Response.Redirect("../default.asp");

    }
}
