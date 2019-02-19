using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_other_inc_supercom : PageBase
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
        if (Util.GetStringSafeFromQueryString(Page, "cmd") != "BLUComputers.com")
            base.InitialDatabase();
        //
        //
        DataTable dt = Config.ExecuteDataTable(@"select f0, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, f11, f12, 
	f13,f14, luc_sku
	from 
	tb_supercom_store_2");

        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
      
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        ExcelHelper eh = new ExcelHelper(Config.ExecuteDataTable(@"select 	mfp, cost, quantity, date_format(ETA, '%m/%d/%Y') ETA, name, brand, category, supercom_SKU	 
	,UPC,LUC_SKU
from 
	tb_supercom_notebook "));
        eh.FileName = "supercom_notebook.xls";
        eh.Export();
    }
}
