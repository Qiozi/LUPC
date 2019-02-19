using LU.Data;
using System;
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

public partial class Q_Admin_product_helper_category_coefficient : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            CoefficientID = 0;
            SetMenuChildName(CategoryID);
            SetCoefficientValue(CategoryID);
        }
    }
    
    private void SetMenuChildName(int id)
    {
        var mcm = ProductCategoryModel.GetProductCategoryModel(DBContext, id);
        this.lbl_menu_child_name.Text = mcm.menu_child_name;
    }

    private void SetCoefficientValue(int id)
    {
        var pcc = new ProductCoefficientCategory();
        var pcs = pcc.FindModelsByCategoryID(DBContext, id);

        if (pcs.Length == 1)
            IsNew = false;
        else
            IsNew = true;
        if (!IsNew)
        {
            this.txt_category_coefficient.Text = pcs[0].coefficient.ToString();
            CoefficientID = pcs[0].id;
        }
    }

    #region porperties
    private bool IsNew
    {
        get { return (bool)ViewState["IsNew"]; }
        set { ViewState["IsNew"] = value; }
    }
    private int CoefficientID
    {
        get { return (int)ViewState["CoefficientID"]; }
        set { ViewState["CoefficientID"] = value; }
    }
    private int CategoryID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "categoryID", -1); }
    }
    #endregion
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsNew)
            {
                var pcc = new tb_product_coefficient_category();
                decimal coefficient;
                decimal.TryParse(this.txt_category_coefficient.Text, out coefficient);
                pcc.coefficient = coefficient;
                pcc.menu_child_serial_no = CategoryID;
                DBContext.tb_product_coefficient_category.Add(pcc);
                DBContext.SaveChanges();
            }
            else
            {
                var pcc = DBContext.tb_product_coefficient_category.FirstOrDefault(me => me.id.Equals(CoefficientID));// ProductCoefficientCategory.GetProductCoefficientCategory(CoefficientID);
                decimal coefficient;
                decimal.TryParse(this.txt_category_coefficient.Text, out coefficient);
                pcc.coefficient = coefficient;
                DBContext.SaveChanges();
            }
            CH.Alert(KeyFields.SaveIsOK, this.txt_category_coefficient);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.txt_category_coefficient);
        }
    }
}
