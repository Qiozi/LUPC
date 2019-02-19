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

public partial class Q_Admin_product_helper_query_keyword : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
        }
    }
    

    private void BindLV(int category_id)
    {
        this.lv_keyword_cate_list.DataSource = Config.ExecuteDataTable(string.Format(@"
select * from tb_product_category_keyword where category_id='{0}' order by priority, id asc", category_id));
        this.lv_keyword_cate_list.DataBind();
    }

    protected void btn_go_Click(object sender, EventArgs e)
    {
        this.literal_categorys.Text = "";
        this.txt_container_category.Text = "";
        this.CurrentCategoryID = this.CategoryDropDownLoad1.categoryId;
        if (CategoryDropDownLoad1.categoryId > 0)
        {
          
            DataTable dt = Config.ExecuteDataTable(string.Format("Select menu_child_name, keywords_cates from tb_product_category where menu_child_serial_no='{0}'", this.CategoryDropDownLoad1.categoryId));
            if (dt.Rows.Count == 1)
            {
                string cids = dt.Rows[0][1].ToString().Trim();
                this.lbl_category_name.Text = dt.Rows[0][0].ToString();
                this.txt_container_category.Text = cids;
                if (cids.Length > 0)
                {
                    if (cids.Substring(0, 1) == ",")
                        cids = cids.Substring(1);
                    if (cids.Substring(cids.Length - 1, 1) == ",")
                        cids = cids.Substring(0, cids.Length - 1);
                    if (cids.Length == 0)
                        cids = "0";
                    DataTable cndt = Config.ExecuteDataTable("select menu_child_name from tb_product_category where menu_child_serial_no in ("+ cids+")");
                    for (int i = 0; i < cndt.Rows.Count; i++)
                    {
                        this.literal_categorys.Text += string.Format("｜&nbsp;&nbsp;{0}", cndt.Rows[i][0].ToString());
                    }
                }
                this.btn_new_query_cate.Visible = true;
                this.txt_container_category.Visible = true;
            }
        }
        else
        {
            this.lbl_category_name.Text = "None";
        }
        BindLV(CurrentCategoryID);

  
    }

    #region properties
    public int CurrentCategoryID
    {
        get { return (int)ViewState["CurrentCategoryID"]; }
        set { ViewState["CurrentCategoryID"] = value; }
    }


    #endregion
    protected void btn_new_query_cate_Click(object sender, EventArgs e)
    {
        if (CurrentCategoryID > 0)
        {
            var pckm = new tb_product_category_keyword();// ProductCategoryKeywordModel();
            pckm.category_id = CurrentCategoryID;
            pckm.keyword = "";
            pckm.showit = true;
            DBContext.tb_product_category_keyword.Add(pckm);
            DBContext.SaveChanges();
        }
        BindLV(CurrentCategoryID);
    }
    protected void lv_keyword_cate_list_ItemCommand(object sender, ListViewCommandEventArgs e)
    {
        switch (e.CommandName)
        {
            case "SaveParentKeyword":
                string parent_keyword = ((TextBox)e.Item.FindControl("_txt_parent_keyword")).Text.Trim();
                var pckm = ProductCategoryKeywordModel.GetProductCategoryKeywordModel(DBContext, int.Parse(e.CommandArgument.ToString()));
                pckm.keyword = parent_keyword;
                DBContext.SaveChanges();
                BindLV(CurrentCategoryID);
                break;

            case "NewChildCateKeyword":
                string keyword = ((TextBox)e.Item.FindControl("_txt_keyword")).Text.Trim();
                if (keyword.Length > 0)
                {
                    var pcksm = new tb_product_category_keyword_sub();// ProductCategoryKeywordSubModel();
                    pcksm.parent_id = int.Parse(e.CommandArgument.ToString());
                    pcksm.keyword = keyword;
                    pcksm.showit = true;
                    pcksm.regdate = DateTime.Now;
                    DBContext.tb_product_category_keyword_sub.Add(pcksm);
                    DBContext.SaveChanges();

                    BindLV(CurrentCategoryID);
                }
                else
                {

                    CH.Alert("Please Input.", this.Literal1);
                }
                break;
        }
    }
    protected void lv_keyword_cate_list_ItemDataBound(object sender, ListViewItemEventArgs e)
    {
        string parent_keyword = ((TextBox)e.Item.FindControl("_txt_parent_keyword")).Text.Trim();
        if (parent_keyword.Length <= 0)
        {
            ((Button)e.Item.FindControl("_btn_save")).Visible = true;
        }
        HiddenField _hf_id = (HiddenField)e.Item.FindControl("_hf_id");
        int parent_id;
        int.TryParse(_hf_id.Value, out parent_id);

        Repeater rpt = (Repeater)e.Item.FindControl("_rpt_sub_keyword");
        rpt.DataSource = Config.ExecuteDataTable("select * from tb_product_category_keyword_sub where showit=1 and parent_id='" + parent_id.ToString() + "' order by priority,id asc");
        rpt.DataBind();
        rpt.ItemCommand += new RepeaterCommandEventHandler(rpt_ItemCommand);
    }

    protected void rpt_ItemCommand(object source, RepeaterCommandEventArgs e)
    {

        switch (e.CommandName)
        {
            case "DeleteSubKeyword":
                Config.ExecuteNonQuery("Delete from tb_product_category_keyword_sub where id='" + e.CommandArgument.ToString() + "'");
                BindLV(CurrentCategoryID);
                break;
        }

    }
    protected void btn_save_cates_id_Click(object sender, EventArgs e)
    {
        try
        {
            if (this.txt_container_category.Text.Trim().Length == 0)
            {
                CH.Alert("Please input IDS", this.Literal1);
                return;
            }
            var pcm = ProductCategoryModel.GetProductCategoryModel(DBContext, CurrentCategoryID);
            pcm.keywords_cates = this.txt_container_category.Text.Trim();
            //pcm.is_view_all = true;
            DBContext.SaveChanges();
            CH.Alert(KeyFields.SaveIsOK, this.Literal1);
            btn_go_Click(null, null);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.Literal1);
        }
    }
}
