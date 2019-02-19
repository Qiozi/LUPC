using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Q_Admin_UC_IncStat : CtrlBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //InitialPage();
        }
    }

//    private void InitialPage()
//    {
//        this.GridView1.DataSource = Config.ExecuteDataTable(@"select 	ID, other_inc_name 
//,   case when inc_record=0 then '' else inc_record end as inc_record 
//,   case when inc_record_valid=0 then '' else inc_record_valid end as inc_record_valid 
//,   case when inc_record_match=0 then '' else inc_record_match end as inc_record_match 
//,   case when bigger_than_lu=0 then '' else bigger_than_lu end as bigger_than_lu 
//,   case when less_than_lu=0 then '' else less_than_lu end as less_than_lu 
//,   case when equal_than_lu=0 then '' else equal_than_lu end as equal_than_lu 
//
//, last_run_date
//	 
//	from 
//	tb_other_inc 
//	");
//        this.GridView1.DataBind();

//        this.literal_amount_datetime.Text = Config.ExecuteScalar("select max(run_regdate) from tb_other_inc_amount_date").ToString();
//    }
//    protected void btn_run_amount_Click(object sender, EventArgs e)
//    {
//        try
//        {
//            OtherIncPartInfoModel oipim = new OtherIncPartInfoModel();
//            oipim.RunAmount();
//            InitialPage();
//            Page.CH.CloseParentWatting(this.GridView1);
//            Page.CH.Alert("OK", this.GridView1);
//        }
//        catch (Exception ex)
//        {
//            Page.CH.CloseParentWatting(this.GridView1);
//            Page.CH.Alert(ex.Message, this.GridView1);
//        }
//    }
//    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
//    {
//        if (e.Row.RowType != DataControlRowType.Footer
//            && e.Row.RowType != DataControlRowType.Header
//            && e.Row.RowType != DataControlRowType.Pager)
//        {
//            if (e.Row.Cells[1].Text.IndexOf("Line") != -1)
//                e.Row.BackColor = System.Drawing.Color.FromName("green");

//            if (e.Row.Cells[8].Text.IndexOf("0000") != -1)
//                e.Row.Cells[8].Text = "";
//        }
//    }
}
