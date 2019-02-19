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

public partial class Q_Admin_product_helper_shipping_settings : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.configure_helper);
            InitialDatabase();
        }
    }
    #region Methods

    public override void InitialDatabase()
    {
        BindCountryRadio();
        base.InitialDatabase();
        BindShippingCompanyDG(false);
        BindProductSizeDG(false);
        BindAccountDG(false);
        BindStateShippingDG(false, Config.SystemCategory);
    }

    private void BindShippingCompanyDG(bool autoUpdate)
    {
        ShippingCompanys =  ShippingCompanyModel.FindAll();
        this.dg_shipping_company.DataSource = ShippingCompanys;
        this.dg_shipping_company.DataBind();
        this.dg_shipping_company.UpdateAfterCallBack = autoUpdate;
    }

    private void BindProductSizeDG(bool autoUpdate)
    {
        ProductSizes = ProductSizeModel.FindAll();
        this.dg_product_size.DataSource = ProductSizes;
        this.dg_product_size.DataBind();
        this.dg_product_size.UpdateAfterCallBack = autoUpdate;
    }

    private void BindAccountDG(bool autoUpdate)
    {
        this.dg_account.DataSource = AccountModel.FindAll();
        this.dg_account.DataBind();
        this.dg_account.UpdateAfterCallBack = autoUpdate;
    }

    private void BindStateShippingDG(bool autoUpdate,int country)
    {
        this.dg_state_shipping.DataSource = StateShippingModel.GetModelsBySystemCategory(country);
        this.dg_state_shipping.DataBind();
        this.dg_state_shipping.UpdateAfterCallBack = autoUpdate;
    }

    private void BindCountryRadio()
    {
        this.radioCountry.SelectedValue = Config.SystemCategory.ToString();
    }

    private int GetAccountShippingCompany(int id)
    {
        for (int i = 0; i < Accounts.Length; i++)
        {
            if (Accounts[i].account_id == id)
                return Accounts[i].shipping_company_id;

        }
        return 1;
    }

    private int GetAccountProductSize(int id)
    {
        for (int i = 0; i < Accounts.Length; i++)
        {
            if (Accounts[i].account_id == id)
                return Accounts[i].product_size_id;

        }
        return 1;
    }

    private int GetAccountProductCategory(int id)
    {
        for (int i = 0; i < Accounts.Length; i++)
        {
            if (Accounts[i].account_id == id)
                return Accounts[i].product_category;

        }
        return 1;
    }

    #endregion

    #region Events
    protected void lb_New_shipping_company_Click(object sender, EventArgs e)
    {
        ShippingCompanyModel model = new ShippingCompanyModel();
        model.Create();

        BindShippingCompanyDG(true);
    }  
    
    protected void lb_Save_shipping_company_Click(object sender, EventArgs e)
    {
        Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_shipping_company;
        for (int i = 0; i < dg.Items.Count; i++)
        {
            DataGridItem item = dg.Items[i];
            int id = AnthemHelper.GetAnthemDataGridCellText(item, 0);

            ShippingCompanyModel model = ShippingCompanyModel.GetShippingCompanyModel(id);
            model.qty = AnthemHelper.GetAnthemDataGridCellTextBoxTextInt(item, 2, "_txt_qty");
            model.shipping_company_name = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 1, "_txt_shipping_company_name");
            model.Update();

           
        }
        AnthemHelper.Alert(KeyFields.SaveIsOK);
    }
    protected void lb_new_product_size_Click(object sender, EventArgs e)
    {
        ProductSizeModel model = new ProductSizeModel();
        model.Create();

        BindProductSizeDG(true);
    }
    protected void lb_save_product_Click(object sender, EventArgs e)
    {
        try
        {
            Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_product_size;
            for (int i = 0; i < dg.Items.Count; i++)
            {
                DataGridItem item = dg.Items[i];
                int id = AnthemHelper.GetAnthemDataGridCellText(item, 0);

                ProductSizeModel model = ProductSizeModel.GetProductSizeModel(id);
                model.product_size_name = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 1, "_txt_product_size_name");
                model.begin_price = AnthemHelper.GetAnthemDataGridCellTextBoxTextDecimal(item, 2, "_txt_begin_price");
                model.end_price = AnthemHelper.GetAnthemDataGridCellTextBoxTextDecimal(item, 3, "_txt_end_price");
                model.Update();
            }
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
    protected void lb_new_state_shipping_Click(object sender, EventArgs e)
    {
        StateShippingModel model = new StateShippingModel();
        int country = int.Parse(this.radioCountry.SelectedValue.ToString());
        model.system_category_serial_no = country;
        model.Create();

        BindStateShippingDG(true, country);
    }
    protected void lb_save_state_shipping_Click(object sender, EventArgs e)
    {
        Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_state_shipping;

        for (int i = 0; i < dg.Items.Count; i++)
        {
            DataGridItem item = dg.Items[i];
            int id = AnthemHelper.GetAnthemDataGridCellText(item, 0);

            StateShippingModel model = StateShippingModel.GetStateShippingModel(id);
            model.state_name = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 1, "_txt_state_name");
            model.state_short_name = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 2, "_txt_short_name");
            model.gst = byte.Parse(AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 3, "_txt_gst"));
            model.pst = byte.Parse(AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 4, "_txt_pst"));
            model.state_shipping = AnthemHelper.GetAnthemDataGridCellTextBoxTextDouble(item, 5, "_txt_state_shipping");
            model.Update();

           
        }
        AnthemHelper.Alert(KeyFields.SaveIsOK);
    }
    protected void lb_new_account_Click(object sender, EventArgs e)
    {
        AccountModel model = new AccountModel();
        model.Create();

        BindAccountDG(true);
    }

    protected void lb_save_account_Click(object sender, EventArgs e)
    {
        Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_account;

        for (int i = 0; i < dg.Items.Count; i++)
        {
            DataGridItem item = dg.Items[i];
            int id = AnthemHelper.GetAnthemDataGridCellText(item, 0);

            AccountModel model = AccountModel.GetAccountModel(id);
            model.charge = AnthemHelper.GetAnthemDataGridCellTextBoxTextDecimal(item, 1, "_txt_charge");
            model.shipping_company_id = AnthemHelper.GetAnthemDataGridCellDropDownList(item, 2, "_ddl_shipping_company");
            model.product_size_id = AnthemHelper.GetAnthemDataGridCellDropDownList(item, 3, "ddl_product_size");
            model.product_category = AnthemHelper.GetAnthemDataGridCellDropDownList(item, 4, "_ddl_product_category");
            model.qty = AnthemHelper.GetAnthemDataGridCellTextBoxTextInt(item, 5, "_txt_account_qty");
            model.Update();
        }
        AnthemHelper.Alert(KeyFields.SaveIsOK);
    }
    #endregion

    protected void dg_account_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            int id = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);

            Anthem.DropDownList shippingDDL = (Anthem.DropDownList)e.Item.Cells[2].FindControl("_ddl_shipping_company");
            shippingDDL.DataSource = ShippingCompanys;
            shippingDDL.DataTextField = "shipping_company_name";
            shippingDDL.DataValueField = "shipping_company_id";
            shippingDDL.DataBind();
            shippingDDL.SelectedValue = GetAccountShippingCompany(id).ToString();

            Anthem.DropDownList sizeDDL = (Anthem.DropDownList)e.Item.Cells[2].FindControl("ddl_product_size");
            sizeDDL.DataSource = ProductSizes;
            sizeDDL.DataTextField = "product_size_name";
            sizeDDL.DataValueField = "product_size_id";
            sizeDDL.DataBind();
            sizeDDL.SelectedValue = GetAccountProductSize(id).ToString();

            Anthem.DropDownList cateDDL = (Anthem.DropDownList)e.Item.Cells[2].FindControl("_ddl_product_category");
            cateDDL.DataSource = Product_category_helper.product_category_ToDataTable();
            cateDDL.DataTextField = KeyText.description;
            cateDDL.DataValueField = KeyText.value;
            cateDDL.DataBind();
            cateDDL.SelectedValue = GetAccountProductCategory(id).ToString();
        }
    }




    #region Fields
    public ShippingCompanyModel[] ShippingCompanys
    {
        get
        {
            object o = ViewState["ShippingCompanys"];
            if (o != null)
            {
                return (ShippingCompanyModel[])o;
            }
            return ShippingCompanyModel.FindAll();
        }
        set { ViewState["ShippingCompanys"] = value; }
    }
    public ProductSizeModel[] ProductSizes
    {
        get
        {
            object o = ViewState["ProductSizes"];
            if (o != null)
            {
                return (ProductSizeModel[])o;
            }
            return ProductSizeModel.FindAll();
        }
        set { ViewState["ProductSizes"] = value; }
    }

    public AccountModel[] Accounts
    {
        get
        {
            object o = ViewState["Accounts"];
            if (o != null)
            {
                return (AccountModel[])o;
            }
            return AccountModel.FindAll();
        }
        set { ViewState["Accounts"] = value; }
    }
    #endregion
    protected void dg_account_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
    {
        this.dg_account.CurrentPageIndex = e.NewPageIndex;
        this.BindAccountDG(true);
    }
    protected void radioCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindStateShippingDG(true, int.Parse(this.radioCountry.SelectedValue.ToString()));
    }
}
