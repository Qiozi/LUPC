using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_ebayMaster_LU_eBay_Sys_Category_Edit : PageBase
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
        InitPage();
    }

    private void InitPage()
    {
        BindCateList();

        // Bind Templete DDL
        this.ddl_templete.DataSource = Config.ExecuteDataTable(@"Select * from tb_ebay_templete");
        this.ddl_templete.DataTextField = "templete_comment";
        this.ddl_templete.DataValueField = "id";
        this.ddl_templete.DataBind();

        this.ddl_templete.Items.Insert(0, new ListItem("Select", "-1"));
    }

    private void BindCateList()
    {
        //
        // bind GridView
        this.DataList1.DataSource = GetCateList();
        this.DataList1.DataBind();
    }

    private DataTable GetCateList()
    {
        DataTable dt= Config.ExecuteDataTable("Select *, '' tpl_id from tb_product_category_new order by priority desc");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataTable cdt = Config.ExecuteDataTable("select templete_id from tb_ebay_templete_and_category where sys_category_id ='" + dt.Rows[i]["category_id"].ToString() + "'");
            if (cdt.Rows.Count == 1)
                dt.Rows[i]["tpl_id"] = cdt.Rows[0][0].ToString();
            else
                dt.Rows[i]["tpl_id"] = 0;
        }
        return dt;
    }

    protected void btn_submit_Click(object sender, EventArgs e)
    {
        string cate_name = this.txt_cate_name.Text.Trim();
        int priority ;
        int.TryParse( this.txt_priority.Text.Trim(),out priority);

        bool showit = this.cb_showit.Checked;
        try
        {
            if (ReqID > 0)
            {
                ProductCategoryNewModel pc = ProductCategoryNewModel.GetProductCategoryNewModel(ReqID);
                pc.category_name = cate_name;
                pc.priority = priority;
                pc.showit = showit;
                pc.Update();

               
            }
            else
            {
                ProductCategoryNewModel pc = new ProductCategoryNewModel();
                pc.category_name = cate_name;
                pc.priority = priority;
                pc.showit = showit;
                pc.Create();
            }

            Config.ExecuteNonQuery(@"
delete from tb_ebay_templete_and_category where sys_category_id='"+ ReqID.ToString()+@"';");

            EbayTempleteAndCategoryModel etc = new EbayTempleteAndCategoryModel();
            etc.sys_category_id = ReqID;
            etc.templete_id = int.Parse(this.ddl_templete.SelectedValue.ToString());
            etc.Create();
            this.ddl_templete.SelectedValue = "-1";
            this.txt_cate_name.Text = "";
            this.txt_priority.Text = "";
        }
        catch (Exception ex)
        {
            this.Label_note.Text = ex.Message;
        }
        this.Label_note.Text = "It is OK.";
        BindCateList();
        ReqID = -1;
    }

    protected void DataList1_ItemCommand(object source, DataListCommandEventArgs e)
    {
        this.Label_note.Text = "";
        switch (e.CommandName)
        {
            case "EditComment":
                //int id;
                //int.TryParse(e.CommandSource, out id);
                int priority;
                int.TryParse(((Label)e.Item.FindControl("lbl_priority")).Text, out priority);

                int tpl_id;
                int.TryParse(((Label)e.Item.FindControl("lbl_tpl_id")).Text, out tpl_id);
                if (tpl_id > 0)
                    this.ddl_templete.SelectedValue = tpl_id.ToString();
                else
                    this.ddl_templete.SelectedValue = "-1";

                string category_name = ((Label)e.Item.FindControl("lbl_cate_name")).Text;

                int id;
                int.TryParse(e.CommandArgument.ToString(), out id);

                bool showit = ((Label)e.Item.FindControl("lbl_showit")).Text =="1";

                ReqID = id;

                this.txt_cate_name.Text = category_name;
                this.txt_priority.Text = priority.ToString();
                this.cb_showit.Checked = showit;
                break;
        }
    }

    #region preporites
    public int ReqID
    {
        get
        {
            if (ViewState["id"] == null)
                return -1;
            return int.Parse(ViewState["id"].ToString());
        }
        set { ViewState["id"] = value; }
    }
    #endregion
}
