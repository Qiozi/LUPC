﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

public partial class Q_Admin_order_set_invoice : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            OrderHelperModel ohm = new OrderHelperModel();
            string new_invoice = ohm.SetInvoiceToOrder(OrderCode);
            
        }
    }

    public int OrderCode
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "order_code", -1); }
    }
}