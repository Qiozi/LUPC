using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OpenFlashChart;
using System.Data;

public partial class Q_Admin_OpenFlashChartData : PageBase
{

    string _colorWeb = "#2E58AC";
    string _coloreBay = "#339900";
    string _colorWeb2 = "#8BA2D1";
    string _colorEbay2 = "#B5DAA2";
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

        InitPage();
    }

    string ReqCmd
    {
        get { return Util.GetStringSafeFromQueryString(Page, "cmd"); }
    }

    void InitPage()
    {
        Response.Clear();
        switch (ReqCmd)
        {
            case "GetStat30Days":
                GetStat30Days();
                break;
            case "GetStat360Days":
                GetStat360Days();
                break;
        }

        Response.End();
    }

    void GetStat360Days()
    {
        Stat s = new Stat();
        DataTable dayYear = s.FindOrderStatByMonthAgo();

        List<ChartItem> listChart360 = new List<ChartItem>();
        List<string> title360 = new List<string>();
        double maxValue360 = 0;

        List<double> webValue = new List<double>();
        List<double> ebayValue = new List<double>();
        List<double> webPreValue = new List<double>();
        List<double> ebayPreValue = new List<double>();

        double lastYearWebTotal = 0;
        double lastYearEbayTotal = 0;
        s.FindOrderStatLastYearSameTerm(ref lastYearWebTotal, ref lastYearEbayTotal);

        double webTotal = 0;
        double eBayTotal = 0;


        for (int i = 0; i < dayYear.Rows.Count; i++)
        {
            DataRow dr = dayYear.Rows[i];
            double grandTotal;
            double.TryParse(dr["grand_total"].ToString(), out grandTotal);
            webValue.Add(grandTotal);
            webTotal += grandTotal;

            if (grandTotal > maxValue360)
                maxValue360 = grandTotal;

            double eBayGrandTotal;
            double.TryParse(dr["eBayGrandTotal"].ToString(), out eBayGrandTotal);
            ebayValue.Add(eBayGrandTotal);
            eBayTotal += eBayGrandTotal;

            if (eBayGrandTotal > maxValue360)
                maxValue360 = eBayGrandTotal;


            double preGrandTotal;
            double.TryParse(dr["pre_grand_total"].ToString(), out preGrandTotal);
            webPreValue.Add(preGrandTotal);
            //lastYearWebTotal += preGrandTotal;

            if (preGrandTotal > maxValue360)
                maxValue360 = preGrandTotal;


            double eBayPreGrandTotal;
            double.TryParse(dr["Pre_eBayGrandTotal"].ToString(), out eBayPreGrandTotal);
            ebayPreValue.Add(eBayPreGrandTotal);
            //lastYearEbayTotal += eBayPreGrandTotal;

            if (eBayPreGrandTotal > maxValue360)
                maxValue360 = eBayPreGrandTotal;

            title360.Add(dr["pre_M"].ToString().Split(new char[]{' '})[0]);
        }


        listChart360.Add(new ChartItem()
        {
            Data = webValue,
            LabelColor = new KeyValuePair<string, string>("Web", _colorWeb)
        });
        listChart360.Add(new ChartItem()
        {
            Data = ebayValue,
            LabelColor = new KeyValuePair<string, string>("eBay", _coloreBay)
        });
        listChart360.Add(new ChartItem()
        {
            Data = webPreValue,
            LabelColor = new KeyValuePair<string, string>("Web last year", _colorWeb2)
        });
        listChart360.Add(new ChartItem()
        {
            Data = ebayPreValue,
            LabelColor = new KeyValuePair<string, string>("eBay last year", _colorEbay2)
        });

       // Response.Write("D");
        Response.Write(ExportChartBar(listChart360, string.Format(@"  web last year (the same term): {0}; web year total: {1};
eBay last year (the same term): {2}; eBay year total: {3}; "
                , lastYearWebTotal.ToString("##,###,##0.00$")
                , webTotal.ToString("##,###,##0.00$")
                , lastYearEbayTotal.ToString("##,###,##0.00$")
                , eBayTotal.ToString("##,###,##0.00$"))
            , title360.ToArray(), maxValue360+1000).ToPrettyString());
    }

    void GetStat30Days()
    {
        Stat s = new Stat();

        DataTable day30 = s.FindOrderTotalAgo30day();
        
        List<ChartItem> listChart30 = new List<ChartItem>();
        List<string> title30 = new List<string>();
        double maxValue30 = 0;

        List<double> webValue = new List<double>();
        List<double> webQty = new List<double>();
        List<double> ebayValue = new List<double>();

        double webTodayTotal = 0;
        decimal webCurrMonthTotal = s.CurrentMonthStatWeb();
        double ebayTodayTotal = 0;
        decimal ebayCurrMonthTotal = s.CurrentMonthStatEbay() ;

        for (int i = 0; i < day30.Rows.Count; i++)
        {
            DataRow dr = day30.Rows[i];
            double grandTotal;
            double.TryParse(dr["grand_total"].ToString(), out grandTotal);
            webValue.Add(grandTotal);
            webTodayTotal = grandTotal; // today

            if (grandTotal > maxValue30)
                maxValue30 = grandTotal;

            double eBayGrandTotal;
            double.TryParse(dr["eBayGrandTotal"].ToString(), out eBayGrandTotal);
            ebayValue.Add(eBayGrandTotal);
            ebayTodayTotal = eBayGrandTotal; // today

            if (eBayGrandTotal > maxValue30)
                maxValue30 = eBayGrandTotal;

            int qty;
            int.TryParse(dr["c"].ToString(), out qty);
            webQty.Add(qty);

            title30.Add(dr["D"].ToString());
        }

        listChart30.Add(new ChartItem()
        {
            Data = webValue,
            LabelColor = new KeyValuePair<string,string>("Web", _colorWeb)
        });
        listChart30.Add(new ChartItem()
        {
            Data = ebayValue,
            LabelColor = new KeyValuePair<string, string>("eBay", _coloreBay)
        });

        Response.Write(ExportChartBar(listChart30, string.Format(@"web {0} total: {1}; web today: {2}
ebay {0} total: {3}; eBay today;{4} "
                , DateTime.Now.ToString("yyyy/MM")
                , webCurrMonthTotal.ToString("##,###,##0.00$")
                , webTodayTotal.ToString("##,###,##0.00$")
                , ebayCurrMonthTotal.ToString("##,###,##0.00$")
                , ebayTodayTotal.ToString("##,###,##0.00$"))
            , title30.ToArray(), maxValue30+50).ToPrettyString());
    }

    void BindChart()
    {
        //Stat s = new Stat();
        //DataTable day30 = s.FindOrderTotalAgo30day();
        //DataTable dayYear = s.FindOrderStatByMonthAgo();

        //List<ChartItem> listChart30 = new List<ChartItem>();
        //List<ChartItem> listChart360 = new List<ChartItem>();
        //List<string> title30 = new List<string>();
        //List<string> title360 = new List<string>();
        //double maxValue30 = 0;
        //double maxValue360 = 0;

        //List<double> webValue = new List<double>();
        //List<double> webQty = new List<double>();
        //for (int i = 0; i < day30.Rows.Count; i++)
        //{
        //    DataRow dr = day30.Rows[i];
        //    double grandTotal;
        //    double.TryParse(dr["grand_total"].ToString(), out grandTotal);
        //    webValue.Add(grandTotal);

        //    if (grandTotal > maxValue30)
        //        maxValue30 = grandTotal;

        //    int qty;
        //    int.TryParse(dr["c"].ToString(), out qty);
        //    webQty.Add(qty);

        //    title30.Add(dr["D"].ToString());
        //}


        //listChart30.Add(new ChartItem()
        //{
        //    Data = webValue,
        //    LabelColor = "#FF0000"
        //});
        //listChart30.Add(new ChartItem()
        //{
        //    Data = webQty,
        //    LabelColor = "#46FF74"
        //});
        //for (int i = 0; i < dayYear.Rows.Count; i++)
        //{

        //}
        //Response.Write(maxValue30.ToString());
        //OpenFlashChartControl1.EnableCache = false;
        //OpenFlashChartControl1.Chart = ExportChartBar(listChart30, string.Format("30 Days Total: {0}", "33")
        //    , title30.ToArray(), maxValue30);

    }

    public OpenFlashChart.OpenFlashChart ExportChartBar(List<ChartItem> data, string title, string[] xLabels, double maxValue)
    {
        OpenFlashChart.OpenFlashChart chart = new OpenFlashChart.OpenFlashChart();
        chart.Title = new Title(title);
        
        for (int i = 0; i < data.Count; i++)
        {
            Bar bar = new OpenFlashChart.Bar();
           
            Random random = new Random();
            bar.Colour = data[i].LabelColor.Value;

            bar.Alpha = 0.7;
            bar.Text = data[i].LabelColor.Key;
            bar.FontSize = 12;
            bar.Values = data[i].Data;
            
            chart.AddElement(bar);
        }
        XAxis xaxis = new XAxis();
        xaxis.Labels.SetLabels(xLabels);
        xaxis.Labels.Vertical = data.Count !=4? true:false;
        xaxis.Labels.FontSize = 12;
        //xaxis.Steps = 1;
        //xaxis.Offset = true;
        //xaxis.SetRange(-2, 15);
        chart.X_Axis = xaxis;
        chart.X_Axis.Axis3D = 1;
        //YAxis yaxis = new YAxis();
        //yaxis.Steps = 4;
        //yaxis.SetRange(0, 20);
        //chart.Y_Axis = yaxis;
        chart.Y_Axis.SetRange(0, maxValue, 6);
        //bar.Tooltip = "提示:label:#x_label#<br>#top#<br>#bottom#<br>#val#";
        return chart;
    }
}