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

public partial class Q_Admin_product_helper_factory : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.product_manage);
            this.InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindFactory(false);
    }

    private void BindFactory(bool autoUpdate)
    {
        
        this.dgFactory.DataSource = ProducterModel.GetProducterModelByAll(false);
        this.dgFactory.DataBind();
        this.dgFactory.AutoUpdateAfterCallBack = autoUpdate;
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        Anthem.DataGrid dg = (Anthem.DataGrid)this.dgFactory;
        for (int i = 0; i < dg.Items.Count; i++)
        {
            DataGridItem item = dg.Items[i];
            int factory_id = int.Parse(item.Cells[0].Text);
            ProducterModel model = ProducterModel.GetProducterModel(factory_id);
            model.producter_name = AnthemHelper.GetAnthemTextBox((Anthem.TextBox)item.Cells[1].FindControl("_txt_factory_name"));
            model.producter_web_address = AnthemHelper.GetAnthemTextBox((Anthem.TextBox)item.Cells[2].FindControl("_txt_factory_web"));
            model.Update();
        }
        AnthemHelper.Alert("Save is OK");
        this.BindFactory(true);
    }
    protected void btnCreate_Click(object sender, EventArgs e)
    {
        ProducterModel model = new ProducterModel();
        model.producter_name = "new....";
        model.producter_web_address = "http://";
        model.Create();

        this.BindFactory(true);
    }
}