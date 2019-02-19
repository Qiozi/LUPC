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

public partial class Q_Admin_sale_promotion_History : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.sales_promotion_rebate);
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindProductPromotionHistory(false);
        BindProductNameLabel();
    }

    private void BindProductPromotionHistory(bool autoUpdate)
    {
        // 1 表示promotion类
        this.dg_product_history.DataSource = SalePromotionModel.GetModelsByProduct(ProductID,int.Parse(this.radio_rebate.SelectedValue));
        this.dg_product_history.DataBind();

        this.dg_product_history.UpdateAfterCallBack = autoUpdate;
    }

    private void BindProductNameLabel()
    {
        this.lbl_product.Text = ProductModel.GetProductModel(ProductID).product_name;
       
    }

    public int ProductID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "product_id", -1); }
    }
    protected void radio_rebate_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProductPromotionHistory(true);
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            for (int i = 0; i < this.dg_product_history.Items.Count; i++)
            {
                int serial_no = AnthemHelper.GetAnthemDataGridCellText(this.dg_product_history.Items[i], 0);
                bool b = AnthemHelper.GetAnthemDataGridCellCheckBoxChecked(this.dg_product_history.Items[i], 6, "_cb_showit");
                SalePromotionModel spm = SalePromotionModel.GetSalePromotionModel(serial_no);
                spm.show_it = b;
                spm.Update();

            }
            BindProductPromotionHistory(true);
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
}
