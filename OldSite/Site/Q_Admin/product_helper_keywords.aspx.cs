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
using LU.Data;

public partial class Q_Admin_product_helper_keywords : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.search_keywords_manage);

            BindKeywordDG(false);
        }
    }
    

    private void BindKeywordDG(bool autoUpdate)
    {
     var kms = KeywordModel.FindAllByOrder(DBContext);

        this.DataGrid1.DataSource = kms;
        this.DataGrid1.DataBind();

        this.DataGrid1.UpdateAfterCallBack = autoUpdate;
    }
    protected void btn_new_Click(object sender, EventArgs e)
    {
        var m = new tb_keyword();// KeywordModel();
        DBContext.tb_keyword.Add(m);
        DBContext.SaveChanges();
        BindKeywordDG(true);
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            Anthem.DataGrid dg = (Anthem.DataGrid)this.DataGrid1;
            for (int i = 0; i < dg.Items.Count; i++)
            {
                int id = AnthemHelper.GetAnthemDataGridCellText(dg.Items[i], 0);
                string keyword = AnthemHelper.GetAnthemDataGridCellTextBoxText(dg.Items[i], 1, "_txt_keywords");
                var m = KeywordModel.GetKeywordModelModel(DBContext, id);
                m.keyword = keyword;
                DBContext.SaveChanges();
            }
            BindKeywordDG(true);
            AnthemHelper.Alert(KeyFields.SaveIsOK);
        }
        catch (Exception ex)
        {
            AnthemHelper.Alert(ex.Message);
        }
    }
}
