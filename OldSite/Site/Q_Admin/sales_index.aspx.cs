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
using OpenFlashChart;
using System.Collections.Generic;

public partial class Q_Admin_sales_index : PageBase
{
    decimal _total_30_day = 0M;
    decimal _total_current_month = 0M;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSatDG();

            BindChangePrice();
            //BindETC();

            BindCurrencyConverter();
        }
    }


    #region methods


    private void BindChangePrice()
    {

    }

    private void BindETC()
    {
       
    }

    private void BindSatDG()
    {
        //Stat s = new Stat();
        //this.dg_sat.DataSource = s.FindOrderTotalAgo30day();
        //this.dg_sat.DataBind();

        //this.dg_current_month.DataSource = s.FindOrderStatByMonthAgo();
        //this.dg_current_month.DataBind();
    }

    #endregion

    protected void dg_sat_ItemDataBound(object sender, GridViewRowEventArgs e)
    {
        GridView dg = (GridView)sender;
        if (e.Row.RowType != DataControlRowType.Header
            && e.Row.RowType != DataControlRowType.Footer
            && e.Row.RowType != DataControlRowType.Pager)
        {


            Label lbl = (Label)e.Row.Cells[2].FindControl("_lbl_chat");
            decimal width = 0;

            decimal.TryParse(e.Row.Cells[1].Text, out width);
            if (width > 20000M && "dg_sat" == dg.ID)
                width = 20000;
            if ("dg_sat" == dg.ID)
                _total_30_day += width;
            else
                _total_current_month += width;

            string background = "red";

            if (width > 999.99M)
                background = "Brown";
            if (width > 4999.99M)
                background = "green";
            lbl.Text = string.Format("<div style=\"background:{1}; height:14px; width: {0}px;\">&nbsp;</div>", width / ("dg_sat" == dg.ID ? 100 : 1000), background);

            if (e.Row.Cells.Count > 6)
            {
                lbl = (Label)e.Row.Cells[6].FindControl("_lbl_pre_chat");
                width = 0;

                decimal.TryParse(e.Row.Cells[5].Text, out width);
                if (width > 20000M && "dg_sat" == dg.ID)
                    width = 20000;
                if ("dg_sat" == dg.ID)
                    _total_30_day += width;
                else
                    _total_current_month += width;

                background = "red";

                if (width > 999.99M)
                    background = "Brown";
                if (width > 4999.99M)
                    background = "green";
                lbl.Text = string.Format("<div style=\"background:{1}; height:14px; width: {0}px;\">&nbsp;</div>", width / ("dg_sat" == dg.ID ? 100 : 1000), background);
            }
        }
    }
    protected void dg_sat_DataBound(object sender, EventArgs e)
    {
        GridView dg = (GridView)sender;
        //if ("dg_sat" == dg.ID)
        //{
        //    this.lbl_30day_total.Text = string.Format("{0}", _total_30_day.ToString("$###,###.00"));
        //    dg.Columns[2].HeaderText = string.Format("{0}", _total_30_day.ToString("$###,###.00"));
        //}
        //else
        //{
        //    this.lbl_current_month_total.Text = string.Format("{0}", _total_current_month.ToString("$###,###.00"));
        //    dg.Columns[2].HeaderText = string.Format("{0}", _total_current_month.ToString("$###,###.00"));
        //}
    }

    private void BindCurrencyConverter()
    {
        DataTable dt = Config.ExecuteDataTable("Select id,currency_cad, currency_usd, case when is_auto=1 then 'auto' else 'input' end as is_auto, date_format(regdate, '%b/%d/%Y') regdate from tb_currency_convert order by id desc limit 0,5");
        this.rpt_currency_converter.DataSource = dt;
        this.rpt_currency_converter.DataBind();


        if (dt.Rows.Count > 0)
            this.lbl_currency_converter.Text = ConvertPrice.CurrentCurrencyConverter.ToString();// dt.Rows[0]["currency_usd"].ToString();
    }



}
