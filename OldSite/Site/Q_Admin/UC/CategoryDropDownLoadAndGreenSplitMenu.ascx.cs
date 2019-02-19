using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_UC_CategoryDropDownLoadAndGreenSplitMenu :CtrlBase
{
    public delegate void userEvent(object sender, EventArgs arg);

    public event userEvent TextChange;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }

    protected void lb_openwin_Click(object sender, EventArgs e)
    {
        Page.CH.RunJavaScript("winOpen(\"/q_admin/asp/CategorySelected.asp?id=" + this.category_id.ClientID + "&textid=" + this.category_text.ClientID + "&menuid="+ this.menu_id.ClientID+"&menutext="+this.menu_text.ClientID +"\", 'right_manage', 880, 800, 120, 200);", this.Literal1);
    }


    #region properties
    public string text
    {
        get { return this.category_text.Text.Trim(); }
        set { this.category_text.Text = value; }
    }

    public int id
    {
        get
        {
            int _id;
            int.TryParse(this.category_id.Value, out _id);
            return _id;
        }
        set { this.category_id.Value = value.ToString(); }
    }

    public string menuText
    {
        get { return this.menu_text.Text.Trim(); }
        set { this.menu_text.Text = value; }
    }

    public int menuID
    {
        get
        {
            int _id;
            int.TryParse(this.menu_id.Value, out _id);
            return _id;
        }
        set { this.menu_id.Value = value.ToString(); }
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
