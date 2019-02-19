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

public partial class HS_Admin_UC_Menus : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.InitialDatabase();
        }
    }

    private void InitialDatabase()
    {

        //MenuModel[] mcm = MenuModel.MenuModelsByMenuParentSerialNo(menu_id);
        //this.Repeater1.DataSource = mcm;
        //this.Repeater1.DataBind();

        //this.Repeater1.Visible = true;


        //if (mcm.Length < 1)
        //    Response.Write("<script> window.onload = function() { document.getElementById('win_left').style.display='none';}</script>");
            
    }

    public int menu_id
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "menu_id", -1);}
    }

    public sys_model SysModel
    {
        get {
            foreach (int i in Enum.GetValues(typeof(sys_model)))
            { 
                if(i == menu_id)
                    return (sys_model)Enum.Parse(typeof(sys_model),Enum.GetName(typeof(sys_model),i));
            }
            return sys_model.product;
        }
    }
}
