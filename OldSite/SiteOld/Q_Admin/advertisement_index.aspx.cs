﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_advertisement_index : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

      

    }  
}