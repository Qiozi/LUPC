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

public partial class product_collection_manage : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitialDatabase();
        }
    }

    #region Methods

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindData(false);
    }

    private void BindData(bool autoUpdate)
    {
        ProductCollectionModel pcm = ProductCollectionModel.GetProductCollectionModel(ReqID);
        if (pcm != null)
        {
            this.txt_navigation.Text = pcm.product_collection_title;
            this.txt_navigation_return_path.Text = pcm.product_collection_title_path;
            this.txt_product_group.Text = pcm.product_collections;
            this.txt_product_topic.Text = pcm.product_collection_topic;

            this.txt_product_topic.UpdateAfterCallBack = autoUpdate;
            this.txt_product_group.UpdateAfterCallBack = autoUpdate;
            this.txt_navigation_return_path.UpdateAfterCallBack = autoUpdate;
            this.txt_navigation.UpdateAfterCallBack = autoUpdate;

            this.ddl_product_category.SelectedValue = pcm.product_collection_redirect_path_type.ToString();
            this.ddl_product_category.UpdateAfterCallBack = autoUpdate;
        }
        else
        {
            this.lbl_note.Text = "ID Error";
            this.lbl_note.UpdateAfterCallBack = true;
        }
    }
    #endregion

    #region properties
    public int ReqID
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "cid", -1); }
    }
    #endregion

    #region Events
    protected void btn_save_Click(object sender, EventArgs e)
    {
        ProductCollectionModel pcm = ProductCollectionModel.GetProductCollectionModel(ReqID);
        pcm.product_collection_redirect_path_type = int.Parse(this.ddl_product_category.SelectedValue);
        pcm.product_collection_title = this.txt_navigation.Text.Trim();
        pcm.product_collection_title_path = this.txt_navigation_return_path.Text.Trim();
        pcm.product_collection_topic = this.txt_product_topic.Text.Trim();
        pcm.product_collections = this.txt_product_group.Text.Trim();
        pcm.regdate = DateTime.Now;
        pcm.Update();
        AnthemHelper.Alert(KeyFields.SaveIsOK);
    }
    #endregion
}
