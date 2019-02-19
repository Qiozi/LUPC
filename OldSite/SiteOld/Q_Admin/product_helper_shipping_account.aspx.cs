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

public partial class Q_Admin_product_helper_shipping_account : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            this.ValidateLoginRule(Role.configure_helper);
            InitialDatabase();
        }
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        AccountSetLabel();
    }

    public void AccountSetLabel()
    {
        AccountProduct ap = new AccountProduct();
        ap.shipping_company_id = 2;
        ap.product_size = 1;
        ap.product_cate = product_category.part_product;
        ap.price = 1000;

        AccountProduct ap2 = new AccountProduct();
        ap2.shipping_company_id = 2;
        ap2.product_size = 1;
        ap2.product_cate = product_category.part_product;
        ap2.price = 100;

       
        AccountProduct[] aps = new AccountProduct[] {ap ,ap2};

        Account account = new Account(aps, 13);
        this.lbl_result.Text = account.getResult().ToString();
        
    }
}
