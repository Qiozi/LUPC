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

public partial class Q_Admin_product_part_move_or_copy : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            // this.lbl_cmd.Text = quantity
            if (cmd == "Copy")
                this.CategoryDropDownLoad1.CFT = categoryFileType.part_vistual;


            this.lbl_cmd.Text = cmd;
            this.lbl_sku.Text = Sku.ToString();

        }
    }
    
    #region properties
    public string cmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    public int Sku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", -1); }
    }
    #endregion
    protected void btn_cmd_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.CategoryDropDownLoad1.categoryId > 0)
            {
                int priority;
                int.TryParse(this.txt_priority.Text, out priority);
                if (cmd == "Move")
                {
                    Config.ExecuteNonQuery("Update tb_product set menu_child_serial_no='" + this.CategoryDropDownLoad1.categoryId.ToString() + "',is_modify=1 where product_serial_no='" + Sku.ToString() + "'");
                    CH.RunJavaScript("window.close();", this.Literal1);
                }
                if (cmd == "Copy")
                {
                    Config.ExecuteNonQuery(string.Format(@"
delete from tb_product_virtual where lu_sku='{0}' and menu_child_serial_no='{1}';
insert into tb_product_virtual 
	( lu_sku, menu_child_serial_no, priority, showit)
	values
	( '{0}', '{1}', '{2}', '{3}')", Sku, this.CategoryDropDownLoad1.categoryId, priority, 1));

                    CH.RunJavaScript("window.close();", this.Literal1);
                }
            }
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.Literal1);
        }
    }
}
