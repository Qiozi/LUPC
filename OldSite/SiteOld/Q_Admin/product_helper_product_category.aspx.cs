using System;
using System.Web.UI.WebControls;
using System.IO;

public partial class Q_Admin_product_helper_product_category : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.product_category_settings);
            InitialDatabase();
        }

    }

    #region Methods
    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindDG1(false);
    }

    private void BindDG1(bool autoUpdate)
    {
        this.dg1.DataSource = ProductCategoryModel.ProductCategoryModelsParentAll();
        this.dg1.DataBind();
    }


    private void BindDG2(int id, bool autoUpdate)
    {
        this.dg2.DataSource = ProductCategoryModel.ProductCategoryModelsByMenuPreSerialNo(id);
        this.dg2.DataBind();
    }

    private void BindDG3(int id, bool autoUpdate)
    {
        this.dg3.DataSource = ProductCategoryModel.ProductCategoryModelsByMenuPreSerialNo(id);
        this.dg3.DataBind();
    }
    #endregion

    #region Evert
    protected void dg1_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                int id = int.Parse(e.Item.Cells[0].Text);
                BindDG2(id, true);
                ParentID = id;
                TextBox tb = (TextBox) e.Item.Cells[1].FindControl("_txtName");
                CH.SetLabel(this.lblTitle2, tb.Text);

                break;
        }
    }

    

    protected void dg2_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Create":
                int id = int.Parse(e.Item.Cells[0].Text);
                BindDG3(id, true);
                ParentID2 = id;
                TextBox tb = (TextBox)e.Item.Cells[1].FindControl("_txtName2");
                CH.SetLabel(this.lblTitle3, tb.Text);

                break;
        }
    }

    protected void dg1_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            string is_exist_sub = e.Item.Cells[2].Text;
            e.Item.Cells[2].Text = is_exist_sub == "1" ? "有" : "无";
        }

    }
    protected void btnSave1_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.dg1.Items.Count; i++)
        {
            int serial_no = int.Parse(this.dg1.Items[i].Cells[0].Text);
            ProductCategoryModel model = ProductCategoryModel.GetProductCategoryModel(serial_no);
            model.menu_child_name = ((TextBox)this.dg1.Items[i].Cells[1].FindControl("_txtName")).Text;
            model.page_category = ((CheckBox)this.dg1.Items[i].Cells[3].FindControl("_cbPageCategory")).Checked == true ? 1 : 0;
            model.tag = byte.Parse(((CheckBox)this.dg1.Items[i].Cells[4].FindControl("_cbShowit")).Checked == true ? "1" : "0");
            model.menu_child_order = int.Parse(((TextBox)this.dg1.Items[i].Cells[5].FindControl("_txtOrder")).Text.Trim());
            model.is_noebook = byte.Parse(((CheckBox)this.dg1.Items[i].Cells[6].FindControl("_cb_noebook")).Checked == true ? "1" : "0");
            model.is_view_menu = model.tag==1;
            model.Update();
        }
        CH.Alert(KeyFields.SaveIsOK, this.Literal1);
    }
    protected void btnSave2_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < this.dg2.Items.Count; i++)
            {
                int serial_no = int.Parse(this.dg2.Items[i].Cells[0].Text);
                ProductCategoryModel model = ProductCategoryModel.GetProductCategoryModel(serial_no);
                model.menu_child_name = ((TextBox)this.dg2.Items[i].Cells[1].FindControl("_txtName2")).Text;
                model.page_category = ((CheckBox)this.dg2.Items[i].Cells[3].FindControl("_cbPageCategory2")).Checked == true ? 1 : 0;
                model.tag = byte.Parse(((CheckBox)this.dg2.Items[i].Cells[4].FindControl("_cbShowit2")).Checked == true ? "1" : "0");
                model.menu_child_order = int.Parse(((TextBox)this.dg2.Items[i].Cells[5].FindControl("_txtOrder2")).Text.Trim());
                model.is_noebook = byte.Parse(((CheckBox)this.dg2.Items[i].Cells[6].FindControl("_cb_noebook_child")).Checked == true ? "1" : "0");
                model.is_virtual = ((CheckBox)this.dg2.Items[i].Cells[7].FindControl("_cb_is_not_real")).Checked;
                model.is_view_menu = model.tag == 1; 
                model.Update();
            }
            CH.Alert(KeyFields.SaveIsOK, this.Literal1);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.Literal1);
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        for (int i = 0; i < this.dg3.Items.Count; i++)
        {
            int serial_no = int.Parse(this.dg3.Items[i].Cells[0].Text);
            ProductCategoryModel model = ProductCategoryModel.GetProductCategoryModel(serial_no);
            model.menu_child_name = ((TextBox)this.dg3.Items[i].Cells[1].FindControl("_txtName3")).Text;
            model.page_category = ((CheckBox)this.dg3.Items[i].Cells[2].FindControl("_cbPageCategory3")).Checked == true ? 1 : 0;
            model.tag = byte.Parse(((CheckBox)this.dg3.Items[i].Cells[3].FindControl("_cbShowit3")).Checked == true ? "1" : "0");
            model.menu_child_order = int.Parse(((TextBox)this.dg3.Items[i].Cells[4].FindControl("_txtOrder3")).Text.Trim());
            model.is_noebook = byte.Parse(((CheckBox)this.dg3.Items[i].Cells[5].FindControl("_cb_noebook_sub")).Checked == true ? "1" : "0");
            model.is_virtual = ((CheckBox)this.dg3.Items[i].Cells[6].FindControl("_cb_is_not_real")).Checked;
            model.is_view_menu = model.tag == 1;
            model.Update();
        }
        CH.Alert(KeyFields.SaveIsOK, this.Literal1);
    }

    protected void btnCreate1_Click(object sender, EventArgs e)
    {
        CreateNewRecord(1);
        this.BindDG1(true);
    }
    protected void btnCreate2_Click(object sender, EventArgs e)
    {
        CreateNewRecord(ParentID);
        this.BindDG1(true);
        this.BindDG2(ParentID, true);
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        CreateNewRecord(ParentID2);
        // this.BindDG2(ParentID, true);
        this.BindDG3(ParentID2, true);
    }
    protected void btn_create_left_menu_Click(object sender, EventArgs e)
    {
        try
        {
            new GetAllValidCategory().GenerateAllValidCategory();

            CH.Alert("it is OK.", this.Literal1);

        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.Literal1);
        }
    }

    #endregion

    private void CreateNewRecord(int parentID)
    {
        ProductCategoryModel model = new ProductCategoryModel();
        model.menu_parent_serial_no = 1;
        model.menu_pre_serial_no =  parentID;
        model.menu_child_name = "new....";
        model.tag = 0;
        model.page_category = 1;
        model.menu_is_exist_sub = 0;// byte.Parse((parentID == 1 ? 0 : 1).ToString());
        
        model.Create();

        if (parentID != 1)
        {
            model = ProductCategoryModel.GetProductCategoryModel(parentID);
            model.menu_is_exist_sub = 1;
            model.Update();
        }
    }

    public int ParentID2
    {
        get
        {
            object o = ViewState["ParentID"];
            if (o != null)
                return int.Parse(o.ToString());
            return 1;
        }
        set { ViewState["ParentID"] = value; }
    }

    public int ParentID
    {
        get
        {
            object o = ViewState["ParentID"];
            if (o != null)
                return int.Parse(o.ToString());
            return 1;
        }
        set { ViewState["ParentID"] = value; }
    }

}
