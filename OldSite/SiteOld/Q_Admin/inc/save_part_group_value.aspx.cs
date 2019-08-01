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

public partial class Q_Admin_inc_save_part_group_value : PageBase
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
        SaveGroupValue(SKU, PartGroupID);
    }

    public void SaveGroupValue(int sku, int group_id)
    {
        PartGroupDetailModel pgdm = new PartGroupDetailModel();
        pgdm.nominate = 0;
        pgdm.part_group_id = group_id;
        pgdm.product_serial_no = sku;
        pgdm.showit = true;
        pgdm.Create();

        Response.Write("SKU: "+ sku.ToString() + " is ok.");
        Response.End();
    }
    #region  properties

    public int SKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", -1); }
    }

    public int PartGroupID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "part_group_id", -1); }
    }

    #endregion
}