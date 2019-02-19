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

public partial class Q_Admin_sale_promotion_settings : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Response.Write("此界面功能在<a href='/q_admin/product_edit_frame.aspx?menu_id=6' >product->part(</a>操作,<a href='/q_admin/product_edit_frame.aspx?menu_id=6' >点击此处进入</a>");
            Response.End();
            this.ValidateLoginRule(Role.sales_promotion_rebate);
            InitialDatabase();
        }
    }     

    #region Methods

    private void FindAllPdfFilename()
    {
        FileHelper fh = new FileHelper();

        this.rpt_all_pdf_filename.DataSource = fh.FindAllFilenameByPath(Server.MapPath(Config.StoreProductRebatePdfPath));
        this.rpt_all_pdf_filename.DataBind();
    }

    private bool IsExistSubCategory(int id)
    {
        ProductCategoryModel model = ProductCategoryModel.GetProductCategoryModel(id);
        if (model.menu_is_exist_sub == 1)
        {
            return true;// BindChildCategory(model.menu_child_serial_no);
        }
        else
        {
            return false;// AnthemHelper.Alert("没有子类");
        }
    }

    private void BindChildCategory(int parentID)
    {
        if (!IsExistSubCategory(parentID))
        {
            return;
        }
        ProductCategoryModel[] models = ProductCategoryModel.ProductCategoryModelsByMenuPreSerialNo(parentID);
        this.ddlChildCategory.DataSource = models;
        this.ddlChildCategory.DataTextField = "menu_child_name";
        this.ddlChildCategory.DataValueField = "menu_child_serial_no";
        this.ddlChildCategory.DataBind();
        this.ddlChildCategory.AutoUpdateAfterCallBack = true;
        this.ddlChildSubCategory.Items.Clear();
        this.ddlChildSubCategory.AutoUpdateAfterCallBack = true;

        // 绑定子类
        int id = int.Parse(this.ddlChildCategory.SelectedValue);
        if (IsExistSubCategory(id))
        {
            BindChildSubCategory(id);
        }
        else
        {
            this.ddlChildSubCategory.Items.Clear();
            this.ddlChildCategory.AutoUpdateAfterCallBack = true;
            //AnthemHelper.Alert("没有子类");
        }
    }    /// <summary>
    /// 绑定第三级类别
    /// </summary>
    /// <param name="parentID"></param>
    private void BindChildSubCategory(int parentID)
    {

        this.ddlChildSubCategory.DataSource = ProductCategoryModel.ProductCategoryModelsByMenuPreSerialNo(parentID, true);
        this.ddlChildSubCategory.DataTextField = "menu_child_name";
        this.ddlChildSubCategory.DataValueField = "menu_child_serial_no";
        this.ddlChildSubCategory.DataBind();
        this.ddlChildSubCategory.AutoUpdateAfterCallBack = true;


    }
    private void ClearControls()
    {
        this.ddlChildCategory.Items.Clear();
        this.ddlChildCategory.AutoUpdateAfterCallBack = true;
        this.ddlChildSubCategory.Items.Clear();
        this.ddlChildSubCategory.AutoUpdateAfterCallBack = true;

    }
    private int GetParentCategory(string parent_category_name)
    {
        for (int i = 0; i < ParentProductCategory.Length; i++)
        {
            if (ParentProductCategory[i].menu_child_name == parent_category_name)
                return ParentProductCategory[i].menu_child_serial_no;
        }
        return 0;
    }
    public override void InitialDatabase()
    {
        base.InitialDatabase();
        ParentProductCategory = ProductCategoryModel.ProductCategoryModelsByParts(true);
        BindRptParentCategory(false);

        FindAllPdfFilename();
    }

    private void BindRptParentCategory(bool autoUpdate)
    {
        this.rptParentCategory.DataSource = ParentProductCategory;
        this.rptParentCategory.DataBind();
        this.rptParentCategory.AutoUpdateAfterCallBack = autoUpdate;
    }
    private void SetParentCategoryStyle()
    {
        Anthem.Repeater rpt = (Anthem.Repeater)this.rptParentCategory;
        for (int i = 0; i < rpt.Items.Count; i++)
        {
            Anthem.LinkButton lb = (Anthem.LinkButton)rpt.Items[i].FindControl("_lbTitle");
            lb.CssClass = "Title1";
        }

    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    private int GetCurrentCategory()
    {
        if (this.ddlChildSubCategory.Items.Count == 0)
        {
            if (this.ddlChildCategory.Items.Count == 0)
            {
                return ParentCategory;
            }
            else
                return int.Parse(this.ddlChildCategory.SelectedValue);
        }
        else
        {
            return int.Parse(this.ddlChildSubCategory.SelectedValue);
        }
    }

    private void BindProductSaveDG(int category)
    {
        this.dg_product_save.DataSource = ProductModel.GetModelsByTagAndLine(CurrentCategory);
        this.dg_product_save.DataBind();
        this.dg_product_save.UpdateAfterCallBack = true;
    }
    #endregion


    #region Events
    protected void rptParentCategory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        // AnthemHelper.Alert(this.rptParentCategory.Items.Count.ToString());
        ClearControls();
        SetParentCategoryStyle();
        Anthem.LinkButton lb = (Anthem.LinkButton)e.Item.FindControl("_lbTitle");
        lb.CssClass = "Title2";

        int parentID = GetParentCategory(lb.Text);
        ParentCategory = parentID;
        BindChildCategory(parentID);
    }
    protected void ddlChildCategory_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = int.Parse(this.ddlChildCategory.SelectedValue);
        if (IsExistSubCategory(id))
        {
            BindChildSubCategory(id);
        }
        else
        {
            this.ddlChildSubCategory.Items.Clear();
            this.ddlChildSubCategory.AutoUpdateAfterCallBack = true;
            // AnthemHelper.Alert("没有子类");
        }
    }
    protected void ddlChildSubCategory_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void lb_go_Click(object sender, EventArgs e)
    {
        try
        {
            show_it = this.cb_all.Checked == true ? Showit.all : Showit.show_true;
            //AnthemHelper.Alert("当前产品类别已变化");
            CurrentCategory = GetCurrentCategory();
            BindProductSaveDG(CurrentCategory);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
    protected void dg_product_save_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            int product_id = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
            DataTable sale = SalePromotionModel.GetOne(product_id, PromotionOrRebate);
            if (sale.Rows.Count == 1)
            {
                DataRow dr = sale.Rows[0];
                DateTime _begin_date = DateTime.Parse(dr["begin_datetime"].ToString());
                DateTime _end_date = DateTime.Parse(dr["end_datetime"].ToString());
                Anthem.TextBox _begin = (Anthem.TextBox)e.Item.Cells[5].FindControl("_txt_begin_datetime");
                _begin.Text = _begin_date.ToString("yyyy-MM-dd");

                Anthem.TextBox _end = (Anthem.TextBox)e.Item.Cells[6].FindControl("_txt_end_datetime");
                _end.Text = _end_date.ToString("yyyy-MM-dd");

                Anthem.TextBox _save_cost = (Anthem.TextBox)e.Item.Cells[7].FindControl("_txt_save_cost");
                _save_cost.Text = dr["sale_price"].ToString();

                e.Item.Cells[8].Text = dr["save_cost"].ToString();

                // AnthemHelper.SetAnthemDataGridCellCheckBoxChecked(e.Item, 9, "_cb_checkbox", dr["show_it"].ToString() == "1" ? true : false);
                Anthem.TextBox _pdf_filename = (Anthem.TextBox)e.Item.Cells[10].FindControl("_txt_pdf_filename");
                _pdf_filename.Text = dr["pdf_filename"].ToString();

                if (DateTime.Now > _begin_date && DateTime.Now < _end_date)
                {
                    _begin.ForeColor = System.Drawing.Color.Green;
                    _end.ForeColor = System.Drawing.Color.Green;
                }
                if (DateTime.Now < _begin_date)
                {
                    _begin.ForeColor = System.Drawing.Color.GreenYellow;
                    _end.ForeColor = System.Drawing.Color.GreenYellow;
                }
                if (DateTime.Now > _end_date)
                {
                    _begin.ForeColor = System.Drawing.Color.Red;
                    _end.ForeColor = System.Drawing.Color.Red;
                }

                Anthem.TextBox _comment = (Anthem.TextBox)e.Item.Cells[9].FindControl("_txt_comment");
                _comment.Text = dr["comment"].ToString();
            }
        }
    }
    protected void lb_save_Click(object sender, EventArgs e)
    {
        try
        {
            Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_product_save;
            for (int i = 0; i < dg.Items.Count; i++)
            {
                DataGridItem item = dg.Items[i];
                int product_id = AnthemHelper.GetAnthemDataGridCellText(item, 0);
                string begin_date = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 5, "_txt_begin_datetime");
                string end_date = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 6, "_txt_end_datetime");
                string sale_price = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 7, "_txt_save_cost");
            
                string _comment = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 9, "_txt_comment");
                string _pdf_filename = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 10, "_txt_pdf_filename");

                if (begin_date != "" && end_date != "" && sale_price != "")
                {
                    int count = SalePromotionModel.GetOneMore(product_id, DateTime.Parse(begin_date), DateTime.Parse(end_date), decimal.Parse(sale_price), true, PromotionOrRebate, _comment, _pdf_filename).Rows.Count;
                    if ((count < 1))
                    {
                        decimal save_cost = 0M;
                        if (this.RadioButtonList1.SelectedValue.ToString() == "1")
                            save_cost = AnthemHelper.GetAnthemDataGridCellTextDecimal(item, 4) - decimal.Parse(sale_price);
                        else
                            save_cost = decimal.Parse(sale_price);
                        SalePromotionModel model = new SalePromotionModel();
                        ProductModel product = ProductModel.GetProductModel(product_id);
                        model.begin_datetime = DateTime.Parse(begin_date);
                        model.end_datetime = DateTime.Parse(end_date);
                        model.product_serial_no = product_id;
                        model.save_cost = save_cost;
                        model.create_datetime = DateTime.Now;
                        model.price = product.product_current_price;
                        model.cost = product.product_current_cost;
                        model.sale_price = decimal.Parse(sale_price);
                        model.promotion_or_rebate = this.PromotionOrRebate;
                        model.comment = _comment;
                        model.pdf_filename = _pdf_filename;
                        model.show_it = true;
                        model.Create();
                        if (this.PromotionOrRebate == 1)
                            TrackModel.InsertInfo("Create Sale Promotion (" + product_id.ToString() + ")", LoginUser.LoginIDInt);
                        else
                            TrackModel.InsertInfo("Create Rebate (" + product_id.ToString() + ")", LoginUser.LoginIDInt);
                    }
                    else
                    {

                    }
                    // AnthemHelper.Alert(count.ToString());
                }
            }
            BindProductSaveDG(CurrentCategory);
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }

    protected void dg_product_save_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "ViewHistory":
                    AnthemHelper.OpenWin("sale_promotion_History.aspx?product_id=" + AnthemHelper.GetAnthemDataGridCellText(e.Item, 0), 800, 500, 30, 100);
                    break;
            }
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
    #endregion

    #region porperties
    public int ParentCategory
    {
        get
        {
            object o = ViewState["ParentCategory"];
            if (o != null)
            {
                return int.Parse(o.ToString());
            }
            return 0;
        }
        set { ViewState["ParentCategory"] = value; }
    }
    public ProductCategoryModel[] ParentProductCategory
    {
        get
        {
            object o = ViewState["ParentProductCategory"];
            if (o != null)
            {
                return (ProductCategoryModel[])o;
            }
            return ProductCategoryModel.ProductCategoryModelsParent();
        }
        set { ViewState["ParentProductCategory"] = value; }
    }
    public int PromotionOrRebate
    {
        get { return int.Parse(this.RadioButtonList1.SelectedValue.ToString()); }
        set
        {
            this.RadioButtonList1.SelectedValue = value.ToString();
            this.RadioButtonList1.UpdateAfterCallBack = true;
        }
    }
    public int CurrentCategory
    {
        get
        {
            object o = ViewState["CurrentCategory"];
            if (o != null)
                return int.Parse(o.ToString());
            else
                return -1;
        }
        set { ViewState["CurrentCategory"] = value; }
    }

    public Showit show_it
    {
        get
        {
            object o = ViewState["show_it"];
            if (o != null)
                return (Showit)Enum.Parse(typeof(Showit), o.ToString());
            else
                return Showit.show_true;
        }
        set { ViewState["show_it"] = value; }
    }
    #endregion
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (this.RadioButtonList1.SelectedValue.ToString() == "1")
        {
            this.dg_product_save.Columns[7].HeaderText = "$Selling Price";
        }
        if (this.RadioButtonList1.SelectedValue.ToString() == "2")
        {
            this.dg_product_save.Columns[7].HeaderText = "$rebate";
        }
        this.dg_product_save.UpdateAfterCallBack = true;
    }
}
