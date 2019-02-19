using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

/// <summary>
/// ProductHelper 的摘要说明
/// </summary>
public class ProductPageBase : PageBase
{
	public ProductPageBase()
	{
		//
		// TODO: 在此处添加构造函数逻辑
		//
	}

    #region Fields
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

    public bool product_order
    {
        get
        {
            object o = ViewState["product_order"];
            if (o != null)
                return (bool)o;
            else
                return true;
        }
        set { ViewState["product_order"] = value; }
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

    public ProductModel[] PartProducts
    {
        get
        {
            object o = ViewState["PartProducts"];
            if (o != null)
            {
                return (ProductModel[])o;
            }
            return new ProductModel[0];
        }
        set { ViewState["PartProducts"] = value; }
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

    public int Category
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "product_category", 0); }
    }

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

    public product_page_category page_category
    {
        get
        {
            object o = ViewState["page_category"];
            if (o != null)
                return (product_page_category)Enum.Parse(typeof(product_page_category), o.ToString());
            return product_page_category.part;
        }
        set { ViewState["page_category"] = value; }
    }

    public product_page_status page_status
    {
        get
        {
            object o = ViewState["page_status"];
            if (o != null)
                return (product_page_status)Enum.Parse(typeof(product_page_status), o.ToString());
            return product_page_status.part_group;
        }
        set { ViewState["page_status"] = value; }
    }
    public string selected_style = "input_change";
    #endregion

    public void CheckBoxChange(object sender)
    {
        Anthem.CheckBox cb = (Anthem.CheckBox)sender;
        cb.CssClass = selected_style;
        cb.AutoUpdateAfterCallBack = true;
    }

    public void TextBoxChange(object sender)
    {
        Anthem.TextBox tb = (Anthem.TextBox)sender;
        tb.CssClass = selected_style;
        tb.AutoUpdateAfterCallBack = true;
    }
}

public class ProductCtrlBase : System.Web.UI.UserControl
{
    public ProductPageBase PPageBase
    {
        get { return (ProductPageBase)base.Page; }
    }
}