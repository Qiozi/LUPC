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

public partial class Q_Admin_ebayMaster_ebay_system_temp_page_view : PageBase
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            this.Title = "Sys SKU:" + SystemSku.ToString();
            this.literal_page.Text = GetDescription(SystemSku, IsNew);
        }
    }
    /// <summary>
    /// Get eBay system Description.
    /// </summary>
    /// <param name="system_sku"></param>
    /// <param name="is_new"></param>
    /// <returns></returns>
    public string GetDescription(int system_sku, bool is_new)
    {
        var esm = EbaySystemModel.GetEbaySystemModel(DBContext, system_sku);

        return new EbayPageText().GetEbayPageHtml(DBContext, system_sku
            , eBayProdType.system
            , false
            , eBaySystemWorker.GetFlashType(esm));
       // return new eBaySysDescription().GetDescription(system_sku, is_new, this.Server);
    }

    #region Properties
    public int SystemSku
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "system_sku", -1); }
    }
    public bool IsNew
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "New", -1) == 1; }
    }
    #endregion
    
    #region methods

    #endregion
}
