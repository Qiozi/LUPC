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

public partial class part_product_photo_manage : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    #region Methods

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindPartSKU();

        this.lbl_current_part.Text = this.ProductID.ToString();
    }


    private void BindPartSKU()
    {
        ProductModel pm = ProductModel.GetProductModel(this.ProductID);
        this.txt_sku.Text = pm.other_product_sku.ToString();
        
    }
    #endregion

    #region Properties
    public int ProductID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "productID", -1); }
    }
    #endregion
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            ProductModel pm = ProductModel.GetProductModel(this.ProductID);
            pm.other_product_sku = int.Parse(this.txt_sku.Text.Trim());
            pm.Update();
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
}
