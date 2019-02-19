using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_UC_CategoryDropDownLoad : CtrlBase
{
    public delegate void userEvent(object sender, EventArgs arg);

     public event userEvent TextChange;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            switch (CFT)
            {
                case categoryFileType.no_system:
                    this.Literal1.Text = @"<iframe frameborder=""0"" src=""/q_admin/asp/category_selected_not_sys.asp?div_id=uc_dropDownList_category_selected&id=" + this.txt_id.ClientID + "&textid=" + this.txt_text.ClientID + "\" style=\"width: 300px; height: 300px;border: 1px solid #ccc;\" ></iframe>";
                    break;
                case categoryFileType.system:
                    this.Literal1.Text = @"<iframe frameborder=""0"" src=""/q_admin/asp/category_selected_sys.asp?div_id=uc_dropDownList_category_selected&id=" + this.txt_id.ClientID + "&textid=" + this.txt_text.ClientID + "\" style=\"width: 300px; height: 300px;border: 1px solid #ccc;\" ></iframe>";
                   
                    break;
                case categoryFileType.ASI:
                    this.Literal1.Text = @"<iframe frameborder=""0"" src=""/q_admin/asp/asi_category.asp?div_id=uc_dropDownList_category_selected&id=" + this.txt_id.ClientID + "&textid=" + this.txt_text.ClientID + "\" style=\"width: 300px; height: 300px;border: 1px solid #ccc;\" ></iframe>";
                   
                    break;
                case categoryFileType.system_vistual:
                    this.Literal1.Text = @"<iframe frameborder=""0"" src=""/q_admin/asp/category_selected_sys_virtual.asp?div_id=uc_dropDownList_category_selected&id=" + this.txt_id.ClientID + "&textid=" + this.txt_text.ClientID + "\" style=\"width: 300px; height: 300px;border: 1px solid #ccc;\" ></iframe>";
                   
                    break;
                case categoryFileType.part_vistual:
                    this.Literal1.Text = @"<iframe frameborder=""0"" src=""/q_admin/asp/category_selected_not_sys_virtual.asp?div_id=uc_dropDownList_category_selected&id=" + this.txt_id.ClientID + "&textid=" + this.txt_text.ClientID + "\" style=\"width: 300px; height: 300px;border: 1px solid #ccc;\" ></iframe>";
                    
                    break;
                case categoryFileType.parent:
                    this.Literal1.Text = @"<iframe frameborder=""0"" src=""/q_admin/asp/category_selected_parent.asp?div_id=uc_dropDownList_category_selected&id=" + this.txt_id.ClientID + "&textid=" + this.txt_text.ClientID + "\" style=\"width: 300px; height: 300px;border: 1px solid #ccc;\" ></iframe>";
                    break;
                case categoryFileType.all:
                    this.Literal1.Text = @"<iframe frameborder=""0"" src=""/q_admin/asp/category_selected_all.asp?div_id=uc_dropDownList_category_selected&id=" + this.txt_id.ClientID + "&textid=" + this.txt_text.ClientID + "\" style=\"width: 300px; height: 300px;border: 1px solid #ccc;\" ></iframe>";
                   
                    break;
                case categoryFileType.AllAll:
                    this.Literal1.Text = @"<iframe frameborder=""0"" src=""/q_admin/asp/category_selected_allall.asp?div_id=uc_dropDownList_category_selected&id=" + this.txt_id.ClientID + "&textid=" + this.txt_text.ClientID + "\" style=\"width: 300px; height: 300px;border: 1px solid #ccc;\" ></iframe>";
                   
                    break;
                default:
                    this.Literal1.Text = @"<iframe frameborder=""0"" src=""/q_admin/asp/category_selected_not_sys.asp?div_id=uc_dropDownList_category_selected&id=" + this.txt_id.ClientID + "&textid=" + this.txt_text.ClientID + "\" style=\"width: 300px; height: 300px;border: 1px solid #ccc;\" ></iframe>";
                  
                    break;
            }
        }
    }

    protected void lb_openwin_Click(object sender, EventArgs e)
    {
      //Page.CH.RunJavaScript("winOpen(\"/q_admin/asp/CategorySelected.asp?id="+ this.txt_id.ClientID+"&textid="+ this.txt_text.ClientID+"\", 'right_manage', 600, 600, 120, 200);", this.Literal1);
    }


    #region properties
    public string text
    {
        get { return this.txt_text.Text.Trim(); }
        set { this.txt_text.Text = value; }
    }

    public int categoryId
    {
        get
        {
            int _id;
            int.TryParse(this.txt_id.Value, out _id);
            return _id;
        }
        set { this.txt_id.Value = value.ToString(); }
    }

    public object CategoryValue
    {
        get { return this.txt_id.Value; }
        set { this.txt_id.Value = value.ToString(); }
    }

    categoryFileType _categoryFileType = categoryFileType.no_system;
    public categoryFileType CFT
    {
        get { return _categoryFileType; }
        set { _categoryFileType = value; }
    }
   
    #endregion

    protected void txt_text_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void txt_id_ValueChanged(object sender, EventArgs e)
    {
        if (TextChange != null)
            TextChange(sender, e);
    }
}
[Serializable]
public enum categoryFileType
{
    system,
    no_system,
    ASI,
    system_vistual,
    part_vistual,
    parent,
    AllAll,
    all = 999
}
