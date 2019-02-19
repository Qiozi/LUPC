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

public partial class product_business_title : PageBase
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

        BusinessModel bm = BusinessModel.GetBusinessModel(SKU);
        this.txt_content.Text = bm.business_content;
        this.txt_title.Text = bm.business_title;
        this.txt_url.Text = bm.business_img_url;


    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        try
        {
            BusinessModel bm = BusinessModel.GetBusinessModel(SKU);
            bm.business_content = this.txt_content.Text;
            bm.business_img_url = this.txt_url.Text;
            bm.business_title = this.txt_title.Text;
            bm.Update();
            CH.Alert(KeyFields.SaveIsOK, this.Literal1);
        }
        catch (Exception ex)
        {
            CH.Alert(ex.Message, this.Literal1);
        }
    }

    #region properties
    public int SKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "id", -1); }
    }
    #endregion
}
