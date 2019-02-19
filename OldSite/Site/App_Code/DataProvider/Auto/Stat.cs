using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for Stat
/// </summary>
public class Stat
{
    public Stat()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    /// <summary>
    /// stat user login , order count, new user count
    /// </summary>
    /// <returns></returns>
    public DataTable FindOrderLoginUserInfo()
    {
        return Config.ExecuteDataTable(@"select 'Today' title
	,(select count(customer_serial_no)  from tb_customer where date_format(`create_datetime`,'%Y%j') =date_format(date_sub(current_date, interval 0 day), '%Y%j') ) new_users
	,(select count(login_log_serial_no) from tb_login_log  where date_format(login_datetime,'%Y%j') =date_format(date_sub(current_date, interval 0 day), '%Y%j') ) user_login
	,count(order_helper_serial_no) orders, sum(grand_total) grand_total from tb_order_helper where date_format(`order_date`,'%Y%j') =date_format(date_sub(current_date, interval 0 day), '%Y%j') and  is_ok=1

union all
select 'Yestoday'
	,(select count(customer_serial_no)  from tb_customer where date_format(`create_datetime`,'%Y%j') =date_format(date_sub(current_date, interval 1 day), '%Y%j') ) new_users
	,(select count(login_log_serial_no) from tb_login_log  where date_format(login_datetime,'%Y%j') =date_format(date_sub(current_date, interval 1 day), '%Y%j') ) user_login
	,count(order_helper_serial_no) orders, sum(grand_total) grand_total from tb_order_helper where date_format(`order_date`,'%Y%j') =date_format(date_sub(current_date, interval 1 day), '%Y%j') and  is_ok=1
union all
select 'Last 30 Days'
	,(select count(customer_serial_no)  from tb_customer where date_format(`create_datetime`,'%Y%j') >=date_format(date_sub(current_date, interval 30 day), '%Y%j') ) new_users
	,(select count(login_log_serial_no) from tb_login_log  where date_format(login_datetime,'%Y%j') >=date_format(date_sub(current_date, interval 30 day), '%Y%j') ) user_login
	,count(order_helper_serial_no) orders, sum(grand_total) grand_total from tb_order_helper where date_format(`order_date`,'%Y%j') >=date_format(date_sub(current_date, interval 30 day), '%Y%j') and  is_ok=1

");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public DataTable FindOrderTotalAgo30day()
    {
        return Config.ExecuteDataTable(@"
select 	grand_total, D, C , eBayGrandTotal, eBayC
	from 
	tb_order_total_any_day_ago 
            union all
select ifnull(sum(grand_total),0) grand_total, date_format(date_sub(current_date, interval 0 day), '%W %b %d %Y')  D, count(order_helper_serial_no) c 
, (select ifnull(sum(grand_total),0) grand_total from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and date_format(order_date,'%Y%j') =date_format(date_sub(current_date, interval 0 day), '%Y%j')) 
, (select count(order_helper_serial_no) c from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and date_format(order_date,'%Y%j') =date_format(date_sub(current_date, interval 0 day), '%Y%j')) 
from tb_order_helper where date_format(order_date,'%Y%j') =date_format(date_sub(current_date, interval 0 day), '%Y%j') and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and tag=1 and is_ok=1 and order_source in (0,1,2)

");
    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public decimal CurrentMonthStatWeb()
    {
        return decimal.Parse(Config.ExecuteScalar("select ifnull(sum(grand_total),0) eBayGrandTotal from tb_order_helper where  pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and tag=1 and is_ok=1 and order_source in (0,1,2) and date_format(order_date,'%Y%m') = '" + DateTime.Now.ToString("yyyyMM") + "'").ToString());

    }
    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public decimal CurrentMonthStatEbay()
    {
        return decimal.Parse(Config.ExecuteScalar("select ifnull(sum(grand_total),0) eBayGrandTotal from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and date_format(order_date,'%Y%m') = '" + DateTime.Now.ToString("yyyyMM") + "'").ToString());
    }

    public DataTable FindCurrentMonthStat()
    {
        return Config.ExecuteDataTable(@"
select ifnull(sum(grand_total),0) grand_total, date_format(date_sub(current_date, interval 0 day), '%W %b %d %Y')  D, count(order_helper_serial_no) c from tb_order_helper where date_format(order_date,'%Y%j') =date_format(date_sub(current_date, interval 0 day), '%Y%j') and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and tag=1 and is_ok=1 and order_source in (0,1,2)
 union all
select 	grand_total, D, C 
	from  tb_order_total_any_day_ago where date_format(current_date,'%b')=date_format(DT,'%b')");

    }

    /// <summary>
    /// 30 day ago
    /// </summary>
    public void GenerateOrderStat30DayAgo()
    {
        Config.ExecuteNonQuery("delete from tb_order_total_any_day_ago");

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append(@"insert into tb_order_total_any_day_ago 
	(grand_total, D, C, DT, ebayGrandTotal, eBayC)");
        for (int i = 29; i > 0; i--)
        {
            //            sb.Append(@"select ifnull(sum(grand_total),0) grand_total, date_format(date_sub(current_date, interval " + i.ToString() + @" day), '%W %b %d %Y')  D, count(*) c,max(order_date) DT from tb_order_helper where date_format(order_date,'%Y%j') =date_format(date_sub(current_date, interval " + i.ToString() + @" day), '%Y%j') and out_status <> 6 and tag=1 and is_ok=1
            //            union all
            //");
            if (i == 1)
                sb.Append(@"select ifnull(sum(grand_total),0) grand_total
, date_format(date_sub(current_date, interval " + i.ToString() + @" day), '%W %b %d %Y')  D
, count(order_helper_serial_no) c
, max(create_datetime) DT
, (select ifnull(sum(grand_total),0) eBayGrandTotal from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and date_format(order_date,'%Y%j') =date_format(date_sub(current_date, interval " + i.ToString() + @" day), '%Y%j'))
, (select count(order_helper_serial_no) c from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and date_format(order_date,'%Y%j') =date_format(date_sub(current_date, interval " + i.ToString() + @" day), '%Y%j'))

from tb_order_helper where date_format(order_date,'%Y%j') =date_format(date_sub(current_date, interval " + i.ToString() + @" day), '%Y%j') and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and tag=1 and is_ok=1 and order_source in (0,1,2)
");
            else
                sb.Append(@"select ifnull(sum(grand_total),0) grand_total
, date_format(date_sub(current_date, interval " + i.ToString() + @" day), '%W %b %d %Y')  D
, count(order_helper_serial_no) c
, max(create_datetime) DT
, (select ifnull(sum(grand_total),0) eBayGrandTotal from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and date_format(order_date,'%Y%j') =date_format(date_sub(current_date, interval " + i.ToString() + @" day), '%Y%j'))
, (select count(order_helper_serial_no) c from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and date_format(order_date,'%Y%j') =date_format(date_sub(current_date, interval " + i.ToString() + @" day), '%Y%j'))

from tb_order_helper where date_format(order_date,'%Y%j') =date_format(date_sub(current_date, interval " + i.ToString() + @" day), '%Y%j') and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and tag=1 and is_ok=1 and order_source in (0,1,2)

            union all
");
        }
        Config.ExecuteNonQuery(sb.ToString());
    }

    /// <summary>
    /// by year Stat.
    /// </summary>
    public void GenerateOrderStatByMonthAgo()
    {
        DataTable dt = Config.ExecuteDataTable("select id from tb_order_total_month");
        foreach (DataRow dr in dt.Rows)
        {
            int i = int.Parse(dr[0].ToString());

            string month = "";
            string pre_month = "";
            if (i < 10)
            {
                month = DateTime.Now.Year.ToString() + "0" + i.ToString();
                pre_month = (DateTime.Now.Year - 1).ToString() + "0" + i.ToString();
            }
            else
            {
                month = DateTime.Now.Year.ToString() + i.ToString();
                pre_month = (DateTime.Now.Year - 1).ToString() + i.ToString();
            }

            DataTable Sub1 = Config.ExecuteDataTable(string.Format(@"
select ifnull(sum(grand_total),0) grand_total
, max(date_format(order_date, '%b'))  M

, (select ifnull(sum(grand_total),0) eBayGrandTotal from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and date_format(order_date,'%Y%m') ='{0}') eBayGrandTotal
, (select count(order_helper_serial_no) c from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and  date_format(order_date,'%Y%m') ='{0}') eBayC


, count(order_helper_serial_no) c from tb_order_helper where date_format(order_date,'%Y%m') ='{0}'
  and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") 
  and out_status not in (" + Config.notStatOrderStatus_back_status + @") 
  and tag=1 and is_ok=1 and order_source in (0,1,2)
"
                           , month));
            DataTable preSub1 = Config.ExecuteDataTable(string.Format(@"
select ifnull(sum(grand_total),0) grand_total
, max(date_format(order_date, '%b'))  M

, (select ifnull(sum(grand_total),0) eBayGrandTotal from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and date_format(order_date,'%Y%m') ='{0}') eBayGrandTotal
, (select count(order_helper_serial_no) c from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and date_format(order_date,'%Y%m') ='{0}') eBayC


, count(order_helper_serial_no) c from tb_order_helper where date_format(order_date,'%Y%m') ='{0}'
  and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") 
  and out_status not in (" + Config.notStatOrderStatus_back_status + @") 
  and tag=1 and is_ok=1 and order_source in (0,1,2)"
                           , pre_month));


            if (preSub1.Rows.Count == 1)
            {
                Config.ExecuteNonQuery(string.Format(@"
Update tb_order_total_month set 
grand_total='{0}'
, M='{1}'
, c='{2}'
, pre_grand_total='{3}'
, pre_M='{4}'
, pre_C='{5}'
, eBayGrandTotal = '{7}'
, eBayC = '{8}'
, Pre_eBayGrandTotal = '{9}'
, Pre_eBayC = '{10}'
where id='{6}'"
                    , Sub1.Rows[0]["grand_total"].ToString()
                    , string.IsNullOrEmpty(Sub1.Rows[0]["M"].ToString()) ? GetMonth(i - 1) : Sub1.Rows[0]["M"].ToString()
                    , Sub1.Rows[0]["c"].ToString()
                    , preSub1.Rows[0]["grand_total"].ToString()
                    , string.IsNullOrEmpty(preSub1.Rows[0]["M"].ToString()) ? GetMonth(i - 1) : preSub1.Rows[0]["M"].ToString()
                    , preSub1.Rows[0]["C"].ToString()
                    , i
                    , Sub1.Rows[0]["eBayGrandTotal"].ToString()
                    , Sub1.Rows[0]["eBayC"].ToString()
                    , preSub1.Rows[0]["eBayGrandTotal"].ToString()
                    , preSub1.Rows[0]["eBayC"].ToString()
                    ));

            }

        }
    }

    public string GetMonth(int monthIndex)
    {
        return new string[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" }[monthIndex].ToString();
    }

    /// <summary>
    /// Get this year and last monthly statistics
    /// </summary>
    /// <returns></returns>
    public DataTable FindOrderStatByMonthAgo()
    {
        // GenerateOrderStatByMonthAgo();
        DataTable dt = Config.ExecuteDataTable(@"select ifnull(sum(grand_total),0) grand_total
, date_format(date_sub(current_date, interval 0 Month), '%b')  M
, count(order_helper_serial_no) c 
, (select ifnull(sum(grand_total),0) eBayGrandTotal from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @")  and date_format(order_date,'%Y%b') =date_format(date_sub(current_date,interval 0 month), '%Y%b')) eBayGrandTotal
, (select count(order_helper_serial_no) c from tb_order_helper where order_source=3 and pre_status_serial_no not in (" + Config.notStatOrderStatus + @")  and date_format(order_date,'%Y%b') =date_format(date_sub(current_date,interval 0 month), '%Y%b')) eBayC

from tb_order_helper where date_format(order_date,'%Y%b') =date_format(date_sub(current_date,interval 0 month), '%Y%b') and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") and tag=1 and is_ok=1 and order_source in (0,1,2)
 ");
        if (dt.Rows.Count == 1)
        {
            Config.ExecuteNonQuery(string.Format(@"Update tb_order_total_month set grand_total='{0}'
, M='{1}'
, c='{2}'
, eBayGrandTotal='{4}'
, eBayC ='{5}'
where id = '{3}'"
                , dt.Rows[0]["grand_total"].ToString()
                , dt.Rows[0]["M"].ToString()
                , dt.Rows[0]["c"].ToString()

                , DateTime.Now.Month
                , dt.Rows[0]["eBayGrandTotal"].ToString()
                , dt.Rows[0]["eBayC"].ToString()));
        }
        return Config.ExecuteDataTable(@"
select * from tb_order_total_month");
    }

    /// <summary>
    /// 获取去年同期统计
    /// </summary>
    /// <param name="lastYearWebTotal"></param>
    /// <param name="lastYearEbayTotal"></param>
    public void FindOrderStatLastYearSameTerm(ref double lastYearWebTotal, ref double lastYearEbayTotal)
    {

        lastYearEbayTotal = double.Parse(Config.ExecuteScalar(string.Format(@"
select ifnull(sum(grand_total),0) eBayGrandTotal 
from tb_order_helper 
where order_source=3 
and to_days(order_date) <= to_days(date_sub(current_date,interval 1 year))
and pre_status_serial_no not in (" + Config.notStatOrderStatus + @")
and date_format(order_date,'%Y') =  date_format(date_sub(current_date,interval 1 year), '%Y')
")).ToString());

        lastYearWebTotal = double.Parse(Config.ExecuteScalar(string.Format(@"
select ifnull(sum(grand_total),0) grand_total

from tb_order_helper 
where 
to_days(order_date) <= to_days(date_sub(current_date,interval 1 year)) 
and date_format(order_date,'%Y') =  date_format(date_sub(current_date,interval 1 year), '%Y')
and pre_status_serial_no not in (" + Config.notStatOrderStatus + @") 
and tag=1 and is_ok=1 and order_source in (0,1,2) 
")).ToString());
    }

    /// <summary>
    /// delete invalid data, 
    /// cart_temp
    /// track
    /// ebay send xml data.
    /// </summary>
    public void DeleteInvalidData()
    {
        Config.ExecuteNonQuery(@"
delete from tb_ebay_send_xml_history where date_format(now(), '%y%j') - date_format(regdate, '%y%j') > 30;

delete from tb_ebay_send_xml_result_history where date_format(now(), '%y%j') - date_format(regdate, '%y%j') > 30;

delete from tb_cart_temp where date_format(now(), '%y%j') - date_format(create_datetime, '%y%j') > 60;
delete from tb_cart_temp_price where date_format(now(), '%y%j') - date_format(create_datetime, '%y%j') > 60;

delete from tb_track where date_format(now(), '%y%j') - date_format(track_regdate, '%y%j') > 60;


");


        //        DataTable dt = Config.ExecuteDataTable(@"
        //SET SQL_BIG_SELECTS=1;
        //select SKU from (select ebay_code, SKU from tb_ebay_code_and_luc_sku where is_sys=1) ec 
        //left join (select item_number from tb_order_ebay) oe on ec.ebay_code = oe.item_number 
        //left join (select distinct systemsku onlineSku from tb_ebay_system_and_category) s on s.onlineSku = ec.sku
        //inner join (select id from tb_ebay_system where date_format(now(), ""%Y%j"")-date_format(regdate, ""%Y%j"")>60) es on es.id = ec.SKU
        //where s.onlineSku is null and oe.item_number is null;");

        //        foreach (DataRow dr in dt.Rows)
        //        {

        //            Config.ExecuteNonQuery("delete from tb_ebay_system where id = '" + dr["SKU"].ToString() + "'");
        //            Config.ExecuteNonQuery("delete from tb_ebay_system_parts where system_sku = '" + dr["SKU"].ToString() + "'");
        //        }

    }
}
