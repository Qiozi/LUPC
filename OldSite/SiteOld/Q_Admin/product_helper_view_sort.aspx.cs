using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_product_helper_view_sort : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
            CH.CloseParentWatting(this.Literal1);
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        CurrentCategoryID = RequestCategoryID;
        if (CurrentCategoryID != -1)
            BindLV(CurrentCategoryID);
     
    }

    private int CurrentCategoryID
    {
        get { return (int)ViewState["CurrentCategoryID"]; }
        set { ViewState["CurrentCategoryID"] = value; }
    }

    public int RequestCategoryID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "categoryid", -1); }
    }

    private void BindLV(int category_id)
    {
        this.lv_part_list.DataSource = Config.ExecuteDataTable(string.Format(@"select product_serial_no, 
case when {1}='' then product_short_name else {1} end as product_name, product_current_price-product_current_discount product_sell, product_order, 
split_line from tb_product where tag=1 and is_non=0 and menu_child_serial_no='{0}' order by product_order asc ", category_id, this.RadioButtonList1.SelectedValue.ToString() == "1" ? " product_name " : "product_name_long_en"));
        this.lv_part_list.DataBind();
    }
    protected void btn_go_Click(object sender, EventArgs e)
    {
        CurrentCategoryID = this.CategoryDropDownLoad1.categoryId;
        BindLV(CurrentCategoryID);
    }
    protected void btn_initial_proirity_Click(object sender, EventArgs e)
    {
        int priority =1000;
        //int.TryParse(string.Format("{0}000000", CurrentCategoryID).Substring(0, 6), out priority);
        for (int i = 0; i < this.lv_part_list.Items.Count; i++)
        {
            TextBox tb = (TextBox)this.lv_part_list.Items[i].FindControl("_txt_priority");
            tb.Text = (priority + i * 50).ToString();
        }
    }
    protected void btn_save_Click(object sender, EventArgs e)
    { string error_skus = "";
        try
        {
           
            for (int i = 0; i < this.lv_part_list.Items.Count; i++)
            {
                int lu_sku;
                int.TryParse(((Label)this.lv_part_list.Items[i].FindControl("_lbl_sku")).Text, out lu_sku);

                try
                {
                    TextBox tb = (TextBox)this.lv_part_list.Items[i].FindControl("_txt_priority");
                    int priority;
                    int.TryParse(tb.Text, out priority);


                    if (lu_sku > 0)
                    {
                        Config.ExecuteNonQuery(string.Format("Update tb_product set product_order='{0}',is_modify=1 where product_serial_no='{1}'", priority, lu_sku));
                    }
                }
                catch{ error_skus += lu_sku.ToString(); }
            }
            InsertTraceInfo(string.Format(" Modify Category({0}) Priority", CurrentCategoryID));
            CH.Alert(KeyFields.SaveIsOK, this.Literal1);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message + "<br/>" + error_skus, this.Literal1);
        }
       
    }
}
