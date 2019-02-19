using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Xml;

public partial class Q_Admin_ebayMaster_Online_GetItemStoreCategory : PageBase
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
        if (ReqCmd == "qiozi@msn.com")
            ValidEBaySysLogo();
    }

    void ValidEBaySysLogo()
    {
        Response.Clear();
        string category1 = "";
        string category2 = "";
        string cont = EbayItemGenerate.GetItem(ReqItemId, "<OutputSelector>Item.Storefront</OutputSelector>", ReqSKU, true);
        FilterStr(cont, ref category1, ref category2);

        EbaySystemAndCategoryModel.DeleteAll("SystemSku=" + ReqSKU);
        AddCategory(category1.Trim(), ReqSKU);
        AddCategory(category2.Trim(), ReqSKU);

        Response.Write("1");
        Response.End();
    }

    void AddCategory(string ebayCategory, int reqSKU)
    {
        if (!string.IsNullOrEmpty(ebayCategory))
        {


            int cid = ProductCategoryModel.GetCategoryID(ebayCategory);
            if (cid == 0)
                return;
            EbaySystemAndCategoryModel escm = new EbaySystemAndCategoryModel();
            escm.eBaySysCategoryID = cid;
            escm.SystemSku = reqSKU;
            escm.Create();
        }
    }

    void FilterStr(string str, ref string category1, ref string category2)
    {
        if (!string.IsNullOrEmpty(str))
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(str);

                category1 = doc["GetItemResponse"]["Item"]["Storefront"]["StoreCategoryID"].InnerText;
                category2 = doc["GetItemResponse"]["Item"]["Storefront"]["StoreCategory2ID"].InnerText;
            }
            catch { }

        }
    }


    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    int ReqSKU
    {
        get { return Util.GetInt32SafeFromQueryString(Page, "sku", -1); }
    }

    string ReqItemId
    {
        get { return Util.GetStringSafeFromQueryString(Page, "itemid"); }
    }
}