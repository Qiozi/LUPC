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

public partial class part_showit_manage : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
            CH.CloseParentWatting(this.btn_change);
        }
    }

    #region Methods
    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindCategoryTitle();
        this.BindProductDG(false,this.MenuChildSerialNO, Showit.show_true);


    }

    /// <summary>
    /// Bind product database to DataGrid control
    /// </summary>
    /// <param name="autoUpdate"></param>
    /// <param name="menu_child_serial_no"></param>
    private void BindProductDG(bool autoUpdate, int menu_child_serial_no, Showit showit)
    {
        if (menu_child_serial_no > 0)
        {
            this.dg_part.DataSource = ProductModel.GetProductmodelByMenuChildAndShowit(menu_child_serial_no, showit, true);
            this.dg_part.DataBind();

            this.dg_part.UpdateAfterCallBack = autoUpdate;
        }
    }

    private void BindCategoryTitle()
    {
        ProductCategoryModel pcm = ProductCategoryModel.GetProductCategoryModel(this.MenuChildSerialNO);
        this.lbl_menu_child_name.Text = pcm.menu_child_name;

        // bind checked 
        this.CheckBox_display_stock_status.Checked = pcm.is_display_stock;
    }
    #endregion

    #region Properties
    public int MenuChildSerialNO
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "categoryID", 0); }
    }
    #endregion

    #region Events
    protected void btn_change_Click(object sender, EventArgs e)
    {
        bool b = this.cb_showit_all.Checked;
        BindProductDG(true, MenuChildSerialNO, b == true ? Showit.all : Showit.show_true);

       
    }
    /// <summary>
    /// Save 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < this.dg_part.Items.Count; i++)
            {
                DataGridItem item = this.dg_part.Items[i];
                int product_id = AnthemHelper.GetAnthemDataGridCellText(item, 0);
                bool showit = AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(item, 1, "_cb_showit");
                bool newproduct = AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(item, 2, "_cb_new");
                bool export = AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(item, 3, "_cb_export");
                bool split_line = AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(item, 4, "_cb_split_line");
                bool hot = AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(item, 4, "_cb_hot");
                decimal price = AnthemHelper.GetAnthemDataGridCellTextBoxTextDecimal(item, 7, "_txt_price");
                string manufacturer_part_number = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 9, "_txt_manufacturer_part_number");
                string supplier_sku = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 10, "_txt_supplier_sku");
              
                string short_name = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 11, "_txt_short_name");
                string middle_name = AnthemHelper.GetAnthemDataGridCellTextBoxText(item, 12, "_txt_middle_name");
                int priority = AnthemHelper.GetAnthemDataGridCellTextBoxTextInt(item, 13, "_txt_priority");
                
                ProductModel pm = ProductModel.GetProductModel(product_id);
                pm.tag = byte.Parse(showit == true ? "1" : "0");
                pm.product_current_price = price;
                pm.product_order = priority;
                pm.product_short_name = short_name;
                pm.product_name = middle_name;
                pm.new_product = byte.Parse(newproduct == true ? "1" : "0");
                pm.export = export ;
                pm.manufacturer_part_number = manufacturer_part_number;
                pm.supplier_sku = supplier_sku;
                pm.split_line = byte.Parse(split_line == true ? "1" : "0");
                pm.hot = byte.Parse(hot == true ? "1" : "0");
                pm.last_regdate = DateTime.Now;
                pm.Update();
            }
            AnthemHelper.Alert(KeyFields.SaveIsOK);
            bool b = this.cb_showit_all.Checked;
            BindProductDG(true, MenuChildSerialNO, b == true ? Showit.all : Showit.show_true);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
    /// <summary>
    /// change all product showit properties
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void cb_all_checked_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < this.dg_part.Items.Count; i++)
        {

            AnthemHelper.SetAnthemDataGridCellCheckBoxChecked(this.dg_part.Items[i], 1, "_cb_showit", this.cb_all_checked.Checked);

        }
       
    }  
    
    protected void dg_part_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            bool split_line = AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(e.Item, 4, "_cb_split_line");
            if (split_line)
            {

                e.Item.BackColor = System.Drawing.Color.Green;
                
            }

            int pid = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
            decimal save_price = ProductModel.FindOnSaleDiscountByPID(pid);
            decimal price = AnthemHelper.GetAnthemDataGridCellTextBoxTextDecimal(e.Item, 7, "_txt_price");
            Anthem.Label lbl = (Anthem.Label)e.Item.Cells[5].FindControl("_lbl_sold_price");
      
            if (save_price == 0)
            {
                
                lbl.Text = price.ToString("$###,###.00");
            }
            else
            {
                lbl.ForeColor = System.Drawing.Color.Red;
                lbl.Text = (price - save_price).ToString("$###,###.00");
            }
        }
        this.dg_part.UpdateAfterCallBack = true;
    }
    #endregion


    protected void dg_part_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "ChangeGroup":
                int part_id = int.Parse(e.Item.Cells[0].Text);
                AnthemHelper.OpenWin("q_admin/part_and_group.aspx?partID=" + part_id.ToString(), 600, 500, 100, 100);
                break;
        }
    }
    protected void cb_all_export_checked_CheckedChanged(object sender, EventArgs e)
    {
        for (int i = 0; i < this.dg_part.Items.Count; i++)
        {

            AnthemHelper.SetAnthemDataGridCellCheckBoxChecked(this.dg_part.Items[i], 3, "_cb_export", this.cb_all_export_checked.Checked);

        }
    }
    protected void btn_generate_price_file_Click(object sender, EventArgs e)
    {
        AnthemHelper.OpenWin("/Generate/GenerateList.aspx?categoryid=" + MenuChildSerialNO.ToString(), 600, 500, 100, 100);
    }
    protected void CheckBox_display_stock_status_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            ProductCategoryModel pcm = ProductCategoryModel.GetProductCategoryModel(MenuChildSerialNO);
            pcm.is_display_stock = this.CheckBox_display_stock_status.Checked;
            pcm.Update();
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
}
