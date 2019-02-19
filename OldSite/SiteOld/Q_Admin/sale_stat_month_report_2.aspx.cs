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

public partial class Q_Admin_sale_stat_month_report_2 : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            InitialDatabase();
    }

    public override void InitialDatabase()
    {
        base.InitialDatabase();
        BindDL();
    }

    private void BindDL()
    {
        OrderHelperModel ohm = new OrderHelperModel();
        this.repeater_part_list.DataSource = ohm.FindStatByYear();
        this.repeater_part_list.DataBind();
    }
    protected void repeater_part_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType != ListItemType.Footer
            && e.Item.ItemType != ListItemType.Header
            && e.Item.ItemType != ListItemType.Pager)
        {
            int year = 0;
            int.TryParse(((Literal)e.Item.FindControl("_literal_year")).Text, out year);

            ((Literal)e.Item.FindControl("_literal_month_string")).Text = GenerateMontyStatString(year);
        }
    }

    private string GenerateMontyStatString(int year)
    {
        OrderHelperModel ohm = new OrderHelperModel();
        DataTable dt = ohm.FindStatByMonty(year);
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"<table border=""0"" class=""table_report"" width=""700""  cellspacing=""1"">
            <thead>
                <tr style='background:#aaa'>
                    <td>Month</td>
                    <td>GST</td>
                    <td>PST</td>
                    <td>HST</td>
                    <td>taxable</td>
                    <td>Grand Total</td>
                </tr>
            </thead>");
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            DataRow dr = dt.Rows[i];
            sb.Append(" <tr style='background: #f2f2f2'>");
            sb.Append(string.Format("   <td style=\"cursor: pointer\" onclick=\"statShowYearDetail('table_stat_year_month_{0}_{1}','table_stat_year_month_{0}_{1}_2', {0},{1});\"  class=\"tdChangeBgColor\">{1}</td>", year, dr["m"].ToString()));
            sb.Append(string.Format("   <td>{0}</td>", decimal.Parse(dr["gst"].ToString()).ToString("###,###.00")));
            sb.Append(string.Format("   <td>{0}</td>", decimal.Parse(dr["pst"].ToString()).ToString("###,###.00")));
            sb.Append(string.Format("   <td>{0}</td>", decimal.Parse(dr["hst"].ToString()).ToString("###,###.00")));
            sb.Append(string.Format("   <td>{0}</td>", decimal.Parse(dr["taxable_total"].ToString()).ToString("###,###.00")));
            sb.Append(string.Format("   <td>{0}</td>", decimal.Parse(dr["grand_total"].ToString()).ToString("###,###.00")));
            sb.Append("</tr>");
            //
            // month detail
            sb.Append(string.Format(" <tr style='display:none;' id='table_stat_year_month_{0}_{1}'> ", year, dr["m"].ToString()));
            sb.Append(string.Format("   <td></td>"));
            sb.Append(string.Format("   <td colspan='5'  style='text-align:center; background: #fff' id='table_stat_year_month_{0}_{1}_2'>{2}</td>", year, dr["m"].ToString(), "Loading..."));
            sb.Append("</tr>");
        }
        sb.Append("</table>");
        return sb.ToString();
    }
}
