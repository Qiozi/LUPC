using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KKWStore.Helper
{
    public class ProfitStat
    {
        public static decimal StatMonthProfit(DateTime currDate
            , decimal profit
            , out decimal saleTotal
            , out decimal partCost
            , out decimal proxyCost
            , out decimal payCost
            , out decimal freeCost)
        {
            currDate = DateTime.Parse(currDate.ToString("yyyy-MM-01"));
            string beginDate = currDate.ToString("yyyyMMdd");
            string endDate = currDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");

            db.SqlExec.ExecuteNonQuery("Delete from tb_expend_average");

            string sql = string.Format(@"select ifnull(sum(total), 0) from (select 
         case when date_format(outBeginDate, '%Y%m%d') >= '{0}' and date_format(outEndDate, '%Y%m%d') <= '{1}' then outTotal
              when date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}' then (date_format('{1}', '%Y%j') - date_format('{0}', '%Y%j')+1) * average 
              when date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{0}' then (date_format(outEndDate, '%Y%j') - date_format('{0}', '%Y%j')+1) * average 
              when date_format(outBeginDate, '%Y%m%d') >=  '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}' then (date_format('{1}', '%Y%j') - date_format(outBeginDate, '%Y%j')+1) * average
              
              else  100000000 end total      
    from tb_expend where (date_format(outEndDate, '%Y%m%d') >= {0} and date_format(outEndDate, '%Y%m%d') <= '{1}') 
	or (date_format(outBeginDate, '%Y%m%d') <='{1}' and date_format(outBeginDate, '%Y%m%d') >= '{0}')
    or (date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}')
) t", beginDate, endDate);
            DataTable dtSum = db.SqlExec.ExecuteDataTable(sql);
            string t = dtSum.Rows[0][0].ToString();
            //dr[i - 1] = t;

            DataTable dtSale = db.SqlExec.ExecuteDataTable(string.Format("Select ifnull(sum(saleTotal), 0) from tb_sales_total where date_format(saleDate,'%Y%m')='{0}'",
               currDate.ToString("yyyyMM")));

            DataTable dtPartCost = db.SqlExec.ExecuteDataTable(string.Format("select ifnull(sum(in_cost), 0) from tb_serial_no_and_p_code where date_format(out_regdate, '%Y%m') = '{0}' and IsReturnWholesaler=0 ",
                currDate.ToString("yyyyMM")));
            // 赠品
            DataTable dtFeeCost = db.SqlExec.ExecuteDataTable(string.Format("select ifnull(sum(s.in_cost), 0) from tb_serial_no_and_p_code s inner join tb_out_invoice_product o on s.SerialNO=o.SerialNO and o.IsFree=1 where date_format(s.out_regdate, '%Y%m') = '{0}' and s.IsReturnWholesaler=0 ",
               currDate.ToString("yyyyMM")));

            DataTable dtProxy = db.SqlExec.ExecuteDataTable(string.Format("Select ifnull(sum(saleTotal), 0) from tb_proxy where date_format(saleDate, '%Y%m')='{0}'",
               currDate.ToString("yyyyMM")));

            decimal.TryParse(dtSale.Rows[0][0].ToString(), out saleTotal);

            decimal.TryParse(dtPartCost.Rows[0][0].ToString(), out partCost);

            decimal.TryParse(dtProxy.Rows[0][0].ToString(), out proxyCost);

            decimal.TryParse(t, out payCost);

            decimal.TryParse(dtFeeCost.Rows[0][0].ToString(), out freeCost);

            profit = saleTotal - partCost - proxyCost - payCost;
            return profit;
        }

        /// <summary>
        /// 本月排除金额，用于现金流，重复计算
        /// </summary>
        /// <returns></returns>
        public static decimal StatMonthExclude(DateTime currDate) {
            currDate = DateTime.Parse(currDate.ToString("yyyy-MM-01"));
            string beginDate = currDate.ToString("yyyyMMdd");
            string endDate = currDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd");

            db.SqlExec.ExecuteNonQuery("Delete from tb_expend_average");

            string sql = string.Format(@"select ifnull(sum(total), 0) from (select 
         case when date_format(outBeginDate, '%Y%m%d') >= '{0}' and date_format(outEndDate, '%Y%m%d') <= '{1}' then outTotal
              when date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}' then (date_format('{1}', '%Y%j') - date_format('{0}', '%Y%j')+1) * average 
              when date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{0}' then (date_format(outEndDate, '%Y%j') - date_format('{0}', '%Y%j')+1) * average 
              when date_format(outBeginDate, '%Y%m%d') >=  '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}' then (date_format('{1}', '%Y%j') - date_format(outBeginDate, '%Y%j')+1) * average
              
              else  100000000 end total      
    from tb_expend where regdate >= '2016-08-01' and ( (date_format(outEndDate, '%Y%m%d') >= {0} and date_format(outEndDate, '%Y%m%d') <= '{1}') 
	or (date_format(outBeginDate, '%Y%m%d') <='{1}' and date_format(outBeginDate, '%Y%m%d') >= '{0}')
    or (date_format(outBeginDate, '%Y%m%d') <= '{0}' and date_format(outEndDate, '%Y%m%d') >= '{1}') )
) t", beginDate, endDate);
            DataTable dtSum = db.SqlExec.ExecuteDataTable(sql);
            string t = dtSum.Rows[0][0].ToString();
            //dr[i - 1] = t;



            DataTable dtPartCost = db.SqlExec.ExecuteDataTable(string.Format("select ifnull(sum(in_cost), 0) from tb_serial_no_and_p_code where in_regdate >= '2016-08-01' and date_format(out_regdate, '%Y%m') = '{0}' and IsReturnWholesaler=0 ",
                currDate.ToString("yyyyMM")));
            // 赠品
            DataTable dtFeeCost = db.SqlExec.ExecuteDataTable(string.Format("select ifnull(sum(s.in_cost), 0) from tb_serial_no_and_p_code s inner join tb_out_invoice_product o on s.SerialNO=o.SerialNO and o.IsFree=1 where in_regdate >= '2016-08-01' and date_format(s.out_regdate, '%Y%m') = '{0}' and s.IsReturnWholesaler=0 ",
               currDate.ToString("yyyyMM")));

            var partCost = 0M;
            decimal.TryParse(dtPartCost.Rows[0][0].ToString(), out partCost);

            var payCost = 0M;
            decimal.TryParse(t, out payCost);
            var freeCost = 0M;
            decimal.TryParse(dtFeeCost.Rows[0][0].ToString(), out freeCost);

            // profit= partCost + freeCost + payCost;
            return  partCost + payCost;
        }
    }
}
