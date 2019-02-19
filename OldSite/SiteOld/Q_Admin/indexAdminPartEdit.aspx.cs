using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Q_Admin_indexAdminPartEdit : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }

    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();

        BindLCD();
    }

    void BindLCD()
    {
        DataTable dt1 = Config.ExecuteDataTable("select * from tb_pre_index_page_setting where cateid='" + ReqCategoryId + "'");
        rptLaptop.DataSource = dt1;
        rptLaptop.DataBind();
    }

    int ReqCategoryId
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "cid", -1); }
    }
    protected void btnLaptop_Click(object sender, EventArgs e)
    {
        Repeater rpt = rptLaptop;
        this.lblLaptopNote.Text = "";
        for (int i = 0; i < rpt.Items.Count; i++)
        {
            HiddenField id = rpt.Items[i].FindControl("_hfid") as HiddenField;
            TextBox txtSKU = rpt.Items[i].FindControl("_txtSKU") as TextBox;
            TextBox txtTitle = rpt.Items[i].FindControl("_txtTitle") as TextBox;

            Config.ExecuteNonQuery(string.Format("Update tb_pre_index_page_setting set sku='{0}', title='{1}' where id='{2}';"
                , string.IsNullOrEmpty(txtSKU.Text.Trim()) ? "0" : txtSKU.Text.Trim()
                , txtTitle.Text.Trim().Replace("'", "\\'")
                , id.Value));

        }
        this.lblLaptopNote.Text = "<span style='color:red;'>save is OK</span>";
    }
}