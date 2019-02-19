using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

public partial class Q_Admin_product_helper_configure_helper : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.configure_helper);
            BindconfigureRadio();
            BindParentCategory();
            BindComputerCaseLbl(false);
            BindComputerCpuLbl(false);
        }
    }
    #region methods


    private void BindComputerCaseLbl(bool autoUpdate)
    {
        int category = ComputerCaseModel.GetComputerCaseModel(DBContext, 1).computer_case_category.Value;
        this.lbl_computer_cases.Text = ProductCategoryModel.GetProductCategoryModel(DBContext, category).menu_child_name;
        this.lbl_computer_cases.AutoUpdateAfterCallBack = autoUpdate;
    }

    private void BindComputerCpuLbl(bool autoUpdate)
    {
        int category = ComputerCpuModel.GetComputerCpuModel(DBContext, 1).computer_cpu_category.Value;
        this.lbl_computer_cpu.Text = ProductCategoryModel.GetProductCategoryModel(DBContext, category).menu_child_name;
        this.lbl_computer_cpu.AutoUpdateAfterCallBack = autoUpdate;
    }

    public void BindconfigureRadio()
    {
        this.radio_configure.DataSource = DBContext.tb_system_configure_category.ToList();// SystemConfigureCategoryModel.FindAll();
        this.radio_configure.DataTextField = "system_configure_category_name";
        this.radio_configure.DataValueField = "system_configure_category_serial_no";
        this.radio_configure.DataBind();
    }

    public void BindConfigureDetailDG(int configure_category_serial_no, bool autoUpdate)
    {
        var model = SystemConfigureCategoryModel.GetSystemConfigureCategoryModel(DBContext, configure_category_serial_no);
        string category_list = model.menu_child_list;
        string[] s = category_list.Split(',');
        DataTable dt = new DataTable();
        dt.Columns.Add("text");
        dt.Columns.Add("value");
        for (int i = 0; i < s.Length; i++)
        {
            if (s[i] != "-1")
            {
                DataRow dr = dt.NewRow();
                dr[0] = s[i];
                dr[1] = s[i];
                dt.Rows.Add(dr);
            }
        }
        this.dg_configure_detail.DataSource = dt;
        this.dg_configure_detail.DataBind();
        this.dg_configure_detail.AutoUpdateAfterCallBack = autoUpdate;
    }

    private void BindParentCategory()
    {
        ArrayList al = ProductCategoryModel.GetAllPartCategory(DBContext);
        ProductCategoryModel[] models = new ProductCategoryModel[al.Count];
        for (int i = 0; i < al.Count; i++)
        {
            ProductCategoryModel m = (ProductCategoryModel)al[i];
            models[i] = m;
        }
        this.dg_part_category.DataSource = models;
        this.dg_part_category.DataBind();
    }

    #endregion

    #region Events

    protected void radio_configure_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindConfigureDetailDG(int.Parse(this.radio_configure.SelectedValue), true);
    }

    #endregion
    protected void dg_configure_detail_ItemDataBound(object sender, DataGridItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Header && e.Item.ItemType != ListItemType.Footer)
        {
            int category_id = AnthemHelper.GetAnthemDataGridCellText(e.Item, 1);
            e.Item.Cells[1].Text = ProductCategoryModel.GetProductCategoryModel(DBContext, category_id).menu_child_name;
        }
    }
    protected void btn_Save_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.radio_configure.SelectedValue != null)
            {

                Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_part_category;
                int configure_id = int.Parse(this.radio_configure.SelectedValue);
                for (int i = 0; i < dg.Items.Count; i++)
                {
                    DataGridItem item = dg.Items[i];
                    int category_id = AnthemHelper.GetAnthemDataGridCellText(item, 0);
                    bool b = AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(item, 2, "_cb_category");
                    if (b)
                    {
                        var model = SystemConfigureCategoryModel.GetSystemConfigureCategoryModel(DBContext, configure_id);
                        if (model.menu_child_list.Trim() != "")
                        {
                            model.menu_child_list += "," + category_id.ToString();

                        }
                        else
                            model.menu_child_list = category_id.ToString();
                        DBContext.SaveChanges();
                    }
                }
                BindConfigureDetailDG(configure_id, true);
            }
            else
                AnthemHelper.Alert("check main category");
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }

    }
    protected void dg_configure_detail_ItemCommand(object source, DataGridCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "Delete":
                int id = AnthemHelper.GetAnthemDataGridCellText(e.Item, 0);
                DeleteCategory(id);
                break;
        }
    }

    private void DeleteCategory(int category)
    {
        Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_configure_detail;
        string ids = "-1";
        for (int i = 0; i < dg.Items.Count; i++)
        {
            DataGridItem item = dg.Items[i];
            int id = AnthemHelper.GetAnthemDataGridCellText(item, 0);
            if (id != category)
                ids += "," + id;
        }
        var model = SystemConfigureCategoryModel.GetSystemConfigureCategoryModel(DBContext, int.Parse(this.radio_configure.SelectedValue));
        model.menu_child_list = ids;
        DBContext.SaveChanges();
        this.BindConfigureDetailDG(int.Parse(this.radio_configure.SelectedValue), true);
    }

    /// <summary>
    /// 保存机箱
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btn_Computer_Cases_Click(object sender, EventArgs e)
    {
        Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_part_category;

        for (int i = 0; i < dg.Items.Count; i++)
        {
            DataGridItem item = dg.Items[i];
            int category_id = AnthemHelper.GetAnthemDataGridCellText(item, 0);
            bool b = AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(item, 2, "_cb_category");
            if (b)
            {
                var model = ComputerCaseModel.GetComputerCaseModel(DBContext, 1);
                model.computer_case_category = category_id;
                DBContext.SaveChanges();
                this.BindComputerCaseLbl(true);
                return;
            }
        }
    }
    protected void btn_save_cpu_Click(object sender, EventArgs e)
    {
        Anthem.DataGrid dg = (Anthem.DataGrid)this.dg_part_category;

        for (int i = 0; i < dg.Items.Count; i++)
        {
            DataGridItem item = dg.Items[i];
            int category_id = AnthemHelper.GetAnthemDataGridCellText(item, 0);
            bool b = AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(item, 2, "_cb_category");
            if (b)
            {
                var model = ComputerCpuModel.GetComputerCpuModel(DBContext, 1);
                model.computer_cpu_category = category_id;
                DBContext.SaveChanges();
                this.BindComputerCpuLbl(true);
                return;
            }
        }
    }
}
