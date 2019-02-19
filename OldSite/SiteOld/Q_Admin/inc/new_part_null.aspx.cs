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

public partial class Q_Admin_inc_new_part_null : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string skus = "";
            for (int i = 0; i < quantity; i++)
            {

                if (CID > 0)
                {
                    DataTable dt = Config.ExecuteDataTable(string.Format(@"Insert into tb_product (tag,other_product_sku,export,new,product_name, issue, split_line, is_non, regdate, last_regdate, menu_child_serial_no)
values 
(0,999999,1,1,'new part.....', 0, 0, 0, now(),now(),'{0}');
select last_insert_id();", CID));
                    if (i == 0 && dt.Rows.Count == 1)
                    {
                        skus = dt.Rows[0][0].ToString() + " ~~";
                    }
                    if (i == quantity - 1 && dt.Rows.Count == 1)
                    {
                        skus += dt.Rows[0][0].ToString();
                    }

                }

            }
            if (skus != "")
            {
                Response.Write("<script>alert('"+skus+"');</script>");
            }
            else
            {
                Response.Write("<script>alert('No add part.');</script>");
              
            }
            Response.Write("<script>window.close();</script>");
            Response.End();
        }
    }

    public int quantity
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "qty", 0); }
    }

    public int CID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "cid", 0); }
    }
}
