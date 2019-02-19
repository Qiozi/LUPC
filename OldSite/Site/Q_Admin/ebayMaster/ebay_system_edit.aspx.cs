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

public partial class Q_Admin_ebayMaster_ebay_system_edit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.lbl_luc_sku.Text = "";//Util.GetInt32SafeFromQueryString(Page, 
            BindLVNew();
        }
    }

    public void BindLVNew()
    {
        this.lv_ebay_system.DataSource = Config.ExecuteDataTable("select id, comment from tb_ebay_system_part_comment where showit=1 order by priority asc ");
        this.lv_ebay_system.DataBind();

    }

    #region properties
    public int CategoryID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "category_id", -1); }
    }
    #endregion

    protected void btn_save_step1_Click(object sender, EventArgs e)
    {
        DataTable dt = new DataTable();
        dt.Columns.Add("comment");
        dt.Columns.Add("comment_id");
        dt.Columns.Add("luc_sku");
        dt.Columns.Add("name");

        //bool is_have_one = false;
        System.Text.StringBuilder ids = new System.Text.StringBuilder();
        ids.Append("0");
        for (int i = 0; i < this.lv_ebay_system.Items.Count; i++)
        {
            TextBox _txt_luc_sku = (TextBox)this.lv_ebay_system.Items[i].FindControl("_txt_luc_sku");
            int luc_sku;
            int.TryParse(_txt_luc_sku.Text, out luc_sku);

            Label _lbl_comment = (Label)this.lv_ebay_system.Items[i].FindControl("_lbl_comment");

            if (luc_sku > 0)
            {
               // is_have_one = true;

                HiddenField _hf_comment_id = (HiddenField)this.lv_ebay_system.Items[i].FindControl("_hf_comment_id");
                int comment_id;
                int.TryParse(_hf_comment_id.Value, out comment_id);
                DataRow dr = dt.NewRow();
                dr["comment"] = _lbl_comment.Text;
                dr["luc_sku"] = luc_sku;
                dr["comment_id"] = comment_id;
                ids.Append("," + luc_sku.ToString());
                dt.Rows.Add(dr);
            }
        }
        DataTable partDT = Config.ExecuteDataTable(string.Format(@"
select case when product_ebay_name <> '' then product_ebay_name 
    when product_name_long_en <> '' then product_name_long_en
    when product_name <> '' then product_name 
    else
    product_short_name end as product_ebay_name 
, product_serial_no
from tb_product where product_serial_no in ({0})", ids.ToString()));
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 0; j < partDT.Rows.Count; j++)
            {
                if (dt.Rows[i]["luc_sku"].ToString() == partDT.Rows[j]["product_serial_no"].ToString())
                {
                    string name = partDT.Rows[j]["product_ebay_name"].ToString();
                    dt.Rows[i]["name"] = name;
                }
            }
        }

        this.lv_ebay_system_2.DataSource = dt;
        this.lv_ebay_system_2.DataBind();
        this.panel_step1.Visible = false;
        this.panel_step2.Visible = true;
        CH.RunJavaScript("$('#lbl_edit_step2').attr('class', 'ok');", this.Literal1);
        this.lb_step2.CssClass = "ok";

    }
    protected void lb_step1_Click(object sender, EventArgs e)
    {
        this.panel_step1.Visible = true;
        this.panel_step2.Visible = false;
        this.panel_step3.Visible = false;
        this.panel_step4.Visible = false;

        this.lb_step2.Enabled = false;
        this.lb_step2.CssClass = "step";
        this.lb_step3.Enabled = false;
        this.lb_step3.CssClass = "step";
        this.lb_step4.Enabled = false;
        this.lb_step4.CssClass = "step";

    }
    protected void lb_step2_Click(object sender, EventArgs e)
    {

        this.panel_step1.Visible = false;
        this.panel_step2.Visible = true;
        this.panel_step3.Visible = false;
        this.panel_step4.Visible = false;

        this.lb_step3.Enabled = false;
        this.lb_step3.CssClass = "step";
        this.lb_step4.Enabled = false;
        this.lb_step4.CssClass = "step";
    }
    protected void lb_step3_Click(object sender, EventArgs e)
    {
        this.panel_step1.Visible = false;
        this.panel_step2.Visible = false;
        this.panel_step3.Visible = true;
        this.panel_step4.Visible = false;

        this.lb_step4.Enabled = false;
        this.lb_step4.CssClass = "step";
    }
    protected void lb_step4_Click(object sender, EventArgs e)
    {
        this.panel_step1.Visible = false;
        this.panel_step2.Visible = false;
        this.panel_step3.Visible = false;
        this.panel_step4.Visible = true;
    }
}
